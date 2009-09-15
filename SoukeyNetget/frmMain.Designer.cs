namespace SoukeyNetget
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("正在采集", 1, 1);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("正在发布", 2, 2);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("已经完成的任务", 3, 3);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("任务运行区", 0, 0, new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("任务计划", 11, 11);
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("执行历史", 14, 14);
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("任务执行计划", 10, 10, new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("任务分类", 4, 4);
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolManage = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolUpgradeTask = new System.Windows.Forms.ToolStripMenuItem();
            this.toolImportTask = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.toolManageDict = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.toolWebbrowser = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.toolLookInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolNewTask = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolmenuNewTask = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMenuNewTaskPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.toolmenuNewTaskClass = new System.Windows.Forms.ToolStripMenuItem();
            this.toolCopyTask = new System.Windows.Forms.ToolStripButton();
            this.toolPasteTask = new System.Windows.Forms.ToolStripButton();
            this.toolDelTask = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStartTask = new System.Windows.Forms.ToolStripButton();
            this.toolStopTask = new System.Windows.Forms.ToolStripButton();
            this.toolRestartTask = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBrowserData = new System.Windows.Forms.ToolStripButton();
            this.toolExportData = new System.Windows.Forms.ToolStripSplitButton();
            this.toolExportTxt = new System.Windows.Forms.ToolStripMenuItem();
            this.toolExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.staIsInternet = new System.Windows.Forms.ToolStripStatusLabel();
            this.StateInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ExportProbar = new System.Windows.Forms.ToolStripProgressBar();
            this.PrograBarTxt = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.treeMenu = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.rmenuAddTaskClass = new System.Windows.Forms.ToolStripMenuItem();
            this.rmenuRenameTaskClass = new System.Windows.Forms.ToolStripMenuItem();
            this.rmenuDelTaskClass = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.menuAddTask = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddTaskPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdCloseInfo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataTask = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.rmmenuStartTask = new System.Windows.Forms.ToolStripMenuItem();
            this.rmmenuStopTask = new System.Windows.Forms.ToolStripMenuItem();
            this.rmmenuRestartTask = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.rmenuBrowserData = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.rmenuCopyTask = new System.Windows.Forms.ToolStripMenuItem();
            this.rmenuPasteTask = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.rmmenuEditTask = new System.Windows.Forms.ToolStripMenuItem();
            this.rmmenuRenameTask = new System.Windows.Forms.ToolStripMenuItem();
            this.rmmenuDelTask = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.rmmenuNewTask = new System.Windows.Forms.ToolStripMenuItem();
            this.rmenuAddPlan = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.contextMenuStrip4 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.rMenuExportTxt = new System.Windows.Forms.ToolStripMenuItem();
            this.rMenuExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.rMenuCloseTabPage = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMailto = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuOpenMainfrm = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuCloseSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.服务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMenuDownloadTask = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.toolmenuConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMenuLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this.中文简体ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.toolMenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.Soukey采摘ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMenuProgram = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolMenuVisityijie = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMenuUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolMenuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStrip5 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.rmenuSaveLog = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataTask)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.contextMenuStrip4.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.contextMenuStrip3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip5.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolManage,
            this.toolStripSeparator9,
            this.toolNewTask,
            this.toolCopyTask,
            this.toolPasteTask,
            this.toolDelTask,
            this.toolStripSeparator1,
            this.toolStartTask,
            this.toolStopTask,
            this.toolRestartTask,
            this.toolStripButton5,
            this.toolBrowserData,
            this.toolExportData,
            this.toolStripSeparator8,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(891, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolManage
            // 
            this.toolManage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolUpgradeTask,
            this.toolImportTask,
            this.toolStripSeparator17,
            this.toolManageDict,
            this.toolStripSeparator10,
            this.toolWebbrowser,
            this.toolStripSeparator11,
            this.toolLookInfo});
            this.toolManage.Image = ((System.Drawing.Image)(resources.GetObject("toolManage.Image")));
            this.toolManage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolManage.Name = "toolManage";
            this.toolManage.Size = new System.Drawing.Size(61, 22);
            this.toolManage.Text = "工具";
            // 
            // toolUpgradeTask
            // 
            this.toolUpgradeTask.Name = "toolUpgradeTask";
            this.toolUpgradeTask.Size = new System.Drawing.Size(148, 22);
            this.toolUpgradeTask.Text = "任务升级";
            this.toolUpgradeTask.Click += new System.EventHandler(this.toolUpgradeTask_Click);
            // 
            // toolImportTask
            // 
            this.toolImportTask.Name = "toolImportTask";
            this.toolImportTask.Size = new System.Drawing.Size(148, 22);
            this.toolImportTask.Text = "导入任务";
            this.toolImportTask.Click += new System.EventHandler(this.toolImportTask_Click);
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(145, 6);
            // 
            // toolManageDict
            // 
            this.toolManageDict.Name = "toolManageDict";
            this.toolManageDict.Size = new System.Drawing.Size(148, 22);
            this.toolManageDict.Text = "维护参数字典";
            this.toolManageDict.Click += new System.EventHandler(this.toolManageDict_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(145, 6);
            // 
            // toolWebbrowser
            // 
            this.toolWebbrowser.Name = "toolWebbrowser";
            this.toolWebbrowser.Size = new System.Drawing.Size(148, 22);
            this.toolWebbrowser.Text = "Mini 浏览器";
            this.toolWebbrowser.Click += new System.EventHandler(this.toolWebbrowser_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(145, 6);
            // 
            // toolLookInfo
            // 
            this.toolLookInfo.Checked = true;
            this.toolLookInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolLookInfo.Name = "toolLookInfo";
            this.toolLookInfo.Size = new System.Drawing.Size(148, 22);
            this.toolLookInfo.Text = "查看系统信息";
            this.toolLookInfo.Click += new System.EventHandler(this.toolLookInfo_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // toolNewTask
            // 
            this.toolNewTask.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolmenuNewTask,
            this.toolMenuNewTaskPlan,
            this.toolStripSeparator14,
            this.toolmenuNewTaskClass});
            this.toolNewTask.Image = global::SoukeyNetget.Properties.Resources.file;
            this.toolNewTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolNewTask.Name = "toolNewTask";
            this.toolNewTask.Size = new System.Drawing.Size(61, 22);
            this.toolNewTask.Text = "新建";
            this.toolNewTask.ToolTipText = "新建任务";
            // 
            // toolmenuNewTask
            // 
            this.toolmenuNewTask.Name = "toolmenuNewTask";
            this.toolmenuNewTask.Size = new System.Drawing.Size(172, 22);
            this.toolmenuNewTask.Text = "新建任务";
            this.toolmenuNewTask.Click += new System.EventHandler(this.toolmenuNewTask_Click);
            // 
            // toolMenuNewTaskPlan
            // 
            this.toolMenuNewTaskPlan.Name = "toolMenuNewTaskPlan";
            this.toolMenuNewTaskPlan.Size = new System.Drawing.Size(172, 22);
            this.toolMenuNewTaskPlan.Text = "新建任务执行计划";
            this.toolMenuNewTaskPlan.Click += new System.EventHandler(this.toolMenuNewTaskPlan_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(169, 6);
            // 
            // toolmenuNewTaskClass
            // 
            this.toolmenuNewTaskClass.Name = "toolmenuNewTaskClass";
            this.toolmenuNewTaskClass.Size = new System.Drawing.Size(172, 22);
            this.toolmenuNewTaskClass.Text = "新建任务分类";
            this.toolmenuNewTaskClass.Click += new System.EventHandler(this.toolmenuNewTaskClass_Click);
            // 
            // toolCopyTask
            // 
            this.toolCopyTask.Enabled = false;
            this.toolCopyTask.Image = ((System.Drawing.Image)(resources.GetObject("toolCopyTask.Image")));
            this.toolCopyTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolCopyTask.Name = "toolCopyTask";
            this.toolCopyTask.Size = new System.Drawing.Size(52, 22);
            this.toolCopyTask.Text = "复制";
            this.toolCopyTask.ToolTipText = "复制任务";
            this.toolCopyTask.Click += new System.EventHandler(this.toolCopyTask_Click);
            // 
            // toolPasteTask
            // 
            this.toolPasteTask.Enabled = false;
            this.toolPasteTask.Image = ((System.Drawing.Image)(resources.GetObject("toolPasteTask.Image")));
            this.toolPasteTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolPasteTask.Name = "toolPasteTask";
            this.toolPasteTask.Size = new System.Drawing.Size(52, 22);
            this.toolPasteTask.Text = "粘贴";
            this.toolPasteTask.ToolTipText = "粘贴任务";
            this.toolPasteTask.Click += new System.EventHandler(this.toolPasteT_Click);
            // 
            // toolDelTask
            // 
            this.toolDelTask.Image = ((System.Drawing.Image)(resources.GetObject("toolDelTask.Image")));
            this.toolDelTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDelTask.Name = "toolDelTask";
            this.toolDelTask.Size = new System.Drawing.Size(52, 22);
            this.toolDelTask.Text = "删除";
            this.toolDelTask.Click += new System.EventHandler(this.toolDelTask_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStartTask
            // 
            this.toolStartTask.Enabled = false;
            this.toolStartTask.Image = ((System.Drawing.Image)(resources.GetObject("toolStartTask.Image")));
            this.toolStartTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStartTask.Name = "toolStartTask";
            this.toolStartTask.Size = new System.Drawing.Size(52, 22);
            this.toolStartTask.Text = "开始";
            this.toolStartTask.ToolTipText = "启动任务";
            this.toolStartTask.Click += new System.EventHandler(this.toolStartTask_Click);
            // 
            // toolStopTask
            // 
            this.toolStopTask.Enabled = false;
            this.toolStopTask.Image = ((System.Drawing.Image)(resources.GetObject("toolStopTask.Image")));
            this.toolStopTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStopTask.Name = "toolStopTask";
            this.toolStopTask.Size = new System.Drawing.Size(52, 22);
            this.toolStopTask.Text = "停止";
            this.toolStopTask.ToolTipText = "停止正在运行的任务";
            this.toolStopTask.Click += new System.EventHandler(this.toolStopTask_Click);
            // 
            // toolRestartTask
            // 
            this.toolRestartTask.Enabled = false;
            this.toolRestartTask.Image = ((System.Drawing.Image)(resources.GetObject("toolRestartTask.Image")));
            this.toolRestartTask.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolRestartTask.Name = "toolRestartTask";
            this.toolRestartTask.Size = new System.Drawing.Size(52, 22);
            this.toolRestartTask.Text = "重置";
            this.toolRestartTask.ToolTipText = "重置任务 将已运行的任务重置初始状态";
            this.toolRestartTask.Click += new System.EventHandler(this.toolRestartTask_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolBrowserData
            // 
            this.toolBrowserData.Enabled = false;
            this.toolBrowserData.Image = ((System.Drawing.Image)(resources.GetObject("toolBrowserData.Image")));
            this.toolBrowserData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBrowserData.Name = "toolBrowserData";
            this.toolBrowserData.Size = new System.Drawing.Size(76, 22);
            this.toolBrowserData.Text = "查看数据";
            this.toolBrowserData.ToolTipText = "查看已采集的数据";
            this.toolBrowserData.Click += new System.EventHandler(this.toolBrowserData_Click);
            // 
            // toolExportData
            // 
            this.toolExportData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolExportTxt,
            this.toolExportExcel});
            this.toolExportData.Enabled = false;
            this.toolExportData.Image = ((System.Drawing.Image)(resources.GetObject("toolExportData.Image")));
            this.toolExportData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolExportData.Name = "toolExportData";
            this.toolExportData.Size = new System.Drawing.Size(88, 22);
            this.toolExportData.Text = "导出数据";
            this.toolExportData.ToolTipText = "导出采集数据";
            this.toolExportData.ButtonClick += new System.EventHandler(this.toolExportData_ButtonClick);
            // 
            // toolExportTxt
            // 
            this.toolExportTxt.Name = "toolExportTxt";
            this.toolExportTxt.Size = new System.Drawing.Size(148, 22);
            this.toolExportTxt.Text = "导出文本文件";
            this.toolExportTxt.Click += new System.EventHandler(this.toolExportTxt_Click);
            // 
            // toolExportExcel
            // 
            this.toolExportExcel.Name = "toolExportExcel";
            this.toolExportExcel.Size = new System.Drawing.Size(148, 22);
            this.toolExportExcel.Text = "导出Excel";
            this.toolExportExcel.Click += new System.EventHandler(this.toolExportExcel_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton2.Text = "退出";
            this.toolStripButton2.ToolTipText = "退出 Soukey采摘";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.staIsInternet,
            this.StateInfo,
            this.toolStripStatusLabel1,
            this.ExportProbar,
            this.PrograBarTxt,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 452);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(891, 26);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // staIsInternet
            // 
            this.staIsInternet.AutoSize = false;
            this.staIsInternet.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.staIsInternet.Image = ((System.Drawing.Image)(resources.GetObject("staIsInternet.Image")));
            this.staIsInternet.Name = "staIsInternet";
            this.staIsInternet.Size = new System.Drawing.Size(60, 21);
            this.staIsInternet.Text = "在线";
            // 
            // StateInfo
            // 
            this.StateInfo.AutoSize = false;
            this.StateInfo.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.StateInfo.Name = "StateInfo";
            this.StateInfo.Size = new System.Drawing.Size(200, 21);
            this.StateInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 21);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // ExportProbar
            // 
            this.ExportProbar.AutoSize = false;
            this.ExportProbar.Enabled = false;
            this.ExportProbar.Name = "ExportProbar";
            this.ExportProbar.Size = new System.Drawing.Size(100, 20);
            this.ExportProbar.Visible = false;
            // 
            // PrograBarTxt
            // 
            this.PrograBarTxt.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.PrograBarTxt.Name = "PrograBarTxt";
            this.PrograBarTxt.Size = new System.Drawing.Size(96, 21);
            this.PrograBarTxt.Text = "当前正在导出：";
            this.PrograBarTxt.Visible = false;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(616, 21);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(891, 402);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 50);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(891, 402);
            this.toolStripContainer1.TabIndex = 5;
            this.toolStripContainer1.Text = "toolStripContainer1";
            this.toolStripContainer1.TopToolStripPanelVisible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(891, 402);
            this.splitContainer1.SplitterDistance = 169;
            this.splitContainer1.TabIndex = 4;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.treeMenu);
            this.splitContainer3.Panel1MinSize = 300;
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer3.Panel2.Controls.Add(this.txtLog);
            this.splitContainer3.Panel2.Controls.Add(this.panel1);
            this.splitContainer3.Panel2MinSize = 0;
            this.splitContainer3.Size = new System.Drawing.Size(169, 402);
            this.splitContainer3.SplitterDistance = 300;
            this.splitContainer3.TabIndex = 1;
            // 
            // treeMenu
            // 
            this.treeMenu.AllowDrop = true;
            this.treeMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeMenu.ContextMenuStrip = this.contextMenuStrip1;
            this.treeMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeMenu.HideSelection = false;
            this.treeMenu.ImageIndex = 0;
            this.treeMenu.ImageList = this.imageList1;
            this.treeMenu.Location = new System.Drawing.Point(0, 0);
            this.treeMenu.Name = "treeMenu";
            treeNode1.ImageIndex = 1;
            treeNode1.Name = "nodRunning";
            treeNode1.SelectedImageIndex = 1;
            treeNode1.Text = "正在采集";
            treeNode1.ToolTipText = "显示需要采集数据的任务";
            treeNode2.ImageIndex = 2;
            treeNode2.Name = "nodPublish";
            treeNode2.SelectedImageIndex = 2;
            treeNode2.Text = "正在发布";
            treeNode2.ToolTipText = "显示正在发布的任务 不能人工干预";
            treeNode3.ImageIndex = 3;
            treeNode3.Name = "nodComplete";
            treeNode3.SelectedImageIndex = 3;
            treeNode3.Text = "已经完成的任务";
            treeNode3.ToolTipText = "显示已经完成采集的任务";
            treeNode4.ImageIndex = 0;
            treeNode4.Name = "nodSnap";
            treeNode4.SelectedImageIndex = 0;
            treeNode4.Text = "任务运行区";
            treeNode5.ImageIndex = 11;
            treeNode5.Name = "nodPlanRunning";
            treeNode5.SelectedImageIndex = 11;
            treeNode5.Text = "任务计划";
            treeNode6.ImageIndex = 14;
            treeNode6.Name = "nodPlanCompleted";
            treeNode6.SelectedImageIndex = 14;
            treeNode6.Text = "执行历史";
            treeNode7.ImageIndex = 10;
            treeNode7.Name = "nodTaskPlan";
            treeNode7.SelectedImageIndex = 10;
            treeNode7.Text = "任务执行计划";
            treeNode8.ImageIndex = 4;
            treeNode8.Name = "nodTaskClass";
            treeNode8.SelectedImageIndex = 4;
            treeNode8.Text = "任务分类";
            this.treeMenu.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode7,
            treeNode8});
            this.treeMenu.SelectedImageIndex = 0;
            this.treeMenu.Size = new System.Drawing.Size(169, 300);
            this.treeMenu.TabIndex = 0;
            this.treeMenu.DragLeave += new System.EventHandler(this.treeMenu_DragLeave);
            this.treeMenu.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeMenu_AfterLabelEdit);
            this.treeMenu.Enter += new System.EventHandler(this.treeMenu_Enter);
            this.treeMenu.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeMenu_DragDrop);
            this.treeMenu.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeMenu_AfterSelect);
            this.treeMenu.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeMenu_DragEnter);
            this.treeMenu.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeMenu_NodeMouseClick);
            this.treeMenu.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeMenu_KeyDown);
            this.treeMenu.DragOver += new System.Windows.Forms.DragEventHandler(this.treeMenu_DragOver);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rmenuAddTaskClass,
            this.rmenuRenameTaskClass,
            this.rmenuDelTaskClass,
            this.toolStripSeparator16,
            this.menuAddTask,
            this.menuAddTaskPlan});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(190, 120);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // rmenuAddTaskClass
            // 
            this.rmenuAddTaskClass.Name = "rmenuAddTaskClass";
            this.rmenuAddTaskClass.Size = new System.Drawing.Size(189, 22);
            this.rmenuAddTaskClass.Text = "添加分类";
            this.rmenuAddTaskClass.Click += new System.EventHandler(this.rmenuAddTaskClass_Click);
            // 
            // rmenuRenameTaskClass
            // 
            this.rmenuRenameTaskClass.Enabled = false;
            this.rmenuRenameTaskClass.Name = "rmenuRenameTaskClass";
            this.rmenuRenameTaskClass.Size = new System.Drawing.Size(189, 22);
            this.rmenuRenameTaskClass.Text = "重命名          F2";
            this.rmenuRenameTaskClass.Click += new System.EventHandler(this.rmenuRenameTaskClass_Click);
            // 
            // rmenuDelTaskClass
            // 
            this.rmenuDelTaskClass.Enabled = false;
            this.rmenuDelTaskClass.Image = ((System.Drawing.Image)(resources.GetObject("rmenuDelTaskClass.Image")));
            this.rmenuDelTaskClass.Name = "rmenuDelTaskClass";
            this.rmenuDelTaskClass.Size = new System.Drawing.Size(189, 22);
            this.rmenuDelTaskClass.Text = "删除分类       Delete";
            this.rmenuDelTaskClass.Click += new System.EventHandler(this.rmenuDelTaskClass_Click);
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(186, 6);
            // 
            // menuAddTask
            // 
            this.menuAddTask.Name = "menuAddTask";
            this.menuAddTask.Size = new System.Drawing.Size(189, 22);
            this.menuAddTask.Text = "新建任务";
            this.menuAddTask.Click += new System.EventHandler(this.menuAddTask_Click);
            // 
            // menuAddTaskPlan
            // 
            this.menuAddTaskPlan.Image = ((System.Drawing.Image)(resources.GetObject("menuAddTaskPlan.Image")));
            this.menuAddTaskPlan.Name = "menuAddTaskPlan";
            this.menuAddTaskPlan.Size = new System.Drawing.Size(189, 22);
            this.menuAddTaskPlan.Text = "新建任务执行计划";
            this.menuAddTaskPlan.Click += new System.EventHandler(this.menuAddTaskPlan_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "task");
            this.imageList1.Images.SetKeyName(1, "run");
            this.imageList1.Images.SetKeyName(2, "export");
            this.imageList1.Images.SetKeyName(3, "OK");
            this.imageList1.Images.SetKeyName(4, "tree");
            this.imageList1.Images.SetKeyName(5, "started");
            this.imageList1.Images.SetKeyName(6, "pause");
            this.imageList1.Images.SetKeyName(7, "stop");
            this.imageList1.Images.SetKeyName(8, "logo");
            this.imageList1.Images.SetKeyName(9, "error");
            this.imageList1.Images.SetKeyName(10, "taskplan");
            this.imageList1.Images.SetKeyName(11, "planrunning");
            this.imageList1.Images.SetKeyName(12, "PlanCompleted");
            this.imageList1.Images.SetKeyName(13, "disabledplan");
            this.imageList1.Images.SetKeyName(14, "log");
            // 
            // txtLog
            // 
            this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(0, 21);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(169, 77);
            this.txtLog.TabIndex = 2;
            this.txtLog.Text = "";
            this.txtLog.WordWrap = false;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.cmdCloseInfo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(169, 21);
            this.panel1.TabIndex = 1;
            // 
            // cmdCloseInfo
            // 
            this.cmdCloseInfo.BackColor = System.Drawing.Color.Transparent;
            this.cmdCloseInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdCloseInfo.FlatAppearance.BorderSize = 0;
            this.cmdCloseInfo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.cmdCloseInfo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.cmdCloseInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCloseInfo.Image = ((System.Drawing.Image)(resources.GetObject("cmdCloseInfo.Image")));
            this.cmdCloseInfo.Location = new System.Drawing.Point(154, 0);
            this.cmdCloseInfo.Name = "cmdCloseInfo";
            this.cmdCloseInfo.Size = new System.Drawing.Size(15, 21);
            this.cmdCloseInfo.TabIndex = 1;
            this.cmdCloseInfo.UseVisualStyleBackColor = false;
            this.cmdCloseInfo.Click += new System.EventHandler(this.cmdCloseInfo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(6, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "系统信息";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dataTask);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer2.Size = new System.Drawing.Size(718, 402);
            this.splitContainer2.SplitterDistance = 200;
            this.splitContainer2.TabIndex = 0;
            // 
            // dataTask
            // 
            this.dataTask.AllowUserToAddRows = false;
            this.dataTask.AllowUserToOrderColumns = true;
            this.dataTask.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Linen;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dataTask.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataTask.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dataTask.BackgroundColor = System.Drawing.Color.White;
            this.dataTask.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataTask.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataTask.ColumnHeadersHeight = 20;
            this.dataTask.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataTask.ContextMenuStrip = this.contextMenuStrip2;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataTask.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataTask.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataTask.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.dataTask.Location = new System.Drawing.Point(0, 0);
            this.dataTask.Margin = new System.Windows.Forms.Padding(0);
            this.dataTask.Name = "dataTask";
            this.dataTask.ReadOnly = true;
            this.dataTask.RowHeadersVisible = false;
            this.dataTask.RowTemplate.Height = 23;
            this.dataTask.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataTask.Size = new System.Drawing.Size(718, 200);
            this.dataTask.StandardTab = true;
            this.dataTask.TabIndex = 0;
            this.dataTask.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataTask_MouseDown);
            this.dataTask.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataTask_CellBeginEdit);
            this.dataTask.Enter += new System.EventHandler(this.dataTask_Enter);
            this.dataTask.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataTask_CellDoubleClick);
            this.dataTask.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dataTask_MouseMove);
            this.dataTask.DoubleClick += new System.EventHandler(this.dataTask_DoubleClick);
            this.dataTask.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataTask_CellEndEdit);
            this.dataTask.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataTask_CellClick);
            this.dataTask.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataTask_KeyDown);
            this.dataTask.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataTask_CellContentClick);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rmmenuStartTask,
            this.rmmenuStopTask,
            this.rmmenuRestartTask,
            this.toolStripSeparator5,
            this.rmenuBrowserData,
            this.toolStripSeparator15,
            this.rmenuCopyTask,
            this.rmenuPasteTask,
            this.toolStripSeparator18,
            this.rmmenuEditTask,
            this.rmmenuRenameTask,
            this.rmmenuDelTask,
            this.toolStripSeparator3,
            this.rmmenuNewTask,
            this.rmenuAddPlan});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(178, 270);
            this.contextMenuStrip2.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip2_Opening);
            // 
            // rmmenuStartTask
            // 
            this.rmmenuStartTask.Image = ((System.Drawing.Image)(resources.GetObject("rmmenuStartTask.Image")));
            this.rmmenuStartTask.Name = "rmmenuStartTask";
            this.rmmenuStartTask.Size = new System.Drawing.Size(177, 22);
            this.rmmenuStartTask.Text = "启动任务";
            this.rmmenuStartTask.Click += new System.EventHandler(this.rmmenuStartTask_Click);
            // 
            // rmmenuStopTask
            // 
            this.rmmenuStopTask.Image = ((System.Drawing.Image)(resources.GetObject("rmmenuStopTask.Image")));
            this.rmmenuStopTask.Name = "rmmenuStopTask";
            this.rmmenuStopTask.Size = new System.Drawing.Size(177, 22);
            this.rmmenuStopTask.Text = "停止任务";
            this.rmmenuStopTask.Click += new System.EventHandler(this.rmmenuStopTask_Click);
            // 
            // rmmenuRestartTask
            // 
            this.rmmenuRestartTask.Image = ((System.Drawing.Image)(resources.GetObject("rmmenuRestartTask.Image")));
            this.rmmenuRestartTask.Name = "rmmenuRestartTask";
            this.rmmenuRestartTask.Size = new System.Drawing.Size(177, 22);
            this.rmmenuRestartTask.Text = "重置任务";
            this.rmmenuRestartTask.Click += new System.EventHandler(this.rmmenuRestartTask_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(174, 6);
            // 
            // rmenuBrowserData
            // 
            this.rmenuBrowserData.Name = "rmenuBrowserData";
            this.rmenuBrowserData.Size = new System.Drawing.Size(177, 22);
            this.rmenuBrowserData.Text = "查看数据";
            this.rmenuBrowserData.Click += new System.EventHandler(this.rmenuBrowserData_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(174, 6);
            // 
            // rmenuCopyTask
            // 
            this.rmenuCopyTask.Image = ((System.Drawing.Image)(resources.GetObject("rmenuCopyTask.Image")));
            this.rmenuCopyTask.Name = "rmenuCopyTask";
            this.rmenuCopyTask.Size = new System.Drawing.Size(177, 22);
            this.rmenuCopyTask.Text = "复制          Ctrl+C";
            this.rmenuCopyTask.Click += new System.EventHandler(this.rmenuCopyTask_Click);
            // 
            // rmenuPasteTask
            // 
            this.rmenuPasteTask.Image = ((System.Drawing.Image)(resources.GetObject("rmenuPasteTask.Image")));
            this.rmenuPasteTask.Name = "rmenuPasteTask";
            this.rmenuPasteTask.Size = new System.Drawing.Size(177, 22);
            this.rmenuPasteTask.Text = "粘贴          Ctrl+V";
            this.rmenuPasteTask.Click += new System.EventHandler(this.rmenuPasteTask_Click);
            // 
            // toolStripSeparator18
            // 
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            this.toolStripSeparator18.Size = new System.Drawing.Size(174, 6);
            // 
            // rmmenuEditTask
            // 
            this.rmmenuEditTask.Name = "rmmenuEditTask";
            this.rmmenuEditTask.Size = new System.Drawing.Size(177, 22);
            this.rmmenuEditTask.Text = "编辑";
            this.rmmenuEditTask.Click += new System.EventHandler(this.rmmenuEditTask_Click);
            // 
            // rmmenuRenameTask
            // 
            this.rmmenuRenameTask.Name = "rmmenuRenameTask";
            this.rmmenuRenameTask.Size = new System.Drawing.Size(177, 22);
            this.rmmenuRenameTask.Text = "重命名       F2";
            this.rmmenuRenameTask.Click += new System.EventHandler(this.rmmenuRenameTask_Click);
            // 
            // rmmenuDelTask
            // 
            this.rmmenuDelTask.Image = ((System.Drawing.Image)(resources.GetObject("rmmenuDelTask.Image")));
            this.rmmenuDelTask.Name = "rmmenuDelTask";
            this.rmmenuDelTask.Size = new System.Drawing.Size(177, 22);
            this.rmmenuDelTask.Text = "删除          Delete";
            this.rmmenuDelTask.Click += new System.EventHandler(this.rmmenuDelTask_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(174, 6);
            // 
            // rmmenuNewTask
            // 
            this.rmmenuNewTask.Image = ((System.Drawing.Image)(resources.GetObject("rmmenuNewTask.Image")));
            this.rmmenuNewTask.Name = "rmmenuNewTask";
            this.rmmenuNewTask.Size = new System.Drawing.Size(177, 22);
            this.rmmenuNewTask.Text = "新建任务";
            this.rmmenuNewTask.Click += new System.EventHandler(this.rmmenuNewTask_Click);
            // 
            // rmenuAddPlan
            // 
            this.rmenuAddPlan.Image = ((System.Drawing.Image)(resources.GetObject("rmenuAddPlan.Image")));
            this.rmenuAddPlan.Name = "rmenuAddPlan";
            this.rmenuAddPlan.Size = new System.Drawing.Size(177, 22);
            this.rmenuAddPlan.Text = "新建任务执行计划";
            this.rmenuAddPlan.Click += new System.EventHandler(this.rmenuAddPlan_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.ContextMenuStrip = this.contextMenuStrip4;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(718, 198);
            this.tabControl1.TabIndex = 13;
            this.tabControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDown);
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // contextMenuStrip4
            // 
            this.contextMenuStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rMenuExportTxt,
            this.rMenuExportExcel,
            this.toolStripSeparator4,
            this.rMenuCloseTabPage});
            this.contextMenuStrip4.Name = "contextMenuStrip4";
            this.contextMenuStrip4.Size = new System.Drawing.Size(149, 76);
            this.contextMenuStrip4.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip4_Opening);
            // 
            // rMenuExportTxt
            // 
            this.rMenuExportTxt.Name = "rMenuExportTxt";
            this.rMenuExportTxt.Size = new System.Drawing.Size(148, 22);
            this.rMenuExportTxt.Text = "导出文本";
            this.rMenuExportTxt.Click += new System.EventHandler(this.rMenuExportTxt_Click);
            // 
            // rMenuExportExcel
            // 
            this.rMenuExportExcel.Name = "rMenuExportExcel";
            this.rMenuExportExcel.Size = new System.Drawing.Size(148, 22);
            this.rMenuExportExcel.Text = "导出Excel";
            this.rMenuExportExcel.Click += new System.EventHandler(this.rMenuExportExcel_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(145, 6);
            // 
            // rMenuCloseTabPage
            // 
            this.rMenuCloseTabPage.Name = "rMenuCloseTabPage";
            this.rMenuCloseTabPage.Size = new System.Drawing.Size(148, 22);
            this.rMenuCloseTabPage.Text = "关闭此选项卡";
            this.rMenuCloseTabPage.Click += new System.EventHandler(this.rMenuCloseTabPage_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.webBrowser);
            this.tabPage1.ImageIndex = 8;
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(710, 168);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "soukey采摘";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 22);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(710, 168);
            this.webBrowser.TabIndex = 14;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip3;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "soukey采摘";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseMove);
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sToolStripMenuItem,
            this.menuMailto,
            this.toolStripSeparator7,
            this.MenuOpenMainfrm,
            this.MenuCloseSystem});
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(167, 98);
            // 
            // sToolStripMenuItem
            // 
            this.sToolStripMenuItem.Name = "sToolStripMenuItem";
            this.sToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.sToolStripMenuItem.Text = "Soukey采摘主页";
            this.sToolStripMenuItem.Click += new System.EventHandler(this.sToolStripMenuItem_Click);
            // 
            // menuMailto
            // 
            this.menuMailto.Name = "menuMailto";
            this.menuMailto.Size = new System.Drawing.Size(166, 22);
            this.menuMailto.Text = "给一孑发Email";
            this.menuMailto.Click += new System.EventHandler(this.menuMailto_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(163, 6);
            // 
            // MenuOpenMainfrm
            // 
            this.MenuOpenMainfrm.Image = ((System.Drawing.Image)(resources.GetObject("MenuOpenMainfrm.Image")));
            this.MenuOpenMainfrm.Name = "MenuOpenMainfrm";
            this.MenuOpenMainfrm.Size = new System.Drawing.Size(166, 22);
            this.MenuOpenMainfrm.Text = "打开主窗口";
            this.MenuOpenMainfrm.Click += new System.EventHandler(this.MenuOpenMainfrm_Click);
            // 
            // MenuCloseSystem
            // 
            this.MenuCloseSystem.Name = "MenuCloseSystem";
            this.MenuCloseSystem.Size = new System.Drawing.Size(166, 22);
            this.MenuCloseSystem.Text = "退出";
            this.MenuCloseSystem.Click += new System.EventHandler(this.MenuCloseSystem_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.服务ToolStripMenuItem,
            this.Soukey采摘ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(891, 25);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 服务ToolStripMenuItem
            // 
            this.服务ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolMenuDownloadTask,
            this.toolStripSeparator12,
            this.toolmenuConfig,
            this.toolMenuLanguage,
            this.toolStripSeparator13,
            this.toolMenuExit});
            this.服务ToolStripMenuItem.Name = "服务ToolStripMenuItem";
            this.服务ToolStripMenuItem.Size = new System.Drawing.Size(101, 21);
            this.服务ToolStripMenuItem.Text = "Soukey采摘(&S)";
            // 
            // toolMenuDownloadTask
            // 
            this.toolMenuDownloadTask.Name = "toolMenuDownloadTask";
            this.toolMenuDownloadTask.Size = new System.Drawing.Size(165, 22);
            this.toolMenuDownloadTask.Text = "下载任务模板(&D)";
            this.toolMenuDownloadTask.Click += new System.EventHandler(this.toolMenuDownloadTask_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(162, 6);
            // 
            // toolmenuConfig
            // 
            this.toolmenuConfig.Name = "toolmenuConfig";
            this.toolmenuConfig.Size = new System.Drawing.Size(165, 22);
            this.toolmenuConfig.Text = "参数设置(&S)";
            this.toolmenuConfig.Click += new System.EventHandler(this.toolmenuConfig_Click);
            // 
            // toolMenuLanguage
            // 
            this.toolMenuLanguage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.中文简体ToolStripMenuItem});
            this.toolMenuLanguage.Name = "toolMenuLanguage";
            this.toolMenuLanguage.Size = new System.Drawing.Size(165, 22);
            this.toolMenuLanguage.Text = "Language(&L)";
            // 
            // 中文简体ToolStripMenuItem
            // 
            this.中文简体ToolStripMenuItem.Checked = true;
            this.中文简体ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.中文简体ToolStripMenuItem.Name = "中文简体ToolStripMenuItem";
            this.中文简体ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.中文简体ToolStripMenuItem.Text = "中文简体";
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(162, 6);
            // 
            // toolMenuExit
            // 
            this.toolMenuExit.Name = "toolMenuExit";
            this.toolMenuExit.Size = new System.Drawing.Size(165, 22);
            this.toolMenuExit.Text = "退出(&E)";
            this.toolMenuExit.Click += new System.EventHandler(this.toolMenuExit_Click);
            // 
            // Soukey采摘ToolStripMenuItem
            // 
            this.Soukey采摘ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolMenuProgram,
            this.toolStripSeparator2,
            this.toolMenuVisityijie,
            this.toolMenuUpdate,
            this.toolStripSeparator6,
            this.toolMenuAbout});
            this.Soukey采摘ToolStripMenuItem.Name = "Soukey采摘ToolStripMenuItem";
            this.Soukey采摘ToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.Soukey采摘ToolStripMenuItem.Text = "帮助(&H)";
            // 
            // toolMenuProgram
            // 
            this.toolMenuProgram.Name = "toolMenuProgram";
            this.toolMenuProgram.Size = new System.Drawing.Size(182, 22);
            this.toolMenuProgram.Text = "加入开发联盟(&P)";
            this.toolMenuProgram.Click += new System.EventHandler(this.toolMenuProgram_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(179, 6);
            // 
            // toolMenuVisityijie
            // 
            this.toolMenuVisityijie.Name = "toolMenuVisityijie";
            this.toolMenuVisityijie.Size = new System.Drawing.Size(182, 22);
            this.toolMenuVisityijie.Text = "访问Soukey官方(&V)";
            this.toolMenuVisityijie.Click += new System.EventHandler(this.toolMenuVisityijie_Click);
            // 
            // toolMenuUpdate
            // 
            this.toolMenuUpdate.Name = "toolMenuUpdate";
            this.toolMenuUpdate.Size = new System.Drawing.Size(182, 22);
            this.toolMenuUpdate.Text = "检查更新(&U)";
            this.toolMenuUpdate.Click += new System.EventHandler(this.toolMenuUpdate_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(179, 6);
            // 
            // toolMenuAbout
            // 
            this.toolMenuAbout.Name = "toolMenuAbout";
            this.toolMenuAbout.Size = new System.Drawing.Size(182, 22);
            this.toolMenuAbout.Text = "关于...(&A)";
            this.toolMenuAbout.Click += new System.EventHandler(this.toolMenuAbout_Click);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // contextMenuStrip5
            // 
            this.contextMenuStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rmenuSaveLog});
            this.contextMenuStrip5.Name = "contextMenuStrip5";
            this.contextMenuStrip5.Size = new System.Drawing.Size(173, 26);
            // 
            // rmenuSaveLog
            // 
            this.rmenuSaveLog.Image = ((System.Drawing.Image)(resources.GetObject("rmenuSaveLog.Image")));
            this.rmenuSaveLog.Name = "rmenuSaveLog";
            this.rmenuSaveLog.Size = new System.Drawing.Size(172, 22);
            this.rmenuSaveLog.Text = "保存数据采集日志";
            this.rmenuSaveLog.Click += new System.EventHandler(this.rmenuSaveLog_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 478);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Soukey采摘 V1.60 [免费 开源]";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataTask)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.contextMenuStrip4.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.contextMenuStrip3.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStartTask;
        private System.Windows.Forms.ToolStripButton toolRestartTask;
        private System.Windows.Forms.ToolStripButton toolStopTask;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolDelTask;
        private System.Windows.Forms.ToolStripSplitButton toolExportData;
        private System.Windows.Forms.ToolStripMenuItem toolExportTxt;
        private System.Windows.Forms.ToolStripMenuItem toolExportExcel;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeMenu;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem rmenuAddTaskClass;
        private System.Windows.Forms.ToolStripMenuItem rmenuDelTaskClass;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem rmmenuStartTask;
        private System.Windows.Forms.ToolStripMenuItem rmmenuRestartTask;
        private System.Windows.Forms.ToolStripMenuItem rmmenuStopTask;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripStatusLabel staIsInternet;
        private System.Windows.Forms.ToolStripStatusLabel StateInfo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripMenuItem rmmenuNewTask;
        private System.Windows.Forms.ToolStripMenuItem rmmenuEditTask;
        private System.Windows.Forms.ToolStripMenuItem rmmenuDelTask;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem MenuOpenMainfrm;
        private System.Windows.Forms.ToolStripMenuItem MenuCloseSystem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip4;
        private System.Windows.Forms.ToolStripMenuItem rMenuExportTxt;
        private System.Windows.Forms.ToolStripMenuItem rMenuExportExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem rMenuCloseTabPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Soukey采摘ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolMenuProgram;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolMenuVisityijie;
        private System.Windows.Forms.ToolStripMenuItem toolMenuAbout;
        private System.Windows.Forms.ToolStripMenuItem 服务ToolStripMenuItem;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ToolStripMenuItem rmenuBrowserData;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolBrowserData;
        private System.Windows.Forms.ToolStripMenuItem toolMenuDownloadTask;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripStatusLabel PrograBarTxt;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem sToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuMailto;
        private System.Windows.Forms.ToolStripDropDownButton toolManage;
        private System.Windows.Forms.ToolStripMenuItem toolImportTask;
        private System.Windows.Forms.ToolStripMenuItem toolManageDict;
        private System.Windows.Forms.ToolStripMenuItem toolLookInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdCloseInfo;
        private System.Windows.Forms.ToolStripProgressBar ExportProbar;
        private System.Windows.Forms.ToolStripMenuItem toolMenuUpdate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip5;
        private System.Windows.Forms.ToolStripMenuItem rmenuSaveLog;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem toolWebbrowser;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripMenuItem toolMenuLanguage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripMenuItem toolMenuExit;
        private System.Windows.Forms.ToolStripMenuItem 中文简体ToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolNewTask;
        private System.Windows.Forms.ToolStripMenuItem toolmenuNewTask;
        private System.Windows.Forms.ToolStripMenuItem toolMenuNewTaskPlan;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripMenuItem toolmenuNewTaskClass;
        private System.Windows.Forms.ToolStripMenuItem rmenuAddPlan;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
        private System.Windows.Forms.ToolStripMenuItem menuAddTask;
        private System.Windows.Forms.ToolStripMenuItem menuAddTaskPlan;
        private System.Windows.Forms.ToolStripMenuItem toolUpgradeTask;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
        private System.Windows.Forms.ToolStripMenuItem toolmenuConfig;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.ToolStripMenuItem rmenuRenameTaskClass;
        private System.Windows.Forms.ToolStripButton toolCopyTask;
        private System.Windows.Forms.ToolStripButton toolPasteTask;
        private System.Windows.Forms.ToolStripMenuItem rmmenuRenameTask;
        private System.Windows.Forms.ToolStripMenuItem rmenuCopyTask;
        private System.Windows.Forms.ToolStripMenuItem rmenuPasteTask;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator18;
        private System.Windows.Forms.DataGridView  dataTask;
    }
}