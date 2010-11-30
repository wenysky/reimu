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
            return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT(pmid) FROM {0}pms WHERE folder=0", MainForm.cic.SrcDbTablePrefix)));//Discuz!X1.5统一了发件箱和收件箱，所以只需要转发送者的那一条就够了；因为一条数据会被插入3次，所以*3
        }

        public List<Pms> GetPmList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM {0}pms WHERE folder=0 ORDER BY pmid", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM {0}pms WHERE folder=0 AND pmid NOT IN (SELECT TOP {2} pmid FROM {0}pms WHERE folder=0 ORDER BY pmid) ORDER BY pmid", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion

            System.Data.Common.DbDataReader dr = MainForm.srcDBH.ExecuteReader(sql);
            List<Pms> pmlist = new List<Pms>();
            while (dr.Read())
            {
                Pms objPms = new Pms();
                objPms.pmid = Convert.ToInt32(dr["pmid"]);
                objPms.msgfrom = (dr["msgfrom"].ToString().Trim() == "系统" && objPms.msgfromid == 0) ? "" : dr["msgfrom"].ToString();
                objPms.msgfromid = Convert.ToInt32(dr["msgfromid"]);
                objPms.msgtoid = Convert.ToInt32(dr["msgtoid"]);
                objPms.folder = "inbox";
                objPms.isnew = Convert.ToInt32(dr["new"]);
                objPms.subject = dr["subject"].ToString();
                objPms.dateline = Utils.TypeParse.DateTime2TimeStamp(Convert.ToDateTime(dr["postdatetime"]));
                objPms.message = dr["message"].ToString();
                objPms.delstatus = 0;
                objPms.related = 1;
                objPms.fromappid = 1;

                pmlist.Add(objPms);

                ////Discuz!X要求插入2条相同数据才能显示
                //Pms objPms3 = new Pms();
                //objPms3.pmid = (objPms.msgfrom.Trim() == string.Empty && objPms.msgfromid == 0) ? -77 : objPms.pmid;
                //objPms3.msgfrom = objPms.msgfrom;
                //objPms3.msgfromid = objPms.msgfromid;
                //objPms3.msgtoid = objPms.msgtoid;
                //objPms3.folder = objPms.folder;
                //objPms3.isnew = objPms.isnew;
                //objPms3.subject = objPms.subject;
                //objPms3.dateline = objPms.dateline;
                //objPms3.message = objPms.message;
                //objPms3.delstatus = objPms.delstatus;
                //objPms3.related = 0;
                //objPms3.fromappid = objPms.fromappid;
                //pmlist.Add(objPms3);

                ////Discuz!X要求额外插入一条来自和发给的uid对调的数据
                //Pms objPms2 = new Pms();
                //objPms2.pmid = (objPms.msgfrom.Trim() == string.Empty && objPms.msgfromid == 0) ? -77 : objPms.pmid;
                //objPms2.msgfrom = objPms.msgfrom;
                //objPms2.msgfromid = (objPms.msgfrom.Trim() == string.Empty && objPms.msgfromid == 0) ? -77 : objPms.msgtoid;
                //objPms2.msgtoid = objPms.msgfromid;
                //objPms2.folder = objPms.folder;
                //objPms2.isnew = objPms.isnew;
                //objPms2.subject = objPms.subject;
                //objPms2.dateline = objPms.dateline;
                //objPms2.message = objPms.message;
                //objPms2.delstatus = objPms.delstatus;
                //objPms2.related = 0;
                //objPms2.fromappid = 0;
                //pmlist.Add(objPms2);
            }
            dr.Close();
            dr.Dispose();
            return pmlist;
        }

        #endregion
    }
}
