using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;
using NConvert.Entity;
using Yuwen.Tools.Data;
using System.Collections;

namespace NConvert.dnt30_dzx15
{
    public partial class Provider : IProvider
    {
        public int GetUsers4OtherRecordCount()
        {
            DBHelper userDBH = MainForm.GetSrcDBH_OldVer();
            return Convert.ToInt32(
                userDBH.ExecuteScalar(
                    string.Format("SELECT COUNT(id) FROM [bbsuser].[dbo].[user]")
                    )
                );
        }




        public List<Users> GetUser4OtherList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {0} id,usermail,username,userpass,regtime,birthday,UserMobile,blogtype,UserInfo,realname,blogshen,ifblog,ifgood,jigoublog,blogname,blogjie,savetime,bloggong FROM [bbsuser].[dbo].[user] ORDER BY id", MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {0} id,usermail,username,userpass,regtime,birthday,UserMobile,blogtype,UserInfo,realname,blogshen,ifblog,ifgood,jigoublog,blogname,blogjie,savetime,bloggong FROM [bbsuser].[dbo].[user] WHERE id NOT IN (SELECT TOP {1} id FROM [bbsuser].[dbo].[user] ORDER BY id) ORDER BY id", MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion

            DBHelper userlistDBH = MainForm.GetSrcDBH_OldVer();
            DBHelper dbhUserTemp = MainForm.GetSrcDBH_OldVer();
            DBHelper dbhBlogLinkTemp = MainForm.GetSrcDBH_OldVer();
            DBHelper dbhBlogMusicTemp = MainForm.GetSrcDBH_OldVer();

            System.Data.Common.DbDataReader dr = userlistDBH.ExecuteReader(sql);
            List<Users> userlist = new List<Users>();
            while (dr.Read())
            {
                Users objUser = new Users();

                objUser.uid = Convert.ToInt32(dr["id"]);
                objUser.email = dr["usermail"] != DBNull.Value ? dr["usermail"].ToString() : "";
                objUser.username = dr["username"] != DBNull.Value ? dr["username"].ToString() : "";
                objUser.password = dr["userpass"] != DBNull.Value ? dr["userpass"].ToString().ToLower() : "";
                objUser.salt = Utils.Text.GenerateRandom(6, NConvert.Utils.Text.RandomType.NumberAndLowercased);
                objUser.ucpassword = Utils.Text.MD5(objUser.password + objUser.salt).ToLower();
                objUser.status = 0;
                objUser.emailstatus = 0;
                objUser.avatarstatus = 0;
                objUser.videophotostatus = 0;
                objUser.adminid = 0;
                objUser.groupid = 10;
                objUser.groupexpiry = 0;
                objUser.extgroupids = "";
                objUser.regdate = dr["regtime"] != DBNull.Value ? Utils.TypeParse.DateTime2TimeStamp(Convert.ToDateTime(dr["regtime"])) : Utils.TypeParse.DateTime2TimeStamp(DateTime.Now);
                objUser.credits = 0;
                objUser.notifysound = 0;
                objUser.timeoffset = "";
                objUser.newpm = 0;
                objUser.newprompt = 0;
                objUser.accessmasks = 0;
                objUser.allowadmincp = 0;

                objUser.extcredits1 = 0;
                objUser.extcredits2 = 0;
                objUser.extcredits3 = 0;
                objUser.extcredits4 = 0;
                objUser.extcredits5 = 0;
                objUser.extcredits6 = 0;
                objUser.extcredits7 = 0;
                objUser.extcredits8 = 0;
                objUser.friends = 0;
                objUser.posts = 0;
                objUser.threads = 0;
                objUser.digestposts = 0;
                objUser.doings = 0;
                objUser.blogs = 0;
                objUser.albums = 0;
                objUser.sharings = 0;
                objUser.attachsize = 0;

                objUser.oltime = 0;

                objUser.publishfeed = 0;
                objUser.customshow = 26;
                objUser.customstatus = "";
                objUser.medals = "";
                objUser.sightml = "";
                objUser.groupterms = "";
                objUser.authstr = "";
                objUser.groups = "";
                objUser.attentiongroup = "";

                objUser.gender = 0;
                if (dr["birthday"] != DBNull.Value)
                {
                    if (dr["birthday"].ToString().Trim() == "female")
                    {
                        objUser.gender = 2;
                    }
                    else if (dr["birthday"].ToString().Trim() == "male")
                    {
                        objUser.gender = 1;
                    }
                }
                int bdayt;
                if (dr["birthday"] != DBNull.Value
                    && dr["birthday"].ToString().Trim() != string.Empty
                    && int.TryParse(dr["birthday"].ToString().Trim(), out bdayt))
                {
                    objUser.birthyear = bdayt;
                    objUser.birthmonth = 1;
                    objUser.birthday = 1;
                }
                else
                {
                    objUser.birthyear = 0;
                    objUser.birthmonth = 0;
                    objUser.birthday = 0;
                }
                objUser.constellation = "";
                objUser.zodiac = "";
                objUser.telephone = "";
                objUser.mobile = dr["UserMobile"] != DBNull.Value ? dr["UserMobile"].ToString().Trim() : "";
                objUser.idcardtype = "身份证";
                objUser.idcard = "";
                objUser.address = "";
                objUser.zipcode = "";
                objUser.nationality = "";
                objUser.birthprovince = "";
                objUser.birthcity = "";
                objUser.resideprovince = "";
                objUser.residecity = "";
                objUser.residedist = "";
                objUser.residecommunity = "";
                objUser.residesuite = "";
                objUser.graduateschool = "";
                objUser.company = "";
                objUser.education = "";
                objUser.occupation = "";
                objUser.position = "";
                objUser.revenue = "";
                objUser.affectivestatus = "";
                objUser.lookingfor = "";
                objUser.bloodtype = "";
                objUser.height = "";
                objUser.weight = "";
                objUser.alipay = "";
                objUser.icq = "";
                objUser.qq = "";
                objUser.yahoo = "";
                objUser.msn = "";
                objUser.taobao = "";
                objUser.site = "";
                objUser.bio = "";
                objUser.interest = "";

                objUser.field2 = "";
                objUser.field3 = "";
                objUser.field4 = "";
                objUser.field5 = "";
                objUser.field6 = "";
                objUser.field7 = "";
                objUser.field8 = "";

                objUser.regip = "";
                objUser.lastip = "";
                objUser.lastvisit = Utils.TypeParse.DateTime2TimeStamp(DateTime.Now);
                objUser.lastactivity = Utils.TypeParse.DateTime2TimeStamp(DateTime.Now);
                objUser.lastpost = Utils.TypeParse.DateTime2TimeStamp(DateTime.Now);
                objUser.lastsendmail = 0;
                objUser.notifications = 0;
                objUser.myinvitations = 0;
                objUser.pokes = 0;
                objUser.pendingfriends = 0;
                objUser.invisible = 0;
                objUser.buyercredit = 0;
                objUser.sellercredit = 0;
                objUser.favtimes = 0;
                objUser.sharetimes = 0;


                objUser.videophoto = "";
                objUser.domain = "";
                objUser.addsize = 0;
                objUser.addfriend = 0;
                objUser.menunum = 0;
                objUser.theme = "";
                objUser.spacecss = "";
                objUser.recentnote = "";
                objUser.spacenote = "";
                objUser.privacy = "";
                objUser.feedfriend = "";
                objUser.acceptemail = "";
                objUser.magicgift = "";



                //objUser.views = Convert.ToInt32(dbhUserTemp.ExecuteScalar(
                //    string.Format(
                //        "SELECT count1 FROM [science].[dbo].[kexue_blogcount] WHERE userid={0}",
                //        objUser.uid
                //        )
                //    )
                //);
                objUser.views = 0;



                //field1被用作八大研究领域了，那边的参数写作“realm”对应[user]-blogtype
                objUser.field1 = dr["blogtype"] != DBNull.Value ? dr["blogtype"].ToString() : "";

                string userInfo = dr["UserInfo"] != DBNull.Value ? dr["UserInfo"].ToString() : "";
                string[] arrayUserInfo = userInfo.Split('\\');//一共有15个
                objUser.university = arrayUserInfo.Length == 15 ? arrayUserInfo[10] : "";
                objUser.universityid = 0;
                objUser.laboratory = "";
                objUser.initialstudyear = 0;
                objUser.educational = arrayUserInfo.Length == 15 ? arrayUserInfo[9] : "";
                objUser.grade = 1;

                objUser.telephone = arrayUserInfo.Length == 15 ? arrayUserInfo[12] : "";

                //objUser.realname = dr["realname"].ToString(); 
                if (arrayUserInfo.Length == 15 && arrayUserInfo[0].Trim() != string.Empty)
                {
                    objUser.realname = arrayUserInfo[0].Trim();
                }
                else
                {
                    objUser.realname = dr["realname"] != DBNull.Value ? dr["realname"].ToString() : "";
                }

                //如果blogshen=1, 那么usertype=1 groupid=原来的值 extgroupids='20';
                //如果blogshen!=1， 那么usertype=0 groupid=原来的值 extgroupids=''

                int blogshen = dr["blogshen"] != DBNull.Value ? Convert.ToInt32(dr["blogshen"]) : 0;
                int ifblog = dr["ifblog"] != DBNull.Value ? Convert.ToInt32(dr["ifblog"]) : 0;

                if (blogshen == 1)
                {
                    objUser.usertype = 1;
                    objUser.extgroupids = "20";
                }
                else
                {
                    objUser.usertype = 0;
                    objUser.groupid = 8;
                }

                objUser.blogShowStatus = Convert.ToInt32(dr["ifgood"]) == -1 ? 0 : 1;
                objUser.organblog = Convert.ToInt32(dr["jigoublog"]);
                objUser.userlevel = Convert.ToInt32(dr["ifgood"]);


                objUser.spacename = dr["blogname"] != DBNull.Value ? Utils.Text.RemoveHtml(dr["blogname"].ToString()) : "";
                objUser.spacedescription = dr["blogjie"] != DBNull.Value ? dr["blogjie"].ToString() : "";
                //if (dr["bloggong"] != DBNull.Value && dr["bloggong"].ToString().Trim() != string.Empty)
                //{
                //    objUser.spacedescription += dr["bloggong"].ToString().Trim();
                //}
                objUser.blogstartime = dr["savetime"] != DBNull.Value ? Utils.TypeParse.DateTime2TimeStamp(Convert.ToDateTime(dr["savetime"])) : 0;

                #region bloglinks
                //StringBuilder blogLinks = new StringBuilder();
                //string sqlBlogLinkTemp = string.Format(
                //"SELECT linkname,linkaddress FROM [science].[dbo].[kexue_bloglink] WHERE linkname>'' AND linkaddress>'' AND userid={0}",
                //objUser.uid
                //);
                //System.Data.Common.DbDataReader drBlogLinkTemp = dbhBlogLinkTemp.ExecuteReader(sqlBlogLinkTemp);
                //while (drBlogLinkTemp.Read())
                //{
                //    blogLinks.Append(
                //        string.Format("[url={1}]{0}[/url]\r\n",
                //            drBlogLinkTemp["linkname"].ToString(),
                //            drBlogLinkTemp["linkaddress"].ToString()
                //            )
                //        );
                //}
                //drBlogLinkTemp.Close();
                //drBlogLinkTemp.Dispose();
                #endregion

                #region blogmusic
                ArrayList arrayBlogMusiclist = new ArrayList();                
                #endregion

                string bloglink = "";//blogLinks.ToString();不导入了
                string description = dr["blogjie"] != DBNull.Value ? dr["blogjie"].ToString().Trim() : "";
                string annc = dr["bloggong"] != DBNull.Value ? dr["bloggong"].ToString().Trim() : "";

                if (bloglink != string.Empty || description != string.Empty || annc != string.Empty || arrayBlogMusiclist.Count > 0)
                {
                    #region 序列化
                    string blockposition = "a:3:{s:10:\"parameters\";a:3:{s:6:\"block1\";a:2:{s:5:\"title\";s:6:\"公告栏\";s:7:\"content\";s:10:\"{1}\\r\\n{2}\";}s:5:\"music\";a:2:{s:7:\"mp3list\";a:1:{i:0;a:3:{s:6:\"mp3url\";s:20:\"http://abc.com/1.mp3\";s:7:\"mp3name\";s:6:\"曲目名\";s:4:\"cdbj\";s:23:\"http://abc.com/封面.mp3\";}}s:6:\"config\";a:6:{s:7:\"showmod\";s:7:\"default\";s:7:\"autorun\";s:4:\"true\";s:7:\"shuffle\";s:4:\"true\";s:12:\"crontabcolor\";s:7:\"#D2FF8C\";s:11:\"buttoncolor\";s:7:\"#1F43FF\";s:9:\"fontcolor\";s:7:\"#1F43FF\";}}s:6:\"block2\";a:2:{s:5:\"title\";s:8:\"友情链接\";s:7:\"content\";s:10:\"=友情链接=\";}}s:5:\"block\";a:1:{s:12:\"frame`frame1\";a:4:{s:4:\"attr\";a:4:{s:4:\"name\";s:6:\"frame1\";s:8:\"moveable\";s:5:\"false\";s:9:\"className\";s:8:\"frame cl\";s:6:\"titles\";s:0:\"\";}s:18:\"column`frame1_left\";a:6:{s:4:\"attr\";a:2:{s:4:\"name\";s:11:\"frame1_left\";s:9:\"className\";s:8:\"z column\";}s:13:\"block`profile\";a:1:{s:4:\"attr\";a:3:{s:4:\"name\";s:7:\"profile\";s:9:\"className\";s:15:\"block move-span\";s:6:\"titles\";a:3:{s:9:\"className\";a:1:{i:0;s:16:\"blocktitle title\";}s:5:\"style\";s:0:\"\";i:0;a:8:{s:4:\"text\";s:8:\"个人资料\";s:4:\"href\";s:62:\"http://dzx15.s.com/home.php?mod=space&uid=1&do=profile&view=me\";s:5:\"color\";s:0:\"\";s:5:\"float\";s:0:\"\";s:6:\"margin\";s:0:\"\";s:9:\"font-size\";s:0:\"\";s:9:\"className\";s:0:\"\";s:3:\"src\";s:0:\"\";}}}}s:12:\"block`block1\";a:1:{s:4:\"attr\";a:3:{s:4:\"name\";s:6:\"block1\";s:9:\"className\";s:15:\"block move-span\";s:6:\"titles\";a:3:{s:9:\"className\";a:1:{i:0;s:16:\"blocktitle title\";}s:5:\"style\";s:0:\"\";i:0;a:8:{s:4:\"text\";s:6:\"公告栏\";s:4:\"href\";s:0:\"\";s:5:\"color\";s:0:\"\";s:5:\"float\";s:0:\"\";s:6:\"margin\";s:0:\"\";s:9:\"font-size\";s:0:\"\";s:9:\"className\";s:0:\"\";s:3:\"src\";s:0:\"\";}}}}s:11:\"block`doing\";a:1:{s:4:\"attr\";a:3:{s:4:\"name\";s:5:\"doing\";s:9:\"className\";s:15:\"block move-span\";s:6:\"titles\";a:3:{s:9:\"className\";a:1:{i:0;s:16:\"blocktitle title\";}s:5:\"style\";s:0:\"\";i:0;a:8:{s:4:\"text\";s:4:\"记录\";s:4:\"href\";s:60:\"http://dzx15.s.com/home.php?mod=space&uid=1&do=doing&view=me\";s:5:\"color\";s:0:\"\";s:5:\"float\";s:0:\"\";s:6:\"margin\";s:0:\"\";s:9:\"font-size\";s:0:\"\";s:9:\"className\";s:0:\"\";s:3:\"src\";s:0:\"\";}}}}s:11:\"block`music\";a:1:{s:4:\"attr\";a:3:{s:4:\"name\";s:5:\"music\";s:9:\"className\";s:15:\"block move-span\";s:6:\"titles\";a:3:{s:9:\"className\";a:1:{i:0;s:16:\"blocktitle title\";}s:5:\"style\";s:0:\"\";i:0;a:8:{s:4:\"text\";s:6:\"音乐盒\";s:4:\"href\";s:0:\"\";s:5:\"color\";s:0:\"\";s:5:\"float\";s:0:\"\";s:6:\"margin\";s:0:\"\";s:9:\"font-size\";s:0:\"\";s:9:\"className\";s:0:\"\";s:3:\"src\";s:0:\"\";}}}}s:12:\"block`block2\";a:1:{s:4:\"attr\";a:3:{s:4:\"name\";s:6:\"block2\";s:9:\"className\";s:15:\"block move-span\";s:6:\"titles\";a:3:{s:9:\"className\";a:1:{i:0;s:16:\"blocktitle title\";}s:5:\"style\";s:0:\"\";i:0;a:8:{s:4:\"text\";s:8:\"友情链接\";s:4:\"href\";s:0:\"\";s:5:\"color\";s:0:\"\";s:5:\"float\";s:0:\"\";s:6:\"margin\";s:0:\"\";s:9:\"font-size\";s:0:\"\";s:9:\"className\";s:0:\"\";s:3:\"src\";s:0:\"\";}}}}}s:20:\"column`frame1_center\";a:6:{s:4:\"attr\";a:2:{s:4:\"name\";s:13:\"frame1_center\";s:9:\"className\";s:8:\"z column\";}s:10:\"block`feed\";a:1:{s:4:\"attr\";a:3:{s:4:\"name\";s:4:\"feed\";s:9:\"className\";s:15:\"block move-span\";s:6:\"titles\";a:3:{s:9:\"className\";a:1:{i:0;s:16:\"blocktitle title\";}s:5:\"style\";s:0:\"\";i:0;a:8:{s:4:\"text\";s:4:\"动态\";s:4:\"href\";s:59:\"http://dzx15.s.com/home.php?mod=space&uid=1&do=feed&view=me\";s:5:\"color\";s:0:\"\";s:5:\"float\";s:0:\"\";s:6:\"margin\";s:0:\"\";s:9:\"font-size\";s:0:\"\";s:9:\"className\";s:0:\"\";s:3:\"src\";s:0:\"\";}}}}s:11:\"block`share\";a:1:{s:4:\"attr\";a:3:{s:4:\"name\";s:5:\"share\";s:9:\"className\";s:15:\"block move-span\";s:6:\"titles\";a:3:{s:9:\"className\";a:1:{i:0;s:16:\"blocktitle title\";}s:5:\"style\";s:0:\"\";i:0;a:8:{s:4:\"text\";s:4:\"分享\";s:4:\"href\";s:60:\"http://dzx15.s.com/home.php?mod=space&uid=1&do=share&view=me\";s:5:\"color\";s:0:\"\";s:5:\"float\";s:0:\"\";s:6:\"margin\";s:0:\"\";s:9:\"font-size\";s:0:\"\";s:9:\"className\";s:0:\"\";s:3:\"src\";s:0:\"\";}}}}s:10:\"block`blog\";a:1:{s:4:\"attr\";a:3:{s:4:\"name\";s:4:\"blog\";s:9:\"className\";s:15:\"block move-span\";s:6:\"titles\";a:3:{s:9:\"className\";a:1:{i:0;s:16:\"blocktitle title\";}s:5:\"style\";s:0:\"\";i:0;a:8:{s:4:\"text\";s:4:\"日志\";s:4:\"href\";s:59:\"http://dzx15.s.com/home.php?mod=space&uid=1&do=blog&view=me\";s:5:\"color\";s:0:\"\";s:5:\"float\";s:0:\"\";s:6:\"margin\";s:0:\"\";s:9:\"font-size\";s:0:\"\";s:9:\"className\";s:0:\"\";s:3:\"src\";s:0:\"\";}}}}s:12:\"block`thread\";a:1:{s:4:\"attr\";a:3:{s:4:\"name\";s:6:\"thread\";s:9:\"className\";s:15:\"block move-span\";s:6:\"titles\";s:0:\"\";}}s:10:\"block`wall\";a:1:{s:4:\"attr\";a:3:{s:4:\"name\";s:4:\"wall\";s:9:\"className\";s:15:\"block move-span\";s:6:\"titles\";a:4:{s:9:\"className\";a:1:{i:0;s:16:\"blocktitle title\";}s:5:\"style\";s:0:\"\";i:0;a:8:{s:4:\"text\";s:6:\"留言板\";s:4:\"href\";s:59:\"http://dzx15.s.com/home.php?mod=space&uid=1&do=wall&view=me\";s:5:\"color\";s:0:\"\";s:5:\"float\";s:0:\"\";s:6:\"margin\";s:0:\"\";s:9:\"font-size\";s:0:\"\";s:9:\"className\";s:0:\"\";s:3:\"src\";s:0:\"\";}i:1;a:8:{s:4:\"text\";s:4:\"全部\";s:4:\"href\";s:51:\"http://dzx15.s.com/home.php?mod=space&uid=1&do=wall\";s:5:\"color\";s:0:\"\";s:5:\"float\";s:0:\"\";s:6:\"margin\";s:0:\"\";s:9:\"font-size\";s:0:\"\";s:9:\"className\";s:5:\"y xw0\";s:3:\"src\";s:0:\"\";}}}}}s:19:\"column`frame1_right\";a:6:{s:4:\"attr\";a:2:{s:4:\"name\";s:12:\"frame1_right\";s:9:\"className\";s:8:\"z column\";}s:12:\"block`friend\";a:1:{s:4:\"attr\";a:3:{s:4:\"name\";s:6:\"friend\";s:9:\"className\";s:15:\"block move-span\";s:6:\"titles\";a:3:{s:9:\"className\";a:1:{i:0;s:16:\"blocktitle title\";}s:5:\"style\";s:0:\"\";i:0;a:8:{s:4:\"text\";s:4:\"好友\";s:4:\"href\";s:61:\"http://dzx15.s.com/home.php?mod=space&uid=1&do=friend&view=me\";s:5:\"color\";s:0:\"\";s:5:\"float\";s:0:\"\";s:6:\"margin\";s:0:\"\";s:9:\"font-size\";s:0:\"\";s:9:\"className\";s:0:\"\";s:3:\"src\";s:0:\"\";}}}}s:13:\"block`visitor\";a:1:{s:4:\"attr\";a:3:{s:4:\"name\";s:7:\"visitor\";s:9:\"className\";s:15:\"block move-span\";s:6:\"titles\";a:3:{s:9:\"className\";a:1:{i:0;s:16:\"blocktitle title\";}s:5:\"style\";s:0:\"\";i:0;a:8:{s:4:\"text\";s:8:\"最近访客\";s:4:\"href\";s:66:\"http://dzx15.s.com/home.php?mod=space&uid=1&do=friend&view=visitor\";s:5:\"color\";s:0:\"\";s:5:\"float\";s:0:\"\";s:6:\"margin\";s:0:\"\";s:9:\"font-size\";s:0:\"\";s:9:\"className\";s:0:\"\";s:3:\"src\";s:0:\"\";}}}}s:11:\"block`group\";a:1:{s:4:\"attr\";a:3:{s:4:\"name\";s:5:\"group\";s:9:\"className\";s:15:\"block move-span\";s:6:\"titles\";a:3:{s:9:\"className\";a:1:{i:0;s:16:\"blocktitle title\";}s:5:\"style\";s:0:\"\";i:0;a:8:{s:4:\"text\";s:4:\"群组\";s:4:\"href\";s:69:\"http://dzx15.s.com/home.php?mod=space&uid=1&do=group&view=groupthread\";s:5:\"color\";s:0:\"\";s:5:\"float\";s:0:\"\";s:6:\"margin\";s:0:\"\";s:9:\"font-size\";s:0:\"\";s:9:\"className\";s:0:\"\";s:3:\"src\";s:0:\"\";}}}}s:11:\"block`album\";a:1:{s:4:\"attr\";a:3:{s:4:\"name\";s:5:\"album\";s:9:\"className\";s:15:\"block move-span\";s:6:\"titles\";a:3:{s:9:\"className\";a:1:{i:0;s:16:\"blocktitle title\";}s:5:\"style\";s:0:\"\";i:0;a:8:{s:4:\"text\";s:4:\"相册\";s:4:\"href\";s:60:\"http://dzx15.s.com/home.php?mod=space&uid=1&do=album&view=me\";s:5:\"color\";s:0:\"\";s:5:\"float\";s:0:\"\";s:6:\"margin\";s:0:\"\";s:9:\"font-size\";s:0:\"\";s:9:\"className\";s:0:\"\";s:3:\"src\";s:0:\"\";}}}}s:15:\"block`statistic\";a:1:{s:4:\"attr\";a:3:{s:4:\"name\";s:9:\"statistic\";s:9:\"className\";s:15:\"block move-span\";s:6:\"titles\";a:3:{s:9:\"className\";a:1:{i:0;s:16:\"blocktitle title\";}s:5:\"style\";s:0:\"\";i:0;a:8:{s:4:\"text\";s:8:\"统计信息\";s:4:\"href\";s:0:\"\";s:5:\"color\";s:0:\"\";s:5:\"float\";s:0:\"\";s:6:\"margin\";s:0:\"\";s:9:\"font-size\";s:0:\"\";s:9:\"className\";s:0:\"\";s:3:\"src\";s:0:\"\";}}}}}}}s:13:\"currentlayout\";s:5:\"1:2:1\";}";//.Replace("{0}", "1");//.Replace("{1}", description).Replace("{2}", annc).Replace("{3}", bloglink);
                    //,
                    //objUser.uid,
                    //description,
                    //annc,
                    //bloglink
                    //);
                    #endregion

                    byte[] blocks = System.Text.Encoding.GetEncoding("gb2312").GetBytes(blockposition);
                    object oBlock = PHPSerializer.UnSerialize(blocks, System.Text.Encoding.GetEncoding("gb2312"));
                    Hashtable hBlock = (Hashtable)oBlock;
                    if (bloglink != string.Empty)
                    {
                        ((Hashtable)((Hashtable)hBlock["parameters"])["block2"])["content"] = bloglink;
                    }
                    else
                    {
                        ((Hashtable)(hBlock["parameters"])).Remove("block2");
                        ((Hashtable)((Hashtable)((Hashtable)hBlock["block"])["frame`frame1"])["column`frame1_left"]).Remove("block`block2");
                    }

                    if (description == string.Empty && annc == string.Empty)
                    {
                        ((Hashtable)(hBlock["parameters"])).Remove("block1");
                        ((Hashtable)((Hashtable)((Hashtable)hBlock["block"])["frame`frame1"])["column`frame1_left"]).Remove("block`block1");
                    }
                    else
                    {
                        if (description != string.Empty)
                        {
                            ((Hashtable)((Hashtable)hBlock["parameters"])["block1"])["content"] = description;
                        }

                        if (annc != string.Empty)
                        {
                            if (((Hashtable)((Hashtable)hBlock["parameters"])["block1"])["content"].ToString() != string.Empty)
                            {
                                ((Hashtable)((Hashtable)hBlock["parameters"])["block1"])["content"] += "\r\n";
                            }
                            ((Hashtable)((Hashtable)hBlock["parameters"])["block1"])["content"] += annc;
                        }
                    }

                    if (arrayBlogMusiclist.Count > 0)
                    {
                        ((Hashtable)((Hashtable)hBlock["parameters"])["music"])["mp3list"] = arrayBlogMusiclist;
                    }
                    else
                    {
                        ((Hashtable)(hBlock["parameters"])).Remove("music");
                        ((Hashtable)((Hashtable)((Hashtable)hBlock["block"])["frame`frame1"])["column`frame1_left"]).Remove("block`music");
                    }
                    objUser.blockposition = System.Text.Encoding.GetEncoding("gb2312").GetString(PHPSerializer.Serialize(hBlock, System.Text.Encoding.GetEncoding("gb2312")));
                }
                else
                {
                    objUser.blockposition = "";
                }
                userlist.Add(objUser);
            }
            dr.Close();
            dr.Dispose();
            return userlist;
        }
    }
}
