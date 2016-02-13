using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YAME
{
    public partial class InitForm : Form
    {
        public INI.Ini AppIniFile;
        public Constantes yamecst = new Constantes();

        public InitForm()
        {
            InitializeComponent();
        }

        private void InitForm_Load(object sender, EventArgs e)
        {

            AppIniFile = new INI.Ini(yamecst.Ininame, true);
            AppIniFile["MODS FOLDER", "Name"] = yamecst.Defaultmoddir;

            TextBox tb = new TextBox();
            tb.KeyDown += new KeyEventHandler(tb_KeyDown);

        }

        static void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //enter key is down
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            string folder = textBoxFolderName.Text;
            
            if (folder != String.Empty)
            {
                if(folder != yamecst.Defaultmoddir)
                    AppIniFile["MODS FOLDER", "Name"] = folder;

                if (isFolderReady(folder))
                {
                    this.Hide();
                    var mainform = new MainForm();
                    mainform.Closed += (s, args) => this.Close();
                    mainform.Show();
                } else
                    this.Close();
            }
            
        }

        private bool isFolderReady(string folder)
        {
            string backupdir = folder + "\\" + yamecst.Backupdir;
            string modlogsdir = folder + "\\" + yamecst.Modlogsdir;

            System.IO.Directory.CreateDirectory(folder);
            if (!System.IO.Directory.Exists(folder))
                return false;

            System.IO.DirectoryInfo dibackupdir = System.IO.Directory.CreateDirectory(backupdir);
            dibackupdir.Attributes = System.IO.FileAttributes.Directory | System.IO.FileAttributes.Hidden;
            if (!System.IO.Directory.Exists(backupdir))
                return false;

            System.IO.DirectoryInfo dimodlogsdir = System.IO.Directory.CreateDirectory(modlogsdir);
            dimodlogsdir.Attributes = System.IO.FileAttributes.Directory | System.IO.FileAttributes.Hidden;
            if (!System.IO.Directory.Exists(modlogsdir))
                return false;

            return true;

        }

        private void textBoxFolderName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonOK.PerformClick();
                // these last two lines will stop the beep sound
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }
    }
}
