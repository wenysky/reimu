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
        string tablename = "dnt_users";
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
                       ("SELECT TOP {1} * FROM {0} ORDER BY {2}", MainForm.cic.SrcDbTablePrefix + tablename, MainForm.PageSize, pkidname);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM {0} WHERE {3} NOT IN (SELECT TOP {2} {3} FROM {0} ORDER BY {3}) ORDER BY {3}", MainForm.cic.SrcDbTablePrefix + tablename, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1), pkidname);
            }
            #endregion

            DBHelper userlistDBH = MainForm.GetSrcDBH_OldVer();
            System.Data.Common.DbDataReader dr = userlistDBH.ExecuteReader(sql);
            List<Users> userlist = new List<Users>();
            while (dr.Read())
            {
                Users objUser = new Users();
                objUser.uid = Convert.ToInt32(dr["id"]);
                objUser.username = dr["username"].ToString();
                objUser.password = dr["password"] == DBNull.Value ? "" : dr["password"].ToString();
                if (dr["sex"] == DBNull.Value || dr["sex"].ToString() == "sect" || dr["sex"].ToString() == "")
                {
                    objUser.gender = 0;
                }
                else
                {
                    objUser.gender = dr["sex"].ToString() == "male" ? 1 : 2;
                }
                objUser.email = dr["email"] == DBNull.Value ? "" : dr["email"].ToString();
                objUser.groupid = GetNewUserGroupID(Convert.ToInt16(dr["usermode"]));
                //if (Convert.ToInt32(dr["LockUser"]) == 1 || Convert.ToInt32(dr["UserDel"]) == 1)
                //{
                //    objUser.groupid = 8;//未审核,未被激活,锁定,删除=>等待验证会员
                //}
                objUser.adminid = objUser.groupid < 4 ? objUser.groupid : 0;


                //objUser = dr["GroupExpirty"].ToString();
                objUser.uid = Convert.ToInt32(dr["uid"]);
                objUser.email = dr["email"].ToString();
                objUser.username = dr["username"].ToString();
                objUser.password = dr["password"].ToString();
                objUser.status = Convert.ToInt32(dr["status"]);
                objUser.emailstatus = Convert.ToInt32(dr["emailstatus"]);
                objUser.avatarstatus = Convert.ToInt32(dr["avatarstatus"]);
                objUser.videophotostatus = Convert.ToInt32(dr["videophotostatus"]);
                objUser.adminid = Convert.ToInt32(dr["adminid"]);
                objUser.groupid = Convert.ToInt32(dr["groupid"]);
                objUser.groupexpiry = Convert.ToInt32(dr["groupexpiry"]);
                objUser.extgroupids = Convert.ToInt32(dr["extgroupids"]);
                objUser.regdate = Convert.ToInt32(dr["regdate"]);
                objUser.credits = Convert.ToInt32(dr["credits"]);
                objUser.notifysound = Convert.ToInt32(dr["notifysound"]);
                objUser.timeoffset = Convert.ToInt32(dr["timeoffset"]);
                objUser.newpm = Convert.ToInt32(dr["newpm"]);
                objUser.newprompt = Convert.ToInt32(dr["newprompt"]);
                objUser.accessmasks = Convert.ToInt32(dr["accessmasks"]);
                objUser.allowadmincp = Convert.ToInt32(dr["allowadmincp"]);

                objUser.extcredits1 = Convert.ToInt32(dr["extcredits1"]);
                objUser.extcredits2 = Convert.ToInt32(dr["extcredits2"]);
                objUser.extcredits3 = Convert.ToInt32(dr["extcredits3"]);
                objUser.extcredits4 = Convert.ToInt32(dr["extcredits4"]);
                objUser.extcredits5 = Convert.ToInt32(dr["extcredits5"]);
                objUser.extcredits6 = Convert.ToInt32(dr["extcredits6"]);
                objUser.extcredits7 = Convert.ToInt32(dr["extcredits7"]);
                objUser.extcredits8 = Convert.ToInt32(dr["extcredits8"]);
                objUser.friends = Convert.ToInt32(dr["friends"]);
                objUser.posts = Convert.ToInt32(dr["posts"]);
                objUser.threads = Convert.ToInt32(dr["threads"]);
                objUser.digestposts = Convert.ToInt32(dr["digestposts"]);
                objUser.doings = Convert.ToInt32(dr["doings"]);
                objUser.blogs = Convert.ToInt32(dr["blogs"]);
                objUser.albums = Convert.ToInt32(dr["albums"]);
                objUser.sharings = Convert.ToInt32(dr["sharings"]);
                objUser.attachsize = Convert.ToInt32(dr["attachsize"]);
                objUser.views = Convert.ToInt32(dr["views"]);
                objUser.oltime = Convert.ToInt32(dr["oltime"]);

                objUser.publishfeed = Convert.ToInt32(dr["publishfeed"]);
                objUser.customshow = Convert.ToInt32(dr["customshow"]);
                objUser.customstatus = dr["customstatus"].ToString();
                objUser.medals = dr["medals"].ToString();
                objUser.sightml = dr["sightml"].ToString();
                objUser.groupterms = dr["groupterms"].ToString();
                objUser.authstr = dr["authstr"].ToString();
                objUser.groups = dr["groups"].ToString();
                objUser.attentiongroup = dr["attentiongroup"].ToString();

                objUser.realname = dr["realname"].ToString();
                objUser.gender = Convert.ToInt32(dr["gender"]);
                objUser.birthyear = Convert.ToInt32(dr["birthyear"]);
                objUser.birthmonth = Convert.ToInt32(dr["birthmonth"]);
                objUser.birthday = Convert.ToInt32(dr["birthday"]);
                objUser.constellation = dr["constellation"].ToString();
                objUser.zodiac = dr["zodiac"].ToString();
                objUser.telephone = dr["telephone"].ToString();
                objUser.mobile = dr["mobile"].ToString();
                objUser.idcardtype = dr["idcardtype"].ToString();
                objUser.idcard = dr["idcard"].ToString();
                objUser.address = dr["address"].ToString();
                objUser.zipcode = dr["zipcode"].ToString();
                objUser.nationality = dr["nationality"].ToString();
                objUser.birthprovince = dr["birthprovince"].ToString();
                objUser.birthcity = dr["birthcity"].ToString();
                objUser.resideprovince = dr["resideprovince"].ToString();
                objUser.residecity = dr["residecity"].ToString();
                objUser.residedist = dr["residedist"].ToString();
                objUser.residecommunity = dr["residecommunity"].ToString();
                objUser.residesuite = dr["residesuite"].ToString();
                objUser.graduateschool = dr["graduateschool"].ToString();
                objUser.company = dr["company"].ToString();
                objUser.education = dr["education"].ToString();
                objUser.occupation = dr["occupation"].ToString();
                objUser.position = dr["position"].ToString();
                objUser.revenue = dr["revenue"].ToString();
                objUser.affectivestatus = dr["affectivestatus"].ToString();
                objUser.lookingfor = dr["lookingfor"].ToString();
                objUser.bloodtype = dr["bloodtype"].ToString();
                objUser.height = dr["height"].ToString();
                objUser.weight = dr["weight"].ToString();
                objUser.alipay = dr["alipay"].ToString();
                objUser.icq = dr["icq"].ToString();
                objUser.qq = dr["qq"].ToString();
                objUser.yahoo = dr["yahoo"].ToString();
                objUser.msn = dr["msn"].ToString();
                objUser.taobao = dr["taobao"].ToString();
                objUser.site = dr["site"].ToString();
                objUser.bio = dr["bio"].ToString();
                objUser.interest = dr["interest"].ToString();
                objUser.field1 = dr["field1"].ToString();
                objUser.field2 = dr["field2"].ToString();
                objUser.field3 = dr["field3"].ToString();
                objUser.field4 = dr["field4"].ToString();
                objUser.field5 = dr["field5"].ToString();
                objUser.field6 = dr["field6"].ToString();
                objUser.field7 = dr["field7"].ToString();
                objUser.field8 = dr["field8"].ToString();

                objUser.regip = dr["regip"].ToString();
                objUser.lastip = dr["lastip"].ToString();
                objUser.lastvisit = Convert.ToInt32(dr["lastvisit"]);
                objUser.lastactivity = Convert.ToInt32(dr["lastactivity"]);
                objUser.lastpost = Convert.ToInt32(dr["lastpost"]);
                objUser.lastsendmail = Convert.ToInt32(dr["lastsendmail"]);
                objUser.notifications = Convert.ToInt32(dr["notifications"]);
                objUser.myinvitations = Convert.ToInt32(dr["myinvitations"]);
                objUser.pokes = Convert.ToInt32(dr["pokes"]);
                objUser.pendingfriends = Convert.ToInt32(dr["pendingfriends"]);
                objUser.invisible = Convert.ToInt32(dr["invisible"]);
                objUser.buyercredit = Convert.ToInt32(dr["buyercredit"]);
                objUser.sellercredit = Convert.ToInt32(dr["sellercredit"]);
                objUser.favtimes = Convert.ToInt32(dr["favtimes"]);
                objUser.sharetimes = Convert.ToInt32(dr["sharetimes"]);

                
                userlist.Add(objUser);
            }
            return userlist;
        }

        private static short GetNewUserGroupID(short OldUserGroupID)
        {
            short NewGroupID;
            switch (OldUserGroupID)
            {
                case 7: 
                    NewGroupID = 1;
                    break;
                case 6: 
                    NewGroupID = 2;
                    break;
                case 3: 
                    NewGroupID = 3;
                    break;
                case 2: 
                    NewGroupID = 10;
#warning 需要完善 认证会员组
                    break;
                case 1://普通用户组全部到10积分用户组
                    NewGroupID = 10;
                    break;
                default:
                    throw new Exception("用户组错误");
            }

            if (NewGroupID > 5)//6以上的都转换到普通积分用户组中
            {
                NewGroupID = 10;
            }
            return NewGroupID;
        }
    }
}
