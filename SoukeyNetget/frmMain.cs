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

///���ܣ�Soukey��ժ�����洦�������߳���Ӧ�¼���
///���ʱ�䣺2009-3-2
///���ߣ�һ��
///�������⣺���뻹δ�������ܿ������Ƚ���
///�����ƻ�����
///˵����
///�汾��00.90.00
///�޶�����
namespace SoukeyNetget
{

    public partial class frmMain : Form
    {

        private cGatherControl m_GatherControl;
        private cPublishControl m_PublishControl;
        private bool IsTimer = true;
        private string DelName="";
        private cGlobalParas.ExitPara m_ePara = cGlobalParas.ExitPara.Exit;

        #region �����ʼ������
        
        //������ĳ�ʼ���������ⲿ������

        public frmMain()
        {
            InitializeComponent();

            //��������ͼ��
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.ShowBalloonTip(2, "soukey��ժ", "�Ѿ�����", ToolTipIcon.Info); 
        }

        //�˷�����Ҫ���н����ʼ���Ĳ������޲���
        //�˷�����Ҫ�ǳ�ʼ��������ʾ������,����
        //���νṹ,Ĭ�Ͻڵ�,��ҳ��Ĭ�ϵ�ҳ��ȵ�
        public void IniForm()
        {
            this.treeMenu.ExpandAll();
            TreeNode newNode;
            int i = 0;

            //��ʼ����ҳ���̶���ַ��������ת�ɴ�ҳ��������
            this.webBrowser.Navigate("http://www.yijie.net/softini.html");

            //��ʼ��ʼ�����νṹ,ȡxml�е�����,��ȡ�������
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

            //��������ڵ㸳·��ֵ
            this.treeMenu.Nodes["nodTaskClass"].Tag = Program.getPrjPath() + "Tasks";

            //����Ĭ��ѡ������νṹ�ڵ�Ϊ���������С�
            TreeNode SelectNode = new TreeNode();
            SelectNode = this.treeMenu.Nodes[0].Nodes[0];
            this.treeMenu.SelectedNode = SelectNode;
            SelectNode = null;

            //����ɾ����Ϊ���νṹ
            DelName = this.treeMenu.Name;

        }

        //�˷�����Ҫ�ǳ�ʼ��ϵͳ����,������Ҫ��ʼ����Ϣ�¼�,��ʼ���ɼ�
        //���������,������������������,���������������,���ʼ��һ����
        //�Ķ���
        public void UserIni()
        {

            //��ʼ��һ���ɼ�����Ŀ�����,�ɼ������ɴ˿�����������ɼ�����
            //����
            m_GatherControl = new cGatherControl();

            //����������������,��������������Ҫ�Ǹ���taskrun.xml(Ĭ����Tasks\\TaskRun.xml)�ļ���
            //�����ݽ��м���,

            //�����ж�TaskRun.xml�ļ��Ƿ����,����������һ��
            if (!System.IO.File.Exists(Program.getPrjPath() + "Tasks\\taskrun.xml"))
            {
                CreateTaskRun();
            }

            cTaskDataList gList = new cTaskDataList();
            gList.LoadTaskRunData ();
            
            //���ݼ��ص���������������Ϣ,��ʼ��ʼ���ɼ�����
            //try
            //{
                m_GatherControl.AddGatherTask(gList);
            //}
            //catch (System.Exception ex)
            //{

            //}

            //�ɼ��������¼���,�󶨺�,ҳ�������Ӧ�ɼ����������¼�
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

            //��ʼ�������ڵ�����������Ϣ
            m_PublishControl = new cPublishControl();

            //ע�ᷢ��������¼�
            m_PublishControl.PublishManage.PublishCompleted += this.Publish_Complete;
            m_PublishControl.PublishManage.PublishError += this.Publish_Error;
            m_PublishControl.PublishManage.PublishFailed += this.Publish_Failed;
            m_PublishControl.PublishManage.PublishStarted  += this.Publish_Started;

            //����״̬����Ϣ
            UpdateStatebarTask();


        }

 #endregion

        #region �˵� ������ ���νṹ listview �ȿؼ��� ��Ӧ�¼�

       
        private void rmenuAddTaskClass_Click(object sender, EventArgs e)
        {
            NewTaskClass();
        }

        private void toolNewTask_ButtonClick(object sender, EventArgs e)
        {
            NewTask();
        }

        //�������񣬵�ǰ�����ֻ������һ�����񣬲�֧�������������
        private void toolStartTask_Click(object sender, EventArgs e)
        {

            if (int.Parse(this.dataTask.SelectedCells[7].Value.ToString()) > 0 && this.treeMenu.SelectedNode.Name.ToString () =="nodRunning")
            {
                cGatherTask t = m_GatherControl.TaskManage.FindTask(Int64.Parse(this.dataTask.SelectedCells[1].Value.ToString()));
                t.ResetTask();

                //ɾ��Tabpage
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

            //�жϵ�ǰѡ������ڵ�
            if (this.treeMenu.SelectedNode.Name == "nodRunning" && this.dataTask.SelectedCells.Count != 0)
            {
                //ִ������ִ�е�����
                t = m_GatherControl.TaskManage.FindTask(Int64.Parse(this.dataTask.SelectedCells[1].Value.ToString()));

            }
            else if (this.treeMenu.SelectedNode.Name.Substring(0, 1) == "C" || this.treeMenu.SelectedNode.Name == "nodTaskClass")
            {
                ///�����ѡ����������ڵ㣬����˰�ť�����Ƚ���������ص���������Ȼ�����
                ///starttask��������������
                t = AddRunTask();
                if (t == null)
                {
                    //��ʾ���������û��жϣ�Ҳ�п�������Ϊ�������
                    return;
                }
            }

            //�жϴ������Ƿ���Ҫ��¼�������Ҫ��¼����Ҫ�û���Ԥ
            if (t.TaskData.IsLogin == true)
            {
                frmWeblink f = new frmWeblink(t.TaskData.LoginUrl);
                f.Owner = this;
                f.rCookie = new frmWeblink.ReturnCookie(GetCookie);
                f.ShowDialog();
                f.Dispose();

                t.UpdateCookie(this.Cookie);

            }

            //����ɹ���������Ҫ����TabPage������ʾ���������־���ɼ����ݵ���Ϣ
            AddTab(t.TaskID,t.TaskName );

            //����������
            m_GatherControl.Start(t);

            //���������ɹ���ʾ��Ϣ
            ShowInfo("��������", t.TaskName);
        }

        //ͨ���û���¼��ȡcookie
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
                case "nodRunning":   //������������
                    try
                    {
                        LoadRunTask(e.Node.Name);
                    }
                    catch (System.IO.IOException)
                    {
                        if (MessageBox.Show("�������м���ļ���ʧ�������Ƿ�����������е���������Զ�������", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            CreateTaskRun();
                        }
                    }
                    catch (System.Exception)
                    {
                        MessageBox.Show("���ص��������м���ļ��Ƿ���������ļ���" + Program.getPrjPath () + "tasks\\taskrun.xml" + "���������ʽ�Ƿ�����ͨ��Windows�ļ������ɾ���������µ���˽ڵ���ϵͳ�Զ�������", "ϵͳ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }

                    SetDataShow();

                    //����ʱ�������ڸ���������ʾ�Ľ���
                    this.timer1.Enabled = true;

                    //��ɾ����ťΪ��Ч
                    this.toolDelTask.Enabled = false;

                    break;

                case "nodPublish":

                    LoadPublishTask();

                    //����ʱ�������ڸ���������ʾ�Ľ���
                    this.timer1.Enabled = true;

                    //��ɾ����ťΪ��Ч
                    this.toolDelTask.Enabled = false;

                    SetDataShow();

                    break;

                case "nodComplete":    //�Ѿ���ɲɼ�������

                    try
                    {
                        LoadCompleteTask();
                    }
                    catch (System.IO.IOException)
                    {
                        if (MessageBox.Show("��������������ļ���ʧ����Ҫ���½��������Ѿ���ɵ�������Ϣ���ᶪʧ���������ݲ��ᶪʧ��Ĭ�ϴ洢�ڣ�" + Program.getPrjPath () + "Data" + "Ŀ¼�£��Ƿ�����", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            CreateTaskComplete ();
                        }
                    }
                    catch (System.Exception)
                    {
                        MessageBox.Show("���ص���������������ļ��Ƿ���������ļ���" + Program.getPrjPath () + "data\\index.xml" + "���������ʽ�Ƿ�����ͨ��Windows�ļ������ɾ���������µ���˽ڵ���ϵͳ�Զ�������", "ϵͳ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    };

                    SetDataShow();

                    this.timer1.Enabled = false;

                    //��ɾ����ťΪ��Ч
                    this.toolDelTask.Enabled = false;

                    break;

                default:
                    try
                    {
                        LoadOther(e.Node);
                    }
                    catch (System.IO.IOException)
                    {
                        if (MessageBox.Show(this.treeMenu.SelectedNode.Text + "�����µ������ļ���ʧ�������Ƿ��Զ�������", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            CreateTaskIndex(this.treeMenu.SelectedNode.Tag.ToString());
                        }
                    }
                    catch (System.Exception)
                    {
                        MessageBox.Show("���ص�������������ļ��Ƿ���������ļ���" + e.Node.Tag.ToString() + "\\index.xml" + "���������ʽ�Ƿ�����ͨ��Windows�ļ������ɾ���������µ���˽ڵ���ϵͳ�Զ�������", "ϵͳ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }

                    SetDataHide();

                    //��ɾ����ťΪ��Ч
                    this.toolDelTask.Enabled = false ;

                    this.timer1.Enabled = false;
                    break;
            }

           //���۵�����νṹ�Ĳ˵�������ť��Ϊ������
            this.toolStartTask.Enabled = false;
            this.toolRestartTask.Enabled = false;
            this.toolStopTask.Enabled = false;
            this.toolExportData.Enabled = false;
            this.toolBrowserData.Enabled = false;

            UpdateStatebarTaskState("��ǰ��ʾ�� " + e.Node.Text );

        }

        //���νṹ������ȷʵ�ܲ�����������������ĸ��ˣ���һ���汾������
        //��Ϊ��һ���汾���������νṹ��Ӧ�ã����ò����ˣ��Ǻ�
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
                    MessageBox.Show(ex.Message, "ϵͳ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
            //�����жϵ�ǰ����Ŀؼ�
            //Ȼ����ȷ��ɾ�����Ƿ��໹������


            //ɾ���������񣬵���Ҫ�ж�ɾ�����Ǻ�������
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

        #region ����ؼ��¼� �����õķ���
        private void NewTaskClass()
        {
            frmTaskClass frmTClass = new frmTaskClass();
            frmTClass.RTaskClass = new frmTaskClass.ReturnTaskClass(AddTaskClassNode);
            frmTClass.ShowDialog();
            frmTClass.Dispose();
        }

        //������������󣬸�������ӵ���Ϣ����ʼ�������������νṹ
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
                //��ʾѡ����Ƿ���ڵ�
                TClass = this.treeMenu.SelectedNode.Text;
            }
            frmTask fTask = new frmTask();
            fTask.NewTask(TClass);
            fTask.FormState = cGlobalParas.FormState.New;
            fTask.rTClass = refreshNode;
            fTask.ShowDialog();
            fTask.Dispose();

            //ˢ��һ�µ�ǰ����ʾ�Ľڵ���Ϣ����Ϊ�Ѿ�������һ������

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

            ///���񷢲����ĺܼ򵥣�������ɼ���ɺ��Զ�������ʼ�������ݵķ�����
            /// ����������˹���Ԥ����ǰ��Ϊ���ַ�����ʽ���߱�̫���ʵ���ԣ�����
            /// ��ǰ����Ϊ��һ����ʱ�����������ڻ������ƣ�ϣ�������ҵ����ʵķ���
            /// ��ʽ
            ///��Ҫ���������ݲ����б����ļ��ı��棬ֱ�ӱ�����m_PublishControl��

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

            //����ɵ������м���
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

        //��������ִ�е���������ִ�е������¼��Ӧ�ó���Ŀ¼�µ�RunningTask.xml�ļ���
        private void LoadRunTask(string nodKey)
        {

            ShowRunTask();

            //��ʼ��ʼ���������е�����
            //��m_TaskControl�ж�ȡ
            //ÿ�μ��ػ�����������С��ȴ���ֹͣ�����е�����
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
                            taskList[i].TaskName,cGlobalParas.ConvertName ((int) taskList[i].TaskType),(taskList[i].IsLogin ==true ? "��¼":"����¼"),
                            taskList[i].GatheredUrlCount, taskList[i].UrlCount, taskList[i].GatheredUrlCount * 100 / taskList[i].UrlCount, cGlobalParas.ConvertName((int)taskList[i].RunType),
                            cGlobalParas.ConvertName((int)taskList[i].PublishType));
                        break ;

                    case cGlobalParas.TaskState.Stopped :
                        if (taskList[i].GatheredUrlCount > 0)
                        {
                            dataTask.Rows.Add(imageList1.Images["pause"], taskList[i].TaskID, taskList[i].State, this.treeMenu.SelectedNode.Name,
                                taskList[i].TaskName, cGlobalParas.ConvertName((int)taskList[i].TaskType), (taskList[i].IsLogin == true ? "��¼" : "����¼"),
                                taskList[i].GatheredUrlCount, taskList[i].UrlCount, taskList[i].GatheredUrlCount * 100 / taskList[i].UrlCount, cGlobalParas.ConvertName((int)taskList[i].RunType),
                                cGlobalParas.ConvertName((int)taskList[i].PublishType ));
                        }
                        else
                        {
                            dataTask.Rows.Add(imageList1.Images["stop"], taskList[i].TaskID, taskList[i].State, this.treeMenu.SelectedNode.Name,
                                taskList[i].TaskName, cGlobalParas.ConvertName((int)taskList[i].TaskType), (taskList[i].IsLogin == true ? "��¼" : "����¼"),
                                taskList[i].GatheredUrlCount, taskList[i].UrlCount, taskList[i].GatheredUrlCount * 100 / taskList[i].UrlCount, cGlobalParas.ConvertName((int)taskList[i].RunType),
                                cGlobalParas.ConvertName((int)taskList[i].PublishType));
                        }
                        break;
                    case cGlobalParas.TaskState.UnStart :
                        dataTask.Rows.Add(imageList1.Images["stop"], taskList[i].TaskID, taskList[i].State, this.treeMenu.SelectedNode.Name,
                            taskList[i].TaskName, cGlobalParas.ConvertName((int)taskList[i].TaskType), (taskList[i].IsLogin == true ? "��¼" : "����¼"),
                            taskList[i].GatheredUrlCount, taskList[i].UrlCount, taskList[i].GatheredUrlCount * 100 / taskList[i].UrlCount, cGlobalParas.ConvertName((int)taskList[i].RunType),
                            cGlobalParas.ConvertName((int)taskList[i].PublishType));
                        break;
                    case cGlobalParas.TaskState.Failed:
                        dataTask.Rows.Add(imageList1.Images["error"], taskList[i].TaskID, taskList[i].State, this.treeMenu.SelectedNode.Name,
                            taskList[i].TaskName, cGlobalParas.ConvertName((int)taskList[i].TaskType), (taskList[i].IsLogin == true ? "��¼" : "����¼"),
                            taskList[i].GatheredUrlCount, taskList[i].UrlCount, taskList[i].GatheredUrlCount * 100 / taskList[i].UrlCount, cGlobalParas.ConvertName((int)taskList[i].RunType),
                            cGlobalParas.ConvertName((int)taskList[i].PublishType));
                        break;
                    default:
                        dataTask.Rows.Add(imageList1.Images["stop"], taskList[i].TaskID, taskList[i].State, this.treeMenu.SelectedNode.Name,
                            taskList[i].TaskName, cGlobalParas.ConvertName((int)taskList[i].TaskType), (taskList[i].IsLogin == true ? "��¼" : "����¼"),
                            taskList[i].GatheredUrlCount, taskList[i].UrlCount, taskList[i].GatheredUrlCount * 100 / taskList[i].UrlCount, cGlobalParas.ConvertName((int)taskList[i].RunType),
                            cGlobalParas.ConvertName((int)taskList[i].PublishType));
                        break;
                }

            }

            this.dataTask.ClearSelection();

        }

        //�˲��ֵ������Ǹ��ݵ�ǰ�Ѿ���ɵĵ�������
        //ʵʱ����������
        private void LoadExportDataTask(string mNode)
        {
            //this.myListData.Items.Clear();
        }

        private void LoadOther(TreeNode mNode)
        {
            //���������Ϊһ������ķ�����д���
            //�˽ڵ����е�����ȫ��ΪĬ�ϣ����ṩ�û��ɲ����Ĺ���
            if (mNode.Name.Substring(0, 1) == "C" || mNode.Name == "nodTaskClass")
            {
                //��ʾ���ص���������Ϣ
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
                //������޷���֤id��Ψһ�ԣ����ԣ����е�����ȫ��������������ȡ
                string TaskClassName = mNode.Text;
                xmlTasks.GetTaskDataByClass(TaskClassName);
            }

            //��ʼ��ʼ���˷����µ�����
            int count = xmlTasks.GetTaskClassCount();

            for (int i = 0; i < count; i++)
            {
                dataTask.Rows.Add(imageList1.Images["task"], xmlTasks.GetTaskID(i), cGlobalParas.TaskState.UnStart, this.treeMenu.SelectedNode.Name, xmlTasks.GetTaskName(i),
                    cGlobalParas.ConvertName(int.Parse(xmlTasks.GetTaskType(i).ToString())), (xmlTasks.GetIsLogin(i) == true ? "��¼" : "����¼"),
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
            if (MessageBox.Show("��ѡ��ɾ�����ࣺ" + this.treeMenu.SelectedNode.Text   + "��ɾ������ֻ�ǽ��˷����µ�������index.xml���ļ�ɾ����" +
               "�������Ϣ���Ա����������Ա�����Щ���񣬻��ߵ��뵽���������£��˷����Ŀ¼��ַΪ��" + this.treeMenu.SelectedNode.Tag.ToString() + "�����Ƿ����ִ��ɾ��������",
               "ϵͳѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
                    MessageBox.Show(ex.Message, "ϵͳ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                //��ʾѡ���������ڵ�
                try
                {
                    if (MessageBox.Show("��ѡ��ɾ������" + this.dataTask.SelectedCells[4].Value + "������������������У�ɾ�������������Ӱ�죬��ɾ��������񲻿ɻָ���������", "ϵͳѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
                    MessageBox.Show(ex.Message, "ϵͳ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return false ;
                }
            }

            return false;

        }
        
        /// <summary>
        /// ɾ���������е�����
        /// </summary>
        private void DelRunTask()
        {
            if (this.dataTask.Rows.Count == 0)
                return;

            if(MessageBox.Show ("��ȷ��ɾ������" + this.dataTask.SelectedCells[4].Value.ToString () + "��ɾ�������������񲻻�Ӱ��������Ѿ��ɼ���ɵ����ݣ��Ƿ������","ϵͳѯ��",
                MessageBoxButtons.YesNo ,MessageBoxIcon.Question )==DialogResult.No )
            {
                return ;
            }

            cGatherTask t = m_GatherControl.TaskManage.FindTask(Int64.Parse(this.dataTask.SelectedCells[1].Value.ToString()));

            m_GatherControl.Remove(t);

            Int64 TaskID = Int64.Parse(this.dataTask.SelectedCells[1].Value.ToString());


            //ɾ��taskrun�ڵ�
            cTaskRun tr = new cTaskRun();
            tr.LoadTaskRunData();
            tr.DelTask(TaskID);
            tr=null;

            //ɾ��run�е�����ʵ���ļ�
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

        //ɾ���Ѿ���ɵ�����
        private void DelCompletedTask()
        {
            if (this.dataTask.Rows.Count == 0)
                return;

            if (MessageBox.Show("��ȷ��ɾ������" + this.dataTask.SelectedCells[4].Value.ToString() + "��ɾ���Ѿ���ɵ����񽫻�ɾ���Ѿ��ɼ������ݣ�������ɾ������ǰ�������ݣ��Ƿ������", "ϵͳѯ��",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            Int64 TaskID = Int64.Parse(this.dataTask.SelectedCells[1].Value.ToString());
            string TaskName = this.dataTask.SelectedCells[4].Value.ToString();

            //ɾ��taskcomplete�ڵ�
            cTaskComplete tc = new cTaskComplete();
            tc.LoadTaskData();
            tc.DelTask (TaskID );
            tc = null; 

            //ɾ��run�е�����ʵ���ļ�
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

        #region �������

        private void frmMain_Resize(object sender, EventArgs e)
        {
            //���ݴ���仯�������ؼ�����
            this.toolStrip2.Width = 300;
            this.toolStrip2.Left = this.Width - this.toolStrip2.Width;
        }

         #endregion

    
        #region �¼�����

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
            //����ִ����Ϻ���Ҫ�����������Ѿ���ɵĽڵ��У�
            //�ڴ����ѡ�����nodRunning��ɾ��datagridview������
            //Ȼ����ӵ���ɶ�����
            cGatherTask t = (cGatherTask)sender;
            InvokeMethod(this, "ShowInfo", new object[] { "����ɼ����", t.TaskName });

            //�жϴ������Ƿ���Ҫ�Զ���������
            //if (t.PublishType == cGlobalParas.PublishType.NoPublish)
            //{
            //    //���Զ���������
            //    InvokeMethod(this, "UpdateTaskComplete", new object[] { t.TaskID });

            //}
            //else
            //{
                //�Զ���������
            InvokeMethod(this, "UpdateTaskPublish", new object[] { t.TaskID });
            //}

            t = null;

            InvokeMethod(this, "UpdateStatebarTask", null);

            //����ɼ���ɺ��Ƿ���Ҫ���з���
            //���񷢲���һ���˹����ø�Ԥ�Ĺ��̣���������֤�����ʧ�ܣ�����ܻ����һ��������
            //���п��ܳ����ظ�����


        }

        

        //��������ɼ���ɵĹ���,���ڴ˵Ĵ�������Ҫ����������
        public void UpdateTaskPublish(Int64 TaskID)
        {
            //����������ӵ�����������

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
                //���¼���������������Ϣ
                LoadPublishTask();
            }


        }


        //��������ɼ���ɺ�Ĺ������������ѡ����������еĽڵ㣬��
        //ɾ���˽ڵ�,Ȼ���taskrun������ɾ��,Ȼ����ɾ��ʵ�ʵ��ļ�
        public void UpdateTaskComplete(int TaskID)
        {
            //���Ѿ���ɵ�������ӵ��������������ļ���
            Task.cTaskComplete t = new Task.cTaskComplete();
            t.InsertTaskComplete(TaskID, cGlobalParas.GatherResult.GatherSucceed );
            t = null;

            //ɾ��taskrun�ڵ�
            cTaskRun tr = new cTaskRun();
            tr.LoadTaskRunData();
            tr.DelTask(TaskID);

            //ɾ��run�е�����ʵ���ļ�
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
                //���¼���������������Ϣ
                LoadCompleteTask();
            }

        }

        private void tManage_TaskStart(object sender, EventArgs e)
        {
            //����������������޸������ͼ�꣬���¼����ɵ����ť������
            //�������д���
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
            //�ݲ����κδ���

            UpdateStatebarTask();

        }

        private void tManage_TaskStateChanged(object sender, TaskStateChangedEventArgs e)
        {
            SetTaskShowState(e.TaskID, e.NewState);

            UpdateStatebarTask();
 
        }

        private void tManage_TaskStop(object sender, cTaskEventArgs e)
        {

           //����������������޸������ͼ�꣬���¼����ɵ����ť������
            //�������д���
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
            InvokeMethod(this, "ShowInfo", new object[] { "����ɼ�����", t.TaskName });
            t = null;

            InvokeMethod(this, "UpdateStatebarTask", null);
        }

        private void tManage_TaskFailed(object sender, EventArgs e)
        {
            cGatherTask t = (cGatherTask)sender;
            InvokeMethod(this, "ShowInfo", new object[] { "����ɼ�ʧ��", t.TaskName });
            t = null;

            InvokeMethod(this, "UpdateStatebarTask", null);
        }

        private void m_Gather_Completed(object sender, EventArgs e)
        {
            //����ɼ���ɣ���������Ϣ֪ͨ���壬֪ͨ�û�


        }

        //д��־�¼�
        private void tManage_Log(object sender, cGatherTaskLogArgs e)
        {
            //д��־
            Int64 TaskID = e.TaskID;
            string strLog = e.strLog;
            string conName="sCon" + TaskID ;
            string pageName="page" + TaskID ;

            SetValue(this.tabControl1.TabPages[pageName].Controls[conName].Controls[1].Controls[0], "Text", strLog);

        }

        //д�����¼�
        private void tManage_GData(object sender, cGatherDataEventArgs e)
        {
            //д�ɼ����ݵ�����Datagridview
            Int64 TaskID = e.TaskID;
            DataTable gData = e.gData;
            string conName="sCon" + TaskID ;
            string pageName="page" + TaskID ;

            SetValue(this.tabControl1.TabPages[pageName].Controls[conName].Controls[0].Controls[0], "gData", gData);

        }

        #endregion

        #region ί�д��� ���ں�̨�̵߳��� ����UI�̵߳ķ���������

        delegate void bindvalue(object Instance, string Property, object value);
        delegate object invokemethod(object Instance, string Method, object[] parameters);
        delegate object invokepmethod(object Instance, string Property, string Method, object[] parameters);
        delegate object invokechailmethod(object InstanceInvokeRequired, object Instance, string Method, object[] parameters);

        /// <summary>
        /// ί�����ö�������
        /// </summary>
        /// <param name="Instance">����</param>
        /// <param name="Property">������</param>
        /// <param name="value">����ֵ</param>
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
        /// ί��ִ��ʵ���ķ���
        /// </summary>
        /// <param name="Instance">��ʵ��</param>
        /// <param name="Method">������</param>
        /// <param name="parameters">�����б�</param>
        /// <returns>����ֵ</returns>
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
        /// ί��ִ��ʵ���ķ���
        /// </summary>
        /// <param name="InstanceInvokeRequired">����ؼ�����</param>
        /// <param name="Instance">��Ҫִ�з����Ķ���</param>
        /// <param name="Method">������</param>
        /// <param name="parameters">�����б�</param>
        /// <returns>����ֵ</returns>
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
        /// ί��ִ��ʵ�������Եķ���
        /// </summary>
        /// <param name="Instance">��ʵ��</param>
        /// <param name="Property">������</param>
        /// <param name="Method">������</param>
        /// <param name="parameters">�����б�</param>
        /// <returns>����ֵ</returns>
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
        /// ��ȡʵ��������ֵ
        /// </summary>
        /// <param name="ClassInstance">��ʵ��</param>
        /// <param name="PropertyName">������</param>
        /// <returns>����ֵ</returns>
        private static object GetPropertyValue(object ClassInstance, string PropertyName)
        {
            Type myType = ClassInstance.GetType();
            PropertyInfo myPropertyInfo = myType.GetProperty(PropertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            return myPropertyInfo.GetValue(ClassInstance, null);
        }
        /// <summary>
        /// ����ʵ��������ֵ
        /// </summary>
        /// <param name="ClassInstance">��ʵ��</param>
        /// <param name="PropertyName">������</param>
        private static void SetPropertyValue(object ClassInstance, string PropertyName, object PropertyValue)
        {
            Type myType = ClassInstance.GetType();
            PropertyInfo myPropertyInfo = myType.GetProperty(PropertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            myPropertyInfo.SetValue(ClassInstance, PropertyValue, null);
        }

        /// <summary>
        /// ִ��ʵ���ķ���
        /// </summary>
        /// <param name="ClassInstance">��ʵ��</param>
        /// <param name="MethodName">������</param>
        /// <param name="parameters">�����б�</param>
        /// <returns>����ֵ</returns>
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

        #region ������� ���� ֹͣ 
        //�����ɼ�����
        private cGatherTask AddRunTask()
        {

            //��ѡ���������ӵ�������
            //�����жϴ������Ƿ��Ѿ���ӵ�������,
            //����Ѿ���ӵ�����������Ҫѯ���Ƿ�����һ������ʵ��
            string tName = this.dataTask.SelectedCells[4].Value.ToString();
            bool IsExist = false;

            //��ʼ��ʼ���������е�����
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
                if (MessageBox.Show("��ѡ�������������Ѿ������������ڻ�������ͬ���Ƶ������Ѿ��������������Ƿ�ȷ�ϴ�������Ҫ���л���Ҫ���������еڶ���ʵ����",
                    "ϵͳѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
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

            //��ȡ����ִ��ID
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

            //�������������
            m_GatherControl.AddGatherTask(tData);

            tData = null;

            //������ӵ���������,��Ҫ����ӵ�����ִ���б���
            tr = null;

            return  m_GatherControl.TaskManage.FindTask(NewID);

        }

        private void StopTask()
        {
            cGatherTask t = null;

            //�жϵ�ǰѡ������ڵ�
            if (this.treeMenu.SelectedNode.Name == "nodRunning" && this.dataTask.SelectedCells.Count != 0)
            {
                //ִ������ִ�е�����
                t = m_GatherControl.TaskManage.FindTask(Int64.Parse(this.dataTask.SelectedCells[1].Value.ToString()));

                //����������
                m_GatherControl.Stop(t);

                //���������ɹ���ʾ��Ϣ
                ShowInfo("����ֹͣ", t.TaskName);

            }
        }

        #endregion

        #region ���̵Ĵ���
        private void notifyIcon1_MouseMove(object sender, MouseEventArgs e)
        {
            //��ʾ��ǰ�����������
            string s;

            s = "soukey��ժ [��Ѱ�]     " + "\n";
            s += "�������е�����: " + m_GatherControl.TaskManage.TaskListControl.RunningTaskList.Count + "\n";
            s += "���ڷ���������: " + m_PublishControl.PublishManage.ListPublish.Count;

            this.notifyIcon1.Text = s;

        }
        #endregion

        #region ����datagridview���б�ͷ

        //������ʾ��������Datalistview���б�ͷ
        private void ShowRunTask()
        {
            this.dataTask.Columns.Clear();
            this.dataTask.Rows.Clear();

            #region �˲���Ϊ�̶���ʾ �������͵����񶼱���̶���ʾ����
            DataGridViewImageColumn tStateImg = new DataGridViewImageColumn();
            tStateImg.HeaderText = "״̬";
            tStateImg.Width = 40;
            tStateImg.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tStateImg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataTask.Columns.Insert(0, tStateImg);

            //������,����ʾ����
            DataGridViewTextBoxColumn tID = new DataGridViewTextBoxColumn();
            tID.Name  = "������";
            tID.Width = 0;
            tID.Visible = false;
            this.dataTask.Columns.Insert(1, tID);

            //����״̬,����ʾ����
            DataGridViewTextBoxColumn tState = new DataGridViewTextBoxColumn();
            tState.Name = "����״̬";
            tState.Width = 0;
            tState.Visible = false;
            this.dataTask.Columns.Insert(2, tState);

            //����ͨ���ж�Datagridview�����ݾͿ�֪����ǰ���νṹѡ��Ľڵ�
            //���ڿ���(����)������ʾ״̬
            DataGridViewTextBoxColumn tTreeNode = new DataGridViewTextBoxColumn();
            tTreeNode.HeaderText = "��ǰ���Խṹ�Ľڵ�name";
            tTreeNode.Visible = false;
            this.dataTask.Columns.Insert(3, tTreeNode);

            #endregion

            DataGridViewTextBoxColumn tName = new DataGridViewTextBoxColumn();
            tName.HeaderText = "��������";
            tName.Width = 150;
            this.dataTask.Columns.Insert(4, tName);

            DataGridViewTextBoxColumn tType = new DataGridViewTextBoxColumn();
            tType.HeaderText = "��������";
            tType.Width = 80;
            this.dataTask.Columns.Insert(5, tType);

            DataGridViewTextBoxColumn Islogin = new DataGridViewTextBoxColumn();
            Islogin.HeaderText = "�Ƿ��¼";
            Islogin.Width = 80;
            this.dataTask.Columns.Insert(6, Islogin);

            DataGridViewTextBoxColumn GatheredUrlCount = new DataGridViewTextBoxColumn();
            GatheredUrlCount.HeaderText = "�����";
            GatheredUrlCount.Width = 50;
            GatheredUrlCount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataTask.Columns.Insert(7, GatheredUrlCount);

            DataGridViewTextBoxColumn tUrlCount = new DataGridViewTextBoxColumn();
            tUrlCount.HeaderText  = "�ɼ���";
            tUrlCount.Width = 50;
            tUrlCount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataTask.Columns.Insert(8, tUrlCount);

             
            DataGridViewProgressBarColumn tPro = new DataGridViewProgressBarColumn();
            tPro.HeaderText = "����";
            tPro.Width = 120;
            this.dataTask.Columns.Insert(9, tPro);

            DataGridViewTextBoxColumn tRunType = new DataGridViewTextBoxColumn();
            tRunType.HeaderText = "ִ������";
            tRunType.Width = 120;
            this.dataTask.Columns.Insert(10, tRunType);

            DataGridViewTextBoxColumn tExportFile = new DataGridViewTextBoxColumn();
            tExportFile.HeaderText  = "��������";
            tExportFile.Width = 1900;
            this.dataTask.Columns.Insert(11, tExportFile);

        }

        private void ShowPublishTask()
        {
            this.dataTask.Columns.Clear();
            this.dataTask.Rows.Clear();

            #region �˲���Ϊ�̶���ʾ �������͵����񶼱���̶���ʾ����
            DataGridViewImageColumn tStateImg = new DataGridViewImageColumn();
            tStateImg.HeaderText = "״̬";
            tStateImg.Width = 40;
            tStateImg.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tStateImg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataTask.Columns.Insert(0, tStateImg);

            //������,����ʾ����
            DataGridViewTextBoxColumn tID = new DataGridViewTextBoxColumn();
            tID.Name = "������";
            tID.Width = 0;
            tID.Visible = false;
            this.dataTask.Columns.Insert(1, tID);

            //����״̬,����ʾ����
            DataGridViewTextBoxColumn tState = new DataGridViewTextBoxColumn();
            tState.Name = "����״̬";
            tState.Width = 0;
            tState.Visible = false;
            this.dataTask.Columns.Insert(2, tState);

            //����ͨ���ж�Datagridview�����ݾͿ�֪����ǰ���νṹѡ��Ľڵ�
            //���ڿ���(����)������ʾ״̬
            DataGridViewTextBoxColumn tTreeNode = new DataGridViewTextBoxColumn();
            tTreeNode.HeaderText = "��ǰ���Խṹ�Ľڵ�name";
            tTreeNode.Visible = false;
            this.dataTask.Columns.Insert(3, tTreeNode);

            #endregion

            DataGridViewTextBoxColumn tName = new DataGridViewTextBoxColumn();
            tName.HeaderText = "��������";
            tName.Width = 150;
            this.dataTask.Columns.Insert(4, tName);

            DataGridViewTextBoxColumn PublishedCount = new DataGridViewTextBoxColumn();
            PublishedCount.HeaderText = "�ѵ���";
            PublishedCount.Width = 50;
            PublishedCount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataTask.Columns.Insert(5, PublishedCount);

            DataGridViewTextBoxColumn Count = new DataGridViewTextBoxColumn();
            Count.HeaderText = "����";
            Count.Width = 50;
            Count.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataTask.Columns.Insert(6, Count);


            DataGridViewProgressBarColumn tPro = new DataGridViewProgressBarColumn();
            tPro.HeaderText = "����";
            tPro.Width = 120;
            this.dataTask.Columns.Insert(7, tPro);


            DataGridViewTextBoxColumn PublishType = new DataGridViewTextBoxColumn();
            PublishType.HeaderText = "��������";
            PublishType.Width = 1900;
            this.dataTask.Columns.Insert(8, PublishType);
        }

        private void ShowCompletedTask()
        {
            this.dataTask.Columns.Clear();
            this.dataTask.Rows.Clear();

            #region �˲���Ϊ�̶���ʾ
            DataGridViewImageColumn tStateImg = new DataGridViewImageColumn();
            tStateImg.HeaderText = "״̬";
            tStateImg.Width = 40;
            this.dataTask.Columns.Insert(0, tStateImg);

            //������,����ʾ����
            DataGridViewTextBoxColumn tID = new DataGridViewTextBoxColumn();
            tID.Name = "������";
            tID.Width = 0;
            tID.Visible = false;
            this.dataTask.Columns.Insert(1, tID);

            //����״̬,����ʾ����
            DataGridViewTextBoxColumn tState= new DataGridViewTextBoxColumn();
            tState.Name = "����״̬";
            tState.Width = 0;
            tState.Visible = false;
            this.dataTask.Columns.Insert(2, tState);

            //����ͨ���ж�Datagridview�����ݾͿ�֪����ǰ���νṹѡ��Ľڵ�
            //���ڿ���(����)������ʾ״̬
            DataGridViewTextBoxColumn tTreeNode = new DataGridViewTextBoxColumn();
            tTreeNode.HeaderText = "��ǰ���Խṹ�Ľڵ�name";
            tTreeNode.Visible = false;
            this.dataTask.Columns.Insert(3, tTreeNode);
            #endregion

            DataGridViewTextBoxColumn tName = new DataGridViewTextBoxColumn();
            tName.HeaderText = "��������";
            tName.Width = 150;
            this.dataTask.Columns.Insert(4, tName);

            DataGridViewTextBoxColumn tType = new DataGridViewTextBoxColumn();
            tType.HeaderText = "��������";
            tType.Width = 80;
            this.dataTask.Columns.Insert(5, tType);

            //DataGridViewTextBoxColumn gatherUrlCount = new DataGridViewTextBoxColumn();
            //gatherUrlCount.HeaderText = "�ɹ��ɼ�";
            //gatherUrlCount.Width = 80;
            //gatherUrlCount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //this.dataTask.Columns.Insert(6, gatherUrlCount);

            //DataGridViewTextBoxColumn errUrlCount = new DataGridViewTextBoxColumn();
            //errUrlCount.HeaderText = "ʧ������";
            //errUrlCount.Width = 80;
            //errUrlCount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //this.dataTask.Columns.Insert(7, errUrlCount);

            DataGridViewTextBoxColumn tUrlCount = new DataGridViewTextBoxColumn();
            tUrlCount.HeaderText = "�ɼ�����";
            tUrlCount.Width = 80;
            tUrlCount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataTask.Columns.Insert(6, tUrlCount);

            DataGridViewTextBoxColumn tPro = new DataGridViewTextBoxColumn();
            tPro.HeaderText = "ִ������";
            tPro.Width = 120;
            this.dataTask.Columns.Insert(7, tPro);

            DataGridViewTextBoxColumn tExportFile = new DataGridViewTextBoxColumn();
            tExportFile.HeaderText = "��������";
            tExportFile.Width = 1900;
            this.dataTask.Columns.Insert(8, tExportFile);

        }

        private void ShowTaskInfo()
        {
            this.dataTask.Columns.Clear();
            this.dataTask.Rows.Clear();

            #region �Ȳ���Ϊ�̶���ʾ
            DataGridViewImageColumn tStateImg = new DataGridViewImageColumn();
            tStateImg.HeaderText = "״̬";
            tStateImg.Width = 40;
            tStateImg.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tStateImg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataTask.Columns.Insert(0, tStateImg);

            DataGridViewTextBoxColumn tID = new DataGridViewTextBoxColumn();
            tID.Name  = "������";
            tID.Width = 0;
            tID.Visible = false;
            this.dataTask.Columns.Insert(1, tID);

            //����״̬,����ʾ����
            DataGridViewTextBoxColumn tState = new DataGridViewTextBoxColumn();
            tState.Name = "������";
            tState.Width = 0;
            tState.Visible = false;
            this.dataTask.Columns.Insert(2, tState);

            //����ͨ���ж�Datagridview�����ݾͿ�֪����ǰ���νṹѡ��Ľڵ�
            //���ڿ���(����)������ʾ״̬
            DataGridViewTextBoxColumn tTreeNode = new DataGridViewTextBoxColumn();
            tTreeNode.HeaderText = "��ǰ���Խṹ�Ľڵ�name";
            tTreeNode.Visible = false;
            this.dataTask.Columns.Insert(3, tTreeNode);

            #endregion

            DataGridViewTextBoxColumn tName = new DataGridViewTextBoxColumn();
            tName.HeaderText = "��������";
            tName.Width = 150;
            this.dataTask.Columns.Insert(4, tName);

            DataGridViewTextBoxColumn tType = new DataGridViewTextBoxColumn();
            tType.HeaderText = "��������";
            tType.Width = 80;
            this.dataTask.Columns.Insert(5, tType);

            DataGridViewTextBoxColumn tLogin = new DataGridViewTextBoxColumn();
            tLogin.HeaderText = "�Ƿ��¼";
            tLogin.Width = 80;
            this.dataTask.Columns.Insert(6, tLogin);

            DataGridViewTextBoxColumn tUrlCount = new DataGridViewTextBoxColumn();
            tUrlCount.HeaderText = "�ɼ�����";
            tUrlCount.Width = 80;
            tUrlCount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
       
            this.dataTask.Columns.Insert(7, tUrlCount);

            DataGridViewTextBoxColumn tPro = new DataGridViewTextBoxColumn();
            tPro.HeaderText = "ִ������";
            tPro.Width = 120;

            this.dataTask.Columns.Insert(8, tPro);

            DataGridViewTextBoxColumn tExportFile = new DataGridViewTextBoxColumn();
            tExportFile.HeaderText = "��������";
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

                    //ֻҪ�����ݾͿ���ɾ��
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

                    //ֻҪ�����ݾͿ���ɾ��
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

        #region �Զ���ӿؼ�������ʾ����ִ�еĽ��

        private void AddTab(Int64 TaskID,string TaskName)
        {
            bool IsExist = false;
            int j = 0;

            //�жϴ������Ƿ��Ѿ������Tabҳ
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

            //�����������Ƶ���Ϣ
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

        ///��ʱˢ�½�����ʾ����Ϣ����Ҫ��ʾ��������ִ�е�����Ľ���
        ///��������(���)��״̬��ˢ��,״̬������Ϣˢ��
        private void timer1_Tick(object sender, EventArgs e)
        {
            int proI = 0;

            if (IsTimer == true)
            {
                IsTimer = false;

                if (this.treeMenu.SelectedNode.Name == "nodRunning" )
                {
                    //�����ǰѡ����ʼ����
                    //����m_GatherControl.TaskManage.TaskList���и���
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
                    //�����ǰѡ����ʼ����
                    //����m_GatherControl.TaskManage.TaskList���и���
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

      
        #region MSN ֪ͨģʽ ��̬��Ϣ��ʾ�����
        //����һ����������������ж����Ϣ����ͬʱ��ʾ�����

        private List<frmInfo> frmInfos = new List<frmInfo>();

        public void ShowInfo(string Title,string Info)
        {
            //��������д��ڴ��ڣ�����Ҫ�жϴ����Ƿ��Ѿ�
            //�Զ���ʾ��ɣ������ɣ���Ӽ�����ɾ��
            //������ϵĸ���Ӧ�����Ƚ��ȳ�����û����Queue
            while (frmInfos.Count > 0 && frmInfos[0].IsShow == false)
            {
                frmInfos.Remove(frmInfos[0]);
            }

            frmInfo fInfo = new frmInfo();
            fInfo.HeightMax = 85;//��������ĸ߶�
            fInfo.WidthMax = 221;//��������Ŀ��
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
           

            //���ҵ�ǰ���б���ʾ������
            //�����жϵ�ǰѡ�е����νڵ��Ƿ����������Ľڵ�
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
                //��ʾѡ���������ڵ�
                try
                {
                    string Filename = this.dataTask.SelectedCells[4].Value.ToString() + ".xml";
                    string tPath = this.treeMenu.SelectedNode.Tag.ToString();

                    LoadTaskInfo(tPath, Filename, cGlobalParas.FormState.Edit);
                }
                catch (System.IO.IOException)
                {
                    MessageBox.Show("��ָ���򿪵����񲻴��ڣ������ѱ�ɾ����ͨ������£�ϵͳ�Ὠ��������ı��ݣ�����ͨ�����빦�ܵ��������", "ϵͳ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("�����ص��������������ϢΪ��" + ex.Message + "��ͨ������£�ϵͳ�Ὠ��������ı��ݣ�����ͨ�����빦�ܵ��������", "ϵͳ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
            }
            else if (this.treeMenu.SelectedNode.Name == "nodRunning")
            {
                MessageBox.Show("��ǰ�����Ѿ����ص��������޷������޸Ĳ�����", "soukey��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        #region �˲��ִ��������ֹ��������ݣ��Ѿ������ϲɼ����������ݣ�
        //����һ�����������ڵ�������ʱ���н�������ˢ��
        delegate void ShowProgressDelegate(int totalMessages, int messagesSoFar, bool statusDone);

        //�ֶ���������ͬһʱ��ֻ�ܵ���һ�����񣬲��ܽ��ж���������ݵ���
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
                MessageBox.Show("�Ѿ����ֶ����������������У���ȴ�������������ɺ��ٽ����ֶ����ݵ�����", "Soukey��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string FileName;

            this.saveFileDialog1.OverwritePrompt = true;
            this.saveFileDialog1.Title = "��ָ���洢���ݵ��ļ���";
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
                MessageBox.Show("�Ѿ����ֶ����������������У���ȴ�������������ɺ��ٽ����ֶ����ݵ�����", "Soukey��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string FileName;

            this.saveFileDialog1.OverwritePrompt = true;
            this.saveFileDialog1.Title = "��ָ���洢���ݵ��ļ���";
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
            this.PrograBarTxt.Text = "�ֶ��������ݣ�" + tName + " " + "0/0";
            this.ExportProbar.Visible = true;

            //����һ����̨�߳����ڵ������ݲ���
            ShowProgressDelegate showProgress = new ShowProgressDelegate(ShowProgress);

            cExport eExcel = new cExport(this, showProgress, pType, FileName, (DataTable)tmp.DataSource);
            Thread t = new Thread(new ThreadStart(eExcel.RunProcess));
            t.IsBackground = true;
            t.Start();
            ShowInfo("�ֹ���������", "���������Ѿ���ʼ");

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
                ShowInfo("�ֶ���������", "���������Ѿ����");
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
            //�����жϵ�ǰ����Ŀؼ�
            //Ȼ����ȷ��ɾ�����Ƿ��໹������
            if (DelName == "treeMenu")
            {
                //ɾ�����Ƿ���
                if (this.treeMenu.SelectedNode.Name.Substring(0, 1) == "C")
                {
                    DelTaskClass();
                }
            }
            else if (DelName == "dataTask")
            {
                //ɾ���������񣬵���Ҫ�ж�ɾ�����Ǻ�������
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
            //�����жϵ�ǰ��Ҫ�������ݵ�Tab��datagridview
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
            //��ȡ��ѡ��
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
            
            //��ʼ������������Ϣ
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
                    //�ж���ֱ���˳�������С������
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
                MessageBox.Show("ϵͳ�����ļ�����ʧ�ܣ��ɴӰ�װ�ļ��п����ļ���SoukeyConfig.xml ��Soukey��ժ��װĿ¼�������ļ��𻵲���Ӱ��ϵͳ���У���������ϵͳ���ÿ����޷����棡", "Soukey��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
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

            //�ж��Ƿ�������е�����
            if (m_GatherControl.TaskManage.TaskListControl.RunningTaskList.Count != 0)
            {
                if (MessageBox.Show("���������������е������Ƿ��˳���", "Soukeyѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

            //����ر� ������ֹͣ�������е�����
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
            //�������� ��ָ��������ָ���ΪĬ�ϵ�״̬
            if (this.treeMenu.SelectedNode.Name == "nodRunning")
            {
                if (MessageBox.Show("��ѡ������������" + this.dataTask.SelectedCells[4].Value.ToString() + "���������񽫻ָ�����ĳ�ʼ״̬����ɾ���Ѿ��ɼ������ݣ���������Ѿ����������ݻ���ϵͳ�Զ����������ݣ��򲻻��յ�Ӱ�죬�Ƿ������",
                    "soukeyѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cGatherTask t = m_GatherControl.TaskManage.FindTask(Int64.Parse(this.dataTask.SelectedCells[1].Value.ToString()));
                    t.ResetTask();

                    //ɾ��Tabpage
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
            string s="������У�";

            s +=m_GatherControl.TaskManage.TaskListControl.RunningTaskList.Count + m_GatherControl.TaskManage.TaskListControl.StoppedTaskList.Count +  m_PublishControl.PublishManage.ListPublish.Count + "-������  ";
            s +=m_GatherControl.TaskManage.TaskListControl.RunningTaskList.Count + "-������  ";
            s +=  m_GatherControl.TaskManage.TaskListControl.StoppedTaskList.Count + "-��ֹͣ  ";
            s += m_PublishControl.PublishManage.ListPublish.Count + "-������";

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
                    this.StateInfo.Text = "����δ����";
                    break;
                case cGlobalParas.TaskState .Stopped :
                    this.StateInfo.Text = "������ֹͣ";
                    break;
                case cGlobalParas.TaskState.Completed :
                    this.StateInfo.Text = "���������";
                    break;
                case cGlobalParas.TaskState.Failed :
                    this.StateInfo.Text = "����ʧ��";
                    break;
                case cGlobalParas.TaskState.Pause :
                    this.StateInfo.Text = "������ͣ";
                    break;
                case cGlobalParas.TaskState.Running :
                    this.StateInfo.Text = "������������";
                    break;
                case cGlobalParas.TaskState.Started :
                    this.StateInfo.Text = "������������";
                    break;
                case cGlobalParas.TaskState.Waiting :
                    this.StateInfo.Text = "����ȴ�����";
                    break;
                case cGlobalParas.TaskState.Publishing :
                    this.StateInfo.Text = "�������ڷ���";
                    break;
                default:
                    break;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        //ÿ����ӽ���һ��ˢ�£��鿴��ǰ������״̬
        //�������״̬�����ı���Ҫ��������״̬
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (cTool.IsLinkInternet ()==false )
            {
                this.staIsInternet.Image =Bitmap.FromFile(Program.getPrjPath () +  "img\\a08.gif");
;
                this.staIsInternet.Text ="����";

                //�����⵽������ֹͣ��ǰ�������е�����

            }
            else
            {
                this.staIsInternet.Image = Bitmap.FromFile(Program.getPrjPath() + "img\\a07.gif");
                this.staIsInternet.Text ="����";

                //�����⵽���ߣ��������Ѿ�ֹͣ����Ҫ�ɼ�������
            }
        }


        #region ����������¼�����
        private void Publish_Complete(object sender, PublishCompletedEventArgs e)
        {
            InvokeMethod(this, "ShowInfo", new object[] { e.TaskName, "�ɹ�����" });

            InvokeMethod(this, "UpdateTaskPublished", new object[] { e.TaskID ,cGlobalParas.GatherResult.PublishSuccees});

            InvokeMethod(this, "UpdateStatebarTask", null);

        }

        private void Publish_Started(object sender, PublishStartedEventArgs e)
        {
            InvokeMethod(this, "ShowInfo", new object[] {  e.TaskName ,"�Ѿ���ʼ�������ݷ���" });

            InvokeMethod(this, "UpdateStatebarTask", null);
        }

        private void Publish_Error(object sender, PublishErrorEventArgs e)
        {
            InvokeMethod(this, "UpdateTaskPublished", new object[] { e.TaskID ,cGlobalParas.GatherResult.PublishFailed });

            InvokeMethod(this, "ShowInfo", new object[] { e.TaskName, "���񷢲�ʧ�ܣ��Ѿ�ת�����������" });

            InvokeMethod(this, "UpdateStatebarTask", null);

        }

        private void Publish_Failed(object sender, PublishFailedEventArgs e)
        {
            InvokeMethod(this, "UpdateTaskPublished", new object[] { e.TaskID ,cGlobalParas.GatherResult.PublishFailed });

            InvokeMethod(this, "ShowInfo", new object[] { e.TaskName,"���񷢲�ʧ�ܣ��Ѿ�ת�����������" });

            InvokeMethod(this, "UpdateStatebarTask", null);
        }

        #endregion


        //�������񷢲���ɵĹ���
        public void UpdateTaskPublished(Int64 TaskID,cGlobalParas.GatherResult tState)
        {
            //���Ѿ���ɷ�����������ӵ��������������ļ���
            Task.cTaskComplete t = new Task.cTaskComplete();
            t.InsertTaskComplete(TaskID, tState);
            t = null;

            //ɾ��taskrun�ڵ�
            cTaskRun tr = new cTaskRun();
            tr.LoadTaskRunData();
            tr.DelTask(TaskID);

            //�޸�Tabҳ������


            //ɾ��run�е�����ʵ���ļ�
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
                //���¼���������������Ϣ
                LoadCompleteTask();
            }
        }

        private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
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
            this.openFileDialog1.Title = "��ָ�������ļ���";

            openFileDialog1.InitialDirectory = Program.getPrjPath() + "tasks";
            openFileDialog1.Filter = "Soukey Task Files(*.xml)|*.xml";


            if (this.openFileDialog1.ShowDialog() ==DialogResult.Cancel )
            {
                return;
            }

            string FileName = this.openFileDialog1.FileName;
            string TaskClass="";
            string NewFileName = "";

            //��֤�����ʽ�Ƿ���ȷ
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

                 //��������ķ��ർ��ָ����Ŀ¼
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
                    MessageBox.Show("������������Ѿ����ڣ���ѡ��������Ҫ���������", "ϵͳ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Task.cTaskClass cTClass = new Task.cTaskClass();

                if (!cTClass.IsExist (TaskClass) && TaskClass !="")
                {
                    //��ʾ��һ�����������
                    int TaskClassID = cTClass.AddTaskClass(TaskClass, TaskPath);
                    cTClass = null;

                    //�������νṹ���������ڵ�
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
                    MessageBox.Show("��ָ���������Ѿ����ڣ����������޷�ͨ��Soukey��ժ����������Ϊ������ɾ���˴����񣬵������ļ������ڣ���ͨ���޸Ĵ��ļ������ٽ��������������", "ϵͳ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //�������������ļ�
                t.InsertTaskIndex(TaskPath);

                t = null;

                MessageBox.Show("��ָ���������ѳɹ����롰" + TaskClass + "�������£�", "ϵͳ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("������Ϣ��" + ex.Message + "������ʧ�ܣ�", "ϵͳ����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MessageBox.Show("�޸�������෢�����󣬴�����Ϣ��" + ex.Message ,"ϵͳ����",MessageBoxButtons.OK ,MessageBoxIcon .Error);
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

            //�ж��Ƿ�Ϊ�����������������Ҫ�����ϷŲ���
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