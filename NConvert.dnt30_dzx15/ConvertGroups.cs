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
        #region IProvider 成员
        public int GetGroupRecordCount()
        {
            if (MainForm.RecordCount == -1)
            {
                return 2 + Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT(id) FROM [science].[dbo].[group_info]")));
            }
            else
            {
                return MainForm.RecordCount;
            }
        }

        public List<Forums> GetGroupList()
        {
            string sqlBoard = string.Format(
                "SELECT * FROM [science].[dbo].[group_info]",
                MainForm.cic.SrcDbTablePrefix
                );

            System.Data.Common.DbDataReader drBoard = MainForm.srcDBH.ExecuteReader(sqlBoard);

            List<Forums> forumList = new List<Forums>();

            Forums objDefGroupTopType = new Forums();
            objDefGroupTopType.fid = 200;
            objDefGroupTopType.fup = 0;
            objDefGroupTopType.type = FupType.group.ToString();
            objDefGroupTopType.name = "群组一级分类";
            objDefGroupTopType.status = 3;
            objDefGroupTopType.displayorder = 0;
            objDefGroupTopType.styleid = 0;
            objDefGroupTopType.threads = 0;
            objDefGroupTopType.posts = 0;
            objDefGroupTopType.todayposts = 0;
            objDefGroupTopType.lastpost = "";
            objDefGroupTopType.domain = "";
            objDefGroupTopType.allowsmilies = 0;
            objDefGroupTopType.allowhtml = 1;
            objDefGroupTopType.allowbbcode = 0;
            objDefGroupTopType.allowimgcode = 0;
            objDefGroupTopType.allowmediacode = 0;
            objDefGroupTopType.allowanonymous = 0;
            objDefGroupTopType.allowpostspecial = 0;
            objDefGroupTopType.allowspecialonly = 0;
            objDefGroupTopType.allowappend = 0;
            objDefGroupTopType.alloweditrules = 0;
            objDefGroupTopType.allowfeed = 0;
            objDefGroupTopType.allowside = 0;
            objDefGroupTopType.recyclebin = 0;
            objDefGroupTopType.modnewposts = 0;
            objDefGroupTopType.jammer = 0;
            objDefGroupTopType.disablewatermark = 0;
            objDefGroupTopType.inheritedmod = 0;
            objDefGroupTopType.autoclose = 0;
            objDefGroupTopType.forumcolumns = 0;
            objDefGroupTopType.threadcaches = 0;
            objDefGroupTopType.alloweditpost = 1;
            objDefGroupTopType.simple = 0;
            objDefGroupTopType.modworks = 0;
            objDefGroupTopType.allowtag = 1;
            objDefGroupTopType.allowglobalstick = 1;
            objDefGroupTopType.level = 0;
            objDefGroupTopType.commoncredits = 0;
            objDefGroupTopType.archive = 0;
            objDefGroupTopType.recommend = 0;
            objDefGroupTopType.favtimes = 0;
            objDefGroupTopType.sharetimes = 0;

            objDefGroupTopType.description = "";
            objDefGroupTopType.password = "";
            objDefGroupTopType.icon = "";
            objDefGroupTopType.redirect = "";
            objDefGroupTopType.attachextensions = "";
            objDefGroupTopType.creditspolicy = "";
            objDefGroupTopType.formulaperm = "";
            objDefGroupTopType.moderators = "";
            objDefGroupTopType.rules = "";
            objDefGroupTopType.threadtypes = "";
            objDefGroupTopType.threadsorts = "";
            objDefGroupTopType.viewperm = "";
            objDefGroupTopType.postperm = "";
            objDefGroupTopType.replyperm = "";
            objDefGroupTopType.getattachperm = "";
            objDefGroupTopType.postattachperm = "";
            objDefGroupTopType.postimageperm = "";//Discuz!NT无这个，统一为上传附件权限
            objDefGroupTopType.spviewperm = "";
            objDefGroupTopType.keywords = "";
            objDefGroupTopType.supe_pushsetting = "";
            objDefGroupTopType.modrecommend = "";
            objDefGroupTopType.threadplugin = "";
            objDefGroupTopType.extra = "";
            objDefGroupTopType.jointype = 0;
            objDefGroupTopType.gviewperm = 0;
            objDefGroupTopType.membernum = 0;
            objDefGroupTopType.dateline = 0;
            objDefGroupTopType.lastupdate = 0;
            objDefGroupTopType.activity = 0;
            objDefGroupTopType.founderuid = 0;
            objDefGroupTopType.foundername = "";
            objDefGroupTopType.banner = "";
            objDefGroupTopType.groupnum = 0;
            objDefGroupTopType.commentitem = "";
            objDefGroupTopType.hidemenu = 0;
            forumList.Add(objDefGroupTopType);


            Forums objDefGroupSubType = new Forums();
            objDefGroupSubType.fid = 201;
            objDefGroupSubType.fup = 200;
            objDefGroupSubType.type = FupType.forum.ToString();
            objDefGroupSubType.name = "群组二级分类";
            objDefGroupSubType.status = 3;
            objDefGroupSubType.displayorder = 0;
            objDefGroupSubType.styleid = 0;
            objDefGroupSubType.threads = 0;
            objDefGroupSubType.posts = 0;
            objDefGroupSubType.todayposts = 0;
            objDefGroupSubType.lastpost = "";
            objDefGroupSubType.domain = "";
            objDefGroupSubType.allowsmilies = 0;
            objDefGroupSubType.allowhtml = 0;
            objDefGroupSubType.allowbbcode = 0;
            objDefGroupSubType.allowimgcode = 0;
            objDefGroupSubType.allowmediacode = 0;
            objDefGroupSubType.allowanonymous = 0;
            objDefGroupSubType.allowpostspecial = 0;
            objDefGroupSubType.allowspecialonly = 0;
            objDefGroupSubType.allowappend = 0;
            objDefGroupSubType.alloweditrules = 0;
            objDefGroupSubType.allowfeed = 0;
            objDefGroupSubType.allowside = 0;
            objDefGroupSubType.recyclebin = 0;
            objDefGroupSubType.modnewposts = 0;
            objDefGroupSubType.jammer = 0;
            objDefGroupSubType.disablewatermark = 0;
            objDefGroupSubType.inheritedmod = 0;
            objDefGroupSubType.autoclose = 0;
            objDefGroupSubType.forumcolumns = 0;
            objDefGroupSubType.threadcaches = 0;
            objDefGroupSubType.alloweditpost = 1;
            objDefGroupSubType.simple = 0;
            objDefGroupSubType.modworks = 0;
            objDefGroupSubType.allowtag = 1;
            objDefGroupSubType.allowglobalstick = 1;
            objDefGroupSubType.level = 0;
            objDefGroupSubType.commoncredits = 0;
            objDefGroupSubType.archive = 0;
            objDefGroupSubType.recommend = 0;
            objDefGroupSubType.favtimes = 0;
            objDefGroupSubType.sharetimes = 0;

            objDefGroupSubType.description = "";
            objDefGroupSubType.password = "";
            objDefGroupSubType.icon = "";
            objDefGroupSubType.redirect = "";
            objDefGroupSubType.attachextensions = "";
            objDefGroupSubType.creditspolicy = "";
            objDefGroupSubType.formulaperm = "";
            objDefGroupSubType.moderators = "";
            objDefGroupSubType.rules = "";
            objDefGroupSubType.threadtypes = "";
            objDefGroupSubType.threadsorts = "";
            objDefGroupSubType.viewperm = "";
            objDefGroupSubType.postperm = "";
            objDefGroupSubType.replyperm = "";
            objDefGroupSubType.getattachperm = "";
            objDefGroupSubType.postattachperm = "";
            objDefGroupSubType.postimageperm = "";//Discuz!NT无这个，统一为上传附件权限
            objDefGroupSubType.spviewperm = "";
            objDefGroupSubType.keywords = "";
            objDefGroupSubType.supe_pushsetting = "";
            objDefGroupSubType.modrecommend = "";
            objDefGroupSubType.threadplugin = "";
            objDefGroupSubType.extra = "";
            objDefGroupSubType.jointype = 0;
            objDefGroupSubType.gviewperm = 0;
            objDefGroupSubType.membernum = 0;
            objDefGroupSubType.dateline = 0;
            objDefGroupSubType.lastupdate = 0;
            objDefGroupSubType.activity = 0;
            objDefGroupSubType.founderuid = 0;
            objDefGroupSubType.foundername = "";
            objDefGroupSubType.banner = "";
            objDefGroupSubType.groupnum = 0;
            objDefGroupSubType.commentitem = "";
            objDefGroupSubType.hidemenu = 0;
            forumList.Add(objDefGroupSubType);


            while (drBoard.Read())
            {
                Forums objForum = new Forums();
                objForum.fid = forumList[forumList.Count - 1].fid + 1;
                objForum.fup = objDefGroupSubType.fid;
                objForum.type = FupType.sub.ToString();
                objForum.name = drBoard["group_name"].ToString();
                objForum.status = 3;
                objForum.displayorder = 0;
                objForum.styleid = 0;
                objForum.threads = Convert.ToInt32(drBoard["group_book"]);
                objForum.posts = Convert.ToInt32(drBoard["group_book"]);
                objForum.todayposts = 0;
                objForum.lastpost = "";
                objForum.domain = drBoard["group_user"].ToString();
                objForum.allowsmilies = 0;
                objForum.allowhtml = 0;
                objForum.allowbbcode = 0;
                objForum.allowimgcode = 0;
                objForum.allowmediacode = 1;
                objForum.allowanonymous = 0;
                objForum.allowpostspecial = 0;
                objForum.allowspecialonly = 0;
                objForum.allowappend = 0;
                objForum.alloweditrules = 0;
                objForum.allowfeed = 0;
                objForum.allowside = 0;
                objForum.recyclebin = 0;
                objForum.modnewposts = 0;
                objForum.jammer = 0;
                objForum.disablewatermark = 0;
                objForum.inheritedmod = 0;
                objForum.autoclose = 0;
                objForum.forumcolumns = 0;
                objForum.threadcaches = 0;
                objForum.alloweditpost = 1;
                objForum.simple = 0;
                objForum.modworks = 0;
                objForum.allowtag = 1;
                objForum.allowglobalstick = 1;
                objForum.level = 0;
                objForum.commoncredits = 0;
                objForum.archive = 0;
                objForum.recommend = 0;
                objForum.favtimes = 0;
                objForum.sharetimes = 0;

                objForum.description = drBoard["group_demo"].ToString();
                objForum.password = "";
                objForum.icon = drBoard["group_logo"] != DBNull.Value ? drBoard["group_logo"].ToString().Trim('/') : "";
                objForum.redirect = "";
                objForum.attachextensions = "";
                objForum.creditspolicy = "";
                objForum.formulaperm = "";
                objForum.moderators = "";
                objForum.rules = "";
                objForum.threadtypes = "";
                objForum.threadsorts = "";
                objForum.viewperm = "";
                objForum.postperm = "";
                objForum.replyperm = "";
                objForum.getattachperm = "";
                objForum.postattachperm = "";
                objForum.postimageperm = "";
                objForum.spviewperm = "";
                objForum.keywords = "";
                objForum.supe_pushsetting = "";
                objForum.modrecommend = "";
                objForum.threadplugin = "";
                objForum.extra = "";
                objForum.jointype = 0;
                objForum.gviewperm = 0;
                objForum.membernum = 0;
                objForum.dateline = 0;
                objForum.lastupdate = 0;
                objForum.activity = 0;
                objForum.founderuid = 0;
                objForum.foundername = "";
                objForum.banner = "";
                objForum.groupnum = 0;
                objForum.commentitem = "";
                objForum.hidemenu = 0;
                forumList.Add(objForum);
            }
            drBoard.Close();
            drBoard.Dispose();
            return forumList;
        }

        public Dictionary<string, int> GetGroupDic()
        {
            Dictionary<string, int> GroupDic = new Dictionary<string, int>();

            string sqlGroupList = string.Format("SELECT fid,domain FROM {0}forum_forum WHERE status=3 AND type='sub'", MainForm.cic.TargetDbTablePrefix);
            Yuwen.Tools.Data.DBHelper dbh = MainForm.GetTargetDBH_OldVer();
            System.Data.Common.DbDataReader drGroupList = dbh.ExecuteReader(sqlGroupList);
            while (drGroupList.Read())
            {
                GroupDic.Add(drGroupList["domain"].ToString().Trim(), Convert.ToInt32(drGroupList["fid"]));
            }
            drGroupList.Close();
            drGroupList.Dispose();
            return GroupDic;
        }

        #endregion
    }
}
