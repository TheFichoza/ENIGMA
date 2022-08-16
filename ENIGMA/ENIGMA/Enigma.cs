﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENIGMA
{
    class Enigma
    {
        int[] Reflector = { 24, 17, 20, 7, 16, 18, 11, 3, 15, 23, 13, 6, 14, 10, 12, 8, 4, 1, 5, 25, 2, 22, 21, 9, 0, 19 };
        Rotor Rotor1,Rotor2, Rotor3;
        Plugboard plugboard;
        public Enigma(int[] rotorConfig, int[] rotorPositions, string[] plugList)
        {
            Rotor1 = new Rotor(rotorConfig[0], rotorPositions[0]);
            Rotor2 = new Rotor(rotorConfig[1], rotorPositions[1]);
            Rotor3 = new Rotor(rotorConfig[2], rotorPositions[2]);
            plugboard = new Plugboard(plugList);
        }
        public Enigma(int[] rotorConfig, int[] rotorPositions)
        {
            Rotor1 = new Rotor(rotorConfig[0], rotorPositions[0]);
            Rotor2 = new Rotor(rotorConfig[1], rotorPositions[1]);
            Rotor3 = new Rotor(rotorConfig[2], rotorPositions[2]);
            plugboard = new Plugboard();
        }
        public void Rotate()
        {
            if(Rotor2.Notch())
            {
                Rotor2.TurnOver();
                Rotor3.TurnOver();
            }
            if (Rotor1.Notch())
            {
                Rotor2.TurnOver();
            }
            Rotor1.TurnOver();
        }
        public char Encrypt(char element)
        {
            Rotate();
            int trans = element - 65;
            trans = plugboard.Connect(trans);
            trans = Rotor1.Forward(trans);
            trans = Rotor2.Forward(trans);
            trans = Rotor3.Forward(trans);
            trans = Reflector[trans];
            trans = Rotor3.Back(trans);
            trans = Rotor2.Back(trans);
            trans = Rotor1.Back(trans);
            trans = plugboard.Connect(trans);
            return (char)(trans + 65);

        }
    }
}
