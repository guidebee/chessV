
grammar ChessVC;

chunk
	: unit EOF
	;
	
unit
	: declaration*
	;

declaration
	: pieceTypeDeclaration                                # PieceTypeDecl
	| gameDeclaration                                     # GameDecl
	;
	
pieceTypeDeclaration
	: 'PieceType' objectid '{' declMember* '}'
	;
	
gameDeclaration
	: 'Game' objectid ':' objectid '{' declMember* '}'
	;

declMember
	: assignment                                          # MemberAssign
	| functionDefn                                        # FnDefinition
	| memberDefn                                          # MemberDefinition
	;
	
functionDefn
	: objectid block
	;

memberDefn
	: objectid objectid ';'
	;
	
statement
	: block                                               # BlockStatement
	| 'if' '(' expr ')' statement ('else' statement)?     # IfStatement
	| assignment                                          # AssignStatement
	| functionCall                                        # FnCallStatement
	;
	
block
	: '{' statement* '}'
	;
	
assignment
	: objectid '=' expr ';'
	;
	
functionCall
	: objectid '(' argumentList? ')' ';'
	;
	
argumentList
	: expr (',' expr)*
	;
	
expr
	: functionCall                                        # FnCallExpr
	| '-' expr                                            # UnaryMinus
	| '!' expr                                            # UnaryNot
	| expr ('*' | '/' | '%') expr                         # OpMultDivMod
	| expr ('+' | '-') expr                               # OpAddSub
	| expr ('<<' | '>>') expr                             # OpBitShift
	| expr ('<' | '<=' | '>' | '>=') expr                 # OpComparison
	| expr ('==' | '!=') expr                             # OpEquality
	| expr '&' expr                                       # OpBitwiseAnd
	| expr '^' expr                                       # OpBitwiseXor
	| expr '|' expr                                       # OpBitwiseOr
	| expr '&&' expr                                      # OpLogicalAnd
	| expr '||' expr                                      # OpLogicalOr
	| objectid                                            # ObjectIdExpr
	| constant                                            # ConstantExpr
	| '(' expr ')'                                        # ParenExpr
	| '{' (expr (',' expr)*)? '}'                         # ListExpr
	;

objectid
	: IDENTIFIER ('.' IDENTIFIER)*
	;
	
constant
	: INTEGER                                             # ConstInt
	| STRING                                              # ConstStr
	| INTEGER '..' INTEGER                                # ConstRange
	| '<' ('-')? INTEGER ',' ('-')? INTEGER '>'           # ConstDir
	| 'true'                                              # ConstBoolTrue
	| 'false'                                             # ConstBoolFalse
	| 'null'                                              # ConstNull
	;

fragment ID_ESC
	: '\\\''
	| '\\\\'
	;

IDENTIFIER
	: ('@' | ':')? [a-zA-Z_][a-zA-Z0-9_]*
	| ('@' | ':')? '\'' (ID_ESC | .)*? '\''
	;

fragment STR_ESC
	: '\\"' 
	| '\\\\'
	;
	 
STRING
	: '"' (STR_ESC | .)*? '"'
	;
	
fragment DIGIT
	: [0-9]
	;

INTEGER
	: DIGIT+
	;

LINE_COMMENT
	: '//' .*? '\r'? '\n' -> channel(HIDDEN)
	;
	
COMMENT
	: '/*' .*? '*/' -> channel(HIDDEN)
	;

WHITESPACE: [ \t\r\n]+ -> skip ; // skip spaces, tabs, newlines
