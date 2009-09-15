using System;
using System.Collections.Generic;
using System.Text;

///功能：全局 变量 常量 等
///完成时间：2009-3-2
///作者：一孑
///遗留问题：无
///开发计划：无
///说明：转换函数实际无意义，直接可用中文进行定义，但个人变成习惯不喜欢这样做，所以提供了转换函数。
///版本：01.10.00
///修订：无
namespace SoukeyNetget
{
    public static class cGlobalParas
    {

        #region 枚举常量
        public enum TaskState
        {
            UnStart = 1010,
            Started=1011,
            Aborted=1012,
            Waiting=1013,
            Running = 1014,
            Pause = 1015,
            Stopped = 1016,
            Exporting = 1017,
            Completed=1018,
            Failed = 1019,
            Publishing=1020,
        }

        public enum GatherResult
        {
            GatherSucceed=1071,
            GatherFailed=1072,
            PublishSuccees=1073,
            PublishFailed=1074,
        }

        public enum DownloadResult
        {
            Succeed=1081,
            Failed=1082,
            Err=1083,
        }

        public enum GDataType
        {
            Txt=1091,
            Picture=1092,
            Flash=1093,
            File=1094,
        }

        public enum GatherThreadState
        {
            UnStart = 1030,
            Started = 1031,
            Running = 1032,
            Stopped = 1033,
            Completed = 1034,
            Failed = 1035,
            Aborted=1036,
            Waiting=1037,
        }

        public enum FormState
        {
            New=1021,
            Edit=1022,
            Browser=1023,
        }

        public enum TaskRunType
        {
            OnlyGather=1041,
            GatherExportData=1042,
        }

        public enum TaskType
        {
            HtmlByUrl=1051,
            RssByUrl=1052,
            HtmlByWeb=1053,
            AjaxHtmlByUrl=1054,
        }

        public enum PublishType
        {
            NoPublish=1060,
            PublishAccess = 1061,
            PublishMSSql = 1062,
            PublishTxt=1063,
            PublishExcel=1064,
            PublishMySql=1065,
            PublishWeb=1066,
        }

        public enum LimitSign
        {
           NoLimit = 2001,          //不做任意格式的限制
           NoWebSign = 2002,        //匹配时去掉网页符号
           OnlyCN = 2003,           //只匹配中文
           OnlyDoubleByte=2004,     //只匹配双字节字符
           OnlyNumber=2005,         //只匹配数字
           OnlyChar=2006,           //只匹配字母数字及常用字符
           Custom = 2007,           //自定义正则匹配表达式 
        }

        public enum ExportLimit
        {
            ExportNoLimit = 2040,      //不做输出控制
            ExportNoWebSign = 2041,    //输出时去掉网页符号
            ExportPrefix = 2042,       //输出时附加前缀  
            ExportSuffix = 2043,       //输出时附加后缀
            ExportTrimLeft = 2044,     //左起去掉字符
            ExportTrimRight = 2045,    //右起去掉字符
            ExportReplace = 2046,      //替换其中符合条件的字符
            ExportTrim = 2047,         //去掉字符串首尾空格
            ExportRegexReplace = 2048, //输出时采用正则表达式进行替换


        }

        public enum WebCode
        {
            auto = 1000,
            gb2312 = 1001,
            utf8 = 1002,
            gbk = 1003,
            big5=1004,
            NoCoding=1005,
        }

        public enum ExitPara
        {
            Exit=2010,
            MinForm=2012,
        }

        public enum UpdateUrlCountType
        {
            Gathered=2020,
            Err=2021,
            ReIni=2022,
            UrlCountAdd=2023,
            ErrUrlCountAdd=2024,
        }

        public enum UrlGatherResult
        {
            UnGather=2031,
            Succeed=2032,
            Error=2033,
            Gathered=2034,
        }

        public enum DatabaseType
        {
            Access=2051,
            MSSqlServer=2052,
            MySql=2053,
        }

        public enum LogType
        {
            Info=2061,
            Error=2062,
            Warning=2063,
            RunPlanTask=2064,
            GatherError=2065,
            PublishError=2066,
        }

        public enum RunTaskType
        {
            SoukeyTask=2071,           //Soukey采摘任务
            DataTask=2072,               //数据库存储过程
            OtherTask=2073,            //外接可执行命令
        }

        public enum RunTaskPlanType
        {
            Ones=2081,                //仅运行一次
            DayOnes=2082,             //每天运行一次
            DayTwice=2083,            //每天上午下午各运行一次
            Weekly=2084,              //每周运行
            Custom=2085,              //自定义运行间隔
        }

        public enum PlanDisabledType
        {
            RunTime=2091,            //按照运行次数失效
            RunDateTime=2092,        //按照时间失效 
        }

        public enum PlanState
        {
            Enabled=3001,           //有效
            Disabled=3002,          //无效
            Expired =3003,          //过期
        }

        //监听类型
        public enum MessageType
        {
            RunSoukeyTask=3010,
            RunFileTask=3011,
            RunData=3013,
            ReloadPlan=3013,
            MonitorFileFaild=3014,
        }

        //触发器类型
        public enum TriggerType
        {
            GatheredRun=3020,
            PublishedRun=3021,
        }


        #endregion
       

        #region 界面显示与系统处理转换  这个需要改成字典表进行筛选了，不能这样了
        static public int ConvertID(string enumName)
        {
            switch (enumName)
            {
                case "不编码":
                    return (int)WebCode.NoCoding;
                case "自动":
                    return (int)WebCode.auto;
                case "gb2312":
                    return (int)WebCode.gb2312;
                case "UTF-8" :
                    return (int)WebCode.utf8;
                case "gbk":
                    return (int)WebCode.gbk;
                case "big5":
                    return (int)WebCode.big5;


                case "仅采集数据" :
                    return (int)TaskRunType.OnlyGather ;
                case "采集并发布数据" :
                    return (int)TaskRunType.GatherExportData;
                case  "根据网址采集网页数据":
                    return (int)TaskType.HtmlByUrl;
                case "采集整站网页数据" :
                    return (int)TaskType.HtmlByWeb ;
                case "采集RSS聚合数据":
                    return (int)TaskType.RssByUrl;
                case "采集ajax网页数据":
                    return (int)TaskType.AjaxHtmlByUrl ;


                case "不做任意格式的限制":
                    return (int)LimitSign.NoLimit;
                case "匹配时去掉网页符号":
                    return (int)LimitSign.NoWebSign;
                case "只匹配中文":
                    return (int)LimitSign.OnlyCN;
                case "只匹配双字节字符":
                    return (int)LimitSign.OnlyDoubleByte; 
                case "只匹配数字":
                    return (int)LimitSign.OnlyNumber ;
                case "只匹配字母数字及常用字符":
                    return (int)LimitSign.OnlyChar ;
                case "自定义正则匹配表达式":
                    return (int)LimitSign.Custom;

                case "不做输出控制":
                    return (int)ExportLimit.ExportNoLimit;
                case "输出时去掉网页符号":
                    return (int)ExportLimit.ExportNoWebSign;
                case "输出时附加前缀":
                    return (int)ExportLimit.ExportPrefix;
                case "输出时附加后缀":
                    return (int)ExportLimit.ExportSuffix;
                case "左起去掉字符":
                    return (int)ExportLimit.ExportTrimLeft;
                case "右起去掉字符":
                    return (int)ExportLimit.ExportTrimRight;
                case "替换其中符合条件的字符":
                    return (int)ExportLimit.ExportReplace;
                case "去掉字符串首尾空格":
                    return (int)ExportLimit.ExportTrim;
                case "输出时采用正则表达式进行替换":
                    return (int)ExportLimit.ExportRegexReplace;

                case "文件":
                    return (int)GDataType.File;
                case "Flash":
                    return (int)GDataType.Flash;
                case "图片":
                    return (int)GDataType.Picture;
                case "文本":
                    return (int)GDataType.Txt;

                case "Soukey采摘任务":
                    return (int)RunTaskType.SoukeyTask;
                case "数据库任务":
                    return (int)RunTaskType.DataTask;
                case "其它任务":
                    return (int)RunTaskType.OtherTask;

                case "超过指定运行次数计划失效":
                    return (int)PlanDisabledType.RunTime;
                case "超过指定时间计划失效":
                    return (int)PlanDisabledType.RunDateTime;

                case "Access":
                    return (int)DatabaseType.Access;
                case "MSSqlServer":
                    return (int)DatabaseType.MSSqlServer;
                case "MySql":
                    return (int)DatabaseType.MySql;

                default:
                    return 0;
            }
        }

        static public string  ConvertName(int CustomType)
        {
            switch (CustomType)
            {
                case (int)WebCode.NoCoding :
                    return "不编码";
                case (int)WebCode.auto:
                    return "自动";
                case (int)WebCode.gb2312:
                    return "gb2312";
                case (int)WebCode.utf8:
                    return "UTF-8";
                case (int)WebCode.gbk:
                    return "gbk";
                case (int)WebCode.big5 :
                    return "big5";

                case (int)TaskRunType.OnlyGather :
                    return "仅采集数据";
                case (int)TaskRunType.GatherExportData :
                    return "采集并发布数据";
                case (int)TaskType.HtmlByUrl :
                    return "根据网址采集网页数据";
                case (int)TaskType.HtmlByWeb :
                    return "采集整站网页数据";
                case (int)TaskType.RssByUrl :
                    return "采集RSS聚合数据";
                case (int)TaskType.AjaxHtmlByUrl :
                    return "采集ajax网页数据";

                case (int)LimitSign.NoLimit:
                    return "不做任意格式的限制";
                case (int)LimitSign.NoWebSign:
                    return "匹配时去掉网页符号";
                case (int)LimitSign.OnlyCN:
                    return "只匹配中文";
                case (int)LimitSign.OnlyDoubleByte:
                    return "只匹配双字节字符";
                case (int)LimitSign.OnlyNumber:
                    return "只匹配数字";
                case (int)LimitSign.OnlyChar:
                    return "只匹配字母数字及常用字符";
                case (int)LimitSign.Custom:
                    return "自定义正则匹配表达式";

                case (int)ExportLimit.ExportNoLimit:
                    return "不做输出控制";
                case (int)ExportLimit.ExportNoWebSign:
                    return "输出时去掉网页符号";
                case (int)ExportLimit.ExportPrefix:
                    return "输出时附加前缀";
                case (int)ExportLimit.ExportSuffix:
                    return "输出时附加后缀";
                case (int)ExportLimit.ExportTrimLeft:
                    return "左起去掉字符";
                case (int)ExportLimit.ExportTrimRight:
                    return "右起去掉字符";
                case (int)ExportLimit.ExportReplace:
                    return "替换其中符合条件的字符";
                case (int)ExportLimit.ExportTrim :
                    return "去掉字符串首尾空格";
                case (int)ExportLimit.ExportRegexReplace :
                    return "输出时采用正则表达式进行替换";

                case (int)PublishType.NoPublish :
                    return "不发布数据";
                case (int)PublishType.PublishAccess :
                    return "发布到Access数据库";
                case (int)PublishType.PublishMSSql :
                    return "发布到MS Sql Server";
                case (int)PublishType.PublishExcel :
                    return "发布到Excel文件";
                case (int)PublishType.PublishTxt :
                    return "发布到文本文件";
                case (int)PublishType.PublishMySql:
                    return "发布到MySql数据库";
                case (int)PublishType.PublishWeb :
                    return "在线发布";

                case (int)GDataType.File :
                    return "文件";
                case (int)GDataType.Flash  :
                    return "Flash";
                case (int)GDataType.Picture  :
                    return "图片";
                case (int)GDataType.Txt :
                    return "文本";

                case (int)RunTaskType.SoukeyTask :
                    return "Soukey采摘任务";
                case (int)RunTaskType.DataTask :
                    return "数据库任务";
                case (int)RunTaskType.OtherTask :
                    return "其它任务";

                case (int)PlanDisabledType.RunTime:
                    return "超过指定运行次数计划失效";
                case (int)PlanDisabledType.RunDateTime:
                    return "超过指定时间计划失效";

                case (int)PlanState .Enabled :
                    return "有效";
                case (int)PlanState.Disabled :
                    return "失效";
                case (int)PlanState.Expired :
                    return "过期";

                case (int)DatabaseType.Access:
                    return "Access";
                case (int)DatabaseType.MSSqlServer:
                    return "MSSqlServer";
                case (int)DatabaseType.MySql:
                    return "MySql";


                default:
                    return "";
            }
        }

        #endregion

    }
}
