using System;
using System.Collections.Generic;
using System.Text;

namespace NConvert.Entity
{
    public class Attachments
    {
        private int m_aid = -1;

        private int m_uid = -1;

        private int m_tid = -1;

        private int m_pid = -1;

        private System.DateTime m_postdatetime = DateTime.Now;

        private int m_readperm = 0;

        private string m_filename = "储存文件名";

        private string m_description = "描述";

        private string m_filetype = "类型";

        private int m_filesize = 0;

        private string m_attachment = "附件原始文件名";

        private int m_downloads = 0;



        /// <summary> aid </summary>
        public int aid
        {
            get
            {
                return this.m_aid;
            }
            set
            {
                this.m_aid = value;
            }
        }

        /// <summary> uid </summary>
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

        /// <summary> filename </summary>
        public string filename
        {
            get
            {
                return this.m_filename;
            }
            set
            {
                this.m_filename = value;
            }
        }

        /// <summary> description </summary>
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

        /// <summary> filetype </summary>
        public string filetype
        {
            get
            {
                return this.m_filetype;
            }
            set
            {
                this.m_filetype = value;
            }
        }

        /// <summary> filesize </summary>
        public int filesize
        {
            get
            {
                return this.m_filesize;
            }
            set
            {
                this.m_filesize = value;
            }
        }

        /// <summary> attachment </summary>
        public string attachment
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

        /// <summary> downloads </summary>
        public int downloads
        {
            get
            {
                return this.m_downloads;
            }
            set
            {
                this.m_downloads = value;
            }
        }
    }
}
