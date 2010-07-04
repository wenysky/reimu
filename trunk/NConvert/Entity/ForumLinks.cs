using System;

namespace NConvert.Entity
{
    public class ForumLinks
    {
        private short m_id = -1;

        private int m_displayorder = 0;

        private string m_name = "名称";

        private string m_url = "http://nt.discuz.net";

        private string m_note = "";

        private string m_logo = "http://nt.discuz.net/images/logo.gif";

        /// <summary> id </summary>
        public short id
        {
            get
            {
                return this.m_id;
            }
            set
            {
                this.m_id = value;
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

        /// <summary> name </summary>
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

        /// <summary> url </summary>
        public string url
        {
            get
            {
                return this.m_url;
            }
            set
            {
                this.m_url = value;
            }
        }

        /// <summary> note </summary>
        public string note
        {
            get
            {
                return this.m_note;
            }
            set
            {
                this.m_note = value;
            }
        }

        /// <summary> logo </summary>
        public string logo
        {
            get
            {
                return this.m_logo;
            }
            set
            {
                this.m_logo = value;
            }
        }
    }
}
