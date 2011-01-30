using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;
using NConvert.Entity;

namespace NConvert.dnt30_dzx15
{
    public partial class Provider : IProvider
    {
        #region IProvider 成员


        public int GetGroupTopicRecordCount()
        {
            return Convert.ToInt32(
                MainForm.srcDBH.ExecuteScalar(
                string.Format(
                "SELECT COUNT(id) FROM [science].[dbo].[group_forum]",
                MainForm.cic.SrcDbTablePrefix)
                )
                );
        }

        public List<TopicsP> GetGroupTopicList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM [science].[dbo].[group_forum] ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM [science].[dbo].[group_forum] WHERE id NOT IN (SELECT TOP {2} id FROM [science].[dbo].[group_forum] ORDER BY id) ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion



            System.Data.Common.DbDataReader dr = MainForm.srcDBH.ExecuteReader(sql);
            List<TopicsP> topiclist = new List<TopicsP>();
            while (dr.Read())
            {
                TopicsP objTopic = new TopicsP();
                objTopic.tid = Convert.ToInt32(dr["id"]);
                objTopic.fid = MainForm.groupidList[dr["group_username"].ToString().Trim()];
                //todo 分表
                objTopic.posttableid = 0;
                objTopic.typeid = 0;
                objTopic.sortid = 0;
                objTopic.readperm = 0;
                objTopic.author = dr["username"].ToString();
                objTopic.authorid = GetUIDbyUsername(objTopic.author);
                objTopic.subject = dr["title"].ToString();
                objTopic.dateline = Utils.TypeParse.DateTime2TimeStamp(Convert.ToDateTime(dr["posttime"]));
                objTopic.lastpost = Utils.TypeParse.DateTime2TimeStamp(Convert.ToDateTime(dr["posttime"]));
                objTopic.lastposter = "";
                objTopic.views = Convert.ToInt32(dr["hits"]);
                objTopic.replies = Convert.ToInt32(dr["replies"]);
                objTopic.displayorder = Convert.ToInt32(dr["istop"]);
                if (dr["titlecolor"].ToString() != string.Empty)
                {
                    //todo
                    objTopic.highlight = 41;
                }
                else
                {
                    objTopic.highlight = 0;
                }
                objTopic.digest = 0;
                objTopic.rate = 0;
                objTopic.special = 0;
                objTopic.attachment = 0;
                objTopic.moderated = 0;
                objTopic.closed = 0;
                objTopic.stickreply = 0;
                objTopic.recommends = 0;
                objTopic.recommend_add = 0;
                objTopic.recommend_sub = 0;
                objTopic.heats = 0;
                //管理操作后  好像都是32
                objTopic.status = 0;
                objTopic.isgroup = 1;
                objTopic.favtimes = 0;
                objTopic.sharetimes = 0;
                objTopic.stamp = -1;
                objTopic.icon = -1;
                objTopic.pushedaid = 0;
                if (Convert.ToInt32(dr["good"]) > 0)
                {
                    objTopic.recommend = Utils.TypeParse.DateTime2TimeStamp(Convert.ToDateTime(dr["posttime"]));
                }
                else
                {
                    objTopic.recommend = 0;
                }

                objTopic.message = dr["content"].ToString().Replace("<br>", "\r\n").Replace("<br/>", "\r\n").Replace("<br />", "\r\n");
                objTopic.message = Utils.Text.HtmlDecode(Utils.Text.RemoveHtml(objTopic.message));
                topiclist.Add(objTopic);

            }
            dr.Close();
            dr.Dispose();
            return topiclist;
        }


        #endregion
    }
}
