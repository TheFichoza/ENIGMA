using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENIGMA
{
    public class Plug
    {
        private char s1, s2;
        public Plug(char symbol1, char symbol2)
        {
            if(symbol1 == symbol2) throw new ArgumentException("Невалидни връзки!");
            s1 = symbol1;
            s2 = symbol2;
        }
        public char Connect(char symbol)
        {
            if (symbol == s1) { symbol = s2; return symbol; }
            if (symbol == s2) { symbol = s1; return symbol; }
            return symbol;
        }
        public override string ToString()
        {
            return $"{s1}-{s2}";
        }
        public bool Contains(char a, char b)
        {
            if (a == s1 || a == s2 || b == s1 || b == s2) return true;
            return false;
        }
    }
}
