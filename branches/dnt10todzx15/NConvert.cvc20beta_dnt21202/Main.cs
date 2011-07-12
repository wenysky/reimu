using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;

namespace NConvert.cvc20beta_dnt21202
{
    public partial class Provider:IProvider
    {
        #region IProvider 成员

        public int GetRecordCount()
        {
            return 1;
        }

        public string GetDllDisplayName()
        {
            return "CVC2.0Beta(SqlServer)";
        }

        public string GetDllDescription()
        {
            return @"CVC2.0Beta转Discuz!NT2.5.0(SqlServer)
拷贝uploadfaces目录(包含uploadfaces)到 /avatars/upload/ 目录下.(头像目录变为 avatars\upload\uploadfaces\xxx.gif)

拷贝uploadfiles目录下面的文件到 /upload/ 目录下(附件目录变为 upload\2008\5\xxx.zip).

恢复数据库,一般置顶贴都会消失,解决方法为:随意找到一个普通主题,置顶之后就其他帖子的置顶就恢复了.";
        }

        public string[] GetSupportSrcDbType()
        {
            return new string[2] { "SqlServer", "Access" };
        }

        public string[] GetSupportTargetDbType()
        {
            return new string[1] { "SqlServer" };
        }
        #endregion
    }
}
