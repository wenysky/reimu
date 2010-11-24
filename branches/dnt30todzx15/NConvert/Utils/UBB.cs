using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NConvert.Utils
{
    public class UBB
    {
        private static RegexOptions options = RegexOptions.IgnoreCase;

        public static Regex[] r = new Regex[20];


        static UBB()
        {
            r[0] = new Regex(@"\s*\[code\]([\s\S]+?)\[\/code\]\s*", options);
            r[1] = new Regex(@"(\[upload=([^\]]{1,4})\])(.*?)(\[\/upload\])", options);
            r[2] = new Regex(@"viewfile.asp\?id=(\d{1,})", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            r[3] = new Regex(@"(\[uploadimage\])(.*?)(\[\/uploadimage\])", options);
            r[4] = new Regex(@"viewfile.asp\?id=(\d{1,})", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            r[5] = new Regex(@"(\[uploadfile\])(.*?)(\[\/uploadfile\])", options);
            r[6] = new Regex(@"viewfile.asp\?id=(\d{1,})", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            r[7] = new Regex(@"(\[upload\])(.*?)(\[\/upload\])", options);
            r[8] = new Regex(@"viewfile.asp\?id=(\d{1,})", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            r[9] = new Regex(@"(\r\n((&nbsp;)|　)+)(?<正文>\S+)", options);
            r[10] = new Regex(@"\s*\[hide\][\n\r]*([\s\S]+?)[\n\r]*\[\/hide\]\s*", RegexOptions.IgnoreCase);
            //r[11] = new Regex(@"\s*\[table(=(\d{1,4}%?)(,([\(\)%,#\w]+))?)?\][\n\r]*([\s\S]+?)[\n\r]*\[\/table\]\s*", options);
            r[11] = new Regex(@"\[table(?:=(\d{1,4}%?)(?:,([\(\)%,#\w ]+))?)?\]\s*([\s\S]+?)\s*\[\/table\]", options);
            r[12] = new Regex(@"\[media=(\w{1,4}),(\d{1,4}),(\d{1,4}),(\d)\]\s*([^\[\<\r\n]+?)\s*\[\/media\]", options);
            r[13] = new Regex(@"\[attach\](\d+)(\[/attach\])*", options);
            r[14] = new Regex(@"\[attachimg\](\d+)(\[/attachimg\])*", options);
        }

        /// <summary>
        /// UBB代码处理函数
        /// </summary>
        ///	<param name="_postpramsinfo">UBB转换参数表</param>
        /// <returns>输出字符串</returns>
        public static string UBBToHTML(string UBBString)
        {

            Match m;

            string sDetail = UBBString;

            StringBuilder sb = new StringBuilder();
            int pcodecount = -1;
            string sbStr = "";
            //if (_postpramsinfo.Bbcodeoff == 0)
            //{

            //for (m = r[0].Match(sDetail); m.Success; m = m.NextMatch())
            //{
            //    sbStr = Parsecode(m.Groups[1].ToString(), prefix, ref pcodecount, ref sb);
            //    sDetail = sDetail.Replace(m.Groups[0].ToString(), sbStr);
            //}
            //}


            sDetail = Regex.Replace(sDetail, @"\[b(?:\s*)\]", "<b>", options);
            sDetail = Regex.Replace(sDetail, @"\[i(?:\s*)\]", "<i>", options);
            sDetail = Regex.Replace(sDetail, @"\[u(?:\s*)\]", "<u>", options);
            sDetail = Regex.Replace(sDetail, @"\[/b(?:\s*)\]", "</b>", options);
            sDetail = Regex.Replace(sDetail, @"\[/i(?:\s*)\]", "</i>", options);
            sDetail = Regex.Replace(sDetail, @"\[/u(?:\s*)\]", "</u>", options);

            // Sub/Sup
            //
            sDetail = Regex.Replace(sDetail, @"\[sup(?:\s*)\]", "<sup>", options);
            sDetail = Regex.Replace(sDetail, @"\[sub(?:\s*)\]", "<sub>", options);
            sDetail = Regex.Replace(sDetail, @"\[/sup(?:\s*)\]", "</sup>", options);
            sDetail = Regex.Replace(sDetail, @"\[/sub(?:\s*)\]", "</sub>", options);

            // P
            //
            sDetail = Regex.Replace(sDetail, @"((\r\n)*\[p\])(.*?)((\r\n)*\[\/p\])", "<p>$3</p>", RegexOptions.IgnoreCase | RegexOptions.Singleline);




            // Anchors
            //
            sDetail = Regex.Replace(sDetail, @"\[url(?:\s*)\](www\.(.*?))\[/url(?:\s*)\]", "<a href=\"http://$1\" target=\"_blank\">$1</a>", options);
            sDetail = Regex.Replace(sDetail, @"\[url(?:\s*)\]\s*((https?://|ftp://|gopher://|news://|telnet://|rtsp://|mms://|callto://|bctp://|ed2k://|tencent)([^\[""']+?))\s*\[\/url(?:\s*)\]", "<a href=\"$1\" target=\"_blank\">$1</a>", options);
            sDetail = Regex.Replace(sDetail, @"\[url=www.([^\[""']+?)(?:\s*)\]([\s\S]+?)\[/url(?:\s*)\]", "<a href=\"http://www.$1\" target=\"_blank\">$2</a>", options);
            sDetail = Regex.Replace(sDetail, @"\[url=((https?://|ftp://|gopher://|news://|telnet://|rtsp://|mms://|callto://|bctp://|ed2k://|tencent://)([^\[""']+?))(?:\s*)\]([\s\S]+?)\[/url(?:\s*)\]", "<a href=\"$1\" target=\"_blank\">$4</a>", options);

            // Email
            //
            sDetail = Regex.Replace(sDetail, @"\[email(?:\s*)\](.*?)\[\/email\]", "<a href=\"mailto:$1\" target=\"_blank\">$1</a>", options);
            sDetail = Regex.Replace(sDetail, @"\[email=(.[^\[]*)(?:\s*)\](.*?)\[\/email(?:\s*)\]", "<a href=\"mailto:$1\" target=\"_blank\">$2</a>", options);

            #region Font

            // Font
            //
            sDetail = Regex.Replace(sDetail, @"\[color=([^\[\<]+?)\]", "<font color=\"$1\">", options);
            sDetail = Regex.Replace(sDetail, @"\[size=(\d+?)\]", "<font size=\"$1\">", options);
            sDetail = Regex.Replace(sDetail, @"\[size=(\d+(\.\d+)?(px|pt|in|cm|mm|pc|em|ex|%)+?)\]", "<font style=\"font-size: $1\">", options);
            sDetail = Regex.Replace(sDetail, @"\[font=([^\[\<]+?)\]", "<font face=\"$1\">", options);

            sDetail = Regex.Replace(sDetail, @"\[align=([^\[\<]+?)\]", "<p align=\"$1\">", options);
            sDetail = Regex.Replace(sDetail, @"\[float=(left|right)\]", "<br style=\"clear: both\"><span style=\"float: $1;\">", options);


            sDetail = Regex.Replace(sDetail, @"\[/color(?:\s*)\]", "</font>", options);
            sDetail = Regex.Replace(sDetail, @"\[/size(?:\s*)\]", "</font>", options);
            sDetail = Regex.Replace(sDetail, @"\[/font(?:\s*)\]", "</font>", options);
            sDetail = Regex.Replace(sDetail, @"\[/align(?:\s*)\]", "</p>", options);
            sDetail = Regex.Replace(sDetail, @"\[/float(?:\s*)\]", "</span>", options);

            // BlockQuote
            //
            sDetail = Regex.Replace(sDetail, @"\[indent(?:\s*)\]", "<blockquote>", options);
            sDetail = Regex.Replace(sDetail, @"\[/indent(?:\s*)\]", "</blockquote>", options);
            sDetail = Regex.Replace(sDetail, @"\[simpletag(?:\s*)\]", "<blockquote>", options);
            sDetail = Regex.Replace(sDetail, @"\[/simpletag(?:\s*)\]", "</blockquote>", options);

            // List
            //
            sDetail = Regex.Replace(sDetail, @"\[list\]", "<ul>", options);
            sDetail = Regex.Replace(sDetail, @"\[list=(1|A|a|I|i| )\]", "<ul type=\"$1\">", options);
            sDetail = Regex.Replace(sDetail, @"\[\*\]", "<li>", options);
            sDetail = Regex.Replace(sDetail, @"\[/list\]", "</ul>", options);


            #endregion

            // shadow
            //
            sDetail = Regex.Replace(sDetail, @"(\[SHADOW=)(\d*?),(#*\w*?),(\d*?)\]([\s]||[\s\S]+?)(\[\/SHADOW\])", "<table width='$2'  style='filter:SHADOW(COLOR=$3, STRENGTH=$4)'>$5</table>", options);

            // glow
            //
            sDetail = Regex.Replace(sDetail, @"(\[glow=)(\d*?),(#*\w*?),(\d*?)\]([\s]||[\s\S]+?)(\[\/glow\])", "<table width='$2'  style='filter:GLOW(COLOR=$3, STRENGTH=$4)'>$5</table>", options);

            // center
            //
            sDetail = Regex.Replace(sDetail, @"\[center\]([\s]||[\s\S]+?)\[\/center\]", "<center>$1</center>", options);



            #region 处理[quote][/quote]标记

            int intQuoteIndexOf = sDetail.ToLower().IndexOf("[quote]");
            int quotecount = 0;
            while (intQuoteIndexOf >= 0 && sDetail.ToLower().IndexOf("[/quote]") >= 0 && quotecount < 3)
            {
                quotecount++;
                sDetail = Regex.Replace(sDetail, @"\[quote\]([\s\S]+?)\[/quote\]", "<br /><br /><div class=\"msgheader\">引用:</div><div class=\"msgborder\">$1</div>", options);


                intQuoteIndexOf = sDetail.ToLower().IndexOf("[quote]", intQuoteIndexOf + 7);
            }
            #endregion

            #region 将网址字符串转换为链接


            sDetail = sDetail.Replace("&amp;", "&");

            // p2p link
            sDetail = Regex.Replace(sDetail, @"^((tencent|ed2k|thunder|vagaa):\/\/[\[\]\|A-Za-z0-9\.\/=\?%\-&_~`@':+!]+)", "<a target=\"_blank\" href=\"$1\">$1</a>", options);
            sDetail = Regex.Replace(sDetail, @"((tencent|ed2k|thunder|vagaa):\/\/[\[\]\|A-Za-z0-9\.\/=\?%\-&_~`@':+!]+)$", "<a target=\"_blank\" href=\"$1\">$1</a>", options);
            sDetail = Regex.Replace(sDetail, @"[^>=\]""]((tencent|ed2k|thunder|vagaa):\/\/[\[\]\|A-Za-z0-9\.\/=\?%\-&_~`@':+!]+)", "<a target=\"_blank\" href=\"$1\">$1</a>", options);
            #endregion


            #region 处理[img][/img]标记


            //控制签名html长度
            //if (_postpramsinfo.Signature == 1)
            //{
            sDetail = Regex.Replace(sDetail, @"\[img\]\s*(http://[^\[\<\r\n]+?)\s*\[\/img\]", "<img src=\"$1\" border=\"0\" />", options);
            sDetail = Regex.Replace(sDetail, @"\[img=(\d{1,4})[x|\,](\d{1,4})\]\s*(http://[^\[\<\r\n]+?)\s*\[\/img\]", "<img src=\"$3\" width=\"$1\" height=\"$2\" border=\"0\" />", options);
            sDetail = Regex.Replace(sDetail, @"\[image\](http://[\s\S]+?)\[/image\]", "<img src=\"$1\" border=\"0\" />", options);
            //}
            //else
            //{
            //    sDetail = Regex.Replace(sDetail, @"\[img\]\s*(http://[^\[\<\r\n]+?)\s*\[\/img\]", "<img src=\"$1\" border=\"0\" onload=\"attachimg(this, 'load');\" onclick=\"zoom(this)\" />", options);
            //    sDetail = Regex.Replace(sDetail, @"\[img=(\d{1,4})[x|\,](\d{1,4})\]\s*(http://[^\[\<\r\n]+?)\s*\[\/img\]", "<img src=\"$3\" width=\"$1\" height=\"$2\" border=\"0\" onload=\"attachimg(this, 'load');\" onclick=\"zoom(this)\"  />", options);
            //    sDetail = Regex.Replace(sDetail, @"\[image\](http://[\s\S]+?)\[/image\]", "<img src=\"$1\" border=\"0\" />", options);
            //}

            #endregion

            #region 处理空格

            sDetail = sDetail.Replace("\t", "&nbsp; &nbsp; ");
            sDetail = sDetail.Replace("  ", "&nbsp; ");

            #endregion

            // [r/]
            //
            sDetail = Regex.Replace(sDetail, @"\[r/\]", "\r", options);

            // [n/]
            //
            sDetail = Regex.Replace(sDetail, @"\[n/\]", "\n", options);

            #region 处理换行

            //处理换行,在每个新行的前面添加两个全角空格
            for (m = r[9].Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(), "<br />　　" + m.Groups["正文"].ToString());
            }

            //处理换行,在每个新行的前面添加两个全角空格
            sDetail = sDetail.Replace("\r\n", "<br />");
            sDetail = sDetail.Replace("\r", "");
            sDetail = sDetail.Replace("\n\n", "<br /><br />");
            sDetail = sDetail.Replace("\n", "<br />");
            sDetail = sDetail.Replace("{rn}", "\r\n");
            sDetail = sDetail.Replace("{nn}", "\n\n");
            sDetail = sDetail.Replace("{r}", "\r");
            sDetail = sDetail.Replace("{n}", "\n");

            #endregion

            return sDetail;


        }
    }
}
