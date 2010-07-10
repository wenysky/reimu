using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;
using NConvert.Entity;
using Yuwen.Tools.Data;

namespace NConvert.lybbs70_dnt31
{
    public partial class Provider : IProvider
    {
        #region IProvider 成员


        public int GetPollOptionsRecordCount()
        {
            return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT(Id) FROM {0}vote", MainForm.cic.SrcDbTablePrefix)));
        }

        public List<PollOptionInfo> GetPollOptionsList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            sql = string.Format(
                "SELECT * FROM {0}vote ORDER BY Id LIMIT {1},{2}",
                MainForm.cic.SrcDbTablePrefix,
                MainForm.PageSize * (CurrentPage - 1),
                MainForm.PageSize
                );
            #endregion

            System.Data.Common.DbDataReader dr = MainForm.srcDBH.ExecuteReader(sql);
            List<PollOptionInfo> polloptionlist = new List<PollOptionInfo>();
            while (dr.Read())
            {
                PollOptionInfo objPoll = new PollOptionInfo();
                objPoll.Polloptionid = Convert.ToInt32(dr["id"]);
                objPoll.Tid = Convert.ToInt32(dr["postsid"]);
                objPoll.Pollid = Convert.ToInt32(dr["postsid"]);
                objPoll.Votes = Convert.ToInt32(dr["votenumber"]);
                objPoll.Polloption = dr["votetitle"].ToString();
                //objPoll.Voternames = "";

                polloptionlist.Add(objPoll);
            }
            dr.Close();
            dr.Dispose();
            return polloptionlist;
        }

        #endregion
    }
}
