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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuDelDictClass = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuAddDict = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDelDict = new System.Windows.Forms.ToolStripMenuItem();
            this.comDictClass = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDictClassName = new System.Windows.Forms.TextBox();
            this.listDict = new System.Windows.Forms.ListView();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.button3 = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDelDictClass,
            this.toolStripSeparator1,
            this.menuAddDict,
            this.menuDelDict});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(245, 76);
            // 
            // menuDelDictClass
            // 
            this.menuDelDictClass.Name = "menuDelDictClass";
            this.menuDelDictClass.Size = new System.Drawing.Size(244, 22);
            this.menuDelDictClass.Text = "删除字典分类及此分类下的内容";
            this.menuDelDictClass.Click += new System.EventHandler(this.menuDelDictClass_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(241, 6);
            // 
            // menuAddDict
            // 
            this.menuAddDict.Name = "menuAddDict";
            this.menuAddDict.Size = new System.Drawing.Size(244, 22);
            this.menuAddDict.Text = "添加字典内容";
            this.menuAddDict.Click += new System.EventHandler(this.menuAddDict_Click);
            // 
            // menuDelDict
            // 
            this.menuDelDict.Name = "menuDelDict";
            this.menuDelDict.Size = new System.Drawing.Size(244, 22);
            this.menuDelDict.Text = "删除字典内容";
            this.menuDelDict.Click += new System.EventHandler(this.menuDelDict_Click);
            // 
            // comDictClass
            // 
            this.comDictClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comDictClass.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comDictClass.FormattingEnabled = true;
            this.comDictClass.Location = new System.Drawing.Point(58, 11);
            this.comDictClass.Name = "comDictClass";
            this.comDictClass.Size = new System.Drawing.Size(313, 21);
            this.comDictClass.TabIndex = 1;
            this.comDictClass.SelectedIndexChanged += new System.EventHandler(this.comDictClass_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "分类：";
            // 
            // txtDictClassName
            // 
            this.txtDictClassName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDictClassName.Location = new System.Drawing.Point(13, 39);
            this.txtDictClassName.Name = "txtDictClassName";
            this.txtDictClassName.Size = new System.Drawing.Size(289, 20);
            this.txtDictClassName.TabIndex = 3;
            // 
            // listDict
            // 
            this.listDict.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listDict.HideSelection = false;
            this.listDict.LabelEdit = true;
            this.listDict.Location = new System.Drawing.Point(13, 68);
            this.listDict.MultiSelect = false;
            this.listDict.Name = "listDict";
            this.listDict.Size = new System.Drawing.Size(358, 210);
            this.listDict.TabIndex = 4;
            this.listDict.UseCompatibleStateImageBehavior = false;
            this.listDict.View = System.Windows.Forms.View.List;
            this.listDict.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listDict_AfterLabelEdit);
            this.listDict.SelectedIndexChanged += new System.EventHandler(this.listDict_SelectedIndexChanged);
            this.listDict.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listDict_KeyDown);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(298, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "添加分类";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(2, 286);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(374, 5);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(301, 297);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(70, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "关 闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // button3
            // 
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(213, 297);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(70, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "操 作";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // frmDict
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 329);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listDict);
            this.Controls.Add(this.txtDictClassName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comDictClass);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDict";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "参数字典表管理";
            this.Load += new System.EventHandler(this.frmDict_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmDict_FormClosed);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private global::System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private global::System.Windows.Forms.ToolStripMenuItem menuDelDictClass;
        private global::System.Windows.Forms.ToolStripMenuItem menuDelDict;
        private global::System.Windows.Forms.ToolStripMenuItem menuAddDict;
        private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ComboBox comDictClass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDictClassName;
        private System.Windows.Forms.ListView listDict;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button button3;
    }
}