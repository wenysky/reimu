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
            return Convert.ToInt32(MainForm.srcDBH.ExecuteScalar(string.Format("SELECT COUNT(aid) FROM {0}attachments", MainForm.cic.SrcDbTablePrefix)));
        }

        public List<Attachments> GetAttachmentList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM {0}attachments ORDER BY aid", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM {0}attachments WHERE aid NOT IN (SELECT TOP {2} aid FROM {0}attachments ORDER BY aid) ORDER BY aid", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion

            System.Data.Common.DbDataReader dr = MainForm.srcDBH.ExecuteReader(sql);
            List<Attachments> attachmentlist = new List<Attachments>();
            while (dr.Read())
            {
                Attachments objAttachment = new Attachments();
                objAttachment.aid = Convert.ToInt32(dr["aid"]);
                objAttachment.tid = Convert.ToInt32(dr["tid"]);
                objAttachment.pid = Convert.ToInt32(dr["pid"]);
                objAttachment.width = Convert.ToInt32(dr["width"]);
                objAttachment.dateline = Utils.TypeParse.DateTime2TimeStamp(Convert.ToDateTime(dr["postdatetime"]));
                objAttachment.readperm = Convert.ToInt32(dr["readperm"]);
                objAttachment.price = Convert.ToInt32(dr["attachprice"]);
                objAttachment.filename = dr["attachment"].ToString();
                objAttachment.filetype = "application/octet-stream";// dr["filetype"].ToString();
                objAttachment.filesize = Convert.ToInt32(dr["filesize"]);
                objAttachment.attachment = dr["filename"].ToString().Replace("\\", "/");
                objAttachment.downloads = Convert.ToInt32(dr["downloads"]);
                List<string> isImage = new List<string>();
                isImage.Add(".jpg");
                isImage.Add(".gif");
                isImage.Add(".png");
                isImage.Add(".jpeg");
                if (isImage.Contains(System.IO.Path.GetExtension(objAttachment.filename.Trim())))
                {
                    objAttachment.isimage = -1;
                }
                else
                {
                    objAttachment.isimage = 0;
                }
                objAttachment.uid = Convert.ToInt32(dr["uid"]);
                objAttachment.thumb = 0;
                objAttachment.remote = 0;
                objAttachment.picid = 0;
                objAttachment.description = 0;

                attachmentlist.Add(objAttachment);
            }
            dr.Close();
            dr.Dispose();
            return attachmentlist;
        }

        #endregion
    }
}
