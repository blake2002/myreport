using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace PMS.Libraries.ToolControls.PMSPublicInfo
{
    #region CNSDogVarExpert类
    public unsafe class CNSDogVarExpert
    {
        int _iHandle = 0;

        [DllImport("nsRunController_CSharp.dll", EntryPoint = "_DogVarExpert_Create@0", CallingConvention = CallingConvention.StdCall)]
        private static extern int DogVarExpert_Create();

        [DllImport("nsRunController_CSharp.dll", EntryPoint = "_DogVarExpert_Destroy@4", CallingConvention = CallingConvention.StdCall)]
        private static extern void DogVarExpert_Destroy(int iHandle);

        [DllImport("nsRunController_CSharp.dll", EntryPoint = "_DogVarExpert_Init@4", CallingConvention = CallingConvention.StdCall)]
        private static extern bool DogVarExpert_Init(int iHandle);

        [DllImport("nsRunController_CSharp.dll", EntryPoint = "_DogVarExpert_Release@4", CallingConvention = CallingConvention.StdCall)]
        private static extern void DogVarExpert_Release(int iHandle);

        [DllImport("nsRunController_CSharp.dll", EntryPoint = "_DogVarExpert_IsDogThere@0", CallingConvention = CallingConvention.StdCall)]
        private static extern bool DogVarExpert_IsDogThere();

        [DllImport("nsRunController_CSharp.dll", EntryPoint = "_DogVarExpert_ReadMESFlag@4", CallingConvention = CallingConvention.StdCall)]
        private static extern int DogVarExpert_ReadMESFlag(int iHandle);

        [DllImport("nsRunController_CSharp.dll", EntryPoint = "_DogVarExpert_IsMESReportEnabled@4", CallingConvention = CallingConvention.StdCall)]
        private static extern bool DogVarExpert_IsMESReportEnabled(int iHandle);

        [DllImport("nsRunController_CSharp.dll", EntryPoint = "_NetSCADA_IsFieldRunning@0", CallingConvention = CallingConvention.StdCall)]
        private static extern bool NetSCADA_IsFieldRunning();

        public bool Init()
        {
            bool bResult = false;
            if (0 == _iHandle)
            {
                _iHandle = DogVarExpert_Create();
                bResult = DogVarExpert_Init(_iHandle);
            }
            return bResult;
        }

        public void Release()
        {
            if (0 != _iHandle)
            {
                DogVarExpert_Release(_iHandle);
                DogVarExpert_Destroy(_iHandle);
            }
        }

        public int ReadMESFlag()
        {
            Init();

            int iType = DogVarExpert_ReadMESFlag(_iHandle);
            return iType;
        }

        public bool IsMESReportEnabled()
        {
            Init();

            return DogVarExpert_IsMESReportEnabled(_iHandle);
        }

        public bool IsFieldRunning()
        {
            Init();

            return NetSCADA_IsFieldRunning();
        }
    }
    #endregion
}
