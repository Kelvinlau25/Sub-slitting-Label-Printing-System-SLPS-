Imports ACL
Imports ACL.[Object]
Imports ACL.OracleClass
Imports cls_DB
Imports Microsoft.VisualBasic.CompilerServices
Imports Symbol.RFID3
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Configuration
Imports System.Data
Imports System.Diagnostics
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading

Namespace WIP_Process
	Public Class cls_WIP_Process
		Private obj_DB As cls_MSSQL

		Private obj_Ora_DB_ACL As cls_Oracle

		Private obj_Ora_DB_PO As cls_Oracle

		Private obj_Ora_DB_WIP As cls_Oracle

		Private obj_Ora_DB_SCH As cls_Oracle

		Private Str_DB_Tag_or_ConnStr As String

		Private str_WIP_DB_ConnStr As String

		Private str_ORACLE_ACL_ConnStr As String

		Private str_ORACLE_PO_ConnStr As String

		Private str_ORACLE_WIP_ConnStr As String

		Private str_ORACLE_SCH_ConnStr As String

		Public Sub New(ByVal pstr_WIP_DB_ConnStr As String, ByVal pstr_ORACLE_ACL_ConnStr As String, ByVal pstr_ORACLE_PO_ConnStr As String, ByVal pstr_ORACLE_WIP_ConnStr As String, ByVal pstr_ORACLE_SCH_ConnStr As String)
			MyBase.New()
			Me.str_WIP_DB_ConnStr = pstr_WIP_DB_ConnStr
			Me.str_ORACLE_ACL_ConnStr = pstr_ORACLE_ACL_ConnStr
			Me.str_ORACLE_PO_ConnStr = pstr_ORACLE_PO_ConnStr
			Me.str_ORACLE_WIP_ConnStr = pstr_ORACLE_WIP_ConnStr
			Me.str_ORACLE_SCH_ConnStr = pstr_ORACLE_SCH_ConnStr
			Me.check_component()
		End Sub

		Public Sub Active_Engine_Only()
			Dim _str_ConsoleApp_Path As String = String.Concat(AppDomain.CurrentDomain.BaseDirectory, "Backend_App\Engine\PAB_Real_Time_WIP_Console.exe")
			Me.Check_Process("PAB_Real_Time_WIP_Console")
			Process.Start(_str_ConsoleApp_Path, "")
		End Sub

		Public Function ActiveAll_RFID_Reader() As String
			Dim str As String
			Try
				Dim processes As Process() = Process.GetProcesses()
				Dim num As Integer = 0
				While num < CInt(processes.Length)
					Dim p As Process = processes(num)
					If (p.ProcessName.ToString().Trim().Equals("PAB_Real_Time_WIP_Console.exe")) Then
						p.Kill()
					End If
					If (p.ProcessName.ToString().Trim().Equals("PAB_Real_Time_WIP_RFID_Host.exe")) Then
						p.Kill()
					End If
					num = num + 1
				End While
				Dim _str_path As String = Directory.GetCurrentDirectory()
				Process.Start(String.Concat(_str_path, "\Backend_App\Engine\PAB_Real_Time_WIP_Console.exe"))
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str = exception.ToString()
				ProjectData.ClearProjectError()
				Return str
			End Try
			str = ""
			Return str
		End Function

		Public Function AddEquipmentRegistration(ByVal pStr_UserID As String, ByVal pStr_RFID As String, ByVal pStr_Equip_Name As String, ByVal pStr_Batch_ID As String, ByVal pStr_RFID_Type As String, ByVal pStr_UserLoc As String) As String
			Dim str As String
			Try
				Dim _arr_str_param(5, 1) As String
				_arr_str_param(0, 0) = "@pStr_Equip_Name"
				_arr_str_param(0, 1) = pStr_Equip_Name
				_arr_str_param(1, 0) = "@pStr_RFID_Type"
				_arr_str_param(1, 1) = pStr_RFID_Type
				_arr_str_param(2, 0) = "@pStr_Batch_ID"
				_arr_str_param(2, 1) = pStr_Batch_ID
				_arr_str_param(3, 0) = "@pStr_RFID"
				_arr_str_param(3, 1) = pStr_RFID
				_arr_str_param(4, 0) = "@pUserID"
				_arr_str_param(4, 1) = pStr_UserID
				_arr_str_param(5, 0) = "@pUserLoc"
				_arr_str_param(5, 1) = pStr_UserLoc
				Me.obj_DB.executeActionQuery("PS_AddEquipmentRegistration", _arr_str_param, True)
				str = ""
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str = exception.ToString()
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Function AddNew_WIP_RFID_TRANS_By_RFID_Reader(ByVal pStr_RFID_CEL_READER_NAME As String, ByVal pStr_ID_RFID As String, ByVal pStr_IPADDR As String, ByVal pStr_LOCATION_NAME As String, ByVal pStr_RFID_LOC_AREA As String) As String
			Dim str As String
			Try
				Dim _arr_str_param(4, 1) As String
				_arr_str_param(0, 0) = "@str_RFID_CEL_READER_NAME"
				_arr_str_param(0, 1) = pStr_RFID_CEL_READER_NAME
				_arr_str_param(1, 0) = "@str_ID_RFID"
				_arr_str_param(1, 1) = pStr_ID_RFID
				_arr_str_param(2, 0) = "@str_IPADDR"
				_arr_str_param(2, 1) = pStr_IPADDR
				_arr_str_param(3, 0) = "@str_LOCATION_NAME"
				_arr_str_param(3, 1) = pStr_LOCATION_NAME
				_arr_str_param(4, 0) = "@str_RFID_LOC_AREA"
				_arr_str_param(4, 1) = pStr_RFID_LOC_AREA
				Dim _obj_DT As DataTable = New DataTable()
				_obj_DT.Load(Me.obj_DB.executeQuery("ps_AddNew_WIP_RFID_TRANS_By_RFID_Reader", _arr_str_param, True))
				Dim _str_Date As String = _obj_DT.Rows(0)("RETURN_VALUE").ToString().Trim()
				_obj_DT.Dispose()
				str = _str_Date
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str = ""
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Function AddNewBleaching(ByVal pStr_RFID_01 As String, ByVal pStr_RFID_02 As String, ByVal pStr_DeviceName As String, ByVal pStr_LOCATION_NAME As String, ByVal pStr_SCAN_FLAG As String, ByVal pStr_HW_DEVICE As String, ByVal pStr_UserID As String, ByVal pStr_IPAddr As String, ByVal pInt_YARDAGE As Integer, ByVal pStr_BatchID As String) As String
			Dim str As String
			Try
				If (pStr_SCAN_FLAG.ToLower().Equals("machine in")) Then
					Dim _str_DMS_Sch As String = ""
					_str_DMS_Sch = Me.CheckSchMch(Me.Get_PO_SERAIL_BY_BATCHID(pStr_BatchID), Me.GET_ID_MM_MACHINE_BY_LOCATION_NAME(pStr_LOCATION_NAME))
					If (Not _str_DMS_Sch.Equals("FOLLOW SCHEDULE")) Then
						str = _str_DMS_Sch
						Return str
					End If
				End If
				Dim _obj_DT As DataTable = New DataTable()
				Dim _arr_str_param(6, 1) As String
				_arr_str_param(0, 0) = "@str_DeviceName"
				_arr_str_param(0, 1) = pStr_DeviceName
				_arr_str_param(1, 0) = "@str_LOCATION_NAME"
				_arr_str_param(1, 1) = pStr_LOCATION_NAME
				_arr_str_param(2, 0) = "@str_SCAN_FLAG"
				_arr_str_param(2, 1) = pStr_SCAN_FLAG
				_arr_str_param(3, 0) = "@str_HW_DEVICE"
				_arr_str_param(3, 1) = pStr_HW_DEVICE
				_arr_str_param(4, 0) = "@str_UserID"
				_arr_str_param(4, 1) = pStr_UserID
				_arr_str_param(5, 0) = "@str_IPAddr"
				_arr_str_param(5, 1) = pStr_IPAddr
				_arr_str_param(6, 0) = "@str_Batch"
				_arr_str_param(6, 1) = pStr_BatchID
				_obj_DT.Load(Me.obj_DB.executeQuery("SP_ADDNEW_MACHINE_OUT", _arr_str_param, True))
				Dim _str_return_value As String = Me.obj_DB.get_LastError()
				If (_str_return_value.Equals("")) Then
					_str_return_value = _obj_DT.Rows(0)("INSERT_STATUS").ToString()
					_obj_DT.Dispose()
					str = _str_return_value
				Else
					_obj_DT.Dispose()
					str = _str_return_value
				End If
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str = exception.ToString()
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Function AddNewGrey(ByVal pStr_UserID As String, ByVal pStr_POSerial As String, ByVal pInt_Yardage As Integer, ByVal pStr_NxtProc As String, ByVal pStr_RFID_1 As String, ByVal pStr_RFID_2 As String, ByVal pStr_LocName As String, ByVal pStr_IPAddr As String, ByVal pStr_Shift As String, ByVal pStr_DeviceType As String, ByVal pStr_DeviceName As String, ByVal pStr_Batch As String) As String
			Dim str As String
			Try
				Dim PO_No As String = ""
				PO_No = Me.Get_PONumber_By_POSerialNumber(pStr_POSerial)
				If (PO_No.Equals("")) Then
					str = "NOPO"
				ElseIf (Me.CheckPoQty(pStr_POSerial, pInt_Yardage).Equals("OK")) Then
					Dim _obj_DT As DataTable = New DataTable()
					Dim _arr_str_param(11, 1) As String
					_arr_str_param(0, 0) = "@str_PO_No"
					_arr_str_param(0, 1) = PO_No
					_arr_str_param(1, 0) = "@str_POSerial"
					_arr_str_param(1, 1) = pStr_POSerial
					_arr_str_param(2, 0) = "@str_LocName"
					_arr_str_param(2, 1) = pStr_LocName
					_arr_str_param(3, 0) = "@str_HW_Device"
					_arr_str_param(3, 1) = pStr_DeviceType
					_arr_str_param(4, 0) = "@str_RFID_01"
					_arr_str_param(4, 1) = pStr_RFID_1
					_arr_str_param(5, 0) = "@str_RFID_02"
					_arr_str_param(5, 1) = pStr_RFID_2
					_arr_str_param(6, 0) = "@str_UserID"
					_arr_str_param(6, 1) = pStr_UserID
					_arr_str_param(7, 0) = "@str_IPAddr"
					_arr_str_param(7, 1) = pStr_IPAddr
					_arr_str_param(8, 0) = "@int_YARDAGE"
					_arr_str_param(8, 1) = Conversions.ToString(pInt_Yardage)
					_arr_str_param(9, 0) = "@str_DeviceName"
					_arr_str_param(9, 1) = pStr_DeviceName
					_arr_str_param(10, 0) = "@str_NEXT_PROCESS"
					_arr_str_param(10, 1) = pStr_NxtProc
					_arr_str_param(11, 0) = "@str_Batch"
					_arr_str_param(11, 1) = pStr_Batch
					Me.obj_DB.Begin_Trans()
					_obj_DT.Load(Me.obj_DB.executeQuery("PS_AddNewGrey", _arr_str_param, True))
					Dim _str_return_value As String = Me.obj_DB.get_LastError()
					If (_str_return_value.Equals("")) Then
						_str_return_value = _obj_DT.Rows(0)("INSERT_STATUS").ToString()
						If (_str_return_value.Trim().ToLower().Equals("success")) Then
							If (Not Me.AddNewWIPS_Greyroom_Trans(pStr_POSerial, Conversions.ToInteger(pStr_NxtProc), pStr_Batch, pInt_Yardage, pStr_LocName, pStr_Shift, pStr_UserID, pStr_IPAddr).ToLower().Equals("fail")) Then
								Try
									Me.obj_DB.Commit_Trans()
								Catch exception As System.Exception
									ProjectData.SetProjectError(exception)
									ProjectData.ClearProjectError()
								End Try
							Else
								Try
									Me.obj_DB.Rollback_Trans()
								Catch exception1 As System.Exception
									ProjectData.SetProjectError(exception1)
									ProjectData.ClearProjectError()
								End Try
							End If
						End If
						_obj_DT.Dispose()
						str = _str_return_value
					Else
						_obj_DT.Dispose()
						str = _str_return_value
					End If
				Else
					str = "OverLimit"
				End If
			Catch exception3 As System.Exception
				ProjectData.SetProjectError(exception3)
				Dim ex As System.Exception = exception3
				Try
					Me.obj_DB.Rollback_Trans()
				Catch exception2 As System.Exception
					ProjectData.SetProjectError(exception2)
					ProjectData.ClearProjectError()
				End Try
				str = ex.ToString()
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Function AddNewStaging(ByVal pStr_RFID_01 As String, ByVal pStr_RFID_02 As String, ByVal pStr_DeviceName As String, ByVal pStr_LOCATION_NAME As String, ByVal pStr_SCAN_FLAG As String, ByVal pStr_HW_DEVICE As String, ByVal pStr_UserID As String, ByVal pStr_IPAddr As String, ByVal pStr_BatchID As String) As String
			Dim str As String
			Try
				Dim _obj_DT As DataTable = New DataTable()
				Dim _arr_str_param(9, 1) As String
				_arr_str_param(0, 0) = "@str_RFID_01"
				_arr_str_param(0, 1) = pStr_RFID_01
				_arr_str_param(1, 0) = "@str_RFID_02"
				_arr_str_param(1, 1) = pStr_RFID_02
				_arr_str_param(2, 0) = "@str_DeviceName"
				_arr_str_param(2, 1) = pStr_DeviceName
				_arr_str_param(3, 0) = "@str_LOCATION_NAME"
				_arr_str_param(3, 1) = pStr_LOCATION_NAME
				_arr_str_param(4, 0) = "@str_SCAN_FLAG"
				_arr_str_param(4, 1) = pStr_SCAN_FLAG
				_arr_str_param(5, 0) = "@str_HW_DEVICE"
				_arr_str_param(5, 1) = pStr_HW_DEVICE
				_arr_str_param(6, 0) = "@str_UserID"
				_arr_str_param(6, 1) = pStr_UserID
				_arr_str_param(7, 0) = "@str_IPAddr"
				_arr_str_param(7, 1) = pStr_IPAddr
				_arr_str_param(8, 0) = "@int_YARDAGE"
				_arr_str_param(8, 1) = "0"
				_arr_str_param(9, 0) = "@str_Batch"
				_arr_str_param(9, 1) = pStr_BatchID
				_obj_DT.Load(Me.obj_DB.executeQuery("SP_ADDNEW_MACHINE_IN", _arr_str_param, True))
				Dim _str_return_value As String = Me.obj_DB.get_LastError()
				If (_str_return_value.Equals("")) Then
					_str_return_value = _obj_DT.Rows(0)("INSERT_STATUS").ToString()
					_obj_DT.Dispose()
					str = _str_return_value
				Else
					_obj_DT.Dispose()
					str = _str_return_value
				End If
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str = exception.ToString()
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Function AddNewWIPS_BDPF_Trans(ByVal pStr_POSerial As String, ByVal pInt_Qty As Integer, ByVal pStr_NxtProc As Integer, ByVal pStr_BatchOut As String, ByVal pStr_ShiftGroup As String, ByVal pStr_Ref_ID As String, ByVal pStr_UserID As String, ByVal pStr_UserLoc As String) As String
			Dim str As String
			Try
				Dim _arr_str_param(7, 1) As String
				_arr_str_param(0, 0) = "pPOSerial"
				_arr_str_param(0, 1) = pStr_POSerial
				_arr_str_param(1, 0) = "pYardage"
				_arr_str_param(1, 1) = Conversions.ToString(pInt_Qty)
				_arr_str_param(2, 0) = "pToProcess"
				_arr_str_param(2, 1) = Conversions.ToString(pStr_NxtProc)
				_arr_str_param(3, 0) = "pBatchOut"
				_arr_str_param(3, 1) = pStr_BatchOut
				_arr_str_param(4, 0) = "pShiftGroup"
				_arr_str_param(4, 1) = pStr_ShiftGroup
				_arr_str_param(5, 0) = "pRefID"
				_arr_str_param(5, 1) = pStr_Ref_ID
				_arr_str_param(6, 0) = "pUserID"
				_arr_str_param(6, 1) = pStr_UserID
				_arr_str_param(7, 0) = "pUserLoc"
				_arr_str_param(7, 1) = pStr_UserLoc
				Dim return_value As String = Me.obj_Ora_DB_WIP.executeActionQuery("SP_Add_Grey_WIPS_Trans", _arr_str_param, True).ToString()
				str = If(Conversions.ToDouble(return_value) <= 0, "Fail", return_value)
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str = exception.ToString()
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Function AddNewWIPS_Greyroom_Trans(ByVal pStr_POSerial As String, ByVal pStr_NxtProc As Integer, ByVal pStr_BatchOut As String, ByVal pInt_Qty As Integer, ByVal pStr_LocName As String, ByVal pStr_ShiftGroup As String, ByVal pStr_UserID As String, ByVal pStr_UserLoc As String) As String
			Dim str As String
			Try
				Dim _arr_str_param(7, 1) As String
				_arr_str_param(0, 0) = "pPOSerial"
				_arr_str_param(0, 1) = pStr_POSerial
				_arr_str_param(1, 0) = "pToProcess"
				_arr_str_param(1, 1) = Conversions.ToString(pStr_NxtProc)
				_arr_str_param(2, 0) = "pBatchOut"
				_arr_str_param(2, 1) = pStr_BatchOut
				_arr_str_param(3, 0) = "pYardage"
				_arr_str_param(3, 1) = Conversions.ToString(pInt_Qty)
				_arr_str_param(4, 0) = "pLocName"
				_arr_str_param(4, 1) = pStr_LocName
				_arr_str_param(5, 0) = "pShiftGroup"
				_arr_str_param(5, 1) = pStr_ShiftGroup
				_arr_str_param(6, 0) = "pUserID"
				_arr_str_param(6, 1) = pStr_UserID
				_arr_str_param(7, 0) = "pUserLoc"
				_arr_str_param(7, 1) = pStr_UserLoc
				Dim return_value As String = Me.obj_Ora_DB_WIP.executeActionQuery("SP_Add_Grey_Room_Trans", _arr_str_param, True).ToString()
				If (Conversions.ToDouble(return_value) <= 0) Then
					str = "Fail"
				Else
					Me.AddNewWIPS_BDPF_Trans(pStr_POSerial, pInt_Qty, pStr_NxtProc, pStr_BatchOut, pStr_ShiftGroup, return_value, pStr_UserID, pStr_UserLoc)
					str = return_value
				End If
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str = exception.ToString()
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Function AddRFID_Usage(ByVal pID_Section As String, ByVal pID_Machine As String, ByVal pPOSerial As String, ByVal pModule As String, ByVal pModuleDesc As String, ByVal pDevice As String, ByVal pGroupNo As String, ByVal pStr_UserID As String, ByVal pStr_UserLoc As String) As String
			Dim str As String
			Try
				Dim _arr_str_param(8, 1) As String
				_arr_str_param(0, 0) = "@pID_Section"
				_arr_str_param(0, 1) = pID_Section
				_arr_str_param(1, 0) = "@pID_Machine"
				_arr_str_param(1, 1) = pID_Machine
				_arr_str_param(2, 0) = "@pPOSerial"
				_arr_str_param(2, 1) = pPOSerial
				_arr_str_param(3, 0) = "@pModule"
				_arr_str_param(3, 1) = pModule
				_arr_str_param(4, 0) = "@pModuleDesc"
				_arr_str_param(4, 1) = pModuleDesc
				_arr_str_param(5, 0) = "@pDevice"
				_arr_str_param(5, 1) = pDevice
				_arr_str_param(6, 0) = "@pGroupNo"
				_arr_str_param(6, 1) = pGroupNo
				_arr_str_param(7, 0) = "@pUserID"
				_arr_str_param(7, 1) = pStr_UserID
				_arr_str_param(8, 0) = "@pUserLoc"
				_arr_str_param(8, 1) = pStr_UserLoc
				Me.obj_DB.executeActionQuery("PS_AddRFID_Usage", _arr_str_param, True)
				str = ""
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str = exception.ToString()
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Private Sub check_component()
			If (Me.obj_DB Is Nothing) Then
				If (Not Me.str_WIP_DB_ConnStr.Equals("")) Then
					Me.obj_DB = New cls_MSSQL(Me.str_WIP_DB_ConnStr)
				End If
			End If
			If (Me.obj_Ora_DB_ACL Is Nothing) Then
				If (Not Me.str_ORACLE_ACL_ConnStr.Equals("")) Then
					Me.obj_Ora_DB_ACL = New cls_Oracle(Me.str_ORACLE_ACL_ConnStr)
				End If
			End If
			If (Me.obj_Ora_DB_PO Is Nothing) Then
				If (Not Me.str_ORACLE_PO_ConnStr.Equals("")) Then
					Me.obj_Ora_DB_PO = New cls_Oracle(Me.str_ORACLE_PO_ConnStr)
				End If
			End If
			If (Me.obj_Ora_DB_WIP Is Nothing) Then
				If (Not Me.str_ORACLE_WIP_ConnStr.Equals("")) Then
					Me.obj_Ora_DB_WIP = New cls_Oracle(Me.str_ORACLE_WIP_ConnStr)
				End If
			End If
			If (Me.obj_Ora_DB_SCH Is Nothing) Then
				If (Not Me.str_ORACLE_SCH_ConnStr.Equals("")) Then
					Me.obj_Ora_DB_SCH = New cls_Oracle(Me.str_ORACLE_SCH_ConnStr)
				End If
			End If
		End Sub

		Public Function Check_Engine_Availability() As Boolean
			Dim _bol_Check As Boolean = False
			Dim processes As Process() = Process.GetProcesses()
			Dim num As Integer = 0
			While num < CInt(processes.Length)
				If (Not processes(num).ProcessName.ToString().Trim().Equals("PAB_Real_Time_WIP_Console")) Then
					num = num + 1
				Else
					_bol_Check = True
					Exit While
				End If
			End While
			Return _bol_Check
		End Function

		Private Sub Check_Process(ByVal pStr_AppName As String)
			Dim processes As Process() = Process.GetProcesses()
			Dim num As Integer = 0
			While num < CInt(processes.Length)
				Dim _obj_processlist As Process = processes(num)
				If (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(pStr_AppName, _obj_processlist.ProcessName.ToString().Trim(), False) <> 0) Then
					num = num + 1
				Else
					_obj_processlist.Kill()
					Exit While
				End If
			End While
		End Sub

		Public Function Check_RFID_Station(ByVal pStr_DeviceName As String) As Boolean
			Dim _str_Msg As String = String.Concat(pStr_DeviceName, "|Check")
			Dim _bol_Check As Boolean = False
			Dim clientSocket As TcpClient = New TcpClient()
			Dim _int_iCount As Integer = 0
			Do
				Try
					clientSocket.Connect("127.0.0.1", 8888)
					Exit Do
				Catch exception As System.Exception
					ProjectData.SetProjectError(exception)
					Thread.Sleep(1000)
					ProjectData.ClearProjectError()
				End Try
				_int_iCount = _int_iCount + 1
			Loop While _int_iCount <= 100
			Dim serverStream As NetworkStream = clientSocket.GetStream()
			Dim outStream As Byte() = Encoding.ASCII.GetBytes(String.Concat(_str_Msg, "$"))
			serverStream.Write(outStream, 0, CInt(outStream.Length))
			serverStream.Flush()
			Dim _str_ReturnValue As String = ""
			Dim _byte_bytesFrom(10025) As Byte
			serverStream.Read(_byte_bytesFrom, 0, Convert.ToInt32(clientSocket.ReceiveBufferSize))
			_str_ReturnValue = Encoding.ASCII.GetString(_byte_bytesFrom)
			_str_ReturnValue = _str_ReturnValue.Substring(0, _str_ReturnValue.IndexOf("$"))
			serverStream.Flush()
			_bol_Check = If(Not _str_ReturnValue.Trim().ToLower().Equals("active"), False, True)
			serverStream.Close()
			clientSocket.GetStream().Close()
			clientSocket.Close()
			Try
				serverStream.Dispose()
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				ProjectData.ClearProjectError()
			End Try
			Return _bol_Check
		End Function

		Public Function CheckPoQty(ByVal pStr_POSerial As String, ByVal pInt_Qty As Integer) As String
			Dim str As String
			Try
				Me.check_component()
				Dim _arr_str_param(1, 1) As String
				_arr_str_param(0, 0) = "pPOSerial"
				_arr_str_param(0, 1) = pStr_POSerial
				_arr_str_param(1, 0) = "pYardage"
				_arr_str_param(1, 1) = Conversions.ToString(pInt_Qty)
				str = If(Conversions.ToDouble(Me.obj_Ora_DB_WIP.executeActionQuery("SP_GRAY_CHECK_PO_QTY", _arr_str_param, True).ToString()) <= 0, "OK", "PO Qty Over Limit")
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str = exception.ToString()
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Function CheckSchMch(ByVal pStr_POSerial As String, ByVal pStr_IDMachine As String) As String
			Dim str As String
			Try
				Me.check_component()
				Dim _arr_str_param(2, 1) As String
				_arr_str_param(0, 0) = "pPOSerial"
				_arr_str_param(0, 1) = pStr_POSerial
				_arr_str_param(1, 0) = "pIDMachine"
				_arr_str_param(1, 1) = pStr_IDMachine
				_arr_str_param(2, 0) = "pSection"
				_arr_str_param(2, 1) = "2"
				Dim return_value As String = Me.obj_Ora_DB_SCH.executeActionQuery("SP_CHECK_SCH_MCH", _arr_str_param, True).ToString()
				If (Conversions.ToDouble(return_value) <> 1) Then
					str = If(Conversions.ToDouble(return_value) <> 2, "Invalid SCHEDULE", "ALREADY START")
				Else
					str = "FOLLOW SCHEDULE"
				End If
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str = exception.ToString()
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Function Chk_DeCom(ByVal pStr_RFID As String) As String
			Dim str As String
			Try
				Dim _obj_DT As DataTable = New DataTable()
				Dim _arr_str_param(0, 1) As String
				_arr_str_param(0, 0) = "@str_RFID"
				_arr_str_param(0, 1) = pStr_RFID
				_obj_DT.Load(Me.obj_DB.executeQuery("SP_CHECK_DECOM", _arr_str_param, True))
				Dim _str_return_value As String = Me.obj_DB.get_LastError()
				If (_str_return_value.Equals("")) Then
					If (_obj_DT.Rows.Count <= 0) Then
						_str_return_value = "Unknown RFID"
					Else
						_str_return_value = _obj_DT.Rows(0)("BATCH_ID").ToString()
						If (_str_return_value.Equals("")) Then
							_str_return_value = "Unknown RFID"
						End If
					End If
					_obj_DT.Dispose()
					str = _str_return_value
				Else
					_obj_DT.Dispose()
					str = _str_return_value
				End If
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str = exception.ToString()
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Function DeCom(ByVal pStr_UserID As String, ByVal pStr_RFID As String, ByVal pStr_IPAddr As String) As String
			Dim str As String
			Try
				Dim _obj_DT As DataTable = New DataTable()
				Dim _arr_str_param(2, 1) As String
				_arr_str_param(0, 0) = "@str_RFID"
				_arr_str_param(0, 1) = pStr_RFID
				_arr_str_param(1, 0) = "@str_IPAddr"
				_arr_str_param(1, 1) = pStr_IPAddr
				_arr_str_param(2, 0) = "@str_UserID"
				_arr_str_param(2, 1) = pStr_UserID
				_obj_DT.Load(Me.obj_DB.executeQuery("SP_DECOM", _arr_str_param, True))
				Dim _str_return_value As String = Me.obj_DB.get_LastError()
				If (_str_return_value.Equals("")) Then
					_str_return_value = _obj_DT.Rows(0)("DECOM").ToString()
					_obj_DT.Dispose()
					str = _str_return_value
				Else
					_obj_DT.Dispose()
					str = _str_return_value
				End If
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str = exception.ToString()
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Function Equip_Reg_Confirm(ByVal pStr_EQUIPMENT_NAME As String, ByVal pStr_RFID_TYPE As String, ByVal pStr_RFID_01 As String, ByVal pStr_RFID_02 As String, ByVal pStr_IPAddr As String, ByVal pStr_UserID As String, ByVal pStr_BATCH As String, ByVal pStr_ID_MM_EQUIPMENT_REG As String) As String
			Me.check_component()
			Dim _arr_str_param(8, 1) As String
			_arr_str_param(0, 0) = "@str_EQUIPMENT_NAME"
			_arr_str_param(0, 1) = pStr_EQUIPMENT_NAME
			_arr_str_param(1, 0) = "@str_RFID_TYPE"
			_arr_str_param(1, 1) = pStr_RFID_TYPE
			_arr_str_param(2, 0) = "@str_RFID_01"
			_arr_str_param(2, 1) = pStr_RFID_01
			_arr_str_param(3, 0) = "@str_RFID_02"
			_arr_str_param(3, 1) = pStr_RFID_02
			_arr_str_param(4, 0) = "@str_IPAddr"
			_arr_str_param(4, 1) = pStr_IPAddr
			_arr_str_param(5, 0) = "@str_BATCH"
			_arr_str_param(5, 1) = pStr_BATCH
			_arr_str_param(6, 0) = "@str_UserID"
			_arr_str_param(6, 1) = pStr_UserID
			_arr_str_param(7, 0) = "@int_ID_MM_EQUIPMENT_REG"
			_arr_str_param(7, 1) = pStr_ID_MM_EQUIPMENT_REG
			_arr_str_param(8, 0) = "@str_Option"
			_arr_str_param(8, 1) = "Equip_Reg_Confirm"
			Dim _obj_DT As DataTable = New DataTable()
			Dim _str_return_value As String = ""
			_obj_DT.Load(Me.obj_DB.executeQuery("SP_EQUIPMENT_REGISTRATION", _arr_str_param, True))
			_str_return_value = Me.obj_DB.get_LastError()
			Return If(_str_return_value.Equals(""), "Success", _str_return_value)
		End Function

		Public Function Equip_Reg_Generate_BatchNo(ByVal pStr_EQUIPMENT_NAME As String, ByVal pStr_RFID_TYPE As String, ByVal pStr_RFID_01 As String, ByVal pStr_RFID_02 As String, ByVal pStr_IPAddr As String, ByVal pStr_UserID As String) As String
			Me.check_component()
			Dim _arr_str_param(8, 1) As String
			_arr_str_param(0, 0) = "@str_EQUIPMENT_NAME"
			_arr_str_param(0, 1) = pStr_EQUIPMENT_NAME
			_arr_str_param(1, 0) = "@str_RFID_TYPE"
			_arr_str_param(1, 1) = pStr_RFID_TYPE
			_arr_str_param(2, 0) = "@str_RFID_01"
			_arr_str_param(2, 1) = pStr_RFID_01
			_arr_str_param(3, 0) = "@str_RFID_02"
			_arr_str_param(3, 1) = pStr_RFID_02
			_arr_str_param(4, 0) = "@str_IPAddr"
			_arr_str_param(4, 1) = pStr_IPAddr
			_arr_str_param(5, 0) = "@str_BATCH"
			_arr_str_param(5, 1) = ""
			_arr_str_param(6, 0) = "@str_UserID"
			_arr_str_param(6, 1) = pStr_UserID
			_arr_str_param(7, 0) = "@int_ID_MM_EQUIPMENT_REG"
			_arr_str_param(7, 1) = ""
			_arr_str_param(8, 0) = "@str_Option"
			_arr_str_param(8, 1) = "Generate_BatchNo"
			Dim _obj_DT As DataTable = New DataTable()
			Dim _str_return_value As String = ""
			_obj_DT.Load(Me.obj_DB.executeQuery("SP_EQUIPMENT_REGISTRATION", _arr_str_param, True))
			_str_return_value = Me.obj_DB.get_LastError()
			If (_obj_DT.Rows(0)("EQUIPMENT_REGISTRATION").ToString().Trim().Equals("EXISTED")) Then
				_str_return_value = "Fail"
			ElseIf (_obj_DT.Rows(0)("EQUIPMENT_REGISTRATION").ToString().Trim().Equals("OCCUPIED")) Then
				_str_return_value = "OCCUPIED"
			ElseIf (_obj_DT.Rows(0)("EQUIPMENT_REGISTRATION").ToString().Trim().Equals("INSERTED")) Then
				_str_return_value = _obj_DT.Rows(0)("BATCH_NUMBER").ToString().Trim()
				_str_return_value = String.Concat(_str_return_value, "|", _obj_DT.Rows(0)("ID_MM_EQUIPMENT_REG").ToString().Trim())
			End If
			Return _str_return_value
		End Function

		Public Function Equipment_Registration_BK_10APR2017(ByVal pStr_EQUIPMENT_NAME As String, ByVal pStr_RFID_TYPE As String, ByVal pStr_RFID_01 As String, ByVal pStr_RFID_02 As String, ByVal pStr_IPAddr As String, ByVal pStr_BATCH As String) As String
			Me.check_component()
			Dim _arr_str_param(5, 1) As String
			_arr_str_param(0, 0) = "@str_EQUIPMENT_NAME"
			_arr_str_param(0, 1) = pStr_EQUIPMENT_NAME
			_arr_str_param(1, 0) = "@str_RFID_TYPE"
			_arr_str_param(1, 1) = pStr_RFID_TYPE
			_arr_str_param(2, 0) = "@str_RFID_01"
			_arr_str_param(2, 1) = pStr_RFID_01
			_arr_str_param(3, 0) = "@str_RFID_02"
			_arr_str_param(3, 1) = pStr_RFID_02
			_arr_str_param(4, 0) = "@str_IPAddr"
			_arr_str_param(4, 1) = pStr_IPAddr
			_arr_str_param(5, 0) = "@str_BATCH"
			_arr_str_param(5, 1) = pStr_BATCH
			Dim _obj_DT As DataTable = New DataTable()
			Dim _str_return_value As String = ""
			_obj_DT.Load(Me.obj_DB.executeQuery("SP_EQUIPMENT_REGISTRATION", _arr_str_param, True))
			_str_return_value = Me.obj_DB.get_LastError()
			If (_obj_DT.Rows(0)("EQUIPMENT_REGISTRATION").ToString().Trim().Equals("EXISTED")) Then
				_str_return_value = "Fail"
			ElseIf (_obj_DT.Rows(0)("EQUIPMENT_REGISTRATION").ToString().Trim().Equals("INSERTED")) Then
				_str_return_value = "Success"
			End If
			Return _str_return_value
		End Function

		Public Function Get_All_RFID_Reder_Info() As String
			Me.check_component()
			Dim _obj_DT_Info As DataTable = New DataTable()
			Dim _str_Info As String = ""
			_obj_DT_Info.Load(Me.obj_DB.executeQuery("SP_GET_ALL_RFID_READER_INFO", Nothing, True))
			Dim count As Integer = _obj_DT_Info.Rows.Count - 1
			Dim _int_IRFID As Integer = 0
			Do
				_str_Info = String.Concat(_str_Info, "@", _obj_DT_Info.Rows(_int_IRFID)("RFID_CEL_READER_NAME").ToString(), ",")
				_str_Info = String.Concat(_str_Info, _obj_DT_Info.Rows(_int_IRFID)("IP_ADDRESS").ToString(), ",")
				_str_Info = String.Concat(_str_Info, _obj_DT_Info.Rows(_int_IRFID)("LOCATION_NAME").ToString(), ",")
				_str_Info = String.Concat(_str_Info, _obj_DT_Info.Rows(_int_IRFID)("RFID_STATUS").ToString(), ",")
				_str_Info = String.Concat(_str_Info, _obj_DT_Info.Rows(_int_IRFID)("ID_MM_RFID_LOC_READER_REG").ToString(), ",")
				_str_Info = String.Concat(_str_Info, _obj_DT_Info.Rows(_int_IRFID)("RFID_DEVICE_NAME").ToString())
				_int_IRFID = _int_IRFID + 1
			Loop While _int_IRFID <= count
			_str_Info = _str_Info.Substring(1, _str_Info.Length - 1).Trim()
			Return _str_Info
		End Function

		Public Function GET_BLEACHING_INFO_BY_RFID(ByVal pStr_RFID As String) As String
			Dim lastError As String
			Me.check_component()
			Dim _arr_str_param(1, 1) As String
			_arr_str_param(0, 0) = "@str_RFID"
			_arr_str_param(0, 1) = pStr_RFID
			Dim _obj_DT As DataTable = New DataTable()
			Dim _str_return_value As String = ""
			_obj_DT.Load(Me.obj_DB.executeQuery("SP_GET_STAGING_INFO_BY_RFID", _arr_str_param, True))
			If (_obj_DT.Rows.Count <= 0) Then
				_obj_DT.Dispose()
				lastError = Me.obj_DB.get_LastError()
			Else
				_str_return_value = String.Concat(_obj_DT.Rows(0)("LOCATION_NAME").ToString().Trim(), "$")
				_str_return_value = String.Concat(_str_return_value, _obj_DT.Rows(0)("RFID_LOC_AREA").ToString().Trim(), "$")
				_str_return_value = String.Concat(_str_return_value, _obj_DT.Rows(0)("YARDAGE_REQUIRE").ToString().Trim())
				_obj_DT.Dispose()
				lastError = _str_return_value
			End If
			Return lastError
		End Function

		Public Function Get_Equipment_Name_Listing() As String
			Dim str As String
			Dim _str_Equipment_Name As String = ""
			Dim _obj_DT As DataTable = New DataTable()
			_obj_DT.Load(Me.obj_DB.executeQuery("SP_GET_EQ_NAME_LST", Nothing, True))
			If (_obj_DT.Rows.Count <= 0) Then
				Try
					_obj_DT.Dispose()
				Catch exception As System.Exception
					ProjectData.SetProjectError(exception)
					ProjectData.ClearProjectError()
				End Try
				str = Nothing
			Else
				Dim count As Integer = _obj_DT.Rows.Count - 1
				Dim _int_iLoop As Integer = 0
				Do
					Dim strArrays() As String = { _str_Equipment_Name, "@", _obj_DT.Rows(_int_iLoop)("ID_MM_EQUIPMENT").ToString().Trim(), "$", _obj_DT.Rows(_int_iLoop)("EQUIPMENT_NAME").ToString().Trim() }
					_str_Equipment_Name = String.Concat(strArrays)
					_int_iLoop = _int_iLoop + 1
				Loop While _int_iLoop <= count
				_obj_DT.Dispose()
				Me.obj_DB.dispose_command()
				str = _str_Equipment_Name
			End If
			Return str
		End Function

		Public Function GET_Equipment_RFID_Listing() As DataTable
			Me.check_component()
			Dim _obj_DT As DataTable = New DataTable("RFID")
			Dim _arr_List As ArrayList = New ArrayList()
			_obj_DT.Load(Me.obj_DB.executeQuery("SP_GET_ALL_EQUIPMENT_REG_RFID", Nothing, True))
			Dim count As Integer = _obj_DT.Rows.Count - 1
			Dim _int_iCount As Integer = 0
			Do
				_arr_List.Add(_obj_DT.Rows(_int_iCount)("RFID").ToString().Trim())
				_int_iCount = _int_iCount + 1
			Loop While _int_iCount <= count
			Return _obj_DT
		End Function

		Public Function Get_Error_Message(ByVal pStr_ModuleName As String, ByVal pStr_ComponentName As String) As String
			Dim str As String
			Dim _str_ERROR_MSG As String = ""
			Dim _obj_DT As DataTable = New DataTable()
			Dim _arr_str_param(1, 1) As String
			_arr_str_param(0, 0) = "@pModuleName"
			_arr_str_param(0, 1) = pStr_ModuleName
			_arr_str_param(1, 0) = "@pComponentName"
			_arr_str_param(1, 1) = pStr_ComponentName
			_obj_DT.Load(Me.obj_DB.executeQuery("SP_GET_ERROR_MSG", _arr_str_param, True))
			If (_obj_DT.Rows.Count <= 0) Then
				Try
					_obj_DT.Dispose()
				Catch exception As System.Exception
					ProjectData.SetProjectError(exception)
					ProjectData.ClearProjectError()
				End Try
				str = Nothing
			Else
				_str_ERROR_MSG = _obj_DT.Rows(0)("ERROR_MSG").ToString().Trim()
				_obj_DT.Dispose()
				Me.obj_DB.dispose_command()
				str = _str_ERROR_MSG
			End If
			Return str
		End Function

		Public Function Get_GroupNo() As String
			Dim str As String
			Dim _str_GroupNo As String = ""
			Dim _obj_DT As DataTable = New DataTable()
			_obj_DT.Load(Me.obj_DB.executeQuery("SP_GET_GROUPNO", Nothing, True))
			If (_obj_DT.Rows.Count <= 0) Then
				Try
					_obj_DT.Dispose()
				Catch exception As System.Exception
					ProjectData.SetProjectError(exception)
					ProjectData.ClearProjectError()
				End Try
				str = Nothing
			Else
				_str_GroupNo = _obj_DT.Rows(0)("GroupNo").ToString().Trim()
				_obj_DT.Dispose()
				Me.obj_DB.dispose_command()
				str = _str_GroupNo
			End If
			Return str
		End Function

		Private Function GET_ID_MM_MACHINE_BY_LOCATION_NAME(ByVal pStr_LOCATION_NAME As String) As String
			Dim _obj_DT As DataTable = New DataTable()
			Dim _arr_str_param(0, 1) As String
			_arr_str_param(0, 0) = "@str_LOCATION_NAME"
			_arr_str_param(0, 1) = pStr_LOCATION_NAME
			_obj_DT.Load(Me.obj_DB.executeQuery("SP_GET_ID_MM_MACHINE_BY_LOCATION_NAME", _arr_str_param, True))
			Dim _str_ReturnValue As String = _obj_DT.Rows(0)("ID_MM_MACHINE").ToString().Trim()
			_obj_DT.Dispose()
			Return _str_ReturnValue
		End Function

		Public Function Get_Machine_Entry_Listing() As String
			Dim str As String
			Dim _str_Machine_Entry As String = ""
			Dim _obj_DT As DataTable = New DataTable()
			_obj_DT.Load(Me.obj_DB.executeQuery("SP_GET_MAC_ENTRY_LST", Nothing, True))
			If (_obj_DT.Rows.Count <= 0) Then
				Try
					_obj_DT.Dispose()
				Catch exception As System.Exception
					ProjectData.SetProjectError(exception)
					ProjectData.ClearProjectError()
				End Try
				str = Nothing
			Else
				Dim count As Integer = _obj_DT.Rows.Count - 1
				Dim _int_iLoop As Integer = 0
				Do
					_str_Machine_Entry = String.Concat(_str_Machine_Entry, "@", _obj_DT.Rows(_int_iLoop)("RFID_LOC_AREA").ToString().Trim())
					_int_iLoop = _int_iLoop + 1
				Loop While _int_iLoop <= count
				_obj_DT.Dispose()
				Me.obj_DB.dispose_command()
				str = _str_Machine_Entry
			End If
			Return str
		End Function

		Public Function Get_MachineName_Listing_All() As String
			Dim str As String
			Dim _obj_DT As DataTable = New DataTable()
			Dim _return_value As String = ""
			_obj_DT.Load(Me.obj_Ora_DB_PO.executeQuery("SP_MACHINE_NAME_ALL_SEL", Nothing, True))
			If (_obj_DT.Rows.Count <= 0) Then
				Try
					_obj_DT.Dispose()
				Catch exception As System.Exception
					ProjectData.SetProjectError(exception)
					ProjectData.ClearProjectError()
				End Try
				str = Nothing
			Else
				Dim _obj_List As List(Of Machine_Name_Dtl) = New List(Of Machine_Name_Dtl)()
				Dim count As Integer = _obj_DT.Rows.Count - 1
				Dim _int_iLoop As Integer = 0
				Do
					Dim _obj As Machine_Name_Dtl = New Machine_Name_Dtl() With
					{
						.ID_MM_MACHINE = _obj_DT.Rows(_int_iLoop)("ID_MM_MACHINE").ToString().Trim(),
						.MACHINE_CODE = _obj_DT.Rows(_int_iLoop)("MACHINE_CODE").ToString().Trim(),
						.MACHINE_DESC = _obj_DT.Rows(_int_iLoop)("MACHINE_DESC").ToString().Trim()
					}
					_return_value = String.Concat(_return_value, "@", _obj_DT.Rows(_int_iLoop)("MACHINE_CODE").ToString().Trim())
					_obj_List.Add(_obj)
					_int_iLoop = _int_iLoop + 1
				Loop While _int_iLoop <= count
				_obj_DT.Dispose()
				Me.obj_Ora_DB_PO.dispose_command()
				str = _return_value
			End If
			Return str
		End Function

		Public Function GET_MM_RFID_LOC_READER_REG_BY_RFID(ByVal pStr_RFID As String) As String
			Dim lastError As String
			Me.check_component()
			Dim _arr_str_param(0, 1) As String
			_arr_str_param(0, 0) = "@str_RFID"
			_arr_str_param(0, 1) = pStr_RFID
			Dim _obj_DT As DataTable = New DataTable()
			Dim _str_return_value As String = ""
			_obj_DT.Load(Me.obj_DB.executeQuery("SP_GET_MM_RFID_LOC_READER_REG_BY_RFID", _arr_str_param, True))
			If (_obj_DT.Rows.Count <= 0) Then
				_obj_DT.Dispose()
				lastError = Me.obj_DB.get_LastError()
			Else
				_str_return_value = String.Concat(_obj_DT.Rows(0)("LOCATION_NAME").ToString().Trim(), "$")
				_str_return_value = String.Concat(_str_return_value, _obj_DT.Rows(0)("RFID_LOC_AREA").ToString().Trim(), "$")
				_str_return_value = String.Concat(_str_return_value, _obj_DT.Rows(0)("YARDAGE_REQUIRE").ToString().Trim())
				_obj_DT.Dispose()
				lastError = _str_return_value
			End If
			Return lastError
		End Function

		Public Function Get_Next_Process_Listing() As String
			Dim str As String
			Dim _obj_DT As DataTable = New DataTable()
			Dim _return_value As String = ""
			_obj_DT.Load(Me.obj_Ora_DB_PO.executeQuery("SP_GET_NXT_PROC_LST", Nothing, True))
			If (_obj_DT.Rows.Count <= 0) Then
				Try
					_obj_DT.Dispose()
				Catch exception As System.Exception
					ProjectData.SetProjectError(exception)
					ProjectData.ClearProjectError()
				End Try
				str = Nothing
			Else
				Dim count As Integer = _obj_DT.Rows.Count - 1
				Dim _int_iLoop As Integer = 0
				Do
					Dim strArrays() As String = { _return_value, "@", _obj_DT.Rows(_int_iLoop)("ID_MM_PROCESS_GROUP").ToString().Trim(), "$", _obj_DT.Rows(_int_iLoop)("PROCESS_GROUP_NAME").ToString().Trim() }
					_return_value = String.Concat(strArrays)
					_int_iLoop = _int_iLoop + 1
				Loop While _int_iLoop <= count
				_obj_DT.Dispose()
				Me.obj_Ora_DB_PO.dispose_command()
				str = _return_value
			End If
			Return str
		End Function

		Public Function Get_Next_Shift_Group_Listing() As String
			Dim str As String
			Dim _obj_DT As DataTable = New DataTable()
			Dim _return_value As String = ""
			_obj_DT.Load(Me.obj_Ora_DB_PO.executeQuery("SP_GET_SHIFT_GROUP_LST", Nothing, True))
			If (_obj_DT.Rows.Count <= 0) Then
				Try
					_obj_DT.Dispose()
				Catch exception As System.Exception
					ProjectData.SetProjectError(exception)
					ProjectData.ClearProjectError()
				End Try
				str = Nothing
			Else
				Dim count As Integer = _obj_DT.Rows.Count - 1
				Dim _int_iLoop As Integer = 0
				Do
					Dim strArrays() As String = { _return_value, "@", _obj_DT.Rows(_int_iLoop)("ID_MM_SHIFT_GROUP").ToString().Trim(), "$", _obj_DT.Rows(_int_iLoop)("SHIFT_GROUP").ToString().Trim() }
					_return_value = String.Concat(strArrays)
					_int_iLoop = _int_iLoop + 1
				Loop While _int_iLoop <= count
				_obj_DT.Dispose()
				Me.obj_Ora_DB_PO.dispose_command()
				str = _return_value
			End If
			Return str
		End Function

		Public Function Get_NxtProc_Desc(ByVal pStr_NxtProc As String) As String
			Dim str As String
			Dim _obj_DT As DataTable = New DataTable()
			Dim _arr_str_param(0, 1) As String
			_arr_str_param(0, 0) = "@pNxtProc"
			_arr_str_param(0, 1) = pStr_NxtProc
			_obj_DT.Load(Me.obj_Ora_DB_PO.executeQuery("SP_GET_NXT_PROC_SEL", _arr_str_param, True))
			If (_obj_DT.Rows.Count <= 0) Then
				Try
					_obj_DT.Dispose()
				Catch exception As System.Exception
					ProjectData.SetProjectError(exception)
					ProjectData.ClearProjectError()
				End Try
				str = Nothing
			Else
				Dim vNext_Process As String = _obj_DT.Rows(0)("SEC_DESC").ToString().Trim()
				_obj_DT.Dispose()
				Me.obj_Ora_DB_PO.dispose_command()
				str = vNext_Process
			End If
			Return str
		End Function

		Public Function Get_PO_SERAIL_BY_BATCHID(ByVal pStr_BATCH_ID As String) As String
			Dim str As String
			Dim _str_PO As String = ""
			Dim _str_PO_SERIAL As String = ""
			Dim _obj_DT As DataTable = New DataTable()
			Dim _arr_str_param(0, 1) As String
			_arr_str_param(0, 0) = "@str_BatchID"
			_arr_str_param(0, 1) = pStr_BATCH_ID
			_obj_DT.Load(Me.obj_DB.executeQuery("SP_GET_RFID_NUMBER_BY_BATCHID", _arr_str_param, True))
			If (_obj_DT.Rows.Count <= 0) Then
				Try
					_obj_DT.Dispose()
				Catch exception As System.Exception
					ProjectData.SetProjectError(exception)
					ProjectData.ClearProjectError()
				End Try
				str = Nothing
			Else
				_str_PO = _obj_DT.Rows(0)("PO_NO").ToString().Trim()
				_str_PO_SERIAL = _obj_DT.Rows(0)("PO_SERIAL_NO").ToString().Trim()
				_obj_DT.Dispose()
				Me.obj_DB.dispose_command()
				str = _str_PO_SERIAL
			End If
			Return str
		End Function

		Public Function Get_PONumber_By_POSerialNumber(ByVal pStr_POSerial As String) As String
			Dim str As String
			Dim _obj_DT As DataTable = New DataTable()
			Dim _arr_str_param(0, 1) As String
			_arr_str_param(0, 0) = "pPOSerial"
			_arr_str_param(0, 1) = pStr_POSerial
			_obj_DT.Load(Me.obj_Ora_DB_PO.executeQuery("SP_GET_PO_SEL", _arr_str_param, True))
			If (_obj_DT.Rows.Count <= 0) Then
				Try
					_obj_DT.Dispose()
				Catch exception As System.Exception
					ProjectData.SetProjectError(exception)
					ProjectData.ClearProjectError()
				End Try
				str = ""
			Else
				Dim PO_NO As String = _obj_DT.Rows(0)("PROD_ORDER_NO").ToString().Trim()
				_obj_DT.Dispose()
				Me.obj_Ora_DB_PO.dispose_command()
				str = PO_NO
			End If
			Return str
		End Function

		Public Function GET_RFID_CODE_FROM_MM_EQUIPMENT_REG(ByVal pStr_RFID_01 As String, ByVal pStr_RFID_02 As String) As String
			Dim lastError As String
			Me.check_component()
			Dim _arr_str_param(1, 1) As String
			_arr_str_param(0, 0) = "@str_RFID_01"
			_arr_str_param(0, 1) = pStr_RFID_01
			_arr_str_param(1, 0) = "@str_RFID_02"
			_arr_str_param(1, 1) = pStr_RFID_02
			Dim _obj_DT As DataTable = New DataTable()
			Dim _str_return_value As String = ""
			_obj_DT.Load(Me.obj_DB.executeQuery("SP_GET_RFID_CODE_FROM_MM_EQUIPMENT_REG", _arr_str_param, True))
			If (_obj_DT.Rows.Count <= 0) Then
				_obj_DT.Dispose()
				lastError = Me.obj_DB.get_LastError()
			Else
				_str_return_value = _obj_DT.Rows(0)("RFID_CODE").ToString().Trim()
				_obj_DT.Dispose()
				lastError = _str_return_value
			End If
			Return lastError
		End Function

		Public Function GET_RFID_CODE_FROM_MM_EQUIPMENT_REG_4_PALLET(ByVal pStr_RFID_01 As String, ByVal pStr_RFID_02 As String) As String
			Dim lastError As String
			Me.check_component()
			Dim _arr_str_param(1, 1) As String
			_arr_str_param(0, 0) = "@str_RFID_01"
			_arr_str_param(0, 1) = pStr_RFID_01
			_arr_str_param(1, 0) = "@str_RFID_02"
			_arr_str_param(1, 1) = pStr_RFID_02
			Dim _obj_DT As DataTable = New DataTable()
			Dim _str_return_value As String = ""
			_obj_DT.Load(Me.obj_DB.executeQuery("SP_GET_RFID_CODE_FROM_MM_EQUIPMENT_REG_4_PALLET", _arr_str_param, True))
			If (_obj_DT.Rows.Count <= 0) Then
				_obj_DT.Dispose()
				lastError = Me.obj_DB.get_LastError()
			Else
				_str_return_value = _obj_DT.Rows(0)("BATCH_ID").ToString().Trim()
				_obj_DT.Dispose()
				lastError = _str_return_value
			End If
			Return lastError
		End Function

		Public Function Get_RFID_DB_Info(ByVal pStr_UserName As String, ByVal pStr_Password As String) As List(Of RFID_Reader_Setting)
			Dim rFIDReaderSettings As List(Of RFID_Reader_Setting)
			Dim _obj_DT As DataTable = New DataTable()
			_obj_DT.Load(Me.obj_DB.executeQuery("sp_GetUserLogin", Nothing, True))
			If (_obj_DT.Rows.Count <= 0) Then
				Try
					_obj_DT.Dispose()
				Catch exception As System.Exception
					ProjectData.SetProjectError(exception)
					ProjectData.ClearProjectError()
				End Try
				rFIDReaderSettings = Nothing
			Else
				Dim _obj_List As List(Of RFID_Reader_Setting) = New List(Of RFID_Reader_Setting)()
				Dim count As Integer = _obj_DT.Rows.Count
				Dim _int_iLoop As Integer = 0
				Do
					Dim _obj As RFID_Reader_Setting = New RFID_Reader_Setting() With
					{
						.Station_Name = _obj_DT.Rows(_int_iLoop)("Station_Name").ToString().Trim(),
						.Catagory = _obj_DT.Rows(_int_iLoop)("Catagory").ToString().Trim(),
						.Exe_Name = _obj_DT.Rows(_int_iLoop)("Application_Name").ToString().Trim(),
						.IP_Addr = _obj_DT.Rows(_int_iLoop)("IP_Address").ToString().Trim(),
						.AppPath = _obj_DT.Rows(_int_iLoop)("AppPath").ToString().Trim()
					}
					_obj_List.Add(_obj)
					_int_iLoop = _int_iLoop + 1
				Loop While _int_iLoop <= count
				_obj_DT.Dispose()
				Me.obj_DB.dispose_command()
				rFIDReaderSettings = _obj_List
			End If
			Return rFIDReaderSettings
		End Function

		Public Function GET_RFID_READER_INFO_BY_DEVICE_NAME(ByVal pStr_DeviceName As String) As String
			Me.check_component()
			Dim _obj_DT_Info As DataTable = New DataTable()
			Dim _str_Info As String = ""
			Dim _arr_str_param(0, 1) As String
			_arr_str_param(0, 0) = "@str_DeviceName"
			_arr_str_param(0, 1) = pStr_DeviceName
			_obj_DT_Info.Load(Me.obj_DB.executeQuery("SP_GET_RFID_READER_INFO_BY_DEVICE_NAME", _arr_str_param, True))
			_str_Info = String.Concat(_obj_DT_Info.Rows(0)("RSSI").ToString(), "|")
			_str_Info = String.Concat(_str_Info, _obj_DT_Info.Rows(0)("MM_RFID_LOC_AREA").ToString(), "|")
			_str_Info = String.Concat(_str_Info, _obj_DT_Info.Rows(0)("TAG_COUNT").ToString(), "|")
			_str_Info = String.Concat(_str_Info, pStr_DeviceName, "|")
			_str_Info = String.Concat(_str_Info, _obj_DT_Info.Rows(0)("RFID_CEL_READER_NAME").ToString(), "|")
			_str_Info = String.Concat(_str_Info, _obj_DT_Info.Rows(0)("LOCATION_NAME").ToString(), "|")
			Dim _str_Path As String = _obj_DT_Info.Rows(0)("APPL_PATH_URL").ToString().Trim()
			If (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(_str_Path.Substring(_str_Path.Length - 1, 1), "\", False) <> 0) Then
				_str_Path = String.Concat(_str_Path, "\")
			End If
			_str_Path = String.Concat(_str_Path, _obj_DT_Info.Rows(0)("APPL_NAME").ToString().Trim())
			_str_Info = String.Concat(_str_Info, _str_Path)
			_obj_DT_Info.Dispose()
			Return _str_Info
		End Function

		Public Function GET_RFID_SIGNAL_SETTING(ByVal pStr_DeviceName As String) As String
			Dim _str_return_value As String = ""
			Dim _obj_DT As DataTable = New DataTable()
			Dim _arr_str_param(0, 1) As String
			_arr_str_param(0, 0) = "@str_DeviceName"
			_arr_str_param(0, 1) = pStr_DeviceName
			_obj_DT.Load(Me.obj_DB.executeQuery("SP_GET_RFID_SIGNAL_SETTING", _arr_str_param, True))
			If (_obj_DT.Rows.Count > 0) Then
				_str_return_value = _obj_DT.Rows(0)("RSSI").ToString().Trim()
				_str_return_value = String.Concat(_str_return_value, "@", _obj_DT.Rows(0)("TAG_COUNT").ToString().Trim())
			End If
			_obj_DT.Dispose()
			Return _str_return_value
		End Function

		Public Function Get_RFID_Type_Listing() As String
			Dim str As String
			Dim _str_RFID_Type As String = ""
			Dim _obj_DT As DataTable = New DataTable()
			_obj_DT.Load(Me.obj_DB.executeQuery("SP_GET_RFID_TYPE_LST", Nothing, True))
			If (_obj_DT.Rows.Count <= 0) Then
				Try
					_obj_DT.Dispose()
				Catch exception As System.Exception
					ProjectData.SetProjectError(exception)
					ProjectData.ClearProjectError()
				End Try
				str = Nothing
			Else
				Dim count As Integer = _obj_DT.Rows.Count - 1
				Dim _int_iLoop As Integer = 0
				Do
					Dim strArrays() As String = { _str_RFID_Type, "@", _obj_DT.Rows(_int_iLoop)("ID_MM_RFID_TYPE").ToString().Trim(), "$", _obj_DT.Rows(_int_iLoop)("RFID_TYPE").ToString().Trim() }
					_str_RFID_Type = String.Concat(strArrays)
					_int_iLoop = _int_iLoop + 1
				Loop While _int_iLoop <= count
				_obj_DT.Dispose()
				Me.obj_DB.dispose_command()
				str = _str_RFID_Type
			End If
			Return str
		End Function

		Public Function GET_SCANFLAG_BY_DEVICE_NAME(ByVal pStr_DeviceName As String) As String
			Dim _str_ScanFlag As String = ""
			Dim _obj_DT As DataTable = New DataTable()
			Dim _arr_str_param(0, 1) As String
			_arr_str_param(0, 0) = "@str_DeviceName"
			_arr_str_param(0, 1) = pStr_DeviceName
			_obj_DT.Load(Me.obj_DB.executeQuery("SP_GET_RFID_LOC_AREA_BY_DEVICENAME", _arr_str_param, True))
			If (_obj_DT.Rows.Count > 0) Then
				_str_ScanFlag = _obj_DT.Rows(0)("RFID_LOC_AREA").ToString().Trim()
			End If
			_obj_DT.Dispose()
			Return _str_ScanFlag
		End Function

		Public Function Get_Staging_Area_Listing() As String
			Dim str As String
			Dim _str_Staging_Area As String = ""
			Dim _obj_DT As DataTable = New DataTable()
			_obj_DT.Load(Me.obj_DB.executeQuery("SP_GET_STG_AREA_LST", Nothing, True))
			If (_obj_DT.Rows.Count <= 0) Then
				Try
					_obj_DT.Dispose()
				Catch exception As System.Exception
					ProjectData.SetProjectError(exception)
					ProjectData.ClearProjectError()
				End Try
				str = Nothing
			Else
				Dim count As Integer = _obj_DT.Rows.Count - 1
				Dim _int_iLoop As Integer = 0
				Do
					_str_Staging_Area = String.Concat(_str_Staging_Area, "@", _obj_DT.Rows(_int_iLoop)("LOCATION_NAME").ToString().Trim())
					_int_iLoop = _int_iLoop + 1
				Loop While _int_iLoop <= count
				_obj_DT.Dispose()
				Me.obj_DB.dispose_command()
				str = _str_Staging_Area
			End If
			Return str
		End Function

		Public Function Get_Staging_Entry_Listing() As String
			Dim str As String
			Dim _str_Staging_Entry As String = ""
			Dim _obj_DT As DataTable = New DataTable()
			_obj_DT.Load(Me.obj_DB.executeQuery("SP_GET_STG_ENTRY_LST", Nothing, True))
			If (_obj_DT.Rows.Count <= 0) Then
				Try
					_obj_DT.Dispose()
				Catch exception As System.Exception
					ProjectData.SetProjectError(exception)
					ProjectData.ClearProjectError()
				End Try
				str = Nothing
			Else
				Dim count As Integer = _obj_DT.Rows.Count - 1
				Dim _int_iLoop As Integer = 0
				Do
					_str_Staging_Entry = String.Concat(_str_Staging_Entry, "@", _obj_DT.Rows(_int_iLoop)("RFID_LOC_AREA").ToString().Trim())
					_int_iLoop = _int_iLoop + 1
				Loop While _int_iLoop <= count
				_obj_DT.Dispose()
				Me.obj_DB.dispose_command()
				str = _str_Staging_Entry
			End If
			Return str
		End Function

		Public Function Get_WIP_RFID_TRANS_INFO(ByVal pStr_RFID_ID As String) As String
			Dim str As String
			Dim _str_PO As String = ""
			Dim _str_PO_SERIAL As String = ""
			Dim _obj_DT As DataTable = New DataTable()
			Dim _arr_str_param(0, 1) As String
			_arr_str_param(0, 0) = "@pRFID_ID"
			_arr_str_param(0, 1) = pStr_RFID_ID
			_obj_DT.Load(Me.obj_DB.executeQuery("SP_GET_WIP_RFID_TRANS", _arr_str_param, True))
			If (_obj_DT.Rows.Count <= 0) Then
				Try
					_obj_DT.Dispose()
				Catch exception As System.Exception
					ProjectData.SetProjectError(exception)
					ProjectData.ClearProjectError()
				End Try
				str = Nothing
			Else
				_str_PO = _obj_DT.Rows(0)("PO_NO").ToString().Trim()
				_str_PO_SERIAL = _obj_DT.Rows(0)("PO_SERIAL_NO").ToString().Trim()
				_obj_DT.Dispose()
				Me.obj_DB.dispose_command()
				str = _str_PO_SERIAL
			End If
			Return str
		End Function

		Public Sub OnOff_RFID_Station(ByVal pStr_DeviceName As String, ByVal pStr_IPAddress As String, ByVal pArr_Str_StationInfo As String(), ByVal pBol_OnOff As Boolean)
			Dim _str_Msg As String
			If (Not pBol_OnOff) Then
				_str_Msg = String.Concat(pStr_DeviceName, "|Kill")
			Else
				_str_Msg = String.Concat(pStr_IPAddress, "|Wake|")
				Dim pArrStrStationInfo As String() = pArr_Str_StationInfo
				Dim num As Integer = 0
				While num < CInt(pArrStrStationInfo.Length)
					_str_Msg = String.Concat(_str_Msg, pArrStrStationInfo(num), "|")
					num = num + 1
				End While
				If (_str_Msg.Substring(_str_Msg.Length - 1, 1).Equals("|")) Then
					_str_Msg = _str_Msg.Substring(0, _str_Msg.Length - 1)
				End If
			End If
			Dim clientSocket As TcpClient = New TcpClient()
			Dim _int_iCount As Integer = 0
			Do
				Try
					clientSocket.Connect("127.0.0.1", 8888)
					Exit Do
				Catch exception As System.Exception
					ProjectData.SetProjectError(exception)
					Thread.Sleep(1000)
					ProjectData.ClearProjectError()
				End Try
				_int_iCount = _int_iCount + 1
			Loop While _int_iCount <= 100
			Dim serverStream As NetworkStream = clientSocket.GetStream()
			Dim outStream As Byte() = Encoding.ASCII.GetBytes(String.Concat(_str_Msg, "$"))
			serverStream.Write(outStream, 0, CInt(outStream.Length))
			serverStream.Flush()
			clientSocket.GetStream().Close()
			serverStream.Close()
			clientSocket.Close()
			Try
				serverStream.Dispose()
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				ProjectData.ClearProjectError()
			End Try
			Thread.Sleep(1000)
			Me.Reset_RFID_Reader(pStr_IPAddress)
		End Sub

		Public Function Reset_RFID_Reader(ByVal pstr_IPAddr As String) As Boolean
			Dim flag As Boolean
			Dim _ReaderAPI As RFIDReader = New RFIDReader(pstr_IPAddr, 5084, 0)
			Try
				_ReaderAPI.Connect()
				_ReaderAPI.Config.GPO(1).PortState = GPOs.GPO_PORT_STATE.[FALSE]
				_ReaderAPI.Config.GPO(2).PortState = GPOs.GPO_PORT_STATE.[FALSE]
				_ReaderAPI.Config.GPO(3).PortState = GPOs.GPO_PORT_STATE.[FALSE]
				_ReaderAPI.Disconnect()
				_ReaderAPI.Dispose()
				flag = True
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				flag = False
				ProjectData.ClearProjectError()
			End Try
			Return flag
		End Function

		Public Function Resolve_HostName_2_IP(ByVal pStr_hostname As String) As String
			Dim str As String
			Try
				str = Dns.GetHostAddresses(pStr_hostname)(0).ToString()
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str = ""
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Function RFID_Code_Verification(ByVal pStr_RFID_01 As String, ByVal pStr_RFID_02 As String) As String
			Dim lastError As String
			Dim _obj_DT As DataTable = New DataTable()
			Dim _arr_str_param(1, 1) As String
			_arr_str_param(0, 0) = "@str_RFID_01"
			_arr_str_param(0, 1) = pStr_RFID_01
			_arr_str_param(1, 0) = "@str_RFID_02"
			_arr_str_param(1, 1) = pStr_RFID_02
			_obj_DT.Load(Me.obj_DB.executeQuery("SP_GET_MM_EQUIPMENT_REG_BY_RFID_CODE", _arr_str_param, True))
			If (_obj_DT.Rows.Count > 0) Then
				_obj_DT.Dispose()
				lastError = "available"
			ElseIf (Me.obj_DB.get_LastError().Equals("")) Then
				_obj_DT.Dispose()
				lastError = "nothing"
			Else
				_obj_DT.Dispose()
				lastError = Me.obj_DB.get_LastError()
			End If
			Return lastError
		End Function

		Public Function Send_Error_Notification(ByVal pStr_module As String, ByVal pStr_proc As String, ByVal pStr_error As String) As String
			Dim str As String
			Try
				Dim _arr_str_param(4, 1) As String
				_arr_str_param(0, 0) = "p_subject"
				_arr_str_param(0, 1) = ""
				_arr_str_param(1, 0) = "p_prog"
				_arr_str_param(1, 1) = ""
				_arr_str_param(2, 0) = "p_module"
				_arr_str_param(2, 1) = pStr_module
				_arr_str_param(3, 0) = "p_proc"
				_arr_str_param(3, 1) = pStr_proc
				_arr_str_param(4, 0) = "p_error"
				_arr_str_param(4, 1) = pStr_error
				Me.obj_Ora_DB_PO.executeActionQuery("SEND_EMAL_ERR_RFID_WIP", _arr_str_param, True).ToString()
				str = ""
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				str = exception.ToString()
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function

		Public Sub ShutDown_ALL_RFID_Station()
			Dim processes As Process() = Process.GetProcesses()
			Dim num As Integer = 0
			While num < CInt(processes.Length)
				Dim p As Process = processes(num)
				If (p.ProcessName.ToString().Trim().Equals("PAB_Real_Time_WIP_Console")) Then
					p.Kill()
				End If
				If (p.ProcessName.ToString().Trim().Equals("PAB_Real_Time_WIP_RFID_Host")) Then
					p.Kill()
				End If
				num = num + 1
			End While
		End Sub

		Public Function STAGING_RFID_VERIFIER(ByVal pStr_ID_RFID As String) As String
			Dim _arr_str_param(0, 1) As String
			_arr_str_param(0, 0) = "@str_RFID"
			_arr_str_param(0, 1) = pStr_ID_RFID
			Dim _obj_DT As DataTable = New DataTable()
			Dim _str_ReturnValue As String = "FAIL"
			_obj_DT.Load(Me.obj_DB.executeQuery("SP_STAGING_RFID_VERIFIER", _arr_str_param, True))
			If (_obj_DT.Rows.Count > 0) Then
				_str_ReturnValue = _obj_DT.Rows(0)("RESULT").ToString().Trim()
			End If
			_obj_DT.Dispose()
			Return _str_ReturnValue
		End Function

		Public Sub UPDATE_MACHINE_OUT_BY_RFID(ByVal pStr_ID_RFID As String)
			Dim _arr_str_param(0, 1) As String
			_arr_str_param(0, 0) = "@str_ID_RFID"
			_arr_str_param(0, 1) = pStr_ID_RFID
			Me.obj_DB.executeActionQuery("SP_UPDATE_MACHINE_OUT_BY_RFID", _arr_str_param, True)
		End Sub

		Public Function update_OnOff_RFID_Station(ByVal pStr_DeviceName As String, ByVal pStr_IPAddr As String, ByVal pStr_UserID As String, ByVal pStr_Status As String, ByVal pStr_Option As String) As String
			Me.check_component()
			Dim _arr_str_param(4, 1) As String
			_arr_str_param(0, 0) = "@int_Status"
			_arr_str_param(0, 1) = pStr_Status
			_arr_str_param(1, 0) = "@str_UserID"
			_arr_str_param(1, 1) = pStr_UserID
			_arr_str_param(2, 0) = "@str_IPAddress"
			_arr_str_param(2, 1) = pStr_IPAddr
			_arr_str_param(3, 0) = "@str_DeviceName"
			_arr_str_param(3, 1) = pStr_DeviceName
			_arr_str_param(4, 0) = "@int_Option"
			_arr_str_param(4, 1) = pStr_Option
			Me.obj_DB.executeActionQuery("SP_RFID_DEVICE_STATUS_UPDATE", _arr_str_param, True)
			Return Me.obj_DB.get_LastError()
		End Function

		Public Function User_ACL_AccessRight(ByVal pStr_UserID As String) As String
			Dim str As String
			Dim enumerator As List(Of ACL.[Object].Resource).Enumerator = New List(Of ACL.[Object].Resource).Enumerator()
			Dim SystemID As String = "0"
			Dim _list As String = ""
			SystemID = Conversions.ToString(ACL.OracleClass.Resource.RetrieveApplicationIDByName(ConfigurationManager.ConnectionStrings("ORCL_ACL").ConnectionString, "WIP RFID HANDHELD"))
			Dim _aclResource As ACL.OracleClass.Resource = New ACL.OracleClass.Resource(ConfigurationManager.ConnectionStrings("ORCL_ACL").ConnectionString)
			Dim _sourcelist As List(Of ACL.[Object].Resource) = _aclResource.RetrieveResource(Conversions.ToInteger(pStr_UserID), Conversions.ToInteger(SystemID))
			Try
				enumerator = Search.GetParent(_sourcelist, Conversions.ToInteger(SystemID)).GetEnumerator()
				While enumerator.MoveNext()
					Dim itm As ACL.[Object].Resource = enumerator.Current
					_list = String.Concat(_list, "@", itm.ResouceDesc)
				End While
			Finally
				DirectCast(enumerator, IDisposable).Dispose()
			End Try
			str = If(Microsoft.VisualBasic.CompilerServices.Operators.CompareString(_list, "", False) = 0, "Acess Fail", _list)
			Return str
		End Function

		Public Function User_ACL_Login(ByVal pStr_UserName As String, ByVal pStr_Password As String) As String
			Dim str As String
			Dim SystemID As String = "0"
			SystemID = Conversions.ToString(ACL.OracleClass.Resource.RetrieveApplicationIDByName(ConfigurationManager.ConnectionStrings("ORCL_ACL").ConnectionString, "WIP RFID HANDHELD"))
			Dim _aclUser As ACL.OracleClass.User = New ACL.OracleClass.User(ConfigurationManager.ConnectionStrings("ORCL_ACL").ConnectionString)
			Dim userobj As ACL.[Object].User = New ACL.[Object].User()
			userobj = _aclUser.validateWithRetrieveUser(pStr_UserName.Trim(), pStr_Password.Trim(), Conversions.ToInteger(SystemID))
			str = If(Microsoft.VisualBasic.CompilerServices.Operators.CompareString(userobj.UserID, "", False) = 0, "Login Fail", userobj.UserID)
			Return str
		End Function

		Public Function user_login(ByVal pStr_UserName As String, ByVal pStr_Password As String) As String
			Me.check_component()
			Dim _arr_str_param(2, 2) As String
			_arr_str_param(0, 0) = "@str_UserName"
			_arr_str_param(0, 1) = pStr_UserName
			_arr_str_param(1, 0) = "@str_Password"
			_arr_str_param(1, 1) = pStr_Password
			Dim _obj_DT As DataTable = New DataTable()
			_obj_DT.Load(Me.obj_DB.executeQuery("sp_GetUserLogin", _arr_str_param, True))
			Return If(_obj_DT.Rows.Count <= 0, "", "UserID")
		End Function

		Public Function WIP_RFID_Transaction(ByVal pStr_PO_Serial_Number As String, ByVal pStr_RFID_Cel_Reader_Name As String, ByVal pStr_DeviceIP As String, ByVal pStr_Scan_Flag As String, ByVal pStr_HW_Device As String, ByVal pStr_RFID_Number As String, ByVal pInt_Record_Type As Short, ByVal pStr_UserID As String, ByVal pStr_Option As String) As String
			Dim str As String
			Try
				Dim _arr_str_param(2, 2) As String
				Dim _str_PO_Number As String = ""
				If (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(pStr_HW_Device, "WEB", False) <> 0) Then
					_arr_str_param(0, 0) = "@str_RFID_Number"
					_arr_str_param(0, 1) = pStr_RFID_Number
					_arr_str_param(1, 0) = "@str_PO_Serial_Number"
					_arr_str_param(1, 1) = pStr_PO_Serial_Number
					Dim _obj_DT As DataTable = New DataTable()
					_obj_DT.Load(Me.obj_DB.executeQuery("sp_check_Grey_Receiving_by_RFID_n_PO_Serial", _arr_str_param, True))
					If (_obj_DT.Rows.Count <= 0) Then
						Try
							_obj_DT.Dispose()
						Catch exception As System.Exception
							ProjectData.SetProjectError(exception)
							ProjectData.ClearProjectError()
						End Try
					Else
						Try
							_obj_DT.Dispose()
						Catch exception1 As System.Exception
							ProjectData.SetProjectError(exception1)
							ProjectData.ClearProjectError()
						End Try
						If (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(pStr_HW_Device, "HANDHELD", False) <> 0) Then
							str = ""
							Return str
						Else
							str = "Data just added is existed."
							Return str
						End If
					End If
				Else
					_str_PO_Number = ""
				End If
				_arr_str_param = DirectCast(Utils.CopyArray(DirectCast(_arr_str_param, Array), New String(9, 2) {}), String(,))
				_arr_str_param(0, 0) = "@str_PO_Number"
				_arr_str_param(0, 1) = _str_PO_Number
				_arr_str_param(1, 0) = "@str_PO_Serial_Number"
				_arr_str_param(1, 1) = pStr_PO_Serial_Number
				_arr_str_param(2, 0) = "@str_RFID_Cel_Reader_Name"
				_arr_str_param(2, 1) = pStr_RFID_Cel_Reader_Name
				_arr_str_param(3, 0) = "@str_Scan_Flag"
				_arr_str_param(3, 1) = pStr_Scan_Flag
				_arr_str_param(4, 0) = "@str_HW_Device"
				_arr_str_param(4, 1) = pStr_HW_Device
				_arr_str_param(5, 0) = "@str_RFID_Number"
				_arr_str_param(5, 1) = pStr_RFID_Number
				_arr_str_param(6, 0) = "@int_Record_Type"
				_arr_str_param(6, 1) = pInt_Record_Type.ToString()
				_arr_str_param(7, 0) = "@str_UserID"
				_arr_str_param(7, 1) = pStr_UserID
				_arr_str_param(8, 0) = "@str_Option"
				_arr_str_param(8, 1) = pStr_Option
				Me.obj_DB.executeActionQuery("sp_Insert_Grey_Receiving_by_RFID_n_PO_Serial", _arr_str_param, True)
				str = ""
			Catch exception2 As System.Exception
				ProjectData.SetProjectError(exception2)
				str = exception2.ToString()
				ProjectData.ClearProjectError()
			End Try
			Return str
		End Function
	End Class
End Namespace