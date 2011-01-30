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


        public int GetGroupPostRecordCount()
        {
            return Convert.ToInt32(
                MainForm.srcDBH.ExecuteScalar(
                string.Format(
                "SELECT COUNT(id) FROM [science].[dbo].[group_forum]",
                MainForm.cic.SrcDbTablePrefix)
                )
                );
        }

        public List<Posts> GetGroupPostList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM [science].[dbo].[group_reforum] ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM [science].[dbo].[group_reforum] WHERE id NOT IN (SELECT TOP {2} id FROM [science].[dbo].[group_reforum] ORDER BY id) ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion



            System.Data.Common.DbDataReader dr = MainForm.srcDBH.ExecuteReader(sql);

            List<Posts> postlist = new List<Posts>();
            while (dr.Read())
            {
                Posts objPost = new Posts();
                objPost.pid = Convert.ToInt32(dr["id"]);
                objPost.fid = 0;
                objPost.tid = Convert.ToInt32(dr["topicid"]);
                objPost.first = 0;
                objPost.author = dr["username"].ToString();
                objPost.authorid = GetUIDbyUsername(objPost.author); ;
                objPost.subject = "";
                objPost.dateline = Utils.TypeParse.DateTime2TimeStamp(Convert.ToDateTime(dr["posttime"]));
                objPost.message = dr["content"].ToString().Replace("<br>", "\r\n").Replace("<br/>", "\r\n").Replace("<br />", "\r\n");
                objPost.message = Utils.Text.HtmlDecode(Utils.Text.RemoveHtml(objPost.message));
                objPost.useip = dr["postip"].ToString();
                objPost.invisible = 0;
                objPost.anonymous = 0;
                objPost.usesig = 1;
                objPost.htmlon = 1;
                objPost.bbcodeoff = 0;
                objPost.smileyoff = 0;
                objPost.parseurloff = 0;
                objPost.attachment = 0;

                objPost.rate = 0;
                objPost.ratetimes = 0;
                objPost.status = 0;
                objPost.tags = "";
                objPost.comment = 0;
                postlist.Add(objPost);

            }
            dr.Close();
            dr.Dispose();
            return postlist;
        }


        #endregion
    }
}
