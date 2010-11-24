using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;
using NConvert.Entity;

namespace NConvert.lybbs70_dnt31
{
    public partial class Provider : IProvider
    {
        #region IProvider 成员


        public int GetTopicTypesRecordCount()
        {
            return 0;
        }

        public List<TopicTypes> GetTopicTypeList()
        {
            return new List<TopicTypes>();
        }

        public int GetForumLinksRecordCount()
        {
            return 0;
        }

        public List<ForumLinks> GetForumLinkList()
        {
            return new List<ForumLinks>();
        }

        #endregion
    }
}
