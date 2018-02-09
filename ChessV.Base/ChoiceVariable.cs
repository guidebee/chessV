
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

namespace ChessV
{
	public class ChoiceVariable: ICloneable
	{
		public List<string> Choices;
		private string value;

		public string Value 
		{
			get 
			{ return value; }

			set
			{
				if( value == null )
					this.value = null;
				else
				{
					foreach( string choice in Choices )
						if( choice.ToUpper() == value.ToUpper() )
						{
							this.value = choice;
							return;
						}
					throw new Exception( "Specified value not valid for choice variable: " + value );
				}
			}
		}

		public ChoiceVariable()
		{ Choices = new List<string>(); }

		public ChoiceVariable( ChoiceVariable original )
		{
			//	Copy constructor
			Choices = new List<string>();
			foreach( string choice in original.Choices )
				Choices.Add( choice );
			value = original.value;
		}

		public ChoiceVariable( string[] choices ): this()
		{
			foreach( string choice in choices )
				Choices.Add( choice );
		}

		public ChoiceVariable( List<string> choices )
		{
			Choices = choices;
		}

		public void AddChoice( string newChoice )
		{
			Choices.Add( newChoice );
		}

		public void RemoveChoice( string choice )
		{
			Choices.Remove( choice );
		}

		public override string ToString()
		{
			return value;
		}

		object ICloneable.Clone()
		{
			return new ChoiceVariable( this );
		}
	}
}
