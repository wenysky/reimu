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
        #region IProvider 成员


        public int GetHomeCommentRecordCount()
        {
            if (MainForm.RecordCount == -1)
            {
                return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT(id) FROM [science].[dbo].[kexue_blogbook]")));
            }
            else
            {
                return MainForm.RecordCount;
            }
        }

        public List<CommentInfo> GetHomeCommentList(int CurrentPage)
        {
            string sqlBoard;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sqlBoard = string.Format
                       ("SELECT TOP {1} * FROM [science].[dbo].[kexue_blogbook] ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            }
            else
            {
                sqlBoard = string.Format
                       ("SELECT TOP {1} * FROM [science].[dbo].[kexue_blogbook] WHERE id NOT IN (SELECT TOP {2} id FROM [science].[dbo].[kexue_blogbook] ORDER BY id) ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion

            System.Data.Common.DbDataReader drBoard = MainForm.srcDBH.ExecuteReader(sqlBoard);

            List<CommentInfo> forumList = new List<CommentInfo>();
            while (drBoard.Read())
            {
                CommentInfo objForum = new CommentInfo();
                objForum.cid = Convert.ToInt32(drBoard["id"]);
                objForum.uid = Convert.ToInt32(drBoard["userid"]);
                objForum.id = Convert.ToInt32(drBoard["userid"]);
                objForum.idtype = "uid";
                objForum.author = drBoard["username"] != DBNull.Value ? drBoard["username"].ToString() : "";
                objForum.authorid = GetUIDbyUsername(objForum.author);
                objForum.ip = drBoard["ip"].ToString();
                objForum.dateline = Utils.TypeParse.DateTime2TimeStamp(Convert.ToDateTime(drBoard["updatetime"]));
                objForum.message = drBoard["content"].ToString();
                objForum.magicflicker = 0;
                objForum.status = 0;
                forumList.Add(objForum);
            }
            drBoard.Close();
            drBoard.Dispose();
            return forumList;
        }

        #endregion
    }
}
