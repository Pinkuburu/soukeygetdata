using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace SoukeyNetget
{
    public partial class frmUpdate : Form
    {
        private  string Old_Copy;
        private string New_Copy;
        private string SCode = "";
        private bool OnLoad = false;

        public frmUpdate()
        {
            InitializeComponent();
        }

        private void frmUpdate_Load(object sender, EventArgs e)
        {
            
        }

        private void GetCopy()
        {
            Gather.cGatherWeb gData = new Gather.cGatherWeb();

            this.textBox1.Text ="正在获取本地系统版本号........";
            Application.DoEvents();
            Old_Copy = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.textBox1.Text +="\r\n" + "本地版本号为：" + Assembly.GetExecutingAssembly().GetName().Version;
            Application.DoEvents();
            this.textBox1.Text += "\r\n" + "开始连接Soukey采摘官方网站：http://www.yijie.net ......";
            Application.DoEvents();
            SCode = cTool.GetHtmlSource("http://www.yijie.net/user/soft/updatesoukey.html", true);
            if (SCode == "" || SCode == null)
            {
                this.textBox1.Text += "\r\n" + "连接失败，请确认是否已经连接到Internet。" + "\r\n" + "检查已经停止！";
                Application.DoEvents();
                return;
            }

            this.textBox1.Text += "\r\n" + "连接成功！" + "\r\n" + "开始获取Soukey采摘最新版本号.......";
            Application.DoEvents();

            //增加采集的标志
            Task.cWebpageCutFlag c;

            c = new Task.cWebpageCutFlag();
            c.id =0;
            c.Title = "版本";
            c.DataType =(int) cGlobalParas.GDataType.Txt;
            c.StartPos = "版本：";
            c.EndPos = "</p>";
            c.LimitSign =(int) cGlobalParas.LimitSign.NoLimit;
            gData.CutFlag.Add(c);
            c = null;

            DataTable dGather = gData.GetGatherData("http://www.yijie.net/user/soft/updatesoukey.html", cGlobalParas.WebCode.utf8, "", "", "", Program.getPrjPath());

            New_Copy =dGather.Rows [0].ItemArray[0].ToString ();
            this.textBox1.Text += "\r\n" + "Soukey采摘最新版本号为：" + New_Copy ;
            Application.DoEvents();

            ///版本号比较需要比较四个界别：00.00.00.00，所有版本必须遵照此格式，否则会出现错误。
            ///比较顺序为：主版本->低版本，只要有一个新版本号大于旧版本号，则就进行升级操作

            int Old_V;
            int New_V;

            for (int i = 0; i < 3; i++)
            {
                Old_V=int.Parse ( Old_Copy .Substring(0,Old_Copy .IndexOf (".")));
                Old_Copy =Old_Copy .Substring (Old_Copy .IndexOf (".")+1,Old_Copy .Length -Old_Copy .IndexOf (".")-1);

                New_V = int.Parse(New_Copy.Substring(0, New_Copy.IndexOf(".")));
                New_Copy = New_Copy.Substring(New_Copy.IndexOf(".")+1, New_Copy.Length - New_Copy.IndexOf(".")-1);

                if (New_V >Old_V )
                {
                    if (MessageBox.Show("发现新的版本，是否下载？", "Soukey采摘 系统询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        this.textBox1.Text += "\r\n" + "发现了新的版本，但您取消了下载操作，如需下载请再次运行“检查更新”操作。";
                        Application.DoEvents();
                        gData = null;

                        this.button1.Enabled = true;
                        return;
                    }
                    else
                    {
                        DownloadSoft();
                        return;
                    }   
                }
            }

            this.textBox1.Text += "\r\n" + "没有新版本，无需进行下载操作！";
            Application.DoEvents();

            this.button1.Enabled = true;

        }

        private void DownloadSoft()
        {
            Gather.cGatherWeb gData = new Gather.cGatherWeb();

            //增加采集的标志
            Task.cWebpageCutFlag c;

            c = new Task.cWebpageCutFlag();
            c.id = 0;
            c.Title = "版本";
            c.DataType = (int)cGlobalParas.GDataType.File;
            c.StartPos = "<a href=\"";
            c.EndPos = "\"";
            c.LimitSign = (int)cGlobalParas.LimitSign.NoLimit;
            gData.CutFlag.Add(c);
            c = null;

            this.textBox1.Text += "\r\n" + "开始下载最新版本，请等待......" ;
            Application.DoEvents();

            DataTable dGather = gData.GetGatherData("http://www.yijie.net/user/soft/updatesoukey.html", cGlobalParas.WebCode.utf8, "", "", "", Program.getPrjPath());
            
            this.textBox1.Text += "\r\n" + "下载成功，请在Soukey采摘工作目录中检查Soukey.exe文件，此文件为最新版本，并且是rar自解压的免安装版本！" ;
            Application.DoEvents();

        }

        private void frmUpdate_Activated(object sender, EventArgs e)
        {
            if (OnLoad == false)
            {
                OnLoad = true;

                try
                {
                    GetCopy();
                }
                catch (System.Exception ex)
                {
                    this.textBox1.Text += "\r\n" + "检查更新失败！错误信息为：" + ex.Message ;
                    Application.DoEvents();
                }

                this.button1.Enabled=true ;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}