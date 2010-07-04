using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;
using NConvert.Entity;
using Yuwen.Tools.Data;

namespace NConvert.lybbs70_dnt31
{
    public partial class Provider : IProvider
    {
        #region IProvider 成员


        public int GetPollsRecordCount()
        {
            return 0;
        }

        public List<Polls> GetPollList(int pagei)
        {
            return null;
        }

        #endregion
    }
}
