using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENIGMA
{
    class Rotor
    {
        private int[,] mapping;
        int position;
        public Rotor(int num)
        {
            switch (num)
            {
                case 1:
                    mapping = RotorSelection.Rotor1; break;
                case 2:
                    mapping = RotorSelection.Rotor2; break;
                case 3:
                    mapping = RotorSelection.Rotor3; break;
                case 4:
                    mapping = RotorSelection.Rotor4; break;
                case 5:
                    mapping = RotorSelection.Rotor5; break;
                default:
                    throw new IndexOutOfRangeException("Invalid Rotor");
            }
        }
    }
}
