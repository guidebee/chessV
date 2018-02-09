
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
using System.Diagnostics;
using System.Text;

namespace ChessV
{
	public class EngineBuilder
	{
		public EngineConfiguration EngineConfiguration { get; private set; }
		public IDebugMessageLog MessageLog { get; private set; }

		public EngineBuilder( IDebugMessageLog messageLog, EngineConfiguration config )
		{
			EngineConfiguration = config;
			MessageLog = messageLog;
		}

		public virtual Engine Create( TimerFactory timerFactory, ReadyEventHandler readyHandler = null )
		{
			Engine engine = null;

			Process process = new Process();
			StringBuilder args = new StringBuilder( 100 );
			foreach( string arg in EngineConfiguration.Arguments )
			{
				if( args.Length > 0 )
					args.Append( ' ' );
				args.Append( arg );
			}
			ProcessStartInfo si = new ProcessStartInfo( EngineConfiguration.Command, args.ToString() );
			si.UseShellExecute = false;
			si.RedirectStandardInput = true;
			si.RedirectStandardOutput = true;
			si.WorkingDirectory = EngineConfiguration.WorkingDirectory;
			si.WindowStyle = ProcessWindowStyle.Hidden;
			si.CreateNoWindow = true;
			process.StartInfo = si;
			process.Start();
			process.BeginOutputReadLine();

			if( EngineConfiguration.Protocol == "xboard" )
				engine = new XBoardEngine( MessageLog, timerFactory, process );
			else
				throw new Exception( "unsupported protocol" );

			if( readyHandler != null )
				engine.Ready += readyHandler;

			engine.SetupProcess();
			engine.ApplyConfiguration( EngineConfiguration );
			engine.Start();

			return engine;
		}
	}
}
