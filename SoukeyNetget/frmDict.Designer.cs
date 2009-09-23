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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeDict = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuAddDictClass = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddDict = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuDelDictClass = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDelDict = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.listDict = new System.Windows.Forms.ListView();
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
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeDict);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listDict);
            // 
            // treeDict
            // 
            this.treeDict.ContextMenuStrip = this.contextMenuStrip1;
            resources.ApplyResources(this.treeDict, "treeDict");
            this.treeDict.HideSelection = false;
            this.treeDict.ImageList = this.imageList1;
            this.treeDict.Name = "treeDict";
            this.treeDict.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            ((System.Windows.Forms.TreeNode)(resources.GetObject("treeDict.Nodes")))});
            this.treeDict.ShowRootLines = false;
            this.treeDict.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeDict_AfterLabelEdit);
            this.treeDict.Enter += new System.EventHandler(this.treeDict_Enter);
            this.treeDict.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeDict_AfterSelect);
            this.treeDict.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeDict_KeyDown);
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
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // menuAddDictClass
            // 
            this.menuAddDictClass.Name = "menuAddDictClass";
            resources.ApplyResources(this.menuAddDictClass, "menuAddDictClass");
            this.menuAddDictClass.Click += new System.EventHandler(this.menuAddDictClass_Click);
            // 
            // menuAddDict
            // 
            this.menuAddDict.Name = "menuAddDict";
            resources.ApplyResources(this.menuAddDict, "menuAddDict");
            this.menuAddDict.Click += new System.EventHandler(this.menuAddDict_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // menuDelDictClass
            // 
            this.menuDelDictClass.Name = "menuDelDictClass";
            resources.ApplyResources(this.menuDelDictClass, "menuDelDictClass");
            this.menuDelDictClass.Click += new System.EventHandler(this.menuDelDictClass_Click);
            // 
            // menuDelDict
            // 
            this.menuDelDict.Name = "menuDelDict";
            resources.ApplyResources(this.menuDelDict, "menuDelDict");
            this.menuDelDict.Click += new System.EventHandler(this.menuDelDict_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "tree.gif");
            this.imageList1.Images.SetKeyName(1, "Cur.ico");
            // 
            // listDict
            // 
            this.listDict.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listDict.ContextMenuStrip = this.contextMenuStrip1;
            resources.ApplyResources(this.listDict, "listDict");
            this.listDict.HideSelection = false;
            this.listDict.MultiSelect = false;
            this.listDict.Name = "listDict";
            this.listDict.UseCompatibleStateImageBehavior = false;
            this.listDict.View = System.Windows.Forms.View.List;
            this.listDict.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listDict_AfterLabelEdit);
            this.listDict.Enter += new System.EventHandler(this.listDict_Enter);
            this.listDict.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listDict_KeyDown);
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
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolAddDictClass,
            this.toolAddDict});
            resources.ApplyResources(this.toolStripButton1, "toolStripButton1");
            this.toolStripButton1.Name = "toolStripButton1";
            // 
            // toolAddDictClass
            // 
            this.toolAddDictClass.Name = "toolAddDictClass";
            resources.ApplyResources(this.toolAddDictClass, "toolAddDictClass");
            this.toolAddDictClass.Click += new System.EventHandler(this.toolAddDictClass_Click);
            // 
            // toolAddDict
            // 
            this.toolAddDict.Name = "toolAddDict";
            resources.ApplyResources(this.toolAddDict, "toolAddDict");
            this.toolAddDict.Click += new System.EventHandler(this.toolAddDict_Click);
            // 
            // toolStripButton2
            // 
            resources.ApplyResources(this.toolStripButton2, "toolStripButton2");
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // toolStripButton3
            // 
            resources.ApplyResources(this.toolStripButton3, "toolStripButton3");
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // frmDict
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.MinimizeBox = false;
            this.Name = "frmDict";
            this.Load += new System.EventHandler(this.frmDict_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmDict_FormClosed);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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