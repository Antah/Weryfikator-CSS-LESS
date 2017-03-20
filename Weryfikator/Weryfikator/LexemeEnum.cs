using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Weryfikator
{
    enum Lexeme
    {
        OPENING_BRACE,
        CLOSING_BRACE,
        OPENING_PARENTHESIS,
        CLOSING_PARENTHESIS,
        COLON,
        SEMICOLON,
        AT,
        WHITESPACE,
        TEXT,
        COLOR,
        NUMBER,
        QUOTATION,
        CLOSING_ANGLE_BRACKET,
        TILDE,
        COMMENT,
        OPEN_COMMENT,
        CLOSE_COMMENT,
        LINE_COMMENT,
        DOUBLESLASH,
        NEW_LINE,
        PLUS,
        MINUS,
        STAR,
        COMMA,
        HASH,
        UNIT,
        END,
        FUNCTION,
        SLASH,
        DOT
    }

    public static class LexemeHashTable
    {
        internal static Dictionary<Lexeme, String> LexemeDictinary = new Dictionary<Lexeme, String>
        {
            { Lexeme.OPENING_BRACE, "{" },
            { Lexeme.CLOSING_BRACE, "}" },
            { Lexeme.OPENING_PARENTHESIS, "(" },
            { Lexeme.CLOSING_PARENTHESIS, ")" },
            { Lexeme.COLON, ":" },
            { Lexeme.SEMICOLON, ";" },
            { Lexeme.AT, "@" },
            { Lexeme.WHITESPACE, "" },
            { Lexeme.TEXT, "" },
            { Lexeme.COLOR, "" },
            { Lexeme.NUMBER, "" },
            { Lexeme.QUOTATION, "" },
            { Lexeme.CLOSING_ANGLE_BRACKET, ">" },
            { Lexeme.TILDE, "~" },
            { Lexeme.OPEN_COMMENT, "/*" },
            { Lexeme.CLOSE_COMMENT, "*/" },
            { Lexeme.LINE_COMMENT, "" },
            { Lexeme.DOUBLESLASH, "//" },
            { Lexeme.NEW_LINE, "\n" },
            { Lexeme.PLUS, "+" },
            { Lexeme.MINUS, "-" },
            { Lexeme.STAR, "*" },
            { Lexeme.COMMA, "," },
            { Lexeme.HASH, "#" },
            { Lexeme.UNIT, "" },
            { Lexeme.END, "" },
            { Lexeme.FUNCTION, "" },
            { Lexeme.SLASH, "/" },
            { Lexeme.DOT, "." },
        };
        
    }
}
