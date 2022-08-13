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

        int r1, r2, r3;
        int[,] Rotor1 = { { 0, 15 }, { 1, 4 }, { 2, 25 }, { 3, 20 }, { 4, 14 }, { 5, 7 }, { 6, 23 }, { 7, 18 }, { 8, 2 }, { 9, 21 }, { 10, 5 }, { 11, 12 }, { 12, 19 }, { 13, 1 }, { 14, 6 }, { 15, 11 }, { 16, 17 }, { 17, 8 }, { 18, 13 }, { 19, 16 }, { 20, 9 }, { 21, 22 }, { 22, 0 }, { 23, 24 }, { 24, 3 }, { 25, 10 } },
        Rotor2 = { { 0, 25 }, { 1, 14 }, { 2, 20 }, { 3, 4 }, { 4, 18 }, { 5, 24 }, { 6, 3 }, { 7, 10 }, { 8, 5 }, { 9, 22 }, { 10, 15 }, { 11, 2 }, { 12, 8 }, { 13, 16 }, { 14, 23 }, { 15, 7 }, { 16, 12 }, { 17, 21 }, { 18, 1 }, { 19, 11 }, { 20, 6 }, { 21, 13 }, { 22, 9 }, { 23, 17 }, { 24, 0 }, { 25, 19 } },
        Rotor3 = { { 0, 4 }, { 1, 7 }, { 2, 17 }, { 3, 21 }, { 4, 23 }, { 5, 6 }, { 6, 0 }, { 7, 14 }, { 8, 1 }, { 9, 16 }, { 10, 20 }, { 11, 18 }, { 12, 8 }, { 13, 12 }, { 14, 25 }, { 15, 5 }, { 16, 11 }, { 17, 24 }, { 18, 13 }, { 19, 22 }, { 20, 10 }, { 21, 19 }, { 22, 15 }, { 23, 3 }, { 24, 9 }, { 25, 2 } },
        Rotor4 = { { 0, 8 }, { 1, 12 }, { 2, 4 }, { 3, 19 }, { 4, 2 }, { 5, 6 }, { 6, 5 }, { 7, 17 }, { 8, 0 }, { 9, 24 }, { 10, 18 }, { 11, 16 }, { 12, 1 }, { 13, 25 }, { 14, 23 }, { 15, 22 }, { 16, 11 }, { 17, 7 }, { 18, 10 }, { 19, 3 }, { 20, 21 }, { 21, 20 }, { 22, 15 }, { 23, 14 }, { 24, 9 }, { 25, 13 } },
        Rotor5 = { { 0, 16 }, { 1, 22 }, { 2, 4 }, { 3, 17 }, { 4, 19 }, { 5, 25 }, { 6, 20 }, { 7, 8 }, { 8, 14 }, { 9, 0 }, { 10, 18 }, { 11, 3 }, { 12, 5 }, { 13, 6 }, { 14, 7 }, { 15, 9 }, { 16, 10 }, { 17, 15 }, { 18, 24 }, { 19, 23 }, { 20, 2 }, { 21, 21 }, { 22, 1 }, { 23, 13 }, { 24, 12 }, { 25, 11 } },
            R1, R2, R3;
        int[] Reflector = { 24, 17, 20, 7, 16, 18, 11, 3, 15, 23, 13, 6, 14, 10, 12, 8, 4, 1, 5, 25, 2, 22, 21, 9, 0, 19 }, histogram = new int[26];
        List<Plug> plugs = new List<Plug>();

        private void Form1_Load(object sender, EventArgs e)
        {
            r1 = int.Parse(label1.Text) - 1;
            r2 = int.Parse(label2.Text) - 1;
            r3 = int.Parse(label3.Text) - 1;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            r1++;
            if (r1 == 26) r1 = 0;
            label1.Text = (r1 + 1).ToString();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            r1--;
            if (r1 == -1) r1 = 25;
            label1.Text = (r1 + 1).ToString();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            r2++;
            if (r2 == 26) r2 = 0;
            label2.Text = (r2 + 1).ToString();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            r2--;
            if (r2 == -1) r2 = 25;
            label2.Text = (r2 + 1).ToString();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            r3++;
            if (r3 == 26) r3 = 0;
            label3.Text = (r3 + 1).ToString();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            r3--;
            if (r3 == -1) r3 = 25;
            label3.Text = (r3 + 1).ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string s = textBox2.Text;
            char a = s[0], b = s[2];
            foreach (Plug plug in plugs)
            {
                if (plug.Contains(a, b)) throw new ArgumentException("Невалидни връзки!");
            }
            plugs.Add(new Plug(a, b)); listBox2.Items.Add(plugs[plugs.Count-1].ToString());
        }
        private void button9_Click(object sender, EventArgs e)
        {
            plugs = new List<Plug>();
            listBox2.Items.Clear();
        }

        public void Rotate()
        {
            r1++;
            if (r1 > 25)
            {
                r1 -= 26;
                r2++;
                if (r2 > 25) { r2 -=26; r3++; if (r3 > 25) r3-=26; }
            }
            label1.Text = (r1 + 1).ToString();
            label2.Text = (r2 + 1).ToString();
            label3.Text = (r3 + 1).ToString();
        }
        public int Increase(int input, int inc)
        {
            return ((input + inc) % 26);
        }
        public int Decrease(int input, int inc)
        {
            input-= inc;
            if (input < 0) input += 26;
            return input;
        }
        public int Forward(int input)
        {
            input = Increase(input, r1);
            input = R1[input, 1];
            input = Decrease(input, r1);
            input = Increase(input, r2);
            input = R2[input, 1];
            input = Decrease(input, r2);
            input = Increase(input, r3);
            input = R3[input, 1];
            input = Decrease(input, r3);
            input = Reflector[input];
            return input;
        }
        public int Back(int input)
        {
            input = Increase(input, r3);
            for (int i = 0; i < 26; i++)
            {
                if (R3[i, 1] == input){ input = R3[i, 0]; break; }
            }
            input = Decrease(input, r3);
            input = Increase(input, r2);
            for (int i = 0; i < 26; i++)
            {
                if (R2[i, 1] == input){ input = R2[i, 0]; break; }
            }
            input = Decrease(input, r2);
            input = Increase(input, r1);
            for (int i = 0; i < 26; i++)
            {
                if (R1[i, 1] == input) { input = R1[i, 0]; break; }
            }
            input = Decrease(input, r1);
            return input;
        }
        public int Encrypt(int trans)
        {
            Rotate();
            trans = Forward(trans);
            trans = Back(trans);
            histogram[trans]++;
            return trans;
        }
        public int[,] Check(int a)
        {
            switch(a)
            {
                case 1:
                    return Rotor1;
                case 2:
                    return Rotor2;
                case 3:
                    return Rotor3;
                case 4:
                    return Rotor4;
                case 5:
                    return Rotor5;
                default:
                    throw new ArgumentException("Invalid value!");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox3.Text = "";
            int a = int.Parse(comboBox1.Text), b = int.Parse(comboBox2.Text), c = int.Parse(comboBox3.Text);
            if (a == b || a == c || b == c) throw new ArgumentException("Overlapping values!");
            R1 = Check(a);
            R2 = Check(b);
            R3 = Check(c);
            string normal = richTextBox2.Text, cypher = "";
            char encrypted;
            int trans;
            foreach (char symbol in normal)
            {
                char sym = symbol;
                if (symbol > 96 && symbol < 123) sym = (char)((int)sym - 32);
                else if (symbol < 65 || symbol > 90) { cypher += symbol; continue; }
                for (int i = 0; i < plugs.Count; i++)
                {
                    if (sym != plugs[i].Connect(sym)) { sym = plugs[i].Connect(sym); break; }
                }
                trans = (int)sym - 65;
                trans = Encrypt(trans);
                encrypted = (char)(trans + 65);
                for (int i = 0; i < plugs.Count; i++)
                {
                    if (encrypted != plugs[i].Connect(encrypted)) { encrypted = plugs[i].Connect(encrypted); break; }
                }
                if (symbol > 96 && symbol < 123) encrypted = (char)((int)encrypted + 32);
                cypher += encrypted;
            }
            richTextBox3.Text = cypher;
        }

        //DECRYPTION
        int test1, test2, test3;
        public void Increment()
        {
            test1++;
            if (test1 > 25)
            {
                test1 -= 26;
                test2++;
                if (test2 > 25) { test2 -= 26; test3++; if (test3 > 25) test3 -= 26; }
            }
        }
        private void button10_Click(object sender, EventArgs e)                      
        {
            string start = richTextBox1.Text, format = "", decrypted = "";
            int[] rotor_config = { 1, 2, 3 };
            Key min = new Key(rotor_config, 0, 0, 0, histogram);
            min.ioc = 1;
            test1 = 0; test2 = 0; test3 = 0;histogram = new int[26];
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
            for (int i = 0; i < 17576; i++)
            {
                r1 = test1; r2 = test2; r3 = test3;
                foreach (char sym in format)
                {
                    decrypted += (char)(Encrypt(sym - 65) + 65);
                }
                Key temp = new Key(rotor_config, test1+1, test2+1, test3+1, histogram);
                if (temp.ioc < min.ioc) { min = temp;/* richTextBox4.Text += min.ToString(); */}
                Increment();
                decrypted = "";
                histogram = new int[26];
            }
            richTextBox4.Text += $"BEST - {min.ToString()}";
        }
    }
}
