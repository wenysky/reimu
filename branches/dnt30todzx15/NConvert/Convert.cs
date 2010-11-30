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
                //System.Windows.Forms.MessageBox.Show("数据库连接失败!\r\n");
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
                //ResetTopicsInfo();
            }

            if (MainForm.IsConvertAttachments)
                ConvertAttachments();
            if (MainForm.IsConvertPosts)
            {
                MainForm.extAttachList = new List<Attachments>();
                Yuwen.Tools.TinyData.DBHelper dbhExtattach = MainForm.GetTargetDBH();
                dbhExtattach.Open();
                object maxaid = dbhExtattach.ExecuteScalar(
                        string.Format("SELECT MAX(tid) FROM {0}forum_attachment", MainForm.cic.TargetDbTablePrefix)
                        );
                if (maxaid != DBNull.Value)
                {
                    MainForm.extAttachAidStartIndex = 1 + Convert.ToInt32(maxaid);
                }
                dbhExtattach.Close();
                dbhExtattach.Dispose();
                ConvertPosts();
                if (MainForm.extAttachList.Count > 0)
                {
                    ConvertextAttach();
                }
                //UpdateLastPostid();
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
            Yuwen.Tools.Data.DBHelper dbhConvertUserGroups = MainForm.GetTargetDBH_OldVer();//.GetTargetDBH();
            dbhConvertUserGroups.Open();
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
            dbhConvertUserGroups.ExecuteNonQuery(
                string.Format("TRUNCATE TABLE {0}common_usergroup", MainForm.cic.TargetDbTablePrefix)
                );
            dbhConvertUserGroups.ExecuteNonQuery(
                string.Format("TRUNCATE TABLE {0}common_usergroup_field", MainForm.cic.TargetDbTablePrefix)
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
`maxinviteday`
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
@maxinviteday
)", MainForm.cic.TargetDbTablePrefix);


            string sqlUserGroupField = string.Format(@"Insert into {0}common_usergroup_field
(
`groupid`,
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
                        dbhConvertUserGroups.ParametersClear();
                        #region dnt_users表参数
                        dbhConvertUserGroups.ParameterAdd("@groupid", objUser.groupid, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@radminid", objUser.radminid, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@type", objUser.type, DbType.String, 10);
                        dbhConvertUserGroups.ParameterAdd("@system", objUser.system, DbType.String, 255);
                        dbhConvertUserGroups.ParameterAdd("@grouptitle", objUser.grouptitle, DbType.String, 255);
                        dbhConvertUserGroups.ParameterAdd("@creditshigher", objUser.creditshigher, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@creditslower", objUser.creditslower, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@stars", objUser.stars, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@color", objUser.color, DbType.String, 255);
                        dbhConvertUserGroups.ParameterAdd("@icon", objUser.icon, DbType.String, 255);
                        dbhConvertUserGroups.ParameterAdd("@allowvisit", objUser.allowvisit, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowsendpm", objUser.allowsendpm, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowinvite", objUser.allowinvite, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowmailinvite", objUser.allowmailinvite, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@maxinvitenum", objUser.maxinvitenum, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@inviteprice", objUser.inviteprice, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@maxinviteday", objUser.maxinviteday, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@readaccess", objUser.readaccess, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowpost", objUser.allowpost, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowreply", objUser.allowreply, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowpostpoll", objUser.allowpostpoll, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowpostreward", objUser.allowpostreward, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowposttrade", objUser.allowposttrade, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowpostactivity", objUser.allowpostactivity, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowdirectpost", objUser.allowdirectpost, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowgetattach", objUser.allowgetattach, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowpostattach", objUser.allowpostattach, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowpostimage", objUser.allowpostimage, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowvote", objUser.allowvote, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowmultigroups", objUser.allowmultigroups, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowsearch", objUser.allowsearch, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowcstatus", objUser.allowcstatus, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowinvisible", objUser.allowinvisible, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowtransfer", objUser.allowtransfer, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowsetreadperm", objUser.allowsetreadperm, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowsetattachperm", objUser.allowsetattachperm, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowhidecode", objUser.allowhidecode, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowhtml", objUser.allowhtml, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowanonymous", objUser.allowanonymous, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowsigbbcode", objUser.allowsigbbcode, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowsigimgcode", objUser.allowsigimgcode, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowmagics", objUser.allowmagics, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@disableperiodctrl", objUser.disableperiodctrl, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@reasonpm", objUser.reasonpm, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@maxprice", objUser.maxprice, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@maxsigsize", objUser.maxsigsize, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@maxattachsize", objUser.maxattachsize, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@maxsizeperday", objUser.maxsizeperday, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@maxpostsperhour", objUser.maxpostsperhour, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@attachextensions", objUser.attachextensions, DbType.String, 100);
                        dbhConvertUserGroups.ParameterAdd("@raterange", objUser.raterange, DbType.String, 150);
                        dbhConvertUserGroups.ParameterAdd("@mintradeprice", objUser.mintradeprice, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@maxtradeprice", objUser.maxtradeprice, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@minrewardprice", objUser.minrewardprice, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@maxrewardprice", objUser.maxrewardprice, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@magicsdiscount", objUser.magicsdiscount, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@maxmagicsweight", objUser.maxmagicsweight, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowpostdebate", objUser.allowpostdebate, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@tradestick", objUser.tradestick, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@exempt", objUser.exempt, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@maxattachnum", objUser.maxattachnum, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowposturl", objUser.allowposturl, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowrecommend", objUser.allowrecommend, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowpostrushreply", objUser.allowpostrushreply, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@maxfriendnum", objUser.maxfriendnum, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@maxspacesize", objUser.maxspacesize, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowcomment", objUser.allowcomment, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowcommentarticle", objUser.allowcommentarticle, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@searchinterval", objUser.searchinterval, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@searchignore", objUser.searchignore, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowblog", objUser.allowblog, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowdoing", objUser.allowdoing, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowupload", objUser.allowupload, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowshare", objUser.allowshare, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowblogmod", objUser.allowblogmod, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowdoingmod", objUser.allowdoingmod, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowuploadmod", objUser.allowuploadmod, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowsharemod", objUser.allowsharemod, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowcss", objUser.allowcss, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowpoke", objUser.allowpoke, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowfriend", objUser.allowfriend, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowclick", objUser.allowclick, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowmagic", objUser.allowmagic, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowstat", objUser.allowstat, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowstatdata", objUser.allowstatdata, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@videophotoignore", objUser.videophotoignore, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowviewvideophoto", objUser.allowviewvideophoto, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowmyop", objUser.allowmyop, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@magicdiscount", objUser.magicdiscount, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@domainlength", objUser.domainlength, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@seccode", objUser.seccode, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@disablepostctrl", objUser.disablepostctrl, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowbuildgroup", objUser.allowbuildgroup, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowgroupdirectpost", objUser.allowgroupdirectpost, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowgroupposturl", objUser.allowgroupposturl, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@edittimelimit", objUser.edittimelimit, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowpostarticle", objUser.allowpostarticle, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowdownlocalimg", objUser.allowdownlocalimg, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowpostarticlemod", objUser.allowpostarticlemod, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowspacediyhtml", objUser.allowspacediyhtml, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowspacediybbcode", objUser.allowspacediybbcode, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowspacediyimgcode", objUser.allowspacediyimgcode, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowcommentpost", objUser.allowcommentpost, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@allowcommentitem", objUser.allowcommentitem, DbType.Int32, 4);
                        dbhConvertUserGroups.ParameterAdd("@ignorecensor", objUser.ignorecensor, DbType.Int32, 4);
                        #endregion
                        dbhConvertUserGroups.ExecuteNonQuery(sqlUser);//插入dnt_userGroups表
                        dbhConvertUserGroups.ExecuteNonQuery(sqlUserGroupField);
                        MainForm.SuccessedRecordCount++;
                    }
                    catch (Exception ex)
                    {
                        MainForm.MessageForm.SetMessage(string.Format("错误:{0}.groupid={1}\r\n", ex.Message, objUser.groupid));
                        MainForm.FailedRecordCount++;
                    }
                    MainForm.MessageForm.CurrentProgressBarNumAdd();
                }
                MainForm.MessageForm.TotalProgressBarNumAdd();
            }

            //dbh.ExecuteNonQuery(string.Format("SET IDENTITY_INSERT {0}users OFF", MainForm.cic.TargetDbTablePrefix));

            //dbhConvertUsers.SetIdentityInsertOFF(string.Format("{0}users", MainForm.cic.TargetDbTablePrefix));
            //dbhConvertUsers.Dispose();//.Close();
            dbhConvertUserGroups.Dispose();
            MainForm.RecordCount = -1;
            MainForm.MessageForm.SetMessage(string.Format("完成转换用户。成功{0}，失败{1}\r\n", MainForm.SuccessedRecordCount, MainForm.FailedRecordCount));
        }

        /// <summary>
        /// 转换用户
        /// </summary>
        public static void ConvertUsers()
        {
            Yuwen.Tools.Data.DBHelper dbhConvertUsers = MainForm.GetTargetDBH_OldVer();
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
            dbhConvertUsers.TruncateTable(string.Format("{0}ucenter_members", MainForm.cic.TargetDbTablePrefix));
            dbhConvertUsers.TruncateTable(string.Format("{0}ucenter_memberfields", MainForm.cic.TargetDbTablePrefix));
            dbhConvertUsers.TruncateTable(string.Format("{0}common_member", MainForm.cic.TargetDbTablePrefix));
            dbhConvertUsers.TruncateTable(string.Format("{0}common_member_count", MainForm.cic.TargetDbTablePrefix));
            dbhConvertUsers.TruncateTable(string.Format("{0}common_member_field_forum", MainForm.cic.TargetDbTablePrefix));
            dbhConvertUsers.TruncateTable(string.Format("{0}common_member_profile", MainForm.cic.TargetDbTablePrefix));
            dbhConvertUsers.TruncateTable(string.Format("{0}common_member_status", MainForm.cic.TargetDbTablePrefix));



            #region sql语句
            string sqlUCUser = string.Format(@"INSERT INTO {0}ucenter_members (
uid,
username,
password,
email,
myid,
myidkey,
regip,
regdate,
lastloginip,
lastlogintime,
salt,
secques
)
VALUES (
@uid,
@username,
@ucpassword,
@email,
'',
'',
@regip,
@regdate,
@lastloginip,
@lastlogintime,
@salt,
''
)", MainForm.cic.TargetDbTablePrefix);

            string sqlUCUserfield = string.Format(@"INSERT INTO {0}ucenter_memberfields (
`uid`,
`blacklist` 
)
VALUES (
@uid,
'' 
)", MainForm.cic.TargetDbTablePrefix);

            string sqlMember = string.Format(@"INSERT INTO {0}common_member (
`uid` ,
`email` ,
`username` ,
`password` ,
`status` ,
`emailstatus` ,
`avatarstatus` ,
`videophotostatus` ,
`adminid` ,
`groupid` ,
`groupexpiry` ,
`extgroupids` ,
`regdate` ,
`credits` ,
`notifysound` ,
`timeoffset` ,
`newpm` ,
`newprompt` ,
`accessmasks` ,
`allowadmincp` 
)
VALUES (
@uid,
@email,
@username,
@password,
@status,
@emailstatus,
@avatarstatus,
@videophotostatus,
@adminid,
@groupid,
@groupexpiry,
@extgroupids,
@regdate,
@credits,
@notifysound,
@timeoffset,
@newpm,
@newprompt,
@accessmasks,
@allowadmincp
)", MainForm.cic.TargetDbTablePrefix);
            string sqlMembercount = string.Format(@"INSERT INTO {0}common_member_count (
`uid` ,
`extcredits1` ,
`extcredits2` ,
`extcredits3` ,
`extcredits4` ,
`extcredits5` ,
`extcredits6` ,
`extcredits7` ,
`extcredits8` ,
`friends` ,
`posts` ,
`threads` ,
`digestposts` ,
`doings` ,
`blogs` ,
`albums` ,
`sharings` ,
`attachsize` ,
`views` ,
`oltime` 
)
VALUES (
@uid,
@extcredits1,
@extcredits2,
@extcredits3,
@extcredits4,
@extcredits5,
@extcredits6,
@extcredits7,
@extcredits8,
@friends,
@posts,
@threads,
@digestposts,
@doings,
@blogs,
@albums,
@sharings,
@attachsize,
@views,
@oltime
)", MainForm.cic.TargetDbTablePrefix);


            string sqlMemberfieldforum = string.Format(@"INSERT INTO {0}common_member_field_forum (
`uid` ,
`publishfeed` ,
`customshow` ,
`customstatus` ,
`medals` ,
`sightml` ,
`groupterms` ,
`authstr` ,
`groups` ,
`attentiongroup` 
)
VALUES (
@uid,
@publishfeed,
@customshow,
@customstatus,
@medals,
@sightml,
@groupterms,
@authstr,
@groups,
@attentiongroup
)", MainForm.cic.TargetDbTablePrefix);
            string sqlMemberprofile = string.Format(@"INSERT INTO {0}common_member_profile (
`uid` ,
`realname` ,
`gender` ,
`birthyear` ,
`birthmonth` ,
`birthday` ,
`constellation` ,
`zodiac` ,
`telephone` ,
`mobile` ,
`idcardtype` ,
`idcard` ,
`address` ,
`zipcode` ,
`nationality` ,
`birthprovince` ,
`birthcity` ,
`resideprovince` ,
`residecity` ,
`residedist` ,
`residecommunity` ,
`residesuite` ,
`graduateschool` ,
`company` ,
`education` ,
`occupation` ,
`position` ,
`revenue` ,
`affectivestatus` ,
`lookingfor` ,
`bloodtype` ,
`height` ,
`weight` ,
`alipay` ,
`icq` ,
`qq` ,
`yahoo` ,
`msn` ,
`taobao` ,
`site` ,
`bio` ,
`interest` ,
`realmtiny3` ,
`field2` ,
`field3` ,
`field4` ,
`field5` ,
`field6` ,
`field7` ,
`field8` 
)
VALUES (
@uid,
@realname,
@gender,
@birthyear,
@birthmonth,
@birthday,
@constellation,
@zodiac,
@telephone,
@mobile,
@idcardtype,
@idcard,
@address,
@zipcode,
@nationality,
@birthprovince,
@birthcity,
@resideprovince,
@residecity,
@residedist,
@residecommunity,
@residesuite,
@graduateschool,
@company,
@education,
@occupation,
@position,
@revenue,
@affectivestatus,
@lookingfor,
@bloodtype,
@height,
@weight,
@alipay,
@icq,
@qq,
@yahoo,
@msn,
@taobao,
@site,
@bio,
@interest,
@field1,
@field2,
@field3,
@field4,
@field5,
@field6,
@field7,
@field8
)", MainForm.cic.TargetDbTablePrefix);
            string sqlMemberstatus = string.Format(@"INSERT INTO {0}common_member_status (
`uid` ,
`regip` ,
`lastip` ,
`lastvisit` ,
`lastactivity` ,
`lastpost` ,
`lastsendmail` ,
`notifications` ,
`myinvitations` ,
`pokes` ,
`pendingfriends` ,
`invisible` ,
`buyercredit` ,
`sellercredit` ,
`favtimes` ,
`sharetimes` 
)
VALUES (
@uid,
@regip,
@lastip,
@lastvisit,
@lastactivity,
@lastpost,
@lastsendmail,
@notifications,
@myinvitations,
@pokes,
@pendingfriends,
@invisible,
@buyercredit,
@sellercredit,
@favtimes,
@sharetimes
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
                        dbhConvertUsers.ParametersClear();
                        #region users参数
                        dbhConvertUsers.ParameterAdd("@uid", objUser.uid, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@lastloginip", 0, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@lastlogintime", 0, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@salt", objUser.salt, DbType.String, 6);
                        dbhConvertUsers.ParameterAdd("@email", objUser.email, DbType.String, 40);
                        dbhConvertUsers.ParameterAdd("@username", objUser.username, DbType.String, 15);
                        dbhConvertUsers.ParameterAdd("@password", objUser.password, DbType.String, 32);
                        dbhConvertUsers.ParameterAdd("@ucpassword", objUser.ucpassword, DbType.String, 32);
                        dbhConvertUsers.ParameterAdd("@status", objUser.status, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@emailstatus", objUser.emailstatus, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@avatarstatus", objUser.avatarstatus, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@videophotostatus", objUser.videophotostatus, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@adminid", objUser.adminid, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@groupid", objUser.groupid, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@groupexpiry", objUser.groupexpiry, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@extgroupids", objUser.extgroupids, DbType.String, 20);
                        dbhConvertUsers.ParameterAdd("@regdate", objUser.regdate, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@credits", objUser.credits, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@notifysound", objUser.notifysound, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@timeoffset", objUser.timeoffset, DbType.String, 4);
                        dbhConvertUsers.ParameterAdd("@newpm", objUser.newpm, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@newprompt", objUser.newprompt, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@accessmasks", objUser.accessmasks, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@allowadmincp", objUser.allowadmincp, DbType.Int32, 4);

                        dbhConvertUsers.ParameterAdd("@extcredits1", objUser.extcredits1, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@extcredits2", objUser.extcredits2, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@extcredits3", objUser.extcredits3, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@extcredits4", objUser.extcredits4, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@extcredits5", objUser.extcredits5, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@extcredits6", objUser.extcredits6, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@extcredits7", objUser.extcredits7, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@extcredits8", objUser.extcredits8, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@friends", objUser.friends, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@posts", objUser.posts, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@threads", objUser.threads, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@digestposts", objUser.digestposts, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@doings", objUser.doings, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@blogs", objUser.blogs, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@albums", objUser.albums, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@sharings", objUser.sharings, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@attachsize", objUser.attachsize, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@views", objUser.views, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@oltime", objUser.oltime, DbType.Int32, 4);

                        dbhConvertUsers.ParameterAdd("@publishfeed", objUser.publishfeed, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@customshow", objUser.customshow, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@customstatus", objUser.customstatus, DbType.String, 30);
                        dbhConvertUsers.ParameterAdd("@medals", objUser.medals, DbType.String, 5000);
                        dbhConvertUsers.ParameterAdd("@sightml", objUser.sightml, DbType.String, 5000);
                        dbhConvertUsers.ParameterAdd("@groupterms", objUser.groupterms, DbType.String, 5000);
                        dbhConvertUsers.ParameterAdd("@authstr", objUser.authstr, DbType.String, 20);
                        dbhConvertUsers.ParameterAdd("@groups", objUser.groups, DbType.String, 5000);
                        dbhConvertUsers.ParameterAdd("@attentiongroup", objUser.attentiongroup, DbType.String, 255);

                        dbhConvertUsers.ParameterAdd("@realname", objUser.realname, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@gender", objUser.gender, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@birthyear", objUser.birthyear, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@birthmonth", objUser.birthmonth, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@birthday", objUser.birthday, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@constellation", objUser.constellation, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@zodiac", objUser.zodiac, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@telephone", objUser.telephone, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@mobile", objUser.mobile, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@idcardtype", objUser.idcardtype, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@idcard", objUser.idcard, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@address", objUser.address, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@zipcode", objUser.zipcode, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@nationality", objUser.nationality, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@birthprovince", objUser.birthprovince, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@birthcity", objUser.birthcity, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@resideprovince", objUser.resideprovince, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@residecity", objUser.residecity, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@residedist", objUser.residedist, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@residecommunity", objUser.residecommunity, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@residesuite", objUser.residesuite, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@graduateschool", objUser.graduateschool, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@company", objUser.company, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@education", objUser.education, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@occupation", objUser.occupation, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@position", objUser.position, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@revenue", objUser.revenue, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@affectivestatus", objUser.affectivestatus, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@lookingfor", objUser.lookingfor, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@bloodtype", objUser.bloodtype, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@height", objUser.height, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@weight", objUser.weight, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@alipay", objUser.alipay, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@icq", objUser.icq, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@qq", objUser.qq, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@yahoo", objUser.yahoo, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@msn", objUser.msn, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@taobao", objUser.taobao, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@site", objUser.site, DbType.String, 255);
                        dbhConvertUsers.ParameterAdd("@bio", objUser.bio, DbType.String, 5000);
                        dbhConvertUsers.ParameterAdd("@interest", objUser.interest, DbType.String, 5000);
                        dbhConvertUsers.ParameterAdd("@field1", objUser.field1, DbType.String, 5000);
                        dbhConvertUsers.ParameterAdd("@field2", objUser.field2, DbType.String, 5000);
                        dbhConvertUsers.ParameterAdd("@field3", objUser.field3, DbType.String, 5000);
                        dbhConvertUsers.ParameterAdd("@field4", objUser.field4, DbType.String, 5000);
                        dbhConvertUsers.ParameterAdd("@field5", objUser.field5, DbType.String, 5000);
                        dbhConvertUsers.ParameterAdd("@field6", objUser.field6, DbType.String, 5000);
                        dbhConvertUsers.ParameterAdd("@field7", objUser.field7, DbType.String, 5000);
                        dbhConvertUsers.ParameterAdd("@field8", objUser.field8, DbType.String, 5000);

                        dbhConvertUsers.ParameterAdd("@regip", objUser.regip, DbType.String, 15);
                        dbhConvertUsers.ParameterAdd("@lastip", objUser.lastip, DbType.String, 15);
                        dbhConvertUsers.ParameterAdd("@lastvisit", objUser.lastvisit, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@lastactivity", objUser.lastactivity, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@lastpost", objUser.lastpost, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@lastsendmail", objUser.lastsendmail, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@notifications", objUser.notifications, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@myinvitations", objUser.myinvitations, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@pokes", objUser.pokes, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@pendingfriends", objUser.pendingfriends, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@invisible", objUser.invisible, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@buyercredit", objUser.buyercredit, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@sellercredit", objUser.sellercredit, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@favtimes", objUser.favtimes, DbType.Int32, 4);
                        dbhConvertUsers.ParameterAdd("@sharetimes", objUser.sharetimes, DbType.Int32, 4);
                        #endregion
                        dbhConvertUsers.ExecuteNonQuery(sqlUCUser);//插入dnt_users表
                        dbhConvertUsers.ExecuteNonQuery(sqlUCUserfield);//插入dnt_users表
                        dbhConvertUsers.ExecuteNonQuery(sqlMember);//插入dnt_users表
                        dbhConvertUsers.ExecuteNonQuery(sqlMembercount);//插入dnt_userfields表
                        dbhConvertUsers.ExecuteNonQuery(sqlMemberfieldforum);//插入dnt_userfields表
                        dbhConvertUsers.ExecuteNonQuery(sqlMemberprofile);//插入dnt_userfields表
                        dbhConvertUsers.ExecuteNonQuery(sqlMemberstatus);//插入dnt_userfields表
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

            //dbhConvertUsers.Close();
            dbhConvertUsers.Dispose();
            MainForm.RecordCount = -1;
            MainForm.MessageForm.SetMessage(string.Format("完成转换用户。成功{0}，失败{1}\r\n", MainForm.SuccessedRecordCount, MainForm.FailedRecordCount));
        }

        /// <summary>
        /// 转换论坛版块
        /// </summary>
        public static void ConvertForums()
        {
            Yuwen.Tools.Data.DBHelper dbhConvertForums = MainForm.GetTargetDBH_OldVer();
            dbhConvertForums.Open();
            MainForm.MessageForm.SetMessage("开始转换版块\r\n");
            MainForm.SuccessedRecordCount = 0;
            MainForm.FailedRecordCount = 0;

            MainForm.RecordCount = Provider.Provider.GetInstance().GetForumsRecordCount();

            MainForm.MessageForm.InitTotalProgressBar(1);
            MainForm.MessageForm.InitCurrentProgressBar(MainForm.RecordCount);

            //清理数据库
            dbhConvertForums.TruncateTable(string.Format("{0}forum_forum", MainForm.cic.TargetDbTablePrefix));
            dbhConvertForums.TruncateTable(string.Format("{0}forum_forumfield", MainForm.cic.TargetDbTablePrefix));


            #region sql语句
            string sqlForum = string.Format(@"INSERT INTO {0}forum_forum (
`fid` ,
`fup` ,
`type` ,
`name` ,
`status` ,
`displayorder` ,
`styleid` ,
`threads` ,
`posts` ,
`todayposts` ,
`lastpost` ,
`domain` ,
`allowsmilies` ,
`allowhtml` ,
`allowbbcode` ,
`allowimgcode` ,
`allowmediacode` ,
`allowanonymous` ,
`allowpostspecial` ,
`allowspecialonly` ,
`allowappend` ,
`alloweditrules` ,
`allowfeed` ,
`allowside` ,
`recyclebin` ,
`modnewposts` ,
`jammer` ,
`disablewatermark` ,
`inheritedmod` ,
`autoclose` ,
`forumcolumns` ,
`threadcaches` ,
`alloweditpost` ,
`simple` ,
`modworks` ,
`allowtag` ,
`allowglobalstick` ,
`level` ,
`commoncredits` ,
`archive` ,
`recommend` ,
`favtimes` ,
`sharetimes` 
)
VALUES (
@fid,
@fup,
@type,
@name,
@status,
@displayorder,
@styleid,
@threads,
@posts,
@todayposts,
@lastpost,
@domain,
@allowsmilies,
@allowhtml,
@allowbbcode,
@allowimgcode,
@allowmediacode,
@allowanonymous,
@allowpostspecial,
@allowspecialonly,
@allowappend,
@alloweditrules,
@allowfeed,
@allowside,
@recyclebin,
@modnewposts,
@jammer,
@disablewatermark,
@inheritedmod,
@autoclose,
@forumcolumns,
@threadcaches,
@alloweditpost,
@simple,
@modworks,
@allowtag,
@allowglobalstick,
@level,
@commoncredits,
@archive,
@recommend,
@favtimes,
@sharetimes
)", MainForm.cic.TargetDbTablePrefix);
            string sqlForumfields = string.Format(@"INSERT INTO {0}forum_forumfield (
`fid` ,
`description` ,
`password` ,
`icon` ,
`redirect` ,
`attachextensions` ,
`creditspolicy` ,
`formulaperm` ,
`moderators` ,
`rules` ,
`threadtypes` ,
`threadsorts` ,
`viewperm` ,
`postperm` ,
`replyperm` ,
`getattachperm` ,
`postattachperm` ,
`postimageperm` ,
`spviewperm` ,
`keywords` ,
`supe_pushsetting` ,
`modrecommend` ,
`threadplugin` ,
`extra` ,
`jointype` ,
`gviewperm` ,
`membernum` ,
`dateline` ,
`lastupdate` ,
`activity` ,
`founderuid` ,
`foundername` ,
`banner` ,
`groupnum` ,
`commentitem` ,
`hidemenu` 
)
VALUES (
@fid,
@description,
@password,
@icon,
@redirect,
@attachextensions,
@creditspolicy,
@formulaperm,
@moderators,
@rules,
@threadtypes,
@threadsorts,
@viewperm,
@postperm,
@replyperm,
@getattachperm,
@postattachperm,
@postimageperm,
@spviewperm,
@keywords,
@supe_pushsetting,
@modrecommend,
@threadplugin,
@extra,
@jointype,
@gviewperm,
@membernum,
@dateline,
@lastupdate,
@activity,
@founderuid,
@foundername,
@banner,
@groupnum,
@commentitem,
@hidemenu
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
                    dbhConvertForums.ParameterAdd("@fup", objForum.fup, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@type", objForum.type, DbType.String, 5);
                    dbhConvertForums.ParameterAdd("@name", objForum.name, DbType.String, 50);
                    dbhConvertForums.ParameterAdd("@status", objForum.status, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@displayorder", objForum.displayorder, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@styleid", objForum.styleid, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@threads", objForum.threads, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@posts", objForum.posts, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@todayposts", objForum.todayposts, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@lastpost", objForum.lastpost, DbType.String, 110);
                    dbhConvertForums.ParameterAdd("@domain", objForum.domain, DbType.String, 15);
                    dbhConvertForums.ParameterAdd("@allowsmilies", objForum.allowsmilies, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@allowhtml", objForum.allowhtml, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@allowbbcode", objForum.allowbbcode, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@allowimgcode", objForum.allowimgcode, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@allowmediacode", objForum.allowmediacode, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@allowanonymous", objForum.allowanonymous, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@allowpostspecial", objForum.allowpostspecial, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@allowspecialonly", objForum.allowspecialonly, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@allowappend", objForum.allowappend, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@alloweditrules", objForum.alloweditrules, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@allowfeed", objForum.allowfeed, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@allowside", objForum.allowside, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@recyclebin", objForum.recyclebin, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@modnewposts", objForum.modnewposts, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@jammer", objForum.jammer, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@disablewatermark", objForum.disablewatermark, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@inheritedmod", objForum.inheritedmod, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@autoclose", objForum.autoclose, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@forumcolumns", objForum.forumcolumns, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@threadcaches", objForum.threadcaches, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@alloweditpost", objForum.alloweditpost, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@simple", objForum.simple, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@modworks", objForum.modworks, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@allowtag", objForum.allowtag, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@allowglobalstick", objForum.allowglobalstick, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@level", objForum.level, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@commoncredits", objForum.commoncredits, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@archive", objForum.archive, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@recommend", objForum.recommend, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@favtimes", objForum.favtimes, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@sharetimes", objForum.sharetimes, DbType.Int32, 4);

                    dbhConvertForums.ParameterAdd("@description", objForum.description, DbType.String, 5000);
                    dbhConvertForums.ParameterAdd("@password", objForum.password, DbType.String, 12);
                    dbhConvertForums.ParameterAdd("@icon", objForum.icon, DbType.String, 255);
                    dbhConvertForums.ParameterAdd("@redirect", objForum.redirect, DbType.String, 255);
                    dbhConvertForums.ParameterAdd("@attachextensions", objForum.attachextensions, DbType.String, 255);
                    dbhConvertForums.ParameterAdd("@creditspolicy", objForum.creditspolicy, DbType.String, 5000);
                    dbhConvertForums.ParameterAdd("@formulaperm", objForum.formulaperm, DbType.String, 5000);
                    dbhConvertForums.ParameterAdd("@moderators", objForum.moderators, DbType.String, 5000);
                    dbhConvertForums.ParameterAdd("@rules", objForum.rules, DbType.String, 5000);
                    dbhConvertForums.ParameterAdd("@threadtypes", objForum.threadtypes, DbType.String, 5000);
                    dbhConvertForums.ParameterAdd("@threadsorts", objForum.threadsorts, DbType.String, 5000);
                    dbhConvertForums.ParameterAdd("@viewperm", objForum.viewperm, DbType.String, 5000);
                    dbhConvertForums.ParameterAdd("@postperm", objForum.postperm, DbType.String, 5000);
                    dbhConvertForums.ParameterAdd("@replyperm", objForum.replyperm, DbType.String, 5000);
                    dbhConvertForums.ParameterAdd("@getattachperm", objForum.getattachperm, DbType.String, 5000);
                    dbhConvertForums.ParameterAdd("@postattachperm", objForum.postattachperm, DbType.String, 5000);
                    dbhConvertForums.ParameterAdd("@postimageperm", objForum.postimageperm, DbType.String, 5000);
                    dbhConvertForums.ParameterAdd("@spviewperm", objForum.spviewperm, DbType.String, 5000);
                    dbhConvertForums.ParameterAdd("@keywords", objForum.keywords, DbType.String, 5000);
                    dbhConvertForums.ParameterAdd("@supe_pushsetting", objForum.supe_pushsetting, DbType.String, 5000);
                    dbhConvertForums.ParameterAdd("@modrecommend", objForum.modrecommend, DbType.String, 5000);
                    dbhConvertForums.ParameterAdd("@threadplugin", objForum.threadplugin, DbType.String, 5000);
                    dbhConvertForums.ParameterAdd("@extra", objForum.extra, DbType.String, 5000);
                    dbhConvertForums.ParameterAdd("@jointype", objForum.jointype, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@gviewperm", objForum.gviewperm, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@membernum", objForum.membernum, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@dateline", objForum.dateline, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@lastupdate", objForum.lastupdate, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@activity", objForum.activity, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@founderuid", objForum.founderuid, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@foundername", objForum.foundername, DbType.String, 255);
                    dbhConvertForums.ParameterAdd("@banner", objForum.banner, DbType.String, 255);
                    dbhConvertForums.ParameterAdd("@groupnum", objForum.groupnum, DbType.Int32, 4);
                    dbhConvertForums.ParameterAdd("@commentitem", objForum.commentitem, DbType.String, 5000);
                    dbhConvertForums.ParameterAdd("@hidemenu", objForum.hidemenu, DbType.Int32, 4);
                    #endregion
                    dbhConvertForums.ExecuteNonQuery(sqlForum);//插入dnt_forums表
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

            //dbhConvertForums.Close();
            dbhConvertForums.Dispose();
            MainForm.RecordCount = -1;
            MainForm.MessageForm.SetMessage(string.Format("完成转换版块。成功{0}，失败{1}\r\n", MainForm.SuccessedRecordCount, MainForm.FailedRecordCount));
            //整理版块
            //Utils.Forums.ResetForums();
            //MainForm.MessageForm.SetMessage("完成整理版块\r\n");
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
            Yuwen.Tools.Data.DBHelper dbh = MainForm.GetTargetDBH_OldVer();
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
            dbh.ExecuteNonQuery(string.Format("TRUNCATE TABLE {0}forum_thread", MainForm.cic.TargetDbTablePrefix));

            #region sql语句
            string sqlTopic = string.Format(@"INSERT INTO {0}forum_thread (
`tid` ,
`fid` ,
`posttableid` ,
`typeid` ,
`sortid` ,
`readperm` ,
`price` ,
`author` ,
`authorid` ,
`subject` ,
`dateline` ,
`lastpost` ,
`lastposter` ,
`views` ,
`replies` ,
`displayorder` ,
`highlight` ,
`digest` ,
`rate` ,
`special` ,
`attachment` ,
`moderated` ,
`closed` ,
`stickreply` ,
`recommends` ,
`recommend_add` ,
`recommend_sub` ,
`heats` ,
`status` ,
`isgroup` ,
`favtimes` ,
`sharetimes` ,
`stamp` ,
`icon` ,
`pushedaid` ,
recommend
)
VALUES (
@tid,
@fid,
@posttableid,
@typeid,
@sortid,
@readperm,
@price,
@author,
@authorid,
@subject,
@dateline,
@lastpost,
@lastposter,
@views,
@replies,
@displayorder,
@highlight,
@digest,
@rate,
@special,
@attachment,
@moderated,
@closed,
@stickreply,
@recommends,
@recommend_add,
@recommend_sub,
@heats,
@status,
@isgroup,
@favtimes,
@sharetimes,
@stamp,
@icon,
@pushedaid,
@recommend
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
                        dbh.ParameterAdd("@tid", objTopic.tid, DbType.Int32, 4);
                        dbh.ParameterAdd("@fid", objTopic.fid, DbType.Int32, 4);
                        dbh.ParameterAdd("@posttableid", objTopic.posttableid, DbType.Int32, 4);
                        dbh.ParameterAdd("@typeid", objTopic.typeid, DbType.Int32, 4);
                        dbh.ParameterAdd("@sortid", objTopic.sortid, DbType.Int32, 4);
                        dbh.ParameterAdd("@readperm", objTopic.readperm, DbType.Int32, 4);
                        dbh.ParameterAdd("@price", objTopic.price, DbType.Int32, 4);
                        dbh.ParameterAdd("@author", objTopic.author, DbType.String, 15);
                        dbh.ParameterAdd("@authorid", objTopic.authorid, DbType.Int32, 4);
                        dbh.ParameterAdd("@subject", objTopic.subject, DbType.String, 80);
                        dbh.ParameterAdd("@dateline", objTopic.dateline, DbType.Int32, 4);
                        dbh.ParameterAdd("@lastpost", objTopic.lastpost, DbType.Int32, 4);
                        dbh.ParameterAdd("@lastposter", objTopic.lastposter, DbType.String, 15);
                        dbh.ParameterAdd("@views", objTopic.views, DbType.Int32, 4);
                        dbh.ParameterAdd("@replies", objTopic.replies, DbType.Int32, 4);
                        dbh.ParameterAdd("@displayorder", objTopic.displayorder, DbType.Int32, 4);
                        dbh.ParameterAdd("@highlight", objTopic.highlight, DbType.Int32, 4);
                        dbh.ParameterAdd("@digest", objTopic.digest, DbType.Int32, 4);
                        dbh.ParameterAdd("@rate", objTopic.rate, DbType.Int32, 4);
                        dbh.ParameterAdd("@special", objTopic.special, DbType.Int32, 4);
                        dbh.ParameterAdd("@attachment", objTopic.attachment, DbType.Int32, 4);
                        dbh.ParameterAdd("@moderated", objTopic.moderated, DbType.Int32, 4);
                        dbh.ParameterAdd("@closed", objTopic.closed, DbType.Int32, 4);
                        dbh.ParameterAdd("@stickreply", objTopic.stickreply, DbType.Int32, 4);
                        dbh.ParameterAdd("@recommends", objTopic.recommends, DbType.Int32, 4);
                        dbh.ParameterAdd("@recommend_add", objTopic.recommend_add, DbType.Int32, 4);
                        dbh.ParameterAdd("@recommend_sub", objTopic.recommend_sub, DbType.Int32, 4);
                        dbh.ParameterAdd("@heats", objTopic.heats, DbType.Int32, 4);
                        dbh.ParameterAdd("@status", objTopic.status, DbType.Int32, 4);
                        dbh.ParameterAdd("@isgroup", objTopic.isgroup, DbType.Int32, 4);
                        dbh.ParameterAdd("@favtimes", objTopic.favtimes, DbType.Int32, 4);
                        dbh.ParameterAdd("@sharetimes", objTopic.sharetimes, DbType.Int32, 4);
                        dbh.ParameterAdd("@stamp", objTopic.stamp, DbType.Int32, 4);
                        dbh.ParameterAdd("@icon", objTopic.icon, DbType.Int32, 4);
                        dbh.ParameterAdd("@pushedaid", objTopic.pushedaid, DbType.Int32, 4);
                        dbh.ParameterAdd("@recommend", objTopic.recommend, DbType.Int32, 4);
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

            dbh.Dispose();
            MainForm.RecordCount = -1;
            MainForm.MessageForm.SetMessage(string.Format("完成转换主题。成功{0}，失败{1}\r\n", MainForm.SuccessedRecordCount, MainForm.FailedRecordCount));
        }

        /// <summary>
        /// 转换帖子
        /// </summary>
        public static void ConvertPosts()
        {
            Yuwen.Tools.Data.DBHelper dbh = MainForm.GetTargetDBH_OldVer();
            dbh.Open();

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
            dbh.TruncateTable(string.Format("{0}forum_post", MainForm.cic.TargetDbTablePrefix));

            #region sql语句
            string sqlPost = string.Format(@"INSERT INTO {0}forum_post (
`pid` ,
`fid` ,
`tid` ,
`first` ,
`author` ,
`authorid` ,
`subject` ,
`dateline` ,
`message` ,
`useip` ,
`invisible` ,
`anonymous` ,
`usesig` ,
`htmlon` ,
`bbcodeoff` ,
`smileyoff` ,
`parseurloff` ,
`attachment` ,
`rate` ,
`ratetimes` ,
`status` ,
`tags` ,
`comment` 
)
VALUES (
@pid,
@fid,
@tid,
@first,
@author,
@authorid,
@subject,
@dateline,
@message,
@useip,
@invisible,
@anonymous,
@usesig,
@htmlon,
@bbcodeoff,
@smileyoff,
@parseurloff,
@attachment,
@rate,
@ratetimes,
@status,
@tags,
@comment
)",
                MainForm.cic.TargetDbTablePrefix);
            #endregion

            for (int pagei = 1; pagei <= MainForm.PageCount; pagei++)
            {
                //分段得到主题列表
                List<Posts> postList = Provider.Provider.GetInstance().GetPostList(pagei);
                foreach (Posts objPost in postList)
                {
                    //清理上次执行的参数
                    dbh.ParametersClear();
                    #region dnt_posts表参数
                    dbh.ParameterAdd("@pid", objPost.pid, DbType.Int32, 4);
                    dbh.ParameterAdd("@fid", objPost.fid, DbType.Int32, 4);
                    dbh.ParameterAdd("@tid", objPost.tid, DbType.Int32, 4);
                    dbh.ParameterAdd("@first", objPost.first, DbType.Int32, 4);
                    dbh.ParameterAdd("@author", objPost.author, DbType.String, 15);
                    dbh.ParameterAdd("@authorid", objPost.authorid, DbType.Int32, 4);
                    dbh.ParameterAdd("@subject", objPost.subject, DbType.String, 80);
                    dbh.ParameterAdd("@dateline", objPost.dateline, DbType.Int32, 4);
                    dbh.ParameterAdd("@message", objPost.message, DbType.String, 10000);
                    dbh.ParameterAdd("@useip", objPost.useip, DbType.String, 15);
                    dbh.ParameterAdd("@invisible", objPost.invisible, DbType.Int32, 4);
                    dbh.ParameterAdd("@anonymous", objPost.anonymous, DbType.Int32, 4);
                    dbh.ParameterAdd("@usesig", objPost.usesig, DbType.Int32, 4);
                    dbh.ParameterAdd("@htmlon", objPost.htmlon, DbType.Int32, 4);
                    dbh.ParameterAdd("@bbcodeoff", objPost.bbcodeoff, DbType.Int32, 4);
                    dbh.ParameterAdd("@smileyoff", objPost.smileyoff, DbType.Int32, 4);
                    dbh.ParameterAdd("@parseurloff", objPost.parseurloff, DbType.Int32, 4);
                    dbh.ParameterAdd("@attachment", objPost.attachment, DbType.Int32, 4);
                    dbh.ParameterAdd("@rate", objPost.rate, DbType.Int32, 4);
                    dbh.ParameterAdd("@ratetimes", objPost.ratetimes, DbType.Int32, 4);
                    dbh.ParameterAdd("@status", objPost.status, DbType.Int32, 4);
                    dbh.ParameterAdd("@tags", objPost.tags, DbType.String, 255);
                    dbh.ParameterAdd("@comment", objPost.comment, DbType.Int32, 4);
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
            dbh.Dispose();
            MainForm.RecordCount = -1;
            MainForm.MessageForm.SetMessage(string.Format("完成转换帖子。成功{0}，失败{1}\r\n", MainForm.SuccessedRecordCount, MainForm.FailedRecordCount));
        }

        //TODO dnt_tablelist       

        /// <summary>
        /// 转换附件
        /// </summary>
        public static void ConvertAttachments()
        {
            Yuwen.Tools.Data.DBHelper dbh = MainForm.GetTargetDBH_OldVer();
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
            dbh.TruncateTable(string.Format("{0}forum_attachment", MainForm.cic.TargetDbTablePrefix));
            dbh.TruncateTable(string.Format("{0}forum_attachmentfield", MainForm.cic.TargetDbTablePrefix));
            for (int pagei = 1; pagei <= MainForm.PageCount; pagei++)
            {
                //分段得到主题列表
                List<Attachments> attachmentList = Provider.Provider.GetInstance().GetAttachmentList(pagei);
                foreach (Attachments objAttachment in attachmentList)
                {
                    #region sql语句
                    string sqlAttachment = string.Format(@"INSERT INTO {0}forum_attachment (
`aid` ,
`tid` ,
`pid` ,
`width` ,
`dateline` ,
`readperm` ,
`price` ,
`filename` ,
`filetype` ,
`filesize` ,
`attachment` ,
`downloads` ,
`isimage` ,
`uid` ,
`thumb` ,
`remote` ,
`picid` 
)
VALUES (
@aid,
@tid,
@pid,
@width,
@dateline,
@readperm,
@price,
@filename,
@filetype,
@filesize,
@attachment,
@downloads,
@isimage,
@uid,
@thumb,
@remote,
@picid
)", MainForm.cic.TargetDbTablePrefix);


                    string sqlAttachmentField = string.Format(@"INSERT INTO {0}forum_attachmentfield (
`aid` ,
`tid` ,
`pid` ,
`uid` ,
`description` 
)
VALUES (
@aid,
@tid,
@pid,
@uid,
@description
)", MainForm.cic.TargetDbTablePrefix);
                    #endregion
                    //清理上次执行的参数
                    dbh.ParametersClear();
                    #region dnt_attachment表参数
                    dbh.ParameterAdd("@aid", objAttachment.aid, DbType.Int32, 4);
                    dbh.ParameterAdd("@tid", objAttachment.tid, DbType.Int32, 4);
                    dbh.ParameterAdd("@pid", objAttachment.pid, DbType.Int32, 4);
                    dbh.ParameterAdd("@width", objAttachment.width, DbType.Int32, 4);
                    dbh.ParameterAdd("@dateline", objAttachment.dateline, DbType.Int32, 4);
                    dbh.ParameterAdd("@readperm", objAttachment.readperm, DbType.Int32, 4);
                    dbh.ParameterAdd("@price", objAttachment.price, DbType.Int32, 4);
                    dbh.ParameterAdd("@filename", objAttachment.filename, DbType.String, 100);
                    dbh.ParameterAdd("@filetype", objAttachment.filetype, DbType.String, 50);
                    dbh.ParameterAdd("@filesize", objAttachment.filesize, DbType.Int32, 4);
                    dbh.ParameterAdd("@attachment", objAttachment.attachment, DbType.String, 100);
                    dbh.ParameterAdd("@downloads", objAttachment.downloads, DbType.Int32, 4);
                    dbh.ParameterAdd("@isimage", objAttachment.isimage, DbType.Int32, 4);
                    dbh.ParameterAdd("@uid", objAttachment.uid, DbType.Int32, 4);
                    dbh.ParameterAdd("@thumb", objAttachment.thumb, DbType.Int32, 4);
                    dbh.ParameterAdd("@remote", objAttachment.remote, DbType.Int32, 4);
                    dbh.ParameterAdd("@picid", objAttachment.picid, DbType.Int32, 4);
                    dbh.ParameterAdd("@description", objAttachment.description, DbType.Int32, 4);
                    #endregion

                    try
                    {
                        dbh.ExecuteNonQuery(sqlAttachment);//插入dnt_topics表
                        dbh.ExecuteNonQuery(sqlAttachmentField);
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
            dbh.Dispose();
            MainForm.RecordCount = -1;
            MainForm.MessageForm.SetMessage(string.Format("完成转换附件。成功{0}，失败{1}\r\n", MainForm.SuccessedRecordCount, MainForm.FailedRecordCount));
        }


        private static void ConvertextAttach()
        {
            Yuwen.Tools.Data.DBHelper dbh = MainForm.GetTargetDBH_OldVer();
            dbh.Open();
            MainForm.MessageForm.SetMessage("开始转换额外附件\r\n");
            MainForm.SuccessedRecordCount = 0;
            MainForm.FailedRecordCount = 0;

            MainForm.RecordCount = MainForm.extAttachList.Count;
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
            //dbh.TruncateTable(string.Format("{0}forum_attachment", MainForm.cic.TargetDbTablePrefix));
            //dbh.TruncateTable(string.Format("{0}forum_attachmentfield", MainForm.cic.TargetDbTablePrefix));
            MainForm.PageCount = 1;//debug
            for (int pagei = 1; pagei <= MainForm.PageCount; pagei++)
            {
                //分段得到主题列表
                List<Attachments> attachmentList = MainForm.extAttachList;
                foreach (Attachments objAttachment in attachmentList)
                {
                    #region sql语句
                    string sqlAttachment = string.Format(@"INSERT INTO {0}forum_attachment (
`aid` ,
`tid` ,
`pid` ,
`width` ,
`dateline` ,
`readperm` ,
`price` ,
`filename` ,
`filetype` ,
`filesize` ,
`attachment` ,
`downloads` ,
`isimage` ,
`uid` ,
`thumb` ,
`remote` ,
`picid` 
)
VALUES (
@aid,
@tid,
@pid,
@width,
@dateline,
@readperm,
@price,
@filename,
@filetype,
@filesize,
@attachment,
@downloads,
@isimage,
@uid,
@thumb,
@remote,
@picid
)", MainForm.cic.TargetDbTablePrefix);


                    string sqlAttachmentField = string.Format(@"INSERT INTO {0}forum_attachmentfield (
`aid` ,
`tid` ,
`pid` ,
`uid` ,
`description` 
)
VALUES (
@aid,
@tid,
@pid,
@uid,
@description
)", MainForm.cic.TargetDbTablePrefix);
                    #endregion
                    //清理上次执行的参数
                    dbh.ParametersClear();
                    #region dnt_attachment表参数
                    dbh.ParameterAdd("@aid", objAttachment.aid, DbType.Int32, 4);
                    dbh.ParameterAdd("@tid", objAttachment.tid, DbType.Int32, 4);
                    dbh.ParameterAdd("@pid", objAttachment.pid, DbType.Int32, 4);
                    dbh.ParameterAdd("@width", objAttachment.width, DbType.Int32, 4);
                    dbh.ParameterAdd("@dateline", objAttachment.dateline, DbType.Int32, 4);
                    dbh.ParameterAdd("@readperm", objAttachment.readperm, DbType.Int32, 4);
                    dbh.ParameterAdd("@price", objAttachment.price, DbType.Int32, 4);
                    dbh.ParameterAdd("@filename", objAttachment.filename, DbType.String, 100);
                    dbh.ParameterAdd("@filetype", objAttachment.filetype, DbType.String, 50);
                    dbh.ParameterAdd("@filesize", objAttachment.filesize, DbType.Int32, 4);
                    dbh.ParameterAdd("@attachment", objAttachment.attachment, DbType.String, 100);
                    dbh.ParameterAdd("@downloads", objAttachment.downloads, DbType.Int32, 4);
                    dbh.ParameterAdd("@isimage", objAttachment.isimage, DbType.Int32, 4);
                    dbh.ParameterAdd("@uid", objAttachment.uid, DbType.Int32, 4);
                    dbh.ParameterAdd("@thumb", objAttachment.thumb, DbType.Int32, 4);
                    dbh.ParameterAdd("@remote", objAttachment.remote, DbType.Int32, 4);
                    dbh.ParameterAdd("@picid", objAttachment.picid, DbType.Int32, 4);
                    dbh.ParameterAdd("@description", objAttachment.description, DbType.Int32, 4);
                    #endregion

                    try
                    {
                        dbh.ExecuteNonQuery(sqlAttachment);//插入dnt_topics表
                        dbh.ExecuteNonQuery(sqlAttachmentField);
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
            dbh.Dispose();
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
            Yuwen.Tools.Data.DBHelper dbh = MainForm.GetTargetDBH_OldVer();
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
            dbh.TruncateTable(string.Format("{0}ucenter_pms", MainForm.cic.TargetDbTablePrefix));
            //dbh.SetIdentityInsertON(string.Format("{0}Pms", MainForm.cic.TargetDbTablePrefix));

            #region sql语句
            //准备语句中，related=0
            string sqlPmsPre = string.Format(@"INSERT INTO {0}ucenter_pms (
`msgfrom` ,
`msgfromid` ,
`msgtoid` ,
`folder` ,
`new` ,
`subject` ,
`dateline` ,
`message` ,
`delstatus` ,
`related` ,
`fromappid` 
)
VALUES (
@msgfrom,
@msgfromid,
@msgtoid,
@folder,
@new,
@subject,
@dateline,
@message,
@delstatus,
0,
@fromappid
)", MainForm.cic.TargetDbTablePrefix);
            //反插的准备语句中，fromid和toid是反的
            string sqlPmsOutPre = string.Format(@"INSERT INTO {0}ucenter_pms (
`msgfrom` ,
`msgfromid` ,
`msgtoid` ,
`folder` ,
`new` ,
`subject` ,
`dateline` ,
`message` ,
`delstatus` ,
`related` ,
`fromappid` 
)
VALUES (
@msgfrom,
@msgtoid,
@msgfromid,
@folder,
@new,
@subject,
@dateline,
@message,
@delstatus,
0,
@fromappid
)", MainForm.cic.TargetDbTablePrefix);
            //正式语句中 related=1
            string sqlPms = string.Format(@"INSERT INTO {0}ucenter_pms (
`msgfrom` ,
`msgfromid` ,
`msgtoid` ,
`folder` ,
`new` ,
`subject` ,
`dateline` ,
`message` ,
`delstatus` ,
`related` ,
`fromappid` 
)
VALUES (
@msgfrom,
@msgfromid,
@msgtoid,
@folder,
@new,
@subject,
@dateline,
@message,
@delstatus,
1,
@fromappid
)", MainForm.cic.TargetDbTablePrefix);
            #endregion

            for (int pagei = 1; pagei <= MainForm.PageCount; pagei++)
            {
                //分段得到主题列表
                List<Pms> pmList = Provider.Provider.GetInstance().GetPmList(pagei);
                foreach (Pms objPm in pmList)
                {

                    //清理上次执行的参数
                    dbh.ParametersClear();
                    #region dnt_pms表参数
                    dbh.ParameterAdd("@pmid", objPm.pmid, DbType.Int32, 4);
                    dbh.ParameterAdd("@msgfrom", objPm.msgfrom, DbType.String, 15);
                    dbh.ParameterAdd("@msgfromid", objPm.msgfromid, DbType.Int32, 4);
                    dbh.ParameterAdd("@msgtoid", objPm.msgtoid, DbType.Int32, 4);
                    dbh.ParameterAdd("@folder", objPm.folder, DbType.String, 6);
                    dbh.ParameterAdd("@new", objPm.isnew, DbType.Int32, 4);
                    dbh.ParameterAdd("@subject", objPm.subject, DbType.String, 75);
                    dbh.ParameterAdd("@dateline", objPm.dateline, DbType.Int32, 4);
                    dbh.ParameterAdd("@message", objPm.message, DbType.String, 8000);
                    dbh.ParameterAdd("@delstatus", objPm.delstatus, DbType.Int32, 4);
                    dbh.ParameterAdd("@related", objPm.related, DbType.Int32, 4);
                    dbh.ParameterAdd("@fromappid", objPm.fromappid, DbType.Int32, 4);
                    #endregion

                    try
                    {
                        //if 1=>2 !=true
                        //  insert 1=>2(related=0)
                        //else
                        //  update 1=>2(related=0)

                        //if 2=>1 != true
                        //  insert 2=>1(related=0)

                        //insert 1=>2 (related=1)
                        if (!(objPm.msgfrom.Trim() == string.Empty && objPm.msgfromid == 0))
                        {
                            if (Convert.ToInt32(
                                dbh.ExecuteScalar(
                                    string.Format(
                                        "SELECT count(*) FROM {0}ucenter_pms WHERE msgfromid=@msgfromid AND msgtoid=@msgtoid AND folder='inbox' AND related='0'",
                                        MainForm.cic.TargetDbTablePrefix
                                        )
                                    )
                                )
                            == 0)
                            {
                                dbh.ExecuteNonQuery(sqlPmsPre);
                            }
                            else
                            {
                                dbh.ExecuteNonQuery(
                                    string.Format(
                                    "UPDATE {0}ucenter_pms SET subject=@subject, message=@message, dateline=@dateline, new=@new, fromappid=@fromappid WHERE msgfromid=@msgfromid AND msgtoid=@msgtoid AND folder='inbox' AND related='0'",
                                    MainForm.cic.TargetDbTablePrefix
                                    )
                                    );
                            }

                            if (Convert.ToInt32(
                                dbh.ExecuteScalar(
                                    string.Format(
                                        "SELECT count(*) FROM {0}ucenter_pms WHERE msgfromid=@msgtoid AND msgtoid=@msgfromid AND folder='inbox' AND related='0'",
                                        MainForm.cic.TargetDbTablePrefix
                                        )
                                    )
                                )
                            == 0)
                            {
                                dbh.ExecuteNonQuery(sqlPmsOutPre);
                            }
                        }
                        //正式插入了
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

            //dbh.SetIdentityInsertOFF(string.Format("{0}Pms", MainForm.cic.TargetDbTablePrefix));

            //MainForm.MessageForm.SetMessage("清理临时短消息数据\r\n");
            //dbh.ExecuteNonQuery(string.Format("DELETE FROM {0}ucenter_pms WHERE msgfromid=-77", MainForm.cic.TargetDbTablePrefix));
            dbh.Dispose();
            MainForm.RecordCount = -1;
            MainForm.MessageForm.SetMessage(string.Format("完成转换短消息。成功(有效条数){0}，失败{1}\r\n", MainForm.SuccessedRecordCount, MainForm.FailedRecordCount));
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
