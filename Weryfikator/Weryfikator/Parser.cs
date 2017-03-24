using System;

namespace Weryfikator
{
    public static class Parser
    {
        private static Lexeme current_lexeme;

        private static void withdrawLexem(Lexeme lex)
        {
            Lexer.withdrawLexem(lex);
        }

        private static void withdrawWhitespace()
        {
            Lexer.checkForWhitespace();
        }

        public static void parserStart(string text)
        {
            Lexer.setText(text);
            parserList();
        }

        private static void parserEnd()
        {
            
        }

        private static void parserList()
        {
            current_lexeme = Lexer.getNewLexeme();
            switch (current_lexeme)
            {
                // variable
                case Lexeme.VARIABLE:
                    withdrawLexem(Lexeme.VARIABLE);
                    withdrawLexem(Lexeme.COLON);
                    parserValue();
                    withdrawLexem(Lexeme.SEMICOLON);
                    break;

                // style
                default:
                    parserSelector();
                    parserSelectorTail();
                    withdrawLexem(Lexeme.OPENING_BRACE);
                    parserDefinition();
                    withdrawLexem(Lexeme.CLOSING_BRACE);
                    break;
            }
            parserList();
        }

        private static void parserValue()
        {
            current_lexeme = Lexer.getValueLexeme();
            switch (current_lexeme)
            {
                case Lexeme.VARIABLE:
                    withdrawLexem(Lexeme.VARIABLE);
                    break;
                case Lexeme.QUOTATION:
                    withdrawLexem(Lexeme.QUOTATION);
                    withdrawLexem(Lexeme.TEXT);
                    withdrawLexem(Lexeme.QUOTATION);
                    break;
                case Lexeme.COLOR:
                    withdrawLexem(Lexeme.COLOR);
                    break;
                case Lexeme.NUMBER:
                    withdrawLexem(Lexeme.NUMBER);
                    break;
                /*case Lexeme.OPENING_PARENTHESIS:
                    withdrawLexem(Lexeme.OPENING_PARENTHESIS);
                    parserOperationHead();
                    parserOperationTail();
                    withdrawLexem(Lexeme.CLOSING_PARENTHESIS);
                    break;*/
                case Lexeme.FUNCTION:
                    withdrawLexem(Lexeme.FUNCTION);
                    parserValue();
                    parserFunction();
                    withdrawLexem(Lexeme.CLOSING_PARENTHESIS);
                    break;
                case Lexeme.NAME:
                    withdrawLexem(Lexeme.NAME);
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }
        }

        private static void parserFunction()
        {
            current_lexeme = Lexer.getFunctionLexeme();
            switch (current_lexeme)
            {
                case Lexeme.COMMA:
                    withdrawLexem(Lexeme.COMMA);
                    parserValue();
                    break;
                default:
                    break;
            }
        }

        private static void parserSelector()
        {
            current_lexeme = Lexer.getSelectorLexeme();
            switch (current_lexeme)
            {
                case Lexeme.CLASS:
                    withdrawLexem(Lexeme.CLASS);
                    withdrawLexem(Lexeme.NAME);
                    break;
                case Lexeme.ID:
                    withdrawLexem(Lexeme.ID);
                    withdrawLexem(Lexeme.NAME);
                    break;
                case Lexeme.NAME:
                    withdrawLexem(Lexeme.NAME);
                    parserComplexSelector();
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }
        }

        private static void parserComplexSelector()
        {
            current_lexeme = Lexer.getComplexSelectorLexeme();
            switch (current_lexeme)
            {
                case Lexeme.CLASS:
                    withdrawLexem(Lexeme.CLASS);
                    withdrawLexem(Lexeme.NAME);
                    break;
                case Lexeme.ID:
                    withdrawLexem(Lexeme.ID);
                    withdrawLexem(Lexeme.NAME);
                    break;
                default:
                    break;
            }
        }

        private static void parserSelectorTail()
        {
            current_lexeme = Lexer.getSelectorTailLexeme();
            switch (current_lexeme)
            {
                case Lexeme.TILDE:
                    withdrawLexem(Lexeme.TILDE);
                    parserSelector();
                    parserSelectorTail();
                    break;
                case Lexeme.CLOSING_ANGLE_BRACKET:
                    withdrawLexem(Lexeme.CLOSING_ANGLE_BRACKET);
                    parserSelector();
                    parserSelectorTail();
                    break;
                case Lexeme.PLUS:
                    withdrawLexem(Lexeme.PLUS);
                    parserSelector();
                    parserSelectorTail();
                    break;
                case Lexeme.COMMA:
                    withdrawLexem(Lexeme.COMMA);
                    parserSelectorGroup();
                    break;
                default:
                    break;
            }
        }

        private static void parserSelectorGroup()
        {
            current_lexeme = Lexer.getSelectorGroupLexeme();
            parserSelector();
            switch (current_lexeme)
            {
                case Lexeme.COMMA:
                    withdrawLexem(Lexeme.COMMA);
                    parserSelectorGroup();
                    break;
                default:
                    break;
            }
        }

        private static void parserDefinition()
        {
            current_lexeme = Lexer.getDefinitionLexeme();
            switch (current_lexeme)
            {
                case Lexeme.NAME:
                    withdrawLexem(Lexeme.NAME);
                    withdrawLexem(Lexeme.COLON);
                    parserValue();
                    parserProperties();
                    break;
                default:
                    return;
            }
            parserDefinition();
        }

        private static void parserProperties()
        {
            current_lexeme = Lexer.getPropertiesLexeme();
            switch (current_lexeme)
            {
                case Lexeme.SEMICOLON:
                    withdrawLexem(Lexeme.SEMICOLON);
                    break;
                default:
                    withdrawWhitespace();
                    parserValue();
                    parserProperties();
                    break;
            }
        }

        private static void parserOperationHead()
        {
            throw new NotImplementedException();
            switch (current_lexeme)
            {
                case Lexeme.COLOR:
                    withdrawLexem(Lexeme.COLOR);
                    break;
                case Lexeme.NUMBER:
                    withdrawLexem(Lexeme.NUMBER);
                    break;
                case Lexeme.OPENING_PARENTHESIS:
                    withdrawLexem(Lexeme.OPENING_PARENTHESIS);
                    parserOperationHead();
                    parserOperationTail();
                    withdrawLexem(Lexeme.CLOSING_PARENTHESIS);
                    break;
            }
        }

        private static void parserOperationTail()
        {
            switch (current_lexeme)
            {
                case Lexeme.STAR:
                    withdrawLexem(Lexeme.STAR);
                    parserOperationHead();
                    break;
                case Lexeme.SLASH:
                    withdrawLexem(Lexeme.SLASH);
                    parserOperationHead();
                    break;
                case Lexeme.PLUS:
                    withdrawLexem(Lexeme.PLUS);
                    parserOperationHead();
                    break;
                case Lexeme.MINUS:
                    withdrawLexem(Lexeme.MINUS);
                    parserOperationHead();
                    break;
                default:
                    break;
            }
        }
    }
}
