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


        public int GetPollsRecordCount()
        {
            return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT(Id) FROM {0}posts WHERE vote>0", MainForm.cic.SrcDbTablePrefix)));
        }

        public List<Polls> GetPollList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            sql = string.Format(
                "SELECT * FROM {0}posts WHERE vote>0 ORDER BY Id LIMIT {1},{2}",
                MainForm.cic.SrcDbTablePrefix,
                MainForm.PageSize * (CurrentPage - 1),
                MainForm.PageSize
                );
            #endregion

            System.Data.Common.DbDataReader dr = MainForm.srcDBH.ExecuteReader(sql);
            List<Polls> polllist = new List<Polls>();
            while (dr.Read())
            {
                Polls objPoll = new Polls();
                objPoll.Pollid = Convert.ToInt32(dr["id"]);
                objPoll.Tid = Convert.ToInt32(dr["id"]);
                if (Convert.ToInt32(dr["vote"]) == 1 || Convert.ToInt32(dr["vote"]) == 3)
                {
                    objPoll.Multiple = 1;
                }
                else
                {
                    objPoll.Multiple = 0;
                }
                if (Convert.ToInt32(dr["votecondition"]) < 0)
                {
                    objPoll.Maxchoices = -Convert.ToInt32(dr["votecondition"]) - 1;
                }
                else if (Convert.ToInt32(dr["votecondition"]) > 60)
                {
                    objPoll.Maxchoices = Convert.ToInt32(dr["votecondition"]) - 60;
                }
                else
                {
                    objPoll.Maxchoices = 99;
                }
                objPoll.Expiration = DateTime.Now.AddYears(1);
                //objPoll.Voternames = "";

                polllist.Add(objPoll);
            }
            dr.Close();
            dr.Dispose();
            return polllist;
        }

        #endregion
    }
}
