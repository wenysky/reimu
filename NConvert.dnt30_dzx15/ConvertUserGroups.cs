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
        public int GetUserGroupsRecordCount()
        {
            DBHelper userDBH = MainForm.GetSrcDBH_OldVer();
            return Convert.ToInt32(
                userDBH.ExecuteScalar(
                    string.Format("SELECT COUNT(groupid) FROM {0}usergroups", MainForm.cic.SrcDbTablePrefix)
                    )
                );
        }




        public List<UserGroupInfo> GetUserGroupList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM {0}usergroups ORDER BY groupid", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM {0}usergroups WHERE groupid NOT IN (SELECT TOP {2} groupid FROM {0}usergroups ORDER BY groupid) ORDER BY groupid", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion

            DBHelper userlistDBH = MainForm.GetSrcDBH_OldVer();
            System.Data.Common.DbDataReader dr = userlistDBH.ExecuteReader(sql);
            List<UserGroupInfo> userlist = new List<UserGroupInfo>();


            while (dr.Read())
            {
                //SELECT *
                //FROM `pre_common_usergroup`
                //LEFT JOIN `pre_common_usergroup_field` ON `pre_common_usergroup`.groupid = `pre_common_usergroup_field`.groupid
                UserGroupInfo objUser = new UserGroupInfo();

                objUser.groupid = Convert.ToInt32(dr["groupid"]);
                objUser.radminid = Convert.ToInt32(dr["radminid"]);
                if (objUser.radminid == -1)
                {
                    objUser.type = "special";
                    objUser.radminid = 0;
                }
                else if (Convert.ToInt32(dr["system"]) == 1)
                {
                    objUser.type = "system";
                }
                else if (objUser.radminid <= 3 && objUser.radminid > 0)//非系统的管理组
                {
                    objUser.type = "special";
                }
                else
                {
                    objUser.type = "member";
                }
                objUser.system = "private";
                objUser.grouptitle = dr["grouptitle"].ToString();
                objUser.creditshigher = Convert.ToInt32(dr["creditshigher"]);
                objUser.creditslower = Convert.ToInt32(dr["creditslower"]);
                objUser.stars = Convert.ToInt32(dr["stars"]);
                objUser.color = dr["color"].ToString();
                objUser.icon = dr["groupavatar"].ToString();
                objUser.allowvisit = Convert.ToInt32(dr["allowvisit"]);
                objUser.allowsendpm = 1;
                objUser.allowinvite = GetGroupValueByModers(objUser.radminid, 0, 1);
                objUser.allowmailinvite = GetGroupValueByModers(objUser.radminid, 0, 1);
                objUser.maxinvitenum = 0;
                objUser.inviteprice = 0;
                objUser.maxinviteday = GetGroupValueByLowerUsers(objUser.groupid, 0, 10);
                objUser.readaccess = Convert.ToInt32(dr["readaccess"]);
                objUser.allowpost = Convert.ToInt32(dr["allowpost"]);
                objUser.allowreply = Convert.ToInt32(dr["allowreply"]);
                objUser.allowpostpoll = Convert.ToInt32(dr["allowpostpoll"]);
                objUser.allowpostreward = GetGroupValueByLowerUsers(objUser.groupid, 0, 1);
                objUser.allowposttrade = GetGroupValueByLowerUsers(objUser.groupid, 0, 1);
                objUser.allowpostactivity = GetGroupValueByLowerUsers(objUser.groupid, 0, 1);
                objUser.allowdirectpost = Convert.ToInt32(dr["allowdirectpost"]);
                objUser.allowgetattach = Convert.ToInt32(dr["allowgetattach"]);
                objUser.allowpostattach = Convert.ToInt32(dr["allowpostattach"]);
                objUser.allowpostimage = GetGroupValueByLowerUsers(objUser.groupid, 0, 1);
                objUser.allowvote = Convert.ToInt32(dr["allowvote"]);
                objUser.allowmultigroups = GetGroupValueByLowerUsers(objUser.groupid, 0, 1);
                objUser.allowsearch = Convert.ToInt32(dr["allowsearch"]);
                objUser.allowcstatus = Convert.ToInt32(dr["allowcstatus"]);
                objUser.allowinvisible = Convert.ToInt32(dr["allowinvisible"]);
                objUser.allowtransfer = Convert.ToInt32(dr["allowtransfer"]);
                objUser.allowsetreadperm = Convert.ToInt32(dr["allowsetreadperm"]);
                objUser.allowsetattachperm = Convert.ToInt32(dr["allowsetattachperm"]);
                objUser.allowhidecode = Convert.ToInt32(dr["allowhidecode"]);
                objUser.allowhtml = Convert.ToInt32(dr["allowhtml"]);
                objUser.allowanonymous = 0;
                objUser.allowsigbbcode = Convert.ToInt32(dr["allowsigbbcode"]);
                objUser.allowsigimgcode = Convert.ToInt32(dr["allowsigimgcode"]);
                objUser.allowmagics = 0;
                objUser.disableperiodctrl = Convert.ToInt32(dr["disableperiodctrl"]);
                objUser.reasonpm = Convert.ToInt32(dr["reasonpm"]);
                objUser.maxprice = Convert.ToInt32(dr["maxprice"]);
                objUser.maxsigsize = Convert.ToInt32(dr["maxsigsize"]);
                objUser.maxattachsize = Convert.ToInt32(dr["maxattachsize"]);
                objUser.maxsizeperday = Convert.ToInt32(dr["maxsizeperday"]);
                objUser.maxpostsperhour = 0;
                objUser.attachextensions = dr["attachextensions"].ToString();
                objUser.raterange = dr["raterange"].ToString();
                objUser.mintradeprice = 1;
                objUser.maxtradeprice = 0;
                objUser.minrewardprice = 1;
                objUser.maxrewardprice = 0;
                objUser.magicsdiscount = 0;
                objUser.maxmagicsweight = 100;
                objUser.allowpostdebate = 0;
                objUser.tradestick = 5;
                if (objUser.groupid <= 2)
                {
                    objUser.exempt = 255;
                }
                else if (objUser.groupid == 224)
                {
                    objUser.exempt = 224;
                }
                else
                {
                    objUser.exempt = 0;
                }

                objUser.maxattachnum = 0;
                objUser.allowposturl = 3;
                if (objUser.groupid == 7)
                {
                    objUser.allowrecommend = 0;
                }
                else
                {
                    objUser.allowrecommend = 1;
                }
                if (objUser.groupid == 1)
                {
                    objUser.allowpostrushreply = 1;
                }
                else
                {
                    objUser.allowpostrushreply = 0;
                }
                objUser.maxfriendnum = 0;
                objUser.maxspacesize = 0;
                if (objUser.groupid >= 4 && objUser.groupid <= 9)
                {
                    objUser.allowcomment = 0;
                    objUser.allowcommentarticle = 0;
                }
                else
                {
                    objUser.allowcomment = 1;
                    objUser.allowcommentarticle = 1000;
                }
                objUser.searchinterval = 0;
                objUser.searchignore = 0;
                objUser.allowblog = GetGroupValueByLowerUsers(objUser.groupid, 0, 1);
                objUser.allowdoing = GetGroupValueByLowerUsers(objUser.groupid, 0, 1);
                objUser.allowupload = GetGroupValueByLowerUsers(objUser.groupid, 0, 1);
                objUser.allowshare = GetGroupValueByLowerUsers(objUser.groupid, 0, 1);
                objUser.allowblogmod = 0;
                objUser.allowdoingmod = 0;
                objUser.allowuploadmod = 0;
                objUser.allowsharemod = 0;
                objUser.allowcss = 0;
                objUser.allowpoke = GetGroupValueByLowerUsers(objUser.groupid, 0, 1);
                objUser.allowfriend = GetGroupValueByLowerUsers(objUser.groupid, 0, 1);
                objUser.allowclick = GetGroupValueByLowerUsers(objUser.groupid, 0, 1);
                objUser.allowmagic = 0;
                objUser.allowstat = GetGroupValueByAdminUsers(objUser.radminid, 0, 1);
                objUser.allowstatdata = GetGroupValueByAdminUsers(objUser.radminid, 0, 1);
                objUser.videophotoignore = GetGroupValueByAdminUsers(objUser.radminid, 0, 1);
                objUser.allowviewvideophoto = GetGroupValueByAdminUsers(objUser.radminid, 0, 1);
                objUser.allowmyop = GetGroupValueByLowerUsers(objUser.groupid, 0, 1);
                objUser.magicdiscount = 0;
                objUser.domainlength = GetGroupValueByLowerUsers(objUser.groupid, 0, 5);
                objUser.seccode = 1;
                objUser.disablepostctrl = GetGroupValueByModers(objUser.radminid, 0, 1);
                objUser.allowbuildgroup = GetGroupValueByLowerUsers(objUser.groupid, 0, 5);
                objUser.allowgroupdirectpost = 3;
                objUser.allowgroupposturl = GetGroupValueByAdminUsers(objUser.radminid, 0, 3);
                objUser.edittimelimit = 0;
                objUser.allowpostarticle = GetGroupValueByAdminUsers(objUser.radminid, 0, 1);
                objUser.allowdownlocalimg = GetGroupValueByAdminUsers(objUser.radminid, 0, 1);
                objUser.allowpostarticlemod = 0;
                objUser.allowspacediyhtml = GetGroupValueByAdminUsers(objUser.radminid, 0, 1);
                objUser.allowspacediybbcode = GetGroupValueByLowerUsers(objUser.groupid, 0, 1);
                objUser.allowspacediyimgcode = GetGroupValueByLowerUsers(objUser.groupid, 0, 1);
                objUser.allowcommentpost = GetGroupValueByLowerUsers(objUser.groupid, 0, 2);
                objUser.allowcommentitem = GetGroupValueByLowerUsers(objUser.groupid, 0, 1);
                objUser.ignorecensor = GetGroupValueByModers(objUser.radminid, 0, 1);
                userlist.Add(objUser);
            }
            //userlist[userlist.Count - 1].Creditslower = 99999999;
            return userlist;
        }
        /// <summary>
        /// 受限用户组的判断并返回值（禁言/访问/IP、游客、待验证、限制会员/乞丐）
        /// </summary>
        /// <param name="groupid"></param>
        /// <param name="lowerValue"></param>
        /// <param name="HigherValue"></param>
        /// <returns></returns>
        private static int GetGroupValueByLowerUsers(int groupid, int lowerValue, int HigherValue)
        {
            if (groupid >= 4 && groupid <= 9)
            {
                return lowerValue;
            }
            else
            {
                return HigherValue;
            }
        }

        /// <summary>
        /// 管理员特权
        /// </summary>
        /// <param name="groupid"></param>
        /// <param name="lowerValue"></param>
        /// <param name="adminValue"></param>
        /// <returns></returns>
        private static int GetGroupValueByAdminUsers(int radminid, int lowerValue, int adminValue)
        {
            if (radminid == 1)
            {
                return adminValue;
            }
            else
            {
                return lowerValue;
            }
        }

        /// <summary>
        /// 管理组特权
        /// </summary>
        /// <param name="groupid"></param>
        /// <param name="lowerValue"></param>
        /// <param name="adminValue"></param>
        /// <returns></returns>
        private static int GetGroupValueByModers(int radminid, int lowerValue, int adminValue)
        {
            if (radminid <= 3)
            {
                return adminValue;
            }
            else
            {
                return lowerValue;
            }
        }
    }
}
