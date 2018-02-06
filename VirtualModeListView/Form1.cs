using System;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;

namespace VirtualModeListView
{
    public partial class Form1 : Form
    {
        private static int maxLines = 5;

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

        // Populate Items
        private void Form1_Load(object sender, EventArgs e)
        {
            listView.VirtualMode = true;
            listView.VirtualListSize = maxLines;
            toolStripStatusLabel2.Text = maxLines.ToString();

            for (int l = 0; l < maxLines; l++)
            {
                NewListViewItem = new ListViewItem("OrgName " + l + "NG");
                NewListViewItem.Name = "OrgKey" + l;
                listOfLVI.Add(NewListViewItem);
            }

            PropertyInfo aProp = typeof(ListView).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            aProp.SetValue(listView, true, null);
        }

        private void listView1_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            foreach (ListViewItem each in listOfLVI)
            {
                // Console.WriteLine(each.Text);
            }
            ListViewItem lvi = new ListViewItem();
            lvi = listOfLVI[e.ItemIndex];

            ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem();
            lvsi.Text = lvi.Text;
            lvi.SubItems.Add(lvsi);

            e.Item = lvi;
        }

        private void Move_Click(object sender, EventArgs e)
        {
            // Get element text of selected Index
            ListView.SelectedIndexCollection col = listView.SelectedIndices;
            foreach (var eachItemInCollection in col)
            {
                Console.WriteLine(listView.Items[Convert.ToInt32(eachItemInCollection)].Text);
            }
        }

        // Remove Item
        /* A virtual list knows nothing about your list of items. 
         * It doesn't keep track of them, not even a small set. 
         * It only ever asks "what do you want to show at the n'th row?"
         * If your master list changes, all you need to do is redraw the list.
         * The listview will then ask you again what it should show at every row visible in the control.
        */
        private void remove_Click(object sender, EventArgs e)
        {
            // Get element text of selected Index
            ListView.SelectedIndexCollection col = listView.SelectedIndices;
            List<int> listOfIndexestoRemove = new List<int>();
            foreach (var elementToRemove in col)
            {
                int index = Convert.ToInt32(elementToRemove);
                listOfIndexestoRemove.Add(index);
            }

            // Very imp step
            listOfIndexestoRemove.Reverse();

            foreach (int indexOfElementToRemove in listOfIndexestoRemove)
            {
                listOfLVI.RemoveAt(indexOfElementToRemove);
                maxLines = maxLines - 1;
            }
            listView.VirtualListSize = maxLines;
        }

        // Add Item
        private void AddItems_Click(object sender, EventArgs e)
        {
            Console.WriteLine("maxLines: " + maxLines);
            int count = maxLines;
            for (int l = count; l < (count + 5); l++)
            {
                NewListViewItem = new ListViewItem("OrgName " + l + "FZZ");
                NewListViewItem.Name = "OrgKey" + l;
                listOfLVI.Add(NewListViewItem);
                maxLines = maxLines + 1;
            }

            foreach (ListViewItem each in listOfLVI)
            {
                String eachString = each.Text;
            }

            NewListViewItem = new ListViewItem("OrgName " + 1 + "FZZ");
            NewListViewItem.Name = "OrgKey" + 1;
            listOfLVI.Add(NewListViewItem);
            maxLines = maxLines + 1;

            NewListViewItem = new ListViewItem("OrgName " + 2 + "FZZ");
            NewListViewItem.Name = "OrgKey" + 2;
            listOfLVI.Add(NewListViewItem);
            maxLines = maxLines + 1;

            listOfLVI.Sort(delegate (ListViewItem x, ListViewItem y)
                {
                    return (x.Text).CompareTo(y.Text);
                });

            listView.VirtualListSize = maxLines;
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}