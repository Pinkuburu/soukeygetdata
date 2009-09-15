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
            this.comLimit.Items.Add("���������ʽ������");
            this.comLimit.Items.Add("ƥ��ʱȥ����ҳ����");
            this.comLimit.Items.Add("ֻƥ������");
            this.comLimit.Items.Add("ֻƥ��˫�ֽ��ַ�");
            this.comLimit.Items.Add("ֻƥ������");
            this.comLimit.Items.Add("ֻƥ����ĸ���ּ������ַ�");
            this.comLimit.Items.Add("�Զ�������ƥ����ʽ");
            this.comLimit.SelectedIndex = 0;

            this.comExportLimit.Items.Add("�����������");
            this.comExportLimit.Items.Add("���ʱȥ����ҳ����");
            this.comExportLimit.Items.Add("���ʱ����ǰ׺");
            this.comExportLimit.Items.Add("���ʱ���Ӻ�׺");
            this.comExportLimit.Items.Add("����ȥ���ַ�");
            this.comExportLimit.Items.Add("����ȥ���ַ�");
            this.comExportLimit.Items.Add("�滻���з����������ַ�");
            this.comExportLimit.Items.Add("ȥ���ַ�����β�ո�");
            this.comExportLimit.Items.Add("���ʱ����������ʽ�����滻");
            this.comExportLimit.SelectedIndex = 0;

            this.comGetType.Items.Add("�ı�");
            this.comGetType.Items.Add("ͼƬ");
            this.comGetType.Items.Add("Flash");
            this.comGetType.Items.Add("�ļ�");
            this.comGetType.SelectedIndex = 0;

            this.txtGetTitleName.Items.Add("���ӵ�ַ");
            this.txtGetTitleName.Items.Add("����");
            this.txtGetTitleName.Items.Add("����");
            this.txtGetTitleName.Items.Add("ͼƬ");
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
                    this.label37.Text = "�ӹ�������";
                    this.txtExpression.Text = "";
                    this.txtExpression.Enabled = false;
                    break;
                case 1:
                    this.label37.Text = "�ӹ�������";
                    this.txtExpression.Text = "";
                    this.txtExpression.Enabled = false;
                    break;
                case 2:
                    this.label37.Text = "ǰ׺��";
                    this.txtExpression.Text = "";
                    this.txtExpression.Enabled = true;
                    break;
                case 3:
                    this.label37.Text = "��׺��";
                    this.txtExpression.Text = "";
                    this.txtExpression.Enabled = true;
                    break;
                case 4:
                    this.label37.Text = "��ȡ�ַ�����";
                    this.txtExpression.Text = "0";
                    this.txtExpression.Enabled = true;
                    break;
                case 5:
                    this.label37.Text = "��ȡ�ַ�����";
                    this.txtExpression.Text = "0";
                    this.txtExpression.Enabled = true;
                    break;
                case 6:
                    this.label37.Text = "���ʽ��";
                    this.txtExpression.Text = "\"\",\"\"";
                    this.txtExpression.Enabled = true;
                    break;
                case 7:
                    this.txtExpression.Enabled = false;
                    break;
                case 8:
                    this.label37.Text = "������ʽ��";
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
                MessageBox.Show ("������ɼ����ݵ�����","Soukey��ժ ϵͳ��Ϣ",MessageBoxButtons.OK ,MessageBoxIcon.Information );
                this.txtGetTitleName.Focus();
                return;
            }

            if (this.txtGetStart.Text.Trim().ToString() == "")
            {
                MessageBox.Show("������ɼ����ݵ���ʼ��־", "Soukey��ժ ϵͳ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtGetStart.Focus();
                return;

            }

            if (this.txtGetEnd.Text.Trim().ToString() == "")
            {
                MessageBox.Show("������ɼ����ݵĽ�����־", "Soukey��ժ ϵͳ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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