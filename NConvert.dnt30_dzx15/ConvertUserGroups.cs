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
                UserGroupInfo objUser = new UserGroupInfo();





                objUser.groupid = Convert.ToInt32(dr["groupid"]);
                objUser.radminid = Convert.ToInt32(dr["radminid"]);
                if (objUser.radminid == -1)
                {
                    objUser.type = "special";
                }
                else if (Convert.ToInt32(dr["system"]) == 1)
                {
                    objUser.type = "system";
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
                objUser.allowsendpm = Convert.ToInt32(dr["allowsendpm"]);
                objUser.allowinvite = Convert.ToInt32(dr["allowinvite"]);
                objUser.allowmailinvite = Convert.ToInt32(dr["allowmailinvite"]);
                objUser.maxinvitenum = Convert.ToInt32(dr["maxinvitenum"]);
                objUser.inviteprice = Convert.ToInt32(dr["inviteprice"]);
                objUser.maxinviteday = Convert.ToInt32(dr["maxinviteday"]);
                objUser.readaccess = Convert.ToInt32(dr["readaccess"]);
                objUser.allowpost = Convert.ToInt32(dr["allowpost"]);
                objUser.allowreply = Convert.ToInt32(dr["allowreply"]);
                objUser.allowpostpoll = Convert.ToInt32(dr["allowpostpoll"]);
                objUser.allowpostreward = Convert.ToInt32(dr["allowpostreward"]);
                objUser.allowposttrade = Convert.ToInt32(dr["allowposttrade"]);
                objUser.allowpostactivity = Convert.ToInt32(dr["allowpostactivity"]);
                objUser.allowdirectpost = Convert.ToInt32(dr["allowdirectpost"]);
                objUser.allowgetattach = Convert.ToInt32(dr["allowgetattach"]);
                objUser.allowpostattach = Convert.ToInt32(dr["allowpostattach"]);
                objUser.allowpostimage = Convert.ToInt32(dr["allowpostimage"]);
                objUser.allowvote = Convert.ToInt32(dr["allowvote"]);
                if (objUser.groupid > 3 && objUser.groupid < 13)
                {
                    objUser.allowmultigroups = 0;
                }
                else
                {
                    objUser.allowmultigroups = 1;
                }
                objUser.allowsearch = Convert.ToInt32(dr["allowsearch"]);
                objUser.allowcstatus = Convert.ToInt32(dr["allowcstatus"]);
                objUser.allowinvisible = Convert.ToInt32(dr["allowinvisible"]);
                objUser.allowtransfer = Convert.ToInt32(dr["allowtransfer"]);
                objUser.allowsetreadperm = Convert.ToInt32(dr["allowsetreadperm"]);
                objUser.allowsetattachperm = Convert.ToInt32(dr["allowsetattachperm"]);
                objUser.allowhidecode = Convert.ToInt32(dr["allowhidecode"]);
                objUser.allowhtml = Convert.ToInt32(dr["allowhtml"]);
                objUser.allowanonymous = Convert.ToInt32(dr["allowanonymous"]);
                objUser.allowsigbbcode = Convert.ToInt32(dr["allowsigbbcode"]);
                objUser.allowsigimgcode = Convert.ToInt32(dr["allowsigimgcode"]);
                objUser.allowmagics = Convert.ToInt32(dr["allowmagics"]);
                objUser.disableperiodctrl = Convert.ToInt32(dr["disableperiodctrl"]);
                objUser.reasonpm = Convert.ToInt32(dr["reasonpm"]);
                objUser.maxprice = Convert.ToInt32(dr["maxprice"]);
                objUser.maxsigsize = Convert.ToInt32(dr["maxsigsize"]);
                objUser.maxattachsize = Convert.ToInt32(dr["maxattachsize"]);
                objUser.maxsizeperday = Convert.ToInt32(dr["maxsizeperday"]);
                objUser.maxpostsperhour = Convert.ToInt32(dr["maxpostsperhour"]);
                objUser.attachextensions = dr["attachextensions"].ToString();
                objUser.raterange = dr["raterange"].ToString();
                objUser.mintradeprice = Convert.ToInt32(dr["mintradeprice"]);
                objUser.maxtradeprice = Convert.ToInt32(dr["maxtradeprice"]);
                objUser.minrewardprice = Convert.ToInt32(dr["minrewardprice"]);
                objUser.maxrewardprice = Convert.ToInt32(dr["maxrewardprice"]);
                objUser.magicsdiscount = Convert.ToInt32(dr["magicsdiscount"]);
                objUser.maxmagicsweight = Convert.ToInt32(dr["maxmagicsweight"]);
                objUser.allowpostdebate = Convert.ToInt32(dr["allowpostdebate"]);
                objUser.tradestick = Convert.ToInt32(dr["tradestick"]);
                objUser.exempt = Convert.ToInt32(dr["exempt"]);
                objUser.maxattachnum = Convert.ToInt32(dr["maxattachnum"]);
                objUser.allowposturl = Convert.ToInt32(dr["allowposturl"]);
                objUser.allowrecommend = Convert.ToInt32(dr["allowrecommend"]);
                objUser.allowpostrushreply = Convert.ToInt32(dr["allowpostrushreply"]);
                objUser.maxfriendnum = Convert.ToInt32(dr["maxfriendnum"]);
                objUser.maxspacesize = Convert.ToInt32(dr["maxspacesize"]);
                objUser.allowcomment = Convert.ToInt32(dr["allowcomment"]);
                objUser.allowcommentarticle = Convert.ToInt32(dr["allowcommentarticle"]);
                objUser.searchinterval = Convert.ToInt32(dr["searchinterval"]);
                objUser.searchignore = Convert.ToInt32(dr["searchignore"]);
                objUser.allowblog = Convert.ToInt32(dr["allowblog"]);
                objUser.allowdoing = Convert.ToInt32(dr["allowdoing"]);
                objUser.allowupload = Convert.ToInt32(dr["allowupload"]);
                objUser.allowshare = Convert.ToInt32(dr["allowshare"]);
                objUser.allowblogmod = Convert.ToInt32(dr["allowblogmod"]);
                objUser.allowdoingmod = Convert.ToInt32(dr["allowdoingmod"]);
                objUser.allowuploadmod = Convert.ToInt32(dr["allowuploadmod"]);
                objUser.allowsharemod = Convert.ToInt32(dr["allowsharemod"]);
                objUser.allowcss = Convert.ToInt32(dr["allowcss"]);
                objUser.allowpoke = Convert.ToInt32(dr["allowpoke"]);
                objUser.allowfriend = Convert.ToInt32(dr["allowfriend"]);
                objUser.allowclick = Convert.ToInt32(dr["allowclick"]);
                objUser.allowmagic = Convert.ToInt32(dr["allowmagic"]);
                objUser.allowstat = Convert.ToInt32(dr["allowstat"]);
                objUser.allowstatdata = Convert.ToInt32(dr["allowstatdata"]);
                objUser.videophotoignore = Convert.ToInt32(dr["videophotoignore"]);
                objUser.allowviewvideophoto = Convert.ToInt32(dr["allowviewvideophoto"]);
                objUser.allowmyop = Convert.ToInt32(dr["allowmyop"]);
                objUser.magicdiscount = Convert.ToInt32(dr["magicdiscount"]);
                objUser.domainlength = Convert.ToInt32(dr["domainlength"]);
                objUser.seccode = Convert.ToInt32(dr["seccode"]);
                objUser.disablepostctrl = Convert.ToInt32(dr["disablepostctrl"]);
                objUser.allowbuildgroup = Convert.ToInt32(dr["allowbuildgroup"]);
                objUser.allowgroupdirectpost = Convert.ToInt32(dr["allowgroupdirectpost"]);
                objUser.allowgroupposturl = Convert.ToInt32(dr["allowgroupposturl"]);
                objUser.edittimelimit = Convert.ToInt32(dr["edittimelimit"]);
                objUser.allowpostarticle = Convert.ToInt32(dr["allowpostarticle"]);
                objUser.allowdownlocalimg = Convert.ToInt32(dr["allowdownlocalimg"]);
                objUser.allowpostarticlemod = Convert.ToInt32(dr["allowpostarticlemod"]);
                objUser.allowspacediyhtml = Convert.ToInt32(dr["allowspacediyhtml"]);
                objUser.allowspacediybbcode = Convert.ToInt32(dr["allowspacediybbcode"]);
                objUser.allowspacediyimgcode = Convert.ToInt32(dr["allowspacediyimgcode"]);
                objUser.allowcommentpost = Convert.ToInt32(dr["allowcommentpost"]);
                objUser.allowcommentitem = Convert.ToInt32(dr["allowcommentitem"]);
                objUser.ignorecensor = Convert.ToInt32(dr["ignorecensor"]);


















                userlist.Add(objUser);
            }
            //userlist[userlist.Count - 1].Creditslower = 99999999;
            return userlist;
        }
    }
}
