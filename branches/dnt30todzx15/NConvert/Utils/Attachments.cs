using System;
using System.Collections.Generic;
using System.Text;

namespace NConvert.Utils
{
    public class Attachments
    {
        public static string GetFileType(string filename)
        {
            string mimeType;
            switch (System.IO.Path.GetExtension(filename.Trim()).ToLower())
            {
                case ".jpg":
                    mimeType = "image/jpeg";
                    break;
                case ".gif":
                    mimeType = "image/gif";
                    break;
                case ".rar":
                    mimeType = "application/octet-stream";
                    break;
                case ".zip":
                    mimeType = "application/x-zip-compressed";
                    break;
                case ".doc":
                    mimeType = "application/msword";
                    break;
                default:
                    mimeType = "application/octet-stream";
                    break;
            }
            return mimeType;
        }
    }
}
