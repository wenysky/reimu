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


        public int GetBlogSubjectRecordCount()
        {
            return Convert.ToInt32(
                MainForm.srcDBH.ExecuteScalar(
                string.Format(
                "SELECT COUNT(id) FROM [science].[dbo].[kexue_blogsubject]",
                MainForm.cic.SrcDbTablePrefix)
                )
                );
        }

        public List<BlogSubjectInfo> GetBlogSubjectList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM [science].[dbo].[kexue_blogsubject] ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM [science].[dbo].[kexue_blogsubject] WHERE id NOT IN (SELECT TOP {2} id FROM [science].[dbo].[kexue_blogsubject] ORDER BY id) ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion



            System.Data.Common.DbDataReader dr = MainForm.srcDBH.ExecuteReader(sql);
            List<BlogSubjectInfo> Recommandlist = new List<BlogSubjectInfo>();
            while (dr.Read())
            {
                BlogSubjectInfo objFriend = new BlogSubjectInfo();
                objFriend.sbid = dr["sbid"] != DBNull.Value ? Convert.ToInt32(dr["id"]) : 0;
                objFriend.title = dr["title"] != DBNull.Value ? dr["title"].ToString().Trim() : "";
                objFriend.content = dr["content"] != DBNull.Value ? dr["content"].ToString().Trim() : "";
                objFriend.sbtype = dr["parent"] != DBNull.Value ? Convert.ToInt32(dr["parent"]) : 0;
                objFriend.sborder = dr["order1"] != DBNull.Value ? Convert.ToInt32(dr["order1"]) : 0;
                objFriend.logo = dr["logo"] != DBNull.Value ? dr["logo"].ToString().Trim() : "";
                objFriend.updatetime = dr["updatetime"] != DBNull.Value ? Utils.TypeParse.DateTime2TimeStamp(Convert.ToDateTime(dr["updatetime"])) : 0;
                Recommandlist.Add(objFriend);
            }
            dr.Close();
            dr.Dispose();
            return Recommandlist;
        }

        #endregion
    }
}
