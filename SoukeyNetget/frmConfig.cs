using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SoukeyNetget
{
    public partial class frmConfig : Form
    {
        public frmConfig()
        {
            InitializeComponent();
        }

        private void treeMenu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (e.Node.Name)
            {
                case "nodNormal":
                    this.panel1.Visible =true ;
                    this.panel2.Visible =false ;
                    break;

                case "nodExit":
                    this.panel1.Visible =false ;
                    this.panel2.Visible =true ;
                    break;
                default :
                    break;
            }
        }

        //保存配置信息
        private void SaveConfigData()
        {
            try
            {
                cXmlSConfig Config = new cXmlSConfig();
                Config.IsInstantSave = false;

                if (this.raMin.Checked == true)
                    Config.ExitSelected = 0;
                else
                    Config.ExitSelected = 1;

                if (this.checkBox1.Checked == true)
                    Config.ExitIsShow = true;
                else
                    Config.ExitIsShow = false;

                if (this.IsAutoSystemLog.Checked == true)
                    Config.AutoSaveLog = true;
                else
                    Config.AutoSaveLog = false;

                Config.Save();

                Config = null;

                this.IsSave.Text = "false";
            }
            catch (System.Exception)
            {
                MessageBox.Show("系统配置文件加载失败，可从安装文件中拷贝文件：SoukeyConfig.xml 到Soukey采摘安装目录！", "Soukey信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            this.txtLogPath.Text = Program.getPrjPath() + "log";

            try
            {
                cXmlSConfig Config = new cXmlSConfig();

                if (Config.ExitSelected == 0)
                    this.raMin.Checked = true;
                else
                    this.raExit.Checked = true;

                if (Config.ExitIsShow == true)
                    this.checkBox1.Checked = true;
                else
                    this.checkBox1.Checked = false;

                this.IsAutoSystemLog.Checked = Config.AutoSaveLog;
                
                Config = null;

                this.cmdApply.Enabled = false;
                this.IsSave.Text = "false";

            }
            catch (System.Exception)
            {
                MessageBox.Show("系统配置文件加载失败，可从安装文件中拷贝文件：SoukeyConfig.xml 到Soukey采摘安装目录，配置文件损坏并不影响系统运行，但您做的系统配置可能无法保存！", "Soukey信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void IsSave_TextChanged(object sender, EventArgs e)
        {
            if (this.IsSave.Text == "true" )
            {
                this.cmdApply.Enabled = true;
            }
            else if (this.IsSave.Text == "false")
            {
                this.cmdApply.Enabled = false;
            }
        }

        private void IsAutoSystemLog_CheckedChanged(object sender, EventArgs e)
        {
            this.IsSave.Text = "true";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.IsSave.Text = "true";
        }

        private void raMin_CheckedChanged(object sender, EventArgs e)
        {
            this.IsSave.Text = "true";
        }

        private void raExit_CheckedChanged(object sender, EventArgs e)
        {
            this.IsSave.Text = "true";
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            SaveConfigData();
            this.cmdApply.Enabled = false;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (this.cmdApply.Enabled == true)
                SaveConfigData();

            this.Close();
        }

      
    }
}