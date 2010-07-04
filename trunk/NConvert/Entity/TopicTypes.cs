using System;
using System.Collections.Generic;
using System.Text;

namespace NConvert.Entity
{
    public class TopicTypes
    {
        private int m_typeid = -1;

        private int m_displayorder = 0;

        private string m_name = "分类名";

        private string m_description = "描述";

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
    }
}
