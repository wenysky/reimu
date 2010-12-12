using System;
using System.Collections.Generic;
using System.Text;

namespace NConvert.Entity
{
    /// <summary>
    /// 相册系统栏目
    /// </summary>
    public class BlogFavoriteInfo
    {
        public int favid { get; set; }
        /// <summary>
        /// 收藏人uid
        /// </summary>
        public int uid { get; set; }
        /// <summary>
        /// 收藏的对象id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// char 255 收藏的对象类型 tid blogid albumid
        /// </summary>
        public string idtype { get; set; }
        /// <summary>
        /// 收藏对象的作者的空间uid（帖子的话，就是0）
        /// </summary>
        public int spaceuid { get; set; }
        /// <summary>
        /// char 255 收藏标题（默认为对象的标题）
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string description { get; set; }
        public int dateline { get; set; }
    }
}
