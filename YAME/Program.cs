using System;
using System.Windows.Forms;

namespace YAME
{
    /// <summary>
    /// Class with program entry point.
    /// </summary>

    internal sealed class Program
    {
        /// <summary>
        /// Program entry point.
        /// </summary>


        [STAThread]

        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (isFirstCall())
                Application.Run(new InitForm());
            else
                Application.Run(new MainForm());
        }

        private static bool isFirstCall()
        {

        Constantes yamecst = new Constantes();

        INI.Ini AppIniFile = new INI.Ini(yamecst.Ininame, true);
            string modpath;

            if ((modpath = AppIniFile["MODS FOLDER", "Name"]) == "")
                return true;

            return false;
        }
    }
}
