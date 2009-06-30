using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

///���ܣ�����ϵͳ���е�������Ϣ
///���ʱ�䣺2009-4-
///���ߣ�һ��
///�������⣺��
///�����ƻ�������
///˵������cSystem���й����ظ�����һ�����������Ա���
///�汾��01.00.00
///�޶�����
namespace SoukeyNetget
{
    class cXmlSConfig
    {
        cXmlIO xmlConfig;

        public cXmlSConfig()
        {
            //�������ļ�
            xmlConfig = new cXmlIO("SoukeyConfig.xml");
           
        }

        ~cXmlSConfig()
        {
        }

        public bool ExitIsShow
        {
            get 
            {
                if (xmlConfig.GetNodeValue("Config/Exit/IsShow") == "0")
                    return false;
                else
                    return true;
            }
            set 
            {
                string s = "0";
                if (value == true)
                    s = "1";
                else
                    s = "0";
            
                xmlConfig.EditNodeValue("Config/Exit/IsShow", s);
                xmlConfig.Save();
            }
        }

        public int ExitSelected
        {
            get {return int.Parse (xmlConfig.GetNodeValue("Config/Exit/Selected")); }
            set 
            {
                int i = value;
                xmlConfig.EditNodeValue("Config/Exit/Selected", i.ToString ());
                xmlConfig.Save();
            }
        }

    }
}
