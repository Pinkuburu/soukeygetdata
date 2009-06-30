using System;
using System.Collections.Generic;
using System.Text;
using System.Threading ;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;

///���ܣ��ɼ����� �ֽ���������
///���ʱ�䣺2009-6-1
///���ߣ�һ��
///�������⣺��
///�����ƻ�����
///˵������ 
///�汾��01.00.00
///�޶�����
namespace SoukeyNetget.Gather
{

    //���ǲɼ��������С��Ԫ,�����һ���ɼ��������,��һ��URL��ַ����
    //�����ж��������ǧ��URL��ַ,Ϊ���������,��URl�ɼ������˶��̵߳�
    //����ʽ,�������һ������Ҫ���в��,��ֵ������Ǵ������Ƶ��߳�
    //��,������ʼִ��.������Ҫ����ִ����ôһ����С��Ԫ������.

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


        #region �����࣬����ʼ���������

        ///���캯�����εķ�ʽ̫�����ˣ��������ƿ��ǲ��ܵ����⣬��Ҫ
        ///��һ���޸�
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

        //��ʾ�зֽ������
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


        #region ����


        /// <summary>
        /// �¼� �߳�ͬ����
        /// </summary>
        private readonly Object m_eventLock = new Object();
        /// <summary>
        /// ���� �߳�ͬ����
        /// </summary>
        private readonly Object m_mstreamLock = new Object();

        /// <summary>
        /// ��ȡ��ǰ�̵߳Ĵ������
        /// </summary>
        public int ErrorCount
        {
            get { return m_ErrorCount; }
        }

        /// <summary>
        /// ��ȡ/���� �ֽ������ʼ��״̬
        /// </summary>
        internal bool IsInitialized
        {
            get { return m_IsInitialized; }
            set { m_IsInitialized = value; }
        }

        /// <summary>
        /// ����/��ȡ �ȴ�ʱ�䣨���룩
        /// </summary>
        internal int Waittime
        {
            get { return m_Waittime; }
            set { m_Waittime = value; }
        }

        /// <summary>
        /// ����/��ȡ�ɼ����������
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
        /// �ֽ�ɼ������Ƿ��Ѿ����
        /// </summary>
        public bool IsCompleted
        {
            //get { return m_TaskSplitData.GatheredUrlCount == m_TaskSplitData.UrlCount ; }
            get { return m_TaskSplitData.UrlCount > 1 && m_TaskSplitData.GatheredUrlCount + m_TaskSplitData.GatheredErrUrlCount  == m_TaskSplitData.TrueUrlCount; }
        }
        /// <summary>
        /// �ֽ�Ĳɼ������Ƿ��Ѿ��ɼ����
        /// </summary>
        public bool IsGathered
        {
            get { return m_TaskSplitData.UrlCount > 0 && m_TaskSplitData.GatheredUrlCount +m_TaskSplitData.GatheredErrUrlCount == m_TaskSplitData.TrueUrlCount ; }
        }
        /// <summary>
        /// ��ȡһ��״ֵ̬��ʾ��ǰ�߳��Ƿ��ںϷ�����״̬ [���߳��ڲ�ʹ��]
        /// </summary>
        private bool IsCurrentRunning
        {
            get { return ThreadState  ==cGlobalParas.GatherThreadState.Started  && Thread.CurrentThread.Equals(m_Thread); }
        }

        /// <summary>
        /// ��ȡ��ǰ�߳��Ƿ��Ѿ�ֹͣ��ע��IsThreadRunningֻ��һ����־
        /// ������ȷ���̵߳�״̬��ֻ�Ǹ����߳���Ҫֹͣ��
        /// IsStop�������ⲿ������˷ֽ������Ƿ�ֹͣʹ��
        /// </summary>
        public bool IsStop
        {
            get { return m_ThreadRunning ==false && this.IsCurrentRunning==false;  }
        }
        

        /// <summary>
        /// ��ǰ��Ĺ����߳�
        /// </summary>
        internal Thread WorkThread
        {
            get { return m_Thread; }
        }

        /// <summary>
        /// ��ȡ��ǰ�̵߳�ִ��״̬
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
        /// �ֽ����������Ƿ��ѳ�ʼ��
        /// </summary>
        //public bool IsDataInitialized
        //{
        //    get { return m_IsDataInitialized; }
        //}

        /// <summary>
        /// ��ʼ�ɼ���ҳ��ַ������
        /// </summary>
        public int BeginIndex
        {
            get { return m_TaskSplitData.BeginIndex; }
        }

        /// <summary>
        /// �����ɼ���ҳ��ַ������
        /// </summary>
        public int EndIndex
        {
            get { return m_TaskSplitData.EndIndex ; }
        }

        /// <summary>
        /// ��ǰ���ڲɼ���ַ������
        /// </summary>
        public int CurIndex
        {
            get { return m_TaskSplitData.CurIndex; }
        }

        /// <summary>
        /// ��ǰ�ɼ���ַ
        /// </summary>
        public string CurUrl
        {
            get { return m_TaskSplitData.CurUrl; }
        }

        /// <summary>
        /// �Ѳɼ���ַ����
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
        /// �Ѳɼ����������ַ����
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
        /// һ����Ҫ�ɼ�����ַ����
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


        #region �¼�
        /// <summary>
        /// �ɼ������ʼ���¼�
        /// </summary>
        private event EventHandler<TaskInitializedEventArgs> e_TaskInit;
        internal event EventHandler<TaskInitializedEventArgs> TaskInit
        {
            add { lock (m_eventLock) { e_TaskInit += value; } }
            remove { lock (m_eventLock) { e_TaskInit -= value; } }
        }

        /// <summary>
        /// �ֽ�����ɼ���ʼ�¼�
        /// </summary>
        private event EventHandler<cTaskEventArgs> e_Started;
        internal event EventHandler<cTaskEventArgs> Started
        {
            add { lock (m_eventLock) { e_Started += value; } }
            remove { lock (m_eventLock) { e_Started -= value; } }
        }

        /// <summary>
        /// �ֽ�����ֹͣ�¼�
        /// </summary>
        private event EventHandler<cTaskEventArgs> e_Stopped;
        internal event EventHandler<cTaskEventArgs> Stopped
        {
            add { lock (m_eventLock) { e_Stopped += value; } }
            remove { lock (m_eventLock) { e_Stopped -= value; } }
        }

        /// <summary>
        /// �ֽ�����ɼ�����¼�
        /// </summary>
        private event EventHandler<cTaskEventArgs> e_Completed;
        internal event EventHandler<cTaskEventArgs> Completed
        {
            add { lock (m_eventLock) { e_Completed += value; } }
            remove { lock (m_eventLock) { e_Completed -= value; } }
        }

        /// <summary>
        /// �ֽ�����ɼ������¼�
        /// </summary>       
        private event EventHandler<TaskThreadErrorEventArgs> e_Error;
        internal event EventHandler<TaskThreadErrorEventArgs> Error
        {
            add { lock (m_eventLock) { e_Error += value; } }
            remove { lock (m_eventLock) { e_Error -= value; } }
        }

        /// <summary>
        ///  �ֽ�����ɼ�ʧ���¼�
        /// </summary>
        private event EventHandler<cTaskEventArgs> e_Failed;
        internal event EventHandler<cTaskEventArgs> Failed
        {
            add { lock (m_eventLock) { e_Failed += value; } }
            remove { lock (m_eventLock) { e_Failed -= value; } }
        }

        /// <summary>
        /// д��־�¼�
        /// </summary>
        private event EventHandler<cGatherTaskLogArgs> e_Log;
        internal event EventHandler<cGatherTaskLogArgs> Log
        {
            add { lock (m_eventLock) { e_Log += value; } }
            remove { lock (m_eventLock) { e_Log -= value; } }
        }
        
        /// <summary>
        /// ���زɼ������¼�
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

        #region �¼���������
        //�¼�����������ͨ���ı�����״̬����ɵ�
        //ͨ����������״̬�ı�����������¼��������շ���������
        //������Ȩ��������

        /// <summary>
        /// ��ȡ��ǰ�߳�״̬
        /// </summary>    
        private cGlobalParas.GatherThreadState m_ThreadState;

        /// <summary>
        /// ����/��ȡ �߳�״̬ �����ڲ�ʹ�ã������¼���
        /// </summary>
        protected cGlobalParas.GatherThreadState ThreadState
        {
            get { return m_ThreadState; }
            set
            {
                m_ThreadState = value;
                // ע�⣬�����漰�߳�״̬������¼����ڴ˴���
                switch (m_ThreadState)
                {
                    case cGlobalParas.GatherThreadState.Completed:
                        if (e_Completed != null)
                        {
                            // ������ �ɼ�������� �¼�
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
                            // ������ �߳�ʧ�� �¼�
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
                            // ���� �߳̿�ʼ �¼�
                            e_Started(this, new cTaskEventArgs());
                        }
                        break;
                    case cGlobalParas.GatherThreadState.Stopped:
                        if (e_Stopped != null)
                        {
                            // ���� �߳�ֹͣ �¼�
                            e_Stopped(this, new cTaskEventArgs());
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// ��������������󣬲���ֹͣ�����������������
        /// ����Ҫ������Ϣ�����û���һ����ַ�ɼ�����
        /// </summary>
        /// <param name="exp"></param>
        private void onError(Exception exp)
        {
            if (this.IsCurrentRunning)
            {
                // ������Stopped�¼�
                //m_ThreadState = cGlobalParas.GatherThreadState.Stopped;

                if (e_Error != null)
                {
                    // ������ �̴߳��� �¼�
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


        #region �߳̿��� ���� ֹͣ ���� ����

        /// <summary>
        /// �߳���
        /// </summary>
        private readonly Object m_threadLock = new Object();
        /// <summary>
        /// ���������߳�
        /// </summary>
        public void Start()
        {
            if (m_ThreadState != cGlobalParas.GatherThreadState.Started && !IsThreadAlive && !IsCompleted)
            {
                lock (m_threadLock)
                {
                    //�����߳����б�־����ʶ���߳�����
                    m_ThreadRunning = true; 

                    m_Thread = new Thread(this.ThreadWorkInit);

                    //�����߳�����,���ڵ���ʹ��
                    m_Thread.Name =m_TaskID.ToString() + "-" + m_TaskSplitData.BeginIndex.ToString ();

                    m_Thread.Start();
                    m_ThreadState = cGlobalParas.GatherThreadState.Started;
                }
            }
        }

        /// <summary>
        /// �������������߳�
        /// </summary>
        public void ReStart()
        {   // �������߳������
            Stop();
            Start();
        }

        /// <summary>
        /// ֹͣ��ǰ�߳�
        /// </summary>
        public void Stop()
        {   
            //�������߳������
            //����ֹͣ�̱߳�־���߳�ֹֻͣ�ܵȴ�һ����ַ�ɼ���ɺ�ֹͣ������
            //ǿ���жϣ�����ᶪʧ����

            ///ע�⣺�ڴ�ֻ�Ǵ���һ����ǣ��̲߳�û����������
            m_ThreadRunning = false;


            if (m_ThreadState == cGlobalParas.GatherThreadState.Started && IsThreadAlive)
            {
                lock (m_threadLock)
                {

                    //��ʼ����Ƿ������̶߳�����ɻ��˳�
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
        /// �����߳̿�Ϊδ��ʼ��״̬
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

        #region �����̴߳����ɼ���ҳ���ݣ� ִ��һ���ɼ��ֽ�����

        /// <summary>
        /// �ɼ���ʼ���߳�
        /// </summary>
        private void ThreadWorkInit()
        {
            if (!m_IsInitialized)
            {
                ///����ǰ�Ĵ���ʽ��ֻҪ����������������Ѿ���ʼ����������Ϣ
                ///������һ�����δ���������Ǵ��е�������ַ��������ַ��Ҫ����ʵ��
                ///�Ľ�������ſ��Ի����������Ҫ�ɼ����ݵ���ַ����ô����Ҫ����Щʵʱ
                ///������������ַ�ٴθ����߳������������֣�������Ҫ���¶��������
                ///��ʼ�������������ǰδ���д����ر�����
                e_TaskInit(this, new TaskInitializedEventArgs(m_TaskID));
               
            }
            else if (GatheredUrlCount !=TrueUrlCount )
            {
                ThreadWork();
            }
        }

        /// <summary>
        /// �ɼ�����
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

                            e_Log(this, new cGatherTaskLogArgs(m_TaskID, "���ڲɼ���" + m_TaskSplitData.Weblink[i].Weblink + "\n"));

                            //�жϴ���ַ�Ƿ�Ϊ������ַ������ǵ�����ַ����Ҫ���Ƚ���Ҫ�ɼ�����ַ��ȡ����
                            //Ȼ����о�����ַ�Ĳɼ�
                            if (m_TaskSplitData.Weblink[i].IsNavigation == true)
                            {
                                IsSucceed = GatherNavigationUrl(m_TaskSplitData.Weblink[i].Weblink, m_TaskSplitData.Weblink[i].NagRule, m_TaskSplitData.Weblink[i].IsOppPath, m_TaskSplitData.Weblink[i].IsNextpage, m_TaskSplitData.Weblink[i].NextPageRule);
                            }
                            else
                            {
                                IsSucceed=GatherSingleUrl(m_TaskSplitData.Weblink[i].Weblink, m_TaskSplitData.Weblink[i].IsNextpage, m_TaskSplitData.Weblink[i].NextPageRule);
                            }

                            //����ɼ�����������ֱ�ӵ�����onError���д�����
                            //�����ڴ��е�������ַ��һ�βɼ�����Զ����ַ�����ϵͳ
                            //ֹͣ���񣬶��ڶ����ַ�Ĳɼ��Ǿ��ǲɼ�����δ�ɹ�����Ȼ���ɹ�
                            //����Ҫ����GatheredUrlCount����������
                            if (IsSucceed == true)
                            {
                                //ÿ�ɼ����һ��Url������Ҫ�޸ĵ�ǰCurIndex��ֵ����ʾϵͳ�������У���
                                //����ȷ���˷ֽ������Ƿ��Ѿ����,���ұ�ʾ����ַ�Ѿ��ɼ����
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
                    //��ʾ�߳���Ҫ��ֹ���˳�forѭ��
                    break;
                }


            }
           

            //ÿ�ɼ����һ����ַ��Ҫ�жϵ�ǰ�߳��Ƿ��Ѿ�����
            //��Ҫ�ж��Ѳɼ�����ַ�Ͳɼ������������ַ
            if (UrlCount == GatheredUrlCount + GatherErrUrlCount && UrlCount != GatherErrUrlCount)
            {
                ThreadState = cGlobalParas.GatherThreadState.Completed;
            }
            else if (UrlCount == GatherErrUrlCount)
            {
                //��ʾ�ɼ�ʧ�ܣ�������һ��������ԣ�һ���̵߳���ȫʧ�ܲ�������
                //����Ҳ�ɼ���ȫʧ�ܣ���������İ�ȫʧ����Ҫ������ɼ������ж�
                //���ԣ��ڴ˻��Ƿ����߳��������ʱ�䣬����ʧ��ʱ�䡣
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

        //���ڲɼ�һ����ҳ������
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

                        e_Log(this, new cGatherTaskLogArgs(m_TaskID, "���ڲɼ���" + Url + "\n"));
                        
                        tmpData = gWeb.GetGatherData(Url, m_WebCode, m_Cookie, m_gStartPos, m_gEndPos,m_SavePath );

                        if (tmpData != null)
                        {
                            m_GatherData.Merge(tmpData);
                        }

                        //������־���ɼ����ݵ��¼�
                        if (tmpData == null || tmpData.Rows.Count == 0)
                        {
                        }
                        else
                        {
                            e_GData(this, new cGatherDataEventArgs(m_TaskID, tmpData));
                        }
                        e_Log(this, new cGatherTaskLogArgs(m_TaskID, "�ɼ���ɣ�" + Url + "\n"));

                        string webSource=gWeb.GetHtml (Url,m_WebCode,m_Cookie,"","");
                        string NRule="((?<=href=[\'|\"])\\S[^#+$<>\\s]*(?=[\'|\"]))[^<]*(?<=" + NextRule + ")";
                        Match charSetMatch = Regex.Match(webSource, NRule, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string strNext = charSetMatch.Groups[1].Value;

                        if (strNext != "")
                        {
                            //�жϻ�ȡ�ĵ�ַ�Ƿ�Ϊ��Ե�ַ
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

                    //������־���ɼ����ݵ��¼�
                    if (tmpData == null || tmpData.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        e_GData(this, new cGatherDataEventArgs(m_TaskID, tmpData));
                    }
                    e_Log(this, new cGatherTaskLogArgs(m_TaskID, "�ɼ���ɣ�" + Url + "\n"));
                }
                

                //�����ɼ���ַ�����¼�
                e_GUrlCount(this, new cGatherUrlCountArgs(m_TaskID, cGlobalParas.UpdateUrlCountType.Gathered, 0));

                m_TaskSplitData.GatheredTrueUrlCount++;

            }
            catch (System.Exception ex)
            {
                e_Log(this, new cGatherTaskLogArgs(m_TaskID, Url + "�ɼ���������" + ex.Message  + "\n"));
                e_GUrlCount(this, new cGatherUrlCountArgs(m_TaskID, cGlobalParas.UpdateUrlCountType.Err, 0));
                m_TaskSplitData.GatheredTrueErrUrlCount++;
                onError(ex);
                return false;
            }

            gWeb = null;
            tmpData = null;

            return true;
        }

        //���ڲɼ���Ҫ��������ҳ���ڴ˴�����һҳ�Ĺ���
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

                        e_Log(this, new cGatherTaskLogArgs(m_TaskID, "���ڲɼ���" + Url + "\n"));

                        IsSucceed=ParseGatherNavigationUrl(Url, NagRule, IsOppPath);

                        e_Log(this, new cGatherTaskLogArgs(m_TaskID, "�ɼ���ɣ�" + Url + "\n"));

                        string webSource = gWeb.GetHtml(Url, m_WebCode, m_Cookie, "", "");
                        string NRule = "((?<=href=[\'|\"])\\S[^#+$<>\\s]*(?=[\'|\"]))[^<]*(?<=" + NextRule + ")";
                        Match charSetMatch = Regex.Match(webSource, NRule, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string strNext = charSetMatch.Groups[1].Value;

                        //�жϻ�ȡ�ĵ�ַ�Ƿ�Ϊ��Ե�ַ
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
                        //��ʶҪ����ֹ�̣߳�ֹͣ�����˳�doѭ����ǰ��������
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

        //���ڲɼ���Ҫ��������ҳ���ڴ˴�����ҳ����
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
                onError(new System.Exception("��ҳ��ַ����ʧ�ܣ�"));
                return false ;
            }

            //����ʵ�ʲɼ���ַ���������ǵ���ҳ�棬����ʵ�ʲɼ���ַ���������˱仯
            //ͨ���¼�������������Ĳɼ�����������ͬʱ����������Ĳɼ�����
            //ע�⣬������ʵ�ʲɼ���ַ������������������ַ��������������ֵ������ά�����Ե�ҵ���߼�����

            //ϵͳ���������񵼺��ķֽ��������ʱ���Ѿ��޸�����Ҫ�ɼ���������������ԣ���Ҫ�����������ʵ�ʲɼ���ַ������
            //ͬʱ���败����Ӧ���¼��޸���������Ĳɼ���ַ������
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


                        //�����ɼ���ַ�����¼�
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
                    //��ʶҪ����ֹ�̣߳�ֹͣ�����˳�forѭ����ǰ��������
                    if (j == gUrls.Count)
                    {
                        //��ʾ���ǲɼ������
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

        //���ڲɼ�������ҳ�ֽ��ĵ�����ַ
        private bool GatherParsedUrl(string Url)
        {
            cGatherWeb gWeb = new cGatherWeb();
            DataTable tmpData=null;

            gWeb.CutFlag = m_TaskSplitData.CutFlag;

            try
            {
                e_Log(this, new cGatherTaskLogArgs(m_TaskID, "���ڲɼ���" + Url + "\n"));

                tmpData = gWeb.GetGatherData(Url, m_WebCode, m_Cookie, m_gStartPos, m_gEndPos,m_SavePath );

                if (tmpData != null)
                {
                    m_GatherData.Merge(tmpData);
                }

                //������־���ɼ����ݵ��¼�
                if (tmpData == null || tmpData.Rows.Count == 0)
                {
                }
                else
                {
                    e_GData(this, new cGatherDataEventArgs(m_TaskID, tmpData));
                }
                if (tmpData == null)
                {
                    e_Log(this, new cGatherTaskLogArgs(m_TaskID, Url + " �˵�ַ�����ݣ�" + "\n"));
                }
                else
                {
                    e_Log(this, new cGatherTaskLogArgs(m_TaskID, "�ɼ���ɣ�" + Url + "\n"));
                }
                tmpData = null;

            }
            catch (System.Exception ex)
            {
                e_Log(this, new cGatherTaskLogArgs(m_TaskID, Url + "�ɼ���������" + ex.Message + "\n"));
                onError(ex);
                return false  ;
            }

            gWeb = null;

            return true;

        }

        #endregion

        #region ��������ֽ����ݣ����ⲿ����

        /// <summary>
        /// ���÷ֽ����������,�ֽ������������Ҫ��������ʼ�ɼ�
        /// ����ҳ����,��ֹ�ɼ�����ҳ����,һ����Ҫ�ɼ�����ҳ����
        /// </summary>
        /// <param name="beginIndex">��ʼ�Ĳɼ���ҳ��ַ</param>
        /// <param name="endIndex">��ֹ�Ĳɼ���ҳ��ַ</param>
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

        #region IDisposable ��Ա
        private bool m_disposed;
        /// <summary>
        /// �ͷ��� �ɼ� �ĵ�ǰʵ��ʹ�õ�������Դ
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
