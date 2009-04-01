using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

///功能：xml文件处理
///完成时间：2009－3－2
///作者：一孑
///遗留问题：无
///开发计划：无
///说明：参见注释
///版本：00.90.00
///修订：无
namespace SoukeyNetget
{
    class cXmlIO
    {
        protected XmlDocument objXmlDoc;
        protected string strXmlFile;

        public cXmlIO()
        {
            objXmlDoc = new XmlDocument();
        }

        public cXmlIO(string XmlFile)
        {
            objXmlDoc = new XmlDocument();

            try
            {
                objXmlDoc.Load(XmlFile);
            }
            catch (System.Exception ex)
            {
                objXmlDoc = null;
                throw ex;
            }
            strXmlFile = XmlFile;
         }

        ~cXmlIO()
        {
            objXmlDoc = null;
        }

        public void NewXmlFile(string FileName,string strXml )
        {
            //获取路径

            string s = "\\b.*(?=\\\\)\\b";
            Match m = Regex.Match(FileName, s);
            string fPath = m.Groups[0].Value.ToString();
            if (!System.IO.Directory.Exists(fPath))
            {
                //目录不存在，首先需要创建此目录
                System.IO.Directory.CreateDirectory(fPath);
            }

            objXmlDoc = new XmlDocument();
            objXmlDoc.LoadXml(strXml);
            objXmlDoc.Save(FileName);

            strXmlFile = FileName;

        }

        //根据指定的路径读取一个值
        public string GetNodeValue(string nodPath)
        {
            return  objXmlDoc.SelectSingleNode(nodPath).InnerText.ToString ();
        }

        //根据节点返回数据,类型为dataview
        public DataView GetData(string XmlPathNode)
        {
            DataSet ds = new DataSet();
            StringReader read = new StringReader(objXmlDoc.SelectSingleNode(XmlPathNode).OuterXml);
            ds.ReadXml(read);
            if (ds.Tables.Count == 0)
            {
                return null;
            }
            else
            {
                return ds.Tables[0].DefaultView;
            }
        }

        //根据指定节点的编号,来返回节点内容,尽管返回的是一条记录,
        //但还是作为DataView进行返回,这样做的目的是为了更好的访问性
        public DataView GetData(string NodeCollection, string Node, string content)
        {
            XmlNodeList fathernode = objXmlDoc.GetElementsByTagName(NodeCollection);
            XmlNodeList nodes = fathernode[0].ChildNodes;

            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = 0; j < nodes[i].ChildNodes.Count; j++)
                {
                    //for (int m=0;
                    if (nodes[i].ChildNodes[j].Name == Node && nodes[i].ChildNodes[j].InnerText == content)
                    {
                        StringReader read = new StringReader(nodes[i].OuterXml);
                        DataSet ds = new DataSet();
                        ds.ReadXml(read);
                        if (ds.Tables.Count == 0)
                        {
                            return null;
                        }
                        else
                        {
                            return ds.Tables[0].DefaultView;
                        }

                    }
                }

            }
            return null;
           
        }

        //删除节点
        //根据指定的节点删除此节点以及此节点一下的内容
        public void DeleteNode(string Node)
        {
            XmlNodeList nodes = objXmlDoc.GetElementsByTagName(Node);
            XmlNode delNode = nodes[0];
            delNode.ParentNode.RemoveChild(delNode);
        }

        //根据指定的节点，删除子节点符合content内容的子节点
        //此方法比较特殊，是针对soukey采摘中特有的xml来进行操作，并非支持所有的xml文件
        //在soukey采摘中的xml文件中，通常都会有1对多的关系，这种关系通过一个可
        //重复的节点来表示，而在删除的时候，并非指定这个节点，而是指定这个节点的父节点
        //因为要循环集合中的内容，根据集合中的一个节点，中下的内容来进行删除
        //举例<tasks><task><id>1</id><name>soukey</name></task><task><id>2</id><name>采摘</name></task></tasks>
        //删除子节点是指删除task节点，但根据的条件是指定的id或者name符合content的内容，
        //所以调用方法是DeleteChildNodes("tasks","name","soukey")
        //调用后，将删除task中name＝soukey的task节点,传入的MainNode必须是一个集合，如果传入的是一个结合子节点，
        //将导致错误
        public void DeleteChildNodes(string NodeCollection,string Node, string content)
        {
            XmlNodeList fathernode = objXmlDoc.GetElementsByTagName(NodeCollection);
            XmlNodeList nodes = fathernode[0].ChildNodes;

            for (int i=0;i< nodes.Count; i++)
            {
                for (int j = 0; j < nodes[i].ChildNodes.Count; j++)
                {
                    //for (int m=0;
                    if (nodes[i].ChildNodes[j].Name ==Node &&  nodes[i].ChildNodes[j].InnerText == content)
                    {
                        fathernode[0].RemoveChild(nodes[i]);
                        return;
                    }
                }

            }

        }

        //插入一个节点和此节点的一子节点
        public void InsertNode(string MainNode,string ChildNode,string Element,string Content)
        {
           XmlNode objRootNode = objXmlDoc.SelectSingleNode(MainNode);
           XmlElement objChildNode = objXmlDoc.CreateElement(ChildNode);
           objRootNode.AppendChild(objChildNode);
           XmlElement objElement = objXmlDoc.CreateElement(Element);
           objElement.InnerText = Content;
           objChildNode.AppendChild(objElement);

        }

        //修改一个节点信息
        public void EditNode(string Element, string Old_Content,string Content)
        {

            XmlNodeList nodes = objXmlDoc.GetElementsByTagName(Element);

            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                if (nodes[i].ChildNodes[0].InnerText == Old_Content) 
                {
                    nodes[i].ChildNodes[0].InnerText = Content;
                }

            }
        }

        //插入一个节点，带一个属性
        public void InsertElement(string MainNode,string Element,string Attrib,string AttribContent,string Content)
        {
           XmlNode objNode = objXmlDoc.SelectSingleNode(MainNode);
           XmlElement objElement = objXmlDoc.CreateElement(Element);
           objElement.SetAttribute(Attrib,AttribContent);
           objElement.InnerText = Content;
           objNode.AppendChild(objElement);
        }

        //插入一个节点
        public void InsertElement(string MainNode,string Element,string Content)
        {
           XmlNode objNode = objXmlDoc.SelectSingleNode(MainNode);
           XmlElement objElement = objXmlDoc.CreateElement(Element);
           objElement.InnerXml  = Content;
           objNode.AppendChild(objElement);
        }

        //保存xml文件
        public void Save()
        {
           try
           {
            objXmlDoc.Save(strXmlFile);
           }
           catch (System.Exception ex)
           {
            throw ex;
           }
           
        }
           
   }

      
}
