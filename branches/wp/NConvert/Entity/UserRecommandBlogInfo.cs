using System;

namespace NConvert.Entity
{
    /// <summary>
    /// 博主推荐其他博主的文章（新表）[tuijianren]
    /// </summary>
    public class UserRecommandBlogInfo
    {
        /// <summary>
        /// 推荐id 
        /// </summary>
        public int rid { get; set; }
        /// <summary>
        /// 日志作者id[homeurl]
        /// </summary>
        public int uid { get; set; }
        /// <summary>
        /// 被推荐日志id[id]
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 推荐类型 char20（统一置为“blogid”）
        /// </summary>
        public string idtype { get; set; }
        /// <summary>
        /// 推荐者uid[tuijianren] |分开格式 uid#username
        /// </summary>
        public int authorid { get; set; }
        /// <summary>
        /// 推荐者username char15 [tuijianren] |分开格式 uid#username
        /// </summary>
        public string author { get; set; }
        /// <summary>
        /// char20 X
        /// </summary>
        public string ip { get; set; }
        /// <summary>
        /// X
        /// </summary>
        public int dateline { get; set; }
    }
}
