using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Yuwen.Tools.TinyData;
using NConvert.Entity;
using System.Threading;

namespace NConvert
{
    public partial class MainForm : Form
    {

        #region 全局变量

        /// <summary>
        /// 转换线程
        /// </summary>
        Thread ConvertThread;
        /// <summary>
        /// 界面线程的窗体对象-其他线程调用窗体线程的Form对象
        /// </summary>
        public static MainForm MessageForm;

        /// <summary>
        /// 当前准备转换表的记录数.如果变量为-1,则从数据库读取
        /// </summary>
        public static int RecordCount = -1;
        /// <summary>
        /// 分页大小
        /// </summary>
        public static int PageSize = 1000;
        /// <summary>
        /// 当前表分页数
        /// </summary>
        public static int PageCount = -1;
        /// <summary>
        /// 成功的记录数
        /// </summary>
        public static int SuccessedRecordCount;
        /// <summary>
        /// 失败的记录数
        /// </summary>
        public static int FailedRecordCount;
        /// <summary>
        /// 分表大小(默认3W主题一个表)
        /// </summary>
        public static int PostsTableSize = 30000;
        /// <summary>
        /// 分表信息
        /// </summary>
        public static List<PostTables> PostTableList;

        /// <summary>
        /// 转换类型(初步决定为例如NConvert.{0}.dll)
        /// </summary>
        public static ConvertInfoConfig cic = null;

        public static List<Attachments> extAttachList;

        public static int extAttachAidStartIndex;

        public static Dictionary<string, int> groupidList;

        public static List<BlogPostInfo> trashBlogPostList;

        #region 数据库连接信息
        /// <summary>
        /// 源数据库连接字符串
        /// </summary>
        public static string srcDbConn = "Data Source=localhost;Initial Catalog=cvc2;User ID=web;Password=itca;";

        //public static string srcDbTyep = "SqlServer";

        public static string srcDbTypeNamespace = "System.Data.SqlClient";

        //public static string srcDbTableProfix = "CVC_";
        /// <summary>
        /// 源数据库DBH对象
        /// </summary>
        public static Yuwen.Tools.Data.DBHelper srcDBH;



        /// <summary>
        /// 目标数据库连接字符串
        /// </summary>
        public static string targetDbConn = "Data Source=localhost;Initial Catalog=dnt2_dadach;User ID=web;Password=itca;";

        //public static string targetDbTyep = "SqlServer";

        public static string targetDbTypeNamespace = "System.Data.SqlClient";

        //public static string targetDbTableProfix = "dnt_";
        /// <summary>
        /// 目标数据库DBH对象
        /// </summary>
        public static Yuwen.Tools.Data.DBHelper targetDBH;

        #endregion

        #region 转换项目
        public static bool IsConvertUserGroups;//转换用户组
        public static bool IsConvertUsers;//转换用户
        public static bool IsConvertForums;//转换版块
        public static bool IsConvertTopicTypes;//转换主题分类
        public static bool IsConvertTopics;//转换主题
        public static bool IsConvertPosts;//转换帖子
        public static bool IsConvertPolls;//转换投票
        public static bool IsConvertPollRecords;//转换投票记录（人）
        public static bool IsConvertAttachments;//转换附件
        public static bool IsConvertForumLinks;//转换友情链接
        public static bool IsConvertPms;//转换短消息
        public static bool IsResetTopicLastpostid;//重建主题排序信息
        public static bool IsResetTopicReplies;//重建主题回复数
        public static bool IsUpdatePostsInfo;//更新帖子信息
        public static bool IsConvertGroups;//转换
        public static bool IsConvertGroupPosts;//转换
        public static bool IsConvertBlogPosts;//转换
        public static bool IsConvertBlogComments;//转换
        public static bool IsConvertHomeComments;//转换
        public static bool IsConvertAlbumCategories;//转换
        public static bool IsConvertAlbums;//转换
        public static bool IsConvertAlbumPics;//转换
        public static bool IsConvertGroupBlogTypes;//转换
        public static bool IsConvertGroupBlogs;//转换
        public static bool IsConvertBlogFavorites;//转换
        public static bool IsConvertRateLogs;//转换

        public static bool IsConvertUserBlogClass;//转换
        public static bool IsConvertFriends;//转换
        public static bool IsConvertUserRecommands;//转换

        public static bool IsConvertIndexRecomandBlogPicss;//转换
        public static bool IsConvertIndexRecomandBlogs;//转换
        public static bool IsConvertBlogSubjects;//转换

        /*
        public static bool IsConvert;//转换
        public static bool IsConvert;//转换
        public static bool IsConvert;//转换
        public static bool IsConvert;//转换
        public static bool IsConvert;//转换
        public static bool IsConvert;//转换
        public static bool IsConvert;//转换
        public static bool IsConvert;//转换
         */
        #endregion

        #endregion


        public MainForm()
        {
            InitializeComponent();
            cic = new ConvertInfoConfig();
        }


        //检查数据库连接
        internal static bool DbConnStatus()
        {
            try
            {
                srcDBH.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("源数据库链接失败，" + ex.Message);
                return false;
            }

            try
            {
                targetDBH.Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("目标数据库链接失败，" + ex.Message);
                return false;
            }
        }

        public void InitializeConvertInfo()
        {
            MessageForm = this;//将当前线程窗体赋值给全局变量供其他线程和方法调用            
            cic.ConvertTypeName = cbbxConvertType.SelectedValue.ToString().Trim();

            cic.SrcDbType = cbbxSrcDbType.Text.Trim();
            cic.SrcDbFilePath = tbxSrcAccessDBPath.Text.Trim();
            cic.SrcDbAddress = tbxSrcAddress.Text.Trim();
            cic.SrcDbName = tbxSrcDBName.Text.Trim();
            cic.SrcDbUsername = tbxSrcLoginID.Text.Trim();
            cic.SrcDbUserpassword = tbxSrcPassword.Text.Trim();
            cic.SrcDbTablePrefix = tbxSrcDbTablePrefix.Text.Trim();
            if (cic.SrcDbType.ToLower() == "access")
            {
                srcDbConn = cic.SrcDbFilePath;
                srcDbTypeNamespace = "System.Data.OleDb";
            }
            else if (cic.SrcDbType.ToLower() == "mysql")
            {
                srcDbConn = string.Format(@"Data Source={0};Port=3306;Initial Catalog={1};User ID={2};Password={3};Allow Zero Datetime=true;charset=utf8;", cic.SrcDbAddress, cic.SrcDbName, cic.SrcDbUsername, cic.SrcDbUserpassword);
                srcDbTypeNamespace = "MySql.Data.MySqlClient";
            }
            else
            {
                srcDbConn = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3};", cic.SrcDbAddress, cic.SrcDbName, cic.SrcDbUsername, cic.SrcDbUserpassword);
            }


            cic.TargetDbType = cbbxTargetDbType.Text.Trim();
            cic.TargetDbAddress = tbxTargetAddress.Text.Trim();
            cic.TargetDbName = tbxTargetDBName.Text.Trim();
            cic.TargetDbUsername = tbxTargetLoginID.Text.Trim();
            cic.TargetDbUserpassword = tbxTargetPassword.Text.Trim();
            cic.TargetDbTablePrefix = tbxTargetDbTablePrefix.Text.Trim();

            if (cic.TargetDbType.ToLower() == "access")
            {
                targetDbConn = cic.TargetDbFilePath;
                targetDbTypeNamespace = "System.Data.OleDb";
            }
            else if (cic.TargetDbType.ToLower() == "mysql")
            {
                targetDbConn = string.Format(@"Data Source={0};Port=3306;Initial Catalog={1};User ID={2};Password={3};Allow Zero Datetime=true;charset=utf8;", cic.TargetDbAddress, cic.TargetDbName, cic.TargetDbUsername, cic.TargetDbUserpassword);
                targetDbTypeNamespace = "MySql.Data.MySqlClient";
            }
            else
            {
                targetDbConn = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3};", cic.TargetDbAddress, cic.TargetDbName, cic.TargetDbUsername, cic.TargetDbUserpassword);
            }

            //初始化全局静态DHB对象
            srcDBH = GetSrcDBH_OldVer();
            targetDBH = GetTargetDBH_OldVer();
            #region 设置需要转换的项目
            IsConvertUserGroups = ckbxConvertUserGroups.Checked;
            IsConvertUsers = ckbxConvertUsers.Checked;
            IsConvertForums = ckbxConvertForums.Checked;
            IsConvertTopicTypes = ckbxConvertTopicTypes.Checked;
            IsConvertTopics = ckbxConvertTopics.Checked;
            IsConvertPosts = ckbxConvertPosts.Checked;
            IsConvertPolls = ckbxConvertPolls.Checked;
            IsConvertPollRecords = ckbxConvertPollRecords.Checked;
            IsConvertAttachments = ckbxConvertAttachments.Checked;
            IsConvertForumLinks = ckbxConvertForumLinks.Checked;
            IsConvertPms = ckbxConvertPms.Checked;
            IsResetTopicLastpostid = ckbxResetTopicLastpostid.Checked;
            IsResetTopicReplies = ckbxResetTopicReplies.Checked;
            IsUpdatePostsInfo = ckbxUpdatePostsInfo.Checked;

            IsConvertBlogPosts = ckbxIsConvertBlogPosts.Checked;
            IsConvertGroups = ckbxIsConvertGroups.Checked;
            IsConvertGroupPosts = ckbxIsConvertGroupPost.Checked;
            IsConvertBlogComments = ckbxIsConvertBlogComment.Checked;
            IsConvertHomeComments = ckbxIsConvertUserComment.Checked;

            IsConvertAlbumCategories = ckbxIsConvertAlbumCategory.Checked;
            IsConvertAlbums = ckbxIsConvertAlbum.Checked;
            IsConvertAlbumPics = ckbxIsConvertPics.Checked;


            IsConvertGroupBlogTypes = ckbxIsConvertGroupBlogType.Checked;//转换
            IsConvertGroupBlogs = ckbxIsConvertGroupBlog.Checked;//转换
            IsConvertBlogFavorites = ckbxIsConvertBlogPostFavorite.Checked;//转换

            IsConvertRateLogs = ckbxIsConvertRateLogs.Checked;


            IsConvertUserBlogClass = ckbxIsConvertUserBlogClass.Checked;
            IsConvertFriends = ckbxIsConvertFriend.Checked;
            IsConvertUserRecommands = ckbxIsConvertUserRecommand.Checked;

            IsConvertIndexRecomandBlogPicss = ckbxIsConvertIndexRecomandBlogPics.Checked;
            IsConvertIndexRecomandBlogs = ckbxIsConvertIndexRecomandBlogs.Checked;
            IsConvertBlogSubjects = ckbxIsConvertBlogSubjects.Checked;

            #endregion
        }

        private void btnConvertStart_Click(object sender, EventArgs e)
        {
            InitializeConvertInfo();
            SetButtonStatus(true);

            SetMessage(string.Format("========={0}==========\r\n线程启动,初始化数据库信息!\r\n", DateTime.Now));
            ConvertThread = new Thread(new ThreadStart(DBConvert.Start));
            ConvertThread.Start();

        }



        #region 消息通知显示

        //设置消息显示的委托
        delegate void SetMessageDelegate(string Message);
        delegate void InitTotalProgressBarDelegate(int TotalMaximum);
        delegate void InitCurrentProgressBarDelegate(int CurrentMaximum);
        delegate void SetTotalProgressBarNumDelegate(int Num);
        delegate void SetCurrentProgressBarNumDelegate(int Num);
        delegate void TotalProgressBarNumAddDelegate();
        delegate void CurrentProgressBarNumAddDelegate();
        delegate void SetButtonStatusDelegate(bool IsStart);

        /// <summary>
        /// 设置消息显示
        /// </summary>
        /// <param name="Message">消息内容</param>
        public void SetMessage(string Message)
        {
            if (this.tbxMessage.InvokeRequired)
            {
                SetMessageDelegate dSetMessage = new SetMessageDelegate(SetMessage);
                this.Invoke(dSetMessage, new object[] { Message });
            }
            else
            {
                this.tbxMessage.Text += Message;

                this.tbxMessage.Focus();//让文本框获取焦点
                this.tbxMessage.Select(this.tbxMessage.TextLength, 0);//设置光标的位置到文本尾age.
                this.tbxMessage.ScrollToCaret();//滚动到控件光标处Message.
            }
        }

        public void InitTotalProgressBar(int TotalMaximum)
        {
            if (this.pbTotal.InvokeRequired)
            {
                InitTotalProgressBarDelegate dInitTotalPb = new InitTotalProgressBarDelegate(InitTotalProgressBar);
                this.Invoke(dInitTotalPb, new object[] { TotalMaximum });
            }
            else
            {
                this.pbTotal.Value = 0;
                this.pbTotal.Maximum = TotalMaximum;
            }
        }

        public void InitCurrentProgressBar(int CurrentMaximum)
        {
            if (this.pbCurrent.InvokeRequired)
            {
                InitTotalProgressBarDelegate dInitCurrentPb = new InitTotalProgressBarDelegate(InitCurrentProgressBar);
                this.Invoke(dInitCurrentPb, new object[] { CurrentMaximum });
            }
            else
            {
                this.pbCurrent.Value = 0;
                this.pbCurrent.Maximum = CurrentMaximum;
                this.lbTotalRecord.Text = this.pbCurrent.Maximum.ToString();
                this.lbCurrentRecord.Text = "0";
            }
        }

        public void SetTotalProgressBarNum(int Num)
        {
            if (this.pbTotal.InvokeRequired)
            {
                SetTotalProgressBarNumDelegate dSetTotalPb = new SetTotalProgressBarNumDelegate(SetTotalProgressBarNum);
                this.Invoke(dSetTotalPb, new object[] { Num });
            }
            else
            {
                this.pbTotal.Value = Num;
            }
        }

        public void SetCurrentProgressBarNum(int Num)
        {
            if (this.pbCurrent.InvokeRequired)
            {
                SetCurrentProgressBarNumDelegate dSetCurrentPb = new SetCurrentProgressBarNumDelegate(SetCurrentProgressBarNum);
                this.Invoke(dSetCurrentPb, new object[] { Num });
            }
            else
            {
                this.pbCurrent.Value = Num;
                this.lbCurrentRecord.Text = this.pbCurrent.Value.ToString();
            }
        }

        public void TotalProgressBarNumAdd()
        {
            if (this.pbTotal.InvokeRequired)
            {
                TotalProgressBarNumAddDelegate dTotalPbAdd = new TotalProgressBarNumAddDelegate(TotalProgressBarNumAdd);
                this.Invoke(dTotalPbAdd);
            }
            else
            {
                this.pbTotal.Value++;
            }
        }

        public void CurrentProgressBarNumAdd()
        {
            if (this.pbCurrent.InvokeRequired)
            {
                CurrentProgressBarNumAddDelegate dCurrentPbAdd = new CurrentProgressBarNumAddDelegate(CurrentProgressBarNumAdd);
                this.Invoke(dCurrentPbAdd);
            }
            else
            {
                if (this.pbCurrent.Value < this.pbCurrent.Maximum)
                {
                    this.pbCurrent.Value++;
                }
                else
                {
                    SetMessage("CurrentProgressBarNumAdd ERR");
                }
                this.lbCurrentRecord.Text = this.pbCurrent.Value.ToString();
            }
        }

        public void SetButtonStatus(bool IsStart)
        {
            if (this.btnConvertStart.InvokeRequired)
            {
                SetButtonStatusDelegate dSetButtonStatus = new SetButtonStatusDelegate(SetButtonStatus);
                this.Invoke(dSetButtonStatus, new object[] { IsStart });
            }
            else
            {
                if (IsStart)
                {
                    this.btnConvertStart.Enabled = false;
                    this.btnStop.Enabled = true;
                }
                else
                {
                    this.btnConvertStart.Enabled = true;
                    this.btnStop.Enabled = false;
                }
            }
        }
        #endregion

        #region 得到DBH对象
        /// <summary>
        /// 得到源数据库的DBH对象
        /// </summary>
        /// <returns></returns>
        public static DBHelper GetSrcDBH()
        {
            return new DBHelper(srcDbConn, srcDbTypeNamespace);
        }
        public static Yuwen.Tools.Data.DBHelper GetSrcDBH_OldVer()
        {
            return new Yuwen.Tools.Data.DBHelper(srcDbConn, srcDbTypeNamespace);
        }

        /// <summary>
        /// 得到目标数据库的DBH对象
        /// </summary>
        /// <returns></returns>
        public static DBHelper GetTargetDBH()
        {
            return new DBHelper(targetDbConn, targetDbTypeNamespace);
        }
        public static Yuwen.Tools.Data.DBHelper GetTargetDBH_OldVer()
        {
            return new Yuwen.Tools.Data.DBHelper(targetDbConn, targetDbTypeNamespace);
        }
        #endregion

        #region 数据库类型改变引发的界面变化
        private void cbbxSrcDbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbxSrcDbType.Text.Trim().ToLower() == "access")
            {
                plSrcDBSetting_sql.Enabled = false;
                plSrcDBSetting_sql.Visible = false;
                plSrcDBSetting_acc.Enabled = true;
                plSrcDBSetting_acc.Visible = true;
            }
            else
            {
                plSrcDBSetting_sql.Enabled = true;
                plSrcDBSetting_sql.Visible = true;
                plSrcDBSetting_acc.Enabled = false;
                plSrcDBSetting_acc.Visible = false;
            }
        }
        private void cbbxTargetDbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbxTargetDbType.Text == "Access")
            {
                plTargetDBSetting.Enabled = false;
                plTargetDBSetting.Visible = false;
            }
            else
            {
                plTargetDBSetting.Enabled = true;
                plTargetDBSetting.Visible = true;
            }
        }
        #endregion






        //测试
        private void btnTest_Click(object sender, EventArgs e)
        {
            InitializeConvertInfo();

            new MoreSetting().Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            #region 初始化转换项目列表

            string[] dllfiles = System.IO.Directory.GetFiles(Application.StartupPath, "NConvert.*.dll");
            List<DllInfo> dlllist = new List<DllInfo>();
            foreach (string dllfile in dllfiles)
            {
                DllInfo objDllInfo = new DllInfo();
                objDllInfo.Classname = System.IO.Path.GetFileNameWithoutExtension(dllfile).Replace("NConvert.", "");
                objDllInfo.Displayname = GetProvider(objDllInfo.Classname).GetDllDisplayName();
                objDllInfo.Description = GetProvider(objDllInfo.Classname).GetDllDescription();
                dlllist.Add(objDllInfo);
            }

            cbbxConvertType.DataSource = dlllist;
            cbbxConvertType.DisplayMember = "Displayname";
            cbbxConvertType.ValueMember = "Classname";

            #endregion

            //初始化支持的数据信息
            InitializeSupportInfo(cbbxConvertType.SelectedValue.ToString().Trim());
        }

        //绑定支持的数据信息
        private void InitializeSupportInfo(string DllClassName)
        {
            cbbxSrcDbType.DataSource = GetProvider(DllClassName).GetSupportSrcDbType();
            cbbxTargetDbType.DataSource = GetProvider(DllClassName).GetSupportTargetDbType();
            lbDllName.Text = DllClassName;
        }


        //工具:反射DLL
        private Provider.IProvider GetProvider(string name)
        {
            return (NConvert.Provider.IProvider)Activator.CreateInstance(Type.GetType(string.Format("NConvert.{0}.Provider, NConvert.{0}", name), false, true));
        }

        //转换类型改变时 重新绑定信息
        private void cbbxConvertType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            InitializeSupportInfo(cbbxConvertType.SelectedValue.ToString().Trim());
        }

        //说明对话框
        private void btnReadme_Click(object sender, EventArgs e)
        {
            MessageBox.Show(GetProvider(cbbxConvertType.SelectedValue.ToString().Trim()).GetDllDescription(), "转换说明");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (ConvertThread != null && ConvertThread.IsAlive)
            {
                try
                {
                    ConvertThread.Abort();
                }
                catch (ThreadAbortException ex)
                {
                    SetMessage("正在终止线程...");
                }
                SetMessage("已手动停止转换\r\n");
            }
            else
            {
                SetMessage("没有启动任何线程！\r\n");
            }
        }

        private void btnBrowerAccessDBFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
                tbxSrcAccessDBPath.Text = ofd.FileName;
        }
    }
}
