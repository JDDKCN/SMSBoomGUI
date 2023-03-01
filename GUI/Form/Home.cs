using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static GUI.Msg;

namespace GUI
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            this.textBox1.BackAlpha = 0;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Multiline = false;
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

        //窗体大小改变
        const int WM_NCHITTEST = 0x0084;
        const int HTLEFT = 10;
        const int HTRIGHT = 11;
        const int HTTOP = 12;
        const int HTTOPLEFT = 13;
        const int HTTOPRIGHT = 14;
        const int HTBOTTOM = 15;
        const int HTBOTTOMLEFT = 0x10;
        const int HTBOTTOMRIGHT = 17;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    Point vPoint = new Point((int)m.LParam & 0xFFFF,
                        (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (vPoint.X <= 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPLEFT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMLEFT;
                        else m.Result = (IntPtr)HTLEFT;
                    else if (vPoint.X >= ClientSize.Width - 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPRIGHT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMRIGHT;
                        else m.Result = (IntPtr)HTRIGHT;
                    else if (vPoint.Y <= 5)
                        m.Result = (IntPtr)HTTOP;
                    else if (vPoint.Y >= ClientSize.Height - 5)
                        m.Result = (IntPtr)HTBOTTOM;
                    break;
            }
        }

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

        //改变窗体大小
        public const int WMSZ_LEFT = 0xF001;
        public const int WMSZ_RIGHT = 0xF002;
        public const int WMSZ_TOP = 0xF003;
        public const int WMSZ_TOPLEFT = 0xF004;
        public const int WMSZ_TOPRIGHT = 0xF005;
        public const int WMSZ_BOTTOM = 0xF006;
        public const int WMSZ_BOTTOMLEFT = 0xF007;
        public const int WMSZ_BOTTOMRIGHT = 0xF008;
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

        private void pictureBox4_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox4.BackColor = Color.White;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.Transparent;
        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox4.BackColor = Color.DarkGray;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox6_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox6.BackColor = Color.SkyBlue;
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.BackColor = Color.Transparent;
        }
        private void pictureBox6_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox6.BackColor = Color.DarkGray;
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

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            panel4.BackColor = Color.FromArgb(150, 167, 220, 224);//ARGB，第一个为调节不透明度

            ControlPaint.DrawBorder(e.Graphics,
                                        panel4.ClientRectangle,
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

        public void SMS(string command)
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
                    var ret = MsgShow("将要对手机号" + a + "进行短信轰炸！\n您确定吗？", "提示", true);
                    if (ret)
                        try
                        {
                            RunSMS();
                        }
                        catch (Exception ex)
                        {
                            MsgShow("程序出错！\n" + ex.Message, "提示", true);
                        }
                    else
                    return;
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
                GUI.Msg.MsgShow("未能正确加载背景图片！ \n请进入设置重新指定文件路径。", "Error - 资源加载失败", true);
            }
            //获得当前登录的Windows用户标示
            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            //判断当前登录用户是否为管理员
            if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            {
                //是管理员
                label3.Visible = true;
            }
            label2.Text = "Version " + Ver.Version.ToString() + "\r\n免费软件 禁止商用\r\n";
        }

        #endregion

        #region 用户交互
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(Ver.biliURL);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                //json读取
                StreamReader reader = File.OpenText(".\\config.json");
                JsonTextReader jsonTextReader = new JsonTextReader(reader);
                JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
                string FilePath = jsonObject["SMSBoomPath"].ToString(); // 类似
                reader.Close();

                if (System.IO.File.Exists(FilePath + "\\smsboom.exe"))
                {
                }
                else
                {
                    var ret = GUI.Msg.MsgShow("未找到SMSBoom.exe，无法使用程序！ \n是否下载？点\"确定\"开始下载。", "提示", true);
                    if (ret)
                        StrDow();
                    else
                        return;
                }

                if (System.IO.File.Exists(FilePath + "\\api.json"))
                {
                    if (System.IO.File.Exists(FilePath + "\\GETAPI.json"))
                    {
                        Run();
                    }
                    else
                    {
                        var ret = GUI.Msg.MsgShow("未找到API文件，无法使用程序！请更新接口。 \n点\"确定\"开始更新。", "提示", true);
                        if (ret)
                            UpdateSMS();
                        else
                            return;
                    }
                }
                else
                {
                    var ret = GUI.Msg.MsgShow("未找到API文件，无法使用程序！请更新接口。 \n点\"确定\"开始更新。", "提示", true);
                    if (ret)
                        UpdateSMS();
                    else
                        return;
                }
            }
            catch
            {
            }

        }
        void StrDow()
        {
            download frm = new download();
            frm.ShowDialog();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Setting frm = new Setting();
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
                label6.Visible = true;
                label5.Visible = true;
                numericUpDown2.Visible = true;
                numericUpDown3.Visible = true;
            }
            if (checkBox1.Checked == false)
            {
                label6.Visible = false;
                label5.Visible = false;
                numericUpDown2.Visible = false;
                numericUpDown3.Visible = false;
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

        #endregion

    }
}
