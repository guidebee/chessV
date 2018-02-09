
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

namespace ChessV
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
	public class GameAttribute: System.Attribute
	{
		public string GameName { get; set; }
		public Type GeometryType { get; set; }
		public int[] GeometryParameters { get; set; }
		public string Definitions { get; set; }
		public bool Template { get; set; }
		public string Invented { get; set; }
		public string InventedBy { get; set; }
		public string Tags { get; set; }
		public string GameDescription1 { get; set; }
		public string GameDescription2 { get; set; }
		public string XBoardName { get; set; }

		public GameAttribute( string gamename, Type geometryType, params int[] geometryParameters )
		{ 
			GameName = gamename; 
			Template = false;
			GeometryType = geometryType;
			GeometryParameters = geometryParameters;
		}

		public List<string> TagList
		{ get { return Tags == null ? null : new List<string>( Tags.Split( ',' ) ); } }

		public BoardGeometry BoardGeometry
		{
			get
			{
				if( !GeometryType.IsSubclassOf( typeof(BoardGeometry) ) )
					throw new Exception( "FATAL: GameAttribute passed geometry not derived from BoardGeometry class" );
				ConstructorInfo[] constructors = GeometryType.GetConstructors();
				if( constructors.Length != 1 )
					throw new Exception( "FATAL: BoardGeometry derived class has multiple constructors" );
				ConstructorInfo constructor = constructors[0];
				ParameterInfo[] parameters = constructor.GetParameters();
				foreach( ParameterInfo parameter in parameters )
					if( parameter.ParameterType != typeof(int) )
						throw new Exception( "FATAL: BoardGeometry derived class has constructor with non-int argument" );
				object[] constructorParameters = new object[parameters.Length];
				for( int x = 0; x < GeometryParameters.Length; x++ )
					constructorParameters[x] = GeometryParameters[x];
				for( int y = GeometryParameters.Length; y < parameters.Length; y++ )
					constructorParameters[y] = Type.Missing;
				return (BoardGeometry) constructor.Invoke( constructorParameters );
			}
		}
	}
}
