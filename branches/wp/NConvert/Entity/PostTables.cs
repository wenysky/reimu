using System;

namespace NConvert.Entity
{
    public class PostTables
    {
        private int m_id;

        public int id
        {
            get { return m_id; }
            set { m_id = value; }
        }
        private DateTime m_createdatetime;

        public DateTime createdatetime
        {
            get { return m_createdatetime; }
            set { m_createdatetime = value; }
        }
        private string m_description;

        public string description
        {
            get { return m_description; }
            set { m_description = value; }
        }
        private int m_mintid;

        public int mintid
        {
            get { return m_mintid; }
            set { m_mintid = value; }
        }
        private int m_maxtid;

        public int maxtid
        {
            get { return m_maxtid; }
            set { m_maxtid = value; }
        }
    }
}
