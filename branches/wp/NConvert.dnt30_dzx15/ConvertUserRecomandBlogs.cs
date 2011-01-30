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


        public int GetUserRecommandBlogRecordCount()
        {
            return Convert.ToInt32(
                MainForm.srcDBH.ExecuteScalar(
                string.Format(
                "SELECT COUNT(id) FROM [science].[dbo].[kexue_blogarticle] WHERE tuijianren>''",
                MainForm.cic.SrcDbTablePrefix)
                )
                );
        }

        public List<UserRecommandBlogInfo> GetUserRecommandBlogList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {1} id,homeurl,tuijianren FROM [science].[dbo].[kexue_blogarticle] WHERE tuijianren>'' ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} id,homeurl,tuijianren FROM [science].[dbo].[kexue_blogarticle] WHERE tuijianren>'' AND id NOT IN (SELECT TOP {2} id FROM [science].[dbo].[kexue_blogarticle] WHERE tuijianren>'' ORDER BY id) ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion



            System.Data.Common.DbDataReader dr = MainForm.srcDBH.ExecuteReader(sql);
            List<UserRecommandBlogInfo> Recommandlist = new List<UserRecommandBlogInfo>();
            while (dr.Read())
            {
                string[] recommandUsers = dr["tuijianren"] != DBNull.Value ? dr["tuijianren"].ToString().Trim().Split('|') : new string[0];

                foreach (string fname in recommandUsers)
                {
                    if (fname.Trim() != string.Empty)
                    {
                        string[] user = fname.Split('#');
                        if (user.Length == 2)
                        {
                            UserRecommandBlogInfo objFriend = new UserRecommandBlogInfo();
                            objFriend.rid = -1;
                            objFriend.uid = dr["homeurl"] != DBNull.Value ? Convert.ToInt32(dr["homeurl"]) : 0;
                            objFriend.id = Convert.ToInt32(dr["id"]);
                            objFriend.idtype = "blogid";
                            objFriend.author = user[1].Trim();
                            int authorid;
                            if (objFriend.author == string.Empty || !int.TryParse(user[0].Trim(), out authorid))
                            {
                                continue;
                            }
                            objFriend.authorid = authorid;
                            objFriend.ip = "";
                            objFriend.dateline = 0;

                            Recommandlist.Add(objFriend);
                        }
                    }
                }
            }
            dr.Close();
            dr.Dispose();
            return Recommandlist;
        }

        #endregion
    }
}
