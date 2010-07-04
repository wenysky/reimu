using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Entity;

namespace NConvert.Provider
{
    public interface IProvider
    {

        #region DLL基本信息

        /// <summary>
        /// 得到转换项目名
        /// </summary>
        /// <returns></returns>
        string GetDllDisplayName();

        /// <summary>
        /// 得到转换说明
        /// </summary>
        /// <returns></returns>
        string GetDllDescription();

        /// <summary>
        /// 支持的源数据库类型
        /// </summary>
        /// <returns></returns>
        string[] GetSupportSrcDbType();

        /// <summary>
        /// 支持的目标数据库类型
        /// </summary>
        /// <returns></returns>
        string[] GetSupportTargetDbType();

        #endregion

        #region 转换用户
        /// <summary>
        /// 得到用户表中记录数.优先读取全局静态变量,如果变量为-1,则从数据库读取.
        /// </summary>
        /// <returns>记录数</returns>
        int GetUsersRecordCount();
        /// <summary>
        /// 分页得到用户列表
        /// </summary>
        /// <param name="CurrentPage">获取第几页</param>
        /// <returns>用户列表</returns>
        List<Users> GetUserList(int CurrentPage);
        #endregion

        #region 转换论坛版块
        /// <summary>
        /// 得到版块表中记录数.优先读取全局静态变量,如果变量为-1,则从数据库读取.
        /// </summary>
        /// <returns>记录数</returns>
        int GetForumsRecordCount();

        /// <summary>
        /// 得到论坛版块列表
        /// </summary>
        /// <returns>论坛版块列表</returns>
        List<Forums> GetForumList();
        #endregion

        #region 转换主题
        /// <summary>
        /// 得到主题表中记录数.优先读取全局静态变量,如果变量为-1,则从数据库读取.
        /// </summary>
        /// <returns>记录数</returns>
        int GetTopicsRecordCount();
        /// <summary>
        /// 得到主题列表
        /// </summary>
        /// <returns>主题列表</returns>
        List<Topics> GetTopicList(int CurrentPage);
        #endregion

        #region 转换帖子
        int GetPostsRecordCount();

        List<Posts> GetPostList(int pagei);
        #endregion

        #region 转换附件
        int GetAttachmentsRecordCount();

        List<Attachments> GetAttachmentList(int pagei);
        #endregion

        #region 转换投票
        int GetPollsRecordCount();

        List<Polls> GetPollList(int pagei);
        #endregion

        #region 转换短消息
        int GetPmsRecordCount();

        List<Pms> GetPmList(int pagei);
        #endregion

        #region 转换主题分类

        int GetTopicTypesRecordCount();

        List<TopicTypes> GetTopicTypeList();

        #endregion

        #region 转换友情链接
        int GetForumLinksRecordCount();

        List<ForumLinks> GetForumLinkList();
        #endregion


        
    }
}
