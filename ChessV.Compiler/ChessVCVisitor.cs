
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

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.5.3
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\greg\workspace\ChessVCParser\ChessVC.g4 by ANTLR 4.5.3

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="ChessVCParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.5.3")]
[System.CLSCompliant(false)]
public interface IChessVCVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="ChessVCParser.chunk"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitChunk([NotNull] ChessVCParser.ChunkContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="ChessVCParser.unit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUnit([NotNull] ChessVCParser.UnitContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>PieceTypeDecl</c>
	/// labeled alternative in <see cref="ChessVCParser.declaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPieceTypeDecl([NotNull] ChessVCParser.PieceTypeDeclContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>GameDecl</c>
	/// labeled alternative in <see cref="ChessVCParser.declaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGameDecl([NotNull] ChessVCParser.GameDeclContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="ChessVCParser.pieceTypeDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPieceTypeDeclaration([NotNull] ChessVCParser.PieceTypeDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="ChessVCParser.gameDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGameDeclaration([NotNull] ChessVCParser.GameDeclarationContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>MemberAssign</c>
	/// labeled alternative in <see cref="ChessVCParser.declMember"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMemberAssign([NotNull] ChessVCParser.MemberAssignContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>FnDefinition</c>
	/// labeled alternative in <see cref="ChessVCParser.declMember"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFnDefinition([NotNull] ChessVCParser.FnDefinitionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>MemberDefinition</c>
	/// labeled alternative in <see cref="ChessVCParser.declMember"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMemberDefinition([NotNull] ChessVCParser.MemberDefinitionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="ChessVCParser.functionDefn"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionDefn([NotNull] ChessVCParser.FunctionDefnContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="ChessVCParser.memberDefn"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMemberDefn([NotNull] ChessVCParser.MemberDefnContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>BlockStatement</c>
	/// labeled alternative in <see cref="ChessVCParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlockStatement([NotNull] ChessVCParser.BlockStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>IfStatement</c>
	/// labeled alternative in <see cref="ChessVCParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIfStatement([NotNull] ChessVCParser.IfStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>AssignStatement</c>
	/// labeled alternative in <see cref="ChessVCParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssignStatement([NotNull] ChessVCParser.AssignStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>FnCallStatement</c>
	/// labeled alternative in <see cref="ChessVCParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFnCallStatement([NotNull] ChessVCParser.FnCallStatementContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="ChessVCParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlock([NotNull] ChessVCParser.BlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="ChessVCParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssignment([NotNull] ChessVCParser.AssignmentContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="ChessVCParser.functionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionCall([NotNull] ChessVCParser.FunctionCallContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="ChessVCParser.argumentList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArgumentList([NotNull] ChessVCParser.ArgumentListContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>OpBitwiseOr</c>
	/// labeled alternative in <see cref="ChessVCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOpBitwiseOr([NotNull] ChessVCParser.OpBitwiseOrContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>OpEquality</c>
	/// labeled alternative in <see cref="ChessVCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOpEquality([NotNull] ChessVCParser.OpEqualityContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ConstantExpr</c>
	/// labeled alternative in <see cref="ChessVCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstantExpr([NotNull] ChessVCParser.ConstantExprContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>UnaryMinus</c>
	/// labeled alternative in <see cref="ChessVCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUnaryMinus([NotNull] ChessVCParser.UnaryMinusContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ObjectIdExpr</c>
	/// labeled alternative in <see cref="ChessVCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitObjectIdExpr([NotNull] ChessVCParser.ObjectIdExprContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>OpAddSub</c>
	/// labeled alternative in <see cref="ChessVCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOpAddSub([NotNull] ChessVCParser.OpAddSubContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>UnaryNot</c>
	/// labeled alternative in <see cref="ChessVCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUnaryNot([NotNull] ChessVCParser.UnaryNotContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>OpBitwiseXor</c>
	/// labeled alternative in <see cref="ChessVCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOpBitwiseXor([NotNull] ChessVCParser.OpBitwiseXorContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>OpMultDivMod</c>
	/// labeled alternative in <see cref="ChessVCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOpMultDivMod([NotNull] ChessVCParser.OpMultDivModContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>OpLogicalAnd</c>
	/// labeled alternative in <see cref="ChessVCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOpLogicalAnd([NotNull] ChessVCParser.OpLogicalAndContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>OpComparison</c>
	/// labeled alternative in <see cref="ChessVCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOpComparison([NotNull] ChessVCParser.OpComparisonContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>OpBitwiseAnd</c>
	/// labeled alternative in <see cref="ChessVCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOpBitwiseAnd([NotNull] ChessVCParser.OpBitwiseAndContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ListExpr</c>
	/// labeled alternative in <see cref="ChessVCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitListExpr([NotNull] ChessVCParser.ListExprContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ParenExpr</c>
	/// labeled alternative in <see cref="ChessVCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParenExpr([NotNull] ChessVCParser.ParenExprContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>OpBitShift</c>
	/// labeled alternative in <see cref="ChessVCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOpBitShift([NotNull] ChessVCParser.OpBitShiftContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>OpLogicalOr</c>
	/// labeled alternative in <see cref="ChessVCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOpLogicalOr([NotNull] ChessVCParser.OpLogicalOrContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>FnCallExpr</c>
	/// labeled alternative in <see cref="ChessVCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFnCallExpr([NotNull] ChessVCParser.FnCallExprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="ChessVCParser.objectid"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitObjectid([NotNull] ChessVCParser.ObjectidContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ConstInt</c>
	/// labeled alternative in <see cref="ChessVCParser.constant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstInt([NotNull] ChessVCParser.ConstIntContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ConstStr</c>
	/// labeled alternative in <see cref="ChessVCParser.constant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstStr([NotNull] ChessVCParser.ConstStrContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ConstRange</c>
	/// labeled alternative in <see cref="ChessVCParser.constant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstRange([NotNull] ChessVCParser.ConstRangeContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ConstDir</c>
	/// labeled alternative in <see cref="ChessVCParser.constant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstDir([NotNull] ChessVCParser.ConstDirContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ConstBoolTrue</c>
	/// labeled alternative in <see cref="ChessVCParser.constant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstBoolTrue([NotNull] ChessVCParser.ConstBoolTrueContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ConstBoolFalse</c>
	/// labeled alternative in <see cref="ChessVCParser.constant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstBoolFalse([NotNull] ChessVCParser.ConstBoolFalseContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ConstNull</c>
	/// labeled alternative in <see cref="ChessVCParser.constant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstNull([NotNull] ChessVCParser.ConstNullContext context);
}
