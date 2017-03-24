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
            line = 1;
        }

        internal static void checkForWhitespace()
        {
            if (!textTmp.StartsWith(LexemeHashTable.LexemeDictinary[Lexeme.WHITESPACE]))
                throw new NotImplementedException();
        }

        internal static void withdrawLexem(Lexeme lex)
        {
            checkForComment();
            if (!removeWhitespace())
                throw new NotImplementedException();

            var match = Regex.Match(textTmp, LexemeHashTable.LexemeDictinary[lex]);

            if (match.Success)
            {

                if (match.Index > 0)
                {
                    throw new NotImplementedException();
                }

                string removedPart = textTmp.Substring(0, match.Length);
                countSkipedLines(removedPart);

                textTmp = textTmp.Substring(match.Length);
                Console.WriteLine("removed part: " + removedPart);
            }
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

            return Lexeme.DEFAULT;
        }

        internal static Lexeme getValueLexeme()
        {
            checkForComment();
            if (!removeWhitespace())
                throw new NotImplementedException();

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

            /*
            if (textTmp.StartsWith(LexemeHashTable.LexemeDictinary[Lexeme.OPENING_PARENTHESIS]))
                return Lexeme.OPENING_PARENTHESIS;
                */

            return Lexeme.DEFAULT;
        }

        internal static Lexeme getFunctionLexeme()
        {
            checkForComment();
            if (!removeWhitespace())
                throw new NotImplementedException();

            if (matchLexeme(Lexeme.COMMA))
                return Lexeme.COMMA;

            return Lexeme.DEFAULT;
        }

        internal static Lexeme getSelectorLexeme()
        {
            checkForComment();
            if (!removeWhitespace())
                throw new NotImplementedException();

            if (matchLexeme(Lexeme.CLASS))
                return Lexeme.CLASS;
            if (matchLexeme(Lexeme.ID))
                return Lexeme.ID;
            if (matchLexeme(Lexeme.NAME))
                return Lexeme.NAME;

            return Lexeme.DEFAULT;
        }

        internal static Lexeme getComplexSelectorLexeme()
        {
            checkForComment();
            if (!removeWhitespace())
                throw new NotImplementedException();

            if (matchLexeme(Lexeme.CLASS))
                return Lexeme.CLASS;
            if (matchLexeme(Lexeme.ID))
                return Lexeme.ID;

            return Lexeme.DEFAULT;
        }

        internal static Lexeme getSelectorTailLexeme()
        {
            checkForComment();
            if (!removeWhitespace())
                throw new NotImplementedException();

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
            checkForComment();
            if (!removeWhitespace())
                throw new NotImplementedException();

            if (matchLexeme(Lexeme.COMMA))
                return Lexeme.COMMA;

            return Lexeme.DEFAULT;
        }

        internal static Lexeme getDefinitionLexeme()
        {
            checkForComment();
            if (!removeWhitespace())
                throw new NotImplementedException();

            if (matchLexeme(Lexeme.NAME))
                return Lexeme.NAME;

            return Lexeme.DEFAULT;
        }

        internal static Lexeme getPropertiesLexeme()
        {
            checkForComment();
            if (!removeWhitespace())
                throw new NotImplementedException();

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
                if(index < 0 )
                    throw new NotImplementedException();

                string comment = textTmp.Substring(0, index + 2);
                countSkipedLines(comment);
                Console.WriteLine("skipped comment: " + comment);

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
            string skippedLine = s.Substring(0, s.IndexOf(Environment.NewLine));
            Console.WriteLine("skipped line: " + skippedLine);
            return s.Substring(s.IndexOf(Environment.NewLine) + 2);
        }

        private static void countSkipedLines(string s)
        {
            int count = s.Length - s.Replace("\n", "").Length;
            line += count;
            Console.WriteLine("skipped lines: " + count);
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
