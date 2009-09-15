using System;
using System.Collections.Generic;
using System.Text;

///���ܣ�ȫ�� ���� ���� ��
///���ʱ�䣺2009-3-2
///���ߣ�һ��
///�������⣺��
///�����ƻ�����
///˵����ת������ʵ�������壬ֱ�ӿ������Ľ��ж��壬�����˱��ϰ�߲�ϲ���������������ṩ��ת��������
///�汾��01.10.00
///�޶�����
namespace SoukeyNetget
{
    public static class cGlobalParas
    {

        #region ö�ٳ���
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
           NoLimit = 2001,          //���������ʽ������
           NoWebSign = 2002,        //ƥ��ʱȥ����ҳ����
           OnlyCN = 2003,           //ֻƥ������
           OnlyDoubleByte=2004,     //ֻƥ��˫�ֽ��ַ�
           OnlyNumber=2005,         //ֻƥ������
           OnlyChar=2006,           //ֻƥ����ĸ���ּ������ַ�
           Custom = 2007,           //�Զ�������ƥ����ʽ 
        }

        public enum ExportLimit
        {
            ExportNoLimit = 2040,      //�����������
            ExportNoWebSign = 2041,    //���ʱȥ����ҳ����
            ExportPrefix = 2042,       //���ʱ����ǰ׺  
            ExportSuffix = 2043,       //���ʱ���Ӻ�׺
            ExportTrimLeft = 2044,     //����ȥ���ַ�
            ExportTrimRight = 2045,    //����ȥ���ַ�
            ExportReplace = 2046,      //�滻���з����������ַ�
            ExportTrim = 2047,         //ȥ���ַ�����β�ո�
            ExportRegexReplace = 2048, //���ʱ����������ʽ�����滻


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
            SoukeyTask=2071,           //Soukey��ժ����
            DataTask=2072,               //���ݿ�洢����
            OtherTask=2073,            //��ӿ�ִ������
        }

        public enum RunTaskPlanType
        {
            Ones=2081,                //������һ��
            DayOnes=2082,             //ÿ������һ��
            DayTwice=2083,            //ÿ���������������һ��
            Weekly=2084,              //ÿ������
            Custom=2085,              //�Զ������м��
        }

        public enum PlanDisabledType
        {
            RunTime=2091,            //�������д���ʧЧ
            RunDateTime=2092,        //����ʱ��ʧЧ 
        }

        public enum PlanState
        {
            Enabled=3001,           //��Ч
            Disabled=3002,          //��Ч
            Expired =3003,          //����
        }

        //��������
        public enum MessageType
        {
            RunSoukeyTask=3010,
            RunFileTask=3011,
            RunData=3013,
            ReloadPlan=3013,
            MonitorFileFaild=3014,
        }

        //����������
        public enum TriggerType
        {
            GatheredRun=3020,
            PublishedRun=3021,
        }


        #endregion
       

        #region ������ʾ��ϵͳ����ת��  �����Ҫ�ĳ��ֵ�����ɸѡ�ˣ�����������
        static public int ConvertID(string enumName)
        {
            switch (enumName)
            {
                case "������":
                    return (int)WebCode.NoCoding;
                case "�Զ�":
                    return (int)WebCode.auto;
                case "gb2312":
                    return (int)WebCode.gb2312;
                case "UTF-8" :
                    return (int)WebCode.utf8;
                case "gbk":
                    return (int)WebCode.gbk;
                case "big5":
                    return (int)WebCode.big5;


                case "���ɼ�����" :
                    return (int)TaskRunType.OnlyGather ;
                case "�ɼ�����������" :
                    return (int)TaskRunType.GatherExportData;
                case  "������ַ�ɼ���ҳ����":
                    return (int)TaskType.HtmlByUrl;
                case "�ɼ���վ��ҳ����" :
                    return (int)TaskType.HtmlByWeb ;
                case "�ɼ�RSS�ۺ�����":
                    return (int)TaskType.RssByUrl;
                case "�ɼ�ajax��ҳ����":
                    return (int)TaskType.AjaxHtmlByUrl ;


                case "���������ʽ������":
                    return (int)LimitSign.NoLimit;
                case "ƥ��ʱȥ����ҳ����":
                    return (int)LimitSign.NoWebSign;
                case "ֻƥ������":
                    return (int)LimitSign.OnlyCN;
                case "ֻƥ��˫�ֽ��ַ�":
                    return (int)LimitSign.OnlyDoubleByte; 
                case "ֻƥ������":
                    return (int)LimitSign.OnlyNumber ;
                case "ֻƥ����ĸ���ּ������ַ�":
                    return (int)LimitSign.OnlyChar ;
                case "�Զ�������ƥ����ʽ":
                    return (int)LimitSign.Custom;

                case "�����������":
                    return (int)ExportLimit.ExportNoLimit;
                case "���ʱȥ����ҳ����":
                    return (int)ExportLimit.ExportNoWebSign;
                case "���ʱ����ǰ׺":
                    return (int)ExportLimit.ExportPrefix;
                case "���ʱ���Ӻ�׺":
                    return (int)ExportLimit.ExportSuffix;
                case "����ȥ���ַ�":
                    return (int)ExportLimit.ExportTrimLeft;
                case "����ȥ���ַ�":
                    return (int)ExportLimit.ExportTrimRight;
                case "�滻���з����������ַ�":
                    return (int)ExportLimit.ExportReplace;
                case "ȥ���ַ�����β�ո�":
                    return (int)ExportLimit.ExportTrim;
                case "���ʱ����������ʽ�����滻":
                    return (int)ExportLimit.ExportRegexReplace;

                case "�ļ�":
                    return (int)GDataType.File;
                case "Flash":
                    return (int)GDataType.Flash;
                case "ͼƬ":
                    return (int)GDataType.Picture;
                case "�ı�":
                    return (int)GDataType.Txt;

                case "Soukey��ժ����":
                    return (int)RunTaskType.SoukeyTask;
                case "���ݿ�����":
                    return (int)RunTaskType.DataTask;
                case "��������":
                    return (int)RunTaskType.OtherTask;

                case "����ָ�����д����ƻ�ʧЧ":
                    return (int)PlanDisabledType.RunTime;
                case "����ָ��ʱ��ƻ�ʧЧ":
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
                    return "������";
                case (int)WebCode.auto:
                    return "�Զ�";
                case (int)WebCode.gb2312:
                    return "gb2312";
                case (int)WebCode.utf8:
                    return "UTF-8";
                case (int)WebCode.gbk:
                    return "gbk";
                case (int)WebCode.big5 :
                    return "big5";

                case (int)TaskRunType.OnlyGather :
                    return "���ɼ�����";
                case (int)TaskRunType.GatherExportData :
                    return "�ɼ�����������";
                case (int)TaskType.HtmlByUrl :
                    return "������ַ�ɼ���ҳ����";
                case (int)TaskType.HtmlByWeb :
                    return "�ɼ���վ��ҳ����";
                case (int)TaskType.RssByUrl :
                    return "�ɼ�RSS�ۺ�����";
                case (int)TaskType.AjaxHtmlByUrl :
                    return "�ɼ�ajax��ҳ����";

                case (int)LimitSign.NoLimit:
                    return "���������ʽ������";
                case (int)LimitSign.NoWebSign:
                    return "ƥ��ʱȥ����ҳ����";
                case (int)LimitSign.OnlyCN:
                    return "ֻƥ������";
                case (int)LimitSign.OnlyDoubleByte:
                    return "ֻƥ��˫�ֽ��ַ�";
                case (int)LimitSign.OnlyNumber:
                    return "ֻƥ������";
                case (int)LimitSign.OnlyChar:
                    return "ֻƥ����ĸ���ּ������ַ�";
                case (int)LimitSign.Custom:
                    return "�Զ�������ƥ����ʽ";

                case (int)ExportLimit.ExportNoLimit:
                    return "�����������";
                case (int)ExportLimit.ExportNoWebSign:
                    return "���ʱȥ����ҳ����";
                case (int)ExportLimit.ExportPrefix:
                    return "���ʱ����ǰ׺";
                case (int)ExportLimit.ExportSuffix:
                    return "���ʱ���Ӻ�׺";
                case (int)ExportLimit.ExportTrimLeft:
                    return "����ȥ���ַ�";
                case (int)ExportLimit.ExportTrimRight:
                    return "����ȥ���ַ�";
                case (int)ExportLimit.ExportReplace:
                    return "�滻���з����������ַ�";
                case (int)ExportLimit.ExportTrim :
                    return "ȥ���ַ�����β�ո�";
                case (int)ExportLimit.ExportRegexReplace :
                    return "���ʱ����������ʽ�����滻";

                case (int)PublishType.NoPublish :
                    return "����������";
                case (int)PublishType.PublishAccess :
                    return "������Access���ݿ�";
                case (int)PublishType.PublishMSSql :
                    return "������MS Sql Server";
                case (int)PublishType.PublishExcel :
                    return "������Excel�ļ�";
                case (int)PublishType.PublishTxt :
                    return "�������ı��ļ�";
                case (int)PublishType.PublishMySql:
                    return "������MySql���ݿ�";
                case (int)PublishType.PublishWeb :
                    return "���߷���";

                case (int)GDataType.File :
                    return "�ļ�";
                case (int)GDataType.Flash  :
                    return "Flash";
                case (int)GDataType.Picture  :
                    return "ͼƬ";
                case (int)GDataType.Txt :
                    return "�ı�";

                case (int)RunTaskType.SoukeyTask :
                    return "Soukey��ժ����";
                case (int)RunTaskType.DataTask :
                    return "���ݿ�����";
                case (int)RunTaskType.OtherTask :
                    return "��������";

                case (int)PlanDisabledType.RunTime:
                    return "����ָ�����д����ƻ�ʧЧ";
                case (int)PlanDisabledType.RunDateTime:
                    return "����ָ��ʱ��ƻ�ʧЧ";

                case (int)PlanState .Enabled :
                    return "��Ч";
                case (int)PlanState.Disabled :
                    return "ʧЧ";
                case (int)PlanState.Expired :
                    return "����";

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
