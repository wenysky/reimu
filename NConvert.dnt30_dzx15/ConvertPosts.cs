using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;
using NConvert.Entity;
using System.Text.RegularExpressions;

namespace NConvert.dnt30_dzx15
{
    public partial class Provider : IProvider
    {
        #region IProvider 成员


        public int GetPostsRecordCount()
        {
            //#warning debug
            //            return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT(pid) FROM {0}posts1 WHERE tid=28696", MainForm.cic.SrcDbTablePrefix)));
            //#warning end debug
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

            //#warning debug
            //            sql = string.Format
            //                   ("SELECT TOP {1} * FROM {0}posts1 WHERE tid=28696 ORDER BY pid", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            //#warning end debug

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
                objPost.invisible = Convert.ToInt32(dr["invisible"]) == 1 ? -1 : 0;
                objPost.anonymous = 0;
                objPost.usesig = Convert.ToInt32(dr["usesig"]);
                objPost.htmlon = Utils.Text.IsIncludeHtmlTag(objPost.message) ? 1 : 0;//Convert.ToInt32(dr["htmlon"]);
                objPost.bbcodeoff = Convert.ToInt32(dr["bbcodeoff"]);
                objPost.smileyoff = Convert.ToInt32(dr["smileyoff"]);
                objPost.parseurloff = Convert.ToInt32(dr["parseurloff"]);
                objPost.attachment = Convert.ToInt32(dr["attachment"]);

                MatchCollection mc = Utils.Text.GetMatchFull(objPost.message, @"/bbs/download\.aspx\?id=([0-9]+)");
                if (mc != null && mc.Count > 0)
                {
                    foreach (Match m in mc)
                    {
                        string extaid = m.Groups[1].Value;

                        //string srcExtDbConn = string.Format("Data Source={0};Initial Catalog=science;User ID={1};Password={2};", 
                        //    MainForm.cic.SrcDbAddress,
                        //    MainForm.cic.SrcDbUsername,
                        //    MainForm.cic.SrcDbUserpassword
                        //    );
                        Yuwen.Tools.TinyData.DBHelper dbhExtattach = MainForm.GetSrcDBH();
                        dbhExtattach.Open();
                        System.Data.Common.DbDataReader drExtattach = dbhExtattach.ExecuteReader(
                            string.Format("SELECT * FROM [science].[dbo].[kexue_appendix] WHERE content='{0}'", extaid)
                            );
                        if (drExtattach.Read())
                        {
                            int extNewaid;
                            if (MainForm.extAttachList.Count > 0)
                            {
                                extNewaid = MainForm.extAttachList[MainForm.extAttachList.Count - 1].aid + 1;
                            }
                            else
                            {
                                extNewaid = MainForm.extAttachAidStartIndex + 1;
                            }

                            Attachments objAttachment = new Attachments();
                            objAttachment.aid = extNewaid;
                            objAttachment.tid = objPost.tid;
                            objAttachment.pid = objPost.pid;
                            objAttachment.width = 0;
                            objAttachment.dateline = 0;
                            objAttachment.readperm = 0;
                            objAttachment.price = Convert.ToInt32(drExtattach["mymoney"]);
                            objAttachment.filename = drExtattach["title"].ToString();
                            objAttachment.filetype = "application/octet-stream";// dr["filetype"].ToString();
                            objAttachment.filesize = Convert.ToInt32(drExtattach["field"]);
                            objAttachment.attachment = drExtattach["url"].ToString();
                            objAttachment.downloads = Convert.ToInt32(drExtattach["dnum"]);
                            List<string> isImage = new List<string>();
                            isImage.Add(".jpg");
                            isImage.Add(".gif");
                            isImage.Add(".png");
                            isImage.Add(".jpeg");
                            if (isImage.Contains(System.IO.Path.GetExtension(objAttachment.filename.Trim())))
                            {
                                objAttachment.isimage = -1;
                            }
                            else
                            {
                                objAttachment.isimage = 0;
                            }
                            objAttachment.uid = Convert.ToInt32(drExtattach["newsid"]);
                            objAttachment.thumb = 0;
                            objAttachment.remote = 0;
                            objAttachment.picid = 0;
                            objAttachment.description = 0;
                            MainForm.extAttachList.Add(objAttachment);
                            objPost.attachment = 1;
                            //[attach]
                            //objPost.message = Utils.Text.ReplaceRegex(@"/bbs/download\.aspx\?id=" + m.Groups[0].Value, objPost.message, m.Groups[0].Value);
                        }

                        drExtattach.Close();
                        drExtattach.Dispose();
                    }
                }

                objPost.rate = Convert.ToInt32(dr["rate"]);
                objPost.ratetimes = Convert.ToInt32(dr["ratetimes"]);
                objPost.status = Convert.ToInt32(dr["invisible"]) == -2 ? 1 : 0;
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
