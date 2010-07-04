using System;
using System.Collections.Generic;
using System.Text;

namespace NConvert.Entity
{
    public class Posts
    {
        private int m_pid=0;
        
        private int m_fid=0;
        
        private int m_tid=0;
        
        private int m_parentid=0;
        
        private int m_layer=0;
        
        private string m_poster="";
        
        private int m_posterid=-1;
        
        private string m_title="标题";
        
        private System.DateTime m_postdatetime = DateTime.Now;
        
        private string m_message="内容";
        
        private string m_ip="127.0.0.1";
        
        private string m_lastedit="";
        
        private int m_invisible=0;
        
        private int m_usesig=1;
        
        private int m_htmlon=1;
        
        private int m_smileyoff=0;
        
        private int m_parseurloff=0;
        
        private int m_bbcodeoff=0;
        
        private int m_attachment=1;
        
        private int m_rate=0;
        
        private int m_ratetimes=0;
        
        
        /// <summary> pid </summary>
        public int pid
        {
            get
            {
                return this.m_pid;
            }
            set
            {
                this.m_pid = value;
            }
        }
        
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

        /// <summary> 父帖ID,用来处理树形，一般情况下和pid相同 </summary>
        public int parentid
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

        /// <summary> 帖子所处层次(主题贴为0) </summary>
        public int layer
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
        
        /// <summary> message </summary>
        public string message
        {
            get
            {
                return this.m_message;
            }
            set
            {
                this.m_message = value;
            }
        }
        
        /// <summary> ip </summary>
        public string ip
        {
            get
            {
                return this.m_ip;
            }
            set
            {
                this.m_ip = value;
            }
        }
        
        /// <summary> lastedit </summary>
        public string lastedit
        {
            get
            {
                return this.m_lastedit;
            }
            set
            {
                this.m_lastedit = value;
            }
        }

        /// <summary> 是否隐藏, 如果未通过审核则为隐藏 </summary>
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
        
        /// <summary> usesig </summary>
        public int usesig
        {
            get
            {
                return this.m_usesig;
            }
            set
            {
                this.m_usesig = value;
            }
        }
        
        /// <summary> htmlon </summary>
        public int htmlon
        {
            get
            {
                return this.m_htmlon;
            }
            set
            {
                this.m_htmlon = value;
            }
        }

        /// <summary> 是否关闭smaile表情 </summary>
        public int smileyoff
        {
            get
            {
                return this.m_smileyoff;
            }
            set
            {
                this.m_smileyoff = value;
            }
        }

        /// <summary> 是否关闭url自动解析 </summary>
        public int parseurloff
        {
            get
            {
                return this.m_parseurloff;
            }
            set
            {
                this.m_parseurloff = value;
            }
        }
        
        /// <summary> 关闭 Discuz!NT 代码支持 </summary>
        public int bbcodeoff
        {
            get
            {
                return this.m_bbcodeoff;
            }
            set
            {
                this.m_bbcodeoff = value;
            }
        }
        
        /// <summary> 是否含有附件 </summary>
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
        
        /// <summary> 评分分数 </summary>
        public int rate
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
        
        /// <summary> 评分次数 </summary>
        public int ratetimes
        {
            get
            {
                return this.m_ratetimes;
            }
            set
            {
                this.m_ratetimes = value;
            }
        }
    }
}
