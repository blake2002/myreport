using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.ServiceProcess;

namespace PMS.Libraries.ToolControls.PMSPublicInfo.ScmWrapper
{
    #region Win32 API Declarations

    [Flags]
    enum ServiceControlAccessRights : int
    {
        SC_MANAGER_CONNECT = 0x0001, // Required to connect to the service control manager. 
        SC_MANAGER_CREATE_SERVICE = 0x0002, // Required to call the CreateService function to create a service object and add it to the database. 
        SC_MANAGER_ENUMERATE_SERVICE = 0x0004, // Required to call the EnumServicesStatusEx function to list the services that are in the database. 
        SC_MANAGER_LOCK = 0x0008, // Required to call the LockServiceDatabase function to acquire a lock on the database. 
        SC_MANAGER_QUERY_LOCK_STATUS = 0x0010, // Required to call the QueryServiceLockStatus function to retrieve the lock status information for the database
        SC_MANAGER_MODIFY_BOOT_CONFIG = 0x0020, // Required to call the NotifyBootConfigStatus function. 
        SC_MANAGER_ALL_ACCESS = 0xF003F // Includes STANDARD_RIGHTS_REQUIRED, in addition to all access rights in this table. 
    }

    [Flags]
    enum ServiceAccessRights : int
    {
        SERVICE_QUERY_CONFIG = 0x0001, // Required to call the QueryServiceConfig and QueryServiceConfig2 functions to query the service configuration. 
        SERVICE_CHANGE_CONFIG = 0x0002, // Required to call the ChangeServiceConfig or ChangeServiceConfig2 function to change the service configuration. Because this grants the caller the right to change the executable file that the system runs, it should be granted only to administrators. 
        SERVICE_QUERY_STATUS = 0x0004, // Required to call the QueryServiceStatusEx function to ask the service control manager about the status of the service. 
        SERVICE_ENUMERATE_DEPENDENTS = 0x0008, // Required to call the EnumDependentServices function to enumerate all the services dependent on the service. 
        SERVICE_START = 0x0010, // Required to call the StartService function to start the service. 
        SERVICE_STOP = 0x0020, // Required to call the ControlService function to stop the service. 
        SERVICE_PAUSE_CONTINUE = 0x0040, // Required to call the ControlService function to pause or continue the service. 
        SERVICE_INTERROGATE = 0x0080, // Required to call the ControlService function to ask the service to report its status immediately. 
        SERVICE_USER_DEFINED_CONTROL = 0x0100, // Required to call the ControlService function to specify a user-defined control code.
        SERVICE_ALL_ACCESS = 0xF01FF // Includes STANDARD_RIGHTS_REQUIRED in addition to all access rights in this table. 
    }

    enum ServiceConfig2InfoLevel : int
    {
        SERVICE_CONFIG_DESCRIPTION = 0x00000001, // The lpBuffer parameter is a pointer to a SERVICE_DESCRIPTION structure.
        SERVICE_CONFIG_FAILURE_ACTIONS = 0x00000002 // The lpBuffer parameter is a pointer to a SERVICE_FAILURE_ACTIONS structure.
    }

    public enum SC_ACTION_TYPE : uint
    {
        SC_ACTION_NONE = 0x00000000, // No action.
        SC_ACTION_RESTART = 0x00000001, // Restart the service.
        SC_ACTION_REBOOT = 0x00000002, // Reboot the computer.
        SC_ACTION_RUN_COMMAND = 0x00000003 // Run a command.
    }

    struct SERVICE_FAILURE_ACTIONS 
    {
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 dwResetPeriod;
        [MarshalAs(UnmanagedType.LPStr)]
        public String lpRebootMsg;
        [MarshalAs(UnmanagedType.LPStr)]
        public String lpCommand;
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 cActions;  
        public IntPtr lpsaActions;
    }

    public struct SC_ACTION
    {
        [MarshalAs(UnmanagedType.U4)]
        public SC_ACTION_TYPE Type;
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 Delay;
    }

    #endregion

    #region Native Methods

    class NativeMethods
    {
        private NativeMethods() { }

        [DllImport("advapi32.dll", EntryPoint = "OpenSCManager")]
        public static extern IntPtr OpenSCManager(
            string machineName,
            string databaseName,
            ServiceControlAccessRights desiredAccess);

        [DllImport("advapi32.dll", EntryPoint = "CloseServiceHandle")]
        public static extern int CloseServiceHandle(IntPtr hSCObject);

        [DllImport("advapi32.dll", EntryPoint = "OpenService")]
        public static extern IntPtr OpenService(
            IntPtr hSCManager,
            string serviceName,
            ServiceAccessRights desiredAccess);

        [DllImport("advapi32.dll", EntryPoint = "QueryServiceConfig2")]
        public static extern int QueryServiceConfig2(
            IntPtr hService,
            ServiceConfig2InfoLevel dwInfoLevel,
            IntPtr lpBuffer,
            int cbBufSize,
            out int pcbBytesNeeded);

        [DllImport("advapi32.dll", EntryPoint = "ChangeServiceConfig2")]
        public static extern int ChangeServiceConfig2(
            IntPtr hService,
            ServiceConfig2InfoLevel dwInfoLevel,
            IntPtr lpInfo);
    }

    #endregion

    public class ServiceControlManager: IDisposable
    {
        private IntPtr SCManager;
        private bool disposed;

        /// <summary>
        /// Calls the Win32 OpenService function and performs error checking.
        /// </summary>
        /// <exception cref="ComponentModel.Win32Exception">"Unable to open the requested Service."</exception>
        private IntPtr OpenService(string serviceName, ServiceAccessRights desiredAccess)
        {
            // Open the service
            IntPtr service = NativeMethods.OpenService(
                SCManager,
                serviceName,
                desiredAccess);

            // Verify if the service is opened
            if (service == IntPtr.Zero)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), "Unable to open the requested Service.");
            }

            return service;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceControlManager"/> class.
        /// </summary>
        /// <exception cref="ComponentModel.Win32Exception">"Unable to open Service Control Manager."</exception>
        [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
        public ServiceControlManager()
        {
            // Open the service control manager
            SCManager = NativeMethods.OpenSCManager(
                null,
                null,
                ServiceControlAccessRights.SC_MANAGER_CONNECT);

            // Verify if the SC is opened
            if (SCManager == IntPtr.Zero)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), "Unable to open Service Control Manager.");
            }
        }

        /// <summary>
        /// Dertermines whether the nominated service is set to restart on failure.
        /// </summary>
        /// <exception cref="ComponentModel.Win32Exception">"Unable to query the Service configuration."</exception>
        [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
        public bool HasRestartOnFailure(string serviceName)
        {
            const int bufferSize = 1024 * 8;

            IntPtr service = IntPtr.Zero;
            IntPtr bufferPtr = IntPtr.Zero;
            bool result = false;

            try
            {
                // Open the service
                service = OpenService(serviceName, ServiceAccessRights.SERVICE_QUERY_CONFIG);

                int dwBytesNeeded = 0;

                // Allocate memory for struct
                bufferPtr = Marshal.AllocHGlobal(bufferSize);
                int queryResult = NativeMethods.QueryServiceConfig2(
                    service,
                    ServiceConfig2InfoLevel.SERVICE_CONFIG_FAILURE_ACTIONS,
                    bufferPtr,
                    bufferSize,
                    out dwBytesNeeded);

                if (queryResult == 0)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error(), "Unable to query the Service configuration.");
                }

                // Cast the buffer to a QUERY_SERVICE_CONFIG struct
                SERVICE_FAILURE_ACTIONS config =
                    (SERVICE_FAILURE_ACTIONS)Marshal.PtrToStructure(bufferPtr, typeof(SERVICE_FAILURE_ACTIONS));

                // Determine whether the service is set to auto restart
                if (config.cActions != 0)
                {
                    SC_ACTION action = (SC_ACTION)Marshal.PtrToStructure(config.lpsaActions, typeof(SC_ACTION));
                    result = (action.Type == SC_ACTION_TYPE.SC_ACTION_RESTART);
                }                

                return result;
            }
            finally
            {
                // Clean up
                if (bufferPtr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(bufferPtr);
                }

                if (service != IntPtr.Zero)
                {
                    NativeMethods.CloseServiceHandle(service);
                }
            }
        }

        /// <summary>
        /// Sets the nominated service to restart on failure.
        /// </summary>
        [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
        public void SetRestartOnFailure(string serviceName)
        {
            const int actionCount = 2;
            const uint delay = 60000;

            IntPtr service = IntPtr.Zero;
            IntPtr failureActionsPtr = IntPtr.Zero;
            IntPtr actionPtr = IntPtr.Zero;

            try
            {
                // Open the service
                service = OpenService(serviceName, 
                    ServiceAccessRights.SERVICE_CHANGE_CONFIG | 
                    ServiceAccessRights.SERVICE_START);

                // Allocate memory for the individual actions
                actionPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SC_ACTION)) * actionCount);
                
                // Set up the restart action
                SC_ACTION action1 = new SC_ACTION();
                action1.Type = SC_ACTION_TYPE.SC_ACTION_RESTART;
                action1.Delay = delay;
                Marshal.StructureToPtr(action1, actionPtr, false);

                // Set up the restart action
                SC_ACTION action2 = new SC_ACTION();
                action2.Type = SC_ACTION_TYPE.SC_ACTION_RESTART;
                action2.Delay = delay;
                Marshal.StructureToPtr(action2, (IntPtr)((Int64)actionPtr + Marshal.SizeOf(typeof(SC_ACTION))), false);

                //// Set up the restart action
                //SC_ACTION action3 = new SC_ACTION();
                //action3.Type = SC_ACTION_TYPE.SC_ACTION_RESTART;
                //action3.Delay = delay;
                //Marshal.StructureToPtr(action3, (IntPtr)((Int64)actionPtr + Marshal.SizeOf(typeof(SC_ACTION)) * 2), false);

                // Set up the failure actions
                SERVICE_FAILURE_ACTIONS failureActions = new SERVICE_FAILURE_ACTIONS();
                failureActions.dwResetPeriod = 0;
                failureActions.cActions = actionCount;
                failureActions.lpsaActions = actionPtr;

                failureActionsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SERVICE_FAILURE_ACTIONS)));
                Marshal.StructureToPtr(failureActions, failureActionsPtr, false);

                // Make the change
                int changeResult = NativeMethods.ChangeServiceConfig2(
                    service,
                    ServiceConfig2InfoLevel.SERVICE_CONFIG_FAILURE_ACTIONS,
                    failureActionsPtr);

                // Check that the change occurred
                if (changeResult == 0)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error(), "Unable to change the Service configuration.");
                }
            }
            finally
            {
                // Clean up
                if (failureActionsPtr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(failureActionsPtr);
                }

                if (actionPtr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(actionPtr);
                }

                if (service != IntPtr.Zero)
                {
                    NativeMethods.CloseServiceHandle(service);
                }
            }
        }

        #region IDisposable Members

        /// <summary>
        /// See <see cref="IDisposable.Dispose"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

		/// <summary>
		/// Implements the Dispose(bool) pattern outlined by MSDN and enforced by FxCop.
		/// </summary>
        private void Dispose(bool disposing)
        {            
            if (!this.disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources here
                }

                // Unmanaged resources always need disposing
                if (SCManager != IntPtr.Zero)
                {
                    NativeMethods.CloseServiceHandle(SCManager);
                    SCManager = IntPtr.Zero;
                }
            }
            disposed = true;
        }

        /// <summary>
        /// Finalizer for the <see cref="ServiceControlManager"/> class.
        /// </summary>
        ~ServiceControlManager()
        {
            Dispose(false);
        }

        #endregion
    }

    public class ServiceConfigurator
    {
        private const int SERVICE_ALL_ACCESS = 0xF01FF;
        private const int SC_MANAGER_ALL_ACCESS = 0xF003F;
        private const int SERVICE_CONFIG_DESCRIPTION = 0x1;
        private const int SERVICE_CONFIG_FAILURE_ACTIONS = 0x2;
        private const int SERVICE_NO_CHANGE = -1;
        private const int ERROR_ACCESS_DENIED = 5;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct SERVICE_FAILURE_ACTIONS
        {
            public int dwResetPeriod;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpRebootMsg;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpCommand;
            public int cActions;
            public IntPtr lpsaActions;
        }

        [DllImport("advapi32.dll", EntryPoint = "ChangeServiceConfig2")]
        private static extern bool ChangeServiceFailureActions(IntPtr hService, int dwInfoLevel, [MarshalAs(UnmanagedType.Struct)] ref SERVICE_FAILURE_ACTIONS lpInfo);
        //[DllImport("advapi32.dll", EntryPoint = "ChangeServiceConfig2")]
        //private static extern bool ChangeServiceDescription(IntPtr hService, int dwInfoLevel, [MarshalAs(UnmanagedType.Struct)] ref SERVICE_DESCRIPTION lpInfo);

        [DllImport("kernel32.dll")]
        private static extern int GetLastError();

        private IntPtr _ServiceHandle;
        public IntPtr ServiceHandle { get { return _ServiceHandle; } }

        public ServiceConfigurator(string serviceName)
        {
            ServiceController svcController = new ServiceController(serviceName);
            this._ServiceHandle = svcController.ServiceHandle.DangerousGetHandle();
        }

        public void SetRecoveryOptions()
        {
            uint delay = 60000;
            SC_ACTION pFirstFailure = new SC_ACTION();
            pFirstFailure.Type = SC_ACTION_TYPE.SC_ACTION_RESTART;
            pFirstFailure.Delay = delay;
            SC_ACTION pSecondFailure = new SC_ACTION();
            pSecondFailure.Type = SC_ACTION_TYPE.SC_ACTION_RESTART;
            pSecondFailure.Delay = delay;
            SC_ACTION pSubsequentFailures = new SC_ACTION();
            pSubsequentFailures.Type = SC_ACTION_TYPE.SC_ACTION_RESTART;
            pSubsequentFailures.Delay = delay;
            int pDaysToResetFailureCount = 0;
            int NUM_ACTIONS = 3;
            
            int[] arrActions = new int[NUM_ACTIONS * 2];
            int index = 0;
            arrActions[index++] = (int)pFirstFailure.Type;
            arrActions[index++] = (int)pFirstFailure.Delay;
            arrActions[index++] = (int)pSecondFailure.Type;
            arrActions[index++] = (int)pSecondFailure.Delay;
            arrActions[index++] = (int)pSubsequentFailures.Type;
            arrActions[index++] = (int)pSubsequentFailures.Delay;

            IntPtr tmpBuff = Marshal.AllocHGlobal(NUM_ACTIONS * 8);

            try
            {
                Marshal.Copy(arrActions, 0, tmpBuff, NUM_ACTIONS * 2);
                SERVICE_FAILURE_ACTIONS sfa = new SERVICE_FAILURE_ACTIONS();
                sfa.cActions = 3;
                sfa.dwResetPeriod = pDaysToResetFailureCount;
                sfa.lpCommand = null;
                sfa.lpRebootMsg = null;
                sfa.lpsaActions = new IntPtr(tmpBuff.ToInt32());

                bool success = ChangeServiceFailureActions(_ServiceHandle, SERVICE_CONFIG_FAILURE_ACTIONS, ref sfa);
                if (!success)
                {
                    if (GetLastError() == ERROR_ACCESS_DENIED)
                        throw new Exception("Access denied while setting failure actions.");
                    else
                        throw new Exception("Unknown error while setting failure actions.");
                }
            }
            finally
            {
                Marshal.FreeHGlobal(tmpBuff);
                tmpBuff = IntPtr.Zero;
            }
        }
    }
}
