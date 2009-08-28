namespace SoukeyNetget
{
    partial class frmTaskPlan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTaskPlan));
            this.label1 = new System.Windows.Forms.Label();
            this.txtPlanName = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtPlanState = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.DisabledDateTime = new System.Windows.Forms.DateTimePicker();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.DisabledRunNum = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.raDateTime = new System.Windows.Forms.RadioButton();
            this.raNumber = new System.Windows.Forms.RadioButton();
            this.cboxIsDisabled = new System.Windows.Forms.CheckBox();
            this.cboxIsRun = new System.Windows.Forms.CheckBox();
            this.txtPlanDemo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdDelTask = new System.Windows.Forms.Button();
            this.cmdAddTask = new System.Windows.Forms.Button();
            this.listTask = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.label13 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.EnabledDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.raOneTime = new System.Windows.Forms.RadioButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.raDay = new System.Windows.Forms.RadioButton();
            this.raWeekly = new System.Windows.Forms.RadioButton();
            this.PanelOne = new System.Windows.Forms.Panel();
            this.RunOnceTime = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.PanelWeekly = new System.Windows.Forms.Panel();
            this.cboxThursday = new System.Windows.Forms.CheckBox();
            this.cboxWednesday = new System.Windows.Forms.CheckBox();
            this.cboxSturday = new System.Windows.Forms.CheckBox();
            this.cboxFriday = new System.Windows.Forms.CheckBox();
            this.cboxTuesday = new System.Windows.Forms.CheckBox();
            this.cboxMonday = new System.Windows.Forms.CheckBox();
            this.cboxSunday = new System.Windows.Forms.CheckBox();
            this.RunWeeklyTime = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.PanelDay = new System.Windows.Forms.Panel();
            this.RunDayTwice1Time = new System.Windows.Forms.DateTimePicker();
            this.RunDayOnesTime = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.raRuntwice = new System.Windows.Forms.RadioButton();
            this.raRunones = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.RunDayTwice2Time = new System.Windows.Forms.DateTimePicker();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.IsSave = new System.Windows.Forms.TextBox();
            this.cmdApply = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtPlanID = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DisabledRunNum)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.PanelOne.SuspendLayout();
            this.PanelWeekly.SuspendLayout();
            this.PanelDay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "计划名称：";
            // 
            // txtPlanName
            // 
            this.txtPlanName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPlanName.Location = new System.Drawing.Point(82, 12);
            this.txtPlanName.Name = "txtPlanName";
            this.txtPlanName.Size = new System.Drawing.Size(411, 20);
            this.txtPlanName.TabIndex = 1;
            this.txtPlanName.Tag = "";
            this.txtPlanName.TextChanged += new System.EventHandler(this.txtPlanName_TextChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(12, 38);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(491, 300);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.ImageIndex = 2;
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(483, 270);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "常规设置";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.txtPlanState);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.raDateTime);
            this.groupBox3.Controls.Add(this.raNumber);
            this.groupBox3.Controls.Add(this.cboxIsDisabled);
            this.groupBox3.Controls.Add(this.cboxIsRun);
            this.groupBox3.Controls.Add(this.txtPlanDemo);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(1, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(476, 261);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "常规设置";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label15.Location = new System.Drawing.Point(176, 19);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(247, 13);
            this.label15.TabIndex = 13;
            this.label15.Text = "无法修改，由系统根据计划运行情况自动维护";
            // 
            // txtPlanState
            // 
            this.txtPlanState.BackColor = System.Drawing.Color.White;
            this.txtPlanState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPlanState.Location = new System.Drawing.Point(65, 15);
            this.txtPlanState.Name = "txtPlanState";
            this.txtPlanState.ReadOnly = true;
            this.txtPlanState.Size = new System.Drawing.Size(105, 20);
            this.txtPlanState.TabIndex = 12;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(16, 19);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 13);
            this.label14.TabIndex = 11;
            this.label14.Text = "状态：";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.DisabledDateTime);
            this.groupBox5.Enabled = false;
            this.groupBox5.Location = new System.Drawing.Point(19, 197);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(440, 58);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "运行终止时间过期";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "过期时间";
            // 
            // DisabledDateTime
            // 
            this.DisabledDateTime.CustomFormat = "yyyy-MM-dd ddd HH:mm";
            this.DisabledDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DisabledDateTime.Location = new System.Drawing.Point(90, 24);
            this.DisabledDateTime.Name = "DisabledDateTime";
            this.DisabledDateTime.ShowUpDown = true;
            this.DisabledDateTime.Size = new System.Drawing.Size(176, 20);
            this.DisabledDateTime.TabIndex = 8;
            this.DisabledDateTime.ValueChanged += new System.EventHandler(this.DisabledDateTime_ValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.DisabledRunNum);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Enabled = false;
            this.groupBox4.Location = new System.Drawing.Point(19, 135);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(440, 56);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "运行次数过期";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(148, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "次后，任务过期";
            // 
            // DisabledRunNum
            // 
            this.DisabledRunNum.Location = new System.Drawing.Point(61, 23);
            this.DisabledRunNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DisabledRunNum.Name = "DisabledRunNum";
            this.DisabledRunNum.Size = new System.Drawing.Size(81, 20);
            this.DisabledRunNum.TabIndex = 7;
            this.DisabledRunNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DisabledRunNum.ValueChanged += new System.EventHandler(this.DisabledRunNum_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "执行";
            // 
            // raDateTime
            // 
            this.raDateTime.AutoSize = true;
            this.raDateTime.Enabled = false;
            this.raDateTime.Location = new System.Drawing.Point(197, 106);
            this.raDateTime.Name = "raDateTime";
            this.raDateTime.Size = new System.Drawing.Size(61, 17);
            this.raDateTime.TabIndex = 5;
            this.raDateTime.TabStop = true;
            this.raDateTime.Text = "按时间";
            this.raDateTime.UseVisualStyleBackColor = true;
            this.raDateTime.CheckedChanged += new System.EventHandler(this.raDateTime_CheckedChanged);
            // 
            // raNumber
            // 
            this.raNumber.AutoSize = true;
            this.raNumber.Checked = true;
            this.raNumber.Enabled = false;
            this.raNumber.Location = new System.Drawing.Point(109, 105);
            this.raNumber.Name = "raNumber";
            this.raNumber.Size = new System.Drawing.Size(61, 17);
            this.raNumber.TabIndex = 4;
            this.raNumber.TabStop = true;
            this.raNumber.Text = "按次数";
            this.raNumber.UseVisualStyleBackColor = true;
            this.raNumber.CheckedChanged += new System.EventHandler(this.raNumber_CheckedChanged);
            // 
            // cboxIsDisabled
            // 
            this.cboxIsDisabled.AutoSize = true;
            this.cboxIsDisabled.Location = new System.Drawing.Point(19, 106);
            this.cboxIsDisabled.Name = "cboxIsDisabled";
            this.cboxIsDisabled.Size = new System.Drawing.Size(74, 17);
            this.cboxIsDisabled.TabIndex = 3;
            this.cboxIsDisabled.Text = "是否过期";
            this.cboxIsDisabled.UseVisualStyleBackColor = true;
            this.cboxIsDisabled.CheckedChanged += new System.EventHandler(this.cboxIsDisabled_CheckedChanged);
            // 
            // cboxIsRun
            // 
            this.cboxIsRun.AutoSize = true;
            this.cboxIsRun.Location = new System.Drawing.Point(264, 107);
            this.cboxIsRun.Name = "cboxIsRun";
            this.cboxIsRun.Size = new System.Drawing.Size(266, 17);
            this.cboxIsRun.TabIndex = 2;
            this.cboxIsRun.Text = "如果过了计划开始操作时间，则立即启动任务";
            this.cboxIsRun.UseVisualStyleBackColor = true;
            this.cboxIsRun.Visible = false;
            this.cboxIsRun.CheckedChanged += new System.EventHandler(this.cboxIsRun_CheckedChanged);
            // 
            // txtPlanDemo
            // 
            this.txtPlanDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPlanDemo.Location = new System.Drawing.Point(65, 41);
            this.txtPlanDemo.Multiline = true;
            this.txtPlanDemo.Name = "txtPlanDemo";
            this.txtPlanDemo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPlanDemo.Size = new System.Drawing.Size(394, 55);
            this.txtPlanDemo.TabIndex = 1;
            this.txtPlanDemo.TextChanged += new System.EventHandler(this.txtPlanDemo_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "备注：";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.ImageIndex = 0;
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(483, 270);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "任务信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmdDelTask);
            this.groupBox2.Controls.Add(this.cmdAddTask);
            this.groupBox2.Controls.Add(this.listTask);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Location = new System.Drawing.Point(1, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(476, 261);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "任务信息";
            // 
            // cmdDelTask
            // 
            this.cmdDelTask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdDelTask.Location = new System.Drawing.Point(399, 199);
            this.cmdDelTask.Name = "cmdDelTask";
            this.cmdDelTask.Size = new System.Drawing.Size(71, 23);
            this.cmdDelTask.TabIndex = 2;
            this.cmdDelTask.Text = "删  除";
            this.cmdDelTask.UseVisualStyleBackColor = true;
            this.cmdDelTask.Click += new System.EventHandler(this.cmdDelTask_Click);
            // 
            // cmdAddTask
            // 
            this.cmdAddTask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdAddTask.Location = new System.Drawing.Point(312, 199);
            this.cmdAddTask.Name = "cmdAddTask";
            this.cmdAddTask.Size = new System.Drawing.Size(71, 23);
            this.cmdAddTask.TabIndex = 1;
            this.cmdAddTask.Text = "添  加";
            this.cmdAddTask.UseVisualStyleBackColor = true;
            this.cmdAddTask.Click += new System.EventHandler(this.cmdAddTask_Click);
            // 
            // listTask
            // 
            this.listTask.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listTask.FullRowSelect = true;
            this.listTask.Location = new System.Drawing.Point(6, 19);
            this.listTask.Name = "listTask";
            this.listTask.Size = new System.Drawing.Size(464, 174);
            this.listTask.TabIndex = 0;
            this.listTask.UseCompatibleStateImageBehavior = false;
            this.listTask.View = System.Windows.Forms.View.Details;
            this.listTask.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listTask_KeyDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "任务类型";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "任务";
            this.columnHeader2.Width = 180;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "参数";
            this.columnHeader3.Width = 120;
            // 
            // label13
            // 
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label13.Location = new System.Drawing.Point(3, 228);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(467, 30);
            this.label13.TabIndex = 3;
            this.label13.Text = "系统支持两种自动运行任务：Soukey采摘任务和用户自定义任务。但需要注意：如果计划执行的任务被删除，则计划执行会失败！";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox7);
            this.tabPage2.ImageIndex = 1;
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(483, 270);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "执行计划";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label12);
            this.groupBox7.Controls.Add(this.EnabledDate);
            this.groupBox7.Controls.Add(this.label5);
            this.groupBox7.Controls.Add(this.raOneTime);
            this.groupBox7.Controls.Add(this.groupBox6);
            this.groupBox7.Controls.Add(this.raDay);
            this.groupBox7.Controls.Add(this.raWeekly);
            this.groupBox7.Controls.Add(this.PanelDay);
            this.groupBox7.Controls.Add(this.PanelOne);
            this.groupBox7.Controls.Add(this.PanelWeekly);
            this.groupBox7.Location = new System.Drawing.Point(1, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(476, 261);
            this.groupBox7.TabIndex = 5;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "执行计划";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label12.Location = new System.Drawing.Point(7, 193);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(453, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "如果选择“运行一次”，则系统会自动修改任务过期参数为：任务运行1次后任务过期。";
            // 
            // EnabledDate
            // 
            this.EnabledDate.CustomFormat = "yyyy-MM-dd";
            this.EnabledDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EnabledDate.Location = new System.Drawing.Point(196, 28);
            this.EnabledDate.Name = "EnabledDate";
            this.EnabledDate.Size = new System.Drawing.Size(130, 20);
            this.EnabledDate.TabIndex = 9;
            this.EnabledDate.ValueChanged += new System.EventHandler(this.EnabledDate_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(123, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "生效日期：";
            // 
            // raOneTime
            // 
            this.raOneTime.AutoSize = true;
            this.raOneTime.Location = new System.Drawing.Point(10, 24);
            this.raOneTime.Name = "raOneTime";
            this.raOneTime.Size = new System.Drawing.Size(73, 17);
            this.raOneTime.TabIndex = 0;
            this.raOneTime.Text = "执行一次";
            this.raOneTime.UseVisualStyleBackColor = true;
            this.raOneTime.CheckedChanged += new System.EventHandler(this.raOneTime_CheckedChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Location = new System.Drawing.Point(96, 15);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(2, 168);
            this.groupBox6.TabIndex = 4;
            this.groupBox6.TabStop = false;
            // 
            // raDay
            // 
            this.raDay.AutoSize = true;
            this.raDay.Checked = true;
            this.raDay.Location = new System.Drawing.Point(10, 55);
            this.raDay.Name = "raDay";
            this.raDay.Size = new System.Drawing.Size(73, 17);
            this.raDay.TabIndex = 1;
            this.raDay.TabStop = true;
            this.raDay.Text = "每天执行";
            this.raDay.UseVisualStyleBackColor = true;
            this.raDay.CheckedChanged += new System.EventHandler(this.raDay_CheckedChanged);
            // 
            // raWeekly
            // 
            this.raWeekly.AutoSize = true;
            this.raWeekly.Location = new System.Drawing.Point(10, 86);
            this.raWeekly.Name = "raWeekly";
            this.raWeekly.Size = new System.Drawing.Size(73, 17);
            this.raWeekly.TabIndex = 2;
            this.raWeekly.Text = "每周执行";
            this.raWeekly.UseVisualStyleBackColor = true;
            this.raWeekly.CheckedChanged += new System.EventHandler(this.raWeekly_CheckedChanged);
            // 
            // PanelOne
            // 
            this.PanelOne.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelOne.Controls.Add(this.RunOnceTime);
            this.PanelOne.Controls.Add(this.label6);
            this.PanelOne.Location = new System.Drawing.Point(114, 58);
            this.PanelOne.Name = "PanelOne";
            this.PanelOne.Size = new System.Drawing.Size(348, 121);
            this.PanelOne.TabIndex = 10;
            this.PanelOne.Visible = false;
            // 
            // RunOnceTime
            // 
            this.RunOnceTime.CustomFormat = "yyyy-MM-dd ddd HH:mm";
            this.RunOnceTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.RunOnceTime.Location = new System.Drawing.Point(81, 9);
            this.RunOnceTime.Name = "RunOnceTime";
            this.RunOnceTime.ShowUpDown = true;
            this.RunOnceTime.Size = new System.Drawing.Size(182, 20);
            this.RunOnceTime.TabIndex = 1;
            this.RunOnceTime.ValueChanged += new System.EventHandler(this.RunOnceTime_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "执行时间：";
            // 
            // PanelWeekly
            // 
            this.PanelWeekly.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelWeekly.Controls.Add(this.cboxThursday);
            this.PanelWeekly.Controls.Add(this.cboxWednesday);
            this.PanelWeekly.Controls.Add(this.cboxSturday);
            this.PanelWeekly.Controls.Add(this.cboxFriday);
            this.PanelWeekly.Controls.Add(this.cboxTuesday);
            this.PanelWeekly.Controls.Add(this.cboxMonday);
            this.PanelWeekly.Controls.Add(this.cboxSunday);
            this.PanelWeekly.Controls.Add(this.RunWeeklyTime);
            this.PanelWeekly.Controls.Add(this.label11);
            this.PanelWeekly.Location = new System.Drawing.Point(114, 58);
            this.PanelWeekly.Name = "PanelWeekly";
            this.PanelWeekly.Size = new System.Drawing.Size(348, 121);
            this.PanelWeekly.TabIndex = 12;
            this.PanelWeekly.Visible = false;
            // 
            // cboxThursday
            // 
            this.cboxThursday.AutoSize = true;
            this.cboxThursday.Location = new System.Drawing.Point(12, 71);
            this.cboxThursday.Name = "cboxThursday";
            this.cboxThursday.Size = new System.Drawing.Size(62, 17);
            this.cboxThursday.TabIndex = 8;
            this.cboxThursday.Text = "星期四";
            this.cboxThursday.UseVisualStyleBackColor = true;
            this.cboxThursday.CheckedChanged += new System.EventHandler(this.cboxThursday_CheckedChanged);
            // 
            // cboxWednesday
            // 
            this.cboxWednesday.AutoSize = true;
            this.cboxWednesday.Location = new System.Drawing.Point(228, 45);
            this.cboxWednesday.Name = "cboxWednesday";
            this.cboxWednesday.Size = new System.Drawing.Size(62, 17);
            this.cboxWednesday.TabIndex = 7;
            this.cboxWednesday.Text = "星期三";
            this.cboxWednesday.UseVisualStyleBackColor = true;
            this.cboxWednesday.CheckedChanged += new System.EventHandler(this.cboxWednesday_CheckedChanged);
            // 
            // cboxSturday
            // 
            this.cboxSturday.AutoSize = true;
            this.cboxSturday.Location = new System.Drawing.Point(154, 71);
            this.cboxSturday.Name = "cboxSturday";
            this.cboxSturday.Size = new System.Drawing.Size(62, 17);
            this.cboxSturday.TabIndex = 6;
            this.cboxSturday.Text = "星期六";
            this.cboxSturday.UseVisualStyleBackColor = true;
            this.cboxSturday.CheckedChanged += new System.EventHandler(this.cboxSturday_CheckedChanged);
            // 
            // cboxFriday
            // 
            this.cboxFriday.AutoSize = true;
            this.cboxFriday.Location = new System.Drawing.Point(83, 71);
            this.cboxFriday.Name = "cboxFriday";
            this.cboxFriday.Size = new System.Drawing.Size(62, 17);
            this.cboxFriday.TabIndex = 5;
            this.cboxFriday.Text = "星期五";
            this.cboxFriday.UseVisualStyleBackColor = true;
            this.cboxFriday.CheckedChanged += new System.EventHandler(this.cboxFriday_CheckedChanged);
            // 
            // cboxTuesday
            // 
            this.cboxTuesday.AutoSize = true;
            this.cboxTuesday.Location = new System.Drawing.Point(154, 43);
            this.cboxTuesday.Name = "cboxTuesday";
            this.cboxTuesday.Size = new System.Drawing.Size(62, 17);
            this.cboxTuesday.TabIndex = 4;
            this.cboxTuesday.Text = "星期二";
            this.cboxTuesday.UseVisualStyleBackColor = true;
            this.cboxTuesday.CheckedChanged += new System.EventHandler(this.cboxTuesday_CheckedChanged);
            // 
            // cboxMonday
            // 
            this.cboxMonday.AutoSize = true;
            this.cboxMonday.Location = new System.Drawing.Point(83, 43);
            this.cboxMonday.Name = "cboxMonday";
            this.cboxMonday.Size = new System.Drawing.Size(62, 17);
            this.cboxMonday.TabIndex = 3;
            this.cboxMonday.Text = "星期一";
            this.cboxMonday.UseVisualStyleBackColor = true;
            this.cboxMonday.CheckedChanged += new System.EventHandler(this.cboxMonday_CheckedChanged);
            // 
            // cboxSunday
            // 
            this.cboxSunday.AutoSize = true;
            this.cboxSunday.Location = new System.Drawing.Point(12, 42);
            this.cboxSunday.Name = "cboxSunday";
            this.cboxSunday.Size = new System.Drawing.Size(62, 17);
            this.cboxSunday.TabIndex = 2;
            this.cboxSunday.Text = "星期日";
            this.cboxSunday.UseVisualStyleBackColor = true;
            this.cboxSunday.CheckedChanged += new System.EventHandler(this.cboxSunday_CheckedChanged);
            // 
            // RunWeeklyTime
            // 
            this.RunWeeklyTime.CustomFormat = "HH:mm";
            this.RunWeeklyTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.RunWeeklyTime.Location = new System.Drawing.Point(71, 12);
            this.RunWeeklyTime.Name = "RunWeeklyTime";
            this.RunWeeklyTime.ShowUpDown = true;
            this.RunWeeklyTime.Size = new System.Drawing.Size(129, 20);
            this.RunWeeklyTime.TabIndex = 1;
            this.RunWeeklyTime.ValueChanged += new System.EventHandler(this.RunWeeklyTime_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 14);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "执行时间：";
            // 
            // PanelDay
            // 
            this.PanelDay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelDay.Controls.Add(this.RunDayTwice1Time);
            this.PanelDay.Controls.Add(this.RunDayOnesTime);
            this.PanelDay.Controls.Add(this.label10);
            this.PanelDay.Controls.Add(this.label9);
            this.PanelDay.Controls.Add(this.raRuntwice);
            this.PanelDay.Controls.Add(this.raRunones);
            this.PanelDay.Controls.Add(this.label8);
            this.PanelDay.Controls.Add(this.RunDayTwice2Time);
            this.PanelDay.Location = new System.Drawing.Point(114, 58);
            this.PanelDay.Name = "PanelDay";
            this.PanelDay.Size = new System.Drawing.Size(348, 121);
            this.PanelDay.TabIndex = 11;
            // 
            // RunDayTwice1Time
            // 
            this.RunDayTwice1Time.CustomFormat = "HH:mm";
            this.RunDayTwice1Time.Enabled = false;
            this.RunDayTwice1Time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.RunDayTwice1Time.Location = new System.Drawing.Point(100, 61);
            this.RunDayTwice1Time.Name = "RunDayTwice1Time";
            this.RunDayTwice1Time.ShowUpDown = true;
            this.RunDayTwice1Time.Size = new System.Drawing.Size(111, 20);
            this.RunDayTwice1Time.TabIndex = 6;
            this.RunDayTwice1Time.ValueChanged += new System.EventHandler(this.RunDayTwice1Time_ValueChanged);
            // 
            // RunDayOnesTime
            // 
            this.RunDayOnesTime.CustomFormat = "HH:mm";
            this.RunDayOnesTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.RunDayOnesTime.Location = new System.Drawing.Point(100, 35);
            this.RunDayOnesTime.Name = "RunDayOnesTime";
            this.RunDayOnesTime.ShowUpDown = true;
            this.RunDayOnesTime.Size = new System.Drawing.Size(111, 20);
            this.RunDayOnesTime.TabIndex = 5;
            this.RunDayOnesTime.ValueChanged += new System.EventHandler(this.RunDayOnesTime_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Enabled = false;
            this.label10.Location = new System.Drawing.Point(9, 64);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "上午执行时间：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Enabled = false;
            this.label9.Location = new System.Drawing.Point(9, 89);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "下午执行时间：";
            // 
            // raRuntwice
            // 
            this.raRuntwice.AutoSize = true;
            this.raRuntwice.Location = new System.Drawing.Point(130, 12);
            this.raRuntwice.Name = "raRuntwice";
            this.raRuntwice.Size = new System.Drawing.Size(133, 17);
            this.raRuntwice.TabIndex = 2;
            this.raRuntwice.Text = "上午下午各执行一次";
            this.raRuntwice.UseVisualStyleBackColor = true;
            this.raRuntwice.CheckedChanged += new System.EventHandler(this.raRuntwice_CheckedChanged);
            // 
            // raRunones
            // 
            this.raRunones.AutoSize = true;
            this.raRunones.Checked = true;
            this.raRunones.Location = new System.Drawing.Point(12, 12);
            this.raRunones.Name = "raRunones";
            this.raRunones.Size = new System.Drawing.Size(109, 17);
            this.raRunones.TabIndex = 1;
            this.raRunones.TabStop = true;
            this.raRunones.Text = "每天仅执行一次";
            this.raRunones.UseVisualStyleBackColor = true;
            this.raRunones.CheckedChanged += new System.EventHandler(this.raRunones_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "执行时间：";
            // 
            // RunDayTwice2Time
            // 
            this.RunDayTwice2Time.CustomFormat = "HH:mm";
            this.RunDayTwice2Time.Enabled = false;
            this.RunDayTwice2Time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.RunDayTwice2Time.Location = new System.Drawing.Point(100, 89);
            this.RunDayTwice2Time.Name = "RunDayTwice2Time";
            this.RunDayTwice2Time.ShowUpDown = true;
            this.RunDayTwice2Time.Size = new System.Drawing.Size(111, 20);
            this.RunDayTwice2Time.TabIndex = 7;
            this.RunDayTwice2Time.ValueChanged += new System.EventHandler(this.RunDayTwice2Time_ValueChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "跟踪.gif");
            this.imageList1.Images.SetKeyName(1, "A18.gif");
            this.imageList1.Images.SetKeyName(2, "Table.gif");
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(2, 344);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(516, 5);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // IsSave
            // 
            this.IsSave.Location = new System.Drawing.Point(196, 358);
            this.IsSave.Name = "IsSave";
            this.IsSave.Size = new System.Drawing.Size(43, 20);
            this.IsSave.TabIndex = 9;
            this.IsSave.Visible = false;
            this.IsSave.TextChanged += new System.EventHandler(this.IsSave_TextChanged);
            // 
            // cmdApply
            // 
            this.cmdApply.Enabled = false;
            this.cmdApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdApply.Image = ((System.Drawing.Image)(resources.GetObject("cmdApply.Image")));
            this.cmdApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdApply.Location = new System.Drawing.Point(428, 355);
            this.cmdApply.Name = "cmdApply";
            this.cmdApply.Size = new System.Drawing.Size(65, 24);
            this.cmdApply.TabIndex = 8;
            this.cmdApply.Text = "应 用";
            this.cmdApply.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdApply.UseVisualStyleBackColor = true;
            this.cmdApply.Click += new System.EventHandler(this.cmdApply_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCancel.Image = ((System.Drawing.Image)(resources.GetObject("cmdCancel.Image")));
            this.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cmdCancel.Location = new System.Drawing.Point(349, 355);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(65, 24);
            this.cmdCancel.TabIndex = 7;
            this.cmdCancel.Text = "取 消";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdOK.Image = ((System.Drawing.Image)(resources.GetObject("cmdOK.Image")));
            this.cmdOK.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.cmdOK.Location = new System.Drawing.Point(271, 355);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(65, 24);
            this.cmdOK.TabIndex = 6;
            this.cmdOK.Text = "确 定";
            this.cmdOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txtPlanID
            // 
            this.txtPlanID.Location = new System.Drawing.Point(82, 358);
            this.txtPlanID.Name = "txtPlanID";
            this.txtPlanID.Size = new System.Drawing.Size(66, 20);
            this.txtPlanID.TabIndex = 10;
            this.txtPlanID.Visible = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label16.Location = new System.Drawing.Point(14, 360);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(235, 13);
            this.label16.TabIndex = 11;
            this.label16.Text = "计划必须在Soukey采摘运行状态下方可执行";
            // 
            // frmTaskPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 390);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtPlanID);
            this.Controls.Add(this.IsSave);
            this.Controls.Add(this.cmdApply);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtPlanName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTaskPlan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "任务执行计划";
            this.Load += new System.EventHandler(this.frmTaskPlan_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DisabledRunNum)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.PanelOne.ResumeLayout(false);
            this.PanelOne.PerformLayout();
            this.PanelWeekly.ResumeLayout(false);
            this.PanelWeekly.PerformLayout();
            this.PanelDay.ResumeLayout(false);
            this.PanelDay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPlanName;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox IsSave;
        private System.Windows.Forms.Button cmdApply;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView listTask;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cboxIsRun;
        private System.Windows.Forms.TextBox txtPlanDemo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdDelTask;
        private System.Windows.Forms.Button cmdAddTask;
        private System.Windows.Forms.RadioButton raWeekly;
        private System.Windows.Forms.RadioButton raDay;
        private System.Windows.Forms.RadioButton raOneTime;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.RadioButton raDateTime;
        private System.Windows.Forms.RadioButton raNumber;
        private System.Windows.Forms.CheckBox cboxIsDisabled;
        private System.Windows.Forms.DateTimePicker DisabledDateTime;
        private System.Windows.Forms.NumericUpDown DisabledRunNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.DateTimePicker EnabledDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel PanelOne;
        private System.Windows.Forms.DateTimePicker RunOnceTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel PanelDay;
        private System.Windows.Forms.RadioButton raRuntwice;
        private System.Windows.Forms.RadioButton raRunones;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel PanelWeekly;
        private System.Windows.Forms.DateTimePicker RunDayTwice2Time;
        private System.Windows.Forms.DateTimePicker RunDayTwice1Time;
        private System.Windows.Forms.DateTimePicker RunDayOnesTime;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox cboxThursday;
        private System.Windows.Forms.CheckBox cboxWednesday;
        private System.Windows.Forms.CheckBox cboxSturday;
        private System.Windows.Forms.CheckBox cboxFriday;
        private System.Windows.Forms.CheckBox cboxTuesday;
        private System.Windows.Forms.CheckBox cboxMonday;
        private System.Windows.Forms.CheckBox cboxSunday;
        private System.Windows.Forms.DateTimePicker RunWeeklyTime;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox txtPlanID;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtPlanState;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
    }
}