using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using SoukeyNetget.Gather;

///���ܣ��ɼ�������
///���ʱ�䣺2009-3-2
///���ߣ�һ��
///�������⣺��
///�����ƻ�����
///˵������ 
///�汾��00.90.00
///�޶�����
namespace SoukeyNetget.Gather
{
    ///�ɼ�������,����һ���ɼ�����(��һ��Task.xml�ļ�)

    public class cGatherTask
    {
        private cTaskData m_TaskData;
        private cGlobalParas.TaskState m_State;
        private List<cGatherTaskSplit> m_list_GatherTaskSplit;
        private bool m_IsDataInitialized;
        private bool m_IsInitialized;
        private cGatherManage m_TaskManage;

        #region ���� ���� 

        /// ��ʼ���ɼ��������,��������������ȷ���������Ƿ�
        /// �ɶ���߳������,�����,���������ֽ�
        internal cGatherTask(cGatherManage taskManage, cTaskData taskData)
        {
            m_TaskManage = taskManage;
            m_TaskData = taskData;
            m_State = TaskData.TaskState;

            m_list_GatherTaskSplit = new List<cGatherTaskSplit>();
            
            //���������ݴ�����֮��,ֱ�ӶԵ�ǰ�����������ֽ�,
            //�Ƿ���Ҫ���߳̽���,����ʼ�������������
            SplitTask();

            //��ʼ��ʼ������
            TaskInit();
        }
        #endregion


        #region ����

        /// <summary>
        /// �¼� �߳�ͬ����
        /// </summary>
        private readonly Object m_eventLock = new Object();
        /// <summary>
        /// �ļ� �߳�ͬ����
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
        /// ����/��ȡ ��ǰ�������ݶ���
        /// </summary>
        internal cTaskData TaskData
        {
            get { return m_TaskData; }
        }
        /// <summary>
        /// ��ȡ�ɼ���ַ�ĸ���
        /// </summary>
        public int UrlCount
        {
            get { return m_TaskData.UrlCount; }
        }
        /// <summary>
        /// ����/��ȡ ������ɼ����߳���
        /// </summary>
        public int ThreadCount
        {
            //get { return m_TaskData.ThreadCount; }
            set { m_TaskData.ThreadCount = value; }
        }
        /// <summary>
        /// ��ȡ����ɲɼ����������
        /// </summary>
        public int GatheredUrlCount
        {
            get { return m_TaskData.GatheredUrlCount; }
        }

        /// <summary>
        /// ����/��ȡ ����ID
        /// </summary>
        public Int64 TaskID
        {
            get { return m_TaskData.TaskID; }
            //set { m_TaskData.TaskID = value; }
        }

        /// <summary>
        /// ����/��ȡ ������
        /// </summary>
        public string TaskName
        {
            get { return m_TaskData.TaskName; }
            //set { m_TaskData.TaskName = value; }
        }
        /// <summary>
        /// ��ȡ��ҳ�ĵ�ַ ����
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
        /// ��ȡ�ɼ��������������
        /// </summary>
        public cGlobalParas.TaskRunType RunType
        {
            get { return m_TaskData.RunType; }
        }
        /// <summary>
        /// �Ƿ��Ѿ��ɼ����
        /// </summary>
        public bool IsCompleted
        {
            get { return GatheredUrlCount ==UrlCount ; }
        }

        /// <summary>
        /// �ֽ������� ���
        /// </summary>
        public List<cGatherTaskSplit> TaskSplit
        {
            get { return m_list_GatherTaskSplit; }
            set { m_list_GatherTaskSplit = value; }
        }

        #endregion


        #region �¼����� ����״̬����
        /// ����״̬�ı���¼�����
        /// /// ����/��ȡ ����״̬ �����ڲ�ʹ�ã������¼���
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

                // ע�⣬�����漰����״̬������¼����ڴ˴���
                bool cancel = (evt != null && evt.Cancel);

                switch (m_State)
                {
                    case cGlobalParas.TaskState.Aborted:
                        // ���� ����ȡ�� �¼�
                        if (e_TaskAborted != null)
                        {
                            e_TaskAborted(this, new cTaskEventArgs(cancel));
                        }
                        break;
                    case cGlobalParas.TaskState.Completed:
                        // ���� ������� �¼�
                        if (e_TaskCompleted != null)
                        {
                            e_TaskCompleted(this, new cTaskEventArgs(cancel));
                        }
                        break;
                    case cGlobalParas.TaskState.Failed:
                        // ���� ����ʧ�� �¼�
                        if (e_TaskFailed != null)
                        {
                            e_TaskFailed(this, new cTaskEventArgs(cancel));
                        }
                        break;
                    case cGlobalParas.TaskState.Started:
                        // ���� ����ʼ �¼�
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
                            // ���� ����ֹͣ �¼�
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

        #region �¼�
        
        /// �ɼ���������¼�
        private event EventHandler<cTaskEventArgs> e_TaskCompleted;
        internal event EventHandler<cTaskEventArgs> TaskCompleted
        {
            add { lock (m_eventLock) { e_TaskCompleted += value; } }
            remove { lock (m_eventLock) { e_TaskCompleted -= value; } }
        }

        /// �ɼ�����ɼ�ʧ���¼�
        private event EventHandler<cTaskEventArgs> e_TaskFailed;
        internal event EventHandler<cTaskEventArgs> TaskFailed
        {
            add { lock (m_eventLock) { e_TaskFailed += value; } }
            remove { lock (m_eventLock) { e_TaskFailed -= value; } }
        }
        
        /// �ɼ�����ʼ�¼�
        private event EventHandler<cTaskEventArgs> e_TaskStarted;
        internal event EventHandler<cTaskEventArgs> TaskStarted
        {
            add { lock (m_eventLock) {  e_TaskStarted += value; } }
            remove { lock (m_eventLock) {  e_TaskStarted -= value; } }
        }

        /// �ɼ�����ֹͣ�¼�
        private event EventHandler<cTaskEventArgs> e_TaskStopped;
        internal event EventHandler<cTaskEventArgs> TaskStopped
        {
            add { lock (m_eventLock) {  e_TaskStopped += value; } }
            remove { lock (m_eventLock) {  e_TaskStopped -= value; } }
        }

        /// �ɼ�����ȡ���¼�
        private event EventHandler<cTaskEventArgs> e_TaskAborted;
        internal event EventHandler<cTaskEventArgs> TaskAborted
        {
            add { lock (m_eventLock) { e_TaskAborted += value; } }
            remove { lock (m_eventLock) { e_TaskAborted -= value; } }
        }

        /// �ɼ���������¼�
        private event EventHandler<TaskErrorEventArgs> e_TaskError;
        internal event EventHandler<TaskErrorEventArgs> TaskError
        {
            add { lock (m_eventLock) {  e_TaskError += value; } }
            remove { lock (m_eventLock) {  e_TaskError -= value; } }
        }

        /// ����״̬����¼�,ÿ������״̬�������ʱ���д���,
        /// ������������¼�,���ڽ���״̬�ĸı�
        private event EventHandler<TaskStateChangedEventArgs> e_TaskStateChanged;
        internal event EventHandler<TaskStateChangedEventArgs> TaskStateChanged
        {
            add { lock (m_eventLock) {  e_TaskStateChanged += value; } }
            remove { lock (m_eventLock) {  e_TaskStateChanged -= value; } }
        }

        /// �ɼ�����ֽ��ʼ������¼�
        private event EventHandler<TaskInitializedEventArgs> e_TaskThreadInitialized;
        internal event EventHandler<TaskInitializedEventArgs> TaskThreadInitialized
        {
            add { lock (m_eventLock) {  e_TaskThreadInitialized += value; } }
            remove { lock (m_eventLock) { e_TaskThreadInitialized -= value; } }
        }

        /// <summary>
        /// �ɼ���־�¼�
        /// </summary>
        private event EventHandler<cGatherTaskLogArgs> e_Log;
        internal event EventHandler<cGatherTaskLogArgs> Log
        {
            add { e_Log += value; }
            remove { e_Log -= value; }
        }

        /// <summary>
        /// �ɼ������¼�
        /// </summary>
        private event EventHandler<cGatherDataEventArgs> e_GData;
        internal event EventHandler<cGatherDataEventArgs> GData
        {
            add { e_GData += value; }
            remove { e_GData -= value; }
        }

        #endregion

        #region ������� ���� ֹͣ ���� ȡ��

        /// ��ʼ����
        public void Start()
        {
            // ȷ��λ��ʼ���������Ƚ��г�ʼ�����������ļ���ȡ��������Ϣ��
            if (m_State !=cGlobalParas.TaskState.Started && m_TaskManage != null)
            {
                TaskInit();
                StartAll();
            }
        }

        /// �������вɼ������߳� ���û�зֽ�����,�������ĵ����߳̽��вɼ�
        private void StartAll()
        {
            foreach (cGatherTaskSplit dtc in m_list_GatherTaskSplit)
            {
                dtc.Start();
            }
            State = cGlobalParas.TaskState.Started;
        }

        /// ����׼���������ȴ���ʼ��
        public void ReadyToStart()
        {
            if (m_State != cGlobalParas.TaskState.Started && m_State != cGlobalParas.TaskState.Completed)
            {
                State = cGlobalParas.TaskState.Waiting;
            }
        }

        /// ֹͣ����
        public void Stop()
        {
            foreach (cGatherTaskSplit dtc in m_list_GatherTaskSplit)
            {
                dtc.Stop();
            }
            State = cGlobalParas.TaskState.Stopped;
        }

        /// ȡ�������Ƴ�����
        public void Abort()
        {
            foreach (cGatherTaskSplit dtc in m_list_GatherTaskSplit)
            {
                dtc.Stop();
            }
            State = cGlobalParas.TaskState.Aborted;
        }
        #endregion

        #region  �෽�� �ڲ�ʹ��

        //����ָ��������ID�Ե�ǰ��������зֽ⣬����е���ҳ��Ҳ��Ҫ�ڴ˽���
        //�ֽ�
        //����ʼ��������Ĺؼ�����
        private void SplitTask()
        {
            cGatherTaskSplit dtc;
            List<Task.cWebLink> tWeblink;
            Task.cTask t= new Task.cTask();
            //m_TaskData.TaskID = e.TaskID;

            //����ָ����TaskID���������ַ��Ϣ
            try
            {
                t.LoadTask(Int64.Parse (m_TaskData.TaskID.ToString ()));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            ////����ҳ��Ĳɼ���ʼλ�ú���ֹλ��
            ///���������ݲ���taskrun�д洢�����������xml�ļ��д洢
            ///��m_TaskData�ǰ���taskrun�����ص����ݣ������޷����ش���
            ///��ֵ�Ͳɼ�ҳ��Ĺ�����ַ��
            ///Ϊʲô��taskrun�м��أ�����Ϊ������taskrun��ʱ�������ʾ����
            ///��Ϣ�����Ծ͹�����һ��������Ϣ������
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

            ////������ҳ��ַ���ݼ��ɼ���־����
            ////�ٴ�ȥ����������в�������ַ,����Ҫ���зֽ�
            ////ȷ�����ص���ַ�϶���һ����Ч����ַ
            ////ע��,��ʱ�����п��ֽܷ�������Ϣ,����,��ַ�����ڴ˻ᷢ���仯,����,���ջ����޸���ַ����
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
                    
                    //��ʼ���m_TaskData.weblink����
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

            //���³�ʼ��UrlCount
            //m_TaskData.UrlCount = m_TaskData.Weblink.Count;

            //��ʼ��������ֿ�,���������Url����������߳���,���߳���>1
            if (m_TaskData.UrlCount > m_TaskData.ThreadCount && m_TaskData.ThreadCount > 1)
            {
                int SplitUrlCount = (int)Math.Ceiling((decimal)m_TaskData.UrlCount / (decimal)m_TaskData.ThreadCount);

                //����ÿ���ֽ��������ʼUrl��������ֹ��Url����
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

        //����cookie��ֵ��cookie��ֵ������û���������������仯
        public void UpdateCookie(string cookie)
        {
            this.TaskData.Cookie = cookie;

            foreach (cGatherTaskSplit tp in m_list_GatherTaskSplit)
            {
                tp.UpdateCookie(cookie);
            }

        }

        /// ��ʼ���ɼ������߳�
        private void TaskInit()
        {
            if (!m_IsDataInitialized)
            {
                if (m_list_GatherTaskSplit.Count > 0)
                {   // ������ܴ��ڵ����߳�
                    foreach (cGatherTaskSplit dtc in m_list_GatherTaskSplit)
                    {
                        dtc.Stop();
                    }
                    m_list_GatherTaskSplit.Clear();
                }

                if (IsCompleted)
                {   
                    // �޸Ĵ˲ɼ������״̬Ϊ�Ѳɼ����,����Ϊ״̬Ϊ����ɣ��������¼�
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
                    {   // ���������½����߳�
                        m_list_GatherTaskSplit.Add(new cGatherTaskSplit(m_TaskManage, m_TaskData.TaskID, m_TaskData.WebCode, m_TaskData.IsUrlEncode, m_TaskData.UrlEncode, m_TaskData.Cookie, m_TaskData.StartPos, m_TaskData.EndPos));
                    }


                    foreach (cGatherTaskSplit dtc in m_list_GatherTaskSplit)
                    {   
                        // ��ʼ���������߳�
                        TaskEventInit(dtc);
                    }
                }

                m_IsDataInitialized = true;
            }
        }

        //���ֽ������¼����а�
        private void TaskEventInit(cGatherTaskSplit dtc)
        {
            if (!dtc.IsInitialized)
            {
                // �� ��ʼ���¼�������¼�
                dtc.TaskInit += this.TaskWorkThreadInit;
                dtc.Completed += this.TaskWorkThreadCompleted;
                dtc.GUrlCount += this.onGUrlCount;
                dtc.Log += this.onLog;
                dtc.GData += this.onGData;
                dtc.Error += this.TaskThreadError;
                dtc.IsInitialized = true;
            }
        }



        /// ��������Ϊδ��ʼ��״̬
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

        /// ���òɼ�����Ϊδ����״̬
        internal void ResetTaskData()
        {
            // ֹͣ����
            Stop();

            // �������߳�
            //m_list_GatherTaskSplit.Clear();

            m_TaskData.GatheredUrlCount = 0;
            Task.cTaskRun t = new Task.cTaskRun();
            t.LoadSingleTask(m_TaskData.TaskID);
            m_TaskData.UrlCount = t.GetUrlCount(0);
            t = null;

            //m_TaskData.TaskSplitData.Clear ();
            //m_IsDataInitialized = false;
        }

        /// ������������
        internal void Remove()
        {
            ResetTaskState();
        }

        //��������
        public void ResetTask()
        {
            //ResetTaskState();
            ResetTaskData();
            //SplitTask();
            //TaskInit();
        }

        #endregion

        #region ���з���

        /// �����л�������д���ļ�
        public void WriteToFile()
        {

        }


        #endregion

        #region  ��Ӧ�ֽ�ɼ�����(���߳�)�¼�

        /// �����ʼ��,�ɷֽ����񴥷�,
        private void TaskWorkThreadInit(object sender, TaskInitializedEventArgs e)
        {
            cGatherTaskSplit dtc = (cGatherTaskSplit)sender;
            m_TaskData.TaskID  =e.TaskID ;

            if (e_TaskThreadInitialized != null)
            {
                // ������ �����ʼ�� �¼�
                m_TaskManage.EventProxy.AddEvent(delegate()
                {
                    e_TaskThreadInitialized(this, new TaskInitializedEventArgs(m_TaskData.TaskID));
                });
            }
            
        }

        /// �ֽ�ɼ����� �߳���� �¼�����
        private void TaskWorkThreadCompleted(object sender, cTaskEventArgs e)
        {
            cGatherTaskSplit dtc = (cGatherTaskSplit)sender;

            ///�������ʱ������д���ļ�
            
            if (UrlCount  == m_TaskData.UrlCount )
            {  
                // ����ɼ����
                onTaskCompleted();
            }
        }

        private void onTaskCompleted()
        {
            if (m_TaskData.UrlCount  == m_TaskData.GatheredUrlCount  && m_State != cGlobalParas.TaskState.Completed )
            {   
               
                // ����Ϊ���״̬��������������¼�
                State = cGlobalParas.TaskState.Completed;
            }
        }


        /// ���� �ֽ�ɼ����� �����¼�
        private void TaskThreadError(object sender, TaskThreadErrorEventArgs e)
        {
            cGatherTaskSplit gt = (cGatherTaskSplit)sender;

            //���������ô��¼�,Ҳ��ʾ�����һ����ַ�Ĳɼ�,���ǳ�����
            m_TaskData.GatheredUrlCount++;

            if (gt.ErrorCount >= cGatherManage.MaxErrorCount)
            {   
                // �ﵽ����������ֹͣ��ǰ�߳�
                bool failed = true;

                // �����ǰ�������е��̶߳�ֹͣ�ˣ����ж�Ϊ����ʧ��
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

        //������־�¼�
        public void onLog(object sender, cGatherTaskLogArgs e)
        {
            if (e_TaskStarted != null && !e.Cancel)
            {
                e_Log(sender, e);
            }
        }

        //���������¼�
        public void onGData(object sender, cGatherDataEventArgs e)
        {
            if (e_TaskStarted != null && !e.Cancel)
            {
                e_GData(sender, e);
            }
        }

        //�ɼ���ַ�����¼�,ÿ���ô��¼�һ��,���һ,��ʾ�ɼ����һ����ַ
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
