using System;
using System.Collections.Generic;
using System.Text;

namespace NConvert.Entity
{
    public class Pms
    {
        private int m_pmid = -1;

        private string m_msgfrom = "发件人";

        private int m_msgfromid = -1;

        private string m_msgto = "收件人";

        private int m_msgtoid = -1;

        private short m_folder = 0;

        private int m_new = 0;

        private string m_subject = "标题";

        private System.DateTime m_postdatetime = DateTime.Now;

        private string m_message = "内容";


        /// <summary> pmid </summary>
        public int pmid
        {
            get
            {
                return this.m_pmid;
            }
            set
            {
                this.m_pmid = value;
            }
        }

        /// <summary> msgfrom </summary>
        public string msgfrom
        {
            get
            {
                return this.m_msgfrom;
            }
            set
            {
                this.m_msgfrom = value;
            }
        }

        /// <summary> msgfromid </summary>
        public int msgfromid
        {
            get
            {
                return this.m_msgfromid;
            }
            set
            {
                this.m_msgfromid = value;
            }
        }

        /// <summary> msgto </summary>
        public string msgto
        {
            get
            {
                return this.m_msgto;
            }
            set
            {
                this.m_msgto = value;
            }
        }

        /// <summary> msgtoid </summary>
        public int msgtoid
        {
            get
            {
                return this.m_msgtoid;
            }
            set
            {
                this.m_msgtoid = value;
            }
        }

        /// <summary> folder </summary>
        public short folder
        {
            get
            {
                return this.m_folder;
            }
            set
            {
                this.m_folder = value;
            }
        }

        /// <summary> new </summary>
        public int newmessage
        {
            get
            {
                return this.m_new;
            }
            set
            {
                this.m_new = value;
            }
        }

        /// <summary> subject </summary>
        public string subject
        {
            get
            {
                return this.m_subject;
            }
            set
            {
                this.m_subject = value;
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
    }
}
