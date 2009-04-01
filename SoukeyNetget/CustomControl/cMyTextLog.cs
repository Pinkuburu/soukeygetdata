using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms ;

///功能：自定义文本控件，主要用于采集任务日志的显示
///完成时间：2009-3-2
///作者：一孑
///遗留问题：无
///开发计划：无
///说明：无 
///版本：00.90.00
///修订：无
namespace SoukeyNetget.CustomControl
{
    class cMyTextLog : RichTextBox 
    {
        public cMyTextLog()
        {
            this.Text = "";
        }

        public void Clear() 
        {
            this.Text = "";
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value + base.Text ;
            }
        }

       
    }
}
