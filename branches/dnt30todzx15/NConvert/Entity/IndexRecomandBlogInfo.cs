using System;

namespace NConvert.Entity
{
    /// <summary>
    /// 博主推荐其他博主的文章（新表）[tuijianren]
    /// </summary>
    public class IndexRecomandBlogInfo
    {
        public int rfid { get; set; }
        public int blogid { get; set; }
        /// <summary>
        /// char 200
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// char255
        /// </summary>
        public string content { get; set; }
        public int status { get; set; }
        public int recommendtime { get; set; }
        public int bloguid { get; set; }
        /// <summary>
        /// char30
        /// </summary>
        public string relateblog { get; set; }
        /// <summary>
        /// char 120 标题连接
        /// </summary>
        public string titlelink { get; set; }
    }
}
