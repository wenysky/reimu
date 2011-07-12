using System;
using System.Collections.Generic;
using System.Text;

namespace NConvert.Entity
{
    public class AlbumPicInfo
    {
        public int picid { get; set; }
        public int albumid { get; set; }
        public int uid { get; set; }
        /// <summary>
        /// char 15
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        public int dateline { get; set; }
        /// <summary>
        /// 上传IP char255
        /// </summary>
        public string postip { get; set; }
        /// <summary>
        /// 图片文件名 char255
        /// </summary>
        public string filename { get; set; }
        /// <summary>
        /// 图片标题 char255
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 图片类型（jpg 扩展名不带.号）char255
        /// </summary>
        public string type { get; set; }
        public int size { get; set; }
        /// <summary>
        /// 图片的物理相对路径 char255
        /// </summary>
        public string filepath { get; set; }
        /// <summary>
        /// 是否有缩略图
        /// </summary>
        public int thumb { get; set; }
        /// <summary>
        /// 是否有远程图片
        /// </summary>
        public int remote { get; set; }
        /// <summary>
        /// 热度
        /// </summary>
        public int hot { get; set; }
        public int sharetimes { get; set; }
        public int click1 { get; set; }
        public int click2 { get; set; }
        public int click3 { get; set; }
        public int click4 { get; set; }
        public int click5 { get; set; }
        public int click6 { get; set; }
        public int click7 { get; set; }
        public int click8 { get; set; }
        /// <summary>
        /// 道具
        /// </summary>
        public int magicframe { get; set; }
        public int status { get; set; }
    }
}
