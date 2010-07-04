using System;

namespace NConvert.Entity
{
    public class Topics
    {
        private int m_tid = 0;

        private short m_fid = 1;

        private System.Byte m_iconid = 0;

        private int m_typeid = 0;

        private int m_readperm = 0;

        private short m_price = 0;

        private string m_poster = "发布人";

        private int m_posterid = 0;

        private string m_title = "标题";

        private System.DateTime m_postdatetime = DateTime.Now;

        private System.DateTime m_lastpost = DateTime.Now;

        private int m_lastpostid = 0;

        private string m_lastposter = "";

        private int m_lastposterid = 0;

        private int m_views = 0;

        private int m_replies = 0;

        private int m_displayorder = 0;

        private string m_highlight = "";

        private System.Byte m_digest = 0;

        private System.Byte m_rate = 0;

        private int m_hide = 0;

        private int m_poll = 0;

        private int m_attachment = 0;

        private System.Byte m_moderated = 0;

        private int m_closed = 0;

        private int m_magic = 0;

        private int m_identify = 0;



        /// <summary> tid </summary>
        public int tid
        {
            get
            {
                return this.m_tid;
            }
            set
            {
                this.m_tid = value;
            }
        }

        /// <summary> fid </summary>
        public short fid
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

        /// <summary> iconid </summary>
        public System.Byte iconid
        {
            get
            {
                return this.m_iconid;
            }
            set
            {
                this.m_iconid = value;
            }
        }

        /// <summary> typeid </summary>
        public int typeid
        {
            get
            {
                return this.m_typeid;
            }
            set
            {
                this.m_typeid = value;
            }
        }

        /// <summary> readperm </summary>
        public int readperm
        {
            get
            {
                return this.m_readperm;
            }
            set
            {
                this.m_readperm = value;
            }
        }

        /// <summary> price </summary>
        public short price
        {
            get
            {
                return this.m_price;
            }
            set
            {
                this.m_price = value;
            }
        }

        /// <summary> poster </summary>
        public string poster
        {
            get
            {
                return this.m_poster;
            }
            set
            {
                this.m_poster = value;
            }
        }

        /// <summary> posterid </summary>
        public int posterid
        {
            get
            {
                return this.m_posterid;
            }
            set
            {
                this.m_posterid = value;
            }
        }

        /// <summary> title </summary>
        public string title
        {
            get
            {
                return this.m_title;
            }
            set
            {
                this.m_title = value;
            }
        }

        /// <summary> postdatetime </summary>
        public System.DateTime postdatetime
        {
            get
            {
                return this.m_postdatetime;
            }
            set
            {
                this.m_postdatetime = value;
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

        /// <summary> lastpostid </summary>
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

        /// <summary> views </summary>
        public int views
        {
            get
            {
                return this.m_views;
            }
            set
            {
                this.m_views = value;
            }
        }

        /// <summary> replies </summary>
        public int replies
        {
            get
            {
                return this.m_replies;
            }
            set
            {
                this.m_replies = value;
            }
        }

        /// <summary> displayorder </summary>
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

        /// <summary> highlight </summary>
        public string highlight
        {
            get
            {
                return this.m_highlight;
            }
            set
            {
                this.m_highlight = value;
            }
        }

        /// <summary> digest </summary>
        public System.Byte digest
        {
            get
            {
                return this.m_digest;
            }
            set
            {
                this.m_digest = value;
            }
        }

        /// <summary> rate </summary>
        public System.Byte rate
        {
            get
            {
                return this.m_rate;
            }
            set
            {
                this.m_rate = value;
            }
        }

        /// <summary> 如果帖子(包括主题和回帖)中包含[hide]标签,则应该为1 </summary>
        public int hide
        {
            get
            {
                return this.m_hide;
            }
            set
            {
                this.m_hide = value;
            }
        }

        /// <summary> poll </summary>
        public int poll
        {
            get
            {
                return this.m_poll;
            }
            set
            {
                this.m_poll = value;
            }
        }

        /// <summary> attachment </summary>
        public int attachment
        {
            get
            {
                return this.m_attachment;
            }
            set
            {
                this.m_attachment = value;
            }
        }

        /// <summary> moderated </summary>
        public System.Byte moderated
        {
            get
            {
                return this.m_moderated;
            }
            set
            {
                this.m_moderated = value;
            }
        }

        /// <summary> closed </summary>
        public int closed
        {
            get
            {
                return this.m_closed;
            }
            set
            {
                this.m_closed = value;
            }
        }

        /// <summary> magic </summary>
        public int magic
        {
            get
            {
                return this.m_magic;
            }
            set
            {
                this.m_magic = value;
            }
        }

        /// <summary> identify </summary>
        public int identify
        {
            get
            {
                return this.m_identify;
            }
            set
            {
                this.m_identify = value;
            }
        }
    }
}
