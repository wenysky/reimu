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


        public int GetIndexRecomandBlogPicRecordCount()
        {
            return Convert.ToInt32(
                MainForm.srcDBH.ExecuteScalar(
                string.Format(
                "SELECT COUNT(id) FROM [science].[dbo].[kexue_blogpic]",
                MainForm.cic.SrcDbTablePrefix)
                )
                );
        }

        public List<IndexRecomandBlogPicInfo> GetIndexRecomandBlogPicList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM [science].[dbo].[kexue_blogpic] ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM [science].[dbo].[kexue_blogpic] WHERE id NOT IN (SELECT TOP {2} id FROM [science].[dbo].[kexue_blogpic] ORDER BY id) ORDER BY id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion



            System.Data.Common.DbDataReader dr = MainForm.srcDBH.ExecuteReader(sql);
            List<IndexRecomandBlogPicInfo> Recommandlist = new List<IndexRecomandBlogPicInfo>();
            while (dr.Read())
            {
                IndexRecomandBlogPicInfo objFriend = new IndexRecomandBlogPicInfo();
                objFriend.rpid = Convert.ToInt32(dr["id"]);
                objFriend.title = dr["name"] != DBNull.Value ? dr["name"].ToString().Trim() : "";
                objFriend.pictype = dr["ifhead"] != DBNull.Value ? Convert.ToInt32(dr["ifhead"]) : 0;
                objFriend.picsrc = dr["pic"] != DBNull.Value ? "olddata/kexue.com.cn/admin/" + dr["pic"].ToString().Trim().Trim('/') : "";


                string link = dr["link"] != DBNull.Value ? dr["link"].ToString().Trim() : "";
                int rblogid = 0;
                int uid = 0;

                if (link != string.Empty)
                {
                    string blogid = Utils.Text.GetMatch(link, "user_content.aspx\\?id=([0-9]+).*?");

                    if (blogid != string.Empty && int.TryParse(blogid, out rblogid))
                    {
                        //如果blogid》0，那么就是日志链接了
                        uid = GetUIDbyBlogid(rblogid);
                        objFriend.linksrc = string.Format(
                                "home.php?mod=space&uid={0}&do=blog&id={1}&from=space",
                                uid,
                                rblogid
                                );
                    }
                    else
                    {
                        //如果blogid=0，就尝试弄uid
                        string suid = Utils.Text.GetMatch(link, "user_index.aspx\\?userid=([0-9]+).*?");
                        if (suid != null && suid.Trim() != string.Empty && int.TryParse(suid, out uid))
                        {
                            //如果uid》0，就是空间链接
                            objFriend.linksrc = string.Format(
                                "home.php?mod=space&uid={0}",
                                uid
                                );
                        }
                        else if (objFriend.pictype == 2)
                        {
                            //如果pictype==2，就是空间链接(姑且相信)
                            objFriend.linksrc = string.Format(
                                "home.php?mod=space&uid={0}",
                                dr["userid"].ToString().Trim()
                                );
                        }
                        else
                        {
                            //blogid=0,uid=0，那么就保持原装吧
                            objFriend.linksrc = link;
                        }
                    }

                    /*飕飕飕
                     * 
                    if (objFriend.pictype == 2)
                    {
                        objFriend.linksrc = string.Format(
                            "home.php?mod=space&uid={0}",
                            Utils.Text.GetMatch(link, "http://www.sciencenet.cn/blog/user_index.aspx\\?userid=([0-9]+).*?")
                            );
                    }
                    else if (objFriend.pictype == 1 || objFriend.pictype == 3)
                    {
                        string sblogid = Utils.Text.GetMatch(link, "http://www.sciencenet.cn/blog/user_content.aspx\\?id=([0-9]+).*?");
                        
                        if (sblogid != null && sblogid.Trim() != string.Empty && int.TryParse(sblogid, out blogid))
                        {
                            uid = GetUIDbyBlogid(blogid);
                            objFriend.linksrc = string.Format(
                                "home.php?mod=space&uid={0}&do=blog&id={1}&from=space",
                                uid,
                                blogid
                                );
                        }
                        else
                        {
                            objFriend.linksrc = link;
                        }
                    }
                    else
                    {
                        objFriend.linksrc = link;
                    }
                     */
                }
                else
                {
                    objFriend.linksrc = "";
                }
                objFriend.userid = dr["userid"] != DBNull.Value ? Convert.ToInt32(dr["userid"]) : uid;
                objFriend.readme = dr["readme"] != DBNull.Value ? dr["readme"].ToString().Trim() : "";
                Recommandlist.Add(objFriend);
            }
            dr.Close();
            dr.Dispose();
            return Recommandlist;
        }

        #endregion
    }
}
