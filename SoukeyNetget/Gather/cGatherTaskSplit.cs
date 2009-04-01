using System;
using System.Collections.Generic;
using System.Text;
using System.Threading ;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;

///功能：采集任务 分解子任务处理
///完成时间：2009-3-2
///作者：一孑
///遗留问题：无
///开发计划：无
///说明：无 
///版本：00.90.00
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


        #region 构造类，并初始化相关数据

        ///构造函数传参的方式太恶心了，是最初考虑不周的问题，需要
        ///下一版修改
        internal cGatherTaskSplit(cGatherManage TaskManage, Int64 TaskID, cGlobalParas.WebCode webCode, bool IsUrlEncode, string UrlEncode, string strCookie, string StartPos, string EndPos)
        {
            //m_mstream = new MemoryStream();
            m_TaskManage = TaskManage;
            m_TaskID = TaskID;
            m_gStartPos = StartPos;
            m_gEndPos = EndPos;
            m_Cookie = strCookie;
            m_WebCode = webCode;
            m_IsUrlEncode = IsUrlEncode;
            m_UrlEncode = UrlEncode;
            m_TaskSplitData = new cTaskSplitData ();
            m_ThreadState = cGlobalParas.GatherThreadState.Stopped;

            m_GatherData = new DataTable();
        }

        //表示有分解的任务
        internal cGatherTaskSplit(cGatherManage TaskManage, Int64 TaskID, cGlobalParas.WebCode webCode, bool IsUrlEncode, string UrlEncode, string strCookie, string StartPos, string EndPos, cTaskSplitData TaskSplitData)
        {
            m_TaskManage = TaskManage;
            m_TaskID = TaskID;
            m_gStartPos = StartPos;
            m_gEndPos = EndPos;
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

         /// <summary>
        /// 分解采集任务是否已经完成
        /// </summary>
        public bool IsCompleted
        {
            //get { return m_TaskSplitData.GatheredUrlCount == m_TaskSplitData.UrlCount ; }
            get { return m_TaskSplitData.UrlCount >1 && m_TaskSplitData.GatheredUrlCount == m_TaskSplitData.UrlCount; }
        }
        /// <summary>
        /// 分解的采集任务是否已经采集完成
        /// </summary>
        public bool IsGathered
        {
            get { return m_TaskSplitData.UrlCount > 0 && m_TaskSplitData.GatheredUrlCount == m_TaskSplitData.UrlCount; }
        }
        /// <summary>
        /// 获取一个状态值表示当前线程是否处于合法运行状态 [子线程内部使用]
        /// </summary>
        private bool IsCurrentRunning
        {
            get { return ThreadState  ==cGlobalParas.GatherThreadState.Started  && Thread.CurrentThread.Equals(m_Thread); }
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
        public long GatheredUrlCount
        {
            get { return m_TaskSplitData.GatheredUrlCount; }
        }

        /// <summary>
        /// 一共需要采集的网址数量
        /// </summary>
        public int UrlCount
        {
            get { return m_TaskSplitData.UrlCount; }
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
        //public cGlobalParas.GatherThreadState ThreadState
        //{
        //    get { return m_State; }
        //}
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
        /// 错误处理
        /// </summary>
        /// <param name="exp"></param>
        private void onError(Exception exp)
        {
            if (this.IsCurrentRunning)
            {
                // 不触发Stopped事件
                m_ThreadState = cGlobalParas.GatherThreadState.Stopped;

                if (e_Error != null)
                {
                    // 代理触发 线程错误 事件
                    m_TaskManage.EventProxy.AddEvent(delegate()
                    {
                        m_ErrorCount++;
                        e_Error(this, new TaskThreadErrorEventArgs(exp));
                    });
                }

                m_Thread = null;
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
        {   // 仅在子线程外调用
            if (m_ThreadState == cGlobalParas.GatherThreadState.Started && IsThreadAlive)
            {
                lock (m_threadLock)
                {
                    if (m_ThreadState == cGlobalParas.GatherThreadState.Started && IsThreadAlive)
                    {
                        m_Thread.Abort();
                        m_Thread = null;
                        ThreadState  = cGlobalParas.GatherThreadState.Stopped;
                    }
                    else
                    {
                        m_Thread = null;
                        if (m_ThreadState == cGlobalParas.GatherThreadState.Started)
                        {
                            m_ThreadState = cGlobalParas.GatherThreadState.Stopped;
                        }
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
            else if (GatheredUrlCount !=UrlCount)
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
            DataTable tmpData;

            gWeb.CutFlag =m_TaskSplitData.CutFlag ;

            try
            {
                for (int i = 0; i < m_TaskSplitData.Weblink.Count; i++)
                {

                    e_Log(this, new cGatherTaskLogArgs(m_TaskID, "正在采集：" + m_TaskSplitData.Weblink[i].Weblink + "\n"));

                    //判断此网址是否为导航网址，如果是导航网址则需要首先将需要采集的网址提取出来
                    //然后进行具体网址的采集
                    if (m_TaskSplitData.Weblink[i].IsNavigation == true)
                    {
                        Task.cUrlAnalyze u = new Task.cUrlAnalyze();
                        List<string> gUrls;

                        if (m_IsUrlEncode == true)
                        {
                            gUrls = u.ParseUrlRule(m_TaskSplitData.Weblink[i].Weblink, m_TaskSplitData.Weblink[i].NagRule, m_IsUrlEncode, m_UrlEncode);
                        }
                        else
                        {
                            gUrls = u.ParseUrlRule(m_TaskSplitData.Weblink[i].Weblink, m_TaskSplitData.Weblink[i].NagRule, m_IsUrlEncode);
                        }

                        u = null;
                        if (gUrls == null)
                        {

                            throw new System.Exception("网页地址解析失败！");
                        }

                        e_GUrlCount(this, new cGatherUrlCountArgs(m_TaskID, gUrls.Count));

                        for (int j = 0; j < gUrls.Count; j++)
                        {
                            if (m_TaskSplitData.Weblink[i].IsOppPath == true)
                            {
                                string PreUrl = m_TaskSplitData.Weblink[i].Weblink;

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

                                GatherUrl(PreUrl + gUrls[j].ToString());
                            }
                            else
                            {
                                GatherUrl(gUrls[j].ToString());
                            }

                            //触发采集网址计数事件
                            e_GUrlCount(this, new cGatherUrlCountArgs(m_TaskID, 0));
                            //m_TaskSplitData.GatheredUrlCount++;

                        }
                    }
                    else
                    {
                        tmpData = gWeb.GetGatherData(m_TaskSplitData.Weblink[i].Weblink,m_WebCode ,m_Cookie , m_gStartPos, m_gEndPos);
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
                        e_Log(this, new cGatherTaskLogArgs(m_TaskID,"采集完成：" + m_TaskSplitData.Weblink[i].Weblink + "\n"));

                        //触发采集网址计数事件
                       e_GUrlCount(this, new cGatherUrlCountArgs(m_TaskID,0));
                       //m_TaskSplitData.GatheredUrlCount++;

                    }

                    //每采集完成一个Url，都需要修改当前CurIndex的值，表示系统正在运行，并
                    //最终确定此分解任务是否已经完成,并且表示此网址已经采集完成
                    m_TaskSplitData.CurIndex++;
                    m_TaskSplitData.GatheredUrlCount++;

                    m_TaskSplitData.Weblink[i].IsGathered = true;

                    

                    tmpData = null;

                }
            }
            catch (System.Net.WebException ex)
            {
                onError(ex);
                return;
            }
            catch (System.Exception es)
            {
                onError(es);
                return;
            }

            //每采集完成一个网址需要判断当前线程是否已经结束
            if (UrlCount  ==GatheredUrlCount)
            {
                ThreadState = cGlobalParas.GatherThreadState.Completed;
            }

        }

        //用于导航网址的采集调用
        private void GatherUrl(string Url)
        {
            cGatherWeb gWeb = new cGatherWeb();
            DataTable tmpData;

            gWeb.CutFlag = m_TaskSplitData.CutFlag;

            try
            {
                e_Log(this, new cGatherTaskLogArgs(m_TaskID, "正在采集：" + Url + "\n"));

                tmpData = gWeb.GetGatherData(Url,m_WebCode ,m_Cookie , m_gStartPos, m_gEndPos);
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
                    e_Log(this, new cGatherTaskLogArgs(m_TaskID,  Url + " 此地址无数据！" + "\n"));
                }
                else
                {
                    e_Log(this, new cGatherTaskLogArgs(m_TaskID, "采集完成：" + Url + "\n"));
                }
                tmpData = null;

            }
            catch (System.Net.WebException ex)
            {
                onError(ex);
                return;
            }

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
