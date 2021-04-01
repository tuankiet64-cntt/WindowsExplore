using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tree
{
    public partial class Form2 : Form
    {
        public Form2(string path)
        {
            InitializeComponent();
          
            pictureBox1.Image = Image.FromFile(path);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
