using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;
using NConvert.Entity;
using Yuwen.Tools.Data;

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


                objUser.views = Convert.ToInt32(dbhUserTemp.ExecuteScalar(
                    string.Format(
                        "SELECT count1 FROM [science].[dbo].[kexue_blogcount] WHERE userid={0}",
                        objUser.uid
                        )
                    )
                );

                string sqlKexueUser = string.Format(
                    "SELECT realname,blogtype,UserInfo,blogshen,ifgood,jigoublog FROM [sciencebbs].[dbo].[user] WHERE id={0}",
                    objUser.uid
                    );
                System.Data.Common.DbDataReader drKexueUser = dbhUserTemp.ExecuteReader(sqlKexueUser);

                if (drKexueUser.Read())
                {
                    //objUser.realname = dr["realname"].ToString();  
                    objUser.realname = drKexueUser["realname"] != DBNull.Value ? drKexueUser["realname"].ToString() : "";
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
                    }
                    objUser.blogShowStatus = Convert.ToInt32(drKexueUser["ifgood"]);
                    objUser.organblog = Convert.ToInt32(drKexueUser["jigoublog"]);
                    objUser.userlevel = 0;
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
            try
            {
                uid = Convert.ToInt32(dbh.ExecuteScalar(string.Format("SELECT uid FROM {0}common_member WHERE username='{1}'", MainForm.cic.TargetDbTablePrefix, username)));
            }
            catch
            {
                uid = 0;
            }
            return uid;
        }
    }
}
