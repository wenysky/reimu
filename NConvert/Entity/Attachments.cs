using System;
using System.Collections.Generic;
using System.Text;

namespace NConvert.Entity
{
    public class Attachments
    {
        public int aid { get; set; }
        public int tid { get; set; }
        public int pid { get; set; }
        public int width { get; set; }
        public int dateline { get; set; }
        public int readperm { get; set; }
        public int price { get; set; }
        /// <summary>
        /// 100 原文件名
        /// </summary>
        public string filename { get; set; }
        /// <summary>
        /// 50
        /// </summary>
        public string filetype { get; set; }
        public int filesize { get; set; }
        /// <summary>
        /// 100 服务器路径
        /// </summary>
        public string attachment { get; set; }
        public int downloads { get; set; }
        public int isimage { get; set; }
        public int uid { get; set; }
        public int thumb { get; set; }
        public int remote { get; set; }
        public int picid { get; set; }
        public int description { get; set; }
    }
}
