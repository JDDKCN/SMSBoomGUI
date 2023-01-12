using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMSBoomGUI
{
    public partial class SetProxy : Form
    {
        public SetProxy()
        {
            InitializeComponent();
        }

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

        private void SetProxy_Resize(object sender, EventArgs e)
        {
            SetWindowRegion();
        }

        #endregion

        #region 绘制程序Panel白线及颜色
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

        #endregion

        #region 鼠标事件
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        bool beginMove = false;//初始化鼠标位置
        int currentXPosition;
        int currentYPosition;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                beginMove = true;
                currentXPosition = MousePosition.X;//鼠标的x坐标为当前窗体左上角x坐标
                currentYPosition = MousePosition.Y;//鼠标的y坐标为当前窗体左上角y坐标
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

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {

        }
        //关闭picturebox的鼠标操作
        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Transparent;

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox1.BackColor = Color.Red;
        }


        #endregion

        #region 功能实现

        private void Write()
        {
            try
            {
                string Path1 = ".\\http_proxy.txt";
                string Path2 = ".\\socks4_proxy.txt";
                string Path3 = ".\\socks5_proxy.txt";

                StreamWriter sw = new StreamWriter(Path1, false);
                sw.WriteLine(richTextBox1.Text);
                sw.Close();

                StreamWriter sw2 = new StreamWriter(Path2, false);
                sw2.WriteLine(richTextBox1.Text);
                sw2.Close();

                StreamWriter sw3 = new StreamWriter(Path3, false);
                sw3.WriteLine(richTextBox1.Text);
                sw3.Close();

                MsgShow("保存成功！", "提示", true);

            }
            catch (Exception ex)
            {
                MsgShow("程序出错！\n" + ex.Message, "Error",true);
            }

        }

        private void Open()
        {
            //打开文件
            OpenFileDialog ofd = new OpenFileDialog();      //声明打开文件
            ofd.Title = "请选择打开的文件";
            ofd.Filter = "txt文件(*.txt)|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)  //窗体打开成功
            {
                try
                {
                    string reader2 = File.ReadAllText(ofd.FileName);
                    richTextBox1.Text = reader2;
                }
                catch (System.Exception ex)
                {
                    MsgShow("程序出错！\n" + ex.Message, "Error",true);
                }
            }

        }

        private void Save()
        {
            try
            {
                string str = System.Environment.CurrentDirectory;
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "";
                sfd.InitialDirectory = str;
                sfd.Filter = "文本文件| *.txt";
                sfd.ShowDialog();
                string Path = sfd.FileName;
                if(Path != "")
                {
                    StreamWriter sw = new StreamWriter(Path, false);
                    sw.WriteLine(richTextBox1.Text);
                    sw.Close();
                    MsgShow("保存成功！", "提示", true);
                }

            }
            catch (Exception ex)
            {
                MsgShow("程序出错！\n" + ex.Message, "Error",true);
            }

        }
        #endregion

        #region 剪切板右键菜单

        private void 文字编辑器V100BetaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selectText = ((RichTextBox)contextMenuStrip1.SourceControl).SelectedText;
            if (selectText != "")
            {
                Clipboard.SetText(selectText);
            }
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                RichTextBox txtBox = (RichTextBox)contextMenuStrip1.SourceControl;
                int index = txtBox.SelectionStart;  //记录下粘贴前的光标位置
                string text = txtBox.Text;
                //删除选中的文本
                text = text.Remove(txtBox.SelectionStart, txtBox.SelectionLength);
                //在当前光标输入点插入剪切板内容
                text = text.Insert(txtBox.SelectionStart, Clipboard.GetText());
                txtBox.Text = text;
                //重设光标位置
                txtBox.SelectionStart = index;
            }
        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            //没有选择文本时，复制菜单禁用
            string selectText = ((RichTextBox)contextMenuStrip1.SourceControl).SelectedText;
            if (selectText != "")
                复制ToolStripMenuItem.Enabled = true;
            else
                复制ToolStripMenuItem.Enabled = false;
            //剪切板没有文本内容时，粘贴菜单禁用
            if (Clipboard.ContainsText())
            {
                粘贴ToolStripMenuItem.Enabled = true;
            }
            else
                粘贴ToolStripMenuItem.Enabled = false;
        }

        #endregion

        #region 窗体初始化
        private void SetProxy_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackgroundImage = Image.FromFile(".\\skin\\bg.png");
            }
            catch
            {
            }

            richTextBox1.ContextMenuStrip = contextMenuStrip1;
            文字编辑器V100BetaToolStripMenuItem.Enabled = false;

        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            Write();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Save();
        }
    }
}
