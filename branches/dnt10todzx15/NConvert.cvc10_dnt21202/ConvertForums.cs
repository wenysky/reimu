using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;
using NConvert.Entity;
using Yuwen.Tools.Data;

namespace NConvert.cvc10_dnt21202
{
    public partial class Provider : IProvider
    {
        public int GetForumsRecordCount()
        {
            if (MainForm.RecordCount == -1)
            {
                return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT([Id]) FROM (SELECT [Id] FROM {0}Board UNION ALL SELECT [Id] FROM {0}Category)a", MainForm.srcDbTableProfix)));
            }
            else
            {
                return MainForm.RecordCount;
            }
        }

        public List<Forums> GetForumList()
        {
            string sqlBoard = string.Format
                       ("SELECT Id,CategoryId,ParentId,SortId,BoardName,Description,Topics,Replies,Childs,LastTopicId,LastTopicTitle,LastTopicTime,LastTopicUID,LastTopicAuthor,Announcement,Hidden,ChildHorizontal,ChildColumns FROM {0}Board ORDER BY Id", MainForm.srcDbTableProfix);

            int maxfid = Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT MAX(Id) FROM {0}Board", MainForm.srcDbTableProfix)));//取得最大的自增号,重排CategoryId

            System.Data.Common.DbDataReader drBoard = MainForm.srcDBH.ExecuteReader(sqlBoard);

            List<Forums> forumList = new List<Forums>();
            while (drBoard.Read())
            {
                Forums objForum = new Forums();
                objForum.fid = Convert.ToInt32(drBoard["Id"]);
                if (Convert.ToInt32(drBoard["ParentId"]) == 0)
                {
                    objForum.parentid = Convert.ToInt16(Convert.ToInt16(drBoard["CategoryId"]) + maxfid);
                }
                else
                {
                    objForum.parentid = Convert.ToInt16(drBoard["ParentId"]);
                }
                objForum.displayorder = Convert.ToInt32(drBoard["SortId"]);
                objForum.name = drBoard["BoardName"].ToString();
                objForum.description = drBoard["Description"].ToString();
                objForum.topics = Convert.ToInt32(drBoard["Topics"]);
                objForum.posts = Convert.ToInt32(drBoard["Replies"]) + objForum.topics;
                objForum.subforumcount = Convert.ToInt32(drBoard["Childs"]);
                objForum.lasttid = Convert.ToInt32(drBoard["LastTopicId"]);
                objForum.lasttitle = drBoard["LastTopicTitle"].ToString();
                objForum.lastpost = Convert.ToDateTime(drBoard["LastTopicTime"]);
                objForum.lastposterid = Convert.ToInt32(drBoard["LastTopicUID"]);
                objForum.lastposter = drBoard["LastTopicAuthor"].ToString();
                objForum.rules = drBoard["Announcement"].ToString();
                objForum.status = Convert.ToInt32(drBoard["Hidden"]) == 1 ? 0 : 1;
                if (Convert.ToInt32(drBoard["ChildHorizontal"]) == 1)
                {
                    objForum.colcount = Convert.ToInt16(drBoard["ChildColumns"]);
                }

                forumList.Add(objForum);
            }
            drBoard.Close();
            drBoard.Dispose();

            string sqlCategory = string.Format
                       ("SELECT Id,CategoryName,Description,Horizontal,Columns,Visible,SortId FROM {0}Category ORDER BY Id", MainForm.srcDbTableProfix);
            System.Data.Common.DbDataReader drCategory = MainForm.srcDBH.ExecuteReader(sqlCategory);
            while (drCategory.Read())
            {
                Forums objForumCategory = new Forums();
                objForumCategory.fid = Convert.ToInt32(drCategory["Id"]) + maxfid;
                objForumCategory.name = drCategory["CategoryName"].ToString();
                objForumCategory.description = drCategory["Description"].ToString();
                if (Convert.ToInt32(drCategory["Horizontal"]) == 1)
                {
                    objForumCategory.colcount = Convert.ToInt16(drCategory["Columns"]);
                }
                objForumCategory.status = Convert.ToInt32(drCategory["Visible"]);
                objForumCategory.displayorder = Convert.ToInt32(drCategory["SortId"]);
                forumList.Add(objForumCategory);
            }
            drCategory.Close();
            drCategory.Dispose();
            return forumList;
        }
    }
}
