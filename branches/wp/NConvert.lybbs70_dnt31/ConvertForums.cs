using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;
using NConvert.Entity;
using Yuwen.Tools.Data;

namespace NConvert.lybbs70_dnt31
{
    public partial class Provider : IProvider
    {
        public int GetForumsRecordCount()
        {
            if (MainForm.RecordCount == -1)
            {
                return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT(id) FROM (SELECT id FROM {0}db UNION ALL SELECT Id FROM {0}forum)a", MainForm.cic.SrcDbTablePrefix)));
            }
            else
            {
                return MainForm.RecordCount;
            }
        }

        public List<Forums> GetForumList()
        {
            string sqlBoard = string.Format
                       ("SELECT * FROM {0}db ORDER BY id", MainForm.cic.SrcDbTablePrefix);

            int maxfid = Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT MAX(id) FROM {0}db", MainForm.cic.SrcDbTablePrefix)));//取得最大的自增号,重排分区id

            System.Data.Common.DbDataReader drBoard = MainForm.srcDBH.ExecuteReader(sqlBoard);

            List<Forums> forumList = new List<Forums>();
            while (drBoard.Read())
            {
                Forums objForum = new Forums();
                objForum.fid = Convert.ToInt32(drBoard["id"]);
                if (Convert.ToInt32(drBoard["parentid"]) == 0)//如果是板块的子版块，则不会变（重排的只是分区）
                {
                    objForum.parentid = Convert.ToInt16(Convert.ToInt16(drBoard["forum"]) + maxfid);
                }
                else
                {
                    objForum.parentid = Convert.ToInt16(drBoard["ParentId"]);
                }
                objForum.displayorder = Convert.ToInt32(drBoard["db"]);
                objForum.name = drBoard["dbname"].ToString();
                objForum.description = drBoard["dbdescription"].ToString();
                objForum.topics = Convert.ToInt32(drBoard["topicnumber"]);
                objForum.posts = Convert.ToInt32(drBoard["replynumber"]) + objForum.topics;
                //objForum.subforumcount = Convert.ToInt32(drBoard["Childs"]);
                //objForum.lasttid = Convert.ToInt32(drBoard["LastTopicId"]);
                //objForum.lasttitle = drBoard["LastTopicTitle"].ToString();
                //objForum.lastpost = Convert.ToDateTime(drBoard["LastTopicTime"]);
                //objForum.lastposterid = Convert.ToInt32(drBoard["LastTopicUID"]);
                //objForum.lastposter = drBoard["LastTopicAuthor"].ToString();
                //objForum.rules = drBoard["Announcement"].ToString();
                //objForum.status = Convert.ToInt32(drBoard["Hidden"]) == 1 ? 0 : 1;
                //if (Convert.ToInt32(drBoard["ChildHorizontal"]) == 1)
                //{
                //    objForum.colcount = Convert.ToInt16(drBoard["ChildColumns"]);
                //}

                forumList.Add(objForum);
            }
            drBoard.Close();
            drBoard.Dispose();

            string sqlCategory = string.Format
                       ("SELECT * FROM {0}forum ORDER BY Id", MainForm.cic.SrcDbTablePrefix);
            System.Data.Common.DbDataReader drCategory = MainForm.srcDBH.ExecuteReader(sqlCategory);
            while (drCategory.Read())
            {
                Forums objForumCategory = new Forums();
                objForumCategory.fid = Convert.ToInt32(drCategory["Id"]) + maxfid;
                objForumCategory.name = drCategory["forumname"].ToString();
                //objForumCategory.description = drCategory["Description"].ToString();
                //if (Convert.ToInt32(drCategory["Horizontal"]) == 1)
                //{
                //    objForumCategory.colcount = Convert.ToInt16(drCategory["Columns"]);
                //}
                //objForumCategory.status = Convert.ToInt32(drCategory["Visible"]);
                objForumCategory.displayorder = Convert.ToInt32(drCategory["forum"]);
                forumList.Add(objForumCategory);
            }
            drCategory.Close();
            drCategory.Dispose();
            return forumList;
        }
    }
}
