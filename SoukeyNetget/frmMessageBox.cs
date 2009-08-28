using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SoukeyNetget
{
    public partial class frmMessageBox : Form
    {
        //窗体关闭延迟时间，默认8秒
        private int DelayTime = 8000;
        private string m_Title;

        public frmMessageBox()
        {
            InitializeComponent();
        }

        public void MessageBox(string Mess, string Title, MessageBoxButtons but, MessageBoxIcon icon)
        {
            switch (icon)
            {
                case MessageBoxIcon.Question :
                    m_Title ="Soukey采摘 系统询问";
                    
                    //启动定时器
                    this.timer1.Enabled = true;
                    this.labDelay.Visible = true;

                    break;
                case MessageBoxIcon.Error :
                    m_Title ="Soukey采摘 系统错误";
                    break;
                case MessageBoxIcon.Information :
                    m_Title ="Soukey采摘 系统信息";
                    break ;
                default :
                    m_Title ="Soukey采摘 系统信息";
                    break ;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DelayTime == 0 || DelayTime < 0)
                SendKeys.Send("{Enter}");
            else
                DelayTime = DelayTime - 1000;

            string s=DelayTime.ToString().Substring(0, 1) + " 秒";
            this.Text = m_Title + " 自动响应：" + s;
            this.labDelay.Text = "自动响应：" + s + " 系统默认选择为“是(Y)”";
        }
    }
}