using System;
using System.Collections.Generic;
using System.Text;

///功能：发布任务入口 事件定义
///完成时间：2009-3-2
///作者：一孑
///遗留问题：无
///开发计划：无
///说明：无 
///版本：00.90.00
///修订：无
namespace SoukeyNetget.publish
{
    class cPublishControl
    {

        //发布做的简单了，实际应该按照采集的模式来进行，可以进行各种监控
        //并且如果发布的 数据量大的时候可以多线程发布，但总感觉这种发布模式
        //实用性不是很大，所以就先做一个发布功能，可以让系统跑起来，感觉一下
        //试用的效果,后面会慢慢进行修改.

        private cPublishManage m_PublishManage;

        public cPublishControl()
        {
            m_PublishManage = new cPublishManage();
        }

        ~cPublishControl()
        {
        }

        public cPublishManage PublishManage
        {
            get { return m_PublishManage; }
        }
        
        //增加发布任务,并启动
        public void startPublish(cPublishTask pT)
        {
            m_PublishManage.AddPublishTask(pT );
        }

    }


    #region 定义发布事件
    //任务事件
    public class cPublishEventArgs : EventArgs
    {

        public cPublishEventArgs()
        {

        }
    
        /// <param name="cancel">是否取消事件</param>
        public cPublishEventArgs(bool cancel)
        {
            m_Cancel = cancel;
        }

        private bool m_Cancel;
        /// <summary>
        /// 是否取消事件
        /// </summary>
        public bool Cancel
        {
            get { return m_Cancel; }
            set { m_Cancel = value; }
        }
    }

    public class PublishErrorEventArgs : cPublishEventArgs
    {
        public PublishErrorEventArgs(Int64 TaskID, string TaskName, Exception error)
        {
            m_TaskID = TaskID;
            m_TaskName = TaskName;
            m_Error = error;
        }

        private Exception m_Error;
 
        public Exception Error
        {
            get { return m_Error; }
            set { m_Error = value; }
        }

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
    }

    //任务状态改变事件
    public class PublishStartedEventArgs : cPublishEventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="old_state">旧的状态</param>
        /// <param name="new_statue">新的状态</param>
        public PublishStartedEventArgs(Int64 TaskID, string TaskName)
        {
            m_TaskID = TaskID;

            m_TaskName = TaskName;
        }

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
        
    }

    public class PublishCompletedEventArgs : cPublishEventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="old_state">旧的状态</param>
        /// <param name="new_statue">新的状态</param>
        public PublishCompletedEventArgs(Int64 TaskID, string TaskName)
        {
            m_TaskID = TaskID;
            m_TaskName = TaskName;
        }

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

    }

    public class PublishFailedEventArgs : cPublishEventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="old_state">旧的状态</param>
        /// <param name="new_statue">新的状态</param>
        public PublishFailedEventArgs(Int64 TaskID, string TaskName)
        {
            m_TaskID = TaskID;
            m_TaskName = TaskName;
        }

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

    }

    #endregion

}
