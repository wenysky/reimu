using System;
using System.Text;
using System.Collections.Generic;

namespace NConvert.Entity
{
    /// <summary>
    /// 投票项信息类s
    /// </summary>
    public class VoteRecords
    {
        public int Pollid { get; set; }
        public List<string> Voternames { get; set; }
        /// <summary>
        /// Dictionary<PollOptionId,voternames>
        /// </summary>
        public Dictionary<int,string> Voterecords { get; set; }
    }
}
