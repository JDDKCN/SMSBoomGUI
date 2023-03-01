using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GUI
{
    public partial class download : Form
    {
        public download()
        {
            InitializeComponent();
        }

        #region 窗体边框阴影效果

        const int CS_DropSHADOW = 0x20000;
        const int GCL_STYLE = (-26);
        //声明Win32 API
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);

        public void LoginForm()
        {
            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW); //API函数加载，实现窗体边框阴影效果
        }

        #endregion

        #region 绘制圆角窗体
        private void SetWindowRegion()
        {
            GraphicsPath path = new GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            path = getRoundRectPath(rect, 50);
            this.Region = new Region(path);
        }
        private GraphicsPath getRoundRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            Rectangle arcRect = new Rectangle(rect.Location, new Size(radius, radius));
            //左上角
            path.AddArc(arcRect, 180, 90);//从180度开始，顺时针,滑过90度
            //右上角
            arcRect.X = rect.Right - radius;
            path.AddArc(arcRect, 270, 90); //
            //右下角
            arcRect.Y = rect.Bottom - radius;
            path.AddArc(arcRect, 0, 90);
            //左下角
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();//闭合曲线
            return path;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            SetWindowRegion();
        }
        #endregion

        #region 鼠标事件

        //引用
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        //常量
        public const int WM_SYSCOMMAND = 0x0112;

        //窗体移动
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //拖动窗体
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
        }


        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            //拖动窗体
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox1.BackColor = Color.Red;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Transparent;
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox3.BackColor = Color.SkyBlue;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Transparent;
        }


        #endregion

        #region 绘制程序Panel
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            panel1.BackColor = Color.FromArgb(150, 131, 175, 200);//ARGB，第一个为调节不透明度

            ControlPaint.DrawBorder(e.Graphics,
                                        panel1.ClientRectangle,
                                        Color.White,
                                        1,
                                        ButtonBorderStyle.Solid,
                                        Color.White,
                                        1,
                                        ButtonBorderStyle.Solid,
                                        Color.White,
                                        1,
                                        ButtonBorderStyle.Solid,
                                        Color.White,
                                        1,
                                        ButtonBorderStyle.Solid);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            panel2.BackColor = Color.FromArgb(0, 0, 0, 0);//ARGB，第一个为调节不透明度
        }


        private void Home_Paint(object sender, PaintEventArgs e)
        {

        }

        #endregion

        #region 窗体初始化

        private void Form1_Load(object sender, EventArgs e)
        {
            LoginForm();//加载程序外阴影
            this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);//限制最大化窗体大小
            this.MinimumSize = new Size(this.Width, this.Height);//窗体改变大小时最小限定在初始化大小
            string ImgFile;
            try
            {
                FilesINI ConfigINI = new FilesINI();
                ImgFile = ConfigINI.INIRead("Image", "BgFile", ".\\skin\\info.ini");
                this.BackgroundImage = Image.FromFile(ImgFile);
            }
            catch
            {
            }
            label2.Text = Ver.Version.ToString() + " " + Ver.APPName.ToString();
            UsC();
            Download_Control();
        }

        #endregion

        #region 用户交互
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var ret = GUI.Msg.MsgShow("是否关闭下载页面？ \n正在下载的内容将中断。", "提示", true);
            if (ret)
                Close();
            else
                return;

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(Ver.biliURL.ToString());
        }

        #endregion

        #region 控件相关功能
        //创建用户控件变量
        public downloadfile d1;

        //实例化用户控件
        private void UsC()
        {
            d1 = new downloadfile();
        }

        //将控件显示到panel上
        internal void Download_Control()
        {
            d1.Show();
            panel2.Controls.Clear();
            panel2.Controls.Add(d1);
        }


        #endregion

    }
}
