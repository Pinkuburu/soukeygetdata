using System;
using System.Collections.Generic;
using System.Text;

///功能：全局 变量 常量 等
///完成时间：2009-3-2
///作者：一孑
///遗留问题：无
///开发计划：无
///说明：转换函数实际无意义，直接可用中文进行定义，但个人变成习惯不喜欢这样做，所以提供了转换函数。
///版本：01.00.00
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
            ByWebUrl=1051,
            ByWeb=1052,
        }

        public enum PublishType
        {
            NoPublish=1060,
            PublishAccess = 1061,
            PublishMSSql = 1062,
            PublishTxt=1063,
            PublishExcel=1064,
        }

        public enum LimitSign
        {
           NoLimit=2001,
           NoWebSign = 2002,
           ShowNoWebSign=2003,
           NoCN=2004,
        }

        public enum WebCode
        {
            auto = 1000,
            gb2312 = 1001,
            utf8 = 1002,
            gbk = 1003,
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

        #endregion

        #region 界面显示与系统处理转换
        static public int ConvertID(string enumName)
        {
            switch (enumName)
            {
                case "自动":
                    return (int)WebCode.auto;
                case "gb2312":
                    return (int)WebCode.gb2312;
                case "UTF-8" :
                    return (int)WebCode.utf8;
                case "GBK" :
                    return (int)WebCode.gbk;
                case "仅采集数据" :
                    return (int)TaskRunType.OnlyGather ;
                case "采集并发布数据" :
                    return (int)TaskRunType.GatherExportData;
                case  "根据指定的网址采集数据":
                    return (int)TaskType.ByWebUrl;
                case "根据网站的入口地址采集网站所有地址" :
                    return (int)TaskType.ByWeb;
                case "不做任意格式的限制":
                    return (int)LimitSign.NoLimit;
                case "不允许出现网页标识符":
                    return (int)LimitSign.NoWebSign;
                case "允许匹配但显示时删除":
                    return (int)LimitSign.ShowNoWebSign;
                case "不允许出现中文":
                    return (int)LimitSign.NoCN;
                case "文件":
                    return (int)GDataType.File;
                case "Flash":
                    return (int)GDataType.Flash;
                case "图片":
                    return (int)GDataType.Picture;
                case "文本":
                    return (int)GDataType.Txt;
                default:
                    return 0;
            }
        }

        static public string  ConvertName(int CustomType)
        {
            switch (CustomType)
            {
                case (int)WebCode.auto:
                    return "自动";
                case (int)WebCode.gb2312:
                    return "gb2312";
                case (int)WebCode.utf8:
                    return "UTF-8";
                case (int)WebCode.gbk:
                    return "GBK";
                case (int)TaskRunType.OnlyGather :
                    return "仅采集数据";
                case (int)TaskRunType.GatherExportData :
                    return "采集并发布数据";
                case (int)TaskType.ByWebUrl :
                    return "根据指定的网址采集数据";
                case (int)TaskType.ByWeb :
                    return "根据网站的入口地址采集网站所有地址";
                case (int)LimitSign.NoLimit:
                    return "不做任意格式的限制";
                case (int)LimitSign.NoWebSign:
                    return "不允许出现网页标识符";
                case (int)LimitSign.ShowNoWebSign:
                    return "允许匹配但显示时删除";
                case (int)LimitSign.NoCN :
                    return "不允许出现中文";
                case (int)PublishType.NoPublish :
                    return "不发布数据";
                case (int)PublishType.PublishAccess :
                    return "发布到Access数据库";
                case (int)PublishType.PublishMSSql :
                    return "发布到MS Sql Server";
                case (int)GDataType.File :
                    return "文件";
                case (int)GDataType.Flash  :
                    return "Flash";
                case (int)GDataType.Picture  :
                    return "图片";
                case (int)GDataType.Txt :
                    return "文本";
                default:
                    return "";
            }
        }

        #endregion
    }
}
