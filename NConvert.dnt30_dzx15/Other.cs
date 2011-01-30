using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;
using NConvert.Entity;

namespace NConvert.dnt30_dzx15
{
    public partial class Provider : IProvider
    {
        #region IProvider 成员

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
