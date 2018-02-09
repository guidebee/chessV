
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

namespace ChessV.Compiler
{
	public class PartialDefinition
	{
		public PartialDefinition( string name, string baseName = null )
		{
			Name = name;
			BaseName = baseName;
			VariableAssignments = new Dictionary<string, object>();
			MemberVariableDeclarations = new Dictionary<string, Type>();
			FunctionDeclarations = new Dictionary<string, Antlr4.Runtime.ParserRuleContext>();
		}

		public void AddVariableAssignment( string memberName, object memberValue )
		{
			VariableAssignments.Add( memberName, memberValue );
		}

		public void AddFunctionDeclaration( string memberName, Antlr4.Runtime.ParserRuleContext functionDefinition )
		{
			FunctionDeclarations.Add( memberName, functionDefinition );
		}

		public void AddMemberVariableDeclaration( string memberName, Type type )
		{
			MemberVariableDeclarations.Add( memberName, type );
		}

		public string Name { get; private set; }
		public string BaseName { get; private set; }
		public Dictionary<string, object> VariableAssignments { get; private set; }
		public Dictionary<string, Type> MemberVariableDeclarations { get; private set; }
		public Dictionary<string, Antlr4.Runtime.ParserRuleContext> FunctionDeclarations { get; private set; }
	}
}
