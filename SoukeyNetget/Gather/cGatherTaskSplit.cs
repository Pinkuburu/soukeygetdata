using System;
using System.Collections.Generic;
using System.Text;
using System.Threading ;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;

///���ܣ��ɼ����� �ֽ���������
///���ʱ�䣺2009-3-2
///���ߣ�һ��
///�������⣺��
///�����ƻ�����
///˵������ 
///�汾��00.90.00
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


        #region �����࣬����ʼ���������

        ///���캯�����εķ�ʽ̫�����ˣ���������ǲ��ܵ����⣬��Ҫ
        ///��һ���޸�
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

        //��ʾ�зֽ������
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

         /// <summary>
        /// �ֽ�ɼ������Ƿ��Ѿ����
        /// </summary>
        public bool IsCompleted
        {
            //get { return m_TaskSplitData.GatheredUrlCount == m_TaskSplitData.UrlCount ; }
            get { return m_TaskSplitData.UrlCount >1 && m_TaskSplitData.GatheredUrlCount == m_TaskSplitData.UrlCount; }
        }
        /// <summary>
        /// �ֽ�Ĳɼ������Ƿ��Ѿ��ɼ����
        /// </summary>
        public bool IsGathered
        {
            get { return m_TaskSplitData.UrlCount > 0 && m_TaskSplitData.GatheredUrlCount == m_TaskSplitData.UrlCount; }
        }
        /// <summary>
        /// ��ȡһ��״ֵ̬��ʾ��ǰ�߳��Ƿ��ںϷ�����״̬ [���߳��ڲ�ʹ��]
        /// </summary>
        private bool IsCurrentRunning
        {
            get { return ThreadState  ==cGlobalParas.GatherThreadState.Started  && Thread.CurrentThread.Equals(m_Thread); }
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
        public long GatheredUrlCount
        {
            get { return m_TaskSplitData.GatheredUrlCount; }
        }

        /// <summary>
        /// һ����Ҫ�ɼ�����ַ����
        /// </summary>
        public int UrlCount
        {
            get { return m_TaskSplitData.UrlCount; }
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
        //public cGlobalParas.GatherThreadState ThreadState
        //{
        //    get { return m_State; }
        //}
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
        /// ������
        /// </summary>
        /// <param name="exp"></param>
        private void onError(Exception exp)
        {
            if (this.IsCurrentRunning)
            {
                // ������Stopped�¼�
                m_ThreadState = cGlobalParas.GatherThreadState.Stopped;

                if (e_Error != null)
                {
                    // ������ �̴߳��� �¼�
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
        {   // �������߳������
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
            else if (GatheredUrlCount !=UrlCount)
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
            DataTable tmpData;

            gWeb.CutFlag =m_TaskSplitData.CutFlag ;

            try
            {
                for (int i = 0; i < m_TaskSplitData.Weblink.Count; i++)
                {

                    e_Log(this, new cGatherTaskLogArgs(m_TaskID, "���ڲɼ���" + m_TaskSplitData.Weblink[i].Weblink + "\n"));

                    //�жϴ���ַ�Ƿ�Ϊ������ַ������ǵ�����ַ����Ҫ���Ƚ���Ҫ�ɼ�����ַ��ȡ����
                    //Ȼ����о�����ַ�Ĳɼ�
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

                            throw new System.Exception("��ҳ��ַ����ʧ�ܣ�");
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

                            //�����ɼ���ַ�����¼�
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

                        //������־���ɼ����ݵ��¼�
                        if (tmpData == null || tmpData.Rows.Count == 0)
                        {
                        }
                        else
                        {
                            e_GData(this, new cGatherDataEventArgs(m_TaskID, tmpData));
                        }
                        e_Log(this, new cGatherTaskLogArgs(m_TaskID,"�ɼ���ɣ�" + m_TaskSplitData.Weblink[i].Weblink + "\n"));

                        //�����ɼ���ַ�����¼�
                       e_GUrlCount(this, new cGatherUrlCountArgs(m_TaskID,0));
                       //m_TaskSplitData.GatheredUrlCount++;

                    }

                    //ÿ�ɼ����һ��Url������Ҫ�޸ĵ�ǰCurIndex��ֵ����ʾϵͳ�������У���
                    //����ȷ���˷ֽ������Ƿ��Ѿ����,���ұ�ʾ����ַ�Ѿ��ɼ����
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

            //ÿ�ɼ����һ����ַ��Ҫ�жϵ�ǰ�߳��Ƿ��Ѿ�����
            if (UrlCount  ==GatheredUrlCount)
            {
                ThreadState = cGlobalParas.GatherThreadState.Completed;
            }

        }

        //���ڵ�����ַ�Ĳɼ�����
        private void GatherUrl(string Url)
        {
            cGatherWeb gWeb = new cGatherWeb();
            DataTable tmpData;

            gWeb.CutFlag = m_TaskSplitData.CutFlag;

            try
            {
                e_Log(this, new cGatherTaskLogArgs(m_TaskID, "���ڲɼ���" + Url + "\n"));

                tmpData = gWeb.GetGatherData(Url,m_WebCode ,m_Cookie , m_gStartPos, m_gEndPos);
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
                    e_Log(this, new cGatherTaskLogArgs(m_TaskID,  Url + " �˵�ַ�����ݣ�" + "\n"));
                }
                else
                {
                    e_Log(this, new cGatherTaskLogArgs(m_TaskID, "�ɼ���ɣ�" + Url + "\n"));
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
