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
        public int GetAlbumCategoryRecordCount()
        {
            if (MainForm.RecordCount == -1)
            {
                return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT(id) FROM [science].[dbo].[kexue_photo1]")));
            }
            else
            {
                return MainForm.RecordCount;
            }
        }

        public List<AlbumCategoryInfo> GetAlbumCategoryList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM [science].[dbo].[kexue_photo1] ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM [science].[dbo].[kexue_photo1] WHERE id NOT IN (SELECT TOP {2} id FROM [science].[dbo].[kexue_photo1] ORDER BY id) ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion
            System.Data.Common.DbDataReader drAlbumCategory = MainForm.srcDBH.ExecuteReader(sql);

            List<AlbumCategoryInfo> forumList = new List<AlbumCategoryInfo>();
            while (drAlbumCategory.Read())
            {
                AlbumCategoryInfo objForum = new AlbumCategoryInfo();
                objForum.catid = Convert.ToInt32(drAlbumCategory["id"]);
                objForum.upid = 0;
                objForum.catname = drAlbumCategory["title"].ToString();
                objForum.num = 0;
                objForum.displayorder = 0;
                forumList.Add(objForum);
            }
            drAlbumCategory.Close();
            drAlbumCategory.Dispose();
            return forumList;
        }
    }
}
