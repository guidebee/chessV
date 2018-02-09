
/***************************************************************************

                                 ChessV

                  COPYRIGHT (C) 2012-2017 BY GREG STRONG

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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ChessV;

namespace ChessV.GUI
{
	//**********************************************************************
	//
	//                             BoardControl
	//
	//
	//  This is a UserControl that handles the graphical representation of 
	//  the board(s) for a Game.  It receives user moves with mouse clicks 
	//  and reports them back to the Game.

	public partial class BoardControl: UserControl
	{
		// *** PROPERTIES *** //

		//	BoardPresentation encapsulating the logic for rendering the board
		public BoardPresentation Presentation { get; private set; }

		//	highlight the last move made unless we made it with this control
        public bool HighlightMove { get; set; }

		//	true if rendering from second player's perspective
		public bool RotateBoard { get; set; }

		//	Theme to use for rendering (color schemes, etc.)
		public Theme Theme { get; private set; }


		//	protected members
		protected Game game;
		protected Board board;
		protected Form owningForm;
		protected MouseEventArgs lastMouseDown;
		protected DateTime lastMouseDownTime;
		protected Piece pieceBeingLifted;
		protected MouseEventArgs mouseDownEventArg;


		// *** CONSTRUCTION *** //

		public BoardControl()
		{
			InitializeComponent();

			this.SetStyle( ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true );
		}

		public void Initialize( Game game, Board board, Theme theme, Form owningForm = null )
		{
			this.game = game;
			this.board = board;
			this.Theme = theme;
			this.owningForm = owningForm;
            HighlightMove = true;
			RotateBoard = false;
			Presentation = PresentationFactory.CreatePresentation( game, theme );
		}


		// *** OPERATIONS *** //

		public Location GetLocationAtCoordinate( Point pt )
		{
			Presentation.MouseCoordinate = pt;
			return Presentation.GetMouseLocation( RotateBoard );
		}


		// **************************************** //
		// ***                                  *** //
		// ***          Event Handlers          *** //
		// ***                                  *** //
		// **************************************** //

		//	Paint event handler
		private void BoardControl_Paint( object sender, PaintEventArgs e )
		{
			if( Presentation != null )
			{
				Bitmap boardPresentation = Presentation.Render( HighlightMove, RotateBoard );
				e.Graphics.DrawImageUnscaled( boardPresentation, new Point() );
				return;
			}
		}

		//	Mouse Down event handler
		private void BoardControl_MouseDown( object sender, MouseEventArgs e )
		{
			if( Presentation.PieceLifted == null && !((GameForm) owningForm).ReviewMode )
			{
				Presentation.MouseCoordinate = e.Location;
				Location location = Presentation.GetMouseLocation( RotateBoard );
				if( !location.IsNull )
				{
					Piece piece = board[board.LocationToSquare( location )];
					if( piece != null )
					{
						//	check for right-button (which opens context menu)
						if( e.Button == MouseButtons.Right )
						{
							if( owningForm != null && owningForm is GameForm )
							{
								((GameForm) owningForm).ShowContextMenu( PointToScreen( e.Location ), piece );
								return;
							}
							return;
						}

						//	if the game is over, just return
						if( !game.Result.IsNone )
							return;

						//	see if this piece has any legal moves
						MoveInfo[] moves;
						int nMoves = game.GetRootMoves( out moves, piece );
						if( nMoves == 0 )
						{
							//	this piece can't move; determine appropriate explanation
							if( piece.Player != game.CurrentSide )
								MessageBox.Show( "That is not your piece to move" );
							else
								MessageBox.Show( "That piece cannot move at this time" );
							return;
						}

						//	record MouseDown event time - if we get a MouseClick quickly 
						//	enough, we'll consider this a click-and-click-again type movement 
						//	rather than a click-drag type movement.
						lastMouseDownTime = DateTime.Now;

						//	we'll also store some additional information and set the timer.
						//	if we don't get a MouseClick event that corresponds with this 
						//	quickly, we'll initiate the actual click-drag type movement.
						//	we don't want to do this prematurely because we don't want the 
						//	picture of the piece to "jump" on a simple click event.
						pieceBeingLifted = piece;
						mouseDownEventArg = e;
						timerPieceLiftTimer.Start();
					}
				}
			}
		}

		//	Mouse Move event handler
		private void BoardControl_MouseMove( object sender, MouseEventArgs e )
		{
			if( Presentation != null && Presentation.PieceLifted != null )
			{
				Presentation.MouseCoordinate = e.Location;
				Invalidate();
			}
		}

		//	Mouse Up event handler
		private void BoardControl_MouseUp( object sender, MouseEventArgs e )
		{
			//	MouseUp is only meaningful if a piece is lifted and being dragged
			if( Presentation.PieceLifted != null && Presentation.LiftedPieceIsDragged )
			{
				//	Find board location over which the mouse was released
				Location location = Presentation.GetMouseLocation( RotateBoard );
				if( !location.IsNull )
					TryMoveToLocation( location );
			}
		}

		private void BoardControl_MouseClick( object sender, MouseEventArgs e )
		{
			//	see if this click goes with a (just processed) mouse down event 
			//	which corresponded with lifting a piece for movement.  if so, 
			//	we convert this movement input from a hold-and-drag type to 
			//	a click and click again type.
			if( Presentation.PieceLifted != null && Presentation.LiftedPieceIsDragged &&
				(DateTime.Now - lastMouseDownTime).TotalMilliseconds < 500 &&
				Math.Abs( e.X - lastMouseDown.X ) < 4 && Math.Abs( e.Y - lastMouseDown.Y ) < 4 )
			{
				//	stop the timer set during MouseDown since we now know 
				//	that this is not a click-drag type piece movement
				timerPieceLiftTimer.Stop();
				//	inform the Presentation that we have a piece lifted 
				//	and invalidate so it draws appropriately.
				Presentation.LiftedPieceIsDragged = false;
				//	redraw the board control
				Invalidate();
			}
			else
			{
				//	the other click of interest is if we are already performing 
				//	a click and click again type move and this is the second click.
				if( Presentation.PieceLifted != null && !Presentation.LiftedPieceIsDragged )
				{
					//	Find board location over which the mouse was released
					Location location = Presentation.GetMouseLocation( RotateBoard );
					if( !location.IsNull )
						TryMoveToLocation( location );
				}
			}
		}

		private void TryMoveToLocation( Location location )
		{
			//	is the current square one to which the lifted piece can move?
			int currentSquare = board.LocationToSquare( location );
			MoveInfo[] moves;
			int nMoves = game.GetRootMoves( out moves, Presentation.PieceLifted );
			List<MoveInfo> applicableMoves = new List<MoveInfo>();
			for( int x = 0; x < nMoves; x++ )
				if( moves[x].PieceMoved == Presentation.PieceLifted && moves[x].ToSquare == currentSquare )
					applicableMoves.Add( moves[x] );

			Presentation.PieceLifted = null;
			if( applicableMoves.Count > 0 )
			{
				MoveInfo selectedMove = applicableMoves[0];
				if( applicableMoves.Count > 1 )
				{
					//	there are multiple possible moves between these coordinates 
					//	so ask the user with a MoveSelectForm
					MoveSelectForm frm = new MoveSelectForm( game, applicableMoves );
					frm.ShowDialog();
					selectedMove = frm.SelectedMove;
				}
				if( game.CurrentPlayer is HumanPlayer )
					((HumanPlayer) game.CurrentPlayer).OnHumanMove( new List<Movement>() { selectedMove }, game.CurrentSide );
			}
			//	Presentation.Update();
			Invalidate();
		}

		private void timerPieceLiftTimer_Tick( object sender, EventArgs e )
		{
			//	if the timer is firing that means that we got a MouseDown event 
			//	without a quick MouseClick event, so we can now engage the actual 
			//	movement of a piece by the click-drag method.  before this, we 
			//	didn't know for sure if it was a click-drag movement or a 
			//	click-and-click again movement.
			timerPieceLiftTimer.Stop();
			Presentation.PieceLifted = pieceBeingLifted;
			Presentation.LiftedPieceIsDragged = true;
			lastMouseDown = mouseDownEventArg;
			Invalidate();
		}
	}
}
