using System;
using System.Drawing;
using System.Windows.Forms;

namespace YAME
{
	public partial class AboutForm : Form
	{
		public AboutForm()
		{
			InitializeComponent();
		}
		
		static AboutForm MsgBox;
        static DialogResult result = new DialogResult();
        
        public static DialogResult Show(string version)
        {
            MsgBox = new AboutForm();

            MsgBox.label1.Text = version;

            MsgBox.ShowDialog();
            MsgBox.button1.Focus();
            return result;
        }
		void button1_Click(object sender, EventArgs e)
		{
			result = DialogResult.OK;
			MsgBox.Close();
		}
        
	}
}
