using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


///���ܣ�������Ӧ������ĵ�������ģ��
///���ʱ�䣺2009-3-2
///���ߣ�һ��
///�������⣺��
///�����ƻ�����
///˵������ 
///�汾��00.90.00
///�޶�����
namespace SoukeyNetget
{
    public partial class frmImportTask : Form
    {

        public delegate void ReturnTaskID(int TaskID, string TaskName);

        public ReturnTaskID RTaskID;

        public frmImportTask()
        {
            InitializeComponent();
        }

        private void frmImportTask_Load(object sender, EventArgs e)
        {
            int i;

            Task.cTaskClass xmlTClass = new Task.cTaskClass();
            string TaskClass;

            int TClassCount = xmlTClass.GetTaskClassCount();

            //ÿ�ε���������Ҫ�����������ı���������񣬵�combobox�޷�������
            //���Բ������ַ���
            for (i = 0; i < TClassCount; i++)
            {

                TaskClass = xmlTClass.GetTaskClassName(i);
                TaskClass += "                                                                                        ";
                TaskClass += "-" + xmlTClass.GetTaskClassID(i);
                this.comTaskClass.Items.Add(TaskClass);
            }
            xmlTClass = null;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            ReturnValue();
            this.Dispose();
        }

        private void comTaskClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listTask.Items.Clear();

            try
            {

                ListViewItem litem;
                int TaskClassID = 0;

                int Starti = this.comTaskClass.SelectedItem.ToString().IndexOf("-");

                TaskClassID = int.Parse(this.comTaskClass.SelectedItem.ToString().Substring((Starti + 1), (this.comTaskClass.SelectedItem.ToString().Length - Starti - 1)));

                Task.cTaskIndex xmlTasks = new Task.cTaskIndex();
                xmlTasks.GetTaskDataByClass(TaskClassID);

                //��ʼ��ʼ���˷����µ�����
                int count = xmlTasks.GetTaskClassCount();
                this.listTask.Items.Clear();

                for (int i = 0; i < count; i++)
                {
                    litem = new ListViewItem();
                    litem.Name = "S" + xmlTasks.GetTaskID(i);
                    litem.Text = xmlTasks.GetTaskName(i);
                    litem.SubItems.Add(xmlTasks.GetTaskType(i));
                    litem.ImageIndex = 0;
                    this.listTask.Items.Add(litem);
                    litem = null;
                }
                xmlTasks = null;
            }

            catch (System.IO.IOException)
            {
                MessageBox.Show("�����µ������ļ���ʧ���뷵��������ͨ�������ʧ�ļ�����������Զ�����", "ѯ��", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            catch (System.Exception)
            {
                MessageBox.Show("���ص�������������ļ��Ƿ����뷵��������ͨ��������������У���ļ��Ƿ�Ϸ���", "ϵͳ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return ;
            }
        }

        private void listTask_DoubleClick(object sender, EventArgs e)
        {
            ReturnValue();
            this.Dispose ();
        }

        private void ReturnValue()
        {
            if (this.listTask.Items.Count ==0 )
            {
                this.errorProvider1.SetError(this.listTask, "ѡ���������ɸѡ�����������������������ȡ����");
                return;
            }
            int tClassID = int.Parse(this.listTask.SelectedItems[0].Name.Substring(1, this.listTask.SelectedItems[0].Name.Length - 1).ToString());
            string tClassName = this.listTask.SelectedItems[0].Text.ToString();
            RTaskID(tClassID,tClassName );
        }
       
    }
}