using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.IO;

///功能：常用小功能 静态
///完成时间：2009-3-2
///作者：一孑
///遗留问题：随着系统完善补充
///开发计划：待定
///说明：无
///版本：00.90.00
///修订：无
namespace SoukeyNetget
{
    static class cTool
    {
           
         //定义（引用）API函数  
        [DllImport("wininet.dll")]  
        private static   extern   bool   InternetGetConnectedState   (out int lpdwFlags ,int dwReserved );  
           
        //判断当前是否连接Internet
        static public  bool IsLinkInternet ()
        {  
            int   lfag=0;
            bool IsInternet;

            if (InternetGetConnectedState(out lfag, 0))
                IsInternet = true;
            else
                IsInternet = false;

            return IsInternet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">指定的Url地址</param>
        /// <param name="Isbool">是否去除网页源码中的回车换行符，true 去除</param>
        /// <returns></returns>
        static  public string GetHtmlSource(string url,bool Isbool)
        {
            string charSet = "";
            WebClient myWebClient = new WebClient();


            //获取或设置用于对向 Internet 资源的请求进行身份验证的网络凭据。   
            myWebClient.Credentials = CredentialCache.DefaultCredentials;


            byte[] myDataBuffer;
            string strWebData;

            try
            {
                //从资源下载数据并返回字节数组。（加@是因为网址中间有"/"符号）   
                myDataBuffer = myWebClient.DownloadData(@url);
                strWebData = Encoding.Default.GetString(myDataBuffer);
            }
            catch (System.Net.WebException ex ) 
            {
                return "";
            }

            //获取此页面的编码格式
            Match charSetMatch = Regex.Match(strWebData, "<meta([^<]*)charset=([^<]*)\"", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            string webCharSet = charSetMatch.Groups[2].Value;
            if (charSet == null || charSet == "")
                charSet = webCharSet;

            if (charSet != null && charSet != "" && Encoding.GetEncoding(charSet) != Encoding.Default)
                strWebData = Encoding.GetEncoding(charSet).GetString(myDataBuffer);

            if (Isbool == true)
            {
                strWebData = Regex.Replace(strWebData, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                strWebData.Replace(@"\r\n", "");
            }

            return strWebData;

        }

        //去除字符串的回车换行符号
        static public string ClearFlag(string str)
        {
            str = Regex.Replace(str, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            str.Replace(@"\r\n", "");

            return str;
        }

        //根据指定网址判断当前页面的编码
        static public string GetWebpageCode(string url,cGlobalParas.WebCode WebCode)
        {
            string charSet = "";

            WebClient myWebClient = new WebClient();    

            myWebClient.Credentials = CredentialCache.DefaultCredentials;

            //从资源下载数据并返回字节数组。（加@是因为网址中间有"/"符号） 
            byte[] myDataBuffer = myWebClient.DownloadData(url);
            string strWebData = Encoding.Default.GetString(myDataBuffer);

            //获取网页字符编码描述信息 
            Match charSetMatch = Regex.Match(strWebData, "<meta([^<]*)charset=([^<]*)\"", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            string webCharSet = charSetMatch.Groups[2].Value;
            if (charSet == null || charSet == "")
                charSet = webCharSet;
            
            return charSet;

        }

        static private int CountUrl(string WebUrl)
        {
            int Count = 0;
            Match Para = Regex.Match(WebUrl, "{.*}");
            
            return Count;
        }

        //将大写字母转成小写字母
        static public string LetterToLower(string str)
        {
            if (str == "" || str == null)
                return "";

            string lowerStr="";
            char c;

            for (int i=0;i<str.Length ;i++)
            {
                c =char .Parse ( str.Substring(i, 1));

                if (Char.IsUpper(c))
                {
                    c = Char.ToLower(c);
                }
                lowerStr += c;
            }

            return lowerStr;
           
        }

        //将字符串中的字符（转义的）进行替换
        //这个替换真的有点头晕，对正则理解还不到位，不知道是否可以一次按照分组那样的形式对应进行替换
        //请高手修改此类，这样写实在是不得已，呵呵
        static public string ReplaceTrans(string str)
        {
            if (str == "" || str==null )
                return "";

            string conStr="";
            if (Regex.IsMatch(str, "['\"<>&]"))
            {
                Regex re = new Regex("&", RegexOptions.IgnoreCase);
                str = re.Replace(str, "&amp;");
                re = null;

                re = new Regex("<", RegexOptions.IgnoreCase);
                str = re.Replace(str, "&lt;");
                re = null;

                re = new Regex(">", RegexOptions.IgnoreCase);
                str = re.Replace(str, "&gt;");
                re = null;

                re = new Regex("'", RegexOptions.IgnoreCase);
                str = re.Replace(str, "&apos;");
                re = null;

                re = new Regex("\"", RegexOptions.IgnoreCase);
                str = re.Replace(str, "&quot;");
                re = null;
                conStr = str;

            }
            else
            {
                conStr = str;
            }
            return conStr;
        }

        //正则表达式转义
        static public string RegexReplaceTrans(string str)
        {
            if (str == "" || str == null)
                return "";

            string conStr = "";
            if (Regex.IsMatch(str, @"[\.\*\[\]\?\\]"))
            {
                Regex re = new Regex(@"\.", RegexOptions.IgnoreCase);
                str = re.Replace(str, @"\.");
                re = null;

                re = new Regex(@"\*", RegexOptions.IgnoreCase);
                str = re.Replace(str, @"\*");
                re = null;

                re = new Regex(@"\[", RegexOptions.IgnoreCase);
                str = re.Replace(str, @"\[");
                re = null;

                re = new Regex(@"\]", RegexOptions.IgnoreCase);
                str = re.Replace(str, @"\]");
                re = null;

                re = new Regex(@"\?", RegexOptions.IgnoreCase);
                str = re.Replace(str, @"\?");
                re = null;

                conStr = str;

            }
            else
            {
                conStr = str;
            }
            return conStr;
        }

        //用于将字符串转换成UTF-8编码
        static public string ToUtf8(string str)
        {
            if (str == null)
            {
                return string.Empty;
            }
            else
            {
                char[] hexDigits = {  '0', '1', '2', '3', '4','5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};

                Encoding utf8 = Encoding.UTF8;
                StringBuilder result = new StringBuilder();

                for (int i = 0; i < str.Length; i++)
                {
                    string sub = str.Substring(i, 1);
                    byte[] bytes = utf8.GetBytes(sub);

                    for (int j = 0; j < bytes.Length; j++)
                    {
                        result.Append("%" + hexDigits[bytes[j] >> 4] + hexDigits[bytes[j] & 0XF]);
                    }
                }

                return result.ToString();
            }
        }

        //将UTF-8编码转换成字符串
        static public string FromUtf8(string str)
        {
            char[] hexDigits = {  '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};
            List<byte> byteList = new List<byte>(str.Length / 3);

            if (str != null)
            {
                List<string> strList = new List<string>();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < str.Length; ++i)
                {
                    if (str[i] == '%')
                    {
                        strList.Add(str.Substring(i, 3));
                    }
                }

                foreach (string tempStr in strList)
                {
                    int num = 0;
                    int temp = 0;
                    for (int j = 0; j < hexDigits.Length; ++j)
                    {
                        if (hexDigits[j].Equals(tempStr[1]))
                        {
                            temp = j;
                            num = temp << 4;
                        }
                    }

                    for (int j = 0; j < hexDigits.Length; ++j)
                    {
                        if (hexDigits[j].Equals(tempStr[2]))
                        {
                            num += j;
                        }
                    }

                    byteList.Add((byte)num);
                }
            }

            return Encoding.UTF8.GetString(byteList.ToArray());
        }

        static public string UTF8ToGB2312(string str)
        {
            try
            {
                Encoding utf8 = Encoding.GetEncoding(65001);
                Encoding gb2312 = Encoding.GetEncoding("gb2312");//Encoding.Default ,936
                byte[] temp = utf8.GetBytes(str);
                byte[] temp1 = Encoding.Convert(utf8, gb2312, temp);
                string result = gb2312.GetString(temp1);
                return result;
            }
            catch (Exception)//(UnsupportedEncodingException ex)
            {
                return null;
            }
        }

    
    }
}
