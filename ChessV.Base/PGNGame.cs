
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

namespace ChessV
{
	public class PGNGame
	{
		//	Mode for writing PGN games
		public enum PGNMode
		{
			//	Only use data which is required by the PGN standard
			Minimal,

			//	Use additional data like extra tags and comments
			Verbose
		};

		//	Brief structure for storing the game's move history
		public class MoveData
		{
			//	The zobrist position key before the move
			public UInt64 Key;

			//	The move in the "generic" format
			public Movement Move;

			//	The move in Standard Algebraic Notation
			public string MoveString;

			//	A comment/annotation describing the move
			public string Comment;
		};


		// *** CONSTRUCTION *** //

		public PGNGame()
		{
			StartingSide = 0;
			Tags = new Dictionary<string,string>();
			Moves = new List<MoveData>();
		}


		// *** PROPERTIES *** //

		//	List of MoveData records for the game moves
		public List<MoveData> Moves { get; protected set; }

		//	true if the game doesn't contain any tags or moves
		public bool IsNull 
		{ get { return Tags.Count == 0 && Moves.Count == 0; } }

		//	Deletes all tags and moves
		public void Clear()
		{
			StartingSide = -1;
			//m_eco = EcoNode::root();
			Tags = new Dictionary<string,string>();
			Moves = new List<MoveData>();
		}

		//	List of tags that describe the game
		public Dictionary<string, string> Tags { get; private set; }

		//	Adds a new move to the game
		public void AddMove( MoveData move )
		{
			Moves.Add( move );
			// TODO: ECO stuff
		}

		/*!
		 * Creates a board object for viewing or analyzing the game.
		 *
		 * The board is set to the game's starting position.
		 * Returns 0 on error.
		 */
		//public Chess::Board* createBoard() const;

		/*!
		 * Reads a game from a PGN text stream.
		 *
		 * \param in The PGN stream to read from.
		 * \param maxMoves The maximum number of halfmoves to read.
		 *
		 * \note Even if the stream contains multiple games,
		 * only one will be read.
		 *
		 * Returns true if any tags and/or moves were read.
		 */
		//public bool Read( PgnStream in, int maxMoves = INT_MAX - 1 );
		/*! Writes the game to a text stream. */
		//public void Write( QTextStream out, PgnMode mode = Verbose );
		/*!
		 * Writes the game to a file.
		 * If the file already exists, the game will be appended
		 * to the end of the file.
		 *
		 * Returns true if successfull.
		 */
		//public bool write(const string filename, PgnMode mode = Verbose) const;
		
		//	Returns true only for a "standard" game of Orthodox Chess
		//	played from the default starting position
		public bool IsStandard
		{ get { return Variant == "standard" && !Tags.ContainsKey( "FEN" ); } }

		//	Looks up the value of a tag - null if it doesn't exist
		public string TagValue( string tag )
		{ 
			if( Tags.ContainsKey( tag ) )
				return Tags[tag];
			return null;
		}

		//	the name of the tournament or match event
		public string Event
		{ 
			get
			{
				if( Tags.ContainsKey( "Event" ) )
					return Tags["Event"];
				return null;
			}
			set
			{
				Tags["Event"] = value;
			}
		}

		//	the location of the event
		public string Site
		{
			get
			{
				if( Tags.ContainsKey( "Site" ) )
					return Tags["Site"];
				return null;
			}
			set
			{
				Tags["Site"] = value;
			}
		}

		//	the starting date of the game
		public string Date
		{
			get
			{
				if( Tags.ContainsKey( "Date" ) )
					return Tags["Date"];
				return null;
			}
			set
			{
				Tags["Date"] = value;
			}
		}

		//	the playing round ordinal of the game
		public string Round
		{
			get
			{
				if( Tags.ContainsKey( "Round" ) )
					return Tags["Round"];
				return null;
			}
			set
			{
				Tags["Round"] = value;
			}
		}

		//	Returns the player's name for the given side
		public string GetPlayerName( int side )
		{
			string tagname = side == 0 ? "White" : (side == 1 ? "Black" : null);
			if( tagname != null )
				if( Tags.ContainsKey( tagname ) )
					return Tags[tagname];
			return null;
		}

		//	Returns the result of the game
		public Result Result
		{
			get
			{
				if( Tags.ContainsKey( "Result" ) )
					return new Result( Tags["Result"] );
				return null;
			}
			set
			{
				Tags["Result"] = value.ShortString;
				switch( Result.Type )
				{
					case ResultType.Adjudication:
						Tags["Termination"] = "adjudication";
						break;
					case ResultType.Timeout:
						Tags["Termination"] = "time forfeit";
						break;
					case ResultType.Disconnection:
						Tags["Termination"] = "abandoned";
						break;
					case ResultType.NoResult:
						Tags["Termination"] = "unterminated";
						break;
					default:
						if( Tags.ContainsKey( "Termination" ) )
							Tags.Remove( "Termination" );
						break;
				}
			}
		}

		//	the chess variant of the game
		public string Variant
		{
			get
			{
				if( Tags.ContainsKey( "Variant" ) )
					return Tags["Variant"];
				return "standard";
			}
			set
			{
				if( Variant == "standard" )
				{
					if( Tags.ContainsKey( "Variant" ) )
							Tags.Remove("Variant");
				}
				else
					Tags["Variant"] = value;
			}
		}

		//	the side that starts the game
		public int StartingSide { get; set; }

		//	the FEN string of the starting position
		public string StartingFEN
		{
			get
			{
				if( Tags.ContainsKey( "FEN" ) )
					return Tags["FEN"];
				return null;
			}
		}

		//	sets the value of a tag
		public void SetTag( string tag, string value )
		{
			if( value == null || value == "" )
			{
				if( Tags.ContainsKey( tag ) )
					Tags.Remove( tag );
			}
			else
				Tags[tag] = value;
			// TODO: send to tag receiver (whatever that's for)
		}

		//	sets the name of the player for the given side
		public void SetPlayerName( int side, string name )
		{
			if( side == 0 )
				Tags["White"] = name;
			else if( side == 1 )
				Tags["Black"] = name;
		}

		//	sets the starting position's FEN string
		public void SetStartingFenString( int side, string fen )
		{
			StartingSide = side;
			if( fen == null )
			{
				if( Tags.ContainsKey( "FEN" ) )
					Tags.Remove( "FEN" );
				if( Tags.ContainsKey( "SetUp" ) )
					Tags.Remove( "SetUp" );
			}
			else
			{
				Tags["FEN"] = fen;
				Tags["SetUp"] = "1";
			}
		}

		//	Sets a description for the result.
		//	This is appended to the last move's comment/annotation.
		//	NOTE - this is not the same as the "Termination" tag which can
		//	only hold one of the standardized values.
		public void SetResultDescription( string description )
		{
			if( description == null || Moves.Count == 0 )
				return;

			string comment = Moves[Moves.Count - 1].Comment;
			if( comment != null )
				comment += ", ";
			comment += description;
		}

		/*!
		 * Sets a receiver for PGN tags
		 *
		 * \a receiver is an object whose "setTag(string tag, string value)"
		 * slot is called when a PGN tag changes.
		 */
		//public void setTagReceiver(QObject* receiver);

	
//		private bool parseMove(PgnStream in);
		
		//private const EcoNode* m_eco;
		//private QObject* m_tagReceiver;
	}
}
