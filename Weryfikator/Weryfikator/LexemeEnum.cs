using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        FUNCTION
    }
}
