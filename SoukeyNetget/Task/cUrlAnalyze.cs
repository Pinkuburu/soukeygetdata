using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Web;

///功能：任务中URL规则解析处理
///完成时间：2009-3-2
///作者：一孑
///遗留问题：无
///开发计划：无
///说明：无 
///版本：01.00.00
///修订：无
namespace SoukeyNetget.Task
{
    //主要处理带有参数的网址信息
    //分解/解析 等
    public class cUrlAnalyze
    {
        public cUrlAnalyze()
        {
        }

        ~cUrlAnalyze()
        {
        }

        //根据指定的导航规则进行页面导航
        public List<string> ParseUrlRule(string Url, string UrlRule, bool IsUrlEncode)
        {
            return  PUrlRule(Url, UrlRule, IsUrlEncode, "");
        }

        public List<string> ParseUrlRule(string Url, string UrlRule, bool IsUrlEncode, string UrlEncode)
        {
           return  PUrlRule(Url, UrlRule, IsUrlEncode, UrlEncode);
        }

        public List<string> PUrlRule(string Url, string UrlRule,bool IsUrlEncode,string UrlEncode)
        {
            string Url1;
            List<string> Urls=new List<string> ();

            //判断网址是否存在参数，如果存在参数则取出第一个可用网址
            if (Regex.IsMatch(Url, "{.*}"))
            {
                List<string> Urls1 = SplitWebUrl(Url,IsUrlEncode ,UrlEncode );
                Url1 = Urls1[0].ToString();
            }
            else
            {
                Url1 = Url;
            }

            //返回网址的源码，并根据提取规则提取导航的网址
            string UrlSource= cTool.GetHtmlSource(Url1,true );
            //string Rule=@"(?<=href=[\W])[" + UrlRule + "](\\S*)(?=[\\s'\"])";

            if (UrlSource == "")
            {
                return null ;
            }

            string Rule=@"(?<=href=[\W])" + cTool.RegexReplaceTrans(UrlRule) + @"(\S[^'"">]*)(?=[\s'""])";

            Regex re = new Regex(Rule, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            MatchCollection aa = re.Matches(UrlSource);
            foreach (Match ma in aa)
            {
                Urls.Add(ma.Value.ToString());
            }

            return Urls;
        }

        //拆分网址
        public List<string> SplitWebUrl(string Url, bool IsUrlEncode)
        {
            return SplitUrl(Url, IsUrlEncode, "");
        }

        public List<string> SplitWebUrl(string Url, bool IsUrlEncode, string UrlEncode)
        {
            return SplitUrl(Url, IsUrlEncode, UrlEncode);
        }

        private List<string> SplitUrl(string Url, bool IsUrlEncode, string UrlEncode)
        {
            List<string> tmp_list_Url=new List<string>() ;
            List<string> list_Url;
            List<string> g_Url = new List<string>();
            string Para = "";

            if (!Regex.IsMatch(Url, "{.*}"))
            {
                tmp_list_Url.Add(Url);
                return tmp_list_Url;
            }

            //增加tmp_list_Url的初始值
            //初始值为Url第一个参数前所有字符
            //应该以{为准
            tmp_list_Url.Add(Url.Substring (0,Url.IndexOf ("{")));

            //初始化参数值
            //Para = Url.Substring(Url.IndexOf("?") + 1, Url.IndexOf("=") - Url.IndexOf("?") );

            //判断是否存在
            while (Regex.IsMatch(Url, "{.*}"))
            {
                //提取参数内容
                string strMatch = "(?<={)[^}]*(?=})";
                Match s = Regex.Match(Url, strMatch,RegexOptions.IgnoreCase);
                string UrlPara = s.Groups[0].Value;

                g_Url = getListUrl(UrlPara,IsUrlEncode ,UrlEncode );

                list_Url = new List<string>();
               
                for (int j=0 ;j<tmp_list_Url.Count ;j++)
                {
                    for (int i=0;i<g_Url.Count ;i++)
                    {
                        list_Url.Add(tmp_list_Url[j].ToString() + Para + g_Url[i].ToString());

                    }
                }

                tmp_list_Url =list_Url ;
                list_Url = null;

                Url = Url.Substring(Url.IndexOf("}")+1, Url.Length - Url.IndexOf("}")-1);

                //判断是否还有参数，如果有，则截取中间部分以拼接网址
                if (Url.IndexOf("{") > 0)
                {
                    //Para = Url.Substring(Url.IndexOf("&"), Url.IndexOf("=") - Url.IndexOf("&") + 1);
                    Para =Url.Substring (0,Url.IndexOf ("{"));
                }
                 
            }

            list_Url = new List<string>();
            
            for (int m = 0; m < tmp_list_Url.Count; m++)
            {
                list_Url.Add(tmp_list_Url[m].ToString() + Url);
            }

            tmp_list_Url = null;
            g_Url = null;

            return list_Url;

        }

        //判断当前所含参数的数量,其中包括字典的内容
        public int GetUrlCount(string Url)
        {
            if (Url == "")
                return 0;

            int UrlCount = 1;
            List<string> g_Url = new List<string>();

            while (Regex.IsMatch(Url, "{.*}"))
            {
                //提取参数内容
                string strMatch = "(?<={)[^}]*(?=})";
                Match s = Regex.Match(Url, strMatch, RegexOptions.IgnoreCase);
                string UrlPara = s.Groups[0].Value;

                //因为仅计算网址的数量，所以不需要对url进行编码转换，如果有需要转换的话，也不需要
                g_Url = getListUrl(UrlPara,false,"");

                UrlCount = UrlCount * g_Url.Count;
                Url = Url.Substring(Url.IndexOf("}") + 1, Url.Length - Url.IndexOf("}") - 1);

            }

            if (UrlCount == 0)
            {
                UrlCount = 1;
            }
            return UrlCount;
        }

        private List<string> getListUrl(string dicPre,bool IsUrlEncode,string UrlEncode)
        {
            List<string> list_Para=new List<string>();
            Regex re;
            MatchCollection aa;
            int step;
            int startI;
            int endI;
            int i = 0;

            switch (dicPre.Substring(0, dicPre.IndexOf(":")))
            {
                
                case "Num":

                    re = new Regex("([\\-\\d]+)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    aa = re.Matches(dicPre);

                    startI = int.Parse(aa[0].Groups[0].Value.ToString());
                    endI = int.Parse(aa[1].Groups[0].Value.ToString());
                    step = int.Parse(aa[2].Groups[0].Value.ToString());

                    if (step > 0)
                    {
                        for (i = startI; i <= endI; i = i + step)
                        {
                            list_Para.Add(i.ToString());
                        }
                    }
                    else
                    {
                        for (i = startI; i >= endI; i = i + step)
                        {
                            list_Para.Add(i.ToString());
                        }
                    }

                    

                    break;
                
                case "Letter":
                    startI =getAsc( dicPre.Substring(dicPre.IndexOf(":") + 1, 1));
                    endI  =getAsc( dicPre.Substring(dicPre.IndexOf(",") + 1, 1));

                    if (startI > endI)
                    {
                        step = -1;
                    }
                    else
                    {
                        step = 1;
                    }

                    for (i = startI; i <= endI; i = i + step)
                    {
                        char s;
                        s = Convert.ToChar(i);  
                        list_Para.Add(s.ToString ());
                    }

                    break;
                case "Dict":
                    cDict d = new cDict();
                    string tClass = dicPre.Substring(dicPre.IndexOf(":") + 1, dicPre.Length - dicPre.IndexOf(":") - 1);
                    DataView dName = d.GetDict(tClass);
                   
                    if (IsUrlEncode == true)
                    {
                        for (i = 0; i < dName.Count; i++)
                        {
                            if ((cGlobalParas.WebCode)(int.Parse (UrlEncode)) == cGlobalParas.WebCode.utf8)
                            {
                                list_Para.Add(HttpUtility .UrlEncode (dName[i].Row["Name"].ToString(),Encoding.UTF8 ));
                            }
                            else
                            {
                                list_Para.Add(HttpUtility.UrlEncode(dName[i].Row["Name"].ToString(),Encoding.GetEncoding ("gb2312")));
                            }
                            
                        }
                    }
                    else
                    { 
                        for (i = 0; i < dName.Count; i++)
                        {
                            list_Para.Add(dName[i].Row["Name"].ToString());
                        }
                    }

                    break;
                default:
                    list_Para = null;
                    break;
            }

            return list_Para;
        }

        private int getAsc(string s)
        {
            byte[] array = new byte[1];
            array = System.Text.Encoding.ASCII.GetBytes(s);
            int asciicode = (int)(array[0]);
            return  asciicode; 
        }
    }
}
