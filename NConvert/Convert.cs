using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NConvert.Entity;
using Yuwen.Tools.TinyData;
using System.IO;

namespace NConvert
{
    public class DBConvert
    {
        public static void Start()
        {
            if (!MainForm.DbConnStatus())
            {
                //System.Windows.Forms.MessageBox.Show("数据库连接失败!\r\n");
                MainForm.MessageForm.SetButtonStatus(false);
                System.Threading.Thread.CurrentThread.Abort();
            }

            if (MainForm.IsConvertUsers)
                ConvertUsers();

            MainForm.MessageForm.SetMessage(string.Format("========={0}==========\r\n", DateTime.Now));
            MainForm.MessageForm.SetButtonStatus(false);
        }




        /// <summary>
        /// 转换用户
        /// </summary>
        public static void ConvertUsers()
        {
            Yuwen.Tools.Data.DBHelper dbhConvertUsers = MainForm.GetSrcDBH_OldVer();
            dbhConvertUsers.Open();
            MainForm.MessageForm.SetMessage("开始转换用户\r\n");
            MainForm.SuccessedRecordCount = 0;
            MainForm.FailedRecordCount = 0;

            MainForm.RecordCount = Provider.Provider.GetInstance().GetUsersRecordCount();
            if (MainForm.RecordCount % MainForm.PageSize != 0)
            {
                MainForm.PageCount = MainForm.RecordCount / MainForm.PageSize + 1;
            }
            else
            {
                MainForm.PageCount = MainForm.RecordCount / MainForm.PageSize;
            }
            MainForm.MessageForm.InitTotalProgressBar(MainForm.PageCount);
            MainForm.MessageForm.InitCurrentProgressBar(MainForm.RecordCount);

            int pageid = 1;
            if (pageid <= 1)
            {
                //string sql = "if exists (select * from sysobjects where id = object_id(N'[wysky_mytemp]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [wysky_mytemp];";
                //清理数据库
                //dbhConvertUsers.TruncateTable("wysky_mytemp");
            }
            else
            {
                MainForm.MessageForm.SetTotalProgressBarNum(pageid - 1);
                MainForm.MessageForm.SetCurrentProgressBarNum((pageid - 1) * MainForm.PageSize);
            }


            #region sql语句
            string sqlUCUser = string.Format(@"INSERT INTO wysky_mytemp (
ids,
bdate
)
VALUES (
@ids,
@bdate
)", MainForm.cic.TargetDbTablePrefix);
            #endregion

            for (int pagei = pageid; pagei <= MainForm.PageCount; pagei++)
            {
                //分段得到用户列表
                List<Users> userList=new List<Users>();// = 
                Provider.Provider.GetInstance().GetUserList(pagei);
                foreach (Users objUser in userList)
                {
                    try
                    {
                        //dbhConvertUsers.ParametersClear();
                        //#region users参数
                        //dbhConvertUsers.ParameterAdd("@ids", objUser.salt, DbType.String, 600000);
                        //dbhConvertUsers.ParameterAdd("@bdate", objUser.salt, DbType.DateTime, 4);

                        //#endregion
                        //dbhConvertUsers.ExecuteNonQuery(sqlUCUser);
                        //MainForm.SuccessedRecordCount++;
                    }
                    catch (Exception ex)
                    {
                        MainForm.MessageForm.SetMessage(string.Format("错误:{0}.uid={1}\r\n", ex.Message, objUser.uid));
                        MainForm.FailedRecordCount++;
                    }
                    MainForm.MessageForm.CurrentProgressBarNumAdd();
                }
                MainForm.MessageForm.TotalProgressBarNumAdd();
            }

            MainForm.RecordCount = -1;
            MainForm.MessageForm.SetMessage(string.Format("完成转换用户。成功{0}，失败{1}\r\n", MainForm.SuccessedRecordCount, MainForm.FailedRecordCount));
            MainForm.SuccessedRecordCount = 0;
            MainForm.FailedRecordCount = 0;
            //dbhConvertUsers.Close();

            dbhConvertUsers.Dispose();
        }
    }
}
