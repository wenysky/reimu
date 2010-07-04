using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;
using NConvert.Entity;

namespace NConvert.lybbs70_dnt31
{
    public partial class Provider : IProvider
    {
        #region IProvider 成员


        public int GetTopicsRecordCount()
        {
            return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT(Id) FROM {0}posts WHERE parentid = 0", MainForm.cic.SrcDbTablePrefix)));
        }

        public List<Topics> GetTopicList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            sql = string.Format(
                "SELECT * FROM {0}posts WHERE parentid = 0 ORDER BY Id LIMIT {1},{2}",
                MainForm.cic.SrcDbTablePrefix,
                MainForm.PageSize * (CurrentPage - 1),
                MainForm.PageSize
                );
            #endregion

            System.Data.Common.DbDataReader dr = MainForm.srcDBH.ExecuteReader(sql);
            List<Topics> topiclist = new List<Topics>();
            while (dr.Read())
            {
                //TODO 彩色标题
                Topics objTopic = new Topics();
                objTopic.tid = Convert.ToInt32(dr["id"]);
                objTopic.fid = Convert.ToInt16(dr["db"]);
                //objTopic.iconid = dr[""] ;
                //如果转换主题分类,则转换此字段
                if (MainForm.IsConvertTopicTypes)
                {
                    objTopic.typeid = Convert.ToInt32(dr["SubjectId"]);//将此字段事先更新为int
                }
                //objTopic.readperm = Convert.ToInt32(dr["Require"]);
                //objTopic.price = Convert.ToInt32(dr["Price"]) > Int16.MaxValue ? Int16.MaxValue : Convert.ToInt16(dr["Price"]);//数据库smallint最大只有3W多
                objTopic.poster = dr["author"].ToString();
                objTopic.posterid = Convert.ToInt32(dr["authorid"]);
                objTopic.title = dr["title"].ToString();
                objTopic.postdatetime = Convert.ToDateTime(dr["postat"]);

                //objTopic.lastpost = dr["lastpostat"] == DBNull.Value ? DateTime.Now : Timestamp2Date(dr["lastpostat"].ToString());
                if (MainForm.IsResetTopicLastpostid)
                {
                    objTopic.lastpostid = 785200 + objTopic.tid;
                }
                else
                {
                    objTopic.lastpostid = objTopic.tid;
                }
                objTopic.lastposter = dr["lastauthor"] == DBNull.Value ? "" : dr["lastauthor"].ToString();
                objTopic.lastposterid = dr["lastauthorid"] == DBNull.Value ? 0 : Convert.ToInt32(dr["lastauthorid"]);

                objTopic.views = Convert.ToInt32(dr["viewnumber"]);
                objTopic.replies = Convert.ToInt32(dr["replynum"]);


                objTopic.displayorder = Convert.ToInt32(dr["istop"]) > 0 ? Convert.ToInt32(dr["istop"]) : 0;


                //if (Convert.ToBoolean(dr["Deleted"]))
                //{
                //    objTopic.displayorder = -1;
                //}

                objTopic.highlight = dr["color"] == DBNull.Value ? "" : "color:" + dr["color"].ToString() + ";";
                if (dr["boldcode"] != DBNull.Value && Convert.ToInt32(dr["boldcode"]) > 0)
                {
                    objTopic.highlight += "font-weight:bold;";
                }


                objTopic.digest = Convert.ToByte(dr["jinghua"]);
                //objTopic.rate = dr[""] ;
                objTopic.hide = 1; //TODO
                objTopic.special = Convert.ToInt32(dr["vote"]) > 0 ? Byte.Parse("1") : Byte.Parse("0");
                objTopic.attachment = 1;
                //objTopic.moderated = dr[""] ;
                objTopic.closed = Convert.ToInt32(dr["titlelock"]);
                //objTopic.magic = dr[""] ;
                //objTopic.identify= dr[""] ;
                topiclist.Add(objTopic);
            }
            dr.Close();
            dr.Dispose();
            return topiclist;
        }

        #endregion

        public static DateTime Timestamp2Date(string s)
        {
            string timeStamp = s;
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
    }
}
