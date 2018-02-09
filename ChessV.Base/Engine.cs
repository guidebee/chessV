
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
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ChessV
{
	public abstract class Engine: Player
	{
		public enum WriteMode
		{
			Buffered,			//	Use the write buffer
			Unbuffered			//	Bypass the write buffer
		};

		public Engine( IDebugMessageLog messageLog, TimerFactory timerFactory, Process process ):
			base( messageLog, timerFactory )
		{
			id = s_count++;
			pingState = PlayerState.NotStarted;
			isPinging = false;
			WhiteEvalPov = false;
			Restart = EngineConfiguration.RestartMode.Auto;
			writeBuffer = new List<string>();
			options = new List<EngineOption>();
			optionBuffer = new Dictionary<string, object>();
			Variants = new List<string>();
			this.process = process;
			readBuffer = new List<string>();

			//	set up ping timer
			pingTimer = timerFactory.NewTimer();
			pingTimer.Interval = 10000;
			pingTimer.Tick += onPingTimeout;
			pingTimer.Tick += onSingleShotTimerTick;

			//	set up quit timer
			quitTimer = timerFactory.NewTimer();
			quitTimer.Interval = 2000;
			quitTimer.Tick += onQuitTimeout;
			quitTimer.Tick += onSingleShotTimerTick;

			//	set up idle timer
			idleTimer = timerFactory.NewTimer();
			idleTimer.Interval = 10000;
			idleTimer.Tick += onIdleTimeout;
			idleTimer.Tick += onSingleShotTimerTick;

			//	set up read timer
			readTimer = timerFactory.NewTimer();
			readTimer.Interval = 55;
			readTimer.Tick += onReadTimer;
		}

		public override bool IsHuman
		{ get { return false; } }

		public override bool IsReady
		{
			get
			{
				if( isPinging )
					return false;
				return base.IsReady;
			}
		}

		public bool WhiteEvalPov { get; protected set; }

		public EngineConfiguration.RestartMode Restart { get; protected set; }

		public EngineGameAdaptor Adaptor { get; set; }

		public override void EndGame( Result result )
		{
			base.EndGame( result );

			if( restartsBetweenGames )
				Quit();
			else
				Ping();
			
		}

		public override bool SupportsVariant( string variant )
		{
			return Variants.Contains( variant );
		}

		public List<string> Variants { get; protected set; }

		//	Starts communicating with the engine
		//	NOTE: The engine device must already be started
		public void Start()
		{
			if( State != PlayerState.NotStarted )
				return;

			isPinging = false;
			State = PlayerState.Starting;

			flushWriteBuffer();
			readTimer.Start();

			startProtocol();
			isPinging = true;
		}

		//	Hooks up the handlers for the engine process events
		public void SetupProcess()
		{
			process.OutputDataReceived += onReadyRead; // connect( m_ioDevice, SIGNAL( readyRead() ), this, SLOT( onReadyRead() ) );
			process.Exited += onCrashed; // connect( m_ioDevice, SIGNAL( readChannelFinished() ), this, SLOT( onCrashed() ) );
		}

		//	Applies a configuration to the engine
		public void ApplyConfiguration( EngineConfiguration configuration )
		{
			if( configuration.FriendlyName != null && configuration.FriendlyName != "" )
				Name = configuration.FriendlyName;

			foreach( string str in configuration.InitStrings )
				Write( str );

			foreach( EngineOption option in configuration.EngineOptions )
				SetOption( option.Name, option.Value );

			WhiteEvalPov = configuration.WhiteEvalPov;
			Restart = configuration.Restart;
			ClaimsValidated = configuration.ClaimsValidated;
		}

		//	Sends a ping message (an echo request) to the engine to
		//	check if it's still responding to input, and to synchronize
		//	it with the game operator. If the engine doesn't respond in
		//	reasonable time, it will be terminated.
		//	NOTE: All input to the engine will be delayed until we
		//	get a response to the ping.
		public void Ping()
		{
			if( isPinging || State == PlayerState.NotStarted || State == PlayerState.Disconnected || !sendPing() )
				return;

			isPinging = true;
			pingState = State;
			pingTimer.Start();
		}

		//	Returns the engine's chess protocol
		public abstract string Protocol { get; }

		//	Puts the engine in the correct mode to start communicating
		//	with it, using the chosen chess protocol
		protected abstract void startProtocol();

		/*! Parses a line of input from the engine. */
		protected abstract void parseLine( string line );

		//	Sends a ping command to the engine
		//	returns true if successfull
		protected abstract bool sendPing();

		//	Sends the stop command to the engine
		protected abstract void sendStop();

		//	Sends the quit command to the engine
		protected abstract void sendQuit();

		//	Writes text data to the chess engine.
		//	If mode is Unbuffered, the data will be written to
		//	the device immediately even if the engine is being pinged.
		public void Write( string data, WriteMode mode = WriteMode.Buffered )
		{
			if( State == PlayerState.Disconnected )
				return;
			if( State == PlayerState.NotStarted || (isPinging && mode == WriteMode.Buffered))
			{
				writeBuffer.Add( data );
				return;
			}

			//Q_ASSERT(m_ioDevice->isWritable());
			debugMessage( string.Format( ">{0}({1}): {2}", Name, id, data ) );
			process.StandardInput.WriteLine( data );
		}

		//	Sets option name to value
		//	NOTE: If the engine doesn't have an option with the specified name,
		//	nothing happens.
		public void SetOption( string name, object value )
		{
			if( State == PlayerState.Starting || State == PlayerState.NotStarted )
			{
				optionBuffer.Add( name, value );
				return;
			}

			EngineOption option = getOption( name );
			if( option == null )
			{
				//qDebug( "%s doesn't have option %s", qPrintable( this->name() ), qPrintable( name ) );
				return;
			}

			if( !option.IsValid( value ) )
			{
				//qDebug( "Invalid value for option %s: %s", qPrintable( name ), qPrintable( value.toString() ) );
				return;
			}

			option.Value = value;
			sendOption( option.Name, option.Value );
		}


		// *** INHERRITED FROM Player *** //

		public override void Go( Player sender )
		{
			if( State == PlayerState.Observing )
				Ping();
			base.Go( sender );
		}

		public override void Quit()
		{
			if( process == null || process.HasExited || State == PlayerState.Disconnected )
			{
				base.Quit();
			}
			else
			{
				process.Exited -= onCrashed; //disconnect( m_ioDevice, SIGNAL( readChannelFinished() ), this, SLOT( onCrashed() ) );
				process.Exited += onQuitTimeout; //connect( m_ioDevice, SIGNAL( readChannelFinished() ), this, SLOT( onQuitTimeout() ) );
				sendQuit();
				quitTimer.Start();
			}
		}

		public override void Kill()
		{
			if( State == PlayerState.Disconnected )
				return;

			isPinging = false;
			pingTimer.Stop();
			writeBuffer.Clear();

			process.Exited -= onCrashed; // disconnect(m_ioDevice, SIGNAL(readChannelFinished()), this, SLOT(onCrashed()));
			process.Close();

			base.Kill();
		}

		//	Tells the engine to stop thinking and move now
		public override void StopThinking()
		{
			if( State == PlayerState.Thinking && isPinging )
			{
				idleTimer.Start();
				sendStop();
			}
		}

		//	Adds option to the engine options list
		protected void addOption( EngineOption option )
		{
			options.Add( option );
		}

		//	Returns the option that matches name - 
		//	Returns 0 if an option with that name doesn't exist
		protected EngineOption getOption( string name )
		{
			foreach( EngineOption option in options )
				if( option.Alias == name || option.Name == name )
					return option;
			return null;
		}
		
		//	Tells the engine to set option name's value to value
		protected abstract void sendOption( string name, object value );

		protected void addVariant( string variant )
		{
			if( !Variants.Contains( variant ) )
				Variants.Add( variant );
		}

		protected void clearVariants()
		{
			Variants.Clear();
		}

		//	Reads the first whitespace-delimited token from a string
		//	and returns a string referencing the token.
		//	
		//	If readToEnd is true, the whole string is read, except
		//	for leading and trailing whitespace. Otherwise only one
		//	word is read.
		//	
		//	If str doesn't contain any words, null is returned
		protected static string firstToken( string str, out int cursor, bool readToEnd = false )
		{
			cursor = 0;
			return nextToken( str, ref cursor, readToEnd );
		}

		//	Reads the first whitespace-delimited token after the
		//	token referenced by  previous.
		//	
		//	If readToEnd is true, everything from the first word
		//	after previous to the end of the string is read,
		//	except for leading and trailing whitespace. Otherwise
		//	only one word is read.
		//	
		//	If previous is null or it's not followed by any words,
		//	null is returned.
		protected static string nextToken( string str, ref int cursor, bool readToEnd = false )
		{
			int i;
			int start = -1;
			for( i = cursor; i < str.Length; i++ )
			{
				if( Char.IsWhiteSpace( str[i] ) )
				{
					if( start == -1 )
						continue;
					break;
				}
				else if( start == -1 )
				{
					start = i;
					if( readToEnd )
					{
						int end = str.Length;
						while( Char.IsWhiteSpace( str[--end] ) )
							;
						i = end + 1;
						break;
					}
				}
			}
			if( start == -1 )
				return null;
			cursor = i;
			return str.Substring( start, i - start );
		}

		protected override void onTimeout()
		{
			StopThinking();
		}

		protected virtual bool restartsBetweenGames
		{ get { return Restart == EngineConfiguration.RestartMode.On; } }

		//	Reads input from the engine
		protected void onReadyRead( Object sender, DataReceivedEventArgs e )
		{
			if( !string.IsNullOrEmpty( e.Data ) )
			{
				string line = e.Data;
				if( line.Trim() == "" )
					return;

				lock( readBuffer )
				{
					readBuffer.Add( line );
				}

/*				debugMessage( string.Format( "<{0}({1}): {2}", Name, id, line ) );
				parseLine( line );

				if( idleTimer.Enabled )
				{
					if( State == PlayerState.Thinking && !isPinging )
						idleTimer.Start();
					else
						idleTimer.Stop();
				}*/
			}
		}

		protected void onReadTimer( object sender, System.EventArgs e )
		{
			while( true )
			{
				string line = null;
				lock( readBuffer )
				{
					if( readBuffer.Count > 0 )
					{
						line = readBuffer[0];
						readBuffer.RemoveAt( 0 );
					}
					else
						return;
				}

				debugMessage( string.Format( "<{0}({1}): {2}", Name, id, line ) );
				parseLine( line );

				if( idleTimer.Enabled )
				{
					if( State == PlayerState.Thinking && !isPinging )
						idleTimer.Start();
					else
						idleTimer.Stop();
				}
			}
		}

		//	Called when the engine doesn't respond to ping
		protected void onPingTimeout( object sender, System.EventArgs e )
		{
			//qDebug("Engine %s failed to respond to ping", qPrintable(name()));

			isPinging = false;
			writeBuffer.Clear();
			Kill();

			forfeit( ResultType.StalledConnection );
		}

		//	Called when the engine idles for too long
		protected void onIdleTimeout( object sender, System.EventArgs e )
		{
			idleTimer.Stop();
			if( State != PlayerState.Thinking || isPinging )
				return;

			writeBuffer.Clear();
			Kill();

			forfeit( ResultType.StalledConnection );
		}

		//	Called when the engine responds to ping
		protected void pong( object sender, System.EventArgs e )
		{
			if( !isPinging )
				return;

			pingTimer.Stop();
			isPinging = false;
			flushWriteBuffer();

			if( State == PlayerState.FinishingGame )
			{
				if( pingState == PlayerState.FinishingGame )
				{
					State = PlayerState.Idle;
					pingState = PlayerState.Idle;
				}
				//	if the status changed while waiting for a ping response, then
				//	ping again to make sure that we can move on to the next game
				else
				{
					Ping();
					return;
				}
			}

			ready();
		}

		//	Called when the engine has started the chess protocol and
		//	is ready to start a game
		protected void onProtocolStart()
		{
			isPinging = false;
			State = PlayerState.Idle;
			//Q_ASSERT(isReady());

			flushWriteBuffer();

			foreach( KeyValuePair<string, object> pair in optionBuffer )
				SetOption( pair.Key, pair.Value );
			optionBuffer.Clear();
		}

		//	Flushes the write buffer.  If there are any commands in the buffer, 
		//	they will be sent to the engine.
		protected void flushWriteBuffer()
		{
			if( isPinging || State == PlayerState.NotStarted )
				return;

			foreach( string line in writeBuffer )
				Write( line );
			writeBuffer.Clear();
		}

		private void onQuitTimeout( object sender, System.EventArgs e )
		{
			//Q_ASSERT(state() != Disconnected);

			process.Exited -= onQuitTimeout; //disconnect(m_ioDevice, SIGNAL(readChannelFinished()), this, SLOT(onQuitTimeout()));

			if( !quitTimer.Enabled )
				Kill();
			else
				quitTimer.Stop();

			base.Quit();
		}


		protected int id { get; set; }
		protected bool isPinging { get; set; }
		protected List<string> writeBuffer { get; set; }
		protected Process process { get; set; }
		protected List<EngineOption> options { get; set; }
		protected Dictionary<string, object> optionBuffer { get; set; }
		protected PlayerState pingState;
		protected Timer idleTimer;
		protected Timer pingTimer;
		protected Timer quitTimer;
		protected static int s_count = 0;

		protected Timer readTimer;
		protected List<string> readBuffer;
	}
}
