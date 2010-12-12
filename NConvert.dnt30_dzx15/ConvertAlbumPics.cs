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
        public int GetAlbumPicRecordCount()
        {
            if (MainForm.RecordCount == -1)
            {
                return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT(id) FROM [science].[dbo].[kexue_photo]")));
            }
            else
            {
                return MainForm.RecordCount;
            }
        }

        public List<AlbumPicInfo> GetAlbumPicList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM [science].[dbo].[kexue_photo] ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM [science].[dbo].[kexue_photo] WHERE id NOT IN (SELECT TOP {2} id FROM [science].[dbo].[kexue_photo] ORDER BY id) ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion
            System.Data.Common.DbDataReader drAlbumPic = MainForm.srcDBH.ExecuteReader(sql);

            List<AlbumPicInfo> AlbumPicList = new List<AlbumPicInfo>();
            while (drAlbumPic.Read())
            {
                AlbumPicInfo objAlbumPicInfo = new AlbumPicInfo();
                objAlbumPicInfo.picid = Convert.ToInt32(drAlbumPic["id"]);
                objAlbumPicInfo.albumid = Convert.ToInt32(drAlbumPic["class1"]);
                objAlbumPicInfo.username = drAlbumPic["username"].ToString();
                objAlbumPicInfo.uid = GetUIDbyUsername(objAlbumPicInfo.username);
                objAlbumPicInfo.dateline = Convert.ToInt32(drAlbumPic["posttime"]);
                objAlbumPicInfo.postip = "";
                if (drAlbumPic["address"] != DBNull.Value && drAlbumPic["address"].ToString().Trim() != string.Empty)
                {
                    objAlbumPicInfo.filepath = drAlbumPic["address"].ToString().Trim();
                }
                else
                {
                    continue;
                }
                objAlbumPicInfo.filename = System.IO.Path.GetFileName(objAlbumPicInfo.filepath);
                objAlbumPicInfo.title = drAlbumPic["title"].ToString();
                objAlbumPicInfo.type = System.IO.Path.GetExtension(objAlbumPicInfo.filepath);
                objAlbumPicInfo.size = 1;
                objAlbumPicInfo.thumb = 0;
                objAlbumPicInfo.remote = 0;
                objAlbumPicInfo.hot = 0;
                objAlbumPicInfo.sharetimes = 0;
                objAlbumPicInfo.click1 = 0;
                objAlbumPicInfo.click2 = 0;
                objAlbumPicInfo.click3 = 0;
                objAlbumPicInfo.click4 = 0;
                objAlbumPicInfo.click5 = 0;
                objAlbumPicInfo.click6 = 0;
                objAlbumPicInfo.click7 = 0;
                objAlbumPicInfo.click8 = 0;
                objAlbumPicInfo.magicframe = 0;
                objAlbumPicInfo.status = 0;
                AlbumPicList.Add(objAlbumPicInfo);
            }
            drAlbumPic.Close();
            drAlbumPic.Dispose();
            return AlbumPicList;
        }
    }
}
