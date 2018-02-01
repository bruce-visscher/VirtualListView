using System;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;

namespace VirtualModeListView
{
    public partial class Form1 : Form
    {
        private const int maxLines = 1000000;
        private NumberText nt = new NumberText();

        System.Collections.Generic.List<ListViewItem> listOfLVI = new List<ListViewItem>();
        ListViewItem NewListViewItem = null;
        System.Collections.Generic.List<String> listNew = new System.Collections.Generic.List<String>();

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
            
            for (int l = 0; l <= maxLines; l++)
            {
                NewListViewItem = new ListViewItem("OrgName " + l );
                NewListViewItem.Name = "OrgKey" + l;
                listOfLVI.Add(NewListViewItem);
            }

            PropertyInfo aProp = typeof(ListView).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            aProp.SetValue(listView1, true, null);
        }

        private void listView1_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            ListViewItem lvi = new ListViewItem();
            lvi = listOfLVI[e.ItemIndex];
            
            ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = lvi.Text;
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