using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;
using NConvert.Entity;
using Yuwen.Tools.Data;

namespace NConvert.dnt30_dzx15
{
    public partial class Provider : IProvider
    {
        public int GetBlogFavoriteRecordCount()
        {
            if (MainForm.RecordCount == -1)
            {
                return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT(id) FROM [science].[dbo].[kexue_blogxxsc]")));
            }
            else
            {
                return MainForm.RecordCount;
            }
        }

        public List<FavoriteInfo> GetBlogFavoriteList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM [science].[dbo].[kexue_blogxxsc] ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM [science].[dbo].[kexue_blogxxsc] WHERE id NOT IN (SELECT TOP {2} id FROM [science].[dbo].[kexue_blogxxsc] ORDER BY id) ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion
            System.Data.Common.DbDataReader drGroupBlogType = MainForm.srcDBH.ExecuteReader(sql);

            List<FavoriteInfo> forumList = new List<FavoriteInfo>();
            while (drGroupBlogType.Read())
            {
                FavoriteInfo objGroupBlogTypeInfo = new FavoriteInfo();
                objGroupBlogTypeInfo.favid = Convert.ToInt32(drGroupBlogType["id"]);
                objGroupBlogTypeInfo.uid = drGroupBlogType["username"] != DBNull.Value ? GetUIDbyUsername(drGroupBlogType["username"].ToString()) : 0;
                objGroupBlogTypeInfo.id = Convert.ToInt32(drGroupBlogType["xxid"]);
                objGroupBlogTypeInfo.idtype = "blogid";
                objGroupBlogTypeInfo.spaceuid = GetUIDbyBlogid(objGroupBlogTypeInfo.id);
                objGroupBlogTypeInfo.title = drGroupBlogType["title"] != DBNull.Value ? drGroupBlogType["title"].ToString() : "";
                objGroupBlogTypeInfo.description = "";
                objGroupBlogTypeInfo.dateline = 0;
                forumList.Add(objGroupBlogTypeInfo);
            }
            drGroupBlogType.Close();
            drGroupBlogType.Dispose();
            return forumList;
        }
    }
}
