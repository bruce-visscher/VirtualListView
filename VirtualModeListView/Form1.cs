using System;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;

namespace VirtualModeListView
{
    public partial class Form1 : Form
    {
        private static int maxLines = 10;
        private static int count = 0;

        System.Collections.Generic.List<ListViewItem> listOfAvailLVI = new List<ListViewItem>();
        ListViewItem newListViewItem = null;
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
                newListViewItem = new ListViewItem();
                newListViewItem.Name = "newListViewItem_Name" + l;
                newListViewItem.Text = "newListViewItem_Text" + l;
                newListViewItem.Tag = "newListViewItem_Tag" + l;
                newListViewItem.SubItems.Add("SubItems_Add" + l);

                newListViewItem.SubItems[1].Text = "SubItems_text" + l;

                listOfAvailLVI.Add(newListViewItem);
            }
            Console.WriteLine("");
        }

        private void listView1_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            ListViewItem it = new ListViewItem(listOfAvailLVI[e.ItemIndex].Text);

            int index = e.ItemIndex;
            count = listOfAvailLVI[e.ItemIndex].SubItems.Count;

            String subItemTextIndex;
            if (count == 1) // No subitem
            {
                subItemTextIndex = listOfAvailLVI[e.ItemIndex].SubItems[0].Text;
                it.SubItems.Add("");
            }
            else // count = 2   
            /*
              1 subitem. Count 2 means 
              1st item(0 index) is listOfAvailLVI[e.ItemIndex].Text
              and
              2nd Item(1st index) is listOfAvailLVI[e.ItemIndex].SubItems[1].Text
            */
            {
                subItemTextIndex = listOfAvailLVI[e.ItemIndex].SubItems[1].Text;
                if (!String.IsNullOrEmpty(subItemTextIndex))
                {
                    it.SubItems.Add(subItemTextIndex);
                }
            }

            e.Item = it;
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
                listOfAvailLVI.RemoveAt(indexOfElementToRemove);
                maxLines = maxLines - 1;
            }
            listView.VirtualListSize = maxLines;
        }

        // Add Item
        private void AddItems_Click(object sender, EventArgs e)
        {
            Console.WriteLine("maxLines: " + maxLines);
            int count = maxLines;
            for (int l = count; l < (count + 10); l++)
            {
                newListViewItem = new ListViewItem("OrgName " + l + "FZZ");
                newListViewItem.Name = "OrgKey" + l;
                listOfAvailLVI.Add(newListViewItem);
                maxLines = maxLines + 1;
            }

            foreach (ListViewItem each in listOfAvailLVI)
            {
                String eachString = each.Text;
            }

            newListViewItem = new ListViewItem();
            newListViewItem.Name = "newListViewItem_Name";
            newListViewItem.Text = "newListViewItem_Text";
            newListViewItem.SubItems.Add("123");
            newListViewItem.SubItems[1].Text = "123";

            listOfAvailLVI.Add(newListViewItem);
            maxLines = maxLines + 1;

            listOfAvailLVI.Sort(delegate (ListViewItem x, ListViewItem y)
            {
                return (x.Text).CompareTo(y.Text);
            });

            listView.VirtualListSize = maxLines;
        }

        private void SelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listView.VirtualListSize; i++)
            {
                listView.Items[i].Selected = true;
            }

            // Get element text of selected Index
            ListView.SelectedIndexCollection col = listView.SelectedIndices;
            foreach (var eachItemInCollection in col)
            {
                Console.WriteLine(listView.Items[Convert.ToInt32(eachItemInCollection)].Text);
            }
        }
    }
}