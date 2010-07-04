using System;

namespace NConvert.Entity
{
    public class ShortForums
    {
        private int m_fid = 0;

        private short m_parentid = 0;

        private short m_layer = 0;

        private string m_pathlist = "";

        private string m_parentidlist = "0";

        private int m_subforumcount = 0;

        private string m_name = "";

        private int m_status = 1;

        private short m_colcount = 1;

        private int m_displayorder = 0;

        private short m_templateid = 0;

        private int m_topics = 0;

        private int m_curtopics = 0;

        private int m_posts = 0;

        private int m_todayposts = 0;

        private int m_lasttid = 0;

        private string m_lasttitle = "";

        private System.DateTime m_lastpost = DateTime.Now;

        private int m_lastposterid = 0;

        private string m_lastposter = "";

        private int m_allowsmilies = 1;

        private int m_allowrss = 1;

        private int m_allowhtml = 0;

        private int m_allowbbcode = 1;

        private int m_allowimgcode = 1;

        private int m_allowblog = 0;

        private int m_istrade = 0;

        private int m_allowpostspecial = 21;

        private int m_allowspecialonly = 0;

        private int m_alloweditrules = 0;

        private int m_allowthumbnail = 0;

        private int m_allowtag = 1;

        private int m_recyclebin = 0;

        private int m_modnewposts = 0;

        private int m_jammer = 0;

        private int m_disablewatermark = 0;

        private int m_inheritedmod = 1;

        private short m_autoclose = 0;

        /// <summary> fid </summary>
        public int fid
        {
            get
            {
                return this.m_fid;
            }
            set
            {
                this.m_fid = value;
            }
        }

        /// <summary> 本论坛的上级论坛或分本论坛的上级论坛或分类的fid </summary>
        public short parentid
        {
            get
            {
                return this.m_parentid;
            }
            set
            {
                this.m_parentid = value;
            }
        }

        /// <summary> 论坛层次 </summary>
        public short layer
        {
            get
            {
                return this.m_layer;
            }
            set
            {
                this.m_layer = value;
            }
        }

        /// <summary> 论坛级别所处路径的html链接代码 </summary>
        public string pathlist
        {
            get
            {
                return this.m_pathlist;
            }
            set
            {
                this.m_pathlist = value;
            }
        }

        /// <summary> 论坛级别所处路径id列表 </summary>
        public string parentidlist
        {
            get
            {
                return this.m_parentidlist;
            }
            set
            {
                this.m_parentidlist = value;
            }
        }

        /// <summary> 论坛包括的子论坛个数 </summary>
        public int subforumcount
        {
            get
            {
                return this.m_subforumcount;
            }
            set
            {
                this.m_subforumcount = value;
            }
        }

        /// <summary> 论坛名称 </summary>
        public string name
        {
            get
            {
                return this.m_name;
            }
            set
            {
                this.m_name = value;
            }
        }

        /// <summary> 是否显示 </summary>
        public int status
        {
            get
            {
                return this.m_status;
            }
            set
            {
                this.m_status = value;
            }
        }

        /// <summary> 设置该论坛的子论坛在列表时分几列显示 </summary>
        public short colcount
        {
            get
            {
                return this.m_colcount;
            }
            set
            {
                this.m_colcount = value;
            }
        }

        /// <summary> 显示顺序 </summary>
        public int displayorder
        {
            get
            {
                return this.m_displayorder;
            }
            set
            {
                this.m_displayorder = value;
            }
        }

        /// <summary> 风格id,0为默认 </summary>
        public short templateid
        {
            get
            {
                return this.m_templateid;
            }
            set
            {
                this.m_templateid = value;
            }
        }

        /// <summary> 主题数 </summary>
        public int topics
        {
            get
            {
                return this.m_topics;
            }
            set
            {
                this.m_topics = value;
            }
        }

        /// <summary> 当前主题数 </summary>
        public int curtopics
        {
            get
            {
                return this.m_curtopics;
            }
            set
            {
                this.m_curtopics = value;
            }
        }

        /// <summary> 帖子数 </summary>
        public int posts
        {
            get
            {
                return this.m_posts;
            }
            set
            {
                this.m_posts = value;
            }
        }

        /// <summary> 今日发帖 </summary>
        public int todayposts
        {
            get
            {
                return this.m_todayposts;
            }
            set
            {
                this.m_todayposts = value;
            }
        }

        /// <summary> 最后发表的帖子ID </summary>
        public int lasttid
        {
            get
            {
                return this.m_lasttid;
            }
            set
            {
                this.m_lasttid = value;
            }
        }

        /// <summary> 最后发表的帖子标题 </summary>
        public string lasttitle
        {
            get
            {
                return this.m_lasttitle;
            }
            set
            {
                this.m_lasttitle = value;
            }
        }

        /// <summary> lastpost </summary>
        public System.DateTime lastpost
        {
            get
            {
                return this.m_lastpost;
            }
            set
            {
                this.m_lastpost = value;
            }
        }

        /// <summary> lastposterid </summary>
        public int lastposterid
        {
            get
            {
                return this.m_lastposterid;
            }
            set
            {
                this.m_lastposterid = value;
            }
        }

        /// <summary> lastposter </summary>
        public string lastposter
        {
            get
            {
                return this.m_lastposter;
            }
            set
            {
                this.m_lastposter = value;
            }
        }

        /// <summary> allowsmilies </summary>
        public int allowsmilies
        {
            get
            {
                return this.m_allowsmilies;
            }
            set
            {
                this.m_allowsmilies = value;
            }
        }

        /// <summary> allowrss </summary>
        public int allowrss
        {
            get
            {
                return this.m_allowrss;
            }
            set
            {
                this.m_allowrss = value;
            }
        }

        /// <summary> allowhtml </summary>
        public int allowhtml
        {
            get
            {
                return this.m_allowhtml;
            }
            set
            {
                this.m_allowhtml = value;
            }
        }

        /// <summary> allowbbcode </summary>
        public int allowbbcode
        {
            get
            {
                return this.m_allowbbcode;
            }
            set
            {
                this.m_allowbbcode = value;
            }
        }

        /// <summary> allowimgcode </summary>
        public int allowimgcode
        {
            get
            {
                return this.m_allowimgcode;
            }
            set
            {
                this.m_allowimgcode = value;
            }
        }

        /// <summary> 允许将文章添加为Blog </summary>
        public int allowblog
        {
            get
            {
                return this.m_allowblog;
            }
            set
            {
                this.m_allowblog = value;
            }
        }

        /// <summary> 允许交易贴 </summary>
        public int istrade
        {
            get
            {
                return this.m_istrade;
            }
            set
            {
                this.m_istrade = value;
            }
        }

        /// <summary> 允许特殊贴(默认为21) </summary>
        public int allowpostspecial
        {
            get
            {
                return this.m_allowpostspecial;
            }
            set
            {
                this.m_allowpostspecial = value;
            }
        }

        /// <summary> 只允许特殊贴 </summary>
        public int allowspecialonly
        {
            get
            {
                return this.m_allowspecialonly;
            }
            set
            {
                this.m_allowspecialonly = value;
            }
        }

        /// <summary> 允许版主编辑论坛规则 </summary>
        public int alloweditrules
        {
            get
            {
                return this.m_alloweditrules;
            }
            set
            {
                this.m_alloweditrules = value;
            }
        }

        /// <summary> 使用主题缩略图 </summary>
        public int allowthumbnail
        {
            get
            {
                return this.m_allowthumbnail;
            }
            set
            {
                this.m_allowthumbnail = value;
            }
        }

        /// <summary> 允许使用标签 </summary>
        public int allowtag
        {
            get
            {
                return this.m_allowtag;
            }
            set
            {
                this.m_allowtag = value;
            }
        }

        /// <summary> 打开回收站 </summary>
        public int recyclebin
        {
            get
            {
                return this.m_recyclebin;
            }
            set
            {
                this.m_recyclebin = value;
            }
        }


        /// <summary> 发帖需要审核 </summary>
        public int modnewposts
        {
            get
            {
                return this.m_modnewposts;
            }
            set
            {
                this.m_modnewposts = value;
            }
        }

        /// <summary> 帖子中添加干扰码,防止恶意复制 </summary>
        public int jammer
        {
            get
            {
                return this.m_jammer;
            }
            set
            {
                this.m_jammer = value;
            }
        }

        /// <summary> 禁止附件自动水印 </summary>
        public int disablewatermark
        {
            get
            {
                return this.m_disablewatermark;
            }
            set
            {
                this.m_disablewatermark = value;
            }
        }

        /// <summary> 继承上级论坛或分类的版主设定 </summary>
        public int inheritedmod
        {
            get
            {
                return this.m_inheritedmod;
            }
            set
            {
                this.m_inheritedmod = value;
            }
        }

        /// <summary> 定期自动关闭主题,单位为天 </summary>
        public short autoclose
        {
            get
            {
                return this.m_autoclose;
            }
            set
            {
                this.m_autoclose = value;
            }
        }
    }

    public class Forums : ShortForums
    {
        private string m_password = "";

        private string m_icon = "";

        private string m_postcredits = "";

        private string m_replycredits = "";

        private string m_redirect = "";

        private string m_attachextensions = "";

        private string m_rules = "";

        private string m_topictypes = "";

        private string m_viewperm = "";

        private string m_postperm = "";

        private string m_replyperm = "";

        private string m_getattachperm = "";

        private string m_postattachperm = "";

        private string m_moderators = "";

        private string m_description = "";

        private System.Byte m_applytopictype = 0;

        private System.Byte m_postbytopictype = 0;

        private System.Byte m_viewbytopictype = 0;

        private System.Byte m_topictypeprefix = 0;

        private string m_permuserlist = "";



        /// <summary> 访问本论坛的密码,留空为不需密码 </summary>
        public string password
        {
            get
            {
                return this.m_password;
            }
            set
            {
                this.m_password = value;
            }
        }

        /// <summary> icon </summary>
        public string icon
        {
            get
            {
                return this.m_icon;
            }
            set
            {
                this.m_icon = value;
            }
        }

        /// <summary> postcredits </summary>
        public string postcredits
        {
            get
            {
                return this.m_postcredits;
            }
            set
            {
                this.m_postcredits = value;
            }
        }

        /// <summary> replycredits </summary>
        public string replycredits
        {
            get
            {
                return this.m_replycredits;
            }
            set
            {
                this.m_replycredits = value;
            }
        }

        /// <summary> redirect </summary>
        public string redirect
        {
            get
            {
                return this.m_redirect;
            }
            set
            {
                this.m_redirect = value;
            }
        }

        /// <summary> attachextensions </summary>
        public string attachextensions
        {
            get
            {
                return this.m_attachextensions;
            }
            set
            {
                this.m_attachextensions = value;
            }
        }

        /// <summary> 本版规则 </summary>
        public string rules
        {
            get
            {
                return this.m_rules;
            }
            set
            {
                this.m_rules = value;
            }
        }

        /// <summary> 主题分类 </summary>
        public string topictypes
        {
            get
            {
                return this.m_topictypes;
            }
            set
            {
                this.m_topictypes = value;
            }
        }

        /// <summary> viewperm </summary>
        public string viewperm
        {
            get
            {
                return this.m_viewperm;
            }
            set
            {
                this.m_viewperm = value;
            }
        }

        /// <summary> postperm </summary>
        public string postperm
        {
            get
            {
                return this.m_postperm;
            }
            set
            {
                this.m_postperm = value;
            }
        }

        /// <summary> replyperm </summary>
        public string replyperm
        {
            get
            {
                return this.m_replyperm;
            }
            set
            {
                this.m_replyperm = value;
            }
        }

        /// <summary> getattachperm </summary>
        public string getattachperm
        {
            get
            {
                return this.m_getattachperm;
            }
            set
            {
                this.m_getattachperm = value;
            }
        }

        /// <summary> postattachperm </summary>
        public string postattachperm
        {
            get
            {
                return this.m_postattachperm;
            }
            set
            {
                this.m_postattachperm = value;
            }
        }

        /// <summary> moderators </summary>
        public string moderators
        {
            get
            {
                return this.m_moderators;
            }
            set
            {
                this.m_moderators = value;
            }
        }

        /// <summary> 论坛描述 </summary>
        public string description
        {
            get
            {
                return this.m_description;
            }
            set
            {
                this.m_description = value;
            }
        }

        /// <summary> applytopictype </summary>
        public System.Byte applytopictype
        {
            get
            {
                return this.m_applytopictype;
            }
            set
            {
                this.m_applytopictype = value;
            }
        }

        /// <summary> postbytopictype </summary>
        public System.Byte postbytopictype
        {
            get
            {
                return this.m_postbytopictype;
            }
            set
            {
                this.m_postbytopictype = value;
            }
        }

        /// <summary> viewbytopictype </summary>
        public System.Byte viewbytopictype
        {
            get
            {
                return this.m_viewbytopictype;
            }
            set
            {
                this.m_viewbytopictype = value;
            }
        }

        /// <summary> topictypeprefix </summary>
        public System.Byte topictypeprefix
        {
            get
            {
                return this.m_topictypeprefix;
            }
            set
            {
                this.m_topictypeprefix = value;
            }
        }

        /// <summary> permuserlist </summary>
        public string permuserlist
        {
            get
            {
                return this.m_permuserlist;
            }
            set
            {
                this.m_permuserlist = value;
            }
        }
    }
}
