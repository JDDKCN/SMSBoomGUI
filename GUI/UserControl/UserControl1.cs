using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static GUI.Msg;

namespace GUI
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            this.richEdit502.Multiline = false;
            this.richEdit502.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richEdit501.Multiline = false;
            this.richEdit501.BorderStyle = System.Windows.Forms.BorderStyle.None;
        }

        #region 绘制程序Panel

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.BackColor = Color.FromArgb(150, 167, 220, 224);//ARGB，第一个为调节不透明度

            ControlPaint.DrawBorder(e.Graphics,
                                        panel1.ClientRectangle,
                                        Color.White,
                                        2,
                                        ButtonBorderStyle.Solid,
                                        Color.White,
                                        2,
                                        ButtonBorderStyle.Solid,
                                        Color.White,
                                        2,
                                        ButtonBorderStyle.Solid,
                                        Color.White,
                                        2,
                                        ButtonBorderStyle.Solid);

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            panel2.BackColor = Color.FromArgb(150, 167, 220, 224);//ARGB，第一个为调节不透明度

            ControlPaint.DrawBorder(e.Graphics,
                                        panel2.ClientRectangle,
                                        Color.White,
                                        2,
                                        ButtonBorderStyle.Solid,
                                        Color.White,
                                        2,
                                        ButtonBorderStyle.Solid,
                                        Color.White,
                                        2,
                                        ButtonBorderStyle.Solid,
                                        Color.White,
                                        2,
                                        ButtonBorderStyle.Solid);

        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            groupBox1.BackColor = Color.FromArgb(100, 167, 220, 224);//ARGB，第一个为调节不透明度

            ControlPaint.DrawBorder(e.Graphics,
                            groupBox1.ClientRectangle,
                            Color.White,
                            2,
                            ButtonBorderStyle.Solid,
                            Color.White,
                            2,
                            ButtonBorderStyle.Solid,
                            Color.White,
                            2,
                            ButtonBorderStyle.Solid,
                            Color.White,
                            2,
                            ButtonBorderStyle.Solid);

        }

        private void label6_Paint(object sender, PaintEventArgs e)
        {
        }

        #endregion

        #region 窗体Load
        private void UserControl1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(50, 0, 0, 0);//ARGB，第一个为调节不透明度

            FilesINI ConfigINI = new FilesINI();
            string ImgFile = ConfigINI.INIRead("Image", "BgFile", ".\\skin\\info.ini");
            string ImgFile2 = ConfigINI.INIRead("Image", "InfoFile", ".\\skin\\info.ini");
            if (File.Exists(ImgFile) == true)
            {
                richEdit502.Text = ImgFile;
            }
            else
            {
                richEdit502.Text = "文件路径失效，请重新选择目录...";
            }

            if (File.Exists(ImgFile2) == true)
            {
                richEdit501.Text = ImgFile2;
            }
            else
            {
                richEdit501.Text = "文件路径失效，请重新选择目录...";
            }

            Setting pform = this.ParentForm as Setting;
            pform.pictureBox4.BackColor = Color.SkyBlue;
        }
        #endregion

        #region 自定义程序背景
        private void BgSet(string Path)
        {
            //打开文件
            OpenFileDialog ofd = new OpenFileDialog();      //声明打开文件
            ofd.Title = "请选择作为背景的图片";
            ofd.Filter = "图片文件(*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp|所有文件(*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)  //窗体打开成功
            {
                try
                {
                    string FilePath = ofd.FileName;
                    FilesINI ConfigINI = new FilesINI();
                    ConfigINI.INIWrite("Image", Path, FilePath, ".\\skin\\info.ini");
                    //this.BackgroundImage = Image.FromFile(ImgFile);
                    this.BackgroundImage = Image.FromFile(FilePath);
                    GUI.Msg.MsgShow("设置成功！ \n", "提示", true);
                }
                catch (System.Exception ex)
                {
                    GUI.Msg.MsgShow("程序出错！\n" + ex.Message, "Error", true);
                }
            }

        }
        #endregion

        #region 用户交互
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            BgSet("BgFile");
            FilesINI ConfigINI = new FilesINI();
            string ImgFile = ConfigINI.INIRead("Image", "BgFile", ".\\skin\\info.ini");
            richEdit502.Text = ImgFile;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BgSet("InfoFile");
            FilesINI ConfigINI = new FilesINI();
            string ImgFile = ConfigINI.INIRead("Image", "InfoFile", ".\\skin\\info.ini");
            richEdit501.Text = ImgFile;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Proxy frm = new Proxy();
            frm.ShowDialog();
        }

        #endregion

        #region UpdateAPI
        private void button4_Click(object sender, EventArgs e)
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
                    SMS("update");
                }
                else
                {
                    var ret = GUI.Msg.MsgShow("未找到SMSBoom.exe，无法使用程序！ \n是否下载？点\"确定\"开始下载。", "提示", true);
                    if (ret)
                        StrDow();
                    else
                        return;
                }

            }
            catch (Exception ex)
            {
                MsgShow("程序出错！\n" + ex.Message, "Error", true);
            }

            void StrDow()
            {
                download frm = new download();
                frm.ShowDialog();
            }

            void SMS(string command)
            {
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
                sw.WriteLine("SET NAME=升级接口");
                sw.WriteLine("TITLE %NAME%");
                sw.WriteLine("REM COLOR C");
                sw.WriteLine("set mod=%1");
                sw.WriteLine("cd /d " + str);
                sw.WriteLine("smsboom " + command);
                sw.WriteLine("mshta vbscript:msgbox(\"运行完毕。\",64,\"提示\")(window.close)");
                sw.Close();
            }

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

            Process proc = null;
            try
            {
                string TempPath = System.IO.Path.GetTempPath();
                string BoomRun = "\\KCN\\BoomStart.cmd";//运行脚本

                string targetDir = string.Format(TempPath + BoomRun);
                proc = new Process();
                proc.StartInfo.WorkingDirectory = targetDir;
                proc.StartInfo.FileName = TempPath + BoomRun;
                proc.StartInfo.Arguments = string.Format("10");
                proc.Start();
            }
            catch (Exception ex)
            {
                MsgShow("程序出错！\n" + ex.Message, "Error", true);
                return;
            }

        }

        #endregion

    }
}
