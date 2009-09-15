namespace SoukeyNetget
{
    partial class frmUpgradeTask
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpgradeTask));
            this.listTask = new System.Windows.Forms.ListView();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.IsAutoBackup = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stalabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ProBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tmenuFindTask = new System.Windows.Forms.ToolStripButton();
            this.tmenuAddTask = new System.Windows.Forms.ToolStripButton();
            this.tmenuResetTask = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tmenuStart = new System.Windows.Forms.ToolStripButton();
            this.tmenuExit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listTask
            // 
            this.listTask.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader6});
            this.listTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listTask.FullRowSelect = true;
            this.listTask.Location = new System.Drawing.Point(0, 0);
            this.listTask.Name = "listTask";
            this.listTask.Size = new System.Drawing.Size(772, 355);
            this.listTask.SmallImageList = this.imageList1;
            this.listTask.TabIndex = 8;
            this.listTask.UseCompatibleStateImageBehavior = false;
            this.listTask.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "状态";
            this.columnHeader5.Width = 100;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "任务名称";
            this.columnHeader1.Width = 160;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "版本";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "所属分类";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "存储路径";
            this.columnHeader4.Width = 160;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "信息";
            this.columnHeader6.Width = 260;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "task");
            this.imageList1.Images.SetKeyName(1, "running");
            this.imageList1.Images.SetKeyName(2, "success");
            this.imageList1.Images.SetKeyName(3, "error");
            // 
            // IsAutoBackup
            // 
            this.IsAutoBackup.AutoSize = true;
            this.IsAutoBackup.Checked = true;
            this.IsAutoBackup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IsAutoBackup.Location = new System.Drawing.Point(12, 13);
            this.IsAutoBackup.Name = "IsAutoBackup";
            this.IsAutoBackup.Size = new System.Drawing.Size(422, 17);
            this.IsAutoBackup.TabIndex = 9;
            this.IsAutoBackup.Text = "自动备份升级任务，备份文件将存储在原有目录下，文件名为源文件名+bak";
            this.IsAutoBackup.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stalabel,
            this.ProBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 424);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(772, 22);
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // stalabel
            // 
            this.stalabel.Name = "stalabel";
            this.stalabel.Size = new System.Drawing.Size(92, 17);
            this.stalabel.Text = "当前状态：就绪";
            // 
            // ProBar
            // 
            this.ProBar.Name = "ProBar";
            this.ProBar.Size = new System.Drawing.Size(100, 16);
            this.ProBar.Visible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmenuFindTask,
            this.tmenuAddTask,
            this.tmenuResetTask,
            this.toolStripSeparator1,
            this.tmenuStart,
            this.tmenuExit,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(772, 25);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tmenuFindTask
            // 
            this.tmenuFindTask.Image = ((System.Drawing.Image)(resources.GetObject("tmenuFindTask.Image")));
            this.tmenuFindTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tmenuFindTask.Name = "tmenuFindTask";
            this.tmenuFindTask.Size = new System.Drawing.Size(136, 22);
            this.tmenuFindTask.Text = "自动发现待升级任务";
            this.tmenuFindTask.Click += new System.EventHandler(this.tmenuFindTask_Click);
            // 
            // tmenuAddTask
            // 
            this.tmenuAddTask.Image = ((System.Drawing.Image)(resources.GetObject("tmenuAddTask.Image")));
            this.tmenuAddTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tmenuAddTask.Name = "tmenuAddTask";
            this.tmenuAddTask.Size = new System.Drawing.Size(136, 22);
            this.tmenuAddTask.Text = "手工添加待升级任务";
            this.tmenuAddTask.Click += new System.EventHandler(this.tmenuAddTask_Click);
            // 
            // tmenuResetTask
            // 
            this.tmenuResetTask.Image = ((System.Drawing.Image)(resources.GetObject("tmenuResetTask.Image")));
            this.tmenuResetTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tmenuResetTask.Name = "tmenuResetTask";
            this.tmenuResetTask.Size = new System.Drawing.Size(52, 22);
            this.tmenuResetTask.Text = "重置";
            this.tmenuResetTask.Click += new System.EventHandler(this.tmenuResetTask_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tmenuStart
            // 
            this.tmenuStart.Image = ((System.Drawing.Image)(resources.GetObject("tmenuStart.Image")));
            this.tmenuStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tmenuStart.Name = "tmenuStart";
            this.tmenuStart.Size = new System.Drawing.Size(76, 22);
            this.tmenuStart.Text = "开始升级";
            this.tmenuStart.Click += new System.EventHandler(this.tmenuStart_Click);
            // 
            // tmenuExit
            // 
            this.tmenuExit.Image = ((System.Drawing.Image)(resources.GetObject("tmenuExit.Image")));
            this.tmenuExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tmenuExit.Name = "tmenuExit";
            this.tmenuExit.Size = new System.Drawing.Size(52, 22);
            this.tmenuExit.Text = "关闭";
            this.tmenuExit.Click += new System.EventHandler(this.tmenuExit_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.IsAutoBackup);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listTask);
            this.splitContainer1.Size = new System.Drawing.Size(772, 399);
            this.splitContainer1.SplitterDistance = 40;
            this.splitContainer1.TabIndex = 14;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // frmUpgradeTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 446);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmUpgradeTask";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "任务版本升级 Soukey采摘1.6支持的任务版本号为：1.3";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listTask;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.CheckBox IsAutoBackup;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stalabel;
        private System.Windows.Forms.ToolStripProgressBar ProBar;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tmenuStart;
        private System.Windows.Forms.ToolStripButton tmenuExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tmenuFindTask;
        private System.Windows.Forms.ToolStripButton tmenuAddTask;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripButton tmenuResetTask;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ImageList imageList1;
    }
}