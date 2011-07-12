using System;

namespace NConvert.Entity
{
    /// <summary>
    /// 博客推荐图片(新表) 对应kexue_blogpic
    /// </summary>
    public class IndexRecomandBlogPicInfo
    {
        public int rpid { get; set; }
        /// <summary>
        /// char120 名称
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// char120 图片地址
        /// </summary>
        public string picsrc { get; set; }
        /// <summary>
        /// char120链接地址
        /// </summary>
        public string linksrc { get; set; }
        /// <summary>
        /// 推断：0不显示，1左上，2左中，3推荐博主（对应ifhead）
        /// </summary>
        public int pictype { get; set; }
        public int userid { get; set; }
        /// <summary>
        /// char255 推荐图片说明
        /// </summary>
        public string readme { get; set; }
    }
}
