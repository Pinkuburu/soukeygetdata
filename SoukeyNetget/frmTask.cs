using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using MySql.Data.MySqlClient;
using System.Web;

///���ܣ��ɼ�������Ϣ����  
///���ʱ�䣺2009-3-2
///���ߣ�һ��
///�������⣺��
///�����ƻ�����
///˵������ 
///�汾��01.10.00
///�޶��������ʽ�汾�Ѿ��޸���1.2
namespace SoukeyNetget
{
    public partial class frmTask : Form
    {

        public delegate void ReturnTaskClass(string tClass);
        public ReturnTaskClass rTClass;

        //�Ƿ��ѱ���������������棬������ȡ����ʱ��
        //Ҳ��Ҫ����������������з��أ���Ҫ�����ڡ�Ӧ�á�
        //�͡�ȡ������ť���ж���
        private bool IsSaveTask = false;

        //����һ��ToolTip
        ToolTip HelpTip = new ToolTip();
        Task.cUrlAnalyze gUrl = new Task.cUrlAnalyze();

        public frmTask()
        {
            InitializeComponent();
            IniData();
        }

        #region ��������״̬
        private cGlobalParas.FormState m_FormState;
        public cGlobalParas.FormState FormState
        {
            get { return m_FormState;}
            set { m_FormState = value; }
        }
        #endregion

        //����ToolTip����Ϣ
        private void SetTooltip()
        {
            //HelpTip.SetToolTip(this.tTask, @"�����������ƣ��������ƿ��������Ļ�Ӣ�ģ������������.*\/,��");
        }

        public void NewTask(string TaskClassName)
        {
            if (TaskClassName != "")
            {
                for (int i = 0; i < this.comTaskClass.Items.Count; i++)
                {
                    if (TaskClassName == this.comTaskClass.Items[i].ToString())
                    {
                        this.comTaskClass.SelectedIndex = i;
                        return;
                    }
                }
            }
            
        }

        public void EditTask(string TClassPath, string TaskName)
        {
            LoadTask(TClassPath, TaskName);
        }

        public void Browser(string TClassPath, string TaskName)
        {
            LoadTask(TClassPath, TaskName);
        }

        private void LoadTask(string TClassPath, string TaskName)
        {
            Task.cTask t = new Task.cTask();
            t.LoadTask(TClassPath + "\\" + TaskName);

            this.tTask.Text =t.TaskName ;
            this.txtTaskDemo.Text = t.TaskDemo;

            if (t.TaskClass == "")
            {
                this.comTaskClass.SelectedIndex = 0;
            }
            else
            {
                this.comTaskClass.SelectedItem=t.TaskClass ;
            }


            this.TaskType.SelectedItem = cGlobalParas.ConvertName(int.Parse(t.TaskType));
            this.comRunType.SelectedItem = cGlobalParas.ConvertName(int.Parse(t.RunType));

            if (this.comRunType.SelectedIndex == 0)
            {
                //���ڵ�������
                switch ((cGlobalParas.PublishType)int.Parse(t.ExportType))
                {
                    case cGlobalParas.PublishType.PublishExcel :
                        this.raExportExcel.Checked = true;
                        break;
                    case cGlobalParas.PublishType.PublishTxt :

                        this.raExportTxt.Checked = true;
                        break;
                    case cGlobalParas.PublishType.PublishAccess:
                        this.raExportAccess.Checked = true;
                        break;
                    case cGlobalParas.PublishType.PublishMSSql :
                        this.raExportMSSQL.Checked = true;
                        break;
                    case cGlobalParas.PublishType .PublishMySql :
                        this.raExportMySql.Checked = true;
                        break;
                    case cGlobalParas.PublishType .PublishWeb :
                        this.raExportWeb.Checked = true;
                        break;
                    default :
                        break;
                }
                this.txtFileName.Text = t.ExportFile;
                this.txtDataSource.Text = t.DataSource;
                this.comTableName.Text = t.DataTableName;
                this.txtInsertSql.Text  = t.InsertSql;
                this.txtExportUrl.Text  = t.ExportUrl;
                if (t.ExportUrlCode == null || t.ExportUrlCode =="")
                    this.comExportUrlCode.SelectedIndex = 0;
                else
                    this.comExportUrlCode.SelectedItem = cGlobalParas.ConvertName(int.Parse(t.ExportUrlCode));
                this.txtExportCookie.Text  = t.ExportCookie;
            }
            else
            {
                //���ɼ�����
                this.groupBox4.Enabled = false;
                this.txtFileName.Text = "";
                this.comTableName.Text = "";

            }

            this.txtSavePath.Text = t.SavePath;
            this.udThread.Value = t.ThreadCount;
            this.txtStartPos.Text = t.StartPos;
            this.txtEndPos.Text = t.EndPos;
            this.txtWeblinkDemo.Text = t.DemoUrl;
            this.txtCookie.Text = t.Cookie;
            this.comWebCode.SelectedItem = cGlobalParas.ConvertName(int.Parse (t.WebCode));
            this.IsLogin.Checked = t.IsLogin;
            this.txtLoginUrl.Text = t.LoginUrl;
            this.IsUrlEncode.Checked = t.IsUrlEncode;
            if (t.UrlEncode == "")
            {
                this.comUrlEncode.SelectedIndex = -1;
            }
            else
            {
                this.comUrlEncode.SelectedItem = cGlobalParas.ConvertName(int.Parse(t.UrlEncode));
            }

            ListViewItem item;
            int i = 0;
            for (i = 0; i < t.WebpageLink.Count;  i++)
            {
                item=new ListViewItem ();
                item.Name =t.WebpageLink[i].id.ToString ();
                item.Text =t.WebpageLink[i].Weblink.ToString ();
                item.SubItems.Add(t.WebpageLink[i].NagRule);
                item.SubItems.Add((t.WebpageLink[i].IsOppPath == true) ? "��" : "��");
                item.SubItems.Add(t.WebpageLink[i].NextPageRule);
                item.SubItems.Add(gUrl.GetUrlCount(t.WebpageLink[i].Weblink.ToString ()).ToString ());
                this.listWeblink.Items.Add (item);
                item=null;
            }

            
            for (i = 0; i < t.WebpageCutFlag.Count ; i++)
            {
                item=new ListViewItem() ;
                item.Name =t.WebpageCutFlag[i].id.ToString ();
                item.Text =t.WebpageCutFlag[i].Title.ToString ();
                item.SubItems.Add(cGlobalParas.ConvertName (t.WebpageCutFlag[i].DataType) );
                item.SubItems.Add (t.WebpageCutFlag[i].StartPos.ToString ());
                item.SubItems.Add (t.WebpageCutFlag[i].EndPos .ToString ());
                item.SubItems.Add(cGlobalParas.ConvertName (t.WebpageCutFlag[i].LimitSign));

                item.SubItems.Add(t.WebpageCutFlag [i].RegionExpression );
                if ((int)t.WebpageCutFlag[i].ExportLimit == 0)
                    item.SubItems.Add("");
                else
                    item.SubItems.Add(cGlobalParas.ConvertName(t.WebpageCutFlag[i].ExportLimit));

                item.SubItems.Add(t.WebpageCutFlag [i].ExportExpression );
                
                this.listWebGetFlag.Items.Add(item);
                item=null;
            }

            t=null ;
            
        }

        #region ������ʼ������ ����������״̬���м��أ��½����޸ġ������
        
        private void IniData()
        {
            //��ʼ��ҳ���������

            this.TaskType.Items.Add("������ַ�ɼ���ҳ����");
            this.TaskType.Items.Add("�ɼ�ajax��ҳ����");
            this.TaskType.SelectedIndex = 0;

            this.comRunType.Items.Add("�ɼ�����������");
            this.comRunType.Items.Add("���ɼ�����");
            this.comRunType.SelectedIndex = 1;

            this.comLimit.Items.Add("���������ʽ������");
            this.comLimit.Items.Add("ƥ��ʱȥ����ҳ����");
            this.comLimit.Items.Add("ֻƥ������");
            this.comLimit.Items.Add("ֻƥ��˫�ֽ��ַ�");
            this.comLimit.Items.Add("ֻƥ������");
            this.comLimit.Items.Add("ֻƥ����ĸ���ּ������ַ�");
            this.comLimit.Items.Add("�Զ�������ƥ����ʽ");
            this.comLimit.SelectedIndex = 0;

            this.comExportLimit.Items.Add("�����������");
            this.comExportLimit.Items.Add("���ʱȥ����ҳ����");
            this.comExportLimit.Items.Add("���ʱ����ǰ׺");
            this.comExportLimit.Items.Add("���ʱ���Ӻ�׺");
            this.comExportLimit.Items.Add("����ȥ���ַ�");
            this.comExportLimit.Items.Add("����ȥ���ַ�");
            this.comExportLimit.Items.Add("�滻���з����������ַ�");
            this.comExportLimit.Items.Add("ȥ���ַ�����β�ո�");
            this.comExportLimit.Items.Add("���ʱ����������ʽ�����滻");
            this.comExportLimit.SelectedIndex = 0;

            this.comWebCode.Items.Add("�Զ�");
            this.comWebCode.Items.Add("gb2312");
            this.comWebCode.Items.Add("UTF-8");
            this.comWebCode.Items.Add("gbk");
            this.comWebCode.Items.Add("big5");

            this.comExportUrlCode.Items.Add("������");
            this.comExportUrlCode.Items.Add("gb2312");
            this.comExportUrlCode.Items.Add("UTF-8");
            this.comExportUrlCode.Items.Add("gbk");
            this.comExportUrlCode.Items.Add("big5");
            this.comExportUrlCode.SelectedIndex = 0;

            this.comWebCode.SelectedIndex = 0;

            this.comUrlEncode.Items.Add("UTF-8");
            this.comUrlEncode.Items.Add("gb2312");
            this.comUrlEncode.Items.Add("gbk");
            this.comUrlEncode.Items.Add("big5");

            this.comGetType.Items.Add("�ı�");
            this.comGetType.Items.Add("ͼƬ");
            this.comGetType.Items.Add("Flash");
            this.comGetType.Items.Add("�ļ�");
            this.comGetType.SelectedIndex =0;

            this.txtSavePath.Text =Program.getPrjPath() + "data";

            this.txtGetTitleName.Items.Add("���ӵ�ַ");
            this.txtGetTitleName.Items.Add("����");
            this.txtGetTitleName.Items.Add("����");
            this.txtGetTitleName.Items.Add("ͼƬ");
            

            //��ʼ��ҳ�����ʱ�����ؼ���״̬


            //��ʼ���������
            //��ʼ��ʼ�����νṹ,ȡxml�е�����,��ȡ�������
            this.comTaskClass.Items.Add("�����񲻷��ֱ࣬�ӱ��浽Ĭ��·��");
            Task.cTaskClass xmlTClass = new Task.cTaskClass();

            int TClassCount = xmlTClass.GetTaskClassCount();

            for (int i = 0; i < TClassCount; i++)
            {
                this.comTaskClass.Items.Add(xmlTClass.GetTaskClassName(i));
            }
            xmlTClass = null;

            this.comTaskClass.SelectedIndex = 0;
        }

        #endregion

        #region ��ť��������Ʋ���

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            if (IsSaveTask == true)
            {
                if (this.comTaskClass.SelectedIndex == 0)
                {
                    rTClass("");
                }
                else
                {
                    rTClass(this.comTaskClass.SelectedItem.ToString());
                }
            }

            this.Close();
        }

        private void cmdAddWeblink_Click(object sender, EventArgs e)
        {
            this.errorProvider1.Clear();
            int UrlCount = 0;

            if (this.txtWebLink.Text.ToString() == null || this.txtWebLink.Text.Trim().ToString() == "")
            {
                this.errorProvider1.SetError(this.txtWebLink, "��������ַ��Ϣ");
                return;
            }
            else
            {
                if (!Regex.IsMatch (this.txtWebLink.Text.Trim().ToString (),"http://",RegexOptions.IgnoreCase))
                {
                    this.errorProvider1.SetError(this.txtWebLink, "���������ַ���Ϸ�������");
                    return;
                }
            }

            ListViewItem litem;
            litem = new ListViewItem();
            litem.Text = this.txtWebLink.Text.ToString();
            if (this.checkBox2.Checked == true)
            {
                litem.SubItems.Add(this.txtNag.Text.ToString());
                if (this.checkBox1.Checked == true)
                {
                    litem.SubItems.Add("��");
                }
                else
                {
                    litem.SubItems.Add("��");
                }
            }
            else
            {
                litem.SubItems.Add("");
                litem.SubItems.Add("");
            }

           

            if (this.checkBox3.Checked == true)
            {
                litem.SubItems.Add(this.txtNextPage.Text.ToString());
            }
            else
            {
                litem.SubItems.Add("");
            }

            UrlCount = gUrl.GetUrlCount(this.txtWebLink.Text.ToString());

            litem.SubItems.Add(UrlCount.ToString());

            this.listWeblink.Items.Add(litem);
            litem = null;

            this.txtWebLink.Text = "http://";
            this.checkBox2.Checked = false;
            this.checkBox1.Checked = false;
            this.checkBox3.Checked = false;
            this.txtNag.Text = "";
            this.txtNextPage.Text = "";

            this.IsSave.Text = "true";
        }

        private string AddDemoUrl(string SourceUrl,bool IsNavPage,bool IsAPath,string APath)
        {
                string Url;
                List<string> Urls;

                
                Urls = gUrl.SplitWebUrl(SourceUrl);
               
                if (IsNavPage ==true )
                {

                    if (IsAPath ==true)
                        Url = GetUrl(SourceUrl, APath, true);
                    else
                        Url = GetUrl(SourceUrl, APath, false);

                }
                else
                {
                    Url = Urls[0].ToString();
                }

                Urls = null;
                return Url;

        }

        private void cmdGetCode_Click(object sender, EventArgs e)
        {

            //string Code = cTool.GetWebpageCode(this.txtWeblink.Text);
            //this.comCode.Text = Code;

            //wait.Dispose();

            //if (Code == "")
            //{
            //    MessageBox.Show("ϵͳ�޷��Զ���ȡ��ҳ���룬��ͨ���鿴ҳ�����ԣ�Firefox����鿴ҳ����루IE�����ж�ҳ������ʽ", "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //}
        }

        private void cmdDelWeblink_Click(object sender, EventArgs e)
        {
            if (this.listWeblink.SelectedItems.Count != 0)
            {
                this.listWeblink.Items.Remove(this.listWeblink.SelectedItems[0]);
            }

            if (this.listWeblink.Items.Count == 0)
            {
                this.txtWeblinkDemo.Text = "";
            }

            this.IsSave.Text = "true";

        }

        private void comRunType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.comRunType.SelectedIndex )
            {
                case 0:
                    this.groupBox4.Enabled = true;
                    this.groupBox9.Enabled =true ;
                    this.groupBox10.Enabled = true;
                    break;
                case 1:
                    this.groupBox4.Enabled = false;
                    this.groupBox9.Enabled = false;
                    this.groupBox10.Enabled = false;
                    break;
                default :
                    break;
            }
        }

        //���񱣴棬�����񱣴��ʱ�򣬲����������ģ����Ϣ�����������û��Զ���
        //�˹���ֻ���û��ڽ�������ʱ��һ�ֿ��ٲ���
        private bool SaveTask(string TaskPath)
        {

            Task.cTask t = new Task.cTask();

            //����Ǳ༭״̬������Ҫɾ��ԭ���ļ�
            if (this.FormState == cGlobalParas.FormState.Edit)
            {
                t.TaskName = this.tTask.Text;

                if (this.comTaskClass.SelectedIndex == 0)
                {
                    try
                    {
                        //ɾ��ԭ���������ҪĿ����Ϊ�˱��ݣ�������������������
                        t.DeleTask("", this.tTask.Text);
                    }
                    catch (System.Exception )
                    {
                    }
                }
                else
                {
                    //��ȡ����������·��
                    Task.cTaskClass tClass = new Task.cTaskClass();
                    string tPath=tClass.GetTaskClassPathByName(this.comTaskClass.SelectedItem.ToString());
                    try
                    {
                        //ɾ��ԭ���������ҪĿ����Ϊ�˱��ݣ�������������������
                        t.DeleTask(tPath, this.tTask.Text);
                    }
                    catch (System.Exception )
                    {

                    }
                }
            }

            int i = 0;
            int UrlCount = 0;

            

            //��������
            

            //�½�һ������
            t.New();

            t.TaskName = this.tTask.Text;
            t.TaskDemo = this.txtTaskDemo.Text;

            if (this.comTaskClass.SelectedIndex == 0)
            {
                t.TaskClass = "";
            }
            else
            {
                t.TaskClass = this.comTaskClass.SelectedItem.ToString ();
            }

            t.TaskType = cGlobalParas.ConvertID ( this.TaskType.SelectedItem.ToString ()).ToString ();
            t.RunType = cGlobalParas.ConvertID(this.comRunType.SelectedItem.ToString()).ToString();
            if (this.txtSavePath.Text.Trim().ToString() == "")
                t.SavePath = Program.getPrjPath() + "data";
            else
                t.SavePath = this.txtSavePath.Text;
            t.ThreadCount = int.Parse (this.udThread.Value.ToString ());
            t.StartPos = this.txtStartPos.Text;
            t.EndPos = this.txtEndPos.Text;
            t.DemoUrl = this.txtWeblinkDemo.Text;
            t.Cookie = this.txtCookie.Text;
            t.WebCode = cGlobalParas.ConvertID(this.comWebCode.SelectedItem.ToString()).ToString();
            t.IsLogin = this.IsLogin.Checked;
            t.LoginUrl = this.txtLoginUrl.Text;
            t.IsUrlEncode = this.IsUrlEncode.Checked;
            if (this.IsUrlEncode.Checked==false )
            {
                t.UrlEncode = "";
            }
            else
            {
                t.UrlEncode = cGlobalParas.ConvertID(this.comUrlEncode.SelectedItem.ToString()).ToString();
            }

            //�ж��Ƿ񵼳��ļ�
            if (this.comRunType.SelectedIndex == 0)
            {
               
                if (this.raExportTxt.Checked ==true )
                {
                    t.ExportType = ((int)cGlobalParas.PublishType.PublishTxt).ToString ();
                }
                else if (this.raExportExcel.Checked == true)
                {
                    t.ExportType = ((int)cGlobalParas.PublishType.PublishExcel ).ToString();
                }
                else if (this.raExportAccess.Checked == true)
                {
                    t.ExportType =((int) cGlobalParas.PublishType.PublishAccess).ToString () ;
                }
                else if (this.raExportMSSQL.Checked == true)
                {
                    t.ExportType = ((int)cGlobalParas.PublishType.PublishMSSql).ToString();
                }
                else if (this.raExportMySql.Checked == true)
                {
                    t.ExportType = ((int)cGlobalParas.PublishType.PublishMySql ).ToString();
                }
                else if (this.raExportWeb.Checked == true)
                {
                    t.ExportType = ((int)cGlobalParas.PublishType.PublishWeb ).ToString();
                }

                t.ExportFile = this.txtFileName.Text.ToString();
                t.DataSource = this.txtDataSource.Text.ToString();

                //���ݿ��û���������������1.2�汾���Ѿ�ɾ�������Բ����б��棬�����벻����ɾ������Ϊ
                //�˰汾��Ҫ����1.0����֤����Ŀɶ��ԣ��ʲ�ɾ��
                //t.DataUser =this.txtDataUser.Text.ToString ();
                //t.DataPwd =this.txtDataPwd.Text .ToString () ;

                t.DataTableName = this.comTableName.Text.ToString();
                t.InsertSql = this.txtInsertSql.Text;
                t.ExportUrl = this.txtExportUrl.Text;
                t.ExportUrlCode =cGlobalParas.ConvertID( this.comExportUrlCode.SelectedItem.ToString()).ToString ();
                t.ExportCookie = this.txtExportCookie.Text;
            }
            else
            {

                t.ExportFile = "";
                t.DataSource = "";

                t.ExportType = ((int)cGlobalParas.PublishType.NoPublish).ToString();
                t.DataSource = "";
                t.DataUser = "";
                t.DataPwd ="";
                t.DataTableName = "";
            }

            for (i = 0; i < this.listWeblink.Items.Count; i++)
            {
                UrlCount += int.Parse ( this.listWeblink.Items[i].SubItems[4].Text);
            }
            t.UrlCount = UrlCount;

            Task.cWebLink w;
            for (i = 0; i < this.listWeblink.Items.Count; i++)
            {
                w=new Task.cWebLink ();
                w.id = i;
                w.Weblink=this.listWeblink.Items[i].Text ;
                if (this.listWeblink.Items[i].SubItems[1].Text == "" || this.listWeblink.Items[i].SubItems[1].Text == null)
                {
                    w.IsNavigation = false;
                    w.IsOppPath = false;
                }
                else
                {
                    w.IsNavigation = true;
                    if (this.listWeblink.Items[i].SubItems[2].Text == "��")
                    {
                        w.IsOppPath = true;
                    }
                    else
                    {
                        w.IsOppPath = false;
                    }
                    w.NagRule = this.listWeblink.Items[i].SubItems[1].Text.ToString();
                }

                if (this.listWeblink.Items[i].SubItems[3].Text == "" || this.listWeblink.Items[i].SubItems[3].Text==null)
                {
                    w.IsNextpage = false;
                }
                else 
                {
                    w.IsNextpage = true;
                    w.NextPageRule = this.listWeblink.Items[i].SubItems[3].Text;
                }

                t.WebpageLink.Add (w);
                w=null;
            }

            Task.cWebpageCutFlag c;
            for (i = 0; i < this.listWebGetFlag.Items.Count; i++)
            {
                c = new Task.cWebpageCutFlag();
                c.id = i;
                c.Title = this.listWebGetFlag.Items[i].Text;
                c.DataType = cGlobalParas.ConvertID(this.listWebGetFlag.Items[i].SubItems[1].Text);
                c.StartPos = this.listWebGetFlag.Items[i].SubItems[2].Text;
                c.EndPos = this.listWebGetFlag.Items[i].SubItems[3].Text;
                c.LimitSign =cGlobalParas.ConvertID (this.listWebGetFlag.Items[i].SubItems[4].Text);
                
                try
                {
                    c.RegionExpression = this.listWebGetFlag.Items[i].SubItems[5].Text;
                    c.ExportLimit = cGlobalParas.ConvertID(this.listWebGetFlag.Items[i].SubItems[6].Text);
                    c.ExportExpression = this.listWebGetFlag.Items[i].SubItems[7].Text;
                }
                catch (System.Exception)
                {
                    //������󲻴�������1.0�汾
                }
                
                t.WebpageCutFlag.Add(c);
                c = null;

            }

            t.Save(TaskPath);
            t=null;

            return true;

        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (!CheckInputvalidity())
            {
                return ;
            }

            if (this.listWeblink.Items.Count == 0 || this.listWebGetFlag.Items.Count == 0)
            {
                if (MessageBox.Show("�ڴ�����û�ж�����Ҫ�ɼ�����ַ���߲ɼ���ַ�Ĺ����Ƿ������������", "soukey��ժ ϵͳѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }

            try
            {
                if (this.IsSave.Text == "true")
                {
                    if (!SaveTask(""))
                    {
                        return;
                    }
                }

                if (this.comTaskClass.SelectedIndex == 0)
                {
                    rTClass("");
                }
                else
                {
                    rTClass(this.comTaskClass.SelectedItem.ToString());
                }

                this.IsSave.Text = "false";

                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("���񱣴�ʱʧ�ܣ�����ԭ���ǣ�" + ex.Message, "Soukey��ժ ������Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        #endregion

        #region �������� ������
        //����û��������ݵ���Ч�ԣ�ֻҪ���������ƾͿ��Ա��棬����ʹ���Ѷ�
        //�����������һ������ִ�У���Ҫ��ִ��ǰ����һ���޸�
        private bool CheckInputvalidity()
        {
            this.errorProvider1.Clear();

            if (this.tTask.Text.ToString () == null || this.tTask.Text.Trim().ToString () == "")
            {
                this.errorProvider1.SetError(this.tTask, "�������Ʋ���Ϊ��!");
                return false ;
            }

            //if (this.TaskType.SelectedIndex ==1)
            //{
            //    //��ʾѡ���˸���������н���
            //    if (this.txtTaskTemp.Text.ToString() == null || this.txtTaskTemp.Text.ToString() == "")
            //    {
            //        this.errorProvider1.SetError(this.txtTaskTemp, "��ѡ����ͨ������ģ�彨���������뵼��������Ϣ");
            //        return;
            //    }
            //}

            //if (this.comRunType.SelectedIndex  == 0)
            //{
            //    //��ʾ�߲ɼ����ݱ�ֱ�ӵ�������
            //    if (this.txtFileName.Text.ToString() == null || this.txtFileName.Text.ToString() == "")
            //    {
            //        this.errorProvider1.SetError(this.txtFileName, "��ѡ������Ҫʵʱ�������ݣ����������뵼�����ݵ��ļ�����");
            //        return;
            //    }

            //}

            //if (this.listWeblink.Items.Count == 0)
            //{
            //    this.tabControl1.SelectedTab = this.tabControl1.TabPages[1];

            //    this.errorProvider1.SetError(this.listWeblink, "����Ҫ�ɼ�����ַ�����飡");
            //    return;
            //}

            //if (this.comWebCode.SelectedItem == null)
            //{
            //    this.tabControl1.SelectedTab = this.tabControl1.TabPages[1];

            //    this.errorProvider1.SetError(this.comWebCode, "��ҳ���벻��Ϊ�գ����Ϊ������ܵ����������");
            //    return;
            //}

            //if (this.listWebGetFlag.Items.Count == 0)
            //{
            //    this.tabControl1.SelectedTab = this.tabControl1.TabPages[2];
            //    this.errorProvider1.SetError(this.listWebGetFlag, "����Ҫ�ɼ������ݱ�ǩ������");
            //    return;
            //}
            return true;
        }

        #endregion

        #region ���ݸ�ί�еķ���
        //private void GetTaskID(int TaskID,string TaskName)
        //{
        //    this.txtTaskTemp.Tag  = TaskID.ToString();
        //    this.txtTaskTemp.Text = TaskName;

        //}

        private void GetDataSource(string strDataConn)
        {
            this.txtDataSource.Text = strDataConn;
        }

        private void GetUrl(string Url, int UrlCount)
        {
            this.txtWebLink.Text = Url;
            this.txtWebLink.Tag = UrlCount;
        }

        private void GetCookie(string strCookie)
        {
            this.txtCookie.Text = strCookie;
        }

        private void GetExportCookie(string strCookie)
        {
            this.txtExportCookie.Text = strCookie;
        }

        private void GetPData(string strCookie, string pData)
        {
            this.txtCookie.Text = strCookie;
            this.txtWebLink.Text += "<POST>" + pData + "</POST>";
        }

        private void GetExportpData(string strCookie, string pData)
        {
            this.txtExportUrl.Text += "<POST>" + pData + "</POST>";
        }

        #endregion

        private void listWeblink_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                this.listWeblink.Items.Remove(this.listWeblink.SelectedItems[0]);
                if (this.listWeblink.Items.Count == 0)
                {
                    this.txtWeblinkDemo.Text = "";
                }

                this.IsSave.Text = "true";
            }
        }

       
        private void listWebGetFlag_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.listWebGetFlag.Items.Count  == 0)
            {
                return;
            }

            if (e.KeyCode == Keys.Delete)
            {
                this.listWebGetFlag.Items.Remove(this.listWebGetFlag.SelectedItems[0]);

                this.IsSave.Text = "true";
            }
        }

        private void frmTask_Load(object sender, EventArgs e)
        {
            //��Tooltip���г�ʼ������
            HelpTip.ToolTipIcon = ToolTipIcon.Info;
            HelpTip.ForeColor =Color.YellowGreen;
            HelpTip.BackColor = Color.LightGray;
            HelpTip.AutoPopDelay = 5000;
            HelpTip.ShowAlways = true;
            HelpTip.ToolTipTitle = "";

            SetTooltip();

            //��ʼ�����������datagrid�ı�ͷ
            DataGridViewTextBoxColumn nRuleLevel = new DataGridViewTextBoxColumn();
            nRuleLevel.HeaderText = "����";
            nRuleLevel.Width = 40;
            this.dataNRule.Columns.Insert(0, nRuleLevel);

            DataGridViewTextBoxColumn nRule = new DataGridViewTextBoxColumn();
            nRule.HeaderText = "��������";
            nRule.Width = 170;
            this.dataNRule.Columns.Insert(1, nRule);

            DataGridViewTextBoxColumn nOUrl = new DataGridViewTextBoxColumn();
            nOUrl.HeaderText = "��Ե�ַ";
            nOUrl.Width =70;
            this.dataNRule.Columns.Insert(2, nOUrl);

            switch (this.FormState)
            {
                case cGlobalParas.FormState.New :
                    break;
                case cGlobalParas.FormState.Edit :
                    //�༭״̬���������޸ķ���

                    this.tTask.ReadOnly = true;
                    this.comTaskClass.Enabled = false;

                    break;
                case cGlobalParas.FormState.Browser :
                    SetFormBrowser();
                    break;
                default :
                    break ;
            }

            this.IsSave.Text = "false";
        }

        private void SetFormBrowser()
        {
            this.cmdOpenFolder.Enabled = false;
            this.button10.Enabled = false;
            this.cmdBrowser.Enabled = false;
            this.button2.Enabled = false;
            this.button3.Enabled = false;
            this.button4.Enabled = false;
            this.button6.Enabled = false;
            this.cmdAddWeblink.Enabled = false;
            this.cmdEditWeblink.Enabled = false;
            this.cmdDelWeblink.Enabled = false;

            this.cmdAddCutFlag.Enabled = false;
            this.button8.Enabled = false;
            this.cmdDelCutFlag.Enabled = false;

            this.button7.Enabled = false;

            this.button1.Enabled = false;

            this.cmdCancel.Text = "�� ��";

            this.cmdOK.Enabled =false ;

            this.cmdApply.Enabled = false;
        }

      
        private void GatherData()
        {


            if (this.listWeblink.Items.Count == 0 )
            {
                MessageBox.Show("�ڴ�����û�ж�����Ҫ�ɼ�����ַ���޷����вɼ����Թ�����", "soukey��ժ ϵͳ��Ϣ", MessageBoxButtons.OK , MessageBoxIcon.Information );
                this.tabControl1.SelectedTab = this.tabControl1.TabPages[1];
                return;
            }

            if (this.listWebGetFlag.Items.Count == 0)
            {
                MessageBox.Show("�ڴ�����û�ж�����Ҫ�ɼ����ݵĲɼ������޷����вɼ����Թ�����", "soukey��ժ ϵͳ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.tabControl1.SelectedTab = this.tabControl1.TabPages[2];
                return;
            }

            //���Բɼ��������û���������ݲ��Բɼ�
            //��֤�����Ƿ���ȷ
            //�Ȳ��������п�������һ���е���һ��ҳ��������

            //�ж��Ƿ��Ѿ���ȡ��ʾ����ַ�����û�У��������ȡ
            if (this.txtWeblinkDemo.Text.ToString() == null || this.txtWeblinkDemo.Text.ToString() == "")
            {
                GetDemoUrl();
            }
                       
            this.tabControl1.SelectedTab = this.tabControl1.TabPages[4];
            this.labWaiting.Visible = true;

            Application.DoEvents();

            Gather.cGatherWeb gData = new Gather.cGatherWeb();

            //���Ӳɼ��ı�־
            Task.cWebpageCutFlag c;

            for (int i = 0; i < this.listWebGetFlag.Items.Count; i++)
            {
                c = new Task.cWebpageCutFlag();
                c.id = i;
                c.Title = this.listWebGetFlag.Items[i].Text;
                c.DataType = cGlobalParas.ConvertID ( this.listWebGetFlag.Items[i].SubItems[1].Text);
                c.StartPos = this.listWebGetFlag.Items[i].SubItems[2].Text;
                c.EndPos = this.listWebGetFlag.Items[i].SubItems[3].Text;
                c.LimitSign =cGlobalParas.ConvertID ( this.listWebGetFlag.Items[i].SubItems[4].Text);
                c.RegionExpression = this.listWebGetFlag.Items[i].SubItems[5].Text;
                c.ExportLimit = cGlobalParas.ConvertID(this.listWebGetFlag.Items[i].SubItems[6].Text);
                c.ExportExpression = this.listWebGetFlag.Items[i].SubItems[7].Text;
                gData.CutFlag.Add(c);
                c = null;
            }

            try
            {
                string tmpSavePath = this.txtSavePath.Text.ToString() + "\\" + this.tTask.Text.ToString() + "_file";

                bool IsAjax = false;

                if (cGlobalParas.ConvertID(this.TaskType.SelectedItem.ToString()) == (int)cGlobalParas.TaskType.AjaxHtmlByUrl)
                    IsAjax = true;

                DataTable dGather = gData.GetGatherData(this.txtWeblinkDemo.Text.ToString(), (cGlobalParas.WebCode)cGlobalParas.ConvertID(this.comWebCode.SelectedItem.ToString()), this.txtCookie.Text.ToString(), this.txtStartPos.Text.ToString(), this.txtEndPos.Text.ToString(), tmpSavePath,IsAjax );

                //�󶨵���ʾ��DataGrid��
                this.dataTestGather.DataSource = dGather;

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("�ɼ��������󣬴�����Ϣ��" + ex.Message, "������Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            this.labWaiting.Visible = false;

        }

      
        private void button2_Click(object sender, EventArgs e)
        {
            this.contextMenuStrip1.Show(this.button2,0,21);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            frmDict dfrm = new frmDict();
            dfrm.ShowDialog();
            dfrm.Dispose();

        }

        private void menuOpenDict_Click(object sender, EventArgs e)
        {
            frmDict d = new frmDict();
            d.ShowDialog();
            d.Dispose();
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "�ֹ�����POST����")
            {
                frmWeblink wftm = new frmWeblink();
                wftm.getFlag = 1;
                wftm.rPData  = new frmWeblink.ReturnPOST (GetPData);
                wftm.ShowDialog();
                wftm.Dispose();

                return;
            }

            Match s;

            if (Regex.IsMatch(e.ClickedItem.ToString(), "[{].*[}]"))
            {
                s = Regex.Match(e.ClickedItem.ToString(), "[{].*[}]");
            }
            else
            {
                s = Regex.Match(e.ClickedItem.ToString(), "[<].*[>]");
            }

            int startPos = this.txtWebLink.SelectionStart;
            int l = this.txtWebLink.SelectionLength;

            this.txtWebLink.Text = this.txtWebLink.Text.Substring(0, startPos) + s.Groups[0].Value + this.txtWebLink.Text.Substring(startPos + l , this.txtWebLink.Text.Length - startPos - l);

            this.txtWebLink.SelectionStart = startPos + s.Groups[0].Value.Length;
            this.txtWebLink.ScrollToCaret();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox2.Checked == true)
            {
                //this.label10.Enabled = true;
                //this.txtNag.Enabled = true;
                //this.checkBox1.Enabled = true;
                //this.cmdAddNRule.Enabled = true;
                //this.cmdDelNRule.Enabled = true;
                //this.dataNRule.Enabled = true;
                this.groupBox14.Enabled = true;
            }
            else
            {
                //this.label10.Enabled = false;
                //this.txtNag.Enabled = false;
                //this.checkBox1.Enabled = false;
                //this.cmdAddNRule.Enabled = false;
                //this.cmdDelNRule.Enabled = false;
                //this.dataNRule.Enabled = false;
                this.groupBox14.Enabled = false;
            }

            this.IsSave.Text = "true";
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if(this.checkBox3.Checked ==true )
            {
                this.label13.Enabled = true;
                this.txtNextPage.Enabled =true ;
                this.txtNextPage.Text = "��һҳ";
            }
            else
            {
                if (this.txtNextPage.Text == "��һҳ")
                {
                    this.txtNextPage.Text = "";
                }
                this.label13.Enabled = false;
                this.txtNextPage.Enabled =false ;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GatherData();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.txtNag.Text.Trim() == "")
            {
                MessageBox.Show("��������Ϊ�գ��޷����ԣ�", "soukey��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string Url = GetUrl(this.txtWebLink.Text, this.txtNag.Text, this.checkBox1.Checked);

            if (!Regex.IsMatch(Url, @"(http|https|ftp)+://[^\s]*"))
            {
                MessageBox.Show("��ַ�޷��򿪣����ܳ���������ַ����������", "soukey��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return ;
            }

            System.Diagnostics.Process.Start( Url);

        }

        private string  GetUrl(string webLink,string NavRule,bool IsBool)
        {
            List<string> Urls;


            Urls = gUrl.ParseUrlRule(webLink, NavRule);
           

            if (Urls == null || Urls.Count ==0)
                return "";

            string isReg="[\"\\s]";
            string Url="";

            for (int m=0 ;m<Urls.Count ;m++)
            {
                if (!Regex.IsMatch (Urls[m].ToString (),isReg ))
                {
                    Url = Urls[m].ToString();
                    break ;
                }
            }
             

            string PreUrl = "";

            //��Ҫ�ж���ҳ��ַǰ����ڵ����Ż�˫����
            if (Url.Substring(0, 1) == "'" || Url.Substring(0, 1) == "\"")
            {
                Url = Url.Substring(1, Url.Length - 1);
            }

            if (Url.Substring(Url.Length - 1, 1) == "'" || Url.Substring(Url.Length - 1, 1) == "\"")
            {
                Url = Url.Substring(0, Url.Length - 1);
            }

            if (IsBool== true)
            {
                if (Url.Substring(0, 1) == "/")
                {
                    PreUrl = webLink.Substring(7, webLink.Length  - 7);
                    PreUrl = PreUrl.Substring(0, PreUrl.IndexOf("/"));
                    PreUrl = "http://" + PreUrl;
                }
                else
                {
                    Match aa = Regex.Match(webLink, ".*/");
                    PreUrl = aa.Groups[0].Value.ToString();
                }
            }

            return PreUrl + Url;

        }

        private void cmdWebSource_Click(object sender, EventArgs e)
        {
            if (this.txtWeblinkDemo.Text.Trim().ToString() == "")
            {
                MessageBox.Show("�����Ȼ�ȡʾ����ַ���ٽ���Դ����鿴��", "Soukey��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                this.errorProvider1.Clear();
                this.errorProvider1.SetError(this.txtWeblinkDemo, "��������ַ��Ϣ");

                return;
            }

            string tmpPath = Path.GetTempPath();

            try
            {
                //��ȡԴ����Ҫ����cGatherWeb�еķ������У���Ϊ�п���Url�а���
                //POST����
                Gather.cGatherWeb cg = new SoukeyNetget.Gather.cGatherWeb();

                bool IsAjax = false;

                if (cGlobalParas.ConvertID (this.TaskType.SelectedItem.ToString ())==(int)cGlobalParas.TaskType.AjaxHtmlByUrl )
                    IsAjax =true ;

                string WebSource = cg.GetHtml(this.txtWeblinkDemo.Text, (cGlobalParas.WebCode)cGlobalParas.ConvertID(this.comWebCode.Text), this.txtCookie.Text, "", "", false,IsAjax);
                cg = null;

                //������ʱ�ļ�
                string m_FileName = "~" + DateTime.Now.ToFileTime().ToString() + ".txt";
                m_FileName = tmpPath + "\\" + m_FileName;
                FileStream myStream = File.Open(m_FileName, FileMode.Create, FileAccess.Write, FileShare.Write);
                StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding("gb2312"));
                sw.Write(WebSource);
                sw.Close();
                myStream.Close();

                System.Diagnostics.Process.Start(m_FileName);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("��ȡ��ҳԴ�������������ϢΪ��" + ex.Message, "Soukey��ժ ����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdOKRun_Click(object sender, EventArgs e)
        {

        }

        private void GetDemoUrl()
        {
            if (this.listWeblink.Items.Count >= 1)
            {
                bool IsNav;
                bool IsAPath;
                string APath = "";
                string DemoUrl = "";

                if (this.listWeblink.Items[0].SubItems[1].Text == "")
                {
                    IsNav = false;
                }
                else
                {
                    IsNav = true;
                    APath = this.listWeblink.Items[0].SubItems[1].Text;
                }

                if (this.listWeblink.Items[0].SubItems[2].Text == "��")
                {
                    IsAPath = true;
                }
                else
                {
                    IsAPath = false;
                }

                DemoUrl = AddDemoUrl(this.listWeblink.Items[0].Text.ToString(), IsNav, IsAPath, APath);

                //�����жϵ�ǰ�Ƿ���ҪUrl���б���
                if (this.IsUrlEncode.Checked == true)
                {
                    this.txtWeblinkDemo.Text = cTool.UrlEncode(DemoUrl, (cGlobalParas.WebCode)(cGlobalParas.ConvertID(this.comUrlEncode.SelectedItem.ToString())));
                }
                else
                {
                    this.txtWeblinkDemo.Text = DemoUrl;
                }
            }
        }

        private void ConnectAccess()
        {
            string connectionstring = "provider=microsoft.jet.oledb.4.0;data source=";
            connectionstring += this.txtFileName.Text;
            //if (this.txtDataUser.Text.Trim() != "")
            //{
            //    connectionstring += "UID=" + this.txtDataUser.Text;

            //}
            //OleDbConnection con = new OleDbConnection(connectionstring);
            //con.Open();
            //con.Close ();
            //MessageBox.Show("���ݿ����ӳɹ���", "soukey��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ConnectSqlServer()
        {
            //string strDataBase = "Server=.;DataBase=Library;Uid=" + this.txtDataUser.Text.Trim() + ";pwd=" + this.txtDataPwd.Text + ";";
            //SqlConnection conn = new SqlConnection(strDataBase);
            //conn.Open();
            //conn.Close();
            //MessageBox.Show("���ݿ����ӳɹ���", "soukey��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            frmWeblink wftm = new frmWeblink();
            wftm.getFlag = 0;
            wftm.rCookie = new frmWeblink.ReturnCookie(GetCookie);
            wftm.ShowDialog();
            wftm.Dispose();

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            GatherData();
        }

        private void IsLogin_CheckedChanged(object sender, EventArgs e)
        {
            if (this.IsLogin.Checked == true)
            {
                this.txtLoginUrl.Enabled = true;
            }
            else
            {
                this.txtLoginUrl.Enabled = false;
            }

            this.IsSave.Text = "true";

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            this.contextMenuStrip1.Items.Clear();

            this.contextMenuStrip1.Items.Add("��������{Num:1,100,1}");
            this.contextMenuStrip1.Items.Add("�ݼ�����{Num:100,1,-1}");
            this.contextMenuStrip1.Items.Add(new ToolStripSeparator());
            this.contextMenuStrip1.Items.Add("��ĸ����{Letter:a,z}");
            this.contextMenuStrip1.Items.Add("��ĸ�ݼ�{Letter:z,a}");
            this.contextMenuStrip1.Items.Add(new ToolStripSeparator());
            this.contextMenuStrip1.Items.Add("POSTǰ׺<POST>");
            this.contextMenuStrip1.Items.Add("POST��׺</POST>");
            this.contextMenuStrip1.Items.Add("�ֹ�����POST����");
            this.contextMenuStrip1.Items.Add(new ToolStripSeparator());

            //��ʼ���ֵ�˵�����Ŀ
            cDict d = new cDict();
            int count = d.GetDictClassCount();
            

            for (int i = 0; i < count; i++)
            {
                this.contextMenuStrip1.Items.Add("�ֵ�:{Dict:" + d.GetDictClassName(i).ToString() + "}");
            }

        }

        private void IsUrlEncode_CheckedChanged(object sender, EventArgs e)
        {
            if (this.IsUrlEncode.Checked == true)
            {
                this.comUrlEncode.Enabled = true;
                this.comUrlEncode.SelectedIndex = 0;
            }
            else
            {
                this.comUrlEncode.Enabled = false;
                this.comUrlEncode.SelectedIndex= - 1;
            }

            this.IsSave.Text = "true";
        }

        private void listWebGetFlag_Click(object sender, EventArgs e)
        {
            if (this.listWebGetFlag.SelectedItems.Count != 0)
            {
                this.txtGetTitleName.Text = this.listWebGetFlag.SelectedItems[0].Text;
                this.comGetType.Text = this.listWebGetFlag.SelectedItems[0].SubItems[1].Text;
                this.txtGetStart.Text = this.listWebGetFlag.SelectedItems[0].SubItems[2].Text;
                this.txtGetEnd.Text = this.listWebGetFlag.SelectedItems[0].SubItems[3].Text;
                this.comLimit.SelectedItem = this.listWebGetFlag.SelectedItems[0].SubItems[4].Text;
                try
                {
                    this.txtRegion.Text = this.listWebGetFlag.SelectedItems[0].SubItems[5].Text;
                    this.comExportLimit.SelectedItem = this.listWebGetFlag.SelectedItems[0].SubItems[6].Text;
                    this.txtExpression.Text = this.listWebGetFlag.SelectedItems[0].SubItems[7].Text;
                }
                catch (System.Exception)
                {
                    //�������������Ϊ���Ǽ���1.0�汾��������Ϣ
                }

                this.IsSave.Text = "true";
            }
        }

        private void listWeblink_Click(object sender, EventArgs e)
        {

            this.txtWebLink.Text = this.listWeblink.SelectedItems[0].Text;
            if (this.listWeblink.SelectedItems[0].SubItems[1].Text == "" || this.listWeblink.SelectedItems[0].SubItems[1].Text == null)
            {
                this.checkBox2.Checked = false;
            }
            else
            {
                this.checkBox2.Checked = true;
                this.txtNag.Text = this.listWeblink.SelectedItems[0].SubItems[1].Text;
                if (this.listWeblink.SelectedItems[0].SubItems[2].Text == "��")
                {
                    this.checkBox1.Checked = true;
                }
                else
                {
                    this.checkBox1.Checked = false;
                }
               
            }

            if (this.listWeblink.SelectedItems[0].SubItems[3].Text == "")
            {
                this.checkBox3.Checked = false;
                this.txtNextPage.Text = "";
            }
            else
            {
                this.checkBox3.Checked = true;
                this.txtNextPage.Text = this.listWeblink.SelectedItems[0].SubItems[3].Text;
            }

        }

        private void cmdEditWeblink_Click(object sender, EventArgs e)
        {
            if (this.listWeblink.SelectedItems.Count == 0)
            {
                return;
            }

            this.errorProvider1.Clear();
            int UrlCount = 0;

            if (this.txtWebLink.Text.ToString() == null || this.txtWebLink.Text.Trim().ToString() == "")
            {
                this.errorProvider1.SetError(this.txtWebLink, "��������ַ��Ϣ");
                return;
            }
            else
            {
                if (!Regex.IsMatch(this.txtWebLink.Text.Trim().ToString(), "http://", RegexOptions.IgnoreCase))
                {
                    this.errorProvider1.SetError(this.txtWebLink, "���������ַ���Ϸ�������");
                    return;
                }
            }

            this.listWeblink.SelectedItems[0].Text = this.txtWebLink.Text.ToString();
            if (this.checkBox2.Checked == true)
            {
                this.listWeblink.SelectedItems[0].SubItems [1].Text =this.txtNag.Text.ToString();
                if (this.checkBox1.Checked == true)
                {
                    this.listWeblink.SelectedItems[0].SubItems[2].Text ="��";
                }
                else
                {
                    this.listWeblink.SelectedItems[0].SubItems[2].Text = "��";
                }
            }
            else
            {
                this.listWeblink.SelectedItems[0].SubItems[1].Text = "";
                this.listWeblink.SelectedItems[0].SubItems[2].Text = "";
            }



            if (this.checkBox3.Checked == true)
            {
                this.listWeblink.SelectedItems[0].SubItems[3].Text=this.txtNextPage.Text.ToString();
            }
            else
            {
                this.listWeblink.SelectedItems[0].SubItems[3].Text="";
            }

            UrlCount = gUrl.GetUrlCount(this.txtWebLink.Text.ToString());
            this.listWeblink.SelectedItems[0].SubItems[4].Text = UrlCount.ToString ();

            this.txtWebLink.Text = "http://";
            this.checkBox2.Checked = false;
            this.checkBox1.Checked = false;
            this.checkBox3.Checked = false;
            this.txtNag.Text = "";
            this.txtNextPage.Text = "";

            this.IsSave.Text = "true";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(this.txtWebLink.Text, @"(http|https|ftp)+://[^\s]*"))
            {
                MessageBox.Show("��ַ�޷��򿪣����ܳ���������ַ����������", "soukey��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            string Url=AddDemoUrl (this.txtWebLink.Text,false,false,"" );
            GetNextPageFlag(Url);

        }
        
        //�Զ���ȡ��һҳ�ı�ʶ
        private string GetNextPageFlag(string Url)
        {
            string webCode = cTool.GetHtmlSource(Url, true);
            return "";
        }

        private void comGetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comGetType.SelectedIndex == 0)
            {
                this.comLimit.Enabled = true;
                this.comLimit.SelectedIndex = 0;

                this.comExportLimit.Enabled = true;
                this.comExportLimit.SelectedIndex = 0;
            }
            else
            {
                this.comLimit.SelectedIndex = -1;
                this.comLimit.Enabled = false;

                this.comExportLimit.SelectedIndex = -1;
                this.comExportLimit.Enabled = false;

                this.txtExpression.Text = "";
                this.txtExpression.Enabled = false;
            }
        }

        private void cmdAddCutFlag_Click(object sender, EventArgs e)
        {
            this.errorProvider1.Clear();

            if (this.txtGetTitleName.Text.Trim().ToString() == "")
            {
                this.errorProvider1.SetError(this.txtGetTitleName, "������ɼ����ݵ�����");
                return;
            }

            if (this.txtGetStart.Text.Trim().ToString() == "")
            {
                this.errorProvider1.SetError(this.txtGetStart, "������ɼ����ݵ���ʼ��־");
                return;

            }

            if (this.txtGetEnd.Text.Trim().ToString() == "")
            {
                this.errorProvider1.SetError(this.txtGetEnd, "������ɼ����ݵĽ�����־");
                return;
            }

            if (this.comLimit.SelectedIndex == -1)
            {
                this.comLimit.SelectedIndex = 0;
            }

            //�ж������Ƿ��Ѿ��ظ�
            for (int i = 0; i < this.listWebGetFlag.Items.Count; i++)
            {
                if (this.listWebGetFlag.Items[i].Text == this.txtGetTitleName.Text)
                {
                    this.errorProvider1.Clear();
                    this.errorProvider1.SetError(this.txtGetEnd, "�ɼ������Ʋ����ظ�������������");
                    return;
                }
            }

            ListViewItem item = new ListViewItem();
            item.Text = this.txtGetTitleName.Text.ToString();
            item.SubItems.Add(this.comGetType.SelectedItem.ToString());
            item.SubItems.Add(cTool.ClearFlag(this.txtGetStart.Text.ToString()));
            item.SubItems.Add(cTool.ClearFlag(this.txtGetEnd.Text.ToString()));
            item.SubItems.Add(this.comLimit.SelectedItem.ToString());
            item.SubItems.Add(this.txtRegion.Text.ToString());
            item.SubItems.Add(this.comExportLimit.SelectedItem.ToString () );
            item.SubItems.Add(this.txtExpression.Text .ToString ());
            this.listWebGetFlag.Items.Add(item);
            item = null;

            this.txtGetTitleName.Text = "";
            this.txtGetStart.Text = "";
            this.txtGetEnd.Text = "";
            this.comLimit.SelectedIndex =0;
            this.txtRegion.Text = "";
            this.comExportLimit.SelectedIndex =0;
            this.txtExpression.Text = "";

            this.IsSave.Text = "true";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (this.listWebGetFlag.SelectedItems.Count == 0)
            {
                return;
            }

            this.errorProvider1.Clear();

            if (this.txtGetTitleName.Text.Trim().ToString() == "")
            {
                this.errorProvider1.SetError(this.txtGetTitleName, "������ɼ����ݵ�����");
                return;
            }

            if (this.txtGetStart.Text.Trim().ToString() == "")
            {
                this.errorProvider1.SetError(this.txtGetStart, "������ɼ����ݵ���ʼ��־");
                return;

            }

            if (this.txtGetEnd.Text.Trim().ToString() == "")
            {
                this.errorProvider1.SetError(this.txtGetEnd, "������ɼ����ݵĽ�����־");
                return;
            }

            this.listWebGetFlag.SelectedItems[0].Text = this.txtGetTitleName.Text.ToString();
            this.listWebGetFlag.SelectedItems[0].SubItems[1].Text = this.comGetType.SelectedItem.ToString();
            this.listWebGetFlag.SelectedItems[0].SubItems[2].Text = cTool.ClearFlag(this.txtGetStart.Text.ToString());
            this.listWebGetFlag.SelectedItems[0].SubItems[3].Text = cTool.ClearFlag(this.txtGetEnd.Text.ToString());
            if (this.comLimit.SelectedIndex == -1)
            {
                this.listWebGetFlag.SelectedItems[0].SubItems[4].Text = this.comLimit.Items[0].ToString();
            }
            else
            {
                this.listWebGetFlag.SelectedItems[0].SubItems[4].Text = this.comLimit.SelectedItem.ToString();
            }

            this.listWebGetFlag.SelectedItems[0].SubItems[5].Text=this.txtRegion.Text.ToString();
            this.listWebGetFlag.SelectedItems[0].SubItems[6].Text = this.comExportLimit.SelectedItem.ToString();
            this.listWebGetFlag.SelectedItems[0].SubItems[7].Text = this.txtExpression.Text.ToString();


            this.txtGetTitleName.Text = "";
            this.txtGetStart.Text = "";
            this.txtGetEnd.Text = "";
            this.comLimit.SelectedIndex =0;
            this.txtRegion.Text = "";
            this.comExportLimit.SelectedIndex = 0;
            this.txtExpression.Text = "";

            this.IsSave.Text = "true";
        }

        private void cmdDelCutFlag_Click(object sender, EventArgs e)
        {
            if (this.listWebGetFlag.SelectedItems.Count != 0)
            {
                this.listWebGetFlag.Items.Remove(this.listWebGetFlag.SelectedItems[0]);

                this.IsSave.Text = "true";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GatherData();
        }

        private void cmdOpenFolder_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.Description = "��ѡ��ɼ��������ݴ洢��·����" ;
            this.folderBrowserDialog1.SelectedPath = Program.getPrjPath();
            if (this.folderBrowserDialog1.ShowDialog()==DialogResult.OK)
            {
                this.txtSavePath.Text = this.folderBrowserDialog1.SelectedPath;
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            GetDemoUrl();
        }

        private void listWebGetFlag_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listWebGetFlag.SelectedItems.Count != 0)
            {
                this.txtGetTitleName.Text = this.listWebGetFlag.SelectedItems[0].Text;
                this.comGetType.Text = this.listWebGetFlag.SelectedItems[0].SubItems[1].Text;
                this.txtGetStart.Text = this.listWebGetFlag.SelectedItems[0].SubItems[2].Text;
                this.txtGetEnd.Text = this.listWebGetFlag.SelectedItems[0].SubItems[3].Text;
                this.comLimit.SelectedItem = this.listWebGetFlag.SelectedItems[0].SubItems[4].Text;
                try
                {
                    this.txtRegion.Text = this.listWebGetFlag.SelectedItems[0].SubItems[5].Text;
                    this.comExportLimit.SelectedItem = this.listWebGetFlag.SelectedItems[0].SubItems[6].Text;
                    this.txtExpression.Text = this.listWebGetFlag.SelectedItems[0].SubItems[7].Text;
                }
                catch (System.Exception)
                {
                    //�������������Ϊ���Ǽ���1.0�汾��������Ϣ
                }

                this.IsSave.Text = "true";
            }
        }

        private void frmTask_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_FormState == cGlobalParas.FormState.Browser)
                return;

            if (this.IsSave.Text == "true")
            {
                if (MessageBox.Show("������Ϣ�Ѿ��������޸ģ��������˳���", "Soukey��ժ ��Ϣ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    e.Cancel = true;
                    return;
            }
        }

        #region �����޸ı�����
        private void tTask_TextChanged(object sender, EventArgs e)
        {
            this.IsSave.Text = "true";
        }

        private void txtTaskDemo_TextChanged(object sender, EventArgs e)
        {
            this.IsSave.Text = "true";
        }

        private void comTaskClass_TextChanged(object sender, EventArgs e)
        {
            this.IsSave.Text = "true";
        }

        private void TaskType_TextChanged(object sender, EventArgs e)
        {
            this.IsSave.Text = "true";
        }

        private void comRunType_TextChanged(object sender, EventArgs e)
        {
            this.IsSave.Text = "true";
        }

        private void udThread_ValueChanged(object sender, EventArgs e)
        {
            this.IsSave.Text = "true";
        }

        private void txtSavePath_TextChanged(object sender, EventArgs e)
        {
            this.IsSave.Text = "true";
        }

        private void comWebCode_TextChanged(object sender, EventArgs e)
        {
            this.IsSave.Text = "true";
        }

        private void txtCookie_TextChanged(object sender, EventArgs e)
        {
            this.IsSave.Text = "true";
        }

        private void txtLoginUrl_TextChanged(object sender, EventArgs e)
        {
            this.IsSave.Text = "true";
        }

        private void DataSource_TextChanged(object sender, EventArgs e)
        {
            this.IsSave.Text = "true";
        }

        private void txtDataPwd_TextChanged(object sender, EventArgs e)
        {
            this.IsSave.Text = "true";
        }

        private void txtTableName_TextChanged(object sender, EventArgs e)
        {
            this.IsSave.Text = "true";
        }

        private void comUrlEncode_TextChanged(object sender, EventArgs e)
        {
            this.IsSave.Text = "true";
        }

        private void txtStartPos_TextChanged(object sender, EventArgs e)
        {
            this.IsSave.Text = "true";
        }

        private void txtEndPos_TextChanged(object sender, EventArgs e)
        {
            this.IsSave.Text = "true";
        }

        #endregion

        private void comLimit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comLimit.SelectedIndex == 6)
                this.txtRegion.Enabled = true;
            else
                this.txtRegion.Enabled = false;

        }

        private void comExportLimit_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.comExportLimit.SelectedIndex)
            {
                case 0:
                    this.label37.Text = "�ӹ�������";
                    this.txtExpression.Text = "";
                    this.txtExpression.Enabled = false;
                    break;
                case 1:
                    this.label37.Text = "�ӹ�������";
                    this.txtExpression.Text = "";
                    this.txtExpression.Enabled = false;
                    break;
                case 2:
                    this.label37.Text = "ǰ׺��";
                    this.txtExpression.Text = "";
                    this.txtExpression.Enabled = true;
                    break;
                case 3:
                    this.label37.Text = "��׺��";
                    this.txtExpression.Text = "";
                    this.txtExpression.Enabled = true;
                    break;
                case 4:
                    this.label37.Text = "��ȡ�ַ�����";
                    this.txtExpression.Text = "0";
                    this.txtExpression.Enabled = true;
                    break;
                case 5:
                    this.label37.Text = "��ȡ�ַ�����";
                    this.txtExpression.Text = "0";
                    this.txtExpression.Enabled = true;
                    break;
                case 6:
                    this.label37.Text = "���ʽ��";
                    this.txtExpression.Text = "\"\",\"\"";
                    this.txtExpression.Enabled = true;
                    break;
                case 7:
                    this.txtExpression.Enabled = false;
                    break;
                case 8:
                    this.label37.Text = "������ʽ��";
                    this.txtExpression.Text = "\"\",\"\"";
                    this.txtExpression.Enabled = true;
                    break;
                default :
                    this.txtExpression.Enabled = false;
                    break;
            }
        }

        private void raExportTxt_CheckedChanged(object sender, EventArgs e)
        {
            if (raExportTxt.Checked == true)
            {
                this.raExportExcel.Checked = false;
                this.raExportAccess.Checked = false;
                this.raExportMSSQL.Checked = false;
                this.raExportMySql.Checked = false;
                this.raExportWeb.Checked = false;

                SetExportFile();

                if (this.txtFileName.Text.Trim() != "")
                {
                    if (this.txtFileName.Text.EndsWith("xls"))
                        this.txtFileName.Text = this.txtFileName.Text.Substring(0, this.txtFileName.Text.Length - 3) + "txt";
                }

                this.IsSave.Text = "true";
            }
            
        }

        private void raExportExcel_CheckedChanged(object sender, EventArgs e)
        {
            if (raExportExcel.Checked == true)
            {
                this.raExportTxt.Checked = false;
                this.raExportAccess.Checked = false;
                this.raExportMSSQL.Checked = false;
                this.raExportMySql.Checked = false;
                this.raExportWeb.Checked = false;

                SetExportFile();

                if (this.txtFileName.Text.Trim() != "")
                {
                    if (this.txtFileName.Text.EndsWith("txt"))
                        this.txtFileName.Text = this.txtFileName.Text.Substring(0, this.txtFileName.Text.Length - 3) + "xls";
                }

                this.IsSave.Text = "true";
            }
        }

        private void raExportAccess_CheckedChanged(object sender, EventArgs e)
        {
            if (raExportAccess.Checked == true)
            {
                this.raExportTxt.Checked = false;
                this.raExportExcel.Checked = false;
                this.raExportMSSQL.Checked = false;
                this.raExportMySql.Checked = false;
                this.raExportWeb.Checked = false;

                SetExportDB();

                this.txtDataSource.Text = "";
                this.comTableName.Items.Clear();

                this.IsSave.Text = "true";
            }
        }

        private void raExportMSSQL_CheckedChanged(object sender, EventArgs e)
        {
            if (raExportMSSQL.Checked == true)
            {
                this.raExportTxt.Checked = false;
                this.raExportExcel.Checked = false;
                this.raExportAccess.Checked = false;
                this.raExportMySql.Checked = false;
                this.raExportWeb.Checked = false;

                SetExportDB();

                this.txtDataSource.Text = "";
                this.comTableName.Items.Clear();

                this.IsSave.Text = "true";
            }
        }

        private void raExportMySql_CheckedChanged(object sender, EventArgs e)
        {
            if (raExportMySql.Checked == true)
            {
                this.raExportTxt.Checked = false;
                this.raExportExcel.Checked = false;
                this.raExportAccess.Checked = false;
                this.raExportMSSQL.Checked = false;
                this.raExportWeb.Checked = false;

                SetExportDB();

                this.txtDataSource.Text = "";
                this.comTableName.Items.Clear();

                this.IsSave.Text = "true";
            }
        }

        private void raExportWeb_CheckedChanged(object sender, EventArgs e)
        {
            if (raExportWeb.Checked == true)
            {
                this.raExportTxt.Checked = false;
                this.raExportExcel.Checked = false;
                this.raExportAccess.Checked = false;
                this.raExportMSSQL.Checked = false;
                this.raExportMySql.Checked = false;

                SetExportWeb();

                this.IsSave.Text = "true";
            }
        }

        private void SetExportFile()
        {
            this.txtFileName.Enabled = true;
            this.cmdBrowser.Enabled = true;

            this.txtDataSource.Enabled = false;
            this.comTableName.Enabled = false;
            this.button12.Enabled = false;
            this.txtInsertSql.Enabled = false;

            this.txtExportUrl.Enabled = false;
            this.txtExportCookie.Enabled = false;
            this.button11.Enabled = false;
            this.button9.Enabled = false;
            this.comExportUrlCode.Enabled = false;
        }

        private void SetExportDB()
        {
            this.txtFileName.Enabled = false;
            this.cmdBrowser.Enabled = false;

            this.txtDataSource.Enabled = true;
            this.comTableName.Enabled = true;
            this.button12.Enabled = true;
            this.txtInsertSql.Enabled = true;

            this.txtExportUrl.Enabled = false;
            this.txtExportCookie.Enabled = false;
            this.button11.Enabled = false;
            this.button9.Enabled = false;
            this.comExportUrlCode.Enabled = false;
        }

        private void SetExportWeb()
        {
            this.txtFileName.Enabled = false;
            this.cmdBrowser.Enabled = false;

            this.txtDataSource.Enabled = false;
            this.comTableName.Enabled = false;
            this.button12.Enabled = false;
            this.txtInsertSql.Enabled = false;

            this.txtExportUrl.Enabled = true;
            this.txtExportCookie.Enabled = true;
            this.button11.Enabled = true;
            this.button9.Enabled = true;
            this.comExportUrlCode.Enabled = true ;
        }

        private void cmdBrowser_Click(object sender, EventArgs e)
        {
            if (this.raExportTxt.Checked == true)
            {
                this.saveFileDialog1.Title = "��ָ���������ı��ļ���";

                saveFileDialog1.InitialDirectory = Program.getPrjPath();
                saveFileDialog1.Filter = "txt Files(*.txt)|*.txt|All Files(*.*)|*.*";
            }
            else if (this.raExportExcel.Checked == true)
            {
                this.saveFileDialog1.Title = "��ָ��������Excel�ļ���";

                saveFileDialog1.InitialDirectory = Program.getPrjPath();
                saveFileDialog1.Filter = "Excel Files(*.xls)|*.xls|All Files(*.*)|*.*";
            }

            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtFileName.Text = this.saveFileDialog1.FileName;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            frmSetData fSD = new frmSetData();

            if (this.raExportAccess.Checked == true)
                fSD.FormState = 0;
            else if (this.raExportMSSQL .Checked ==true )
                fSD.FormState=1;
            else if (this.raExportMySql.Checked ==true )
                fSD.FormState =2;

            fSD.rDataSource = new frmSetData.ReturnDataSource(GetDataSource);
            fSD.ShowDialog();
            fSD.Dispose();
           
        }

        private void button11_Click(object sender, EventArgs e)
        {
            frmWeblink wftm = new frmWeblink();
            wftm.getFlag = 2;
            wftm.rExportCookie = new frmWeblink.ReturnExportCookie(GetExportCookie);
            wftm.ShowDialog();
            wftm.Dispose();

        }

        private void comTableName_DropDown(object sender, EventArgs e)
        {
            if (this.comTableName.Items.Count == 0)
            {
                if (this.raExportAccess.Checked == true)
                {
                    FillAccessTable();
                }
                else if (this.raExportMSSQL.Checked == true)
                {
                    FillMSSqlTable();
                }
                else if (this.raExportMySql.Checked == true)
                {
                    FillMySql();
                }

            }
        }

        private void FillAccessTable()
        {
            if (this.comTableName.Items.Count != 0)
                return;

            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = this.txtDataSource.Text; 

            try
            {
                conn.Open();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("���ݿ����ӷ������󣬴�����Ϣ��" + ex.Message, "Soukey��ժ ������Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable tb = conn.GetSchema("Tables");

            foreach (DataRow r in tb.Rows)
            {
                if (r[3].ToString() == "TABLE")
                {
                    this.comTableName.Items.Add(r[2].ToString());
                }
                
            }
           
        }

        private void FillMSSqlTable()
        {
            if (this.comTableName.Items.Count != 0)
                return;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = this.txtDataSource.Text;

            try
            {
                conn.Open();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("���ݿ����ӷ������󣬴�����Ϣ��" + ex.Message, "Soukey��ժ ������Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable tb = conn.GetSchema("Tables");

            foreach (DataRow r in tb.Rows)
            {
               
                this.comTableName.Items.Add(r[2].ToString());
                
            }
        }

        private void FillMySql()
        {
            if (this.comTableName.Items.Count != 0)
                return;

            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = this.txtDataSource.Text;

            try
            {
                conn.Open();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("���ݿ����ӷ������󣬴�����Ϣ��" + ex.Message, "Soukey��ժ ������Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable tb = conn.GetSchema("Tables");

            foreach (DataRow r in tb.Rows)
            {

                this.comTableName.Items.Add(r[2].ToString());

            }
        }

        private void comTableName_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillInsertSql(this.comTableName.SelectedItem.ToString ());

            this.IsSave.Text = "true";
        }

        private void txtDataSource_TextChanged(object sender, EventArgs e)
        {
            if (this.comTableName.Items.Count != 0)
                this.comTableName.Items.Clear();

            this.IsSave.Text = "true";
        }

        private DataTable GetTableColumns(string tName)
        {
            DataTable tc=new DataTable ();

            try
            {

                if (this.raExportAccess.Checked == true)
                {
                    OleDbConnection conn = new OleDbConnection();
                    conn.ConnectionString = this.txtDataSource.Text;

                    conn.Open();

                    string[] Restrictions = new string[4];
                    Restrictions[2] = tName;

                    tc = conn.GetSchema("Columns",Restrictions);

                    return tc;

                }
                else if (this.raExportMSSQL.Checked == true)
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = this.txtDataSource.Text;

                    conn.Open();

                    string[] Restrictions = new string[4];
                    Restrictions[2] = tName;

                    tc = conn.GetSchema("Columns", Restrictions);

                    return tc;
                }
                else if (this.raExportMySql.Checked == true)
                {
                    MySqlConnection conn = new MySqlConnection();
                    conn.ConnectionString = this.txtDataSource.Text;

                    conn.Open();

                    string[] Restrictions = new string[4];
                    Restrictions[2] = tName;

                    tc = conn.GetSchema("Columns", Restrictions);

                    return tc;
                }

                return tc;

            }
            catch (System.Exception )
            {
                return null;
            }


        }

        private void FillInsertSql(string TableName)
        {
            string iSql = "";
            string strColumns = "";

            iSql = "insert into " + TableName + " (";

            DataTable tc = GetTableColumns(TableName);

            for (int i = 0; i < tc.Rows.Count; i++)
            {
                strColumns += tc.Rows[i][3].ToString() + ",";
            }

            strColumns = strColumns.Substring(0, strColumns.Length - 1);

            iSql = iSql + strColumns + ") values ( ";

            string strColumnsValue = "";

            for (int j = 0; j < this.listWebGetFlag.Items.Count; j++)
            {
                if (this.listWebGetFlag.Items[j].SubItems[1].Text == "�ı�")
                    strColumnsValue += "\"{" + this.listWebGetFlag.Items[j].Text + "}\",";

            }

            if (strColumnsValue!="")
                strColumnsValue = strColumnsValue.Substring(0, strColumnsValue.Length - 1);

            iSql = iSql + strColumnsValue + ")";

            this.txtInsertSql .Text = iSql;

        }

        private void comTableName_TextChanged(object sender, EventArgs e)
        {
            string iSql = "insert into " + this.comTableName.Text + "(";
            string strColumns = "";
            string strColumnsValue = "";

            for (int j = 0; j < this.listWebGetFlag.Items.Count; j++)
            {
                if (this.listWebGetFlag.Items[j].SubItems[1].Text == "�ı�")
                    strColumns += this.listWebGetFlag.Items[j].Text + ",";
                    strColumnsValue += "\"{" + this.listWebGetFlag.Items[j].Text + "}\",";

            }

            if (strColumns != "")
            {
                strColumns = strColumns.Substring(0, strColumns.Length - 1);
                strColumnsValue = strColumnsValue.Substring(0, strColumnsValue.Length - 1);
            }

            
            iSql = iSql + strColumns + ") values (" + strColumnsValue + ")";
            this.txtInsertSql.Text = iSql;

        }

        private void rmenuGetFormat_Opening(object sender, CancelEventArgs e)
        {
           
            this.rmenuGetFormat.Items.Clear();
            this.rmenuGetFormat.Items.Add("POSTǰ׺<POST>");
            this.rmenuGetFormat.Items.Add("POST��׺</POST>");
            this.rmenuGetFormat.Items.Add("�ֹ�����POST����");
            this.rmenuGetFormat.Items.Add(new ToolStripSeparator());

            for (int i = 0; i < this.listWebGetFlag.Items.Count; i++)
            {
                this.rmenuGetFormat.Items.Add("{" + this.listWebGetFlag.Items[i].Text + "}") ;
            }
        }

        private void rmenuGetFormat_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "�ֹ�����POST����")
            {
                frmWeblink wftm = new frmWeblink();
                wftm.getFlag = 3;
                wftm.rExportPData = new frmWeblink.ReturnExportPOST(GetExportpData);
                wftm.ShowDialog();
                wftm.Dispose();

                return;
            }

            Match s;

            if (Regex.IsMatch(e.ClickedItem.ToString(), "[{].*[}]"))
            {
                s = Regex.Match(e.ClickedItem.ToString(), "[{].*[}]");
            }
            else
            {
                s = Regex.Match(e.ClickedItem.ToString(), "[<].*[>]");
            }

            int startPos = this.txtExportUrl.SelectionStart;
            int l = this.txtExportUrl.SelectionLength;

            this.txtExportUrl.Text = this.txtExportUrl.Text.Substring(0, startPos) + s.Groups[0].Value + this.txtExportUrl.Text.Substring(startPos + l, this.txtExportUrl.Text.Length - startPos - l);

            this.txtExportUrl.SelectionStart = startPos + s.Groups[0].Value.Length;
            this.txtExportUrl.ScrollToCaret();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.rmenuGetFormat.Show(this.button9, 0, 21);
        }

        private void IsSave_TextChanged(object sender, EventArgs e)
        {
            if (this.IsSave.Text == "true" && this.FormState !=cGlobalParas.FormState .Browser )
            {
                this.cmdApply.Enabled = true;
            }
            else if (this.IsSave.Text == "false")
            {
                this.cmdApply.Enabled = false;
            }
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            if (!CheckInputvalidity())
            {
                return;
            }

            try
            {

                if (!SaveTask(""))
                {
                    return;
                }
                
                this.IsSave.Text = "false";

                IsSaveTask = true;

                if (this.FormState == cGlobalParas.FormState.New)
                {
                    this.FormState = cGlobalParas.FormState.Edit;
                }
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("���񱣴�ʱʧ�ܣ�����ԭ���ǣ�" + ex.Message, "Soukey��ժ ������Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void txtWeblinkDemo_TextChanged(object sender, EventArgs e)
        {
            this.IsSave.Text = "true";
        }

        private void txtFileName_TextChanged(object sender, EventArgs e)
        {
            this.IsSave.Text = "true";
        }

        private void txtInsertSql_TextChanged(object sender, EventArgs e)
        {
            this.IsSave.Text = "true";
        }

        private void txtExportUrl_TextChanged(object sender, EventArgs e)
        {
            this.IsSave.Text = "true";
        }

        private void txtExportCookie_TextChanged(object sender, EventArgs e)
        {
            this.IsSave.Text = "true";
        }

        private void comExportUrlCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.IsSave.Text = "true";
        }

        private void TaskType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cGlobalParas.ConvertID(this.TaskType.SelectedItem.ToString()) == (int)cGlobalParas.TaskType.AjaxHtmlByUrl)
                this.label42.Visible = true;
            else
                this.label42.Visible = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.IsSave.Text = "true";
        }

        private void txtNag_TextChanged(object sender, EventArgs e)
        {
            this.IsSave.Text = "true";
        }

        private void cmdAddNRule_Click(object sender, EventArgs e)
        {
            if (this.txtNag.Text == "" || this.txtNag.Text == null)
            {
                this.txtNag.Focus();
                MessageBox.Show("����д��������������", "Soukey��ժ ��Ϣ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            for (int i = 0; i<this.dataNRule.Rows.Count; i++)
            {
                this.dataNRule.Rows[i].Cells[0].Value = i + 1;
            }

            int MaxLevel = 0;
            if (this.dataNRule.Rows.Count == 0)
                MaxLevel = 1;
            else
                MaxLevel = this.dataNRule.Rows.Count + 1;

            if (this.checkBox1.Checked ==true )
                this.dataNRule.Rows.Add(MaxLevel.ToString (),this.txtNag.Text ,"Y");
            else
                this.dataNRule.Rows.Add(MaxLevel.ToString (),this.txtNag.Text ,"N");
            
            this.IsSave.Text = "true";
        }

        private void cmdDelNRule_Click(object sender, EventArgs e)
        {
            this.dataNRule.Focus();
            SendKeys.Send("{Del}");
            this.IsSave.Text = "true";
        }

        private void IsGatherErrorAgain_CheckedChanged(object sender, EventArgs e)
     
      
    }

        private void IsPublishErrLog_CheckedChanged()
        {
        
        }

        private void radioButton1_CheckedChanged()
        {
        
        }

        private void txtErrorLog_TextChanged()
        {
        
        }

        private void udAgainNumber_ValueChanged()
        {
        
        }
}