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
        public int GetAlbumRecordCount()
        {
            if (MainForm.RecordCount == -1)
            {
                return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT(id) FROM [science].[dbo].[kexue_photoclass]")));
            }
            else
            {
                return MainForm.RecordCount;
            }
        }

        public List<AlbumInfo> GetAlbumList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM [science].[dbo].[kexue_photoclass] ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM [science].[dbo].[kexue_photoclass] WHERE id NOT IN (SELECT TOP {2} id FROM [science].[dbo].[kexue_photoclass] ORDER BY id) ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion
            System.Data.Common.DbDataReader drAlbumCategory = MainForm.srcDBH.ExecuteReader(sql);

            List<AlbumInfo> AlbumList = new List<AlbumInfo>();
            while (drAlbumCategory.Read())
            {
                AlbumInfo objAlbumInfo = new AlbumInfo();
                objAlbumInfo.albumid = Convert.ToInt32(drAlbumCategory["id"]);
                objAlbumInfo.albumname = drAlbumCategory["title"].ToString();
                objAlbumInfo.catid = Convert.ToInt32(drAlbumCategory["parent1"]);
                objAlbumInfo.username = drAlbumCategory["username"].ToString();
                objAlbumInfo.uid = GetUIDbyUsername(objAlbumInfo.username);
                objAlbumInfo.dateline = drAlbumCategory["updatetime"] != DBNull.Value ? Utils.TypeParse.DateTime2TimeStamp(Convert.ToDateTime(drAlbumCategory["updatetime"])) : 0;
                objAlbumInfo.updatetime = objAlbumInfo.dateline;
                objAlbumInfo.picnum = Convert.ToInt32(drAlbumCategory["shu"]);
                objAlbumInfo.pic = drAlbumCategory["pic"].ToString();
                objAlbumInfo.picflag = objAlbumInfo.picnum > 0 ? 1 : 0;
                objAlbumInfo.friend = Convert.ToInt32(drAlbumCategory["hide"]) == 1 ? 3 : 0;
                objAlbumInfo.password = "";
                objAlbumInfo.target_ids = "";
                objAlbumInfo.favtimes = 0;
                objAlbumInfo.sharetimes = 0;
                AlbumList.Add(objAlbumInfo);
            }
            drAlbumCategory.Close();
            drAlbumCategory.Dispose();
            return AlbumList;
        }
    }
}
