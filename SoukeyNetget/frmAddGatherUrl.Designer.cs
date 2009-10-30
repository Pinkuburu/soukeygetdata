namespace SoukeyNetget
{
    partial class frmAddGatherUrl
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddGatherUrl));
            this.label47 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.cmdMoreNRule = new System.Windows.Forms.Button();
            this.dataNRule = new System.Windows.Forms.DataGridView();
            this.nRuleLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nRule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.cmdDelNRule = new System.Windows.Forms.Button();
            this.cmdAddNRule = new System.Windows.Forms.Button();
            this.txtNag = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.IsNavigPage = new System.Windows.Forms.CheckBox();
            this.label31 = new System.Windows.Forms.Label();
            this.txtWebLink = new System.Windows.Forms.TextBox();
            this.txtNextPage = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.IsAutoNextPage = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuNumAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNumDec = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuLettAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLettDec = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.pOST前缀POSTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pOST后缀ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rmenuGetPostData = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataNRule)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label47
            // 
            resources.ApplyResources(this.label47, "label47");
            this.label47.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label47.Name = "label47";
            // 
            // button6
            // 
            resources.ApplyResources(this.button6, "button6");
            this.button6.Name = "button6";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.cmdMoreNRule);
            this.groupBox14.Controls.Add(this.dataNRule);
            this.groupBox14.Controls.Add(this.groupBox15);
            this.groupBox14.Controls.Add(this.cmdDelNRule);
            this.groupBox14.Controls.Add(this.cmdAddNRule);
            this.groupBox14.Controls.Add(this.txtNag);
            this.groupBox14.Controls.Add(this.label10);
            this.groupBox14.Controls.Add(this.label22);
            resources.ApplyResources(this.groupBox14, "groupBox14");
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.TabStop = false;
            // 
            // cmdMoreNRule
            // 
            resources.ApplyResources(this.cmdMoreNRule, "cmdMoreNRule");
            this.cmdMoreNRule.Name = "cmdMoreNRule";
            this.cmdMoreNRule.UseVisualStyleBackColor = true;
            this.cmdMoreNRule.Click += new System.EventHandler(this.cmdMoreNRule_Click);
            // 
            // dataNRule
            // 
            this.dataNRule.AllowUserToAddRows = false;
            this.dataNRule.AllowUserToResizeRows = false;
            this.dataNRule.BackgroundColor = System.Drawing.Color.White;
            this.dataNRule.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            resources.ApplyResources(this.dataNRule, "dataNRule");
            this.dataNRule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataNRule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nRuleLevel,
            this.nRule});
            this.dataNRule.MultiSelect = false;
            this.dataNRule.Name = "dataNRule";
            this.dataNRule.RowHeadersVisible = false;
            this.dataNRule.RowTemplate.Height = 20;
            this.dataNRule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // nRuleLevel
            // 
            resources.ApplyResources(this.nRuleLevel, "nRuleLevel");
            this.nRuleLevel.Name = "nRuleLevel";
            // 
            // nRule
            // 
            resources.ApplyResources(this.nRule, "nRule");
            this.nRule.Name = "nRule";
            // 
            // groupBox15
            // 
            resources.ApplyResources(this.groupBox15, "groupBox15");
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.TabStop = false;
            // 
            // cmdDelNRule
            // 
            resources.ApplyResources(this.cmdDelNRule, "cmdDelNRule");
            this.cmdDelNRule.Name = "cmdDelNRule";
            this.cmdDelNRule.UseVisualStyleBackColor = true;
            this.cmdDelNRule.Click += new System.EventHandler(this.cmdDelNRule_Click);
            // 
            // cmdAddNRule
            // 
            resources.ApplyResources(this.cmdAddNRule, "cmdAddNRule");
            this.cmdAddNRule.Name = "cmdAddNRule";
            this.cmdAddNRule.UseVisualStyleBackColor = true;
            this.cmdAddNRule.Click += new System.EventHandler(this.cmdAddNRule_Click);
            // 
            // txtNag
            // 
            this.txtNag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.txtNag, "txtNag");
            this.txtNag.Name = "txtNag";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label22
            // 
            this.label22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.label22, "label22");
            this.label22.Name = "label22";
            // 
            // IsNavigPage
            // 
            resources.ApplyResources(this.IsNavigPage, "IsNavigPage");
            this.IsNavigPage.Name = "IsNavigPage";
            this.IsNavigPage.UseVisualStyleBackColor = true;
            this.IsNavigPage.CheckedChanged += new System.EventHandler(this.IsNavigPage_CheckedChanged);
            // 
            // label31
            // 
            resources.ApplyResources(this.label31, "label31");
            this.label31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label31.Name = "label31";
            // 
            // txtWebLink
            // 
            this.txtWebLink.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.txtWebLink, "txtWebLink");
            this.txtWebLink.Name = "txtWebLink";
            // 
            // txtNextPage
            // 
            this.txtNextPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.txtNextPage, "txtNextPage");
            this.txtNextPage.Name = "txtNextPage";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // IsAutoNextPage
            // 
            resources.ApplyResources(this.IsAutoNextPage, "IsAutoNextPage");
            this.IsAutoNextPage.Name = "IsAutoNextPage";
            this.IsAutoNextPage.UseVisualStyleBackColor = true;
            this.IsAutoNextPage.CheckedChanged += new System.EventHandler(this.IsAutoNextPage_CheckedChanged);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button5
            // 
            resources.ApplyResources(this.button5, "button5");
            this.button5.Name = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNumAdd,
            this.menuNumDec,
            this.toolStripSeparator1,
            this.menuLettAdd,
            this.menuLettDec,
            this.toolStripSeparator2,
            this.pOST前缀POSTToolStripMenuItem,
            this.pOST后缀ToolStripMenuItem,
            this.rmenuGetPostData,
            this.toolStripSeparator4});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // menuNumAdd
            // 
            this.menuNumAdd.Name = "menuNumAdd";
            resources.ApplyResources(this.menuNumAdd, "menuNumAdd");
            // 
            // menuNumDec
            // 
            this.menuNumDec.Name = "menuNumDec";
            resources.ApplyResources(this.menuNumDec, "menuNumDec");
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // menuLettAdd
            // 
            this.menuLettAdd.Name = "menuLettAdd";
            resources.ApplyResources(this.menuLettAdd, "menuLettAdd");
            // 
            // menuLettDec
            // 
            this.menuLettDec.Name = "menuLettDec";
            resources.ApplyResources(this.menuLettDec, "menuLettDec");
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // pOST前缀POSTToolStripMenuItem
            // 
            this.pOST前缀POSTToolStripMenuItem.Name = "pOST前缀POSTToolStripMenuItem";
            resources.ApplyResources(this.pOST前缀POSTToolStripMenuItem, "pOST前缀POSTToolStripMenuItem");
            // 
            // pOST后缀ToolStripMenuItem
            // 
            this.pOST后缀ToolStripMenuItem.Name = "pOST后缀ToolStripMenuItem";
            resources.ApplyResources(this.pOST后缀ToolStripMenuItem, "pOST后缀ToolStripMenuItem");
            // 
            // rmenuGetPostData
            // 
            this.rmenuGetPostData.Name = "rmenuGetPostData";
            resources.ApplyResources(this.rmenuGetPostData, "rmenuGetPostData");
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Name = "label1";
            // 
            // dataGridViewTextBoxColumn1
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // frmAddGatherUrl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.groupBox14);
            this.Controls.Add(this.IsNavigPage);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.txtWebLink);
            this.Controls.Add(this.txtNextPage);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.IsAutoNextPage);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label47);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddGatherUrl";
            this.Load += new System.EventHandler(this.frmAddGatherUrl_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmAddGatherUrl_FormClosed);
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataNRule)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.Button cmdDelNRule;
        private System.Windows.Forms.Button cmdAddNRule;
        private System.Windows.Forms.TextBox txtNag;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.CheckBox IsNavigPage;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox txtWebLink;
        private System.Windows.Forms.TextBox txtNextPage;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox IsAutoNextPage;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuNumAdd;
        private System.Windows.Forms.ToolStripMenuItem menuNumDec;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuLettAdd;
        private System.Windows.Forms.ToolStripMenuItem menuLettDec;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem pOST前缀POSTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pOST后缀ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rmenuGetPostData;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataNRule;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Button cmdMoreNRule;
        private System.Windows.Forms.DataGridViewTextBoxColumn nRuleLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn nRule;
    }
}