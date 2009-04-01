using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

///功能：发布任务
///完成时间：2009-3-2
///作者：一孑
///遗留问题：无
///开发计划：无
///说明：无 
///版本：00.90.00
///修订：无
namespace SoukeyNetget.publish
{
    class cPublishTask
    {
        private cPublishTaskData m_pTaskData;
        private cPublishManage m_PublishManage;

        public cPublishTask(cPublishManage taskManage,Int64 TaskID,DataTable dData)
        {
            m_PublishManage = taskManage;
            m_pTaskData = new cPublishTaskData();

            //初始化任务数据
            LoadTaskInfo(TaskID, dData);

        }

        ~cPublishTask()
        {

        }

        #region 属性

        public Int64 TaskID
        {
            get { return m_pTaskData.TaskID; }
        }

        public cPublishTaskData TaskData
        {
            get { return m_pTaskData; }
        }

        public cPublishManage PublishManage
        {
            get { return m_PublishManage; }
        }

        public string FileName
        {
            get { return m_pTaskData.FileName; }
        }

        public cGlobalParas.PublishType PublishType
        {
            get { return m_pTaskData.PublishType; }
        }

        public int Count
        {
            get { return m_pTaskData.Count; }
        }

        private int m_PublishedCount;
        public int PublishedCount
        {
            get { return m_PublishedCount; }
            set { m_PublishedCount = value; }
        }

        #endregion

        //根据提供的taskid加载任务信息
        //数据不应该是传进来,是读取文件的,但现在不支持事务处理,所以传进来
        private void LoadTaskInfo(Int64 TaskID, DataTable dData)
        {
            //DataTable dt = new DataTable();
            Task.cTask t = new Task.cTask();

            t.LoadTask(Program.getPrjPath () + "tasks\\run\\task" + TaskID + ".xml");

            string FileName = Program.getPrjPath() + "Data\\" + t.TaskName + "-" + t.TaskID + ".xml"; 

            m_pTaskData.TaskID =t.TaskID ;
            m_pTaskData.TaskName =t.TaskName ;
            m_pTaskData.DataPwd =t.DataPwd ;
            m_pTaskData.DataSource =t.DataSource ;
            m_pTaskData.DataUser =t.DataUser ;
            m_pTaskData.FileName = FileName;
            
            //dt.ReadXml(FileName);
            m_pTaskData.PublishData = dData ;
            m_pTaskData.PublishData.TableName = t.TaskName + "-" + t.TaskID + ".xml"; 

            m_pTaskData.PublishType =(cGlobalParas.PublishType)(int.Parse (t.ExportType ));
            m_pTaskData.DataTableName =t.DataTableName ;
            t=null;
        }

        #region 事件定义
        private Thread m_Thread;
        private readonly Object m_eventLock = new Object();

        /// 采集任务完成事件
        private event EventHandler<PublishCompletedEventArgs> e_PublishCompleted;
        internal event EventHandler<PublishCompletedEventArgs> PublishCompleted
        {
            add { lock (m_eventLock) { e_PublishCompleted += value; } }
            remove { lock (m_eventLock) { e_PublishCompleted -= value; } }
        }

        /// 采集任务采集失败事件
        private event EventHandler<PublishFailedEventArgs> e_PublishFailed;
        internal event EventHandler<PublishFailedEventArgs> PublishFailed
        {
            add { lock (m_eventLock) { e_PublishFailed += value; } }
            remove { lock (m_eventLock) { e_PublishFailed -= value; } }
        }

        /// 采集任务开始事件
        private event EventHandler<PublishStartedEventArgs> e_PublishStarted;
        internal event EventHandler<PublishStartedEventArgs> PublishStarted
        {
            add { lock (m_eventLock) { e_PublishStarted += value; } }
            remove { lock (m_eventLock) { e_PublishStarted -= value; } }
        }

        /// 采集任务错误事件
        private event EventHandler<PublishErrorEventArgs> e_PublishError;
        internal event EventHandler<PublishErrorEventArgs> PublishError
        {
            add { lock (m_eventLock) { e_PublishError += value; } }
            remove { lock (m_eventLock) { e_PublishError -= value; } }
        }
        #endregion


        private readonly Object m_threadLock = new Object();

        public void startPublic()
        {

            lock (m_threadLock)
            {
                m_Thread = new Thread(this.ThreadWorkInit);

                //定义线程名称,用于调试使用
                m_Thread.Name = FileName;

                m_Thread.Start();
            }
        }

        private void ThreadWorkInit()
        {
            //判断当前的数据文件是否存在

            //开始导出
            ThreadWork();
           
        }

        private bool ConnectData()
        {
            //触发发布启动事件
            PublishStartedEventArgs evt = new PublishStartedEventArgs(this.TaskData.TaskID, this.TaskData.TaskName);
            e_PublishStarted(this,evt);

            if (this.PublishType == cGlobalParas.PublishType.PublishAccess)
            {
                ConnectAccess();
            }
            else if (this.PublishType == cGlobalParas.PublishType.PublishMSSql)
            {
                ConnectSqlServer();
            }

            PublishCompletedEventArgs evt1 = new PublishCompletedEventArgs(this.TaskData.TaskID, this.TaskData.TaskName);
            e_PublishCompleted(this, evt1);

            return true;
        }

        private void ConnectAccess()
        {
            OleDbConnection conn = new OleDbConnection();
            
            string connectionstring = "provider=microsoft.jet.oledb.4.0;data source=";
            connectionstring += m_pTaskData.DataSource;
            if (m_pTaskData.DataUser != "")
            {
                connectionstring += "UID=" + m_pTaskData.DataUser;

            }
            conn.ConnectionString = connectionstring;

            try
            {
                conn.Open();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            string CreateTablesql = getCreateTablesql();

            OleDbCommand com = new OleDbCommand();
            com.Connection = conn;
            com.CommandText = CreateTablesql;
            com.CommandType = CommandType.Text;
            try
            {
                int result = com.ExecuteNonQuery();
            }
            catch (System.Data.OleDb.OleDbException  ex)
            {
                if (ex.ErrorCode != -2147217900)
                {
                    throw ex;
                }
            }

            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter("SELECT * FROM " + m_pTaskData.DataTableName , conn);
            System.Data.OleDb.OleDbCommandBuilder builder = new System.Data.OleDb.OleDbCommandBuilder(da);

            DataSet ds = new DataSet();
            da.Fill(ds, m_pTaskData.DataTableName);

            for (int i = 0; i < m_pTaskData.PublishData.Rows.Count; i++)
            {
                DataRow dr = ds.Tables[0].NewRow();
                for (int j=0;j<m_pTaskData.PublishData.Columns.Count ;j++)
                {
                    dr[j] = m_pTaskData.PublishData.Rows [i][j].ToString();
                }
                ds.Tables[0].Rows.Add(dr);
            }
            int m = da.Update(ds.Tables[0]);

            conn.Close();
        }

        private bool ConnectSqlServer()
        {
            try
            {
                string strDataBase = "Server=.;DataBase=Library;Uid=" + m_pTaskData.DataUser + ";pwd=" + m_pTaskData.DataPwd  + ";";
                SqlConnection conn = new SqlConnection(strDataBase);
                conn.Open();
            }
            catch (System.Exception ex)
            {
                throw ex;
                
            }
            return true;
        }

        private string getCreateTablesql()
        {
            string strsql = "";

            strsql = "create table " + this.m_pTaskData.DataTableName + "(";
            for (int i=0;i<m_pTaskData.PublishData.Columns.Count ;i++)
            {
                strsql +=  m_pTaskData.PublishData.Columns[i].ColumnName + " " + "text" + "," ;
            }
            strsql = strsql.Substring(0, strsql.Length - 1);
            strsql += ")";

            return strsql;
        }

        private void ThreadWork()
        {
            //无论数据是否发布，都需要保存采集下来的数据
            //保存到本地磁盘

            m_pTaskData.PublishData.WriteXml(m_pTaskData.FileName, XmlWriteMode.WriteSchema);

            try
            {
                ConnectData();
            }
            catch (System.Exception ex)
            {
                e_PublishError(this, new PublishErrorEventArgs(this.TaskData.TaskID, this.TaskData.TaskName, ex));
            }
        }

    }
}
