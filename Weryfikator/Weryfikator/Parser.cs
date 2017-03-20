using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weryfikator;

namespace Weryfikator
{
    public static class Parser
    {
        private static Lexeme current_lexeme;

        private static void withdrawLexem(Lexeme dOUBLESLASH)
        {
            throw new NotImplementedException();
        }

        public static void parserStart()
        {
            parserList();
        }

        private static void parserEnd()
        {
            withdrawLexem(Lexeme.END);
        }

        private static void parserList()
        {
            switch (current_lexeme)
            {
                // variable
                case Lexeme.AT:
                    withdrawLexem(Lexeme.AT);
                    withdrawLexem(Lexeme.TEXT);
                    withdrawLexem(Lexeme.COLON);
                    parserValue();
                    withdrawLexem(Lexeme.SEMICOLON);
                    break;

                // comment
                case Lexeme.DOUBLESLASH:
                    withdrawLexem(Lexeme.DOUBLESLASH);
                    withdrawLexem(Lexeme.LINE_COMMENT);
                    withdrawLexem(Lexeme.NEW_LINE);
                    break;
                case Lexeme.OPEN_COMMENT:
                    withdrawLexem(Lexeme.OPEN_COMMENT);
                    withdrawLexem(Lexeme.COMMENT);
                    withdrawLexem(Lexeme.CLOSE_COMMENT);
                    break;

                //end
                case Lexeme.END:
                    parserEnd();
                    return;

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

        private static void parserDefinition()
        {
            switch (current_lexeme)
            {
                // comment
                case Lexeme.DOUBLESLASH:
                    withdrawLexem(Lexeme.DOUBLESLASH);
                    withdrawLexem(Lexeme.LINE_COMMENT);
                    withdrawLexem(Lexeme.NEW_LINE);
                    break;
                case Lexeme.OPEN_COMMENT:
                    withdrawLexem(Lexeme.OPEN_COMMENT);
                    withdrawLexem(Lexeme.COMMENT);
                    withdrawLexem(Lexeme.CLOSE_COMMENT);
                    break;

                // property
                case Lexeme.TEXT:
                    withdrawLexem(Lexeme.TEXT);
                    withdrawLexem(Lexeme.COLON);
                    parserValue();
                    parserProperties();
                    withdrawLexem(Lexeme.SEMICOLON);
                    break;
                default:
                    return;
            }
            parserDefinition();
        }

        private static void parserProperties()
        {
            switch (current_lexeme)
            {
                case Lexeme.SEMICOLON:
                    return;
                default:
                    parserValue();
                    break;
            }
        }

        private static void parserSelector()
        {
            switch (current_lexeme)
            {
                case Lexeme.HASH:
                    withdrawLexem(Lexeme.HASH);
                    withdrawLexem(Lexeme.TEXT);
                    break;
                case Lexeme.DOT:
                    withdrawLexem(Lexeme.DOT);
                    withdrawLexem(Lexeme.TEXT);
                    break;
                case Lexeme.TEXT:
                    withdrawLexem(Lexeme.TEXT);
                    parserComplexSelector();
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }
        }

        private static void parserSelectorTail()
        {
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

        private static void parserComplexSelector()
        {
            switch (current_lexeme)
            {
                case Lexeme.HASH:
                    withdrawLexem(Lexeme.HASH);
                    withdrawLexem(Lexeme.TEXT);
                    break;
                case Lexeme.DOT:
                    withdrawLexem(Lexeme.DOT);
                    withdrawLexem(Lexeme.TEXT);
                    break;
                default:
                    break;
            }
        }

        private static void parserValue()
        {
            switch (current_lexeme)
            {
                case Lexeme.AT:
                    withdrawLexem(Lexeme.AT);
                    withdrawLexem(Lexeme.TEXT);
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
                    parserUnit();
                    break;
                case Lexeme.OPENING_PARENTHESIS:
                    withdrawLexem(Lexeme.OPENING_PARENTHESIS);
                    parserOperationHead();
                    parserOperationTail();
                    withdrawLexem(Lexeme.CLOSING_PARENTHESIS);
                    break;
                case Lexeme.FUNCTION:
                    withdrawLexem(Lexeme.FUNCTION);
                    parserValue();
                    parserFunction();
                    withdrawLexem(Lexeme.CLOSING_PARENTHESIS);
                    break;
                case Lexeme.TEXT:
                    withdrawLexem(Lexeme.TEXT);
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }
        }

        private static void parserFunction()
        {
            switch (current_lexeme)
            {
                case Lexeme.COMMA:
                    withdrawLexem(Lexeme.COMMA;
                    parserValue();
                    break;
                default:
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

        private static void parserUnit()
        {
            switch (current_lexeme)
            {
                case Lexeme.UNIT:
                    withdrawLexem(Lexeme.UNIT);
                    parserOperationHead();
                    break;
                default:
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
                    parserUnit();
                    break;
                case Lexeme.OPENING_PARENTHESIS:
                    withdrawLexem(Lexeme.OPENING_PARENTHESIS);
                    parserOperationHead();
                    parserOperationTail();
                    withdrawLexem(Lexeme.CLOSING_PARENTHESIS);
                    break;
            }
        }


    }
}
