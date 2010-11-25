using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NConvert.Entity;
using Yuwen.Tools.TinyData;

namespace NConvert
{
    public class DBConvert
    {
        public static void Start()
        {
            //try
            //{
            if (!MainForm.DbConnStatus())
            {
                System.Windows.Forms.MessageBox.Show("数据库连接失败!\r\n");
                MainForm.MessageForm.SetButtonStatus(false);
                System.Threading.Thread.CurrentThread.Abort();
            }

            if (MainForm.IsConvertUserGroups)
                ConvertUserGroups();

            if (MainForm.IsConvertUsers)
                ConvertUsers();

            if (MainForm.IsConvertForums)
                ConvertForums();

            if (MainForm.IsConvertTopicTypes)
                ConvertTopicTypes();

            if (MainForm.IsConvertTopics)
            {
                ConvertTopics();
                ResetTopicsInfo();
            }

            if (MainForm.IsConvertPosts)
            {
                ConvertPosts();
                UpdateLastPostid();
            }
            if (MainForm.IsUpdatePostsInfo)
            {
                UpdatePostsInfo();
            }

            if (MainForm.IsConvertPolls)
            {
                ConvertPolls();
                ConvertPollOptions();
            }
            if (MainForm.IsConvertPollRecords)
            {
                ConvertVoteRecords();
            }

            if (MainForm.IsConvertAttachments)
                ConvertAttachments();
            if (MainForm.IsConvertPms)
                ConvertPms();
            if (MainForm.IsConvertForumLinks)
                ConvertForumLinks();
            if (MainForm.IsResetTopicLastpostid)
                Utils.Topics.ResetTopicLastpostid();

            if (MainForm.IsResetTopicReplies)
                ResetTopicReplyCount();


            MainForm.MessageForm.SetMessage(string.Format("========={0}==========\r\n", DateTime.Now));
            //}
            //catch (Exception ex)
            //{
            //MainForm.MessageForm.SetMessage(string.Format("初始化错误:{0}\r\n", ex.Message));
            //}
            MainForm.MessageForm.SetButtonStatus(false);

        }


        /// <summary>
        /// 转换用户
        /// </summary>
        public static void ConvertUserGroups()
        {
            Yuwen.Tools.Data.DBHelper dbhConvertUsers = MainForm.GetTargetDBH_OldVer();//.GetTargetDBH();
            dbhConvertUsers.Open();
            MainForm.MessageForm.SetMessage("开始转换用户组\r\n");
            MainForm.SuccessedRecordCount = 0;
            MainForm.FailedRecordCount = 0;

            MainForm.RecordCount = Provider.Provider.GetInstance().GetUserGroupsRecordCount();
            if (MainForm.RecordCount % MainForm.PageSize != 0)
            {
                MainForm.PageCount = MainForm.RecordCount / MainForm.PageSize + 1;
            }
            else
            {
                MainForm.PageCount = MainForm.RecordCount / MainForm.PageSize;
            }
            MainForm.MessageForm.InitTotalProgressBar(MainForm.PageCount);
            MainForm.MessageForm.InitCurrentProgressBar(MainForm.RecordCount);

            //清理数据库
            //dbhConvertUsers.TruncateTable(string.Format("{0}usergroups", MainForm.cic.TargetDbTablePrefix));
            dbhConvertUsers.ExecuteNonQuery(
                string.Format("TRUNCATE TABLE {0}common_usergroup", MainForm.cic.TargetDbTablePrefix)
                );

            //try
            //{
            //dbhConvertUsers.SetIdentityInsertON(string.Format("{0}usergroups", MainForm.cic.TargetDbTablePrefix));
            //}
            //catch (Exception ex)
            //{
            //    MainForm.MessageForm.SetMessage(string.Format("{0}\r\n", ex.Message));
            //}

            #region sql语句
            string sqlUser = string.Format(@"Insert into {0}common_usergroup
(
`groupid`, 
`radminid`, 
`type`, 
`system`, 
`grouptitle`, 
`creditshigher`, 
`creditslower`, 
`stars`, 
`color`, 
`icon`, 
`allowvisit`, 
`allowsendpm`, 
`allowinvite`, 
`allowmailinvite`, 
`maxinvitenum`, 
`inviteprice`, 
`maxinviteday`, 
`readaccess`, 
`allowpost`, 
`allowreply`, 
`allowpostpoll`, 
`allowpostreward`, 
`allowposttrade`, 
`allowpostactivity`, 
`allowdirectpost`, 
`allowgetattach`, 
`allowpostattach`, 
`allowpostimage`, 
`allowvote`, 
`allowmultigroups`, 
`allowsearch`, 
`allowcstatus`, 
`allowinvisible`, 
`allowtransfer`, 
`allowsetreadperm`, 
`allowsetattachperm`, 
`allowhidecode`, 
`allowhtml`, 
`allowanonymous`, 
`allowsigbbcode`, 
`allowsigimgcode`, 
`allowmagics`, 
`disableperiodctrl`, 
`reasonpm`, 
`maxprice`, 
`maxsigsize`, 
`maxattachsize`, 
`maxsizeperday`, 
`maxpostsperhour`, 
`attachextensions`, 
`raterange`, 
`mintradeprice`, 
`maxtradeprice`, 
`minrewardprice`, 
`maxrewardprice`, 
`magicsdiscount`, 
`maxmagicsweight`, 
`allowpostdebate`, 
`tradestick`, 
`exempt`, 
`maxattachnum`, 
`allowposturl`, 
`allowrecommend`, 
`allowpostrushreply`, 
`maxfriendnum`, 
`maxspacesize`, 
`allowcomment`, 
`allowcommentarticle`, 
`searchinterval`, 
`searchignore`, 
`allowblog`, 
`allowdoing`, 
`allowupload`, 
`allowshare`, 
`allowblogmod`, 
`allowdoingmod`, 
`allowuploadmod`, 
`allowsharemod`, 
`allowcss`, 
`allowpoke`, 
`allowfriend`, 
`allowclick`, 
`allowmagic`, 
`allowstat`, 
`allowstatdata`, 
`videophotoignore`, 
`allowviewvideophoto`, 
`allowmyop`, 
`magicdiscount`, 
`domainlength`, 
`seccode`, 
`disablepostctrl`, 
`allowbuildgroup`, 
`allowgroupdirectpost`, 
`allowgroupposturl`, 
`edittimelimit`, 
`allowpostarticle`, 
`allowdownlocalimg`, 
`allowpostarticlemod`, 
`allowspacediyhtml`, 
`allowspacediybbcode`, 
`allowspacediyimgcode`, 
`allowcommentpost`, 
`allowcommentitem`, 
`ignorecensor`
) 
values
(
@groupid,
@radminid, 
@type, 
@system, 
@grouptitle, 
@creditshigher, 
@creditslower, 
@stars, 
@color, 
@icon, 
@allowvisit, 
@allowsendpm, 
@allowinvite, 
@allowmailinvite, 
@maxinvitenum, 
@inviteprice, 
@maxinviteday, 
@readaccess, 
@allowpost, 
@allowreply, 
@allowpostpoll, 
@allowpostreward, 
@allowposttrade, 
@allowpostactivity, 
@allowdirectpost, 
@allowgetattach, 
@allowpostattach, 
@allowpostimage, 
@allowvote, 
@allowmultigroups, 
@allowsearch, 
@allowcstatus, 
@allowinvisible, 
@allowtransfer, 
@allowsetreadperm, 
@allowsetattachperm, 
@allowhidecode, 
@allowhtml, 
@allowanonymous, 
@allowsigbbcode, 
@allowsigimgcode, 
@allowmagics, 
@disableperiodctrl, 
@reasonpm, 
@maxprice, 
@maxsigsize, 
@maxattachsize, 
@maxsizeperday, 
@maxpostsperhour, 
@attachextensions, 
@raterange, 
@mintradeprice, 
@maxtradeprice, 
@minrewardprice, 
@maxrewardprice, 
@magicsdiscount, 
@maxmagicsweight, 
@allowpostdebate, 
@tradestick, 
@exempt, 
@maxattachnum, 
@allowposturl, 
@allowrecommend, 
@allowpostrushreply, 
@maxfriendnum, 
@maxspacesize, 
@allowcomment, 
@allowcommentarticle, 
@searchinterval, 
@searchignore, 
@allowblog, 
@allowdoing, 
@allowupload, 
@allowshare, 
@allowblogmod, 
@allowdoingmod, 
@allowuploadmod, 
@allowsharemod, 
@allowcss, 
@allowpoke, 
@allowfriend, 
@allowclick, 
@allowmagic, 
@allowstat, 
@allowstatdata, 
@videophotoignore, 
@allowviewvideophoto, 
@allowmyop, 
@magicdiscount, 
@domainlength, 
@seccode, 
@disablepostctrl, 
@allowbuildgroup, 
@allowgroupdirectpost, 
@allowgroupposturl, 
@edittimelimit, 
@allowpostarticle, 
@allowdownlocalimg, 
@allowpostarticlemod, 
@allowspacediyhtml, 
@allowspacediybbcode, 
@allowspacediyimgcode, 
@allowcommentpost, 
@allowcommentitem, 
@ignorecensor
)", MainForm.cic.TargetDbTablePrefix);
            #endregion

            for (int pagei = 1; pagei <= MainForm.PageCount; pagei++)
            {
                //分段得到用户列表
                List<UserGroupInfo> userList = Provider.Provider.GetInstance().GetUserGroupList(pagei);
                foreach (UserGroupInfo objUser in userList)
                {
                    try
                    {
                        //清理上次执行的参数
                        dbhConvertUsers.ParametersClear();
                        #region dnt_users表参数
                        dbhConvertUsers.ParameterAdd("@groupid", objUser.Groupid, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@radminid", objUser.Radminid, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@type", objUser.Type, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@system", objUser.System, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@grouptitle", objUser.Grouptitle, DbType.String, 50);
                        dbhConvertUsers.ParameterAdd("@creditshigher", objUser.Creditshigher, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@creditslower", objUser.Creditslower, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@stars", objUser.Stars, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@color", objUser.Color, DbType.String, 7);
                        dbhConvertUsers.ParameterAdd("@groupavatar", objUser.Groupavatar, DbType.String, 60);
                        dbhConvertUsers.ParameterAdd("@readaccess", objUser.Readaccess, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allowvisit", objUser.Allowvisit, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allowpost", objUser.Allowpost, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allowreply", objUser.Allowreply, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allowpostpoll", objUser.Allowpostpoll, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allowdirectpost", objUser.Allowdirectpost, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allowgetattach", objUser.Allowgetattach, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allowpostattach", objUser.Allowpostattach, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allowvote", objUser.Allowvote, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allowmultigroups", objUser.Allowmultigroups, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allowsearch", objUser.Allowsearch, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allowavatar", objUser.Allowavatar, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allowcstatus", objUser.Allowcstatus, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allowuseblog", objUser.Allowuseblog, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allowinvisible", objUser.Allowinvisible, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allowtransfer", objUser.Allowtransfer, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allowsetreadperm", objUser.Allowsetreadperm, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allowsetattachperm", objUser.Allowsetattachperm, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allowhidecode", objUser.Allowhidecode, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allowhtml", objUser.Allowhtml, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allowcusbbcode", objUser.Allowcusbbcode, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allownickname", objUser.Allownickname, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allowsigbbcode", objUser.Allowsigbbcode, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allowsigimgcode", objUser.Allowsigimgcode, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allowviewpro", objUser.Allowviewpro, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allowviewstats", objUser.Allowviewstats, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@disableperiodctrl", objUser.Disableperiodctrl, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@reasonpm", objUser.Reasonpm, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@maxprice", objUser.Maxprice, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@maxpmnum", objUser.Maxpmnum, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@maxsigsize", objUser.Maxsigsize, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@maxattachsize", objUser.Maxattachsize, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@maxsizeperday", objUser.Maxsizeperday, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@attachextensions", objUser.Attachextensions, DbType.String, 100);
                        dbhConvertUsers.ParameterAdd("@raterange", objUser.Raterange, DbType.String, 500);
                        dbhConvertUsers.ParameterAdd("@allowspace", objUser.Allowspace, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@maxspaceattachsize", objUser.Maxspaceattachsize, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@maxspacephotosize", objUser.Maxspacephotosize, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allowdebate", objUser.Allowdebate, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allowbonus", objUser.Allowbonus, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@minbonusprice", objUser.Minbonusprice, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@maxbonusprice", objUser.Maxbonusprice, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allowtrade", objUser.Allowtrade, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allowdiggs", objUser.Allowdiggs, DbType.Int32, 4);
                        #endregion
                        dbhConvertUsers.ExecuteNonQuery(sqlUser);//插入dnt_userGroups表
                        MainForm.SuccessedRecordCount++;
                    }
                    catch (Exception ex)
                    {
                        MainForm.MessageForm.SetMessage(string.Format("错误:{0}.groupid={1}\r\n", ex.Message, objUser.Groupid));
                        MainForm.FailedRecordCount++;
                    }
                    MainForm.MessageForm.CurrentProgressBarNumAdd();
                }
                MainForm.MessageForm.TotalProgressBarNumAdd();
            }

            //dbh.ExecuteNonQuery(string.Format("SET IDENTITY_INSERT {0}users OFF", MainForm.cic.TargetDbTablePrefix));

            dbhConvertUsers.SetIdentityInsertOFF(string.Format("{0}users", MainForm.cic.TargetDbTablePrefix));
            dbhConvertUsers.Dispose();//.Close();
            dbhConvertUsers.Dispose();
            MainForm.RecordCount = -1;
            MainForm.MessageForm.SetMessage(string.Format("完成转换用户。成功{0}，失败{1}\r\n", MainForm.SuccessedRecordCount, MainForm.FailedRecordCount));
        }

        /// <summary>
        /// 转换用户
        /// </summary>
        public static void ConvertUsers()
        {
            DBHelper dbhConvertUsers = MainForm.GetTargetDBH();
            dbhConvertUsers.Open();
            MainForm.MessageForm.SetMessage("开始转换用户\r\n");
            MainForm.SuccessedRecordCount = 0;
            MainForm.FailedRecordCount = 0;

            MainForm.RecordCount = Provider.Provider.GetInstance().GetUsersRecordCount();
            if (MainForm.RecordCount % MainForm.PageSize != 0)
            {
                MainForm.PageCount = MainForm.RecordCount / MainForm.PageSize + 1;
            }
            else
            {
                MainForm.PageCount = MainForm.RecordCount / MainForm.PageSize;
            }
            MainForm.MessageForm.InitTotalProgressBar(MainForm.PageCount);
            MainForm.MessageForm.InitCurrentProgressBar(MainForm.RecordCount);

            //清理数据库
            dbhConvertUsers.TruncateTable(string.Format("{0}users", MainForm.cic.TargetDbTablePrefix));
            dbhConvertUsers.TruncateTable(string.Format("{0}userfields", MainForm.cic.TargetDbTablePrefix));

            //try
            //{
            dbhConvertUsers.SetIdentityInsertON(string.Format("{0}users", MainForm.cic.TargetDbTablePrefix));
            //}
            //catch (Exception ex)
            //{
            //    MainForm.MessageForm.SetMessage(string.Format("{0}\r\n", ex.Message));
            //}

            #region sql语句
            string sqlUser = string.Format(@"Insert into {0}users
(
uid,
username,
nickname,
password,
secques,
spaceid,
gender,
adminid,
groupid,
groupexpiry,
extgroupids,
regip,
joindate,
lastip,
lastvisit,
lastactivity,
lastpost,
lastpostid,
lastposttitle,
posts,
digestposts,
oltime,
pageviews,
credits,
extcredits1,
extcredits2,
extcredits3,
extcredits4,
extcredits5,
extcredits6,
extcredits7,
extcredits8,
avatarshowid,
email,
bday,
sigstatus,
tpp,
ppp,
templateid,
pmsound,
showemail,
invisible,
newpm,
newpmcount,
accessmasks,
onlinestate,
newsletter
) 
values
(
@uid,
@username,
@nickname,
@password,
@secques,
@spaceid,
@gender,
@adminid,
@groupid,
@groupexpiry,
@extgroupids,
@regip,
@joindate,
@lastip,
@lastvisit,
@lastactivity,
@lastpost,
@lastpostid,
@lastposttitle,
@posts,
@digestposts,
@oltime,
@pageviews,
@credits,
@extcredits1,
@extcredits2,
@extcredits3,
@extcredits4,
@extcredits5,
@extcredits6,
@extcredits7,
@extcredits8,
@avatarshowid,
@email,
@bday,
@sigstatus,
@tpp,
@ppp,
@templateid,
@pmsound,
@showemail,
@invisible,
@newpm,
@newpmcount,
@accessmasks,
@onlinestate,
@newsletter
)", MainForm.cic.TargetDbTablePrefix);
            string sqlUserfields = string.Format(@"INSERT INTO {0}userfields
(
uid,
website,
icq,
qq,
yahoo,
msn,
skype,
location,
customstatus,
avatar,
avatarwidth,
avatarheight,
medals,
bio,
signature,
sightml,
authstr,
authtime,
authflag,
realname,
idcard,
mobile,
phone
) VALUES
(
@uid,
@website,
@icq,
@qq,
@yahoo,
@msn,
@skype,
@location,
@customstatus,
@avatar,
@avatarwidth,
@avatarheight,
@medals,
@bio,
@signature,
@sightml,
@authstr,
@authtime,
@authflag,
@realname,
@idcard,
@mobile,
@phone
)", MainForm.cic.TargetDbTablePrefix);
            #endregion

            for (int pagei = 1; pagei <= MainForm.PageCount; pagei++)
            {
                //分段得到用户列表
                List<Users> userList = Provider.Provider.GetInstance().GetUserList(pagei);
                foreach (Users objUser in userList)
                {
                    try
                    {
                        //清理上次执行的参数
                        dbhConvertUsers.ParametersClear();
                        #region dnt_users表参数
                        dbhConvertUsers.ParameterAdd("@uid", objUser.uid, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@username", objUser.username, DbType.String, 20);
                        dbhConvertUsers.ParameterAdd("@nickname", objUser.nickname, DbType.String, 20);
                        dbhConvertUsers.ParameterAdd("@password", objUser.password, DbType.String, 32);
                        dbhConvertUsers.ParameterAdd("@secques", objUser.secques, DbType.String, 8);
                        dbhConvertUsers.ParameterAdd("@spaceid", objUser.spaceid, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@gender", objUser.gender, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@adminid", objUser.adminid, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@groupid", objUser.groupid, DbType.Int16, 2);
                        dbhConvertUsers.ParameterAdd("@groupexpiry", objUser.groupexpiry, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@extgroupids", objUser.extgroupids, DbType.String, 60);
                        dbhConvertUsers.ParameterAdd("@regip", objUser.regip, DbType.String, 15);
                        dbhConvertUsers.ParameterAdd("@joindate", objUser.joindate, DbType.DateTime, 4);
                        dbhConvertUsers.ParameterAdd("@lastip", objUser.lastip, DbType.String, 15);
                        dbhConvertUsers.ParameterAdd("@lastvisit", objUser.lastvisit, DbType.DateTime, 8);
                        dbhConvertUsers.ParameterAdd("@lastactivity", objUser.lastactivity, DbType.DateTime, 8);
                        dbhConvertUsers.ParameterAdd("@lastpost", objUser.lastpost, DbType.DateTime, 8);
                        dbhConvertUsers.ParameterAdd("@lastpostid", objUser.lastpostid, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@lastposttitle", objUser.lastposttitle, DbType.String, 60);
                        dbhConvertUsers.ParameterAdd("@posts", objUser.posts, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@digestposts", objUser.digestposts, DbType.Int16, 2);
                        dbhConvertUsers.ParameterAdd("@oltime", objUser.oltime, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@pageviews", objUser.pageviews, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@credits", objUser.credits, DbType.Decimal, 9);
                        dbhConvertUsers.ParameterAdd("@extcredits1", objUser.extcredits1, DbType.Decimal, 9);
                        dbhConvertUsers.ParameterAdd("@extcredits2", objUser.extcredits2, DbType.Decimal, 9);
                        dbhConvertUsers.ParameterAdd("@extcredits3", objUser.extcredits3, DbType.Decimal, 9);
                        dbhConvertUsers.ParameterAdd("@extcredits4", objUser.extcredits4, DbType.Decimal, 9);
                        dbhConvertUsers.ParameterAdd("@extcredits5", objUser.extcredits5, DbType.Decimal, 9);
                        dbhConvertUsers.ParameterAdd("@extcredits6", objUser.extcredits6, DbType.Decimal, 9);
                        dbhConvertUsers.ParameterAdd("@extcredits7", objUser.extcredits7, DbType.Decimal, 9);
                        dbhConvertUsers.ParameterAdd("@extcredits8", objUser.extcredits8, DbType.Decimal, 9);
                        dbhConvertUsers.ParameterAdd("@avatarshowid", objUser.avatarshowid, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@email", objUser.email, DbType.String, 50);
                        dbhConvertUsers.ParameterAdd("@bday", objUser.bday, DbType.String, 10);
                        dbhConvertUsers.ParameterAdd("@sigstatus", objUser.sigstatus, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@tpp", objUser.tpp, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@ppp", objUser.ppp, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@templateid", objUser.templateid, DbType.Int16, 2);
                        dbhConvertUsers.ParameterAdd("@pmsound", objUser.pmsound, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@showemail", objUser.showemail, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@invisible", objUser.invisible, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@newpm", objUser.newpm, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@newpmcount", objUser.newpmcount, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@accessmasks", objUser.accessmasks, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@onlinestate", objUser.onlinestate, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@newsletter", objUser.newsletter, DbType.Int32, 4);
                        #endregion
                        dbhConvertUsers.ExecuteNonQuery(sqlUser);//插入dnt_users表


                        //清理上次执行的参数
                        dbhConvertUsers.ParametersClear();
                        #region dnt_userfields表参数
                        dbhConvertUsers.ParameterAdd("@uid", objUser.uid, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@website", objUser.website, DbType.String, 80);
                        dbhConvertUsers.ParameterAdd("@icq", objUser.icq, DbType.String, 12);
                        dbhConvertUsers.ParameterAdd("@qq", objUser.qq, DbType.String, 12);
                        dbhConvertUsers.ParameterAdd("@yahoo", objUser.yahoo, DbType.String, 40);
                        dbhConvertUsers.ParameterAdd("@msn", objUser.msn, DbType.String, 40);
                        dbhConvertUsers.ParameterAdd("@skype", objUser.skype, DbType.String, 40);
                        dbhConvertUsers.ParameterAdd("@location", objUser.location, DbType.String, 50);
                        dbhConvertUsers.ParameterAdd("@customstatus", objUser.customstatus, DbType.String, 50);
                        dbhConvertUsers.ParameterAdd("@avatar", objUser.avatar, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@avatarwidth", objUser.avatarwidth, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@avatarheight", objUser.avatarheight, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@medals", objUser.medals, DbType.String, 300);
                        dbhConvertUsers.ParameterAdd("@bio", objUser.bio, DbType.String, 500);
                        dbhConvertUsers.ParameterAdd("@signature", objUser.signature, DbType.String, 500);
                        dbhConvertUsers.ParameterAdd("@sightml", objUser.sightml, DbType.String, 1000);
                        dbhConvertUsers.ParameterAdd("@authstr", objUser.authstr, DbType.String, 20);
                        dbhConvertUsers.ParameterAdd("@authtime", objUser.authtime, DbType.DateTime, 4);
                        dbhConvertUsers.ParameterAdd("@authflag", objUser.authflag, DbType.Boolean, 1);
                        dbhConvertUsers.ParameterAdd("@realname", objUser.realname, DbType.String, 10);
                        dbhConvertUsers.ParameterAdd("@idcard", objUser.idcard, DbType.String, 20);
                        dbhConvertUsers.ParameterAdd("@mobile", objUser.mobile, DbType.String, 20);
                        dbhConvertUsers.ParameterAdd("@phone", objUser.phone, DbType.String, 20);
                        #endregion
                        dbhConvertUsers.ExecuteNonQuery(sqlUserfields);//插入dnt_userfields表
                        MainForm.SuccessedRecordCount++;
                    }
                    catch (Exception ex)
                    {
                        MainForm.MessageForm.SetMessage(string.Format("错误:{0}.uid={1}\r\n", ex.Message, objUser.uid));
                        MainForm.FailedRecordCount++;
                    }
                    MainForm.MessageForm.CurrentProgressBarNumAdd();
                }
                MainForm.MessageForm.TotalProgressBarNumAdd();
            }

            //dbh.ExecuteNonQuery(string.Format("SET IDENTITY_INSERT {0}users OFF", MainForm.cic.TargetDbTablePrefix));

            dbhConvertUsers.SetIdentityInsertOFF(string.Format("{0}users", MainForm.cic.TargetDbTablePrefix));
            dbhConvertUsers.Close();
            dbhConvertUsers.Dispose();
            MainForm.RecordCount = -1;
            MainForm.MessageForm.SetMessage(string.Format("完成转换用户。成功{0}，失败{1}\r\n", MainForm.SuccessedRecordCount, MainForm.FailedRecordCount));
        }

        /// <summary>
        /// 转换论坛版块
        /// </summary>
        public static void ConvertForums()
        {
            DBHelper dbhConvertForums = new DBHelper(MainForm.targetDbConn, "System.Data.SqlClient");
            dbhConvertForums.Open();
            MainForm.MessageForm.SetMessage("开始转换版块\r\n");
            MainForm.SuccessedRecordCount = 0;
            MainForm.FailedRecordCount = 0;

            MainForm.RecordCount = Provider.Provider.GetInstance().GetForumsRecordCount();

            MainForm.MessageForm.InitTotalProgressBar(1);
            MainForm.MessageForm.InitCurrentProgressBar(MainForm.RecordCount);

            //清理数据库
            dbhConvertForums.TruncateTable(string.Format("{0}forums", MainForm.cic.TargetDbTablePrefix));
            dbhConvertForums.TruncateTable(string.Format("{0}forumfields", MainForm.cic.TargetDbTablePrefix));


            //try
            //{
            dbhConvertForums.SetIdentityInsertON(string.Format("{0}forums", MainForm.cic.TargetDbTablePrefix));
            //}
            //catch (Exception ex)
            //{
            //    MainForm.MessageForm.SetMessage(string.Format("{0}\r\n", ex.Message));
            //}
            #region sql语句
            string sqlForum = string.Format(@"INSERT INTO {0}forums
(
fid, 
layer, 
parentid, 
pathlist, 
parentidlist, 
subforumcount, 
name, 
status, 
colcount, 
displayorder, 
templateid, 
topics, 
curtopics, 
posts, 
todayposts, 
lasttid, 
lasttitle, 
lastpost, 
lastposterid, 
lastposter, 
allowsmilies, 
allowrss, 
allowhtml, 
allowbbcode, 
allowimgcode, 
allowblog, 
istrade, 
allowpostspecial, 
allowspecialonly,
alloweditrules, 
allowthumbnail, 
allowtag,
recyclebin, 
modnewposts, 
jammer, 
disablewatermark, 
inheritedmod, 
autoclose
)
VALUES 
(
@fid, 
@layer, 
@parentid, 
@pathlist, 
@parentidlist, 
@subforumcount, 
@name, 
@status, 
@colcount, 
@displayorder, 
@templateid, 
@topics, 
@curtopics, 
@posts, 
@todayposts, 
@lasttid, 
@lasttitle, 
@lastpost, 
@lastposterid,
@lastposter, 
@allowsmilies, 
@allowrss, 
@allowhtml, 
@allowbbcode, 
@allowimgcode, 
@allowblog, 
@istrade, 
@allowpostspecial,
@allowspecialonly, 
@alloweditrules, 
@allowthumbnail,  
@allowtag,
@recyclebin, 
@modnewposts, 
@jammer, 
@disablewatermark, 
@inheritedmod, 
@autoclose
)", MainForm.cic.TargetDbTablePrefix);
            string sqlForumfields = string.Format(@"INSERT INTO {0}forumfields
(
fid, 
password, 
icon, 
postcredits, 
replycredits, 
redirect, 
attachextensions, 
rules, 
topictypes, 
viewperm, 
postperm, 
replyperm, 
getattachperm, 
postattachperm, 
moderators, 
description, 
applytopictype, 
postbytopictype, 
viewbytopictype, 
topictypeprefix, 
permuserlist
)
VALUES 
(
@fid,
@password,
@icon,
@postcredits,
@replycredits,
@redirect,
@attachextensions,
@rules,
@topictypes,
@viewperm,
@postperm,
@replyperm,
@getattachperm,
@postattachperm,
@moderators,
@description,
@applytopictype,
@postbytopictype,
@viewbytopictype,
@topictypeprefix,
@permuserlist
)
", MainForm.cic.TargetDbTablePrefix);
            #endregion

            List<Forums> forumList = Provider.Provider.GetInstance().GetForumList();
            foreach (Forums objForum in forumList)
            {
                try
                {
                    //清理上次执行的参数
                    dbhConvertForums.ParametersClear();
                    #region dnt_forums表参数
                    dbhConvertForums.ParameterAdd("@fid", objForum.fid, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@layer", objForum.layer, DbType.Int16, 2);
                    dbhConvertForums.ParameterAdd("@parentid", objForum.parentid, DbType.Int16, 2);
                    dbhConvertForums.ParameterAdd("@pathlist", objForum.pathlist, DbType.String, 3000);
                    dbhConvertForums.ParameterAdd("@parentidlist", objForum.parentidlist, DbType.String, 300);
                    dbhConvertForums.ParameterAdd("@subforumcount", objForum.subforumcount, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@name", objForum.name, DbType.String, 50);
                    dbhConvertForums.ParameterAdd("@status", objForum.status, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@colcount", objForum.colcount, DbType.Int16, 2);
                    dbhConvertForums.ParameterAdd("@displayorder", objForum.displayorder, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@templateid", objForum.templateid, DbType.Int16, 2);
                    dbhConvertForums.ParameterAdd("@topics", objForum.topics, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@curtopics", objForum.curtopics, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@posts", objForum.posts, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@todayposts", objForum.todayposts, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@lasttid", objForum.lasttid, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@lasttitle", objForum.lasttitle, DbType.String, 60);
                    dbhConvertForums.ParameterAdd("@lastpost", objForum.lastpost, DbType.DateTime, 8);
                    dbhConvertForums.ParameterAdd("@lastposterid", objForum.lastposterid, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@lastposter", objForum.lastposter, DbType.String, 20);
                    dbhConvertForums.ParameterAdd("@allowsmilies", objForum.allowsmilies, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@allowrss", objForum.allowrss, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@allowhtml", objForum.allowhtml, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@allowbbcode", objForum.allowbbcode, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@allowimgcode", objForum.allowimgcode, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@allowblog", objForum.allowblog, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@istrade", objForum.istrade, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@allowpostspecial", objForum.allowpostspecial, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@allowspecialonly", objForum.allowspecialonly, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@alloweditrules", objForum.alloweditrules, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@allowthumbnail", objForum.allowthumbnail, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@allowtag", objForum.allowtag, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@recyclebin", objForum.recyclebin, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@modnewposts", objForum.modnewposts, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@jammer", objForum.jammer, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@disablewatermark", objForum.disablewatermark, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@inheritedmod", objForum.inheritedmod, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@autoclose", objForum.autoclose, DbType.Int32, 2);
                    #endregion
                    dbhConvertForums.ExecuteNonQuery(sqlForum);//插入dnt_forums表


                    //清理上次执行的参数
                    dbhConvertForums.ParametersClear();
                    #region dnt_forumfields表参数
                    dbhConvertForums.ParameterAdd("@fid", objForum.fid, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@password", objForum.password, DbType.String, 16);
                    dbhConvertForums.ParameterAdd("@icon", objForum.icon, DbType.String, 255);
                    dbhConvertForums.ParameterAdd("@postcredits", objForum.postcredits, DbType.String, 255);
                    dbhConvertForums.ParameterAdd("@replycredits", objForum.replycredits, DbType.String, 255);
                    dbhConvertForums.ParameterAdd("@redirect", objForum.redirect, DbType.String, 255);
                    dbhConvertForums.ParameterAdd("@attachextensions", objForum.attachextensions, DbType.String, 255);
                    dbhConvertForums.ParameterAdd("@rules", objForum.rules, DbType.String, 4000);
                    dbhConvertForums.ParameterAdd("@topictypes", objForum.topictypes, DbType.String, 4000);
                    dbhConvertForums.ParameterAdd("@viewperm", objForum.viewperm, DbType.String, 4000);
                    dbhConvertForums.ParameterAdd("@postperm", objForum.postperm, DbType.String, 4000);
                    dbhConvertForums.ParameterAdd("@replyperm", objForum.replyperm, DbType.String, 4000);
                    dbhConvertForums.ParameterAdd("@getattachperm", objForum.getattachperm, DbType.String, 4000);
                    dbhConvertForums.ParameterAdd("@postattachperm", objForum.postattachperm, DbType.String, 4000);
                    dbhConvertForums.ParameterAdd("@moderators", objForum.moderators, DbType.String, 4000);
                    dbhConvertForums.ParameterAdd("@description", objForum.description, DbType.String, 4000);
                    dbhConvertForums.ParameterAdd("@applytopictype", objForum.applytopictype, DbType.Byte, 1);
                    dbhConvertForums.ParameterAdd("@postbytopictype", objForum.postbytopictype, DbType.Byte, 1);
                    dbhConvertForums.ParameterAdd("@viewbytopictype", objForum.viewbytopictype, DbType.Byte, 1);
                    dbhConvertForums.ParameterAdd("@topictypeprefix", objForum.topictypeprefix, DbType.Byte, 1);
                    dbhConvertForums.ParameterAdd("@permuserlist", objForum.permuserlist, DbType.String, 4000);
                    //MainForm.targetDBH.ParameterAdd("@", objForum, DbType);
                    #endregion
                    dbhConvertForums.ExecuteNonQuery(sqlForumfields);//插入dnt_forumfields表
                    MainForm.SuccessedRecordCount++;
                }
                catch (Exception ex)
                {
                    MainForm.MessageForm.SetMessage(string.Format("错误:{0}.fid={1}\r\n", ex.Message, objForum.fid));
                    MainForm.FailedRecordCount++;
                }
                MainForm.MessageForm.CurrentProgressBarNumAdd();
            }
            MainForm.MessageForm.TotalProgressBarNumAdd();

            //dbh.ExecuteNonQuery(string.Format("SET IDENTITY_INSERT {0}forums OFF", MainForm.cic.TargetDbTablePrefix));
            dbhConvertForums.SetIdentityInsertOFF(string.Format("{0}forums", MainForm.cic.TargetDbTablePrefix));
            dbhConvertForums.Close();
            dbhConvertForums.Dispose();
            MainForm.RecordCount = -1;
            MainForm.MessageForm.SetMessage(string.Format("完成转换版块。成功{0}，失败{1}\r\n", MainForm.SuccessedRecordCount, MainForm.FailedRecordCount));
            //整理版块
            Utils.Forums.ResetForums();
            MainForm.MessageForm.SetMessage("完成整理版块\r\n");
        }

        /// <summary>
        /// 转换主题分类
        /// </summary>
        public static void ConvertTopicTypes()
        {
            DBHelper dbhConvertTopicTypes = new DBHelper(MainForm.targetDbConn, "System.Data.SqlClient");
            dbhConvertTopicTypes.Open();
            MainForm.MessageForm.SetMessage("开始转换主题分类\r\n");
            MainForm.SuccessedRecordCount = 0;
            MainForm.FailedRecordCount = 0;

            MainForm.RecordCount = Provider.Provider.GetInstance().GetTopicTypesRecordCount();

            MainForm.MessageForm.InitTotalProgressBar(1);
            MainForm.MessageForm.InitCurrentProgressBar(MainForm.RecordCount);

            //清理数据库
            dbhConvertTopicTypes.TruncateTable(string.Format("{0}topictypes", MainForm.cic.TargetDbTablePrefix));
            dbhConvertTopicTypes.SetIdentityInsertON(string.Format("{0}topictypes", MainForm.cic.TargetDbTablePrefix));


            //得到主题列表
            List<TopicTypes> topictypeList = Provider.Provider.GetInstance().GetTopicTypeList();
            foreach (TopicTypes objTopicType in topictypeList)
            {
                #region sql语句
                string sqlTopicType = string.Format(@"INSERT INTO {0}topictypes
(
typeid, 
displayorder,
name, 
description
)
VALUES 
(
@typeid, 
@displayorder,
@name, 
@description
)", MainForm.cic.TargetDbTablePrefix);
                #endregion
                //清理上次执行的参数
                dbhConvertTopicTypes.ParametersClear();
                #region dnt_topictypes表参数
                dbhConvertTopicTypes.ParameterAdd("@typeid", objTopicType.typeid, DbType.Int32, 4);
                dbhConvertTopicTypes.ParameterAdd("@displayorder", objTopicType.displayorder, DbType.Int32, 4);
                dbhConvertTopicTypes.ParameterAdd("@name", objTopicType.name, DbType.String, 30);
                dbhConvertTopicTypes.ParameterAdd("@description", objTopicType.description, DbType.String, 500);
                #endregion

                try
                {
                    dbhConvertTopicTypes.ExecuteNonQuery(sqlTopicType);//插入dnt_topictypes表
                    MainForm.SuccessedRecordCount++;
                }
                catch (Exception ex)
                {
                    MainForm.MessageForm.SetMessage(string.Format("错误:{0}。typeid={1}\r\n", ex.Message, objTopicType.typeid));
                    MainForm.FailedRecordCount++;
                }
                MainForm.MessageForm.CurrentProgressBarNumAdd();
            }
            //一次分页完毕
            MainForm.MessageForm.TotalProgressBarNumAdd();


            dbhConvertTopicTypes.SetIdentityInsertOFF(string.Format("{0}topictypes", MainForm.cic.TargetDbTablePrefix));
            dbhConvertTopicTypes.Dispose();
            MainForm.RecordCount = -1;
            MainForm.MessageForm.SetMessage(string.Format("完成转换主题分类。成功{0}，失败{1}\r\n", MainForm.SuccessedRecordCount, MainForm.FailedRecordCount));
        }
        /// <summary>
        /// 转换主题
        /// </summary>
        public static void ConvertTopics()
        {
            DBHelper dbh = MainForm.GetTargetDBH();
            dbh.Open();
            MainForm.MessageForm.SetMessage("开始转换主题\r\n");
            MainForm.SuccessedRecordCount = 0;
            MainForm.FailedRecordCount = 0;



            MainForm.RecordCount = Provider.Provider.GetInstance().GetTopicsRecordCount();
            if (MainForm.RecordCount % MainForm.PageSize != 0)
            {
                MainForm.PageCount = MainForm.RecordCount / MainForm.PageSize + 1;
            }
            else
            {
                MainForm.PageCount = MainForm.RecordCount / MainForm.PageSize;
            }

            MainForm.MessageForm.InitTotalProgressBar(MainForm.PageCount);
            MainForm.MessageForm.InitCurrentProgressBar(MainForm.RecordCount);

            //清理数据库
            dbh.ExecuteNonQuery(string.Format("TRUNCATE TABLE {0}topics", MainForm.cic.TargetDbTablePrefix));
            dbh.SetIdentityInsertON(string.Format("{0}topics", MainForm.cic.TargetDbTablePrefix));
            #region sql语句
            string sqlTopic = string.Format(@"INSERT INTO {0}topics
(
tid,
fid,
iconid,
typeid,
readperm,
price,
poster,
posterid,
title,
postdatetime,
lastpost,
lastpostid,
lastposter,
lastposterid,
views,
replies,
displayorder,
highlight,
digest,
rate,
hide,
attachment,
moderated,
closed,
magic,
identify,
special
)
VALUES 
(
@tid,
@fid,
@iconid,
@typeid,
@readperm,
@price,
@poster,
@posterid,
@title,
@postdatetime,
@lastpost,
@lastpostid,
@lastposter,
@lastposterid,
@views,
@replies,
@displayorder,
@highlight,
@digest,
@rate,
@hide,
@attachment,
@moderated,
@closed,
@magic,
@identify,
@special
)", MainForm.cic.TargetDbTablePrefix);
            #endregion
            for (int pagei = 1; pagei <= MainForm.PageCount; pagei++)
            {
                //分段得到主题列表
                List<Topics> topicList = Provider.Provider.GetInstance().GetTopicList(pagei);
                foreach (Topics objTopic in topicList)
                {
                    try
                    {
                        //清理上次执行的参数
                        dbh.ParametersClear();
                        #region dnt_topics表参数
                        dbh.ParameterAdd("@tid ", objTopic.tid, DbType.Int32, 4);
                        dbh.ParameterAdd("@fid ", objTopic.fid, DbType.Int16, 2);
                        dbh.ParameterAdd("@iconid ", objTopic.iconid, DbType.Byte, 1);
                        dbh.ParameterAdd("@typeid ", objTopic.typeid, DbType.Int32, 1);
                        dbh.ParameterAdd("@readperm ", objTopic.readperm, DbType.Int32, 4);
                        dbh.ParameterAdd("@price ", objTopic.price, DbType.Int16, 2);
                        dbh.ParameterAdd("@poster ", objTopic.poster, DbType.String, 20);
                        dbh.ParameterAdd("@posterid ", objTopic.posterid, DbType.Int32, 4);
                        dbh.ParameterAdd("@title ", objTopic.title, DbType.String, 60);
                        dbh.ParameterAdd("@postdatetime", objTopic.postdatetime, DbType.DateTime, 8);
                        dbh.ParameterAdd("@lastpost ", objTopic.lastpost, DbType.DateTime, 8);
                        dbh.ParameterAdd("@lastpostid ", objTopic.lastpostid, DbType.Int32, 4);
                        dbh.ParameterAdd("@lastposter ", objTopic.lastposter, DbType.String, 20);
                        dbh.ParameterAdd("@lastposterid", objTopic.lastposterid, DbType.Int32, 4);
                        dbh.ParameterAdd("@views ", objTopic.views, DbType.Int32, 4);
                        dbh.ParameterAdd("@replies ", objTopic.replies, DbType.Int32, 4);
                        dbh.ParameterAdd("@displayorder", objTopic.displayorder, DbType.Int32, 4);
                        dbh.ParameterAdd("@highlight ", objTopic.highlight, DbType.String, 500);
                        dbh.ParameterAdd("@digest ", objTopic.digest, DbType.Int16, 2);
                        dbh.ParameterAdd("@rate ", objTopic.rate, DbType.Int16, 2);
                        dbh.ParameterAdd("@hide ", objTopic.hide, DbType.Int32, 4);
                        dbh.ParameterAdd("@poll ", objTopic.poll, DbType.Int32, 4);
                        dbh.ParameterAdd("@attachment ", objTopic.attachment, DbType.Int32, 4);
                        dbh.ParameterAdd("@moderated ", objTopic.moderated, DbType.Int16, 2);
                        dbh.ParameterAdd("@closed ", objTopic.closed, DbType.Int32, 4);
                        dbh.ParameterAdd("@magic ", objTopic.magic, DbType.Int32, 4);
                        dbh.ParameterAdd("@identify", objTopic.identify, DbType.Int32, 4);
                        dbh.ParameterAdd("@special ", objTopic.special, DbType.Byte, 1);
                        #endregion
                        dbh.ExecuteNonQuery(sqlTopic);//插入dnt_topics表
                        MainForm.SuccessedRecordCount++;
                    }
                    catch (Exception ex)
                    {
                        MainForm.MessageForm.SetMessage(string.Format("错误:{0}.tid={1}\r\n", ex.Message, objTopic.tid));
                        MainForm.FailedRecordCount++;
                    }

                    MainForm.MessageForm.CurrentProgressBarNumAdd();
                }
                MainForm.MessageForm.TotalProgressBarNumAdd();
            }


            dbh.SetIdentityInsertOFF(string.Format("{0}topics", MainForm.cic.TargetDbTablePrefix));
            dbh.Dispose();
            MainForm.RecordCount = -1;
            MainForm.MessageForm.SetMessage(string.Format("完成转换主题。成功{0}，失败{1}\r\n", MainForm.SuccessedRecordCount, MainForm.FailedRecordCount));
        }

        /// <summary>
        /// 转换帖子
        /// </summary>
        public static void ConvertPosts()
        {
            DBHelper dbh = MainForm.GetTargetDBH();
            dbh.Open();
            MainForm.MessageForm.SetMessage(string.Format("创建帖子分表..."));
            CreatePostesTables();
            MainForm.MessageForm.SetMessage(string.Format("完成。\r\n"));

            MainForm.MessageForm.SetMessage("开始转换帖子\r\n");
            MainForm.SuccessedRecordCount = 0;
            MainForm.FailedRecordCount = 0;

            MainForm.RecordCount = Provider.Provider.GetInstance().GetPostsRecordCount();
            if (MainForm.RecordCount % MainForm.PageSize != 0)
            {
                MainForm.PageCount = MainForm.RecordCount / MainForm.PageSize + 1;
            }
            else
            {
                MainForm.PageCount = MainForm.RecordCount / MainForm.PageSize;
            }

            MainForm.MessageForm.InitTotalProgressBar(MainForm.PageCount);
            MainForm.MessageForm.InitCurrentProgressBar(MainForm.RecordCount);

            //清理数据库
            dbh.TruncateTable(string.Format("{0}Posts1", MainForm.cic.TargetDbTablePrefix));

            for (int pagei = 1; pagei <= MainForm.PageCount; pagei++)
            {
                //分段得到主题列表
                List<Posts> postList = Provider.Provider.GetInstance().GetPostList(pagei);
                foreach (Posts objPost in postList)
                {
                    //查找分表,构造sql语句
                    #region sql语句
                    string sqlPost = string.Format(@"INSERT INTO {0}posts{1}
(
pid, 
fid, 
tid, 
parentid, 
layer, 
poster, 
posterid, 
title, 
postdatetime, 
message, 
ip, 
lastedit,
invisible, 
usesig, 
htmlon, 
smileyoff, 
parseurloff, 
bbcodeoff, 
attachment, 
rate, 
ratetimes
)
VALUES 
(
@pid, 
@fid, 
@tid, 
@parentid, 
@layer, 
@poster, 
@posterid, 
@title, 
@postdatetime,
@message, 
@ip, 
@lastedit,
@invisible, 
@usesig, 
@htmlon, 
@smileyoff, 
@parseurloff, 
@bbcodeoff, 
@attachment, 
@rate, 
@ratetimes
)", MainForm.cic.TargetDbTablePrefix, NConvert.Utils.Posts.GetPostTableId(objPost.tid));
                    #endregion
                    //清理上次执行的参数
                    dbh.ParametersClear();
                    #region dnt_posts表参数
                    //if (objPost.pid == 1036)
                    //{
                    //    MainForm.MessageForm.SetMessage(string.Format("第一次遇到1036:pid={0}.tid={1},pagei={2}\r\n", objPost.pid, objPost.tid, pagei));
                    //}
                    dbh.ParameterAdd("@pid", objPost.pid, DbType.Int32, 4);
                    dbh.ParameterAdd("@fid", objPost.fid, DbType.Int32, 4);
                    dbh.ParameterAdd("@tid", objPost.tid, DbType.Int32, 4);
                    dbh.ParameterAdd("@parentid ", objPost.parentid, DbType.Int32, 4);
                    dbh.ParameterAdd("@layer", objPost.layer, DbType.Int32, 4);
                    dbh.ParameterAdd("@poster", objPost.poster, DbType.String, 20);
                    dbh.ParameterAdd("@posterid", objPost.posterid, DbType.Int32, 4);
                    dbh.ParameterAdd("@title", objPost.title, DbType.String, 60);
                    dbh.ParameterAdd("@postdatetime", objPost.postdatetime, DbType.DateTime, 4);
                    dbh.ParameterAdd("@message", objPost.message, DbType.String, 1073741823);
                    dbh.ParameterAdd("@ip", objPost.ip, DbType.String, 15);
                    dbh.ParameterAdd("@lastedit", objPost.lastedit, DbType.String, 50);
                    dbh.ParameterAdd("@invisible ", objPost.invisible, DbType.Int32, 4);
                    dbh.ParameterAdd("@usesig", objPost.usesig, DbType.Int32, 4);
                    dbh.ParameterAdd("@htmlon", objPost.htmlon, DbType.Int32, 4);
                    dbh.ParameterAdd("@smileyoff", objPost.smileyoff, DbType.Int32, 4);
                    dbh.ParameterAdd("@parseurloff ", objPost.parseurloff, DbType.Int32, 4);
                    dbh.ParameterAdd("@bbcodeoff", objPost.bbcodeoff, DbType.Int32, 4);
                    dbh.ParameterAdd("@attachment", objPost.attachment, DbType.Int32, 4);
                    dbh.ParameterAdd("@rate", objPost.rate, DbType.Int32, 4);
                    dbh.ParameterAdd("@ratetimes", objPost.ratetimes, DbType.Int32, 4);
                    #endregion

                    try
                    {
                        dbh.ExecuteNonQuery(sqlPost);//插入dnt_topics表
                        MainForm.SuccessedRecordCount++;
                    }
                    catch (Exception ex)
                    {
                        MainForm.MessageForm.SetMessage(string.Format("错误:{0}.pid={1} tid={1}\r\n", ex.Message, objPost.pid, objPost.tid));
                        MainForm.FailedRecordCount++;
                    }
                    MainForm.MessageForm.CurrentProgressBarNumAdd();
                }
                //一次分页完毕
                MainForm.MessageForm.TotalProgressBarNumAdd();
            }
            dbh.Close();
            MainForm.RecordCount = -1;
            MainForm.MessageForm.SetMessage(string.Format("完成转换帖子。成功{0}，失败{1}\r\n", MainForm.SuccessedRecordCount, MainForm.FailedRecordCount));
        }

        //TODO dnt_tablelist       

        /// <summary>
        /// 转换附件
        /// </summary>
        public static void ConvertAttachments()
        {
            DBHelper dbh = MainForm.GetTargetDBH();
            dbh.Open();
            MainForm.MessageForm.SetMessage("开始转换附件\r\n");
            MainForm.SuccessedRecordCount = 0;
            MainForm.FailedRecordCount = 0;

            MainForm.RecordCount = Provider.Provider.GetInstance().GetAttachmentsRecordCount();
            if (MainForm.RecordCount % MainForm.PageSize != 0)
            {
                MainForm.PageCount = MainForm.RecordCount / MainForm.PageSize + 1;
            }
            else
            {
                MainForm.PageCount = MainForm.RecordCount / MainForm.PageSize;
            }

            MainForm.MessageForm.InitTotalProgressBar(MainForm.PageCount);
            MainForm.MessageForm.InitCurrentProgressBar(MainForm.RecordCount);

            //清理数据库
            dbh.TruncateTable(string.Format("{0}attachments", MainForm.cic.TargetDbTablePrefix));
            dbh.SetIdentityInsertON(string.Format("{0}attachments", MainForm.cic.TargetDbTablePrefix));

            for (int pagei = 1; pagei <= MainForm.PageCount; pagei++)
            {
                //分段得到主题列表
                List<Attachments> attachmentList = Provider.Provider.GetInstance().GetAttachmentList(pagei);
                foreach (Attachments objAttachment in attachmentList)
                {
                    #region sql语句
                    string sqlAttachment = string.Format(@"INSERT INTO {0}attachments
(
aid,
uid, 
tid, 
pid, 
postdatetime, 
readperm, 
filename, 
description, 
filetype, 
filesize,
attachment, 
downloads
)
VALUES 
(
@aid,
@uid, 
@tid, 
@pid, 
@postdatetime, 
@readperm, 
@filename, 
@description, 
@filetype, 
@filesize,
@attachment, 
@downloads
)", MainForm.cic.TargetDbTablePrefix);
                    #endregion
                    //清理上次执行的参数
                    dbh.ParametersClear();
                    #region dnt_attachment表参数
                    dbh.ParameterAdd("@aid", objAttachment.aid, DbType.Int32, 4);
                    dbh.ParameterAdd("@uid", objAttachment.uid, DbType.Int32, 4);
                    dbh.ParameterAdd("@tid", objAttachment.tid, DbType.Int32, 4);
                    dbh.ParameterAdd("@pid", objAttachment.pid, DbType.Int32, 4);
                    dbh.ParameterAdd("@postdatetime", objAttachment.postdatetime, DbType.DateTime, 8);
                    dbh.ParameterAdd("@readperm", objAttachment.readperm, DbType.Int32, 4);
                    dbh.ParameterAdd("@filename", objAttachment.filename, DbType.String, 100);
                    dbh.ParameterAdd("@description", objAttachment.description, DbType.String, 100);
                    dbh.ParameterAdd("@filetype", objAttachment.filetype, DbType.String, 50);
                    dbh.ParameterAdd("@filesize", objAttachment.filesize, DbType.Int32, 4);
                    dbh.ParameterAdd("@attachment", objAttachment.attachment, DbType.String, 100);
                    dbh.ParameterAdd("@downloads", objAttachment.downloads, DbType.Int32, 4);
                    #endregion

                    try
                    {
                        dbh.ExecuteNonQuery(sqlAttachment);//插入dnt_topics表
                        MainForm.SuccessedRecordCount++;
                    }
                    catch (Exception ex)
                    {
                        MainForm.MessageForm.SetMessage(string.Format("错误:{0}。aid={1} pid={2} tid={3}\r\n", ex.Message, objAttachment.aid, objAttachment.pid, objAttachment.tid));
                        MainForm.FailedRecordCount++;
                    }
                    MainForm.MessageForm.CurrentProgressBarNumAdd();
                }
                //一次分页完毕
                MainForm.MessageForm.TotalProgressBarNumAdd();
            }
            dbh.SetIdentityInsertOFF(string.Format("{0}attachments", MainForm.cic.TargetDbTablePrefix));
            dbh.Close();
            MainForm.RecordCount = -1;
            MainForm.MessageForm.SetMessage(string.Format("完成转换附件。成功{0}，失败{1}\r\n", MainForm.SuccessedRecordCount, MainForm.FailedRecordCount));
        }

        /// <summary>
        /// 转换投票主体
        /// </summary>
        public static void ConvertPolls()
        {
            DBHelper dbh = MainForm.GetTargetDBH();
            dbh.Open();
            MainForm.MessageForm.SetMessage("开始转换投票\r\n");
            MainForm.SuccessedRecordCount = 0;
            MainForm.FailedRecordCount = 0;

            MainForm.RecordCount = Provider.Provider.GetInstance().GetPollsRecordCount();
            if (MainForm.RecordCount % MainForm.PageSize != 0)
            {
                MainForm.PageCount = MainForm.RecordCount / MainForm.PageSize + 1;
            }
            else
            {
                MainForm.PageCount = MainForm.RecordCount / MainForm.PageSize;
            }

            MainForm.MessageForm.InitTotalProgressBar(MainForm.PageCount);
            MainForm.MessageForm.InitCurrentProgressBar(MainForm.RecordCount);

            //清理数据库
            dbh.TruncateTable(string.Format("{0}Polls", MainForm.cic.TargetDbTablePrefix));
            dbh.SetIdentityInsertON(string.Format("{0}Polls", MainForm.cic.TargetDbTablePrefix));

            for (int pagei = 1; pagei <= MainForm.PageCount; pagei++)
            {
                //分段得到主题列表
                List<Polls> pollList = Provider.Provider.GetInstance().GetPollList(pagei);
                foreach (Polls objPoll in pollList)
                {
                    #region sql语句
                    string sqlPoll = string.Format(@"INSERT INTO {0}polls
(
pollid
,tid
,displayorder
,multiple
,visible
,maxchoices
,expiration
,uid
,voternames
)
VALUES 
(
@pollid
,@tid
,@displayorder
,@multiple
,@visible
,@maxchoices
,@expiration
,@uid
,@voternames
)", MainForm.cic.TargetDbTablePrefix);
                    #endregion
                    //清理上次执行的参数
                    dbh.ParametersClear();
                    #region dnt_posts表参数
                    dbh.ParameterAdd("@pollid", objPoll.Pollid, DbType.Int32, 4);
                    dbh.ParameterAdd("@tid", objPoll.Tid, DbType.Int32, 4);
                    dbh.ParameterAdd("@displayorder", objPoll.Displayorder, DbType.Int32, 4);
                    dbh.ParameterAdd("@multiple", objPoll.Multiple, DbType.Int32, 4);
                    dbh.ParameterAdd("@visible", objPoll.Visible, DbType.Int32, 4);
                    dbh.ParameterAdd("@maxchoices", objPoll.Maxchoices, DbType.Int32, 4);
                    dbh.ParameterAdd("@expiration", objPoll.Expiration, DbType.DateTime, 8);
                    dbh.ParameterAdd("@uid", objPoll.Uid, DbType.Int32, 4);
                    dbh.ParameterAdd("@voternames", objPoll.Voternames, DbType.String, 1073741823);
                    #endregion

                    try
                    {
                        dbh.ExecuteNonQuery(sqlPoll);//插入dnt_polls表
                        MainForm.SuccessedRecordCount++;
                    }
                    catch (Exception ex)
                    {
                        MainForm.MessageForm.SetMessage(string.Format("错误:{0}.Pollid={1}\r\n", ex.Message, objPoll.Pollid));
                        MainForm.FailedRecordCount++;
                    }
                    MainForm.MessageForm.CurrentProgressBarNumAdd();
                }
                //一次分页完毕
                MainForm.MessageForm.TotalProgressBarNumAdd();
            }
            dbh.SetIdentityInsertOFF(string.Format("{0}Polls", MainForm.cic.TargetDbTablePrefix));
            dbh.Close();
            MainForm.RecordCount = -1;
            MainForm.MessageForm.SetMessage(string.Format("完成转换投票。成功{0}，失败{1}\r\n", MainForm.SuccessedRecordCount, MainForm.FailedRecordCount));
        }

        /// <summary>
        /// 转换投票项目
        /// </summary>
        public static void ConvertPollOptions()
        {
            DBHelper dbh = MainForm.GetTargetDBH();
            dbh.Open();
            MainForm.MessageForm.SetMessage("开始转换投票\r\n");
            MainForm.SuccessedRecordCount = 0;
            MainForm.FailedRecordCount = 0;

            MainForm.RecordCount = Provider.Provider.GetInstance().GetPollOptionsRecordCount();
            if (MainForm.RecordCount % MainForm.PageSize != 0)
            {
                MainForm.PageCount = MainForm.RecordCount / MainForm.PageSize + 1;
            }
            else
            {
                MainForm.PageCount = MainForm.RecordCount / MainForm.PageSize;
            }

            MainForm.MessageForm.InitTotalProgressBar(MainForm.PageCount);
            MainForm.MessageForm.InitCurrentProgressBar(MainForm.RecordCount);

            //清理数据库
            dbh.TruncateTable(string.Format("{0}polloptions", MainForm.cic.TargetDbTablePrefix));
            dbh.SetIdentityInsertON(string.Format("{0}polloptions", MainForm.cic.TargetDbTablePrefix));

            for (int pagei = 1; pagei <= MainForm.PageCount; pagei++)
            {
                //分段得到主题列表
                List<PollOptionInfo> polloptionList = Provider.Provider.GetInstance().GetPollOptionsList(pagei);
                foreach (PollOptionInfo objPoll in polloptionList)
                {
                    #region sql语句
                    string sqlPoll = string.Format(@"INSERT INTO {0}polloptions
(
 polloptionid
,tid
,pollid
,votes
,displayorder
,polloption
,voternames
)
VALUES 
(
@polloptionid
,@tid
,@pollid
,@votes
,@displayorder
,@polloption
,@voternames
)", MainForm.cic.TargetDbTablePrefix);
                    #endregion
                    //清理上次执行的参数
                    dbh.ParametersClear();
                    #region dnt_posts表参数
                    dbh.ParameterAdd("@polloptionid", objPoll.Polloptionid, DbType.Int32, 4);
                    dbh.ParameterAdd("@tid", objPoll.Tid, DbType.Int32, 4);
                    dbh.ParameterAdd("@pollid", objPoll.Pollid, DbType.Int32, 4);
                    dbh.ParameterAdd("@votes", objPoll.Votes, DbType.Int32, 4);
                    dbh.ParameterAdd("@displayorder", objPoll.Displayorder, DbType.Int32, 4);
                    dbh.ParameterAdd("@polloption", objPoll.Polloption, DbType.String, 80);
                    dbh.ParameterAdd("@voternames", objPoll.Voternames, DbType.String, 1073741823);
                    #endregion

                    try
                    {
                        dbh.ExecuteNonQuery(sqlPoll);//插入dnt_polls表
                        MainForm.SuccessedRecordCount++;
                    }
                    catch (Exception ex)
                    {
                        MainForm.MessageForm.SetMessage(string.Format("错误:{0}.Polloptionid={1}\r\n", ex.Message, objPoll.Polloptionid));
                        MainForm.FailedRecordCount++;
                    }
                    MainForm.MessageForm.CurrentProgressBarNumAdd();
                }
                //一次分页完毕
                MainForm.MessageForm.TotalProgressBarNumAdd();
            }
            dbh.SetIdentityInsertOFF(string.Format("{0}polloptions", MainForm.cic.TargetDbTablePrefix));
            dbh.Close();
            MainForm.RecordCount = -1;
            MainForm.MessageForm.SetMessage(string.Format("完成转换投票项。成功{0}，失败{1}\r\n", MainForm.SuccessedRecordCount, MainForm.FailedRecordCount));
        }

        /// <summary>
        /// 整理投票记录
        /// </summary>
        public static void ConvertVoteRecords()
        {
            DBHelper dbh = MainForm.GetTargetDBH();
            dbh.Open();
            MainForm.MessageForm.SetMessage("开始转换投票人记录\r\n");
            MainForm.SuccessedRecordCount = 0;
            MainForm.FailedRecordCount = 0;

            MainForm.RecordCount = Provider.Provider.GetInstance().GetVotesRecordCount();
            if (MainForm.RecordCount % MainForm.PageSize != 0)
            {
                MainForm.PageCount = MainForm.RecordCount / MainForm.PageSize + 1;
            }
            else
            {
                MainForm.PageCount = MainForm.RecordCount / MainForm.PageSize;
            }

            MainForm.MessageForm.InitTotalProgressBar(MainForm.PageCount);
            MainForm.MessageForm.InitCurrentProgressBar(MainForm.RecordCount);

            //清理数据库
            //dbh.TruncateTable(string.Format("{0}polloptions", MainForm.cic.TargetDbTablePrefix));
            //dbh.SetIdentityInsertON(string.Format("{0}polloptions", MainForm.cic.TargetDbTablePrefix));

            for (int pagei = 1; pagei <= MainForm.PageCount; pagei++)
            {
                //分段得到主题列表
                List<VoteRecords> pollrecordList = Provider.Provider.GetInstance().GetVotesList(pagei);
                foreach (VoteRecords objPoll in pollrecordList)
                {
                    #region sql语句
                    string sqlPoll = string.Format(@"UPDATE {0}polls SET voternames=@voternames WHERE pollid=@pollid",
                        MainForm.cic.TargetDbTablePrefix);
                    #endregion
                    //清理上次执行的参数
                    dbh.ParametersClear();
                    #region dnt_posts表参数
                    StringBuilder sbTopic = new StringBuilder();
                    foreach (string name in objPoll.Voternames)
                    {
                        sbTopic.Append("\r\n" + name);
                    }
                    dbh.ParameterAdd("@voternames", sbTopic.ToString().Trim('\r').Trim('\n'), DbType.String, 1073741823);
                    dbh.ParameterAdd("@pollid", objPoll.Pollid, DbType.Int32, 4);
                    #endregion

                    try
                    {
                        dbh.ExecuteNonQuery(sqlPoll);//插入dnt_polls表

                        foreach (KeyValuePair<int, string> record in objPoll.Voterecords)
                        {
                            string sql2 = string.Format(
                                @"UPDATE {0}polloptions SET voternames=@voternames WHERE polloptionid=@polloptionid",
                                MainForm.cic.TargetDbTablePrefix
                                );

                            dbh.ParametersClear();
                            #region dnt_posts表参数
                            dbh.ParameterAdd("@voternames", record.Value, DbType.String, 1073741823);
                            dbh.ParameterAdd("@polloptionid", record.Key, DbType.Int32, 4);
                            #endregion

                            try
                            {
                                dbh.ExecuteNonQuery(sql2);//插入dnt_polls表
                            }
                            catch (Exception ex2)
                            {
                                throw new Exception(
                                    string.Format("polloptionid={0},{1}\r\n",
                                    record.Key.ToString(),
                                    ex2.Message)
                                    );
                            }
                        }


                        MainForm.SuccessedRecordCount++;
                    }
                    catch (Exception ex)
                    {
                        MainForm.MessageForm.SetMessage(
                            string.Format("错误:{0}.Pollid={1}\r\n", ex.Message, objPoll.Pollid)
                            );
                        MainForm.FailedRecordCount++;
                    }


                    MainForm.MessageForm.CurrentProgressBarNumAdd();
                }
                //一次分页完毕
                MainForm.MessageForm.TotalProgressBarNumAdd();
            }
            dbh.SetIdentityInsertOFF(string.Format("{0}polloptions", MainForm.cic.TargetDbTablePrefix));
            dbh.Close();
            MainForm.RecordCount = -1;
            MainForm.MessageForm.SetMessage(string.Format("完成转换投票项。成功{0}，失败{1}\r\n", MainForm.SuccessedRecordCount, MainForm.FailedRecordCount));
        }

        /// <summary>
        /// 转换短消息
        /// </summary>
        public static void ConvertPms()
        {
            DBHelper dbh = MainForm.GetTargetDBH();
            dbh.Open();
            MainForm.MessageForm.SetMessage("开始转换短消息\r\n");
            MainForm.SuccessedRecordCount = 0;
            MainForm.FailedRecordCount = 0;

            MainForm.RecordCount = Provider.Provider.GetInstance().GetPmsRecordCount();
            if (MainForm.RecordCount % MainForm.PageSize != 0)
            {
                MainForm.PageCount = MainForm.RecordCount / MainForm.PageSize + 1;
            }
            else
            {
                MainForm.PageCount = MainForm.RecordCount / MainForm.PageSize;
            }

            MainForm.MessageForm.InitTotalProgressBar(MainForm.PageCount);
            MainForm.MessageForm.InitCurrentProgressBar(MainForm.RecordCount);

            //清理数据库,开启自增插入
            dbh.TruncateTable(string.Format("{0}Pms", MainForm.cic.TargetDbTablePrefix));
            dbh.SetIdentityInsertON(string.Format("{0}Pms", MainForm.cic.TargetDbTablePrefix));

            for (int pagei = 1; pagei <= MainForm.PageCount; pagei++)
            {
                //分段得到主题列表
                List<Pms> pmList = Provider.Provider.GetInstance().GetPmList(pagei);
                foreach (Pms objPm in pmList)
                {
                    #region sql语句
                    string sqlPms = string.Format(@"INSERT INTO {0}pms
(
pmid,
msgfrom,
msgfromid,
msgto,
msgtoid,
folder,
new,
subject,
postdatetime,
message
)
VALUES 
(
@pmid,
@msgfrom,
@msgfromid,
@msgto,
@msgtoid,
@folder,
@new,
@subject,
@postdatetime,
@message
)", MainForm.cic.TargetDbTablePrefix);
                    #endregion
                    //清理上次执行的参数
                    dbh.ParametersClear();
                    #region dnt_pms表参数
                    dbh.ParameterAdd("@pmid", objPm.pmid, DbType.Int32, 4);
                    dbh.ParameterAdd("@msgfrom", objPm.msgfrom, DbType.String, 50);
                    dbh.ParameterAdd("@msgfromid", objPm.msgfromid, DbType.Int32, 4);
                    dbh.ParameterAdd("@msgto", objPm.msgto, DbType.String, 50);
                    dbh.ParameterAdd("@msgtoid", objPm.msgtoid, DbType.Int32, 4);
                    dbh.ParameterAdd("@folder", objPm.folder, DbType.Int16, 2);
                    dbh.ParameterAdd("@new", objPm.newmessage, DbType.Int32, 4);
                    dbh.ParameterAdd("@subject", objPm.subject, DbType.String, 60);
                    dbh.ParameterAdd("@postdatetime", objPm.postdatetime, DbType.DateTime, 8);
                    dbh.ParameterAdd("@message", objPm.message, DbType.String, 1073741823);
                    #endregion

                    try
                    {
                        dbh.ExecuteNonQuery(sqlPms);//插入dnt_topics表
                        MainForm.SuccessedRecordCount++;
                    }
                    catch (Exception ex)
                    {
                        MainForm.MessageForm.SetMessage(string.Format("错误:{0}。pmid={1}\r\n", ex.Message, objPm.pmid));
                        MainForm.FailedRecordCount++;
                    }
                    MainForm.MessageForm.CurrentProgressBarNumAdd();
                }
                //一次分页完毕
                MainForm.MessageForm.TotalProgressBarNumAdd();
            }

            dbh.SetIdentityInsertOFF(string.Format("{0}Pms", MainForm.cic.TargetDbTablePrefix));
            dbh.Dispose();
            MainForm.RecordCount = -1;
            MainForm.MessageForm.SetMessage(string.Format("完成转换短消息。成功{0}，失败{1}\r\n", MainForm.SuccessedRecordCount, MainForm.FailedRecordCount));
        }

        /// <summary>
        /// 转换友情链接
        /// </summary>
        public static void ConvertForumLinks()
        {
            DBHelper dbh = MainForm.GetTargetDBH();
            dbh.Open();
            MainForm.MessageForm.SetMessage("开始转换友情链接\r\n");
            MainForm.SuccessedRecordCount = 0;
            MainForm.FailedRecordCount = 0;

            MainForm.RecordCount = Provider.Provider.GetInstance().GetForumLinksRecordCount();

            MainForm.MessageForm.InitTotalProgressBar(1);
            MainForm.MessageForm.InitCurrentProgressBar(MainForm.RecordCount);

            //清理数据库
            dbh.TruncateTable(string.Format("{0}forumlinks", MainForm.cic.TargetDbTablePrefix));
            dbh.SetIdentityInsertON(string.Format("{0}forumlinks", MainForm.cic.TargetDbTablePrefix));


            //得到主题列表
            List<ForumLinks> forumlinkList = Provider.Provider.GetInstance().GetForumLinkList();
            foreach (ForumLinks objForumLink in forumlinkList)
            {
                #region sql语句
                string sqlForumLink = string.Format(@"INSERT INTO {0}forumlinks
(
id,
displayorder,
name,
url,
note,
logo
)
VALUES 
(
@id,
@displayorder,
@name,
@url,
@note,
@logo
)", MainForm.cic.TargetDbTablePrefix);
                #endregion
                //清理上次执行的参数
                dbh.ParametersClear();
                #region dnt_forumlinks表参数
                dbh.ParameterAdd("@id", objForumLink.id, DbType.Int16, 2);
                dbh.ParameterAdd("@displayorder", objForumLink.displayorder, DbType.Int32, 4);
                dbh.ParameterAdd("@name", objForumLink.name, DbType.String, 100);
                dbh.ParameterAdd("@url", objForumLink.url, DbType.String, 100);
                dbh.ParameterAdd("@note", objForumLink.note, DbType.String, 200);
                dbh.ParameterAdd("@logo", objForumLink.logo, DbType.String, 100);
                #endregion

                try
                {
                    dbh.ExecuteNonQuery(sqlForumLink);
                    MainForm.SuccessedRecordCount++;
                }
                catch (Exception ex)
                {
                    MainForm.MessageForm.SetMessage(string.Format("错误:{0}。id={1}\r\n", ex.Message, objForumLink.id));
                    MainForm.FailedRecordCount++;
                }
                MainForm.MessageForm.CurrentProgressBarNumAdd();
            }
            //一次分页完毕
            MainForm.MessageForm.TotalProgressBarNumAdd();


            dbh.SetIdentityInsertOFF(string.Format("{0}forumlinks", MainForm.cic.TargetDbTablePrefix));
            dbh.Dispose();
            MainForm.RecordCount = -1;
            MainForm.MessageForm.SetMessage(string.Format("完成转换友情链接。成功{0}，失败{1}\r\n", MainForm.SuccessedRecordCount, MainForm.FailedRecordCount));
        }












        /// <summary>
        /// 创建分表
        /// </summary>
        private static void CreatePostesTables()
        {
            int PostsTableSize = MainForm.PostsTableSize;//分表大小.
            int PostsTableCount = 1;//可以创建分表的个数
            //清空dnt_tablelist表 开启自增插入
            DBHelper ClearTablelistDBH = MainForm.GetTargetDBH();
            ClearTablelistDBH.Open();
            ClearTablelistDBH.TruncateTable(string.Format("{0}tablelist", MainForm.cic.TargetDbTablePrefix));
            ClearTablelistDBH.SetIdentityInsertON(string.Format("{0}tablelist", MainForm.cic.TargetDbTablePrefix));
            ClearTablelistDBH.Dispose();

            DBHelper RecordCountDBH = MainForm.GetTargetDBH();
            RecordCountDBH.Open();
            int RecordCount = Convert.ToInt32(RecordCountDBH.ExecuteScalar(string.Format("SELECT COUNT(tid) FROM {0}topics", MainForm.cic.TargetDbTablePrefix)));
            RecordCountDBH.Dispose();


            if (RecordCount % PostsTableSize != 0)
            {
                PostsTableCount = RecordCount / PostsTableSize + 1;
            }
            else
            {
                PostsTableCount = RecordCount / PostsTableSize;
            }
            DBHelper TableListDBH = MainForm.GetTargetDBH();
            TableListDBH.Open();
            for (int PostsTableId = 1; PostsTableId <= PostsTableCount; PostsTableId++)
            {
                string sql;
                DataSet ds = new DataSet();
                if (PostsTableId <= 1)
                {
                    sql = string.Format
                           ("SELECT TOP {1} tid FROM {0}topics ORDER BY tid", MainForm.cic.TargetDbTablePrefix, PostsTableSize);
                }
                else
                {
                    sql = string.Format
                           ("SELECT TOP {1} tid FROM {0}topics WHERE tid NOT IN (SELECT TOP {2} tid FROM {0}topics ORDER BY tid) ORDER BY tid", MainForm.cic.TargetDbTablePrefix, PostsTableSize, PostsTableSize * (PostsTableId - 1));
                }
                ds = TableListDBH.ExecuteDataSet(sql);
                TableListDBH.Close();
                int mintid = Convert.ToInt32(ds.Tables[0].Rows[0]["tid"]);
                int maxtid = Convert.ToInt32(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["tid"]);
                Utils.Posts.CreatePostsTable(PostsTableId, mintid, maxtid);
            }

            //关闭自增插入
            DBHelper indentyInsertDBH = MainForm.GetTargetDBH();
            indentyInsertDBH.Open();
            indentyInsertDBH.SetIdentityInsertOFF(string.Format("{0}tablelist", MainForm.cic.TargetDbTablePrefix));
            indentyInsertDBH.Dispose();

            //更新最后一个分表的maxtid=0
            DBHelper updateMaxtidDBH = MainForm.GetTargetDBH();
            updateMaxtidDBH.Open();
            updateMaxtidDBH.ExecuteNonQuery(string.Format("UPDATE {0}tablelist SET maxtid=0 WHERE id=(SELECT max(id) FROM {0}tablelist)", MainForm.cic.TargetDbTablePrefix));
            updateMaxtidDBH.Dispose();

            GetPostTableList();
        }

        /// <summary>
        /// 将分表信息赋值给全局变量
        /// </summary>
        public static void GetPostTableList()
        {
            //将分表信息赋值给全局变量
            MainForm.PostTableList = new List<PostTables>();
            DBHelper posttableDBH = MainForm.GetTargetDBH();
            posttableDBH.Open();
            IDataReader drTableList = posttableDBH.ExecuteReader(string.Format("SELECT id, createdatetime, description, mintid, maxtid FROM {0}tablelist", MainForm.cic.TargetDbTablePrefix));
            while (drTableList.Read())
            {
                PostTables objPostTable = new PostTables();
                objPostTable.id = Convert.ToInt32(drTableList["id"]);
                objPostTable.createdatetime = Convert.ToDateTime(drTableList["createdatetime"]);
                objPostTable.description = drTableList["description"].ToString();
                objPostTable.mintid = Convert.ToInt32(drTableList["mintid"]);
                objPostTable.maxtid = Convert.ToInt32(drTableList["maxtid"]);

                MainForm.PostTableList.Add(objPostTable);
            }

            drTableList.Close();
            drTableList.Dispose();
        }

        /// <summary>
        /// 整理主题的主题分类
        /// </summary>
        public static void ResetTopicsInfo()
        {
            //如果转换了主题分类,则要清除主题表中不存在的主题分类id
            if (MainForm.IsConvertTopicTypes)
            {
                DBHelper dbh = MainForm.GetTargetDBH();
                MainForm.MessageForm.SetMessage(string.Format("开始整理主题分类。\r\n"));
                int rows = 0;
                try
                {
                    dbh.Open();
                    rows = dbh.ExecuteNonQuery(string.Format("UPDATE {0}topics SET typeid=0 WHERE typeid IN (SELECT DISTINCT typeid FROM {0}topics WHERE typeid>0 AND typeid NOT IN (SELECT DISTINCT typeid FROM {0}topictypes))", MainForm.cic.TargetDbTablePrefix));
                }
                catch (Exception ex)
                {
                    MainForm.MessageForm.SetMessage(string.Format("错误:{0}\r\n", ex.Message));
                }
                finally
                {
                    dbh.Close();
                }
                MainForm.MessageForm.SetMessage(string.Format("完成整理主题分类。影响行数{0}\r\n", rows));
            }
        }

        /// <summary>
        /// 更新最后pid
        /// </summary>
        public static void UpdateLastPostid()
        {
            MainForm.MessageForm.SetMessage("更新Lastpostid。。。");
            GetPostTableList();
            int PostsTableCount = MainForm.PostTableList.Count;

            string sqlMaxpid = string.Format("select max(maxpid) from(select max(pid) AS maxpid from {0}posts1", MainForm.cic.TargetDbTablePrefix);
            for (int i = 2; i <= PostsTableCount; i++)
            {
                sqlMaxpid += string.Format(" UNION SELECT max(pid) AS maxpid FROM {0}posts{1}", MainForm.cic.TargetDbTablePrefix, i);
            }
            sqlMaxpid += ")a";
            DBHelper maxpidDBH = MainForm.GetTargetDBH();
            maxpidDBH.Open();
            int maxpid = Convert.ToInt32(maxpidDBH.ExecuteScalar(sqlMaxpid));
            maxpidDBH.Close();

            DBHelper updateLastPostidDBH = MainForm.GetTargetDBH();
            updateLastPostidDBH.Open();
            updateLastPostidDBH.TruncateTable(string.Format("{0}postid", MainForm.cic.TargetDbTablePrefix));
            updateLastPostidDBH.SetIdentityInsertON(string.Format("{0}postid", MainForm.cic.TargetDbTablePrefix));
            updateLastPostidDBH.ExecuteNonQuery(string.Format("INSERT INTO {0}postid(pid,postdatetime) VALUES({1},'{2}')", MainForm.cic.TargetDbTablePrefix, maxpid, DateTime.Now));
            updateLastPostidDBH.SetIdentityInsertOFF(string.Format("{0}postid", MainForm.cic.TargetDbTablePrefix));
            updateLastPostidDBH.Close();

            MainForm.MessageForm.SetMessage("完毕\r\n");
        }

        /// <summary>
        /// 整理帖子信息
        /// </summary>
        public static void UpdatePostsInfo()
        {
            MainForm.MessageForm.SetMessage("开始整理帖子\r\n");
            MainForm.SuccessedRecordCount = 0;
            MainForm.FailedRecordCount = 0;


            DBHelper topicCountDBH = MainForm.GetTargetDBH();
            topicCountDBH.Open();
            int topicCount = Convert.ToInt32(topicCountDBH.ExecuteScalar(string.Format("SELECT MAX(tid) FROM {0}topics", MainForm.cic.TargetDbTablePrefix)));
            topicCountDBH.Dispose();

            MainForm.MessageForm.InitTotalProgressBar(1);
            MainForm.MessageForm.InitCurrentProgressBar(topicCount);

            DBHelper topicinfoDBH = MainForm.GetTargetDBH();
            topicinfoDBH.Open();
            DBHelper postinfoDBH = MainForm.GetTargetDBH();
            postinfoDBH.Open();
            for (int i = 1; i <= topicCount; i++)
            {
                DataTable dtTopicInfo = topicinfoDBH.ExecuteDataSet(string.Format("SELECT tid,fid,title FROM {0}topics WHERE tid={1}", MainForm.cic.TargetDbTablePrefix, i)).Tables[0];
                if (dtTopicInfo.Rows.Count > 0)
                {
                    postinfoDBH.ParametersClear();
                    postinfoDBH.ParameterAdd("@fid", Convert.ToInt32(dtTopicInfo.Rows[0]["fid"]), DbType.Int32, 4);
                    postinfoDBH.ParameterAdd("@title", dtTopicInfo.Rows[0]["title"].ToString().Trim(), DbType.String, 60);
                    postinfoDBH.ParameterAdd("@tid", i, DbType.Int32, 4);
                    try
                    {
                        postinfoDBH.ExecuteNonQuery(string.Format("UPDATE {0}posts{1} SET fid=@fid,title=@title WHERE tid=@tid", MainForm.cic.TargetDbTablePrefix, Utils.Posts.GetPostTableId(i)));
                        MainForm.SuccessedRecordCount++;
                    }
                    catch (Exception ex)
                    {
                        MainForm.MessageForm.SetMessage(string.Format("错误:{0}。tid={1}\r\n", ex.Message, i));
                        MainForm.FailedRecordCount++;
                    }
                    MainForm.MessageForm.CurrentProgressBarNumAdd();
                }
            }
            topicinfoDBH.Dispose();
            postinfoDBH.Dispose();
            MainForm.MessageForm.TotalProgressBarNumAdd();
            MainForm.MessageForm.SetMessage(string.Format("完成整理帖子。成功{0}，失败{1}\r\n", MainForm.SuccessedRecordCount, MainForm.FailedRecordCount));
        }

        /// <summary>
        /// 整理主题回复数
        /// </summary>
        public static void ResetTopicReplyCount()
        {
            MainForm.MessageForm.SetMessage("开始更新回复数\r\n");
            MainForm.SuccessedRecordCount = 0;
            MainForm.FailedRecordCount = 0;


            DBHelper topicCountDBH = MainForm.GetTargetDBH();
            topicCountDBH.Open();
            int topicCount = Convert.ToInt32(topicCountDBH.ExecuteScalar(string.Format("SELECT MAX(tid) FROM {0}topics", MainForm.cic.TargetDbTablePrefix)));
            topicCountDBH.Dispose();

            MainForm.MessageForm.InitTotalProgressBar(1);
            MainForm.MessageForm.InitCurrentProgressBar(topicCount);

            DBHelper replycountDBH = MainForm.GetTargetDBH();
            DBHelper updatetopicDBH = MainForm.GetTargetDBH();
            replycountDBH.Open();
            updatetopicDBH.Open();
            for (int i = 1; i <= topicCount; i++)
            {
                int replycount = Convert.ToInt32(replycountDBH.ExecuteScalar(string.Format("SELECT COUNT(1) FROM {0}posts{1} WHERE tid={2}", MainForm.cic.TargetDbTablePrefix, Utils.Posts.GetPostTableId(i), i)));

                try
                {
                    updatetopicDBH.ExecuteNonQuery(string.Format("UPDATE {0}topics SET replies={1} WHERE tid={2}", MainForm.cic.TargetDbTablePrefix, replycount, i));
                    MainForm.SuccessedRecordCount++;
                }
                catch (Exception ex)
                {
                    MainForm.MessageForm.SetMessage(string.Format("错误:{0}。tid={1}\r\n", ex.Message, i));
                    MainForm.FailedRecordCount++;
                }
                MainForm.MessageForm.CurrentProgressBarNumAdd();
            }
            replycountDBH.Dispose();
            updatetopicDBH.Dispose();
            MainForm.MessageForm.TotalProgressBarNumAdd();
            MainForm.MessageForm.SetMessage(string.Format("完成更新回复数。成功{0}，失败{1}\r\n", MainForm.SuccessedRecordCount, MainForm.FailedRecordCount));
        }
    }
}
