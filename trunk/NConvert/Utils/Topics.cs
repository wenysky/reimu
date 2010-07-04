using System;
using System.Collections.Generic;
using System.Text;
using Yuwen.Tools.TinyData;
using System.Windows.Forms;
using System.Data;

namespace NConvert.Utils
{
    public class Topics
    {
        /// <summary>
        /// 重建主题排序信息
        /// </summary>
        public static void ResetTopicLastpostid()
        {
            DBConvert.GetPostTableList();
            int StartTid = 1;
            //得到最大主题id
            MainForm.RecordCount = Convert.ToInt32(MainForm.targetDBH.ExecuteScalar(string.Format("SELECT MAX(tid) FROM {0}topics", MainForm.cic.TargetDbTablePrefix)));

            if (StartTid > MainForm.RecordCount)
            {
                MessageBox.Show(string.Format("最大tid值为{0}，你填写的起始tid值超过了此数值！", MainForm.RecordCount));
                return;
            }

            MainForm.MessageForm.SetMessage("开始重建主题排序信息\r\n");
            MainForm.SuccessedRecordCount = 0;
            MainForm.FailedRecordCount = 0;
            //设置进度条
            MainForm.MessageForm.InitTotalProgressBar(1);
            MainForm.MessageForm.InitCurrentProgressBar(MainForm.RecordCount);


            DBHelper postsInfoDBH = MainForm.GetTargetDBH();
            postsInfoDBH.Open();
            DBHelper updateTopicDBH = MainForm.GetTargetDBH();
            updateTopicDBH.Open();

            //从tid=StartTid开始,向tid=MainForm.RecordCount递增
            for (int tid = StartTid; tid <= MainForm.RecordCount; tid++)
            {

                //得到当前主题的最后回复的帖子
                string sqlPostsInfo = string.Format("SELECT TOP 1 postdatetime,pid,poster,posterid  FROM {0}posts{1} WHERE  tid={2}  ORDER BY pid DESC", MainForm.cic.TargetDbTablePrefix, Posts.GetPostTableId(tid), tid);
                DataTable dtPost = postsInfoDBH.ExecuteDataSet(sqlPostsInfo).Tables[0];

                //如果存在

                if (dtPost.Rows.Count > 0)
                {
                    //更新主题
                    string sqlUpdateTopic = string.Format("UPDATE {0}topics SET lastpost =@lastpost,lastpostid =@lastpostid,lastposter = @lastposter,lastposterid = @lastposterid WHERE tid =@tid", MainForm.cic.TargetDbTablePrefix);
                    updateTopicDBH.ParametersClear();
                    updateTopicDBH.ParameterAdd("@lastpost", dtPost.Rows[0]["postdatetime"], DbType.DateTime, 8);
                    updateTopicDBH.ParameterAdd("@lastpostid", Convert.ToInt32(dtPost.Rows[0]["pid"]), DbType.Int32, 4);
                    updateTopicDBH.ParameterAdd("@lastposter", dtPost.Rows[0]["poster"], DbType.String, 20);
                    updateTopicDBH.ParameterAdd("@lastposterid", Convert.ToInt32(dtPost.Rows[0]["posterid"]), DbType.Int32, 4);
                    updateTopicDBH.ParameterAdd("@tid", tid, DbType.Int32, 4);
                    try
                    {
                        updateTopicDBH.ExecuteNonQuery(sqlUpdateTopic);
                        MainForm.SuccessedRecordCount++;
                    }
                    catch (Exception ex)
                    {
                        MainForm.MessageForm.SetMessage(string.Format("错误:{0}.tid={1}\r\n", ex.Message, tid));
                        MainForm.FailedRecordCount++;
                    }
                    MainForm.MessageForm.SetCurrentProgressBarNum(tid);
                }
            }
            updateTopicDBH.Close();
            postsInfoDBH.Close();
            //一次分页完毕
            MainForm.MessageForm.TotalProgressBarNumAdd();

            MainForm.RecordCount = -1;
            MainForm.MessageForm.SetMessage(string.Format("完成重建主题排序信息。有效数{2}，成功{0}，失败{1}\r\n", MainForm.SuccessedRecordCount, MainForm.FailedRecordCount, MainForm.SuccessedRecordCount + MainForm.FailedRecordCount));
        }
    }
}
