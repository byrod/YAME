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
    public partial class SnapshotForm : Form
    {
        public SnapshotForm()
        {
            InitializeComponent();
        }

        static SnapshotForm MsgBox;
        static DialogResult result = new DialogResult();
        static string StartPath;
        static SortedDictionary<string, string> ListFiles = new SortedDictionary<string, string>();
        static bool modulo = false;

        public static DialogResult Show(string startPath, string fileName, SortedDictionary<string, string> listFiles, int counterSame, int counterDifferent, int counterNew, int counterRemoved)
        {
            MsgBox = new SnapshotForm();

            DateTime timeFile = System.IO.File.GetLastWriteTime(fileName);
            DateTime now = DateTime.Now;

            MsgBox.labelTitre1.Text = "Compare snapshots : SAVED " + timeFile + " / NOW " + now + ".";
            MsgBox.labelTitre2.Text = startPath;
            MsgBox.labelState.Text = "SAME:" + counterSame + "   DIFFERENT:" + counterDifferent + "   NEW:" + counterNew + "   REMOVED:" + counterRemoved;
            StartPath = startPath;
            ListFiles = listFiles;

            foreach (KeyValuePair<string, string> file in listFiles)
            {
                string[] row = { file.Key, file.Value };
                MsgBox.listView.Items.Add(new ListViewItem(row));
            }

            MsgBox.listView.ColumnClick += new ColumnClickEventHandler(ColumnClick);

            MsgBox.ShowDialog();
            MsgBox.buttonClose.Focus();
            return result;
        }

        // ColumnClick event handler.
        private static void ColumnClick(object o, ColumnClickEventArgs e)
        {
            modulo = !modulo;
            MsgBox.listView.ListViewItemSorter = new ListViewItemComparer(e.Column);
        }

        class ListViewItemComparer : System.Collections.IComparer
        {
            private int col;
            public ListViewItemComparer()
            {
                col = 0;
            }
            public ListViewItemComparer(int column)
            {
                col = column;
            }
            public int Compare(object x, object y)
            {
                
                if (modulo)
                    return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
                else
                    return -1 * String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);

                
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

            saveFileDialog1.InitialDirectory = StartPath;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.DefaultExt = "*";
            saveFileDialog1.Filter = "All files (*.*)|*.*|YAME doc file (*.yame)|*.yame|TXT file (*.txt)|*.txt";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string contentSnapshotFile = MsgBox.labelTitre1.Text + "\r\n"
                                            + MsgBox.labelState.Text + "\r\n"
                                            + StartPath + "\r\n" + "\r\n";
                foreach (KeyValuePair<string, string> file in ListFiles)
                {
                    contentSnapshotFile += file.Key + "\t" + file.Value + "\r\n";
                }

                string snapshotFile = saveFileDialog1.FileName;
                System.IO.File.WriteAllText(snapshotFile, contentSnapshotFile);
            }

            result = DialogResult.Yes;
        }

    private void buttonClose_Click(object sender, EventArgs e)
        {
            result = DialogResult.No;
            MsgBox.Close();
        }
    }
}
