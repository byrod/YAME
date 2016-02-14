using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YAME.SMARTDIALOG
{
    public partial class SmartDialog : Form
    {
        public SmartDialog()
        {
            InitializeComponent();
        }
        static SmartDialog MsgBox;
        static DialogResult result = new DialogResult();

        public static DialogResult Show(string message, string label2, string label1, string Caption, string BtnOk, string BtnCancel = "")
        {
            MsgBox = new SmartDialog();
            MsgBox.rText.Text = message;
            MsgBox.rText.SelectAll();
            MsgBox.rText.SelectionAlignment = HorizontalAlignment.Center;
            MsgBox.rText.Select(0, 0);

            MsgBox.label1.Text = label1;
            MsgBox.label2.Text = label2;
            MsgBox.Text = Caption;

            MsgBox.button1.Text = BtnOk;
            if (BtnCancel == "")
                MsgBox.button2.Hide();
            else
                MsgBox.button2.Text = BtnCancel;

            MsgBox.ShowDialog();
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            result = DialogResult.Yes;
            MsgBox.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            result = DialogResult.No;
            MsgBox.Close();
        }

    }
}
