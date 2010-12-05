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


        public int GetBlogRecordCount()
        {
            return Convert.ToInt32(
                MainForm.srcDBH.ExecuteScalar(
                string.Format(
                "SELECT COUNT(id) FROM [science].[dbo].[kexue_blogarticle]",
                MainForm.cic.SrcDbTablePrefix)
                )
                );
        }

        public List<BlogPostInfo> GetBlogList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM [science].[dbo].[kexue_blogarticle] ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM [science].[dbo].[kexue_blogarticle] WHERE id NOT IN (SELECT TOP {2} id FROM [science].[dbo].[kexue_blogarticle] ORDER BY id) ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion



            System.Data.Common.DbDataReader dr = MainForm.srcDBH.ExecuteReader(sql);
            List<BlogPostInfo> blogPostlist = new List<BlogPostInfo>();
            while (dr.Read())
            {
                BlogPostInfo objBlogPostList = new BlogPostInfo();
                objBlogPostList.blogid = Convert.ToInt32(dr["id"]);
                objBlogPostList.uid = Convert.ToInt32(dr["homeurl"]);
                objBlogPostList.username = dr["username"].ToString();
                objBlogPostList.subject = dr["title"].ToString();
                objBlogPostList.classid = Convert.ToInt32(dr["typeid"]);
                objBlogPostList.catid = Convert.ToInt32(dr["classid"]);
                objBlogPostList.viewnum = Convert.ToInt32(dr["hits"]);
                objBlogPostList.replynum = Convert.ToInt32(dr["commen"]);
                objBlogPostList.hot = 0;
                objBlogPostList.dateline = Utils.TypeParse.DateTime2TimeStamp(Convert.ToDateTime(dr["writetime"]));
                objBlogPostList.blogtype = Convert.ToInt32(dr["iszz"]);
                objBlogPostList.picflag = (dr["upimages"] != DBNull.Value && dr["upimages"].ToString().Trim() != "") ? 1 : 0;
                objBlogPostList.noreply = (Convert.ToInt32(dr["ifcommen"]) == 0 || Convert.ToInt32(dr["ifcommen"]) == 3) ? 1 : 0;
                objBlogPostList.friend = 0;
                objBlogPostList.password = "";
                objBlogPostList.favtimes = 0;
                objBlogPostList.sharetimes = 0;
                objBlogPostList.status = 0;
                objBlogPostList.click1 = 0;
                objBlogPostList.click2 = 0;
                objBlogPostList.click3 = 0;
                objBlogPostList.click4 = 0;
                objBlogPostList.click5 = 0;
                objBlogPostList.click6 = 0;
                objBlogPostList.click7 = 0;
                objBlogPostList.click8 = 0;
                objBlogPostList.stickstatus = Convert.ToInt32(dr["ifgood"]);
                objBlogPostList.recommendstatus = Convert.ToInt32(dr["ifgood"]);

                objBlogPostList.showtitle = dr["subtitle"] == DBNull.Value ? "" : dr["subtitle"].ToString().Trim();
                objBlogPostList.rfirstid = 0;
                objBlogPostList.lastchangetime = Utils.TypeParse.DateTime2TimeStamp(Convert.ToDateTime(dr["updatetime"]));
                objBlogPostList.recommendnum = Convert.ToInt32(dr["tuijian"]);




                objBlogPostList.pic = "";
                objBlogPostList.tag = "";
                objBlogPostList.message = dr["content"].ToString();
                objBlogPostList.postip = "";
                objBlogPostList.related = "";
                objBlogPostList.relatedtime = 0;
                objBlogPostList.target_ids = "";
                objBlogPostList.hotuser = "";
                objBlogPostList.magiccolor = 0;
                objBlogPostList.magicpaper = 0;
                objBlogPostList.pushedaid = 0;
                blogPostlist.Add(objBlogPostList);
            }
            dr.Close();
            dr.Dispose();
            return blogPostlist;
        }

        #endregion
    }
}
