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
    public partial class DependanciesForm : Form
    {
        public DependanciesForm()
        {
            InitializeComponent();
        }

        static DependanciesForm MsgBox;
        static DialogResult result = new DialogResult();

        public static DialogResult Show(string Text, string Caption)
        {
            MsgBox = new DependanciesForm();
            MsgBox.Text = MsgBox.Text + " " + Caption + " !";
            MsgBox.TextBox.Text = Text;
            MsgBox.ShowDialog();
            MsgBox.buttonNo.Focus();
            return result;
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            result = DialogResult.Yes;
            MsgBox.Close();
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            result = DialogResult.No;
            MsgBox.Close();
        }
    }
}
