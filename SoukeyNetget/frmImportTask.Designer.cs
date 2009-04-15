namespace SoukeyNetget
{
    partial class frmImportTask
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportTask));
            this.comTaskClass = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listTask = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // comTaskClass
            // 
            this.comTaskClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comTaskClass.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comTaskClass.FormattingEnabled = true;
            this.comTaskClass.Location = new System.Drawing.Point(83, 14);
            this.comTaskClass.Name = "comTaskClass";
            this.comTaskClass.Size = new System.Drawing.Size(332, 21);
            this.comTaskClass.TabIndex = 0;
            this.comTaskClass.SelectedIndexChanged += new System.EventHandler(this.comTaskClass_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "任务分类：";
            // 
            // listTask
            // 
            this.listTask.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listTask.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listTask.FullRowSelect = true;
            this.listTask.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listTask.Location = new System.Drawing.Point(14, 42);
            this.listTask.MultiSelect = false;
            this.listTask.Name = "listTask";
            this.listTask.Size = new System.Drawing.Size(401, 181);
            this.listTask.SmallImageList = this.imageList1;
            this.listTask.TabIndex = 2;
            this.listTask.UseCompatibleStateImageBehavior = false;
            this.listTask.View = System.Windows.Forms.View.Details;
            this.listTask.DoubleClick += new System.EventHandler(this.listTask_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "任务名称";
            this.columnHeader1.Width = 233;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "任务类型";
            this.columnHeader2.Width = 164;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "跟踪.gif");
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(2, 230);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(450, 9);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // cmdOK
            // 
            this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdOK.Location = new System.Drawing.Point(246, 251);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(76, 25);
            this.cmdOK.TabIndex = 6;
            this.cmdOK.Text = "确 定";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCancel.Location = new System.Drawing.Point(339, 251);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(76, 25);
            this.cmdCancel.TabIndex = 7;
            this.cmdCancel.Text = "取 消";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmImportTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 288);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listTask);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comTaskClass);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmImportTask";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "导入任务";
            this.Load += new System.EventHandler(this.frmImportTask_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comTaskClass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listTask;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}