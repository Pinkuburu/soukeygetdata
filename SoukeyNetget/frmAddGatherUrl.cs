using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SoukeyNetget.Task;
using System.Text.RegularExpressions;

namespace SoukeyNetget
{
    public partial class frmAddGatherUrl : Form
    {
        Task.cUrlAnalyze gUrl = new Task.cUrlAnalyze();

        public delegate void ReturnData(ListViewItem Litem,cNavigRules nRules);
        public ReturnData rData;

        public frmAddGatherUrl()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.contextMenuStrip1.Show(this.button2, 0, 21);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.dataNRule.Rows.Count == 0)
            {
                MessageBox.Show("导航规则为空，无法测试！", "soukey提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<cNavigRule> cns = new List<cNavigRule>();
            cNavigRule cn;

            for (int m = 0; m < this.dataNRule.Rows.Count; m++)
            {
                cn = new cNavigRule();
                cn.Url = this.txtWebLink.Text;
                cn.Level = int.Parse(this.dataNRule.Rows[m].Cells[0].Value.ToString());
                cn.NavigRule = this.dataNRule.Rows[m].Cells[1].Value.ToString();

                cns.Add(cn);
            }

            string Url = GetTestUrl(this.txtWebLink.Text, cns);

            if (!Regex.IsMatch(Url, @"(http|https|ftp)+://[^\s]*"))
            {
                MessageBox.Show("网址无法打开，可能出错，请检查网址及导航规则。", "soukey信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            System.Diagnostics.Process.Start(Url);
        }

        private string GetTestUrl(string webLink, List<cNavigRule> NavRule)
        {
            List<string> Urls;


            Urls = gUrl.ParseUrlRule(webLink, NavRule);


            if (Urls == null || Urls.Count == 0)
                return "";

            string isReg = "[\"\\s]";
            string Url = "";

            for (int m = 0; m < Urls.Count; m++)
            {
                if (!Regex.IsMatch(Urls[m].ToString(), isReg))
                {
                    Url = Urls[m].ToString();
                    break;
                }
            }


            string PreUrl = "";

            //需要判断网页地址前后存在单引号或双引号
            if (Url.Substring(0, 1) == "'" || Url.Substring(0, 1) == "\"")
            {
                Url = Url.Substring(1, Url.Length - 1);
            }

            if (Url.Substring(Url.Length - 1, 1) == "'" || Url.Substring(Url.Length - 1, 1) == "\"")
            {
                Url = Url.Substring(0, Url.Length - 1);
            }

            //去除了相对网址表示，通过程序进行判断
            if (string.Compare(Url.Substring(0, 4), "http", true) != 0)
            {
                if (Url.Substring(0, 1) == "/")
                {
                    PreUrl = webLink.Substring(7, webLink.Length - 7);
                    PreUrl = PreUrl.Substring(0, PreUrl.IndexOf("/"));
                    PreUrl = "http://" + PreUrl;
                }
                else
                {
                    Match aa = Regex.Match(webLink, ".*/");
                    PreUrl = aa.Groups[0].Value.ToString();
                }
            }

            return PreUrl + Url;

        }

        private void cmdAddNRule_Click(object sender, EventArgs e)
        {
            if (this.txtNag.Text == "" || this.txtNag.Text == null)
            {
                this.txtNag.Focus();
                MessageBox.Show("请填写导航规则后再添加", "Soukey采摘 信息信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            for (int i = 0; i < this.dataNRule.Rows.Count; i++)
            {
                this.dataNRule.Rows[i].Cells[0].Value = i + 1;
            }

            int MaxLevel = 0;
            if (this.dataNRule.Rows.Count == 0)
                MaxLevel = 1;
            else
                MaxLevel = this.dataNRule.Rows.Count + 1;

            this.dataNRule.Rows.Add(MaxLevel.ToString(), this.txtNag.Text);

            this.txtNag.Text = "";

        }

        private void cmdDelNRule_Click(object sender, EventArgs e)
        {
            this.dataNRule.Focus();
            SendKeys.Send("{Del}");
        }

        private void frmAddGatherUrl_Load(object sender, EventArgs e)
        {
            //初始化导航规则的datagrid的表头
            DataGridViewTextBoxColumn nRuleLevel = new DataGridViewTextBoxColumn();
            nRuleLevel.HeaderText = "级别";
            nRuleLevel.Width = 40;
            this.dataNRule.Columns.Insert(0, nRuleLevel);

            DataGridViewTextBoxColumn nRule = new DataGridViewTextBoxColumn();
            nRule.HeaderText = "导航规则";
            nRule.Width = 240;
            this.dataNRule.Columns.Insert(1, nRule);
        }

        private void IsAutoNextPage_CheckedChanged(object sender, EventArgs e)
        {
            if (this.IsAutoNextPage.Checked == true)
            {
                this.label13.Enabled = true;
                this.txtNextPage.Enabled = true;
                this.txtNextPage.Text = "下一页";
            }
            else
            {
                if (this.txtNextPage.Text == "下一页")
                {
                    this.txtNextPage.Text = "";
                }
                this.label13.Enabled = false;
                this.txtNextPage.Enabled = false;
            }
            
        }

        private void IsNavigPage_CheckedChanged(object sender, EventArgs e)
        {
            if (this.IsNavigPage.Checked == true)
            {

                this.groupBox14.Enabled = true;
            }
            else
            {

                this.groupBox14.Enabled = false;
            }

            
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "手工捕获POST数据")
            {
                frmWeblink wftm = new frmWeblink();
                wftm.getFlag = 1;
                wftm.rPData = new frmWeblink.ReturnPOST(GetPData);
                wftm.ShowDialog();
                wftm.Dispose();

                return;
            }

            Match s;

            if (Regex.IsMatch(e.ClickedItem.ToString(), "[{].*[}]"))
            {
                s = Regex.Match(e.ClickedItem.ToString(), "[{].*[}]");
            }
            else
            {
                s = Regex.Match(e.ClickedItem.ToString(), "[<].*[>]");
            }

            int startPos = this.txtWebLink.SelectionStart;
            int l = this.txtWebLink.SelectionLength;

            this.txtWebLink.Text = this.txtWebLink.Text.Substring(0, startPos) + s.Groups[0].Value + this.txtWebLink.Text.Substring(startPos + l, this.txtWebLink.Text.Length - startPos - l);

            this.txtWebLink.SelectionStart = startPos + s.Groups[0].Value.Length;
            this.txtWebLink.ScrollToCaret();
        }

        private void GetPData(string strCookie, string pData)
        {
            //this.txtCookie.Text = strCookie;
            this.txtWebLink.Text += "<POST>" + pData + "</POST>";
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            this.contextMenuStrip1.Items.Clear();

            this.contextMenuStrip1.Items.Add("递增变量{Num:1,100,1}");
            this.contextMenuStrip1.Items.Add("递减变量{Num:100,1,-1}");
            this.contextMenuStrip1.Items.Add(new ToolStripSeparator());
            this.contextMenuStrip1.Items.Add("字母递增{Letter:a,z}");
            this.contextMenuStrip1.Items.Add("字母递减{Letter:z,a}");
            this.contextMenuStrip1.Items.Add(new ToolStripSeparator());
            this.contextMenuStrip1.Items.Add("POST前缀<POST>");
            this.contextMenuStrip1.Items.Add("POST后缀</POST>");
            this.contextMenuStrip1.Items.Add("手工捕获POST数据");
            this.contextMenuStrip1.Items.Add(new ToolStripSeparator());

            //初始化字典菜单的项目
            cDict d = new cDict();
            int count = d.GetDictClassCount();


            for (int i = 0; i < count; i++)
            {
                this.contextMenuStrip1.Items.Add("字典:{Dict:" + d.GetDictClassName(i).ToString() + "}");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int UrlCount = 0;
            cNavigRules m_listNaviRule = new cNavigRules();

            if (this.txtWebLink.Text.ToString() == null || this.txtWebLink.Text.Trim().ToString() == "" || this.txtWebLink.Text.Trim().ToString() == "http://")
            {
                MessageBox.Show("请输入网址信息", "Soukey采摘 系统信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtWebLink.Focus();
                return;
            }
            else
            {
                if (!Regex.IsMatch(this.txtWebLink.Text.Trim().ToString(), "http://", RegexOptions.IgnoreCase))
                {
                    MessageBox.Show("您输入的网址不合法，请检查", "Soukey采摘 系统信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtWebLink.Focus();
                    return;
                }
            }

            ListViewItem litem;
            litem = new ListViewItem();
            litem.Text = this.txtWebLink.Text.ToString();

            if (this.IsNavigPage.Checked == true)
            {
                litem.SubItems.Add("Y");

                cNavigRule cn;
                

                for (int m = 0; m < this.dataNRule.Rows.Count; m++)
                {
                    cn = new cNavigRule();
                    cn.Url = this.txtWebLink.Text;
                    cn.Level = m + 1;
                    cn.NavigRule = this.dataNRule.Rows[m].Cells[1].Value.ToString();

                    m_listNaviRule.Url = this.txtWebLink.Text;
                    m_listNaviRule.NavigRule.Add(cn);
                }

                litem.SubItems.Add(this.dataNRule.Rows.Count.ToString());

            }
            else
            {
                litem.SubItems.Add("N");
                litem.SubItems.Add("0");
            }



            if (this.IsAutoNextPage.Checked == true)
            {
                litem.SubItems.Add(this.txtNextPage.Text.ToString());
            }
            else
            {
                litem.SubItems.Add("");
            }

            UrlCount = gUrl.GetUrlCount(this.txtWebLink.Text.ToString());

            litem.SubItems.Add(UrlCount.ToString());

            rData(litem, m_listNaviRule);

            this.Close();

        }
    }
}