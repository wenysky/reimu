using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;
using NConvert.Entity;

namespace NConvert.dvnet11_dnt21202
{
    public partial class Provider : IProvider
    {
        public int GetTopicTypesRecordCount()
        {
            return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT([Id]) FROM {0}Subject", MainForm.cic.SrcDbTablePrefix)));

        }

        public List<TopicTypes> GetTopicTypeList()
        {
            string sqlTopicTypes = string.Format
                       ("SELECT * FROM {0}Subject ORDER BY Id", MainForm.cic.SrcDbTablePrefix);            

            System.Data.Common.DbDataReader dr = MainForm.srcDBH.ExecuteReader(sqlTopicTypes);

            List<TopicTypes> topictypelist = new List<TopicTypes>();
            while (dr.Read())
            {
                TopicTypes objTopicType = new TopicTypes();
                objTopicType.typeid = Convert.ToInt32(dr["Id"]);
                objTopicType.name = dr["SubjectName"].ToString();
                objTopicType.description = dr["Description"] == DBNull.Value ? objTopicType.name : dr["Description"].ToString();

                topictypelist.Add(objTopicType);
            }
            dr.Close();
            dr.Dispose();
            return topictypelist;
        }
    }
}
