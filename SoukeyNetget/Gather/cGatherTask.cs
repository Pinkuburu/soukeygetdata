using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using SoukeyNetget.Gather;

///功能：采集任务处理
///完成时间：2009-3-2
///作者：一孑
///遗留问题：无
///开发计划：无
///说明：无 
///版本：00.90.00
///修订：无
namespace SoukeyNetget.Gather
{
    ///采集任务类,根据一个采集任务(即一个Task.xml文件)

    public class cGatherTask
    {
        private cTaskData m_TaskData;
        private cGlobalParas.TaskState m_State;
        private List<cGatherTaskSplit> m_list_GatherTaskSplit;
        private bool m_IsDataInitialized;
        private bool m_IsInitialized;
        private cGatherManage m_TaskManage;

        #region 构造 析构 

        /// 初始化采集任务对象,并根据任务数据确定此任务是否
        /// 由多个线程来完成,如果是,则进行任务分解
        internal cGatherTask(cGatherManage taskManage, cTaskData taskData)
        {
            m_TaskManage = taskManage;
            m_TaskData = taskData;
            m_State = TaskData.TaskState;

            m_list_GatherTaskSplit = new List<cGatherTaskSplit>();
            
            //当任务数据传进来之后,直接对当前任务进行任务分解,
            //是否需要多线程进行,并初始化相关数据内容
            SplitTask();

            //开始初始化任务
            TaskInit();
        }
        #endregion


        #region 属性

        /// <summary>
        /// 事件 线程同步锁
        /// </summary>
        private readonly Object m_eventLock = new Object();
        /// <summary>
        /// 文件 线程同步锁
        /// </summary>
        private readonly Object m_fstreamLock = new Object();
        public cGatherManage TaskManage
        {
            get { return m_TaskManage; }
        }

        public bool IsInitialized
        {
            get { return m_IsInitialized; }
            set { m_IsInitialized = value; }
        }

        /// <summary>
        /// 设置/获取 当前任务数据对象
        /// </summary>
        internal cTaskData TaskData
        {
            get { return m_TaskData; }
        }
        /// <summary>
        /// 获取采集网址的个数
        /// </summary>
        public int UrlCount
        {
            get { return m_TaskData.UrlCount; }
        }
        /// <summary>
        /// 设置/获取 单任务采集的线程数
        /// </summary>
        public int ThreadCount
        {
            //get { return m_TaskData.ThreadCount; }
            set { m_TaskData.ThreadCount = value; }
        }
        /// <summary>
        /// 获取已完成采集任务的数量
        /// </summary>
        public int GatheredUrlCount
        {
            get { return m_TaskData.GatheredUrlCount; }
        }

        /// <summary>
        /// 设置/获取 任务ID
        /// </summary>
        public Int64 TaskID
        {
            get { return m_TaskData.TaskID; }
            //set { m_TaskData.TaskID = value; }
        }

        /// <summary>
        /// 设置/获取 任务名
        /// </summary>
        public string TaskName
        {
            get { return m_TaskData.TaskName; }
            //set { m_TaskData.TaskName = value; }
        }
        /// <summary>
        /// 获取网页的地址 集合
        /// </summary>
        public List<Task.cWebLink> Weblink
        {
            get { return m_TaskData.Weblink; }
        }

        public List<Task.cWebpageCutFlag> GatherFlag
        {
            get { return  m_TaskData.CutFlag; }
        }

        public string TempFile
        {
            get { return m_TaskData.tempFileName; }
        }

        public cGlobalParas.PublishType  PublishType
        {
            get { return m_TaskData.PublishType; }
        }

        public bool IsLogin
        {
            get { return m_TaskData.IsLogin; }
        }

        public string ExportFileName
        {
            get { return m_TaskData.ExportFileName; }
        }

        public cGlobalParas.TaskType TaskType
        {
            get { return m_TaskData.TaskType; }
        }
       
        /// <summary>
        /// 获取采集任务的运行类型
        /// </summary>
        public cGlobalParas.TaskRunType RunType
        {
            get { return m_TaskData.RunType; }
        }
        /// <summary>
        /// 是否已经采集完成
        /// </summary>
        public bool IsCompleted
        {
            get { return GatheredUrlCount ==UrlCount ; }
        }

        /// <summary>
        /// 分解任务类 结合
        /// </summary>
        public List<cGatherTaskSplit> TaskSplit
        {
            get { return m_list_GatherTaskSplit; }
            set { m_list_GatherTaskSplit = value; }
        }

        #endregion


        #region 事件触发 任务状态触发
        /// 任务状态改变的事件触发
        /// /// 设置/获取 任务状态 （仅内部使用，触发事件）
        /// 
        public cGlobalParas.TaskState TaskState
        {
            get { return m_State; }
        }

        public cGlobalParas.TaskState State
        {
            get { return m_State; }
            set
            {
                cGlobalParas.TaskState tmp = m_State;
                m_State = value;
                TaskStateChangedEventArgs evt = null;

                if (e_TaskStateChanged != null)
                {
                    
                    evt = new TaskStateChangedEventArgs(TaskID, tmp, m_State);
                    e_TaskStateChanged(this, evt);
                }

                // 注意，所以涉及任务状态变更的事件都在此处理
                bool cancel = (evt != null && evt.Cancel);

                switch (m_State)
                {
                    case cGlobalParas.TaskState.Aborted:
                        // 触发 任务取消 事件
                        if (e_TaskAborted != null)
                        {
                            e_TaskAborted(this, new cTaskEventArgs(cancel));
                        }
                        break;
                    case cGlobalParas.TaskState.Completed:
                        // 触发 任务完成 事件
                        if (e_TaskCompleted != null)
                        {
                            e_TaskCompleted(this, new cTaskEventArgs(cancel));
                        }
                        break;
                    case cGlobalParas.TaskState.Failed:
                        // 触发 任务失败 事件
                        if (e_TaskFailed != null)
                        {
                            e_TaskFailed(this, new cTaskEventArgs(cancel));
                        }
                        break;
                    case cGlobalParas.TaskState.Started:
                        // 触发 任务开始 事件
                        m_TaskManage.EventProxy.AddEvent(delegate()
                        {
                            if (e_TaskStarted != null)
                            {
                                e_TaskStarted(this, new cTaskEventArgs(cancel));
                            }
                        });
                        break;
                    case cGlobalParas.TaskState.Stopped:
                        m_TaskManage.EventProxy.AddEvent(delegate()
                        {
                            //WriteToFile();
                            // 触发 任务停止 事件
                            if (e_TaskStopped != null)
                            {
                                e_TaskStopped(this, new cTaskEventArgs(cancel));
                            }
                        });
                        break;
                    case cGlobalParas.TaskState.Waiting:
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region 事件
        
        /// 采集任务完成事件
        private event EventHandler<cTaskEventArgs> e_TaskCompleted;
        internal event EventHandler<cTaskEventArgs> TaskCompleted
        {
            add { lock (m_eventLock) { e_TaskCompleted += value; } }
            remove { lock (m_eventLock) { e_TaskCompleted -= value; } }
        }

        /// 采集任务采集失败事件
        private event EventHandler<cTaskEventArgs> e_TaskFailed;
        internal event EventHandler<cTaskEventArgs> TaskFailed
        {
            add { lock (m_eventLock) { e_TaskFailed += value; } }
            remove { lock (m_eventLock) { e_TaskFailed -= value; } }
        }
        
        /// 采集任务开始事件
        private event EventHandler<cTaskEventArgs> e_TaskStarted;
        internal event EventHandler<cTaskEventArgs> TaskStarted
        {
            add { lock (m_eventLock) {  e_TaskStarted += value; } }
            remove { lock (m_eventLock) {  e_TaskStarted -= value; } }
        }

        /// 采集任务停止事件
        private event EventHandler<cTaskEventArgs> e_TaskStopped;
        internal event EventHandler<cTaskEventArgs> TaskStopped
        {
            add { lock (m_eventLock) {  e_TaskStopped += value; } }
            remove { lock (m_eventLock) {  e_TaskStopped -= value; } }
        }

        /// 采集任务取消事件
        private event EventHandler<cTaskEventArgs> e_TaskAborted;
        internal event EventHandler<cTaskEventArgs> TaskAborted
        {
            add { lock (m_eventLock) { e_TaskAborted += value; } }
            remove { lock (m_eventLock) { e_TaskAborted -= value; } }
        }

        /// 采集任务错误事件
        private event EventHandler<TaskErrorEventArgs> e_TaskError;
        internal event EventHandler<TaskErrorEventArgs> TaskError
        {
            add { lock (m_eventLock) {  e_TaskError += value; } }
            remove { lock (m_eventLock) {  e_TaskError -= value; } }
        }

        /// 任务状态变更事件,每当任务状态发生变更时进行处理,
        /// 并触发界面此事件,用于界面状态的改变
        private event EventHandler<TaskStateChangedEventArgs> e_TaskStateChanged;
        internal event EventHandler<TaskStateChangedEventArgs> TaskStateChanged
        {
            add { lock (m_eventLock) {  e_TaskStateChanged += value; } }
            remove { lock (m_eventLock) {  e_TaskStateChanged -= value; } }
        }

        /// 采集任务分解初始化完成事件
        private event EventHandler<TaskInitializedEventArgs> e_TaskThreadInitialized;
        internal event EventHandler<TaskInitializedEventArgs> TaskThreadInitialized
        {
            add { lock (m_eventLock) {  e_TaskThreadInitialized += value; } }
            remove { lock (m_eventLock) { e_TaskThreadInitialized -= value; } }
        }

        /// <summary>
        /// 采集日志事件
        /// </summary>
        private event EventHandler<cGatherTaskLogArgs> e_Log;
        internal event EventHandler<cGatherTaskLogArgs> Log
        {
            add { e_Log += value; }
            remove { e_Log -= value; }
        }

        /// <summary>
        /// 采集数据事件
        /// </summary>
        private event EventHandler<cGatherDataEventArgs> e_GData;
        internal event EventHandler<cGatherDataEventArgs> GData
        {
            add { e_GData += value; }
            remove { e_GData -= value; }
        }

        #endregion

        #region 任务控制 启动 停止 重启 取消

        /// 开始任务
        public void Start()
        {
            // 确保位初始化的任务先进行初始化（包括从文件读取的任务信息）
            if (m_State !=cGlobalParas.TaskState.Started && m_TaskManage != null)
            {
                TaskInit();
                StartAll();
            }
        }

        /// 启动所有采集任务线程 如果没有分解任务,则启动的单个线程进行采集
        private void StartAll()
        {
            foreach (cGatherTaskSplit dtc in m_list_GatherTaskSplit)
            {
                dtc.Start();
            }
            State = cGlobalParas.TaskState.Started;
        }

        /// 任务准备就绪（等待开始）
        public void ReadyToStart()
        {
            if (m_State != cGlobalParas.TaskState.Started && m_State != cGlobalParas.TaskState.Completed)
            {
                State = cGlobalParas.TaskState.Waiting;
            }
        }

        /// 停止任务
        public void Stop()
        {
            foreach (cGatherTaskSplit dtc in m_list_GatherTaskSplit)
            {
                dtc.Stop();
            }
            State = cGlobalParas.TaskState.Stopped;
        }

        /// 取消任务（移除任务）
        public void Abort()
        {
            foreach (cGatherTaskSplit dtc in m_list_GatherTaskSplit)
            {
                dtc.Stop();
            }
            State = cGlobalParas.TaskState.Aborted;
        }
        #endregion

        #region  类方法 内部使用

        //根据指定的任务ID对当前的任务进行分解，如果有导航页，也需要在此进行
        //分解
        //并初始化此任务的关键数据
        private void SplitTask()
        {
            cGatherTaskSplit dtc;
            List<Task.cWebLink> tWeblink;
            Task.cTask t= new Task.cTask();
            //m_TaskData.TaskID = e.TaskID;

            //根据指定的TaskID加载任务地址信息
            try
            {
                t.LoadTask(Int64.Parse (m_TaskData.TaskID.ToString ()));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            ////加载页面的采集起始位置和终止位置
            ///此两项数据不在taskrun中存储，是在任务的xml文件中存储
            ///但m_TaskData是按照taskrun来加载的数据，所以无法加载此两
            ///项值和采集页面的规则及网址。
            ///为什么从taskrun中加载，是因为在索引taskrun的时候可以显示界面
            ///信息，所以就共用了一个加载信息的内容
            m_TaskData.TaskDemo =t.TaskDemo ;
            m_TaskData.StartPos = t.StartPos;
            m_TaskData.EndPos = t.EndPos;
            m_TaskData.Cookie = t.Cookie;
            m_TaskData.WebCode = (cGlobalParas.WebCode) int.Parse ( t.WebCode);
            m_TaskData.IsLogin = t.IsLogin;
            m_TaskData.LoginUrl = t.LoginUrl;
            m_TaskData.PublishType = (cGlobalParas.PublishType) int.Parse (t.ExportType);
            m_TaskData.IsUrlEncode = t.IsUrlEncode;
            m_TaskData.UrlEncode = t.UrlEncode;

            ////加载网页地址数据及采集标志数据
            ////再次去处理如果带有参数的网址,则需要进行分解
            ////确保加载的网址肯定是一个有效的网址
            ////注意,此时由于有可能分解任务信息,所以,网址数量在此会发生变化,所以,最终还需修改网址数据
            Task.cWebLink w;
            Task.cUrlAnalyze u = new Task.cUrlAnalyze();

            for (int i = 0; i < t.WebpageLink.Count; i++)
            {
                if (Regex.IsMatch(t.WebpageLink[i].Weblink.ToString(), "{.*}"))
                {
                    List<string> Urls;

                    if (m_TaskData.IsUrlEncode == true)
                    {
                        Urls = u.SplitWebUrl(t.WebpageLink[i].Weblink.ToString(), m_TaskData.IsUrlEncode, m_TaskData.UrlEncode);
                    }
                    else
                    {
                        Urls = u.SplitWebUrl(t.WebpageLink[i].Weblink.ToString(), m_TaskData.IsUrlEncode);
                    }
                    
                    //开始添加m_TaskData.weblink数据
                    for (int j=0;j<Urls.Count ;j++)
                    {
                        w = new Task.cWebLink();
                        w.IsGathered = t.WebpageLink[i].IsGathered;
                        w.IsNavigation = t.WebpageLink[i].IsNavigation;
                        w.IsNextpage = t.WebpageLink[i].IsNextpage;
                        w.IsOppPath = t.WebpageLink[i].IsOppPath;
                        w.NagRule = t.WebpageLink[i].NagRule;
                        w.NextPageRule = t.WebpageLink[i].NextPageRule;
                        w.Weblink = Urls[j].ToString();
                        m_TaskData.Weblink.Add(w);
                        w = null;
                    }

                }
                else
                {
                    m_TaskData.Weblink.Add(t.WebpageLink[i]);
                }

            }

            u=null;

            m_TaskData.CutFlag = t.WebpageCutFlag;

            //重新初始化UrlCount
            //m_TaskData.UrlCount = m_TaskData.Weblink.Count;

            //开始进行任务分块,但此任务的Url数必须大于线程数,且线程数>1
            if (m_TaskData.UrlCount > m_TaskData.ThreadCount && m_TaskData.ThreadCount > 1)
            {
                int SplitUrlCount = (int)Math.Ceiling((decimal)m_TaskData.UrlCount / (decimal)m_TaskData.ThreadCount);

                //设置每个分解任务的起始Url索引和终止的Url索引
                int StartIndex = 0;
                int EndIndex = 0;
                int j = 0;

                //for (int i = 1; i <= SplitUrlCount; i++)
                for (int i = 1; i <= m_TaskData.ThreadCount; i++)
                {
                    StartIndex = EndIndex;
                    if (i == m_TaskData.ThreadCount)
                    {
                        EndIndex = m_TaskData.Weblink.Count;
                    }
                    else
                    {
                        //EndIndex = i * m_TaskData.ThreadCount;
                        EndIndex = i * SplitUrlCount;
                    }

                    dtc = new cGatherTaskSplit(m_TaskManage, m_TaskData.TaskID,m_TaskData.WebCode ,m_TaskData.IsUrlEncode ,m_TaskData.UrlEncode , m_TaskData.Cookie , m_TaskData.StartPos, m_TaskData.EndPos);

                    tWeblink = new List<Task.cWebLink>();

                    for (j = StartIndex; j < EndIndex; j++)
                    {
                        tWeblink.Add(m_TaskData.Weblink[j]);
                    }

                    dtc.SetSplitData(StartIndex, EndIndex - 1, tWeblink, m_TaskData.CutFlag);

                    m_TaskData.TaskSplitData.Add(dtc.TaskSplitData);

                    tWeblink = null;
                    dtc = null;

                }

            }
            else
            {
                dtc = new cGatherTaskSplit(m_TaskManage, m_TaskData.TaskID, m_TaskData.WebCode, m_TaskData.IsUrlEncode, m_TaskData.UrlEncode, m_TaskData.Cookie, m_TaskData.StartPos, m_TaskData.EndPos);
                dtc.SetSplitData(0, m_TaskData.UrlCount - 1, m_TaskData.Weblink, m_TaskData.CutFlag);
                m_TaskData.TaskSplitData.Add(dtc.TaskSplitData);
                //m_list_GatherTaskSplit.Add(dtc);
            }

            t = null;
            dtc = null;
        }

        //更新cookie的值，cookie的值会根据用户的输入情况发生变化
        public void UpdateCookie(string cookie)
        {
            this.TaskData.Cookie = cookie;

            foreach (cGatherTaskSplit tp in m_list_GatherTaskSplit)
            {
                tp.UpdateCookie(cookie);
            }

        }

        /// 初始化采集任务线程
        private void TaskInit()
        {
            if (!m_IsDataInitialized)
            {
                if (m_list_GatherTaskSplit.Count > 0)
                {   // 清理可能存在的子线程
                    foreach (cGatherTaskSplit dtc in m_list_GatherTaskSplit)
                    {
                        dtc.Stop();
                    }
                    m_list_GatherTaskSplit.Clear();
                }

                if (IsCompleted)
                {   
                    // 修改此采集任务的状态为已采集完成,设置为状态为已完成，不触发事件
                    m_State = cGlobalParas.TaskState.Completed;
                }
                else
                {
                    if (m_TaskData.TaskSplitData.Count  > 0)
                    {   
                        // 
                        //
                        //
                        foreach (cTaskSplitData configData in m_TaskData.TaskSplitData)
                        {
                            m_list_GatherTaskSplit.Add(new cGatherTaskSplit(m_TaskManage, m_TaskData.TaskID, m_TaskData.WebCode, m_TaskData.IsUrlEncode, m_TaskData.UrlEncode, m_TaskData.Cookie, m_TaskData.StartPos, m_TaskData.EndPos, configData));
                        }

                    }
                    else
                    {   // 新任务，则新建子线程
                        m_list_GatherTaskSplit.Add(new cGatherTaskSplit(m_TaskManage, m_TaskData.TaskID, m_TaskData.WebCode, m_TaskData.IsUrlEncode, m_TaskData.UrlEncode, m_TaskData.Cookie, m_TaskData.StartPos, m_TaskData.EndPos));
                    }


                    foreach (cGatherTaskSplit dtc in m_list_GatherTaskSplit)
                    {   
                        // 初始化所有子线程
                        TaskEventInit(dtc);
                    }
                }

                m_IsDataInitialized = true;
            }
        }

        //将分解任务事件进行绑定
        private void TaskEventInit(cGatherTaskSplit dtc)
        {
            if (!dtc.IsInitialized)
            {
                // 绑定 初始化事件、完成事件
                dtc.TaskInit += this.TaskWorkThreadInit;
                dtc.Completed += this.TaskWorkThreadCompleted;
                dtc.GUrlCount += this.onGUrlCount;
                dtc.Log += this.onLog;
                dtc.GData += this.onGData;
                dtc.Error += this.TaskThreadError;
                dtc.IsInitialized = true;
            }
        }



        /// 重置任务为未初始化状态
        internal void ResetTaskState()
        {
            e_TaskCompleted = null;
            e_TaskStarted = null;
            e_TaskError = null;
            e_TaskStateChanged = null;
            e_TaskStopped = null;
            e_TaskFailed = null;
            e_TaskAborted = null;
            e_TaskThreadInitialized = null;
            this.State = cGlobalParas.TaskState.UnStart;

            m_IsInitialized = false;

            e_Log = null;
            e_GData = null;
        }

        /// 重置采集任务为未启动状态
        internal void ResetTaskData()
        {
            // 停止任务
            Stop();

            // 清理子线程
            //m_list_GatherTaskSplit.Clear();

            m_TaskData.GatheredUrlCount = 0;
            Task.cTaskRun t = new Task.cTaskRun();
            t.LoadSingleTask(m_TaskData.TaskID);
            m_TaskData.UrlCount = t.GetUrlCount(0);
            t = null;

            //m_TaskData.TaskSplitData.Clear ();
            //m_IsDataInitialized = false;
        }

        /// 清理任务数据
        internal void Remove()
        {
            ResetTaskState();
        }

        //重置任务
        public void ResetTask()
        {
            //ResetTaskState();
            ResetTaskData();
            //SplitTask();
            //TaskInit();
        }

        #endregion

        #region 公有方法

        /// 将所有缓存数据写入文件
        public void WriteToFile()
        {

        }


        #endregion

        #region  响应分解采集任务(子线程)事件

        /// 任务初始化,由分解任务触发,
        private void TaskWorkThreadInit(object sender, TaskInitializedEventArgs e)
        {
            cGatherTaskSplit dtc = (cGatherTaskSplit)sender;
            m_TaskData.TaskID  =e.TaskID ;

            if (e_TaskThreadInitialized != null)
            {
                // 代理触发 任务初始化 事件
                m_TaskManage.EventProxy.AddEvent(delegate()
                {
                    e_TaskThreadInitialized(this, new TaskInitializedEventArgs(m_TaskData.TaskID));
                });
            }
            
        }

        /// 分解采集任务 线程完成 事件处理
        private void TaskWorkThreadCompleted(object sender, cTaskEventArgs e)
        {
            cGatherTaskSplit dtc = (cGatherTaskSplit)sender;

            ///任务完成时，立即写入文件
            
            if (UrlCount  == m_TaskData.UrlCount )
            {  
                // 任务采集完成
                onTaskCompleted();
            }
        }

        private void onTaskCompleted()
        {
            if (m_TaskData.UrlCount  == m_TaskData.GatheredUrlCount  && m_State != cGlobalParas.TaskState.Completed )
            {   
               
                // 设置为完成状态，触发任务完成事件
                State = cGlobalParas.TaskState.Completed;
            }
        }


        /// 处理 分解采集任务 错误事件
        private void TaskThreadError(object sender, TaskThreadErrorEventArgs e)
        {
            cGatherTaskSplit gt = (cGatherTaskSplit)sender;

            //如果出错调用此事件,也表示完成了一个网址的采集,但是出错了
            m_TaskData.GatheredUrlCount++;

            if (gt.ErrorCount >= cGatherManage.MaxErrorCount)
            {   
                // 达到最大错误数，停止当前线程
                bool failed = true;

                // 如果当前任务所有的线程都停止了，则判断为任务失败
                foreach (cGatherTaskSplit dtc in m_list_GatherTaskSplit)
                {
                    if (!gt.Equals(dtc) && dtc.IsThreadAlive)
                    {
                        failed = false;
                        break;
                    }
                }
                if (failed)
                {
                    State = cGlobalParas.TaskState.Failed;
                }
            }
            else
            {
                if (e_TaskError != null)
                {
                    e_TaskError(this, new TaskErrorEventArgs(gt, e.Error));
                }
            }
        }

        //处理日志事件
        public void onLog(object sender, cGatherTaskLogArgs e)
        {
            if (e_TaskStarted != null && !e.Cancel)
            {
                e_Log(sender, e);
            }
        }

        //处理数据事件
        public void onGData(object sender, cGatherDataEventArgs e)
        {
            if (e_TaskStarted != null && !e.Cancel)
            {
                e_GData(sender, e);
            }
        }

        //采集网址计数事件,每调用此事件一次,则加一,表示采集完成一个网址
        public void onGUrlCount(object sender, cGatherUrlCountArgs e)
        {
            if (e.UrlCount == 0)
            {
                m_TaskData.GatheredUrlCount++;
            }
            else
            {
                m_TaskData.UrlCount += e.UrlCount - 1;
            }
        }
        #endregion

        
    }
}
