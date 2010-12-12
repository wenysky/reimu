using System;

namespace NConvert.Entity
{
    public class BlogPostInfo
    {
        public int blogid { get; set; }
        public int uid { get; set; }
        /// <summary>
        /// 15
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 标题 char 80
        /// </summary>
        public string subject { get; set; }
        /// <summary>
        /// 个人分类id
        /// </summary>
        public int classid { get; set; }
        /// <summary>
        /// 系统分类id
        /// </summary>
        public int catid { get; set; }
        public int viewnum { get; set; }
        public int replynum { get; set; }
        public int hot { get; set; }
        public int dateline { get; set; }
        /// <summary>
        /// kexue附加 1:原创;2:转载[iszz]
        /// </summary>
        public int blogtype { get; set; }
        /// <summary>
        /// 是否有图片
        /// </summary>
        public int picflag { get; set; }
        /// <summary>
        /// 禁止评论 0 允许  1禁止
        /// </summary>
        public int noreply { get; set; }
        /// <summary>
        /// 日志隐私 0全站可见  1全部好友 2 指定好友 3仅自己 4密码
        /// </summary>
        public int friend { get; set; }
        /// <summary>
        /// 日志密码
        /// </summary>
        public string password { get; set; }
        public int favtimes { get; set; }
        public int sharetimes { get; set; }
        public int status { get; set; }
        public int click1 { get; set; }
        public int click2 { get; set; }
        public int click3 { get; set; }
        public int click4 { get; set; }
        public int click5 { get; set; }
        public int click6 { get; set; }
        public int click7 { get; set; }
        public int click8 { get; set; }
        /// <summary>
        /// 置顶  1:置顶 0:普通(默认)kexue_blogarticle[ifgood]
        /// </summary>
        public int stickstatus { get; set; }
        /// <summary>
        /// 推荐  1:推荐 0:普通(默认)kexue_blogarticle[ifgood]
        /// </summary>
        public int recommendstatus { get; set; }

        /// <summary>
        /// 副标题 char 80
        /// </summary>
        public string showtitle      { get; set; }
        public int rfirstid       { get; set; }
        public int lastchangetime { get; set; }
        public int recommendnum   { get; set; }
        /// <summary>
        /// 标题图片 char255
        /// </summary>
        public string pic { get; set; }
        /// <summary>
        /// tag 255
        /// </summary>
        public string tag { get; set; }
        /// <summary>
        /// text
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// char 255
        /// </summary>
        public string postip { get; set; }
        /// <summary>
        /// 相关日志
        /// </summary>
        public string related { get; set; }
        /// <summary>
        /// 相关日志数据产生时间
        /// </summary>
        public int relatedtime { get; set; }
        /// <summary>
        /// 允许查看的uid 以""间隔
        /// </summary>
        public string target_ids { get; set; }
        /// <summary>
        /// 热点用户
        /// </summary>
        public string hotuser { get; set; }
        /// <summary>
        /// 道具彩色灯
        /// </summary>
        public int magiccolor { get; set; }
        /// <summary>
        /// 信纸
        /// </summary>
        public int magicpaper { get; set; }
        /// <summary>
        /// 推送文章id
        /// </summary>
        public int pushedaid { get; set; }
    }
}
