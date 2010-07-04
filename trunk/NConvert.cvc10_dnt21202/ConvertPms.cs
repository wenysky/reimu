using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;
using NConvert.Entity;

namespace NConvert.cvc10_dnt21202
{
    public partial class Provider : IProvider
    {
        #region IProvider 成员


        public int GetPmsRecordCount()
        {
            return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT([Id]) FROM {0}Message", MainForm.srcDbTableProfix)));
        }

        public List<Pms> GetPmList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM {0}Message ORDER BY Id", MainForm.srcDbTableProfix, MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM {0}Message WHERE Id NOT IN (SELECT TOP {2} Id FROM {0}Message) ORDER BY Id", MainForm.srcDbTableProfix, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion

            System.Data.Common.DbDataReader dr = MainForm.srcDBH.ExecuteReader(sql);
            List<Pms> pmlist = new List<Pms>();
            while (dr.Read())
            {
                Pms objPms = new Pms();
                objPms.pmid = Convert.ToInt32(dr["Id"]);
                objPms.msgfromid = Convert.ToInt32(dr["SenderId"]);
                objPms.msgfrom = dr["SenderName"].ToString();
                objPms.msgtoid = Convert.ToInt32(dr["RecipientId"]);
                objPms.msgto = dr["RecipientName"].ToString();
                objPms.subject = dr["Title"].ToString();
                objPms.message = dr["Body"].ToString();
                objPms.newmessage = Convert.ToInt32(dr["Readed"]);
                objPms.postdatetime = Convert.ToDateTime(dr["SendTime"]);

                pmlist.Add(objPms);
            }
            dr.Close();
            dr.Dispose();
            return pmlist;
        }

        #endregion
    }
}
