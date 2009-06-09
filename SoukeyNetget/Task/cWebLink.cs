using System;
using System.Collections.Generic;
using System.Text;

///功能：采集任务 URL存储 管理
///完成时间：2009-3-2
///作者：一孑
///遗留问题：无
///开发计划：无
///说明：无 
///版本：00.90.00
///修订：无
namespace SoukeyNetget.Task
{
    public class cWebLink
    {

        #region 构造 析构
        public cWebLink()
        {
            m_IsGathered = cGlobalParas.UrlGatherResult.UnGather ;
        }

        ~cWebLink()
        {
        }

        #endregion

        #region 属性
        private int m_id;
        public int id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        private string m_Weblink;
        public string Weblink
        {
            get { return m_Weblink; }
            set { m_Weblink = value; }
        }

        //是否为导航页，如果是导航页则需要根据导航规则来进行内容也的提取
        private bool m_IsNavigation;
        public bool IsNavigation
        {
            get { return m_IsNavigation; }
            set { m_IsNavigation = value; }
        }

        //是否为相对路径
        private bool m_IsOppPath;
        public bool IsOppPath
        {
            get { return m_IsOppPath; }
            set { m_IsOppPath = value; }
        }

        //导航规则
        private string m_NagRule;
        public string NagRule
        {
            get { return m_NagRule; }
            set { m_NagRule = value; }
        }

        //是否提取下一页标识
        private bool m_IsNextPage;
        public bool IsNextpage
        {
            get { return m_IsNextPage; }
            set { m_IsNextPage = value; }
        }

        //下一页标识
        private string m_NextPageRule;
        public string NextPageRule
        {
            get { return m_NextPageRule; }
            set { m_NextPageRule = value; }
        }

        //标识当前网页地址是否已经采集,默认cGlobalParas.UrlGatherResult.UnGather
        private cGlobalParas.UrlGatherResult m_IsGathered;
        public cGlobalParas.UrlGatherResult IsGathered
        {
            get { return m_IsGathered; }
            set { m_IsGathered = value; }
        }

        private string m_CurrentRunning;
        public string CurrentRunning
        {
            get { return m_CurrentRunning; }
            set { m_CurrentRunning = value; }
        }

        #endregion

    }
}
