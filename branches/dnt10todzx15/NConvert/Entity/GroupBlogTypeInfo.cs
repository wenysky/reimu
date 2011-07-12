using System;
using System.Collections.Generic;
using System.Text;

namespace NConvert.Entity
{
    /// <summary>
    /// 额外的
    /// </summary>
    public class GroupBlogTypeInfo
    {
        public int gtid { get; set; }
        /// <summary>
        /// 分类名称 char 60
        /// </summary>
        public string typename { get; set; }
        /// <summary>
        /// 群组id[group_username]转化ID
        /// </summary>
        public int groupid { get; set; }
        /// <summary>
        /// 创建时间 X
        /// </summary>
        public int createtime { get; set; }
    }
}
