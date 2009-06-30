using System;
using System.Collections.Generic;
using System.Text;

///���ܣ�ȫ�� ���� ���� ��
///���ʱ�䣺2009-3-2
///���ߣ�һ��
///�������⣺��
///�����ƻ�����
///˵����ת������ʵ�������壬ֱ�ӿ������Ľ��ж��壬�����˱��ϰ�߲�ϲ���������������ṩ��ת��������
///�汾��01.00.00
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

        #region ������ʾ��ϵͳ����ת��
        static public int ConvertID(string enumName)
        {
            switch (enumName)
            {
                case "�Զ�":
                    return (int)WebCode.auto;
                case "gb2312":
                    return (int)WebCode.gb2312;
                case "UTF-8" :
                    return (int)WebCode.utf8;
                case "GBK" :
                    return (int)WebCode.gbk;
                case "���ɼ�����" :
                    return (int)TaskRunType.OnlyGather ;
                case "�ɼ�����������" :
                    return (int)TaskRunType.GatherExportData;
                case  "����ָ������ַ�ɼ�����":
                    return (int)TaskType.ByWebUrl;
                case "������վ����ڵ�ַ�ɼ���վ���е�ַ" :
                    return (int)TaskType.ByWeb;
                case "���������ʽ������":
                    return (int)LimitSign.NoLimit;
                case "�����������ҳ��ʶ��":
                    return (int)LimitSign.NoWebSign;
                case "����ƥ�䵫��ʾʱɾ��":
                    return (int)LimitSign.ShowNoWebSign;
                case "�������������":
                    return (int)LimitSign.NoCN;
                case "�ļ�":
                    return (int)GDataType.File;
                case "Flash":
                    return (int)GDataType.Flash;
                case "ͼƬ":
                    return (int)GDataType.Picture;
                case "�ı�":
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
                    return "�Զ�";
                case (int)WebCode.gb2312:
                    return "gb2312";
                case (int)WebCode.utf8:
                    return "UTF-8";
                case (int)WebCode.gbk:
                    return "GBK";
                case (int)TaskRunType.OnlyGather :
                    return "���ɼ�����";
                case (int)TaskRunType.GatherExportData :
                    return "�ɼ�����������";
                case (int)TaskType.ByWebUrl :
                    return "����ָ������ַ�ɼ�����";
                case (int)TaskType.ByWeb :
                    return "������վ����ڵ�ַ�ɼ���վ���е�ַ";
                case (int)LimitSign.NoLimit:
                    return "���������ʽ������";
                case (int)LimitSign.NoWebSign:
                    return "�����������ҳ��ʶ��";
                case (int)LimitSign.ShowNoWebSign:
                    return "����ƥ�䵫��ʾʱɾ��";
                case (int)LimitSign.NoCN :
                    return "�������������";
                case (int)PublishType.NoPublish :
                    return "����������";
                case (int)PublishType.PublishAccess :
                    return "������Access���ݿ�";
                case (int)PublishType.PublishMSSql :
                    return "������MS Sql Server";
                case (int)GDataType.File :
                    return "�ļ�";
                case (int)GDataType.Flash  :
                    return "Flash";
                case (int)GDataType.Picture  :
                    return "ͼƬ";
                case (int)GDataType.Txt :
                    return "�ı�";
                default:
                    return "";
            }
        }

        #endregion
    }
}
