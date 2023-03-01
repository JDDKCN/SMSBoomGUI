using System;
using System.Threading;
using System.Windows.Forms;
using static GUI.Msg;

namespace GUI
{
    internal static class Program
    {

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //声明互斥体 使程序只能启动一个
            Mutex mutex = new Mutex(false, "KCNSMSBOOM");
            //判断互斥体是否在使用中
            bool Runing = !mutex.WaitOne(0, false);
            if (!Runing)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                NewWelcome NewWelcome = new NewWelcome();
                if (NewWelcome.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new Home());
                }
            }
            else
            {
                MsgShow("已经有一个程序在运行！", "Error", true);
                Application.Exit();
                return;
            }
        }
    }
}

