namespace NConvert
{
    partial class MainForm
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
            this.btnConvertStart = new System.Windows.Forms.Button();
            this.plSrcDBSetting_sql = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbxSrcPassword = new System.Windows.Forms.TextBox();
            this.tbxSrcLoginID = new System.Windows.Forms.TextBox();
            this.tbxSrcDBName = new System.Windows.Forms.TextBox();
            this.tbxSrcAddress = new System.Windows.Forms.TextBox();
            this.plTargetDBSetting = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbxTargetPassword = new System.Windows.Forms.TextBox();
            this.tbxTargetLoginID = new System.Windows.Forms.TextBox();
            this.tbxTargetDBName = new System.Windows.Forms.TextBox();
            this.tbxTargetAddress = new System.Windows.Forms.TextBox();
            this.tbxMessage = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.pbTotal = new System.Windows.Forms.ProgressBar();
            this.cbbxSrcDbType = new System.Windows.Forms.ComboBox();
            this.cbbxTargetDbType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tbxSrcDbTablePrefix = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbxTargetDbTablePrefix = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.plSrcDBSetting_acc = new System.Windows.Forms.Panel();
            this.btnBrowerAccessDBFile = new System.Windows.Forms.Button();
            this.tbxSrcAccessDBPath = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lbTotalRecord = new System.Windows.Forms.Label();
            this.lbCurrentRecord = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.pbCurrent = new System.Windows.Forms.ProgressBar();
            this.btnExit = new System.Windows.Forms.Button();
            this.cbbxConvertType = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnReadme = new System.Windows.Forms.Button();
            this.lbDllName = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ckbxIsConvertUserRecommand = new System.Windows.Forms.CheckBox();
            this.ckbxIsConvertFriend = new System.Windows.Forms.CheckBox();
            this.ckbxIsConvertUserBlogClass = new System.Windows.Forms.CheckBox();
            this.ckbxIsConvertPics = new System.Windows.Forms.CheckBox();
            this.ckbxIsConvertAblumDir = new System.Windows.Forms.CheckBox();
            this.ckbxIsConvertUserComment = new System.Windows.Forms.CheckBox();
            this.ckbxIsConvertBlogComment = new System.Windows.Forms.CheckBox();
            this.ckbxIsConvertBlogPosts = new System.Windows.Forms.CheckBox();
            this.ckbxIsConvertGroupPost = new System.Windows.Forms.CheckBox();
            this.ckbxIsConvertGroups = new System.Windows.Forms.CheckBox();
            this.ckbxConvertPollRecords = new System.Windows.Forms.CheckBox();
            this.ckbxConvertUserGroups = new System.Windows.Forms.CheckBox();
            this.ckbxUpdatePostsInfo = new System.Windows.Forms.CheckBox();
            this.ckbxResetTopicReplies = new System.Windows.Forms.CheckBox();
            this.ckbxResetTopicLastpostid = new System.Windows.Forms.CheckBox();
            this.ckbxConvertForumLinks = new System.Windows.Forms.CheckBox();
            this.ckbxConvertPms = new System.Windows.Forms.CheckBox();
            this.ckbxConvertAttachments = new System.Windows.Forms.CheckBox();
            this.ckbxConvertPolls = new System.Windows.Forms.CheckBox();
            this.ckbxConvertPosts = new System.Windows.Forms.CheckBox();
            this.ckbxConvertTopics = new System.Windows.Forms.CheckBox();
            this.ckbxConvertTopicTypes = new System.Windows.Forms.CheckBox();
            this.ckbxConvertForums = new System.Windows.Forms.CheckBox();
            this.ckbxConvertUsers = new System.Windows.Forms.CheckBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.plSrcDBSetting_sql.SuspendLayout();
            this.plTargetDBSetting.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.plSrcDBSetting_acc.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConvertStart
            // 
            this.btnConvertStart.Location = new System.Drawing.Point(403, 461);
            this.btnConvertStart.Name = "btnConvertStart";
            this.btnConvertStart.Size = new System.Drawing.Size(75, 23);
            this.btnConvertStart.TabIndex = 0;
            this.btnConvertStart.Text = "开始转换";
            this.btnConvertStart.UseVisualStyleBackColor = true;
            this.btnConvertStart.Click += new System.EventHandler(this.btnConvertStart_Click);
            // 
            // plSrcDBSetting_sql
            // 
            this.plSrcDBSetting_sql.Controls.Add(this.label8);
            this.plSrcDBSetting_sql.Controls.Add(this.label7);
            this.plSrcDBSetting_sql.Controls.Add(this.label5);
            this.plSrcDBSetting_sql.Controls.Add(this.label6);
            this.plSrcDBSetting_sql.Controls.Add(this.tbxSrcPassword);
            this.plSrcDBSetting_sql.Controls.Add(this.tbxSrcLoginID);
            this.plSrcDBSetting_sql.Controls.Add(this.tbxSrcDBName);
            this.plSrcDBSetting_sql.Controls.Add(this.tbxSrcAddress);
            this.plSrcDBSetting_sql.Location = new System.Drawing.Point(16, 39);
            this.plSrcDBSetting_sql.Name = "plSrcDBSetting_sql";
            this.plSrcDBSetting_sql.Size = new System.Drawing.Size(222, 108);
            this.plSrcDBSetting_sql.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 84);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "登录密码";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "登录用户";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "数据库IP";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "数据库名";
            // 
            // tbxSrcPassword
            // 
            this.tbxSrcPassword.Location = new System.Drawing.Point(62, 80);
            this.tbxSrcPassword.Name = "tbxSrcPassword";
            this.tbxSrcPassword.PasswordChar = '*';
            this.tbxSrcPassword.Size = new System.Drawing.Size(160, 21);
            this.tbxSrcPassword.TabIndex = 7;
            this.tbxSrcPassword.Text = "123321qq";
            // 
            // tbxSrcLoginID
            // 
            this.tbxSrcLoginID.Location = new System.Drawing.Point(62, 56);
            this.tbxSrcLoginID.Name = "tbxSrcLoginID";
            this.tbxSrcLoginID.Size = new System.Drawing.Size(160, 21);
            this.tbxSrcLoginID.TabIndex = 6;
            this.tbxSrcLoginID.Text = "sa";
            // 
            // tbxSrcDBName
            // 
            this.tbxSrcDBName.Location = new System.Drawing.Point(62, 32);
            this.tbxSrcDBName.Name = "tbxSrcDBName";
            this.tbxSrcDBName.Size = new System.Drawing.Size(160, 21);
            this.tbxSrcDBName.TabIndex = 5;
            this.tbxSrcDBName.Text = "dnt3";
            // 
            // tbxSrcAddress
            // 
            this.tbxSrcAddress.Location = new System.Drawing.Point(62, 8);
            this.tbxSrcAddress.Name = "tbxSrcAddress";
            this.tbxSrcAddress.Size = new System.Drawing.Size(160, 21);
            this.tbxSrcAddress.TabIndex = 4;
            this.tbxSrcAddress.Text = "LINUX-MYMPC\\SQLEXPRESS2008";
            // 
            // plTargetDBSetting
            // 
            this.plTargetDBSetting.Controls.Add(this.label3);
            this.plTargetDBSetting.Controls.Add(this.label2);
            this.plTargetDBSetting.Controls.Add(this.label4);
            this.plTargetDBSetting.Controls.Add(this.label9);
            this.plTargetDBSetting.Controls.Add(this.tbxTargetPassword);
            this.plTargetDBSetting.Controls.Add(this.tbxTargetLoginID);
            this.plTargetDBSetting.Controls.Add(this.tbxTargetDBName);
            this.plTargetDBSetting.Controls.Add(this.tbxTargetAddress);
            this.plTargetDBSetting.Location = new System.Drawing.Point(20, 46);
            this.plTargetDBSetting.Name = "plTargetDBSetting";
            this.plTargetDBSetting.Size = new System.Drawing.Size(221, 108);
            this.plTargetDBSetting.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "登录密码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "登录用户";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "数据库名";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 12;
            this.label9.Text = "数据库IP";
            // 
            // tbxTargetPassword
            // 
            this.tbxTargetPassword.Location = new System.Drawing.Point(61, 80);
            this.tbxTargetPassword.Name = "tbxTargetPassword";
            this.tbxTargetPassword.PasswordChar = '*';
            this.tbxTargetPassword.Size = new System.Drawing.Size(160, 21);
            this.tbxTargetPassword.TabIndex = 7;
            this.tbxTargetPassword.Text = "123321qq";
            // 
            // tbxTargetLoginID
            // 
            this.tbxTargetLoginID.Location = new System.Drawing.Point(61, 56);
            this.tbxTargetLoginID.Name = "tbxTargetLoginID";
            this.tbxTargetLoginID.Size = new System.Drawing.Size(160, 21);
            this.tbxTargetLoginID.TabIndex = 6;
            this.tbxTargetLoginID.Text = "root";
            // 
            // tbxTargetDBName
            // 
            this.tbxTargetDBName.Location = new System.Drawing.Point(61, 32);
            this.tbxTargetDBName.Name = "tbxTargetDBName";
            this.tbxTargetDBName.Size = new System.Drawing.Size(160, 21);
            this.tbxTargetDBName.TabIndex = 5;
            this.tbxTargetDBName.Text = "sn";
            // 
            // tbxTargetAddress
            // 
            this.tbxTargetAddress.Location = new System.Drawing.Point(61, 8);
            this.tbxTargetAddress.Name = "tbxTargetAddress";
            this.tbxTargetAddress.Size = new System.Drawing.Size(160, 21);
            this.tbxTargetAddress.TabIndex = 4;
            this.tbxTargetAddress.Text = "10.0.4.230";
            // 
            // tbxMessage
            // 
            this.tbxMessage.Location = new System.Drawing.Point(11, 273);
            this.tbxMessage.Multiline = true;
            this.tbxMessage.Name = "tbxMessage";
            this.tbxMessage.ReadOnly = true;
            this.tbxMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxMessage.Size = new System.Drawing.Size(550, 182);
            this.tbxMessage.TabIndex = 10;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(486, 461);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 11;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(403, 490);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 12;
            this.btnTest.Text = "测试";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // pbTotal
            // 
            this.pbTotal.Location = new System.Drawing.Point(78, 461);
            this.pbTotal.Name = "pbTotal";
            this.pbTotal.Size = new System.Drawing.Size(214, 20);
            this.pbTotal.TabIndex = 13;
            // 
            // cbbxSrcDbType
            // 
            this.cbbxSrcDbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbxSrcDbType.FormattingEnabled = true;
            this.cbbxSrcDbType.Items.AddRange(new object[] {
            "SqlServer",
            "Mysql",
            "Access"});
            this.cbbxSrcDbType.Location = new System.Drawing.Point(78, 20);
            this.cbbxSrcDbType.Name = "cbbxSrcDbType";
            this.cbbxSrcDbType.Size = new System.Drawing.Size(160, 20);
            this.cbbxSrcDbType.TabIndex = 14;
            this.cbbxSrcDbType.SelectedIndexChanged += new System.EventHandler(this.cbbxSrcDbType_SelectedIndexChanged);
            // 
            // cbbxTargetDbType
            // 
            this.cbbxTargetDbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbxTargetDbType.FormattingEnabled = true;
            this.cbbxTargetDbType.Items.AddRange(new object[] {
            "SqlServer",
            "Mysql",
            "Access"});
            this.cbbxTargetDbType.Location = new System.Drawing.Point(81, 20);
            this.cbbxTargetDbType.Name = "cbbxTargetDbType";
            this.cbbxTargetDbType.Size = new System.Drawing.Size(160, 20);
            this.cbbxTargetDbType.TabIndex = 15;
            this.cbbxTargetDbType.SelectedIndexChanged += new System.EventHandler(this.cbbxTargetDbType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "数据类型";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 17;
            this.label10.Text = "数据类型";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(19, 157);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 13;
            this.label11.Text = "表 前 缀";
            // 
            // tbxSrcDbTablePrefix
            // 
            this.tbxSrcDbTablePrefix.Location = new System.Drawing.Point(78, 153);
            this.tbxSrcDbTablePrefix.Name = "tbxSrcDbTablePrefix";
            this.tbxSrcDbTablePrefix.Size = new System.Drawing.Size(160, 21);
            this.tbxSrcDbTablePrefix.TabIndex = 12;
            this.tbxSrcDbTablePrefix.Text = "dnt_";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(23, 157);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 15;
            this.label12.Text = "表 前 缀";
            // 
            // tbxTargetDbTablePrefix
            // 
            this.tbxTargetDbTablePrefix.Location = new System.Drawing.Point(81, 153);
            this.tbxTargetDbTablePrefix.Name = "tbxTargetDbTablePrefix";
            this.tbxTargetDbTablePrefix.Size = new System.Drawing.Size(160, 21);
            this.tbxTargetDbTablePrefix.TabIndex = 14;
            this.tbxTargetDbTablePrefix.Text = "pre_";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.plSrcDBSetting_acc);
            this.groupBox1.Controls.Add(this.plSrcDBSetting_sql);
            this.groupBox1.Controls.Add(this.cbbxSrcDbType);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbxSrcDbTablePrefix);
            this.groupBox1.Location = new System.Drawing.Point(12, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(261, 188);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "源数据库信息";
            // 
            // plSrcDBSetting_acc
            // 
            this.plSrcDBSetting_acc.Controls.Add(this.btnBrowerAccessDBFile);
            this.plSrcDBSetting_acc.Controls.Add(this.tbxSrcAccessDBPath);
            this.plSrcDBSetting_acc.Controls.Add(this.label21);
            this.plSrcDBSetting_acc.Location = new System.Drawing.Point(16, 56);
            this.plSrcDBSetting_acc.Name = "plSrcDBSetting_acc";
            this.plSrcDBSetting_acc.Size = new System.Drawing.Size(222, 98);
            this.plSrcDBSetting_acc.TabIndex = 12;
            this.plSrcDBSetting_acc.Visible = false;
            // 
            // btnBrowerAccessDBFile
            // 
            this.btnBrowerAccessDBFile.Location = new System.Drawing.Point(131, 70);
            this.btnBrowerAccessDBFile.Name = "btnBrowerAccessDBFile";
            this.btnBrowerAccessDBFile.Size = new System.Drawing.Size(88, 23);
            this.btnBrowerAccessDBFile.TabIndex = 13;
            this.btnBrowerAccessDBFile.Text = "浏览文件...";
            this.btnBrowerAccessDBFile.UseVisualStyleBackColor = true;
            this.btnBrowerAccessDBFile.Click += new System.EventHandler(this.btnBrowerAccessDBFile_Click);
            // 
            // tbxSrcAccessDBPath
            // 
            this.tbxSrcAccessDBPath.Location = new System.Drawing.Point(5, 26);
            this.tbxSrcAccessDBPath.Name = "tbxSrcAccessDBPath";
            this.tbxSrcAccessDBPath.Size = new System.Drawing.Size(214, 21);
            this.tbxSrcAccessDBPath.TabIndex = 12;
            this.tbxSrcAccessDBPath.Text = "D:\\database\\dbconvert\\angelsu\\db.mdb";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(3, 11);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(89, 12);
            this.label21.TabIndex = 8;
            this.label21.Text = "数据库文件地址";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.plTargetDBSetting);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.cbbxTargetDbType);
            this.groupBox2.Controls.Add(this.tbxTargetDbTablePrefix);
            this.groupBox2.Location = new System.Drawing.Point(300, 74);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(261, 188);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "新数据库信息";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(14, 466);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 20;
            this.label13.Text = "总 进 度";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(298, 466);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(29, 12);
            this.label14.TabIndex = 21;
            this.label14.Text = "总数";
            // 
            // lbTotalRecord
            // 
            this.lbTotalRecord.AutoSize = true;
            this.lbTotalRecord.Location = new System.Drawing.Point(333, 466);
            this.lbTotalRecord.Name = "lbTotalRecord";
            this.lbTotalRecord.Size = new System.Drawing.Size(11, 12);
            this.lbTotalRecord.TabIndex = 22;
            this.lbTotalRecord.Text = "0";
            // 
            // lbCurrentRecord
            // 
            this.lbCurrentRecord.AutoSize = true;
            this.lbCurrentRecord.Location = new System.Drawing.Point(333, 495);
            this.lbCurrentRecord.Name = "lbCurrentRecord";
            this.lbCurrentRecord.Size = new System.Drawing.Size(11, 12);
            this.lbCurrentRecord.TabIndex = 26;
            this.lbCurrentRecord.Text = "0";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(298, 495);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 12);
            this.label16.TabIndex = 25;
            this.label16.Text = "当前";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(14, 495);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 12);
            this.label17.TabIndex = 24;
            this.label17.Text = "当前进度";
            // 
            // pbCurrent
            // 
            this.pbCurrent.Location = new System.Drawing.Point(78, 490);
            this.pbCurrent.Name = "pbCurrent";
            this.pbCurrent.Size = new System.Drawing.Size(214, 20);
            this.pbCurrent.TabIndex = 23;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(486, 490);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 27;
            this.btnExit.Text = "关闭退出";
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // cbbxConvertType
            // 
            this.cbbxConvertType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbxConvertType.FormattingEnabled = true;
            this.cbbxConvertType.Location = new System.Drawing.Point(78, 20);
            this.cbbxConvertType.Name = "cbbxConvertType";
            this.cbbxConvertType.Size = new System.Drawing.Size(183, 20);
            this.cbbxConvertType.TabIndex = 28;
            this.cbbxConvertType.SelectionChangeCommitted += new System.EventHandler(this.cbbxConvertType_SelectionChangeCommitted);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(19, 23);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 17;
            this.label15.Text = "转换类型";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnReadme);
            this.groupBox3.Controls.Add(this.lbDllName);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.cbbxConvertType);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(549, 56);
            this.groupBox3.TabIndex = 29;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "基本设置";
            // 
            // btnReadme
            // 
            this.btnReadme.Location = new System.Drawing.Point(454, 20);
            this.btnReadme.Name = "btnReadme";
            this.btnReadme.Size = new System.Drawing.Size(75, 23);
            this.btnReadme.TabIndex = 31;
            this.btnReadme.Text = "注意事项";
            this.btnReadme.UseVisualStyleBackColor = true;
            this.btnReadme.Click += new System.EventHandler(this.btnReadme_Click);
            // 
            // lbDllName
            // 
            this.lbDllName.AutoSize = true;
            this.lbDllName.Location = new System.Drawing.Point(327, 28);
            this.lbDllName.Name = "lbDllName";
            this.lbDllName.Size = new System.Drawing.Size(59, 12);
            this.lbDllName.TabIndex = 30;
            this.lbDllName.Text = "lbDllName";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(286, 28);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(35, 12);
            this.label18.TabIndex = 29;
            this.label18.Text = "实体:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkBox3);
            this.groupBox4.Controls.Add(this.checkBox5);
            this.groupBox4.Controls.Add(this.checkBox1);
            this.groupBox4.Controls.Add(this.checkBox2);
            this.groupBox4.Controls.Add(this.ckbxIsConvertUserRecommand);
            this.groupBox4.Controls.Add(this.ckbxIsConvertFriend);
            this.groupBox4.Controls.Add(this.ckbxIsConvertUserBlogClass);
            this.groupBox4.Controls.Add(this.ckbxIsConvertPics);
            this.groupBox4.Controls.Add(this.ckbxIsConvertAblumDir);
            this.groupBox4.Controls.Add(this.ckbxIsConvertUserComment);
            this.groupBox4.Controls.Add(this.ckbxIsConvertBlogComment);
            this.groupBox4.Controls.Add(this.ckbxIsConvertBlogPosts);
            this.groupBox4.Controls.Add(this.ckbxIsConvertGroupPost);
            this.groupBox4.Controls.Add(this.ckbxIsConvertGroups);
            this.groupBox4.Controls.Add(this.ckbxConvertPollRecords);
            this.groupBox4.Controls.Add(this.ckbxConvertUserGroups);
            this.groupBox4.Controls.Add(this.ckbxUpdatePostsInfo);
            this.groupBox4.Controls.Add(this.ckbxResetTopicReplies);
            this.groupBox4.Controls.Add(this.ckbxResetTopicLastpostid);
            this.groupBox4.Controls.Add(this.ckbxConvertForumLinks);
            this.groupBox4.Controls.Add(this.ckbxConvertPms);
            this.groupBox4.Controls.Add(this.ckbxConvertAttachments);
            this.groupBox4.Controls.Add(this.ckbxConvertPolls);
            this.groupBox4.Controls.Add(this.ckbxConvertPosts);
            this.groupBox4.Controls.Add(this.ckbxConvertTopics);
            this.groupBox4.Controls.Add(this.ckbxConvertTopicTypes);
            this.groupBox4.Controls.Add(this.ckbxConvertForums);
            this.groupBox4.Controls.Add(this.ckbxConvertUsers);
            this.groupBox4.Location = new System.Drawing.Point(567, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(185, 501);
            this.groupBox4.TabIndex = 30;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "转换项目";
            // 
            // ckbxIsConvertUserRecommand
            // 
            this.ckbxIsConvertUserRecommand.AutoSize = true;
            this.ckbxIsConvertUserRecommand.Location = new System.Drawing.Point(5, 235);
            this.ckbxIsConvertUserRecommand.Name = "ckbxIsConvertUserRecommand";
            this.ckbxIsConvertUserRecommand.Size = new System.Drawing.Size(72, 16);
            this.ckbxIsConvertUserRecommand.TabIndex = 23;
            this.ckbxIsConvertUserRecommand.Text = "博主推荐";
            this.ckbxIsConvertUserRecommand.UseVisualStyleBackColor = true;
            // 
            // ckbxIsConvertFriend
            // 
            this.ckbxIsConvertFriend.AutoSize = true;
            this.ckbxIsConvertFriend.Location = new System.Drawing.Point(95, 212);
            this.ckbxIsConvertFriend.Name = "ckbxIsConvertFriend";
            this.ckbxIsConvertFriend.Size = new System.Drawing.Size(72, 16);
            this.ckbxIsConvertFriend.TabIndex = 22;
            this.ckbxIsConvertFriend.Text = "转换好友";
            this.ckbxIsConvertFriend.UseVisualStyleBackColor = true;
            // 
            // ckbxIsConvertUserBlogClass
            // 
            this.ckbxIsConvertUserBlogClass.AutoSize = true;
            this.ckbxIsConvertUserBlogClass.Location = new System.Drawing.Point(5, 212);
            this.ckbxIsConvertUserBlogClass.Name = "ckbxIsConvertUserBlogClass";
            this.ckbxIsConvertUserBlogClass.Size = new System.Drawing.Size(72, 16);
            this.ckbxIsConvertUserBlogClass.TabIndex = 21;
            this.ckbxIsConvertUserBlogClass.Text = "用户分类";
            this.ckbxIsConvertUserBlogClass.UseVisualStyleBackColor = true;
            // 
            // ckbxIsConvertPics
            // 
            this.ckbxIsConvertPics.AutoSize = true;
            this.ckbxIsConvertPics.Location = new System.Drawing.Point(95, 189);
            this.ckbxIsConvertPics.Name = "ckbxIsConvertPics";
            this.ckbxIsConvertPics.Size = new System.Drawing.Size(72, 16);
            this.ckbxIsConvertPics.TabIndex = 20;
            this.ckbxIsConvertPics.Text = "相册图片";
            this.ckbxIsConvertPics.UseVisualStyleBackColor = true;
            // 
            // ckbxIsConvertAblumDir
            // 
            this.ckbxIsConvertAblumDir.AutoSize = true;
            this.ckbxIsConvertAblumDir.Location = new System.Drawing.Point(5, 192);
            this.ckbxIsConvertAblumDir.Name = "ckbxIsConvertAblumDir";
            this.ckbxIsConvertAblumDir.Size = new System.Drawing.Size(72, 16);
            this.ckbxIsConvertAblumDir.TabIndex = 19;
            this.ckbxIsConvertAblumDir.Text = "相册目录";
            this.ckbxIsConvertAblumDir.UseVisualStyleBackColor = true;
            // 
            // ckbxIsConvertUserComment
            // 
            this.ckbxIsConvertUserComment.AutoSize = true;
            this.ckbxIsConvertUserComment.Location = new System.Drawing.Point(95, 169);
            this.ckbxIsConvertUserComment.Name = "ckbxIsConvertUserComment";
            this.ckbxIsConvertUserComment.Size = new System.Drawing.Size(72, 16);
            this.ckbxIsConvertUserComment.TabIndex = 18;
            this.ckbxIsConvertUserComment.Text = "转换留言";
            this.ckbxIsConvertUserComment.UseVisualStyleBackColor = true;
            // 
            // ckbxIsConvertBlogComment
            // 
            this.ckbxIsConvertBlogComment.AutoSize = true;
            this.ckbxIsConvertBlogComment.Location = new System.Drawing.Point(5, 169);
            this.ckbxIsConvertBlogComment.Name = "ckbxIsConvertBlogComment";
            this.ckbxIsConvertBlogComment.Size = new System.Drawing.Size(72, 16);
            this.ckbxIsConvertBlogComment.TabIndex = 17;
            this.ckbxIsConvertBlogComment.Text = "日志评论";
            this.ckbxIsConvertBlogComment.UseVisualStyleBackColor = true;
            // 
            // ckbxIsConvertBlogPosts
            // 
            this.ckbxIsConvertBlogPosts.AutoSize = true;
            this.ckbxIsConvertBlogPosts.Location = new System.Drawing.Point(95, 146);
            this.ckbxIsConvertBlogPosts.Name = "ckbxIsConvertBlogPosts";
            this.ckbxIsConvertBlogPosts.Size = new System.Drawing.Size(72, 16);
            this.ckbxIsConvertBlogPosts.TabIndex = 16;
            this.ckbxIsConvertBlogPosts.Text = "转换日志";
            this.ckbxIsConvertBlogPosts.UseVisualStyleBackColor = true;
            // 
            // ckbxIsConvertGroupPost
            // 
            this.ckbxIsConvertGroupPost.AutoSize = true;
            this.ckbxIsConvertGroupPost.Location = new System.Drawing.Point(6, 147);
            this.ckbxIsConvertGroupPost.Name = "ckbxIsConvertGroupPost";
            this.ckbxIsConvertGroupPost.Size = new System.Drawing.Size(72, 16);
            this.ckbxIsConvertGroupPost.TabIndex = 15;
            this.ckbxIsConvertGroupPost.Text = "群组帖子";
            this.ckbxIsConvertGroupPost.UseVisualStyleBackColor = true;
            // 
            // ckbxIsConvertGroups
            // 
            this.ckbxIsConvertGroups.AutoSize = true;
            this.ckbxIsConvertGroups.Location = new System.Drawing.Point(96, 124);
            this.ckbxIsConvertGroups.Name = "ckbxIsConvertGroups";
            this.ckbxIsConvertGroups.Size = new System.Drawing.Size(72, 16);
            this.ckbxIsConvertGroups.TabIndex = 14;
            this.ckbxIsConvertGroups.Text = "转换群组";
            this.ckbxIsConvertGroups.UseVisualStyleBackColor = true;
            // 
            // ckbxConvertPollRecords
            // 
            this.ckbxConvertPollRecords.AutoSize = true;
            this.ckbxConvertPollRecords.Location = new System.Drawing.Point(96, 81);
            this.ckbxConvertPollRecords.Name = "ckbxConvertPollRecords";
            this.ckbxConvertPollRecords.Size = new System.Drawing.Size(84, 16);
            this.ckbxConvertPollRecords.TabIndex = 13;
            this.ckbxConvertPollRecords.Text = "转换投票人";
            this.ckbxConvertPollRecords.UseVisualStyleBackColor = true;
            // 
            // ckbxConvertUserGroups
            // 
            this.ckbxConvertUserGroups.AutoSize = true;
            this.ckbxConvertUserGroups.Location = new System.Drawing.Point(6, 15);
            this.ckbxConvertUserGroups.Name = "ckbxConvertUserGroups";
            this.ckbxConvertUserGroups.Size = new System.Drawing.Size(84, 16);
            this.ckbxConvertUserGroups.TabIndex = 12;
            this.ckbxConvertUserGroups.Text = "转换用户组";
            this.ckbxConvertUserGroups.UseVisualStyleBackColor = true;
            // 
            // ckbxUpdatePostsInfo
            // 
            this.ckbxUpdatePostsInfo.AutoSize = true;
            this.ckbxUpdatePostsInfo.Location = new System.Drawing.Point(6, 435);
            this.ckbxUpdatePostsInfo.Name = "ckbxUpdatePostsInfo";
            this.ckbxUpdatePostsInfo.Size = new System.Drawing.Size(96, 16);
            this.ckbxUpdatePostsInfo.TabIndex = 11;
            this.ckbxUpdatePostsInfo.Text = "更新帖子信息";
            this.ckbxUpdatePostsInfo.UseVisualStyleBackColor = true;
            // 
            // ckbxResetTopicReplies
            // 
            this.ckbxResetTopicReplies.AutoSize = true;
            this.ckbxResetTopicReplies.Location = new System.Drawing.Point(6, 413);
            this.ckbxResetTopicReplies.Name = "ckbxResetTopicReplies";
            this.ckbxResetTopicReplies.Size = new System.Drawing.Size(120, 16);
            this.ckbxResetTopicReplies.TabIndex = 10;
            this.ckbxResetTopicReplies.Text = "重建主题回复数量";
            this.ckbxResetTopicReplies.UseVisualStyleBackColor = true;
            // 
            // ckbxResetTopicLastpostid
            // 
            this.ckbxResetTopicLastpostid.AutoSize = true;
            this.ckbxResetTopicLastpostid.Location = new System.Drawing.Point(6, 391);
            this.ckbxResetTopicLastpostid.Name = "ckbxResetTopicLastpostid";
            this.ckbxResetTopicLastpostid.Size = new System.Drawing.Size(120, 16);
            this.ckbxResetTopicLastpostid.TabIndex = 9;
            this.ckbxResetTopicLastpostid.Text = "重建主题排序信息";
            this.ckbxResetTopicLastpostid.UseVisualStyleBackColor = true;
            // 
            // ckbxConvertForumLinks
            // 
            this.ckbxConvertForumLinks.AutoSize = true;
            this.ckbxConvertForumLinks.Location = new System.Drawing.Point(6, 124);
            this.ckbxConvertForumLinks.Name = "ckbxConvertForumLinks";
            this.ckbxConvertForumLinks.Size = new System.Drawing.Size(72, 16);
            this.ckbxConvertForumLinks.TabIndex = 8;
            this.ckbxConvertForumLinks.Text = "友情链接";
            this.ckbxConvertForumLinks.UseVisualStyleBackColor = true;
            // 
            // ckbxConvertPms
            // 
            this.ckbxConvertPms.AutoSize = true;
            this.ckbxConvertPms.Location = new System.Drawing.Point(96, 101);
            this.ckbxConvertPms.Name = "ckbxConvertPms";
            this.ckbxConvertPms.Size = new System.Drawing.Size(84, 16);
            this.ckbxConvertPms.TabIndex = 7;
            this.ckbxConvertPms.Text = "转换短消息";
            this.ckbxConvertPms.UseVisualStyleBackColor = true;
            // 
            // ckbxConvertAttachments
            // 
            this.ckbxConvertAttachments.AutoSize = true;
            this.ckbxConvertAttachments.Location = new System.Drawing.Point(6, 101);
            this.ckbxConvertAttachments.Name = "ckbxConvertAttachments";
            this.ckbxConvertAttachments.Size = new System.Drawing.Size(72, 16);
            this.ckbxConvertAttachments.TabIndex = 6;
            this.ckbxConvertAttachments.Text = "转换附件";
            this.ckbxConvertAttachments.UseVisualStyleBackColor = true;
            // 
            // ckbxConvertPolls
            // 
            this.ckbxConvertPolls.AutoSize = true;
            this.ckbxConvertPolls.Location = new System.Drawing.Point(6, 81);
            this.ckbxConvertPolls.Name = "ckbxConvertPolls";
            this.ckbxConvertPolls.Size = new System.Drawing.Size(72, 16);
            this.ckbxConvertPolls.TabIndex = 5;
            this.ckbxConvertPolls.Text = "转换投票";
            this.ckbxConvertPolls.UseVisualStyleBackColor = true;
            // 
            // ckbxConvertPosts
            // 
            this.ckbxConvertPosts.AutoSize = true;
            this.ckbxConvertPosts.Location = new System.Drawing.Point(96, 59);
            this.ckbxConvertPosts.Name = "ckbxConvertPosts";
            this.ckbxConvertPosts.Size = new System.Drawing.Size(72, 16);
            this.ckbxConvertPosts.TabIndex = 4;
            this.ckbxConvertPosts.Text = "转换帖子";
            this.ckbxConvertPosts.UseVisualStyleBackColor = true;
            // 
            // ckbxConvertTopics
            // 
            this.ckbxConvertTopics.AutoSize = true;
            this.ckbxConvertTopics.Location = new System.Drawing.Point(6, 59);
            this.ckbxConvertTopics.Name = "ckbxConvertTopics";
            this.ckbxConvertTopics.Size = new System.Drawing.Size(72, 16);
            this.ckbxConvertTopics.TabIndex = 3;
            this.ckbxConvertTopics.Text = "转换主题";
            this.ckbxConvertTopics.UseVisualStyleBackColor = true;
            // 
            // ckbxConvertTopicTypes
            // 
            this.ckbxConvertTopicTypes.AutoSize = true;
            this.ckbxConvertTopicTypes.Location = new System.Drawing.Point(96, 37);
            this.ckbxConvertTopicTypes.Name = "ckbxConvertTopicTypes";
            this.ckbxConvertTopicTypes.Size = new System.Drawing.Size(72, 16);
            this.ckbxConvertTopicTypes.TabIndex = 2;
            this.ckbxConvertTopicTypes.Text = "主题分类";
            this.ckbxConvertTopicTypes.UseVisualStyleBackColor = true;
            // 
            // ckbxConvertForums
            // 
            this.ckbxConvertForums.AutoSize = true;
            this.ckbxConvertForums.Location = new System.Drawing.Point(6, 37);
            this.ckbxConvertForums.Name = "ckbxConvertForums";
            this.ckbxConvertForums.Size = new System.Drawing.Size(72, 16);
            this.ckbxConvertForums.TabIndex = 1;
            this.ckbxConvertForums.Text = "转换版块";
            this.ckbxConvertForums.UseVisualStyleBackColor = true;
            // 
            // ckbxConvertUsers
            // 
            this.ckbxConvertUsers.AutoSize = true;
            this.ckbxConvertUsers.Location = new System.Drawing.Point(96, 15);
            this.ckbxConvertUsers.Name = "ckbxConvertUsers";
            this.ckbxConvertUsers.Size = new System.Drawing.Size(72, 16);
            this.ckbxConvertUsers.TabIndex = 0;
            this.ckbxConvertUsers.Text = "转换用户";
            this.ckbxConvertUsers.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(5, 257);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 25;
            this.checkBox1.Text = "呵呵";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(95, 234);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(48, 16);
            this.checkBox2.TabIndex = 24;
            this.checkBox2.Text = "呵呵";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(5, 279);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(48, 16);
            this.checkBox3.TabIndex = 27;
            this.checkBox3.Text = "呵呵";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(95, 256);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(48, 16);
            this.checkBox5.TabIndex = 26;
            this.checkBox5.Text = "呵呵";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 525);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lbCurrentRecord);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.pbCurrent);
            this.Controls.Add(this.lbTotalRecord);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pbTotal);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.tbxMessage);
            this.Controls.Add(this.btnConvertStart);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.plSrcDBSetting_sql.ResumeLayout(false);
            this.plSrcDBSetting_sql.PerformLayout();
            this.plTargetDBSetting.ResumeLayout(false);
            this.plTargetDBSetting.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.plSrcDBSetting_acc.ResumeLayout(false);
            this.plSrcDBSetting_acc.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConvertStart;
        private System.Windows.Forms.Panel plSrcDBSetting_sql;
        private System.Windows.Forms.TextBox tbxSrcPassword;
        private System.Windows.Forms.TextBox tbxSrcLoginID;
        private System.Windows.Forms.TextBox tbxSrcDBName;
        private System.Windows.Forms.TextBox tbxSrcAddress;
        private System.Windows.Forms.Panel plTargetDBSetting;
        private System.Windows.Forms.TextBox tbxTargetPassword;
        private System.Windows.Forms.TextBox tbxTargetLoginID;
        private System.Windows.Forms.TextBox tbxTargetDBName;
        private System.Windows.Forms.TextBox tbxTargetAddress;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxMessage;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.ProgressBar pbTotal;
        private System.Windows.Forms.ComboBox cbbxSrcDbType;
        private System.Windows.Forms.ComboBox cbbxTargetDbType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbxSrcDbTablePrefix;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbxTargetDbTablePrefix;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lbTotalRecord;
        private System.Windows.Forms.Label lbCurrentRecord;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ProgressBar pbCurrent;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ComboBox cbbxConvertType;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox ckbxConvertForums;
        private System.Windows.Forms.CheckBox ckbxConvertUsers;
        private System.Windows.Forms.CheckBox ckbxResetTopicLastpostid;
        private System.Windows.Forms.CheckBox ckbxConvertForumLinks;
        private System.Windows.Forms.CheckBox ckbxConvertPms;
        private System.Windows.Forms.CheckBox ckbxConvertAttachments;
        private System.Windows.Forms.CheckBox ckbxConvertPolls;
        private System.Windows.Forms.CheckBox ckbxConvertPosts;
        private System.Windows.Forms.CheckBox ckbxConvertTopics;
        private System.Windows.Forms.CheckBox ckbxConvertTopicTypes;
        private System.Windows.Forms.CheckBox ckbxResetTopicReplies;
        private System.Windows.Forms.CheckBox ckbxUpdatePostsInfo;
        private System.Windows.Forms.Button btnReadme;
        private System.Windows.Forms.Label lbDllName;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox tbxSrcAccessDBPath;
        private System.Windows.Forms.Panel plSrcDBSetting_acc;
        private System.Windows.Forms.Button btnBrowerAccessDBFile;
        private System.Windows.Forms.CheckBox ckbxConvertUserGroups;
        private System.Windows.Forms.CheckBox ckbxConvertPollRecords;
        private System.Windows.Forms.CheckBox ckbxIsConvertGroupPost;
        private System.Windows.Forms.CheckBox ckbxIsConvertGroups;
        private System.Windows.Forms.CheckBox ckbxIsConvertAblumDir;
        private System.Windows.Forms.CheckBox ckbxIsConvertUserComment;
        private System.Windows.Forms.CheckBox ckbxIsConvertBlogComment;
        private System.Windows.Forms.CheckBox ckbxIsConvertBlogPosts;
        private System.Windows.Forms.CheckBox ckbxIsConvertUserRecommand;
        private System.Windows.Forms.CheckBox ckbxIsConvertFriend;
        private System.Windows.Forms.CheckBox ckbxIsConvertUserBlogClass;
        private System.Windows.Forms.CheckBox ckbxIsConvertPics;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}

