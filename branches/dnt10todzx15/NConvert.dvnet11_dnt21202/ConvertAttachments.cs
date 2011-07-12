using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;
using NConvert.Entity;
using Yuwen.Tools.Data;

namespace NConvert.dvnet11_dnt21202
{
    public partial class Provider : IProvider
    {
        #region IProvider 成员


        public int GetAttachmentsRecordCount()
        {
            return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT([Id]) FROM {0}Attach", MainForm.cic.SrcDbTablePrefix)));
        }

        public List<Attachments> GetAttachmentList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM {0}Attach ORDER BY Id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM {0}Attach WHERE Id NOT IN (SELECT TOP {2} Id FROM {0}Attach) ORDER BY Id", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion

            System.Data.Common.DbDataReader dr = MainForm.srcDBH.ExecuteReader(sql);
            List<Attachments> attachmentlist = new List<Attachments>();
            while (dr.Read())
            {
                Attachments objAttachment = new Attachments();
                objAttachment.aid = Convert.ToInt32(dr["Id"]);
                objAttachment.pid = Convert.ToInt32(dr["ReplyId"]);
                objAttachment.tid = Convert.ToInt32(dr["TopicId"]);
                objAttachment.uid = dr["UID"] == DBNull.Value ? -1 : Convert.ToInt32(dr["UID"]);
                objAttachment.attachment = dr["AttachName"] == DBNull.Value ? "无标题" : dr["AttachName"].ToString();
                objAttachment.filename = dr["AttachName"] == DBNull.Value ? "nothing.zip" : dr["AttachPath"].ToString();
                objAttachment.description = objAttachment.attachment;
                objAttachment.filesize = dr["AttachSize"] == DBNull.Value ? -1 : Convert.ToInt32(dr["AttachSize"]);
                objAttachment.filetype = Utils.Attachments.GetFileType(objAttachment.filename);
                objAttachment.downloads = Convert.ToInt32(dr["Downloads"]);
                objAttachment.readperm = Convert.ToInt32(dr["DownloadRequire"]);
                objAttachment.postdatetime = Convert.ToDateTime(dr["UploadTime"]);

                attachmentlist.Add(objAttachment);
            }
            dr.Close();
            dr.Dispose();
            return attachmentlist;
        }

        #endregion
    }
}
