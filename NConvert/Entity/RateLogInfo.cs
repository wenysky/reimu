using System;
using System.Collections.Generic;
using System.Text;

namespace NConvert.Entity
{
    public class RateLogInfo
    {
        public int pid { get; set; }
        public int uid { get; set; }
        /// <summary>
        /// char 15
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 评分字段
        /// </summary>
        public int extcredits { get; set; }
        public int dateline { get; set; }
        /// <summary>
        /// 分数
        /// </summary>
        public int score { get; set; }
        /// <summary>
        /// char 40 评分理由
        /// </summary>
        public string reason { get; set; }
    }
}
