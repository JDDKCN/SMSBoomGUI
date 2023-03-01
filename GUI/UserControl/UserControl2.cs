using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class UserControl2 : UserControl
    {
        public UserControl2()
        {
            InitializeComponent();
            groupBox1.Text = Ver.Version.ToString() + " Release";
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(50, 0, 0, 0);//ARGB，第一个为调节不透明度
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(Ver.biliURL);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(Ver.githubURL);
        }
    }
}
