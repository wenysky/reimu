using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;
using NConvert.Entity;
using Yuwen.Tools.Data;

namespace NConvert.lybbs70_dnt31
{
    public partial class Provider : IProvider
    {
        public int GetUserGroupsRecordCount()
        {
            DBHelper userDBH = MainForm.GetSrcDBH_OldVer();
            return Convert.ToInt32(
                userDBH.ExecuteScalar(
                    string.Format("SELECT COUNT(grade) FROM {0}grade WHERE usermode=0", MainForm.cic.SrcDbTablePrefix)
                    )
                );
        }




        public List<UserGroupInfo> GetUserGroupList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            sql = string.Format
                   ("SELECT * FROM {0}grade WHERE usermode=0 ORDER BY mpostmark LIMIT {1},{2}", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize * (CurrentPage - 1), MainForm.PageSize);
            #endregion

            DBHelper userlistDBH = MainForm.GetSrcDBH_OldVer();
            System.Data.Common.DbDataReader dr = userlistDBH.ExecuteReader(sql);
            List<UserGroupInfo> userlist = new List<UserGroupInfo>();


            while (dr.Read())
            {
                UserGroupInfo objUser = new UserGroupInfo();
                objUser.Groupid = Convert.ToInt32(dr["grade"]) + 10;
                objUser.Grouptitle = dr["mname"].ToString();
                objUser.Creditshigher = Convert.ToInt32(dr["mpostmark"]);
                if (userlist.Count > 0)
                {
                    userlist[userlist.Count - 1].Creditslower = objUser.Creditshigher;
                }
                userlist.Add(objUser);
            }
            userlist[userlist.Count - 1].Creditslower = 99999999;
            return userlist;
        }
    }
}
