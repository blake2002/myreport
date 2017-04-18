using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceProcess;
using System.Threading;
using System.Runtime.InteropServices;
using System.Reflection;

namespace PMS.Libraries.ToolControls.PMSPublicInfo
{
    public delegate void DisConnect(object sender, EventArgs e);

    public delegate void ReConnect(object sender, EventArgs e);

    public delegate void ReConnectAtOnce(object sender, EventArgs e);

    public class UpdateIDArgs : EventArgs
    {
        public UpdateIDArgs()
        {

        }

        public UpdateIDArgs(string oldId, string newId)
        {
            OldID = oldId;
            NewID = newId;
        }

        public string OldID
        {
            get;set;
        }

        public string NewID
        {
            get;
            set;
        }
    }

    public class DisConnectArgs : EventArgs
    {
        public DisConnectArgs()
        {

        }

        public DisConnectArgs(string clientID)
        {
            ClientID = clientID;
        }

        public string ClientID
        {
            get;
            set;
        }
    }

    public delegate void UpdateIDHandler(object sender, UpdateIDArgs e);

    public class MESServiceException : Exception
    {
        public MESServiceType ServiceType
        {
            get;
            private set;
        }

        public MESServiceException(MESServiceType serviceType)
            : base()
        {
            ServiceType = serviceType;
        }

        public MESServiceException(MESServiceType serviceType, string message)
            : base(message)
        {
            ServiceType = serviceType;
        }

        public MESServiceException(MESServiceType serviceType, string message, Exception innerException)
            : base(message, innerException)
        {
            ServiceType = serviceType;
        }

        protected MESServiceException(MESServiceType serviceType, SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ServiceType = serviceType;
        }
    }

    public class MESChannelErrorException : MESServiceException
    {
        public MESChannelErrorException(MESServiceType serviceType)
            : base(serviceType, serviceType.ToString() + " Channel disConnect!")
        {
            
        }

        public MESChannelErrorException(MESServiceType serviceType, string channelId)
            : base(serviceType, serviceType.ToString() + "[" + channelId + "] Channel disConnect!")
        {

        }
    }

    [ServiceContract(Namespace = "http://Hite.MES.MESService", SessionMode = SessionMode.Required)]
    public interface IMESService
    {
        bool IsReady
        {
            [OperationContract]
            get;
        }

        // 开始服务
        [OperationContract]
        bool Start();

        // 停止服务
        [OperationContract]
        bool Stop();
    }

    /// <summary>
    /// 服务异常处理接口
    /// </summary>
    public interface IMESServiceExceptionProcess
    {
        event DisConnect DisConnectEvent;
        void OnDisConnect();

        event ReConnect ReConnectEvent;
        void OnReConnect();
    }

    [ServiceContract(Namespace = "http://Hite.MES.MESHeartBeat", SessionMode = SessionMode.Required)]
    public interface IMESHeartBeat
    {
        bool IsReady
        {
            [OperationContract]
            get;
        }

        // 心跳包
        [OperationContract(IsOneWay = true)]
        void HeartBeat(string clientID);
    }

    /// <summary>
    /// 客户端维护通道用
    /// </summary>
    [ServiceContract]
    public interface IMESChannel
    {
        // 立即重连通知事件
        event ReConnectAtOnce ReConnectAtOnceEvent;

        // 客户端关闭前
        event EventHandler BeforeClose;

        // 客户端ID更新
        event UpdateIDHandler OnUpdateID;

        // 客户端关闭后
        event EventHandler AfterClose;

        ICommunicationObject CurrentChannel
        {
            get;
        }

        string ClientID
        {
            get;
        }

        // 测试通道
        [OperationContract]
        bool TestChannel();

        // 重建通道
        [OperationContract]
        bool ReCreateChannel();
    }

    /// <summary>
    /// 服务端默认握手协议
    /// </summary>
    [ServiceContract(Namespace = "http://Hite.MES.ShakeHands", SessionMode = SessionMode.Required)]
    public interface IShakeHands
    {
        // 客户端上线通知
        [OperationContract(IsOneWay = true)]
        void Connect(string sessionId);

        // 根据客户端需求更新客户端标识
        [OperationContract(IsOneWay = true)]
        void UpdateID(string oldId, string newId);

        // 客户端断线重连通知
        [OperationContract(IsOneWay = true)]
        void ReConnect(string oldSessionId, string newSessionId);

        // 客户端下线通知
        [OperationContract(IsOneWay = true)]
        void DisConnect(string sessionId);
    }

    /// <summary>
    /// 服务端配置接口
    /// </summary>
    [ServiceContract(Namespace = "http://Hite.MES.SvcConfig", SessionMode = SessionMode.Required)]
    public interface ISvcConfig
    {
        // 设置客户端缓存模式
        [OperationContract(IsOneWay = true)]
        void SetClientCacheMode(string sessionId, bool enableCache);
    }

    [ServiceContract(Namespace = "http://Hite.MES.Monitor", SessionMode = SessionMode.Required)]
    public interface IMonitor
    {
        /// <summary>
        /// get clients relation information
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<string> MonitorGetClientInfo();

        /// <summary>
        /// config client information
        /// </summary>
        /// <param name="ip">monitor ip</param>
        /// <param name="port">monitor port</param>
        /// <param name="netModule">monitor network module</param>
        /// <returns></returns>
        [OperationContract]
        int MonitorConfig(string ip, int port, string netModule);

        /// <summary>
        /// start monitor
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int MonitorStart();

        /// <summary>
        /// stop monitor
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int MonitorStop();
    }

    [DataContract]
    public enum MESServiceType
    {
        [EnumMember]
        None = 0,
        [EnumMember]
        MESAuthorizationService = 1,
        [EnumMember]
        MESFileTransforService = 2,
        [EnumMember]
        MESVarService = 3,
        [EnumMember]
        MESWFService = 4,
        [EnumMember]
        MESPrivilege = 5,
        [EnumMember]
        MESRegistedClient = 6,
        [EnumMember]
        MESBOToClientsClient = 7,
        [EnumMember]
        MESBroadcastorService = 8,
        [EnumMember]
        MESReportServer = 9,
        [EnumMember]
        MESSystemBOService = 10,
        [EnumMember]
        MESApacheActiveMQService = 11,
        [EnumMember]
        MESSyncTimeService = 12,
        [EnumMember]
        MESDataModelService = 13,
    }

    [DataContract]
    public class RegistedClient
    {
        private MESEnvironment _environment = MESEnvironment.None;
        [DataMember]
        public PMS.Libraries.ToolControls.PMSPublicInfo.MESEnvironment Environment
        {
            get { return _environment; }
            set { _environment = value; }
        }

        private MESServiceType _serviceType = MESServiceType.None;
        [DataMember]
        public PMS.Libraries.ToolControls.PMSPublicInfo.MESServiceType ServiceType
        {
            get { return _serviceType; }
            set { _serviceType = value; }
        }

        private string _ipAddress = string.Empty;
        [DataMember]
        public string IpAddress
        {
            get { return _ipAddress; }
            set { _ipAddress = value; }
        }

        private Guid _subId = Guid.Empty;
        [DataMember]
        public System.Guid SubId
        {
            get { return _subId; }
            set { _subId = value; }
        }

        public override bool Equals(object obj)
        {
            return obj is RegistedClient && this == (RegistedClient)obj;
        }

        public static bool operator ==(RegistedClient x, RegistedClient y)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(x, y))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)x == null) || ((object)y == null))
            {
                return false;
            }
            return (x._environment == y._environment) && (x._serviceType == y._serviceType)
                && (x._ipAddress == y._ipAddress) && (x._subId == y._subId);

        }

        public static bool operator !=(RegistedClient x, RegistedClient y)
        {
            return !(x == y);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class RegistedClientCallback
    {
        private IMESRegistedClientCallback _callback = null;
        public PMS.Libraries.ToolControls.PMSPublicInfo.IMESRegistedClientCallback Callback
        {
            get { return _callback; }
            set { _callback = value; }
        }

        private RegistedClient _client = null;
        public PMS.Libraries.ToolControls.PMSPublicInfo.RegistedClient Client
        {
            get { return _client; }
            set { _client = value; }
        }

        public RegistedClientCallback(RegistedClient client, IMESRegistedClientCallback callback)
        {
            _client = client;
            _callback = callback;
        }

        public override bool Equals(object obj)
        {
            return obj is RegistedClientCallback && this == (RegistedClientCallback)obj;
        }

        public static bool operator ==(RegistedClientCallback x, RegistedClientCallback y)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(x, y))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)x == null) || ((object)y == null))
            {
                return false;
            }
            return (x._client == y._client);
        }

        public static bool operator !=(RegistedClientCallback x, RegistedClientCallback y)
        {
            return !(x == y);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    [ServiceContract]
    public interface IMESControllerService
    {
        // 开始服务
        [OperationContract]
        [ServiceKnownType(typeof(MESServiceType))]
        bool Start(MESServiceType svType);

        // 停止服务
        [OperationContract]
        [ServiceKnownType(typeof(MESServiceType))]
        bool Stop(MESServiceType svType);
    }

    public class MESMESControllerClient : System.ServiceModel.ClientBase<PMS.Libraries.ToolControls.PMSPublicInfo.IMESControllerService>, PMS.Libraries.ToolControls.PMSPublicInfo.IMESControllerService
    {

        public MESMESControllerClient()
        {
        }

        public MESMESControllerClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public MESMESControllerClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public MESMESControllerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public MESMESControllerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public bool Start(MESServiceType svType)
        {
            return base.Channel.Start(svType);
        }

        public bool Stop(MESServiceType svType)
        {
            return base.Channel.Stop(svType);
        }
    }

    [ServiceContract]
    public interface IMESReportService
    {
        [OperationContract]
        [ServiceKnownType(typeof(List<PMS.Libraries.ToolControls.PmsSheet.PmsPublicData.PMSVar>))]
        // Content-报表配置文件xml的二进制
        bool GeneratePdfReport(byte[] Content, object FilterCondition, string strFileName);
    }

    public class MESReportClient : System.ServiceModel.ClientBase<PMS.Libraries.ToolControls.PMSPublicInfo.IMESReportService>, PMS.Libraries.ToolControls.PMSPublicInfo.IMESReportService
    {

        public MESReportClient()
        {
        }

        public MESReportClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public MESReportClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public MESReportClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public MESReportClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public bool GeneratePdfReport(byte[] Content, object FilterCondition, string strFileName)
        {
            return base.Channel.GeneratePdfReport(Content, FilterCondition, strFileName);
        }
    }

    [ServiceContract]
    public interface IMESAuthorizationService : IMESHeartBeat, IShakeHands
    {

        [OperationContract]
        string GetReportAddress();

        [OperationContract]
        string GetCalendarAddress();

        [OperationContract]
        string GetVariableAddress();

        [OperationContract]
        string GetWorkFlowAddress();

        [OperationContract]
        string GetPrivileageAddress();

        [OperationContract]
        string GetFileTransforAddress();

        [OperationContract]
        string GetPDAServerAddress();

        [OperationContract]
        DBConfigSetting GetDBConfigSetting();

        [OperationContract]
        int SetServiceConfigSetting(DBConfigSetting setting);

        [OperationContract]
        bool IsLocalDebugMode();

        [OperationContract]
        string GetLocalDebugIpAddress();

        [OperationContract]
        string GetBroadcastorAddress();

        [OperationContract]
        string GetSystemBOAddress();

        [OperationContract]
        string GetApacheActiveMQAddress();

        [OperationContract]
        string GetSyncTimeAddress();

        [OperationContract]
        string GetDataModelAddress();

    }

    public class MESAuthorizationClient : MESClientBase<IMESAuthorizationService>, IMESAuthorizationService
    {
        #region ctor

        public MESAuthorizationClient() :
            base()
        {
            _ServiceType = MESServiceType.MESAuthorizationService;
        }

        public MESAuthorizationClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
            _ServiceType = MESServiceType.MESAuthorizationService;
        }

        public MESAuthorizationClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
            _ServiceType = MESServiceType.MESAuthorizationService;
        }

        public MESAuthorizationClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
            _ServiceType = MESServiceType.MESAuthorizationService;
        }

        public MESAuthorizationClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
            _ServiceType = MESServiceType.MESAuthorizationService;
        }

        #endregion

        #region IMESAuthorizationService

        public string GetReportAddress()
        {
            if (!_ChannelState)
                ThrowException();
            return (CurrentChannel as IMESAuthorizationService).GetReportAddress();
        }

        public string GetCalendarAddress()
        {
            if (!_ChannelState)
                ThrowException();
            return (CurrentChannel as IMESAuthorizationService).GetCalendarAddress();
        }

        public string GetVariableAddress()
        {
            if (!_ChannelState)
                ThrowException();
            return (CurrentChannel as IMESAuthorizationService).GetVariableAddress();
        }

        public string GetWorkFlowAddress()
        {
            if (!_ChannelState)
                ThrowException();
            return (CurrentChannel as IMESAuthorizationService).GetWorkFlowAddress();
        }

        public string GetPrivileageAddress()
        {
            if (!_ChannelState)
                ThrowException();
            return (CurrentChannel as IMESAuthorizationService).GetPrivileageAddress();
        }

        public string GetFileTransforAddress()
        {
            if (!_ChannelState)
                ThrowException();
            return (CurrentChannel as IMESAuthorizationService).GetFileTransforAddress();
        }

        public string GetPDAServerAddress()
        {
            if (!_ChannelState)
                ThrowException();
            return (CurrentChannel as IMESAuthorizationService).GetPDAServerAddress();
        }

        public string GetBroadcastorAddress()
        {
            if (!_ChannelState)
                ThrowException();
            return (CurrentChannel as IMESAuthorizationService).GetBroadcastorAddress();
        }

        public string GetSystemBOAddress()
        {
            if (!_ChannelState)
                ThrowException();
            return (CurrentChannel as IMESAuthorizationService).GetSystemBOAddress();
        }

        public DBConfigSetting GetDBConfigSetting()
        {
            if (!_ChannelState)
                ThrowException();
            return (CurrentChannel as IMESAuthorizationService).GetDBConfigSetting();
        }

        public int SetServiceConfigSetting(DBConfigSetting setting)
        {
            if (!_ChannelState)
                ThrowException();
            return (CurrentChannel as IMESAuthorizationService).SetServiceConfigSetting(setting);
        }

        public bool IsLocalDebugMode()
        {
            if (!_ChannelState)
                ThrowException();
            return (CurrentChannel as IMESAuthorizationService).IsLocalDebugMode();
        }

        public string GetLocalDebugIpAddress()
        {
            if (!_ChannelState)
                ThrowException();
            return (CurrentChannel as IMESAuthorizationService).GetLocalDebugIpAddress();
        }

        public string GetApacheActiveMQAddress()
        {
            if (!_ChannelState)
                ThrowException();
            return (CurrentChannel as IMESAuthorizationService).GetApacheActiveMQAddress();
        }

        public string GetSyncTimeAddress()
        {
            if (!_ChannelState)
                ThrowException();
            return (CurrentChannel as IMESAuthorizationService).GetSyncTimeAddress();
        }

        public string GetDataModelAddress()
        {
            if (!_ChannelState)
                ThrowException();
            return (CurrentChannel as IMESAuthorizationService).GetDataModelAddress();
        }

        #endregion
    }

    public class MESAuthorizationControlClient : System.ServiceModel.ClientBase<PMS.Libraries.ToolControls.PMSPublicInfo.IMESService>, PMS.Libraries.ToolControls.PMSPublicInfo.IMESService
    {

        //public MESAuthorizationControlClient()
        //{
        //}

        //public MESAuthorizationControlClient(string endpointConfigurationName) :
        //    base(endpointConfigurationName)
        //{
        //}

        public MESAuthorizationControlClient(string remoteAddress) :
            base("WSHttpBinding_AuthorizationService_IMESService", remoteAddress)
        {
        }

        //public MESAuthorizationControlClient(string endpointConfigurationName, string remoteAddress) :
        //    base(endpointConfigurationName, remoteAddress)
        //{
        //}

        //public MESAuthorizationControlClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
        //    base(endpointConfigurationName, remoteAddress)
        //{
        //}

        //public MESAuthorizationControlClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
        //    base(binding, remoteAddress)
        //{
        //}

        public bool IsReady
        {
            get;
            protected set;
        }

        public bool Start()
        {
            return base.Channel.Start();
        }

        public bool Stop()
        {
            return base.Channel.Stop();
        }
    }

    public class HeartBeatClient : System.ServiceModel.ClientBase<PMS.Libraries.ToolControls.PMSPublicInfo.IMESHeartBeat>, PMS.Libraries.ToolControls.PMSPublicInfo.IMESHeartBeat, IDisposable
    {
        public delegate void DisposeHandler(object sender, EventArgs e);
        public virtual event DisposeHandler DisposedEvent;

        public HeartBeatClient()
        {
        }

        public HeartBeatClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public HeartBeatClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public HeartBeatClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public HeartBeatClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public bool IsReady
        {
            get
            {
                return base.Channel.IsReady;
            }
        }

        public void HeartBeat(string clientID)
        {
            base.Channel.HeartBeat(clientID);
        }

        public void Dispose()
        {
            if(null != DisposedEvent)
            {
                DisposedEvent(this, new EventArgs());
            }
        }
    }

    [ServiceContract]
    public interface IMESPrivilegeService : IMESHeartBeat, IShakeHands
    {
        [OperationContract]
        int LoginClient(string userName, string pass);

        [OperationContract]
        bool LogoutClient(string userName);

        [OperationContract]
        bool LogCancelClient(string userName, string pass);

        [OperationContract]
        int LoginDev(string userName, string pass);

        [OperationContract]
        bool LogoutDev(string userName);

        [OperationContract]
        bool LogCancelDev(string userName, string pass);

    }

    public class MESPrivilegeClient : MESClientBase<IMESPrivilegeService>, IMESPrivilegeService
    {
        #region ctor

        public MESPrivilegeClient()
        {
            _ServiceType = MESServiceType.MESPrivilege;
        }

        public MESPrivilegeClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
            _ServiceType = MESServiceType.MESPrivilege;
        }

        public MESPrivilegeClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
            _ServiceType = MESServiceType.MESPrivilege;
        }

        public MESPrivilegeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
            _ServiceType = MESServiceType.MESPrivilege;
        }

        public MESPrivilegeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
            _ServiceType = MESServiceType.MESPrivilege;
        }

       #endregion

        #region IMESPrivilegeService

        public int LoginClient(string userName, string pass)
        {
            if (!_ChannelState)
                ThrowException();
            return (CurrentChannel as IMESPrivilegeService).LoginClient(userName, pass);
        }

        public bool LogoutClient(string userName)
        {
            if (!_ChannelState)
                ThrowException();
            return (CurrentChannel as IMESPrivilegeService).LogoutClient(userName);
        }

        public bool LogCancelClient(string userName, string pass)
        {
            if (!_ChannelState)
                ThrowException();
            return (CurrentChannel as IMESPrivilegeService).LogCancelClient(userName, pass);
        }

        public int LoginDev(string userName, string pass)
        {
            if (!_ChannelState)
                ThrowException();
            return (CurrentChannel as IMESPrivilegeService).LoginDev(userName, pass);
        }

        public bool LogoutDev(string userName)
        {
            if (!_ChannelState)
                ThrowException();
            return (CurrentChannel as IMESPrivilegeService).LogoutDev(userName);
        }

        public bool LogCancelDev(string userName, string pass)
        {
            if (!_ChannelState)
                ThrowException();
            return (CurrentChannel as IMESPrivilegeService).LogCancelDev(userName, pass);
        }

        #endregion
    }

    [ServiceContract(Namespace = "http://www.hite.com.cn/RD/MES/DualHttpBinding", CallbackContract = typeof(IMESRegistedClientCallback))]
    public interface IMESRegistedClientService// : IMESHeartBeat
    {
        [OperationContract]
        bool RegisterClient(RegistedClient client);

        [OperationContract]
        bool UnRegisterClient(RegistedClient client);

        // 心跳包
        [OperationContract]
        void HeartBeat(string clientID);

    }

    public interface IMESRegistedClientCallback
    {
        [OperationContract(Name = "BOMonitorCallback", IsOneWay=true)]
        void BOMonitorCallback(s_LogInfo loginfo);

        [OperationContract(Name = "BOMonitorCallback_ex", IsOneWay = true)]
        void BOMonitorCallback(DateTime logtime, string loguser, string source, string description, Guid WFInstanceID, Guid WFTaskID);
    }

    public class MESRegistedClientBOMonitor : IMESRegistedClientCallback
    {
        public static MESRegistedClient client = null;

        public static void Initialize()
        {
            MESRegistedClientBOMonitor program = new MESRegistedClientBOMonitor();
            InstanceContext instanceContext = new InstanceContext(program);
            //client = MES.ServiceManager.MESServiceManager.GetWorkFlowClient(instanceContext);
            //client.Monitoring(PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.CurrentLoginUserID);
        }

        public void BOMonitorCallback(s_LogInfo loginfo)
        {
            PMS.Libraries.ToolControls.PMSPublicInfo.Message.Info(loginfo.UID_, loginfo.Description, false);
        }

        public void BOMonitorCallback(DateTime logtime, string loguser, string source, string description, Guid WFInstanceID, Guid WFTaskID)
        {
            PMS.Libraries.ToolControls.PMSPublicInfo.Message.Info(loguser, description, false);
        }
    }

    //public class MESRegistedClientChain : System.ServiceModel.DuplexClientBase<PMS.Libraries.ToolControls.PMSPublicInfo.IMESRegistedClientService>, PMS.Libraries.ToolControls.PMSPublicInfo.IMESHeartBeat
    //{
    //    private object _CallBackInstance = null;

    //    public MESRegistedClientChain(string endpointConfigurationName) :
    //        base(endpointConfigurationName)
    //    {
    //    }

    //    public MESRegistedClientChain(string endpointConfigurationName, string remoteAddress) :
    //        base(endpointConfigurationName, remoteAddress)
    //    {
    //    }

    //    public MESRegistedClientChain(InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress)
    //        : base(callbackInstance, binding, remoteAddress)
    //    {
    //        _CallBackInstance = callbackInstance;
    //    }

    //    public void HeartBeat()
    //    {
    //        base.Channel.HeartBeat();
    //    }

    //}

    public class MESRegistedClient : System.ServiceModel.DuplexClientBase<PMS.Libraries.ToolControls.PMSPublicInfo.IMESRegistedClientService>, PMS.Libraries.ToolControls.PMSPublicInfo.IMESRegistedClientService, IMESHeartBeat, IMESServiceExceptionProcess
    {
        public event DisConnect DisConnectEvent;
        public event ReConnect ReConnectEvent;

        private object _CallBackInstance = null;

        public MESRegistedClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public MESRegistedClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public MESRegistedClient(InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress)
            : base(callbackInstance, binding, remoteAddress)
        {
            _CallBackInstance = callbackInstance;
        }

        public bool RegisterClient(RegistedClient client)
        {
            return base.Channel.RegisterClient(client);
        }

        public bool UnRegisterClient(RegistedClient client)
        {
            return base.Channel.UnRegisterClient(client);
        }

        public bool IsReady
        {
            get
            {
                return (this as IMESHeartBeat).IsReady;
            }
        }

        public void HeartBeat(string clientID)
        {
            base.Channel.HeartBeat(clientID);
        }

        public void OnDisConnect()
        {
            if (null != DisConnectEvent)
            {
                DisConnectEvent(this, null);
            }
        }

        public void OnReConnect()
        {
            if (null != ReConnectEvent)
            {
                ReConnectEvent(this, null);
            }
        }
    }

    [ServiceContract]
    public interface IMESBOToClientsService// : IMESHeartBeat
    {
        [OperationContract(Name = "BOLogToClients")]
        bool BOLogToClients(s_LogInfo loginfo);

        [OperationContract(Name = "BOLogToClients_ex")]
        bool BOLogToClients(DateTime logtime, string loguser, string source, string description, Guid WFInstanceID, Guid WFTaskID);

        // 心跳包
        [OperationContract]
        void HeartBeat(string clientID);
    }

    public class MESBOToClientsClient : System.ServiceModel.ClientBase<PMS.Libraries.ToolControls.PMSPublicInfo.IMESBOToClientsService>, PMS.Libraries.ToolControls.PMSPublicInfo.IMESBOToClientsService, IMESHeartBeat, PMS.Libraries.ToolControls.PMSPublicInfo.IMESServiceExceptionProcess
    {

        public event DisConnect DisConnectEvent;
        public event ReConnect ReConnectEvent;

        public MESBOToClientsClient()
        {
        }

        public MESBOToClientsClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public MESBOToClientsClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public MESBOToClientsClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public MESBOToClientsClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public bool BOLogToClients(s_LogInfo loginfo)
        {
            return base.Channel.BOLogToClients(loginfo);
        }

        public bool BOLogToClients(DateTime logtime, string loguser, string source, string description, Guid WFInstanceID, Guid WFTaskID)
        {
            return base.Channel.BOLogToClients(logtime, loguser, source, description, WFInstanceID, WFTaskID);
        }

        public bool IsReady
        {
            get
            {
                return (this as IMESHeartBeat).IsReady;
            }
        }

        public void HeartBeat(string clientID)
        {
            base.Channel.HeartBeat(clientID);
        }

        public void OnDisConnect()
        {
            if (null != DisConnectEvent)
            {
                DisConnectEvent(this, null);
            }
        }

        public void OnReConnect()
        {
            if (null != ReConnectEvent)
            {
                ReConnectEvent(this, null);
            }
        }
    }

    public class MESClientBase<TChannel> : ClientBase<TChannel>, IMESHeartBeat, IMESServiceExceptionProcess, IMESChannel, IShakeHands where TChannel : class
    {
        private TChannel _Channel = null;
        protected bool _ChannelState = true;
        protected bool _ServiceIsReady = true;
        protected MESServiceType _ServiceType = MESServiceType.None;

        #region ctor
        
        public MESClientBase() :
            base()
        {
            _Channel = base.Channel;
            CreatClientID();
        }

        public MESClientBase(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
            _Channel = base.Channel;
            CreatClientID();
        }

        public MESClientBase(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
            _Channel = base.Channel;
            CreatClientID();
        }

        public MESClientBase(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
            _Channel = base.Channel;
            CreatClientID();
        }

        public MESClientBase(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
            _Channel = base.Channel;
            CreatClientID();
        }

        #endregion

        #region user func

        private void CreatClientID()
        {
            //ClientID = InnerChannel.GetHashCode().ToString();
            //ClientID = Guid.NewGuid().GetHashCode().ToString();
            byte[] buffer = Guid.NewGuid().ToByteArray();
            ClientID = BitConverter.ToInt64(buffer, 0).ToString();
        }

        // 初始化未Connect之前修改ClientID，Connect之后用UpdateID()
        public void SetInitialClientID(string newID)
        {
            ClientID = newID;
        }

        protected void ThrowException()
        {
            throw new MESChannelErrorException(_ServiceType, ClientID);
        }

        public void StartRepaireAtOnce()
        {
            _ChannelState = false;
            if(null != ReConnectAtOnceEvent)
            {
                ReConnectAtOnceEvent(this, new EventArgs());
            }
        }

        public bool UpdateID(string newId)
        {
            try
            {
                if (newId == this.ClientID)
                    return true;
                if (_Channel is IShakeHands)
                    (_Channel as IShakeHands).UpdateID(this.ClientID, newId);
                UpdateIDArgs arg = new UpdateIDArgs(this.ClientID, newId);
                this.ClientID = newId;
                if (null != OnUpdateID)
                {
                    OnUpdateID(this, arg);
                }
            }
            catch (System.Exception ex)
            {
                _ChannelState = false;
                StartRepaireAtOnce();
                return false;
            }
            return true;
        }

        public new void Close()
        {
            if(null != BeforeClose)
            {
                BeforeClose(this, new DisConnectArgs(ClientID));
            }
            (this as IShakeHands).DisConnect(ClientID);
            if (null != _Channel && _Channel is ICommunicationObject)
                (_Channel as ICommunicationObject).Close();
            //base.Close();
            if (null != AfterClose)
            {
                AfterClose(this, new DisConnectArgs(ClientID));
            }
        }

        #endregion

        #region IMESHeartBeat

        public bool IsReady
        {
            get
            {
                try
                {
                    if (_Channel is IMESHeartBeat)
                    {
                        _ServiceIsReady = (_Channel as IMESHeartBeat).IsReady;
                    }
                }
                catch (System.Exception ex)
                {
                    _ServiceIsReady = false;
                }
                return _ServiceIsReady;
            }
        }

        public void HeartBeat(string clientID = null)
        {
            try
            {
                if (_Channel is IMESHeartBeat)
                {
                    if (string.IsNullOrEmpty(clientID))
                        (_Channel as IMESHeartBeat).HeartBeat(ClientID);
                    else
                        (_Channel as IMESHeartBeat).HeartBeat(clientID);
                }
            }
            catch (System.Exception ex)
            {
                _ChannelState = false;
                ThrowException();
                return;
            }
            _ChannelState = true;
        }

        #endregion

        #region IMESServiceExceptionProcess

        public event DisConnect DisConnectEvent;
        public virtual void OnDisConnect()
        {
            if (null != DisConnectEvent)
            {
                DisConnectEvent(this, null);
            }
        }

        public event ReConnect ReConnectEvent;
        public virtual void OnReConnect()
        {
            if (null != ReConnectEvent)
            {
                ReConnectEvent(this, null);
            }
        }

        #endregion

        #region IMESChannel

        public event ReConnectAtOnce ReConnectAtOnceEvent;

        public event EventHandler BeforeClose;

        public event EventHandler AfterClose;

        public event UpdateIDHandler OnUpdateID;

        public ICommunicationObject CurrentChannel
        {
            get { return _Channel as ICommunicationObject; }
        }

        public string ClientID
        {
            get;
            private set;
        }

        bool IMESChannel.TestChannel()
        {
            try
            {
                ICommunicationObject ic = this.ChannelFactory.CreateChannel() as ICommunicationObject;
                if (null != ic)
                {
                    ic.Open(new TimeSpan(0, 0, 0, 2, 0));
                    ic.Close(new TimeSpan(0, 0, 0, 2, 0));
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }

            return true;
        }

        bool IMESChannel.ReCreateChannel()
        {
            try
            {
                _Channel = this.ChannelFactory.CreateChannel();
                ICommunicationObject ic = _Channel as ICommunicationObject;
                if (null != ic)
                {
                    ic.Open(new TimeSpan(0, 0, 0, 2, 0));
                    if (_Channel is IShakeHands)
                    {
                        IShakeHands ish = _Channel as IShakeHands;
                        if (null != ish)
                        {
                            (this as IShakeHands).ReConnect(ClientID, ClientID);
                        }
                    }
                }
                _ChannelState = true;
            }
            catch (System.Exception ex)
            {
                _Channel = base.Channel;
                _ChannelState = false;
                return false;
            }

            return true;
        }

        #endregion

        #region IShakeHands

        // 客户端上线通知
        void IShakeHands.Connect(string sessionId)
        {
            if(_Channel is IShakeHands)
                (_Channel as IShakeHands).Connect(sessionId);
        }

        // 根据客户端需求更新客户端标识
        void IShakeHands.UpdateID(string oldId, string newId)
        {
            if (oldId == newId)
                return ;
            if (_Channel is IShakeHands)
                (_Channel as IShakeHands).UpdateID(oldId, newId);
        }

        // 客户端断线重连通知
        void IShakeHands.ReConnect(string oldSessionId, string newSessionId)
        {
            if (_Channel is IShakeHands)
                (_Channel as IShakeHands).ReConnect(oldSessionId, newSessionId);
        }

        // 客户端下线通知
        void IShakeHands.DisConnect(string sessionId)
        {
            if (_Channel is IShakeHands)
                (_Channel as IShakeHands).DisConnect(sessionId);
        }

        #endregion
    }

    public class MESDuplexClientBase<TChannel> : DuplexClientBase<TChannel> , IMESHeartBeat, IMESServiceExceptionProcess, IMESChannel, IShakeHands where TChannel : class
    {
        private TChannel _Channel = null;
        protected bool _ChannelState = true;
        protected bool _ServiceIsReady = true;
        protected MESServiceType _ServiceType = MESServiceType.None;
        protected new TChannel Channel 
        { 
            get
            {
                return _Channel;
            }
        }

        #region ctor

        public MESDuplexClientBase(InstanceContext callbackInstance) :
            base(callbackInstance)
        {
            _Channel = base.Channel;
            CreatClientID();
        }

        public MESDuplexClientBase(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
            _Channel = base.Channel;
            CreatClientID();
        }

        public MESDuplexClientBase(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
            _Channel = base.Channel;
            CreatClientID();
        }

        public MESDuplexClientBase(InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(callbackInstance, endpointConfigurationName, remoteAddress)
        {
            _Channel = base.Channel;
            CreatClientID();
        }

        public MESDuplexClientBase(InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(callbackInstance, binding, remoteAddress)
        {
            _Channel = base.Channel;
            CreatClientID();
        }

        #endregion

        #region user func

        private void CreatClientID()
        {
            //ClientID = InnerChannel.GetHashCode().ToString();
            //ClientID = Guid.NewGuid().GetHashCode().ToString();
            byte[] buffer = Guid.NewGuid().ToByteArray();
            ClientID =  Math.Abs(BitConverter.ToInt64(buffer, 0)).ToString();
        }

        // 初始化未Connect之前修改ClientID，Connect之后用UpdateID()
        public void SetInitialClientID(string newID)
        {
            ClientID = newID;
        }

        protected void ThrowException()
        {
            throw new MESChannelErrorException(_ServiceType, ClientID);
        }

        public void StartRepaireAtOnce()
        {
            _ChannelState = false;
            if (null != ReConnectAtOnceEvent)
            {
                ReConnectAtOnceEvent(this, new EventArgs());
            }
        }

        public bool UpdateID(string newId)
        {
            try
            {
                if (newId == this.ClientID)
                    return true;
                if (_Channel is IShakeHands)
                    (_Channel as IShakeHands).UpdateID(this.ClientID, newId);
                UpdateIDArgs arg = new UpdateIDArgs(this.ClientID, newId);
                this.ClientID = newId;
                if (null != OnUpdateID)
                {
                    OnUpdateID(this, arg);
                }
            }
            catch (System.Exception ex)
            {
                _ChannelState = false;
                StartRepaireAtOnce();
                return false;
            }
            return true;
        }

        public new void Close()
        {
            if (null != BeforeClose)
            {
                BeforeClose(this, new DisConnectArgs(ClientID));
            }
            (this as IShakeHands).DisConnect(ClientID);
            if (null != _Channel && _Channel is ICommunicationObject)
                (_Channel as ICommunicationObject).Close();
            //base.Close();
            if (null != AfterClose)
            {
                AfterClose(this, new DisConnectArgs(ClientID));
            }
        }

        #endregion

        #region IMESHeartBeat

        public bool IsReady
        {
            get
            {
                try
                {
                    if (_Channel is IMESHeartBeat)
                    {
                        _ServiceIsReady = (_Channel as IMESHeartBeat).IsReady;
                    }
                }
                catch (System.Exception ex)
                {
                    _ServiceIsReady = false;
                }
                return _ServiceIsReady;
            }
        }

        public void HeartBeat(string clientID)
        {
            try
            {
                if (string.IsNullOrEmpty(clientID))
                    (_Channel as IMESHeartBeat).HeartBeat(ClientID);
                else
                    (_Channel as IMESHeartBeat).HeartBeat(clientID);
            }
            catch (System.Exception ex)
            {
                _ChannelState = false;
                ThrowException();
                return;
            }
            _ChannelState = true;
        }

        #endregion

        #region IMESServiceExceptionProcess

        public event DisConnect DisConnectEvent;
        public virtual void OnDisConnect()
        {
            if (null != DisConnectEvent)
            {
                DisConnectEvent(this, null);
            }
        }

        public event ReConnect ReConnectEvent;
        public virtual void OnReConnect()
        {
            if (null != ReConnectEvent)
            {
                ReConnectEvent(this, null);
            }
        }

        #endregion

        #region IMESChannel

        public event ReConnectAtOnce ReConnectAtOnceEvent;

        public event EventHandler BeforeClose;

        public event EventHandler AfterClose;

        public event UpdateIDHandler OnUpdateID;

        public ICommunicationObject CurrentChannel
        {
            get { return _Channel as ICommunicationObject; }
        }

        public string ClientID
        {
            get;
            private set;
        }

        bool IMESChannel.TestChannel()
        {
            try
            {
                ICommunicationObject ic = this.ChannelFactory.CreateChannel() as ICommunicationObject;
                if (null != ic)
                {
                    ic.Open(new TimeSpan(0, 0, 0, 2, 0));
                    ic.Close(new TimeSpan(0, 0, 0, 2, 0));
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }

            return true;
        }

        bool IMESChannel.ReCreateChannel()
        {
            try
            {
                _Channel = this.ChannelFactory.CreateChannel();
                ICommunicationObject ic = _Channel as ICommunicationObject;
                if (null != ic)
                {
                    ic.Open(new TimeSpan(0, 0, 0, 2, 0));
                    if (_Channel is IShakeHands)
                    {
                        IShakeHands ish = _Channel as IShakeHands;
                        if (null != ish)
                        {
                            (this as IShakeHands).ReConnect(ClientID, ClientID);
                        }
                    }
                }
                _ChannelState = true;
            }
            catch (System.Exception ex)
            {
                _Channel = base.Channel;
                _ChannelState = false;
                return false;
            }

            return true;
        }

        #endregion

        #region IShakeHands

        // 客户端上线通知
        void IShakeHands.Connect(string sessionId)
        {
            if (_Channel is IShakeHands)
                (_Channel as IShakeHands).Connect(sessionId);
        }

        // 根据客户端需求更新客户端标识
        void IShakeHands.UpdateID(string oldId, string newId)
        {
            if (oldId == newId)
                return;
            if (_Channel is IShakeHands)
                (_Channel as IShakeHands).UpdateID(oldId, newId);
        }

        // 客户端断线重连通知
        void IShakeHands.ReConnect(string oldSessionId, string newSessionId)
        {
            if (_Channel is IShakeHands)
                (_Channel as IShakeHands).ReConnect(oldSessionId, newSessionId);
        }

        // 客户端下线通知
        void IShakeHands.DisConnect(string sessionId)
        {
            if (_Channel is IShakeHands)
                (_Channel as IShakeHands).DisConnect(sessionId);
        }

        #endregion
    }

    public class MESCacheClientBase<TChannel> : MESClientBase<TChannel>, ISvcConfig, IMESCacheClient where TChannel : class
    {
        #region ctor
        
        public MESCacheClientBase() :
            base()
        {
            
        }

        public MESCacheClientBase(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
            
        }

        public MESCacheClientBase(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
            
        }

        public MESCacheClientBase(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
            
        }

        public MESCacheClientBase(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
            
        }

        #endregion

        #region IMESCacheClient

        public bool EnableCache
        {
            get;
            set;
        }

        #endregion

        #region ISvcConfig

        public void SetClientCacheMode(string sessionId, bool enableCache)
        {
            if (Channel is ISvcConfig)
                (Channel as ISvcConfig).SetClientCacheMode(sessionId, enableCache);
        }

        #endregion
    }

    public class MESDuplexCacheClientBase<TChannel> : MESDuplexClientBase<TChannel>, ISvcConfig, IMESCacheClient where TChannel : class
    {
        #region ctor

        public MESDuplexCacheClientBase(InstanceContext callbackInstance) :
            base(callbackInstance)
        {
            
        }

        public MESDuplexCacheClientBase(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
            
        }

        public MESDuplexCacheClientBase(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
            
        }

        public MESDuplexCacheClientBase(InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(callbackInstance, endpointConfigurationName, remoteAddress)
        {
            
        }

        public MESDuplexCacheClientBase(InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(callbackInstance, binding, remoteAddress)
        {
            
        }

        #endregion

        #region IMESCacheClient
        
        public bool EnableCache
        {
            get;
            set;
        }

        #endregion

        #region ISvcConfig
        
        public void SetClientCacheMode(string sessionId, bool enableCache)
        {
            if (Channel is ISvcConfig)
                (Channel as ISvcConfig).SetClientCacheMode(sessionId, enableCache);
        }

        #endregion
    }

    #region 普通非缓存模式客户端管理接口

    public interface IMESConnectedClient
    {
        PMS.Libraries.ToolControls.PMSPublicInfo.MESServiceType ServiceType
        {
            get;
            set;
        }

        string IpAddress
        {
            get;
            set;
        }

        DateTime LastHeartbeatUpdateTime
        {
            get;
            set;
        }

        DateTime LastConnectTime
        {
            get;
            set;
        }

        bool ClientState
        {
            get;
            set;
        }
    }

    public interface IMESConnectedClientCallback<TCallBack> : IMESConnectedClient where TCallBack : class
    {
        TCallBack ICallBack
        {
            get;
            set;
        }
    }

    #endregion

    [DataContract]
    public class MESConnectedClient : IMESConnectedClient
    {
        private MESServiceType _serviceType = MESServiceType.None;
        [DataMember]
        public PMS.Libraries.ToolControls.PMSPublicInfo.MESServiceType ServiceType
        {
            get { return _serviceType; }
            set { _serviceType = value; }
        }

        private string _ipAddress = string.Empty;
        [DataMember]
        public string IpAddress
        {
            get { return _ipAddress; }
            set { _ipAddress = value; }
        }

        /// <summary>
        /// 临时变量，记录心跳更新时间，不用比较和序列化
        /// </summary>
        private DateTime _LastHeartbeatUpdateTime = DateTime.MinValue;
        public DateTime LastHeartbeatUpdateTime
        {
            get { return _LastHeartbeatUpdateTime; }
            set { _LastHeartbeatUpdateTime = value; }
        }

        /// <summary>
        /// 临时变量，记录客户端上次上线时间，不用比较和序列化
        /// </summary>
        private DateTime _LastConnectTime = DateTime.MinValue;
        public DateTime LastConnectTime
        {
            get { return _LastConnectTime; }
            set { _LastConnectTime = value; }
        }

        private bool _ClientState = true;
        public bool ClientState
        {
            get { return _ClientState; }
            set { _ClientState = value; }
        }

        public override bool Equals(object obj)
        {
            return obj is MESConnectedClient && this == (MESConnectedClient)obj;
        }

        public static bool operator ==(MESConnectedClient x, MESConnectedClient y)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(x, y))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)x == null) || ((object)y == null))
            {
                return false;
            }
            return (x._serviceType == y._serviceType)
                && (x._ipAddress == y._ipAddress);

        }

        public static bool operator !=(MESConnectedClient x, MESConnectedClient y)
        {
            return !(x == y);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class MESConnectedClient<TCallBack> : MESConnectedClient, IMESConnectedClientCallback<TCallBack> where TCallBack : class
    {
        public TCallBack ICallBack
        {
            get;
            set;
        }
    }

    #region 启用缓存模式客户端管理接口

    public interface IMESCacheClient
    {
        bool EnableCache
        {
            get;
            set;
        }
    }

    [DataContract]
    public class MESCacheConnectedClient : MESConnectedClient, IMESCacheClient
    {
        private bool _EnableCache = false;

        [DataMember]
        public bool EnableCache
        {
            get
            {
                return _EnableCache;
            }
            set
            {
                _EnableCache = value;
            }
        }
    }

    public class MESCacheConnectedClient<TCallBack> : MESCacheConnectedClient, IMESConnectedClientCallback<TCallBack> where TCallBack : class
    {
        public TCallBack ICallBack
        {
            get;
            set;
        }
    }

    #endregion

    public class MESServiceBase : ServiceBase, IMESService, IMESHeartBeat, IShakeHands//, IMonitor
    {
        protected ServiceHost serviceHost = null;

        protected PMS.Libraries.ToolControls.PMSPublicInfo.MESServiceType serviceType = MESServiceType.None;

        private static Dictionary<string, IMESConnectedClient> _clients = new Dictionary<string, IMESConnectedClient>();
        protected virtual Dictionary<string, IMESConnectedClient> clients
        {
            get
            {
                return _clients;
            }
            set
            {
                _clients = value;
            }
        }

        protected static object locker = new object();

        private static Thread _DeadChannelDetectThread = null;
        protected static bool _StopDetect = false;

        public MESServiceBase()
            : base()
        {

        }

        #region user func

        /// <summary>
        /// 供服务端扩展客户端管理对象之用
        /// </summary>
        /// <returns>返回继承于MESConnectedClient的对象</returns>
        protected virtual IMESConnectedClient ObtainOverrideConnectedClient(string sessionId)
        {
            return null;
        }

        protected string GetIncomingIPAddress()
        {
            //提供方法执行的上下文环境
            OperationContext context = OperationContext.Current;
            //获取传进的消息属性
            MessageProperties properties = context.IncomingMessageProperties;
            //获取消息发送的远程终结点IP和端口
            RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            string ip = string.Format("{0}:{1}", endpoint.Address, endpoint.Port);
            return ip;
        }

        private bool StartDetectChannel()
        {
            if (_DeadChannelDetectThread == null && _StopDetect == false)
            {
                _DeadChannelDetectThread = new Thread(new ThreadStart(DetectChannelThread));
                _DeadChannelDetectThread.IsBackground = true;
                _DeadChannelDetectThread.Priority = ThreadPriority.BelowNormal;
                _DeadChannelDetectThread.Start();
            }
            return true;
        }

        private void DetectChannelThread()
        {
            int interval = 20 * 1000;   //unit-second
            while (!_StopDetect)
            {
                Thread.Sleep(interval);
                DateTime dtNow = DateTime.Now;
                List<string> deadClientIDs = GetDeadClientIDs();
                foreach (string id in deadClientIDs)
                {
                    if (_StopDetect)
                        break;
                    DisConnect(id);
                }
            }
        }

        protected virtual List<string> GetDeadClientIDs()
        {
            List<string> deadClientIDs = new List<string>();
            foreach (KeyValuePair<string, IMESConnectedClient> pair in clients)
            {
                if (_StopDetect)
                    break;
                string clientID = pair.Key;
                IMESConnectedClient client = pair.Value;
                TimeSpan ts = DateTime.Now - client.LastHeartbeatUpdateTime;
                // 连续五次未收到心跳，将客户端状态设为false
                if (ts > new TimeSpan(0, 0, 50))
                {
                    client.ClientState = false;
                }
                // 是缓存客户端
                if (client is IMESCacheClient)
                {
                    //且启用了缓存模式，不做自动死链接处理(默认间隔时间为5天)
                    if ((client as IMESCacheClient).EnableCache)
                    {
                        string s = System.Configuration.ConfigurationManager.AppSettings["CacheClient"];
                        if (ts > new TimeSpan(24*5, 0, 0))
                        {
                            // 大于5天未发生心跳的视为Dead Channel，移除
                            deadClientIDs.Add(clientID);
                        }
                    }
                }
                else
                {
                    // 非缓存客户端
                    if (ts > new TimeSpan(0, 10, 0))
                    {
                        // 大于10分钟未发生心跳的视为Dead Channel，移除
                        deadClientIDs.Add(clientID);
                    }
                }
            }
            return deadClientIDs;
        }

        private bool StopDetectChannel()
        {
            _StopDetect = true;
            try
            {
                if (null != _DeadChannelDetectThread)
                {
                    if (false == _DeadChannelDetectThread.Join(new TimeSpan(0, 0, 21)))//21秒钟
                    {
                        _DeadChannelDetectThread.Abort();
                    }
                }
            }
            catch (System.Exception ex)
            {

            }

            _DeadChannelDetectThread = null;
            return true;
        }

        /// <summary>
        /// client infromation state
        /// </summary>
        /// <returns></returns>
        private bool BaseClientIsOk()
        {
            return (clients != null || clients.Count > 0);
        }

        /// <summary>
        /// convert client object to message
        /// </summary>
        /// <returns></returns>
        //private List<string> ConvertAllClientObjToMessage()
        //{
        //    if (BaseClientIsOk()==false) return null;
        //    MES.WFServerLibrary.WorkFlowMessage wfMessage = null;
        //    List<string> clientInfo = null;
        //    if (Global.Manage_Config.GetMessageInstance(MES.BaseResources.Content.MsgType.SysClientInfo, ref wfMessage) == false) return null;
        //    lock(locker)
        //    {
        //        foreach(KeyValuePair<string, IMESConnectedClientCallback<IBroadcastCallBack>> item in clients)
        //        {
        //            if (clientInfo == null) clientInfo = new List<string>();
        //            if (string.IsNullOrEmpty(item.Key)) continue;
        //            if (item.Key.ToLower().StartsWith("sys")) continue;//throw away system client
        //            Global.MESBroadcastClientObj client = item.Value as Global.MESBroadcastClientObj;
        //            if (client!=null)
        //            {
        //                wfMessage.SetValue("ClientID",client.ClientId);
        //                wfMessage.SetValue("State", client.ClientState);
        //                wfMessage.SetValue("IpAddress", client.IpAddress);
        //                wfMessage.SetValue("Timestamp", DateTime.Now);
        //                wfMessage.SetValue("Online", true);
        //                clientInfo.Add(wfMessage.ToString());
        //            }
        //        }
        //    }
        //    wfMessage.Dispose();
        //    wfMessage = null;
        //    return clientInfo;
        //}

        /// <summary>
        /// convert single client object to message
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="online"></param>
        /// <returns></returns>
        //private string ConverSingleClientObjToMessage(string clientId,bool online = true)
        //{
        //    if (clientId.ToLower().StartsWith("sys")) return null;//throw away system client
        //    if (BaseClientIsOk() == false) return null;
        //    string clientInfo = null;
        //    MES.WFServerLibrary.WorkFlowMessage wfMessage = null;
        //    if (Global.Manage_Config.GetMessageInstance(MES.BaseResources.Content.MsgType.SysClientInfo, ref wfMessage) == false) return null;
        //    lock (locker)
        //    {
        //        if (base.clients.ContainsKey(clientId))
        //        {
        //            Global.MESBroadcastClientObj client = base.clients[clientId] as Global.MESBroadcastClientObj;
        //            wfMessage.SetValue("ClientID", client.ClientId);
        //            if (online == false)
        //            {
        //                wfMessage.SetValue("State", online);
        //            }
        //            else
        //            {
        //                wfMessage.SetValue("State", client.ClientState);
        //            }
                    
        //            wfMessage.SetValue("IpAddress", client.IpAddress);
        //            wfMessage.SetValue("Timestamp", DateTime.Now);
        //            wfMessage.SetValue("Online", online);
        //            clientInfo = wfMessage.ToString();
        //        }
        //    }
        //    return clientInfo;
        //}

        #endregion

        #region IMESService

        private static bool _isReady = false;

        public bool IsReady
        {
            get
            {
                return MESServiceBase._isReady;
            }
            protected set
            {
                MESServiceBase._isReady = value;
            }
        }

        public virtual bool Start()
        {
            try
            {
                if (serviceHost != null)
                {
                    serviceHost.Close();
                }
                ///overwrite
                StartDetectChannel();
            }
            catch (System.Exception ex)
            {
                return false;
            }

            return true;
        }

        public virtual bool Stop()
        {
            try
            {
                if (serviceHost != null)
                {
                    serviceHost.Abort();
                    serviceHost.Close();
                    serviceHost = null;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region IMESHeartBeat

        public virtual void HeartBeat(string clientID)
        {
            if (clients.Keys.Contains(clientID))
            {
                IMESConnectedClient client = clients[clientID];
                if (null != client)
                {
                    lock (locker)
                    {
                        client.LastHeartbeatUpdateTime = DateTime.Now;
                        client.ClientState = true;
                    }
                }
            }
        }

        #endregion

        #region IShakeHands

        // 客户端上线通知
        public virtual void Connect(string sessionId)
        {
            if(null == sessionId)
                sessionId = OperationContext.Current.SessionId;
            if (clients.Keys.Contains(sessionId))
                return;

            IMESConnectedClient client = ObtainOverrideConnectedClient(sessionId);
            if (null == client)
            {
                client = new MESConnectedClient()
                {
                    ServiceType = serviceType,
                    IpAddress = GetIncomingIPAddress(),
                    LastHeartbeatUpdateTime = DateTime.Now,
                    LastConnectTime = DateTime.Now,
                    ClientState = true,
                };
            }
            else
            {
                client.ServiceType = serviceType;
                client.IpAddress = GetIncomingIPAddress();
                client.LastHeartbeatUpdateTime = DateTime.Now;
                client.LastConnectTime = DateTime.Now;
                client.ClientState = true;
            }
            lock (locker)
            {
                clients.Add(sessionId, client);
            }
        }

        // 根据客户端需求更新客户端标识
        public virtual void UpdateID(string oldId, string newId)
        {
            if (oldId == newId)
                return;
            if (clients.Keys.Contains(oldId))
            {
                IMESConnectedClient client = clients[oldId];
                if (null != client)
                {
                    lock (locker)
                    {
                        clients.Remove(oldId);
                        clients.Add(newId, client);
                    }
                }
            }
        }

        // 客户端断线重连通知
        public virtual void ReConnect(string oldSessionId, string newSessionId)
        {
            //remove the old client and add new
            if (clients.Keys.Contains(oldSessionId))
            {
                IMESConnectedClient client = clients[oldSessionId];
                if (null != client)
                {
                    lock (locker)
                    {
                        clients.Remove(oldSessionId);
                        client.LastHeartbeatUpdateTime = DateTime.Now;
                        client.LastConnectTime = DateTime.Now;
                        client.ClientState = true;
                        clients.Add(newSessionId, client);
                    }
                }
            }
            else
            {
                Connect(newSessionId);
            }
        }

        // 客户端下线通知
        public virtual void DisConnect(string sessionId)
        {
            if (clients.ContainsKey(sessionId))
            {
                IMESConnectedClient client;
                if (clients.TryGetValue(sessionId, out client))
                {
                    lock (locker)
                    {
                        clients.Remove(sessionId);
                        if(client is IDisposable)
                        {
                            (client as IDisposable).Dispose();
                        }
                    }
                    //PMS.Libraries.ToolControls.PMSPublicInfo.Message.Info("Count:" + _HeartBeatTimerDic.Count.ToString());
                }
            }
        }

         #endregion

        //#region interface monitor
        //public List<string> MonitorGetClientInfo()
        //{
        //    try
        //    {
        //        return ConvertAllClientObjToMessage();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        if (logger != null) logger.Error(ex);
        //    }
        //    return null;
        //}
        //public int MonitorConfig(string ip, int port, string netModule)
        //{
        //    try
        //    {
        //        Global.CustomNLogInstance.Set(ip, port, netModule);
        //        return 1;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        if (logger != null) logger.Error(ex);
        //    }
        //    return -2;
        //}
        //public int MonitorStart()
        //{
        //    try
        //    {
        //        if (Global.CustomNLogInstance.Instance != null)
        //        {
        //            Global.CustomNLogInstance.Instance.Start();
        //            return 1;
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        if (logger != null) logger.Error(ex);
        //    }
        //    return -2;
        //}
        //public int MonitorStop()
        //{
        //    try
        //    {
        //        if (Global.CustomNLogInstance.Instance != null)
        //        {
        //            Global.CustomNLogInstance.Instance.Stop();
        //            return 1;
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        if (logger != null) logger.Error(ex);
        //    }
        //    return -2;
        //}
        //#endregion
    }

    public class MESServiceBase<TCallBack> : MESServiceBase where TCallBack: class
    {
        private static Dictionary<string, IMESConnectedClientCallback<TCallBack>> _clients = new Dictionary<string, IMESConnectedClientCallback<TCallBack>>();

        protected new virtual Dictionary<string, IMESConnectedClientCallback<TCallBack>> clients
        {
            get
            {
                return _clients;
            }
            set
            {
                _clients = value;
            }
        }

        #region user func

        /// <summary>
        /// 供服务端扩展客户端管理对象之用
        /// </summary>
        /// <returns>返回继承于MESConnectedClient<TCallBack>的对象</returns>
        protected new virtual IMESConnectedClientCallback<TCallBack> ObtainOverrideConnectedClient(string sessionId)
        {
            return null;
        }

        protected override List<string> GetDeadClientIDs()
        {
            List<string> deadClientIDs = new List<string>();
            foreach (KeyValuePair<string, IMESConnectedClientCallback<TCallBack>> pair in clients)
            {
                if (_StopDetect)
                    break;
                string clientID = pair.Key;
                IMESConnectedClient client = pair.Value;
                // 是缓存客户端
                if (client is IMESCacheClient)
                {
                    //且启用了缓存模式，不做自动死链接处理
                    if ((client as IMESCacheClient).EnableCache)
                        continue;
                }
                TimeSpan ts = DateTime.Now - client.LastHeartbeatUpdateTime;
                // 连续五次未收到心跳，将客户端状态设为false
                if (ts > new TimeSpan(0, 0, 50))
                {
                    client.ClientState = false;
                }
                if (ts > new TimeSpan(0, 10, 0))
                {
                    // 大于10分钟未发生心跳的视为Dead Channel，移除
                    deadClientIDs.Add(clientID);
                }
            }
            return deadClientIDs;
        }

        #endregion

        #region IMESHeartBeat

        public override void HeartBeat(string clientID)
        {
            if (clients.Keys.Contains(clientID))
            {
                IMESConnectedClient client = clients[clientID];
                if (null != client)
                {
                    lock (locker)
                    {
                        client.LastHeartbeatUpdateTime = DateTime.Now;
                        client.ClientState = true;
                    }
                }
            }
        }

        #endregion

        #region IShakeHands override

        // 客户端上线通知
        public override void Connect(string sessionId)
        {
            if (null == sessionId)
            {
                if(null != OperationContext.Current)
                    sessionId = OperationContext.Current.SessionId;
            }
            TCallBack callback = null;
            if (null != OperationContext.Current)
            {
                try
                {
                    object callbacktmp = OperationContext.Current.GetCallbackChannel<TCallBack>();
                    if (null != callbacktmp && callbacktmp is TCallBack)
                        callback = callbacktmp as TCallBack;
                }
                catch (System.Exception ex)
                {
                	
                }
            }
            IMESConnectedClientCallback<TCallBack> client = ObtainOverrideConnectedClient(sessionId);
            if (null == client)
            {
                client = new MESConnectedClient<TCallBack>()
                {
                    ServiceType = serviceType,
                    IpAddress = GetIncomingIPAddress(),
                    LastHeartbeatUpdateTime = DateTime.Now,
                    ClientState = true,
                    ICallBack = callback,
                };
            }
            else
            {
                client.ServiceType = serviceType;
                client.IpAddress = GetIncomingIPAddress();
                client.LastHeartbeatUpdateTime = DateTime.Now;
                client.ClientState = true;
                client.ICallBack = callback;
            }
            if (clients.ContainsKey(sessionId))
                DisConnect(sessionId);
            lock (locker)
            {
                clients.Add(sessionId, client);
            }
        }

        // 根据客户端需求更新客户端标识
        public override void UpdateID(string oldId, string newId)
        {
            if (oldId == newId)
                return;
            if (clients.Keys.Contains(oldId))
            {
                IMESConnectedClientCallback<TCallBack> client = clients[oldId];
                if (null != client)
                {
                    lock (locker)
                    {
                        clients.Remove(oldId);
                        if (clients.Keys.Contains(newId))
                            clients.Remove(newId);
                        clients.Add(newId, client);
                    }
                }
            }
        }

        // 客户端断线重连通知
        public override void ReConnect(string oldSessionId, string newSessionId)
        {
            //remove the old client and add new
            if (clients.Keys.Contains(oldSessionId))
            {
                IMESConnectedClientCallback<TCallBack> client = clients[oldSessionId];
                if (null != client)
                {
                    TCallBack callback =
                        OperationContext.Current.GetCallbackChannel<TCallBack>();
                    lock (locker)
                    {
                        clients.Remove(oldSessionId);
                        client.ICallBack = callback;
                        client.LastHeartbeatUpdateTime = DateTime.Now;
                        client.ClientState = true;
                        clients.Add(newSessionId, client);
                    }
                }
            }
            else
            {
                Connect(newSessionId);
            }
        }

        // 客户端下线通知
        public override void DisConnect(string sessionId)
        {
            if (clients.ContainsKey(sessionId))
            {
                IMESConnectedClientCallback<TCallBack> client;
                if (clients.TryGetValue(sessionId, out client))
                {
                    lock (locker)
                    {
                        clients.Remove(sessionId);
                        if (client is IDisposable)
                        {
                            (client as IDisposable).Dispose();
                        }
                    }
                    //PMS.Libraries.ToolControls.PMSPublicInfo.Message.Info("Count:" + _HeartBeatTimerDic.Count.ToString());
                }
            }
        }

        #endregion
    }

    public class MESCacheServiceBase : MESServiceBase, ISvcConfig
    {
        /// <summary>
        /// 供服务端扩展客户端管理对象之用
        /// </summary>
        /// <returns>返回继承于MESConnectedClient及IMESCacheClient的对象</returns>
        protected override IMESConnectedClient ObtainOverrideConnectedClient(string sessionId)
        {
            MESCacheConnectedClient client = new MESCacheConnectedClient();
            //client.EnableCache = true;
            return client;
        }

        public virtual void SetClientCacheMode(string sessionId, bool enableCache)
        {
            if (!clients.Keys.Contains(sessionId))
                return;

            IMESConnectedClient client = clients[sessionId];
            if (null != client && client is IMESCacheClient)
            {
                lock (locker)
                {
                    (client as IMESCacheClient).EnableCache = enableCache;
                }
            }
        }
    }

    public class MESCacheServiceBase<TCallBack> : MESServiceBase<TCallBack> where TCallBack : class
    {
        /// <summary>
        /// 供服务端扩展客户端管理对象之用
        /// </summary>
        /// <returns>返回继承于MESConnectedClient<TCallBack>及IMESCacheClient的对象</returns>
        protected override IMESConnectedClientCallback<TCallBack> ObtainOverrideConnectedClient(string sessionId)
        {
            MESCacheConnectedClient<TCallBack> client = new MESCacheConnectedClient<TCallBack>();
            //client.EnableCache = true;
            return client;
        }

        public virtual void SetClientCacheMode(string sessionId, bool enableCache)
        {
            if (!clients.Keys.Contains(sessionId))
                return;

            IMESConnectedClient client = clients[sessionId];
            if (null != client && client is IMESCacheClient)
            {
                lock (locker)
                {
                    (client as IMESCacheClient).EnableCache = enableCache;
                }
            }
        }
    }

    public class AsyncResult : IAsyncResult, IDisposable
    {
        AsyncCallback callback;
        object state;
        ManualResetEvent manualResentEvent;
        public Exception Exception { get; set; }

        public AsyncResult(AsyncCallback callback, object state)
        {
            this.callback = callback;
            this.state = state;
            this.manualResentEvent = new ManualResetEvent(false);
        }

        object IAsyncResult.AsyncState
        {
            get { return state; }
        }

        public ManualResetEvent AsyncWait
        {
            get
            {
                return manualResentEvent;
            }
        }

        WaitHandle IAsyncResult.AsyncWaitHandle
        {
            get { return this.AsyncWait; }
        }

        bool IAsyncResult.CompletedSynchronously
        {
            get { return false; }
        }

        bool IAsyncResult.IsCompleted
        {
            get { return manualResentEvent.WaitOne(0, false); }
        }

        public void Complete()
        {
            manualResentEvent.Set();
            if (callback != null)
                callback(this);
        }

        public void Dispose()
        {
            manualResentEvent.Close();
            manualResentEvent = null;
            state = null;
            callback = null;
        }
    }

    /// <summary>
    /// 发布至服务接口
    /// </summary>
    [ServiceContract]
    public interface IMESPublish
    {
        /// <summary>
        /// 同步发布文件至相关服务
        /// </summary>
        /// <param name="SPathList">服务端路径列表(相对路径)</param>
        /// <param name="CPathList">客户端路径列表(相对路径)</param>
        /// <returns>返回值成功失败</returns>
        [OperationContract]
        bool PublishFiles(List<string> SPathList, List<string> CPathList);

        /// <summary>
        /// 异步发布文件至相关服务
        /// </summary>
        /// <param name="SPathList">服务端路径列表(相对路径)</param>
        /// <param name="CPathList">客户端路径列表(相对路径)</param>
        /// <param name="userCallBack"></param>
        /// <param name="stateObject"></param>
        /// <returns></returns>
        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginPublishFiles(List<string> SPathList, List<string> CPathList, AsyncCallback userCallBack, object stateObject);
        bool EndPublishFiles(IAsyncResult asynResult);
    }
}
