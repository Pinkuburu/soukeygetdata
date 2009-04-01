using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SoukeyNetget
{
    class WMessage
    {
        #region 引入windows dll

        [DllImport("User32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "FindWindowEx", CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="hWnd">接收消息的窗体句柄</param>
        /// <param name="Msg">消息ID（从"RegisterWindowMessage"获取）</param>
        /// <param name="wParam">附加信息</param>
        /// <param name="lParam">附加信息</param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        /// <summary>
        /// 注册/获取自定义消息
        /// </summary>
        /// <param name="lpString"></param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "RegisterWindowMessage", CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        private static extern uint RegisterWindowMessage(string lpString);
        #endregion

        #region 消息类型
        /// <summary>
        /// 下载窗口的消息类型
        /// </summary>
        public enum DF_wParam
        {
            /// <summary>
            /// 窗体关闭
            /// </summary>
            UM_CLOSE = 0x1001,
            /// <summary>
            /// 取消并关闭窗体
            /// </summary>
            UM_CLOSE_CANCEL = 0x1002,
            /// <summary>
            /// 完成并关闭窗体
            /// </summary>
            UM_CLOSE_OK = 0x1003,
            /// <summary>
            /// 弹出升级窗口,开始下载
            /// </summary>
            UM_STARDOWNLOAD = 0x1004,
            /// <summary>
            /// 弹出升级窗口，继续下载
            /// </summary>
            UM_CONTINUEDOWNLOAD = 0x1005
        }

        private static int msg_df = (int)RegisterWindowMessage("MSG_DOWNLOADFORM");
        /// <summary>
        /// 获取自定义 窗体消息
        /// </summary>
        public static int MSG_MAINFORM
        {
            get { return WMessage.msg_df; }
        }
        #endregion


        /// <summary>
        /// 初始化消息控制器
        /// </summary>
        public WMessage()
        {
        }

        /// <summary>
        /// 向指定窗体发送消息
        /// </summary>
        /// <param name="wHander">接收消息的窗体句柄</param>
        /// <param name="wParam">窗口的消息类型</param>
        /// <param name="lParam">附加信息</param>
        /// <returns></returns>
        public static int SendMessage(IntPtr wHander, DF_wParam wParam, IntPtr lParam)
        {
            return WMessage.SendMessage(wHander, WMessage.MSG_MAINFORM, (IntPtr)wParam, lParam);
        }


        #region 事件委托
        public delegate void MSGEventHander(ref Message m);
        private Dictionary<IntPtr, MSGEventHander> m_MSGEvent;
        /// <summary>
        /// 获取 事件表
        /// </summary>
        protected Dictionary<IntPtr, MSGEventHander> MSGEvent
        {
            get
            {
                if (m_MSGEvent == null)
                {
                    m_MSGEvent = new Dictionary<IntPtr, MSGEventHander>();
                }
                return m_MSGEvent;
            }
        }
        #endregion

        #region 自定义消息处理、绑定事件接口

        /// <summary>
        /// 接收处理主窗体消息
        /// </summary>
        /// <param name="m"></param>
        public void mProc(ref Message m)
        {
            if (m.Msg == WMessage.MSG_MAINFORM)
            {
                if (MSGEvent.ContainsKey(m.WParam))
                {
                    MSGEventHander evt = MSGEvent[m.WParam];
                    evt(ref m);
                }
            }
        }

        /// <summary>
        /// 给指定的消息绑定事件
        /// </summary>
        /// <param name="wParam"></param>
        /// <param name="evt"></param>
        public void BindEvent(DF_wParam wParam, MSGEventHander evt)
        {
            if (MSGEvent.ContainsKey((IntPtr)wParam))
            {
                MSGEventHander evt1 = MSGEvent[(IntPtr)wParam];
                evt1 += evt;
                MSGEvent[(IntPtr)wParam] = evt1;
            }
            else
            {
                m_MSGEvent[(IntPtr)wParam] = evt;
            }
        }
        /// <summary>
        /// 移除消息绑定事件
        /// </summary>
        /// <param name="wParam"></param>
        /// <param name="evt"></param>
        public void RemoveEvent(DF_wParam wParam, MSGEventHander evt)
        {
            if (MSGEvent.ContainsKey((IntPtr)wParam))
            {
                MSGEventHander evt1 = MSGEvent[(IntPtr)wParam];
                evt1 -= evt;
                MSGEvent[(IntPtr)wParam] = evt1;
            }
        }

        #endregion
    }
}
