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
        string pkidname = "uid";
        string tablename = "users";
        public int GetUsersRecordCount()
        {
            DBHelper userDBH = MainForm.GetSrcDBH_OldVer();
            return Convert.ToInt32(
                userDBH.ExecuteScalar(
                    string.Format("SELECT COUNT({1}) FROM {0}", MainForm.cic.SrcDbTablePrefix + tablename, pkidname)
                    )
                );
        }




        public List<Users> GetUserList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM {0} LEFT JOIN {3} ON {0}.uid={3}.uid ORDER BY {0}.{2}", MainForm.cic.SrcDbTablePrefix + tablename, MainForm.PageSize, pkidname, MainForm.cic.SrcDbTablePrefix + "userfields");
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM {0} LEFT JOIN {4} ON {0}.uid={4}.uid WHERE {0}.{3} NOT IN (SELECT TOP {2} {3} FROM {0} ORDER BY {0}.{3}) ORDER BY {0}.{3}", MainForm.cic.SrcDbTablePrefix + tablename, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1), pkidname, MainForm.cic.SrcDbTablePrefix + "userfields");
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

                objUser.uid = Convert.ToInt32(dr["uid"]);
                objUser.email = dr["email"].ToString();
                objUser.username = dr["username"].ToString();
                objUser.password = dr["password"].ToString().ToLower();
                objUser.salt = Utils.Text.GenerateRandom(6, NConvert.Utils.Text.RandomType.NumberAndLowercased);
                objUser.ucpassword = Utils.Text.MD5(objUser.password + objUser.salt).ToLower();
                objUser.status = 0;
                objUser.emailstatus = 0;
                objUser.avatarstatus = 0;
                objUser.videophotostatus = 0;
                objUser.adminid = Convert.ToInt32(dr["adminid"]);
                objUser.groupid = Convert.ToInt32(dr["groupid"]);
                objUser.groupexpiry = Convert.ToInt32(dr["groupexpiry"]);
                objUser.extgroupids = "";
                objUser.regdate = Utils.TypeParse.DateTime2TimeStamp(Convert.ToDateTime(dr["joindate"]));
                objUser.credits = Convert.ToInt32(dr["credits"]);
                objUser.notifysound = 0;
                objUser.timeoffset = "";
                objUser.newpm = Convert.ToInt32(dr["newpm"]);
                objUser.newprompt = 0;
                objUser.accessmasks = Convert.ToInt32(dr["accessmasks"]);
                objUser.allowadmincp = 0;

                objUser.extcredits1 = Convert.ToInt32(dr["extcredits1"]);
                objUser.extcredits2 = Convert.ToInt32(dr["extcredits2"]);
                objUser.extcredits3 = Convert.ToInt32(dr["extcredits3"]);
                objUser.extcredits4 = Convert.ToInt32(dr["extcredits4"]);
                objUser.extcredits5 = Convert.ToInt32(dr["extcredits5"]);
                objUser.extcredits6 = Convert.ToInt32(dr["extcredits6"]);
                objUser.extcredits7 = Convert.ToInt32(dr["extcredits7"]);
                objUser.extcredits8 = Convert.ToInt32(dr["extcredits8"]);
                objUser.friends = 0;
                objUser.posts = Convert.ToInt32(dr["posts"]);
                objUser.threads = Convert.ToInt32(dr["posts"]);
                objUser.digestposts = Convert.ToInt32(dr["digestposts"]);
                objUser.doings = 0;
                objUser.blogs = 0;
                objUser.albums = 0;
                objUser.sharings = 0;
                objUser.attachsize = 0;

                objUser.oltime = Convert.ToInt32(dr["oltime"]);

                objUser.publishfeed = 0;
                objUser.customshow = 26;
                objUser.customstatus = dr["customstatus"].ToString();
                objUser.medals = dr["medals"].ToString();
                objUser.sightml = dr["sightml"].ToString();
                objUser.groupterms = "";
                objUser.authstr = dr["authstr"].ToString();
                objUser.groups = "";
                objUser.attentiongroup = "";

                objUser.gender = Convert.ToInt32(dr["gender"]);
                DateTime bdayt;
                if (dr["bday"] != DBNull.Value
                    && dr["bday"].ToString().Trim() != string.Empty
                    && DateTime.TryParse(dr["bday"].ToString().Trim(), out bdayt))
                {
                    objUser.birthyear = bdayt.Year;
                    objUser.birthmonth = bdayt.Month;
                    objUser.birthday = bdayt.Day;
                }
                else
                {
                    objUser.birthyear = 0;
                    objUser.birthmonth = 0;
                    objUser.birthday = 0;
                }
                objUser.constellation = "";
                objUser.zodiac = "";
                objUser.telephone = dr["phone"].ToString();
                objUser.mobile = dr["mobile"].ToString();
                objUser.idcardtype = "身份证";
                objUser.idcard = dr["idcard"].ToString();
                objUser.address = dr["location"].ToString();
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
                objUser.icq = dr["icq"].ToString();
                objUser.qq = dr["qq"].ToString();
                objUser.yahoo = dr["yahoo"].ToString();
                objUser.msn = dr["msn"].ToString();
                objUser.taobao = "";
                objUser.site = dr["website"].ToString();
                objUser.bio = dr["bio"].ToString();
                objUser.interest = "";

                objUser.field2 = "";
                objUser.field3 = "";
                objUser.field4 = "";
                objUser.field5 = "";
                objUser.field6 = "";
                objUser.field7 = "";
                objUser.field8 = "";

                objUser.regip = dr["regip"].ToString();
                objUser.lastip = dr["lastip"].ToString();
                objUser.lastvisit = Utils.TypeParse.DateTime2TimeStamp(Convert.ToDateTime(dr["lastvisit"]));
                objUser.lastactivity = Utils.TypeParse.DateTime2TimeStamp(Convert.ToDateTime(dr["lastactivity"]));
                objUser.lastpost = Utils.TypeParse.DateTime2TimeStamp(Convert.ToDateTime(dr["lastpost"]));
                objUser.lastsendmail = 0;
                objUser.notifications = 0;
                objUser.myinvitations = 0;
                objUser.pokes = 0;
                objUser.pendingfriends = 0;
                objUser.invisible = Convert.ToInt32(dr["invisible"]);
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



                objUser.views = Convert.ToInt32(dbhUserTemp.ExecuteScalar(
                    string.Format(
                        "SELECT count1 FROM [science].[dbo].[kexue_blogcount] WHERE userid={0}",
                        objUser.uid
                        )
                    )
                );

                string sqlKexueUser = string.Format(
                    "SELECT realname,blogtype,UserInfo,blogshen,ifgood,jigoublog,blogname,blogjie,bloggong,savetime FROM [sciencebbs].[dbo].[user] WHERE id={0}",
                    objUser.uid
                    );
                System.Data.Common.DbDataReader drKexueUser = dbhUserTemp.ExecuteReader(sqlKexueUser);

                if (drKexueUser.Read())
                {
                    //field1被用作八大研究领域了，那边的参数写作“realm”对应[user]-blogtype
                    objUser.field1 = drKexueUser["blogtype"] != DBNull.Value ? drKexueUser["blogtype"].ToString() : "";

                    string userInfo = drKexueUser["UserInfo"] != DBNull.Value ? drKexueUser["UserInfo"].ToString() : "";
                    string[] arrayUserInfo = userInfo.Split('\\');//一共有15个
                    objUser.university = arrayUserInfo.Length == 15 ? arrayUserInfo[10] : "";
                    objUser.universityid = 0;
                    objUser.laboratory = "";
                    objUser.initialstudyear = 0;
                    objUser.educational = arrayUserInfo.Length == 15 ? arrayUserInfo[9] : "";
                    objUser.grade = 1;

                    //objUser.realname = dr["realname"].ToString(); 
                    if (arrayUserInfo.Length == 15 && arrayUserInfo[0].Trim() != string.Empty)
                    {
                        objUser.realname = arrayUserInfo[0].Trim();
                    }
                    else
                    {
                        objUser.realname = drKexueUser["realname"] != DBNull.Value ? drKexueUser["realname"].ToString() : "";
                    }

                    int blogshen = Convert.ToInt32(drKexueUser["blogshen"]);
                    if (blogshen == -1)
                    {
                        objUser.usertype = 0;
#warning TODO
                    }
                    else if (blogshen == -2)
                    {
                        objUser.usertype = 0;
                    }
                    else
                    {
                        objUser.usertype = blogshen;
                        objUser.extgroupids = "20";
                    }
                    objUser.blogShowStatus = Convert.ToInt32(drKexueUser["ifgood"]);
                    objUser.organblog = Convert.ToInt32(drKexueUser["jigoublog"]);
                    objUser.userlevel = Convert.ToInt32(drKexueUser["ifgood"]);


                    objUser.spacename = drKexueUser["blogname"] != DBNull.Value ? Utils.Text.RemoveHtml(drKexueUser["blogname"].ToString()) : "";
                    objUser.spacedescription = drKexueUser["blogjie"] != DBNull.Value ? drKexueUser["blogjie"].ToString() : "";
                    //if (dr["bloggong"] != DBNull.Value && dr["bloggong"].ToString().Trim() != string.Empty)
                    //{
                    //    objUser.spacedescription += dr["bloggong"].ToString().Trim();
                    //}
                    objUser.blogstartime = drKexueUser["savetime"] != DBNull.Value ? Utils.TypeParse.DateTime2TimeStamp(Convert.ToDateTime(drKexueUser["savetime"])) : 0;

                    #region bloglinks
                    StringBuilder blogLinks = new StringBuilder();
                    string sqlBlogLinkTemp = string.Format(
                    "SELECT linkname,linkaddress FROM [science].[dbo].[kexue_bloglink] WHERE linkname>'' AND linkaddress>'' AND userid={0}",
                    objUser.uid
                    );
                    System.Data.Common.DbDataReader drBlogLinkTemp = dbhBlogLinkTemp.ExecuteReader(sqlBlogLinkTemp);
                    while (drBlogLinkTemp.Read())
                    {
                        blogLinks.Append(
                            string.Format("[url={1}]{0}[/url]\r\n",
                                drBlogLinkTemp["linkname"].ToString(),
                                drBlogLinkTemp["linkaddress"].ToString()
                                )
                            );
                    }
                    drBlogLinkTemp.Close();
                    drBlogLinkTemp.Dispose();
                    #endregion

                    #region blogmusic
                    ArrayList arrayBlogMusiclist = new ArrayList();
                    string sqlBlogMusicTemp = string.Format(
                    "SELECT title,audiourl FROM [science].[dbo].[kexue_blogaudio] WHERE title>'' AND audiourl>'' AND userid={0}",
                    objUser.uid
                    );
                    System.Data.Common.DbDataReader drBlogMusicTemp = dbhBlogMusicTemp.ExecuteReader(sqlBlogMusicTemp);
                    while (drBlogMusicTemp.Read())
                    {
                        Hashtable ht = new Hashtable();
                        ht.Add("mp3name", drBlogMusicTemp["title"].ToString());
                        ht.Add("cdbj", "");
                        ht.Add("mp3url", drBlogMusicTemp["audiourl"].ToString());
                        arrayBlogMusiclist.Add(ht);
                    }
                    drBlogMusicTemp.Close();
                    drBlogMusicTemp.Dispose();
                    #endregion

                    string bloglink = blogLinks.ToString();
                    string description = drKexueUser["blogjie"] != DBNull.Value ? drKexueUser["blogjie"].ToString().Trim() : "";
                    string annc = drKexueUser["bloggong"] != DBNull.Value ? drKexueUser["bloggong"].ToString().Trim() : "";

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
                }
                else
                {
                    objUser.realname = "";
                    objUser.field1 = "";
                    objUser.university = "";
                    objUser.universityid = 0;
                    objUser.laboratory = "";
                    objUser.initialstudyear = 0;
                    objUser.educational = "";
                    objUser.grade = 1;
                    objUser.usertype = 0;
                    objUser.blogShowStatus = 1;
                    objUser.organblog = 0;
                    objUser.userlevel = 0;

                    objUser.spacename = "";
                    objUser.spacedescription = "";
                    //if (dr["bloggong"] != DBNull.Value && dr["bloggong"].ToString().Trim() != string.Empty)
                    //{
                    //    objUser.spacedescription += dr["bloggong"].ToString().Trim();
                    //}
                    objUser.blogstartime = 0;
                    objUser.blockposition = "";
                }
                drKexueUser.Close();
                drKexueUser.Dispose();
                userlist.Add(objUser);
            }
            dr.Close();
            dr.Dispose();
            return userlist;
        }


        public int GetUIDbyUsername(string username)
        {
            Yuwen.Tools.Data.DBHelper dbh = MainForm.GetTargetDBH_OldVer();
            int uid;
            string sql = string.Format(
                "SELECT uid FROM {0}common_member WHERE username=@username",
                MainForm.cic.TargetDbTablePrefix, username
                );
            dbh.ParameterAdd("@username", username, System.Data.DbType.String, 20);
            System.Data.Common.DbDataReader dr = dbh.ExecuteReader(sql);
            if (dr == null)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("【DEBUG】获取uid失败，username={0}", username));
                return 0;
            }
            if (dr.Read())
            {
                uid = dr["uid"] != DBNull.Value ? Convert.ToInt32(dr["uid"]) : 0;
            }
            else
            {
                uid = 0;
            }
            dr.Close();
            dr.Dispose();
            return uid;
        }

        public int GetUIDbyBlogid(int blogid)
        {
            Yuwen.Tools.Data.DBHelper dbh = MainForm.GetTargetDBH_OldVer();
            int uid;
            string sql = string.Format(
                "SELECT uid FROM {0}home_blog WHERE blogid=@blogid",
                MainForm.cic.TargetDbTablePrefix, blogid
                );
            dbh.ParameterAdd("@blogid", blogid, System.Data.DbType.Int32, 4);
            System.Data.Common.DbDataReader dr = dbh.ExecuteReader(sql);
            if (dr == null)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("【DEBUG】获取uid失败，blogid={0}", blogid));
                return 0;
            }
            if (dr.Read())
            {
                uid = dr["uid"] != DBNull.Value ? Convert.ToInt32(dr["uid"]) : 0;
            }
            else
            {
                uid = 0;
            }
            dr.Close();
            dr.Dispose();
            return uid;
        }
    }
}
