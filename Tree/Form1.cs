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
            listView1.View = View.Details;
            listView1.Columns.Add("Name", -2);
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
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string text = e.Node.Text;
            if (text != "Ổ Đĩa: ")
            {
                listView1.Items.Clear();

                TreeNode parent = treeView1.SelectedNode;
                string currentPath = parent.FullPath.Substring(8) + "\\";
                var directories = Directory.GetDirectories(currentPath);
                parent.Nodes.Clear();
                foreach (string directorie in directories)
                {
                    parent.Nodes.Add(new DirectoryInfo(directorie).Name);
                  
                }
                for (int y = 0; y < directories.Length; y++)
                {
                   
                    listView1.Items.Add(new DirectoryInfo(directories[y]).Name.ToString());
                }
                var filters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp", "svg" };
                var files = GetFilesFrom(currentPath, filters, false);
                for (int x = 0; x < files.Length; x++)
                {
                    listView1.Items.Add(new FileInfo(files[x]).Name.ToString());
                }
                textBox1.Text = (currentPath);
                this.current = null;
            }
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
   

        private void listView1_Click(object sender, EventArgs e)
        {
            TreeNode parent = treeView1.SelectedNode;
            if (this.current == null)
            {
                this.current = listView1.SelectedItems[0].Text;
            }
            else
            {
                this.current = this.current + "\\" + listView1.SelectedItems[0].Text;
            }
            string currentPath = parent.FullPath.Substring(8) + "\\" + this.current;
            if (Directory.Exists(currentPath) && File.Exists(currentPath) == false)
            {
                listView1.Items.Clear();
                var directories = Directory.GetDirectories(currentPath);
                var filters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp", "svg" };
                var files = GetFilesFrom(currentPath, filters, false);
                //string[] files = Directory.GetFiles(textBox1.Text);
                for (int y = 0; y < directories.Length; y++)
                {
                    listView1.Items.Add(new DirectoryInfo(directories[y]).Name);
                }
                for (int x = 0; x < files.Length; x++)
                {
                    listView1.Items.Add(new FileInfo(files[x]).Name);
                }
               
            }
            else
            {
                String filename = listView1.SelectedItems[0].Text;
                Form2 child = new Form2(currentPath);
                child.ShowDialog();
                this.current = this.current.Replace(filename, "");
             
            }
        }
    }
}
