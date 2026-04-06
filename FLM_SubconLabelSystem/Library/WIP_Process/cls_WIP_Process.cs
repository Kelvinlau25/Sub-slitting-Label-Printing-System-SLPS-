using ACL;
using ACL.Object;
using ACL.OracleClass;
using cls_DB;
using Symbol.RFID3;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace WIP_Process
{
    public class cls_WIP_Process
    {
        private cls_MSSQL obj_DB;

        private cls_Oracle obj_Ora_DB_ACL;

        private cls_Oracle obj_Ora_DB_PO;

        private cls_Oracle obj_Ora_DB_WIP;

        private cls_Oracle obj_Ora_DB_SCH;

        private string Str_DB_Tag_or_ConnStr;

        private string str_WIP_DB_ConnStr;

        private string str_ORACLE_ACL_ConnStr;

        private string str_ORACLE_PO_ConnStr;

        private string str_ORACLE_WIP_ConnStr;

        private string str_ORACLE_SCH_ConnStr;

        public cls_WIP_Process(string pstr_WIP_DB_ConnStr, string pstr_ORACLE_ACL_ConnStr, string pstr_ORACLE_PO_ConnStr, string pstr_ORACLE_WIP_ConnStr, string pstr_ORACLE_SCH_ConnStr)
        {
            this.str_WIP_DB_ConnStr = pstr_WIP_DB_ConnStr;
            this.str_ORACLE_ACL_ConnStr = pstr_ORACLE_ACL_ConnStr;
            this.str_ORACLE_PO_ConnStr = pstr_ORACLE_PO_ConnStr;
            this.str_ORACLE_WIP_ConnStr = pstr_ORACLE_WIP_ConnStr;
            this.str_ORACLE_SCH_ConnStr = pstr_ORACLE_SCH_ConnStr;
            this.check_component();
        }

        public void Active_Engine_Only()
        {
            string _str_ConsoleApp_Path = String.Concat(AppDomain.CurrentDomain.BaseDirectory, "Backend_App\\Engine\\PAB_Real_Time_WIP_Console.exe");
            this.Check_Process("PAB_Real_Time_WIP_Console");
            Process.Start(_str_ConsoleApp_Path, "");
        }

        public string ActiveAll_RFID_Reader()
        {
            string str;
            try
            {
                Process[] processes = Process.GetProcesses();
                int num = 0;
                while (num < (int)processes.Length)
                {
                    Process p = processes[num];
                    if (p.ProcessName.ToString().Trim().Equals("PAB_Real_Time_WIP_Console.exe"))
                    {
                        p.Kill();
                    }
                    if (p.ProcessName.ToString().Trim().Equals("PAB_Real_Time_WIP_RFID_Host.exe"))
                    {
                        p.Kill();
                    }
                    num = num + 1;
                }
                string _str_path = Directory.GetCurrentDirectory();
                Process.Start(String.Concat(_str_path, "\\Backend_App\\Engine\\PAB_Real_Time_WIP_Console.exe"));
            }
            catch (System.Exception exception)
            {
                str = exception.ToString();
                return str;
            }
            str = "";
            return str;
        }

        public string AddEquipmentRegistration(string pStr_UserID, string pStr_RFID, string pStr_Equip_Name, string pStr_Batch_ID, string pStr_RFID_Type, string pStr_UserLoc)
        {
            string str;
            try
            {
                string[,] _arr_str_param = new string[6, 2];
                _arr_str_param[0, 0] = "@pStr_Equip_Name";
                _arr_str_param[0, 1] = pStr_Equip_Name;
                _arr_str_param[1, 0] = "@pStr_RFID_Type";
                _arr_str_param[1, 1] = pStr_RFID_Type;
                _arr_str_param[2, 0] = "@pStr_Batch_ID";
                _arr_str_param[2, 1] = pStr_Batch_ID;
                _arr_str_param[3, 0] = "@pStr_RFID";
                _arr_str_param[3, 1] = pStr_RFID;
                _arr_str_param[4, 0] = "@pUserID";
                _arr_str_param[4, 1] = pStr_UserID;
                _arr_str_param[5, 0] = "@pUserLoc";
                _arr_str_param[5, 1] = pStr_UserLoc;
                this.obj_DB.executeActionQuery("PS_AddEquipmentRegistration", _arr_str_param, true);
                str = "";
            }
            catch (System.Exception exception)
            {
                str = exception.ToString();
            }
            return str;
        }

        public string AddNew_WIP_RFID_TRANS_By_RFID_Reader(string pStr_RFID_CEL_READER_NAME, string pStr_ID_RFID, string pStr_IPADDR, string pStr_LOCATION_NAME, string pStr_RFID_LOC_AREA)
        {
            string str;
            try
            {
                string[,] _arr_str_param = new string[5, 2];
                _arr_str_param[0, 0] = "@str_RFID_CEL_READER_NAME";
                _arr_str_param[0, 1] = pStr_RFID_CEL_READER_NAME;
                _arr_str_param[1, 0] = "@str_ID_RFID";
                _arr_str_param[1, 1] = pStr_ID_RFID;
                _arr_str_param[2, 0] = "@str_IPADDR";
                _arr_str_param[2, 1] = pStr_IPADDR;
                _arr_str_param[3, 0] = "@str_LOCATION_NAME";
                _arr_str_param[3, 1] = pStr_LOCATION_NAME;
                _arr_str_param[4, 0] = "@str_RFID_LOC_AREA";
                _arr_str_param[4, 1] = pStr_RFID_LOC_AREA;
                DataTable _obj_DT = new DataTable();
                _obj_DT.Load(this.obj_DB.executeQuery("ps_AddNew_WIP_RFID_TRANS_By_RFID_Reader", _arr_str_param, true));
                string _str_Date = _obj_DT.Rows[0]["RETURN_VALUE"].ToString().Trim();
                _obj_DT.Dispose();
                str = _str_Date;
            }
            catch (System.Exception exception)
            {
                str = "";
            }
            return str;
        }

        public string AddNewBleaching(string pStr_RFID_01, string pStr_RFID_02, string pStr_DeviceName, string pStr_LOCATION_NAME, string pStr_SCAN_FLAG, string pStr_HW_DEVICE, string pStr_UserID, string pStr_IPAddr, int pInt_YARDAGE, string pStr_BatchID)
        {
            string str;
            try
            {
                if (pStr_SCAN_FLAG.ToLower().Equals("machine in"))
                {
                    string _str_DMS_Sch = "";
                    _str_DMS_Sch = this.CheckSchMch(this.Get_PO_SERAIL_BY_BATCHID(pStr_BatchID), this.GET_ID_MM_MACHINE_BY_LOCATION_NAME(pStr_LOCATION_NAME));
                    if (!_str_DMS_Sch.Equals("FOLLOW SCHEDULE"))
                    {
                        str = _str_DMS_Sch;
                        return str;
                    }
                }
                DataTable _obj_DT = new DataTable();
                string[,] _arr_str_param = new string[7, 2];
                _arr_str_param[0, 0] = "@str_DeviceName";
                _arr_str_param[0, 1] = pStr_DeviceName;
                _arr_str_param[1, 0] = "@str_LOCATION_NAME";
                _arr_str_param[1, 1] = pStr_LOCATION_NAME;
                _arr_str_param[2, 0] = "@str_SCAN_FLAG";
                _arr_str_param[2, 1] = pStr_SCAN_FLAG;
                _arr_str_param[3, 0] = "@str_HW_DEVICE";
                _arr_str_param[3, 1] = pStr_HW_DEVICE;
                _arr_str_param[4, 0] = "@str_UserID";
                _arr_str_param[4, 1] = pStr_UserID;
                _arr_str_param[5, 0] = "@str_IPAddr";
                _arr_str_param[5, 1] = pStr_IPAddr;
                _arr_str_param[6, 0] = "@str_Batch";
                _arr_str_param[6, 1] = pStr_BatchID;
                _obj_DT.Load(this.obj_DB.executeQuery("SP_ADDNEW_MACHINE_OUT", _arr_str_param, true));
                string _str_return_value = this.obj_DB.get_LastError();
                if (_str_return_value.Equals(""))
                {
                    _str_return_value = _obj_DT.Rows[0]["INSERT_STATUS"].ToString();
                    _obj_DT.Dispose();
                    str = _str_return_value;
                }
                else
                {
                    _obj_DT.Dispose();
                    str = _str_return_value;
                }
            }
            catch (System.Exception exception)
            {
                str = exception.ToString();
            }
            return str;
        }

        public string AddNewGrey(string pStr_UserID, string pStr_POSerial, int pInt_Yardage, string pStr_NxtProc, string pStr_RFID_1, string pStr_RFID_2, string pStr_LocName, string pStr_IPAddr, string pStr_Shift, string pStr_DeviceType, string pStr_DeviceName, string pStr_Batch)
        {
            string str;
            try
            {
                string PO_No = "";
                PO_No = this.Get_PONumber_By_POSerialNumber(pStr_POSerial);
                if (PO_No.Equals(""))
                {
                    str = "NOPO";
                }
                else if (this.CheckPoQty(pStr_POSerial, pInt_Yardage).Equals("OK"))
                {
                    DataTable _obj_DT = new DataTable();
                    string[,] _arr_str_param = new string[12, 2];
                    _arr_str_param[0, 0] = "@str_PO_No";
                    _arr_str_param[0, 1] = PO_No;
                    _arr_str_param[1, 0] = "@str_POSerial";
                    _arr_str_param[1, 1] = pStr_POSerial;
                    _arr_str_param[2, 0] = "@str_LocName";
                    _arr_str_param[2, 1] = pStr_LocName;
                    _arr_str_param[3, 0] = "@str_HW_Device";
                    _arr_str_param[3, 1] = pStr_DeviceType;
                    _arr_str_param[4, 0] = "@str_RFID_01";
                    _arr_str_param[4, 1] = pStr_RFID_1;
                    _arr_str_param[5, 0] = "@str_RFID_02";
                    _arr_str_param[5, 1] = pStr_RFID_2;
                    _arr_str_param[6, 0] = "@str_UserID";
                    _arr_str_param[6, 1] = pStr_UserID;
                    _arr_str_param[7, 0] = "@str_IPAddr";
                    _arr_str_param[7, 1] = pStr_IPAddr;
                    _arr_str_param[8, 0] = "@int_YARDAGE";
                    _arr_str_param[8, 1] = pInt_Yardage.ToString();
                    _arr_str_param[9, 0] = "@str_DeviceName";
                    _arr_str_param[9, 1] = pStr_DeviceName;
                    _arr_str_param[10, 0] = "@str_NEXT_PROCESS";
                    _arr_str_param[10, 1] = pStr_NxtProc;
                    _arr_str_param[11, 0] = "@str_Batch";
                    _arr_str_param[11, 1] = pStr_Batch;
                    this.obj_DB.Begin_Trans();
                    _obj_DT.Load(this.obj_DB.executeQuery("PS_AddNewGrey", _arr_str_param, true));
                    string _str_return_value = this.obj_DB.get_LastError();
                    if (_str_return_value.Equals(""))
                    {
                        _str_return_value = _obj_DT.Rows[0]["INSERT_STATUS"].ToString();
                        if (_str_return_value.Trim().ToLower().Equals("success"))
                        {
                            if (!this.AddNewWIPS_Greyroom_Trans(pStr_POSerial, Convert.ToInt32(pStr_NxtProc), pStr_Batch, pInt_Yardage, pStr_LocName, pStr_Shift, pStr_UserID, pStr_IPAddr).ToLower().Equals("fail"))
                            {
                                try
                                {
                                    this.obj_DB.Commit_Trans();
                                }
                                catch (System.Exception)
                                {
                                }
                            }
                            else
                            {
                                try
                                {
                                    this.obj_DB.Rollback_Trans();
                                }
                                catch (System.Exception)
                                {
                                }
                            }
                        }
                        _obj_DT.Dispose();
                        str = _str_return_value;
                    }
                    else
                    {
                        _obj_DT.Dispose();
                        str = _str_return_value;
                    }
                }
                else
                {
                    str = "OverLimit";
                }
            }
            catch (System.Exception exception3)
            {
                System.Exception ex = exception3;
                try
                {
                    this.obj_DB.Rollback_Trans();
                }
                catch (System.Exception)
                {
                }
                str = ex.ToString();
            }
            return str;
        }

        public string AddNewStaging(string pStr_RFID_01, string pStr_RFID_02, string pStr_DeviceName, string pStr_LOCATION_NAME, string pStr_SCAN_FLAG, string pStr_HW_DEVICE, string pStr_UserID, string pStr_IPAddr, string pStr_BatchID)
        {
            string str;
            try
            {
                DataTable _obj_DT = new DataTable();
                string[,] _arr_str_param = new string[10, 2];
                _arr_str_param[0, 0] = "@str_RFID_01";
                _arr_str_param[0, 1] = pStr_RFID_01;
                _arr_str_param[1, 0] = "@str_RFID_02";
                _arr_str_param[1, 1] = pStr_RFID_02;
                _arr_str_param[2, 0] = "@str_DeviceName";
                _arr_str_param[2, 1] = pStr_DeviceName;
                _arr_str_param[3, 0] = "@str_LOCATION_NAME";
                _arr_str_param[3, 1] = pStr_LOCATION_NAME;
                _arr_str_param[4, 0] = "@str_SCAN_FLAG";
                _arr_str_param[4, 1] = pStr_SCAN_FLAG;
                _arr_str_param[5, 0] = "@str_HW_DEVICE";
                _arr_str_param[5, 1] = pStr_HW_DEVICE;
                _arr_str_param[6, 0] = "@str_UserID";
                _arr_str_param[6, 1] = pStr_UserID;
                _arr_str_param[7, 0] = "@str_IPAddr";
                _arr_str_param[7, 1] = pStr_IPAddr;
                _arr_str_param[8, 0] = "@int_YARDAGE";
                _arr_str_param[8, 1] = "0";
                _arr_str_param[9, 0] = "@str_Batch";
                _arr_str_param[9, 1] = pStr_BatchID;
                _obj_DT.Load(this.obj_DB.executeQuery("SP_ADDNEW_MACHINE_IN", _arr_str_param, true));
                string _str_return_value = this.obj_DB.get_LastError();
                if (_str_return_value.Equals(""))
                {
                    _str_return_value = _obj_DT.Rows[0]["INSERT_STATUS"].ToString();
                    _obj_DT.Dispose();
                    str = _str_return_value;
                }
                else
                {
                    _obj_DT.Dispose();
                    str = _str_return_value;
                }
            }
            catch (System.Exception exception)
            {
                str = exception.ToString();
            }
            return str;
        }

        public string AddNewWIPS_BDPF_Trans(string pStr_POSerial, int pInt_Qty, int pStr_NxtProc, string pStr_BatchOut, string pStr_ShiftGroup, string pStr_Ref_ID, string pStr_UserID, string pStr_UserLoc)
        {
            string str;
            try
            {
                string[,] _arr_str_param = new string[8, 2];
                _arr_str_param[0, 0] = "pPOSerial";
                _arr_str_param[0, 1] = pStr_POSerial;
                _arr_str_param[1, 0] = "pYardage";
                _arr_str_param[1, 1] = pInt_Qty.ToString();
                _arr_str_param[2, 0] = "pToProcess";
                _arr_str_param[2, 1] = pStr_NxtProc.ToString();
                _arr_str_param[3, 0] = "pBatchOut";
                _arr_str_param[3, 1] = pStr_BatchOut;
                _arr_str_param[4, 0] = "pShiftGroup";
                _arr_str_param[4, 1] = pStr_ShiftGroup;
                _arr_str_param[5, 0] = "pRefID";
                _arr_str_param[5, 1] = pStr_Ref_ID;
                _arr_str_param[6, 0] = "pUserID";
                _arr_str_param[6, 1] = pStr_UserID;
                _arr_str_param[7, 0] = "pUserLoc";
                _arr_str_param[7, 1] = pStr_UserLoc;
                string return_value = this.obj_Ora_DB_WIP.executeActionQuery("SP_Add_Grey_WIPS_Trans", _arr_str_param, true).ToString();
                str = Convert.ToDouble(return_value) <= 0 ? "Fail" : return_value;
            }
            catch (System.Exception exception)
            {
                str = exception.ToString();
            }
            return str;
        }

        public string AddNewWIPS_Greyroom_Trans(string pStr_POSerial, int pStr_NxtProc, string pStr_BatchOut, int pInt_Qty, string pStr_LocName, string pStr_ShiftGroup, string pStr_UserID, string pStr_UserLoc)
        {
            string str;
            try
            {
                string[,] _arr_str_param = new string[8, 2];
                _arr_str_param[0, 0] = "pPOSerial";
                _arr_str_param[0, 1] = pStr_POSerial;
                _arr_str_param[1, 0] = "pToProcess";
                _arr_str_param[1, 1] = pStr_NxtProc.ToString();
                _arr_str_param[2, 0] = "pBatchOut";
                _arr_str_param[2, 1] = pStr_BatchOut;
                _arr_str_param[3, 0] = "pYardage";
                _arr_str_param[3, 1] = pInt_Qty.ToString();
                _arr_str_param[4, 0] = "pLocName";
                _arr_str_param[4, 1] = pStr_LocName;
                _arr_str_param[5, 0] = "pShiftGroup";
                _arr_str_param[5, 1] = pStr_ShiftGroup;
                _arr_str_param[6, 0] = "pUserID";
                _arr_str_param[6, 1] = pStr_UserID;
                _arr_str_param[7, 0] = "pUserLoc";
                _arr_str_param[7, 1] = pStr_UserLoc;
                string return_value = this.obj_Ora_DB_WIP.executeActionQuery("SP_Add_Grey_Room_Trans", _arr_str_param, true).ToString();
                if (Convert.ToDouble(return_value) <= 0)
                {
                    str = "Fail";
                }
                else
                {
                    this.AddNewWIPS_BDPF_Trans(pStr_POSerial, pInt_Qty, pStr_NxtProc, pStr_BatchOut, pStr_ShiftGroup, return_value, pStr_UserID, pStr_UserLoc);
                    str = return_value;
                }
            }
            catch (System.Exception exception)
            {
                str = exception.ToString();
            }
            return str;
        }

        public string AddRFID_Usage(string pID_Section, string pID_Machine, string pPOSerial, string pModule, string pModuleDesc, string pDevice, string pGroupNo, string pStr_UserID, string pStr_UserLoc)
        {
            string str;
            try
            {
                string[,] _arr_str_param = new string[9, 2];
                _arr_str_param[0, 0] = "@pID_Section";
                _arr_str_param[0, 1] = pID_Section;
                _arr_str_param[1, 0] = "@pID_Machine";
                _arr_str_param[1, 1] = pID_Machine;
                _arr_str_param[2, 0] = "@pPOSerial";
                _arr_str_param[2, 1] = pPOSerial;
                _arr_str_param[3, 0] = "@pModule";
                _arr_str_param[3, 1] = pModule;
                _arr_str_param[4, 0] = "@pModuleDesc";
                _arr_str_param[4, 1] = pModuleDesc;
                _arr_str_param[5, 0] = "@pDevice";
                _arr_str_param[5, 1] = pDevice;
                _arr_str_param[6, 0] = "@pGroupNo";
                _arr_str_param[6, 1] = pGroupNo;
                _arr_str_param[7, 0] = "@pUserID";
                _arr_str_param[7, 1] = pStr_UserID;
                _arr_str_param[8, 0] = "@pUserLoc";
                _arr_str_param[8, 1] = pStr_UserLoc;
                this.obj_DB.executeActionQuery("PS_AddRFID_Usage", _arr_str_param, true);
                str = "";
            }
            catch (System.Exception exception)
            {
                str = exception.ToString();
            }
            return str;
        }

        private void check_component()
        {
            if (this.obj_DB == null)
            {
                if (!this.str_WIP_DB_ConnStr.Equals(""))
                {
                    this.obj_DB = new cls_MSSQL(this.str_WIP_DB_ConnStr);
                }
            }
            if (this.obj_Ora_DB_ACL == null)
            {
                if (!this.str_ORACLE_ACL_ConnStr.Equals(""))
                {
                    this.obj_Ora_DB_ACL = new cls_Oracle(this.str_ORACLE_ACL_ConnStr);
                }
            }
            if (this.obj_Ora_DB_PO == null)
            {
                if (!this.str_ORACLE_PO_ConnStr.Equals(""))
                {
                    this.obj_Ora_DB_PO = new cls_Oracle(this.str_ORACLE_PO_ConnStr);
                }
            }
            if (this.obj_Ora_DB_WIP == null)
            {
                if (!this.str_ORACLE_WIP_ConnStr.Equals(""))
                {
                    this.obj_Ora_DB_WIP = new cls_Oracle(this.str_ORACLE_WIP_ConnStr);
                }
            }
            if (this.obj_Ora_DB_SCH == null)
            {
                if (!this.str_ORACLE_SCH_ConnStr.Equals(""))
                {
                    this.obj_Ora_DB_SCH = new cls_Oracle(this.str_ORACLE_SCH_ConnStr);
                }
            }
        }

        public bool Check_Engine_Availability()
        {
            bool _bol_Check = false;
            Process[] processes = Process.GetProcesses();
            int num = 0;
            while (num < (int)processes.Length)
            {
                if (!processes[num].ProcessName.ToString().Trim().Equals("PAB_Real_Time_WIP_Console"))
                {
                    num = num + 1;
                }
                else
                {
                    _bol_Check = true;
                    break;
                }
            }
            return _bol_Check;
        }

        private void Check_Process(string pStr_AppName)
        {
            Process[] processes = Process.GetProcesses();
            int num = 0;
            while (num < (int)processes.Length)
            {
                Process _obj_processlist = processes[num];
                if (string.Compare(pStr_AppName, _obj_processlist.ProcessName.ToString().Trim(), StringComparison.Ordinal) != 0)
                {
                    num = num + 1;
                }
                else
                {
                    _obj_processlist.Kill();
                    break;
                }
            }
        }

        public bool Check_RFID_Station(string pStr_DeviceName)
        {
            string _str_Msg = String.Concat(pStr_DeviceName, "|Check");
            bool _bol_Check = false;
            TcpClient clientSocket = new TcpClient();
            int _int_iCount = 0;
            do
            {
                try
                {
                    clientSocket.Connect("127.0.0.1", 8888);
                    break;
                }
                catch (System.Exception)
                {
                    Thread.Sleep(1000);
                }
                _int_iCount = _int_iCount + 1;
            } while (_int_iCount <= 100);
            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = Encoding.ASCII.GetBytes(String.Concat(_str_Msg, "$"));
            serverStream.Write(outStream, 0, (int)outStream.Length);
            serverStream.Flush();
            string _str_ReturnValue = "";
            byte[] _byte_bytesFrom = new byte[10026];
            serverStream.Read(_byte_bytesFrom, 0, Convert.ToInt32(clientSocket.ReceiveBufferSize));
            _str_ReturnValue = Encoding.ASCII.GetString(_byte_bytesFrom);
            _str_ReturnValue = _str_ReturnValue.Substring(0, _str_ReturnValue.IndexOf("$"));
            serverStream.Flush();
            _bol_Check = _str_ReturnValue.Trim().ToLower().Equals("active") ? true : false;
            serverStream.Close();
            clientSocket.GetStream().Close();
            clientSocket.Close();
            try
            {
                serverStream.Dispose();
            }
            catch (System.Exception)
            {
            }
            return _bol_Check;
        }

        public string CheckPoQty(string pStr_POSerial, int pInt_Qty)
        {
            string str;
            try
            {
                this.check_component();
                string[,] _arr_str_param = new string[2, 2];
                _arr_str_param[0, 0] = "pPOSerial";
                _arr_str_param[0, 1] = pStr_POSerial;
                _arr_str_param[1, 0] = "pYardage";
                _arr_str_param[1, 1] = pInt_Qty.ToString();
                str = Convert.ToDouble(this.obj_Ora_DB_WIP.executeActionQuery("SP_GRAY_CHECK_PO_QTY", _arr_str_param, true).ToString()) <= 0 ? "OK" : "PO Qty Over Limit";
            }
            catch (System.Exception exception)
            {
                str = exception.ToString();
            }
            return str;
        }

        public string CheckSchMch(string pStr_POSerial, string pStr_IDMachine)
        {
            string str;
            try
            {
                this.check_component();
                string[,] _arr_str_param = new string[3, 2];
                _arr_str_param[0, 0] = "pPOSerial";
                _arr_str_param[0, 1] = pStr_POSerial;
                _arr_str_param[1, 0] = "pIDMachine";
                _arr_str_param[1, 1] = pStr_IDMachine;
                _arr_str_param[2, 0] = "pSection";
                _arr_str_param[2, 1] = "2";
                string return_value = this.obj_Ora_DB_SCH.executeActionQuery("SP_CHECK_SCH_MCH", _arr_str_param, true).ToString();
                if (Convert.ToDouble(return_value) != 1)
                {
                    str = Convert.ToDouble(return_value) != 2 ? "Invalid SCHEDULE" : "ALREADY START";
                }
                else
                {
                    str = "FOLLOW SCHEDULE";
                }
            }
            catch (System.Exception exception)
            {
                str = exception.ToString();
            }
            return str;
        }

        public string Chk_DeCom(string pStr_RFID)
        {
            string str;
            try
            {
                DataTable _obj_DT = new DataTable();
                string[,] _arr_str_param = new string[1, 2];
                _arr_str_param[0, 0] = "@str_RFID";
                _arr_str_param[0, 1] = pStr_RFID;
                _obj_DT.Load(this.obj_DB.executeQuery("SP_CHECK_DECOM", _arr_str_param, true));
                string _str_return_value = this.obj_DB.get_LastError();
                if (_str_return_value.Equals(""))
                {
                    if (_obj_DT.Rows.Count <= 0)
                    {
                        _str_return_value = "Unknown RFID";
                    }
                    else
                    {
                        _str_return_value = _obj_DT.Rows[0]["BATCH_ID"].ToString();
                        if (_str_return_value.Equals(""))
                        {
                            _str_return_value = "Unknown RFID";
                        }
                    }
                    _obj_DT.Dispose();
                    str = _str_return_value;
                }
                else
                {
                    _obj_DT.Dispose();
                    str = _str_return_value;
                }
            }
            catch (System.Exception exception)
            {
                str = exception.ToString();
            }
            return str;
        }

        public string DeCom(string pStr_UserID, string pStr_RFID, string pStr_IPAddr)
        {
            string str;
            try
            {
                DataTable _obj_DT = new DataTable();
                string[,] _arr_str_param = new string[3, 2];
                _arr_str_param[0, 0] = "@str_RFID";
                _arr_str_param[0, 1] = pStr_RFID;
                _arr_str_param[1, 0] = "@str_IPAddr";
                _arr_str_param[1, 1] = pStr_IPAddr;
                _arr_str_param[2, 0] = "@str_UserID";
                _arr_str_param[2, 1] = pStr_UserID;
                _obj_DT.Load(this.obj_DB.executeQuery("SP_DECOM", _arr_str_param, true));
                string _str_return_value = this.obj_DB.get_LastError();
                if (_str_return_value.Equals(""))
                {
                    _str_return_value = _obj_DT.Rows[0]["DECOM"].ToString();
                    _obj_DT.Dispose();
                    str = _str_return_value;
                }
                else
                {
                    _obj_DT.Dispose();
                    str = _str_return_value;
                }
            }
            catch (System.Exception exception)
            {
                str = exception.ToString();
            }
            return str;
        }

        public string Equip_Reg_Confirm(string pStr_EQUIPMENT_NAME, string pStr_RFID_TYPE, string pStr_RFID_01, string pStr_RFID_02, string pStr_IPAddr, string pStr_UserID, string pStr_BATCH, string pStr_ID_MM_EQUIPMENT_REG)
        {
            this.check_component();
            string[,] _arr_str_param = new string[9, 2];
            _arr_str_param[0, 0] = "@str_EQUIPMENT_NAME";
            _arr_str_param[0, 1] = pStr_EQUIPMENT_NAME;
            _arr_str_param[1, 0] = "@str_RFID_TYPE";
            _arr_str_param[1, 1] = pStr_RFID_TYPE;
            _arr_str_param[2, 0] = "@str_RFID_01";
            _arr_str_param[2, 1] = pStr_RFID_01;
            _arr_str_param[3, 0] = "@str_RFID_02";
            _arr_str_param[3, 1] = pStr_RFID_02;
            _arr_str_param[4, 0] = "@str_IPAddr";
            _arr_str_param[4, 1] = pStr_IPAddr;
            _arr_str_param[5, 0] = "@str_BATCH";
            _arr_str_param[5, 1] = pStr_BATCH;
            _arr_str_param[6, 0] = "@str_UserID";
            _arr_str_param[6, 1] = pStr_UserID;
            _arr_str_param[7, 0] = "@int_ID_MM_EQUIPMENT_REG";
            _arr_str_param[7, 1] = pStr_ID_MM_EQUIPMENT_REG;
            _arr_str_param[8, 0] = "@str_Option";
            _arr_str_param[8, 1] = "Equip_Reg_Confirm";
            DataTable _obj_DT = new DataTable();
            string _str_return_value = "";
            _obj_DT.Load(this.obj_DB.executeQuery("SP_EQUIPMENT_REGISTRATION", _arr_str_param, true));
            _str_return_value = this.obj_DB.get_LastError();
            return _str_return_value.Equals("") ? "Success" : _str_return_value;
        }

        public string Equip_Reg_Generate_BatchNo(string pStr_EQUIPMENT_NAME, string pStr_RFID_TYPE, string pStr_RFID_01, string pStr_RFID_02, string pStr_IPAddr, string pStr_UserID)
        {
            this.check_component();
            string[,] _arr_str_param = new string[9, 2];
            _arr_str_param[0, 0] = "@str_EQUIPMENT_NAME";
            _arr_str_param[0, 1] = pStr_EQUIPMENT_NAME;
            _arr_str_param[1, 0] = "@str_RFID_TYPE";
            _arr_str_param[1, 1] = pStr_RFID_TYPE;
            _arr_str_param[2, 0] = "@str_RFID_01";
            _arr_str_param[2, 1] = pStr_RFID_01;
            _arr_str_param[3, 0] = "@str_RFID_02";
            _arr_str_param[3, 1] = pStr_RFID_02;
            _arr_str_param[4, 0] = "@str_IPAddr";
            _arr_str_param[4, 1] = pStr_IPAddr;
            _arr_str_param[5, 0] = "@str_BATCH";
            _arr_str_param[5, 1] = "";
            _arr_str_param[6, 0] = "@str_UserID";
            _arr_str_param[6, 1] = pStr_UserID;
            _arr_str_param[7, 0] = "@int_ID_MM_EQUIPMENT_REG";
            _arr_str_param[7, 1] = "";
            _arr_str_param[8, 0] = "@str_Option";
            _arr_str_param[8, 1] = "Generate_BatchNo";
            DataTable _obj_DT = new DataTable();
            string _str_return_value = "";
            _obj_DT.Load(this.obj_DB.executeQuery("SP_EQUIPMENT_REGISTRATION", _arr_str_param, true));
            _str_return_value = this.obj_DB.get_LastError();
            if (_obj_DT.Rows[0]["EQUIPMENT_REGISTRATION"].ToString().Trim().Equals("EXISTED"))
            {
                _str_return_value = "Fail";
            }
            else if (_obj_DT.Rows[0]["EQUIPMENT_REGISTRATION"].ToString().Trim().Equals("OCCUPIED"))
            {
                _str_return_value = "OCCUPIED";
            }
            else if (_obj_DT.Rows[0]["EQUIPMENT_REGISTRATION"].ToString().Trim().Equals("INSERTED"))
            {
                _str_return_value = _obj_DT.Rows[0]["BATCH_NUMBER"].ToString().Trim();
                _str_return_value = String.Concat(_str_return_value, "|", _obj_DT.Rows[0]["ID_MM_EQUIPMENT_REG"].ToString().Trim());
            }
            return _str_return_value;
        }

        public string Equipment_Registration_BK_10APR2017(string pStr_EQUIPMENT_NAME, string pStr_RFID_TYPE, string pStr_RFID_01, string pStr_RFID_02, string pStr_IPAddr, string pStr_BATCH)
        {
            this.check_component();
            string[,] _arr_str_param = new string[6, 2];
            _arr_str_param[0, 0] = "@str_EQUIPMENT_NAME";
            _arr_str_param[0, 1] = pStr_EQUIPMENT_NAME;
            _arr_str_param[1, 0] = "@str_RFID_TYPE";
            _arr_str_param[1, 1] = pStr_RFID_TYPE;
            _arr_str_param[2, 0] = "@str_RFID_01";
            _arr_str_param[2, 1] = pStr_RFID_01;
            _arr_str_param[3, 0] = "@str_RFID_02";
            _arr_str_param[3, 1] = pStr_RFID_02;
            _arr_str_param[4, 0] = "@str_IPAddr";
            _arr_str_param[4, 1] = pStr_IPAddr;
            _arr_str_param[5, 0] = "@str_BATCH";
            _arr_str_param[5, 1] = pStr_BATCH;
            DataTable _obj_DT = new DataTable();
            string _str_return_value = "";
            _obj_DT.Load(this.obj_DB.executeQuery("SP_EQUIPMENT_REGISTRATION", _arr_str_param, true));
            _str_return_value = this.obj_DB.get_LastError();
            if (_obj_DT.Rows[0]["EQUIPMENT_REGISTRATION"].ToString().Trim().Equals("EXISTED"))
            {
                _str_return_value = "Fail";
            }
            else if (_obj_DT.Rows[0]["EQUIPMENT_REGISTRATION"].ToString().Trim().Equals("INSERTED"))
            {
                _str_return_value = "Success";
            }
            return _str_return_value;
        }

        public string Get_All_RFID_Reder_Info()
        {
            this.check_component();
            DataTable _obj_DT_Info = new DataTable();
            string _str_Info = "";
            _obj_DT_Info.Load(this.obj_DB.executeQuery("SP_GET_ALL_RFID_READER_INFO", null, true));
            int count = _obj_DT_Info.Rows.Count - 1;
            int _int_IRFID = 0;
            do
            {
                _str_Info = String.Concat(_str_Info, "@", _obj_DT_Info.Rows[_int_IRFID]["RFID_CEL_READER_NAME"].ToString(), ",");
                _str_Info = String.Concat(_str_Info, _obj_DT_Info.Rows[_int_IRFID]["IP_ADDRESS"].ToString(), ",");
                _str_Info = String.Concat(_str_Info, _obj_DT_Info.Rows[_int_IRFID]["LOCATION_NAME"].ToString(), ",");
                _str_Info = String.Concat(_str_Info, _obj_DT_Info.Rows[_int_IRFID]["RFID_STATUS"].ToString(), ",");
                _str_Info = String.Concat(_str_Info, _obj_DT_Info.Rows[_int_IRFID]["ID_MM_RFID_LOC_READER_REG"].ToString(), ",");
                _str_Info = String.Concat(_str_Info, _obj_DT_Info.Rows[_int_IRFID]["RFID_DEVICE_NAME"].ToString());
                _int_IRFID = _int_IRFID + 1;
            } while (_int_IRFID <= count);
            _str_Info = _str_Info.Substring(1, _str_Info.Length - 1).Trim();
            return _str_Info;
        }

        public string GET_BLEACHING_INFO_BY_RFID(string pStr_RFID)
        {
            string lastError;
            this.check_component();
            string[,] _arr_str_param = new string[2, 2];
            _arr_str_param[0, 0] = "@str_RFID";
            _arr_str_param[0, 1] = pStr_RFID;
            DataTable _obj_DT = new DataTable();
            string _str_return_value = "";
            _obj_DT.Load(this.obj_DB.executeQuery("SP_GET_STAGING_INFO_BY_RFID", _arr_str_param, true));
            if (_obj_DT.Rows.Count <= 0)
            {
                _obj_DT.Dispose();
                lastError = this.obj_DB.get_LastError();
            }
            else
            {
                _str_return_value = String.Concat(_obj_DT.Rows[0]["LOCATION_NAME"].ToString().Trim(), "$");
                _str_return_value = String.Concat(_str_return_value, _obj_DT.Rows[0]["RFID_LOC_AREA"].ToString().Trim(), "$");
                _str_return_value = String.Concat(_str_return_value, _obj_DT.Rows[0]["YARDAGE_REQUIRE"].ToString().Trim());
                _obj_DT.Dispose();
                lastError = _str_return_value;
            }
            return lastError;
        }

        public string Get_Equipment_Name_Listing()
        {
            string str;
            string _str_Equipment_Name = "";
            DataTable _obj_DT = new DataTable();
            _obj_DT.Load(this.obj_DB.executeQuery("SP_GET_EQ_NAME_LST", null, true));
            if (_obj_DT.Rows.Count <= 0)
            {
                try
                {
                    _obj_DT.Dispose();
                }
                catch (System.Exception)
                {
                }
                str = null;
            }
            else
            {
                int count = _obj_DT.Rows.Count - 1;
                int _int_iLoop = 0;
                do
                {
                    string[] strArrays = { _str_Equipment_Name, "@", _obj_DT.Rows[_int_iLoop]["ID_MM_EQUIPMENT"].ToString().Trim(), "$", _obj_DT.Rows[_int_iLoop]["EQUIPMENT_NAME"].ToString().Trim() };
                    _str_Equipment_Name = String.Concat(strArrays);
                    _int_iLoop = _int_iLoop + 1;
                } while (_int_iLoop <= count);
                _obj_DT.Dispose();
                this.obj_DB.dispose_command();
                str = _str_Equipment_Name;
            }
            return str;
        }

        public DataTable GET_Equipment_RFID_Listing()
        {
            this.check_component();
            DataTable _obj_DT = new DataTable("RFID");
            ArrayList _arr_List = new ArrayList();
            _obj_DT.Load(this.obj_DB.executeQuery("SP_GET_ALL_EQUIPMENT_REG_RFID", null, true));
            int count = _obj_DT.Rows.Count - 1;
            int _int_iCount = 0;
            do
            {
                _arr_List.Add(_obj_DT.Rows[_int_iCount]["RFID"].ToString().Trim());
                _int_iCount = _int_iCount + 1;
            } while (_int_iCount <= count);
            return _obj_DT;
        }

        public string Get_Error_Message(string pStr_ModuleName, string pStr_ComponentName)
        {
            string str;
            string _str_ERROR_MSG = "";
            DataTable _obj_DT = new DataTable();
            string[,] _arr_str_param = new string[2, 2];
            _arr_str_param[0, 0] = "@pModuleName";
            _arr_str_param[0, 1] = pStr_ModuleName;
            _arr_str_param[1, 0] = "@pComponentName";
            _arr_str_param[1, 1] = pStr_ComponentName;
            _obj_DT.Load(this.obj_DB.executeQuery("SP_GET_ERROR_MSG", _arr_str_param, true));
            if (_obj_DT.Rows.Count <= 0)
            {
                try
                {
                    _obj_DT.Dispose();
                }
                catch (System.Exception)
                {
                }
                str = null;
            }
            else
            {
                _str_ERROR_MSG = _obj_DT.Rows[0]["ERROR_MSG"].ToString().Trim();
                _obj_DT.Dispose();
                this.obj_DB.dispose_command();
                str = _str_ERROR_MSG;
            }
            return str;
        }

        public string Get_GroupNo()
        {
            string str;
            string _str_GroupNo = "";
            DataTable _obj_DT = new DataTable();
            _obj_DT.Load(this.obj_DB.executeQuery("SP_GET_GROUPNO", null, true));
            if (_obj_DT.Rows.Count <= 0)
            {
                try
                {
                    _obj_DT.Dispose();
                }
                catch (System.Exception)
                {
                }
                str = null;
            }
            else
            {
                _str_GroupNo = _obj_DT.Rows[0]["GroupNo"].ToString().Trim();
                _obj_DT.Dispose();
                this.obj_DB.dispose_command();
                str = _str_GroupNo;
            }
            return str;
        }

        private string GET_ID_MM_MACHINE_BY_LOCATION_NAME(string pStr_LOCATION_NAME)
        {
            DataTable _obj_DT = new DataTable();
            string[,] _arr_str_param = new string[1, 2];
            _arr_str_param[0, 0] = "@str_LOCATION_NAME";
            _arr_str_param[0, 1] = pStr_LOCATION_NAME;
            _obj_DT.Load(this.obj_DB.executeQuery("SP_GET_ID_MM_MACHINE_BY_LOCATION_NAME", _arr_str_param, true));
            string _str_ReturnValue = _obj_DT.Rows[0]["ID_MM_MACHINE"].ToString().Trim();
            _obj_DT.Dispose();
            return _str_ReturnValue;
        }

        public string Get_Machine_Entry_Listing()
        {
            string str;
            string _str_Machine_Entry = "";
            DataTable _obj_DT = new DataTable();
            _obj_DT.Load(this.obj_DB.executeQuery("SP_GET_MAC_ENTRY_LST", null, true));
            if (_obj_DT.Rows.Count <= 0)
            {
                try
                {
                    _obj_DT.Dispose();
                }
                catch (System.Exception)
                {
                }
                str = null;
            }
            else
            {
                int count = _obj_DT.Rows.Count - 1;
                int _int_iLoop = 0;
                do
                {
                    _str_Machine_Entry = String.Concat(_str_Machine_Entry, "@", _obj_DT.Rows[_int_iLoop]["RFID_LOC_AREA"].ToString().Trim());
                    _int_iLoop = _int_iLoop + 1;
                } while (_int_iLoop <= count);
                _obj_DT.Dispose();
                this.obj_DB.dispose_command();
                str = _str_Machine_Entry;
            }
            return str;
        }

        public string Get_MachineName_Listing_All()
        {
            string str;
            DataTable _obj_DT = new DataTable();
            string _return_value = "";
            _obj_DT.Load(this.obj_Ora_DB_PO.executeQuery("SP_MACHINE_NAME_ALL_SEL", null, true));
            if (_obj_DT.Rows.Count <= 0)
            {
                try
                {
                    _obj_DT.Dispose();
                }
                catch (System.Exception)
                {
                }
                str = null;
            }
            else
            {
                List<Machine_Name_Dtl> _obj_List = new List<Machine_Name_Dtl>();
                int count = _obj_DT.Rows.Count - 1;
                int _int_iLoop = 0;
                do
                {
                    Machine_Name_Dtl _obj = new Machine_Name_Dtl()
                    {
                        ID_MM_MACHINE = _obj_DT.Rows[_int_iLoop]["ID_MM_MACHINE"].ToString().Trim(),
                        MACHINE_CODE = _obj_DT.Rows[_int_iLoop]["MACHINE_CODE"].ToString().Trim(),
                        MACHINE_DESC = _obj_DT.Rows[_int_iLoop]["MACHINE_DESC"].ToString().Trim()
                    };
                    _return_value = String.Concat(_return_value, "@", _obj_DT.Rows[_int_iLoop]["MACHINE_CODE"].ToString().Trim());
                    _obj_List.Add(_obj);
                    _int_iLoop = _int_iLoop + 1;
                } while (_int_iLoop <= count);
                _obj_DT.Dispose();
                this.obj_Ora_DB_PO.dispose_command();
                str = _return_value;
            }
            return str;
        }

        public string GET_MM_RFID_LOC_READER_REG_BY_RFID(string pStr_RFID)
        {
            string lastError;
            this.check_component();
            string[,] _arr_str_param = new string[1, 2];
            _arr_str_param[0, 0] = "@str_RFID";
            _arr_str_param[0, 1] = pStr_RFID;
            DataTable _obj_DT = new DataTable();
            string _str_return_value = "";
            _obj_DT.Load(this.obj_DB.executeQuery("SP_GET_MM_RFID_LOC_READER_REG_BY_RFID", _arr_str_param, true));
            if (_obj_DT.Rows.Count <= 0)
            {
                _obj_DT.Dispose();
                lastError = this.obj_DB.get_LastError();
            }
            else
            {
                _str_return_value = String.Concat(_obj_DT.Rows[0]["LOCATION_NAME"].ToString().Trim(), "$");
                _str_return_value = String.Concat(_str_return_value, _obj_DT.Rows[0]["RFID_LOC_AREA"].ToString().Trim(), "$");
                _str_return_value = String.Concat(_str_return_value, _obj_DT.Rows[0]["YARDAGE_REQUIRE"].ToString().Trim());
                _obj_DT.Dispose();
                lastError = _str_return_value;
            }
            return lastError;
        }

        public string Get_Next_Process_Listing()
        {
            string str;
            DataTable _obj_DT = new DataTable();
            string _return_value = "";
            _obj_DT.Load(this.obj_Ora_DB_PO.executeQuery("SP_GET_NXT_PROC_LST", null, true));
            if (_obj_DT.Rows.Count <= 0)
            {
                try
                {
                    _obj_DT.Dispose();
                }
                catch (System.Exception)
                {
                }
                str = null;
            }
            else
            {
                int count = _obj_DT.Rows.Count - 1;
                int _int_iLoop = 0;
                do
                {
                    string[] strArrays = { _return_value, "@", _obj_DT.Rows[_int_iLoop]["ID_MM_PROCESS_GROUP"].ToString().Trim(), "$", _obj_DT.Rows[_int_iLoop]["PROCESS_GROUP_NAME"].ToString().Trim() };
                    _return_value = String.Concat(strArrays);
                    _int_iLoop = _int_iLoop + 1;
                } while (_int_iLoop <= count);
                _obj_DT.Dispose();
                this.obj_Ora_DB_PO.dispose_command();
                str = _return_value;
            }
            return str;
        }

        public string Get_Next_Shift_Group_Listing()
        {
            string str;
            DataTable _obj_DT = new DataTable();
            string _return_value = "";
            _obj_DT.Load(this.obj_Ora_DB_PO.executeQuery("SP_GET_SHIFT_GROUP_LST", null, true));
            if (_obj_DT.Rows.Count <= 0)
            {
                try
                {
                    _obj_DT.Dispose();
                }
                catch (System.Exception)
                {
                }
                str = null;
            }
            else
            {
                int count = _obj_DT.Rows.Count - 1;
                int _int_iLoop = 0;
                do
                {
                    string[] strArrays = { _return_value, "@", _obj_DT.Rows[_int_iLoop]["ID_MM_SHIFT_GROUP"].ToString().Trim(), "$", _obj_DT.Rows[_int_iLoop]["SHIFT_GROUP"].ToString().Trim() };
                    _return_value = String.Concat(strArrays);
                    _int_iLoop = _int_iLoop + 1;
                } while (_int_iLoop <= count);
                _obj_DT.Dispose();
                this.obj_Ora_DB_PO.dispose_command();
                str = _return_value;
            }
            return str;
        }

        public string Get_NxtProc_Desc(string pStr_NxtProc)
        {
            string str;
            DataTable _obj_DT = new DataTable();
            string[,] _arr_str_param = new string[1, 2];
            _arr_str_param[0, 0] = "@pNxtProc";
            _arr_str_param[0, 1] = pStr_NxtProc;
            _obj_DT.Load(this.obj_Ora_DB_PO.executeQuery("SP_GET_NXT_PROC_SEL", _arr_str_param, true));
            if (_obj_DT.Rows.Count <= 0)
            {
                try
                {
                    _obj_DT.Dispose();
                }
                catch (System.Exception)
                {
                }
                str = null;
            }
            else
            {
                string vNext_Process = _obj_DT.Rows[0]["SEC_DESC"].ToString().Trim();
                _obj_DT.Dispose();
                this.obj_Ora_DB_PO.dispose_command();
                str = vNext_Process;
            }
            return str;
        }

        public string Get_PO_SERAIL_BY_BATCHID(string pStr_BATCH_ID)
        {
            string str;
            string _str_PO = "";
            string _str_PO_SERIAL = "";
            DataTable _obj_DT = new DataTable();
            string[,] _arr_str_param = new string[1, 2];
            _arr_str_param[0, 0] = "@str_BatchID";
            _arr_str_param[0, 1] = pStr_BATCH_ID;
            _obj_DT.Load(this.obj_DB.executeQuery("SP_GET_RFID_NUMBER_BY_BATCHID", _arr_str_param, true));
            if (_obj_DT.Rows.Count <= 0)
            {
                try
                {
                    _obj_DT.Dispose();
                }
                catch (System.Exception)
                {
                }
                str = null;
            }
            else
            {
                _str_PO = _obj_DT.Rows[0]["PO_NO"].ToString().Trim();
                _str_PO_SERIAL = _obj_DT.Rows[0]["PO_SERIAL_NO"].ToString().Trim();
                _obj_DT.Dispose();
                this.obj_DB.dispose_command();
                str = _str_PO_SERIAL;
            }
            return str;
        }

        public string Get_PONumber_By_POSerialNumber(string pStr_POSerial)
        {
            string str;
            DataTable _obj_DT = new DataTable();
            string[,] _arr_str_param = new string[1, 2];
            _arr_str_param[0, 0] = "pPOSerial";
            _arr_str_param[0, 1] = pStr_POSerial;
            _obj_DT.Load(this.obj_Ora_DB_PO.executeQuery("SP_GET_PO_SEL", _arr_str_param, true));
            if (_obj_DT.Rows.Count <= 0)
            {
                try
                {
                    _obj_DT.Dispose();
                }
                catch (System.Exception)
                {
                }
                str = "";
            }
            else
            {
                string PO_NO = _obj_DT.Rows[0]["PROD_ORDER_NO"].ToString().Trim();
                _obj_DT.Dispose();
                this.obj_Ora_DB_PO.dispose_command();
                str = PO_NO;
            }
            return str;
        }

        public string GET_RFID_CODE_FROM_MM_EQUIPMENT_REG(string pStr_RFID_01, string pStr_RFID_02)
        {
            string lastError;
            this.check_component();
            string[,] _arr_str_param = new string[2, 2];
            _arr_str_param[0, 0] = "@str_RFID_01";
            _arr_str_param[0, 1] = pStr_RFID_01;
            _arr_str_param[1, 0] = "@str_RFID_02";
            _arr_str_param[1, 1] = pStr_RFID_02;
            DataTable _obj_DT = new DataTable();
            string _str_return_value = "";
            _obj_DT.Load(this.obj_DB.executeQuery("SP_GET_RFID_CODE_FROM_MM_EQUIPMENT_REG", _arr_str_param, true));
            if (_obj_DT.Rows.Count <= 0)
            {
                _obj_DT.Dispose();
                lastError = this.obj_DB.get_LastError();
            }
            else
            {
                _str_return_value = _obj_DT.Rows[0]["RFID_CODE"].ToString().Trim();
                _obj_DT.Dispose();
                lastError = _str_return_value;
            }
            return lastError;
        }

        public string GET_RFID_CODE_FROM_MM_EQUIPMENT_REG_4_PALLET(string pStr_RFID_01, string pStr_RFID_02)
        {
            string lastError;
            this.check_component();
            string[,] _arr_str_param = new string[2, 2];
            _arr_str_param[0, 0] = "@str_RFID_01";
            _arr_str_param[0, 1] = pStr_RFID_01;
            _arr_str_param[1, 0] = "@str_RFID_02";
            _arr_str_param[1, 1] = pStr_RFID_02;
            DataTable _obj_DT = new DataTable();
            string _str_return_value = "";
            _obj_DT.Load(this.obj_DB.executeQuery("SP_GET_RFID_CODE_FROM_MM_EQUIPMENT_REG_4_PALLET", _arr_str_param, true));
            if (_obj_DT.Rows.Count <= 0)
            {
                _obj_DT.Dispose();
                lastError = this.obj_DB.get_LastError();
            }
            else
            {
                _str_return_value = _obj_DT.Rows[0]["BATCH_ID"].ToString().Trim();
                _obj_DT.Dispose();
                lastError = _str_return_value;
            }
            return lastError;
        }

        public List<RFID_Reader_Setting> Get_RFID_DB_Info(string pStr_UserName, string pStr_Password)
        {
            List<RFID_Reader_Setting> rFIDReaderSettings;
            DataTable _obj_DT = new DataTable();
            _obj_DT.Load(this.obj_DB.executeQuery("sp_GetUserLogin", null, true));
            if (_obj_DT.Rows.Count <= 0)
            {
                try
                {
                    _obj_DT.Dispose();
                }
                catch (System.Exception)
                {
                }
                rFIDReaderSettings = null;
            }
            else
            {
                List<RFID_Reader_Setting> _obj_List = new List<RFID_Reader_Setting>();
                int count = _obj_DT.Rows.Count;
                int _int_iLoop = 0;
                do
                {
                    RFID_Reader_Setting _obj = new RFID_Reader_Setting()
                    {
                        Station_Name = _obj_DT.Rows[_int_iLoop]["Station_Name"].ToString().Trim(),
                        Catagory = _obj_DT.Rows[_int_iLoop]["Catagory"].ToString().Trim(),
                        Exe_Name = _obj_DT.Rows[_int_iLoop]["Application_Name"].ToString().Trim(),
                        IP_Addr = _obj_DT.Rows[_int_iLoop]["IP_Address"].ToString().Trim(),
                        AppPath = _obj_DT.Rows[_int_iLoop]["AppPath"].ToString().Trim()
                    };
                    _obj_List.Add(_obj);
                    _int_iLoop = _int_iLoop + 1;
                } while (_int_iLoop <= count);
                _obj_DT.Dispose();
                this.obj_DB.dispose_command();
                rFIDReaderSettings = _obj_List;
            }
            return rFIDReaderSettings;
        }

        public string GET_RFID_READER_INFO_BY_DEVICE_NAME(string pStr_DeviceName)
        {
            this.check_component();
            DataTable _obj_DT_Info = new DataTable();
            string _str_Info = "";
            string[,] _arr_str_param = new string[1, 2];
            _arr_str_param[0, 0] = "@str_DeviceName";
            _arr_str_param[0, 1] = pStr_DeviceName;
            _obj_DT_Info.Load(this.obj_DB.executeQuery("SP_GET_RFID_READER_INFO_BY_DEVICE_NAME", _arr_str_param, true));
            _str_Info = String.Concat(_obj_DT_Info.Rows[0]["RSSI"].ToString(), "|");
            _str_Info = String.Concat(_str_Info, _obj_DT_Info.Rows[0]["MM_RFID_LOC_AREA"].ToString(), "|");
            _str_Info = String.Concat(_str_Info, _obj_DT_Info.Rows[0]["TAG_COUNT"].ToString(), "|");
            _str_Info = String.Concat(_str_Info, pStr_DeviceName, "|");
            _str_Info = String.Concat(_str_Info, _obj_DT_Info.Rows[0]["RFID_CEL_READER_NAME"].ToString(), "|");
            _str_Info = String.Concat(_str_Info, _obj_DT_Info.Rows[0]["LOCATION_NAME"].ToString(), "|");
            string _str_Path = _obj_DT_Info.Rows[0]["APPL_PATH_URL"].ToString().Trim();
            if (string.Compare(_str_Path.Substring(_str_Path.Length - 1, 1), "\\", StringComparison.Ordinal) != 0)
            {
                _str_Path = String.Concat(_str_Path, "\\");
            }
            _str_Path = String.Concat(_str_Path, _obj_DT_Info.Rows[0]["APPL_NAME"].ToString().Trim());
            _str_Info = String.Concat(_str_Info, _str_Path);
            _obj_DT_Info.Dispose();
            return _str_Info;
        }

        public string GET_RFID_SIGNAL_SETTING(string pStr_DeviceName)
        {
            string _str_return_value = "";
            DataTable _obj_DT = new DataTable();
            string[,] _arr_str_param = new string[1, 2];
            _arr_str_param[0, 0] = "@str_DeviceName";
            _arr_str_param[0, 1] = pStr_DeviceName;
            _obj_DT.Load(this.obj_DB.executeQuery("SP_GET_RFID_SIGNAL_SETTING", _arr_str_param, true));
            if (_obj_DT.Rows.Count > 0)
            {
                _str_return_value = _obj_DT.Rows[0]["RSSI"].ToString().Trim();
                _str_return_value = String.Concat(_str_return_value, "@", _obj_DT.Rows[0]["TAG_COUNT"].ToString().Trim());
            }
            _obj_DT.Dispose();
            return _str_return_value;
        }

        public string Get_RFID_Type_Listing()
        {
            string str;
            string _str_RFID_Type = "";
            DataTable _obj_DT = new DataTable();
            _obj_DT.Load(this.obj_DB.executeQuery("SP_GET_RFID_TYPE_LST", null, true));
            if (_obj_DT.Rows.Count <= 0)
            {
                try
                {
                    _obj_DT.Dispose();
                }
                catch (System.Exception)
                {
                }
                str = null;
            }
            else
            {
                int count = _obj_DT.Rows.Count - 1;
                int _int_iLoop = 0;
                do
                {
                    string[] strArrays = { _str_RFID_Type, "@", _obj_DT.Rows[_int_iLoop]["ID_MM_RFID_TYPE"].ToString().Trim(), "$", _obj_DT.Rows[_int_iLoop]["RFID_TYPE"].ToString().Trim() };
                    _str_RFID_Type = String.Concat(strArrays);
                    _int_iLoop = _int_iLoop + 1;
                } while (_int_iLoop <= count);
                _obj_DT.Dispose();
                this.obj_DB.dispose_command();
                str = _str_RFID_Type;
            }
            return str;
        }

        public string GET_SCANFLAG_BY_DEVICE_NAME(string pStr_DeviceName)
        {
            string _str_ScanFlag = "";
            DataTable _obj_DT = new DataTable();
            string[,] _arr_str_param = new string[1, 2];
            _arr_str_param[0, 0] = "@str_DeviceName";
            _arr_str_param[0, 1] = pStr_DeviceName;
            _obj_DT.Load(this.obj_DB.executeQuery("SP_GET_RFID_LOC_AREA_BY_DEVICENAME", _arr_str_param, true));
            if (_obj_DT.Rows.Count > 0)
            {
                _str_ScanFlag = _obj_DT.Rows[0]["RFID_LOC_AREA"].ToString().Trim();
            }
            _obj_DT.Dispose();
            return _str_ScanFlag;
        }

        public string Get_Staging_Area_Listing()
        {
            string str;
            string _str_Staging_Area = "";
            DataTable _obj_DT = new DataTable();
            _obj_DT.Load(this.obj_DB.executeQuery("SP_GET_STG_AREA_LST", null, true));
            if (_obj_DT.Rows.Count <= 0)
            {
                try
                {
                    _obj_DT.Dispose();
                }
                catch (System.Exception)
                {
                }
                str = null;
            }
            else
            {
                int count = _obj_DT.Rows.Count - 1;
                int _int_iLoop = 0;
                do
                {
                    _str_Staging_Area = String.Concat(_str_Staging_Area, "@", _obj_DT.Rows[_int_iLoop]["LOCATION_NAME"].ToString().Trim());
                    _int_iLoop = _int_iLoop + 1;
                } while (_int_iLoop <= count);
                _obj_DT.Dispose();
                this.obj_DB.dispose_command();
                str = _str_Staging_Area;
            }
            return str;
        }

        public string Get_Staging_Entry_Listing()
        {
            string str;
            string _str_Staging_Entry = "";
            DataTable _obj_DT = new DataTable();
            _obj_DT.Load(this.obj_DB.executeQuery("SP_GET_STG_ENTRY_LST", null, true));
            if (_obj_DT.Rows.Count <= 0)
            {
                try
                {
                    _obj_DT.Dispose();
                }
                catch (System.Exception)
                {
                }
                str = null;
            }
            else
            {
                int count = _obj_DT.Rows.Count - 1;
                int _int_iLoop = 0;
                do
                {
                    _str_Staging_Entry = String.Concat(_str_Staging_Entry, "@", _obj_DT.Rows[_int_iLoop]["RFID_LOC_AREA"].ToString().Trim());
                    _int_iLoop = _int_iLoop + 1;
                } while (_int_iLoop <= count);
                _obj_DT.Dispose();
                this.obj_DB.dispose_command();
                str = _str_Staging_Entry;
            }
            return str;
        }

        public string Get_WIP_RFID_TRANS_INFO(string pStr_RFID_ID)
        {
            string str;
            string _str_PO = "";
            string _str_PO_SERIAL = "";
            DataTable _obj_DT = new DataTable();
            string[,] _arr_str_param = new string[1, 2];
            _arr_str_param[0, 0] = "@pRFID_ID";
            _arr_str_param[0, 1] = pStr_RFID_ID;
            _obj_DT.Load(this.obj_DB.executeQuery("SP_GET_WIP_RFID_TRANS", _arr_str_param, true));
            if (_obj_DT.Rows.Count <= 0)
            {
                try
                {
                    _obj_DT.Dispose();
                }
                catch (System.Exception)
                {
                }
                str = null;
            }
            else
            {
                _str_PO = _obj_DT.Rows[0]["PO_NO"].ToString().Trim();
                _str_PO_SERIAL = _obj_DT.Rows[0]["PO_SERIAL_NO"].ToString().Trim();
                _obj_DT.Dispose();
                this.obj_DB.dispose_command();
                str = _str_PO_SERIAL;
            }
            return str;
        }

        public void OnOff_RFID_Station(string pStr_DeviceName, string pStr_IPAddress, string[] pArr_Str_StationInfo, bool pBol_OnOff)
        {
            string _str_Msg;
            if (!pBol_OnOff)
            {
                _str_Msg = String.Concat(pStr_DeviceName, "|Kill");
            }
            else
            {
                _str_Msg = String.Concat(pStr_IPAddress, "|Wake|");
                string[] pArrStrStationInfo = pArr_Str_StationInfo;
                int num = 0;
                while (num < (int)pArrStrStationInfo.Length)
                {
                    _str_Msg = String.Concat(_str_Msg, pArrStrStationInfo[num], "|");
                    num = num + 1;
                }
                if (_str_Msg.Substring(_str_Msg.Length - 1, 1).Equals("|"))
                {
                    _str_Msg = _str_Msg.Substring(0, _str_Msg.Length - 1);
                }
            }
            TcpClient clientSocket = new TcpClient();
            int _int_iCount = 0;
            do
            {
                try
                {
                    clientSocket.Connect("127.0.0.1", 8888);
                    break;
                }
                catch (System.Exception)
                {
                    Thread.Sleep(1000);
                }
                _int_iCount = _int_iCount + 1;
            } while (_int_iCount <= 100);
            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = Encoding.ASCII.GetBytes(String.Concat(_str_Msg, "$"));
            serverStream.Write(outStream, 0, (int)outStream.Length);
            serverStream.Flush();
            clientSocket.GetStream().Close();
            serverStream.Close();
            clientSocket.Close();
            try
            {
                serverStream.Dispose();
            }
            catch (System.Exception)
            {
            }
            Thread.Sleep(1000);
            this.Reset_RFID_Reader(pStr_IPAddress);
        }

        public bool Reset_RFID_Reader(string pstr_IPAddr)
        {
            bool flag;
            RFIDReader _ReaderAPI = new RFIDReader(pstr_IPAddr, 5084, 0);
            try
            {
                _ReaderAPI.Connect();
                _ReaderAPI.Config.GPO(1).PortState = GPOs.GPO_PORT_STATE.FALSE;
                _ReaderAPI.Config.GPO(2).PortState = GPOs.GPO_PORT_STATE.FALSE;
                _ReaderAPI.Config.GPO(3).PortState = GPOs.GPO_PORT_STATE.FALSE;
                _ReaderAPI.Disconnect();
                _ReaderAPI.Dispose();
                flag = true;
            }
            catch (System.Exception)
            {
                flag = false;
            }
            return flag;
        }

        public string Resolve_HostName_2_IP(string pStr_hostname)
        {
            string str;
            try
            {
                str = Dns.GetHostAddresses(pStr_hostname)[0].ToString();
            }
            catch (System.Exception)
            {
                str = "";
            }
            return str;
        }

        public string RFID_Code_Verification(string pStr_RFID_01, string pStr_RFID_02)
        {
            string lastError;
            DataTable _obj_DT = new DataTable();
            string[,] _arr_str_param = new string[2, 2];
            _arr_str_param[0, 0] = "@str_RFID_01";
            _arr_str_param[0, 1] = pStr_RFID_01;
            _arr_str_param[1, 0] = "@str_RFID_02";
            _arr_str_param[1, 1] = pStr_RFID_02;
            _obj_DT.Load(this.obj_DB.executeQuery("SP_GET_MM_EQUIPMENT_REG_BY_RFID_CODE", _arr_str_param, true));
            if (_obj_DT.Rows.Count > 0)
            {
                _obj_DT.Dispose();
                lastError = "available";
            }
            else if (this.obj_DB.get_LastError().Equals(""))
            {
                _obj_DT.Dispose();
                lastError = "nothing";
            }
            else
            {
                _obj_DT.Dispose();
                lastError = this.obj_DB.get_LastError();
            }
            return lastError;
        }

        public string Send_Error_Notification(string pStr_module, string pStr_proc, string pStr_error)
        {
            string str;
            try
            {
                string[,] _arr_str_param = new string[5, 2];
                _arr_str_param[0, 0] = "p_subject";
                _arr_str_param[0, 1] = "";
                _arr_str_param[1, 0] = "p_prog";
                _arr_str_param[1, 1] = "";
                _arr_str_param[2, 0] = "p_module";
                _arr_str_param[2, 1] = pStr_module;
                _arr_str_param[3, 0] = "p_proc";
                _arr_str_param[3, 1] = pStr_proc;
                _arr_str_param[4, 0] = "p_error";
                _arr_str_param[4, 1] = pStr_error;
                this.obj_Ora_DB_PO.executeActionQuery("SEND_EMAL_ERR_RFID_WIP", _arr_str_param, true).ToString();
                str = "";
            }
            catch (System.Exception exception)
            {
                str = exception.ToString();
            }
            return str;
        }

        public void ShutDown_ALL_RFID_Station()
        {
            Process[] processes = Process.GetProcesses();
            int num = 0;
            while (num < (int)processes.Length)
            {
                Process p = processes[num];
                if (p.ProcessName.ToString().Trim().Equals("PAB_Real_Time_WIP_Console"))
                {
                    p.Kill();
                }
                if (p.ProcessName.ToString().Trim().Equals("PAB_Real_Time_WIP_RFID_Host"))
                {
                    p.Kill();
                }
                num = num + 1;
            }
        }

        public string STAGING_RFID_VERIFIER(string pStr_ID_RFID)
        {
            string[,] _arr_str_param = new string[1, 2];
            _arr_str_param[0, 0] = "@str_RFID";
            _arr_str_param[0, 1] = pStr_ID_RFID;
            DataTable _obj_DT = new DataTable();
            string _str_ReturnValue = "FAIL";
            _obj_DT.Load(this.obj_DB.executeQuery("SP_STAGING_RFID_VERIFIER", _arr_str_param, true));
            if (_obj_DT.Rows.Count > 0)
            {
                _str_ReturnValue = _obj_DT.Rows[0]["RESULT"].ToString().Trim();
            }
            _obj_DT.Dispose();
            return _str_ReturnValue;
        }

        public void UPDATE_MACHINE_OUT_BY_RFID(string pStr_ID_RFID)
        {
            string[,] _arr_str_param = new string[1, 2];
            _arr_str_param[0, 0] = "@str_ID_RFID";
            _arr_str_param[0, 1] = pStr_ID_RFID;
            this.obj_DB.executeActionQuery("SP_UPDATE_MACHINE_OUT_BY_RFID", _arr_str_param, true);
        }

        public string update_OnOff_RFID_Station(string pStr_DeviceName, string pStr_IPAddr, string pStr_UserID, string pStr_Status, string pStr_Option)
        {
            this.check_component();
            string[,] _arr_str_param = new string[5, 2];
            _arr_str_param[0, 0] = "@int_Status";
            _arr_str_param[0, 1] = pStr_Status;
            _arr_str_param[1, 0] = "@str_UserID";
            _arr_str_param[1, 1] = pStr_UserID;
            _arr_str_param[2, 0] = "@str_IPAddress";
            _arr_str_param[2, 1] = pStr_IPAddr;
            _arr_str_param[3, 0] = "@str_DeviceName";
            _arr_str_param[3, 1] = pStr_DeviceName;
            _arr_str_param[4, 0] = "@int_Option";
            _arr_str_param[4, 1] = pStr_Option;
            this.obj_DB.executeActionQuery("SP_RFID_DEVICE_STATUS_UPDATE", _arr_str_param, true);
            return this.obj_DB.get_LastError();
        }

        public string User_ACL_AccessRight(string pStr_UserID)
        {
            string str;
            List<ACL.Object.Resource>.Enumerator enumerator = new List<ACL.Object.Resource>.Enumerator();
            string SystemID = "0";
            string _list = "";
            SystemID = ACL.OracleClass.Resource.RetrieveApplicationIDByName(ConfigurationManager.ConnectionStrings["ORCL_ACL"].ConnectionString, "WIP RFID HANDHELD").ToString();
            ACL.OracleClass.Resource _aclResource = new ACL.OracleClass.Resource(ConfigurationManager.ConnectionStrings["ORCL_ACL"].ConnectionString);
            List<ACL.Object.Resource> _sourcelist = _aclResource.RetrieveResource(Convert.ToInt32(pStr_UserID), Convert.ToInt32(SystemID));
            try
            {
                enumerator = Search.GetParent(_sourcelist, Convert.ToInt32(SystemID)).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ACL.Object.Resource itm = enumerator.Current;
                    _list = String.Concat(_list, "@", itm.ResouceDesc);
                }
            }
            finally
            {
                ((IDisposable)enumerator).Dispose();
            }
            str = string.Compare(_list, "", StringComparison.Ordinal) == 0 ? "Acess Fail" : _list;
            return str;
        }

        public string User_ACL_Login(string pStr_UserName, string pStr_Password)
        {
            string str;
            string SystemID = "0";
            SystemID = ACL.OracleClass.Resource.RetrieveApplicationIDByName(ConfigurationManager.ConnectionStrings["ORCL_ACL"].ConnectionString, "WIP RFID HANDHELD").ToString();
            ACL.OracleClass.User _aclUser = new ACL.OracleClass.User(ConfigurationManager.ConnectionStrings["ORCL_ACL"].ConnectionString);
            ACL.Object.User userobj = new ACL.Object.User();
            userobj = _aclUser.validateWithRetrieveUser(pStr_UserName.Trim(), pStr_Password.Trim(), Convert.ToInt32(SystemID));
            str = string.Compare(userobj.UserID, "", StringComparison.Ordinal) == 0 ? "Login Fail" : userobj.UserID;
            return str;
        }

        public string user_login(string pStr_UserName, string pStr_Password)
        {
            this.check_component();
            string[,] _arr_str_param = new string[3, 3];
            _arr_str_param[0, 0] = "@str_UserName";
            _arr_str_param[0, 1] = pStr_UserName;
            _arr_str_param[1, 0] = "@str_Password";
            _arr_str_param[1, 1] = pStr_Password;
            DataTable _obj_DT = new DataTable();
            _obj_DT.Load(this.obj_DB.executeQuery("sp_GetUserLogin", _arr_str_param, true));
            return _obj_DT.Rows.Count <= 0 ? "" : "UserID";
        }

        public string WIP_RFID_Transaction(string pStr_PO_Serial_Number, string pStr_RFID_Cel_Reader_Name, string pStr_DeviceIP, string pStr_Scan_Flag, string pStr_HW_Device, string pStr_RFID_Number, short pInt_Record_Type, string pStr_UserID, string pStr_Option)
        {
            string str;
            try
            {
                string[,] _arr_str_param = new string[3, 3];
                string _str_PO_Number = "";
                if (string.Compare(pStr_HW_Device, "WEB", StringComparison.Ordinal) != 0)
                {
                    _arr_str_param[0, 0] = "@str_RFID_Number";
                    _arr_str_param[0, 1] = pStr_RFID_Number;
                    _arr_str_param[1, 0] = "@str_PO_Serial_Number";
                    _arr_str_param[1, 1] = pStr_PO_Serial_Number;
                    DataTable _obj_DT = new DataTable();
                    _obj_DT.Load(this.obj_DB.executeQuery("sp_check_Grey_Receiving_by_RFID_n_PO_Serial", _arr_str_param, true));
                    if (_obj_DT.Rows.Count <= 0)
                    {
                        try
                        {
                            _obj_DT.Dispose();
                        }
                        catch (System.Exception)
                        {
                        }
                    }
                    else
                    {
                        try
                        {
                            _obj_DT.Dispose();
                        }
                        catch (System.Exception)
                        {
                        }
                        if (string.Compare(pStr_HW_Device, "HANDHELD", StringComparison.Ordinal) != 0)
                        {
                            str = "";
                            return str;
                        }
                        else
                        {
                            str = "Data just added is existed.";
                            return str;
                        }
                    }
                }
                else
                {
                    _str_PO_Number = "";
                }
                // Resize the array from [3,3] to [10,3]
                string[,] _arr_str_param_new = new string[10, 3];
                int bound0 = _arr_str_param.GetLength(0);
                int bound1 = _arr_str_param.GetLength(1);
                for (int i = 0; i < bound0; i++)
                {
                    for (int j = 0; j < bound1; j++)
                    {
                        _arr_str_param_new[i, j] = _arr_str_param[i, j];
                    }
                }
                _arr_str_param = _arr_str_param_new;
                _arr_str_param[0, 0] = "@str_PO_Number";
                _arr_str_param[0, 1] = _str_PO_Number;
                _arr_str_param[1, 0] = "@str_PO_Serial_Number";
                _arr_str_param[1, 1] = pStr_PO_Serial_Number;
                _arr_str_param[2, 0] = "@str_RFID_Cel_Reader_Name";
                _arr_str_param[2, 1] = pStr_RFID_Cel_Reader_Name;
                _arr_str_param[3, 0] = "@str_Scan_Flag";
                _arr_str_param[3, 1] = pStr_Scan_Flag;
                _arr_str_param[4, 0] = "@str_HW_Device";
                _arr_str_param[4, 1] = pStr_HW_Device;
                _arr_str_param[5, 0] = "@str_RFID_Number";
                _arr_str_param[5, 1] = pStr_RFID_Number;
                _arr_str_param[6, 0] = "@int_Record_Type";
                _arr_str_param[6, 1] = pInt_Record_Type.ToString();
                _arr_str_param[7, 0] = "@str_UserID";
                _arr_str_param[7, 1] = pStr_UserID;
                _arr_str_param[8, 0] = "@str_Option";
                _arr_str_param[8, 1] = pStr_Option;
                this.obj_DB.executeActionQuery("sp_Insert_Grey_Receiving_by_RFID_n_PO_Serial", _arr_str_param, true);
                str = "";
            }
            catch (System.Exception exception2)
            {
                str = exception2.ToString();
            }
            return str;
        }
    }
}
