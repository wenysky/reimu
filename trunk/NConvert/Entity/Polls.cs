using System;
using System.Text;

namespace NConvert.Entity
{
    /// <summary>
    /// 投票信息类
    /// </summary>
    public class Polls
    {

        private int _pollid = 0;
        private int _tid = 0;
        private int _displayorder = 0;
        private int _multiple = 0;
        private int _visible = 0;
        private int _maxchoices = 1;
        private DateTime _expiration = DateTime.Now;
        private int _uid = 0;
        private string _voternames = string.Empty;
        /// <summary>
        /// 投票ID
        /// </summary>
        public int Pollid
        {
            set { _pollid = value; }
            get { return _pollid; }
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
        /// 显示顺序
        /// </summary>
        public int Displayorder
        {
            set { _displayorder = value; }
            get { return _displayorder; }
        }
        /// <summary>
        /// 是否多选
        /// </summary>
        public int Multiple
        {
            set { _multiple = value; }
            get { return _multiple; }
        }
        /// <summary>
        /// 是否投票可见
        /// </summary>
        public int Visible
        {
            set { _visible = value; }
            get { return _visible; }
        }
        /// <summary>
        /// 最大可选项数
        /// </summary>
        public int Maxchoices
        {
            set { _maxchoices = value; }
            get { return _maxchoices; }
        }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime Expiration
        {
            set { _expiration = value; }
            get
            {
                if (_expiration == null)
                {
                    return DateTime.Now;
                }
                return _expiration;
            }
        }
        /// <summary>
        /// 发起投票人的ID
        /// </summary>
        public int Uid
        {
            set { _uid = value; }
            get { return _uid; }
        }
        /// <summary>
        /// 已投过票的用户
        /// </summary>
        public string Voternames
        {
            set { _voternames = value; }
            get { return _voternames == null ? "" : _voternames; }
        }


    }
}
