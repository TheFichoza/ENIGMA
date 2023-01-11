using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ENIGMA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //VARIABLES
        int r1 = 0, r2 = 0, r3 = 0;
        int[] histogram = new int[26];
        public static int[,] vars = { { 1, 2, 3 }, { 1, 2, 4 }, { 1, 2, 5 }, { 1, 3, 2 }, { 1, 3, 4 }, { 1, 3, 5 }, { 1, 4, 2 }, { 1, 4, 3 }, { 1, 4, 5 }, { 1, 5, 2 }, { 1, 5, 3 }, { 1, 5, 4 }, { 2, 1, 3 }, { 2, 1, 4 }, { 2, 1, 5 }, { 2, 3, 1 }, { 2, 3, 4 }, { 2, 3, 5 }, { 2, 4, 1 }, { 2, 4, 3 }, { 2, 4, 5 }, { 2, 5, 1 }, { 2, 5, 3 }, { 2, 5, 4 }, { 3, 1, 2 }, { 3, 1, 4 }, { 3, 1, 5 }, { 3, 2, 1 }, { 3, 2, 4 }, { 3, 2, 5 }, { 3, 4, 1 }, { 3, 4, 2 }, { 3, 4, 5 }, { 3, 5, 1 }, { 3, 5, 2 }, { 3, 5, 4 }, { 4, 1, 2 }, { 4, 1, 3 }, { 4, 1, 5 }, { 4, 2, 1 }, { 4, 2, 3 }, { 4, 2, 5 }, { 4, 3, 1 }, { 4, 3, 2 }, { 4, 3, 5 }, { 4, 5, 1 }, { 4, 5, 2 }, { 4, 5, 3 }, { 5, 1, 2 }, { 5, 1, 3 }, { 5, 1, 4 }, { 5, 2, 1 }, { 5, 2, 3 }, { 5, 2, 4 }, { 5, 3, 1 }, { 5, 3, 2 }, { 5, 3, 4 }, { 5, 4, 1 }, { 5, 4, 2 }, { 5, 4, 3 } };
        //ROTOR FUNCTIONS
        private void button2_Click(object sender, EventArgs e)
        {
            r1 = (r1 + 1) % 26;
            label1.Text = (r1 + 1).ToString();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            r1 = (r1-1+26)%26;
            label1.Text = (r1 + 1).ToString();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            r2 = (r2 + 1) % 26;
            label2.Text = (r2 + 1).ToString();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            r2 = (r2 - 1 + 26) % 26;
            label2.Text = (r2 + 1).ToString();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            r3 = (r3 + 1) % 26;
            label3.Text = (r3 + 1).ToString();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            r3 = (r3 - 1 + 26) % 26;
            label3.Text = (r3 + 1).ToString();
        }
        //PLUGBOARD FUNC
        private void button8_Click(object sender, EventArgs e)
        {
            listBox2.Items.Add(textBox2.Text);
            textBox2.Clear();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
        }

        //ENCYPTION
        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox3.Clear();
            int[] rotorConfig = { int.Parse(comboBox1.Text), int.Parse(comboBox2.Text), int.Parse(comboBox3.Text) }, rotorPosition = {r1, r2, r3};
            string[] plugs = new string[listBox2.Items.Count];
            listBox2.Items.CopyTo(plugs, 0);
            Enigma enigma = new Enigma(rotorConfig, rotorPosition, plugs);
            string normal = richTextBox2.Text, cypher = "";
            char encrypted;
            foreach (char symbol in normal)
            {
                char sym = symbol;
                if (symbol > 96 && symbol < 123) sym = (char)(sym - 32);
                else if (symbol < 65 || symbol > 90) { cypher += symbol; continue; }
                encrypted = enigma.Encrypt(sym);
                
                if (symbol > 96 && symbol < 123) 
                {
                    encrypted = (char)(encrypted + 32); 
                }
                cypher += encrypted;
            }
              richTextBox3.Text = cypher;
        }

        //DECRYPTION
        Dictionary<string, decimal> quad;
        Key min;
        decimal hillClimb;
        string format;
        private void button10_Click(object sender, EventArgs e)
        {
            string start = richTextBox1.Text;
            format = "";
            char decSym;
            min = new Key();
            Key temp;
            Enigma enigma;
            histogram = new int[26];
            foreach (char sym in start)
            {
                if (sym > 96 && sym < 123) format += (char)(sym - 32);
                else if (sym > 64 && sym < 91) format += sym;
            }
            
            for (int rotorConfigs = 0; rotorConfigs < 60; rotorConfigs++)
            {
                int[] rotor_config = { vars[rotorConfigs, 0],vars[rotorConfigs, 1], vars[rotorConfigs, 2] };
                enigma = new Enigma(rotor_config);
                for (int test3 = 0; test3 < 26; test3++)
                {
                    for (int test2 = 0; test2 < 26; test2++)
                    {
                        for (int test1 = 0; test1 < 26; test1++)
                        {
                            int[] positions = { test1, test2, test3 };
                            enigma.SetPositons(positions);
                            foreach (char sym in format)
                            {
                                decSym = enigma.Encrypt(sym);
                                histogram[decSym - 65]++;
                            }
                            temp = new Key(rotor_config, positions, histogram);
                            if (temp.ioc < min.ioc) { min = temp; }
                            histogram = new int[26];
                        }
                    }
                }
            }
            enigma = new Enigma(min);
            foreach (var item in format)
            {
                min.text += enigma.Encrypt(item);
            }
            richTextBox4.Text = $"BEST - {min.ioc}:\n{min}";
            button11.Enabled = true;
        }


        private void button11_Click(object sender, EventArgs e)
        {
            Enigma enigma;
            quad = new Dictionary<string, decimal>();
            string[] fileLines = System.IO.File.ReadLines("quadgrams-short (2).txt").ToArray();
            foreach (string line in fileLines)
            {
                string[] a = line.Split(' ');
                quad[a[0]] = decimal.Parse(a[1]);
            }
            min.hillClimb = HillClimb(min.text);

                for (char i = 'A'; i <= 'Z'; i++)
                {
                    for (char j = (char)(i+1); j <= 'Z'; j++)
                    {
                        if ($"{i}{j}".Equals("CD"))
                        {
                            Console.WriteLine("asd");
                        }
                        string start = min.text;
                        enigma = new Enigma(min);
                        string text = "";
                        if (enigma.plugboard.AddPlug($"{i}-{j}"))
                        {
                            foreach (var item in format)
                            {
                                text += enigma.Encrypt(item);
                            }
                            hillClimb = HillClimb(text);
                            if (hillClimb > min.hillClimb)
                            {
                                min.hillClimb = hillClimb;
                                min.plugboard.Add($"{i}-{j}");
                                min.text = text;
                            }
                            enigma.plugboard.RemoveLastPlug();
                        }
                    }
                }
            richTextBox4.Text = min.ToString();
            button11.Enabled = false;
        }

        public decimal HillClimb(string text)
        {
            decimal Climb = 0;
            for (int k = 0; k < text.Length - 4; k++)
            {
                string four = text.Substring(k, 4);
                if (quad.ContainsKey(four)) Climb += quad[four];
                else Climb += -6;
            }
            return Climb;
        }
    }
}
