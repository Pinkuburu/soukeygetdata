using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms ;

///���ܣ��Զ����ı��ؼ�����Ҫ���ڲɼ�������־����ʾ
///���ʱ�䣺2009-3-2
///���ߣ�һ��
///�������⣺��
///�����ƻ�����
///˵������ 
///�汾��00.90.00
///�޶�����
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
