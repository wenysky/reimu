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
            return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT(Id) FROM {0}posts", MainForm.cic.SrcDbTablePrefix)));
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
            sql = string.Format(
                "SELECT * FROM {0}posts p LEFT JOIN {0}postcontent pc ON p.id = pc.postID ORDER BY id LIMIT {1},{2}", 
                MainForm.cic.SrcDbTablePrefix, 
                MainForm.PageSize * (CurrentPage - 1), 
                MainForm.PageSize
                );
            #endregion

            System.Data.Common.DbDataReader dr = MainForm.srcDBH.ExecuteReader(sql);
            List<Posts> postlist = new List<Posts>();
            while (dr.Read())
            {
                Posts objPost = new Posts();
                objPost.pid = Convert.ToInt32(dr["id"]);
                objPost.tid = Convert.ToInt32(dr["parentid"]) == 0 ? objPost.pid : Convert.ToInt32(dr["parentid"]);//如果没有父ID，就说明它是主题贴
                objPost.message = ConvertUBB(dr["content"].ToString());
                //objPost.lastedit = GetLastEditInfo(dr["Body"].ToString());
                objPost.posterid = Convert.ToInt32(dr["authorid"]);
                objPost.poster = dr["author"].ToString();//CVC长度40>20
                objPost.ip = dr["ipfrom"].ToString();
                objPost.postdatetime = Convert.ToDateTime(dr["postat"]);
                if (dr["accessaryname"] != DBNull.Value && dr["accessaryname"].ToString().Trim() != "")
                {
                    objPost.attachment = 1;
                }
                //objPost.invisible = Convert.ToInt32(dr["Deleted"]);

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
