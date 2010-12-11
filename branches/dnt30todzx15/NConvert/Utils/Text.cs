using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace NConvert.Utils
{
    public class Text
    {
        private static char[] constant ={
                                            '0','1','2','3','4','5','6','7','8','9',
                                            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
                                            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
                                        };
        public enum RandomType
        {
            All,
            Number,
            Uppercased,
            Lowercased,
            NumberAndUppercased,
            NumberAndLowercased,
            UppercasedAndLowercased,
        }
        public static string GenerateRandom(int Length, RandomType rt)
        {
            int initsize = 0;
            int beginsize = 0;
            int endsize = 0;
            Boolean IsCross = false;
            switch (rt)
            {
                case RandomType.All:
                    {
                        initsize = 62;
                        beginsize = 1;
                        endsize = 62;
                        //IsCross = false;
                        break;
                    }
                case RandomType.Lowercased:
                    {
                        initsize = 26;
                        beginsize = 11;
                        endsize = 36;
                        //IsCross = false;
                        break;
                    }
                case RandomType.Uppercased:
                    {
                        initsize = 26;
                        beginsize = 37;
                        endsize = 62;
                        // IsCross = false;
                        break;
                    }
                case RandomType.Number:
                    {
                        initsize = 10;
                        beginsize = 1;
                        endsize = 10;
                        //IsCross = false;
                        break;
                    }
                case RandomType.UppercasedAndLowercased:
                    {
                        initsize = 52;
                        beginsize = 11;
                        endsize = 62;
                        //IsCross = false;
                        break;
                    }
                case RandomType.NumberAndLowercased:
                    {
                        initsize = 36;
                        beginsize = 1;
                        endsize = 36;
                        //IsCross = false;
                        break;
                    }
                case RandomType.NumberAndUppercased:
                    {
                        IsCross = true;
                        break;
                    }
            }


            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(initsize);
            Random rd = new Random();
            if (!IsCross)
            {
                for (int i = 0; i < Length; i++)
                {
                    newRandom.Append(constant[rd.Next(beginsize, endsize)]);
                }
            }
            else
            {
                for (int i = 0; i < Length; i++)
                {
                    newRandom.Append(constant[rd.Next(1, 10)]);
                    newRandom.Append(constant[rd.Next(37, 62)]);
                }
            }

            return newRandom.ToString();
        }

        /// <summary>
        /// MD5加密(32位)
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <returns>32位加密后的字符串</returns>
        public static string MD5(string str)
        {
            return MD5(str, false);
        }
        /// <summary>
        /// MD5加密(32/16位)
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="cry16">是否采用16位加密方式</param>
        /// <returns>加密后的字符串</returns>
        public static string MD5(string str, bool cry16)
        {
            if (cry16) //16位MD5加密（取32位加密的9~25字符） 
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").Substring(8, 16);
            }
            else//32位加密 
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
            }
        }




        public static string GetMatch(string strSource, string strRegex)
        {
            try
            {
                Regex r = new Regex(strRegex, RegexOptions.IgnoreCase);
                MatchCollection m = r.Matches(strSource);

                if (m.Count <= 0)
                    return string.Empty;
                else
                    return m[0].Groups[1].Value;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static MatchCollection GetMatchFull(string strSource, string strRegex)
        {
            return GetMatchFull(strSource, strRegex, RegexOptions.IgnoreCase);
        }
        public static MatchCollection GetMatchFull(string strSource, string strRegex, RegexOptions options)
        {
            try
            {
                Regex r = new Regex(strRegex, options);
                MatchCollection m = r.Matches(strSource);

                if (m.Count <= 0)
                    return null;
                else
                    return m;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 正则替换
        /// </summary>
        /// <param name="pattern">替换规则</param>
        /// <param name="input">原始字符串</param>
        /// <param name="replacement">替换为</param>
        /// <returns>替换后的字符串</returns>
        public static string ReplaceRegex(string pattern, string input, string replacement)
        {
            return ReplaceRegex(pattern, input, replacement, RegexOptions.IgnoreCase);
        }

        public static string ReplaceRegex(string pattern, string input, string replacement, RegexOptions options)
        {
            // Regex search and replace
            Regex regex = new Regex(pattern, options);
            return regex.Replace(input, replacement);
        }


        public static bool IsIncludeHtmlTag(string content)
        {
            string regexstr = @"<[^>]*>";
            MatchCollection mc = GetMatchFull(content, regexstr);
            if (mc != null && mc.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 移除Html标记
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveHtml(string content)
        {
            string regexstr = @"<[^>]*>";
            return Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 返回 HTML 字符串的解码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string HtmlDecode(string str)
        {
            return HttpUtility.HtmlDecode(str);
        }
    }
}
