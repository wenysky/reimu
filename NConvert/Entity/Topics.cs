using System;

namespace NConvert.Entity
{
    public class Topics
    {
        public int tid { get; set; }
        public int fid { get; set; }
        public int posttableid { get; set; }
        public int typeid { get; set; }
        public int sortid { get; set; }
        public int readperm { get; set; }
        public int price { get; set; }
        /// <summary>
        /// char15
        /// </summary>
        public string author { get; set; }
        public int authorid { get; set; }
        /// <summary>
        /// char80
        /// </summary>
        public string subject { get; set; }
        /// <summary>
        /// 发表时间
        /// </summary>
        public int dateline { get; set; }
        public int lastpost { get; set; }
        /// <summary>
        /// char15
        /// </summary>
        public string lastposter { get; set; }
        public int views { get; set; }
        public int replies { get; set; }
        public int displayorder { get; set; }
        public int highlight { get; set; }
        public int digest { get; set; }
        public int rate { get; set; }
        /// <summary>
        /// 1投票 2商品 3悬赏 4活动 5辩论
        /// </summary>
        public int special { get; set; }
        public int attachment { get; set; }
        public int moderated { get; set; }
        public int closed { get; set; }
        /// <summary>
        /// 回复置顶
        /// </summary>
        public int stickreply { get; set; }
        /// <summary>
        /// 推荐指数
        /// </summary>
        public int recommends { get; set; }
        /// <summary>
        /// 支持人数
        /// </summary>
        public int recommend_add { get; set; }
        /// <summary>
        /// 反对人数
        /// </summary>
        public int recommend_sub { get; set; }
        public int heats { get; set; }
        /// <summary>
        /// 主题状态
        /// </summary>
        public int status { get; set; }
        public int isgroup { get; set; }
        public int favtimes { get; set; }
        public int sharetimes { get; set; }
        public int stamp { get; set; }
        public int icon { get; set; }
        public int pushedaid { get; set; }
        /// <summary>
        /// 推荐时间 （如果有  就表示被推荐了）
        /// </summary>
        public int recommend { get; set; }
    }
}
