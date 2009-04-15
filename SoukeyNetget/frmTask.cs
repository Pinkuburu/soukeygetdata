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


///功能：采集任务信息处理  
///完成时间：2009-3-2
///作者：一孑
///遗留问题：无
///开发计划：无
///说明：无 
///版本：00.90.00
///修订：无
namespace SoukeyNetget
{
    public partial class frmTask : Form
    {

        public delegate void ReturnTaskClass(string tClass);
        public ReturnTaskClass rTClass;

        //定义一个ToolTip
        ToolTip HelpTip = new ToolTip();
        Task.cUrlAnalyze gUrl = new Task.cUrlAnalyze();

        public frmTask()
        {
            InitializeComponent();
            IniData();
        }

        #region 窗体进入的状态
        private cGlobalParas.FormState m_FormState;
        public cGlobalParas.FormState FormState
        {
            get { return m_FormState;}
            set { m_FormState = value; }
        }
        #endregion

        //设置ToolTip的信息
        private void SetTooltip()
        {
            //HelpTip.SetToolTip(this.tTask, @"输入任务名称，任务名称可以是中文或英文，但不允许出现.*\/,等");
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
                //存在导出任务
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
                //仅采集数据
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
                item.SubItems.Add((t.WebpageLink[i].IsOppPath == true) ? "是" : "否");
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

        #region 启动初始化数据 根据启动的状态进行加载：新建、修改、浏览等
        
        private void IniData()
        {
            //初始化页面加载数据
            
            this.TaskType.Items.Add("根据指定的网址采集数据");
            this.TaskType.SelectedIndex = 0;

            this.comRunType.Items.Add("采集并发布数据");
            this.comRunType.Items.Add("仅采集数据");
            this.comRunType.SelectedIndex = 1;

            this.comLimit.Items.Add("不做任意格式的限制");
            this.comLimit.Items.Add("不允许出现网页标识符");
            this.comLimit.Items.Add("允许匹配但显示时删除");

            this.comWebCode.Items.Add("自动");
            this.comWebCode.Items.Add("gb2312");
            this.comWebCode.Items.Add("UTF-8");
            this.comWebCode.Items.Add("GBK");
            this.comWebCode.SelectedIndex = 0;

            this.comUrlEncode.Items.Add("UTF-8");
            this.comUrlEncode.Items.Add("gb2312");

            //初始化页面加载时各个控间的状态


            //初始化任务分类
            //开始初始化树形结构,取xml中的数据,读取任务分类
            this.comTaskClass.Items.Add("此任务不分类，直接保存到默认路径");
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

        #region 按钮及界面控制操作

       private void cmdBrowser_Click(object sender, EventArgs e)
        {
            if (this.raExportTxt.Checked == true)
            {
                this.saveFileDialog1.Title = "请指定导出的文本文件名";

                saveFileDialog1.InitialDirectory = Program.getPrjPath();
                saveFileDialog1.Filter = "txt Files(*.txt)|*.txt|All Files(*.*)|*.*";
            }
            else if (this.raExportExcel.Checked == true)
            {
                this.saveFileDialog1.Title = "请指定导出的Excel文件名";

                saveFileDialog1.InitialDirectory = Program.getPrjPath();
                saveFileDialog1.Filter = "Excel Files(*.xls)|*.xls|All Files(*.*)|*.*";
            }
            else if (this.raExportAccess.Checked == true)
            {
                this.saveFileDialog1.Title = "请指定导出的Access文件名";

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
                this.errorProvider1.SetError(this.txtWebLink, "请输入网址信息");
                return;
            }
            else
            {
                if (!Regex.IsMatch (this.txtWebLink.Text.Trim().ToString (),"http://",RegexOptions.IgnoreCase))
                {
                    this.errorProvider1.SetError(this.txtWebLink, "您输入的网址不合法，请检查");
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
                    litem.SubItems.Add("是");
                }
                else
                {
                    litem.SubItems.Add("否");
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
                    
                    if (this.listWeblink.Items[0].SubItems[2].Text  == "是")
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
            //    MessageBox.Show("系统无法自动获取网页编码，可通过查看页面属性（Firefox）或查看页面编码（IE）来判断页面编码格式", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

        //任务保存，当任务保存的时候，不保存任务的模板信息任务的类别都是用户自定义
        //此功能只是用户在建立任务时的一种快速操作
        private bool SaveTask()
        {

            Task.cTask t = new Task.cTask();

            //如果是编辑状态，则需要删除原有文件
            if (this.FormState == cGlobalParas.FormState.Edit)
            {
                t.TaskName = this.tTask.Text;

                if (this.comTaskClass.SelectedIndex == 0)
                {
                    t.DeleTask("", this.tTask.Text);
                }
                else
                {
                    //获取此任务分类的路径
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

            //保存任务
            

            //新建一个任务
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

            //判断是否导出文件
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
                    if (this.listWeblink.Items[i].SubItems[2].Text == "是")
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
                if (MessageBox.Show("在此任务没有定义需要采集的网址或者采集网址的规则，是否继续保存任务？", "soukey询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
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
            if (MessageBox.Show("在原有任务基础上修改建立新的任务将导入您即将选择的任务信息，这样您当前已经输入的任务信息将全部删除（任务名称会保留），继续使用此功能？", "系统询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            frmImportTask ft = new frmImportTask();
            ft.RTaskID = new frmImportTask.ReturnTaskID(GetTaskID);
            ft.ShowDialog();
            ft.Dispose();

        }

        #endregion

        #region 其他操作 输入检查
        //检查用户输入内容的有效性，只要任务有名称就可以保存，降低使用难度
        //但保存的任务不一定可以执行，需要在执行前做进一步修改
        private bool CheckInputvalidity()
        {
            this.errorProvider1.Clear();

            if (this.tTask.Text.ToString () == null || this.tTask.Text.Trim().ToString () == "")
            {
                this.errorProvider1.SetError(this.tTask, "任务名称不能为空!");
                return false ;
            }

            //if (this.TaskType.SelectedIndex ==1)
            //{
            //    //表示选择了根据任务进行建立
            //    if (this.txtTaskTemp.Text.ToString() == null || this.txtTaskTemp.Text.ToString() == "")
            //    {
            //        this.errorProvider1.SetError(this.txtTaskTemp, "您选择了通过任务模板建立，所以请导入任务信息");
            //        return;
            //    }
            //}

            //if (this.comRunType.SelectedIndex  == 0)
            //{
            //    //表示边采集数据边直接导出数据
            //    if (this.txtFileName.Text.ToString() == null || this.txtFileName.Text.ToString() == "")
            //    {
            //        this.errorProvider1.SetError(this.txtFileName, "您选择了需要实时导出数据，所以请输入导出数据的文件名称");
            //        return;
            //    }

            //}

            //if (this.listWeblink.Items.Count == 0)
            //{
            //    this.tabControl1.SelectedTab = this.tabControl1.TabPages[1];

            //    this.errorProvider1.SetError(this.listWeblink, "无需要采集的网址，请检查！");
            //    return;
            //}

            //if (this.comWebCode.SelectedItem == null)
            //{
            //    this.tabControl1.SelectedTab = this.tabControl1.TabPages[1];

            //    this.errorProvider1.SetError(this.comWebCode, "网页编码不能为空，如果为空则可能导致乱码出现");
            //    return;
            //}

            //if (this.listWebGetFlag.Items.Count == 0)
            //{
            //    this.tabControl1.SelectedTab = this.tabControl1.TabPages[2];
            //    this.errorProvider1.SetError(this.listWebGetFlag, "无需要采集的数据标签，请检查");
            //    return;
            //}
            return true;
        }

        #endregion

        #region 传递给委托的方法
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
            //对Tooltip进行初始化设置
            HelpTip.ToolTipIcon = ToolTipIcon.Info;
            HelpTip.ForeColor =Color.YellowGreen;
            HelpTip.BackColor = Color.LightGray;
            HelpTip.AutoPopDelay = 5000;
            HelpTip.ShowAlways = true;
            HelpTip.ToolTipTitle = "";

            SetTooltip();

            ////初始化字典菜单的项目
            //cDict d = new cDict();
            //int count = d.GetDictClassCount();
            //ToolStripMenuItem cItem;

            //for (int i = 0; i < count; i++)
            //{
            //    cItem =new ToolStripMenuItem ();
            //    cItem.Name =d.GetDictClassName(i).ToString();
            //    cItem.Text ="字典:{Dict:" + d.GetDictClassName(i).ToString() + "}";
            //    //this.contextMenuStrip1.Items.Add("字典:{Dict:" + d.GetDictClassName(i).ToString() + "}");
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
            //测试采集，根据用户定义的内容测试采集
            //验证数据是否正确
            //比部分内容有可能在下一版中单独一个页面来处理

            if (this.txtWeblinkDemo.Text.ToString() == null || this.txtWeblinkDemo.Text.ToString() == "")
            {
                this.errorProvider1.Clear();
                this.tabControl1.SelectedTab = this.tabControl1.TabPages[2];
                this.errorProvider1.SetError(this.txtWeblinkDemo, "需要指定示例网址，此示例网址是根据您指定的网址自动产生");
                return;
            }
                       
            this.tabControl1.SelectedTab = this.tabControl1.TabPages[3];
            this.labWaiting.Visible = true;

            Application.DoEvents();

            Gather.cGatherWeb gData = new Gather.cGatherWeb();

            //增加采集的标志
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

                //绑定到显示的DataGrid中
                this.dataTestGather.DataSource = dGather;
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("采集发生错误，错误信息：" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            this.labWaiting.Visible = false;

        }

        private void cmdAddCutFlag_Click(object sender, EventArgs e)
        {
            this.errorProvider1.Clear();

            if (this.txtGetTitleName.Text.Trim().ToString() == "")
            {
                this.errorProvider1.SetError(this.txtGetTitleName, "请输入采集数据的名称");
                return;
            }

            if (this.txtGetStart.Text.Trim().ToString() == "")
            {
                this.errorProvider1.SetError(this.txtGetStart, "请输入采集数据的起始标志");
                return;

            }

            if (this.txtGetEnd.Text.Trim().ToString() == "")
            {
                this.errorProvider1.SetError(this.txtGetEnd, "请输入采集数据的结束标志");
                return;
            }

            if (this.comLimit.SelectedIndex ==-1)
            {
                this.comLimit.SelectedIndex=0;
            }

            //判断名称是否已经重复
            for (int i = 0; i < this.listWebGetFlag.Items.Count;i++ )
            {
                if (this.listWebGetFlag.Items[i].Text == this.txtGetTitleName.Text)
                {
                    this.errorProvider1.Clear();
                    this.errorProvider1.SetError(this.txtGetEnd, "采集的名称不能重复，请重新命名");
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
            if (e.ClickedItem.Text == "手工捕获POST数据")
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
                MessageBox.Show("导航规则为空，无法测试！", "soukey提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string Url = GetUrl(this.txtWebLink.Text, this.txtNag.Text, this.checkBox1.Checked);

            if (!Regex.IsMatch(Url, @"(http|https|ftp)+://[^\s]*"))
            {
                MessageBox.Show("网址无法打开，可能出错，请检查网址及导航规则。", "soukey信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            //需要判断网页地址前后存在单引号或双引号
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
                this.errorProvider1.SetError(this.txtGetTitleName, "请输入采集数据的名称");
                return;
            }

            if (this.txtGetStart.Text.Trim().ToString() == "")
            {
                this.errorProvider1.SetError(this.txtGetStart, "请输入采集数据的起始标志");
                return;

            }

            if (this.txtGetEnd.Text.Trim().ToString() == "")
            {
                this.errorProvider1.SetError(this.txtGetEnd, "请输入采集数据的结束标志");
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
                this.label6.Text = "数据库：";
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
            MessageBox.Show("数据库连接成功！", "soukey信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ConnectSqlServer()
        {
            string strDataBase = "Server=.;DataBase=Library;Uid=" + this.txtDataUser.Text.Trim() + ";pwd=" + this.txtDataPwd.Text + ";";
            SqlConnection conn = new SqlConnection(strDataBase);
            conn.Open();
            conn.Close();
            MessageBox.Show("数据库连接成功！", "soukey信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectAccess();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("数据库连接失败，请检查您输入的信息是否正确！", "soukey信息", MessageBoxButtons.OK, MessageBoxIcon.Error );
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

            this.contextMenuStrip1.Items.Add("递增变量{Num:1,100,1}");
            this.contextMenuStrip1.Items.Add("递减变量{Num:100,1,-1}");
            this.contextMenuStrip1.Items.Add(new ToolStripSeparator());
            this.contextMenuStrip1.Items.Add("字母递增{Letter:a,z}");
            this.contextMenuStrip1.Items.Add("字母递减{Letter:z,a}");
            this.contextMenuStrip1.Items.Add(new ToolStripSeparator());
            this.contextMenuStrip1.Items.Add("POST前缀<POST>");
            this.contextMenuStrip1.Items.Add("POST后缀</POST>");
            this.contextMenuStrip1.Items.Add("手工捕获POST数据");
            this.contextMenuStrip1.Items.Add(new ToolStripSeparator());

            //初始化字典菜单的项目
            cDict d = new cDict();
            int count = d.GetDictClassCount();
            

            for (int i = 0; i < count; i++)
            {
                this.contextMenuStrip1.Items.Add("字典:{Dict:" + d.GetDictClassName(i).ToString() + "}");
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
                if (this.listWeblink.SelectedItems[0].SubItems[2].Text == "是")
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
                this.errorProvider1.SetError(this.txtWebLink, "请输入网址信息");
                return;
            }
            else
            {
                if (!Regex.IsMatch(this.txtWebLink.Text.Trim().ToString(), "http://", RegexOptions.IgnoreCase))
                {
                    this.errorProvider1.SetError(this.txtWebLink, "您输入的网址不合法，请检查");
                    return;
                }
            }

            this.listWeblink.SelectedItems[0].Text = this.txtWebLink.Text.ToString();
            if (this.checkBox2.Checked == true)
            {
                this.listWeblink.SelectedItems[0].SubItems [1].Text =this.txtNag.Text.ToString();
                if (this.checkBox1.Checked == true)
                {
                    this.listWeblink.SelectedItems[0].SubItems[2].Text ="是";
                }
                else
                {
                    this.listWeblink.SelectedItems[0].SubItems[2].Text = "否";
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
                this.label6.Text = "文件名：";
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
                this.label6.Text = "文件名：";
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