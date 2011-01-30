using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;
using NConvert.Entity;

namespace NConvert.dvnet11_dnt21202
{
    public partial class Provider : IProvider
    {
        #region IProvider 成员


        public int GetTopicsRecordCount()
        {
            return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT([Id]) FROM {0}Topic", MainForm.cic.SrcDbTablePrefix)));
        }

        public List<Topics> GetTopicList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {1} Id, Type, BoardId, SubjectId, IconId, Title, TitleColor, Body, AuthorId, AuthorName,IssueTime, Views, Replies, LastReplyId, LastReplyUID,LastReplyAuthor,LastReplyTime, UDTime, Require, Price, Topped, Elited, Locked, Deleted FROM {0}Topic ORDER BY Id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} Id, Type, BoardId, SubjectId, IconId, Title, TitleColor, Body, AuthorId, AuthorName,IssueTime, Views, Replies, LastReplyId, LastReplyUID,LastReplyAuthor,LastReplyTime, UDTime, Require, Price, Topped, Elited, Locked, Deleted FROM {0}Topic WHERE Id NOT IN (SELECT TOP {2} Id FROM {0}Topic) ORDER BY Id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion

            System.Data.Common.DbDataReader dr = MainForm.srcDBH.ExecuteReader(sql);
            List<Topics> topiclist = new List<Topics>();
            while (dr.Read())
            {
                //TODO 彩色标题
                Topics objTopic = new Topics();
                objTopic.tid = Convert.ToInt32(dr["Id"]);
                objTopic.fid = Convert.ToInt16(dr["BoardId"]);
                //objTopic.iconid = dr[""] ;
                //如果转换主题分类,则转换此字段
                if (MainForm.IsConvertTopicTypes)
                {
                    objTopic.typeid = Convert.ToInt32(dr["SubjectId"]);//将此字段事先更新为int
                }
                objTopic.readperm = Convert.ToInt32(dr["Require"]);
                objTopic.price = Convert.ToInt16(dr["Price"]);
                objTopic.poster = dr["AuthorName"].ToString();
                objTopic.posterid = Convert.ToInt32(dr["AuthorId"]);
                objTopic.title = dr["Title"].ToString();
                objTopic.postdatetime = Convert.ToDateTime(dr["IssueTime"]);

                objTopic.lastpost = dr["LastReplyTime"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["LastReplyTime"]);
                if (MainForm.IsResetTopicLastpostid)
                {
                    objTopic.lastpostid = 785200 + objTopic.tid;
                }
                else
                {
                    objTopic.lastpostid = dr["LastReplyId"] == DBNull.Value ? objTopic.tid : Convert.ToInt32(dr["LastReplyId"]);
                }
                objTopic.lastposter = dr["LastReplyAuthor"] == DBNull.Value ? "" : dr["LastReplyAuthor"].ToString();
                objTopic.lastposterid = dr["LastReplyUID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["LastReplyUID"]);

                objTopic.views = Convert.ToInt32(dr["Views"]);
                objTopic.replies = Convert.ToInt32(dr["Replies"]);


                objTopic.displayorder = Convert.ToInt32(dr["Topped"]) > 0 ? Convert.ToInt32(dr["Topped"]) : 0;


                if (Convert.ToBoolean(dr["Deleted"]))
                {
                    objTopic.displayorder = -1;
                }

                objTopic.highlight = dr["TitleColor"] == DBNull.Value ? "" : dr["TitleColor"].ToString(); ;


                objTopic.digest = Convert.ToByte(dr["Elited"]);
                //objTopic.rate = dr[""] ;
                objTopic.hide = 1; //TODO
                //objTopic.poll = dr[""] ;
                objTopic.attachment = 1;
                //objTopic.moderated = dr[""] ;
                objTopic.closed = Convert.ToInt32(dr["Locked"]);
                //objTopic.magic = dr[""] ;
                //objTopic.identify= dr[""] ;
                topiclist.Add(objTopic);
            }
            dr.Close();
            dr.Dispose();
            return topiclist;
        }

        #endregion
    }
}
