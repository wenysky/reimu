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
        public int GetGroupShowBlogRecordCount()
        {
            if (MainForm.RecordCount == -1)
            {
                return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT(id) FROM [science].[dbo].[group_article]")));
            }
            else
            {
                return MainForm.RecordCount;
            }
        }

        public List<GroupShowBlogInfo> GetGroupShowBlogList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM [science].[dbo].[group_article] ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM [science].[dbo].[group_article] WHERE id NOT IN (SELECT TOP {2} id FROM [science].[dbo].[group_article] ORDER BY id) ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion
            System.Data.Common.DbDataReader drGroupBlogType = MainForm.srcDBH.ExecuteReader(sql);

            List<GroupShowBlogInfo> forumList = new List<GroupShowBlogInfo>();
            while (drGroupBlogType.Read())
            {
                GroupShowBlogInfo objGroupBlogTypeInfo = new GroupShowBlogInfo();
                objGroupBlogTypeInfo.gid = Convert.ToInt32(drGroupBlogType["id"]);
                objGroupBlogTypeInfo.blogid = Convert.ToInt32(drGroupBlogType["oldid"]);
                objGroupBlogTypeInfo.fid = drGroupBlogType["group_username"] != DBNull.Value ? MainForm.groupidList[drGroupBlogType["group_username"].ToString()] : 0;
                objGroupBlogTypeInfo.commend = Convert.ToInt32(drGroupBlogType["good"]);
                objGroupBlogTypeInfo.senduser = drGroupBlogType["username"] != DBNull.Value ? GetUIDbyUsername(drGroupBlogType["username"].ToString()) : 0;
                objGroupBlogTypeInfo.status = 1;
                objGroupBlogTypeInfo.sendtime = drGroupBlogType["updatetime"] != DBNull.Value ? Utils.TypeParse.DateTime2TimeStamp(Convert.ToDateTime(drGroupBlogType["updatetime"])) : 0;
                objGroupBlogTypeInfo.grouptype = Convert.ToInt32(drGroupBlogType["nclass"]);
                forumList.Add(objGroupBlogTypeInfo);
            }
            drGroupBlogType.Close();
            drGroupBlogType.Dispose();
            return forumList;
        }
    }
}
