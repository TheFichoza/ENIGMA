using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ENIGMA
{
    public class Plugboard
    {
        private const int limit = 3;
        HashSet<int> usedChars;
        Dictionary<int, int> maps;
        public Plugboard()
        {
            usedChars = new HashSet<int>();
            maps = new Dictionary<int, int>();
        }
        public Plugboard(string[] plugs)
        {
            usedChars = new HashSet<int>();
            maps = new Dictionary<int, int>();
            foreach (string plug in plugs)
            {
                AddPlug(plug);
            }
        }
        public string AddPlug(string conn)
        {
            if (Regex.IsMatch(conn, "^[A-Z]-[A-Z]$"))
            {
                int symbol1 = conn[0] - 65,
                symbol2 = conn[2] - 65;
                if (maps.Count < limit)
                {
                    if (usedChars.Contains(symbol1) || usedChars.Contains(symbol2))
                    {
                        return "Character already connected!";
                    }
                    else
                    {
                        usedChars.Add(symbol1);
                        usedChars.Add(symbol2);
                        maps[symbol1] = symbol2;
                        return "Successfully added";
                    }
                }
                else return "Too many connections!";
            }
            else return "Invalid plug format!";
        }
        public int Connect(int symbol)
        {
            if (usedChars.Contains(symbol))
            {
                if (maps.Values.Contains(symbol))
                {
                    return maps.FirstOrDefault(x => x.Value == symbol).Key;
                }
                else 
                {
                    return maps[symbol];
                }
            }
            return symbol;
        }
        public override string ToString()
        {
            string s = "";
            foreach (KeyValuePair<int, int> pair in maps)
            {
                s += $"{(char)(pair.Key+65)}-{(char)(pair.Value + 65)} ";
            }
            return s;
        }
    }
}
