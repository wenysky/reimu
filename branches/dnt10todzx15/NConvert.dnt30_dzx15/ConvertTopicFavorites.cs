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
        public int GetTopicFavoriteRecordCount()
        {
            if (MainForm.RecordCount == -1)
            {
                return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT(tid) FROM {0}favorites", MainForm.cic.SrcDbTablePrefix)));
            }
            else
            {
                return MainForm.RecordCount;
            }
        }

        public List<FavoriteInfo> GetTopicFavoriteList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM {0}favorites f LEFT JOIN {0}topics t ON f.tid=t.tid ORDER BY t.tid", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM {0}favorites f LEFT JOIN {0}topics t ON f.tid=t.tid WHERE t.tid NOT IN (SELECT TOP {2} tid FROM {0}favorites ORDER BY tid) ORDER BY t.tid", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion
            System.Data.Common.DbDataReader drGroupBlogType = MainForm.srcDBH.ExecuteReader(sql);

            List<FavoriteInfo> forumList = new List<FavoriteInfo>();
            while (drGroupBlogType.Read())
            {
                FavoriteInfo objGroupBlogTypeInfo = new FavoriteInfo();
                objGroupBlogTypeInfo.favid = 0;
                objGroupBlogTypeInfo.uid = Convert.ToInt32(drGroupBlogType["uid"]);
                objGroupBlogTypeInfo.id = Convert.ToInt32(drGroupBlogType["tid"]);
                objGroupBlogTypeInfo.idtype = "tid";
                objGroupBlogTypeInfo.spaceuid = drGroupBlogType["posterid"] != DBNull.Value ? Convert.ToInt32(drGroupBlogType["posterid"]) : 0;
                objGroupBlogTypeInfo.title = drGroupBlogType["title"] != DBNull.Value ? drGroupBlogType["title"].ToString() : "";
                if (objGroupBlogTypeInfo.spaceuid == 0 && objGroupBlogTypeInfo.title == string.Empty)
                {
                    continue;
                }
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
