using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SMSBoomGUI
{
    public partial class Home : Form
    {
        public Home()
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

        #region Msg窗口通讯

        private static object Obj = new object();
        public static bool MsgShow(string msg = "Msg", string title = "Msg_Title", bool bcancel = false)
        {
            Task<bool> mtask = new Task<bool>
                (
                () =>
                {
                    lock (Obj)
                    {
                        ErrorForm frWarning = new ErrorForm();//错误窗体
                        frWarning.TopMost = true;

                        try
                        {
                            frWarning.BackgroundImage = Image.FromFile(".\\skin\\bg.png");
                        }
                        catch
                        {
                            frWarning.BackColor = Color.White;
                        }

                        frWarning.lb_msg.Text = msg;
                        frWarning.lb_title.Text = title;

                        if (bcancel)
                        {
                            frWarning.btn_cancle.Visible = true;
                            frWarning.btn_cancle.Enabled = true;
                        }

                        frWarning.ShowDialog();

                        if (frWarning.DialogResult == DialogResult.OK)
                        {
                            frWarning.Dispose();
                            return true;
                        }
                        else
                        {
                            frWarning.Dispose();
                            return false;
                        }
                    }
                });
            mtask.Start();
            mtask.Wait();
            return mtask.Result;
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
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        bool beginMove = false;//初始化鼠标位置
        int currentXPosition;
        int currentYPosition;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                beginMove = true;
                currentXPosition = MousePosition.X;//鼠标的x坐标为当前窗体左上角x坐标
                currentYPosition = MousePosition.Y;//鼠标的y坐标为当前窗体左上角y坐标
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (beginMove)
            {
                this.Left += MousePosition.X - currentXPosition;//根据鼠标x坐标确定窗体的左边坐标x
                this.Top += MousePosition.Y - currentYPosition;//根据鼠标的y坐标窗体的顶部，即Y坐标
                currentXPosition = MousePosition.X;
                currentYPosition = MousePosition.Y;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                currentXPosition = 0; //设置初始状态
                currentYPosition = 0;
                beginMove = false;
            }
        }


        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                beginMove = true;
                currentXPosition = MousePosition.X;//鼠标的x坐标为当前窗体左上角x坐标
                currentYPosition = MousePosition.Y;//鼠标的y坐标为当前窗体左上角y坐标
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (beginMove)
            {
                this.Left += MousePosition.X - currentXPosition;//根据鼠标x坐标确定窗体的左边坐标x
                this.Top += MousePosition.Y - currentYPosition;//根据鼠标的y坐标窗体的顶部，即Y坐标
                currentXPosition = MousePosition.X;
                currentYPosition = MousePosition.Y;
            }

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                currentXPosition = 0; //设置初始状态
                currentYPosition = 0;
                beginMove = false;
            }

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox1.BackColor = Color.Red;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Transparent;
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
            panel2.BackColor = Color.FromArgb(150, 167, 220, 224);//ARGB，第一个为调节不透明度

            ControlPaint.DrawBorder(e.Graphics,
                                        panel2.ClientRectangle,
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

        private void Home_Paint(object sender, PaintEventArgs e)
        {

        }

        #endregion

        #region 调用部分

        public static void ProxyDel()
        {
            try
            {
                //取消代理
                RegistryKey regKey = Registry.CurrentUser;
                string SubKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Internet Settings";
                RegistryKey optionKey = regKey.OpenSubKey(SubKeyPath, true);
                optionKey.SetValue("ProxyEnable", 0);
            }
            catch (Exception ex)
            {
                MsgShow("取消代理出错！\n" + ex.Message, "Error", true);
                return;
            }
        }

        private void RunY(string Path)
        {
            string TempPath = System.IO.Path.GetTempPath();
            Process proc = null;
            try
            {
                string targetDir = string.Format(TempPath + Path);
                proc = new Process();
                proc.StartInfo.WorkingDirectory = targetDir;
                proc.StartInfo.FileName = TempPath + Path;
                proc.StartInfo.Arguments = string.Format("10");
                proc.Start();
            }
            catch (Exception ex)
            {
                MsgShow("程序出错！\n" + ex.Message, "Error", true);
                return;
            }
        }

        private void RunN(string Path)
        {
            string TempPath = System.IO.Path.GetTempPath();
            Process proc = null;
            try
            {
                string targetDir = string.Format(TempPath + Path);
                proc = new Process();
                proc.StartInfo.WorkingDirectory = targetDir;
                proc.StartInfo.FileName = TempPath + Path;
                proc.StartInfo.Arguments = string.Format("10");
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
            }
            catch (Exception ex)
            {
                MsgShow("程序出错！\n" + ex.Message, "Error", true);
                return;
            }
        }

        private void SMS(string command)
        {

            if (checkBox3.Checked == true)
            {
                ProxyDel();
            }

            string TempPath = System.IO.Path.GetTempPath();
            string str = System.Environment.CurrentDirectory;
            Directory.CreateDirectory(TempPath + "KCN");
            string BoomRun = "\\KCN\\BoomStart.cmd";//运行脚本

            StreamWriter sw = new StreamWriter(TempPath + BoomRun, false);
            sw.WriteLine("@echo off");
            sw.WriteLine("title Loading...");
            sw.WriteLine("%1 mshta vbscript:CreateObject(\"Shell.Application\").ShellExecute(\"cmd.exe\",\" / c % ~s0::\",\"\",\"runas\",1)(window.close)&&exit");
            sw.WriteLine("cd /d %~dp0");
            sw.WriteLine("chcp 65001");
            sw.WriteLine("cls");
            sw.WriteLine("@echo off");
            sw.WriteLine("echo GUI By KCN");
            sw.WriteLine("SET NAME=短信轰炸");
            sw.WriteLine("TITLE %NAME%");
            sw.WriteLine("REM COLOR C");
            sw.WriteLine("set mod=%1");
            sw.WriteLine("cd /d " + str);
            sw.WriteLine("smsboom " + command);
            sw.WriteLine("mshta vbscript:msgbox(\"运行完毕。\",64,\"提示\")(window.close)");
            sw.Close();

            if (checkBox4.Checked == true)
            {
                RunN(BoomRun);
            }
            else
            {
                RunY(BoomRun);
            }
        }

        public static bool IsNumber(string str)
        {
            if (str == null || str.Length == 0)
                return false;
            ASCIIEncoding ascii = new ASCIIEncoding();
            byte[] bytestr = ascii.GetBytes(str);

            foreach (byte c in bytestr)
            {
                if (c < 48 || c > 57)
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region 运行部分

        /// <summary>
        /// 发送命令
        /// </summary>
        private void RunSMS()
        {
            //输入变量
            string ThreadNum = this.numericUpDown1.Value.ToString();
            string BoomNum = this.numericUpDown2.Value.ToString();
            string BoomTime = this.numericUpDown3.Value.ToString();
            string TelNum = textBox1.Text;

            string _Boom = "";
            _Boom += checkBox1.Checked ? " -f " + BoomNum + " -i " + BoomTime : "";
            string _Proxy = "";
            _Proxy += checkBox2.Checked ? " -e" : "";

            SMS("run -t " + ThreadNum + " -p " + TelNum + _Boom + _Proxy);

        }

        /// <summary>
        /// 程序判断
        /// </summary>
        private void Run()
        {
            string a = textBox1.Text;
            bool i = IsNumber(a);
            if (button3.Text == "开始轰炸")
            {
                if (textBox1.Text.Length != 11)
                {
                    MsgShow("请输入11位手机号！", "提示", true);
                }
                else if (i == false)
                {
                    MsgShow("请输入正确的手机号！", "提示", true);
                }
                else
                {
                    var ret = MsgShow("将要对手机号" + a + "进行短信轰炸！\n您确定吗？请慎重考虑！", "提示", true);
                    if (ret)
                        try
                        {
                            button3.Text = "停止轰炸";
                            textBox1.ReadOnly = true;
                            RunSMS();
                        }
                        catch (Exception ex)
                        {
                            MsgShow("程序出错！\n" + ex.Message, "提示", true);
                        }
                    else
                        textBox1.ReadOnly = false;
                    button3.Text = "开始轰炸";
                    return;
                }
            }
            else
            {
                textBox1.ReadOnly = false;
                button3.Text = "开始轰炸";
            }
        }

        /// <summary>
        /// Update API
        /// </summary>
        private void UpdateSMS()
        {
            try
            {
                SMS("update");
            }
            catch (Exception ex)
            {
                MsgShow("程序出错！\n" + ex.Message, "Error", true);
            }
        }

        #endregion

        #region 程序初始化
        private void Form1_Load(object sender, EventArgs e)
        {
            Directory.CreateDirectory(".\\skin");
            LoginForm();
            //声明互斥体 使程序只能启动一个
            Mutex mutex = new Mutex(false, "KCNMsgBoom");
            //判断互斥体是否在使用中
            bool Runing = !mutex.WaitOne(0, false);
            if (!Runing)
            {

            }
            else
            {
                MsgShow("已经有一个程序在运行！", "Error", true);
                Close();
                Application.Exit();
                return;
            }

            try
            {
                this.BackgroundImage = Image.FromFile(".\\skin\\bg.png");
            }
            catch (Exception ex)
            {
                MsgShow("未能加载背景图片: \n" + ex.Message, "Error - 资源加载失败", true);
            }

            string BoomPath = ".\\smsboom.exe";
            if (!File.Exists(BoomPath))
            {

                var ret = MsgShow("请把主程序文件smsboom.exe放置在本程序目录下！\n若没有下载主程序文件，请点击确定前往下载。", "Error - 文件缺失", true);
                if (ret)
                System.Diagnostics.Process.Start("https://github.com/OpenEthan/SMSBoom/releases/");
                else
                Close();
                Application.Exit();
                return;
            }

            if (checkBox1.Checked == false)
            {
                label4.Visible = false;
                label5.Visible = false;
                numericUpDown2.Visible = false;
                numericUpDown3.Visible = false;
            }


        }

        #endregion

        #region 用户交互
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Run();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateSMS();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetProxy frm = new SetProxy();
            frm.ShowDialog();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                var ret = MsgShow("勾选此选项，将会静默启动程序，不显示命令行窗口。\n程序运行完毕后，将会自动关闭并弹出提示。", "提示", true);
                if (ret)
                    this.checkBox4.Checked = true;
                else
                    this.checkBox4.Checked = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                label4.Visible = true;
                label5.Visible = true;
                numericUpDown2.Visible = true;
                numericUpDown3.Visible = true;
            }
            if (checkBox1.Checked == false)
            {
                label4.Visible = false;
                label5.Visible = false;
                numericUpDown2.Visible = false;
                numericUpDown3.Visible=false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                var ret = MsgShow("勾选此选项，将会以代理模式启动程序。\n勾选前请确定您设置了有效的代理地址！", "提示", true);
                if (ret)
                    this.checkBox2.Checked = true;
                else
                    this.checkBox2.Checked = false;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://space.bilibili.com/475547854/");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/OpenEthan/SMSBoom/");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MsgShow("本程序使用的背景图片@穆玛飞飞，人物OC@喵太不咕。\n本程序是基于Github开源项目SMSBoom制作的GUI，遵\n循GPL开源协议。本程序完全免费，禁止用于商业及非\n法用途。使用本软件造成的事故与损失，与作者无关。", "版权声明 - Made By KCN Copyright ©  2023", true);
        }

        #endregion

    }
}
