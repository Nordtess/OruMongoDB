using System;
using System.Windows.Forms;
using UI;

namespace OruMongoDB.UI
{
    internal static class Program
    {

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new MainUiForm());

        }
    }
}


