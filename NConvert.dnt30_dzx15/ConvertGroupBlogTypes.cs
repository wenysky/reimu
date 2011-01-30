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
        public int GetGroupBlogTypeRecordCount()
        {
            if (MainForm.RecordCount == -1)
            {
                return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT(id) FROM [science].[dbo].[group_articletype]")));
            }
            else
            {
                return MainForm.RecordCount;
            }
        }

        public List<GroupBlogTypeInfo> GetGroupBlogTypeList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM [science].[dbo].[group_articletype] ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM [science].[dbo].[group_articletype] WHERE id NOT IN (SELECT TOP {2} id FROM [science].[dbo].[group_articletype] ORDER BY id) ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion
            System.Data.Common.DbDataReader drGroupBlogType = MainForm.srcDBH.ExecuteReader(sql);

            List<GroupBlogTypeInfo> forumList = new List<GroupBlogTypeInfo>();
            while (drGroupBlogType.Read())
            {
                GroupBlogTypeInfo objGroupBlogTypeInfo = new GroupBlogTypeInfo();
                objGroupBlogTypeInfo.gtid = Convert.ToInt32(drGroupBlogType["id"]);
                objGroupBlogTypeInfo.typename = drGroupBlogType["title"] != DBNull.Value ? drGroupBlogType["title"].ToString() : "";
                objGroupBlogTypeInfo.groupid = drGroupBlogType["group_username"] != DBNull.Value ? MainForm.groupidList[drGroupBlogType["group_username"].ToString()] : 0;
                objGroupBlogTypeInfo.createtime = 0;
                forumList.Add(objGroupBlogTypeInfo);
            }
            drGroupBlogType.Close();
            drGroupBlogType.Dispose();
            return forumList;
        }
    }
}
