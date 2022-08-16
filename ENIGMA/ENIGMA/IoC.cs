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
        public decimal ioc;
        public Key(int[] rotor_config,int[] positions, int[] histogram)
        {
            this.rotor_config = rotor_config;
            start_position[0] = positions[0];
            start_position[1] = positions[1];
            start_position[2] = positions[2];
            ioc = IoC(histogram);
        }
        public Key()
        {
            ioc = 1;
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
