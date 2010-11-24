using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;
using NConvert.Entity;

namespace NConvert.cvc20beta_dnt21202
{
    public partial class Provider : IProvider
    {
        #region IProvider 成员


        public int GetPostsRecordCount()
        {
            return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT([Id]) FROM {0}Reply", MainForm.cic.SrcDbTablePrefix)));
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
                       ("SELECT TOP {1} * FROM {0}Reply ORDER BY Id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM {0}Reply WHERE Id NOT IN (SELECT TOP {2} Id FROM {0}Reply ORDER BY Id) ORDER BY Id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion

            System.Data.Common.DbDataReader dr = MainForm.srcDBH.ExecuteReader(sql);
            List<Posts> postlist = new List<Posts>();
            while (dr.Read())
            {
                Posts objPost = new Posts();
                objPost.pid = Convert.ToInt32(dr["Id"]);
                objPost.tid = Convert.ToInt32(dr["TopicId"]);
                objPost.message = ConvertUBB(dr["Body"].ToString());
                objPost.lastedit = GetLastEditInfo(dr["Body"].ToString());
                objPost.posterid = Convert.ToInt32(dr["AuthorId"]);
                objPost.poster = dr["AuthorName"].ToString();//CVC长度40>20
                objPost.ip = dr["ip"].ToString();
                objPost.postdatetime = Convert.ToDateTime(dr["IssueTime"]);
                if (dr["AttachIds"] != DBNull.Value && dr["AttachIds"].ToString().Trim() != "")
                {
                    objPost.attachment = 1;
                }
                objPost.invisible = Convert.ToInt32(dr["Deleted"]);

                objPost.parentid = objPost.pid;
                //TODO 板块id需要在后面整理更新
                //layer没有指定

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
