using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using SoukeyNetget.Gather ;
using SoukeyNetget.Task;
using System.Security.Permissions;
using System.Reflection;
using SoukeyNetget.CustomControl;
using System.Runtime.InteropServices;
using SoukeyNetget.publish ;
using System.Web;

///功能：Soukey采摘主界面处理（包括线程响应事件）
///完成时间：2009-3-2
///作者：一孑
///遗留问题：代码还未整理，可能看起来比较乱
///开发计划：无
///说明：
///版本：00.90.00
///修订：无
namespace SoukeyNetget
{

    public partial class frmMain : Form
    {

        private cGatherControl m_GatherControl;
        private cPublishControl m_PublishControl;
        private bool IsTimer = true;
        private string DelName="";
        private cGlobalParas.ExitPara m_ePara = cGlobalParas.ExitPara.Exit;

        #region 窗体初始化操作
        
        //主窗体的初始化操作由外部来控制

        public frmMain()
        {
            InitializeComponent();

            //加载托盘图标
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.ShowBalloonTip(2, "soukey采摘", "已经启动", ToolTipIcon.Info); 
        }

        //此方法主要进行界面初始化的操作，无参数
        //此方法主要是初始化界面显示的内容,包括
        //树形结构,默认节点,网页打开默认的页面等等
        public void IniForm()
        {
            this.treeMenu.ExpandAll();
            TreeNode newNode;
            int i = 0;

            //初始化网页，固定地址，具体跳转由此页面来控制
            this.webBrowser.Navigate("http://www.yijie.net/softini.html");

            //开始初始化树形结构,取xml中的数据,读取任务分类
            Task.cTaskClass xmlTClass = new Task.cTaskClass();

            int TClassCount = xmlTClass.GetTaskClassCount();

            for (i = 0; i < TClassCount; i++)
            {
                newNode  =new TreeNode ();
                newNode.Tag = xmlTClass.GetTaskClassPathByID(xmlTClass.GetTaskClassID(i));
                newNode.Name = "C" + xmlTClass.GetTaskClassID(i);
                newNode.Text = xmlTClass.GetTaskClassName(i);
                newNode.ImageIndex = 0;
                newNode.SelectedImageIndex = 0;
                //this.treeMenu.SelectedNode = newNode;
                this.treeMenu.Nodes["nodTaskClass"].Nodes.Add(newNode);
                newNode=null;
            }
            xmlTClass = null;

            //将任务根节点赋路径值
            this.treeMenu.Nodes["nodTaskClass"].Tag = Program.getPrjPath() + "Tasks";

            //设置默认选择的树形结构节点为“正在运行”
            TreeNode SelectNode = new TreeNode();
            SelectNode = this.treeMenu.Nodes[0].Nodes[0];
            this.treeMenu.SelectedNode = SelectNode;
            SelectNode = null;

            //设置删除项为树形结构
            DelName = this.treeMenu.Name;

        }

        //此方法主要是初始化系统对象,包括需要初始化消息事件,初始化采集
        //任务控制器,并加载运行区的数据,如果运行区无数据,则初始化一个空
        //的对象
        public void UserIni()
        {

            //初始化一个采集任务的控制器,采集任务由此控制器来负责采集任务
            //管理
            m_GatherControl = new cGatherControl();

            //加载运行区的数据,运行区的数据主要是根据taskrun.xml(默认在Tasks\\TaskRun.xml)文件中
            //的内容进行加载,

            //首先判断TaskRun.xml文件是否存在,不存在则建立一个
            if (!System.IO.File.Exists(Program.getPrjPath() + "Tasks\\taskrun.xml"))
            {
                CreateTaskRun();
            }

            cTaskDataList gList = new cTaskDataList();
            gList.LoadTaskRunData ();
            
            //根据加载的运行区的任务信息,开始初始化采集任务
            //try
            //{
                m_GatherControl.AddGatherTask(gList);
            //}
            //catch (System.Exception ex)
            //{

            //}

            //采集控制器事件绑定,绑定后,页面可以响应采集任务的相关事件
            m_GatherControl.TaskManage.TaskCompleted += tManage_Completed;
            m_GatherControl.TaskManage.TaskStarted += tManage_TaskStart;
            m_GatherControl.TaskManage.TaskInitialized += tManage_TaskInitialized;
            m_GatherControl.TaskManage.TaskStateChanged += tManage_TaskStateChanged;
            m_GatherControl.TaskManage.TaskStopped += tManage_TaskStop;
            m_GatherControl.TaskManage.TaskError += tManage_TaskError;
            m_GatherControl.TaskManage.TaskFailed += tManage_TaskFailed;
            m_GatherControl.TaskManage.Log += tManage_Log;
            m_GatherControl.TaskManage.GData += tManage_GData;

            m_GatherControl.Completed += m_Gather_Completed;

            //开始加载正在导出的任务信息
            m_PublishControl = new cPublishControl();

            //注册发布任务的事件
            m_PublishControl.PublishManage.PublishCompleted += this.Publish_Complete;
            m_PublishControl.PublishManage.PublishError += this.Publish_Error;
            m_PublishControl.PublishManage.PublishFailed += this.Publish_Failed;
            m_PublishControl.PublishManage.PublishStarted  += this.Publish_Started;

            //更新状态条信息
            UpdateStatebarTask();


        }

 #endregion

        #region 菜单 工具条 树形结构 listview 等控件的 响应事件

       
        private void rmenuAddTaskClass_Click(object sender, EventArgs e)
        {
            NewTaskClass();
        }

        private void toolNewTask_ButtonClick(object sender, EventArgs e)
        {
            NewTask();
        }

        //启动任务，当前设计是只能启动一个任务，不支持启动多个任务
        private void toolStartTask_Click(object sender, EventArgs e)
        {

            if (int.Parse(this.dataTask.SelectedCells[7].Value.ToString()) > 0 && this.treeMenu.SelectedNode.Name.ToString () =="nodRunning")
            {
                cGatherTask t = m_GatherControl.TaskManage.FindTask(Int64.Parse(this.dataTask.SelectedCells[1].Value.ToString()));
                t.ResetTask();

                //删除Tabpage
                string pageName = "page" + t.TaskID;
                string conName="sCon" + t.TaskID ;

                ((cMyDataGridView)this.tabControl1.TabPages[pageName].Controls[conName].Controls[0].Controls[0]).Clear();

            }

            StartTask();
            
        }

        private void StartTask()
        {
            cGatherTask t = null;

            if (this.dataTask.Rows.Count == 0)
                return;

            //判断当前选择的树节点
            if (this.treeMenu.SelectedNode.Name == "nodRunning" && this.dataTask.SelectedCells.Count != 0)
            {
                //执行正在执行的任务
                t = m_GatherControl.TaskManage.FindTask(Int64.Parse(this.dataTask.SelectedCells[1].Value.ToString()));

            }
            else if (this.treeMenu.SelectedNode.Name.Substring(0, 1) == "C" || this.treeMenu.SelectedNode.Name == "nodTaskClass")
            {
                ///如果是选择的任务分类节点，点击此按钮首先先将此任务加载到运行区，然后调用
                ///starttask方法，启动任务。
                t = AddRunTask();
                if (t == null)
                {
                    //表示启动任务被用户中断，也有可能是因为错误造成
                    return;
                }
            }

            //判断此任务是否需要登录，如果需要登录则需要用户干预
            if (t.TaskData.IsLogin == true)
            {
                frmWeblink f = new frmWeblink(t.TaskData.LoginUrl);
                f.Owner = this;
                f.rCookie = new frmWeblink.ReturnCookie(GetCookie);
                f.ShowDialog();
                f.Dispose();

                t.UpdateCookie(this.Cookie);

            }

            //任务成功启动后，需要建立TabPage用于显示此任务的日志及采集数据的信息
            AddTab(t.TaskID,t.TaskName );

            //启动此任务
            m_GatherControl.Start(t);

            //任务启动成功显示消息
            ShowInfo("任务启动", t.TaskName);
        }

        //通过用户登录获取cookie
        private string m_Cookie;
        private string Cookie
        {
            get { return m_Cookie; }
            set { m_Cookie = value; }
        }

        private void GetCookie(string strCookie)
        {
            this.Cookie= strCookie;
        }

        private void treeMenu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (e.Node.Name)
            {
                case "nodRunning":   //运行区的任务
                    try
                    {
                        LoadRunTask(e.Node.Name);
                    }
                    catch (System.IO.IOException)
                    {
                        if (MessageBox.Show("任务运行监控文件丢失，请问是否根据正在运行的任务情况自动创建？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            CreateTaskRun();
                        }
                    }
                    catch (System.Exception)
                    {
                        MessageBox.Show("加载的任务运行监控文件非法，请检查此文件“" + Program.getPrjPath () + "tasks\\taskrun.xml" + "”，如果格式非法，请通过Windows文件浏览器删除，并重新点击此节点由系统自动建立！", "系统信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }

                    SetDataShow();

                    //启动时间器用于更新任务显示的进度
                    this.timer1.Enabled = true;

                    //置删除按钮为无效
                    this.toolDelTask.Enabled = false;

                    break;

                case "nodPublish":

                    LoadPublishTask();

                    //启动时间器用于更新任务显示的进度
                    this.timer1.Enabled = true;

                    //置删除按钮为无效
                    this.toolDelTask.Enabled = false;

                    SetDataShow();

                    break;

                case "nodComplete":    //已经完成采集的任务

                    try
                    {
                        LoadCompleteTask();
                    }
                    catch (System.IO.IOException)
                    {
                        if (MessageBox.Show("已完成任务索引文件丢失，需要重新建立，但已经完成的任务信息将会丢失，数据内容不会丢失，默认存储在：" + Program.getPrjPath () + "Data" + "目录下，是否建立？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            CreateTaskComplete ();
                        }
                    }
                    catch (System.Exception)
                    {
                        MessageBox.Show("加载的已完成任务索引文件非法，请检查此文件“" + Program.getPrjPath () + "data\\index.xml" + "”，如果格式非法，请通过Windows文件浏览器删除，并重新点击此节点由系统自动建立！", "系统信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    };

                    SetDataShow();

                    this.timer1.Enabled = false;

                    //置删除按钮为无效
                    this.toolDelTask.Enabled = false;

                    break;

                default:
                    try
                    {
                        LoadOther(e.Node);
                    }
                    catch (System.IO.IOException)
                    {
                        if (MessageBox.Show(this.treeMenu.SelectedNode.Text + "分类下的索引文件丢失，请问是否自动创建？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            CreateTaskIndex(this.treeMenu.SelectedNode.Tag.ToString());
                        }
                    }
                    catch (System.Exception)
                    {
                        MessageBox.Show("加载的任务分类索引文件非法，请检查此文件“" + e.Node.Tag.ToString() + "\\index.xml" + "”，如果格式非法，请通过Windows文件浏览器删除，并重新点击此节点由系统自动建立！", "系统信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }

                    SetDataHide();

                    //置删除按钮为无效
                    this.toolDelTask.Enabled = false ;

                    this.timer1.Enabled = false;
                    break;
            }

           //无论点击树形结构的菜单都将按钮置为不可用
            this.toolStartTask.Enabled = false;
            this.toolRestartTask.Enabled = false;
            this.toolStopTask.Enabled = false;
            this.toolExportData.Enabled = false;
            this.toolBrowserData.Enabled = false;

            UpdateStatebarTaskState("当前显示： " + e.Node.Text );

        }

        //树形结构的设置确实很不合理，不过真的是懒的改了，下一个版本会修正
        //因为下一个版本会扩充树形结构的应用，不得不改了，呵呵
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            switch (this.treeMenu.SelectedNode.Name)
            {
                case "nodRunning":
                    this.rmmenuStopTask.Enabled = true;
                    this.rmenuAddTaskClass.Enabled = true;
                    this.rmenuDelTaskClass.Enabled = false;
                    break;
                case "nodExportData":
                    this.rmmenuStopTask.Enabled = true;
                    this.rmenuAddTaskClass.Enabled = true;
                    this.rmenuDelTaskClass.Enabled = false;
                    break;
                case "nodeSnap":
                    break;
                default:
                    if (this.treeMenu.SelectedNode.Name.Substring(0, 1) == "C")
                    {
                        this.rmmenuStopTask.Enabled = false;
                        this.rmenuAddTaskClass.Enabled = true;
                        this.rmenuDelTaskClass.Enabled = true;
                    }
                    break;
            }
        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {
            switch (this.treeMenu.SelectedNode.Name)
            {
                case "nodRunning":
                   
                    cGatherTask t = m_GatherControl.TaskManage.FindTask((Int64)this.dataTask.SelectedCells[1].Value);
                    if (t.TaskState == cGlobalParas.TaskState.Started)
                    {
                        this.rmmenuStartTask.Enabled = false;
                        this.rmmenuStopTask.Enabled = true;
                        this.rmmenuRestartTask.Enabled = true;
                    }
                    else
                    {
                        this.rmmenuStartTask.Enabled = true;
                        this.rmmenuStopTask.Enabled = false;
                        this.rmmenuRestartTask.Enabled = false;
                    }
                    t = null;

                    this.rmenuBrowserData.Enabled = false;
                    this.rmmenuNewTask.Enabled = false;
                    this.rmmenuEditTask.Enabled = false;
                    this.rmmenuDelTask.Enabled = true;
                    break;
                case "nodComplete":
                    this.rmmenuStartTask.Enabled = false;
                    this.rmmenuStopTask.Enabled = false;
                    this.rmmenuRestartTask.Enabled = false;
                    this.rmmenuNewTask.Enabled = false;
                    this.rmmenuDelTask.Enabled = true;
                    this.rmmenuEditTask.Enabled = false;
                    if (this.dataTask.Rows.Count == 0)
                    {
                        this.rmenuBrowserData.Enabled = false;
                        this.rmmenuDelTask.Enabled = false;
                    }
                    else
                    {
                        this.rmenuBrowserData.Enabled = true;
                        this.rmmenuDelTask.Enabled = true;
                    }
                    break;
                case "nodPublish":
                    this.rmmenuStartTask.Enabled = false;
                    this.rmmenuStopTask.Enabled = false;
                    this.rmmenuRestartTask.Enabled = false;
                    this.rmmenuNewTask.Enabled = false;
                    this.rmmenuEditTask.Enabled = false;
                    this.rmmenuDelTask.Enabled = false;
                    this.rmenuBrowserData.Enabled = true;
                    break;
                default :
                    this.rmmenuStartTask.Enabled = true;
                    this.rmmenuStopTask.Enabled = false;
                    this.rmmenuRestartTask.Enabled = false;
                    this.rmmenuNewTask.Enabled = true;
                    this.rmmenuEditTask.Enabled = true;
                    this.rmmenuDelTask.Enabled = true;
                    this.rmenuBrowserData.Enabled = false;
                    break;
            }
        }

        private void rmenuDelTaskClass_Click(object sender, EventArgs e)
        {
            DelTaskClass();
        }

        private void treeMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                try
                {
                    DelTaskClass();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "系统信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

            }
        }

        private void rmmenuNewTask_Click(object sender, EventArgs e)
        {
            NewTask();
        }

        private void rmmenuEditTask_Click(object sender, EventArgs e)
        {
            EditTask();
        }

        private void rmmenuDelTask_Click(object sender, EventArgs e)
        {
            //首先判断当前焦点的控件
            //然后再确定删除的是分类还是任务


            //删除的是任务，但需要判断删除的是何种任务
            switch (this.treeMenu.SelectedNode.Name)
            {
                case "nodRunning":
                    DelRunTask();
                    break;
                case "nodComplete":
                    DelCompletedTask();
                    break;
                case "nodPublish":
                    break;
                default:
                    DelTask();
                    break;
            }
           
        }

        private void myListData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DelTask() == true)
                {
                    //this.myListData.Items.Remove(this.myListData.SelectedItems[0]);
                }
            }
        }


        #endregion

        #region 界面控件事件 所调用的方法
        private void NewTaskClass()
        {
            frmTaskClass frmTClass = new frmTaskClass();
            frmTClass.RTaskClass = new frmTaskClass.ReturnTaskClass(AddTaskClassNode);
            frmTClass.ShowDialog();
            frmTClass.Dispose();
        }

        //当添加任务分类后，根据新添加的信息，开始添加任务分类树形结构
        private void AddTaskClassNode(int TaskClassID, string TaskClassName, string TaskClassPath)
        {
            TreeNode newNode = new TreeNode();
            newNode.Tag = TaskClassPath;
            newNode.Name = "C" + TaskClassID;
            newNode.Text = TaskClassName;
            newNode.ImageIndex = 0;
            newNode.SelectedImageIndex = 0;
            this.treeMenu.Nodes["nodTaskClass"].Nodes.Add(newNode);
            this.treeMenu.SelectedNode = newNode;

        }

        private void NewTask()
        {
            string TClass = "";
            if (this.treeMenu.SelectedNode.Name.Substring(0, 1) == "C")
            {
                //表示选择的是分类节点
                TClass = this.treeMenu.SelectedNode.Text;
            }
            frmTask fTask = new frmTask();
            fTask.NewTask(TClass);
            fTask.FormState = cGlobalParas.FormState.New;
            fTask.rTClass = refreshNode;
            fTask.ShowDialog();
            fTask.Dispose();

            //刷新一下当前所显示的节点信息，因为已经新增了一个任务

        }

        private void refreshNode(string TClass)
        {
            if (TClass == "")
            {
                LoadTask(this.treeMenu.Nodes["nodTaskClass"]);
                return;
            }
            
            foreach (TreeNode a in this.treeMenu.Nodes["nodTaskClass"].Nodes)
            {
                if (a.Text ==TClass)
                {
                    LoadTask (a);
                    break ;
                }
            }

        }

        private void CreateTaskIndex(string tPath)
        {
            Task.cTaskIndex tIndex = new Task.cTaskIndex();
            tIndex.NewIndexFile(tPath);
            tIndex = null;

        }

        private void CreateTaskRun()
        {
            Task.cTaskRun tRun = new Task.cTaskRun();
            tRun.NewTaskRunFile();
            tRun = null;
        }

        private void CreateTaskComplete()
        {
            Task.cTaskComplete t = new Task.cTaskComplete();
            t.NewTaskCompleteFile();
            t = null;
        }

        private void LoadPublishTask()
        {
            ShowPublishTask();

            ///任务发布做的很简单，当任务采集完成后，自动启动开始进行数据的发布，
            /// 不允许进行人工干预，当前认为此种发布方式不具备太大的实用性，所以
            /// 当前的作为是一种临时的做法，后期会逐步完善，希望可以找到合适的发布
            /// 方式
            ///需要发布的数据不进行本地文件的保存，直接保存在m_PublishControl中

            foreach (cPublishTask t in m_PublishControl.PublishManage.ListPublish)
            {
                dataTask.Rows.Add(imageList1.Images["export"], t.TaskData.TaskID , cGlobalParas.TaskState.Publishing, this.treeMenu.SelectedNode.Name,
                    t.TaskData.TaskName, t.PublishedCount, t.Count, (t.Count ==0? 0:t.PublishedCount * 100 / t.Count),
                                   cGlobalParas.ConvertName ((int) t.PublishType));
                                  
            }

            this.dataTask.ClearSelection();

        }

        private void LoadCompleteTask()
        {
            ShowCompletedTask();

            //从完成的任务中加载
            cTaskComplete t = new cTaskComplete();
            t.LoadTaskData();

            for (int i = 0; i < t.GetCount(); i++)
            {
                dataTask.Rows.Add(imageList1.Images["OK"],t.GetTaskID (i), cGlobalParas.TaskState.Completed, this.treeMenu.SelectedNode.Name,
                                   t.GetTaskName (i), cGlobalParas.ConvertName((int)t.GetTaskType (i)),
                                   //t.GetGatheredUrlCount(i), t.GetUrlCount(i) - t.GetGatheredUrlCount(i),
                                   t.GetUrlCount(i), cGlobalParas.ConvertName((int)t.GetTaskRunType(i)),
                                   cGlobalParas.ConvertName((int)t.GetPublishType(i)) );
            }

            this.dataTask.ClearSelection();

        }

        //加载正在执行的任务，正在执行的任务记录在应用程序目录下的RunningTask.xml文件中
        private void LoadRunTask(string nodKey)
        {

            ShowRunTask();

            //开始初始化正在运行的任务
            //从m_TaskControl中读取
            //每次加载会加载正在运行、等待、停止队列中的任务
            List<cGatherTask> taskList=new List<cGatherTask>();
            taskList.AddRange(m_GatherControl.TaskManage.TaskListControl.RunningTaskList);
            taskList.AddRange(m_GatherControl.TaskManage.TaskListControl.StoppedTaskList);
            taskList.AddRange(m_GatherControl.TaskManage.TaskListControl.WaitingTaskList);

            for (int i = 0; i < taskList.Count ; i++)
            {
                switch (taskList[i].State)
                {
                    case cGlobalParas.TaskState.Started:
                        dataTask.Rows.Add(imageList1.Images["started"],taskList[i].TaskID , taskList[i].State , this.treeMenu.SelectedNode.Name,
                            taskList[i].TaskName,cGlobalParas.ConvertName ((int) taskList[i].TaskType),(taskList[i].IsLogin ==true ? "登录":"不登录"),
                            taskList[i].GatheredUrlCount, taskList[i].UrlCount, taskList[i].GatheredUrlCount * 100 / taskList[i].UrlCount, cGlobalParas.ConvertName((int)taskList[i].RunType),
                            cGlobalParas.ConvertName((int)taskList[i].PublishType));
                        break ;

                    case cGlobalParas.TaskState.Stopped :
                        if (taskList[i].GatheredUrlCount > 0)
                        {
                            dataTask.Rows.Add(imageList1.Images["pause"], taskList[i].TaskID, taskList[i].State, this.treeMenu.SelectedNode.Name,
                                taskList[i].TaskName, cGlobalParas.ConvertName((int)taskList[i].TaskType), (taskList[i].IsLogin == true ? "登录" : "不登录"),
                                taskList[i].GatheredUrlCount, taskList[i].UrlCount, taskList[i].GatheredUrlCount * 100 / taskList[i].UrlCount, cGlobalParas.ConvertName((int)taskList[i].RunType),
                                cGlobalParas.ConvertName((int)taskList[i].PublishType ));
                        }
                        else
                        {
                            dataTask.Rows.Add(imageList1.Images["stop"], taskList[i].TaskID, taskList[i].State, this.treeMenu.SelectedNode.Name,
                                taskList[i].TaskName, cGlobalParas.ConvertName((int)taskList[i].TaskType), (taskList[i].IsLogin == true ? "登录" : "不登录"),
                                taskList[i].GatheredUrlCount, taskList[i].UrlCount, taskList[i].GatheredUrlCount * 100 / taskList[i].UrlCount, cGlobalParas.ConvertName((int)taskList[i].RunType),
                                cGlobalParas.ConvertName((int)taskList[i].PublishType));
                        }
                        break;
                    case cGlobalParas.TaskState.UnStart :
                        dataTask.Rows.Add(imageList1.Images["stop"], taskList[i].TaskID, taskList[i].State, this.treeMenu.SelectedNode.Name,
                            taskList[i].TaskName, cGlobalParas.ConvertName((int)taskList[i].TaskType), (taskList[i].IsLogin == true ? "登录" : "不登录"),
                            taskList[i].GatheredUrlCount, taskList[i].UrlCount, taskList[i].GatheredUrlCount * 100 / taskList[i].UrlCount, cGlobalParas.ConvertName((int)taskList[i].RunType),
                            cGlobalParas.ConvertName((int)taskList[i].PublishType));
                        break;
                    case cGlobalParas.TaskState.Failed:
                        dataTask.Rows.Add(imageList1.Images["error"], taskList[i].TaskID, taskList[i].State, this.treeMenu.SelectedNode.Name,
                            taskList[i].TaskName, cGlobalParas.ConvertName((int)taskList[i].TaskType), (taskList[i].IsLogin == true ? "登录" : "不登录"),
                            taskList[i].GatheredUrlCount, taskList[i].UrlCount, taskList[i].GatheredUrlCount * 100 / taskList[i].UrlCount, cGlobalParas.ConvertName((int)taskList[i].RunType),
                            cGlobalParas.ConvertName((int)taskList[i].PublishType));
                        break;
                    default:
                        dataTask.Rows.Add(imageList1.Images["stop"], taskList[i].TaskID, taskList[i].State, this.treeMenu.SelectedNode.Name,
                            taskList[i].TaskName, cGlobalParas.ConvertName((int)taskList[i].TaskType), (taskList[i].IsLogin == true ? "登录" : "不登录"),
                            taskList[i].GatheredUrlCount, taskList[i].UrlCount, taskList[i].GatheredUrlCount * 100 / taskList[i].UrlCount, cGlobalParas.ConvertName((int)taskList[i].RunType),
                            cGlobalParas.ConvertName((int)taskList[i].PublishType));
                        break;
                }

            }

            this.dataTask.ClearSelection();

        }

        //此部分的数据是根据当前已经完成的导出任务
        //实时产生的数据
        private void LoadExportDataTask(string mNode)
        {
            //this.myListData.Items.Clear();
        }

        private void LoadOther(TreeNode mNode)
        {
            //任务分类作为一个特殊的分类进行处理
            //此节点所有的内容全部为默认，不提供用户可操作的功能
            if (mNode.Name.Substring(0, 1) == "C" || mNode.Name == "nodTaskClass")
            {
                //表示加载的是任务信息
                LoadTask(mNode);
            }
            else
            {

            }
        }

        private void LoadTask(TreeNode  mNode)
        {
            ShowTaskInfo();


            Task.cTaskIndex xmlTasks = new Task.cTaskIndex();

            if (mNode.Name == "nodTaskClass")
            {
                xmlTasks.GetTaskDataByClass();
            }
            else
            {
                //因可能无法保证id的唯一性，所以，所有的内容全部都按照名称索取
                string TaskClassName = mNode.Text;
                xmlTasks.GetTaskDataByClass(TaskClassName);
            }

            //开始初始化此分类下的任务
            int count = xmlTasks.GetTaskClassCount();

            for (int i = 0; i < count; i++)
            {
                dataTask.Rows.Add(imageList1.Images["task"], xmlTasks.GetTaskID(i), cGlobalParas.TaskState.UnStart, this.treeMenu.SelectedNode.Name, xmlTasks.GetTaskName(i),
                    cGlobalParas.ConvertName(int.Parse(xmlTasks.GetTaskType(i).ToString())), (xmlTasks.GetIsLogin(i) == true ? "登录" : "不登录"),
                   xmlTasks.GetWebLinkCount(i).ToString(), cGlobalParas.ConvertName(int.Parse(xmlTasks.GetTaskRunType(i).ToString())), 
                   cGlobalParas.ConvertName((int)xmlTasks.GetPublishType (i)));
            }
            xmlTasks = null;

            this.dataTask.ClearSelection();

        }

        private void LoadTaskInfo(string FilePath, string FileName ,cGlobalParas.FormState fState)
        {

            frmTask ft = new frmTask();
            ft.EditTask(FilePath, FileName);
            ft.FormState = fState;
            ft.rTClass = refreshNode;
            ft.ShowDialog();
            ft.Dispose();
                
        }

        private void DelTaskClass()
        {
            if (MessageBox.Show("您选择删除分类：" + this.treeMenu.SelectedNode.Text   + "，删除分类只是将此分类下的索引（index.xml）文件删除，" +
               "任务的信息予以保留，您可以备份这些任务，或者导入到其他分类下，此分类的目录地址为：" + this.treeMenu.SelectedNode.Tag.ToString() + "，您是否继续执行删除操作？",
               "系统询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    Task.cTaskClass tClass = new Task.cTaskClass();
                    
                    if (tClass.DelTaskClass(this.treeMenu.SelectedNode.Text))
                    {
                        tClass = null;
                    }
               
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "系统信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                this.treeMenu.Nodes.Remove(this.treeMenu.SelectedNode);
            }


           
        }

        private bool DelTask()
        {
            if (this.dataTask.Rows.Count ==0)
            {
                return false;
            }

            if (this.treeMenu.SelectedNode.Name.Substring(0, 1) == "C" || this.treeMenu.SelectedNode.Name == "nodTaskClass")
            {
                //表示选择的是任务节点
                try
                {
                    if (MessageBox.Show("您选择删除任务：" + this.dataTask.SelectedCells[4].Value + "，如果此任务正在运行，删除操作不会造成影响，但删除后此任务不可恢复，继续？", "系统询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Task.cTask t = new Task.cTask();
                        t.DeleTask(this.treeMenu.SelectedNode.Tag.ToString(), this.dataTask.SelectedCells[4].Value.ToString ());
                        t = null;

                        this.dataTask.Rows.Remove(this.dataTask.SelectedRows[0]);

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "系统信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return false ;
                }
            }

            return false;

        }
        
        /// <summary>
        /// 删除正在运行的任务
        /// </summary>
        private void DelRunTask()
        {
            if (this.dataTask.Rows.Count == 0)
                return;

            if(MessageBox.Show ("您确认删除任务：" + this.dataTask.SelectedCells[4].Value.ToString () + "，删除运行区的任务不会影响此任务已经采集完成的数据，是否继续？","系统询问",
                MessageBoxButtons.YesNo ,MessageBoxIcon.Question )==DialogResult.No )
            {
                return ;
            }

            cGatherTask t = m_GatherControl.TaskManage.FindTask(Int64.Parse(this.dataTask.SelectedCells[1].Value.ToString()));

            m_GatherControl.Remove(t);

            Int64 TaskID = Int64.Parse(this.dataTask.SelectedCells[1].Value.ToString());


            //删除taskrun节点
            cTaskRun tr = new cTaskRun();
            tr.LoadTaskRunData();
            tr.DelTask(TaskID);
            tr=null;

            //删除run中的任务实例文件
            string FileName = Program.getPrjPath() + "Tasks\\run\\" + "Task" + TaskID + ".xml";
            System.IO.File.Delete(FileName);

            if (this.treeMenu.SelectedNode.Name == "nodRunning")
            {
                for (int i = 0; i < this.dataTask.Rows.Count; i++)
                {
                    if (this.dataTask.Rows[i].Cells[1].Value.ToString() == TaskID.ToString())
                    {
                        this.dataTask.Rows.Remove(this.dataTask.Rows[i]);
                        break;
                    }
                }
            }
            

        }

        //删除已经完成的任务
        private void DelCompletedTask()
        {
            if (this.dataTask.Rows.Count == 0)
                return;

            if (MessageBox.Show("您确认删除任务：" + this.dataTask.SelectedCells[4].Value.ToString() + "，删除已经完成的任务将会删除已经采集的数据，您可在删除任务前导出数据，是否继续？", "系统询问",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            Int64 TaskID = Int64.Parse(this.dataTask.SelectedCells[1].Value.ToString());
            string TaskName = this.dataTask.SelectedCells[4].Value.ToString();

            //删除taskcomplete节点
            cTaskComplete tc = new cTaskComplete();
            tc.LoadTaskData();
            tc.DelTask (TaskID );
            tc = null; 

            //删除run中的任务实例文件
            string FileName = Program.getPrjPath() + "data\\" + TaskName + "-" + TaskID + ".xml";
            System.IO.File.Delete(FileName);

            if (this.treeMenu.SelectedNode.Name == "nodComplete")
            {
                for (int i = 0; i < this.dataTask.Rows.Count; i++)
                {
                    if (this.dataTask.Rows[i].Cells[1].Value.ToString() == TaskID.ToString())
                    {
                        this.dataTask.Rows.Remove(this.dataTask.Rows[i]);
                        break;
                    }
                }
            }
        }

        #endregion

        #region 窗体控制

        private void frmMain_Resize(object sender, EventArgs e)
        {
            //根据窗体变化来调整控件布局
            this.toolStrip2.Width = 300;
            this.toolStrip2.Left = this.Width - this.toolStrip2.Width;
        }

         #endregion

    
        #region 事件处理

        public event EventHandler<cTaskEventArgs> Completed
        {
            add
            {
                m_GatherControl.Completed += value;
            }
            remove
            {
                m_GatherControl.Completed -= value;
            }
        }

        public event EventHandler<cTaskEventArgs> TaskCompleted
        {
            add
            {
                m_GatherControl.TaskManage.TaskCompleted += value;
            }
            remove
            {
                m_GatherControl.TaskManage.TaskCompleted -= value;
            }
        }

        private void tManage_Completed(object sender, EventArgs e)
        {
            //任务执行完毕后，需要将任务移至已经完成的节点中，
            //在此如果选择的是nodRunning则删除datagridview的内容
            //然后添加到完成队列中
            cGatherTask t = (cGatherTask)sender;
            InvokeMethod(this, "ShowInfo", new object[] { "任务采集完成", t.TaskName });

            //判断此任务是否需要自动导出数据
            //if (t.PublishType == cGlobalParas.PublishType.NoPublish)
            //{
            //    //不自动发布数据
            //    InvokeMethod(this, "UpdateTaskComplete", new object[] { t.TaskID });

            //}
            //else
            //{
                //自动发布数据
            InvokeMethod(this, "UpdateTaskPublish", new object[] { t.TaskID });
            //}

            t = null;

            InvokeMethod(this, "UpdateStatebarTask", null);

            //任务采集完成后看是否需要进行发布
            //任务发布是一个人工不得干预的过程，且无事务保证，如果失败，则可能会造成一定的问题
            //即有可能出现重复数据


        }

        

        //处理任务采集完成的工作,但在此的处理是需要发布的任务
        public void UpdateTaskPublish(Int64 TaskID)
        {
            //将此任务添加到发布队列中

            string conName = "sCon" + TaskID;
            string pageName = "page" + TaskID;

            DataTable d = (DataTable)((DataGridView)this.tabControl1.TabPages[pageName].Controls[conName].Controls[0].Controls[0]).DataSource;

            cPublishTask pt = new cPublishTask(m_PublishControl.PublishManage, TaskID,d);
            m_PublishControl.startPublish(pt);


            if (this.treeMenu.SelectedNode.Name == "nodRunning")
            {
                for (int i = 0; i < this.dataTask.Rows.Count; i++)
                {
                    if (this.dataTask.Rows[i].Cells[1].Value.ToString() == TaskID.ToString())
                    {
                        this.dataTask.Rows.Remove(this.dataTask.Rows[i]);
                        break;
                    }
                }
            }
            else if (this.treeMenu.SelectedNode.Name == "nodPublish")
            {
                //重新加载已完成任务的信息
                LoadPublishTask();
            }


        }


        //处理任务采集完成后的工作，首先如果选择的正在运行的节点，则
        //删除此节点,然后从taskrun数据中删除,然后在删除实际的文件
        public void UpdateTaskComplete(int TaskID)
        {
            //将已经完成的任务添加到完成任务的索引文件中
            Task.cTaskComplete t = new Task.cTaskComplete();
            t.InsertTaskComplete(TaskID, cGlobalParas.GatherResult.GatherSucceed );
            t = null;

            //删除taskrun节点
            cTaskRun tr = new cTaskRun();
            tr.LoadTaskRunData();
            tr.DelTask(TaskID);

            //删除run中的任务实例文件
            string FileName = Program.getPrjPath() + "Tasks\\run\\" + "Task" + TaskID + ".xml";
            System.IO.File.Delete(FileName);

            if (this.treeMenu.SelectedNode.Name == "nodRunning")
            {
                for (int i = 0; i < this.dataTask.Rows.Count; i++)
                {
                    if (this.dataTask.Rows[i].Cells[1].Value.ToString() == TaskID.ToString())
                    {
                        this.dataTask.Rows.Remove(this.dataTask.Rows[i]);
                        break;
                    }
                }
            }
            else if (this.treeMenu.SelectedNode.Name == "nodComplete")
            {
                //重新加载已完成任务的信息
                LoadCompleteTask();
            }

        }

        private void tManage_TaskStart(object sender, EventArgs e)
        {
            //如果任务启动，则修改任务的图标，此事件是由点击按钮后任务
            //启动进行触发
            if (this.dataTask.SelectedCells[3].Value.ToString() == "nodRunning")
            {
                //cGatherTask gt = (cGatherTask)sender;
                this.dataTask.SelectedCells[0].Value = imageList1.Images["started"];
                SetValue(this.toolStrip1.Items["toolStartTask"], "Enabled", false);
                SetValue(this.toolStrip1.Items["toolRestartTask"], "Enabled", false);
                SetValue(this.toolStrip1.Items["toolStopTask"], "Enabled", true);
                //gt = null;
            }

            UpdateStatebarTask();
        }

        private void tManage_TaskInitialized(object sender, TaskInitializedEventArgs e)
        {
            //暂不做任何处理

            UpdateStatebarTask();

        }

        private void tManage_TaskStateChanged(object sender, TaskStateChangedEventArgs e)
        {
            SetTaskShowState(e.TaskID, e.NewState);

            UpdateStatebarTask();
 
        }

        private void tManage_TaskStop(object sender, cTaskEventArgs e)
        {

           //如果任务启动，则修改任务的图标，此事件是由点击按钮后任务
            //启动进行触发
            if (this.dataTask.SelectedCells[3].Value.ToString() == "nodRunning")
            {
                //cGatherTask gt = (cGatherTask)sender;
                if (int.Parse(dataTask.SelectedCells[7].Value.ToString()) > 0)
                {
                    this.dataTask.SelectedCells[0].Value = imageList1.Images["pause"];
                }
                else
                {
                    this.dataTask.SelectedCells[0].Value = imageList1.Images["stop"];
                }
                SetValue(this.toolStrip1.Items["toolStartTask"], "Enabled", true);
                SetValue(this.toolStrip1.Items["toolRestartTask"], "Enabled", false);
                SetValue(this.toolStrip1.Items["toolStopTask"], "Enabled", false);
                //gt = null;
            }

            UpdateStatebarTask();
           
        }

        private void tManage_TaskError(object sender, TaskErrorEventArgs e)
        {
            cGatherTask t = (cGatherTask)sender;
            InvokeMethod(this, "ShowInfo", new object[] { "任务采集错误", t.TaskName });
            t = null;

            InvokeMethod(this, "UpdateStatebarTask", null);
        }

        private void tManage_TaskFailed(object sender, EventArgs e)
        {
            cGatherTask t = (cGatherTask)sender;
            InvokeMethod(this, "ShowInfo", new object[] { "任务采集失败", t.TaskName });
            t = null;

            InvokeMethod(this, "UpdateStatebarTask", null);
        }

        private void m_Gather_Completed(object sender, EventArgs e)
        {
            //任务采集完成，则启动消息通知窗体，通知用户


        }

        //写日志事件
        private void tManage_Log(object sender, cGatherTaskLogArgs e)
        {
            //写日志
            Int64 TaskID = e.TaskID;
            string strLog = e.strLog;
            string conName="sCon" + TaskID ;
            string pageName="page" + TaskID ;

            SetValue(this.tabControl1.TabPages[pageName].Controls[conName].Controls[1].Controls[0], "Text", strLog);

        }

        //写数据事件
        private void tManage_GData(object sender, cGatherDataEventArgs e)
        {
            //写采集数据到界面Datagridview
            Int64 TaskID = e.TaskID;
            DataTable gData = e.gData;
            string conName="sCon" + TaskID ;
            string pageName="page" + TaskID ;

            SetValue(this.tabControl1.TabPages[pageName].Controls[conName].Controls[0].Controls[0], "gData", gData);

        }

        #endregion

        #region 委托代理 用于后台线程调用 配置UI线程的方法、属性

        delegate void bindvalue(object Instance, string Property, object value);
        delegate object invokemethod(object Instance, string Method, object[] parameters);
        delegate object invokepmethod(object Instance, string Property, string Method, object[] parameters);
        delegate object invokechailmethod(object InstanceInvokeRequired, object Instance, string Method, object[] parameters);

        /// <summary>
        /// 委托设置对象属性
        /// </summary>
        /// <param name="Instance">对象</param>
        /// <param name="Property">属性名</param>
        /// <param name="value">属性值</param>
        private void SetValue(object Instance, string Property, object value)
        {
            Type iType = Instance.GetType();
            object inst;

            if (iType.Name.ToString() == "ToolStripButton")
            {
                inst = this.toolStrip1;
            }
            else
            {
                inst = Instance;
            }

            bool a = (bool)GetPropertyValue(inst, "InvokeRequired");
            
            if (a)
            {
                bindvalue d = new bindvalue(SetValue);
                this.Invoke(d, new object[] { Instance, Property, value });
            }
            else
            {
                SetPropertyValue(Instance, Property, value);
            }
        }
        /// <summary>
        /// 委托执行实例的方法
        /// </summary>
        /// <param name="Instance">类实例</param>
        /// <param name="Method">方法名</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>返回值</returns>
        private object InvokeMethod(object Instance, string Method, object[] parameters)
        {
            if ((bool)GetPropertyValue(Instance, "InvokeRequired"))
            {
                invokemethod d = new invokemethod(InvokeMethod);
                return this.Invoke(d, new object[] { Instance, Method, parameters });
            }
            else
            {
                return MethodInvoke(Instance, Method, parameters);
            }
        }

        /// <summary>
        /// 委托执行实例的方法
        /// </summary>
        /// <param name="InstanceInvokeRequired">窗体控件对象</param>
        /// <param name="Instance">需要执行方法的对象</param>
        /// <param name="Method">方法名</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>返回值</returns>
        private object InvokeChailMethod(object InstanceInvokeRequired, object Instance, string Method, object[] parameters)
        {
            if ((bool)GetPropertyValue(InstanceInvokeRequired, "InvokeRequired"))
            {
                invokechailmethod d = new invokechailmethod(InvokeChailMethod);
                return this.Invoke(d, new object[] { InstanceInvokeRequired, Instance, Method, parameters });
            }
            else
            {
                return MethodInvoke(Instance, Method, parameters);
            }
        }
        /// <summary>
        /// 委托执行实例的属性的方法
        /// </summary>
        /// <param name="Instance">类实例</param>
        /// <param name="Property">属性名</param>
        /// <param name="Method">方法名</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>返回值</returns>
        private object InvokePMethod(object Instance, string Property, string Method, object[] parameters)
        {
            if ((bool)GetPropertyValue(Instance, "InvokeRequired"))
            {
                invokepmethod d = new invokepmethod(InvokePMethod);
                return this.Invoke(d, new object[] { Instance, Property, Method, parameters });
            }
            else
            {
                return MethodInvoke(GetPropertyValue(Instance, Property), Method, parameters);
            }
        }
        /// <summary>
        /// 获取实例的属性值
        /// </summary>
        /// <param name="ClassInstance">类实例</param>
        /// <param name="PropertyName">属性名</param>
        /// <returns>属性值</returns>
        private static object GetPropertyValue(object ClassInstance, string PropertyName)
        {
            Type myType = ClassInstance.GetType();
            PropertyInfo myPropertyInfo = myType.GetProperty(PropertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            return myPropertyInfo.GetValue(ClassInstance, null);
        }
        /// <summary>
        /// 设置实例的属性值
        /// </summary>
        /// <param name="ClassInstance">类实例</param>
        /// <param name="PropertyName">属性名</param>
        private static void SetPropertyValue(object ClassInstance, string PropertyName, object PropertyValue)
        {
            Type myType = ClassInstance.GetType();
            PropertyInfo myPropertyInfo = myType.GetProperty(PropertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            myPropertyInfo.SetValue(ClassInstance, PropertyValue, null);
        }

        /// <summary>
        /// 执行实例的方法
        /// </summary>
        /// <param name="ClassInstance">类实例</param>
        /// <param name="MethodName">方法名</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>返回值</returns>
        private static object MethodInvoke(object ClassInstance, string MethodName, object[] parameters)
        {
            if (parameters == null)
            {
                parameters = new object[0];
            }
            Type myType = ClassInstance.GetType();
            Type[] types = new Type[parameters.Length];
            for (int i = 0; i < parameters.Length; ++i)
            {
                types[i] = parameters[i].GetType();
            }
            MethodInfo myMethodInfo = myType.GetMethod(MethodName, types);
            return myMethodInfo.Invoke(ClassInstance, parameters);
        }

        #endregion

        #region 任务管理 启动 停止 
        //启动采集任务
        private cGatherTask AddRunTask()
        {

            //将选择的任务添加到运行区
            //首先判断此任务是否已经添加到运行区,
            //如果已经添加到运行区则需要询问是否再起一个运行实例
            string tName = this.dataTask.SelectedCells[4].Value.ToString();
            bool IsExist = false;

            //开始初始化正在运行的任务
            Task.cTaskRun xmlTasks = new Task.cTaskRun();
            xmlTasks.LoadTaskRunData();
            for (int i=0 ;i<xmlTasks.GetCount() ;i++)
            {
                if (xmlTasks.GetTaskName(i) == tName)
                {
                    IsExist = true;
                    break;
                }
            }

            if (IsExist == true)
            {
                if (MessageBox.Show("您选择启动的任务已经在运行区存在或者有相同名称的任务已经在运行区，您是否确认此任务需要运行或需要此任务运行第二个实例？",
                    "系统询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return null;
                }

            }

            cTaskRun tr = new cTaskRun();
            cTaskClass tc = new cTaskClass();
            cTaskData tData=new cTaskData ();

            string tPath=tc.GetTaskClassPathByName(this.treeMenu.SelectedNode.Text );
            tc=null;

            string tFileName = this.dataTask.SelectedCells[4].Value.ToString() + ".xml";

            //获取最大的执行ID
            Int64 NewID = tr.InsertTaskRun(tPath, tFileName);

            tr.LoadSingleTask(NewID);

            tData = new cTaskData();
            tData.TaskID = tr.GetTaskID(0);
            tData.TaskName = tr.GetTaskName(0);
            tData.TaskType = tr.GetTaskType(0);
            tData.RunType = tr.GetTaskRunType(0);
            tData.tempFileName = tr.GetTempFile(0);
            tData.TaskState = tr.GetTaskState(0);
            tData.UrlCount = tr.GetUrlCount(0);
            tData.ThreadCount = tr.GetThreadCount(0);
            tData.GatheredUrlCount = tr.GetGatheredUrlCount(0);

            //添加任务到运行区
            m_GatherControl.AddGatherTask(tData);

            tData = null;

            //任务添加到运行区后,需要再添加到任务执行列表中
            tr = null;

            return  m_GatherControl.TaskManage.FindTask(NewID);

        }

        private void StopTask()
        {
            cGatherTask t = null;

            //判断当前选择的树节点
            if (this.treeMenu.SelectedNode.Name == "nodRunning" && this.dataTask.SelectedCells.Count != 0)
            {
                //执行正在执行的任务
                t = m_GatherControl.TaskManage.FindTask(Int64.Parse(this.dataTask.SelectedCells[1].Value.ToString()));

                //启动此任务
                m_GatherControl.Stop(t);

                //任务启动成功显示消息
                ShowInfo("任务停止", t.TaskName);

            }
        }

        #endregion

        #region 托盘的处理
        private void notifyIcon1_MouseMove(object sender, MouseEventArgs e)
        {
            //显示当前任务队列内容
            string s;

            s = "soukey采摘 [免费版]     " + "\n";
            s += "正在运行的任务: " + m_GatherControl.TaskManage.TaskListControl.RunningTaskList.Count + "\n";
            s += "正在发布的任务: " + m_PublishControl.PublishManage.ListPublish.Count;

            this.notifyIcon1.Text = s;

        }
        #endregion

        #region 设置datagridview的列表头

        //设置显示任务数据Datalistview的列表头
        private void ShowRunTask()
        {
            this.dataTask.Columns.Clear();
            this.dataTask.Rows.Clear();

            #region 此部分为固定显示 任务类型的任务都必须固定显示此列
            DataGridViewImageColumn tStateImg = new DataGridViewImageColumn();
            tStateImg.HeaderText = "状态";
            tStateImg.Width = 40;
            tStateImg.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tStateImg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataTask.Columns.Insert(0, tStateImg);

            //任务编号,不显示此列
            DataGridViewTextBoxColumn tID = new DataGridViewTextBoxColumn();
            tID.Name  = "任务编号";
            tID.Width = 0;
            tID.Visible = false;
            this.dataTask.Columns.Insert(1, tID);

            //任务状态,不显示此列
            DataGridViewTextBoxColumn tState = new DataGridViewTextBoxColumn();
            tState.Name = "任务状态";
            tState.Width = 0;
            tState.Visible = false;
            this.dataTask.Columns.Insert(2, tState);

            //用于通过判断Datagridview的数据就可知道当前树形结构选择的节点
            //用于控制(更新)界面显示状态
            DataGridViewTextBoxColumn tTreeNode = new DataGridViewTextBoxColumn();
            tTreeNode.HeaderText = "当前属性结构的节点name";
            tTreeNode.Visible = false;
            this.dataTask.Columns.Insert(3, tTreeNode);

            #endregion

            DataGridViewTextBoxColumn tName = new DataGridViewTextBoxColumn();
            tName.HeaderText = "任务名称";
            tName.Width = 150;
            this.dataTask.Columns.Insert(4, tName);

            DataGridViewTextBoxColumn tType = new DataGridViewTextBoxColumn();
            tType.HeaderText = "任务类型";
            tType.Width = 80;
            this.dataTask.Columns.Insert(5, tType);

            DataGridViewTextBoxColumn Islogin = new DataGridViewTextBoxColumn();
            Islogin.HeaderText = "是否登录";
            Islogin.Width = 80;
            this.dataTask.Columns.Insert(6, Islogin);

            DataGridViewTextBoxColumn GatheredUrlCount = new DataGridViewTextBoxColumn();
            GatheredUrlCount.HeaderText = "已完成";
            GatheredUrlCount.Width = 50;
            GatheredUrlCount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataTask.Columns.Insert(7, GatheredUrlCount);

            DataGridViewTextBoxColumn tUrlCount = new DataGridViewTextBoxColumn();
            tUrlCount.HeaderText  = "采集数";
            tUrlCount.Width = 50;
            tUrlCount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataTask.Columns.Insert(8, tUrlCount);

             
            DataGridViewProgressBarColumn tPro = new DataGridViewProgressBarColumn();
            tPro.HeaderText = "进度";
            tPro.Width = 120;
            this.dataTask.Columns.Insert(9, tPro);

            DataGridViewTextBoxColumn tRunType = new DataGridViewTextBoxColumn();
            tRunType.HeaderText = "执行类型";
            tRunType.Width = 120;
            this.dataTask.Columns.Insert(10, tRunType);

            DataGridViewTextBoxColumn tExportFile = new DataGridViewTextBoxColumn();
            tExportFile.HeaderText  = "导出类型";
            tExportFile.Width = 1900;
            this.dataTask.Columns.Insert(11, tExportFile);

        }

        private void ShowPublishTask()
        {
            this.dataTask.Columns.Clear();
            this.dataTask.Rows.Clear();

            #region 此部分为固定显示 任务类型的任务都必须固定显示此列
            DataGridViewImageColumn tStateImg = new DataGridViewImageColumn();
            tStateImg.HeaderText = "状态";
            tStateImg.Width = 40;
            tStateImg.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tStateImg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataTask.Columns.Insert(0, tStateImg);

            //任务编号,不显示此列
            DataGridViewTextBoxColumn tID = new DataGridViewTextBoxColumn();
            tID.Name = "任务编号";
            tID.Width = 0;
            tID.Visible = false;
            this.dataTask.Columns.Insert(1, tID);

            //任务状态,不显示此列
            DataGridViewTextBoxColumn tState = new DataGridViewTextBoxColumn();
            tState.Name = "任务状态";
            tState.Width = 0;
            tState.Visible = false;
            this.dataTask.Columns.Insert(2, tState);

            //用于通过判断Datagridview的数据就可知道当前树形结构选择的节点
            //用于控制(更新)界面显示状态
            DataGridViewTextBoxColumn tTreeNode = new DataGridViewTextBoxColumn();
            tTreeNode.HeaderText = "当前属性结构的节点name";
            tTreeNode.Visible = false;
            this.dataTask.Columns.Insert(3, tTreeNode);

            #endregion

            DataGridViewTextBoxColumn tName = new DataGridViewTextBoxColumn();
            tName.HeaderText = "任务名称";
            tName.Width = 150;
            this.dataTask.Columns.Insert(4, tName);

            DataGridViewTextBoxColumn PublishedCount = new DataGridViewTextBoxColumn();
            PublishedCount.HeaderText = "已导出";
            PublishedCount.Width = 50;
            PublishedCount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataTask.Columns.Insert(5, PublishedCount);

            DataGridViewTextBoxColumn Count = new DataGridViewTextBoxColumn();
            Count.HeaderText = "数量";
            Count.Width = 50;
            Count.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataTask.Columns.Insert(6, Count);


            DataGridViewProgressBarColumn tPro = new DataGridViewProgressBarColumn();
            tPro.HeaderText = "进度";
            tPro.Width = 120;
            this.dataTask.Columns.Insert(7, tPro);


            DataGridViewTextBoxColumn PublishType = new DataGridViewTextBoxColumn();
            PublishType.HeaderText = "导出类型";
            PublishType.Width = 1900;
            this.dataTask.Columns.Insert(8, PublishType);
        }

        private void ShowCompletedTask()
        {
            this.dataTask.Columns.Clear();
            this.dataTask.Rows.Clear();

            #region 此部分为固定显示
            DataGridViewImageColumn tStateImg = new DataGridViewImageColumn();
            tStateImg.HeaderText = "状态";
            tStateImg.Width = 40;
            this.dataTask.Columns.Insert(0, tStateImg);

            //任务编号,不显示此列
            DataGridViewTextBoxColumn tID = new DataGridViewTextBoxColumn();
            tID.Name = "任务编号";
            tID.Width = 0;
            tID.Visible = false;
            this.dataTask.Columns.Insert(1, tID);

            //任务状态,不显示此列
            DataGridViewTextBoxColumn tState= new DataGridViewTextBoxColumn();
            tState.Name = "任务状态";
            tState.Width = 0;
            tState.Visible = false;
            this.dataTask.Columns.Insert(2, tState);

            //用于通过判断Datagridview的数据就可知道当前树形结构选择的节点
            //用于控制(更新)界面显示状态
            DataGridViewTextBoxColumn tTreeNode = new DataGridViewTextBoxColumn();
            tTreeNode.HeaderText = "当前属性结构的节点name";
            tTreeNode.Visible = false;
            this.dataTask.Columns.Insert(3, tTreeNode);
            #endregion

            DataGridViewTextBoxColumn tName = new DataGridViewTextBoxColumn();
            tName.HeaderText = "任务名称";
            tName.Width = 150;
            this.dataTask.Columns.Insert(4, tName);

            DataGridViewTextBoxColumn tType = new DataGridViewTextBoxColumn();
            tType.HeaderText = "任务类型";
            tType.Width = 80;
            this.dataTask.Columns.Insert(5, tType);

            //DataGridViewTextBoxColumn gatherUrlCount = new DataGridViewTextBoxColumn();
            //gatherUrlCount.HeaderText = "成功采集";
            //gatherUrlCount.Width = 80;
            //gatherUrlCount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //this.dataTask.Columns.Insert(6, gatherUrlCount);

            //DataGridViewTextBoxColumn errUrlCount = new DataGridViewTextBoxColumn();
            //errUrlCount.HeaderText = "失败数量";
            //errUrlCount.Width = 80;
            //errUrlCount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //this.dataTask.Columns.Insert(7, errUrlCount);

            DataGridViewTextBoxColumn tUrlCount = new DataGridViewTextBoxColumn();
            tUrlCount.HeaderText = "采集数量";
            tUrlCount.Width = 80;
            tUrlCount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataTask.Columns.Insert(6, tUrlCount);

            DataGridViewTextBoxColumn tPro = new DataGridViewTextBoxColumn();
            tPro.HeaderText = "执行类型";
            tPro.Width = 120;
            this.dataTask.Columns.Insert(7, tPro);

            DataGridViewTextBoxColumn tExportFile = new DataGridViewTextBoxColumn();
            tExportFile.HeaderText = "导出类型";
            tExportFile.Width = 1900;
            this.dataTask.Columns.Insert(8, tExportFile);

        }

        private void ShowTaskInfo()
        {
            this.dataTask.Columns.Clear();
            this.dataTask.Rows.Clear();

            #region 比部分为固定显示
            DataGridViewImageColumn tStateImg = new DataGridViewImageColumn();
            tStateImg.HeaderText = "状态";
            tStateImg.Width = 40;
            tStateImg.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tStateImg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataTask.Columns.Insert(0, tStateImg);

            DataGridViewTextBoxColumn tID = new DataGridViewTextBoxColumn();
            tID.Name  = "任务编号";
            tID.Width = 0;
            tID.Visible = false;
            this.dataTask.Columns.Insert(1, tID);

            //任务状态,不显示此列
            DataGridViewTextBoxColumn tState = new DataGridViewTextBoxColumn();
            tState.Name = "任务编号";
            tState.Width = 0;
            tState.Visible = false;
            this.dataTask.Columns.Insert(2, tState);

            //用于通过判断Datagridview的数据就可知道当前树形结构选择的节点
            //用于控制(更新)界面显示状态
            DataGridViewTextBoxColumn tTreeNode = new DataGridViewTextBoxColumn();
            tTreeNode.HeaderText = "当前属性结构的节点name";
            tTreeNode.Visible = false;
            this.dataTask.Columns.Insert(3, tTreeNode);

            #endregion

            DataGridViewTextBoxColumn tName = new DataGridViewTextBoxColumn();
            tName.HeaderText = "任务名称";
            tName.Width = 150;
            this.dataTask.Columns.Insert(4, tName);

            DataGridViewTextBoxColumn tType = new DataGridViewTextBoxColumn();
            tType.HeaderText = "任务类型";
            tType.Width = 80;
            this.dataTask.Columns.Insert(5, tType);

            DataGridViewTextBoxColumn tLogin = new DataGridViewTextBoxColumn();
            tLogin.HeaderText = "是否登录";
            tLogin.Width = 80;
            this.dataTask.Columns.Insert(6, tLogin);

            DataGridViewTextBoxColumn tUrlCount = new DataGridViewTextBoxColumn();
            tUrlCount.HeaderText = "采集数量";
            tUrlCount.Width = 80;
            tUrlCount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
       
            this.dataTask.Columns.Insert(7, tUrlCount);

            DataGridViewTextBoxColumn tPro = new DataGridViewTextBoxColumn();
            tPro.HeaderText = "执行类型";
            tPro.Width = 120;

            this.dataTask.Columns.Insert(8, tPro);

            DataGridViewTextBoxColumn tExportFile = new DataGridViewTextBoxColumn();
            tExportFile.HeaderText = "导出类型";
            tExportFile.Width = 1900;

            this.dataTask.Columns.Insert(9, tExportFile);

            
        }

        #endregion

        private void dataTask_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataTask.SelectedCells.Count == 0)
            {
                return;
            }

            switch (this.treeMenu.SelectedNode.Name)
            {
                case "nodRunning":
                   cGatherTask t = m_GatherControl.TaskManage.FindTask((Int64)this.dataTask.SelectedCells[1].Value);
                    if (t.TaskState == cGlobalParas.TaskState.Started)
                    {
                        this.toolStartTask.Enabled = false;
                        this.toolStopTask.Enabled = true;
                    }
                    else
                    {
                        this.toolStartTask.Enabled = true;
                        this.toolStopTask.Enabled = false;
                    }
                    
                    if (t.GatheredUrlCount ==0)
                    {
                        this.toolRestartTask.Enabled = false;
                    }
                    else
                    {
                        this.toolRestartTask.Enabled = true;
                    }

                    UpdateStatebarTaskState(t.TaskState);

                    t = null;

                    //只要有内容就可以删除
                    this.toolDelTask.Enabled = true;
                    this.toolBrowserData.Enabled = false;

                    break ;
                case "nodPublish":

                    this.toolStartTask.Enabled = false;
                    this.toolRestartTask.Enabled = true;
                    this.toolStopTask.Enabled = false;
                    this.toolDelTask.Enabled = false;
                    this.toolBrowserData.Enabled = false;

                    UpdateStatebarTaskState(cGlobalParas.TaskState.Publishing );
                    break;
                case "nodComplete":
                    this.toolStartTask.Enabled = false;
                    this.toolRestartTask.Enabled = false;
                    this.toolStopTask.Enabled = false;
                    this.toolDelTask.Enabled = true;
                    this.toolBrowserData.Enabled = true;

                    UpdateStatebarTaskState(cGlobalParas.TaskState.Completed );

                    break ;

                default :

                    //只要有内容就可以删除
                    this.toolDelTask.Enabled = true;

                    if (this.treeMenu.SelectedNode.Name.Substring(0, 1) == "C")
                    {
                        this.toolStartTask.Enabled = true;
                        this.toolRestartTask.Enabled = false;
                        this.toolStopTask.Enabled = false;
                    }

                    break;
             }
        }

        #region 自动添加控件用于显示任务执行的结果

        private void AddTab(Int64 TaskID,string TaskName)
        {
            bool IsExist = false;
            int j = 0;

            //判断此任务是否已经添加了Tab页
            for (j=0;j<this.tabControl1.TabPages.Count ;j++)
            {
                if (this.tabControl1.TabPages[j].Name == "page" + TaskID .ToString ())
                {
                    IsExist = true;
                    break ;
                }
            }

            if (IsExist == true)
            {
                this.tabControl1.SelectedTab = this.tabControl1.TabPages[j];
                return;
            }
            
            TabPage tPage=new TabPage ();
            tPage.Name ="page" + TaskID .ToString ();

            //附件任务名称的信息
            tPage.Tag = TaskName;

            tPage.Text = TaskName;
            tPage.ImageIndex = 0;

            SplitContainer sc = new SplitContainer();
            sc.Name = "sCon" + TaskID.ToString();
            sc.Orientation = Orientation.Horizontal;
            sc.Dock = DockStyle.Fill;

            cMyDataGridView d = new cMyDataGridView();
            d.Name = "grid" + TaskID.ToString();
            d.TaskRunID = TaskID;
            d.TaskName = TaskName;
            d.Dock = DockStyle.Fill;
            d.MouseDown += this.Tab_MouseDown;
            d.ContextMenuStrip = this.contextMenuStrip4;
            sc.Panel1.Controls.Add(d);

            cMyTextLog r = new cMyTextLog();
            r.Name = "tLog" + TaskID .ToString();
            r.ReadOnly = true;
            r.BorderStyle = BorderStyle.FixedSingle;
            r.BackColor = Color.White;
            r.DetectUrls = false;
            r.Dock = DockStyle.Fill;
            sc.Panel2.Controls.Add(r);

            tPage.Controls.Add(sc);

            this.tabControl1.TabPages.Add(tPage);
            this.tabControl1.SelectedTab = tPage;

        }

        #endregion

        private void dataTask_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                switch (this.treeMenu.SelectedNode.Name)
                {
                    case "nodRunning":
                        DelRunTask();
                        break;
                    case "nodPublish":
                        break;
                    case "nodComplete":
                        DelCompletedTask();
                        break;
                    default:
                        DelTask();
                        break;
                }
            }
        }

        ///定时刷新界面显示的信息，主要显示各个正在执行的任务的进度
        ///任务启动(完成)后状态的刷新,状态栏的信息刷新
        private void timer1_Tick(object sender, EventArgs e)
        {
            int proI = 0;

            if (IsTimer == true)
            {
                IsTimer = false;

                if (this.treeMenu.SelectedNode.Name == "nodRunning" )
                {
                    //如果当前选中则开始更新
                    //按照m_GatherControl.TaskManage.TaskList进行更新
                    for (int i = 0; i < m_GatherControl.TaskManage.TaskListControl.RunningTaskList.Count; i++)
                    {
                        for (int j = 0; j < this.dataTask.Rows.Count; j++)
                        {
                            if (m_GatherControl.TaskManage.TaskListControl.RunningTaskList[i].TaskID.ToString() == this.dataTask.Rows[j].Cells[1].Value.ToString())
                            {
                                this.dataTask.Rows[j].Cells[7].Value = m_GatherControl.TaskManage.TaskListControl.RunningTaskList[i].GatheredUrlCount;
                                this.dataTask.Rows[j].Cells[8].Value = m_GatherControl.TaskManage.TaskListControl.RunningTaskList[i].UrlCount ;
                                proI = (int)m_GatherControl.TaskManage.TaskListControl.RunningTaskList[i].GatheredUrlCount * 100 / m_GatherControl.TaskManage.TaskListControl.RunningTaskList[i].UrlCount;
                                this.dataTask.Rows[j].Cells[9].Value = proI;
                            }
                        }
                    }
                }
                else if (this.treeMenu.SelectedNode.Name == "nodPublish" )
                {
                    //如果当前选中则开始更新
                    //按照m_GatherControl.TaskManage.TaskList进行更新
                    for (int i = 0; i < m_PublishControl.PublishManage.ListPublish.Count ; i++)
                    {
                        for (int j = 0; j < this.dataTask.Rows.Count; j++)
                        {
                            if (m_PublishControl.PublishManage.ListPublish[i].TaskID.ToString() == this.dataTask.Rows[j].Cells[1].Value.ToString())
                            {
                                this.dataTask.Rows[j].Cells[5].Value = m_PublishControl.PublishManage.ListPublish[i].PublishedCount;
                                if (m_PublishControl.PublishManage.ListPublish[i].Count == 0)
                                    proI = 0;
                                else
                                    proI = (int)m_PublishControl.PublishManage.ListPublish[i].PublishedCount * 100 / m_PublishControl.PublishManage.ListPublish[i].Count ;
                                this.dataTask.Rows[j].Cells[7].Value = proI;
                            }
                        }
                    }
                    
                }
                
                IsTimer = true;
            }
            
        }

      
        #region MSN 通知模式 动态消息提示框操作
        //定义一个集合来控制如果有多个消息窗口同时显示的情况

        private List<frmInfo> frmInfos = new List<frmInfo>();

        public void ShowInfo(string Title,string Info)
        {
            //如果集合中存在窗口，则需要判断窗口是否已经
            //自动显示完成，如果完成，则从集合中删除
            //这个集合的概念应该是先进先出，但没有用Queue
            while (frmInfos.Count > 0 && frmInfos[0].IsShow == false)
            {
                frmInfos.Remove(frmInfos[0]);
            }

            frmInfo fInfo = new frmInfo();
            fInfo.HeightMax = 85;//窗体滚动的高度
            fInfo.WidthMax = 221;//窗体滚动的宽度
            fInfo.startFrom = frmInfos.Count;
            fInfo.txtContent.Text = Info;
            fInfo.txtTitle.Text = Title;
            fInfo.ScrollShow();

            this.frmInfos.Add(fInfo);

            this.Focus();

        }

        #endregion

        private void SetDataShow()
        {
            splitContainer2.SplitterDistance = 250;

        }

        private void SetDataHide()
        {
            splitContainer2.SplitterDistance = 700;
        }

        private void SetTaskShowState(Int64 TaskID, cGlobalParas.TaskState tState)
        {
           

            //查找当前的列表显示的任务
            //首先判断当前选中的树形节点是否是运行区的节点
            if (this.dataTask.Rows.Count > 0 && this.dataTask.Rows[0].Cells[3].Value.ToString() == "nodRunning")
            {
                for (int i = 0; i < this.dataTask.Rows.Count; i++)
                {
                    bool IsSetToolbutState = false;

                    if (this.dataTask.Rows[i].Cells[1].Value.ToString() == TaskID.ToString())
                    {
                        if (i == this.dataTask.CurrentRow.Index)
                        {
                            IsSetToolbutState = true;
                        }

                        switch (tState)
                        {
                            case cGlobalParas.TaskState.Started:

                                break;
                            case cGlobalParas.TaskState.Failed:
                                this.dataTask.Rows[i].Cells[0].Value = imageList1.Images["error"];
                                if (IsSetToolbutState == true)
                                {
                                    SetValue(this.toolStrip1.Items["toolStartTask"], "Enabled", true);
                                    SetValue(this.toolStrip1.Items["toolRestartTask"], "Enabled", false);
                                    SetValue(this.toolStrip1.Items["toolStopTask"], "Enabled", false);

                                }

                                break;
                            case cGlobalParas.TaskState.Completed:
                                this.dataTask.Rows[i].Cells[0].Value = imageList1.Images["stop"];
                                if (IsSetToolbutState == true)
                                {
                                    SetValue(this.toolStrip1.Items["toolStartTask"], "Enabled", true);
                                    SetValue(this.toolStrip1.Items["toolRestartTask"], "Enabled", false);
                                    SetValue(this.toolStrip1.Items["toolStopTask"], "Enabled", false);
                                }
                                
                                break;
                            case cGlobalParas.TaskState.UnStart:

                                break;
                            case cGlobalParas.TaskState.Pause:

                                break;
                            default:

                                break;
                        }

                        break;
                    }
                }
            }

        }

        private void toolStopTask_Click(object sender, EventArgs e)
        {
            StopTask();
        }

        private void dataTask_DoubleClick(object sender, EventArgs e)
        {
            EditTask();
        }

       

        private void EditTask()
        {
            if (this.treeMenu.SelectedNode.Name.Substring(0, 1) == "C" || this.treeMenu.SelectedNode.Name == "nodTaskClass")
            {
                //表示选择的是任务节点
                try
                {
                    string Filename = this.dataTask.SelectedCells[4].Value.ToString() + ".xml";
                    string tPath = this.treeMenu.SelectedNode.Tag.ToString();

                    LoadTaskInfo(tPath, Filename, cGlobalParas.FormState.Edit);
                }
                catch (System.IO.IOException)
                {
                    MessageBox.Show("您指定打开的任务不存在，可能已被删除！通常情况下，系统会建立此任务的备份，您可通过导入功能导入此任务。", "系统信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("您加载的任务出错，错误信息为：" + ex.Message + "。通常情况下，系统会建立此任务的备份，您可通过导入功能导入此任务。", "系统信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
            }
            else if (this.treeMenu.SelectedNode.Name == "nodRunning")
            {
                MessageBox.Show("当前任务已经加载到运行区无法进行修改操作！", "soukey信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolExportData_ButtonClick(object sender, EventArgs e)
        {

        }

        private void toolExportTxt_Click(object sender, EventArgs e)
        {
            ExportTxt();
        }

        private void toolExportExcel_Click(object sender, EventArgs e)
        {
            ExportExcel();
        }

        #region 此部分代码用于手工导出数据（已经从网上采集下来的数据）
        //定义一个代理，用于在导出数据时进行进度条的刷新
        delegate void ShowProgressDelegate(int totalMessages, int messagesSoFar, bool statusDone);

        //手动导出数据同一时间只能导出一个任务，不能进行多任务的数据导出
        private bool IsExportData()
        {
            if (this.PrograBarTxt.Visible == true)
            {
                return false;
            }
            return true;
        }


        private void ExportTxt()
        {
            if (IsExportData() == false)
            {
                MessageBox.Show("已经有手动导出任务正在运行，请等待此任务运行完成后，再进行手动数据导出！", "Soukey信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string FileName;

            this.saveFileDialog1.OverwritePrompt = true;
            this.saveFileDialog1.Title = "请指定存储数据的文件名";
            saveFileDialog1.InitialDirectory = Program.getPrjPath();
            saveFileDialog1.Filter = "Text Files(*.txt)|*.txt|All Files(*.*)|*.*";

            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileName = this.saveFileDialog1.FileName;
            }
            else
            {
                return;
            }
            Application.DoEvents();

            ExportData(FileName , cGlobalParas.PublishType.PublishTxt);

        }

        private void ExportExcel()
        {
            if (IsExportData() == false)
            {
                MessageBox.Show("已经有手动导出任务正在运行，请等待此任务运行完成后，再进行手动数据导出！", "Soukey信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string FileName;

            this.saveFileDialog1.OverwritePrompt = true;
            this.saveFileDialog1.Title = "请指定存储数据的文件名";
            saveFileDialog1.InitialDirectory = Program.getPrjPath();
            saveFileDialog1.Filter = "Excel Files(*.xls)|*.xls|All Files(*.*)|*.*";

            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileName = this.saveFileDialog1.FileName;
            }
            else
            {
                return;
            }
            Application.DoEvents();

            ExportData(FileName , cGlobalParas.PublishType.PublishExcel);
           
        }

        private void ExportData(string FileName,cGlobalParas.PublishType pType)
        {
            cExport eTxt = new cExport();
            string tName = this.tabControl1.SelectedTab.Tag.ToString();
            DataGridView tmp = (DataGridView)this.tabControl1.SelectedTab.Controls[0].Controls[0].Controls[0];
            this.PrograBarTxt.Visible = true;
            this.PrograBarTxt.Text = "手动导出数据：" + tName + " " + "0/0";
            this.ExportProbar.Visible = true;

            //定义一个后台线程用于导出数据操作
            ShowProgressDelegate showProgress = new ShowProgressDelegate(ShowProgress);

            cExport eExcel = new cExport(this, showProgress, pType, FileName, (DataTable)tmp.DataSource);
            Thread t = new Thread(new ThreadStart(eExcel.RunProcess));
            t.IsBackground = true;
            t.Start();
            ShowInfo("手工导出数据", "导出数据已经开始");

            tName = null;
        }

        private void ShowProgress(int total, int messagesSoFar, bool done)
        {
            if (this.ExportProbar.Maximum != total)
            {
                this.ExportProbar.Maximum = total;
            }
            
            ExportProbar.Value = messagesSoFar;
            this.PrograBarTxt.Text = this.PrograBarTxt.Text.Substring(0, this.PrograBarTxt.Text.IndexOf(" ")) + " " + messagesSoFar + "/" + total;

            if (done)
            {
                ShowInfo("手动导出数据", "导出数据已经完成");
                this.ExportProbar.Visible = false;
                this.PrograBarTxt.Visible = false;
            }
        }

        #endregion

        private void toolAbout_Click(object sender, EventArgs e)
        {
            frmAbout f = new frmAbout();
            f.ShowDialog();
            f.Dispose();
        }

        private void toolmenuNewTask_Click(object sender, EventArgs e)
        {
            NewTask();
        }

        private void toolmenuNewTaskClass_Click(object sender, EventArgs e)
        {
            NewTaskClass();
        }

        private void dataTask_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataTask.Rows.Count != 0 && this.dataTask.SelectedCells.Count !=0)
            {
                Int64 TaskID = Int64.Parse(this.dataTask.SelectedCells[1].Value.ToString());
                string pageName = "page" + TaskID;

                for (int i = 0; i < this.tabControl1.TabPages.Count; i++)
                {
                    if (this.tabControl1.TabPages[i].Name == pageName)
                    {
                        this.tabControl1.SelectedIndex = i;
                        break;
                    }
                }
            }

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void toolDelTask_Click(object sender, EventArgs e)
        {
            //首先判断当前焦点的控件
            //然后再确定删除的是分类还是任务
            if (DelName == "treeMenu")
            {
                //删除的是分类
                if (this.treeMenu.SelectedNode.Name.Substring(0, 1) == "C")
                {
                    DelTaskClass();
                }
            }
            else if (DelName == "dataTask")
            {
                //删除的是任务，但需要判断删除的是何种任务
                switch (this.treeMenu.SelectedNode.Name)
                {
                    case "nodRunning":
                        DelRunTask();
                        break;
                    case "nodComplete":
                        DelCompletedTask();
                        break;
                    case "nodPublish":
                        break;
                    default :
                        DelTask();
                        break;
                }
            }

        }

        private void rMenuExportTxt_Click(object sender, EventArgs e)
        {
            //首先判断当前需要导出数据的Tab及datagridview
            ExportTxt();

        }

        private void rMenuExportExcel_Click(object sender, EventArgs e)
        {
            ExportExcel();
        }

        private void rMenuCloseTabPage_Click(object sender, EventArgs e)
        {
            this.tabControl1.TabPages.Remove(this.tabControl1.SelectedTab);
        }

        private void rMenuCopy_Click(object sender, EventArgs e)
        {
            DataGridView tmp = (DataGridView)this.tabControl1.SelectedTab.Controls[0].Controls[0].Controls[0];
            Clipboard.SetDataObject(tmp.GetClipboardContent());
            tmp = null;
        }

        private void dataTask_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.dataTask.Rows.Count == 0)
            {
                return;
            }

            DataGridView.HitTestInfo hittest = this.dataTask .HitTest(e.X, e.Y);
            if (hittest.Type == DataGridViewHitTestType.Cell)  // && e.Button == MouseButtons.Right)
            {
                this.dataTask.Rows[hittest.RowIndex].Selected = true;
            }
            else
            {
                this.dataTask.Rows[this.dataTask.Rows.Count -1].Selected = true;
            }


        }

        private void Tab_MouseDown(object sender, MouseEventArgs e)
        {
            //先取消选择
            DataGridView d = (DataGridView)sender;
            d.ClearSelection();

            if (d.Rows.Count == 0)
            {
                return;
            }

            DataGridView.HitTestInfo hittest = d.HitTest(e.X, e.Y);
            if (hittest.Type == DataGridViewHitTestType.Cell && e.Button == MouseButtons.Right)
            {
                d.Rows[hittest.RowIndex].Selected = true;
            }
            else
            {
                d.Rows[d.Rows.Count - 1].Selected = true;
            }
        }

        private void contextMenuStrip4_Opening(object sender, CancelEventArgs e)
        {
            DataGridView tmp = (DataGridView)this.tabControl1.SelectedTab.Controls[0].Controls[0].Controls[0];

            if (tmp.Rows.Count == 0)
            {
                this.rMenuExportTxt.Enabled = false;
                this.rMenuExportExcel.Enabled = false;
                this.rMenuCopy.Enabled = false;
                this.rMenuDelRow.Enabled = false;
            }
            else
            {
                this.rMenuExportTxt.Enabled = true;
                this.rMenuExportExcel.Enabled = true;
                this.rMenuCopy.Enabled = true;

            }

            tmp = null;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            cXmlSConfig m_config=null;
            
            //初始化参数配置信息
            try
            {
                m_config = new cXmlSConfig();
                if (m_config.ExitIsShow == true)
                {
                    frmClose fc = new frmClose();
                    fc.RExitPara = new frmClose.ReturnExitPara(GetExitPara);
                    if (fc.ShowDialog() == DialogResult.Cancel)
                    {
                        fc.Dispose();
                        e.Cancel = true;
                        return;
                    }
                    else
                    {
                        if (m_ePara == cGlobalParas.ExitPara.MinForm)
                        {
                            fc.Dispose();
                            e.Cancel = true;
                            this.Hide();
                            return;
                        }
                    }
                }
                else
                {
                    //判断是直接退出还是最小化窗体
                    if (m_config.ExitSelected == 0)
                    {
                        this.Hide();
                        e.Cancel = true;
                        return;
                    }
                }

                m_config = null;
            }
            catch (System.Exception)
            {
                MessageBox.Show("系统配置文件加载失败，可从安装文件中拷贝文件：SoukeyConfig.xml 到Soukey采摘安装目录，配置文件损坏并不影响系统运行，但您做的系统配置可能无法保存！", "Soukey信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                frmClose fc = new frmClose();
                fc.RExitPara = new frmClose.ReturnExitPara(GetExitPara);
                if (fc.ShowDialog() == DialogResult.Cancel)
                {
                    fc.Dispose();
                    e.Cancel = true;
                    return;
                }
                else
                {
                    if (m_ePara == cGlobalParas.ExitPara.MinForm)
                    {
                        fc.Dispose();
                        e.Cancel = true;
                        this.Hide();
                        return;
                    }
                }
            }

            //判断是否存在运行的任务
            if (m_GatherControl.TaskManage.TaskListControl.RunningTaskList.Count != 0)
            {
                if (MessageBox.Show("运行区有正在运行的任务，是否退出？", "Soukey询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

            //窗体关闭 首先先停止正在运行的任务
            m_GatherControl.Stop();

        }


        private void GetExitPara(cGlobalParas.ExitPara ePara)
        {
            m_ePara = ePara;
        }

        private void toolRestartTask_Click(object sender, EventArgs e)
        {
            ResetTask();
        }

        private void ResetTask()
        {
            //重置任务 将指定的任务恢复成为默认的状态
            if (this.treeMenu.SelectedNode.Name == "nodRunning")
            {
                if (MessageBox.Show("您选择了重置任务：" + this.dataTask.SelectedCells[4].Value.ToString() + "，重置任务将恢复任务的初始状态，并删除已经采集的数据，但如果您已经导出了数据或由系统自动导出了数据，则不会收到影响，是否继续？",
                    "soukey询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cGatherTask t = m_GatherControl.TaskManage.FindTask(Int64.Parse(this.dataTask.SelectedCells[1].Value.ToString()));
                    t.ResetTask();

                    //删除Tabpage
                    string pageName = "page" + t.TaskID;

                    for (int i = 0; i < this.tabControl1.TabPages.Count; i++)
                    {
                        if (this.tabControl1.TabPages[i].Name == pageName)
                        {
                            this.tabControl1.TabPages.Remove(this.tabControl1.TabPages[i]);
                            break;
                        }
                    }

                    this.dataTask.SelectedRows[0].Cells[7].Value = "0";
                    this.dataTask.SelectedRows[0].Cells[8].Value = t.UrlCount.ToString();
                    this.dataTask.SelectedRows[0].Cells[9].Value = "0";

                    t = null;

                }

            }
        }

        private void rMenuDelRow_Click(object sender, EventArgs e)
        {
            DataGridView tmp = (DataGridView)this.tabControl1.SelectedTab.Controls[0].Controls[0].Controls[0];
            tmp.Rows.Remove(tmp.SelectedRows[0]);
            tmp = null;
        }

        private void rmmenuStartTask_Click(object sender, EventArgs e)
        {
            StartTask();
        }

        private void rmmenuStopTask_Click(object sender, EventArgs e)
        {
            StopTask();
        }

        private void treeMenu_Enter(object sender, EventArgs e)
        {
            DelName = this.treeMenu.Name;
        }

        private void dataTask_Enter(object sender, EventArgs e)
        {
            DelName = this.dataTask.Name;
        }

        private void rmmenuRestartTask_Click(object sender, EventArgs e)
        {
            ResetTask();
        }

        public void UpdateStatebarTask()
        {
            string s="任务队列：";

            s +=m_GatherControl.TaskManage.TaskListControl.RunningTaskList.Count + m_GatherControl.TaskManage.TaskListControl.StoppedTaskList.Count +  m_PublishControl.PublishManage.ListPublish.Count + "-个任务  ";
            s +=m_GatherControl.TaskManage.TaskListControl.RunningTaskList.Count + "-个运行  ";
            s +=  m_GatherControl.TaskManage.TaskListControl.StoppedTaskList.Count + "-个停止  ";
            s += m_PublishControl.PublishManage.ListPublish.Count + "-个导出";

            this.toolStripStatusLabel2.Text = s;

        }

        private void UpdateStatebarTaskState(string Info)
        {
            this.StateInfo.Text = Info;
        }

        private void UpdateStatebarTaskState(cGlobalParas.TaskState tState)
        {

            switch (tState)
            {
                case cGlobalParas.TaskState .UnStart :
                    this.StateInfo.Text = "任务未启动";
                    break;
                case cGlobalParas.TaskState .Stopped :
                    this.StateInfo.Text = "任务已停止";
                    break;
                case cGlobalParas.TaskState.Completed :
                    this.StateInfo.Text = "任务已完成";
                    break;
                case cGlobalParas.TaskState.Failed :
                    this.StateInfo.Text = "任务失败";
                    break;
                case cGlobalParas.TaskState.Pause :
                    this.StateInfo.Text = "任务暂停";
                    break;
                case cGlobalParas.TaskState.Running :
                    this.StateInfo.Text = "任务正在运行";
                    break;
                case cGlobalParas.TaskState.Started :
                    this.StateInfo.Text = "任务正在运行";
                    break;
                case cGlobalParas.TaskState.Waiting :
                    this.StateInfo.Text = "任务等待运行";
                    break;
                case cGlobalParas.TaskState.Publishing :
                    this.StateInfo.Text = "任务正在发布";
                    break;
                default:
                    break;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        //每半分钟进行一次刷新，查看当前的网络状态
        //如果网络状态发生改变需要网络连接状态
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (cTool.IsLinkInternet ()==false )
            {
                this.staIsInternet.Image =Bitmap.FromFile(Program.getPrjPath () +  "img\\a08.gif");
;
                this.staIsInternet.Text ="离线";

                //如果检测到离线则停止当前正在运行的任务

            }
            else
            {
                this.staIsInternet.Image = Bitmap.FromFile(Program.getPrjPath() + "img\\a07.gif");
                this.staIsInternet.Text ="在线";

                //如果检测到在线，则启动已经停止的需要采集的任务
            }
        }


        #region 发布任务的事件处理
        private void Publish_Complete(object sender, PublishCompletedEventArgs e)
        {
            InvokeMethod(this, "ShowInfo", new object[] { e.TaskName, "成功发布" });

            InvokeMethod(this, "UpdateTaskPublished", new object[] { e.TaskID ,cGlobalParas.GatherResult.PublishSuccees});

            InvokeMethod(this, "UpdateStatebarTask", null);

        }

        private void Publish_Started(object sender, PublishStartedEventArgs e)
        {
            InvokeMethod(this, "ShowInfo", new object[] {  e.TaskName ,"已经开始进行数据发布" });

            InvokeMethod(this, "UpdateStatebarTask", null);
        }

        private void Publish_Error(object sender, PublishErrorEventArgs e)
        {
            InvokeMethod(this, "UpdateTaskPublished", new object[] { e.TaskID ,cGlobalParas.GatherResult.PublishFailed });

            InvokeMethod(this, "ShowInfo", new object[] { e.TaskName, "任务发布失败，已经转到已完成区域" });

            InvokeMethod(this, "UpdateStatebarTask", null);

        }

        private void Publish_Failed(object sender, PublishFailedEventArgs e)
        {
            InvokeMethod(this, "UpdateTaskPublished", new object[] { e.TaskID ,cGlobalParas.GatherResult.PublishFailed });

            InvokeMethod(this, "ShowInfo", new object[] { e.TaskName,"任务发布失败，已经转到已完成区域" });

            InvokeMethod(this, "UpdateStatebarTask", null);
        }

        #endregion


        //处理任务发布完成的工作
        public void UpdateTaskPublished(Int64 TaskID,cGlobalParas.GatherResult tState)
        {
            //将已经完成发布的任务添加到完成任务的索引文件中
            Task.cTaskComplete t = new Task.cTaskComplete();
            t.InsertTaskComplete(TaskID, tState);
            t = null;

            //删除taskrun节点
            cTaskRun tr = new cTaskRun();
            tr.LoadTaskRunData();
            tr.DelTask(TaskID);

            //修改Tab页的名称


            //删除run中的任务实例文件
            string FileName = Program.getPrjPath() + "Tasks\\run\\" + "Task" + TaskID + ".xml";
            System.IO.File.Delete(FileName);

            if (this.treeMenu.SelectedNode.Name == "nodPublish")
            {
                for (int i = 0; i < this.dataTask.Rows.Count; i++)
                {
                    if (this.dataTask.Rows[i].Cells[1].Value.ToString() == TaskID.ToString())
                    {
                        this.dataTask.Rows.Remove(this.dataTask.Rows[i]);
                        break;
                    }
                }
            }
            else if (this.treeMenu.SelectedNode.Name == "nodComplete")
            {
                //重新加载已完成任务的信息
                LoadCompleteTask();
            }
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout f = new frmAbout();
            f.ShowDialog();
            f.Dispose();
        }

        private void toolMenuVisityijie_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.yijie.net"); 
        }

        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized )
            {
                //this.Hide();
            }
        }

        private void MenuOpenMainfrm_Click(object sender, EventArgs e)
        {
            this.Visible = true;

            this.WindowState = FormWindowState.Maximized;
            this.Activate();
        }

        private void MenuCloseSystem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.soukey.com/so.aspx?wd=" + HttpUtility.UrlEncode (this.txtSotxt.Text,Encoding.GetEncoding ("gb2312"))); 
        }


        private void BrowserData()
        {
            if (this.dataTask.Rows.Count != 0)
            {
                Int64 TaskID = Int64.Parse (this.dataTask.SelectedCells[1].Value.ToString ());
                DataTable tmp = new DataTable();
                string FileName = Program.getPrjPath () + "data\\" + this.dataTask.SelectedCells[4].Value.ToString () + "-" + TaskID + ".xml";
                string conName = "sCon" + TaskID;
                string pageName = "page" + TaskID;

                AddTab(TaskID, this.dataTask.SelectedCells[4].Value.ToString ());
                tmp.ReadXml(FileName);

                ((DataGridView)(this.tabControl1.TabPages[pageName].Controls[conName].Controls[0].Controls[0])).DataSource = tmp;
                tmp = null;
                
            }
        }

        private void toolBrowserData_Click(object sender, EventArgs e)
        {
            BrowserData();
        }

        private void rmenuBrowserData_Click(object sender, EventArgs e)
        {
            BrowserData();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Visible = true;

                this.WindowState = FormWindowState.Maximized;

                this.Activate();

            }
        }

        private void toolMenuImportTask_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Title = "请指定任务文件名";

            openFileDialog1.InitialDirectory = Program.getPrjPath() + "tasks";
            openFileDialog1.Filter = "Soukey Task Files(*.xml)|*.xml";


            if (this.openFileDialog1.ShowDialog() ==DialogResult.Cancel )
            {
                return;
            }

            string FileName = this.openFileDialog1.FileName;
            string TaskClass="";
            string NewFileName = "";

            //验证任务格式是否正确
            try
            {
                Task.cTask t = new Task.cTask();
                t.LoadTask(FileName);

                if (t.TaskName != "")
                {
                    NewFileName = t.TaskName + ".xml";
                }

                if (t.TaskClass != "")
                {
                    TaskClass =t.TaskClass.ToString ();
                }

                 //根据任务的分类导入指定的目录
                string TaskPath = Program.getPrjPath() + "tasks\\";
                if (TaskClass != "")
                {
                    TaskPath +=TaskClass + "\\";
                }

                if (NewFileName == "")
                {
                    NewFileName = "task" + DateTime.Now.ToFileTime().ToString() + ".xml";
                }


                if (FileName == TaskPath + NewFileName)
                {
                    MessageBox.Show("您导入的任务已经存在，请选择其他需要导入的任务！", "系统信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Task.cTaskClass cTClass = new Task.cTaskClass();

                if (!cTClass.IsExist (TaskClass) && TaskClass !="")
                {
                    //表示是一个新任务分类
                    int TaskClassID = cTClass.AddTaskClass(TaskClass, TaskPath);
                    cTClass = null;

                    //建立树形结构的任务分类节点
                    TreeNode newNode = new TreeNode();
                    newNode.Tag = TaskPath;
                    newNode.Name = "C" + TaskClassID ;
                    newNode.Text = TaskClass;
                    newNode.ImageIndex = 0;
                    newNode.SelectedImageIndex = 0;
                    this.treeMenu.Nodes["nodTaskClass"].Nodes.Add(newNode);
                    newNode = null;

                }

                try
                {
                    System.IO.File.Copy(FileName, TaskPath + NewFileName);
                }
                catch (System.IO.IOException )
                {
                    t = null;
                    MessageBox.Show("您指定的任务已经存在，但您可能无法通过Soukey采摘看到，是因为您曾经删除了此任务，但任务文件还存在，可通过修改此文件名后再进行任务导入操作！", "系统信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //插入任务索引文件
                t.InsertTaskIndex(TaskPath);

                t = null;

                MessageBox.Show("您指定的任务已成功导入“" + TaskClass + "”分类下！", "系统信息", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("错误信息：" + ex.Message + "，导入失败！", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void dataTask_DragDrop(object sender, DragEventArgs e)
        {
           
        }

        private void treeMenu_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy; 
        }

        private void treeMenu_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(DataGridViewRow)))
            {
                Point p = this.treeMenu.PointToClient(new Point(e.X, e.Y));
                TreeViewHitTestInfo index = this.treeMenu.HitTest(p);

                if (index.Node != null)
                {
                    DataGridViewRow drv = (DataGridViewRow)e.Data.GetData(typeof(DataGridViewRow));
                    cTask t = new cTask();

                    string TaskName = drv.Cells[4].Value.ToString();
                    string oldName = this.treeMenu.SelectedNode.Text;
                    string NewName = index.Node.Text;

                    try
                    {
                        t.ChangeTaskClass(TaskName, oldName, NewName);
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("修改任务分类发生错误，错误信息：" + ex.Message ,"系统错误",MessageBoxButtons.OK ,MessageBoxIcon .Error);
                        t = null;
                        return;
                    }
                    
                    t = null;

                    this.dataTask.Rows.Remove(drv);

                }
            }
        }

        private void dataTask_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataTask_MouseMove(object sender, MouseEventArgs e)
        {

            //判断是否为左键点击，如果是则需要启动拖放操作
            if (((e.Button & MouseButtons.Left) == MouseButtons.Left && this.treeMenu.SelectedNode.Name == "nodTaskClass") ||
                ((e.Button & MouseButtons.Left) == MouseButtons.Left && this.treeMenu.SelectedNode.Name.Substring(0, 1) == "C"))
            {

                DataGridViewRow dragData = (DataGridViewRow)this.dataTask.SelectedRows[0];
                //Size dragSize = SystemInformation.DragSize;
                //Rectangle dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
                this.dataTask.DoDragDrop(dragData, DragDropEffects.Copy);
            }
        }

        private void toolMenuDownloadTask_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.yijie.net/downloadTask.html"); 
        }


   
    }

}