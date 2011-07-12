using System;

namespace NConvert.Entity
{
    /// <summary>
    /// 博文专题
    /// </summary>
    public class BlogSubjectInfo
    {
        public int sbid { get; set; }
        /// <summary>
        /// char 50
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// char 1000
        /// </summary>
        public string content { get; set; }
        public int sbtype { get; set; }
        public int sborder { get; set; }
        /// <summary>
        /// char50
        /// </summary>
        public string logo { get; set; }
        public int updatetime { get; set; }
    }
}
