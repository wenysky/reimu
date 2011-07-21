using System;
using System.Collections.Generic;
using System.Text;
using Yuwen.Tools.Data;
using System.Data;
using System.IO;
using Discuz.Common;

namespace ConvertAvatars
{
    class Program
    {
        static void Main(string[] args)
        {
            DBHelper srcDBH = new DBHelper();
            DataTable avatars = srcDBH.ExecuteDataSet(@"SELECT uid,avatar FROM [dnt_userfields] WHERE avatar>'' AND avatar NOT LIKE 'avatars\common\%' ORDER BY uid").Tables[0];

            int totalCount = avatars.Rows.Count;
            int count = 0;
            System.Console.WriteLine("总共找到了{0}条数据", totalCount);
            foreach (DataRow dr in avatars.Rows)
            {
                string url = dr["avatar"].ToString().Trim().Trim('/').Replace('/', Path.DirectorySeparatorChar);
                string sourceAvatarPath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "source" +
                    Path.DirectorySeparatorChar + url
                    );
                string targetAvatarPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "target");
                string uid = dr["uid"].ToString().PadLeft(9, '0');

#if DEBUG
                if (!File.Exists(sourceAvatarPath))
                {
                    string debugPath = Path.GetDirectoryName(sourceAvatarPath);
                    if (!Directory.Exists(debugPath))
                    {
                        Directory.CreateDirectory(debugPath);
                    }
                    try
                    {

                        File.Copy(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "nophoto.gif"), sourceAvatarPath, true);
                    }
                    catch
                    {
                        continue;
                    }
                }
#endif



                if (File.Exists(sourceAvatarPath))
                {
                    string destDir = Path.Combine(targetAvatarPath, uid.Substring(0, 3));
                    destDir = Path.Combine(destDir, uid.Substring(3, 2));
                    destDir = Path.Combine(destDir, uid.Substring(5, 2));

                    //string.Format("{0}avatars/upload/{1}/{2}/{3}", targetAvatarPath, uid.Substring(0, 3), uid.Substring(3, 2), uid.Substring(5, 2));
                    try
                    {
                        if (!Directory.Exists(destDir))
                        {
                            Directory.CreateDirectory(destDir);
                        }

                        string destLargeAvatarPath = Path.Combine(destDir, uid.Substring(7, 2) + "_avatar_big.jpg");
                        string destMediumAvatarPath = Path.Combine(destDir, uid.Substring(7, 2) + "_avatar_middle.jpg");
                        string destSmallAvatarPath = Path.Combine(destDir, uid.Substring(7, 2) + "_avatar_small.jpg");


                        File.Copy(sourceAvatarPath, destLargeAvatarPath, true);
                        File.Copy(sourceAvatarPath, destMediumAvatarPath, true);
                        File.Copy(sourceAvatarPath, destSmallAvatarPath, true);
                        Thumbnail thumb = new Thumbnail();
                        thumb.SetImage(destSmallAvatarPath);
                        thumb.SaveThumbnailImage(48, 48);
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.Message);
                    }
                    count++;
                    System.Console.Write(".");
                    if (count % 200 == 0)
                    {
                        System.Console.WriteLine("进度{1}/{2}", count, totalCount);
                    }
                }
                else
                {
                    System.Console.WriteLine("");
                    System.Console.WriteLine("未找到uid={0}的头像{1}", dr["uid"], dr["avatar"]);
                }
            }
            System.Console.WriteLine("");
            System.Console.WriteLine("提示:头像转换程序已经成功转换了{0}/{1}个头像.", count, totalCount);
        }
    }
}
