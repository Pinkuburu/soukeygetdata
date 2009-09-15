namespace SoukeyNetget
{
    partial class frmAddPlanTask
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddPlanTask));
            this.raSoukeyTask = new System.Windows.Forms.RadioButton();
            this.raOtherTask = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listTask = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.comTaskClass = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.comTableName = new System.Windows.Forms.ComboBox();
            this.button12 = new System.Windows.Forms.Button();
            this.txtDataSource = new System.Windows.Forms.TextBox();
            this.raMySqlTask = new System.Windows.Forms.RadioButton();
            this.raMSSQLTask = new System.Windows.Forms.RadioButton();
            this.raAccessTask = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtPara = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.raDataTask = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // raSoukeyTask
            // 
            this.raSoukeyTask.AutoSize = true;
            this.raSoukeyTask.Checked = true;
            this.raSoukeyTask.Location = new System.Drawing.Point(14, 12);
            this.raSoukeyTask.Name = "raSoukeyTask";
            this.raSoukeyTask.Size = new System.Drawing.Size(109, 17);
            this.raSoukeyTask.TabIndex = 0;
            this.raSoukeyTask.TabStop = true;
            this.raSoukeyTask.Text = "Soukey采摘任务";
            this.raSoukeyTask.UseVisualStyleBackColor = true;
            this.raSoukeyTask.CheckedChanged += new System.EventHandler(this.raSoukeyTask_CheckedChanged);
            // 
            // raOtherTask
            // 
            this.raOtherTask.AutoSize = true;
            this.raOtherTask.Location = new System.Drawing.Point(252, 12);
            this.raOtherTask.Name = "raOtherTask";
            this.raOtherTask.Size = new System.Drawing.Size(73, 17);
            this.raOtherTask.TabIndex = 1;
            this.raOtherTask.Text = "其他任务";
            this.raOtherTask.UseVisualStyleBackColor = true;
            this.raOtherTask.CheckedChanged += new System.EventHandler(this.raOtherTask_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listTask);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comTaskClass);
            this.panel1.Location = new System.Drawing.Point(6, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(443, 177);
            this.panel1.TabIndex = 2;
            // 
            // listTask
            // 
            this.listTask.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listTask.FullRowSelect = true;
            this.listTask.Location = new System.Drawing.Point(15, 37);
            this.listTask.MultiSelect = false;
            this.listTask.Name = "listTask";
            this.listTask.Size = new System.Drawing.Size(412, 132);
            this.listTask.TabIndex = 11;
            this.listTask.UseCompatibleStateImageBehavior = false;
            this.listTask.View = System.Windows.Forms.View.Details;
            this.listTask.DoubleClick += new System.EventHandler(this.listTask_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "任务名称";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "所属分类";
            this.columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "任务类型";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "执行类型";
            this.columnHeader4.Width = 120;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "任务分类：";
            // 
            // comTaskClass
            // 
            this.comTaskClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comTaskClass.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comTaskClass.FormattingEnabled = true;
            this.comTaskClass.Location = new System.Drawing.Point(83, 7);
            this.comTaskClass.Name = "comTaskClass";
            this.comTaskClass.Size = new System.Drawing.Size(344, 21);
            this.comTaskClass.TabIndex = 9;
            this.comTaskClass.SelectedIndexChanged += new System.EventHandler(this.comTaskClass_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Location = new System.Drawing.Point(14, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(455, 202);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Soukey采摘任务";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.comTableName);
            this.panel3.Controls.Add(this.button12);
            this.panel3.Controls.Add(this.txtDataSource);
            this.panel3.Controls.Add(this.raMySqlTask);
            this.panel3.Controls.Add(this.raMSSQLTask);
            this.panel3.Controls.Add(this.raAccessTask);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Location = new System.Drawing.Point(6, 20);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(443, 177);
            this.panel3.TabIndex = 4;
            this.panel3.Visible = false;
            // 
            // comTableName
            // 
            this.comTableName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comTableName.FormattingEnabled = true;
            this.comTableName.Location = new System.Drawing.Point(66, 137);
            this.comTableName.Name = "comTableName";
            this.comTableName.Size = new System.Drawing.Size(309, 21);
            this.comTableName.TabIndex = 55;
            this.comTableName.DropDown += new System.EventHandler(this.comTableName_DropDown);
            // 
            // button12
            // 
            this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button12.Location = new System.Drawing.Point(374, 46);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(60, 23);
            this.button12.TabIndex = 54;
            this.button12.Text = "设 置";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // txtDataSource
            // 
            this.txtDataSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDataSource.Location = new System.Drawing.Point(66, 46);
            this.txtDataSource.Multiline = true;
            this.txtDataSource.Name = "txtDataSource";
            this.txtDataSource.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDataSource.Size = new System.Drawing.Size(309, 55);
            this.txtDataSource.TabIndex = 53;
            // 
            // raMySqlTask
            // 
            this.raMySqlTask.AutoSize = true;
            this.raMySqlTask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.raMySqlTask.Location = new System.Drawing.Point(240, 11);
            this.raMySqlTask.Name = "raMySqlTask";
            this.raMySqlTask.Size = new System.Drawing.Size(89, 17);
            this.raMySqlTask.TabIndex = 52;
            this.raMySqlTask.Text = "MySql数据库";
            this.raMySqlTask.UseVisualStyleBackColor = true;
            // 
            // raMSSQLTask
            // 
            this.raMSSQLTask.AutoSize = true;
            this.raMSSQLTask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.raMSSQLTask.Location = new System.Drawing.Point(109, 11);
            this.raMSSQLTask.Name = "raMSSQLTask";
            this.raMSSQLTask.Size = new System.Drawing.Size(125, 17);
            this.raMSSQLTask.TabIndex = 51;
            this.raMSSQLTask.Text = "MS SqlServer数据库";
            this.raMSSQLTask.UseVisualStyleBackColor = true;
            // 
            // raAccessTask
            // 
            this.raAccessTask.AutoSize = true;
            this.raAccessTask.Checked = true;
            this.raAccessTask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.raAccessTask.Location = new System.Drawing.Point(8, 11);
            this.raAccessTask.Name = "raAccessTask";
            this.raAccessTask.Size = new System.Drawing.Size(95, 17);
            this.raAccessTask.TabIndex = 50;
            this.raAccessTask.TabStop = true;
            this.raAccessTask.Text = "Access数据库";
            this.raAccessTask.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 112);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 13);
            this.label8.TabIndex = 49;
            this.label8.Text = "查询或存储过程：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 48;
            this.label6.Text = "数据库：";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtPara);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.txtFileName);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(6, 20);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(443, 177);
            this.panel2.TabIndex = 3;
            this.panel2.Visible = false;
            // 
            // txtPara
            // 
            this.txtPara.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPara.Location = new System.Drawing.Point(61, 43);
            this.txtPara.Multiline = true;
            this.txtPara.Name = "txtPara";
            this.txtPara.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPara.Size = new System.Drawing.Size(326, 67);
            this.txtPara.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "参数：";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(369, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(62, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "浏览...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFileName.Location = new System.Drawing.Point(61, 12);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(309, 20);
            this.txtFileName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "程序：";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(0, 243);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(501, 5);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // cmdCancel
            // 
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCancel.Image = ((System.Drawing.Image)(resources.GetObject("cmdCancel.Image")));
            this.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdCancel.Location = new System.Drawing.Point(394, 254);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(60, 23);
            this.cmdCancel.TabIndex = 9;
            this.cmdCancel.Text = "取 消";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdOK.Image = ((System.Drawing.Image)(resources.GetObject("cmdOK.Image")));
            this.cmdOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdOK.Location = new System.Drawing.Point(321, 254);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(60, 23);
            this.cmdOK.TabIndex = 8;
            this.cmdOK.Text = "确 定";
            this.cmdOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // raDataTask
            // 
            this.raDataTask.AutoSize = true;
            this.raDataTask.Location = new System.Drawing.Point(143, 12);
            this.raDataTask.Name = "raDataTask";
            this.raDataTask.Size = new System.Drawing.Size(85, 17);
            this.raDataTask.TabIndex = 10;
            this.raDataTask.TabStop = true;
            this.raDataTask.Text = "数据库任务";
            this.raDataTask.UseVisualStyleBackColor = true;
            this.raDataTask.CheckedChanged += new System.EventHandler(this.raDataTask_CheckedChanged);
            // 
            // frmAddPlanTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 286);
            this.Controls.Add(this.raDataTask);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.raOtherTask);
            this.Controls.Add(this.raSoukeyTask);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddPlanTask";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加计划任务";
            this.Load += new System.EventHandler(this.frmAddPlanTask_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton raSoukeyTask;
        private System.Windows.Forms.RadioButton raOtherTask;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView listTask;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comTaskClass;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPara;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RadioButton raDataTask;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox comTableName;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.TextBox txtDataSource;
        private System.Windows.Forms.RadioButton raMySqlTask;
        private System.Windows.Forms.RadioButton raMSSQLTask;
        private System.Windows.Forms.RadioButton raAccessTask;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
    }
}