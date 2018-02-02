using System;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;

namespace VirtualModeListView
{
    public partial class Form1 : Form
    {
        private int maxLines = 10;
        
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
            
            for (int l = 0; l < maxLines; l++)
            {
                NewListViewItem = new ListViewItem("OrgName " + l + "NG");
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
            }
        }

        // Remove Item
        /* A virtual list knows nothing about your list of items. 
         * It doesn't keep track of them, not even a small set. 
         * It only ever asks "what do you want to show at the n'th row?"
         * If your master list changes, all you need to do is redraw the list. 
         * Invalidate() will do that for you. 
         * The listview will then ask you again what it should show at every row visible in the control.
        */
        private void remove_Click(object sender, EventArgs e)
        {
            // Get element text of selected Index
            ListView.SelectedIndexCollection col = listView1.SelectedIndices;
            
            List<int> listOfIndexestoRemove = new List<int>();
            foreach (var indexOfElementToRemove in col)
            {
                int index = Convert.ToInt32(indexOfElementToRemove);
                listOfLVI.RemoveAt(index);
                Console.WriteLine();
                maxLines = maxLines - 1;
            }
            listView1.VirtualListSize = maxLines;
        }

        // Add Item

        // Sort List
    }
}