using System;
using System.Collections.Generic;
using System.Text;

namespace NConvert.Entity
{
    public class DllInfo
    {
        private string m_displayname;

        public string Displayname
        {
            get { return m_displayname; }
            set { m_displayname = value; }
        }
        private string m_classname;

        public string Classname
        {
            get { return m_classname; }
            set { m_classname = value; }
        }
        private string m_description;

        public string Description
        {
            get { return m_description; }
            set { m_description = value; }
        }
    }
}
