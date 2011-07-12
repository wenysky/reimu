using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;
using NConvert.Entity;
using Yuwen.Tools.Data;

namespace NConvert.dnt30_dzx15
{
    public partial class Provider : IProvider
    {
        #region IProvider Members


        public int GetVotesRecordCount()
        {
            return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT(id) FROM {0}voterecord", MainForm.cic.SrcDbTablePrefix)));
        }

        public List<VoteRecords> GetVotesList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            sql = string.Format(
                "SELECT * FROM {0}voterecord ORDER BY voteid",
                MainForm.cic.SrcDbTablePrefix
                );//不分页 不然后面整理可能有丢东西的问题
            #endregion

            System.Data.Common.DbDataReader dr = MainForm.srcDBH.ExecuteReader(sql);
            List<VoteRecords> polloptionlist = new List<VoteRecords>();
            while (dr.Read())
            {
                int pollid = Convert.ToInt32(dr["postsid"]);
                VoteRecords objPoll = polloptionlist.Find(delegate(VoteRecords p) { return p.Pollid == pollid; });
                if (objPoll == null)
                {
                    objPoll = new VoteRecords();
                    objPoll.Pollid = pollid;
                    objPoll.Voternames = new List<string>();
                    objPoll.Voterecords = new Dictionary<int, string>();
                }


                if (dr["votename"] != DBNull.Value
                    && dr["votename"].ToString().Trim() != string.Empty
                    )
                {
                    if (!objPoll.Voternames.Contains(dr["votename"].ToString().Trim()))
                    {
                        objPoll.Voternames.Add(dr["votename"].ToString().Trim());
                    }

                    int voteid=Convert.ToInt32(dr["voteid"]);
                    if (objPoll.Voterecords.ContainsKey(voteid))
                    {
                        objPoll.Voterecords[voteid] += "\r\n" + dr["votename"].ToString().Trim();
                    }
                    else
                    {
                        objPoll.Voterecords.Add(Convert.ToInt32(dr["voteid"]), dr["votename"].ToString().Trim());
                    }
                }
                polloptionlist.Add(objPoll);
            }
            dr.Close();
            dr.Dispose();
            return polloptionlist;
        }

        #endregion
    }
}
