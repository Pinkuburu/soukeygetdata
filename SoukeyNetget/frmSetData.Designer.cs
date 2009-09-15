namespace SoukeyNetget
{
    partial class frmSetData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetData));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtAccessPwd = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAccessUser = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtAccessName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comSqlServerData = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSqlServerPwd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSqlServerUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.txtSqlserver = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.comMySqlCode = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.comMySqlData = new System.Windows.Forms.ComboBox();
            this.txtMySqlPwd = new System.Windows.Forms.TextBox();
            this.txtMySqlUser = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMySqlNumber = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMySql = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtAccessPwd);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtAccessUser);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.txtAccessName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(413, 118);
            this.panel1.TabIndex = 0;
            // 
            // txtAccessPwd
            // 
            this.txtAccessPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAccessPwd.Enabled = false;
            this.txtAccessPwd.Location = new System.Drawing.Point(261, 85);
            this.txtAccessPwd.Name = "txtAccessPwd";
            this.txtAccessPwd.PasswordChar = '*';
            this.txtAccessPwd.Size = new System.Drawing.Size(133, 20);
            this.txtAccessPwd.TabIndex = 33;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(218, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "密码：";
            // 
            // txtAccessUser
            // 
            this.txtAccessUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAccessUser.Enabled = false;
            this.txtAccessUser.Location = new System.Drawing.Point(79, 85);
            this.txtAccessUser.Name = "txtAccessUser";
            this.txtAccessUser.Size = new System.Drawing.Size(133, 20);
            this.txtAccessUser.TabIndex = 32;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 87);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 31;
            this.label7.Text = "用户名：";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(79, 56);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(86, 17);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "需进行验证";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(79, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "浏览...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtAccessName
            // 
            this.txtAccessName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAccessName.Location = new System.Drawing.Point(79, 8);
            this.txtAccessName.Name = "txtAccessName";
            this.txtAccessName.Size = new System.Drawing.Size(315, 20);
            this.txtAccessName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据库文件：";
            // 
            // button9
            // 
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Location = new System.Drawing.Point(205, 168);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(70, 23);
            this.button9.TabIndex = 35;
            this.button9.Text = "测试连接";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.comSqlServerData);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtSqlServerPwd);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtSqlServerUser);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.radioButton2);
            this.panel2.Controls.Add(this.radioButton1);
            this.panel2.Controls.Add(this.txtSqlserver);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(413, 133);
            this.panel2.TabIndex = 36;
            // 
            // comSqlServerData
            // 
            this.comSqlServerData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comSqlServerData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comSqlServerData.FormattingEnabled = true;
            this.comSqlServerData.Location = new System.Drawing.Point(78, 96);
            this.comSqlServerData.Name = "comSqlServerData";
            this.comSqlServerData.Size = new System.Drawing.Size(134, 21);
            this.comSqlServerData.TabIndex = 9;
            this.comSqlServerData.DropDown += new System.EventHandler(this.comSqlServerData_DropDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "数据库：";
            // 
            // txtSqlServerPwd
            // 
            this.txtSqlServerPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSqlServerPwd.Enabled = false;
            this.txtSqlServerPwd.Location = new System.Drawing.Point(261, 65);
            this.txtSqlServerPwd.Name = "txtSqlServerPwd";
            this.txtSqlServerPwd.PasswordChar = '*';
            this.txtSqlServerPwd.Size = new System.Drawing.Size(134, 20);
            this.txtSqlServerPwd.TabIndex = 7;
            this.txtSqlServerPwd.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(217, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "密码：";
            // 
            // txtSqlServerUser
            // 
            this.txtSqlServerUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSqlServerUser.Enabled = false;
            this.txtSqlServerUser.Location = new System.Drawing.Point(78, 64);
            this.txtSqlServerUser.Name = "txtSqlServerUser";
            this.txtSqlServerUser.Size = new System.Drawing.Size(134, 20);
            this.txtSqlServerUser.TabIndex = 5;
            this.txtSqlServerUser.Text = "sa";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "用户名：";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(220, 37);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(141, 17);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.Text = "MS SqlServer 身份验证";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(78, 37);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(120, 17);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Windows 身份验证";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // txtSqlserver
            // 
            this.txtSqlserver.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSqlserver.Location = new System.Drawing.Point(78, 11);
            this.txtSqlserver.Name = "txtSqlserver";
            this.txtSqlserver.Size = new System.Drawing.Size(317, 20);
            this.txtSqlserver.TabIndex = 1;
            this.txtSqlserver.Text = "localhost";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "服务器：";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.comMySqlCode);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.comMySqlData);
            this.panel3.Controls.Add(this.txtMySqlPwd);
            this.panel3.Controls.Add(this.txtMySqlUser);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.txtMySqlNumber);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.txtMySql);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Location = new System.Drawing.Point(12, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(413, 134);
            this.panel3.TabIndex = 37;
            // 
            // comMySqlCode
            // 
            this.comMySqlCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comMySqlCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comMySqlCode.FormattingEnabled = true;
            this.comMySqlCode.Location = new System.Drawing.Point(255, 94);
            this.comMySqlCode.Name = "comMySqlCode";
            this.comMySqlCode.Size = new System.Drawing.Size(119, 21);
            this.comMySqlCode.TabIndex = 12;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(207, 99);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(43, 13);
            this.label13.TabIndex = 11;
            this.label13.Text = "编码：";
            // 
            // comMySqlData
            // 
            this.comMySqlData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comMySqlData.FormattingEnabled = true;
            this.comMySqlData.Location = new System.Drawing.Point(79, 94);
            this.comMySqlData.Name = "comMySqlData";
            this.comMySqlData.Size = new System.Drawing.Size(119, 21);
            this.comMySqlData.TabIndex = 10;
            this.comMySqlData.DropDown += new System.EventHandler(this.comMySqlData_DropDown);
            // 
            // txtMySqlPwd
            // 
            this.txtMySqlPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMySqlPwd.Location = new System.Drawing.Point(255, 66);
            this.txtMySqlPwd.Name = "txtMySqlPwd";
            this.txtMySqlPwd.PasswordChar = '*';
            this.txtMySqlPwd.Size = new System.Drawing.Size(119, 20);
            this.txtMySqlPwd.TabIndex = 9;
            // 
            // txtMySqlUser
            // 
            this.txtMySqlUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMySqlUser.Location = new System.Drawing.Point(79, 66);
            this.txtMySqlUser.Name = "txtMySqlUser";
            this.txtMySqlUser.Size = new System.Drawing.Size(119, 20);
            this.txtMySqlUser.TabIndex = 8;
            this.txtMySqlUser.Text = "root";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(18, 94);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "数据库：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(207, 68);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "密码：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 68);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "用户名：";
            // 
            // txtMySqlNumber
            // 
            this.txtMySqlNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMySqlNumber.Location = new System.Drawing.Point(79, 37);
            this.txtMySqlNumber.Name = "txtMySqlNumber";
            this.txtMySqlNumber.Size = new System.Drawing.Size(49, 20);
            this.txtMySqlNumber.TabIndex = 3;
            this.txtMySqlNumber.Text = "3306";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "端口号：";
            // 
            // txtMySql
            // 
            this.txtMySql.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMySql.Location = new System.Drawing.Point(79, 9);
            this.txtMySql.Name = "txtMySql";
            this.txtMySql.Size = new System.Drawing.Size(295, 20);
            this.txtMySql.TabIndex = 1;
            this.txtMySql.Text = "localhost";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "服务器：";
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(288, 168);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(60, 23);
            this.button2.TabIndex = 38;
            this.button2.Text = "确 定";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(362, 168);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(60, 23);
            this.button3.TabIndex = 39;
            this.button3.Text = "取 消";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(1, 151);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(507, 5);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            // 
            // frmSetData
            // 
            this.AcceptButton = this.button2;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button3;
            this.ClientSize = new System.Drawing.Size(434, 203);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置数据库连接信息";
            this.Load += new System.EventHandler(this.frmSetData_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtAccessName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAccessPwd;
        private System.Windows.Forms.TextBox txtAccessUser;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.TextBox txtSqlserver;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSqlServerPwd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSqlServerUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comSqlServerData;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtMySqlPwd;
        private System.Windows.Forms.TextBox txtMySqlUser;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtMySqlNumber;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMySql;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comMySqlData;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comMySqlCode;
        private System.Windows.Forms.Label label13;
    }
}