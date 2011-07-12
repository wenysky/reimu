using System;
using System.Collections.Generic;
using System.Text;

namespace NConvert.Entity
{
    public class CommentInfo
    {
        public int cid { get; set; }
        /// <summary>
        /// 被评论的作者
        /// </summary>
        public int uid { get; set; }
        /// <summary>
        /// 被评论的对象id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// char20 blogid日志 uid留言 picid图片
        /// </summary>
        public string idtype { get; set; }
        /// <summary>
        /// 评论者
        /// </summary>
        public int authorid { get; set; }
        /// <summary>
        /// char 15
        /// </summary>
        public string author { get; set; }
        /// <summary>
        /// char20
        /// </summary>
        public string ip { get; set; }
        public int dateline { get; set; }
        public string message { get; set; }
        public int magicflicker { get; set; }
        public int status { get; set; }
    }
}
