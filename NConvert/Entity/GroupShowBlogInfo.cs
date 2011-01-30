using System;
using System.Collections.Generic;
using System.Text;

namespace NConvert.Entity
{
    /// <summary>
    /// 额外的
    /// </summary>
    public class GroupShowBlogInfo
    {
        public int gid { get; set; }
        /// <summary>
        /// 被推送的日志ID[group_article]oldid
        /// </summary>
        public int blogid { get; set; }
        /// <summary>
        /// 推送到群组ID[group_username]转换成ID
        /// </summary>
        public int fid { get; set; }
        /// <summary>
        /// 推荐位[good]0,1
        /// </summary>
        public int commend { get; set; }
        /// <summary>
        /// 推荐人Id[username]转化ID
        /// </summary>
        public int senduser { get; set; }
        /// <summary>
        /// 是否通过[全部为1]
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 推送时间[updatetime]
        /// </summary>
        public int sendtime { get; set; }
        /// <summary>
        /// 推荐日志在群组内的分类  默认0表示未分类[nclass]
        /// </summary>
        public int grouptype { get; set; }
    }
}
