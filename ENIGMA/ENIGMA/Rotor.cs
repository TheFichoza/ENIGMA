using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENIGMA
{
    class Rotor
    {
        private int[] forward, reverse;
        private int position;
        public Rotor(int num, int pos)
        {
            switch (num)
            {
                case 1:
                    forward = RotorSelection.forward1;
                    reverse = RotorSelection.reverse1;
                    break;
                case 2:
                    forward = RotorSelection.forward2;
                    reverse = RotorSelection.reverse2;
                    break;
                case 3:
                    forward = RotorSelection.forward3;
                    reverse = RotorSelection.reverse3;
                    break;
                case 4:
                    forward = RotorSelection.forward4;
                    reverse = RotorSelection.forward4;
                    break;
                case 5:
                    forward = RotorSelection.forward5;
                    reverse = RotorSelection.reverse5;
                    break;
                default:
                    throw new IndexOutOfRangeException("Invalid Rotor");
            }
            position = pos;
        }
        public int Forward(int element)
        {
            return (forward[(element + position) % 26] - position + 26) % 26;
        }       
        public int Back(int element)
        {
            return (reverse[(element + position) % 26] - position + 26) % 26;
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
