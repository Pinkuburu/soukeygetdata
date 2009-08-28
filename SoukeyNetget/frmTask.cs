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

///功能：采集任务信息处理  
///完成时间：2009-3-2
///作者：一孑
///遗留问题：无
///开发计划：无
///说明：无 
///版本：01.10.00
///修订：任务格式版本已经修改至1.2
namespace SoukeyNetget
{
    public partial class frmTask : Form
    {

        public delegate void ReturnTaskClass(string tClass);
        public ReturnTaskClass rTClass;

        //是否已保存了任务，如果保存，即便在取消的时候，
        //也需要将任务所述分类进行返回，主要是用在“应用”
        //和“取消”按钮的判断上
        private bool IsSaveTask = false;

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
                //仅采集数据
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

        #region 启动初始化数据 根据启动的状态进行加载：新建、修改、浏览等
        
        private void IniData()
        {
            //初始化页面加载数据

            this.TaskType.Items.Add("根据网址采集网页数据");
            this.TaskType.Items.Add("采集ajax网页数据");
            this.TaskType.SelectedIndex = 0;

            this.comRunType.Items.Add("采集并发布数据");
            this.comRunType.Items.Add("仅采集数据");
            this.comRunType.SelectedIndex = 1;

            this.comLimit.Items.Add("不做任意格式的限制");
            this.comLimit.Items.Add("匹配时去掉网页符号");
            this.comLimit.Items.Add("只匹配中文");
            this.comLimit.Items.Add("只匹配双字节字符");
            this.comLimit.Items.Add("只匹配数字");
            this.comLimit.Items.Add("只匹配字母数字及常用字符");
            this.comLimit.Items.Add("自定义正则匹配表达式");
            this.comLimit.SelectedIndex = 0;

            this.comExportLimit.Items.Add("不做输出控制");
            this.comExportLimit.Items.Add("输出时去掉网页符号");
            this.comExportLimit.Items.Add("输出时附加前缀");
            this.comExportLimit.Items.Add("输出时附加后缀");
            this.comExportLimit.Items.Add("左起去掉字符");
            this.comExportLimit.Items.Add("右起去掉字符");
            this.comExportLimit.Items.Add("替换其中符合条件的字符");
            this.comExportLimit.Items.Add("去掉字符串首尾空格");
            this.comExportLimit.Items.Add("输出时采用正则表达式进行替换");
            this.comExportLimit.SelectedIndex = 0;

            this.comWebCode.Items.Add("自动");
            this.comWebCode.Items.Add("gb2312");
            this.comWebCode.Items.Add("UTF-8");
            this.comWebCode.Items.Add("gbk");
            this.comWebCode.Items.Add("big5");

            this.comExportUrlCode.Items.Add("不编码");
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

            this.comGetType.Items.Add("文本");
            this.comGetType.Items.Add("图片");
            this.comGetType.Items.Add("Flash");
            this.comGetType.Items.Add("文件");
            this.comGetType.SelectedIndex =0;

            this.txtSavePath.Text =Program.getPrjPath() + "data";

            this.txtGetTitleName.Items.Add("链接地址");
            this.txtGetTitleName.Items.Add("标题");
            this.txtGetTitleName.Items.Add("内容");
            this.txtGetTitleName.Items.Add("图片");
            

            //初始化页面加载时各个控件的状态


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
            //    MessageBox.Show("系统无法自动获取网页编码，可通过查看页面属性（Firefox）或查看页面编码（IE）来判断页面编码格式", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

        //任务保存，当任务保存的时候，不保存任务的模板信息任务的类别都是用户自定义
        //此功能只是用户在建立任务时的一种快速操作
        private bool SaveTask(string TaskPath)
        {

            Task.cTask t = new Task.cTask();

            //如果是编辑状态，则需要删除原有文件
            if (this.FormState == cGlobalParas.FormState.Edit)
            {
                t.TaskName = this.tTask.Text;

                if (this.comTaskClass.SelectedIndex == 0)
                {
                    try
                    {
                        //删除原有任务的主要目的是为了备份，但如果发生错误，则忽略
                        t.DeleTask("", this.tTask.Text);
                    }
                    catch (System.Exception )
                    {
                    }
                }
                else
                {
                    //获取此任务分类的路径
                    Task.cTaskClass tClass = new Task.cTaskClass();
                    string tPath=tClass.GetTaskClassPathByName(this.comTaskClass.SelectedItem.ToString());
                    try
                    {
                        //删除原有任务的主要目的是为了备份，但如果发生错误，则忽略
                        t.DeleTask(tPath, this.tTask.Text);
                    }
                    catch (System.Exception )
                    {

                    }
                }
            }

            int i = 0;
            int UrlCount = 0;

            

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

            //判断是否导出文件
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

                //数据库用户名及密码在任务1.2版本中已经删除，所以不进行保存，但代码不进行删除，因为
                //此版本需要兼容1.0，保证代码的可读性，故不删除
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
                    //捕获错误不处理，兼容1.0版本
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
                if (MessageBox.Show("在此任务没有定义需要采集的网址或者采集网址的规则，是否继续保存任务？", "soukey采摘 系统询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
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
                MessageBox.Show("任务保存时失败，出错原因是：" + ex.Message, "Soukey采摘 错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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
            //对Tooltip进行初始化设置
            HelpTip.ToolTipIcon = ToolTipIcon.Info;
            HelpTip.ForeColor =Color.YellowGreen;
            HelpTip.BackColor = Color.LightGray;
            HelpTip.AutoPopDelay = 5000;
            HelpTip.ShowAlways = true;
            HelpTip.ToolTipTitle = "";

            SetTooltip();

            //初始化导航规则的datagrid的表头
            DataGridViewTextBoxColumn nRuleLevel = new DataGridViewTextBoxColumn();
            nRuleLevel.HeaderText = "级别";
            nRuleLevel.Width = 40;
            this.dataNRule.Columns.Insert(0, nRuleLevel);

            DataGridViewTextBoxColumn nRule = new DataGridViewTextBoxColumn();
            nRule.HeaderText = "导航规则";
            nRule.Width = 170;
            this.dataNRule.Columns.Insert(1, nRule);

            DataGridViewTextBoxColumn nOUrl = new DataGridViewTextBoxColumn();
            nOUrl.HeaderText = "相对地址";
            nOUrl.Width =70;
            this.dataNRule.Columns.Insert(2, nOUrl);

            switch (this.FormState)
            {
                case cGlobalParas.FormState.New :
                    break;
                case cGlobalParas.FormState.Edit :
                    //编辑状态进来不能修改分类

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

            this.cmdCancel.Text = "返 回";

            this.cmdOK.Enabled =false ;

            this.cmdApply.Enabled = false;
        }

      
        private void GatherData()
        {


            if (this.listWeblink.Items.Count == 0 )
            {
                MessageBox.Show("在此任务没有定义需要采集的网址，无法进行采集测试工作！", "soukey采摘 系统信息", MessageBoxButtons.OK , MessageBoxIcon.Information );
                this.tabControl1.SelectedTab = this.tabControl1.TabPages[1];
                return;
            }

            if (this.listWebGetFlag.Items.Count == 0)
            {
                MessageBox.Show("在此任务没有定义需要采集数据的采集规则，无法进行采集测试工作！", "soukey采摘 系统信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.tabControl1.SelectedTab = this.tabControl1.TabPages[2];
                return;
            }

            //测试采集，根据用户定义的内容测试采集
            //验证数据是否正确
            //比部分内容有可能在下一版中单独一个页面来处理

            //判断是否已经提取了示例网址，如果没有，则进行提取
            if (this.txtWeblinkDemo.Text.ToString() == null || this.txtWeblinkDemo.Text.ToString() == "")
            {
                GetDemoUrl();
            }
                       
            this.tabControl1.SelectedTab = this.tabControl1.TabPages[4];
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

                //绑定到显示的DataGrid中
                this.dataTestGather.DataSource = dGather;

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("采集发生错误，错误信息：" + ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                this.txtNextPage.Text = "下一页";
            }
            else
            {
                if (this.txtNextPage.Text == "下一页")
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

        private void cmdWebSource_Click(object sender, EventArgs e)
        {
            if (this.txtWeblinkDemo.Text.Trim().ToString() == "")
            {
                MessageBox.Show("请首先获取示例网址，再进行源代码查看！", "Soukey提示", MessageBoxButtons.OK, MessageBoxIcon.Question);
                this.errorProvider1.Clear();
                this.errorProvider1.SetError(this.txtWeblinkDemo, "请输入网址信息");

                return;
            }

            string tmpPath = Path.GetTempPath();

            try
            {
                //获取源码需要采用cGatherWeb中的方法进行，因为有可能Url中包含
                //POST参数
                Gather.cGatherWeb cg = new SoukeyNetget.Gather.cGatherWeb();

                bool IsAjax = false;

                if (cGlobalParas.ConvertID (this.TaskType.SelectedItem.ToString ())==(int)cGlobalParas.TaskType.AjaxHtmlByUrl )
                    IsAjax =true ;

                string WebSource = cg.GetHtml(this.txtWeblinkDemo.Text, (cGlobalParas.WebCode)cGlobalParas.ConvertID(this.comWebCode.Text), this.txtCookie.Text, "", "", false,IsAjax);
                cg = null;

                //创建临时文件
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
                MessageBox.Show("获取网页源代码出错，错误信息为：" + ex.Message, "Soukey采摘 错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                if (this.listWeblink.Items[0].SubItems[2].Text == "是")
                {
                    IsAPath = true;
                }
                else
                {
                    IsAPath = false;
                }

                DemoUrl = AddDemoUrl(this.listWeblink.Items[0].Text.ToString(), IsNav, IsAPath, APath);

                //根据判断当前是否需要Url进行编码
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
            //MessageBox.Show("数据库连接成功！", "soukey信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ConnectSqlServer()
        {
            //string strDataBase = "Server=.;DataBase=Library;Uid=" + this.txtDataUser.Text.Trim() + ";pwd=" + this.txtDataPwd.Text + ";";
            //SqlConnection conn = new SqlConnection(strDataBase);
            //conn.Open();
            //conn.Close();
            //MessageBox.Show("数据库连接成功！", "soukey信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    //捕获错误不做处理，为的是兼容1.0版本的任务信息
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
                if (this.listWeblink.SelectedItems[0].SubItems[2].Text == "是")
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
                MessageBox.Show("网址无法打开，可能出错，请检查网址及导航规则。", "soukey信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            string Url=AddDemoUrl (this.txtWebLink.Text,false,false,"" );
            GetNextPageFlag(Url);

        }
        
        //自动获取下一页的标识
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

            if (this.comLimit.SelectedIndex == -1)
            {
                this.comLimit.SelectedIndex = 0;
            }

            //判断名称是否已经重复
            for (int i = 0; i < this.listWebGetFlag.Items.Count; i++)
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
            this.folderBrowserDialog1.Description = "请选择采集任务数据存储的路径：" ;
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
                    //捕获错误不做处理，为的是兼容1.0版本的任务信息
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
                if (MessageBox.Show("任务信息已经发生了修改，不保存退出？", "Soukey采摘 信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    e.Cancel = true;
                    return;
            }
        }

        #region 设置修改保存标记
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
                    this.label37.Text = "加工条件：";
                    this.txtExpression.Text = "";
                    this.txtExpression.Enabled = false;
                    break;
                case 1:
                    this.label37.Text = "加工条件：";
                    this.txtExpression.Text = "";
                    this.txtExpression.Enabled = false;
                    break;
                case 2:
                    this.label37.Text = "前缀：";
                    this.txtExpression.Text = "";
                    this.txtExpression.Enabled = true;
                    break;
                case 3:
                    this.label37.Text = "后缀：";
                    this.txtExpression.Text = "";
                    this.txtExpression.Enabled = true;
                    break;
                case 4:
                    this.label37.Text = "截取字符数：";
                    this.txtExpression.Text = "0";
                    this.txtExpression.Enabled = true;
                    break;
                case 5:
                    this.label37.Text = "截取字符数：";
                    this.txtExpression.Text = "0";
                    this.txtExpression.Enabled = true;
                    break;
                case 6:
                    this.label37.Text = "表达式：";
                    this.txtExpression.Text = "\"\",\"\"";
                    this.txtExpression.Enabled = true;
                    break;
                case 7:
                    this.txtExpression.Enabled = false;
                    break;
                case 8:
                    this.label37.Text = "正则表达式：";
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
                MessageBox.Show("数据库连接发生错误，错误信息：" + ex.Message, "Soukey采摘 错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("数据库连接发生错误，错误信息：" + ex.Message, "Soukey采摘 错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("数据库连接发生错误，错误信息：" + ex.Message, "Soukey采摘 错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (this.listWebGetFlag.Items[j].SubItems[1].Text == "文本")
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
                if (this.listWebGetFlag.Items[j].SubItems[1].Text == "文本")
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
            this.rmenuGetFormat.Items.Add("POST前缀<POST>");
            this.rmenuGetFormat.Items.Add("POST后缀</POST>");
            this.rmenuGetFormat.Items.Add("手工捕获POST数据");
            this.rmenuGetFormat.Items.Add(new ToolStripSeparator());

            for (int i = 0; i < this.listWebGetFlag.Items.Count; i++)
            {
                this.rmenuGetFormat.Items.Add("{" + this.listWebGetFlag.Items[i].Text + "}") ;
            }
        }

        private void rmenuGetFormat_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "手工捕获POST数据")
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
                MessageBox.Show("任务保存时失败，出错原因是：" + ex.Message, "Soukey采摘 错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("请填写导航规则后再添加", "Soukey采摘 信息信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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