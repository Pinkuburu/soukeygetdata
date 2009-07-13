using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Web;
using System.Net;
using System.IO;
using SHDocVw;


///���ܣ����õ������������cookie
///���ʱ�䣺2009-3-2
///���ߣ�һ��
///�������⣺��
///�����ƻ�����һ����ܻ���չ�ܶ๦�ܣ����幦�ܴ���
///˵������ 
///�汾��01.00.00
///�޶�����
namespace SoukeyNetget
{
    public partial class frmWeblink : Form
    {

        public delegate void ReturnCookie(string Cookie);
        public ReturnCookie rCookie;

        public delegate void ReturnPOST(string cookie, string pData);
        public ReturnPOST rPData;

        //���Ϊ0�򷵻�cookie�����Ϊ1�򷵻�post����
        private int m_GetFlag = 0;

        public int getFlag
        {
            get { return m_GetFlag; }
            set { m_GetFlag = value; }
        }

        public frmWeblink()
        {
            InitializeComponent();
        }

        public frmWeblink(string Url)
        {
            InitializeComponent();

            this.webBrowser1.Navigate(Url);
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Navigate(this.toolStripTextBox1.Text.ToString());
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.webBrowser1.Navigate(this.toolStripTextBox1.Text.ToString());
            }
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            this.toolStripTextBox1.Text = this.webBrowser1.Url.ToString();
            this.textBox1.Text=this.webBrowser1.Document.Cookie.ToString();

          
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (this.getFlag == 0)
            {
                if (rCookie != null)
                {
                    rCookie(this.textBox1.Text);
                }
            }
            else if (this.getFlag == 1)
            {
                if (rPData != null)
                {
                    rPData(this.textBox1.Text,this.textBox2.Text );
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
            this.Dispose();
        }

        private void frmWeblink_Resize(object sender, EventArgs e)
        {
            this.textBox1.Width = this.Width - this.textBox1.Left - 20;
            this.textBox2.Width = this.textBox1.Width;
            this.toolStripTextBox1.Width = this.Width - this.toolStripLabel1.Width - this.toolStripButton1.Width - this.toolStripButton2.Width - 80;
            this.toolStrip1.AutoSize = true;
        }

        private void frmWeblink_Load(object sender, EventArgs e)
        {
            SHDocVw.WebBrowser wb = (SHDocVw.WebBrowser)webBrowser1.ActiveXInstance;
            wb.BeforeNavigate2 += new DWebBrowserEvents2_BeforeNavigate2EventHandler(WebBrowser_BeforeNavigate2);

            this.toolStripTextBox1.Focus();

        }

        private void WebBrowser_BeforeNavigate2(object pDisp, ref object URL, ref object Flags, ref object TargetFrameName, ref object PostData, ref object Headers, ref bool Cancel)
        {
            this.textBox2.Text = System.Text.Encoding.ASCII.GetString(PostData as byte[]);
        }
    }
}