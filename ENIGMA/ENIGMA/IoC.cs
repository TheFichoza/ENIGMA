using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENIGMA
{
    class Key
    {
        private int[] rotor_config, start_position = new int[3];
        public double ioc;
        private string text;
        public Key(int[] rotor_config,int pos1,int pos2,int pos3, int[] histogram)
        {
            this.rotor_config = rotor_config;
            start_position[0] = pos1;
            start_position[1] = pos2;
            start_position[2] = pos3;
            ioc = IoC(histogram);
        }
        private double IoC(int[] histogram)
        {
            double output = 0;
            for (int i = 0; i < 26; i++)
            {
                if (histogram[i] != 0) output += (histogram[i] * (histogram[i] - 1));

            }
            output /= (histogram.Sum()*(histogram.Sum()-1));
            output =Math.Abs(output - 0.067);
            return output;
        }
        public override string ToString()
        {
            return $"({string.Join(", ", rotor_config)}): {string.Join(", ", start_position)} - {ioc}{Environment.NewLine}";
        }
    }
}
