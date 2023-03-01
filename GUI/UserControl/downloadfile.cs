using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GUI
{
    public partial class downloadfile : UserControl
    {

        public downloadfile()
        {
            InitializeComponent();
           
            //应用当前目录
            curPath = AppDomain.CurrentDomain.BaseDirectory.ToString();

            //初始化Http下载对象
            initHttpManager();
        }

        #region Panel绘制
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            panel2.BackColor = Color.FromArgb(150, 131, 175, 200);//ARGB，第一个为调节不透明度

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

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
                                        panel3.ClientRectangle,
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

        #endregion

        #region 功能实现

        //string
        private string mSaveFileName = "";//下载文件的保存文件名      
        string curPath = "";//应用当前目录
        HttpHelper mHttpHelper = null;
        public delegate void DelegateProcessShow(double curSize, double speed, uint remainTime, uint spanTime, eDownloadSta sta);
        public delegate void DelegateSetFileSize(double val);
        private double mDownloadFileSize = 0.0;//文件大小
        private double mTotalSpd = 0.0;//用于计算平均速度
        uint mTotalSpdCnt = 0;//用于计算平均速度

        /// <summary>
        /// 初始化Http下载对象
        /// </summary>
        private void initHttpManager()
        {
            if (null == mHttpHelper)
            {
                mHttpHelper = new HttpHelper(new System.Timers.Timer(), processShow, downloadFileSize);
            }
        }

        /// <summary>
        /// 更新下载进度
        /// </summary>
        /// <param name="totalNum">文件总大小</param>
        /// <param name="num">已下载</param>
        /// <param name="proc">进度</param>
        /// <param name="speed">速度</param>
        /// <param name="remainTime">剩余时间</param>
        /// <param name="msg">消息</param>
        public void processShow(double curSize, double speed, uint remainTime, uint spanTime, eDownloadSta sta)
        {
            if (this.InvokeRequired)
            {
                DelegateProcessShow delegateprocShow = new DelegateProcessShow(processShow);
                this.Invoke(delegateprocShow, new object[] { curSize, speed, remainTime, spanTime, sta });
            }
            else
            {
                //Console.WriteLine("curSize:{0}， mDownloadFileSize:{1}", curSize, mDownloadFileSize);

                if (mDownloadFileSize < 0 || sta == eDownloadSta.STA_ERR)
                {
                    MessageBox.Show("下载出错！请删除" + mSaveFileName + "，并重新尝试！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    mHttpHelper.downloadStop();
                    buttonDownload.Text = "下载";
                }
                else if (mDownloadFileSize > 0)
                {
                    labelDownSize.Text = "已下载：" + HttpHelper.formateSize(curSize);
                    int proc = 0;

                    mTotalSpdCnt++;
                    mTotalSpd += speed;
                    if (curSize < mDownloadFileSize)
                    {
                        proc = (int)((curSize / mDownloadFileSize) * 100);
                    }
                    else if (curSize == mDownloadFileSize)
                    {
                        proc = progressBarDownloadFile.Maximum;
                    }
                    else
                    {
                        mHttpHelper.downloadStop();
                        buttonDownload.Text = "下载";
                        MessageBox.Show("文件续传出错！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    this.labelProcess.Text = string.Format("{0}%", proc);
                    Console.WriteLine("{0}%", proc);//test
                    progressBarDownloadFile.Value = proc;
                    this.labelDownSpd.Text = string.Format("速度：{0}/s", HttpHelper.formateSize(speed));
                    Console.WriteLine("速度：{0}/s", HttpHelper.formateSize(speed));//test
                    this.labelDownNeedTime.Text = string.Format("剩余时间：{0}", HttpHelper.formatTime(remainTime));
                    this.labelSpanTime.Text = string.Format("用时：{0}", HttpHelper.formatTime(spanTime));
                    if (proc == progressBarDownloadFile.Maximum)
                    {
                        mHttpHelper.downloadStop();
                        buttonDownload.Text = "下载";
                        this.labelProcess.Text = "100%";
                        progressBarDownloadFile.Value = proc;

                        labelAvgSpd.Visible = true;
                        labelAvgSpd.Text = string.Format("均速：{0}/s", HttpHelper.formateSize((mTotalSpd / mTotalSpdCnt)));
                        //Console.WriteLine("finish:" + DateTime.Now.ToString("yyyyMMdd-HHmmss"));
                        MessageBox.Show("存放路径：" + curPath + mSaveFileName, "下载完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //closeForm
                        download pform = this.ParentForm as download;
                        pform.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 下载文件大小
        /// </summary>
        /// <param name="fileSize"></param>
        void downloadFileSize(double fileSize)
        {
            if (this.InvokeRequired)
            {
                DelegateSetFileSize delegateSetFileSize = new DelegateSetFileSize(downloadFileSize);
                this.Invoke(delegateSetFileSize, new object[] { fileSize });
            }
            else
            {
                if (fileSize > 0)
                {
                    this.labelFileSizeTotal.Text = "文件大小：" + HttpHelper.formateSize(fileSize);
                    mDownloadFileSize = fileSize;
                }
                else if (fileSize < 0)
                {
                    MessageBox.Show("获取文件长度出错!", "错误");
                }
            }
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="httpUrl"></param>
        /// <param name="saveFileName"></param>
        private void download(string httpUrl, string saveFileName)
        {
            mHttpHelper.download(httpUrl, mSaveFileName);
        }

        /// <summary>
        /// 开始下载
        /// </summary>
        private void StartDownload()
        {
            try
            {
                //json读取
                StreamReader reader = File.OpenText(".\\config.json");
                JsonTextReader jsonTextReader = new JsonTextReader(reader);
                JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
                string JsonPath = jsonObject["DownloadURL"].ToString(); // 类似
                reader.Close();

                if (buttonDownload.Text == "下载")
                {
                    //获取http下载路径
                    string httpUrl = JsonPath;
                    if (!httpUrl.StartsWith("http://") && !httpUrl.StartsWith("https://"))
                    {
                        MessageBox.Show("http下载链接不正确！", "提示");
                        Console.WriteLine("下载链接出错");
                        return;
                    }

                    mSaveFileName = Application.StartupPath + "\\" + Path.GetFileName(httpUrl);
                    progressBarDownloadFile.Value = 0;
                    buttonDownload.Text = "停止";
                    labelProcess.Text = "0%";
                    mTotalSpd = 0.0;
                    mTotalSpdCnt = 0;
                    labelAvgSpd.Visible = false;
                    //Console.WriteLine("start:"+DateTime.Now.ToString("yyyyMMdd-HHmmss"));
                    new Thread(() => download(httpUrl, mSaveFileName)).Start(); //开启下载线程
                    Console.WriteLine("开始下载");
                }
                else//停止
                {
                    buttonDownload.Text = "下载";
                    mHttpHelper.downloadStop();
                    Console.WriteLine("暂停下载");
                }

            }
            catch
            {
                GUI.Msg.MsgShow("json格式错误！！\n请尝试删除config.json并重启软件使其重新生成。", "Error - 格式错误");
                download pform = this.ParentForm as download;
                pform.Close();
                return;

            }

        }

        #endregion

        #region Load代码
        private void Form1_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(".\\config.json"))
            {
                try
                {
                    //json读取
                    StreamReader reader = File.OpenText(".\\config.json");
                    JsonTextReader jsonTextReader = new JsonTextReader(reader);
                    JObject jsonObject = (JObject)JToken.ReadFrom(jsonTextReader);
                    string DowPath = jsonObject["DownloadURL"].ToString(); // 类似
                    reader.Close();

                    label1.Text = "下载直链：" + DowPath;
                }
                catch
                {
                    GUI.Msg.MsgShow("json格式错误！！\n请把正确的download.json放置到软件根目录。", "Error - 格式错误");
                    download pform = this.ParentForm as download;
                    pform.Close();
                    return;
                }
            }
            else
            {
                GUI.Msg.MsgShow("未找到保存下载数据的json！！\n请把download.json放置到软件根目录下再试。", "Error - 文件丢失");
                download pform = this.ParentForm as download;
                pform.Close();
                return;
            }

            try
            {
                FilesINI ConfigINI = new FilesINI();
                string ImgFile = ConfigINI.INIRead("Image", "Start", ".\\skin\\info.ini");
                panel3.BackgroundImage = Image.FromFile(ImgFile);
            }
            catch
            {
                this.panel3.BackgroundImage = global::GUI.Properties.Resources.KCN_Logo_W_B;
            }

        }

        #endregion

        #region 用户交互
        private void buttonDownload_Click_1(object sender, EventArgs e)
        {
            StartDownload();
        }


        #endregion

    }
}