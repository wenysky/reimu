using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;
using NConvert.Entity;
using Yuwen.Tools.Data;
using System.Collections;

namespace NConvert.dnt30_dzx15
{
    public partial class Provider : IProvider
    {
        public int GetUsers4PhoneNumberRecordCount()
        {
            DBHelper userDBH = MainForm.GetSrcDBH_OldVer();
            return Convert.ToInt32(
                userDBH.ExecuteScalar(
                    string.Format("SELECT COUNT(id) FROM [sciencebbs].[dbo].[user]")
                    )
                );
        }




        public List<Users> GetUser4PhoneNumberList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {0} id,username,UserMobile,UserInfo FROM [sciencebbs].[dbo].[user] ORDER BY id", MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {0} id,username,UserMobile,UserInfo FROM [sciencebbs].[dbo].[user] WHERE id NOT IN (SELECT TOP {1} id FROM [sciencebbs].[dbo].[user] ORDER BY id) ORDER BY id", MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion

            DBHelper userlistDBH = MainForm.GetSrcDBH_OldVer();
            DBHelper dbhUserTemp = MainForm.GetSrcDBH_OldVer();
            DBHelper dbhBlogLinkTemp = MainForm.GetSrcDBH_OldVer();
            DBHelper dbhBlogMusicTemp = MainForm.GetSrcDBH_OldVer();

            System.Data.Common.DbDataReader dr = userlistDBH.ExecuteReader(sql);
            List<Users> userlist = new List<Users>();
            while (dr.Read())
            {
                Users objUser = new Users();

                objUser.uid = Convert.ToInt32(dr["id"]);
                objUser.username = dr["username"] != DBNull.Value ? dr["username"].ToString() : "";                
                objUser.mobile = dr["UserMobile"] != DBNull.Value ? dr["UserMobile"].ToString().Trim() : "";

                string userInfo = dr["UserInfo"] != DBNull.Value ? dr["UserInfo"].ToString() : "";
                string[] arrayUserInfo = userInfo.Split('\\');//一共有15个
                objUser.telephone = arrayUserInfo.Length == 15 ? arrayUserInfo[12] : "";

                if (objUser.mobile == string.Empty || objUser.telephone == string.Empty)
                {
                    continue;
                }
                userlist.Add(objUser);
            }
            dr.Close();
            dr.Dispose();
            return userlist;
        }

    }
}
