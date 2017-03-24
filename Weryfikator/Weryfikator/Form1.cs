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

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.Cancel) return;
            textBoxSearch.Text = openFileDialog1.FileName;
        }

        public void SetText(string textTmp)
        {
            richTextBoxCode.Text = textTmp;
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            if (textBoxSearch.Text != null)
            {
                try
                {
                    text = File.ReadAllText(textBoxSearch.Text, Encoding.UTF8);
                    richTextBoxCode.Text = text;
                }
                catch
                {
                    textBoxMessage.Text = "Failed to load file.";
                    richTextBoxCode.Text = "";
                }
            }
        }

        private void buttonVerify_Click(object sender, EventArgs e)
        {
            Parser.parserStart(text);
        }

        internal void SetErrorMessage(string message)
        {
            textBoxMessage.Text = message;
        }

        private void VerificatorForm_Load(object sender, EventArgs e)
        {

        }

        private void richTextBoxCode_TextChanged(object sender, EventArgs e)
        {
            text = richTextBoxCode.Text;
        }
    }
}
