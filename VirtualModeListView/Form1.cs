using System;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;

namespace VirtualModeListView
{
    public partial class Form1 : Form
    {
        private const int maxLines = 10;
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
                NewListViewItem = new ListViewItem("OrgName " + l);
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

        private void Move_Click(object sender, EventArgs e)
        {
            // Get element text of selected Index
            ListView.SelectedIndexCollection col = listView1.SelectedIndices;

            foreach (var eachItemInCollection in col)
            {
                Console.WriteLine(listView1.Items[Convert.ToInt32(eachItemInCollection)].Text);
                //  ListViewItem lvi = listView1.GetItemAt(Convert.ToInt32(eachItemInCollection), 0);
            }

            /* {"When the ListView is in virtual mode, you cannot enumerate through the ListView items collection
            using an enumerator or call GetEnumerator. 
            Use the ListView items indexer instead and access an item by index value."}
            
            foreach (ListViewItem lvi in listView1.Items)
            {
                //    Console.WriteLine(lvi.Text);
            }
            */
        }
    }
}