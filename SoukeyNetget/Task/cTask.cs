using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

///功能：采集任务类
///完成时间：2009-3-2
///作者：一孑
///遗留问题：无
///开发计划：无
///说明：无 
///版本：00.90.00
///修订：无
namespace SoukeyNetget.Task
{
    [Serializable]
    
    ///这个类是一个多功能的类，应该重新设计，采用集成的方式来进行
    ///此问题在下一个版本中修改
    ///此类的设计应该是一个任务的基类（可能会做成抽象类），由任务基类完成响应的派生，派生出任务执行类，及各种任务类别
    /// 当前采集任务仅提供了一种，后期会提供多种采集任务，所以，对此类还暂不修改
    ///现在先说当前的问题，此类主要做任务类，同时将任务执行的信息合并到此，这点需要注意，注释中会作出说明

    public class cTask
    {
        cXmlIO xmlConfig;

        #region 类的构造和销毁
        public cTask()
        {
            this.WebpageLink = new List<cWebLink>();
            this.WebpageCutFlag = new List<cWebpageCutFlag>();
        }

        ~cTask()
        {
            this.WebpageLink = null;
            this.WebpageCutFlag = null;
        }
        #endregion

        #region TaskProperty

        //以下为任务当前状态的属性，如果是新建任务，则应该为未启动
        //此属性有待考虑，当前未用
        public int TaskState
        {
            get { return this.TaskState; }
            set { this.TaskState = value; }
        }

      

        //*****************************************************************************************************************
        //以下定义为Task属性

        private Int64 m_TaskID;
        public Int64 TaskID
        {
            get { return m_TaskID; }
            set { m_TaskID = value; }
        }

        private string m_TaskName;
        public string TaskName
        {
            get { return m_TaskName; }
            set { m_TaskName = value; }
        }

        private string m_TaskDemo;
        public string TaskDemo
        {
            get { return m_TaskDemo; }
            set { m_TaskDemo = value; }
        }

        private string m_TaskClass;
        public string TaskClass
        {
            get { return m_TaskClass; }
            set { m_TaskClass = value; }
        }

        private string m_TaskType;
        public string TaskType
        {
            get { return m_TaskType; }
            set { m_TaskType = value; }
        }

        private string m_TaskTemplate;
        public string TaskTemplate
        {
            get { return m_TaskTemplate; }
            set { m_TaskTemplate = value; }
        }

        private string m_RunType;
        public string RunType
        {
            get { return m_RunType; }
            set { m_RunType = value; }
        }

        private int m_UrlCount;
        public int UrlCount
        {
            get { return m_UrlCount; }
            set { m_UrlCount = value; }
        }

        private string m_DemoUrl;
        public string DemoUrl
        {
            get { return m_DemoUrl; }
            set { m_DemoUrl = value; }
        }

        private string m_Cookie;
        public string Cookie
        {
            get { return m_Cookie; }
            set { m_Cookie = value; }
        }

        private string m_WebCode;
        public string WebCode
        {
            get { return m_WebCode; }
            set { m_WebCode = value; }
        }

        private bool m_IsLogin;
        public bool IsLogin
        {
            get { return m_IsLogin; }
            set { m_IsLogin = value; }
        }

        private string m_LoginUrl;
        public string LoginUrl
        {
            get { return m_LoginUrl; }
            set { m_LoginUrl = value; }
        }

        private bool m_IsUrlEncode;
        public bool IsUrlEncode
        {
            get { return m_IsUrlEncode; }
            set { m_IsUrlEncode = value; }
        }

        private string m_UrlEncode;
        public string UrlEncode
        {
            get { return m_UrlEncode; }
            set { m_UrlEncode = value; }
        }

        //导出数据仅支持Access与MSsqlserver
        private string m_ExportType;
        public string ExportType
        {
            get { return m_ExportType; }
            set { m_ExportType = value; }
        }

        private string m_ExportFile;
        public string ExportFile
        {
            get { return m_ExportFile; }
            set { m_ExportFile = value; }
        }

        private string m_DataSource;
        public string DataSource
        {
            get { return m_DataSource; }
            set { m_DataSource = value; }
        }

        private string m_DataUser;
        public string DataUser
        {
            get { return m_DataUser; }
            set { m_DataUser = value; }
        }

        private string m_DataPwd;
        public string DataPwd
        {
            get { return m_DataPwd; }
            set { m_DataPwd = value; }
        }

        private string m_DataTableName;
        public string DataTableName
        {
            get { return m_DataTableName; }
            set { m_DataTableName = value; }
        }

        private int m_ThreadCount;
        public int ThreadCount
        {
            get { return m_ThreadCount; }
            set { m_ThreadCount = value; }
        }

        //采集页面内容的起始位置
        private string m_StartPos;
        public string StartPos
        {
            get { return m_StartPos; }
            set { m_StartPos = value; }
        }

        private string m_EndPos;
        public string EndPos
        {
            get { return m_EndPos; }
            set { m_EndPos = value; }
        }

        private List<cWebLink> m_WebpageLink;
        public List<cWebLink> WebpageLink
        {
            get { return m_WebpageLink; }
            set { m_WebpageLink = value; }
        }

        private List<cWebpageCutFlag> m_WebpageCutFlag;
        public List<cWebpageCutFlag> WebpageCutFlag
        {
            get { return m_WebpageCutFlag; }
            set { m_WebpageCutFlag = value; }
        }


        #endregion

        
        #region 用于为类方法
        
        //判断任务的索引文件是否存在
        private bool IsExistTaskIndex(string Path)
        {
            string FileName;
            FileName = Path + "\\index.xml";
            bool IsExists = System.IO.File.Exists(FileName);
            return IsExists;
        }

        private string GetTaskClassPath()
        {
            string TClassName = this.TaskClass;
            string Path;

            if (TClassName == null || TClassName == "")
            {
                Path = Program.getPrjPath() + "Tasks";
            }
            else
            {
                cTaskClass tClass = new cTaskClass();
                Path = tClass.GetTaskClassPathByName(TClassName);
                tClass = null;
            }
            return Path;
        }

        //判断任务文件是否存在
        public bool IsExistTaskFile(string FileName)
        {
            string Path = GetTaskClassPath();
            string File = Path + "\\" + FileName;
            bool IsExists = System.IO.File.Exists(File);
            return IsExists;
        }

        //保存任务信息
        public void Save()
        {
            //获取需要保存任务的路径
            string tPath = GetTaskClassPath() + "\\";
            int i=0;

            //判断此路径下是否已经存在了此任务，如果存在则返回错误信息
            if (IsExistTaskFile(tPath + this.TaskName))
            {
                throw new cSoukeyException ("任务已经存在，不能建立");
            }

            //维护任务的Index.xml文件
            int TaskID=InsertTaskIndex(tPath);

            //开始增加Task任务
            //构造Task任务的XML文档格式
            //当前构造xml文件全部采用的拼写字符串的形式,并没有采用xml构造函数
            string tXml;
            tXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" +
                "<Task>" +
                "<State></State>" +       ///此状态值当前无效,用于将来扩充使用
                "<BaseInfo>" +
                "<ID>" + TaskID + "</ID>" +
                "<Name>" + this.TaskName + "</Name>" +
                "<TaskDemo>" + this.TaskDemo + "</TaskDemo>" +
                "<Class>" + this.TaskClass + "</Class>" +
                "<Type>" +  this.TaskType + "</Type>" +
                "<RunType>" + this.RunType + "</RunType>" +
                "<ThreadCount>" + this.ThreadCount + "</ThreadCount>" +
                "<UrlCount>" + this.UrlCount + "</UrlCount>" +
                "<StartPos>" + cTool.ReplaceTrans(this.StartPos) + "</StartPos>" +
                "<EndPos>" + cTool.ReplaceTrans(this.EndPos) + "</EndPos>" +
                "<DemoUrl>" + cTool.ReplaceTrans(this.DemoUrl) + "</DemoUrl>" +
                "<Cookie>" + this.Cookie + "</Cookie>" +
                "<WebCode>" + this.WebCode + "</WebCode>" +
                "<IsLogin>" + this.IsLogin + "</IsLogin>" +
                "<LoginUrl>" + this.LoginUrl + "</LoginUrl>" +
                "<IsUrlEncode>" + this.IsUrlEncode + "</IsUrlEncode>" +
                "<UrlEncode>" + this.UrlEncode + "</UrlEncode>" +
                "</BaseInfo>" +
                "<Result>" +
                "<ExportType>" + this.ExportType + "</ExportType>" +
                "<ExportFileName>" + this.ExportFile + "</ExportFileName>" +
                "<DataSource>" + this.DataSource + "</DataSource>" +
                "<DataTableName>" + this.DataTableName + "</DataTableName>" +
                "<DataUser>" + this.DataUser  + "</DataUser>" +
                "<DataPwd>" + this.DataPwd + "</DataPwd>" +
                "</Result>" +
                "<WebLinks>";

            if (this.WebpageLink != null)
            {
                for (i = 0; i < this.WebpageLink.Count; i++)
                {
                    tXml += "<WebLink>";
                    tXml += "<Url>" + cTool.ReplaceTrans ( this.WebpageLink[i].Weblink.ToString ()) + "</Url>";
                    tXml += "<IsNag>" + this.WebpageLink[i].IsNavigation + "</IsNag>";
                    tXml += "<IsOppPath>" + this.WebpageLink[i].IsOppPath + "</IsOppPath>";
                    tXml += "<NagRule>" + cTool.ReplaceTrans(this.WebpageLink[i].NagRule) + "</NagRule>";
                    tXml += "<IsNextPage>" + this.WebpageLink[i].IsNextpage + "</IsNextPage>";
                    tXml += "<NextPageRule>" + cTool.ReplaceTrans(this.WebpageLink[i].NextPageRule) + "</NextPageRule>";
                    tXml += "</WebLink>";
                }
            }
                 
		    tXml +="</WebLinks>" +
                "<GatherRule>" ;
            if (this.WebpageCutFlag != null)
            {
                for (i = 0; i < this.WebpageCutFlag.Count; i++)
                {
                    tXml += "<Rule>";
                    tXml += "<Title>" + cTool.ReplaceTrans( this.WebpageCutFlag[i].Title) + "</Title>";
                    tXml += "<StartFlag>" + cTool.ReplaceTrans (this.WebpageCutFlag[i].StartPos) + "</StartFlag>";
                    tXml += "<EndFlag>" + cTool.ReplaceTrans (this.WebpageCutFlag[i].EndPos) + "</EndFlag>";
                    tXml += "<LimitSign>" + this.WebpageCutFlag[i].LimitSign + "</LimitSign>";
                    tXml += "</Rule>";
                }
            }
             tXml +="</GatherRule>" +
                "</Task>";
            
            xmlConfig =new cXmlIO ();
            xmlConfig.NewXmlFile (tPath + this.TaskName + ".xml",tXml );


        }

        //更改任务的所属分类
        public void ChangeTaskClass(string TaskName, string OldTaskClass, string NewTaskClass)
        {
            cTaskClass tc = new cTaskClass();
            string oldPath = tc.GetTaskClassPathByName(OldTaskClass);
            string NewPath = tc.GetTaskClassPathByName(NewTaskClass);
            string FileName = TaskName + ".xml";

            System.IO.File.Copy(oldPath + "\\" + FileName, NewPath + "\\" + FileName);

            LoadTask(NewPath + "\\" + FileName);
            this.TaskClass = NewTaskClass;
            Save();

            DeleTask(oldPath, TaskName);

            tc = null;
        }

        //插入任务信息到任务索引文件，返回新建任务索引的任务id
        public int InsertTaskIndex(string tPath)
        {

            cTaskIndex tIndex;

            //判断此路径下是否存在任务的索引文件
            if (!IsExistTaskIndex(tPath))
            {
                //如果不存在索引文件，则需要建立一个文件
                tIndex = new cTaskIndex();
                tIndex.NewIndexFile(tPath);
            }
            else
            {
                tIndex = new cTaskIndex(tPath + "\\index.xml");
            }

            tIndex.GetTaskDataByClass(this.TaskClass);

            int MaxTaskID = tIndex.GetTaskClassCount();

            //构造TaskIndex文件内容,此部分内容应该包含在TaskIndex类中
            string indexXml = "<id>" + MaxTaskID + "</id>" +
                    "<Name>" + this.TaskName + "</Name>" +
                    "<Type>" + this.TaskType + "</Type>" +
                    "<RunType>" + this.RunType + "</RunType>" +
                    "<ExportFile>" + this.ExportFile + "</ExportFile>" +
                    "<WebLinkCount>" + this.UrlCount + "</WebLinkCount>" +
                    "<IsLogin>" + this.IsLogin + "</IsLogin>" +
                    "<PublishType>" + this.ExportType + "</PublishType>";
            tIndex.InsertTaskIndex(indexXml);
            tIndex = null;

            return MaxTaskID;

        }

        //当新建一个任务时，调用此方法
        public void New()
        {
            //this.TaskState =(int) cGlobalParas.TaskState.TaskUnStart;

            if (xmlConfig != null)
            {
                xmlConfig = null;
            }
        }

        //加载一个任务到此类中
        public void LoadTask(String FileName)
        {
            LoadTaskInfo(FileName);
        }

        //加载一个运行区的任务到此类中，返回此类信息
        //此方法由taskrun专用
        public void LoadTask(Int64  TaskID)
        {
            string FileName = Program.getPrjPath() + "Tasks\\run\\task" + TaskID + ".xml";
            LoadTaskInfo(FileName);
        }

        //加载指定任务的信息
        private void LoadTaskInfo(string FileName)
        {
            //根据一个任务名称装载一个任务
            try
            {
                xmlConfig = new cXmlIO(FileName);

                //获取TaskClass节点
                //TaskClass = xmlConfig.GetData("descendant::TaskClasses");
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            //加载任务信息
            this.TaskID =Int64.Parse ( xmlConfig.GetNodeValue("Task/BaseInfo/ID"));
            this.TaskName = xmlConfig.GetNodeValue("Task/BaseInfo/Name");
            this.TaskDemo = xmlConfig.GetNodeValue("Task/BaseInfo/TaskDemo");
            this.TaskClass = xmlConfig.GetNodeValue("Task/BaseInfo/Class");
            this.TaskType=xmlConfig.GetNodeValue("Task/BaseInfo/Type");
            this.RunType = xmlConfig.GetNodeValue("Task/BaseInfo/RunType");
            this.UrlCount =int.Parse (xmlConfig.GetNodeValue("Task/BaseInfo/UrlCount").ToString ());
            this.ThreadCount = int.Parse (xmlConfig.GetNodeValue("Task/BaseInfo/ThreadCount"));
            this.Cookie = xmlConfig.GetNodeValue("Task/BaseInfo/Cookie");
            this.DemoUrl = xmlConfig.GetNodeValue("Task/BaseInfo/DemoUrl");
            this.StartPos = xmlConfig.GetNodeValue("Task/BaseInfo/StartPos");
            this.EndPos = xmlConfig.GetNodeValue("Task/BaseInfo/EndPos");
            this.WebCode = xmlConfig.GetNodeValue("Task/BaseInfo/WebCode");
            this.IsLogin =( xmlConfig.GetNodeValue("Task/BaseInfo/IsLogin")=="True"? true :false );
            this.LoginUrl = xmlConfig.GetNodeValue("Task/BaseInfo/LoginUrl");
            this.IsUrlEncode = (xmlConfig.GetNodeValue("Task/BaseInfo/IsUrlEncode") == "True" ? true : false);
            this.UrlEncode = xmlConfig.GetNodeValue("Task/BaseInfo/UrlEncode");

            this.ExportType =xmlConfig.GetNodeValue("Task/Result/ExportType") ;
            this.ExportFile = xmlConfig.GetNodeValue("Task/Result/ExportFileName");
            this.DataSource = xmlConfig.GetNodeValue("Task/Result/DataSource");
            this.DataTableName = xmlConfig.GetNodeValue("Task/Result/DataTableName");
            this.DataUser = xmlConfig.GetNodeValue("Task/Result/DataUser");
            this.DataPwd = xmlConfig.GetNodeValue("Task/Result/DataPwd");

            cWebLink w;
            DataView dw = new DataView();
            int i;
            dw = xmlConfig.GetData("descendant::WebLinks");
            
            if (dw!=null)
            {
                for (i = 0; i < dw.Count; i++)
                {
                    w = new cWebLink();
                    w.id = i;
                    w.Weblink  = dw[i].Row["Url"].ToString();
                    if (dw[i].Row["IsNag"].ToString() == "True")
                        w.IsNavigation = true;
                    else
                        w.IsNavigation = false;

                    w.NagRule = dw[i].Row["NagRule"].ToString();

                    if (dw[i].Row["IsOppPath"].ToString() == "True")
                        w.IsOppPath = true;
                    else
                        w.IsOppPath = false;

                    if (dw[i].Row["IsNextPage"].ToString() == "True")
                        w.IsNextpage = true;
                    else
                        w.IsNextpage = false;

                    w.NextPageRule = dw[i].Row["NextPageRule"].ToString();
                    this.WebpageLink.Add(w);
                    w = null;
                }
            }

            dw = null;
            dw = new DataView();
            dw = xmlConfig.GetData("descendant::GatherRule");
            Task.cWebpageCutFlag c;
            if (dw != null)
            {
                for (i = 0; i < dw.Count; i++)
                {
                    c = new Task.cWebpageCutFlag();
                    c.Title = dw[i].Row["Title"].ToString();
                    c.StartPos = dw[i].Row["StartFlag"].ToString();
                    c.EndPos = dw[i].Row["EndFlag"].ToString();
                    c.LimitSign = int.Parse((dw[i].Row["LimitSign"].ToString() == null || dw[i].Row["LimitSign"].ToString() == "") ? "0" : dw[i].Row["LimitSign"].ToString());
                    this.WebpageCutFlag.Add(c);
                    c = null;
                }
            }
            dw=null;

        }

        //删除一个任务
        public bool DeleTask(string TaskPath,string TaskName)
        {
            //首先删除此任务所在分类下的index.xml中的索引内容然后再删除具体的任务文件
            string tPath = "";

            if (TaskPath == "")
            {
                tPath = Program.getPrjPath() + "Tasks";
            }
            else
            {
                tPath = TaskPath;
            }

            //先删除索引文件中的任务索引内容
            cTaskIndex tIndex = new cTaskIndex(tPath + "\\index.xml");
            tIndex.DeleTaskIndex(TaskName);
            tIndex =null;

            //如果是编辑状态，为了防止删除了文件后，任务保存失败，则
            //任务文件将丢失的问题，首先先不删除此文件，只是将其改名

            //删除任务的物理文件
            string FileName =TaskPath   + "\\" + TaskName + ".xml" ;
            string tmpFileName=TaskPath   + "\\~" + TaskName + ".xml" ;
            System.IO.File.Delete(tmpFileName);
            System.IO.File.Move(FileName, tmpFileName);

            //将文件设置为隐藏
            System.IO.File.SetAttributes(tmpFileName, System.IO.FileAttributes.Hidden);
            return true;
        }

         #endregion

    }
}
