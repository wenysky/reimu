using System;

namespace NConvert.Entity
{
    /// <summary>
    /// 家园信息分类表
    /// </summary>
    public class HomeClassInfo
    {
        public int classid { get; set; }
        /// <summary>
        /// char 40
        /// </summary>
        public string classname { get; set; }
        public int uid { get; set; }
        public int dateline { get; set; }
    }
}
