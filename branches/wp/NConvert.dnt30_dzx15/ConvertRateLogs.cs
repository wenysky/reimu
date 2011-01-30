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
        public int GetRateLogRecordCount()
        {
            if (MainForm.RecordCount == -1)
            {
                return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT(id) FROM {0}ratelog", MainForm.cic.SrcDbTablePrefix)));
            }
            else
            {
                return MainForm.RecordCount;
            }
        }

        public List<RateLogInfo> GetRateLogList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM {0}ratelog ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM {0}ratelog WHERE id NOT IN (SELECT TOP {2} id FROM {0}ratelog ORDER BY id) ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion
            System.Data.Common.DbDataReader drAlbumCategory = MainForm.srcDBH.ExecuteReader(sql);

            List<RateLogInfo> forumList = new List<RateLogInfo>();
            while (drAlbumCategory.Read())
            {
                RateLogInfo objForum = new RateLogInfo();
                objForum.pid = Convert.ToInt32(drAlbumCategory["pid"]);
                objForum.uid = Convert.ToInt32(drAlbumCategory["uid"]);
                objForum.username = drAlbumCategory["username"].ToString();
                objForum.extcredits = Convert.ToInt32(drAlbumCategory["extcredits"]);
                objForum.dateline = Utils.TypeParse.DateTime2TimeStamp(Convert.ToDateTime(drAlbumCategory["postdatetime"]));
                objForum.score = Convert.ToInt32(drAlbumCategory["score"]);
                objForum.reason = drAlbumCategory["reason"].ToString();
                forumList.Add(objForum);
            }
            drAlbumCategory.Close();
            drAlbumCategory.Dispose();
            return forumList;
        }
    }
}
