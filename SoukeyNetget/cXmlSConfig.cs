using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

///功能：处理系统所有的配置信息
///完成时间：2009-4-
///作者：一孑
///遗留问题：无
///开发计划：待定
///说明：与cSystem会有功能重复，下一步待定，所以保留
///版本：01.00.00
///修订：无
namespace SoukeyNetget
{
    class cXmlSConfig
    {
        cXmlIO xmlConfig;

        public cXmlSConfig()
        {
            //打开配置文件
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
