
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
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

namespace ChessV.Compiler
{
	public class CompilerVisitor: ChessVCBaseVisitor<object>
	{
		public CompilerVisitor( Compiler compiler )
		{
			this.compiler = compiler;
		}

		public override object VisitPieceTypeDeclaration( ChessVCParser.PieceTypeDeclarationContext context )
		{
			string pieceName = getObjectIdName( context.objectid() );
			partial = new PartialDefinition( pieceName );
			var returnval = base.VisitPieceTypeDeclaration( context );
			compiler.AddPieceType( partial );
			return returnval;
		}

		public override object VisitGameDeclaration( ChessVCParser.GameDeclarationContext context )
		{
			string gameName = getObjectIdName( context.objectid( 0 ) );
			string baseGameName = getObjectIdName( context.objectid( 1 ) );
			partial = new PartialDefinition( gameName, baseGameName );
			var returnval = base.VisitGameDeclaration( context );
			compiler.AddGame( partial );
			return returnval;
		}

		public override object VisitMemberDefn( ChessVCParser.MemberDefnContext context )
		{
			string typename = context.objectid( 0 ).GetText();
			string variableName = context.objectid( 1 ).GetText();
			Type variableType = null;
			switch( typename )
			{
				case "Choice":
					variableType = typeof(ChoiceVariable);
					break;

				case "Integer":
					variableType = typeof(IntVariable);
					break;

				case "String":
					variableType = typeof(string);
					break;

				case "PieceType":
					variableType = typeof(PieceType);
					break;
			}
			if( variableType == null )
				throw new Exception( "Game variable declared of unsupported type: " + typename );
			partial.AddMemberVariableDeclaration( variableName, variableType );
			return base.VisitMemberDefn( context );
		}

		public override object VisitFunctionDefn( ChessVCParser.FunctionDefnContext context )
		{
			string functionName = getObjectIdName( context.objectid() );
			if( partial != null )
				partial.AddFunctionDeclaration( functionName, context.block() );
			return base.VisitFunctionDefn( context );
		}

		public override object VisitMemberAssign( ChessVCParser.MemberAssignContext context )
		{
			ChessVCParser.AssignmentContext assignmentContext = (ChessVCParser.AssignmentContext) context.GetChild( 0 );
			string memberName = getObjectIdName( assignmentContext.objectid() );
			
			if( assignmentContext.GetChild( 2 ) is ChessVCParser.ConstantExprContext )
			{
				ChessVCParser.ConstantExprContext constExprContext = (ChessVCParser.ConstantExprContext) assignmentContext.GetChild( 2 );
				if( constExprContext.GetChild( 0 ) is ChessVCParser.ConstIntContext )
				{
					if( partial != null )
						partial.AddVariableAssignment( memberName, getConstInt( constExprContext ) );
				}
				else if( constExprContext.GetChild( 0 ) is ChessVCParser.ConstStrContext )
				{
					if( partial != null )
						partial.AddVariableAssignment( memberName, getConstStr( constExprContext ) );
				}
			}
			else if( assignmentContext.GetChild( 2 ) is ChessVCParser.ExprContext )
			{
				object obj = Visit( ((ChessVCParser.ExprContext) assignmentContext.GetChild( 2 )) );
				if( obj != null && partial != null )
					partial.AddVariableAssignment( memberName, obj );
				if( obj == null )
					throw new Exception( "Invalid top-level assignment" );
			}
			return base.VisitMemberAssign( context );
		}

		public override object VisitObjectid( ChessVCParser.ObjectidContext context )
		{
			if( context.GetText() == "MirrorSymmetry" )
				return new MirrorSymmetry();
			else if( context.GetText() == "RotationalSymmetry" )
				return new RotationalSymmetry();
			else if( context.GetText() == "NoSymmetry" )
				return new NoSymmetry();
			return base.VisitObjectid( context );
		}

		protected string getObjectIdName( ChessVCParser.ObjectidContext context )
		{
			string name = context.GetText();
			if( name.Length >= 2 && name[0] == '\'' )
				name = name.Substring( 1, name.Length - 2 );
			return name;
		}

		protected int getConstInt( ChessVCParser.ConstantExprContext context )
		{
			return Convert.ToInt32( ((ChessVCParser.ConstIntContext) context.GetChild( 0 )).INTEGER().GetText() );
		}

		protected string getConstStr( ChessVCParser.ConstantExprContext context )
		{
			string s = ((ChessVCParser.ConstStrContext) context.GetChild( 0 )).GetText();
			//	trim off the quotation marks
			return s.Substring( 1, s.Length - 2 );
		}

		protected PartialDefinition partial;
		protected Compiler compiler;
	}
}
