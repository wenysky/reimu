using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
