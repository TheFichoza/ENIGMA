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
        private int position;
        public Rotor(int num, int pos)
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
            position = pos;
        }
        public int Forward(int element)
        {
            return (mapping[(element+position)%26, 1]-position+26)%26;
        }       
        public int Back(int element)
        {
            element = (element+position)%26;
            for (int i = 0; i < 26; i++)
            {
                if (mapping[i, 1] == element) 
                {
                    element = mapping[i, 0]; break; 
                }
            }
            element = (element - position + 26) % 26;
            return element;
        }
        public void TurnOver()
        {
            position = (position + 1) % 26;
        }
        public bool Notch()
        {
            return position == 25;
        }
    }
}
