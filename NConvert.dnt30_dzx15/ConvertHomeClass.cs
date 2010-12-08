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
        public int GetHomeClassRecordCount()
        {
            if (MainForm.RecordCount == -1)
            {
                return 2 + Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT(id) FROM [science].[dbo].[kexue_blogarticletype]")));
            }
            else
            {
                return MainForm.RecordCount;
            }
        }

        public List<HomeClassInfo> GetHomeClassList()
        {
            string sqlBoard = string.Format(
                "SELECT * FROM [science].[dbo].[kexue_blogarticletype]",
                MainForm.cic.SrcDbTablePrefix
                );

            System.Data.Common.DbDataReader drBoard = MainForm.srcDBH.ExecuteReader(sqlBoard);

            List<HomeClassInfo> forumList = new List<HomeClassInfo>();
            while (drBoard.Read())
            {
                HomeClassInfo objForum = new HomeClassInfo();;
                objForum.classid = Convert.ToInt32(drBoard["id"]);
                objForum.classname = drBoard["title"].ToString();
                objForum.uid = Convert.ToInt32(drBoard["userid"]);
                objForum.dateline = 0;
                forumList.Add(objForum);
            }
            drBoard.Close();
            drBoard.Dispose();
            return forumList;
        }
    }
}
