using System;
using System.Collections.Generic;
using System.Text;

namespace NConvert.Entity
{
    public class Polls
    {
        private int m_tid;

        private System.Byte m_polltype;

        private short m_itemcount;

        private string m_itemnamelist;

        private string m_itemvaluelist;

        private string m_usernamelist;

        private System.DateTime m_enddatetime = DateTime.MinValue;

        private int m_userid;

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

        /// <summary> polltype </summary>
        public System.Byte polltype
        {
            get
            {
                return this.m_polltype;
            }
            set
            {
                this.m_polltype = value;
            }
        }

        /// <summary> itemcount </summary>
        public short itemcount
        {
            get
            {
                return this.m_itemcount;
            }
            set
            {
                this.m_itemcount = value;
            }
        }

        /// <summary> itemnamelist </summary>
        public string itemnamelist
        {
            get
            {
                return this.m_itemnamelist;
            }
            set
            {
                this.m_itemnamelist = value;
            }
        }

        /// <summary> itemvaluelist </summary>
        public string itemvaluelist
        {
            get
            {
                return this.m_itemvaluelist;
            }
            set
            {
                this.m_itemvaluelist = value;
            }
        }

        /// <summary> usernamelist </summary>
        public string usernamelist
        {
            get
            {
                return this.m_usernamelist;
            }
            set
            {
                this.m_usernamelist = value;
            }
        }

        /// <summary> enddatetime </summary>
        public System.DateTime enddatetime
        {
            get
            {
                return this.m_enddatetime;
            }
            set
            {
                this.m_enddatetime = value;
            }
        }

        /// <summary> userid </summary>
        public int userid
        {
            get
            {
                return this.m_userid;
            }
            set
            {
                this.m_userid = value;
            }
        }
    }
}
