namespace DBTools
{
    partial class Form1
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
            this.btnUpdateLastpostid = new System.Windows.Forms.Button();
            this.tbAddress = new System.Windows.Forms.TextBox();
            this.tbLoginid = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.lbAddress = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbTablePrefix = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.tbDBName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pbTotal = new System.Windows.Forms.ProgressBar();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pbCurrent = new System.Windows.Forms.ProgressBar();
            this.lbCurrentPage = new System.Windows.Forms.Label();
            this.lbCurrentRecord = new System.Windows.Forms.Label();
            this.tbStartPage = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbPageSize = new System.Windows.Forms.TextBox();
            this.btnUpdateLastpostid_Nolist = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.tbStarttid = new System.Windows.Forms.TextBox();
            this.cbxNoList = new System.Windows.Forms.CheckBox();
            this.cbxRandomLastpostid = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lbErrCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnUpdateLastpostid
            // 
            this.btnUpdateLastpostid.Location = new System.Drawing.Point(392, 113);
            this.btnUpdateLastpostid.Name = "btnUpdateLastpostid";
            this.btnUpdateLastpostid.Size = new System.Drawing.Size(156, 23);
            this.btnUpdateLastpostid.TabIndex = 0;
            this.btnUpdateLastpostid.Text = "更新主题信息";
            this.btnUpdateLastpostid.UseVisualStyleBackColor = true;
            this.btnUpdateLastpostid.Click += new System.EventHandler(this.btnUpdateLastpostid_Click);
            // 
            // tbAddress
            // 
            this.tbAddress.Location = new System.Drawing.Point(59, 12);
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.Size = new System.Drawing.Size(77, 21);
            this.tbAddress.TabIndex = 1;
            this.tbAddress.Text = "127.0.0.1";
            // 
            // tbLoginid
            // 
            this.tbLoginid.Location = new System.Drawing.Point(189, 13);
            this.tbLoginid.Name = "tbLoginid";
            this.tbLoginid.Size = new System.Drawing.Size(78, 21);
            this.tbLoginid.TabIndex = 2;
            this.tbLoginid.Text = "sa";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(310, 12);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(76, 21);
            this.tbPassword.TabIndex = 3;
            // 
            // lbAddress
            // 
            this.lbAddress.AutoSize = true;
            this.lbAddress.Location = new System.Drawing.Point(12, 16);
            this.lbAddress.Name = "lbAddress";
            this.lbAddress.Size = new System.Drawing.Size(41, 12);
            this.lbAddress.TabIndex = 4;
            this.lbAddress.Text = "数据库";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(142, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "登录名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(275, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "密码";
            // 
            // tbTablePrefix
            // 
            this.tbTablePrefix.Location = new System.Drawing.Point(59, 42);
            this.tbTablePrefix.Name = "tbTablePrefix";
            this.tbTablePrefix.Size = new System.Drawing.Size(77, 21);
            this.tbTablePrefix.TabIndex = 7;
            this.tbTablePrefix.Text = "dnt_";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "表前缀";
            // 
            // tbMessage
            // 
            this.tbMessage.Location = new System.Drawing.Point(14, 69);
            this.tbMessage.Multiline = true;
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbMessage.Size = new System.Drawing.Size(372, 182);
            this.tbMessage.TabIndex = 9;
            // 
            // tbDBName
            // 
            this.tbDBName.Location = new System.Drawing.Point(449, 12);
            this.tbDBName.Name = "tbDBName";
            this.tbDBName.Size = new System.Drawing.Size(98, 21);
            this.tbDBName.TabIndex = 10;
            this.tbDBName.Text = "dnt2_dadach";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(390, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "数据库名";
            // 
            // pbTotal
            // 
            this.pbTotal.Location = new System.Drawing.Point(61, 267);
            this.pbTotal.Name = "pbTotal";
            this.pbTotal.Size = new System.Drawing.Size(100, 20);
            this.pbTotal.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 275);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "总进度";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(208, 275);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "当前进度";
            // 
            // pbCurrent
            // 
            this.pbCurrent.Location = new System.Drawing.Point(267, 267);
            this.pbCurrent.Name = "pbCurrent";
            this.pbCurrent.Size = new System.Drawing.Size(100, 20);
            this.pbCurrent.TabIndex = 15;
            // 
            // lbCurrentPage
            // 
            this.lbCurrentPage.AutoSize = true;
            this.lbCurrentPage.Location = new System.Drawing.Point(187, 275);
            this.lbCurrentPage.Name = "lbCurrentPage";
            this.lbCurrentPage.Size = new System.Drawing.Size(11, 12);
            this.lbCurrentPage.TabIndex = 16;
            this.lbCurrentPage.Text = "0";
            // 
            // lbCurrentRecord
            // 
            this.lbCurrentRecord.AutoSize = true;
            this.lbCurrentRecord.Location = new System.Drawing.Point(414, 275);
            this.lbCurrentRecord.Name = "lbCurrentRecord";
            this.lbCurrentRecord.Size = new System.Drawing.Size(11, 12);
            this.lbCurrentRecord.TabIndex = 17;
            this.lbCurrentRecord.Text = "0";
            // 
            // tbStartPage
            // 
            this.tbStartPage.Location = new System.Drawing.Point(333, 42);
            this.tbStartPage.Name = "tbStartPage";
            this.tbStartPage.Size = new System.Drawing.Size(53, 21);
            this.tbStartPage.TabIndex = 18;
            this.tbStartPage.Text = "1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(275, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 19;
            this.label7.Text = "开始页数";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(141, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 21;
            this.label8.Text = "分页";
            // 
            // tbPageSize
            // 
            this.tbPageSize.Location = new System.Drawing.Point(189, 42);
            this.tbPageSize.Name = "tbPageSize";
            this.tbPageSize.Size = new System.Drawing.Size(78, 21);
            this.tbPageSize.TabIndex = 20;
            this.tbPageSize.Text = "30000";
            // 
            // btnUpdateLastpostid_Nolist
            // 
            this.btnUpdateLastpostid_Nolist.Location = new System.Drawing.Point(392, 142);
            this.btnUpdateLastpostid_Nolist.Name = "btnUpdateLastpostid_Nolist";
            this.btnUpdateLastpostid_Nolist.Size = new System.Drawing.Size(156, 23);
            this.btnUpdateLastpostid_Nolist.TabIndex = 22;
            this.btnUpdateLastpostid_Nolist.Text = "更新主题信息(无列表)";
            this.btnUpdateLastpostid_Nolist.UseVisualStyleBackColor = true;
            this.btnUpdateLastpostid_Nolist.Click += new System.EventHandler(this.btnUpdateLastpostid_Nolist_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(391, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 24;
            this.label9.Text = "起始tid";
            // 
            // tbStarttid
            // 
            this.tbStarttid.Location = new System.Drawing.Point(449, 42);
            this.tbStarttid.Name = "tbStarttid";
            this.tbStarttid.Size = new System.Drawing.Size(53, 21);
            this.tbStarttid.TabIndex = 23;
            this.tbStarttid.Text = "1";
            // 
            // cbxNoList
            // 
            this.cbxNoList.AutoSize = true;
            this.cbxNoList.Checked = true;
            this.cbxNoList.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxNoList.Location = new System.Drawing.Point(392, 71);
            this.cbxNoList.Name = "cbxNoList";
            this.cbxNoList.Size = new System.Drawing.Size(84, 16);
            this.cbxNoList.TabIndex = 25;
            this.cbxNoList.Text = "无列表判断";
            this.cbxNoList.UseVisualStyleBackColor = true;
            this.cbxNoList.CheckedChanged += new System.EventHandler(this.cbxNoList_CheckedChanged);
            // 
            // cbxRandomLastpostid
            // 
            this.cbxRandomLastpostid.AutoSize = true;
            this.cbxRandomLastpostid.Location = new System.Drawing.Point(392, 91);
            this.cbxRandomLastpostid.Name = "cbxRandomLastpostid";
            this.cbxRandomLastpostid.Size = new System.Drawing.Size(168, 16);
            this.cbxRandomLastpostid.TabIndex = 26;
            this.cbxRandomLastpostid.Text = "随机lastpostid(避免失败)";
            this.cbxRandomLastpostid.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(455, 275);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 12);
            this.label10.TabIndex = 27;
            this.label10.Text = "错误数:";
            // 
            // lbErrCount
            // 
            this.lbErrCount.AutoSize = true;
            this.lbErrCount.Location = new System.Drawing.Point(516, 275);
            this.lbErrCount.Name = "lbErrCount";
            this.lbErrCount.Size = new System.Drawing.Size(11, 12);
            this.lbErrCount.TabIndex = 28;
            this.lbErrCount.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 299);
            this.Controls.Add(this.lbErrCount);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cbxRandomLastpostid);
            this.Controls.Add(this.cbxNoList);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbStarttid);
            this.Controls.Add(this.btnUpdateLastpostid_Nolist);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbPageSize);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbStartPage);
            this.Controls.Add(this.lbCurrentRecord);
            this.Controls.Add(this.lbCurrentPage);
            this.Controls.Add(this.pbCurrent);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pbTotal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbDBName);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbTablePrefix);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbAddress);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbLoginid);
            this.Controls.Add(this.tbAddress);
            this.Controls.Add(this.btnUpdateLastpostid);
            this.Name = "Form1";
            this.Text = "DBTools by sky 20080506 22:50";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUpdateLastpostid;
        private System.Windows.Forms.TextBox tbAddress;
        private System.Windows.Forms.TextBox tbLoginid;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label lbAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbTablePrefix;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.TextBox tbDBName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar pbTotal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ProgressBar pbCurrent;
        private System.Windows.Forms.Label lbCurrentPage;
        private System.Windows.Forms.Label lbCurrentRecord;
        private System.Windows.Forms.TextBox tbStartPage;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbPageSize;
        private System.Windows.Forms.Button btnUpdateLastpostid_Nolist;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbStarttid;
        private System.Windows.Forms.CheckBox cbxNoList;
        private System.Windows.Forms.CheckBox cbxRandomLastpostid;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbErrCount;
    }
}

