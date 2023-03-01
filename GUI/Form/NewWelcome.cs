using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GUI
{
    public partial class NewWelcome : Form
    {
        public NewWelcome()
        {
            InitializeComponent();

            string NewtonjsonPath = ".\\Newtonsoft.Json.dll";
            if (!File.Exists(NewtonjsonPath))
            {
                var ret = GUI.Msg.MsgShow("缺少必要运行库：Newtonsoft.Json.dll 。\n请尝试重新安装程序。", "提示", true);
                if (ret)
                    Close();
                else
                    Close();
                return;
            }

        }

        #region 加载主窗体
        private void F_Loading_Shown(object sender, EventArgs e)
        {
            using (BackgroundWorker bw = new BackgroundWorker())
            {
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);//开始的事件

                bw.DoWork += new DoWorkEventHandler(bw_DoWork);//完成的事件

                bw.RunWorkerAsync("Tank");

            }
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)// 这里是后台线程
        {
            Work();
            System.Threading.Thread.Sleep(1000);
        }

        void Work()
        {
            string TempPath = System.IO.Path.GetTempPath();
            Directory.CreateDirectory(TempPath + "KCN");

            string path = ".\\skin";
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }

            string INIPath = ".\\skin\\info.ini";
            if (!File.Exists(INIPath))
            {
                StreamWriter sw = new StreamWriter(INIPath, false);
                sw.WriteLine("[Image]");
                sw.WriteLine("BgFile=" + ".\\skin\\bg.jpg");
                sw.WriteLine("InfoFile=" + ".\\skin\\info.jpg");
                sw.WriteLine("Start=" + ".\\skin\\Start.jpg");
                sw.Close();
            }

            string JsonAPath = ".\\config.json";
            if (!File.Exists(JsonAPath))
            {
                StreamWriter sw = new StreamWriter(JsonAPath, false);
                sw.WriteLine("{");
                sw.WriteLine("\"SMSBoomPath\": \".\\\\\",");
                sw.WriteLine("\"DownloadURL\": \"https://github.com/OpenEthan/SMSBoom/releases/download/main/smsboom.exe\"");
                sw.WriteLine("}");
                sw.Close();
            }

            try
            {
                //json读取
                StreamReader reader = File.OpenText(".\\config.json");
                JsonTextReader jsonTextReader = new JsonTextReader(reader);
                JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
                jsonObject["Made By KCN 禁止倒卖"] = "SMSBoomGUI";
                reader.Close();
                string convertString = Convert.ToString(jsonObject);
                File.WriteAllText(".\\config.json", convertString);
            }
            catch { }

            try
            {
                //json读取
                StreamReader reader = File.OpenText(".\\config.json");
                JsonTextReader jsonTextReader = new JsonTextReader(reader);
                JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
                string FilePath = jsonObject["SMSBoomPath"].ToString(); // 类似
                reader.Close();

                if (!File.Exists(FilePath + "\\smsboom.exe"))
                {
                    var ret = GUI.Msg.MsgShow("未找到SMSBoom.exe，无法使用程序！ \n是否下载？点\"是\"开始下载。", "提示", true);
                    if (ret)
                        StrDow();
                    else
                        Close();
                    return;

                }
            }
            catch { }
        }

        void StrDow()
        {
            download frm = new download();
            frm.ShowDialog();
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)//后台线程完成后的响应事件
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
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

        private void frmWelcome_MouseDown(object sender, MouseEventArgs e)
        {
            //拖动窗体
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region 窗体Load
        private void frmWelcome_Load(object sender, EventArgs e)
        {
            try
            {
                FilesINI ConfigINI = new FilesINI();
                string ImgFile = ConfigINI.INIRead("Image", "Start", ".\\skin\\info.ini");
                pictureBox2.BackgroundImage = Image.FromFile(ImgFile);
            }
            catch
            {
                this.pictureBox2.Image = global::GUI.Properties.Resources.KCN_Logo_W_B;
            }
            label4.Text = "\r\n   " + Ver.Version.ToString();
            F_Loading_Shown(sender, e);
        }
        #endregion

    }
}
