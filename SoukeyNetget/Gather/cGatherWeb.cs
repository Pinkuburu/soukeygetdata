using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Data;
using System.Text.RegularExpressions;

///���ܣ��ɼ����ݴ���
///���ʱ�䣺2009-3-2
///���ߣ�һ��
///�������⣺��
///�����ƻ�����
///˵������ 
///�汾��00.90.00
///�޶�����
namespace SoukeyNetget.Gather
{

    class cGatherWeb
    {
        DataTable tempData ;
        
        public cGatherWeb()
        {
            this.CutFlag = new List<SoukeyNetget.Task.cWebpageCutFlag>();
        }

        ~cGatherWeb()
        {
            this.CutFlag = null;
        }

        private List<Task.cWebpageCutFlag> m_CutFlag; 
        public List<Task.cWebpageCutFlag> CutFlag
        {
            get { return m_CutFlag; }
            set { m_CutFlag = value; }
        }

        private string m_WebpageSource;
        protected string WebpageSource
        {
            get { return this.m_WebpageSource; }
            set { this.m_WebpageSource = value; }
        }

        //get��ʽ��ȡԴ����
        private void GetHtml(string url, cGlobalParas.WebCode webCode, string cookie, string startPos, string endPos)   
        {
            //�ж���ҳ����
            Encoding wCode;
            string PostPara = "";

            switch (webCode)
            {
                case cGlobalParas.WebCode.auto:
                    wCode = Encoding.Default;
                    break;
                case cGlobalParas.WebCode.gb2312:
                    wCode = Encoding.GetEncoding("gb2312");
                    break;
                case cGlobalParas.WebCode.gbk:
                    wCode = Encoding.GetEncoding("gbk");
                    break;
                case cGlobalParas.WebCode.utf8:
                    wCode = Encoding.UTF8;
                    break;
                default:
                    wCode = Encoding.UTF8;
                    break;
            }

            CookieContainer CookieCon = new CookieContainer();

            HttpWebRequest wReq ;

            if (Regex.IsMatch(url, @"<POST>.*</POST>", RegexOptions.IgnoreCase))
            {
                wReq = (HttpWebRequest)WebRequest.Create(@url.Substring (0,url.IndexOf ("<POST>")));
            }
            else
            {
                wReq = (HttpWebRequest)WebRequest.Create(@url );
            }
            

            wReq.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0; .NET CLR 1.1.4322; .NET CLR 2.0.50215; CrazyCoder.cn;www.aub.org.cn)";

            Match a = Regex.Match(url, @"(http://).[^/]*[?=/]", RegexOptions.IgnoreCase);
            string url1 = a.Groups[0].Value.ToString();
            wReq.Referer = url1;

            //�ж��Ƿ���cookie
            if (cookie != "")
            {
                CookieCollection cl = new CookieCollection();

                foreach (string sc in cookie.Split(';'))
                {
                    string ss = sc.Trim();
                    cl.Add(new Cookie(ss.Split('=')[0].Trim(), ss.Split('=')[1].Trim(), "/"));
                }
                CookieCon.Add(new Uri(url), cl);
                //CookieCon.Add(new Uri(url1 ), cl);
                wReq.CookieContainer = CookieCon;
            }

            //�ж��Ƿ���POST����
            if (Regex.IsMatch(url, @"(?<=<POST>)[\S\s]*(?=</POST>)", RegexOptions.IgnoreCase))
            {

                Match s = Regex.Match(url, @"(?<=<POST>).*(?=</POST>)", RegexOptions.IgnoreCase);
                PostPara = s.Groups[0].Value.ToString();
                byte[] pPara = Encoding.ASCII.GetBytes(PostPara);

                wReq.ContentType = "application/x-www-form-urlencoded";
                wReq.ContentLength = pPara.Length;

                wReq.Method = "POST";

                System.IO.Stream reqStream = wReq.GetRequestStream();
                reqStream.Write(pPara, 0, pPara.Length);
                reqStream.Close();
                
            }
            else
            {
                wReq.Method = "GET";
                
            }

            HttpWebResponse wResp= (HttpWebResponse)wReq.GetResponse();
            System.IO.Stream respStream = wResp.GetResponseStream();
            System.IO.StreamReader reader;
            reader = new System.IO.StreamReader(respStream, wCode);
            string strWebData = reader.ReadToEnd();
            reader.Close();
            reader.Dispose();

            //ȥ���س����з���
            strWebData = Regex.Replace(strWebData, "([\\r\\n])[\\s]+", "", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            strWebData = Regex.Replace(strWebData, "\\n", "", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            strWebData.Replace("\\r\\n", "");


            //��ȡ��ҳ��ı����ʽ,����Դ�����һ���ж�,�����û��Ƿ�ָ������ҳ����
            Match charSetMatch = Regex.Match(strWebData, "<meta([^<]*)charset=([^<]*)\"", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            string webCharSet = charSetMatch.Groups[2].Value;
            string charSet = webCharSet;

            if (charSet != null && charSet != "" && Encoding.GetEncoding(charSet) != wCode)
            {
                byte[] myDataBuffer;
                
                myDataBuffer = System.Text.Encoding.GetEncoding(charSet).GetBytes(strWebData);
                strWebData = Encoding.GetEncoding(charSet).GetString(myDataBuffer);

            }

            //���ս�ȡ��ҳ����ʼ��־����ֹ��־���н�ȡ
            //�����ʼ����ֹ��ȡ��ʶ��һ��Ϊ�գ��򲻽��н�ȡ
            if (startPos != "" && endPos != "")
            {
                string Splitstr = "(" + startPos + ").*?(" + endPos + ")";

                Match aa = Regex.Match(strWebData, Splitstr);
                strWebData = aa.Groups[0].ToString();
            }

            this.m_WebpageSource = strWebData;

         }

        /// <summary>
        /// �ɼ���ҳ����
        /// </summary>
        /// <param name="Url">��ҳ��ַ</param>
        /// <param name="StartPos">��ʼ�ɼ�λ��</param>
        /// <param name="EndPos">��ֹ�ɼ�λ��</param>
        /// <returns></returns>
        public DataTable  GetGatherData(string Url,cGlobalParas.WebCode webCode, string cookie, string startPos,string endPos)
        {
            tempData = new DataTable("tempData");
            int i ;
            int j;
            string strCut="";

            //����ҳ���ȡ�ı�־������ṹ
            for (i = 0; i < this.CutFlag.Count; i++)
            {
                tempData.Columns.Add(new DataColumn(this.CutFlag[i].Title, typeof(string)));
            }
            
            //�����û�ָ����ҳ���ȡλ�ù���������ʽ
            for (i = 0; i < this.CutFlag.Count; i++)
            {
                strCut += "(?<=" + cTool.RegexReplaceTrans(this.CutFlag[i].StartPos) + ")";

                switch (this.CutFlag[i].LimitSign )
                {
                    case 2001:
                        strCut += ".*?";
                        break;
                    case 2002:
                        strCut += "[^<>].*?";
                        break;
                    case 2003:
                        strCut += "[\\S\\s]*?";
                        break;
                    default :
                        strCut += "[\\S\\s]*?";
                        break;
                }
                strCut += "(?=" +  cTool.RegexReplaceTrans(this.CutFlag[i].EndPos) + ")|";
            }

            int rowCount = this.CutFlag.Count;

            //ȥ�����һ����|��
            strCut = strCut.Substring(0, strCut.Length - 1);

            //��ȡ��ҳ��Ϣ
            //�жϴ����Url�Ƿ���ȷ���������ȷ���򷵻ؿ�����
            if (Regex.IsMatch(Url, "[\"\\s]"))
            {
                tempData = null;
                return tempData;
            }
            GetHtml(Url,webCode ,cookie, startPos ,endPos );


            //��ʼ��ȡ��ȡ����
            Regex re = new Regex(@strCut, RegexOptions.IgnoreCase | RegexOptions.Multiline );
            MatchCollection mc = re.Matches(this.WebpageSource);

            DataRow drNew ;

            i = 0;

            //���ݲɼ������������㹲�ж�����
            int rows=(int)Math.Ceiling((decimal) mc.Count /(decimal)rowCount) ;

            Match ma;
            int m = 0;   //����ʹ��
            for (i = 0; i < rows; i++)
            {

                drNew = tempData.NewRow();

                for (j=0;j<rowCount ;j++)
                {
                    //�Ӽ����ж�ȡһ�����ݼ�����

                    if (m>=mc.Count)
                    {
                        break;
                    }
                    ma = mc[m];

                    drNew[j] = ma.Groups[0].Value.ToString();
                    m++;

                }

                tempData.Rows.Add(drNew);
                drNew = null;                
            }

            return tempData;
        }
    }
}
