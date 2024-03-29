﻿using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;

namespace NConvert.cvc10_dnt21202
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
            return "CVC1.0(SqlServer)";
        }

        public string GetDllDescription()
        {
            return @"CVC1.0(SqlServer)转Discuz!NT2.1.202(SqlServer)
拷贝uploadfaces目录到 /avatars/upload/ 目录下.(头像目录变为 avatars\upload\uploadfaces\xxx.gif)

拷贝uploadfiles目录下面的文件到 /upload/ 目录下(附件目录变为 upload\2008\5\xxx.zip).

恢复数据库,一般置顶贴都会消失,解决方法为:随意找到一个普通主题,置顶之后就可以回复其他帖子的置顶了.";
        }

        public string[] GetSupportSrcDbType()
        {
            return new string[3] { "SqlServer", "Mysql", "Access"};
        }

        public string[] GetSupportTargetDbType()
        {
            return new string[1] { "SqlServer" };
        }

        #endregion
    }
}
