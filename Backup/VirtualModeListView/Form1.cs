using System;
using System.Reflection;
using System.Windows.Forms;
using System.Globalization;

namespace VirtualModeListView
{
    public partial class Form1 : Form
    {
        private const int maxLines = 100000000;
        private NumberText nt = new NumberText();

        public Form1()
        {
            InitializeComponent();            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.VirtualMode = true;
            listView1.VirtualListSize = maxLines;
            toolStripStatusLabel2.Text = maxLines.ToString();

            PropertyInfo aProp = typeof(ListView).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            aProp.SetValue(listView1, true, null);
        }

        private void listView1_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.Text = nt.MakeText(e.ItemIndex);
            ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem();
            NumberFormatInfo nfi = new CultureInfo("de-DE").NumberFormat;
            nfi.NumberDecimalDigits = 0;
            lvsi.Text = e.ItemIndex.ToString("n", nfi);
            lvi.SubItems.Add(lvsi);
            e.Item = lvi;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {           
            AboutBox1 ab1 = new AboutBox1();
            ab1.ShowDialog(this);
        }
    }
}