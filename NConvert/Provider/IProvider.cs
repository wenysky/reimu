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
        void GetUserList(int CurrentPage);
        #endregion
    }
}
