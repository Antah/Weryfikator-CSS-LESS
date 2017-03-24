using System;
using System.Collections.Generic;

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
        NAME,
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
        CLASS,
        UNIT,
        END,
        FUNCTION,
        SLASH,
        ID,
        VARIABLE,
        TEXT,
        DEFAULT
    }

    public static class LexemeHashTable
    {
        internal static Dictionary<Lexeme, String> LexemeDictinary = new Dictionary<Lexeme, String>
        {
            { Lexeme.OPENING_BRACE, @"^\s*\{" },
            { Lexeme.CLOSING_BRACE, @"^\s*\}" },
            { Lexeme.OPENING_PARENTHESIS, @"^\s*\(" },
            { Lexeme.CLOSING_PARENTHESIS, @"^\s*\)" },
            { Lexeme.CLOSING_ANGLE_BRACKET, @"\s*>" },

            { Lexeme.COLON, @"^\s*:" },
            { Lexeme.SEMICOLON, @"^\s*;" },
            { Lexeme.COMMA, @"\s*," },           
            { Lexeme.QUOTATION, @"\s*[""']" },          
            { Lexeme.TILDE, @"\s*~" },

            { Lexeme.OPEN_COMMENT, @"^\s*/\*" },
            { Lexeme.CLOSE_COMMENT, @"^\s*\*/" },
            { Lexeme.DOUBLESLASH, @"//" },

            { Lexeme.WHITESPACE, @"^\s+" },
            { Lexeme.NEW_LINE, @"\n" },

            { Lexeme.PLUS, @"^\s*\+" },
            { Lexeme.MINUS, @"^\s*-" },
            { Lexeme.STAR, @"^\s*\*" },
            { Lexeme.SLASH, @"^\s*/" },
            
            { Lexeme.FUNCTION, @"\s*[A-Za-z0-9\*]+\(" }, 

            { Lexeme.NUMBER, @"^\s*-?[0-9]+(\.[0-9]+)?(\s*(px|%))?" },
            { Lexeme.COLOR, @"\s*\#[0-9a-fA-F]{3}([0-9a-fA-F]{3})?" },

            { Lexeme.TEXT, @"^[^""']*" },
            { Lexeme.NAME, @"^\s*[A-Za-z0-9\*-]+" },

            { Lexeme.CLASS, @"^\s*\#[A-Za-z0-9\*]+" },
            { Lexeme.ID, @"^\s*\.[A-Za-z0-9\*]+" },
            { Lexeme.VARIABLE, @"^\s*\@[A-Za-z0-9\*]+" }
        };
        
    }
}
