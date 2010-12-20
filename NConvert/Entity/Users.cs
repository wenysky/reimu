using System;

namespace NConvert.Entity
{
    public class Users
    {
        public int uid { get; set; }
        /// <summary>
        /// char6
        /// </summary>
        public string salt { get; set; }
        public string ucpassword { get; set; }
        /// <summary>
        /// 40char
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 15char
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 32
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// Email是否通过验证
        /// </summary>
        public int emailstatus { get; set; }
        /// <summary>
        /// 是否有头像
        /// </summary>
        public int avatarstatus { get; set; }
        /// <summary>
        /// 视频认证
        /// </summary>
        public int videophotostatus { get; set; }
        /// <summary>
        /// 管理员id
        /// </summary>
        public int adminid { get; set; }
        /// <summary>
        /// 会员组id
        /// </summary>
        public int groupid { get; set; }
        /// <summary>
        /// 用户组有效时间
        /// </summary>
        public int groupexpiry { get; set; }
        /// <summary>
        /// char20  扩展用户组
        /// </summary>
        public string extgroupids { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public int regdate { get; set; }
        /// <summary>
        /// 总积分
        /// </summary>
        public int credits { get; set; }
        public int notifysound { get; set; }
        /// <summary>
        /// char4  时区校正
        /// </summary>
        public string timeoffset { get; set; }
        public int newpm { get; set; }
        /// <summary>
        /// 新提醒数目
        /// </summary>
        public int newprompt { get; set; }
        /// <summary>
        /// 用户特殊权限
        /// </summary>
        public int accessmasks { get; set; }
        /// <summary>
        /// 访问管理面板
        /// </summary>
        public int allowadmincp { get; set; }

        public int extcredits1 { get; set; }
        public int extcredits2 { get; set; }
        public int extcredits3 { get; set; }
        public int extcredits4 { get; set; }
        public int extcredits5 { get; set; }
        public int extcredits6 { get; set; }
        public int extcredits7 { get; set; }
        public int extcredits8 { get; set; }
        /// <summary>
        /// 好友个数
        /// </summary>
        public int friends { get; set; }
        public int posts { get; set; }
        public int threads { get; set; }
        public int digestposts { get; set; }
        public int doings { get; set; }
        public int blogs { get; set; }
        public int albums { get; set; }
        public int sharings { get; set; }
        public int attachsize { get; set; }
        public int views { get; set; }
        public int oltime { get; set; }

        /// <summary>
        /// 用户自定义发送哪些类型的feed
        /// </summary>
        public int publishfeed { get; set; }
        /// <summary>
        /// 自定义帖子显示模式
        /// </summary>
        public int customshow { get; set; }
        /// <summary>
        /// char30 自定义头衔
        /// </summary>
        public string customstatus { get; set; }
        /// <summary>
        /// text 勋章
        /// </summary>
        public string medals { get; set; }
        /// <summary>
        /// text 签名
        /// </summary>
        public string sightml { get; set; }

        /// <summary>
        /// text 公告用户组
        /// </summary>
        public string groupterms { get; set; }
        /// <summary>
        /// char20 找回密码验证串
        /// </summary>
        public string authstr { get; set; }
        /// <summary>
        /// TEXT用户所有群组
        /// </summary>
        public string groups { get; set; }
        /// <summary>
        /// char 255用户管组群组
        /// </summary>
        public string attentiongroup { get; set; }

        /// <summary>
        /// char255
        /// </summary>
        public string realname { get; set; }
        /// <summary>
        /// 性别：0保密 1男 2女
        /// </summary>
        public int gender { get; set; }
        public int birthyear { get; set; }
        public int birthmonth { get; set; }
        public int birthday { get; set; }
        /// <summary>
        /// char 255 星座 根据生日自动计算
        /// </summary>
        public string constellation { get; set; }
        /// <summary>
        /// char 255 生肖 根据生日自动计算
        /// </summary>
        public string zodiac { get; set; }
        /// <summary>
        /// char255 固定电话
        /// </summary>
        public string telephone { get; set; }
        /// <summary>
        /// chare255 手机
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 255证件类型
        /// </summary>
        public string idcardtype { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string idcard { get; set; }
        public string address { get; set; }
        public string zipcode { get; set; }
        /// <summary>
        /// 国籍
        /// </summary>
        public string nationality { get; set; }
        public string birthprovince { get; set; }
        public string birthcity { get; set; }
        /// <summary>
        /// 居住省份
        /// </summary>
        public string resideprovince { get; set; }
        public string residecity { get; set; }
        /// <summary>
        /// 居住行政区/县
        /// </summary>
        public string residedist { get; set; }
        /// <summary>
        /// 居住小区
        /// </summary>
        public string residecommunity { get; set; }
        /// <summary>
        /// 居住房间
        /// </summary>
        public string residesuite { get; set; }
        /// <summary>
        /// 毕业学校
        /// </summary>
        public string graduateschool { get; set; }
        public string company { get; set; }
        public string education { get; set; }
        /// <summary>
        /// 职业
        /// </summary>
        public string occupation { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public string position { get; set; }
        /// <summary>
        /// 年收入
        /// </summary>
        public string revenue { get; set; }
        /// <summary>
        /// 情感状态
        /// </summary>
        public string affectivestatus { get; set; }
        /// <summary>
        /// 交友目的
        /// </summary>
        public string lookingfor { get; set; }
        public string bloodtype { get; set; }
        public string height { get; set; }
        public string weight { get; set; }
        public string alipay { get; set; }
        public string icq { get; set; }
        public string qq { get; set; }
        public string yahoo { get; set; }
        public string msn { get; set; }
        public string taobao { get; set; }
        public string site { get; set; }
        /// <summary>
        /// 以下开始都是text
        /// </summary>
        public string bio { get; set; }
        /// <summary>
        /// 兴趣爱好
        /// </summary>
        public string interest { get; set; }
        public string field1 { get; set; }
        public string field2 { get; set; }
        public string field3 { get; set; }
        public string field4 { get; set; }
        public string field5 { get; set; }
        public string field6 { get; set; }
        public string field7 { get; set; }
        public string field8 { get; set; }

        /// <summary>
        /// char15
        /// </summary>
        public string regip { get; set; }
        /// <summary>
        /// char15
        /// </summary>
        public string lastip { get; set; }
        public int lastvisit { get; set; }
        public int lastactivity { get; set; }
        public int lastpost { get; set; }
        public int lastsendmail { get; set; }
        /// <summary>
        /// 通知数
        /// </summary>
        public int notifications { get; set; }
        /// <summary>
        /// 漫游邀请
        /// </summary>
        public int myinvitations { get; set; }
        /// <summary>
        /// 招呼数
        /// </summary>
        public int pokes { get; set; }
        public int pendingfriends { get; set; }
        /// <summary>
        /// 是否隐身登录
        /// </summary>
        public int invisible { get; set; }
        public int buyercredit { get; set; }
        public int sellercredit { get; set; }
        public int favtimes { get; set; }
        public int sharetimes { get; set; }

        /// <summary>
        /// 视频认证照片地址 char255
        /// </summary>
        public string videophoto { get; set; }
        /// <summary>
        /// 空间描述 char255
        /// </summary>
        public string spacename { get; set; }
        /// <summary>
        /// 空间简介 char255
        /// </summary>
        public string spacedescription { get; set; }
        /// <summary>
        /// char15
        /// </summary>
        public string domain { get; set; }
        public int addsize { get; set; }
        public int addfriend { get; set; }
        /// <summary>
        /// 应用显示个数
        /// </summary>
        public int menunum { get; set; }
        /// <summary>
        /// 空间风格主题 char20
        /// </summary>
        public string theme { get; set; }
        public string spacecss { get; set; }
        public string blockposition { get; set; }
        public string recentnote { get; set; }
        public string spacenote { get; set; }
        public string privacy { get; set; }
        public string feedfriend { get; set; }
        public string acceptemail { get; set; }
        public string magicgift { get; set; }

        #region 额外的
        /// <summary>
        /// 用户类型(0:匿名;1:博客)  [user]blogshen   -1,-2 处理为0 (-1时插入用户到common_member_verify  | common_member_verify_info 对应 flag -1, -2跳过)
        /// </summary>
        public int usertype { get; set; }
        /// <summary>
        /// 该用户博文是否在博客首页显示 1:显示(默认) 0:不显示  （原数据库[user][ifgood]-1对应文章不显示）
        /// </summary>
        public int blogShowStatus { get; set; }
        /// <summary>
        /// 机构博客(0:否；1：是) 对应jigoublog
        /// </summary>
        public int organblog { get; set; }
        /// <summary>
        /// 用户级别(0:普通博主;1:专家名录;2:特别推荐;-1:受限博主)
        /// </summary>
        public int userlevel { get; set; }

        /// <summary>
        /// varchar(255) 高校   对应[user]-UserInfo
        /// </summary>
        public string university { get; set; }
        /// <summary>
        /// 大学或机构id  X
        /// </summary>
        public int universityid { get; set; }
        /// <summary>
        /// varchar(255) 院系X
        /// </summary>
        public string laboratory { get; set; }
        /// <summary>
        /// 入学年份X
        /// </summary>
        public int initialstudyear { get; set; }
        /// <summary>
        /// varchar(255) 学历 对应[user]-UserInfo
        /// </summary>
        public string educational { get; set; }
        /// <summary>
        /// 属于那一级（第一个高校为1，第二高校为2，第三个高校为3）   默认置为1
        /// </summary>
        public int grade { get; set; }

        /// <summary>
        /// 开博时间
        /// </summary>
        public int blogstartime { get; set; }
        #endregion
    }
}
