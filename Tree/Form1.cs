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

        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string text = e.Node.Text;
            treeView2.Nodes.Clear();
            string path = driver + text + "\\";
            if (current != null)
            {
                path = current + text+"\\";
            }
            if (Directory.Exists(path))
            {
                var directories = Directory.GetDirectories(path);
                foreach (string directorie in directories)
                {
                    var dir = new DirectoryInfo(directorie);
                    var dirName = dir.Name;
                    treeView2.Nodes.Add(new TreeNode(dirName));
                }
                current = path;
                textBox1.Text = path;
            }
            
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string text = e.Node.Text;
            if (text != "Ổ Đĩa: ")
            {
                treeView2.Nodes.Clear();
                driver = null;
                current = null;
                driver = text + "\\";
                var directories = Directory.GetDirectories(text + "\\");
                foreach (string directorie in directories)
                {
                    treeView2.Nodes.Add(new TreeNode(directorie.Substring(3)));
                }
                textBox1.Text = (text + "\\");
            }
        }
    }
}
