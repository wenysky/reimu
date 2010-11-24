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
        #region IProvider 成员


        public int GetAttachmentsRecordCount()
        {
            return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT(Id) FROM {0}uploadinfo", MainForm.cic.SrcDbTablePrefix)));
        }

        public List<Attachments> GetAttachmentList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            sql = string.Format(
                "SELECT * FROM {0}uploadinfo ORDER BY Id LIMIT {1},{2}", 
                MainForm.cic.SrcDbTablePrefix, 
                MainForm.PageSize * (CurrentPage - 1), 
                MainForm.PageSize
                );
            #endregion

            System.Data.Common.DbDataReader dr = MainForm.srcDBH.ExecuteReader(sql);
            List<Attachments> attachmentlist = new List<Attachments>();
            while (dr.Read())
            {
                Attachments objAttachment = new Attachments();
                objAttachment.aid = Convert.ToInt32(dr["Id"]);
                objAttachment.pid = Convert.ToInt32(dr["postsid"]);
                objAttachment.tid = Convert.ToInt32(dr["parentid"]);
                objAttachment.uid = dr["authorid"] == DBNull.Value ? -1 : Convert.ToInt32(dr["authorid"]);
                objAttachment.attachment = dr["filerealname"] == DBNull.Value ? "无标题" : dr["filerealname"].ToString();
                objAttachment.filename = dr["filepath"] == DBNull.Value ? "nothing.zip" : dr["filepath"].ToString();
                objAttachment.description = objAttachment.attachment;
                objAttachment.filesize = dr["filesize"] == DBNull.Value ? -1 : Convert.ToInt32(dr["filesize"]);
                objAttachment.filetype = Utils.Attachments.GetFileType(objAttachment.filename);
                //objAttachment.downloads = Convert.ToInt32(dr["Downloads"]);
                //objAttachment.readperm = Convert.ToInt32(dr["DownloadRequire"]);
                objAttachment.postdatetime = Convert.ToDateTime(dr["uploaddate"]);

                attachmentlist.Add(objAttachment);
            }
            dr.Close();
            dr.Dispose();
            return attachmentlist;
        }

        #endregion
    }
}
