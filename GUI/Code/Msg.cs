using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Msg
    {

        private static object Obj = new object();
        public static bool MsgShow(string msg = "Msg", string title = "提示", bool bcancel = false)
        {
            Task<bool> mtask = new Task<bool>
                (
                () =>
                {
                    lock (Obj)
                    {
                        MsgForm frWarning = new MsgForm();//错误窗体
                        frWarning.TopMost = true;

                        try
                        {
                            string ImgFile;
                            FilesINI ConfigINI = new FilesINI();
                            ImgFile = ConfigINI.INIRead("Image", "InfoFile", ".\\skin\\info.ini");
                            frWarning.BackgroundImage = Image.FromFile(ImgFile);
                        }
                        catch
                        {
                            try
                            {
                                string ImgFile;
                                FilesINI ConfigINI = new FilesINI();
                                ImgFile = ConfigINI.INIRead("Image", "BgFile", ".\\skin\\info.ini");
                                frWarning.BackgroundImage = Image.FromFile(ImgFile);
                            }
                            catch
                            {
                                //frWarning.BackColor = Color.White;
                            }
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

    }
}
