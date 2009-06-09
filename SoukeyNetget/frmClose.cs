using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SoukeyNetget
{
    public partial class frmClose : Form
    {
        public delegate void ReturnExitPara(cGlobalParas.ExitPara ePara);
        public ReturnExitPara RExitPara;

        public frmClose()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            cGlobalParas.ExitPara ePara = cGlobalParas.ExitPara.MinForm;

            if (this.raMin.Checked ==true )
            {
                ePara = cGlobalParas.ExitPara.MinForm;
            }
            else if (this.raExit.Checked == true)
            {
                ePara = cGlobalParas.ExitPara.Exit;
            }
            RExitPara(ePara);
            this.Close();
        }

        //保存配置信息
        private void SaveConfigData()
        {
            try
            {
                cXmlSConfig Config = new cXmlSConfig();
                if (this.raMin.Checked == true)
                    Config.ExitSelected = 0;
                else
                    Config.ExitSelected = 1;
                if (this.checkBox1.Checked == true)
                    Config.ExitIsShow = false;
                else
                    Config.ExitIsShow = true;
                Config = null;
            }
            catch (System.Exception)
            {
                MessageBox.Show("系统配置文件加载失败，可从安装文件中拷贝文件：SoukeyConfig.xml 到Soukey采摘安装目录，配置文件损坏并不影响系统运行，但您做的系统配置可能无法保存！", "Soukey信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmClose_Load(object sender, EventArgs e)
        {
            try
            {
                cXmlSConfig Config = new cXmlSConfig();
                if (Config.ExitSelected == 0)
                    this.raMin.Checked = true;
                else
                    this.raExit.Checked = true;

                if (Config.ExitIsShow == true)
                    this.checkBox1.Checked = false;
                else
                    this.checkBox1.Checked = true;

                Config = null;
            }
            catch (System.Exception)
            {
                MessageBox.Show("系统配置文件加载失败，可从安装文件中拷贝文件：SoukeyConfig.xml 到Soukey采摘安装目录，配置文件损坏并不影响系统运行，但您做的系统配置可能无法保存！", "Soukey信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            SaveConfigData();
        }

        private void raExit_CheckedChanged(object sender, EventArgs e)
        {
            if (this.raExit.Checked == true)
            {
                SaveConfigData();
            }
        }

        private void raMin_CheckedChanged(object sender, EventArgs e)
        {
            if (this.raMin.Checked == true)
            {
                SaveConfigData();
            }
        }
    }
}