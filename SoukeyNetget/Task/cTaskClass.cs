using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;

///���ܣ��ɼ�������� ����
///���ʱ�䣺2009-3-2
///���ߣ�һ��
///�������⣺��
///�����ƻ�����
///˵������ 
///�汾��00.90.00
///�޶�����
namespace SoukeyNetget.Task
{
    class cTaskClass
    {
        cXmlIO xmlConfig;
        DataView TaskClass;

        //����һ��������
        public List<cTaskIndex> Task
        {
            get { return Task; }
            set { Task = value; }
        }

        public cTaskClass()
        {
            try
            {
                xmlConfig = new cXmlIO(Program.getPrjPath () + "TaskClass.xml");

                //��ȡTaskClass�ڵ�
                TaskClass = xmlConfig.GetData("descendant::TaskClasses");
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        ~cTaskClass()
        {
            xmlConfig = null;
        }

        //���㵱ǰ���ж��ٸ�TaskClass
        public int GetTaskClassCount()
        {
            int tCount = TaskClass.Count;
            return tCount;
        }

        //����ָ����index����TaskID
        public int GetTaskClassID(int index)
        {
            int TClassID = int.Parse (TaskClass[index].Row["id"].ToString());
            return TClassID;
        }

        //�����ƶ���index����TaskClassName
        public string GetTaskClassName(int index)
        {
            string TClassName = TaskClass[index].Row["Name"].ToString();
            return TClassName;
        }

        //����ָ����index����TaskClassPath
        public string GetTaskClassPathByIndex(int index)
        {
            return "";
        }

        //����ָ����ID����TaskClassPath
        public string GetTaskClassPathByID(int id)
        {
            int i = 0;

            for (i = 0; i < GetTaskClassCount();i++ )
            {
                if (int.Parse (TaskClass[i].Row["id"].ToString()) == id)
                {
                    return TaskClass[i].Row["Path"].ToString();
                }
            }

            return "";
        }

        //����ָ����Task�������Ʒ�������������洢��·��
        public string GetTaskClassPathByName(String Name)
        {
            int i = 0;
            for (i = 0; i < GetTaskClassCount(); i++)
            {
                if (TaskClass[i].Row["Name"].ToString() == Name)
                {
                    return TaskClass[i].Row["Path"].ToString();
                }
            }
            return "";
        }

        //�ж���������Ƿ����
        public bool IsExist(string TaskClassName)
        {
            bool isbool = false;

            for (int i = 0; i < TaskClass.Count; i++)
            {
                if (TaskClass[i].Row["Name"].ToString() == TaskClassName)
                {
                    isbool = true;
                    break;
                }
            }

            return isbool;
        }

        //���ӷ���ڵ㣬�����ӳɹ����򷵻���ӳɹ���ķ���ڵ�ID
        public int AddTaskClass(string TaskClassName,string TaskClassPath)
        {
            //��Ҫ�ж��½�������������Ƿ��Ѿ�����
            for (int i = 0; i < TaskClass.Count; i++)
            {
                if (TaskClass[i].Row["Name"].ToString() == TaskClassName)
                {
                    throw new cSoukeyException("��������Ѿ����ڣ�");
                }
            }

            string strTaskClass = "";
            int MaxID=0;
            int index=TaskClass.Count-1;
            MaxID = int.Parse(TaskClass[index].Row["id"].ToString()) + 1;

            strTaskClass = "<id>" + MaxID + "</id>";
            strTaskClass += "<Name>" + TaskClassName + "</Name>";
            strTaskClass += "<Path>" + TaskClassPath + "</Path>";
            xmlConfig.InsertElement("TaskConfig/TaskClasses", "TaskClass", strTaskClass);
            xmlConfig.Save();
            

            //�����������������Ŀ¼�������ļ�
            if (!System.IO.Directory.Exists(TaskClassPath))
            {
                System.IO.Directory.CreateDirectory(TaskClassPath);
            }

            Task.cTaskIndex tIndex = new Task.cTaskIndex();
            tIndex.NewIndexFile(TaskClassPath);
            tIndex = null;

            return MaxID;

        }

        //ɾ��ָ���ķ����ļ�
        public bool DelTaskClass(string TClassName)
        {
            //����ɾ��TaskClass.xml�е�������������ڵ�
            string FilePath = this.GetTaskClassPathByName(TClassName);
            xmlConfig.DeleteChildNodes("TaskClasses", "Name", TClassName);
            xmlConfig.Save();

            string FileName =FilePath   + "\\index.xml";
            System.IO.File.Delete(FileName);
            return true;
        }
    }
}
