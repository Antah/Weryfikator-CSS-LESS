using System;
using System.Windows.Forms;

namespace Weryfikator
{
    static class Program
    {
        public static VerificatorForm form;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(form = new VerificatorForm());
        }
    }
}
