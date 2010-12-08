using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;
using NConvert.Entity;

namespace NConvert.dnt30_dzx15
{
    public partial class Provider : IProvider
    {
        #region IProvider 成员


        public int GetFriendRecordCount()
        {
            return Convert.ToInt32(
                MainForm.srcDBH.ExecuteScalar(
                string.Format(
                "SELECT COUNT(id) FROM [sciencebbs].[dbo].[user]",
                MainForm.cic.SrcDbTablePrefix)
                )
                );
        }

        public List<FriendInfo> GetFriendList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM [sciencebbs].[dbo].[user] ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM [sciencebbs].[dbo].[user] WHERE id NOT IN (SELECT TOP {2} id FROM [sciencebbs].[dbo].[user] ORDER BY id) ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion



            System.Data.Common.DbDataReader dr = MainForm.srcDBH.ExecuteReader(sql);
            List<FriendInfo> friendlist = new List<FriendInfo>();
            while (dr.Read())
            {
                string[] friends = dr["friend"] != DBNull.Value ? dr["friend"].ToString().Trim().Split('|') : new string[0];

                foreach (string fname in friends)
                {
                    if (fname.Trim() != string.Empty)
                    {
                        FriendInfo objFriend = new FriendInfo();
                        objFriend.uid = Convert.ToInt32(dr["id"]);
                        objFriend.fusername = fname.Trim();
                        objFriend.fuid = GetUIDbyUsername(objFriend.fusername);
                        objFriend.gid = 0;
                        objFriend.num = 0;
                        objFriend.dateline = 0;
                        objFriend.note = "";
                        friendlist.Add(objFriend);
                    }
                }
            }
            dr.Close();
            dr.Dispose();
            return friendlist;
        }

        #endregion
    }
}
