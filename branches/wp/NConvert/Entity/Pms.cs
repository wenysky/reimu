using System;
using System.Collections.Generic;
using System.Text;

namespace NConvert.Entity
{
    public class Pms
    {
        public int pmid { get; set; }
        /// <summary>
        /// char15 发送人姓名
        /// </summary>
        public string msgfrom { get; set; }
        public int msgfromid { get; set; }
        public int msgtoid { get; set; }
        /// <summary>
        /// 收件箱inbox/发件箱outbox
        /// </summary>
        public string folder { get; set; }
        /// <summary>
        /// 是否看过
        /// </summary>
        public int isnew { get; set; }
        /// <summary>
        /// char75
        /// </summary>
        public string subject { get; set; }
        public int dateline { get; set; }
        public string message { get; set; }
        /// <summary>
        /// 删除状态 1表示接收方删除 3表示双方都删除
        /// </summary>
        public int delstatus { get; set; }
        /// <summary>
        /// 关联的主题id  （tid）
        /// </summary>
        public int related { get; set; }
        public int fromappid { get; set; }
    }
}
