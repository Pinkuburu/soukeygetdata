using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;

///功能：采集数据处理
///完成时间：2009-3-2
///作者：一孑
///遗留问题：无
///开发计划：无
///说明：无 
///版本：00.90.00
///修订：无
namespace SoukeyNetget.Gather
{

    class cGatherWeb
    {
        DataTable tempData ;
        private static readonly int DEF_PACKET_LENGTH = 2048;
        
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

        //get方式获取源代码
        public string GetHtml(string url, cGlobalParas.WebCode webCode, string cookie, string startPos, string endPos)   
        {
            //判断网页编码
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
            

            wReq.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.0; .NET CLR 1.1.4322; .NET CLR 2.0.50215;)";

            Match a = Regex.Match(url, @"(http://).[^/]*[?=/]", RegexOptions.IgnoreCase);
            string url1 = a.Groups[0].Value.ToString();
            wReq.Referer = url1;

            //判断是否有cookie
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

            //判断是否含有POST参数
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

            //设置页面超时时间为8秒
            wReq.Timeout = 8000;

            HttpWebResponse wResp = (HttpWebResponse)wReq.GetResponse();
            System.IO.Stream respStream = wResp.GetResponseStream();
            
            System.IO.StreamReader reader;
            reader = new System.IO.StreamReader(respStream, wCode);
            string strWebData = reader.ReadToEnd();
            reader.Close();
            reader.Dispose();

            //去除回车换行符号
            strWebData = Regex.Replace(strWebData, "([\\r\\n])[\\s]+", "", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            strWebData = Regex.Replace(strWebData, "\\n", "", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            strWebData.Replace("\\r\\n", "");


            //获取此页面的编码格式,并对源码进行一次判断,无论用户是否指定了网页代码
            Match charSetMatch = Regex.Match(strWebData, "<meta([^<]*)charset=([^<]*)\"", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            string webCharSet = charSetMatch.Groups[2].Value;
            string charSet = webCharSet;

            if (charSet != null && charSet != "" && Encoding.GetEncoding(charSet) != wCode)
            {
                byte[] myDataBuffer;
                
                myDataBuffer = System.Text.Encoding.GetEncoding(charSet).GetBytes(strWebData);
                strWebData = Encoding.GetEncoding(charSet).GetString(myDataBuffer);

            }

            //按照截取网页的起始标志和终止标志进行截取
            //如果起始或终止截取标识有一个为空，则不进行截取
            if (startPos != "" && endPos != "")
            {
                string Splitstr = "(" + startPos + ").*?(" + endPos + ")";

                Match aa = Regex.Match(strWebData, Splitstr);
                strWebData = aa.Groups[0].ToString();
            }

            this.m_WebpageSource = strWebData;

            return strWebData;

         }

        /// <summary>
        /// 采集网页数据
        /// </summary>
        /// <param name="Url">网页地址</param>
        /// <param name="StartPos">起始采集位置</param>
        /// <param name="EndPos">终止采集位置</param>
        /// <returns></returns>
        public DataTable  GetGatherData(string Url,cGlobalParas.WebCode webCode, string cookie, string startPos,string endPos,string sPath)
        {
            tempData = new DataTable("tempData");
            int i ;
            int j;
            string strCut="";
            bool IsDownloadFile = false;

            //根据页面截取的标志构建表结构
            for (i = 0; i < this.CutFlag.Count; i++)
            {
                tempData.Columns.Add(new DataColumn(this.CutFlag[i].Title, typeof(string)));
                
                if (this.CutFlag[i].DataType !=(int) cGlobalParas.GDataType.Txt && IsDownloadFile ==false)
                {
                    IsDownloadFile = true;
                }
            }
            
            //根据用户指定的页面截取位置构造正则表达式
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

            //去掉最后一个“|”
            strCut = strCut.Substring(0, strCut.Length - 1);

            //获取网页信息
            //判断传入的Url是否正确，如果不正确，则返回空数据
            if (Regex.IsMatch(Url, "[\"\\s]"))
            {
                tempData = null;
                return tempData;
            }

            try
            {
                GetHtml(Url, webCode, cookie, startPos, endPos);
            }
            catch (System.Web.HttpException ex)
            {
                throw ex;
            }

            //开始获取截取内容
            Regex re = new Regex(@strCut, RegexOptions.IgnoreCase | RegexOptions.Multiline );
            MatchCollection mc = re.Matches(this.WebpageSource);

            DataRow drNew ;

            i = 0;

            //根据采集数及列数计算共有多少行
            int rows=(int)Math.Ceiling((decimal) mc.Count /(decimal)rowCount) ;

            Match ma;
            int m = 0;   //计数使用
            for (i = 0; i < rows; i++)
            {
                drNew = tempData.NewRow();

                for (j=0;j<rowCount ;j++)
                {
                    //从集合中读取一个数据集出来

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

            //判断是否存在有下载文件的任务，如果有，则开始下载，因为此功能设计最初是下载图片使用
            //并非是专用的下载工具，所以对下载处理并没有单独进行线程处理
            if (IsDownloadFile == true)
            {
                if (sPath == "")
                {
                    sPath = Program.getPrjPath() + "data\\tem_file";
                }

                if (!Directory.Exists(sPath))
                {
                    Directory.CreateDirectory(sPath);
                }

                string FileUrl="";
                string DownloadFileName="";

                for (i = 0; i < rows; i++)
                {
                    for (j = 0; j < this.CutFlag.Count; j++)
                    {
                        if (this.CutFlag[j].DataType != (int)cGlobalParas.GDataType.Txt)
                        {
                            FileUrl = tempData.Rows[i][j].ToString();

                            //开始获取下载文件名称
                            Regex s = new Regex(@"(?<=/)[^/]*", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                            MatchCollection urlstr = s.Matches(FileUrl);
                            DownloadFileName = urlstr[urlstr.Count - 1].ToString();
                            DownloadFileName = sPath + "\\" + DownloadFileName;

                            if (FileUrl.Substring(0, 4) == "http")
                            {
                                DownloadFile(FileUrl, DownloadFileName);
                            }
                            else
                            {
                                if (FileUrl.Substring(0, 1) == "/")
                                {
                                    Url = Url.Substring(7, Url.Length - 7);
                                    Url = FileUrl.Substring(0, Url.IndexOf("/"));
                                    Url = "http://" + Url;
                                    FileUrl = Url + FileUrl;
                                }
                                else
                                {
                                    FileUrl = Url + FileUrl;
                                }

                                DownloadFile(FileUrl, DownloadFileName);
                            }
                        }
                    }
                }

            }
            return tempData;
        }


        //下载文件，是一个单线程的方法，适用于小文件下载，仅支持http方式
        private cGlobalParas.DownloadResult DownloadFile(string url, string path)
        {

            HttpWebRequest wReq = null;
            HttpWebResponse wRep = null;
            FileStream SaveFileStream = null;

            int startingPoint = 0;

            try
            {
                //For using untrusted SSL Certificates

                wReq = (HttpWebRequest)HttpWebRequest.Create(url);
                wReq.AddRange(startingPoint);

                wRep = (HttpWebResponse)wReq.GetResponse();
                Stream responseSteam = wRep.GetResponseStream();

                if (startingPoint == 0)
                {
                    SaveFileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                }
                else
                {
                    SaveFileStream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                }

                int bytesSize;
                long fileSize = wRep.ContentLength;
                byte[] downloadBuffer = new byte[DEF_PACKET_LENGTH];

                while ((bytesSize = responseSteam.Read(downloadBuffer, 0, downloadBuffer.Length)) > 0)
                {
                    SaveFileStream.Write(downloadBuffer, 0, bytesSize);
                }

                SaveFileStream.Close();
                SaveFileStream.Dispose();
                return cGlobalParas.DownloadResult.Succeed;

                wRep.Close();

            }
            catch (Exception ex)
            {
                return cGlobalParas.DownloadResult.Err;
            }

        }
    }
}
