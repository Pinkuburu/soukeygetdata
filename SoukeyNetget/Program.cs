using System;
using System.Collections.Generic;
using System.Windows.Forms;

//程序入口类
namespace SoukeyNetget
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        /// 
        private static ApplicationContext context;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            frmStart sf = new frmStart();
            sf.Show();
            context = new ApplicationContext();
            context.Tag = sf;
            Application.Idle += new EventHandler(Application_Idle); //注册程序运行空闲去执行主程序窗体相应初始化代码
            Application.Run(context);
        }

        private static void Application_Idle(object sender, EventArgs e)
        {
            Application.Idle -= new EventHandler(Application_Idle);
            if (context.MainForm == null)
            {
                frmMain mf = new frmMain ();
                context.MainForm = mf;
                frmStart sf = (frmStart)context.Tag;

                //初始化界面信息
                sf.label3.Text = "正在初始化主程序应用信息，请等待......";
                Application.DoEvents();
                mf.IniForm();

                //初始化对象并开始启动运行区的任务
                sf.label3.Text = "正在初始化运行区任务信息，请等待......";
                Application.DoEvents();
                mf.UserIni();

                sf.label3.Text = "正在启动自动运行任务监听器，请等待......";
                Application.DoEvents();
                mf.StartListen();

                //mf.IniForm();

                sf.Close();                                 //关闭启动窗体
                sf.Dispose();

                
                mf.Show();                                  //启动主程序窗体

            }
        }

        public static string getPrjPath()
        {
            return Application.StartupPath + "\\";

        }
    }
}