using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Yuwen.Tools.Data;

namespace DBTools
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 数据库表前缀
        /// </summary>
        public static string tablePrefix = "dnt_";
        public static string serverAddress = "127.0.0.1";
        public static string dbName = "dnt2";
        public static string userID = "sa";
        public static string password = "";
        public static int PageSize = 30000;
        public static int PageCount = 0;
        public static int StartPage = 1;
        public static int StartTid = 1;//起始tid
        public Form1()
        {
            InitializeComponent();
        }

        private void btnUpdateLastpostid_Click(object sender, EventArgs e)
        {
            tablePrefix = tbTablePrefix.Text.Trim();
            serverAddress = tbAddress.Text.Trim();
            dbName = tbDBName.Text.Trim();
            userID = tbLoginid.Text.Trim();
            password = tbPassword.Text.Trim();
            PageSize = Convert.ToInt32(tbPageSize.Text.Trim());
            StartPage = Convert.ToInt32(tbStartPage.Text.Trim());

            DBHelper objDBHRecordCount = GetDBHelper();
            int RecordCount = Convert.ToInt32(objDBHRecordCount.ExecuteScalar("SELECT COUNT(tid) FROM dnt_topics"));
            objDBHRecordCount.Dispose();


            //获取分页大小
            if (RecordCount % PageSize != 0)
            {
                PageCount = RecordCount / PageSize + 1;
            }
            else
            {
                PageCount = RecordCount / PageSize;
            }

            if (StartPage > PageCount)
            {
                MessageBox.Show(string.Format("记录分页数为{0}，你填写的开始页数超过了此数值！", PageCount));
                return;
            }

            //设置总进度条,输出消息                        
            pbTotal.Maximum = PageCount;
            pbTotal.Value = StartPage - 1;
            lbCurrentPage.Text = "0";

            pbCurrent.Maximum = RecordCount;
            pbCurrent.Value = PageSize * (StartPage - 1);
            lbCurrentRecord.Text = "0";
            tbMessage.Text += string.Format("共有{0}条主题需要更新。准备分为{1}批(每一批{2}条记录)更新，现在从第{3}批开始更新。\r\n", RecordCount, PageCount, PageSize, StartPage);

            //分页查询,然后更新
            string sql = "";
            for (int pagei = StartPage; pagei <= PageCount; pagei++)
            {
                int errCount = 0;
                tbMessage.Text += string.Format("更新第{0}批。\r\n", pagei);
                if (pagei <= 1)
                {
                    sql = string.Format
                           ("SELECT TOP {1} tid FROM {0}topics", tablePrefix, PageSize);
                }
                else
                {
                    sql = string.Format
                           ("SELECT TOP {1} tid FROM {0}topics WHERE tid NOT IN (SELECT TOP {2} tid FROM {0}topics)", tablePrefix, PageSize, PageSize * (pagei - 1));
                }

                DBHelper objDBHTopicList = GetDBHelper();
                System.Data.Common.DbDataReader drTopics = objDBHTopicList.ExecuteReader(sql);
                while (drTopics.Read())
                {
                    int tid = 0;
                    try
                    {
                        //当前处理的主题tid
                        tid = Convert.ToInt32(drTopics["tid"]);
                        int postTableNum = GetPostTableNum(tid);
                        //得到当前主题的最后回复的帖子
                        string sqlPostsInfo = string.Format("SELECT TOP 1 postdatetime,pid,poster,posterid  FROM {0}posts{1} WHERE  tid={2}  ORDER BY pid DESC", tablePrefix, postTableNum, tid);
                        DBHelper objDBHPostsInfo = GetDBHelper();
                        System.Data.Common.DbDataReader drPost = objDBHPostsInfo.ExecuteReader(sqlPostsInfo);

                        while (drPost.Read())
                        {
                            //更新主题
                            string sqlUpdateTopic = string.Format("UPDATE {0}topics SET lastpost =@lastpost,lastpostid =@lastpostid,lastposter = @lastposter,lastposterid = @lastposterid WHERE tid =@tid", tablePrefix, postTableNum, tid);
                            DBHelper objDBHUpdateTopic = GetDBHelper();
                            objDBHUpdateTopic.ParameterAdd("@lastpost", drPost["postdatetime"], DbType.DateTime, 8);
                            objDBHUpdateTopic.ParameterAdd("@lastpostid", Convert.ToInt32(drPost["pid"]), DbType.Int32, 4);
                            objDBHUpdateTopic.ParameterAdd("@lastposter", drPost["poster"], DbType.String, 20);
                            objDBHUpdateTopic.ParameterAdd("@lastposterid", Convert.ToInt32(drPost["posterid"]), DbType.Int32, 4);
                            objDBHUpdateTopic.ParameterAdd("@tid", tid, DbType.Int32, 4);

                            objDBHUpdateTopic.ExecuteNonQuery(sqlUpdateTopic);
                        }
                        drPost.Close();
                        drPost.Dispose();
                    }
                    catch (Exception ex)
                    {
                        tbMessage.Text += string.Format("发生错误：{0}\r\ntid={1}。\r\n", ex.Message, tid);
                        errCount++;
                    }
                    pbCurrent.Value++;
                    lbCurrentRecord.Text = pbCurrent.Value.ToString();
                    Application.DoEvents();

                }
                drTopics.Close();
                drTopics.Dispose();
                pbTotal.Value++;
                lbCurrentPage.Text = pbTotal.Value.ToString();
                tbMessage.Text += string.Format("更新第{0}批完毕，失败{1}条。\r\n", pagei, errCount);
            }

        }




        private int GetPostTableNum(int tid)
        {
            int PostTableNum = 1;
            string sql = string.Format("SELECT * FROM {0}tablelist WHERE mintid<={1} AND (maxtid<=0 OR maxtid>={1})", tablePrefix, tid);
            DBHelper objDBH = GetDBHelper();
            System.Data.Common.DbDataReader dr = objDBH.ExecuteReader(sql);
            if (dr.Read())
            {
                PostTableNum = Convert.ToInt32(dr["id"]);
            }
            dr.Close();
            objDBH.Dispose();
            return PostTableNum;
        }




        private DBHelper GetDBHelper()
        {
            return new DBHelper(string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3};", serverAddress, dbName, userID, password), "System.Data.SqlClient");
        }

        private void btnUpdateLastpostid_Nolist_Click(object sender, EventArgs e)
        {
            //得到数据库信息
            SetInfo();
            
            //得到最大主题id
            DBHelper objDBHMaxtid = GetDBHelper();
            int maxtid = 0;
            try
            {
                maxtid = Convert.ToInt32(objDBHMaxtid.ExecuteScalar("SELECT MAX(tid) FROM dnt_topics"));
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show("数据库连接失败,请检查~");
                return;
            }
            objDBHMaxtid.Dispose();

            if (StartTid > maxtid)
            {
                MessageBox.Show(string.Format("最大tid值为{0}，你填写的起始tid值超过了此数值！", maxtid));
                return;
            }

            //设置进度条,显示消息
            pbTotal.Maximum = 1;
            pbTotal.Value = 1;
            lbCurrentPage.Text = pbTotal.Value.ToString();

            pbCurrent.Maximum = maxtid;
            pbCurrent.Value = StartTid-1;
            lbCurrentRecord.Text = pbCurrent.Value.ToString();

            tbMessage.Text += string.Format("共有{0}条主题需要更新。现在从第{1}条开始更新。\r\n", maxtid, StartTid);
            //失败条数
            int errCount = 0;
            //lastpostid字段的累加.此字段只负责排序 所以可以随意累加,避免上次转换过程中出现的list重复约束
            int baseNum = 0;
            if (cbxRandomLastpostid.Checked)//如果选中了,则把当前日小时分钟累加上去
            {
                baseNum = int.Parse(DateTime.Now.ToString("ddhhmm"));
            }

            //从tid=1开始,向tid=maxtid递增
            for (int tid = StartTid; tid <= maxtid; tid++)
            {
                

                try
                {
                    //查找分表
                    int postTableNum = GetPostTableNum(tid);
                    //得到当前主题的最后回复的帖子
                    string sqlPostsInfo = string.Format("SELECT TOP 1 postdatetime,pid,poster,posterid  FROM {0}posts{1} WHERE  tid={2}  ORDER BY pid DESC", tablePrefix, postTableNum, tid);
                    DBHelper objDBHPostsInfo = GetDBHelper();
                    System.Data.Common.DbDataReader drPost = objDBHPostsInfo.ExecuteReader(sqlPostsInfo);

                    //如果存在
                    while (drPost.Read())
                    {
                        //更新主题
                        string sqlUpdateTopic = string.Format("UPDATE {0}topics SET lastpost =@lastpost,lastpostid =@lastpostid,lastposter = @lastposter,lastposterid = @lastposterid WHERE tid =@tid", tablePrefix, postTableNum, tid);
                        DBHelper objDBHUpdateTopic = GetDBHelper();
                        objDBHUpdateTopic.ParameterAdd("@lastpost", drPost["postdatetime"], DbType.DateTime, 8);
                        objDBHUpdateTopic.ParameterAdd("@lastpostid", baseNum + Convert.ToInt32(drPost["pid"]), DbType.Int32, 4);
                        objDBHUpdateTopic.ParameterAdd("@lastposter", drPost["poster"], DbType.String, 20);
                        objDBHUpdateTopic.ParameterAdd("@lastposterid", Convert.ToInt32(drPost["posterid"]), DbType.Int32, 4);
                        objDBHUpdateTopic.ParameterAdd("@tid", tid, DbType.Int32, 4);

                        objDBHUpdateTopic.ExecuteNonQuery(sqlUpdateTopic);
                    }
                    drPost.Close();
                    drPost.Dispose();
                }
                catch (Exception ex)
                {
                    tbMessage.Text += string.Format("发生错误：{0}\r\ntid={1}。\r\n", ex.Message, tid);
                    errCount++;
                }
                pbCurrent.Value=tid;
                lbCurrentRecord.Text = pbCurrent.Value.ToString();
                lbErrCount.Text = errCount.ToString();
                Application.DoEvents();
            }
            tbMessage.Text += string.Format("\r\n更新完成。失败条数:{0}\r\n", errCount);
        }

        //从窗体得到数据库信息和转换信息
        private void SetInfo()
        {
            tablePrefix = tbTablePrefix.Text.Trim();
            serverAddress = tbAddress.Text.Trim();
            dbName = tbDBName.Text.Trim();
            userID = tbLoginid.Text.Trim();
            password = tbPassword.Text.Trim();
            PageSize = Convert.ToInt32(tbPageSize.Text.Trim());
            StartPage = Convert.ToInt32(tbStartPage.Text.Trim());
            StartTid = Convert.ToInt32(tbStarttid.Text.Trim());
        }

        private void cbxNoList_CheckedChanged(object sender, EventArgs e)
        {
            SetFormController();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetFormController();
        }

        //设置窗体控件状态
        private void SetFormController()
        {
            if (cbxNoList.Checked)
            {
                tbPageSize.Enabled = false;
                tbStartPage.Enabled = false;
                btnUpdateLastpostid.Enabled = false;
                tbStarttid.Enabled = true;
                btnUpdateLastpostid_Nolist.Enabled = true;
            }
            else
            {
                tbPageSize.Enabled = true;
                tbStartPage.Enabled = true;
                btnUpdateLastpostid.Enabled = true;
                tbStarttid.Enabled = false;
                btnUpdateLastpostid_Nolist.Enabled = false;

            }
        }
    }
}
