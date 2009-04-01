using System;
using System.Collections.Generic;
using System.Text;

///功能：处理系统所有的配置信息
///完成时间：作废
///作者：一孑
///遗留问题：无
///开发计划：待定
///说明：与cSystem会有功能重复，下一步待定，所以保留
///版本：00.90.00
///修订：无
namespace SoukeyNetget
{
    class cXmlSConfig
    {

        public cXmlSConfig()
        {
            //打开配置文件
            cXmlIO xmlConfig = new cXmlIO("soukeyconfig.xml");
        }


    }
}
