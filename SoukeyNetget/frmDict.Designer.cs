namespace SoukeyNetget
{
    partial class frmDict
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDict));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("参数字典", 0, 0);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuAddDictClass = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddDict = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuDelDictClass = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDelDict = new System.Windows.Forms.ToolStripMenuItem();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolAddDictClass = new System.Windows.Forms.ToolStripMenuItem();
            this.toolAddDict = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeDict = new System.Windows.Forms.TreeView();
            this.listDict = new System.Windows.Forms.ListView();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAddDictClass,
            this.menuAddDict,
            this.toolStripSeparator1,
            this.menuDelDictClass,
            this.menuDelDict});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(245, 98);
            // 
            // menuAddDictClass
            // 
            this.menuAddDictClass.Name = "menuAddDictClass";
            this.menuAddDictClass.Size = new System.Drawing.Size(244, 22);
            this.menuAddDictClass.Text = "添加字典分类";
            this.menuAddDictClass.Click += new System.EventHandler(this.menuAddDictClass_Click);
            // 
            // menuAddDict
            // 
            this.menuAddDict.Name = "menuAddDict";
            this.menuAddDict.Size = new System.Drawing.Size(244, 22);
            this.menuAddDict.Text = "添加字典内容";
            this.menuAddDict.Click += new System.EventHandler(this.menuAddDict_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(241, 6);
            // 
            // menuDelDictClass
            // 
            this.menuDelDictClass.Name = "menuDelDictClass";
            this.menuDelDictClass.Size = new System.Drawing.Size(244, 22);
            this.menuDelDictClass.Text = "删除字典分类及此分类下的内容";
            this.menuDelDictClass.Click += new System.EventHandler(this.menuDelDictClass_Click);
            // 
            // menuDelDict
            // 
            this.menuDelDict.Name = "menuDelDict";
            this.menuDelDict.Size = new System.Drawing.Size(244, 22);
            this.menuDelDict.Text = "删除字典内容";
            this.menuDelDict.Click += new System.EventHandler(this.menuDelDict_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripSeparator2,
            this.toolStripButton3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(706, 25);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolAddDictClass,
            this.toolAddDict});
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(61, 22);
            this.toolStripButton1.Text = "添加";
            // 
            // toolAddDictClass
            // 
            this.toolAddDictClass.Name = "toolAddDictClass";
            this.toolAddDictClass.Size = new System.Drawing.Size(148, 22);
            this.toolAddDictClass.Text = "添加字典分类";
            this.toolAddDictClass.Click += new System.EventHandler(this.toolAddDictClass_Click);
            // 
            // toolAddDict
            // 
            this.toolAddDict.Name = "toolAddDict";
            this.toolAddDict.Size = new System.Drawing.Size(152, 22);
            this.toolAddDict.Text = "添加字典内容";
            this.toolAddDict.Click += new System.EventHandler(this.toolAddDict_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton2.Text = "删除";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton3.Text = "退出";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 411);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(706, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(92, 17);
            this.toolStripStatusLabel1.Text = "当前状态：就绪";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "tree.gif");
            this.imageList1.Images.SetKeyName(1, "Cur.ico");
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeDict);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listDict);
            this.splitContainer1.Size = new System.Drawing.Size(706, 386);
            this.splitContainer1.SplitterDistance = 188;
            this.splitContainer1.TabIndex = 12;
            // 
            // treeDict
            // 
            this.treeDict.ContextMenuStrip = this.contextMenuStrip1;
            this.treeDict.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeDict.HideSelection = false;
            this.treeDict.ImageIndex = 0;
            this.treeDict.ImageList = this.imageList1;
            this.treeDict.Location = new System.Drawing.Point(0, 0);
            this.treeDict.Name = "treeDict";
            treeNode1.ImageIndex = 0;
            treeNode1.Name = "nodDict";
            treeNode1.SelectedImageIndex = 0;
            treeNode1.Text = "参数字典";
            this.treeDict.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeDict.SelectedImageIndex = 0;
            this.treeDict.ShowRootLines = false;
            this.treeDict.Size = new System.Drawing.Size(188, 386);
            this.treeDict.TabIndex = 11;
            this.treeDict.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeDict_AfterLabelEdit);
            this.treeDict.Enter += new System.EventHandler(this.treeDict_Enter);
            this.treeDict.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeDict_AfterSelect);
            this.treeDict.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeDict_KeyDown);
            // 
            // listDict
            // 
            this.listDict.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listDict.ContextMenuStrip = this.contextMenuStrip1;
            this.listDict.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listDict.HideSelection = false;
            this.listDict.Location = new System.Drawing.Point(0, 0);
            this.listDict.MultiSelect = false;
            this.listDict.Name = "listDict";
            this.listDict.Size = new System.Drawing.Size(514, 386);
            this.listDict.TabIndex = 4;
            this.listDict.UseCompatibleStateImageBehavior = false;
            this.listDict.View = System.Windows.Forms.View.List;
            this.listDict.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listDict_AfterLabelEdit);
            this.listDict.Enter += new System.EventHandler(this.listDict_Enter);
            this.listDict.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listDict_KeyDown);
            // 
            // frmDict
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 433);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "frmDict";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "参数字典表管理";
            this.Load += new System.EventHandler(this.frmDict_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private global::System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private global::System.Windows.Forms.ToolStripMenuItem menuDelDictClass;
        private global::System.Windows.Forms.ToolStripMenuItem menuDelDict;
        private global::System.Windows.Forms.ToolStripMenuItem menuAddDict;
        private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeDict;
        private System.Windows.Forms.ListView listDict;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem toolAddDictClass;
        private System.Windows.Forms.ToolStripMenuItem toolAddDict;
        private System.Windows.Forms.ToolStripMenuItem menuAddDictClass;
    }
}