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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddGatherUrl));
            this.label47 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.dataNRule = new System.Windows.Forms.DataGridView();
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
            this.groupBox14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataNRule)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label47.Location = new System.Drawing.Point(164, 96);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(235, 13);
            this.label47.TabIndex = 63;
            this.label47.Text = "系统支持多层导航，但只采集最终页数据。";
            // 
            // button6
            // 
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Location = new System.Drawing.Point(529, 93);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(92, 23);
            this.button6.TabIndex = 5;
            this.button6.Text = "测试导航规则";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.dataNRule);
            this.groupBox14.Controls.Add(this.groupBox15);
            this.groupBox14.Controls.Add(this.cmdDelNRule);
            this.groupBox14.Controls.Add(this.cmdAddNRule);
            this.groupBox14.Controls.Add(this.txtNag);
            this.groupBox14.Controls.Add(this.label10);
            this.groupBox14.Controls.Add(this.label22);
            this.groupBox14.Enabled = false;
            this.groupBox14.Location = new System.Drawing.Point(10, 115);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(611, 118);
            this.groupBox14.TabIndex = 62;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "导航规则";
            // 
            // dataNRule
            // 
            this.dataNRule.AllowUserToAddRows = false;
            this.dataNRule.AllowUserToOrderColumns = true;
            this.dataNRule.AllowUserToResizeRows = false;
            this.dataNRule.BackgroundColor = System.Drawing.Color.White;
            this.dataNRule.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataNRule.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataNRule.Location = new System.Drawing.Point(296, 15);
            this.dataNRule.Name = "dataNRule";
            this.dataNRule.RowHeadersVisible = false;
            this.dataNRule.RowHeadersWidth = 20;
            this.dataNRule.RowTemplate.Height = 19;
            this.dataNRule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataNRule.Size = new System.Drawing.Size(305, 93);
            this.dataNRule.TabIndex = 3;
            // 
            // groupBox15
            // 
            this.groupBox15.Location = new System.Drawing.Point(208, 10);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(2, 98);
            this.groupBox15.TabIndex = 29;
            this.groupBox15.TabStop = false;
            // 
            // cmdDelNRule
            // 
            this.cmdDelNRule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdDelNRule.Location = new System.Drawing.Point(226, 70);
            this.cmdDelNRule.Name = "cmdDelNRule";
            this.cmdDelNRule.Size = new System.Drawing.Size(52, 23);
            this.cmdDelNRule.TabIndex = 2;
            this.cmdDelNRule.Text = "移除";
            this.cmdDelNRule.UseVisualStyleBackColor = true;
            this.cmdDelNRule.Click += new System.EventHandler(this.cmdDelNRule_Click);
            // 
            // cmdAddNRule
            // 
            this.cmdAddNRule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdAddNRule.Location = new System.Drawing.Point(226, 36);
            this.cmdAddNRule.Name = "cmdAddNRule";
            this.cmdAddNRule.Size = new System.Drawing.Size(52, 23);
            this.cmdAddNRule.TabIndex = 1;
            this.cmdAddNRule.Text = "添加";
            this.cmdAddNRule.UseVisualStyleBackColor = true;
            this.cmdAddNRule.Click += new System.EventHandler(this.cmdAddNRule_Click);
            // 
            // txtNag
            // 
            this.txtNag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNag.Location = new System.Drawing.Point(13, 40);
            this.txtNag.Name = "txtNag";
            this.txtNag.Size = new System.Drawing.Size(182, 20);
            this.txtNag.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "导航规则：";
            // 
            // label22
            // 
            this.label22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label22.Location = new System.Drawing.Point(10, 67);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(207, 29);
            this.label22.TabIndex = 26;
            this.label22.Text = "直接填写导航网址的前缀（共有部分）即可, 需填写完整";
            // 
            // IsNavigPage
            // 
            this.IsNavigPage.AutoSize = true;
            this.IsNavigPage.Location = new System.Drawing.Point(16, 94);
            this.IsNavigPage.Name = "IsNavigPage";
            this.IsNavigPage.Size = new System.Drawing.Size(140, 17);
            this.IsNavigPage.TabIndex = 4;
            this.IsNavigPage.Text = "导航页(需要导航规则)";
            this.IsNavigPage.UseVisualStyleBackColor = true;
            this.IsNavigPage.CheckedChanged += new System.EventHandler(this.IsNavigPage_CheckedChanged);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label31.Location = new System.Drawing.Point(530, 71);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(91, 13);
            this.label31.TabIndex = 61;
            this.label31.Text = "暂不支持JS跳转";
            // 
            // txtWebLink
            // 
            this.txtWebLink.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWebLink.Location = new System.Drawing.Point(79, 13);
            this.txtWebLink.Multiline = true;
            this.txtWebLink.Name = "txtWebLink";
            this.txtWebLink.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtWebLink.Size = new System.Drawing.Size(454, 48);
            this.txtWebLink.TabIndex = 0;
            this.txtWebLink.Text = "http://";
            // 
            // txtNextPage
            // 
            this.txtNextPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNextPage.Enabled = false;
            this.txtNextPage.Location = new System.Drawing.Point(243, 69);
            this.txtNextPage.Name = "txtNextPage";
            this.txtNextPage.Size = new System.Drawing.Size(273, 20);
            this.txtNextPage.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Enabled = false;
            this.label13.Location = new System.Drawing.Point(167, 71);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(79, 13);
            this.label13.TabIndex = 60;
            this.label13.Text = "下一页标识：";
            // 
            // IsAutoNextPage
            // 
            this.IsAutoNextPage.AutoSize = true;
            this.IsAutoNextPage.Location = new System.Drawing.Point(16, 70);
            this.IsAutoNextPage.Name = "IsAutoNextPage";
            this.IsAutoNextPage.Size = new System.Drawing.Size(158, 17);
            this.IsAutoNextPage.TabIndex = 2;
            this.IsAutoNextPage.Text = "根据下一页标识自动翻页";
            this.IsAutoNextPage.UseVisualStyleBackColor = true;
            this.IsAutoNextPage.CheckedChanged += new System.EventHandler(this.IsAutoNextPage_CheckedChanged);
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(539, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(82, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "参数/变量";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 13);
            this.label9.TabIndex = 51;
            this.label9.Text = "网页地址：";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(0, 260);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(642, 5);
            this.groupBox1.TabIndex = 64;
            this.groupBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(486, 279);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "确 定";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button5
            // 
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.Location = new System.Drawing.Point(561, 279);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(60, 23);
            this.button5.TabIndex = 7;
            this.button5.Text = "取 消";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.contextMenuStrip1.Size = new System.Drawing.Size(210, 176);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // menuNumAdd
            // 
            this.menuNumAdd.Name = "menuNumAdd";
            this.menuNumAdd.Size = new System.Drawing.Size(209, 22);
            this.menuNumAdd.Text = "递增变量{Num:1,100,1}";
            // 
            // menuNumDec
            // 
            this.menuNumDec.Name = "menuNumDec";
            this.menuNumDec.Size = new System.Drawing.Size(209, 22);
            this.menuNumDec.Text = "递减变量{Num:100,1,-1}";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(206, 6);
            // 
            // menuLettAdd
            // 
            this.menuLettAdd.Name = "menuLettAdd";
            this.menuLettAdd.Size = new System.Drawing.Size(209, 22);
            this.menuLettAdd.Text = "字母递增{Letter:a,z}";
            // 
            // menuLettDec
            // 
            this.menuLettDec.Name = "menuLettDec";
            this.menuLettDec.Size = new System.Drawing.Size(209, 22);
            this.menuLettDec.Text = "字母递减{Letter:z,a}";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(206, 6);
            // 
            // pOST前缀POSTToolStripMenuItem
            // 
            this.pOST前缀POSTToolStripMenuItem.Name = "pOST前缀POSTToolStripMenuItem";
            this.pOST前缀POSTToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.pOST前缀POSTToolStripMenuItem.Text = "POST前缀<POST>";
            // 
            // pOST后缀ToolStripMenuItem
            // 
            this.pOST后缀ToolStripMenuItem.Name = "pOST后缀ToolStripMenuItem";
            this.pOST后缀ToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.pOST后缀ToolStripMenuItem.Text = "POST后缀</POST>";
            // 
            // rmenuGetPostData
            // 
            this.rmenuGetPostData.Name = "rmenuGetPostData";
            this.rmenuGetPostData.Size = new System.Drawing.Size(209, 22);
            this.rmenuGetPostData.Text = "手工捕获POST数据";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(206, 6);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(15, 239);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(607, 13);
            this.label1.TabIndex = 68;
            this.label1.Text = "多层导航只采集最后一层网址的数据，同时需要了解，导航层级越多，系统解析的时间就越长网址数量也就越多。";
            // 
            // frmAddGatherUrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 316);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label47);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddGatherUrl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加需要采集的网址信息";
            this.Load += new System.EventHandler(this.frmAddGatherUrl_Load);
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
        private System.Windows.Forms.DataGridView dataNRule;
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
    }
}