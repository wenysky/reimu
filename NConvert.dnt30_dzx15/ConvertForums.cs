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
        public int GetForumsRecordCount()
        {
            if (MainForm.RecordCount == -1)
            {
                return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT(fid) FROM {0}forums", MainForm.cic.SrcDbTablePrefix)));
            }
            else
            {
                return MainForm.RecordCount;
            }
        }

        public List<Forums> GetForumList()
        {
            string sqlBoard = string.Format(
                "SELECT * FROM {0}forums f LEFT JOIN {0}forumfields ff ON f.fid=ff.fid ORDER BY f.fid",
                MainForm.cic.SrcDbTablePrefix
                );

            System.Data.Common.DbDataReader drBoard = MainForm.srcDBH.ExecuteReader(sqlBoard);

            List<Forums> forumList = new List<Forums>();
            while (drBoard.Read())
            {
                Forums objForum = new Forums();
                objForum.fid = Convert.ToInt32(drBoard["fid"]);
                objForum.fup = Convert.ToInt32(drBoard["parentid"]);
                int layer = Convert.ToInt32(drBoard["layer"]) < 3 ? Convert.ToInt32(drBoard["layer"]) : 2;
                objForum.type = ((FupType)layer).ToString();
                objForum.name = drBoard["name"].ToString();
                objForum.status = Convert.ToInt32(drBoard["status"]);
                objForum.displayorder = Convert.ToInt32(drBoard["displayorder"]);
                objForum.styleid = 0;
                objForum.threads = Convert.ToInt32(drBoard["topics"]);
                objForum.posts = Convert.ToInt32(drBoard["posts"]);
                objForum.todayposts = Convert.ToInt32(drBoard["todayposts"]);
                objForum.lastpost = drBoard["lastpost"].ToString();
                objForum.domain = "";
                objForum.allowsmilies = Convert.ToInt32(drBoard["allowsmilies"]);
                objForum.allowhtml = Convert.ToInt32(drBoard["allowhtml"]);
                objForum.allowbbcode = Convert.ToInt32(drBoard["allowbbcode"]);
                objForum.allowimgcode = Convert.ToInt32(drBoard["allowimgcode"]);
                objForum.allowmediacode = 1;
                objForum.allowanonymous = 0;
                objForum.allowpostspecial = Convert.ToInt32(drBoard["allowpostspecial"]);
                objForum.allowspecialonly = Convert.ToInt32(drBoard["allowspecialonly"]);
                objForum.allowappend = 0;
                objForum.alloweditrules = Convert.ToInt32(drBoard["alloweditrules"]);
                objForum.allowfeed = 0;
                objForum.allowside = 0;
                objForum.recyclebin = Convert.ToInt32(drBoard["recyclebin"]);
                objForum.modnewposts = Convert.ToInt32(drBoard["modnewposts"]);
                objForum.jammer = Convert.ToInt32(drBoard["jammer"]);
                objForum.disablewatermark = Convert.ToInt32(drBoard["disablewatermark"]);
                objForum.inheritedmod = Convert.ToInt32(drBoard["inheritedmod"]);
                objForum.autoclose = Convert.ToInt32(drBoard["autoclose"]);
                objForum.forumcolumns = Convert.ToInt32(drBoard["colcount"]) > 1 ? Convert.ToInt32(drBoard["colcount"]) : 0;
                objForum.threadcaches = 0;
                objForum.alloweditpost = 1;
                objForum.simple = 0;
                objForum.modworks = 0;
                objForum.allowtag = Convert.ToInt32(drBoard["allowtag"]);
                objForum.allowglobalstick = 1;
                objForum.level = 0;
                objForum.commoncredits = 0;
                objForum.archive = 0;
                objForum.recommend = 0;
                objForum.favtimes = 0;
                objForum.sharetimes = 0;

                objForum.description = drBoard["description"].ToString();
                objForum.password = drBoard["password"].ToString();
                objForum.icon = drBoard["icon"].ToString();
                objForum.redirect = drBoard["redirect"].ToString();
                objForum.attachextensions = drBoard["attachextensions"].ToString();
                objForum.creditspolicy = "";
                objForum.formulaperm = "";
                objForum.moderators = drBoard["moderators"].ToString();
                objForum.rules = drBoard["rules"].ToString();
                objForum.threadtypes = "";
                objForum.threadsorts = "";
                objForum.viewperm = drBoard["viewperm"].ToString();
                objForum.postperm = drBoard["postperm"].ToString();
                objForum.replyperm = drBoard["replyperm"].ToString();
                objForum.getattachperm = drBoard["getattachperm"].ToString();
                objForum.postattachperm = drBoard["postattachperm"].ToString();
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
    }
}
