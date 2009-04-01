using System;
using System.Collections.Generic;
using System.Text;

///功能：采集任务 分解子任务 数据
///完成时间：2009-3-2
///作者：一孑
///遗留问题：无
///开发计划：无
///说明：无 
///版本：00.90.00
///修订：无
namespace SoukeyNetget.Gather
{
    //任务分解数据,分解依据是一个完整任务按照所执行的线程数进行分解

    public class cTaskSplitData
    {

        #region 构造 析构
        public cTaskSplitData()
        {
            m_CurIndex = 0;
            m_GatheredUrlCount = 0;
        }

        ~cTaskSplitData()
        {
        }
        #endregion

        #region 采集分解任务属性

        //起始采集的位置索引
        private int m_BeginIndex;
        public int BeginIndex
        {
            get { return m_BeginIndex; }
            set { m_BeginIndex = value; }
        }

        //结束采集的位置索引
        private int m_EndIndex;
        public int EndIndex
        {
            get { return m_EndIndex; }
            set { m_EndIndex = value; }
        }

        private int m_CurIndex;
        public int CurIndex
        {
            get { return m_CurIndex; }
            set { m_CurIndex = value; }
        }

        //当前正在采集的Url地址
        private string m_CurUrl;
        public string CurUrl
        {
            get { return m_CurUrl; }
            set { m_CurUrl = value; }
        }
        
        //一共采集Url地址数量
        public int UrlCount
        {
            get { return (EndIndex - BeginIndex+1); }
        }

        //已经采集Url数量
        private int m_GatheredUrlCount;
        public int GatheredUrlCount
        {
            get { return m_GatheredUrlCount; }
            set { m_GatheredUrlCount = value; }
        }

        //此分解任务需要采集的网页地址
        private List<Task.cWebLink> m_Weblink;
        public List<Task.cWebLink> Weblink
        {
            get { return m_Weblink; }
            set { m_Weblink = value; }
        }

        //此分解任务采集网页地址数据的截取标识
        private List<Task.cWebpageCutFlag> m_CutFlag;
        public List<Task.cWebpageCutFlag> CutFlag
        {
            get { return m_CutFlag; }
            set { m_CutFlag = value; }
        }

        #endregion

    }
}
