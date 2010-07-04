using System;

namespace NConvert.Entity
{
    public class ShortUsers
    {
        private int m_uid=0;

        private string m_username="";

        private string m_nickname="";

        private string m_password="";

        private string m_secques="";

        private int m_spaceid=0;

        private int m_gender=0;

        private int m_adminid=0;

        private short m_groupid=0;

        private int m_groupexpiry=0;

        private string m_extgroupids="";

        private string m_regip="";

        private System.DateTime m_joindate = DateTime.Now;

        private string m_lastip="";

        private System.DateTime m_lastvisit = DateTime.Now;

        private System.DateTime m_lastactivity = DateTime.Now;

        private System.DateTime m_lastpost = DateTime.Now;

        private int m_lastpostid=0;

        private string m_lastposttitle="";

        private int m_posts=0;

        private short m_digestposts=0;

        private int m_oltime=0;

        private int m_pageviews = 0;

        private System.Decimal m_credits = 0;

        private System.Decimal m_extcredits1 = 0;

        private System.Decimal m_extcredits2 = 0;

        private System.Decimal m_extcredits3 = 0;

        private System.Decimal m_extcredits4 = 0;

        private System.Decimal m_extcredits5 = 0;

        private System.Decimal m_extcredits6 = 0;

        private System.Decimal m_extcredits7 = 0;

        private System.Decimal m_extcredits8 = 0;

        private int m_avatarshowid = 0;

        private string m_email = "";

        private string m_bday = "";

        private int m_sigstatus = 1;

        private int m_tpp = 0;

        private int m_ppp = 0;

        private short m_templateid = 0;

        private int m_pmsound = 1;

        private int m_showemail = 1;

        private int m_invisible = 0;

        private int m_newpm = 0;

        private int m_newpmcount = 0;

        private int m_accessmasks = 0;

        private int m_onlinestate = 0;

        private int m_newsletter = 7;


        /// <summary>uid </summary>
        public int uid
        {
            get
            {
                return this.m_uid;
            }
            set
            {
                this.m_uid = value;
            }
        }

        /// <summary>username </summary>
        public string username
        {
            get
            {
                return this.m_username;
            }
            set
            {
                this.m_username = value;
            }
        }

        /// <summary>nickname </summary>
        public string nickname
        {
            get
            {
                return this.m_nickname;
            }
            set
            {
                this.m_nickname = value;
            }
        }

        /// <summary>password </summary>
        public string password
        {
            get
            {
                return this.m_password.ToLower();
            }
            set
            {
                this.m_password = value;
            }
        }

        /// <summary>secques </summary>
        public string secques
        {
            get
            {
                return this.m_secques;
            }
            set
            {
                this.m_secques = value;
            }
        }

        /// <summary>spaceid </summary>
        public int spaceid
        {
            get
            {
                return this.m_spaceid;
            }
            set
            {
                this.m_spaceid = value;
            }
        }

        /// <summary>gender </summary>
        public int gender
        {
            get
            {
                return this.m_gender;
            }
            set
            {
                this.m_gender = value;
            }
        }

        /// <summary>adminid </summary>
        public int adminid
        {
            get
            {
                return this.m_adminid;
            }
            set
            {
                this.m_adminid = value;
            }
        }

        /// <summary>groupid </summary>
        public short groupid
        {
            get
            {
                return this.m_groupid;
            }
            set
            {
                this.m_groupid = value;
            }
        }

        /// <summary>groupexpiry </summary>
        public int groupexpiry
        {
            get
            {
                return this.m_groupexpiry;
            }
            set
            {
                this.m_groupexpiry = value;
            }
        }

        /// <summary>extgroupids </summary>
        public string extgroupids
        {
            get
            {
                return this.m_extgroupids;
            }
            set
            {
                this.m_extgroupids = value;
            }
        }

        /// <summary>regip </summary>
        public string regip
        {
            get
            {
                return this.m_regip;
            }
            set
            {
                this.m_regip = value;
            }
        }

        /// <summary>joindate </summary>
        public System.DateTime joindate
        {
            get
            {
                return this.m_joindate;
            }
            set
            {
                this.m_joindate = value;
            }
        }

        /// <summary>lastip </summary>
        public string lastip
        {
            get
            {
                return this.m_lastip;
            }
            set
            {
                this.m_lastip = value;
            }
        }

        /// <summary>lastvisit </summary>
        public System.DateTime lastvisit
        {
            get
            {
                return this.m_lastvisit;
            }
            set
            {
                this.m_lastvisit = value;
            }
        }

        /// <summary>lastactivity </summary>
        public System.DateTime lastactivity
        {
            get
            {
                return this.m_lastactivity;
            }
            set
            {
                this.m_lastactivity = value;
            }
        }

        /// <summary>lastpost </summary>
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

        /// <summary>lastpostid </summary>
        public int lastpostid
        {
            get
            {
                return this.m_lastpostid;
            }
            set
            {
                this.m_lastpostid = value;
            }
        }

        /// <summary>lastposttitle </summary>
        public string lastposttitle
        {
            get
            {
                return this.m_lastposttitle;
            }
            set
            {
                this.m_lastposttitle = value;
            }
        }

        /// <summary>posts </summary>
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

        /// <summary>digestposts </summary>
        public short digestposts
        {
            get
            {
                return this.m_digestposts;
            }
            set
            {
                this.m_digestposts = value;
            }
        }

        /// <summary>oltime </summary>
        public int oltime
        {
            get
            {
                return this.m_oltime;
            }
            set
            {
                this.m_oltime = value;
            }
        }

        /// <summary>pageviews </summary>
        public int pageviews
        {
            get
            {
                return this.m_pageviews;
            }
            set
            {
                this.m_pageviews = value;
            }
        }

        /// <summary>credits </summary>
        public System.Decimal credits
        {
            get
            {
                return this.m_credits;
            }
            set
            {
                this.m_credits = value;
            }
        }

        /// <summary>extcredits1 </summary>
        public System.Decimal extcredits1
        {
            get
            {
                return this.m_extcredits1;
            }
            set
            {
                this.m_extcredits1 = value;
            }
        }

        /// <summary>extcredits2 </summary>
        public System.Decimal extcredits2
        {
            get
            {
                return this.m_extcredits2;
            }
            set
            {
                this.m_extcredits2 = value;
            }
        }

        /// <summary>extcredits3 </summary>
        public System.Decimal extcredits3
        {
            get
            {
                return this.m_extcredits3;
            }
            set
            {
                this.m_extcredits3 = value;
            }
        }

        /// <summary>extcredits4 </summary>
        public System.Decimal extcredits4
        {
            get
            {
                return this.m_extcredits4;
            }
            set
            {
                this.m_extcredits4 = value;
            }
        }

        /// <summary>extcredits5 </summary>
        public System.Decimal extcredits5
        {
            get
            {
                return this.m_extcredits5;
            }
            set
            {
                this.m_extcredits5 = value;
            }
        }

        /// <summary>extcredits6 </summary>
        public System.Decimal extcredits6
        {
            get
            {
                return this.m_extcredits6;
            }
            set
            {
                this.m_extcredits6 = value;
            }
        }

        /// <summary>extcredits7 </summary>
        public System.Decimal extcredits7
        {
            get
            {
                return this.m_extcredits7;
            }
            set
            {
                this.m_extcredits7 = value;
            }
        }

        /// <summary>extcredits8 </summary>
        public System.Decimal extcredits8
        {
            get
            {
                return this.m_extcredits8;
            }
            set
            {
                this.m_extcredits8 = value;
            }
        }

        /// <summary>avatarshowid </summary>
        public int avatarshowid
        {
            get
            {
                return this.m_avatarshowid;
            }
            set
            {
                this.m_avatarshowid = value;
            }
        }

        /// <summary>email </summary>
        public string email
        {
            get
            {
                return this.m_email;
            }
            set
            {
                this.m_email = value;
            }
        }

        /// <summary>bday </summary>
        public string bday
        {
            get
            {
                return this.m_bday;
            }
            set
            {
                this.m_bday = value;
            }
        }

        /// <summary>sigstatus </summary>
        public int sigstatus
        {
            get
            {
                return this.m_sigstatus;
            }
            set
            {
                this.m_sigstatus = value;
            }
        }

        /// <summary>tpp </summary>
        public int tpp
        {
            get
            {
                return this.m_tpp;
            }
            set
            {
                this.m_tpp = value;
            }
        }

        /// <summary>ppp </summary>
        public int ppp
        {
            get
            {
                return this.m_ppp;
            }
            set
            {
                this.m_ppp = value;
            }
        }

        /// <summary>templateid </summary>
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

        /// <summary>pmsound </summary>
        public int pmsound
        {
            get
            {
                return this.m_pmsound;
            }
            set
            {
                this.m_pmsound = value;
            }
        }

        /// <summary>showemail </summary>
        public int showemail
        {
            get
            {
                return this.m_showemail;
            }
            set
            {
                this.m_showemail = value;
            }
        }

        /// <summary>invisible </summary>
        public int invisible
        {
            get
            {
                return this.m_invisible;
            }
            set
            {
                this.m_invisible = value;
            }
        }

        /// <summary>newpm </summary>
        public int newpm
        {
            get
            {
                return this.m_newpm;
            }
            set
            {
                this.m_newpm = value;
            }
        }

        /// <summary>newpmcount </summary>
        public int newpmcount
        {
            get
            {
                return this.m_newpmcount;
            }
            set
            {
                this.m_newpmcount = value;
            }
        }

        /// <summary>accessmasks </summary>
        public int accessmasks
        {
            get
            {
                return this.m_accessmasks;
            }
            set
            {
                this.m_accessmasks = value;
            }
        }

        /// <summary>onlinestate </summary>
        public int onlinestate
        {
            get
            {
                return this.m_onlinestate;
            }
            set
            {
                this.m_onlinestate = value;
            }
        }

        /// <summary>newsletter </summary>
        public int newsletter
        {
            get
            {
                return this.m_newsletter;
            }
            set
            {
                this.m_newsletter = value;
            }
        }
    }


    public class Users:ShortUsers
    {
        private string m_website="";

        private string m_icq="";

        private string m_qq = "";

        private string m_yahoo = "";

        private string m_msn = "";

        private string m_skype = "";

        private string m_location = "";

        private string m_customstatus = "";

        private string m_avatar = "avatars\\common\\0.gif";

        private int m_avatarwidth=60;

        private int m_avatarheight=60;

        private string m_medals= "";

        private string m_bio= "";

        private string m_signature= "";

        private string m_sightml= "";

        private string m_authstr= "";

        private System.DateTime m_authtime = DateTime.Now;

        private System.Byte m_authflag=0;

        private string m_realname= "";

        private string m_idcard= "";

        private string m_mobile= "";

        private string m_phone= "";

        /// <summary>website </summary>
        public string website
        {
            get
            {
                return this.m_website;
            }
            set
            {
                this.m_website = value;
            }
        }

        /// <summary>icq </summary>
        public string icq
        {
            get
            {
                return this.m_icq;
            }
            set
            {
                this.m_icq = value;
            }
        }

        /// <summary>qq </summary>
        public string qq
        {
            get
            {
                return this.m_qq;
            }
            set
            {
                this.m_qq = value;
            }
        }

        /// <summary>yahoo </summary>
        public string yahoo
        {
            get
            {
                return this.m_yahoo;
            }
            set
            {
                this.m_yahoo = value;
            }
        }

        /// <summary>msn </summary>
        public string msn
        {
            get
            {
                return this.m_msn;
            }
            set
            {
                this.m_msn = value;
            }
        }

        /// <summary>skype </summary>
        public string skype
        {
            get
            {
                return this.m_skype;
            }
            set
            {
                this.m_skype = value;
            }
        }

        /// <summary>
        /// 来自
        /// </summary>
        public string location
        {
            get
            {
                return this.m_location;
            }
            set
            {
                this.m_location = value;
            }
        }

        /// <summary>
        /// 自定义头衔
        /// </summary>
        public string customstatus
        {
            get
            {
                return this.m_customstatus;
            }
            set
            {
                this.m_customstatus = value;
            }
        }

        /// <summary>avatar </summary>
        public string avatar
        {
            get
            {
                return this.m_avatar;
            }
            set
            {
                this.m_avatar = value;
            }
        }

        /// <summary>avatarwidth </summary>
        public int avatarwidth
        {
            get
            {
                return this.m_avatarwidth;
            }
            set
            {
                this.m_avatarwidth = value;
            }
        }

        /// <summary>avatarheight </summary>
        public int avatarheight
        {
            get
            {
                return this.m_avatarheight;
            }
            set
            {
                this.m_avatarheight = value;
            }
        }

        /// <summary>medals </summary>
        public string medals
        {
            get
            {
                return this.m_medals;
            }
            set
            {
                this.m_medals = value;
            }
        }

        /// <summary>个人简介</summary>
        public string bio
        {
            get
            {
                return this.m_bio;
            }
            set
            {
                this.m_bio = value;
            }
        }

        /// <summary>个人签名</summary>
        public string signature
        {
            get
            {
                return this.m_signature;
            }
            set
            {
                this.m_signature = value;
            }
        }

        /// <summary>解析后的签名</summary>
        public string sightml
        {
            get
            {
                return this.m_sightml;
            }
            set
            {
                this.m_sightml = value;
            }
        }

        /// <summary>验证码</summary>
        public string authstr
        {
            get
            {
                return this.m_authstr;
            }
            set
            {
                this.m_authstr = value;
            }
        }

        /// <summary>验证码生成日期</summary>
        public System.DateTime authtime
        {
            get
            {
                return this.m_authtime;
            }
            set
            {
                this.m_authtime = value;
            }
        }

        /// <summary>验证码使用标志(0 未使用,1 用户邮箱验证及用户信息激活, 2 用户密码找回)</summary>
        public System.Byte authflag
        {
            get
            {
                return this.m_authflag;
            }
            set
            {
                this.m_authflag = value;
            }
        }

        /// <summary>realname </summary>
        public string realname
        {
            get
            {
                return this.m_realname;
            }
            set
            {
                this.m_realname = value;
            }
        }

        /// <summary>idcard </summary>
        public string idcard
        {
            get
            {
                return this.m_idcard;
            }
            set
            {
                this.m_idcard = value;
            }
        }

        /// <summary>mobile </summary>
        public string mobile
        {
            get
            {
                return this.m_mobile;
            }
            set
            {
                this.m_mobile = value;
            }
        }

        /// <summary>phone </summary>
        public string phone
        {
            get
            {
                return this.m_phone;
            }
            set
            {
                this.m_phone = value;
            }
        }
    }
}
