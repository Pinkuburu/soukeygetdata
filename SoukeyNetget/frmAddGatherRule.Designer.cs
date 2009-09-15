namespace SoukeyNetget
{
    partial class frmAddGatherRule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddGatherRule));
            this.label5 = new System.Windows.Forms.Label();
            this.txtExpression = new System.Windows.Forms.TextBox();
            this.txtRegion = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.comExportLimit = new System.Windows.Forms.ComboBox();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.comGetType = new System.Windows.Forms.ComboBox();
            this.label32 = new System.Windows.Forms.Label();
            this.comLimit = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtGetEnd = new System.Windows.Forms.TextBox();
            this.txtGetStart = new System.Windows.Forms.TextBox();
            this.txtGetTitleName = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label5.Location = new System.Drawing.Point(99, 211);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(412, 13);
            this.label5.TabIndex = 80;
            this.label5.Text = "\"数据输出加工”可以提高采集数据的精确性及有效性，但也会降低采集性能。";
            // 
            // txtExpression
            // 
            this.txtExpression.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExpression.Enabled = false;
            this.txtExpression.Location = new System.Drawing.Point(366, 185);
            this.txtExpression.Name = "txtExpression";
            this.txtExpression.Size = new System.Drawing.Size(162, 20);
            this.txtExpression.TabIndex = 7;
            // 
            // txtRegion
            // 
            this.txtRegion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRegion.Enabled = false;
            this.txtRegion.Location = new System.Drawing.Point(366, 154);
            this.txtRegion.Name = "txtRegion";
            this.txtRegion.Size = new System.Drawing.Size(162, 20);
            this.txtRegion.TabIndex = 5;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(293, 189);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(67, 13);
            this.label37.TabIndex = 77;
            this.label37.Text = "加工条件：";
            // 
            // comExportLimit
            // 
            this.comExportLimit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comExportLimit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comExportLimit.FormattingEnabled = true;
            this.comExportLimit.Location = new System.Drawing.Point(101, 185);
            this.comExportLimit.Name = "comExportLimit";
            this.comExportLimit.Size = new System.Drawing.Size(162, 21);
            this.comExportLimit.TabIndex = 6;
            this.comExportLimit.SelectedIndexChanged += new System.EventHandler(this.comExportLimit_SelectedIndexChanged);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(5, 189);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(91, 13);
            this.label36.TabIndex = 76;
            this.label36.Text = "数据输出加工：";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(281, 157);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(79, 13);
            this.label35.TabIndex = 75;
            this.label35.Text = "正则表达式：";
            // 
            // label28
            // 
            this.label28.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label28.Location = new System.Drawing.Point(269, 40);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(267, 27);
            this.label28.TabIndex = 74;
            this.label28.Text = "采集数据类型为非文本时，系统将进行文件下载操作，下载文件存储在您指定的“数据保存地址”";
            // 
            // comGetType
            // 
            this.comGetType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comGetType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comGetType.FormattingEnabled = true;
            this.comGetType.Location = new System.Drawing.Point(366, 12);
            this.comGetType.Name = "comGetType";
            this.comGetType.Size = new System.Drawing.Size(162, 21);
            this.comGetType.TabIndex = 1;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(269, 15);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(91, 13);
            this.label32.TabIndex = 73;
            this.label32.Text = "采集数据类型：";
            // 
            // comLimit
            // 
            this.comLimit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comLimit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comLimit.FormattingEnabled = true;
            this.comLimit.Location = new System.Drawing.Point(101, 154);
            this.comLimit.Name = "comLimit";
            this.comLimit.Size = new System.Drawing.Size(162, 21);
            this.comLimit.TabIndex = 4;
            this.comLimit.SelectedIndexChanged += new System.EventHandler(this.comLimit_SelectedIndexChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(29, 159);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(67, 13);
            this.label19.TabIndex = 72;
            this.label19.Text = "限制条件：";
            // 
            // txtGetEnd
            // 
            this.txtGetEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGetEnd.Location = new System.Drawing.Point(366, 77);
            this.txtGetEnd.Multiline = true;
            this.txtGetEnd.Name = "txtGetEnd";
            this.txtGetEnd.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGetEnd.Size = new System.Drawing.Size(162, 71);
            this.txtGetEnd.TabIndex = 3;
            // 
            // txtGetStart
            // 
            this.txtGetStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGetStart.Location = new System.Drawing.Point(101, 77);
            this.txtGetStart.Multiline = true;
            this.txtGetStart.Name = "txtGetStart";
            this.txtGetStart.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGetStart.Size = new System.Drawing.Size(162, 71);
            this.txtGetStart.TabIndex = 2;
            // 
            // txtGetTitleName
            // 
            this.txtGetTitleName.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.txtGetTitleName.FormattingEnabled = true;
            this.txtGetTitleName.Location = new System.Drawing.Point(101, 12);
            this.txtGetTitleName.Name = "txtGetTitleName";
            this.txtGetTitleName.Size = new System.Drawing.Size(162, 21);
            this.txtGetTitleName.TabIndex = 0;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(30, 83);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(67, 13);
            this.label16.TabIndex = 69;
            this.label16.Text = "起始位置：";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(293, 83);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(67, 13);
            this.label17.TabIndex = 70;
            this.label17.Text = "终止位置：";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 15);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(91, 13);
            this.label18.TabIndex = 71;
            this.label18.Text = "采集数据名称：";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(8, 238);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(544, 5);
            this.groupBox1.TabIndex = 81;
            this.groupBox1.TabStop = false;
            // 
            // button5
            // 
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.Location = new System.Drawing.Point(468, 249);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(60, 23);
            this.button5.TabIndex = 9;
            this.button5.Text = "取 消";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(393, 249);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "确 定";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmAddGatherRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 281);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtExpression);
            this.Controls.Add(this.txtRegion);
            this.Controls.Add(this.label37);
            this.Controls.Add(this.comExportLimit);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.comGetType);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.comLimit);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.txtGetEnd);
            this.Controls.Add(this.txtGetStart);
            this.Controls.Add(this.txtGetTitleName);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label18);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddGatherRule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "增加采集网页的采集规则";
            this.Load += new System.EventHandler(this.frmAddGatherRule_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtExpression;
        private System.Windows.Forms.TextBox txtRegion;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.ComboBox comExportLimit;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.ComboBox comGetType;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.ComboBox comLimit;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtGetEnd;
        private System.Windows.Forms.TextBox txtGetStart;
        private System.Windows.Forms.ComboBox txtGetTitleName;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button1;
    }
}