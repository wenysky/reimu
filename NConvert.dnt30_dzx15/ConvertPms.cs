using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;
using NConvert.Entity;

namespace NConvert.dnt30_dzx15
{
    public partial class Provider : IProvider
    {
        #region IProvider 成员


        public int GetPmsRecordCount()
        {
            return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT(pmid) FROM {0}pms", MainForm.cic.SrcDbTablePrefix)));
        }

        public List<Pms> GetPmList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM {0}pms ORDER BY pmid", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM {0}pms WHERE pmid NOT IN (SELECT TOP {2} pmid FROM {0}pms ORDER BY pmid) ORDER BY pmid", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion

            System.Data.Common.DbDataReader dr = MainForm.srcDBH.ExecuteReader(sql);
            List<Pms> pmlist = new List<Pms>();
            while (dr.Read())
            {
                Pms objPms = new Pms();
                objPms. = Convert.ToInt32(dr[""]);
                objPms. = Convert.ToInt32(dr[""]);
                objPms. = Convert.ToInt32(dr[""]);
                objPms. = Convert.ToInt32(dr[""]);
                objPms. = Convert.ToInt32(dr[""]);
                objPms. = Convert.ToInt32(dr[""]);
                objPms. = Convert.ToInt32(dr[""]);
                objPms. = Convert.ToInt32(dr[""]);
                objPms. = Convert.ToInt32(dr[""]);
                objPms. = Convert.ToInt32(dr[""]);
                objPms. = Convert.ToInt32(dr[""]);
                objPms. = Convert.ToInt32(dr[""]);
                objPms. = Convert.ToInt32(dr[""]);
                objPms. = Convert.ToInt32(dr[""]);
                objPms. = Convert.ToInt32(dr[""]);
                objPms. = Convert.ToInt32(dr[""]);
                objPms. = Convert.ToInt32(dr[""]);
                objPms. = Convert.ToInt32(dr[""]);
                objPms. = Convert.ToInt32(dr[""]);
                objPms. = Convert.ToInt32(dr[""]);
                pmlist.Add(objPms);
            }
            dr.Close();
            dr.Dispose();
            return pmlist;
        }

        #endregion
    }
}
