
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
using System.Reflection;
using ChessV;
using ChessV.GUI.Attributes;

namespace ChessV.GUI
{
	public static class PresentationFactory
	{
		public static void Initialize()
		{
			boardTypeToPresentationType = new Dictionary<Type, Type>();
			Module m = typeof(PresentationFactory).Module;
			Type[] types = m.GetTypes();
			foreach( Type type in types )
			{
				object[] customAttrs = type.GetCustomAttributes( typeof(PresentsBoardAttribute), false );
				if( customAttrs != null && customAttrs.Length >= 1 )
					foreach( object attr in customAttrs )
					{
						PresentsBoardAttribute presentsBoardAttribute = (PresentsBoardAttribute) attr;
						boardTypeToPresentationType.Add( presentsBoardAttribute.BoardType, type );
					}
			}

		}

		public static BoardPresentation CreatePresentation( Game game, Theme theme, bool smallPreview = false )
		{
			//	Search for a custom type of BoardPresentation for the type of Board used in this game
			Type boardPresentationType = null;
			//	TEST 1: Do we have a PresentationType directly bound with the Board type of this game?
			if( boardTypeToPresentationType.ContainsKey( game.Board.GetType() ) )
				boardPresentationType = boardTypeToPresentationType[game.Board.GetType()];
			//	TEST 2: Do we have a PresentationType boud to a base class of the Board type of this game?
			else
			{
				foreach( KeyValuePair<Type, Type> pair in boardTypeToPresentationType )
					if( game.Board.GetType().IsSubclassOf( pair.Key ) )
						boardPresentationType = pair.Value;
			}

			//	Did we find a mapping?  If so use reflection to dynamically call the constructor
			if( boardPresentationType != null )
			{
				ConstructorInfo ci = boardPresentationType.GetConstructor( new Type[] { typeof( Board ), typeof( Theme ), typeof( bool ) } );
				return (BoardPresentation) ci.Invoke( new object[] { game.Board, theme, smallPreview } );
			}

			//	Otherwise, use the generic BoardPresentation class
			return new BoardPresentation( game.Board, theme, smallPreview );
		}

		public static BoardPresentation CreatePresentation( Game game, bool smallPreview = false )
		{
			return CreatePresentation( game, ThemeFactory.CreateTheme( game ), smallPreview );
		}

		private static Dictionary<Type, Type> boardTypeToPresentationType;
	}
}
