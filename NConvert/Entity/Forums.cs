using System;

namespace NConvert.Entity
{
    public class Forums
    {
        public int fid { get; set; }
        public int fup { get; set; }
        public string type { get; set; }

        /// <summary>
        /// char50
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 0隐藏 1正常 2只显示子版块 3群组
        /// </summary>
        public int status { get; set; }
        public int displayorder { get; set; }
        public int styleid { get; set; }
        public int threads { get; set; }
        public int posts { get; set; }
        public int todayposts { get; set; }
        /// <summary>
        /// 110char
        /// </summary>
        public string lastpost { get; set; }
        /// <summary>
        /// char15
        /// </summary>
        public string domain { get; set; }
        public int allowsmilies { get; set; }
        public int allowhtml { get; set; }
        public int allowbbcode { get; set; }
        public int allowimgcode { get; set; }
        public int allowmediacode { get; set; }
        public int allowanonymous { get; set; }
        public int allowpostspecial { get; set; }
        public int allowspecialonly { get; set; }
        /// <summary>
        /// 开启帖子补充
        /// </summary>
        public int allowappend { get; set; }
        public int alloweditrules { get; set; }
        public int allowfeed { get; set; }
        public int allowside { get; set; }
        public int recyclebin { get; set; }
        public int modnewposts { get; set; }
        public int jammer { get; set; }
        public int disablewatermark { get; set; }
        public int inheritedmod { get; set; }
        public int autoclose { get; set; }
        public int forumcolumns { get; set; }
        public int threadcaches { get; set; }
        public int alloweditpost { get; set; }
        /// <summary>
        /// 只显示子版块
        /// </summary>
        public int simple { get; set; }
        /// <summary>
        /// 本版有待处理事项
        /// </summary>
        public int modworks { get; set; }
        public int allowtag { get; set; }
        /// <summary>
        /// 是否显示全局置顶
        /// </summary>
        public int allowglobalstick { get; set; }
        public int level { get; set; }
        public int commoncredits { get; set; }
        public int archive { get; set; }
        public int recommend { get; set; }
        public int favtimes { get; set; }
        public int sharetimes { get; set; }

        /// <summary>
        /// text
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// char12
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 255char
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 255char
        /// </summary>
        public string redirect { get; set; }
        /// <summary>
        /// 255char
        /// </summary>
        public string attachextensions { get; set; }
        /// <summary>
        /// text
        /// </summary>
        public string creditspolicy { get; set; }
        public string formulaperm { get; set; }
        public string moderators { get; set; }
        public string rules { get; set; }
        public string threadtypes { get; set; }
        public string threadsorts { get; set; }
        public string viewperm { get; set; }
        public string postperm { get; set; }
        public string replyperm { get; set; }
        public string getattachperm { get; set; }
        public string postattachperm { get; set; }
        public string postimageperm { get; set; }
        public string spviewperm { get; set; }
        public string keywords { get; set; }
        public string supe_pushsetting { get; set; }
        public string modrecommend { get; set; }
        public string threadplugin { get; set; }
        public string extra { get; set; }
        /// <summary>
        /// 加入群组方式 -1关闭 0公开 2邀请
        /// </summary>
        public int jointype { get; set; }
        public int gviewperm { get; set; }
        public int membernum { get; set; }
        public int dateline { get; set; }
        public int lastupdate { get; set; }
        public int activity { get; set; }
        public int founderuid { get; set; }
        /// <summary>
        /// char255 群组创始人
        /// </summary>
        public string foundername { get; set; }
        public string banner { get; set; }
        public int groupnum { get; set; }
        public string commentitem { get; set; }
        public int hidemenu { get; set; }
    }

    public enum FupType
    {
        group = 0,
        forum = 1,
        sub = 2
    }
}
