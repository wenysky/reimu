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


        public int GetForumLinksRecordCount()
        {
            return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT([Id]) FROM {0}Link", MainForm.srcDbTableProfix)));
        }

        public List<ForumLinks> GetForumLinkList()
        {
            string sqlTopicTypes = string.Format("SELECT * FROM {0}Link ORDER BY Id", MainForm.srcDbTableProfix);

            System.Data.Common.DbDataReader dr = MainForm.srcDBH.ExecuteReader(sqlTopicTypes);

            List<ForumLinks> forumlinklist = new List<ForumLinks>();
            while (dr.Read())
            {
                ForumLinks objForumLink = new ForumLinks();
                objForumLink.id = Convert.ToInt16(dr["Id"]);
                objForumLink.name = dr["Text"] == DBNull.Value ? "盛夏之地(我爱Discuz!NT)" : dr["Text"].ToString();
                objForumLink.url = dr["Url"] == DBNull.Value ? "http://bbs.52dnt.cn" : dr["Url"].ToString();
                objForumLink.logo = dr["Logo"] == DBNull.Value ? "http://www.52dnt.cn/logo/logo.gif" : dr["Logo"].ToString();
                objForumLink.displayorder = Convert.ToInt32(dr["SortId"]);

                forumlinklist.Add(objForumLink);
            }
            dr.Close();
            dr.Dispose();
            return forumlinklist;
        }

        #endregion
    }
}
