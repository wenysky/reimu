using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;

namespace NConvert.lybbs70_dnt31
{
    public partial class Provider : IProvider
    {
        #region IProvider 成员


        public int GetTopicTypesRecordCount()
        {
            return 0;
        }

        public List<NConvert.Entity.TopicTypes> GetTopicTypeList()
        {
            throw new NotImplementedException();
        }

        public int GetForumLinksRecordCount()
        {
            return 0;
        }

        public List<NConvert.Entity.ForumLinks> GetForumLinkList()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
