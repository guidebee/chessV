
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

namespace ChessV
{
	//**********************************************************************
	//
	//                         GenericPiece
	//
	//    Provides only the most basic information about a piece, namely:
	//
	//        PieceType - the PieceType of the piece
	//        Player - the player who's piece it is (piece color)
	//
	//    Does not know what Game to which it belongs, whether it is on 
	//    a Board square or not, etc.  This is the base class to Piece 
	//    which knows this extra information.

	public class GenericPiece
	{
		// *** PUBLIC PROPERTIES *** //

		public int Player { get; set; }
		public PieceType PieceType { get; set; }


		// *** CONSTRUCTION *** //

		public GenericPiece( GenericPiece piece )
		{
			Player = piece.Player;
			PieceType = piece.PieceType;
		}

		public GenericPiece( int player, PieceType pieceType )
		{
			Player = player;
			PieceType = pieceType;
		}


		// *** OPERATORS *** //

		public static bool operator ==( GenericPiece gp1, GenericPiece gp2 )
		{
			//	If both are null, or both are same instance, return true
			if( System.Object.ReferenceEquals( gp1, gp2 ) )
				return true;

			//	If one is null, but not both, return true
			if( (object) gp1 == null || (object) gp2 == null )
				return false;
			
			return gp1.PieceType == gp2.PieceType && gp1.Player == gp2.Player; 
		}

		public static bool operator !=( GenericPiece gp1, GenericPiece gp2 )
		{
			//	If both are null, or both are same instance, return false
			if( System.Object.ReferenceEquals( gp1, gp2 ) )
				return false;

			//	If one is null, but not both, return true
			if( (object) gp1 == null || (object) gp2 == null )
				return true;

			return gp1.PieceType != gp2.PieceType && gp1.Player != gp2.Player; 
		}

		public bool Equals( GenericPiece other )
		{
			return
				other != null &&
				other.Player == Player &&
				other.PieceType == PieceType;
		}

		public override bool Equals( object obj )
		{
			if( obj is GenericPiece )
				return Equals( (GenericPiece) obj );
			return false;
		}

		public override int GetHashCode()
		{ return PieceType.TypeNumber << 4 | Player; }
	}
}
