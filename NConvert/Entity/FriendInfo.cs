using System;

namespace NConvert.Entity
{
    public class FriendInfo
    {
        public int uid { get; set; }
        /// <summary>
        /// 好友uid
        /// </summary>
        public int fuid { get; set; }
        /// <summary>
        /// 好友名 char 15
        /// </summary>
        public string fusername { get; set; }
        /// <summary>
        /// 好友分组
        /// </summary>
        public int gid { get; set; }
        /// <summary>
        /// 好友间的任务关系数
        /// </summary>
        public int num { get; set; }
        public int dateline { get; set; }
        /// <summary>
        /// 备注 255
        /// </summary>
        public string note { get; set; }
    }
}
