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

            this.textBox1.Text ="���ڻ�ȡ����ϵͳ�汾��........";
            Application.DoEvents();
            Old_Copy = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.textBox1.Text +="\r\n" + "���ذ汾��Ϊ��" + Assembly.GetExecutingAssembly().GetName().Version;
            Application.DoEvents();
            this.textBox1.Text += "\r\n" + "��ʼ����Soukey��ժ�ٷ���վ��http://www.yijie.net ......";
            Application.DoEvents();
            SCode = cTool.GetHtmlSource("http://www.yijie.net/user/soft/updatesoukey.html", true);
            if (SCode == "" || SCode == null)
            {
                this.textBox1.Text += "\r\n" + "����ʧ�ܣ���ȷ���Ƿ��Ѿ����ӵ�Internet��" + "\r\n" + "����Ѿ�ֹͣ��";
                Application.DoEvents();
                return;
            }

            this.textBox1.Text += "\r\n" + "���ӳɹ���" + "\r\n" + "��ʼ��ȡSoukey��ժ���°汾��.......";
            Application.DoEvents();

            //���Ӳɼ��ı�־
            Task.cWebpageCutFlag c;

            c = new Task.cWebpageCutFlag();
            c.id =0;
            c.Title = "�汾";
            c.DataType =(int) cGlobalParas.GDataType.Txt;
            c.StartPos = "�汾��";
            c.EndPos = "</p>";
            c.LimitSign =(int) cGlobalParas.LimitSign.NoLimit;
            gData.CutFlag.Add(c);
            c = null;

            DataTable dGather = gData.GetGatherData("http://www.yijie.net/user/soft/updatesoukey.html", cGlobalParas.WebCode.utf8, "", "", "", Program.getPrjPath());

            New_Copy =dGather.Rows [0].ItemArray[0].ToString ();
            this.textBox1.Text += "\r\n" + "Soukey��ժ���°汾��Ϊ��" + New_Copy ;
            Application.DoEvents();

            ///�汾�űȽ���Ҫ�Ƚ��ĸ����00.00.00.00�����а汾�������մ˸�ʽ���������ִ���
            ///�Ƚ�˳��Ϊ�����汾->�Ͱ汾��ֻҪ��һ���°汾�Ŵ��ھɰ汾�ţ���ͽ�����������

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
                    if (MessageBox.Show("�����µİ汾���Ƿ����أ�", "Soukey��ժ ϵͳѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        this.textBox1.Text += "\r\n" + "�������µİ汾������ȡ�������ز����������������ٴ����С������¡�������";
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

            this.textBox1.Text += "\r\n" + "û���°汾������������ز�����";
            Application.DoEvents();

            this.button1.Enabled = true;

        }

        private void DownloadSoft()
        {
            Gather.cGatherWeb gData = new Gather.cGatherWeb();

            //���Ӳɼ��ı�־
            Task.cWebpageCutFlag c;

            c = new Task.cWebpageCutFlag();
            c.id = 0;
            c.Title = "�汾";
            c.DataType = (int)cGlobalParas.GDataType.File;
            c.StartPos = "<a href=\"";
            c.EndPos = "\"";
            c.LimitSign = (int)cGlobalParas.LimitSign.NoLimit;
            gData.CutFlag.Add(c);
            c = null;

            this.textBox1.Text += "\r\n" + "��ʼ�������°汾����ȴ�......" ;
            Application.DoEvents();

            DataTable dGather = gData.GetGatherData("http://www.yijie.net/user/soft/updatesoukey.html", cGlobalParas.WebCode.utf8, "", "", "", Program.getPrjPath());
            
            this.textBox1.Text += "\r\n" + "���سɹ�������Soukey��ժ����Ŀ¼�м��Soukey.exe�ļ������ļ�Ϊ���°汾��������rar�Խ�ѹ���ⰲװ�汾��" ;
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
                    this.textBox1.Text += "\r\n" + "������ʧ�ܣ�������ϢΪ��" + ex.Message ;
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