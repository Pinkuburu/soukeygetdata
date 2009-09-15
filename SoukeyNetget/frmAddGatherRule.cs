using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SoukeyNetget
{
    public partial class frmAddGatherRule : Form
    {
        public delegate void ReturnData(ListViewItem Litem);
        public ReturnData rData;


        public frmAddGatherRule()
        {
            InitializeComponent();
        }

        private void frmAddGatherRule_Load(object sender, EventArgs e)
        {
            this.comLimit.Items.Add("不做任意格式的限制");
            this.comLimit.Items.Add("匹配时去掉网页符号");
            this.comLimit.Items.Add("只匹配中文");
            this.comLimit.Items.Add("只匹配双字节字符");
            this.comLimit.Items.Add("只匹配数字");
            this.comLimit.Items.Add("只匹配字母数字及常用字符");
            this.comLimit.Items.Add("自定义正则匹配表达式");
            this.comLimit.SelectedIndex = 0;

            this.comExportLimit.Items.Add("不做输出控制");
            this.comExportLimit.Items.Add("输出时去掉网页符号");
            this.comExportLimit.Items.Add("输出时附加前缀");
            this.comExportLimit.Items.Add("输出时附加后缀");
            this.comExportLimit.Items.Add("左起去掉字符");
            this.comExportLimit.Items.Add("右起去掉字符");
            this.comExportLimit.Items.Add("替换其中符合条件的字符");
            this.comExportLimit.Items.Add("去掉字符串首尾空格");
            this.comExportLimit.Items.Add("输出时采用正则表达式进行替换");
            this.comExportLimit.SelectedIndex = 0;

            this.comGetType.Items.Add("文本");
            this.comGetType.Items.Add("图片");
            this.comGetType.Items.Add("Flash");
            this.comGetType.Items.Add("文件");
            this.comGetType.SelectedIndex = 0;

            this.txtGetTitleName.Items.Add("链接地址");
            this.txtGetTitleName.Items.Add("标题");
            this.txtGetTitleName.Items.Add("内容");
            this.txtGetTitleName.Items.Add("图片");
        }

        private void comLimit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comLimit.SelectedIndex == 6)
                this.txtRegion.Enabled = true;
            else
                this.txtRegion.Enabled = false;
        }

        private void comExportLimit_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.comExportLimit.SelectedIndex)
            {
                case 0:
                    this.label37.Text = "加工条件：";
                    this.txtExpression.Text = "";
                    this.txtExpression.Enabled = false;
                    break;
                case 1:
                    this.label37.Text = "加工条件：";
                    this.txtExpression.Text = "";
                    this.txtExpression.Enabled = false;
                    break;
                case 2:
                    this.label37.Text = "前缀：";
                    this.txtExpression.Text = "";
                    this.txtExpression.Enabled = true;
                    break;
                case 3:
                    this.label37.Text = "后缀：";
                    this.txtExpression.Text = "";
                    this.txtExpression.Enabled = true;
                    break;
                case 4:
                    this.label37.Text = "截取字符数：";
                    this.txtExpression.Text = "0";
                    this.txtExpression.Enabled = true;
                    break;
                case 5:
                    this.label37.Text = "截取字符数：";
                    this.txtExpression.Text = "0";
                    this.txtExpression.Enabled = true;
                    break;
                case 6:
                    this.label37.Text = "表达式：";
                    this.txtExpression.Text = "\"\",\"\"";
                    this.txtExpression.Enabled = true;
                    break;
                case 7:
                    this.txtExpression.Enabled = false;
                    break;
                case 8:
                    this.label37.Text = "正则表达式：";
                    this.txtExpression.Text = "\"\",\"\"";
                    this.txtExpression.Enabled = true;
                    break;
                default:
                    this.txtExpression.Enabled = false;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.txtGetTitleName.Text.Trim().ToString() == "")
            {
                MessageBox.Show ("请输入采集数据的名称","Soukey采摘 系统信息",MessageBoxButtons.OK ,MessageBoxIcon.Information );
                this.txtGetTitleName.Focus();
                return;
            }

            if (this.txtGetStart.Text.Trim().ToString() == "")
            {
                MessageBox.Show("请输入采集数据的起始标志", "Soukey采摘 系统信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtGetStart.Focus();
                return;

            }

            if (this.txtGetEnd.Text.Trim().ToString() == "")
            {
                MessageBox.Show("请输入采集数据的结束标志", "Soukey采摘 系统信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtGetEnd.Focus();
                return;
            }

            if (this.comLimit.SelectedIndex == -1)
            {
                this.comLimit.SelectedIndex = 0;
            }

            ListViewItem item = new ListViewItem();
            item.Text = this.txtGetTitleName.Text.ToString();
            item.SubItems.Add(this.comGetType.SelectedItem.ToString());
            item.SubItems.Add(cTool.ClearFlag(this.txtGetStart.Text.ToString()));
            item.SubItems.Add(cTool.ClearFlag(this.txtGetEnd.Text.ToString()));
            item.SubItems.Add(this.comLimit.SelectedItem.ToString());
            item.SubItems.Add(this.txtRegion.Text.ToString());
            item.SubItems.Add(this.comExportLimit.SelectedItem.ToString());
            item.SubItems.Add(this.txtExpression.Text.ToString());

            rData(item);

            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}