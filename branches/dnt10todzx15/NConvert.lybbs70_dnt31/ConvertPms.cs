using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;
using NConvert.Entity;

namespace NConvert.lybbs70_dnt31
{
    public partial class Provider : IProvider
    {
        #region IProvider 成员


        public int GetPmsRecordCount()
        {
            return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT(Id) FROM {0}webqq", MainForm.cic.SrcDbTablePrefix)));
        }

        public List<Pms> GetPmList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            sql = string.Format(
                "SELECT * FROM {0}webqq ORDER BY Id LIMIT {1},{2}", 
                MainForm.cic.SrcDbTablePrefix, 
                MainForm.PageSize * (CurrentPage - 1), 
                MainForm.PageSize
                );
            #endregion

            System.Data.Common.DbDataReader dr = MainForm.srcDBH.ExecuteReader(sql);
            List<Pms> pmlist = new List<Pms>();
            while (dr.Read())
            {
                Pms objPms = new Pms();
                objPms.pmid = Convert.ToInt32(dr["Id"]);
                objPms.msgfromid = 1;
                objPms.msgfrom = dr["fromname"].ToString();
                objPms.msgtoid = 1;
                objPms.msgto = dr["toname"].ToString();
                objPms.subject = dr["Title"].ToString();
                objPms.message = dr["message"].ToString();
                objPms.newmessage = Convert.ToInt32(dr["viewed"]);
                objPms.postdatetime = Convert.ToDateTime(dr["sendat"]);
                if (dr["operation"] != DBNull.Value && dr["operation"].ToString().ToLower()=="send")
                {
                    objPms.folder = 1;
                }

                pmlist.Add(objPms);
            }
            dr.Close();
            dr.Dispose();
            return pmlist;
        }

        #endregion
    }
}
