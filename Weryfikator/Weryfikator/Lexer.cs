using System;
using System.Text.RegularExpressions;

namespace Weryfikator
{
    public static class Lexer
    {
        private static string textTmp;
        private static int line;

        internal static void setText(string text)
        {
            textTmp = text;
            textTmp.Replace("\r\n", "\n").Replace("\r", "\n");
            line = 1;
        }

        internal static bool checkForWhitespace()
        {
            if (!textTmp.StartsWith(LexemeHashTable.LexemeDictinary[Lexeme.WHITESPACE]))
                return false;
            return true;
        }

        private static bool checkForEOF()
        {
            checkForComment();
            if (!removeWhitespace())
                return false;
            return true;
        }

        internal static bool withdrawLexem(Lexeme lex)
        {
            if (!checkForEOF()) {
                Program.form.SetErrorMessage("Error in line: " + line + ". Unexpected EOF.");
                return false;
            }

            var match = Regex.Match(textTmp, LexemeHashTable.LexemeDictinary[lex]);

            if (match.Success)
            {

                if (match.Index > 0)
                {
                    Program.form.SetErrorMessage("Error in line: " + line + ". Expecting " + lex.ToString() + " lexeme.");
                    return false;
                }

                string removedPart = textTmp.Substring(0, match.Length);
                countSkipedLines(removedPart);

                textTmp = textTmp.Substring(match.Length);
                return true;
            }
            Program.form.SetErrorMessage("Error in line: " + line + ". Expecting " + lex.ToString() + " lexeme.");
            return false;
        }

        internal static Lexeme getNewLexeme()
        {
            checkForComment();
            if (!removeWhitespace())
                return Lexeme.END;

            if(matchLexeme(Lexeme.VARIABLE))
                return Lexeme.VARIABLE;
            if (matchLexeme(Lexeme.CLASS))
                return Lexeme.CLASS;
            if (matchLexeme(Lexeme.ID))
                return Lexeme.ID;
            if (matchLexeme(Lexeme.NAME))
                return Lexeme.NAME;

            Program.form.SetErrorMessage("Error in line: " + line + ". Expecting new style or variable.");
            return Lexeme.DEFAULT;
        }

        internal static Lexeme getValueLexeme()
        {
            checkForEOF();

            if (matchLexeme(Lexeme.VARIABLE))
                return Lexeme.VARIABLE;
            if (matchLexeme(Lexeme.QUOTATION))
                return Lexeme.QUOTATION;
            if (matchLexeme(Lexeme.COLOR))
                return Lexeme.COLOR;
            if (matchLexeme(Lexeme.NUMBER))
                return Lexeme.NUMBER;
            if (matchLexeme(Lexeme.FUNCTION))
                return Lexeme.FUNCTION;
            if (matchLexeme(Lexeme.NAME))
                return Lexeme.NAME;

            Program.form.SetErrorMessage("Error in line: " + line + ". Expecting a value.");
            return Lexeme.DEFAULT;
        }

        internal static Lexeme getFunctionLexeme()
        {
            checkForEOF();

            if (matchLexeme(Lexeme.COMMA))
                return Lexeme.COMMA;

            return Lexeme.DEFAULT;
        }

        internal static Lexeme getSelectorLexeme()
        {
            checkForEOF();

            if (matchLexeme(Lexeme.CLASS))
                return Lexeme.CLASS;
            if (matchLexeme(Lexeme.ID))
                return Lexeme.ID;
            if (matchLexeme(Lexeme.NAME))
                return Lexeme.NAME;

            Program.form.SetErrorMessage("Error in line: " + line + ". Expecting new selector.");
            return Lexeme.DEFAULT;
        }

        internal static Lexeme getComplexSelectorLexeme()
        {
            checkForEOF();

            if (matchLexeme(Lexeme.CLASS))
                return Lexeme.CLASS;
            if (matchLexeme(Lexeme.ID))
                return Lexeme.ID;

            return Lexeme.DEFAULT;
        }

        internal static Lexeme getSelectorTailLexeme()
        {
            checkForEOF();

            if (matchLexeme(Lexeme.TILDE))
                return Lexeme.TILDE;
            if (matchLexeme(Lexeme.PLUS))
                return Lexeme.PLUS;
            if (matchLexeme(Lexeme.CLOSING_ANGLE_BRACKET))
                return Lexeme.CLOSING_ANGLE_BRACKET;
            if (matchLexeme(Lexeme.COMMA))
                return Lexeme.COMMA;

            return Lexeme.DEFAULT;
        }

        internal static Lexeme getSelectorGroupLexeme()
        {
            checkForEOF();

            if (matchLexeme(Lexeme.COMMA))
                return Lexeme.COMMA;

            return Lexeme.DEFAULT;
        }

        internal static Lexeme getDefinitionLexeme()
        {
            checkForEOF();

            if (matchLexeme(Lexeme.NAME))
                return Lexeme.NAME;

            return Lexeme.DEFAULT;
        }

        internal static Lexeme getPropertiesLexeme()
        {
            checkForEOF();

            if (matchLexeme(Lexeme.SEMICOLON))
                return Lexeme.SEMICOLON;

            return Lexeme.DEFAULT;
        }

        private static void checkForComment()
        {
            removeWhitespace();
            if (textTmp.StartsWith("//"))
            {
                textTmp = removeFirstLine(textTmp);
                checkForComment();
            }
            else if(textTmp.StartsWith("/*"))
            {
                int index = textTmp.IndexOf("*/");
                if (index < 2)
                    return;

                string comment = textTmp.Substring(0, index + 2);
                countSkipedLines(comment);

                textTmp = textTmp.Substring(index + 2);
                checkForComment();
            }
        }


        private static bool removeWhitespace()
        {
            var match = Regex.Match(textTmp, @"\S");

            if (match.Success)
            {
                if (match.Index > 0)
                {
                    string whitespace = textTmp.Substring(0, match.Index);
                    countSkipedLines(whitespace);
                }

                textTmp = textTmp.Substring(match.Index);
                if (textTmp.Length <= 1)
                    return false;
                return true;
            }
            return false;
        }

        private static string removeFirstLine(string s)
        {
            line++;
            int index = s.IndexOf("\n");
            string skippedLine = s.Substring(0, index);
            return s.Substring(index + 1);
        }

        private static void countSkipedLines(string s)
        {
            int count = s.Length - s.Replace("\n", "").Length;
            line += count;
        }

        private static bool matchLexeme(Lexeme lex)
        {
            var match = Regex.Match(textTmp, LexemeHashTable.LexemeDictinary[lex]);

            if (match.Success && match.Index == 0)
                return true;

            return false;
        }
    }
}
