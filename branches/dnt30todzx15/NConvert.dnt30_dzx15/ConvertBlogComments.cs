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


        public int GetBlogCommentRecordCount()
        {
            if (MainForm.RecordCount == -1)
            {
                return 2 + Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT(id) FROM [science].[dbo].[kexue_blogcomment]")));
            }
            else
            {
                return MainForm.RecordCount;
            }
        }

        public List<CommentInfo> GetBlogCommentList(int pagei)
        {
            string sqlBoard = string.Format(
                "SELECT * FROM [science].[dbo].[kexue_blogcomment]",
                MainForm.cic.SrcDbTablePrefix
                );

            System.Data.Common.DbDataReader drBoard = MainForm.srcDBH.ExecuteReader(sqlBoard);

            List<CommentInfo> forumList = new List<CommentInfo>();
            while (drBoard.Read())
            {
                CommentInfo objForum = new CommentInfo();
                objForum.cid = Convert.ToInt32(drBoard["id"]);
                objForum.uid = Convert.ToInt32(drBoard["userid"]);
                objForum.id = Convert.ToInt32(drBoard["articleid"]);
                objForum.idtype = "blogid";
                objForum.author = drBoard["username"] != DBNull.Value ? drBoard["username"].ToString() : "";
                objForum.authorid = GetUIDbyUsername(objForum.author);
                objForum.ip = drBoard["ip"].ToString();
                objForum.dateline = Utils.TypeParse.DateTime2TimeStamp(Convert.ToDateTime(drBoard[".ToString("]));
                objForum.message = drBoard["commencontent"].ToString();
                objForum.magicflicker =0;
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
