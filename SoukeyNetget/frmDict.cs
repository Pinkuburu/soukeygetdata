using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SoukeyNetget
{
    public partial class frmDict : Form
    {

        cDict d ;
        
        public frmDict()
        {
            InitializeComponent();

        }

        private void IniData()
        {
            DataView dClass = new DataView();
            int i = 0;
            d = new cDict();

            this.comDictClass.Items.Clear();

            dClass = d.GetDictClass();

            for (i = 0; i < dClass.Count; i++)
            {
                this.comDictClass.Items.Add(dClass[i].Row["Name"].ToString());
            }
            
            dClass = null;

        }

        private void comDictClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dName;
            ListViewItem item;
            int i = 0;

            this.listDict.Items.Clear();
            dName = d.GetDict(this.comDictClass.SelectedItem.ToString ());

            if (dName == null)
            {
                return;
            }
            
            for (i = 0; i < dName.Count; i++)
            {
                item = new ListViewItem();
                item.Text =dName[i].Row ["Name"].ToString ();
                this.listDict.Items.Add (item);
            }

            dName = null;

        }

        private void frmDict_Load(object sender, EventArgs e)
        {
            IniData();
        }

        private void frmDict_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            d = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.txtDictClassName.Text.Trim().ToString() == null || this.txtDictClassName.Text.Trim().ToString() == "")
            {
                this.errorProvider1.SetError(this.txtDictClassName, "需要填写字典分类的名称");
                return;
            }

            d.AddDictClass(this.txtDictClassName.Text.ToString());

            this.comDictClass.Items.Clear();
            this.listDict.Items.Clear();

            this.txtDictClassName.Text = "";

            //设置d为空，并重新调用加载
            d = null;
            
            IniData();

        }

        private void menuAddDict_Click(object sender, EventArgs e)
        {
            if (this.comDictClass.SelectedItem == null || this.comDictClass.SelectedItem.ToString() == "")
            {
                this.errorProvider1.SetError(this.comDictClass, "请先选择字典分类再进行字典内容的添加");
                return;
            }
            d.AddDict(this.comDictClass.SelectedItem.ToString (), "新建字典内容");

            ListViewItem item=new ListViewItem() ;
            item.Text = "新建字典内容";
            this.listDict.Items.Add(item);
            item =null;
            this.listDict.Items[this.listDict.Items.Count - 1].BeginEdit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        //修改字典内容
        private void listDict_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label.ToString() == null)
            {
                //表示没有进行修改
                return;
            }
            d.EditDict(this.comDictClass.SelectedItem.ToString (), this.listDict.SelectedItems[0].Text.ToString(), e.Label.ToString());
        }

        private void menuDelDict_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确认删除此字典内容么？如果删除则有可能导致采集任务中的字典规则失效，是否继续？", "删除询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes )
            {
                d.DelDict(this.comDictClass.SelectedItem.ToString(), this.listDict.SelectedItems[0].Text.ToString());
                this.listDict.Items.Remove(this.listDict.SelectedItems[0]);

            }
        }

        private void menuDelDictClass_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确认删除此字典分类么？如果删除则有可能导致采集任务中的字典规则失效，是否继续？", "删除询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                d.DelDictClass(this.comDictClass.SelectedItem.ToString());
                IniData();
            }
        }

        private void listDict_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode  ==Keys.Delete )
            {
                if (MessageBox.Show("您确认删除此字典内容么？如果删除则有可能导致采集任务中的字典规则失效，是否继续？", "删除询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    d.DelDict(this.comDictClass.SelectedItem.ToString(), this.listDict.SelectedItems[0].Text.ToString());
                    this.listDict.Items.Remove(this.listDict.SelectedItems[0]);

                }
            }
        }

        private void listDict_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       
    }
}