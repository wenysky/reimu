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


        public int GetIndexRecomandBlogRecordCount()
        {
            return Convert.ToInt32(
                MainForm.srcDBH.ExecuteScalar(
                string.Format(
                "SELECT COUNT(id) FROM [science].[dbo].[kexue_blognews]",
                MainForm.cic.SrcDbTablePrefix)
                )
                );
        }

        public List<IndexRecomandBlogInfo> GetIndexRecomandBlogList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM [science].[dbo].[kexue_blognews] ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM [science].[dbo].[kexue_blognews] WHERE id NOT IN (SELECT TOP {2} id FROM [science].[dbo].[kexue_blognews] ORDER BY id) ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion



            System.Data.Common.DbDataReader dr = MainForm.srcDBH.ExecuteReader(sql);
            List<IndexRecomandBlogInfo> Recommandlist = new List<IndexRecomandBlogInfo>();
            while (dr.Read())
            {
                IndexRecomandBlogInfo objFriend = new IndexRecomandBlogInfo();
                objFriend.rfid = dr["id"] != DBNull.Value ? Convert.ToInt32(dr["id"]) : 0;

                if (dr["titleurl"] != DBNull.Value && dr["titleurl"].ToString().Trim() != string.Empty)
                {
                    string titleUrl = dr["titleurl"].ToString().Trim();
                    string blogid = Utils.Text.GetMatch(titleUrl, "http://www.sciencenet.cn/blog/user_content.aspx\\?id=([0-9]+).*?");
#warning  断点一下
                    int rblogid;
                    if (blogid == string.Empty || !int.TryParse(blogid, out rblogid))
                    {
                        objFriend.blogid = 0;
                    }
                    else
                    {
                        objFriend.blogid = rblogid;
                    }
                }
                else
                {
                    objFriend.blogid = 0;
                }
                objFriend.title = dr["title"] != DBNull.Value ? dr["title"].ToString().Trim() : "";
                objFriend.content = dr["userurl"] != DBNull.Value ? dr["userurl"].ToString().Trim() : "";
                objFriend.status = dr["ifgood"] != DBNull.Value ? Convert.ToInt32(dr["ifgood"]) : 0;
                objFriend.recommendtime = 0;
                objFriend.bloguid = 0;
                objFriend.relateblog = "";
                Recommandlist.Add(objFriend);
            }
            dr.Close();
            dr.Dispose();
            return Recommandlist;
        }

        #endregion
    }
}
