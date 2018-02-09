
/***************************************************************************

                                 ChessV

                  COPYRIGHT (C) 2012-2017 BY GREG STRONG
  
  THIS FILE DERIVED FROM CUTE CHESS BY ILARI PIHLAJISTO AND ARTO JONSSON

This file is part of ChessV.  ChessV is free software; you can redistribute
it and/or modify it under the terms of the GNU General Public License as 
published by the Free Software Foundation, either version 3 of the License, 
or (at your option) any later version.

ChessV is distributed in the hope that it will be useful, but WITHOUT ANY 
WARRANTY; without even the implied warranty of MERCHANTABILITY or 
FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for 
more details; the file 'COPYING' contains the License text, but if for
some reason you need a copy, please visit <http://www.gnu.org/licenses/>.

****************************************************************************/

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ChessV
{
	public enum PlayerState
	{
		NotStarted,		//	Not started or uninitialized
		Starting,		//	Starting or initializing
		Idle,			//	Idle and ready to start a game
		Observing,		//	Observing a game, or waiting for turn
		Thinking,		//	Thinking of the next move
		FinishingGame,	//	Finishing or cleaning up after a game
		Disconnected	//	Disconnected or terminated
	}

	// delegates for the event handlers used by the Player class
	public delegate void DisconnectedEventHandler( Player sender );
	public delegate void ReadyEventHandler( Player sender );
	public delegate void StoppedThinkingEventHandler();
	public delegate void MoveMadeEventHandler( Player sender, List<Movement> moves );
	public delegate void StartedThinkingEventHandler( long timeleft );
	public delegate void ResultClaimEventHandler( Player sender, Result result );
	public delegate void NameChangedEventHandler( string name );
	public delegate void DebugMessageEventHandler( string data );

	public abstract class Player
	{
		// *** PUBLIC PROPERTIES *** //

		public string Name { get; set; }

		public Game Game { get; protected set; }

		public Player Opponent { get; protected set; }

		//	the side of the player
		public int Side { get; protected set; }

		public TimeControl TimeControl { get; set; }

		public abstract bool IsHuman { get; }

		public IDebugMessageLog MessageLog { get; protected set; }

		//	true if the player is ready for input.  
		//	NOTE: when the player's state is \a Disconnected, this
		//	function still returns true if all the cleanup following
		//	the disconnection is done.
		public virtual bool IsReady
		{
			get
			{
				switch( State )
				{
					case PlayerState.Idle:
					case PlayerState.Observing:
					case PlayerState.Thinking:
					case PlayerState.Disconnected:
						return true;
					default:
						return false;
				}
			}
		}

		public PlayerState State
		{
			get
			{ return state; }

			set
			{
				if( state == value )
					return;
				if( state == PlayerState.Thinking )
					StoppedThinking();
				state = value;
			}
		}

		public MoveEvaluation Evaluation;


		// *** CONSTRUCTION *** //

		public Player( IDebugMessageLog messageLog, TimerFactory timerFactory )
		{
			State = PlayerState.NotStarted;
			this.timer = timerFactory.NewTimer();
			claimedResult = false;
			ClaimsValidated = true;
			Game = null;
			Opponent = null;
			Evaluation = new MoveEvaluation();
			MessageLog = messageLog;
			DebugMessage += onDebugMessage;

			StartedThinking += delegate { };
			StoppedThinking += delegate { };
			MoveMade += delegate { };
		}


		// *** OPERATIONS *** //

		//	Prepares the player for a new chess game, and then calls
		//	startGame() to start the game.
		//
		//	param side: The side (color) the player should play as. It
		//	can be NoSide if the player is in force/observer mode.
		//	param opponent: The opposing player.
		//	param board: The chessboard on which the game is played.
		public void NewGame( int side, Player opponent, Game game )
		{
			//Q_ASSERT( opponent != 0 );
			//Q_ASSERT( board != 0 );
			//Q_ASSERT( isReady() );
			//Q_ASSERT( m_state != Disconnected );

			claimedResult = false;
			Evaluation.Clear();
			Opponent = opponent;
			Game = game;
			Side = side;
			TimeControl.Initialize();

			State = PlayerState.Observing;
			startGame();
		}

		public void AttachGame( int side, Player opponent, Game game, TimeControl timeControl )
		{
			claimedResult = false;
			Evaluation.Clear();
			Opponent = opponent;
			Game = game;
			Side = side;
			TimeControl = timeControl;
			if( side == game.CurrentSide )
				State = PlayerState.Thinking;
			else
				State = PlayerState.Observing;
		}

		//	Tells the player that the game ended by result.
		//	NOTE: Subclasses that reimplement this function must call
		//	the base implementation.
		public virtual void EndGame( Result result )
		{
			if( State != PlayerState.Observing && State != PlayerState.Thinking )
				return;

			//Q_ASSERT( m_state != Disconnected );
			State = PlayerState.FinishingGame;
			Game = null;
			timer.Stop();
			Ready -= Go;

		}

//		const MoveEvaluation& evaluation() const;

		//	Sends the next move of an ongoing game to the player.
		//	If the player is in force/observer mode, the move wasn't
		//	necessarily made by the opponent.
		public abstract void MakeMove( List<Movement> move );

		//	Forces the player to play move as its next move
		public void MakeBookMove( List<Movement> move )
		{
			TimeControl.StartTimer();
			MakeMove( move );
			TimeControl.Update();
			Evaluation.IsBookEval = true;

			MoveMade( this, move );
		}

		public bool ClaimsValidated { get; set; }

		public abstract bool SupportsVariant( string variant );


		//	Waits (without blocking) until the player is ready,
		//	starts the chess clock, and tells the player to start thinking
		//	of the next move.
		//	NOTE: Subclasses that reimplement this function must call
		//	the base implementation.
		public virtual void Go( Player sender )
		{
			if( State == PlayerState.Disconnected )
				return;
			State = PlayerState.Thinking;

			Ready -= Go;
			if( !IsReady )
			{
				Ready += Go; //connect( this, SIGNAL( ready() ), this, SLOT( go() ) );
				return;
			}

			//Q_ASSERT( m_board != 0 );
			Side = Game.CurrentSide;

			startClock();
			startThinking();
		}

		//	Terminates the player non-violently
		public virtual void Quit() 
		{
			State = PlayerState.Disconnected;
			if( Disconnected != null )
				Disconnected( this );
		}

		//	Kills the player process or connection, causing it to
		//	exit immediately.  The player's state is set to Disconnected.
		//	NOTE: Subclasses that reimplement this function must call
		//	the base implementation.
		public virtual void Kill() 
		{
			State = PlayerState.Disconnected;
			if( Disconnected != null )
				Disconnected( this );
		}

		public abstract void StopThinking();



		// *** EVENTS *** //

		//	This signal is emitted when the player disconnects
		public event DisconnectedEventHandler Disconnected;

		//	Signals that the player is ready for input
		public event ReadyEventHandler Ready;

		//	Signals the time left in the player's clock when they
		//	start thinking of their next move.
		public event StartedThinkingEventHandler StartedThinking;

		//	This signal is emitted when the player stops thinking of
		//	a move. Note that it doesn't necessarily mean that the
		//	player has made a move - they could've lost the game on
		//	time, disconnected, etc.
		public event StoppedThinkingEventHandler StoppedThinking;

		//	Signals the player's move
		public event MoveMadeEventHandler MoveMade;

		//	Emitted when the player claims the game to end
		//	with result.
		public event ResultClaimEventHandler ResultClaim;

		//	Signals a debugging message from the player
		public event DebugMessageEventHandler DebugMessage;

		//	Emitted when player's name is changed
		public event NameChangedEventHandler NameChanged;


		// *** EVENT TRIGGERS *** //

		protected void disconnected()
		{ Disconnected( this ); }

		protected void ready()
		{ if( Ready != null ) Ready( this ); }

		protected void startedThinking( int timeleft )
		{ StartedThinking( timeleft ); }

		protected void stoppedThinking()
		{ StoppedThinking(); }

		protected void moveMade( List<Movement> move )
		{ MoveMade( this, move ); }

		protected void resultClaim( Result result )
		{ ResultClaim( this, result ); }

		protected void debugMessage( string message )
		{ DebugMessage( message ); }

		protected void nameChanged( string name )
		{ NameChanged( name ); }
		

		// *** HELPER FUNCITONS *** //

		// This function is attached to a timer's "Tick" event handler to make 
		// the timer a "single shot" timer.  As soon as the timer ticks, this 
		// function will be called and will deactivate the timer
		protected void onSingleShotTimerTick( object sender, System.EventArgs e )
		{
			((Timer) sender).Stop();
		}

		//	Called when the player's process or connection
		//	crashes unexpectedly.  This forfeits the game.
		protected virtual void onCrashed( object sender, System.EventArgs e )
		{
			Kill();
			forfeit( ResultType.Disconnection );
		}

		//	Called when the player's flag falls.
		//	This forfeits the game.
		protected virtual void onTimeout()
		{
			forfeit( ResultType.Timeout );
		}

		protected void onDebugMessage( string message )
		{
			MessageLog.DebugMessage( message );
		}

		//	Starts the chess game set up by newGame()
		protected abstract void startGame();

		//	Tells the player to start thinking of the next move.
		//	The player is guaranteed to be ready to move when this
		//	function is called by go().
		protected abstract void startThinking();

		//	Emits the resultClaim() signal with result
		protected void claimResult( Result result )
		{
			if( claimedResult )
				return;

			timer.Stop();
			if( State == PlayerState.Thinking )
				State = PlayerState.Observing;
			claimedResult = true;

			ResultClaim( this, result );
		}

		//	Emits the resultClaim() signal with a result of type
		//	type, description description, and this player
		//	as the loser.
		protected void forfeit( ResultType type, string description = null )
		{
			if( Side == -1 )
			{
				//qWarning("Player %s has no side", qPrintable(name()));
				return;
			}

			claimResult( new Result( type, Side ^ 1, description ) );
		}

		//	Emits the player's move, and a timeout signal if the
		//	move came too late.
		protected void emitMove( List<Movement> moves )
		{
			if( State == PlayerState.Thinking )
				State = PlayerState.Observing;

			TimeControl.Update();
			Evaluation.Time = TimeControl.LastMoveTime;

			timer.Stop();
			if( false && TimeControl.Expired ) /* TODO: enforce human time control? */
			{
				forfeit( ResultType.Timeout );
				return;
			}

			MoveMade( this, moves );
		}

		private void startClock()
		{
			if( State != PlayerState.Thinking )
				return;

			Evaluation.Clear();

			if( TimeControl.IsValid )
				StartedThinking( TimeControl.TimeLeft );

			TimeControl.StartTimer();

			if( !TimeControl.Infinite )
			{
				long t = TimeControl.TimeLeft + TimeControl.ExpiryMargin;
				timer.Interval = (int) ((t >= 0 ? t : 0) + 200); /* Warning - unsafe cast */
				timer.Start();
			}
		}


		// *** PROTECTED DATA MEMBERS *** //

		protected object eval;
		protected PlayerState state;
		protected Timer timer;
		protected bool claimedResult;
	}
}
