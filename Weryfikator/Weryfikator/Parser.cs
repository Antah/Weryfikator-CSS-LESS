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
            parserEnd();
        }

        private static void parserEnd()
        {
            throw new NotImplementedException();
        }

        private static void parserList()
        {
            throw new NotImplementedException();
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

                // style
                default:
                    break;
            }
        }

        private static void parserValue()
        {
            throw new NotImplementedException();
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
                    break;
                case Lexeme.OPENING_PARENTHESIS:
                    withdrawLexem(Lexeme.OPENING_PARENTHESIS);
                    parserOperand();
                    withdrawLexem(Lexeme.CLOSING_PARENTHESIS);
                    break;
                case Lexeme.FUNCTION:
                    withdrawLexem(Lexeme.FUNCTION);
                    break;
                default:
                    withdrawLexem(Lexeme.TEXT);
                    break;
            }
        }
    }
}
