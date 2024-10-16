using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    public partial class Form1 : Form
    {
        private RSA rsa = new RSA();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = openFileDialog1.FileName;
            // читаем файл в строку
            string fileText = System.IO.File.ReadAllText(filename);
            richTextBox1.Text = fileText.ToLower();
            MessageBox.Show("File is opened.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = saveFileDialog1.FileName;
            // сохраняем текст в файл
            System.IO.File.WriteAllText(filename, richTextBox2.Text);
            MessageBox.Show("File is saved.");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = openFileDialog1.FileName;
            // читаем файл в строку
            string fileText = System.IO.File.ReadAllText(filename);
            richTextBox4.Text = fileText;
            MessageBox.Show("File is opened.");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = saveFileDialog1.FileName;
            // сохраняем текст в файл
            System.IO.File.WriteAllText(filename, richTextBox3.Text);
            MessageBox.Show("File is saved.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] encoded = rsa.Encode(richTextBox1.Text);
            richTextBox2.Text = "";
            foreach (int encodedSymbol in encoded)
                richTextBox2.AppendText(encodedSymbol.ToString() + '\n');

            richTextBox2.AppendText(rsa.EncodeSign(richTextBox1.Text).ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string[] encoded = Regex.Split(richTextBox4.Text, "\n");
            int[] ints = new int[encoded.Length - 1];
            for (int i = 0; i < ints.Length; ++i)
                ints[i] = int.Parse(encoded[i]);

            string decodedText = rsa.Decode(ints);
            int encodedSign = int.Parse(encoded[encoded.Length - 1]);
            //Random random = new Random();
            //int encodedSign = random.Next(0, 32);
            if (rsa.CheckSign(decodedText, encodedSign))
                MessageBox.Show("Підпис розшифровано успішно.");
            else
            {
                MessageBox.Show("Підпис неправильний.");
                return;
            }

            richTextBox3.Text = decodedText;
        }
    }
}
