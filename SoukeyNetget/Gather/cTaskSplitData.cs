using System;
using System.Collections.Generic;
using System.Text;

///���ܣ��ɼ����� �ֽ������� ����
///���ʱ�䣺2009-3-2
///���ߣ�һ��
///�������⣺��
///�����ƻ�����
///˵������ 
///�汾��00.90.00
///�޶�����
namespace SoukeyNetget.Gather
{
    //����ֽ�����,�ֽ�������һ��������������ִ�е��߳������зֽ�

    public class cTaskSplitData
    {

        #region ���� ����
        public cTaskSplitData()
        {
            m_CurIndex = 0;
            m_GatheredUrlCount = 0;
        }

        ~cTaskSplitData()
        {
        }
        #endregion

        #region �ɼ��ֽ���������

        //��ʼ�ɼ���λ������
        private int m_BeginIndex;
        public int BeginIndex
        {
            get { return m_BeginIndex; }
            set { m_BeginIndex = value; }
        }

        //�����ɼ���λ������
        private int m_EndIndex;
        public int EndIndex
        {
            get { return m_EndIndex; }
            set { m_EndIndex = value; }
        }

        private int m_CurIndex;
        public int CurIndex
        {
            get { return m_CurIndex; }
            set { m_CurIndex = value; }
        }

        //��ǰ���ڲɼ���Url��ַ
        private string m_CurUrl;
        public string CurUrl
        {
            get { return m_CurUrl; }
            set { m_CurUrl = value; }
        }
        
        //һ���ɼ�Url��ַ����
        public int UrlCount
        {
            get { return (EndIndex - BeginIndex+1); }
        }

        //�Ѿ��ɼ�Url����
        private int m_GatheredUrlCount;
        public int GatheredUrlCount
        {
            get { return m_GatheredUrlCount; }
            set { m_GatheredUrlCount = value; }
        }

        //�˷ֽ�������Ҫ�ɼ�����ҳ��ַ
        private List<Task.cWebLink> m_Weblink;
        public List<Task.cWebLink> Weblink
        {
            get { return m_Weblink; }
            set { m_Weblink = value; }
        }

        //�˷ֽ�����ɼ���ҳ��ַ���ݵĽ�ȡ��ʶ
        private List<Task.cWebpageCutFlag> m_CutFlag;
        public List<Task.cWebpageCutFlag> CutFlag
        {
            get { return m_CutFlag; }
            set { m_CutFlag = value; }
        }

        #endregion

    }
}
