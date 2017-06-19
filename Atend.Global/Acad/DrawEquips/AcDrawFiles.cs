using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;


namespace Atend.Global.Acad.DrawEquips
{

    public sealed class DicisionNativeApi
    {
        #region Import Superpro API from Superpro Dyanamic Library (sx32w.dll)

        [DllImport("sx32w.dll",
                CharSet = CharSet.Auto,
                EntryPoint = "RNBOsproFormatPacket")]
        public static extern ushort RNBOsproFormatPacket(/* IN */ byte[] packet,
            /* IN */ uint packetSize);
        [DllImport("sx32w.dll",
                CharSet = CharSet.Auto,
                EntryPoint = "RNBOsproInitialize")]
        public static extern ushort RNBOsproInitialize(/* IN */ byte[] packet);

        [DllImport("sx32w.dll",
                CharSet = CharSet.Auto,
                EntryPoint = "RNBOsproFindFirstUnit")]
        public static extern ushort RNBOsproFindFirstUnit(/* IN */ byte[] packet,
            /* IN */ ushort developerID);
        [DllImport("sx32w.dll",
                CharSet = CharSet.Auto,
                EntryPoint = "RNBOsproFindNextUnit")]
        public static extern ushort RNBOsproFindNextUnit(/* IN */ byte[] packet);

        [DllImport("sx32w.dll",
                CharSet = CharSet.Auto,
                EntryPoint = "RNBOsproRead")]
        public static extern ushort RNBOsproRead( /* IN  */ byte[] packet,
            /* IN  */ ushort address,
            /* OUT */ ref ushort data);

        [DllImport("sx32w.dll",
                CharSet = CharSet.Auto,
                EntryPoint = "RNBOsproExtendedRead")]
        public static extern ushort RNBOsproExtendedRead(/* IN  */  byte[] packet,
            /* IN  */  ushort address,
            /* OUT */  ref ushort data,
            /* OUT */  ref byte accessCode);

        [DllImport("sx32w.dll",
                CharSet = CharSet.Auto,
                EntryPoint = "RNBOsproWrite")]
        public static extern ushort RNBOsproWrite(/* IN */ byte[] packet,
            /* IN */ ushort writePassword,
            /* IN */ ushort address,
            /* IN */ ushort data,
            /* IN */ byte accessCode);

        [DllImport("sx32w.dll",
                CharSet = CharSet.Auto,
                EntryPoint = "RNBOsproOverwrite")]
        public static extern ushort RNBOsproOverwrite(/* IN */ byte[] packet,
            /* IN */ ushort writePassword,
            /* IN */ ushort overwritePassword1,
            /* IN */ ushort overwritePassword2,
            /* IN */ ushort address,
            /* IN */ ushort data,
            /* IN */ byte accessCode);

        [DllImport("sx32w.dll",
                CharSet = CharSet.Auto,
                EntryPoint = "RNBOsproDecrement")]
        public static extern ushort RNBOsproDecrement( /* IN */ byte[] packet,
            /* IN */ ushort writePassword,
            /* IN */ ushort address);

        [DllImport("sx32w.dll",
                CharSet = CharSet.Auto,
                EntryPoint = "RNBOsproActivate")]
        public static extern ushort RNBOsproActivate(/* IN */ byte[] packet,
            /* IN */ ushort writePassword,
            /* IN */ ushort activatePassword1,
            /* IN */ ushort activatePassword2,
                                                              ushort address);

        [DllImport("sx32w.dll",
                CharSet = CharSet.Auto,
                EntryPoint = "RNBOsproQuery")]
        public static extern ushort RNBOsproQuery(/* IN  */ byte[] packet,
            /* IN  */ ushort address,
            /* IN  */ byte[] queryData,
            /* OUT */ byte[] response,
            /* OUT */ ref uint response32,
            /* IN  */ ushort length);

        [DllImport("sx32w.dll",
                CharSet = CharSet.Auto,
                EntryPoint = "RNBOsproGetFullStatus")]
        public static extern ushort RNBOsproGetFullStatus( /* IN */byte[] packet);

        [DllImport("sx32w.dll",
                CharSet = CharSet.Auto,
                EntryPoint = "RNBOsproGetVersion")]
        public static extern ushort RNBOsproGetVersion(/* IN  */ byte[] packet,
            /* OUT */ ref byte majVerion,
            /* OUT */ ref byte minVersion,
            /* OUT */ ref byte revision,
            /* OUT */ ref byte osDrvrType);

        [DllImport("sx32w.dll",
                CharSet = CharSet.Auto,
                EntryPoint = "RNBOsproGetKeyType")]
        public static extern ushort RNBOsproGetKeyType(/* IN */  byte[] packet,
            /* OUT */ ref int keyFamily,
            /* OUT */ ref int formFactor,
            /* OUT */ ref int memUsed);

        [DllImport("sx32w.dll",
                CharSet = CharSet.Auto,
                EntryPoint = "RNBOsproCleanup")]
        public static extern void RNBOsproCleanup();

        [DllImport("sx32w.dll",
                CharSet = CharSet.Auto,
                EntryPoint = "RNBOsproGetHardLimit")]
        public static extern ushort RNBOsproGetHardLimit( /* IN  */ byte[] packet,
            /* OUT */ out ushort hardLimit);

        [DllImport("sx32w.dll",
                CharSet = CharSet.Auto,
                EntryPoint = "RNBOsproGetSubLicense")]
        public static extern ushort RNBOsproGetSubLicense(/* IN */ byte[] packet,
            /* IN */ ushort address);

        [DllImport("sx32w.dll",
                CharSet = CharSet.Auto,
                EntryPoint = "RNBOsproReleaseLicense")]
        public static extern ushort RNBOsproReleaseLicense(/* IN     */byte[] packet,
            /* IN     */ushort address,
            /* IN/OUT */ ref ushort numSubLic);

        [DllImport("sx32w.dll",
                CharSet = CharSet.Auto,
                EntryPoint = "RNBOsproSetContactServer")]
        public static extern ushort RNBOsproSetContactServer(/* IN */ byte[] packet,
            /* IN */ [MarshalAs(UnmanagedType.LPStr)] string serverName);

        [DllImport("sx32w.dll",
                CharSet = CharSet.Auto,
                EntryPoint = "RNBOsproGetContactServer")]
        public static extern ushort RNBOsproGetContactServer(/* IN  */ byte[] packet,
            /* OUT */ byte[] serverNameBuffer,
            /* IN  */ uint serverNameBufferSize);

        [DllImport("sx32w.dll",
                CharSet = CharSet.Auto,
                EntryPoint = "RNBOsproEnumServer")]
        public static extern ushort RNBOsproEnumServer(int enumFlag,
                                                         ushort developerID,
                                                         byte[] nsproServerInfo,
                                                         ref ushort numServerInfo);

        [DllImport("sx32w.dll",
                CharSet = CharSet.Auto,
                EntryPoint = "RNBOsproGetKeyInfo")]
        public static extern ushort RNBOsproGetKeyInfo(/* IN */  byte[] packet,
            /* IN */  ushort developerID,
            /* IN */  ushort keyIndex,
            /* OUT */ byte[] nsproMonitorInfo);

        [DllImport("sx32w.dll",
                CharSet = CharSet.Auto,
                EntryPoint = "RNBOsproSetProtocol")]
        public static extern ushort RNBOsproSetProtocol(/* IN */ byte[] packet,
            /* IN */ int protocol);

        [DllImport("sx32w.dll",
                CharSet = CharSet.Auto,
                EntryPoint = "RNBOsproSetHeartBeat")]
        public static extern ushort RNBOsproSetHeartBeat(/* IN */ byte[] packet,
            /* IN */ int heartBeatValue);

        [DllImport("sx32w.dll",
                CharSet = CharSet.Auto,
                EntryPoint = "RNBOsproSetSharedLicense")]
        public static extern ushort RNBOsproSetSharedLicense(/* IN */ byte[] packet,
            /* IN */ ushort shareMainLic,
            /* IN */ ushort shareSubLic);

        [DllImport("sx32w.dll",
                CharSet = CharSet.Auto,
                EntryPoint = "RNBOsproCheckTerminalService")]
        public static extern ushort RNBOsproCheckTerminalService(/* IN */ byte[] packet,
            /* IN */ int termserv);
        #endregion
    }

    public class Dicisionpro : IDisposable
    {
        #region Constructor / Destructor
        /* Define API packet for communication*/
        public static uint SPRO_APIPACKET_SIZE = 1028;
        private byte[] packet;
        private bool disposed = false;

        private string strError = "Unable to load the required SuperPro Library(sx32w.dll).\n" +
                                  "Either the library is missing or corrupted.";

        public Dicisionpro()
        {
            packet = new byte[SPRO_APIPACKET_SIZE];
        }
        ~Dicisionpro()
        {
            Dispose(false);
        }

        /* Implement IDisposable */
        public void Dispose()
        {
            Dispose(true);
        }
        /* Private class specifically for this object.*/
        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.packet = null;
                    /* only MANAGED resource should be dispose     */
                    /* here that implement the IDisposable         */
                    /* Interface                                    */
                }
                /* only UNMANAGED resource should be dispose here */
                /*                                                */
            }
            disposed = true;
        }
        #endregion

        #region Dicisionpro API Error Codes
        /* Dicisionpro API Error codes */

        public const int SP_SUCCESS = 0;
        public const int SP_INVALID_FUNCTION_CODE = 1;
        public const int SP_INVALID_PACKET = 2;
        public const int SP_UNIT_NOT_FOUND = 3;
        public const int SP_ACCESS_DENIED = 4;
        public const int SP_INVALID_MEMORY_ADDRESS = 5;
        public const int SP_INVALID_ACCESS_CODE = 6;
        public const int SP_PORT_IS_BUSY = 7;
        public const int SP_WRITE_NOT_READY = 8;
        public const int SP_NO_PORT_FOUND = 9;
        public const int SP_ALREADY_ZERO = 10;
        public const int SP_DRIVER_OPEN_ERROR = 11;
        public const int SP_DRIVER_NOT_INSTALLED = 12;
        public const int SP_IO_COMMUNICATIONS_ERROR = 13;
        public const int SP_PACKET_TOO_SMALL = 15;
        public const int SP_INVALID_PARAMETER = 16;
        public const int SP_MEM_ACCESS_ERROR = 17;
        public const int SP_VERSION_NOT_SUPPORTED = 18;
        public const int SP_OS_NOT_SUPPORTED = 19;
        public const int SP_QUERY_TOO_LONG = 20;
        public const int SP_INVALID_COMMAND = 21;
        public const int SP_MEM_ALIGNMENT_ERROR = 29;
        public const int SP_DRIVER_IS_BUSY = 30;
        public const int SP_PORT_ALLOCATION_FAILURE = 31;
        public const int SP_PORT_RELEASE_FAILURE = 32;
        public const int SP_ACQUIRE_PORT_TIMEOUT = 39;
        public const int SP_SIGNAL_NOT_SUPPORTED = 42;
        public const int SP_UNKNOWN_MACHINE = 44;
        public const int SP_SYS_API_ERROR = 45;
        public const int SP_UNIT_IS_BUSY = 46;
        public const int SP_INVALID_PORT_TYPE = 47;
        public const int SP_INVALID_MACH_TYPE = 48;
        public const int SP_INVALID_IRQ_MASK = 49;
        public const int SP_INVALID_CONT_METHOD = 50;
        public const int SP_INVALID_PORT_FLAGS = 51;
        public const int SP_INVALID_LOG_PORT_CFG = 52;
        public const int SP_INVALID_OS_TYPE = 53;
        public const int SP_INVALID_LOG_PORT_NUM = 54;
        public const int SP_INVALID_ROUTER_FLGS = 56;
        public const int SP_INIT_NOT_CALLED = 57;
        public const int SP_DRVR_TYPE_NOT_SUPPORTED = 58;
        public const int SP_FAIL_ON_DRIVER_COMM = 59;

        /* Networking Error Codes */

        public const int SP_SERVER_PROBABLY_NOT_UP = 60;
        public const int SP_UNKNOWN_HOST = 61;
        public const int SP_SENDTO_FAILED = 62;
        public const int SP_SOCKET_CREATION_FAILED = 63;
        public const int SP_NORESOURCES = 64;
        public const int SP_BROADCAST_NOT_SUPPORTED = 65;
        public const int SP_BAD_SERVER_MESSAGE = 66;
        public const int SP_NO_SERVER_RUNNING = 67;
        public const int SP_NO_NETWORK = 68;
        public const int SP_NO_SERVER_RESPONSE = 69;
        public const int SP_NO_LICENSE_AVAILABLE = 70;
        public const int SP_INVALID_LICENSE = 71;
        public const int SP_INVALID_OPERATION = 72;
        public const int SP_BUFFER_TOO_SMALL = 73;
        public const int SP_INTERNAL_ERROR = 74;
        public const int SP_PACKET_ALREADY_INITIALIZED = 75;
        public const int SP_PROTOCOL_NOT_INSTALLED = 76;


        public const int SP_NO_LEASE_FEATURE = 101;
        public const int SP_LEASE_EXPIRED = 102;
        public const int SP_COUNTER_LIMIT_REACHED = 103;
        public const int SP_NO_DIGITAL_SIGNATURE = 104;
        public const int SP_SYS_FILE_CORRUPTED = 105;
        public const int SP_STRING_BUFFER_TOO_LONG = 106;

        /* Shell Error Codes */

        public const int SH_SP_BAD_ALGO = 401;
        public const int SH_SP_LONG_MSG = 402;
        public const int SH_SP_READ_ERROR = 403;
        public const int SH_SP_NOT_ENOUGH_MEMORY = 404;
        public const int SH_SP_CANNOT_OPEN = 405;
        public const int SH_SP_WRITE_ERROR = 406;
        public const int SH_SP_CANNOT_OVERWRITE = 407;
        public const int SH_SP_INVALID_HEADER = 408;
        public const int SH_SP_TMP_CREATE_ERROR = 409;
        public const int SH_SP_PATH_NOT_THERE = 410;
        public const int SH_SP_BAD_FILE_INFO = 411;
        public const int SH_SP_NOT_WIN32_FILE = 412;
        public const int SH_SP_INVALID_MACHINE = 413;
        public const int SH_SP_INVALID_SECTION = 414;
        public const int SH_SP_INVALID_RELOC = 415;
        public const int SH_SP_CRYPT_ERROR = 416;
        public const int SH_SP_SMARTHEAP_ERROR = 417;
        public const int SH_SP_IMPORT_OVERWRITE_ERROR = 418;
        public const int SH_SP_NO_PESHELL = 420;
        public const int SH_SP_FRAMEWORK_REQUIRED = 421;
        public const int SH_SP_CANNOT_HANDLE_FILE = 422;
        public const int SH_SP_IMPORT_DLL_ERROR = 423;
        public const int SH_SP_IMPORT_FUNCTION_ERROR = 424;
        public const int SH_SP_X64_SHELL_ENGINE = 425;
        public const int SH_SP_STRONG_NAME = 426;
        public const int SH_SP_FRAMEWORK_10 = 427;
        public const int SH_SP_FRAMEWORK_SDK_10 = 428;
        public const int SH_SP_FRAMEWORK_11 = 429;
        public const int SH_SP_FRAMEWORK_SDK_11 = 430;
        public const int SH_SP_FRAMEWORK_20_OR_30 = 431;
        public const int SH_SP_FRAMEWORK_SDK_20 = 432;
        public const int SH_SP_APP_NOT_SUPPORTED = 433;
        public const int SH_SP_FILE_COPY = 434;
        public const int SH_SP_HEADER_SIZE_EXCEED = 435;

        #endregion

        #region Dicisionpro Constants values used by client application.
        /* Set Protocol Flags */
        public enum PROTOCOL_FLAG
        {
            NSPRO_TCP_PROTOCOL = 1,
            NSPRO_IPX_PROTOCOL = 2,
            NSPRO_NETBEUI_PROTOCOL = 4,
            NSPRO_SAP_PROTOCOL = 8
        }

        /* Enum server falgs */
        public enum ENUM_SERVER_FLAG
        {
            NSPRO_RET_ON_FIRST = 1,
            NSPRO_GET_ALL_SERVERS = 2,
            NSPRO_RET_ON_FIRST_AVAILABLE = 4
        }

        /* Key Monitoring Information */
        public class NSPRO_KEY_MONITOR_INFO
        {
            public ushort developerId;
            public ushort hardLimit;
            public ushort inUse;
            public ushort numTimeOut;
            public ushort highestUse;
        }

        /* Monitoring Information */
        public class NSPRO_MONITOR_INFO
        {
            public byte[] serverName;
            public byte[] serverIPAddress;
            public byte[] serverIPXAddress;
            public byte[] version;
            public ushort protocol;
            public NSPRO_KEY_MONITOR_INFO sproKeyMonitorInfo;

            public NSPRO_MONITOR_INFO()
            {
                this.serverName = new byte[Dicisionpro.MAX_NAME_LEN];
                this.serverIPAddress = new byte[Dicisionpro.MAX_ADDR_LEN];
                this.serverIPXAddress = new byte[Dicisionpro.MAX_ADDR_LEN];
                this.version = new byte[Dicisionpro.MAX_NAME_LEN];
                this.protocol = 0;
                this.sproKeyMonitorInfo = new NSPRO_KEY_MONITOR_INFO();
                this.sproKeyMonitorInfo.developerId = 0;
                this.sproKeyMonitorInfo.hardLimit = 0;
                this.sproKeyMonitorInfo.inUse = 0;
                this.sproKeyMonitorInfo.numTimeOut = 0;
                this.sproKeyMonitorInfo.highestUse = 0;
            }
            public void GetKeyInfo(byte[] bBuffer)
            {
                int index;
                int j = 0;
                int offset = 0;

                for (index = 0; index < Dicisionpro.MAX_NAME_LEN; index++)
                    this.serverName[j++] = bBuffer[index];
                offset = index;
                j = 0;
                for (index = offset; index < (Dicisionpro.MAX_ADDR_LEN + offset); index++)
                    this.serverIPAddress[j++] = bBuffer[index];

                offset = index;
                j = 0;
                for (index = offset; index < (Dicisionpro.MAX_ADDR_LEN + offset); index++)
                    this.serverIPXAddress[j++] = bBuffer[index];

                offset = index;
                j = 0;
                for (index = offset; index < (Dicisionpro.MAX_NAME_LEN + offset); index++)
                    this.version[j++] = bBuffer[index];

                this.protocol = (ushort)BitConverter.ToInt16(bBuffer, index);
                this.sproKeyMonitorInfo.developerId = (ushort)BitConverter.ToUInt16(bBuffer, index + 2);
                this.sproKeyMonitorInfo.hardLimit = (ushort)BitConverter.ToUInt16(bBuffer, index + 4);
                this.sproKeyMonitorInfo.inUse = (ushort)BitConverter.ToUInt16(bBuffer, index + 6);
                this.sproKeyMonitorInfo.numTimeOut = (ushort)BitConverter.ToUInt16(bBuffer, index + 8);
                this.sproKeyMonitorInfo.highestUse = (ushort)BitConverter.ToUInt16(bBuffer, index + 10);
            }
        }

        // Class for ServerInfo
        public class NSPRO_SERVER_INFO
        {
            public byte[] serverAddress;
            public ushort numLicAvail;

            public NSPRO_SERVER_INFO()
            {
                serverAddress = new byte[Dicisionpro.MAX_ADDR_LEN];
                numLicAvail = 0;
            }

            public void GetServerInfo(byte[] bBuffer, int offset)
            {
                int j = 0;
                for (int i = offset; i < (Dicisionpro.MAX_ADDR_LEN + offset); i++)
                    serverAddress[j++] = bBuffer[i];
                this.numLicAvail = (ushort)BitConverter.ToUInt16(bBuffer, Dicisionpro.MAX_ADDR_LEN + offset);
            }
        }

        public const string Message1 = "قفل را به سیستم متصل نمایید";

        /* OS Types */
        public const int RB_MIN_OS_TYPE = 0;
        public const int RB_AUTODETECT_OS_TYPE = 0;    // Autodetect OS type         
        public const int RB_OS_DOS = 1;    // DOS operating system       
        public const int RB_OS_RSRV1 = 2;    // reserved                   
        public const int RB_OS_RSRV2 = 3;    // reserved                   
        public const int RB_OS_WIN3x = 4;    // Windows 3.x operating env  
        public const int RB_OS_WINNT = 5;    // Windows NT operating system
        public const int RB_OS_OS2 = 6;    // OS/2 operating system      
        public const int RB_OS_WIN95 = 7;    // Windows 95 operating system
        public const int RB_OS_WIN32s = 8;    // Windows WIN32s env         
        public const int RB_OS_NW = 9;    // Netware operating system   
        public const int RB_OS_QNX = 10;
        public const int RB_OS_LINUX = 12;   // Linux operating system     
        public const int RB_MAX_OS_TYPE = 12;

        // String for OS Driver type                                                         
        public const int RB_DOSRM_LOCAL_DRVR = 1;    //  DOS Real Mode local driver 
        public const int RB_WIN3x_LOCAL_DRVR = 2;    // Windows 3.x local driver    
        public const int RB_WIN32s_LOCAL_DRVR = 3;    // Win32s local driver         
        public const int RB_WIN3x_SYS_DRVR = 4;    // Windows 3.x system driver   
        public const int RB_WINNT_SYS_DRVR = 5;    // Windows NT system driver    
        public const int RB_OS2_SYS_DRVR = 6;    // OS/2 system driver                   
        public const int RB_WIN95_SYS_DRVR = 7;    // Windows 95 system driver    
        public const int RB_NW_LOCAL_DRVR = 8;    // Netware local driver        
        public const int RB_QNX_LOCAL_DRVR = 9;    // QNX local driver            
        public const int RB_UNIX_SYS_DRVR = 10;   // UNIX local driver           
        public const int RB_SOLARIS_SYS_DRVR = 11;   // SOLARIS local driver        
        public const int RB_LINUX_SYS_DRVR = 12;   // Linux system driver         
        public const int RB_LINUX_LOCAL_DRVR = 13;   // Linux local driver          
        public const int RB_AIX_SYS_DRVR = 14;   // AIX system driver           
        public const int RB_UNIXWARE_SYS_DRVR = 15;   // UNIX system  driver         

        /* Making the License Update Time programmable*/
        public const int LIC_UPDATE_INT = 120;     // Default heartbeat - 2*60 = 2 min.;
        public const int MAX_HEARTBEAT = 2592000; // 30*24*60*60 seconds 
        public const int MIN_HEARTBEAT = 60;      // 60 seconds 
        public const int INFINITE_HEARTBEAT = -1;

        /*Communication Modes */
        public const string RNBO_STANDALONE = "RNBO_STANDALONE";
        public const string RNBO_SPN_LOCAL = "RNBO_SPN_LOCAL";
        public const string RNBO_SPN_DRIVER = "RNBO_SPN_DRIVER";
        public const string RNBO_SPN_BROADCAST = "RNBO_SPN_BROADCAST";
        public const string RNBO_SPN_ALL_MODES = "RNBO_SPN_ALL_MODES";
        public const string RNBO_SPN_SERVER_MODES = "RNBO_SPN_SERVER_MODES";

        /* Sharing Flags */
        public const int SP_SHARE_USERNAME = 1;
        public const int SP_SHARE_MAC_ADDRESS = 2;
        public const int SP_SHARE_DEFAULT = 3;

        /* Dicisionpro 6.4 Sharing Flags */
        public const int SP_DISABLE_MAINLIC_SHARING = 0;
        public const int SP_ENABLE_MAINLIC_SHARING = 1;

        public const int SP_DISABLE_SUBLIC_SHARING = 0;
        public const int SP_ENABLE_SUBLIC_SHARING = 1;


        /* Key Type Constants */
        public const int SP_KEY_FORM_FACTOR_PARALLEL = 0;
        public const int SP_KEY_FORM_FACTOR_USB = 1;

        public const int SP_SUPERPRO_FAMILY_KEY = 0;
        public const int SP_ULTRAPRO_FAMILY_KEY = 1;
        public const int SP_UNKNOWN_FAMILY_KEY = 16;

        /* Maximum number of devices */
        public const int MAX_NUM_DEV = 10;

        /* Maximum host name length */
        public const int MAX_NAME_LEN = 64;

        /* Maximum host address length */
        public const int MAX_ADDR_LEN = 32;

        public const int SPRO_MAX_QUERY_SIZE = 56; //in bytes

        /* SuperPro 6.5 Terminal Client Check Flags */
        public const int SP_DISALLOW_CHECK_TERMINAL_CLIENT = 0;
        public const int SP_ALLOW_CHECK_TERMINAL_CLIENT = 1;


        #endregion

        #region //////////////////////// BEGIN PUBLIC METHODS   ///////////////////////////////////


        public ushort RNBOsproFormatPacket(uint packetSize)
        {
            ushort status = 0;

            try
            {
                status = DicisionNativeApi.RNBOsproFormatPacket(this.packet, packetSize);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            return status;
        }

        public ushort RNBOsproInitialize()
        {
            ushort status = 0;

            try
            {
                status = DicisionNativeApi.RNBOsproInitialize(this.packet);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            return status;
        }

        public ushort RNBOsproFindFirstUnit(ushort developerID)
        {
            ushort status = 0;


            try
            {
                status = DicisionNativeApi.RNBOsproFindFirstUnit(this.packet, developerID);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            return status;
        }

        public ushort RNBOsproFindNextUnit()
        {
            ushort status = 0;

            try
            {
                status = DicisionNativeApi.RNBOsproFindNextUnit(this.packet);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            return status;
        }


        public ushort RNBOsproRead(ushort address, ref ushort data)
        {
            ushort status = 0;

            try
            {
                status = DicisionNativeApi.RNBOsproRead(this.packet, address, ref data);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            return status;
        }

        public ushort RNBOsproExtendedRead(ushort address, ref ushort data, ref byte accessCode)
        {
            ushort status = 0;

            try
            {
                status = DicisionNativeApi.RNBOsproExtendedRead(this.packet, address, ref data, ref accessCode);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            return status;
        }

        public ushort RNBOsproWrite(ushort writePassword, ushort address, ref ushort data, ref byte accessCode)
        {
            ushort status = 0;

            try
            {
                status = DicisionNativeApi.RNBOsproWrite(this.packet, writePassword, address, data, accessCode);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            return status;
        }

        public ushort RNBOsproOverwrite(ushort writePassword, ushort overwritePassword1, ushort overwritePassword2, ushort address, ref ushort data, ref byte accessCode)
        {
            ushort status = 0;

            try
            {
                status = DicisionNativeApi.RNBOsproOverwrite(this.packet, writePassword, overwritePassword1, overwritePassword2, address, data, accessCode);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            return status;
        }

        public ushort RNBOsproDecrement(ushort writePassword, ushort address)
        {
            ushort status = 0;

            try
            {
                status = DicisionNativeApi.RNBOsproDecrement(this.packet, writePassword, address);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            return status;
        }

        public ushort RNBOsproActivate(ushort writePassword, ushort activatePassword1, ushort activatePassword2, ushort address)
        {
            ushort status = 0;

            try
            {
                status = DicisionNativeApi.RNBOsproActivate(this.packet, writePassword, activatePassword1, activatePassword2, address);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            return status;
        }

        public ushort RNBOsproQuery(ushort address, byte[] queryData, byte[] response, ref uint response32, ushort length)
        {
            ushort status = 0;

            try
            {
                status = DicisionNativeApi.RNBOsproQuery(this.packet, address, queryData, response, ref response32, length);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            return status;
        }

        public ushort RNBOsproGetFullStatus()
        {
            ushort status = 0;

            try
            {
                status = DicisionNativeApi.RNBOsproGetFullStatus(this.packet);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            return status;
        }

        public ushort RNBOsproGetVersion(ref byte majVerion, ref byte minVersion, ref byte revision, ref byte osDrvrType)
        {
            ushort status = 0;

            try
            {
                status = DicisionNativeApi.RNBOsproGetVersion(this.packet, ref majVerion, ref minVersion, ref revision, ref osDrvrType);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            return status;
        }

        public ushort RNBOsproGetKeyType(ref int keyFamily, ref int formFactor, ref int memUsed)
        {
            ushort status = 0;

            try
            {
                status = DicisionNativeApi.RNBOsproGetKeyType(this.packet, ref keyFamily, ref formFactor, ref memUsed);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            return status;
        }

        public void RNBOsproCleanup()
        {
            try
            {
                DicisionNativeApi.RNBOsproCleanup();
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            return;
        }

        public ushort RNBOsproGetHardLimit(out ushort hardLimit)
        {
            ushort status = 0;
            hardLimit = 0;

            try
            {
                status = DicisionNativeApi.RNBOsproGetHardLimit(this.packet, out hardLimit);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            return status;
        }

        public ushort RNBOsproGetSubLicense(ushort address)
        {
            ushort status = 0;

            try
            {
                status = DicisionNativeApi.RNBOsproGetSubLicense(this.packet, address);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            return status;
        }

        public ushort RNBOsproReleaseLicense(ushort address, ref ushort numSubLic)
        {
            ushort status = 0;

            try
            {
                status = DicisionNativeApi.RNBOsproReleaseLicense(this.packet, address, ref numSubLic);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            return status;
        }

        public ushort RNBOsproSetContactServer(string serverName)
        {
            ushort status = 0;

            try
            {
                status = DicisionNativeApi.RNBOsproSetContactServer(this.packet, serverName);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            return status;
        }

        public ushort RNBOsproGetContactServer(byte[] serverNameBuffer, uint serverNameBufferSize)
        {
            ushort status = 0;

            try
            {
                status = DicisionNativeApi.RNBOsproGetContactServer(this.packet, serverNameBuffer, serverNameBufferSize);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            return status;
        }

        public ushort RNBOsproEnumServer(int enumFlag, ushort developerID, byte[] nsproServerInfo, ref ushort numServerInfo)
        {
            ushort status = 0;

            try
            {
                status = DicisionNativeApi.RNBOsproEnumServer(enumFlag, developerID, nsproServerInfo, ref numServerInfo);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            return status;
        }

        public ushort RNBOsproGetKeyInfo(ushort developerID, ushort keyIndex, byte[] nsproMonitorInfo)
        {
            ushort status = 0;

            try
            {
                status = DicisionNativeApi.RNBOsproGetKeyInfo(this.packet, developerID, keyIndex, nsproMonitorInfo);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            return status;
        }

        public ushort RNBOsproSetProtocol(int protocol)
        {
            ushort status = 0;

            try
            {
                status = DicisionNativeApi.RNBOsproSetProtocol(this.packet, protocol);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            return status;
        }

        public ushort RNBOsproSetHeartBeat(int heartBeatValue)
        {
            ushort status = 0;

            try
            {
                status = DicisionNativeApi.RNBOsproSetHeartBeat(this.packet, heartBeatValue);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            return status;
        }

        public ushort RNBOsproSetSharedLicense(ushort shareMainLic, ushort shareSubLic)
        {
            ushort status = 0;

            try
            {
                status = DicisionNativeApi.RNBOsproSetSharedLicense(this.packet, shareMainLic, shareSubLic);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            return status;
        }

        public ushort RNBOsproCheckTerminalService(int termserv)
        {
            ushort status = 0;

            try
            {
                status = DicisionNativeApi.RNBOsproCheckTerminalService(this.packet, termserv);
            }
            catch (System.DllNotFoundException)
            {
                throw new System.DllNotFoundException(strError);
            }
            return status;
        }
        #endregion
    }

    public class Dicision
    {

        static byte[] packet = new byte[Dicisionpro.SPRO_APIPACKET_SIZE];
        static ushort status;

        private static int readHexData(string thisString, ref ushort OutData)
        {
            int i = 0;
            char[] singleChar = new char[1];

            //Loop for all character in string
            while (i < thisString.Length)
            {
                singleChar = thisString.ToCharArray(i, 1);

                //Check for valid hex character
                // if not a valid hex character then rerurn -1
                if ((((int)singleChar[0] < '0') || ((int)singleChar[0] > '9')) && (((int)singleChar[0] < 'A') || ((int)singleChar[0] > 'F') && ((int)singleChar[0] < 'a') || ((int)singleChar[0] > 'f')))
                    return -1;
                i++;
            }
            //If valid hex string then convert into integer
            try
            {
                OutData = Convert.ToUInt16(thisString, 16);
            }
            catch
            {
                //If exception occured then return -1
                return -1;
            }
            //If valid hex data then return 0   
            return 0;
        }

        private static int isValidData(string thisString, ushort length, ref ushort OutData)
        {
            if (thisString.Length <= 0)
                return 1;
            if (thisString.Length > length)
                return 2;
            if (readHexData(thisString, ref OutData) == -1)
                return 3;
            return 0;
        }

        public static bool IsHere()
        {
            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //const ushort VALIDDATA_LENGTH = 4;
            //bool Answer = false;
            ////byte[] packet = new byte[Dicisionpro.SPRO_APIPACKET_SIZE];
            ////ed.WriteMessage("  ##  ##  H-1 \n");
            //if (DicisionNativeApi.RNBOsproFormatPacket(packet, Dicisionpro.SPRO_APIPACKET_SIZE) == 0)
            //{
            //    //ed.WriteMessage("  ##  ##  H0 \n");
            //    if (DicisionNativeApi.RNBOsproInitialize(packet) == 0)
            //    {
            //        //ed.WriteMessage("  ##  ##  H1 \n");
            //        string sDevID = "1c6d";// string variable for DevID
            //        ushort DevID = 0;
            //        isValidData(sDevID.Trim(), VALIDDATA_LENGTH, ref DevID);
            //        ushort result = DicisionNativeApi.RNBOsproFindFirstUnit(packet, DevID);
            //        if (result == 0)
            //        {
            //            //ed.WriteMessage("  ##  ##  H2 \n");
            //            //ed.WriteMessage("HERE FOUND \n");
            //            Answer = true;
            //        }
            //    }
            //}

            //return Answer;

            return true;
        }

        public static bool IsThere()
        {
            Autodesk.AutoCAD.EditorInput.Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            //const ushort VALIDDATA_LENGTH = 4;
            //bool Answer = false;

            ////---- check terminal 
            //int allowCheck;
            //allowCheck = Dicisionpro.SP_ALLOW_CHECK_TERMINAL_CLIENT;

            ////if (TerminalClientCheck.Checked == true)
            //if (true)
            //{
            //    allowCheck = Dicisionpro.SP_ALLOW_CHECK_TERMINAL_CLIENT;
            //}
            ////ed.WriteMessage("  ##  ##  0 \n");
            ////if (TerminalClientCheck.Checked == false)
            ////{
            ////    allowCheck = Dicisionpro.SP_DISALLOW_CHECK_TERMINAL_CLIENT;
            ////}

            ////----------------


            ////------ set shered licence -----
            //ushort shareMainLic, shareSubLic;
            ////ushort status;
            //shareMainLic = Dicisionpro.SP_DISABLE_MAINLIC_SHARING;
            //shareSubLic = Dicisionpro.SP_DISABLE_SUBLIC_SHARING;

            ////-------------------------------

            //if (DicisionNativeApi.RNBOsproFormatPacket(packet, Dicisionpro.SPRO_APIPACKET_SIZE) == 0)
            //{
            //    //ed.WriteMessage("  ##  ##  1 \n");
            //    if (DicisionNativeApi.RNBOsproInitialize(packet) == 0)
            //    {
            //        //ed.WriteMessage("  ##  ##  2 \n");
            //        if (DicisionNativeApi.RNBOsproSetProtocol(packet, (int)Dicisionpro.PROTOCOL_FLAG.NSPRO_TCP_PROTOCOL) == 0)
            //        {
            //            //ed.WriteMessage("  ##  ##  3 \n");
            //            status = RNBOsproCheckTerminalService(allowCheck);
            //            //If API successfull
            //            if (status == 0)
            //            {
            //                //ed.WriteMessage("  ##  ##  4 \n");
            //                //Clear all the input text box
            //                //clearTextBox_succ("CheckTerminalService");
            //                string ServerName = Atend.Control.ConnectionString.LST;
            //                if (DicisionNativeApi.RNBOsproSetContactServer(packet, ServerName) == 0)
            //                {
            //                    //ed.WriteMessage("  ##  ##  5 \n");
            //                    string sDevID = "1c6d";// string variable for DevID
            //                    ushort DevID = 0;
            //                    isValidData(sDevID.Trim(), VALIDDATA_LENGTH, ref DevID);
            //                    ushort result = DicisionNativeApi.RNBOsproFindFirstUnit(packet, DevID);
            //                    if (result == 0)
            //                    {
            //                        //ed.WriteMessage("  ##  ##  6 \n");
            //                        //if (MainSharing.Checked != false)
            //                        //if (true)
            //                        //{
            //                        //if (MainSharing.Checked == true && SubLicSharing.Checked == true)
            //                        //if (true && true)
            //                        //{
            //                        //    shareMainLic = Superpro.SP_ENABLE_MAINLIC_SHARING;
            //                        //    shareSubLic = Superpro.SP_ENABLE_SUBLIC_SHARING;
            //                        //}
            //                        //if (MainSharing.Checked == true && SubLicSharing.Checked == false)
            //                        //////////////////if (true)
            //                        //////////////////{
            //                        //////////////////    shareMainLic = Dicisionpro.SP_ENABLE_MAINLIC_SHARING;
            //                        //////////////////    shareSubLic = Dicisionpro.SP_DISABLE_SUBLIC_SHARING;
            //                        //////////////////}
            //                        //}
            //                        //**************RNBOsproSetSharedLicense API **********************
            //                        //////////////////////status = RNBOsproSetSharedLicense(shareMainLic, shareSubLic);
            //                        ////////////////////////If API successfull
            //                        //////////////////////if (status == 0)
            //                        //////////////////////{
            //                        //Clear all the input text box
            //                        //clearTextBox_succ("SetSharedLicense");

            //                        ////////////sDevID = "1c6d";// string variable for DevID
            //                        ////////////DevID = 0;
            //                        ////////////isValidData(sDevID.Trim(), VALIDDATA_LENGTH, ref DevID);
            //                        ////////////result = DicisionNativeApi.RNBOsproFindFirstUnit(packet, DevID);
            //                        ////////////if (result == 0)
            //                        ////////////{

            //                        //ed.WriteMessage("THERE FOUND \n");
            //                        Answer = true;
            //                        ////////////}

            //                        ////////////////////}
            //                        ////////////////////else //If API failed then
            //                        ////////////////////{
            //                        ////////////////////    //Display API failure message with API return code.
            //                        ////////////////////    //clearTextBox_fail("SetSharedLicense", status);
            //                        ////////////////////    //RNBOsproSetSharedLicense
            //                        ////////////////////    ed.WriteMessage("SuperproNativeApi.RNBOsproSetSharedLicense failed \n");
            //                        ////////////////////}

            //                    }
            //                    else
            //                    {
            //                        //RNBOsproFindFirstUnit
            //                        ed.WriteMessage("SuperproNativeApi.RNBOsproFindFirstUnit failed \n");
            //                    }
            //                }
            //                else
            //                {
            //                    ed.WriteMessage("SuperproNativeApi.RNBOsproSetContactServer failed \n");
            //                }
            //            }
            //            else //If API failed then
            //            {
            //                //Display API failure message with API return code.
            //                //clearTextBox_fail("CheckTerminalService", status);
            //                ed.WriteMessage("DicisionNativeApi.RNBOsproCheckTerminalService failed \n");
            //            }
            //        }
            //        else
            //        {
            //            ed.WriteMessage("DicisionNativeApi.RNBOsproSetProtocol failed \n");
            //        }
            //    }
            //    else
            //    {
            //        ed.WriteMessage("DicisionNativeApi.RNBOsproInitialize failed \n");
            //    }
            //}
            //else
            //{
            //    ed.WriteMessage("DicisionNativeApi.RNBOsproFormatPacket failed \n");
            //}
            //return Answer;
            return true;
        }

        static ushort RNBOsproCheckTerminalService(int termserv)
        {
            ushort status = 0;

            try
            {
                status = DicisionNativeApi.RNBOsproCheckTerminalService(packet, termserv);
            }
            catch (System.DllNotFoundException)
            {
                //throw new System.DllNotFoundException(strError);
            }
            return status;
        }

        static ushort RNBOsproSetSharedLicense(ushort shareMainLic, ushort shareSubLic)
        {
            ushort status = 0;

            try
            {
                status = DicisionNativeApi.RNBOsproSetSharedLicense(packet, shareMainLic, shareSubLic);
            }
            catch (System.DllNotFoundException)
            {
                //throw new System.DllNotFoundException(strError);
            }
            return status;
        }




    }

}
