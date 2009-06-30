using System;
using System.Collections.Generic;
using System.Text;

///���ܣ������������ �������� ��Ӧ�¼� �˹���ʵ�ֵĺܼ�
///���ʱ�䣺2009-3-2
///���ߣ�һ��
///�������⣺��
///�����ƻ�����һ����Ҫ���Ʒ�������ģ�飬�Ʊش˹���Ҫ��������
///˵������ 
///�汾��01.00.00
///�޶�����
namespace SoukeyNetget.publish
{
    class cPublishManage
    {
        List<cPublishTask> m_ListPublish;
        private Gather.cEventProxy m_EventProxy;

        public cPublishManage()
        {
            m_ListPublish = new List<cPublishTask>();
            m_EventProxy = new Gather.cEventProxy();
        }

        ~cPublishManage()
        {
        }

        public List<cPublishTask> ListPublish
        {
            get { return m_ListPublish; }
        }

        public void AddPublishTask(cPublishTask pt)
        {
            //��ӵ�������
            ListPublish.Add(pt);
            TaskInit(pt);

            //����������
            pt.startPublic();
        }

        public void AddSaveTempDataTask(cPublishTask pt)
        {
            ListPublish.Add(pt);
            TaskTempSaveInit(pt);

            //����������
            pt.startSaveTempData();
        }

        //ע����ʱ�洢������¼���ϵͳ�Զ�ִ�У������û���Ԥ
        private void TaskTempSaveInit(cPublishTask pTask)
        {
            if (pTask.PublishManage.Equals(this))
            {
                pTask.PublishTempDataCompleted += this.onPublishTempDataCompleted;
            }
        }

        //ע�ᷢ��������¼�
        private void TaskInit(cPublishTask pTask)
        {

            if (pTask.PublishManage.Equals(this))
            {
                pTask.PublishCompleted  += this.onPublishCompleted;
                pTask.PublishFailed  += this.onPublishFailed;
                pTask.PublishStarted  += this.onPublishStarted;
                pTask.PublishError  += this.onPublishError;
                pTask.PublishTempDataCompleted += this.onPublishTempDataCompleted;
            }
        }

        private void onPublishCompleted(object sender, PublishCompletedEventArgs e)
        {

            //�ӵ�ǰ�б���ɾ���˼�¼
            cPublishTask pt = (cPublishTask)sender;
            m_ListPublish.Remove(pt);
            pt = null;

            if (e_PublishCompleted != null && !e.Cancel)
            {
                e_PublishCompleted(sender, e);
            }

        }

        private void onPublishFailed(object sender, PublishFailedEventArgs e)
        {
            //�ӵ�ǰ�б���ɾ���˼�¼
            cPublishTask pt = (cPublishTask)sender;
            m_ListPublish.Remove(pt);
            pt = null;

            if (e_PublishFailed != null && !e.Cancel)
            {
                e_PublishFailed(sender, e);
            }

        }

        private void onPublishStarted(object sender, PublishStartedEventArgs e)
        {
            if (e_PublishStarted != null && !e.Cancel)
            {
                e_PublishStarted(sender, e);
            }
        }

        private void onPublishError(object sender, PublishErrorEventArgs e)
        { 
            //�ӵ�ǰ�б���ɾ���˼�¼
            cPublishTask pt = (cPublishTask)sender;
            m_ListPublish.Remove(pt);
            pt = null;

            if (e_PublishError != null && !e.Cancel)
            {
                e_PublishError(sender, e);
            }

        }

        private void onPublishTempDataCompleted(object sender, PublishTempDataCompletedEventArgs e)
        {

            //�ӵ�ǰ�б���ɾ���˼�¼����ʱ���ݵı���Ҳ����Ϊһ������������ִ�е�
            //���ԣ�������Ϻ���Ҫɾ��������
            cPublishTask pt = (cPublishTask)sender;
            m_ListPublish.Remove(pt);
            pt = null;

            if (e_PublishTempDataCompleted != null && !e.Cancel)
            {
                e_PublishTempDataCompleted(sender, e);
            }
        }

        #region �¼�

        /// �������� ����¼�
        private event EventHandler<PublishCompletedEventArgs> e_PublishCompleted;
        public event EventHandler<PublishCompletedEventArgs> PublishCompleted
        {
            add { e_PublishCompleted += value; }
            remove { e_PublishCompleted -= value; }
        }

        /// �������� ʧ���¼�
        private event EventHandler<PublishFailedEventArgs> e_PublishFailed;
        public event EventHandler<PublishFailedEventArgs> PublishFailed
        {
            add { e_PublishFailed += value; }
            remove { e_PublishFailed -= value; }
        }

        /// �������� ��ʼ�ɼ��¼�
        private event EventHandler<PublishStartedEventArgs> e_PublishStarted;
        public event EventHandler<PublishStartedEventArgs> PublishStarted
        {
            add { e_PublishStarted += value; }
            remove { e_PublishStarted -= value; }
        }

        ///�������� �����¼�
        private event EventHandler<PublishErrorEventArgs> e_PublishError;
        public event EventHandler<PublishErrorEventArgs> PublishError
        {
            add { e_PublishError += value; }
            remove { e_PublishError -= value; }
        }

        ///��ʱ������������¼�
        private event EventHandler<PublishTempDataCompletedEventArgs> e_PublishTempDataCompleted;
        public event EventHandler<PublishTempDataCompletedEventArgs> PublishTempDataCompleted
        {
            add { e_PublishTempDataCompleted += value; }
            remove { e_PublishTempDataCompleted -= value; }
        }
        #endregion
    }
}
