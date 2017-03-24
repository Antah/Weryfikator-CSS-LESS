using System;

namespace Weryfikator
{
    public static class Parser
    {
        private static Lexeme current_lexeme;

        private static bool withdrawLexem(Lexeme lex)
        {
            return Lexer.withdrawLexem(lex);
        }

        public static bool parserStart(string text)
        {
            Lexer.setText(text);
            if (!parserList())
                return false;

            Program.form.SetErrorMessage("Verification successful.");
            return true;
        }

        private static bool parserList()
        {
            current_lexeme = Lexer.getNewLexeme();
            switch (current_lexeme)
            {
                // variable
                case Lexeme.VARIABLE:
                    if (!withdrawLexem(Lexeme.VARIABLE)
                        || !withdrawLexem(Lexeme.COLON)
                        || !parserValue()
                        || !withdrawLexem(Lexeme.SEMICOLON))
                        return false;
                    break;

                case Lexeme.END:
                    return true;

                // style
                default:
                    if (!parserSelector()
                        || !parserSelectorTail()
                        || !withdrawLexem(Lexeme.OPENING_BRACE)
                        || !parserDefinition()
                        || !withdrawLexem(Lexeme.CLOSING_BRACE))
                        return false;
                    break;
            }
            return parserList();
        }

        private static bool parserValue()
        {
            current_lexeme = Lexer.getValueLexeme();
            switch (current_lexeme)
            {
                case Lexeme.VARIABLE:
                    if (!withdrawLexem(Lexeme.VARIABLE))
                        return false;
                    return true;
                case Lexeme.QUOTATION:
                    if (!withdrawLexem(Lexeme.QUOTATION)
                    || !withdrawLexem(Lexeme.TEXT)
                    || !withdrawLexem(Lexeme.QUOTATION))
                        return false;
                    return true;
                case Lexeme.COLOR:
                    if (!withdrawLexem(Lexeme.COLOR))
                        return false;
                    return true;
                case Lexeme.NUMBER:
                    if (!withdrawLexem(Lexeme.NUMBER))
                        return false;
                    return true;
                case Lexeme.FUNCTION:
                    if (!withdrawLexem(Lexeme.FUNCTION)
                    || !parserValue()
                    || !parserFunction()
                    || !withdrawLexem(Lexeme.CLOSING_PARENTHESIS))
                        return false;
                    return true;
                case Lexeme.NAME:
                    if (!withdrawLexem(Lexeme.NAME))
                        return false;
                    return true;
                default:
                    return false;
            }
        }

        private static bool parserFunction()
        {
            current_lexeme = Lexer.getFunctionLexeme();
            switch (current_lexeme)
            {
                case Lexeme.COMMA:
                    if (!withdrawLexem(Lexeme.COMMA)
                    || !parserValue())
                        return false;
                    return true;
                default:
                    return true;
            }
        }

        private static bool parserSelector()
        {
            current_lexeme = Lexer.getSelectorLexeme();
            switch (current_lexeme)
            {
                case Lexeme.CLASS:
                    if (!withdrawLexem(Lexeme.CLASS))
                        return false;
                    return true;
                case Lexeme.ID:
                    if (!withdrawLexem(Lexeme.ID))
                        return false;
                    return true;
                case Lexeme.NAME:
                    if (!withdrawLexem(Lexeme.NAME)
                    || !parserComplexSelector())
                        return false;
                    return true;
                default:
                    return false;
            }
        }

        private static bool parserComplexSelector()
        {
            current_lexeme = Lexer.getComplexSelectorLexeme();
            switch (current_lexeme)
            {
                case Lexeme.CLASS:
                    if (!withdrawLexem(Lexeme.CLASS))
                        return false;
                    return true;
                case Lexeme.ID:
                    if (!withdrawLexem(Lexeme.ID))
                        return false;
                    return true;
                default:
                    return true;
            }
        }

        private static bool parserSelectorTail()
        {
            current_lexeme = Lexer.getSelectorTailLexeme();
            switch (current_lexeme)
            {
                case Lexeme.TILDE:
                    if (!withdrawLexem(Lexeme.TILDE)
                    || !parserSelector()
                    || !parserSelectorTail())
                        return false;
                    return true;
                case Lexeme.CLOSING_ANGLE_BRACKET:
                    if (!withdrawLexem(Lexeme.CLOSING_ANGLE_BRACKET)
                    || !parserSelector()
                    || !parserSelectorTail())
                        return false;
                    return true;
                case Lexeme.PLUS:
                    if (!withdrawLexem(Lexeme.PLUS)
                    || !parserSelector()
                    || !parserSelectorTail())
                        return false;
                    return true;
                case Lexeme.COMMA:
                    if (!withdrawLexem(Lexeme.COMMA)
                    || !parserSelectorGroup())
                        return false;
                    return true;
                default:
                    return true;
            }
        }

        private static bool parserSelectorGroup()
        {
            current_lexeme = Lexer.getSelectorGroupLexeme();
            parserSelector();
            switch (current_lexeme)
            {
                case Lexeme.COMMA:
                    if (!withdrawLexem(Lexeme.COMMA)
                    || !parserSelectorGroup())
                        return false;
                    return true;
                default:
                    return true;
            }
        }

        private static bool parserDefinition()
        {
            current_lexeme = Lexer.getDefinitionLexeme();
            switch (current_lexeme)
            {
                case Lexeme.NAME:
                    if (!withdrawLexem(Lexeme.NAME)
                    || !withdrawLexem(Lexeme.COLON)
                    || !parserValue()
                    || !parserProperties())
                        return false;
                    break;
                default:
                    return true;
            }
            return parserDefinition();
        }

        private static bool parserProperties()
        {
            current_lexeme = Lexer.getPropertiesLexeme();
            switch (current_lexeme)
            {
                case Lexeme.SEMICOLON:
                    if (!withdrawLexem(Lexeme.SEMICOLON))
                        return false;
                    return true;
                default:
                    if (Lexer.checkForWhitespace()
                    || !parserValue()
                    || !parserProperties())
                        return false;
                    return true;
            }
        }
    }
}
