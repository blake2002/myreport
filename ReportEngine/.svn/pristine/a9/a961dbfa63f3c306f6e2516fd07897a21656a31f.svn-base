using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace NetSCADA6.TimerInvokeDll
{
    class InvokeDll
    {
        #region Win API
        [DllImport("kernel32.dll")]
        private extern static IntPtr LoadLibrary(string path);

        [DllImport("kernel32.dll")]
        private extern static bool FreeLibrary(IntPtr lib);

        [DllImport("kernel32.dll")]
        private extern static IntPtr GetProcAddress(IntPtr lib, string funcName);
        
        #endregion

        private IntPtr hLib = IntPtr.Zero;
        public InvokeDll(String DLLPath)
        {
            hLib = LoadLibrary(DLLPath);
        }
        ~InvokeDll()
        {
            FreeLibrary(hLib);
        }
        public Delegate Invoke(string APIName, Type t)
        {
            IntPtr api = GetProcAddress(hLib, APIName);
            if (api == IntPtr.Zero)
            {
                return null;
            }
            else
            {
                return Marshal.GetDelegateForFunctionPointer(api, t);
            }
        }
    }

    public class InvokeTimer
    {
        #region 接口

        private delegate IntPtr InvokeInitDll();
        private delegate bool InvokeUnInitDll(IntPtr lpParser);

        // 添加定时器事件通知，定时器响应函数TimerCallBack中不能有耗时的操作，否则影响定时器正常工作
        private delegate void InvokeEmf2Svg(IntPtr meta, ref String reChar);

        private InvokeDll _InvokeDll = null;
        private IntPtr _ndllPtr = IntPtr.Zero;

        private InvokeInitDll _InitDll = null;
        private InvokeUnInitDll _UnInitDll = null;
        private InvokeEmf2Svg _Emf2Svg = null;
        

        #endregion

        #region 构造
        public InvokeTimer()
        {

        }
        ~InvokeTimer()
        {

        }
        #endregion

        #region 函数接口
        

      
        /// <summary>
        /// 初始化dll
        /// </summary>
        /// <param name="PipeDllName"></param>
        /// <returns></returns>
        public bool InitDll(String DllName)
        {
            try
            {
                if (_InvokeDll == null)
                    _InvokeDll = new InvokeDll(DllName);

                if (_InvokeDll != null)
                {
                    _Emf2Svg = (InvokeEmf2Svg)_InvokeDll.Invoke("_Emf2Svg@8", typeof(InvokeEmf2Svg));
                    
                }
                if (_InitDll != null)
                {
                    _ndllPtr = _InitDll();
                    if (_ndllPtr != null)
                    {
                        return true;
                    }
                }
            }
			catch(Exception e) 
            {
				System.Diagnostics.Debug.WriteLine (e.Message);
            }
            return false;
        }

        /// <summary>
        /// 退出时调用
        /// </summary>
        public bool UnInitDll()
        {
            try
            {
                if (_UnInitDll != null)
                {
                    if (_ndllPtr != null)
                    {
                        bool bSuccess = _UnInitDll(_ndllPtr);
                        _InvokeDll = null;
                        GC.Collect();
                        return bSuccess;
                    }
                }
            }
            catch 
            {
            }
            return false;
        }
        /// <summary>
        /// 添加定时器事件通知，定时器响应函数TimerCallBack中不能有耗时的操作，否则影响定时器正常工作
        /// </summary>
        /// <param name="lpWnd"></param>
        /// <param name="uDelay"></param>
        /// <param name="fun"></param>
        /// <returns></returns>
        public void Emf2SvgEx(IntPtr lpHandle, ref String refReturn)
        {
            try
            {
                if (_Emf2Svg != null)
                {
                    if (_ndllPtr != null)
                    {
                        _Emf2Svg(lpHandle, ref refReturn);
                    }
                }
            }
            catch
            {

            }
        }
    }
    #endregion
}

