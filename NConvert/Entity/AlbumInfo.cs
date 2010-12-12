using System;
using System.Collections.Generic;
using System.Text;

namespace NConvert.Entity
{
    public class AlbumInfo
    {
        public int albumid { get; set; }
        /// <summary>
        /// char 50
        /// </summary>
        public string albumname { get; set; }
        public int catid { get; set; }
        public int uid { get; set; }
        /// <summary>
        /// char 15
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 建立日期
        /// </summary>
        public int dateline { get; set; }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public int updatetime { get; set; }
        public int picnum { get; set; }
        /// <summary>
        /// char 60 封面图片
        /// </summary>
        public string pic { get; set; }
        /// <summary>
        /// 相册内是否有图片
        /// </summary>
        public int picflag { get; set; }
        /// <summary>
        /// 隐私设置 0全站 1 全部好友 2指定好友 3自己 4密码
        /// </summary>
        public int friend { get; set; }
        /// <summary>
        /// char 10
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 允许查看的uid，用“m”间隔
        /// </summary>
        public string target_ids { get; set; }
        public int favtimes { get; set; }
        public int sharetimes { get; set; }
    }
}
