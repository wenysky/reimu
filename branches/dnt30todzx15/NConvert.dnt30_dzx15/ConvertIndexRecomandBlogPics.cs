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


        public int GetIndexRecomandBlogPicRecordCount()
        {
            return Convert.ToInt32(
                MainForm.srcDBH.ExecuteScalar(
                string.Format(
                "SELECT COUNT(id) FROM [science].[dbo].[kexue_blogpic]",
                MainForm.cic.SrcDbTablePrefix)
                )
                );
        }

        public List<IndexRecomandBlogPicInfo> GetIndexRecomandBlogPicList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM [science].[dbo].[kexue_blogpic] ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM [science].[dbo].[kexue_blogpic] WHERE id NOT IN (SELECT TOP {2} id FROM [science].[dbo].[kexue_blogpic] ORDER BY id) ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion



            System.Data.Common.DbDataReader dr = MainForm.srcDBH.ExecuteReader(sql);
            List<IndexRecomandBlogPicInfo> Recommandlist = new List<IndexRecomandBlogPicInfo>();
            while (dr.Read())
            {
                IndexRecomandBlogPicInfo objFriend = new IndexRecomandBlogPicInfo();
                objFriend.rpid = Convert.ToInt32(dr["id"]);
                objFriend.title = dr["name"] != DBNull.Value ? dr["name"].ToString().Trim() : "";
                objFriend.picsrc = dr["pic"] != DBNull.Value ? dr["pic"].ToString().Trim() : "";
                objFriend.linksrc = dr["link"] != DBNull.Value ? dr["link"].ToString().Trim() : "";
                objFriend.pictype = dr["ifhead"] != DBNull.Value ? Convert.ToInt32(dr["ifhead"]) : 0;
                objFriend.userid = dr["userid"] != DBNull.Value ? Convert.ToInt32(dr["userid"]) : 0;
                objFriend.readme = dr["readme"] != DBNull.Value ? dr["readme"].ToString().Trim() : "";
                Recommandlist.Add(objFriend);
            }
            dr.Close();
            dr.Dispose();
            return Recommandlist;
        }

        #endregion
    }
}
