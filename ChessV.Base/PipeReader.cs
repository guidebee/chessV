
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
using System.Threading;

namespace ChessV
{
	//	THIS CLASS IS NOT PRESENTLY USED!!!
	//	It's from Cute Chess but I never hooked it up.  Using 
	//	whatever .NET functionality instead, but this should probably
	//	be connected at some point.
	class PipeReader
	{
		public PipeReader( StreamReader inputStream )
		{
			stream = inputStream;
			buffer = new char[bufferSize];
			mutex = new Mutex();
		}

		//	public void event ReadyToRead();

		public long BytesAvailable
		{
			get
			{
				mutex.WaitOne();
				long bytes = bytesLeft;
				mutex.ReleaseMutex();
				return bytes;
			}
		}

		public bool CanReadLine
		{
			get
			{
				mutex.WaitOne();
				bool canReadLine = lastLineBreak > 0;
				mutex.ReleaseMutex();
				return canReadLine;
			}
		}

		protected void Run()
		{
			int bufferEnd = bufferSize;
			int chunkSize = 0;
			start = 0;
			end = 0;
			bytesLeft = 0;

			while( true )
			{
				mutex.WaitOne();

				//	use a chunk size as large as possible, but still limited
				//	to half of the buffer
				if( end >= start )
					chunkSize = bufferEnd - end;
				else
					chunkSize = start - end;
				chunkSize = chunkSize < bufferSize / 2 ? chunkSize : bufferSize / 2;

				//	wait until more than half of the buffer is free
				if( bytesLeft >= bufferSize / 2 )
				{
					mutex.ReleaseMutex();
					Thread.Sleep( 10 );
				}
				else
				{
					mutex.ReleaseMutex();
					Exception exception = null;
					int bytesRead = 0;
					try
					{
						bytesRead = stream.Read( buffer, end, chunkSize );
					}
					catch( Exception ex )
					{
						exception = ex;
					}
					if( exception != null || bytesRead == 0 )
						return;
					mutex.WaitOne();

					bytesLeft += bytesRead;
					end += bytesRead;
					bool sendReady = findLastNewline( end - 1, bytesRead );
					if( end >= bufferEnd )
						end = 0;

					//	to avoid signal spam, send the 'readyRead' signal only
					//	if we have a whole line of new data
					//				if( sendReady )
					//					ReadyToRead();

					mutex.ReleaseMutex();
				}
			}
		}

		private bool findLastNewline( int end, int size )
		{
			for( int i = 0; i < size; i++ )
			{
				if( buffer[end] == '\n' )
				{
					lastLineBreak = bytesLeft - i;
					return true;
				}
				end--;
			}

			return false;
		}


		// *** PRIVATE DATA MEMBERS *** //

		private const int bufferSize = 0x8000;
		private StreamReader stream;
		private char[] buffer;
		private int start;
		private int end;
		private long bytesLeft;
		private long lastLineBreak;
		private Mutex mutex;
	}
}
