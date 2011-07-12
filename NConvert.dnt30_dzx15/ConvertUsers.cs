using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;
using NConvert.Entity;
using Yuwen.Tools.Data;
using System.Collections;

namespace NConvert.dnt30_dzx15
{
    public partial class Provider : IProvider
    {
        string pkidname = "uid";
        string tablename = "users";
        public int GetUsersRecordCount()
        {
            return 12;
        }




        public void GetUserList(int CurrentPage)
        {
            DBHelper dbhTypeList = MainForm.GetSrcDBH_OldVer();
            DBHelper dbhOrderList = MainForm.GetSrcDBH_OldVer();
            DBHelper dbhIds = MainForm.GetSrcDBH_OldVer();
            DBHelper dbhInsert = MainForm.GetSrcDBH_OldVer();

            DBHelper dbhUpdate1 = MainForm.GetSrcDBH_OldVer();
            DBHelper dbhUpdate2 = MainForm.GetSrcDBH_OldVer();

            for (int i = 1; i <= 12; i++)
            {
                dbhTypeList.TruncateTable("wysky_mytemp");
                string sqlIsignseqList = string.Format("SELECT DISTINCT isignseq FROM GL_accvouch WHERE iperiod={0} ORDER BY isignseq", i);


                System.Data.Common.DbDataReader dr = dbhTypeList.ExecuteReader(sqlIsignseqList);
                List<int> isignseqList = new List<int>();
                while (dr.Read())
                {
                    if (dr["isignseq"] == DBNull.Value)
                    {
                        continue;
                    }
                    string sqlInoidList = string.Format("SELECT DISTINCT ino_id FROM GL_accvouch WHERE isignseq={0} AND iperiod={1} ORDER BY ino_id", dr["isignseq"].ToString(), i);
                    System.Data.Common.DbDataReader drInoidList = dbhOrderList.ExecuteReader(sqlInoidList);
                    while (drInoidList.Read())
                    {
                        string sqlIds = string.Format("SELECT i_id,dbill_date FROM GL_accvouch WHERE ino_id={0} AND isignseq={1} AND iperiod={2} ORDER BY dbill_date", drInoidList["ino_id"], dr["isignseq"].ToString(), i);
                        System.Data.Common.DbDataReader drIds = dbhIds.ExecuteReader(sqlIds);
                        string ids = "";
                        DateTime dt = DateTime.Now;
                        while (drIds.Read())
                        {
                            ids += "," + drIds["i_id"].ToString();
                            dt = Convert.ToDateTime(drIds["dbill_date"]);
                        }
                        drIds.Close();


                        string sqlInsert = string.Format(@"INSERT INTO wysky_mytemp (ids,bdate) VALUES (@ids,@bdate)");
                        dbhInsert.ParametersClear();
                        #region users参数
                        dbhInsert.ParameterAdd("@ids", ids.Trim(','), System.Data.DbType.String, 600000);
                        dbhInsert.ParameterAdd("@bdate", dt, System.Data.DbType.DateTime, 4);

                        #endregion
                        dbhInsert.ExecuteNonQuery(sqlInsert);
                    }
                    drInoidList.Close();
                }
                dr.Close();
                dr.Dispose();
                MainForm.MessageForm.SetMessage(string.Format("{0}月的临时插入完成", i));
                //开始update

                string sqlupdate1 = "SELECT * FROM wysky_mytemp ORDER BY bdate";
                System.Data.Common.DbDataReader drUpdate = dbhUpdate1.ExecuteReader(sqlupdate1);
                int order = 1;
                while (drUpdate.Read())
                {
                    string sqlUpdate2 = string.Format("UPDATE GL_accvouch SET csign='记',isignseq=6,ino_id={0} WHERE i_id IN ({1})", order, drUpdate["ids"].ToString().Trim());
                    dbhUpdate2.ExecuteNonQuery(sqlUpdate2);
                    order++;
                }
                order = 1;
                drUpdate.Close();
                MainForm.MessageForm.SetMessage(string.Format("{0}月的更新完成", i));

            }
        }
    }
}
