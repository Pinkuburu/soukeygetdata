using System;
using System.Collections.Generic;
using System.Text;
using System.Threading ;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;

///功能：采集任务 分解子任务处理
///完成时间：2009-6-1
///作者：一孑
///遗留问题：无
///开发计划：无
///说明：无 
///版本：01.00.00
///修订：无
namespace SoukeyNetget.Gather
{

    //这是采集任务的最小单元,针对于一个采集任务而言,是一个URL地址集合
    //存在有多个甚至上千个URL地址,为了提高性能,对URl采集采用了多线程的
    //处理方式,所以针对一个任务要进行拆分,拆分的依据是此任务定制的线程
    //数,任务拆后开始执行.此类主要就是执行这么一个最小单元的任务.

    public class cGatherTaskSplit : IDisposable 
    {
        private Int64 m_TaskID;
        private string m_gStartPos;
        private string m_gEndPos;
        private string m_Cookie;
        private cTaskSplitData m_TaskSplitData;
        private delegate void work();
        private int m_ErrorCount;
        private Thread m_Thread;
        private cGatherManage m_TaskManage;
        private bool m_IsInitialized;
        private int m_Waittime;
        private cGlobalParas.WebCode m_WebCode;
        private bool m_IsUrlEncode;
        private string m_UrlEncode;

        private bool m_ThreadRunning = false;


        #region 构造类，并初始化相关数据

        ///构造函数传参的方式太恶心了，是最初设计考虑不周的问题，需要
        ///下一版修改
        internal cGatherTaskSplit(cGatherManage TaskManage, Int64 TaskID, cGlobalParas.WebCode webCode, bool IsUrlEncode, string UrlEncode, string strCookie, string StartPos, string EndPos,string sPath)
        {
            //m_mstream = new MemoryStream();
            m_TaskManage = TaskManage;
            m_TaskID = TaskID;
            m_gStartPos = StartPos;
            m_gEndPos = EndPos;
            m_SavePath = sPath;
            m_Cookie = strCookie;
            m_WebCode = webCode;
            m_IsUrlEncode = IsUrlEncode;
            m_UrlEncode = UrlEncode;
            m_TaskSplitData = new cTaskSplitData ();
            m_ThreadState = cGlobalParas.GatherThreadState.Stopped;

            m_GatherData = new DataTable();
        }

        //表示有分解的任务
        internal cGatherTaskSplit(cGatherManage TaskManage, Int64 TaskID, cGlobalParas.WebCode webCode, bool IsUrlEncode, string UrlEncode, string strCookie, string StartPos, string EndPos, string sPath, cTaskSplitData TaskSplitData)
        {
            m_TaskManage = TaskManage;
            m_TaskID = TaskID;
            m_gStartPos = StartPos;
            m_gEndPos = EndPos;
            m_SavePath = sPath;
            m_Cookie = strCookie;
            m_WebCode = webCode;
            m_IsUrlEncode = IsUrlEncode;
            m_UrlEncode = UrlEncode;
            m_TaskSplitData = TaskSplitData;
            //m_IsDataInitialized = true;
            m_ThreadState = cGlobalParas.GatherThreadState.Stopped;

            m_GatherData = new DataTable();
        }

        public void UpdateCookie(string cookie)
        {
            m_Cookie = cookie;
        }

        ~cGatherTaskSplit()
        {
            Dispose(false);
        }

        #endregion


        #region 属性


        /// <summary>
        /// 事件 线程同步锁
        /// </summary>
        private readonly Object m_eventLock = new Object();
        /// <summary>
        /// 缓存 线程同步锁
        /// </summary>
        private readonly Object m_mstreamLock = new Object();

        /// <summary>
        /// 获取当前线程的错误次数
        /// </summary>
        public int ErrorCount
        {
            get { return m_ErrorCount; }
        }

        /// <summary>
        /// 获取/设置 分解任务初始化状态
        /// </summary>
        internal bool IsInitialized
        {
            get { return m_IsInitialized; }
            set { m_IsInitialized = value; }
        }

        /// <summary>
        /// 设置/获取 等待时间（毫秒）
        /// </summary>
        internal int Waittime
        {
            get { return m_Waittime; }
            set { m_Waittime = value; }
        }

        /// <summary>
        /// 设置/获取采集任务管理器
        /// </summary>
        internal cGatherManage TaskManage
        {
            get { return m_TaskManage; }
        }

        private DataTable m_GatherData;
        public DataTable GatherData
        {
            get { return m_GatherData; }
            set { m_GatherData = value; }
        }

        private string m_SavePath;
        public string SavePath
        {
            get { return m_SavePath; }
            set { m_SavePath = value; }
        }

         /// <summary>
        /// 分解采集任务是否已经完成
        /// </summary>
        public bool IsCompleted
        {
            //get { return m_TaskSplitData.GatheredUrlCount == m_TaskSplitData.UrlCount ; }
            get { return m_TaskSplitData.UrlCount > 1 && m_TaskSplitData.GatheredUrlCount + m_TaskSplitData.GatheredErrUrlCount  == m_TaskSplitData.TrueUrlCount; }
        }
        /// <summary>
        /// 分解的采集任务是否已经采集完成
        /// </summary>
        public bool IsGathered
        {
            get { return m_TaskSplitData.UrlCount > 0 && m_TaskSplitData.GatheredUrlCount +m_TaskSplitData.GatheredErrUrlCount == m_TaskSplitData.TrueUrlCount ; }
        }
        /// <summary>
        /// 获取一个状态值表示当前线程是否处于合法运行状态 [子线程内部使用]
        /// </summary>
        private bool IsCurrentRunning
        {
            get { return ThreadState  ==cGlobalParas.GatherThreadState.Started  && Thread.CurrentThread.Equals(m_Thread); }
        }

        /// <summary>
        /// 获取当前线程是否已经停止，注意IsThreadRunning只是一个标志
        /// 并不能确定线程的状态，只是告诉线程需要停止了
        /// IsStop是用于外部任务检查此分解任务是否停止使用
        /// </summary>
        public bool IsStop
        {
            get { return m_ThreadRunning ==false && this.IsCurrentRunning==false;  }
        }
        

        /// <summary>
        /// 当前块的工作线程
        /// </summary>
        internal Thread WorkThread
        {
            get { return m_Thread; }
        }

        /// <summary>
        /// 获取当前线程的执行状态
        /// </summary>
        internal bool IsThreadAlive
        {
            get { return m_Thread != null && m_Thread.IsAlive; }
        }

        internal cTaskSplitData  TaskSplitData
        {
            get { return m_TaskSplitData; }
        }
        /// <summary>
        /// 分解任务数据是否已初始化
        /// </summary>
        //public bool IsDataInitialized
        //{
        //    get { return m_IsDataInitialized; }
        //}

        /// <summary>
        /// 起始采集网页地址的索引
        /// </summary>
        public int BeginIndex
        {
            get { return m_TaskSplitData.BeginIndex; }
        }

        /// <summary>
        /// 结束采集网页地址的索引
        /// </summary>
        public int EndIndex
        {
            get { return m_TaskSplitData.EndIndex ; }
        }

        /// <summary>
        /// 当前正在采集地址的索引
        /// </summary>
        public int CurIndex
        {
            get { return m_TaskSplitData.CurIndex; }
        }

        /// <summary>
        /// 当前采集地址
        /// </summary>
        public string CurUrl
        {
            get { return m_TaskSplitData.CurUrl; }
        }

        /// <summary>
        /// 已采集网址数量
        /// </summary>
        public int GatheredUrlCount
        {
            get { return m_TaskSplitData.GatheredUrlCount; }
        }

        public int GatheredTrueUrlCount
        {
            get { return m_TaskSplitData.GatheredTrueUrlCount; }
        }

        /// <summary>
        /// 已采集但出错的网址数量
        /// </summary>
        public int GatherErrUrlCount
        {
            get { return m_TaskSplitData.GatheredErrUrlCount; }
        }

        public int GatheredTrueErrUrlCount
        {
            get { return m_TaskSplitData.GatheredTrueErrUrlCount; }
        }

        /// <summary>
        /// 一共需要采集的网址数量
        /// </summary>
        public int UrlCount
        {
            get { return m_TaskSplitData.UrlCount; }
        }

        public int TrueUrlCount
        {
            get { return m_TaskSplitData.TrueUrlCount; }
        }

        #endregion


        #region 事件
        /// <summary>
        /// 采集任务初始化事件
        /// </summary>
        private event EventHandler<TaskInitializedEventArgs> e_TaskInit;
        internal event EventHandler<TaskInitializedEventArgs> TaskInit
        {
            add { lock (m_eventLock) { e_TaskInit += value; } }
            remove { lock (m_eventLock) { e_TaskInit -= value; } }
        }

        /// <summary>
        /// 分解任务采集开始事件
        /// </summary>
        private event EventHandler<cTaskEventArgs> e_Started;
        internal event EventHandler<cTaskEventArgs> Started
        {
            add { lock (m_eventLock) { e_Started += value; } }
            remove { lock (m_eventLock) { e_Started -= value; } }
        }

        /// <summary>
        /// 分解任务停止事件
        /// </summary>
        private event EventHandler<cTaskEventArgs> e_Stopped;
        internal event EventHandler<cTaskEventArgs> Stopped
        {
            add { lock (m_eventLock) { e_Stopped += value; } }
            remove { lock (m_eventLock) { e_Stopped -= value; } }
        }

        /// <summary>
        /// 分解任务采集完成事件
        /// </summary>
        private event EventHandler<cTaskEventArgs> e_Completed;
        internal event EventHandler<cTaskEventArgs> Completed
        {
            add { lock (m_eventLock) { e_Completed += value; } }
            remove { lock (m_eventLock) { e_Completed -= value; } }
        }

        /// <summary>
        /// 分解任务采集错误事件
        /// </summary>       
        private event EventHandler<TaskThreadErrorEventArgs> e_Error;
        internal event EventHandler<TaskThreadErrorEventArgs> Error
        {
            add { lock (m_eventLock) { e_Error += value; } }
            remove { lock (m_eventLock) { e_Error -= value; } }
        }

        /// <summary>
        ///  分解任务采集失败事件
        /// </summary>
        private event EventHandler<cTaskEventArgs> e_Failed;
        internal event EventHandler<cTaskEventArgs> Failed
        {
            add { lock (m_eventLock) { e_Failed += value; } }
            remove { lock (m_eventLock) { e_Failed -= value; } }
        }

        /// <summary>
        /// 写日志事件
        /// </summary>
        private event EventHandler<cGatherTaskLogArgs> e_Log;
        internal event EventHandler<cGatherTaskLogArgs> Log
        {
            add { lock (m_eventLock) { e_Log += value; } }
            remove { lock (m_eventLock) { e_Log -= value; } }
        }
        
        /// <summary>
        /// 返回采集数据事件
        /// </summary>
        private event EventHandler<cGatherDataEventArgs> e_GData;
        internal event EventHandler<cGatherDataEventArgs> GData
        {
            add { lock (m_eventLock) { e_GData += value; } }
            remove { lock (m_eventLock) { e_GData -= value; } }
        }

        private event EventHandler<cGatherUrlCountArgs> e_GUrlCount;
        internal event EventHandler<cGatherUrlCountArgs> GUrlCount
        {
            add { lock (m_eventLock) { e_GUrlCount += value; } }
            remove { lock (m_eventLock) { e_GUrlCount -= value; } }
        }
        #endregion

        #region 事件触发操作
        //事件触发操作是通过改变任务状态来完成的
        //通过各种任务状态的变更，来触发事件，并最终反馈到界面
        //将控制权交给界面

        /// <summary>
        /// 获取当前线程状态
        /// </summary>    
        private cGlobalParas.GatherThreadState m_ThreadState;

        /// <summary>
        /// 设置/获取 线程状态 （仅内部使用，触发事件）
        /// </summary>
        protected cGlobalParas.GatherThreadState ThreadState
        {
            get { return m_ThreadState; }
            set
            {
                m_ThreadState = value;
                // 注意，所以涉及线程状态变更的事件都在此处理
                switch (m_ThreadState)
                {
                    case cGlobalParas.GatherThreadState.Completed:
                        if (e_Completed != null)
                        {
                            // 代理触发 采集下载完成 事件
                            m_TaskManage.EventProxy.AddEvent(delegate()
                            {
                                e_Completed(this, new cTaskEventArgs());
                            });
                        }
                        m_Thread = null;
                        break;
                    case cGlobalParas.GatherThreadState.Failed:
                        if (e_Failed != null)
                        {
                            // 代理触发 线程失败 事件
                            m_TaskManage.EventProxy.AddEvent(delegate()
                            {
                                e_Failed(this, new cTaskEventArgs());
                            });
                        }
                        m_Thread = null;
                        break;
                    case cGlobalParas.GatherThreadState.Started:
                        if (e_Started != null)
                        {
                            // 触发 线程开始 事件
                            e_Started(this, new cTaskEventArgs());
                        }
                        break;
                    case cGlobalParas.GatherThreadState.Stopped:
                        if (e_Stopped != null)
                        {
                            // 触发 线程停止 事件
                            e_Stopped(this, new cTaskEventArgs());
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 错误处理，发生错误后，并不停止任务，让任务继续进行
        /// 但需要反馈信息告诉用户有一个地址采集错误
        /// </summary>
        /// <param name="exp"></param>
        private void onError(Exception exp)
        {
            if (this.IsCurrentRunning)
            {
                // 不触发Stopped事件
                //m_ThreadState = cGlobalParas.GatherThreadState.Stopped;

                if (e_Error != null)
                {
                    // 代理触发 线程错误 事件
                    m_TaskManage.EventProxy.AddEvent(delegate()
                    {
                        m_ErrorCount++;
                        e_Error(this, new TaskThreadErrorEventArgs(exp));
                    });
                }

                //m_Thread = null;
            }
        }
        #endregion


        #region 线程控制 启动 停止 重启 重置

        /// <summary>
        /// 线程锁
        /// </summary>
        private readonly Object m_threadLock = new Object();
        /// <summary>
        /// 启动工作线程
        /// </summary>
        public void Start()
        {
            if (m_ThreadState != cGlobalParas.GatherThreadState.Started && !IsThreadAlive && !IsCompleted)
            {
                lock (m_threadLock)
                {
                    //设置线程运行标志，标识此线程运行
                    m_ThreadRunning = true; 

                    m_Thread = new Thread(this.ThreadWorkInit);

                    //定义线程名称,用于调试使用
                    m_Thread.Name =m_TaskID.ToString() + "-" + m_TaskSplitData.BeginIndex.ToString ();

                    m_Thread.Start();
                    m_ThreadState = cGlobalParas.GatherThreadState.Started;
                }
            }
        }

        /// <summary>
        /// 重新启动工作线程
        /// </summary>
        public void ReStart()
        {   // 仅在子线程外调用
            Stop();
            Start();
        }

        /// <summary>
        /// 停止当前线程
        /// </summary>
        public void Stop()
        {   
            //仅在子线程外调用
            //设置停止线程标志，线程停止只能等待一个地址采集完成后停止，不能
            //强行中断，否则会丢失数据

            ///注意：在此只是打了一个标记，线程并没有真正结束
            m_ThreadRunning = false;


            if (m_ThreadState == cGlobalParas.GatherThreadState.Started && IsThreadAlive)
            {
                lock (m_threadLock)
                {

                    //开始检测是否所有线程都以完成或退出
                    //bool isStop = false;

                    //while (!isStop)
                    //{
                    //    isStop = true;

                    //    if (m_ThreadState == cGlobalParas.GatherThreadState.Started && IsThreadAlive)
                    //        isStop = false;

                        
                    //}
                    
                    m_Thread = null;
                    if (m_ThreadState == cGlobalParas.GatherThreadState.Started)
                    {
                        m_ThreadState = cGlobalParas.GatherThreadState.Stopped;
                    }
                }
            }
            else
            {

                m_Thread = null;
                if (m_ThreadState == cGlobalParas.GatherThreadState.Started)
                {
                    m_ThreadState = cGlobalParas.GatherThreadState.Stopped;
                }
            }

            m_ThreadState = cGlobalParas.GatherThreadState.Stopped;
        }

        public void Abort()
        {
            m_ThreadRunning = false;

            if (m_ThreadState == cGlobalParas.GatherThreadState.Started && IsThreadAlive)
            {
                lock (m_threadLock)
                {
                    if (m_ThreadState == cGlobalParas.GatherThreadState.Started && IsThreadAlive)
                    {
                        m_Thread.Abort();
                        m_Thread = null;
                        m_ThreadState = cGlobalParas.GatherThreadState.Aborted;
                    }
                    else
                    {
                        m_Thread = null;
                        if (m_ThreadState == cGlobalParas.GatherThreadState.Started)
                        {
                            m_ThreadState = cGlobalParas.GatherThreadState.Aborted; ;
                        }
                    }
                }
            }
            else
            {
                m_Thread = null;
                if (m_ThreadState == cGlobalParas.GatherThreadState.Started)
                {
                    m_ThreadState = cGlobalParas.GatherThreadState.Aborted; ;
                }
            }

            m_ThreadState = cGlobalParas.GatherThreadState.Aborted;
        }

        /// <summary>
        /// 重置线程块为未初始化状态
        /// </summary>
        internal void Reset()
        {
            e_Completed = null;
            e_Error = null;
            e_Started = null;
            e_Stopped = null;
            e_TaskInit = null;
            m_IsInitialized = false;

            e_Log = null;
            e_GData = null;
        }

        #endregion

        #region 任务线程处理（采集网页数据） 执行一个采集分解任务

        /// <summary>
        /// 采集初始化线程
        /// </summary>
        private void ThreadWorkInit()
        {
            if (!m_IsInitialized)
            {
                ///按当前的处理方式，只要任务可以启动，就已经初始化了任务信息
                ///但是有一种情况未做处理，就是带有导航的网址，导航网址需要根据实际
                ///的解析情况才可以获得真正的需要采集数据的网址，那么就需要对这些实时
                ///解析出来的网址再次根据线程数进行任务拆分，所以需要重新对任务进行
                ///初始化，此类情况当前未进行处理，特别主意
                e_TaskInit(this, new TaskInitializedEventArgs(m_TaskID));
               
            }
            else if (GatheredUrlCount !=TrueUrlCount )
            {
                ThreadWork();
            }
        }

        /// <summary>
        /// 采集任务
        /// </summary>
        private void ThreadWork()
        {
            cGatherWeb gWeb = new cGatherWeb();

            gWeb.CutFlag =m_TaskSplitData.CutFlag ;

            bool IsSucceed = false;

            for (int i = 0; i < m_TaskSplitData.Weblink.Count; i++)
            {
                if (m_ThreadRunning == true)
                {
                    switch (m_TaskSplitData.Weblink[i].IsGathered)
                    {
                        case (int) cGlobalParas.UrlGatherResult.UnGather:

                            e_Log(this, new cGatherTaskLogArgs(m_TaskID, "正在采集：" + m_TaskSplitData.Weblink[i].Weblink + "\n"));

                            //判断此网址是否为导航网址，如果是导航网址则需要首先将需要采集的网址提取出来
                            //然后进行具体网址的采集
                            if (m_TaskSplitData.Weblink[i].IsNavigation == true)
                            {
                                IsSucceed = GatherNavigationUrl(m_TaskSplitData.Weblink[i].Weblink, m_TaskSplitData.Weblink[i].NagRule, m_TaskSplitData.Weblink[i].IsOppPath, m_TaskSplitData.Weblink[i].IsNextpage, m_TaskSplitData.Weblink[i].NextPageRule);
                            }
                            else
                            {
                                IsSucceed=GatherSingleUrl(m_TaskSplitData.Weblink[i].Weblink, m_TaskSplitData.Weblink[i].IsNextpage, m_TaskSplitData.Weblink[i].NextPageRule);
                            }

                            //如果采集发生错误，则直接调用了onError进行错误处理
                            //但对于带有导航的网址，一次采集是针对多个网址，如果系统
                            //停止任务，对于多个网址的采集那就是采集事务未成功，既然不成功
                            //则不需要增加GatheredUrlCount及其他处理
                            if (IsSucceed == true)
                            {
                                //每采集完成一个Url，都需要修改当前CurIndex的值，表示系统正在运行，并
                                //最终确定此分解任务是否已经完成,并且表示此网址已经采集完成
                                m_TaskSplitData.CurIndex++;
                                e_GUrlCount(this, new cGatherUrlCountArgs(m_TaskID, cGlobalParas.UpdateUrlCountType.UrlCountAdd, 0));

                                m_TaskSplitData.Weblink[i].IsGathered = (int)cGlobalParas.UrlGatherResult.Succeed;

                                m_TaskSplitData.GatheredUrlCount++;
                            }

                            break;

                        case (int)cGlobalParas.UrlGatherResult.Succeed:
                            m_TaskSplitData.CurIndex++;
                            m_TaskSplitData.GatheredUrlCount++;
                            m_TaskSplitData.GatheredTrueUrlCount++;
                            e_GUrlCount(this, new cGatherUrlCountArgs(m_TaskID, cGlobalParas.UpdateUrlCountType.UrlCountAdd, 0));
                            break;
                        case (int)cGlobalParas.UrlGatherResult.Error:
                            m_TaskSplitData.CurIndex++;
                            m_TaskSplitData.GatheredErrUrlCount++;
                            m_TaskSplitData.GatheredTrueErrUrlCount++;
                            e_GUrlCount(this, new cGatherUrlCountArgs(m_TaskID, cGlobalParas.UpdateUrlCountType.ErrUrlCountAdd, 0));
                            break;
                        case (int)cGlobalParas.UrlGatherResult.Gathered:
                            m_TaskSplitData.CurIndex++;
                            m_TaskSplitData.GatheredUrlCount++;
                            m_TaskSplitData.GatheredTrueUrlCount++;
                            e_GUrlCount(this, new cGatherUrlCountArgs(m_TaskID, cGlobalParas.UpdateUrlCountType.UrlCountAdd, 0));
                            break;
                    }
                }
                else if (m_ThreadRunning == false)
                {
                    //标示线程需要终止，退出for循环
                    break;
                }


            }
           

            //每采集完成一个网址需要判断当前线程是否已经结束
            //需要判断已采集的网址和采集发生错误的网址
            if (UrlCount == GatheredUrlCount + GatherErrUrlCount && UrlCount != GatherErrUrlCount)
            {
                ThreadState = cGlobalParas.GatherThreadState.Completed;
            }
            else if (UrlCount == GatherErrUrlCount)
            {
                //表示采集失败，但对于一个任务而言，一个线程的完全失败并不代表
                //任务也采集完全失败，所以任务的安全失败需要到任务采集类中判断
                //所以，在此还是返回线程任务完成时间，不是失败时间。
                ThreadState = cGlobalParas.GatherThreadState.Completed;
            }
            else if (UrlCount<GatheredUrlCount + GatherErrUrlCount)
            {
                ThreadState = cGlobalParas.GatherThreadState.Completed;
            }
            else
            {
                ThreadState = cGlobalParas.GatherThreadState.Stopped;
            }

            m_ThreadRunning = false;

        }

        //用于采集一个网页的数据
        private bool GatherSingleUrl(string Url,bool IsNext,string NextRule)
        {
            cGatherWeb gWeb = new cGatherWeb();
            DataTable tmpData;
            string NextUrl=Url ;

            gWeb.CutFlag = m_TaskSplitData.CutFlag;

            try
            {
                if (IsNext)
                {
                    do
                    {
                        Url = NextUrl;

                        e_Log(this, new cGatherTaskLogArgs(m_TaskID, "正在采集：" + Url + "\n"));
                        
                        tmpData = gWeb.GetGatherData(Url, m_WebCode, m_Cookie, m_gStartPos, m_gEndPos,m_SavePath );

                        if (tmpData != null)
                        {
                            m_GatherData.Merge(tmpData);
                        }

                        //触发日志及采集数据的事件
                        if (tmpData == null || tmpData.Rows.Count == 0)
                        {
                        }
                        else
                        {
                            e_GData(this, new cGatherDataEventArgs(m_TaskID, tmpData));
                        }
                        e_Log(this, new cGatherTaskLogArgs(m_TaskID, "采集完成：" + Url + "\n"));

                        string webSource=gWeb.GetHtml (Url,m_WebCode,m_Cookie,"","");
                        string NRule="((?<=href=[\'|\"])\\S[^#+$<>\\s]*(?=[\'|\"]))[^<]*(?<=" + NextRule + ")";
                        Match charSetMatch = Regex.Match(webSource, NRule, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string strNext = charSetMatch.Groups[1].Value;

                        if (strNext != "")
                        {
                            //判断获取的地址是否为相对地址
                            if (strNext.Substring(0, 1) == "/")
                            {
                                string PreUrl = Url;
                                PreUrl = PreUrl.Substring(7, PreUrl.Length - 7);
                                PreUrl = PreUrl.Substring(0, PreUrl.IndexOf("/"));
                                PreUrl = "http://" + PreUrl;
                                strNext = PreUrl + strNext;
                            }
                        }

                        NextUrl = strNext;
                        
                    }
                    while (NextUrl!="");
                }
                else
                {

                    tmpData = gWeb.GetGatherData(Url, m_WebCode, m_Cookie, m_gStartPos, m_gEndPos,m_SavePath );

                    if (tmpData != null)
                    {
                        m_GatherData.Merge(tmpData);
                    }

                    //触发日志及采集数据的事件
                    if (tmpData == null || tmpData.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        e_GData(this, new cGatherDataEventArgs(m_TaskID, tmpData));
                    }
                    e_Log(this, new cGatherTaskLogArgs(m_TaskID, "采集完成：" + Url + "\n"));
                }
                

                //触发采集网址计数事件
                e_GUrlCount(this, new cGatherUrlCountArgs(m_TaskID, cGlobalParas.UpdateUrlCountType.Gathered, 0));

                m_TaskSplitData.GatheredTrueUrlCount++;

            }
            catch (System.Exception ex)
            {
                e_Log(this, new cGatherTaskLogArgs(m_TaskID, Url + "采集发生错误：" + ex.Message  + "\n"));
                e_GUrlCount(this, new cGatherUrlCountArgs(m_TaskID, cGlobalParas.UpdateUrlCountType.Err, 0));
                m_TaskSplitData.GatheredTrueErrUrlCount++;
                onError(ex);
                return false;
            }

            gWeb = null;
            tmpData = null;

            return true;
        }

        //用于采集需要导航的网页，在此处理下一页的规则
        private bool GatherNavigationUrl(string Url, string NagRule, bool IsOppPath,bool IsNext,string NextRule)
        {
            cGatherWeb gWeb = new cGatherWeb();
            //gWeb.CutFlag = m_TaskSplitData.CutFlag;
            string NextUrl = Url;
            string Old_Url = NextUrl;
            bool IsSucceed = false;

            if (IsNext)
            {
                do
                {
                    if (m_ThreadRunning == true)
                    {
                        Url = NextUrl;
                        Old_Url = NextUrl;

                        e_Log(this, new cGatherTaskLogArgs(m_TaskID, "正在采集：" + Url + "\n"));

                        IsSucceed=ParseGatherNavigationUrl(Url, NagRule, IsOppPath);

                        e_Log(this, new cGatherTaskLogArgs(m_TaskID, "采集完成：" + Url + "\n"));

                        string webSource = gWeb.GetHtml(Url, m_WebCode, m_Cookie, "", "");
                        string NRule = "((?<=href=[\'|\"])\\S[^#+$<>\\s]*(?=[\'|\"]))[^<]*(?<=" + NextRule + ")";
                        Match charSetMatch = Regex.Match(webSource, NRule, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string strNext = charSetMatch.Groups[1].Value;

                        //判断获取的地址是否为相对地址
                        if (strNext.Substring(0, 1) == "/")
                        {
                            string PreUrl = Url;
                            PreUrl = PreUrl.Substring(7, PreUrl.Length - 7);
                            PreUrl = PreUrl.Substring(0, PreUrl.IndexOf("/"));
                            PreUrl = "http://" + PreUrl;
                            strNext = PreUrl + strNext;
                        }

                        NextUrl = strNext;
                    }
                    else if (m_ThreadRunning == false)
                    {
                        //标识要求终止线程，停止任务，退出do循环提前结束任务
                        if (NextUrl == "" || Old_Url == NextUrl)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    }

                }
                while (NextUrl != "" && Old_Url !=NextUrl );
            }
            else
            {
                IsSucceed=ParseGatherNavigationUrl(Url, NagRule, IsOppPath);
            }

            gWeb = null;

            return IsSucceed;
        }

        //用于采集需要导航的网页，在此处理导航页规则
        private bool ParseGatherNavigationUrl(string Url, string NagRule, bool IsOppPath)
        {
            Task.cUrlAnalyze u = new Task.cUrlAnalyze();
            List<string> gUrls;
            bool IsSucceed = false;

            if (m_IsUrlEncode == true)
            {
                gUrls = u.ParseUrlRule(Url, NagRule, m_IsUrlEncode, m_UrlEncode);
            }
            else
            {
                gUrls = u.ParseUrlRule(Url, NagRule, m_IsUrlEncode);
            }

            u = null;
            if (gUrls == null || gUrls.Count == 0)
            {
                onError(new System.Exception("网页地址解析失败！"));
                return false ;
            }

            //更新实际采集网址总数，因是导航页面，所以实际采集网址总数发生了变化
            //通过事件触发更新任务的采集数量总数，同时更新子任务的采集总数
            //注意，仅更新实际采集网址的总数，但不更新网址总数，此是两个值，各自维护各自的业务逻辑处理

            //系统进行了任务导航的分解操作，此时，已经修改了需要采集任务的总数，所以，需要更新子任务的实际采集网址的数量
            //同时还需触发相应的事件修改整个任务的采集网址的总数
            m_TaskSplitData.TrueUrlCount += gUrls.Count - 1;
            e_GUrlCount(this, new cGatherUrlCountArgs(m_TaskID, cGlobalParas.UpdateUrlCountType.ReIni, gUrls.Count));

            for (int j = 0; j < gUrls.Count; j++)
            {
                if (m_ThreadRunning == true)
                {
                    try
                    {
                        if (IsOppPath == true)
                        {
                            string PreUrl = Url;

                            if (gUrls[j].Substring(0, 1) == "/")
                            {
                                PreUrl = PreUrl.Substring(7, PreUrl.Length - 7);
                                PreUrl = PreUrl.Substring(0, PreUrl.IndexOf("/"));
                                PreUrl = "http://" + PreUrl;
                            }
                            else
                            {
                                Match aa = Regex.Match(PreUrl, ".*/");
                                PreUrl = aa.Groups[0].Value.ToString();
                            }

                           IsSucceed= GatherParsedUrl(PreUrl + gUrls[j].ToString());
                        }
                        else
                        {
                           IsSucceed= GatherParsedUrl(gUrls[j].ToString());
                        }


                        //触发采集网址计数事件
                        e_GUrlCount(this, new cGatherUrlCountArgs(m_TaskID, cGlobalParas.UpdateUrlCountType.Gathered, 0));
                        m_TaskSplitData.GatheredTrueUrlCount++;

                    }
                    catch (System.Exception ex)
                    {
                        e_GUrlCount(this, new cGatherUrlCountArgs(m_TaskID, cGlobalParas.UpdateUrlCountType.Err, 0));
                        m_TaskSplitData.GatheredTrueErrUrlCount++;
                        onError(ex);
                    }
                }
                else if (m_ThreadRunning == false)
                {
                    //标识要求终止线程，停止任务，退出for循环提前结束任务
                    if (j == gUrls.Count)
                    {
                        //表示还是采集完成了
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }

            }

            return true;
        }

        //用于采集导航网页分解后的单独地址
        private bool GatherParsedUrl(string Url)
        {
            cGatherWeb gWeb = new cGatherWeb();
            DataTable tmpData=null;

            gWeb.CutFlag = m_TaskSplitData.CutFlag;

            try
            {
                e_Log(this, new cGatherTaskLogArgs(m_TaskID, "正在采集：" + Url + "\n"));

                tmpData = gWeb.GetGatherData(Url, m_WebCode, m_Cookie, m_gStartPos, m_gEndPos,m_SavePath );

                if (tmpData != null)
                {
                    m_GatherData.Merge(tmpData);
                }

                //触发日志及采集数据的事件
                if (tmpData == null || tmpData.Rows.Count == 0)
                {
                }
                else
                {
                    e_GData(this, new cGatherDataEventArgs(m_TaskID, tmpData));
                }
                if (tmpData == null)
                {
                    e_Log(this, new cGatherTaskLogArgs(m_TaskID, Url + " 此地址无数据！" + "\n"));
                }
                else
                {
                    e_Log(this, new cGatherTaskLogArgs(m_TaskID, "采集完成：" + Url + "\n"));
                }
                tmpData = null;

            }
            catch (System.Exception ex)
            {
                e_Log(this, new cGatherTaskLogArgs(m_TaskID, Url + "采集发生错误：" + ex.Message + "\n"));
                onError(ex);
                return false  ;
            }

            gWeb = null;

            return true;

        }

        #endregion

        #region 设置任务分解数据，由外部调用

        /// <summary>
        /// 设置分解任务的数据,分解任务的数据主要包括了起始采集
        /// 的网页索引,终止采集的网页索引,一共需要采集的网页总数
        /// </summary>
        /// <param name="beginIndex">起始的采集网页地址</param>
        /// <param name="endIndex">终止的采集网页地址</param>
        public void SetSplitData(int beginIndex, int endIndex,List<Task.cWebLink> tUrl,List<Task.cWebpageCutFlag> tCutFlag)
        {
            lock (m_mstreamLock)
            {
                m_TaskSplitData.BeginIndex = beginIndex;
                m_TaskSplitData.CurIndex = beginIndex;
                m_TaskSplitData.EndIndex = endIndex;
                m_TaskSplitData.Weblink = tUrl;
                m_TaskSplitData.TrueUrlCount = tUrl.Count;
                m_TaskSplitData.CutFlag = tCutFlag;
                //m_IsDataInitialized = true;
            }
        }
       
        #endregion

        #region IDisposable 成员
        private bool m_disposed;
        /// <summary>
        /// 释放由 采集 的当前实例使用的所有资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                if (disposing)
                {

                }

                m_disposed = true;
            }
        }

        #endregion

    }
}
