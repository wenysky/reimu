using System;
using System.Collections.Generic;
using System.Text;

namespace NConvert.Entity
{
    /// <summary>
    /// 相册系统栏目
    /// </summary>
    public class AlbumCategoryInfo
    {
        public int catid { get; set; }
        /// <summary>
        /// 上级栏目id
        /// </summary>
        public int upid { get; set; }
        /// <summary>
        /// char 255 标题
        /// </summary>
        public string catname { get; set; }
        /// <summary>
        /// 相册数
        /// </summary>
        public int num { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int displayorder { get; set; }
    }
}
