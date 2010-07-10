using System;
using System.Text;

namespace NConvert.Entity
{
    /// <summary>
    /// 投票项信息类s
    /// </summary>
    public class PollOptionInfo
    {
        private int _polloptionid = 0;
        private int _tid = 0;
        private int _pollid = 0;
        private int _votes = 0;
        private int _displayorder = 0;
        private string _polloption = "yw";
        private string _voternames = "";

        /// <summary>
        /// 选项ID
        /// </summary>
        public int Polloptionid
        {
            set { _polloptionid = value; }
            get { return _polloptionid; }
        }
        /// <summary>
        /// 主题ID
        /// </summary>
        public int Tid
        {
            set { _tid = value; }
            get { return _tid; }
        }
        /// <summary>
        /// 关联投票id
        /// </summary>
        public int Pollid
        {
            set { _pollid = value; }
            get { return _pollid; }
        }
        /// <summary>
        /// 票数
        /// </summary>
        public int Votes
        {
            set { _votes = value; }
            get { return _votes; }
        }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int Displayorder
        {
            set { _displayorder = value; }
            get { return _displayorder; }
        }
        /// <summary>
        /// 选项内容
        /// </summary>
        public string Polloption
        {
            set { _polloption = value; }
            get { return _polloption; }
        }
        /// <summary>
        /// 投票用户列表
        /// </summary>
        public string Voternames
        {
            set { _voternames = value; }
            get { return _voternames == null ? "" : _voternames; }
        }
    }
}
