using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SoukeyNetget.Plan
{
    class cPlanIndex
    {
        cXmlIO xmlConfig;
        DataView Plans;

        #region 构造和析构

        public cPlanIndex()
        {
            xmlConfig = new cXmlIO();
        }

        ~cPlanIndex()
        {
            xmlConfig =null;
        }

        #endregion

        #region 新建一个index文件,及在此文件下新建一个任务信息

        //计划的index文件是固定的，用户不允许进行修改
        //默认位置为：系统根路径\\tasks\\plan
       
        public void NewIndexFile()
        {

            string strXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" +
                       "<PlanIndex>" +
                       "</PlanIndex>";
            xmlConfig.NewXmlFile(Program.getPrjPath () + "tasks\\plan\\index.xml", strXml);

        }

        //插入一个计划索引
        //插入计划是需要判断计划的状态，通常情况下，新建计划都是有效的。
        //当前计划中有生效时间，但此时间当前并不做计划状态的判断
        public void InsertPlanIndex(string strXml, cGlobalParas.PlanState pState)
        {
            switch (pState)
            {
                case cGlobalParas.PlanState .Disabled :
                    xmlConfig.InsertElement("PlanIndex\\Disabled", "Plan", strXml);
                    break;
                case cGlobalParas.PlanState .Enabled :
                    xmlConfig.InsertElement("PlanIndex\\Enabled", "Plan", strXml);
                    break;
                case cGlobalParas.PlanState .Expired :
                    xmlConfig.InsertElement("PlanIndex\\Expired", "Plan", strXml);
                    break;
                default :
                    break ;
            }
            
            xmlConfig.Save();
        }

        //删除一个计划索引
        public void DelePlanIndex(string PlanID)
        {
            xmlConfig.DeleteChildNodes("PlanIndex", "ID", TaskName);
            xmlConfig.Save();
        }


        #endregion

        #region 查找计划信息

        //根据计划的状态获取计划
        public void GetPlanByState(cGlobalParas.PlanState pState)
        {

            string PlanPath = Program.getPrjPath() + "tasks\\plan";

            xmlConfig = new cXmlIO(PlanPath + "\\index.xml");

            //获取TaskClass节点
            Plans = xmlConfig.GetData("TaskIndex");
        }

        //计算当前共有多少个TaskClass
        public int GetPlanCount()
        {
            int tCount;
            if (Plans == null)
            {
                tCount = 0;
            }
            else
            {
                tCount = Plans.Count;
            }
            return tCount;
        }

        public int GetPlanID(int index)
        {
            int tid = int.Parse(Plans[index].Row["id"].ToString());
            return tid;
        }

        public string GetPlanName(int index)
        {
            string TName = Plans[index].Row["Name"].ToString();
            return TName;
        }


        public string GetPlanState(int index)
        {
            string TType = Plans[index].Row["Type"].ToString();
            return TType;
        }

        public string GetPlanType(int index)
        {
            string TRunType = Plans[index].Row["Type"].ToString();
            return TRunType;
        }

        public string GetPlanRunType(int index)
        {
            string TRunType = Plans[index].Row["RunType"].ToString();
            return TRunType;
        }

        //获取此计划需要执行任务的数量
        public int GetTaskCount(int index)
        {
            int WebLinkCount;
            try
            {
                WebLinkCount = int.Parse(Plans[index].Row["WebLinkCount"].ToString());
            }
            catch
            {
                WebLinkCount = 0;
            }
            return WebLinkCount;
        }

        public bool GetIsDisabled(int index)
        {
            string TRunType = Plans[index].Row["IsDisabled"].ToString();
            return TRunType;
        }

        public bool GetIsOverRun(int index)
        {
            string TRunType = Plans[index].Row["IsOverRun"].ToString();
            return TRunType;
        }

        #endregion

    }
}
