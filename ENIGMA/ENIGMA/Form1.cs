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

        //MAIN STUCTURE AND ENCRYPTION
        int r1 = 0, r2 = 0, r3 = 0;
        int[] histogram = new int[26];
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

        private void button8_Click(object sender, EventArgs e)
        {
            listBox2.Items.Add(textBox2.Text);
            textBox2.Clear();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
        }

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
        private void button10_Click(object sender, EventArgs e)
        {
            string start = richTextBox1.Text, format = "", decrypted = "";
            int[] rotor_config = { 1, 2, 3 };
            char decSym;
            Key min = new Key();
            histogram = new int[26];
            /*Dictionary<string, decimal> quad = new Dictionary<string, decimal>();
            foreach (string line in System.IO.File.ReadLines(@"D:\ENIGMA/quadgrams-short (2).txt"))
            {
                string[] a = line.Split(' ');
                quad[a[0]] = decimal.Parse(a[1]);
            }*/

            foreach (char sym in start)
            {
                if (sym > 96 && sym < 123) format += (char)(sym - 32);
                else if (sym > 64 && sym < 91) format += sym;
            }
            for (int test3 = 0; test3 < 26; test3++)
            {
                for (int test2 = 0; test2 < 26; test2++)
                {
                    for (int test1 = 0; test1 < 26; test1++)
                    {
                        int[] positions = { test1, test2, test3 };
                        Enigma enigma = new Enigma(rotor_config, positions);
                        foreach (char sym in format)
                        {
                            decSym = enigma.Encrypt(sym);
                            
                        }
                        //Key temp = new Key(rotor_config, positions,histogram);
                        //if (temp.ioc < min.ioc) { min = temp; richTextBox4.Text += min.ToString(); }
                        //histogram = new int[26];
                        richTextBox4.Text = string.Join
                    }
                }
            }
            richTextBox4.Text += $"BEST - {min.ToString()}";
        }
    }
}
