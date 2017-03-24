using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Weryfikator
{
    public partial class VerificatorForm : Form
    {
        public string text;
        public VerificatorForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.Cancel) return;
            textBox1.Text = openFileDialog1.FileName;
        }

        public void SetText(string textTmp)
        {
            richTextBox1.Text = textTmp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {
                try
                {
                    text = File.ReadAllText(textBox1.Text, Encoding.UTF8);
                    richTextBox1.Text = text;
                }
                catch
                {
                    text = "nie udało się wczytać pliku";
                    richTextBox1.Text = text;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Parser.parserStart(text);
        }
    }
}
