using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Web;
using System.Reflection;
using System.Resources;

///���ܣ�������URL�����������
///���ʱ�䣺2009-3-2
///���ߣ�һ��
///�������⣺��
///�����ƻ�����
///˵������ 
///�汾��01.10.00
///�޶�����
namespace SoukeyNetget.Task
{
    //��Ҫ������в�������ַ��Ϣ
    //�ֽ�/���� ��
    public class cUrlAnalyze
    {
        public cUrlAnalyze()
        {
        }

        ~cUrlAnalyze()
        {
        }

        ///����ָ���ĵ����������ҳ�浼������1.6�汾�У������˶�㵼���Ĺ���
        ///��ַ����������һ�Զ�Ĺ�ϵ����ÿһ����ĵ�����������һ�ԶࣨҲ����һ��һ�Ĺ�ϵ��
        ///�ڴ������Ǽ������������صĶ������յ���Ҫ�ɼ����ݵ���ַ
        ///��Ϊ�Ƕ�㵼�������������ڵݹ��һ���㷨
        public List<string> ParseUrlRule(string Url, List<cNavigRule> nRules)
        {
            List<string> pUrls = new List<string>();
            List<string> Urls = new List<string>();

            
            
            
            pUrls.Add(Url);

            //��һ�㵼���ֽⶼ�Ǵ�һ����һ��ַ���У�֮����
            //ѡ�񼯺ϣ���Ϊ��ͳһ���ýӿڲ���
            Urls= PUrlRule(pUrls, 1, nRules);

            return Urls;
        }

        ///����������ҳ
        ///�ж��Ƿ�Ϊ���һ��������������Ҫע��һ�����⣬��Ϊ�п���
        ///�洢�ļ��𲢲��ǰ���˳����еģ����ԣ�Ҫ���ݴ���ļ���Level����
        ///�жϣ��������ִ��󣬵�����ҳ�Ľ��������ǰ���˳��ģ������
        ///�޷�����
        public List<string> PUrlRule(List<string> pUrl,int Level, List<cNavigRule> nRules)
        {
            List<string> tmpUrls;
            List<string> Urls =new List<string> ();
           
            string UrlRule="";
            int i;

            //����Level�õ���Ҫ��������ĵ�������
            for (i = 0; i < nRules.Count; i++)
            {
                if (Level ==nRules[i].Level )
                {
                    UrlRule =nRules [i].NavigRule ;
                    break;
                }
            }


            for (i = 0; i < pUrl.Count; i++)
            {
                tmpUrls = new List<string>();

                tmpUrls = GetUrlsByRule(pUrl[i].ToString(), UrlRule);

                Urls.AddRange(tmpUrls);
            }

            //�ж��Ƿ�Ϊ��׼��ĵ�����������򷵻أ�����������������
            if (Level == nRules.Count)
            {
                return Urls;
            }
            else
            {
                List<string> rUrls=  PUrlRule(Urls, Level + 1, nRules);
                return rUrls;
            }

        }

        //���ݵ������򣬻�ȡ��ҳ��ַ����һ������
        public List<string> GetUrlsByRule(string Url, string UrlRule)
        {
            string Url1;
            List<string> Urls=new List<string> ();

            //�ж���ַ�Ƿ���ڲ�����������ڲ�����ȡ����һ��������ַ
            if (Regex.IsMatch(Url, "{.*}"))
            {
                List<string> Urls1 = SplitWebUrl(Url );  //,IsUrlEncode ,UrlEncode
                Url1 = Urls1[0].ToString();
            }
            else
            {
                Url1 = Url;
            }

            //������ַ��Դ�룬��������ȡ������ȡ��������ַ
            string UrlSource= cTool.GetHtmlSource(Url1,true );
            //string Rule=@"(?<=href=[\W])[" + UrlRule + "](\\S*)(?=[\\s'\"])";

            if (UrlSource == "")
            {
                return null ;
            }

            //string Rule=@"(?<=href=[\W])" + cTool.RegexReplaceTrans(UrlRule) + @"(\S[^'"">]*)(?=[\s'""])";

            string Rule = @"(?<=[href=|src=|open(][\W])" + cTool.RegexReplaceTrans(UrlRule) + @"(\S[^'"">]*)(?=[\s'""])";

            Regex re = new Regex(Rule, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            MatchCollection aa = re.Matches(UrlSource);
            foreach (Match ma in aa)
            {
                Urls.Add(ma.Value.ToString());
            }

            return Urls;
        }

        //�����ַ
        public List<string> SplitWebUrl(string Url)         //, bool IsUrlEncode
        {
            List<string> Urls = new List<string>();

            try
            {
                Urls = SplitUrl(Url);
            }
            catch (System.Exception ex)
            {
                throw new cSoukeyException(ex.Message);
            }
            
            return Urls;                                  //, IsUrlEncode, ""
        }

        //public List<string> SplitWebUrl(string Url)//, bool IsUrlEncode, string UrlEncode
        //{
        //    return SplitUrl(Url);//, IsUrlEncode, UrlEncode
        //}

        private List<string> SplitUrl(string Url)   //, bool IsUrlEncode, string UrlEncode
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

            //����tmp_list_Url�ĳ�ʼֵ
            //��ʼֵΪUrl��һ������ǰ�����ַ�
            //Ӧ����{Ϊ׼
            tmp_list_Url.Add(Url.Substring (0,Url.IndexOf ("{")));

            //��ʼ������ֵ
            //Para = Url.Substring(Url.IndexOf("?") + 1, Url.IndexOf("=") - Url.IndexOf("?") );

            //�ж��Ƿ����
            while (Regex.IsMatch(Url, "{.*}"))
            {
                //��ȡ��������
                string strMatch = "(?<={)[^}]*(?=})";
                Match s = Regex.Match(Url, strMatch,RegexOptions.IgnoreCase);
                string UrlPara = s.Groups[0].Value;

                g_Url = getListUrl(UrlPara); //,IsUrlEncode ,UrlEncode 

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

                //�ж��Ƿ��в���������У����ȡ�м䲿����ƴ����ַ
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

        //�жϵ�ǰ��������������,���а����ֵ������
        public int GetUrlCount(string Url)
        {
            if (Url == "")
                return 0;

            int UrlCount = 1;
            List<string> g_Url = new List<string>();

            while (Regex.IsMatch(Url, "{.*}"))
            {
                //��ȡ��������
                string strMatch = "(?<={)[^}]*(?=})";
                Match s = Regex.Match(Url, strMatch, RegexOptions.IgnoreCase);
                string UrlPara = s.Groups[0].Value;

                //��Ϊ��������ַ�����������Բ���Ҫ��url���б���ת�����������Ҫת���Ļ���Ҳ����Ҫ
                g_Url = getListUrl(UrlPara); 

                UrlCount = UrlCount * g_Url.Count;
                Url = Url.Substring(Url.IndexOf("}") + 1, Url.Length - Url.IndexOf("}") - 1);

            }

            if (UrlCount == 0)
            {
                UrlCount = 1;
            }
            return UrlCount;
        }

        private List<string> getListUrl(string dicPre)//,bool IsUrlEncode,string UrlEncode
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
                   
                    //�ڴ˲�����url�������봦����
                    //if (IsUrlEncode == true)
                    //{
                    //    for (i = 0; i < dName.Count; i++)
                    //    {
                    //        switch ((cGlobalParas.WebCode)(int.Parse(UrlEncode)))
                    //        {
                    //            case cGlobalParas.WebCode.utf8 :
                    //                list_Para.Add(HttpUtility.UrlEncode(dName[i].Row["Name"].ToString(), Encoding.UTF8));
                    //                break;
                    //            case cGlobalParas.WebCode.gb2312 :
                    //                list_Para.Add(HttpUtility.UrlEncode(dName[i].Row["Name"].ToString(), Encoding.GetEncoding("gb2312")));
                    //                break;
                    //            case cGlobalParas.WebCode.gbk :
                    //                list_Para.Add(HttpUtility.UrlEncode(dName[i].Row["Name"].ToString(), Encoding.GetEncoding("gbk")));
                    //                break;
                    //            case cGlobalParas.WebCode .big5 :
                    //                list_Para.Add(HttpUtility.UrlEncode(dName[i].Row["Name"].ToString(), Encoding.GetEncoding("big5")));
                    //                break;
                    //            default :
                    //                list_Para.Add(HttpUtility.UrlEncode(dName[i].Row["Name"].ToString(), Encoding.UTF8));
                    //                break;
                    //        }
                            
                            
                    //    }
                    //}
                    //else
                    //{ 
                        for (i = 0; i < dName.Count; i++)
                        {
                            list_Para.Add(dName[i].Row["Name"].ToString());
                        }
                    //}

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
