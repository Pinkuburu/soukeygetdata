using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using Interop.Excel;
using System.IO;


///功能：发布任务
///完成时间：2009-3-2
///作者：一孑
///遗留问题：无
///开发计划：无
///说明：无 
///版本：01.00.00
///修订：无
namespace SoukeyNetget.publish
{
    class cPublishTask
    {
        private cPublishTaskData m_pTaskData;
        private cPublishManage m_PublishManage;

        public cPublishTask(cPublishManage taskManage,Int64 TaskID,System.Data.DataTable dData)
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
        private void LoadTaskInfo(Int64 TaskID, System.Data.DataTable dData)
        {
            //DataTable dt = new DataTable();
            Task.cTask t = new Task.cTask();

            t.LoadTask(Program.getPrjPath () + "tasks\\run\\task" + TaskID + ".xml");

            string FileName = t.SavePath  + "\\" + t.TaskName + "-" + t.TaskID + ".xml"; 
         
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

        /// 临时数据发布完成时间
        private event EventHandler<PublishTempDataCompletedEventArgs> e_PublishTempDataCompleted;
        internal event EventHandler<PublishTempDataCompletedEventArgs> PublishTempDataCompleted
        {
            add { lock (m_eventLock) { e_PublishTempDataCompleted += value; } }
            remove { lock (m_eventLock) { e_PublishTempDataCompleted -= value; } }
        }
        #endregion


        private readonly Object m_threadLock = new Object();

        //此方法用于临时发布数据使用，即如果用户终端了操作
        //需调用此方法对已经采集的数据进行临时发布，当前
        //仅临时进行保存
        public void startSaveTempData()
        {
            lock (m_threadLock)
            {
                m_Thread = new Thread(this.SaveTempData);

                //定义线程名称,用于调试使用
                m_Thread.Name = FileName;

                m_Thread.Start();
            }
        }

        private readonly Object m_fileLock = new Object();

        private void SaveTempData()
        {
            //无论数据是否发布，都需要保存采集下来的数据
            //保存到本地磁盘
            try
            {
                if (File.Exists(m_pTaskData.FileName))
                {
                    lock (m_fileLock)
                    {
                        File.Delete(m_pTaskData.FileName);
                    }
                }

                m_pTaskData.PublishData.WriteXml(m_pTaskData.FileName, XmlWriteMode.WriteSchema);

                //触发临时数据发布成功事件
                e_PublishTempDataCompleted(this, new PublishTempDataCompletedEventArgs(this.TaskData.TaskID, this.TaskData.TaskName));
            }
            catch (System.Exception ex)
            {
                //存储临时数据时，有可能导致多个线程访问的失败操作，当前并没有
                //加锁控制，加锁控制在下一版提供
                e_PublishError(this, new PublishErrorEventArgs(this.TaskData.TaskID, this.TaskData.TaskName, ex));

            }
        }


        //此方法用于采集任务完成后，数据的发布操作
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

        private bool ExportData()
        {
            //触发发布启动事件
            PublishStartedEventArgs evt = new PublishStartedEventArgs(this.TaskData.TaskID, this.TaskData.TaskName);
            e_PublishStarted(this,evt);

            switch (this.PublishType)
            {
                case cGlobalParas.PublishType.PublishAccess :
                    ExportAccess();
                    break;
                case cGlobalParas.PublishType.PublishExcel :
                    ExportExcel();
                    break;
                case cGlobalParas.PublishType.PublishTxt :
                    ExportTxt();
                    break;
                default :
                    break;
            }

            PublishCompletedEventArgs evt1 = new PublishCompletedEventArgs(this.TaskData.TaskID, this.TaskData.TaskName);
            e_PublishCompleted(this, evt1);

            return true;
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
            //保存到本地磁盘，此项工作由SaveTempData（）替代
            //这样做的目的是不触发消息事件，放到后台执行
            //if (File.Exists(m_pTaskData.FileName))
            //{
            //    File.Delete(m_pTaskData.FileName);
            //}
            //m_pTaskData.PublishData.WriteXml(m_pTaskData.FileName, XmlWriteMode.WriteSchema);

            try
            {
                ExportData();
            }
            catch (System.Exception ex)
            {
                e_PublishError(this, new PublishErrorEventArgs(this.TaskData.TaskID, this.TaskData.TaskName, ex));
            }
        }

        private void ExportAccess()
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

        private void ExportExcel()
        {
            string TaskName=m_pTaskData.TaskName ;
            string FileName=m_pTaskData.DataSource ;
            System.Data.DataTable gData= m_pTaskData.PublishData;
            
            // 定义要使用的Excel 组件接口
            // 定义Application 对象,此对象表示整个Excel 程序

            Interop.Excel.Application excelApp = null;
            Interop.Excel.Workbook workBook = null;
            Interop.Excel.Worksheet ws = null;
            Interop.Excel.Range r;
            int row = 1;
            int cell = 1;

            try
            {
                //初始化 Application 对象 excelApp
                excelApp = new Interop.Excel.Application();
                workBook = excelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                ws = (Worksheet)workBook.Worksheets[1];

                // 命名工作表的名称为 "Task Management"
                ws.Name = TaskName;


                // 遍历数据表中的所有列
                for (int i = 0; i < gData.Columns.Count; i++)
                {

                    ws.Cells[row, cell] = gData.Columns[i].ColumnName;
                    r = (Range)ws.Cells[row, cell];
                    ws.get_Range(r, r).HorizontalAlignment = Interop.Excel.XlVAlign.xlVAlignCenter;

                    cell++;

                }

                // 创建行,把数据视图记录输出到对应的Excel 单元格
                for (int i = 0; i < gData.Rows.Count; i++)
                {
                    for (int j = 0; j < gData.Columns.Count; j++)
                    {
                        ws.Cells[i + 2, j + 1] = gData.Rows[i][j];
                        Range rg = (Range)ws.get_Range(ws.Cells[i + 2, j + 1], ws.Cells[i + 2, j + 1]);
                        rg.EntireColumn.ColumnWidth = 20;
                        rg.NumberFormatLocal = "@";
                    }
                }

                workBook.SaveCopyAs(FileName);
                workBook.Saved = true;

            }
            catch (System.Exception )
            {
                return ;
            }
            finally
            {
                excelApp.UserControl = false;
                excelApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ws);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                GC.Collect();

            }

            return ;
        }

        private void ExportTxt()
        {
            string TaskName = m_pTaskData.TaskName;
            string FileName = m_pTaskData.DataSource;
            System.Data.DataTable gData = m_pTaskData.PublishData;

            FileStream myStream = File.Open(FileName, FileMode.Create, FileAccess.Write, FileShare.Write);
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding("gb2312"));
            string str = "";
            string tempStr = "";

            try
            {
                //写标题 
                for (int i = 0; i < gData.Columns.Count; i++)
                {
                    str += "\t";
                    str += gData.Columns[i].ColumnName;
                }

                sw.WriteLine(str);

                //写内容 
                for (int i = 0; i < gData.Rows.Count; i++)
                {
                    for (int j = 0; j < gData.Columns.Count; j++)
                    {

                        tempStr += "\t";
                        tempStr += gData.Rows[i][j];
                    }
                    sw.WriteLine(tempStr);
                    tempStr = "";
                }


                sw.Close();
                myStream.Close();

            }
            catch (Exception)
            {
                return ;
            }
            finally
            {
                sw.Close();
                myStream.Close();
            }


            return ;

        }

    }
}
