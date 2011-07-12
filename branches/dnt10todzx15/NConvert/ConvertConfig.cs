using System;

namespace NConvert
{
    [Serializable]
    public class ConvertInfoConfig
    {
        /// <summary>
        /// 转换类型(初步决定为例如NConvert.{0}.dll)
        /// </summary>
        public string ConvertTypeName { get; set; }

        /// <summary>
        /// 源数据库类型
        /// </summary>
        public string SrcDbType { get; set; }
        /// <summary>
        /// 源数据库表前缀
        /// </summary>
        public string SrcDbTablePrefix { get; set; }
        public string SrcDbAddress { get; set; }
        public string SrcDbName { get; set; }
        public string SrcDbUsername { get; set; }
        public string SrcDbUserpassword { get; set; }
        public string SrcDbFilePath { get; set; }

        /// <summary>
        /// 目标数据库类型
        /// </summary>
        public string TargetDbType { get; set; }
        /// <summary>
        /// 目标数据库表前缀
        /// </summary>
        public string TargetDbTablePrefix { get; set; }
        public string TargetDbAddress { get; set; }
        public string TargetDbName { get; set; }
        public string TargetDbUsername { get; set; }
        public string TargetDbUserpassword { get; set; }
        public string TargetDbFilePath { get; set; } 
    }
}
