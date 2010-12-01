﻿using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;
using NConvert.Entity;

namespace NConvert.dnt30_dzx15
{
    public partial class Provider : IProvider
    {
        #region IProvider 成员


        public int GetTopicsRecordCount()
        {
            //#warning debug
            //            return Convert.ToInt32(
            //                MainForm.srcDBH.ExecuteScalar(
            //                string.Format(
            //                "SELECT COUNT(tid) FROM {0}topics WHERE displayorder>-1 AND tid=28696",
            //                MainForm.cic.SrcDbTablePrefix)
            //                )
            //                );
            //#warning end debug
            return Convert.ToInt32(
                MainForm.srcDBH.ExecuteScalar(
                string.Format(
                "SELECT COUNT(tid) FROM {0}topics WHERE displayorder>-1",
                MainForm.cic.SrcDbTablePrefix)
                )
                );
        }

        public List<Topics> GetTopicList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM {0}topics WHERE displayorder>-1 ORDER BY tid", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM {0}topics WHERE displayorder>-1 AND tid NOT IN (SELECT TOP {2} tid FROM {0}topics WHERE displayorder>-1 ORDER BY tid) ORDER BY tid", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion

            //#warning debug
            //            sql = string.Format
            //                   ("SELECT TOP {1} * FROM {0}topics WHERE displayorder>-1 AND tid=28696 ORDER BY tid", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            //#warning end debug


            System.Data.Common.DbDataReader dr = MainForm.srcDBH.ExecuteReader(sql);
            List<Topics> topiclist = new List<Topics>();
            while (dr.Read())
            {
                Topics objTopic = new Topics();
                objTopic.tid = Convert.ToInt32(dr["tid"]);
                objTopic.fid = Convert.ToInt32(dr["fid"]);
                //todo 分表
                objTopic.posttableid = 0;
                objTopic.typeid = Convert.ToInt32(string.Format("{0}{1}", objTopic.fid, dr["typeid"].ToString()));
                objTopic.sortid = 0;
                objTopic.readperm = Convert.ToInt32(dr["readperm"]);
                objTopic.author = dr["poster"].ToString();
                objTopic.authorid = Convert.ToInt32(dr["posterid"]);
                objTopic.subject = dr["title"].ToString();
                objTopic.dateline = Utils.TypeParse.DateTime2TimeStamp(Convert.ToDateTime(dr["postdatetime"]));
                objTopic.lastpost = Utils.TypeParse.DateTime2TimeStamp(Convert.ToDateTime(dr["lastpost"]));
                objTopic.lastposter = dr["lastposter"].ToString();
                objTopic.views = Convert.ToInt32(dr["views"]);
                objTopic.replies = Convert.ToInt32(dr["replies"]);
                objTopic.displayorder = Convert.ToInt32(dr["displayorder"]);
                if (dr["highlight"].ToString() != string.Empty)
                {
                    //todo
                    objTopic.highlight = 41;
                }
                else
                {
                    objTopic.highlight = 0;
                }
                objTopic.digest = Convert.ToInt32(dr["digest"]);
                objTopic.rate = Convert.ToInt32(dr["rate"]);
                int specialId = GetConvertedSpecialId(Convert.ToInt32(dr["special"]));
                if (specialId == -3)
                {
                    //已经结贴的悬赏
                    objTopic.special = -specialId;
                    objTopic.price = -Convert.ToInt32(dr["price"]);
                }
                else
                {
                    objTopic.special = specialId;
                    objTopic.price = Convert.ToInt32(dr["price"]);
                }
                objTopic.attachment = Convert.ToInt32(dr["attachment"]);
                objTopic.moderated = Convert.ToInt32(dr["moderated"]);
                objTopic.closed = Convert.ToInt32(dr["closed"]);
                objTopic.stickreply = 0;
                objTopic.recommends = 0;
                objTopic.recommend_add = 0;
                objTopic.recommend_sub = 0;
                objTopic.heats = 0;
                //管理操作后  好像都是32
                objTopic.status = 0;
                objTopic.isgroup = 0;
                objTopic.favtimes = 0;
                objTopic.sharetimes = 0;
                objTopic.stamp = -1;
                objTopic.icon = -1;
                objTopic.pushedaid = 0;
                DateTime recommenddate;
                if (dr["tuijiantime"] != DBNull.Value
                    && DateTime.TryParse(
                          dr["tuijiantime"].ToString().Trim(),
                          out recommenddate
                          )
                    )
                {
                    objTopic.recommend = Utils.TypeParse.DateTime2TimeStamp(recommenddate);
                }
                else
                {
                    objTopic.recommend = 0;
                }
                topiclist.Add(objTopic);
            }
            dr.Close();
            dr.Dispose();
            return topiclist;
        }


        #endregion

        private int GetConvertedSpecialId(int dntid)
        {
            int newid;
            switch (dntid)
            {
                case 1:
                    newid = 1;
                    break;
                case 2://Discuz!NT正在悬赏
                    newid = 3;
                    break;
                case 3://Discuz!NT悬赏完成
                    newid = -3;
                    break;
                case 4://Discuz!NT辩论
                    newid = 5;
                    break;
                default:
                    newid = 0;
                    break;
            }
            return newid;
        }
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
