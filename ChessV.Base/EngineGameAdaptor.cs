
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
using System.Text;

namespace ChessV
{
	//**********************************************************************
	//
    //                       EngineGameAdaptor
	//
	//    Objects of this class provide an interface for engines to 
	//    support playing variants they were not originally designed to.
	//    A number of tricks are exploited to accomplish this, from the 
	//    simple (issuing a setboard at the beginning of the game to put 
	//    the pieces in the proper place) to more complicated (like 
	//    mirroring board positions so the engine sees a mirror image of 
	//    the board so its castling implementation will work properly.)
	
	public class EngineGameAdaptor
	{
		// *** PUBLIC CONFIGURATION PROPERTIES *** //

		//	These are set externally to tell the adaptor what operations 
		//	to perform to adapt the engine to the given game.


		//	name of the supported variant the engine will think we are playing
		public string XBoardName { get; set; }

		//	do we need to issue a 'setboard' command before playing?
		public bool IssueSetboard { get; set; }

		//	list of moves that the engine won't be able to interpret -
		//	for example, 'flexible' castling moves in Capablanca variants.
		//	if the opponent makes one of these moves we just send another 
		//	setboard command to reset the pieces to their new locations.
		public List<string> UnhandleableMoves { get; set; }

		//	do we mirror the board (by file) so the engine sees a 
		//	mirror image of the actual game?  if so, all files are reversed
		//	including in the PV returned from the engine
		public bool MirrorBoard { get; set; }


		// *** CONSTRUCTION *** //

		public EngineGameAdaptor( string xboardname )
		{
			XBoardName = xboardname;
			pieceNotationTranslationEngineToGame = new Dictionary<string, string>();
			pieceNotationTranslationGameToEngine = new Dictionary<string, string>();
		}


		// *** INITIALIZATION *** //

		//	After the public properties are set, this function is called 
		//	to finalize the configuration of the adaptor for the given game
		public virtual void Initialize( Game game )
		{
			this.game = game;
			if( MirrorBoard )
			{
				fileTranslation = new Dictionary<char,char>();
				for( int nfile = 0; nfile < game.Board.NumFiles; nfile++ )
				{
					string fileNotation = game.Board.GetFileNotation( nfile );
					string mirrorFileNotation = game.Board.GetFileNotation( game.Board.NumFiles - nfile - 1 );
					if( fileNotation.Length != 1 || mirrorFileNotation.Length != 1 )
						throw new Exception( "Adaptor error - cannot mirror given board" );
					fileTranslation.Add( fileNotation[0], mirrorFileNotation[0] );
				}
			}
		}

		public virtual void TranslatePieceNotation( string gameNotation, string engineNotation )
		{
			pieceNotationTranslationEngineToGame.Add( engineNotation, gameNotation );
			pieceNotationTranslationGameToEngine.Add( gameNotation, engineNotation );
		}


		// *** OPERATIONS (for XBoardEngine) *** //

		internal string TranslatePV( string pv, bool toEngine )
		{
			//	if we're not translating file notations, return the pv given
			if( fileTranslation == null )
				return pv;
			//	parse the PV, translating the files
			StringBuilder newpv = new StringBuilder( pv.Length + 2 );
			int distanceFromSpace = 0;
			for( int cursor = 0; cursor < pv.Length; cursor++ )
			{
				if( distanceFromSpace >= 4 )
				{
					//	translate promotion char
					if( toEngine )
					{
						if( pieceNotationTranslationGameToEngine.ContainsKey( pv[cursor].ToString() ) )
							newpv.Append( pieceNotationTranslationGameToEngine[pv[cursor].ToString()] );
						else
							newpv.Append( pv[cursor] );
					}
					else
					{
						if( pieceNotationTranslationEngineToGame.ContainsKey( pv[cursor].ToString() ) )
							newpv.Append( pieceNotationTranslationEngineToGame[pv[cursor].ToString()] );
						else
							newpv.Append( pv[cursor] );
					}
				}
				else
				{
					//	translate files
					if( fileTranslation.ContainsKey( pv[cursor] ) )
						newpv.Append( fileTranslation[pv[cursor]] );
					else
						newpv.Append( pv[cursor] );
				}
				distanceFromSpace = pv[cursor] == ' ' ? 0 : distanceFromSpace + 1;
			}
			return newpv.ToString();
		}

		internal string TranslateMove( string move, bool toEngine )
		{
			//	translating moves (for now) applies the same logic as PVs
			return TranslatePV( move, toEngine );
		}

		//	This function transates FENs for mirrored representations.
		//	It makes a lot of assumptions, namely (1) only single-char 
		//	piece notations, (2) the FEN format is the standard format 
		//	for Chess, (3) super-specific castling notation translation. 
		//	If these notations don't fit, the Game-derived 
		//	class shouldn't provide this as a valid adaptor!
		//	NOTE: as of now, this is all ok, because it is only used 
		//	for Capablanca variants with the King on e1 rather than f1. 
		//	Mirroring isn't suitable for anything else yet, but there 
		//	may never be the need for anything else.
		internal string TranslateFEN( string fen )
		{
			StringBuilder builder = new StringBuilder( fen.Length + 2 );
			//	split first into parts by space delimiter
			string[] fenParts = fen.Split( ' ' );
			//	perform character translations
			foreach( KeyValuePair<string, string> translation in pieceNotationTranslationGameToEngine )
			{
				fenParts[0] = fenParts[0]
					.Replace( translation.Key.ToLower(), translation.Value.ToLower() )
					.Replace( translation.Key.ToUpper(), translation.Value.ToUpper() );
			}
			if( !MirrorBoard )
			{
				//	if we are not mirroring the board, we are done 
				//	just reassemble the pieces of the FEN and return
				foreach( string part in fenParts )
				{
					builder.Append( part );
					builder.Append( ' ' );
				}
				builder.Remove( builder.Length - 1, 1 );
				return builder.ToString();
			}
			//	we're mirroring the board, so we need to flip the array 
			//	and translate the castling notations

			//	now, split the array by the / delimiter
			string[] arrayParts = fenParts[0].Split( '/' );
			//	reassemble the mirrored versions of the array parts 
			builder.Append( MirrorArrayLine( arrayParts[0] ) );
			for( int x = 1; x < arrayParts.Length; x++ )
			{
				builder.Append( '/' );
				builder.Append( MirrorArrayLine( arrayParts[x] ) );
			}
			//	append the current-player FEN part
			builder.Append( ' ' );
			builder.Append( fenParts[1] );
			//	translate and append the castling FEN part
			builder.Append( ' ' );
			builder.Append( fenParts[2]
				.Replace( 'A', 'K' )
				.Replace( 'J', 'Q' )
				.Replace( 'a', 'k' )
				.Replace( 'j', 'q' ) );
			//	append the rest
			for( int y = 3; y < fenParts.Length; y++ )
			{
				builder.Append( ' ' );
				builder.Append( fenParts[y] );
			}
			return builder.ToString();
		}


		// *** PROTECTED HELPER FUNCTIONS *** //

		protected string MirrorArrayLine( string arrayLine )
		{
			string newArrayLine = "";
			int cursor = 0;

			while( cursor < arrayLine.Length )
			{
				if( Char.IsDigit( arrayLine[cursor] ) )
				{
					int numberStart = cursor;
					//	parse entire number and place at the beginning
					while( cursor < arrayLine.Length && Char.IsDigit( arrayLine[cursor] ) )
						cursor++;
					string number = arrayLine.Substring( numberStart, cursor - numberStart );
					newArrayLine = number + newArrayLine;
				}
				else
					//	take this char and place it at the beginning 
					//	NOTE - we assume only single-char piece notations!
					newArrayLine = arrayLine[cursor++] + newArrayLine;
			}
			return newArrayLine;
		}

		// *** PROTECTED DATA *** //

		protected Game game;
		protected Dictionary<char, char> fileTranslation;
		protected Dictionary<string, string> pieceNotationTranslationEngineToGame;
		protected Dictionary<string, string> pieceNotationTranslationGameToEngine;
	}
}
