using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tree
{
    public partial class Form1 : Form
    {
        public string driver = null;
        public string current = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] drives = Directory.GetLogicalDrives();
         

            TreeNode[] array = new TreeNode[2];
            int i = 0;
            foreach (string drive in drives)
            {
                string disc = drive.Remove(drive.Length - 1);
                array[i]= new TreeNode(disc);
                i++;
            }
            TreeNode Disc = new TreeNode("Ổ Đĩa: ", array);
            treeView1.Nodes.Add(Disc);
        }

    

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            
        }

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            string text = e.Node.Text;
            if (text != "Ổ Đĩa: ")
            {
                
            }

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string text = e.Node.Text;
            if (text != "Ổ Đĩa: ")
            {
                listView1.Clear();
                Debug.WriteLine(text);
                TreeNode parent = treeView1.SelectedNode;
                string currentPath = parent.FullPath.Substring(8) + "\\";
                Debug.WriteLine(currentPath);
                var directories = Directory.GetDirectories(currentPath);
                parent.Nodes.Clear();
                foreach (string directorie in directories)
                {
                    parent.Nodes.Add(new DirectoryInfo(directorie).Name);
                  
                }
                foreach (string directorie in directories)
                {
                    listView1.Items.Add(new DirectoryInfo(directorie).Name);
                }
                textBox1.Text = (currentPath);
                this.current = null;
            }
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            
        }
        public static String[] GetFilesFrom(String searchFolder, String[] filters, bool isRecursive)
        {
            List<String> filesFound = new List<String>();
            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var filter in filters)
            {
                filesFound.AddRange(Directory.GetFiles(searchFolder, String.Format("*.{0}", filter), searchOption));
            }
            return filesFound.ToArray();
        }
        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            listView1.Clear();
            TreeNode parent = treeView1.SelectedNode;
            if (this.current == null)
            {
                this.current = e.Item.Text;
            }
            else
            {
                this.current = this.current +"\\"+ e.Item.Text;
            }
            string currentPath = parent.FullPath.Substring(8) + "\\" + this.current;
            if (Directory.Exists(currentPath))
            {
                var directories = Directory.GetDirectories(currentPath);
                var filters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp", "svg" };
                var files = GetFilesFrom(currentPath, filters, false);
                //string[] files = Directory.GetFiles(textBox1.Text);
                foreach (string directorie in directories)
                {
                    listView1.Items.Add(new DirectoryInfo(directorie).Name);
                }
                for (int x = 0; x < files.Length; x++)
                {
                    listView1.Items.Add(new FileInfo(files[x]).Name);
                }
            }
            else
            {
                MessageBox.Show("Its a file");
            }
     
           
        }
    }
}
