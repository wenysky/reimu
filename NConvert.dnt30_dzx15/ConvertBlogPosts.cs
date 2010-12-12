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
                if (dr["fuurl"] != DBNull.Value && dr["fuurl"].ToString().IndexOf("-曹利军-") > -1)//1:科学网编辑部的博客有外链接不要导入了
                {
                    continue;
                }
                BlogPostInfo objBlogPostInfo = new BlogPostInfo();
                objBlogPostInfo.blogid = Convert.ToInt32(dr["id"]);
                objBlogPostInfo.username = dr["username"].ToString();
                objBlogPostInfo.uid = dr["homeurl"] != DBNull.Value ? Convert.ToInt32(dr["homeurl"]) : GetUIDbyUsername(objBlogPostInfo.username);
                objBlogPostInfo.subject = dr["title"].ToString();
                objBlogPostInfo.classid = Convert.ToInt32(dr["typeid"]);
                objBlogPostInfo.catid = Convert.ToInt32(dr["field"]);
                objBlogPostInfo.viewnum = Convert.ToInt32(dr["hits"]);
                objBlogPostInfo.replynum = Convert.ToInt32(dr["commen"]);
                objBlogPostInfo.hot = 0;
                objBlogPostInfo.dateline = Utils.TypeParse.DateTime2TimeStamp(Convert.ToDateTime(dr["writetime"]));
                objBlogPostInfo.blogtype = Convert.ToInt32(dr["iszz"]) + 1;//原表0是原创 1是转载
                objBlogPostInfo.picflag = (dr["upimages"] != DBNull.Value && dr["upimages"].ToString().Trim() != "") ? 1 : 0;
                if (Convert.ToInt32(dr["ifcommen"]) == 0)//0不允许，1允许所有人，2只允许注册用户，3只允许博主
                {
                    objBlogPostInfo.noreply = 1;
                }
                else if (Convert.ToInt32(dr["ifcommen"]) == 1)
                {
                    objBlogPostInfo.noreply = 0;
                }
                else if (Convert.ToInt32(dr["ifcommen"]) == 2)
                {
                    objBlogPostInfo.noreply = 3;
                }
                else if (Convert.ToInt32(dr["ifcommen"]) == 3)
                {
                    objBlogPostInfo.noreply = 2;
                }
                else
                {
                    objBlogPostInfo.noreply = 0;
                }
                //3：以前是隐藏kexue_blogarticle del!=1,[closed]=3的文章和在草稿kexue_blogarticle[del]=1,[closed]=82中的文章都导入到新系统的“仅自己可见”中。
                if ((Convert.ToInt32(dr["del"]) != 1 && Convert.ToInt32(dr["closed"]) == 3) || (Convert.ToInt32(dr["del"]) == 1 && Convert.ToInt32(dr["closed"]) == 82))
                {
                    objBlogPostInfo.friend = 3;
                }
                else
                {
                    objBlogPostInfo.friend = 0;
                }
                objBlogPostInfo.password = "";
                objBlogPostInfo.favtimes = 0;
                objBlogPostInfo.sharetimes = 0;
                objBlogPostInfo.status = 0;
                objBlogPostInfo.click1 = 0;
                objBlogPostInfo.click2 = 0;
                objBlogPostInfo.click3 = 0;
                objBlogPostInfo.click4 = 0;
                objBlogPostInfo.click5 = 0;
                objBlogPostInfo.click6 = 0;
                objBlogPostInfo.click7 = 0;
                objBlogPostInfo.click8 = 0;
                objBlogPostInfo.stickstatus = Convert.ToInt32(dr["ifgood"]);
                if (Convert.ToInt32(dr["blogclose"]) != 2
                    && Convert.ToInt32(dr["del"]) != 1
                    && Convert.ToInt32(dr["ifgood"]) > 1
                    && Convert.ToInt32(dr["closed"]) != 3
                            )
                {
                    objBlogPostInfo.recommendstatus = 3;
                }
                else if (Convert.ToInt32(dr["blogclose"]) != 2
                    && Convert.ToInt32(dr["del"]) != 1
                    && Convert.ToInt32(dr["ifgood"]) == 1
                    && Convert.ToInt32(dr["closed"]) != 3
                            )
                {
                    objBlogPostInfo.recommendstatus = 3;
                }
                else if (Convert.ToInt32(dr["isview"]) == 1
                    && Convert.ToInt32(dr["blogclose"]) != 2
                    && Convert.ToInt32(dr["del"]) != 1
                    && Convert.ToInt32(dr["closed"]) != 3
                            )
                {
                    objBlogPostInfo.recommendstatus = 1;
                }
                else if (Convert.ToInt32(dr["blogclose"]) == 2)
                {
                    objBlogPostInfo.recommendstatus = -1;
                }
                else
                {
                    objBlogPostInfo.recommendstatus = 0;
                }

                objBlogPostInfo.showtitle = dr["subtitle"] == DBNull.Value ? objBlogPostInfo.subject : dr["subtitle"].ToString().Trim();
                objBlogPostInfo.rfirstid = 0;
                objBlogPostInfo.lastchangetime = Utils.TypeParse.DateTime2TimeStamp(Convert.ToDateTime(dr["updatetime"]));
                objBlogPostInfo.recommendnum = Convert.ToInt32(dr["tuijian"]);




                objBlogPostInfo.pic = "";
                objBlogPostInfo.tag = "";
                objBlogPostInfo.message = dr["content"].ToString();
                objBlogPostInfo.postip = "";
                objBlogPostInfo.related = "";
                objBlogPostInfo.relatedtime = 0;
                objBlogPostInfo.target_ids = "";
                objBlogPostInfo.hotuser = "";
                objBlogPostInfo.magiccolor = 0;
                objBlogPostInfo.magicpaper = 0;
                objBlogPostInfo.pushedaid = 0;
                if (Convert.ToInt32(dr["del"]) == 1 && Convert.ToInt32(dr["closed"]) != 82)
                {
                    MainForm.trashBlogPostList.Add(objBlogPostInfo);
                }
                else
                {
                    blogPostlist.Add(objBlogPostInfo);
                }
            }
            dr.Close();
            dr.Dispose();
            return blogPostlist;
        }

        #endregion
    }
}
