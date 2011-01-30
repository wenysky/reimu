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

        public int GetTopicTypesRecordCount()
        {
            return Convert.ToInt32(
                MainForm.srcDBH.ExecuteScalar(
                string.Format(
                "SELECT COUNT(typeid) FROM {0}topictypes",
                MainForm.cic.SrcDbTablePrefix)
                )
                );
        }

        public List<TopicTypes> GetTopicTypeList()
        {
            string sql;

            sql = string.Format
                   ("SELECT * FROM {0}topictypes ORDER BY typeid", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);


            System.Data.Common.DbDataReader dr = MainForm.srcDBH.ExecuteReader(sql);
            List<TopicTypes> oldTopiclist = new List<TopicTypes>();
            List<TopicTypes> newToiclist = new List<TopicTypes>();
            while (dr.Read())
            {
                TopicTypes objTopic = new TopicTypes();
                objTopic.typeid = Convert.ToInt32(dr["typeid"]);
                objTopic.fid = 0;
                objTopic.name = dr["name"].ToString();
                objTopic.displayorder = Convert.ToInt32(dr["displayorder"]);
                objTopic.icon = "";
                oldTopiclist.Add(objTopic);
            }
            dr.Close();
            dr.Dispose();


            string sqlBoard = string.Format(
                "SELECT * FROM {0}forums ORDER BY fid",
                MainForm.cic.SrcDbTablePrefix
                );
            System.Data.Common.DbDataReader drForums = MainForm.srcDBH.ExecuteReader(sqlBoard);

            while (drForums.Read())
            {
                foreach (TopicTypes objTopicType in oldTopiclist)
                {
                    TopicTypes objNewTopicType = new TopicTypes();
                    objNewTopicType.fid = Convert.ToInt32(drForums["fid"]);
                    objNewTopicType.typeid = Convert.ToInt32(string.Format("{0}", objNewTopicType.fid * 100 + objTopicType.typeid));
                    objNewTopicType.name = objTopicType.name;
                    objNewTopicType.displayorder = objTopicType.displayorder;
                    objNewTopicType.icon = objTopicType.icon;
                    newToiclist.Add(objNewTopicType);
                }
            }
            drForums.Close();
            drForums.Dispose();

            return newToiclist;
        }


        #endregion
    }
}
