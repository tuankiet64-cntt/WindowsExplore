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
            TreeNode Disc = new TreeNode("My Computer", array);
            treeView1.Nodes.Add(Disc);
        }

        //private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        //{
        //    string text = e.Node.Text;
        //    treeView2.Nodes.Clear();
        //    string path = driver + text + "\\";
        //    if (current != null)
        //    {
        //        path = current + text+"\\";
        //    }
        //    if (Directory.Exists(path))
        //    {
        //        var directories = Directory.GetDirectories(path);
        //        foreach (string directorie in directories)
        //        {
        //            var dir = new DirectoryInfo(directorie);
        //            var dirName = dir.Name;
        //            treeView2.Nodes.Add(new TreeNode(dirName));
        //        }
        //        current = path;
        //        textBox1.Text = path;
        //    }
            
        //}

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
           
            string fullpath = e.Node.Text;
            if(fullpath== "My Computer") return;
            TreeNode parent = e.Node.Parent;
            string[] arr = new string[4];
            ListViewItem itm;
            while (parent != null )
            {
                if (parent.Text == "My Computer")
                {
                    fullpath =  fullpath;
                    break;
                }
                else
                    fullpath = parent.Text + "\\" + fullpath;

                parent = parent.Parent;
            }
         
                driver = null;
                current = null;
                driver = fullpath + "\\";
                var directories = Directory.GetDirectories(fullpath + "\\");
                foreach (string var in directories)
                {
                    string[] fs = var.Split('\\');
                    // Thêm node vào treeview
                    TreeNode node = new TreeNode(fs[fs.Length - 1]);
                    node.Text = fs[fs.Length - 1];
                    node.ImageIndex = 1;
                    node.SelectedImageIndex = 2;
                    e.Node.Nodes.Add(node);
                }

                listView1.Items.Clear();
            var folder = Directory.GetDirectories(fullpath + "\\");
            foreach (string var in folder)
                 {
                Debug.WriteLine(var);
                // thêm vào view
                ListViewItem item = new ListViewItem(Path.GetFileName(var), 0);
                item.SubItems.Add(File.GetCreationTime(var).ToLongDateString());
                string[] type = var.Split('.');
                item.SubItems.Add(type[type.Length - 1] + " file type");
                listView1.Items.Add(item);
                }

            textBox1.Text = (fullpath + "\\");
            
        }
    }
}
