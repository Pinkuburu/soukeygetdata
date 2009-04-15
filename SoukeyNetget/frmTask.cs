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


///���ܣ��ɼ�������Ϣ����  
///���ʱ�䣺2009-3-2
///���ߣ�һ��
///�������⣺��
///�����ƻ�����
///˵������ 
///�汾��00.90.00
///�޶�����
namespace SoukeyNetget
{
    public partial class frmTask : Form
    {

        public delegate void ReturnTaskClass(string tClass);
        public ReturnTaskClass rTClass;

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
                this.groupBox4.Enabled = true;

                switch ((cGlobalParas.PublishType)int.Parse(t.ExportType))
                {
                    case cGlobalParas.PublishType.PublishAccess:
                        this.raExportAccess.Checked  = true;
                        break;
                    case cGlobalParas.PublishType.PublishExcel :
                        this.raExportExcel.Checked = true;
                        break;
                    case cGlobalParas.PublishType.PublishTxt :

                        this.raExportTxt.Checked = true;
                        break;
                    default :
                        break;
                }
                this.DataSource.Text = t.DataSource;
                this.txtDataUser.Text  = t.DataUser;
                this.txtDataPwd.Text = t.DataPwd;
                this.txtTableName.Text = t.DataTableName;
            }
            else
            {
                //���ɼ�����
                this.groupBox4.Enabled = false;
                this.DataSource.Text = "";
                this.txtDataUser.Text = "";
                this.txtDataPwd.Text = "";
                this.txtTableName.Text = "";

            }

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
                item.SubItems.Add (t.WebpageCutFlag[i].StartPos.ToString ());
                item.SubItems.Add (t.WebpageCutFlag[i].EndPos .ToString ());
                item.SubItems.Add(cGlobalParas.ConvertName (t.WebpageCutFlag[i].LimitSign));
                this.listWebGetFlag.Items.Add(item);
                item=null;
            }

            t=null ;
            
        }

        #region ������ʼ������ ����������״̬���м��أ��½����޸ġ������
        
        private void IniData()
        {
            //��ʼ��ҳ���������
            
            this.TaskType.Items.Add("����ָ������ַ�ɼ�����");
            this.TaskType.SelectedIndex = 0;

            this.comRunType.Items.Add("�ɼ�����������");
            this.comRunType.Items.Add("���ɼ�����");
            this.comRunType.SelectedIndex = 1;

            this.comLimit.Items.Add("���������ʽ������");
            this.comLimit.Items.Add("�����������ҳ��ʶ��");
            this.comLimit.Items.Add("����ƥ�䵫��ʾʱɾ��");

            this.comWebCode.Items.Add("�Զ�");
            this.comWebCode.Items.Add("gb2312");
            this.comWebCode.Items.Add("UTF-8");
            this.comWebCode.Items.Add("GBK");
            this.comWebCode.SelectedIndex = 0;

            this.comUrlEncode.Items.Add("UTF-8");
            this.comUrlEncode.Items.Add("gb2312");

            //��ʼ��ҳ�����ʱ�����ؼ��״̬


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
            else if (this.raExportAccess.Checked == true)
            {
                this.saveFileDialog1.Title = "��ָ��������Access�ļ���";

                saveFileDialog1.InitialDirectory = Program.getPrjPath();
                saveFileDialog1.Filter = "Access Files(*.mdb)|*.mdb|All Files(*.*)|*.*";
            }

            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.DataSource.Text = this.saveFileDialog1.FileName;
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
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
        }

        private void AddDemoUrl()
        {
            if (this.listWeblink.Items.Count != 0)
            {
                string Url;
                List<string> Urls;

                if (this.IsUrlEncode.Checked == true)
                {
                    Urls = gUrl.SplitWebUrl(this.listWeblink.Items[0].Text.ToString(), this.IsUrlEncode.Checked, cGlobalParas.ConvertID(this.comUrlEncode.SelectedItem.ToString()).ToString());
                }
                else
                {
                    Urls = gUrl.SplitWebUrl(this.listWeblink.Items[0].Text.ToString(), this.IsUrlEncode.Checked);
                }
                if (this.listWeblink.Items[0].SubItems[1].Text  != "")
                {
                    
                    if (this.listWeblink.Items[0].SubItems[2].Text  == "��")
                        Url = GetUrl(this.listWeblink.Items[0].Text.ToString(), this.listWeblink.Items[0].SubItems[1].Text, true);
                    else
                        Url = GetUrl(this.listWeblink.Items[0].Text.ToString(), this.listWeblink.Items[0].SubItems[1].Text, false);

                }
                else
                {
                    Url = Urls[0].ToString();
                }

                this.txtWeblinkDemo.Text = Url;
                Urls = null;
            }
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

            this.listWeblink.Items.Remove(this.listWeblink.SelectedItems[0]);

            if (this.listWeblink.Items.Count == 0)
            {
                this.txtWeblinkDemo.Text = "";
            }

        }

        private void comRunType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.comRunType.SelectedIndex )
            {
                case 0:
                    this.groupBox4.Enabled = true;
                    break;
                case 1:
                    this.groupBox4.Enabled = false;
                    break;
                default :
                    break;
            }
        }

        //���񱣴棬�����񱣴��ʱ�򣬲����������ģ����Ϣ�����������û��Զ���
        //�˹���ֻ���û��ڽ�������ʱ��һ�ֿ��ٲ���
        private bool SaveTask()
        {

            Task.cTask t = new Task.cTask();

            //����Ǳ༭״̬������Ҫɾ��ԭ���ļ�
            if (this.FormState == cGlobalParas.FormState.Edit)
            {
                t.TaskName = this.tTask.Text;

                if (this.comTaskClass.SelectedIndex == 0)
                {
                    t.DeleTask("", this.tTask.Text);
                }
                else
                {
                    //��ȡ����������·��
                    Task.cTaskClass tClass = new Task.cTaskClass();
                    string tPath=tClass.GetTaskClassPathByName(this.comTaskClass.SelectedItem.ToString());
                    t.DeleTask(tPath, this.tTask.Text);
                }
            }

            int i = 0;
            int UrlCount = 0;

            if (!CheckInputvalidity())
            {
                return false ;
            }

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
                if (this.raExportAccess.Checked ==true )
                {
                    t.ExportType =((int) cGlobalParas.PublishType.PublishAccess).ToString () ;
                }
                else if (this.raExportTxt.Checked ==true )
                {
                    t.ExportType = ((int)cGlobalParas.PublishType.PublishTxt).ToString ();
                }
                else if (this.raExportExcel.Checked == true)
                {
                    t.ExportType = ((int)cGlobalParas.PublishType.PublishExcel ).ToString();
                }

                t.DataSource  = this.DataSource.Text.ToString();
                t.DataUser =this.txtDataUser.Text.ToString ();
                t.DataPwd =this.txtDataPwd.Text .ToString () ;
                t.DataTableName = this.txtTableName.Text.ToString();
            }
            else
            {
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
                    w.NextPageRule = this.txtNextPage.Text.ToString();
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
                c.StartPos = this.listWebGetFlag.Items[i].SubItems[1].Text;
                c.EndPos = this.listWebGetFlag.Items[i].SubItems[2].Text;
                c.LimitSign =cGlobalParas.ConvertID (this.listWebGetFlag.Items[i].SubItems[3].Text);
                t.WebpageCutFlag.Add(c);
                c = null;

            }

            t.Save();
            t=null;

            return true;

        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (this.listWeblink.Items.Count == 0 || this.listWebGetFlag.Items.Count == 0)
            {
                if (MessageBox.Show("�ڴ�����û�ж�����Ҫ�ɼ�����ַ���߲ɼ���ַ�Ĺ����Ƿ������������", "soukeyѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }

            if (!SaveTask())
            {
                return;
            }
            if (this.comTaskClass.SelectedIndex == 0)
            {
                rTClass("");
            }
            else
            {
                rTClass(this.comTaskClass.SelectedItem.ToString());
            }

            this.Dispose();

        }

        private void cmdSelectTask_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("��ԭ������������޸Ľ����µ����񽫵���������ѡ���������Ϣ����������ǰ�Ѿ������������Ϣ��ȫ��ɾ�����������ƻᱣ����������ʹ�ô˹��ܣ�", "ϵͳѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            frmImportTask ft = new frmImportTask();
            ft.RTaskID = new frmImportTask.ReturnTaskID(GetTaskID);
            ft.ShowDialog();
            ft.Dispose();

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
        private void GetTaskID(int TaskID,string TaskName)
        {
            this.txtTaskTemp.Tag  = TaskID.ToString();
            this.txtTaskTemp.Text = TaskName;

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

        private void GetPData(string strCookie, string pData)
        {
            this.txtCookie.Text = strCookie;
            this.txtWebLink.Text += "<POST>" + pData + "</POST>";
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

            ////��ʼ���ֵ�˵�����Ŀ
            //cDict d = new cDict();
            //int count = d.GetDictClassCount();
            //ToolStripMenuItem cItem;

            //for (int i = 0; i < count; i++)
            //{
            //    cItem =new ToolStripMenuItem ();
            //    cItem.Name =d.GetDictClassName(i).ToString();
            //    cItem.Text ="�ֵ�:{Dict:" + d.GetDictClassName(i).ToString() + "}";
            //    //this.contextMenuStrip1.Items.Add("�ֵ�:{Dict:" + d.GetDictClassName(i).ToString() + "}");
            //    this.contextMenuStrip1.Items.Add(cItem);
            //}

            switch (this.FormState)
            {
                case cGlobalParas.FormState.New :
                    break;
                case cGlobalParas.FormState.Edit :
                    this.tTask.ReadOnly = true;
                    break;
                case cGlobalParas.FormState.Browser :
                    break;
                default :
                    break ;
            }
            

        }

        private void button1_Click(object sender, EventArgs e)
        {

          GatherData ();

        }

        private void GatherData()
        {
            //���Բɼ��������û���������ݲ��Բɼ�
            //��֤�����Ƿ���ȷ
            //�Ȳ��������п�������һ���е���һ��ҳ��������

            if (this.txtWeblinkDemo.Text.ToString() == null || this.txtWeblinkDemo.Text.ToString() == "")
            {
                this.errorProvider1.Clear();
                this.tabControl1.SelectedTab = this.tabControl1.TabPages[2];
                this.errorProvider1.SetError(this.txtWeblinkDemo, "��Ҫָ��ʾ����ַ����ʾ����ַ�Ǹ�����ָ������ַ�Զ�����");
                return;
            }
                       
            this.tabControl1.SelectedTab = this.tabControl1.TabPages[3];
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
                c.StartPos = this.listWebGetFlag.Items[i].SubItems[1].Text;
                c.EndPos = this.listWebGetFlag.Items[i].SubItems[2].Text;
                c.LimitSign =cGlobalParas.ConvertID ( this.listWebGetFlag.Items[i].SubItems[3].Text);
                gData.CutFlag.Add(c);
                c = null;
            }

            try
            {
                DataTable dGather = gData.GetGatherData(this.txtWeblinkDemo.Text.ToString(), (cGlobalParas.WebCode)cGlobalParas.ConvertID(this.comWebCode.SelectedItem.ToString()), this.txtCookie.Text.ToString(), this.txtStartPos.Text.ToString(), this.txtEndPos.Text.ToString());

                //�󶨵���ʾ��DataGrid��
                this.dataTestGather.DataSource = dGather;
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("�ɼ��������󣬴�����Ϣ��" + ex.Message, "������Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            this.labWaiting.Visible = false;

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

            if (this.comLimit.SelectedIndex ==-1)
            {
                this.comLimit.SelectedIndex=0;
            }

            //�ж������Ƿ��Ѿ��ظ�
            for (int i = 0; i < this.listWebGetFlag.Items.Count;i++ )
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
            item.SubItems.Add(cTool.ClearFlag ( this.txtGetStart.Text.ToString()));
            item.SubItems.Add(cTool.ClearFlag ( this.txtGetEnd.Text.ToString()));
            item.SubItems.Add(this.comLimit.SelectedItem.ToString());
            this.listWebGetFlag.Items.Add(item);
            item = null;

            this.txtGetTitleName.Text = "";
            this.txtGetStart.Text ="";
            this.txtGetEnd.Text ="";
            this.comLimit.SelectedIndex = -1;
            

        }

        private void cmdDelCutFlag_Click(object sender, EventArgs e)
        {
            if (this.listWebGetFlag.Items.Count  == 0)
            {
                return;
            }
            this.listWebGetFlag.Items.Remove(this.listWebGetFlag.SelectedItems[0]);
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

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox2.Checked == true)
            {
                this.label10.Enabled = true;
                this.txtNag.Enabled = true;
                this.checkBox1.Enabled = true;
            }
            else
            {
                this.label10.Enabled = false;
                this.txtNag.Enabled = false;
                this.checkBox1.Enabled = false;
            }

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if(this.checkBox3.Checked ==true )
            {
                this.label13.Enabled = true;
                this.txtNextPage.Enabled =true ;
            }
            else
            {
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

            if (this.IsUrlEncode.Checked == true)
            {
                Urls = gUrl.ParseUrlRule(webLink, NavRule, this.IsUrlEncode.Checked, cGlobalParas.ConvertID(this.comUrlEncode.SelectedItem.ToString()).ToString());
            }
            else
            {
                Urls = gUrl.ParseUrlRule(webLink, NavRule, this.IsUrlEncode.Checked);
            }

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

        private void txtNag_TextChanged(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void cmdWebSource_Click(object sender, EventArgs e)
        {

        }

        private void cmdOKRun_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (this.listWeblink.Items.Count >= 1)
            {
                AddDemoUrl();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (this.listWebGetFlag.SelectedItems.Count  == 0)
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
            this.listWebGetFlag.SelectedItems[0].SubItems[1].Text=cTool.ClearFlag(this.txtGetStart.Text.ToString());
            this.listWebGetFlag.SelectedItems[0].SubItems[2].Text=cTool.ClearFlag(this.txtGetEnd.Text.ToString());
            if (this.comLimit.SelectedIndex == -1)
            {
                this.listWebGetFlag.SelectedItems[0].SubItems[3].Text = this.comLimit.Items[0].ToString();
            }
            else
            {
                this.listWebGetFlag.SelectedItems[0].SubItems[3].Text = this.comLimit.SelectedItem.ToString();
            }

            this.txtGetTitleName.Text = "";
            this.txtGetStart.Text = "";
            this.txtGetEnd.Text = "";
            this.comLimit.SelectedIndex = -1;
        }

        private void raExportAccess_CheckedChanged(object sender, EventArgs e)
        {
            if (this.raExportAccess.Checked == true)
            {
                this.label6.Text = "���ݿ⣺";
                this.label5.Enabled = true;
                this.label7.Enabled = true;
                this.label8.Enabled = true;
                this.txtDataUser.Enabled = true;
                this.txtDataPwd.Enabled = true;
                this.txtTableName.Enabled = true;
                this.button9.Enabled = true;
            }
        }

      

        private void ConnectAccess()
        {
            string connectionstring = "provider=microsoft.jet.oledb.4.0;data source=";
            connectionstring += this.DataSource.Text;
            if (this.txtDataUser.Text.Trim() != "")
            {
                connectionstring += "UID=" + this.txtDataUser.Text;

            }
            OleDbConnection con = new OleDbConnection(connectionstring);
            con.Open();
            con.Close ();
            MessageBox.Show("���ݿ����ӳɹ���", "soukey��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ConnectSqlServer()
        {
            string strDataBase = "Server=.;DataBase=Library;Uid=" + this.txtDataUser.Text.Trim() + ";pwd=" + this.txtDataPwd.Text + ";";
            SqlConnection conn = new SqlConnection(strDataBase);
            conn.Open();
            conn.Close();
            MessageBox.Show("���ݿ����ӳɹ���", "soukey��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectAccess();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("���ݿ�����ʧ�ܣ��������������Ϣ�Ƿ���ȷ��", "soukey��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
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
            }
            else
            {
                this.comUrlEncode.Enabled = false;
            }
        }

        private void listWebGetFlag_Click(object sender, EventArgs e)
        {
            if (this.listWebGetFlag.SelectedItems.Count != 0)
            {
                this.txtGetTitleName.Text = this.listWebGetFlag.SelectedItems[0].Text;
                this.txtGetStart.Text = this.listWebGetFlag.SelectedItems[0].SubItems[1].Text;
                this.txtGetEnd.Text = this.listWebGetFlag.SelectedItems[0].SubItems[2].Text;
                this.comLimit.SelectedItem = this.listWebGetFlag.SelectedItems[0].SubItems[3].Text;
            }
        }

        private void listWebGetFlag_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listWeblink_SelectedIndexChanged(object sender, EventArgs e)
        {

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

            this.txtNextPage.Text = this.listWeblink.SelectedItems[0].SubItems[1].Text; ;
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

            this.txtWebLink.Text = "http://";
            this.checkBox2.Checked = false;
            this.checkBox1.Checked = false;
            this.checkBox3.Checked = false;
            this.txtNag.Text = "";
            this.txtNextPage.Text = "";
        }

        private void raExportTxt_CheckedChanged(object sender, EventArgs e)
        {
            if (this.raExportTxt.Checked == true)
            {
                this.label6.Text = "�ļ�����";
                this.label5.Enabled = false;
                this.label7.Enabled = false;
                this.label8.Enabled = false;
                this.txtDataUser.Enabled = false;
                this.txtDataPwd.Enabled = false;
                this.txtTableName.Enabled = false;
                this.button9.Enabled = false;
            }
                
        }

        private void raExportExcel_CheckedChanged(object sender, EventArgs e)
        {
            if (this.raExportExcel.Checked == true)
            {
                this.label6.Text = "�ļ�����";
                this.label5.Enabled = false;
                this.label7.Enabled = false;
                this.label8.Enabled = false;
                this.txtDataUser.Enabled = false;
                this.txtDataPwd.Enabled = false;
                this.txtTableName.Enabled = false;
                this.button9.Enabled = false;
            }
        }
        

             
    }
}