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


        public int GetPostsRecordCount()
        {
            return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT(pid) FROM {0}posts1", MainForm.cic.SrcDbTablePrefix)));
        }

        /// <summary>
        /// 得到分页转换帖子泛型列表
        /// </summary>
        /// <param name="CurrentPage">当前分页</param>
        /// <returns></returns>
        public List<Posts> GetPostList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM {0}posts1 ORDER BY pid", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM {0}posts1 WHERE pid NOT IN (SELECT TOP {2} pid FROM {0}posts1 ORDER BY pid) ORDER BY pid", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion

            System.Data.Common.DbDataReader dr = MainForm.srcDBH.ExecuteReader(sql);
            List<Posts> postlist = new List<Posts>();
            while (dr.Read())
            {
                Posts objPost = new Posts();
                objPost.pid = Convert.ToInt32(dr["pid"]);
                objPost.fid = Convert.ToInt32(dr["fid"]);
                objPost.tid = Convert.ToInt32(dr["tid"]);
                objPost.first = Convert.ToInt32(dr["layer"]) == 0 ? 1 : 0;
                objPost.author = dr["poster"].ToString();
                objPost.authorid = Convert.ToInt32(dr["posterid"]);
                objPost.subject = dr["title"].ToString();
                objPost.dateline = Utils.TypeParse.DateTime2TimeStamp(Convert.ToDateTime(dr["postdatetime"]));
                objPost.message = dr["message"].ToString();
                objPost.useip = dr["ip"].ToString();
                objPost.invisible = Convert.ToInt32(dr["invisible"]);
                objPost.anonymous = 0;
                objPost.usesig = Convert.ToInt32(dr["usesig"]);
                objPost.htmlon = Convert.ToInt32(dr["htmlon"]);
                objPost.bbcodeoff = Convert.ToInt32(dr["bbcodeoff"]);
                objPost.smileyoff = Convert.ToInt32(dr["smileyoff"]);
                objPost.parseurloff = Convert.ToInt32(dr["parseurloff"]);
                objPost.attachment = Convert.ToInt32(dr["attachment"]);
                objPost.rate = Convert.ToInt32(dr["rate"]);
                objPost.ratetimes = Convert.ToInt32(dr["ratetimes"]);
                objPost.status = 0;
                objPost.tags = "";
                objPost.comment = 0;
                postlist.Add(objPost);
            }
            dr.Close();
            dr.Dispose();
            return postlist;
        }

        /// <summary>
        /// 帖子内容UBB替换
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private static string ConvertUBB(string content)
        {
            string pattern = @"\[uploadimage\]([0-9]+),([\s\S]+?)\[\/uploadimage\]";
            string replacement = @"[attachimg]$1[/attachimg]";
            content = Utils.Posts.ReplaceRegex(pattern, content, replacement);

            pattern = @"\[uploadfile\]([0-9]+)+,([\s\S]+?)\[\/uploadfile\]";
            replacement = @"[attach]$1[/attach]";
            content = Utils.Posts.ReplaceRegex(pattern, content, replacement);

            pattern = @"\[flash\=.*\](.*)\[\/flash\]";
            replacement = @"[flash]$1[/flash]";
            content = Utils.Posts.ReplaceRegex(pattern, content, replacement);

            pattern = @"\[mp\=([0-9]+)+,([0-9]+)+\](.*)\[\/mp\]";
            replacement = @"[wmv=$1,$2]$3[/wmv]";
            content = Utils.Posts.ReplaceRegex(pattern, content, replacement);

            pattern = @"\[quotetitle\](.*)\[\/quotetitle\]\s+\[quote\](.*)\[\/quote\]";
            replacement = @"[quote]$1<br />$2[/quote]";
            content = Utils.Posts.ReplaceRegex(pattern, content, replacement);

            //去掉编辑信息
            pattern = @"\[reedit\](.*)\[\/reedit\]";
            replacement = @"";
            content = Utils.Posts.ReplaceRegex(pattern, content, replacement);
            return content;
        }
        /// <summary>
        /// 取得编辑信息
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private static string GetLastEditInfo(string content)
        {
            string pattern = @"\[reedit\](.*)\[\/reedit\]";
            string replacement = @"$1";
            return Utils.Posts.ReplaceRegex(pattern, content, replacement);
        }
        #endregion
    }
}
