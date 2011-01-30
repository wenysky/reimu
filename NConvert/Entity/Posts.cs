using System;
using System.Collections.Generic;
using System.Text;

namespace NConvert.Entity
{
    public class Posts
    {
        public int pid { get; set; }
        public int fid { get; set; }
        public int tid { get; set; }
        public int first { get; set; }
        /// <summary>
        /// char15
        /// </summary>
        public string author { get; set; }
        public int authorid { get; set; }
        /// <summary>
        /// char80
        /// </summary>
        public string subject { get; set; }
        public int dateline { get; set; }
        /// <summary>
        /// text
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// char15
        /// </summary>
        public string useip { get; set; }
        /// <summary>
        /// 是否通过审核  -2为未审核
        /// </summary>
        public int invisible { get; set; }
        public int anonymous { get; set; }
        public int usesig { get; set; }
        public int htmlon { get; set; }
        public int bbcodeoff { get; set; }
        public int smileyoff { get; set; }
        public int parseurloff { get; set; }
        public int attachment { get; set; }
        public int rate { get; set; }
        public int ratetimes { get; set; }
        /// <summary>
        /// 帖子状态 1屏蔽 2警告 3屏蔽+警告
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// char255
        /// </summary>
        public string tags { get; set; }
        /// <summary>
        /// 是否存在点评
        /// </summary>
        public int comment { get; set; }
    }
}
