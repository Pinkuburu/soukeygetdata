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
        //����ر��ӳ�ʱ�䣬Ĭ��8��
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
                    m_Title ="Soukey��ժ ϵͳѯ��";
                    
                    //������ʱ��
                    this.timer1.Enabled = true;
                    this.labDelay.Visible = true;

                    break;
                case MessageBoxIcon.Error :
                    m_Title ="Soukey��ժ ϵͳ����";
                    break;
                case MessageBoxIcon.Information :
                    m_Title ="Soukey��ժ ϵͳ��Ϣ";
                    break ;
                default :
                    m_Title ="Soukey��ժ ϵͳ��Ϣ";
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

            string s=DelayTime.ToString().Substring(0, 1) + " ��";
            this.Text = m_Title + " �Զ���Ӧ��" + s;
            this.labDelay.Text = "�Զ���Ӧ��" + s + " ϵͳĬ��ѡ��Ϊ����(Y)��";
        }
    }
}