Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.ComponentModel
Imports System.Configuration
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Web.Configuration

Namespace cls_DB
	Public Class cls_MSSQL
		Private dbConn As SqlConnection

		Private dbCmd As SqlCommand

		Private dbParam As SqlParameter

		Private str_connstr As String

		Private dbDr As SqlDataReader

		Private str_LastErroMsg As String

		Private str_DB_Tag As String

		Public Sub New(ByVal pStr_DB_Tag_or_ConnStr As String)
			MyBase.New()
			Me.dbConn = Nothing
			Me.dbCmd = Nothing
			Me.dbParam = Nothing
			Me.str_connstr = ""
			Me.dbDr = Nothing
			Me.str_LastErroMsg = ""
			Me.str_DB_Tag = ""
			Me.Locate_ConnStr(pStr_DB_Tag_or_ConnStr)
		End Sub

		Public Function active_connection() As Boolean
			Return Me.n_active_connection()
		End Function

		Public Function active_connection(ByVal pStr_DB_Tag_or_ConnStr As String) As Boolean
			Me.Locate_ConnStr(pStr_DB_Tag_or_ConnStr)
			Return Me.n_active_connection()
		End Function

		Public Sub Begin_Trans()
			If (Not Me.n_check_conn_status()) Then
				Try
					Me.dbConn.Dispose()
				Catch exception As System.Exception
					ProjectData.SetProjectError(exception)
					ProjectData.ClearProjectError()
				End Try
				Me.dbConn = Nothing
				Me.active_connection()
			End If
			Me.dbConn.BeginTransaction()
		End Sub

		Public Function check_conn_status() As Boolean
			Return Me.n_check_conn_status()
		End Function

		Private Function chk_config_file_auto(ByVal pStr_DB_Tag As String) As String
			Dim connectionString As String
			Me.str_LastErroMsg = ""
			Try
				Dim lower As String = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.ToLower()
				If (lower.IndexOf(String.Concat(AppDomain.CurrentDomain.FriendlyName.ToLower(), ".config")) > 0) Then
					connectionString = ConfigurationManager.ConnectionStrings(pStr_DB_Tag).ConnectionString
					Return connectionString
				ElseIf (lower.IndexOf("web.config") > 0) Then
					connectionString = WebConfigurationManager.ConnectionStrings(pStr_DB_Tag).ConnectionString
					Return connectionString
				ElseIf (lower.IndexOf("app.config") > 0) Then
					connectionString = ConfigurationManager.ConnectionStrings(pStr_DB_Tag).ConnectionString
					Return connectionString
				End If
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				Me.str_LastErroMsg = exception.ToString()
				ProjectData.ClearProjectError()
			End Try
			connectionString = ""
			Return connectionString
		End Function

		Public Sub Commit_Trans()
			Me.dbCmd.Transaction.Commit()
		End Sub

		Public Function crete_command() As Boolean
			Return Me.n_crete_command()
		End Function

		Public Sub dispose_command()
			Me.n_dispose_command()
		End Sub

		Public Sub dispose_dbConn()
			Me.n_dispose_dbConn()
		End Sub

		Public Function executeActionQuery(ByVal pstrQuery As String, ByVal parr_str_param As String(,), ByVal pbol_StoreProc As Boolean) As Boolean
			Return Me.n_executeActionQuery(pstrQuery, parr_str_param, pbol_StoreProc)
		End Function

		Public Function executeQuery(ByVal pstrQuery As String, ByVal parr_str_param As String(,), ByVal pbol_StoreProc As Boolean) As SqlDataReader
			Return Me.n_executeQuery(pstrQuery, parr_str_param, pbol_StoreProc)
		End Function

		Public Function get_LastError() As String
			Return Me.str_LastErroMsg
		End Function

		Public Function getDBConn() As SqlConnection
			Return Me.dbConn
		End Function

		Private Sub Locate_ConnStr(ByVal pStr_DB_Tag_or_ConnStr As String)
			Dim str As String = Me.chk_config_file_auto(pStr_DB_Tag_or_ConnStr)
			If (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "", False) = 0) Then
				Me.str_connstr = pStr_DB_Tag_or_ConnStr
				Me.str_DB_Tag = ""
			Else
				Me.str_connstr = str
				Me.str_DB_Tag = pStr_DB_Tag_or_ConnStr
			End If
		End Sub

		Private Function locate_parameter(ByVal parr_str_param As String(,)) As Boolean
			Dim flag As Boolean
			Dim i As Integer = 0
			Dim flag1 As Boolean = False
			Me.dbCmd.Parameters.Clear()
			Dim length As Integer = parr_str_param.GetLength(0) - 1
			i = 0
			While True
				If (i > length) Then
					If (Not flag1) Then
						Dim num As Integer = parr_str_param.GetLength(0) - 1
						For i = 0 To num Step 1
							Dim sqlParameter As System.Data.SqlClient.SqlParameter = New System.Data.SqlClient.SqlParameter() With
							{
								.ParameterName = parr_str_param(i, 0),
								.Value = parr_str_param(i, 1)
							}
							Me.dbCmd.Parameters.Add(sqlParameter)
						Next

					End If
					flag = False
					Exit While
				ElseIf (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(parr_str_param(i, 0), "", False) <> 0) Then
					flag1 = False
					i = i + 1
				Else
					flag1 = True
					flag = True
					Exit While
				End If
			End While
			Return flag
		End Function

		Private Function n_active_connection() As Boolean
			Dim flag As Boolean
			Me.str_LastErroMsg = ""
			Try
				If (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Me.str_DB_Tag, "", False) <> 0) Then
					Me.str_connstr = Me.chk_config_file_auto(Me.str_DB_Tag)
				End If
				Me.dbConn = New SqlConnection(Me.str_connstr)
				Me.dbConn.Open()
				flag = True
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				Me.str_LastErroMsg = exception.ToString()
				flag = False
				ProjectData.ClearProjectError()
			End Try
			Return flag
		End Function

		Private Function n_check_conn_status() As Boolean
			Dim flag As Boolean
			If (Me.dbConn IsNot Nothing) Then
				If (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Me.str_DB_Tag, "", False) <> 0) Then
					Me.str_connstr = Me.chk_config_file_auto(Me.str_DB_Tag)
				End If
				flag = Me.active_connection()
			ElseIf (Me.dbConn IsNot Nothing) Then
				flag = If(Me.dbConn.State <> ConnectionState.Open, False, True)
			Else
				flag = False
			End If
			Return flag
		End Function

		Private Function n_crete_command() As Boolean
			Dim flag As Boolean
			Me.str_LastErroMsg = ""
			Try
				Me.dbCmd = New SqlCommand() With
				{
					.Connection = Me.dbConn
				}
				flag = False
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				Me.str_LastErroMsg = exception.ToString()
				flag = True
				ProjectData.ClearProjectError()
			End Try
			Return flag
		End Function

		Private Sub n_dispose_command()
			Try
				Me.dbDr.Close()
				Me.dbDr.Dispose()
				Me.dbDr = Nothing
			Catch exception As System.Exception
				ProjectData.SetProjectError(exception)
				ProjectData.ClearProjectError()
			End Try
			Try
				Me.dbCmd.Parameters.Clear()
				Me.dbCmd.Dispose()
				Me.dbCmd = Nothing
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				ProjectData.ClearProjectError()
			End Try
		End Sub

		Private Sub n_dispose_dbConn()
			Me.dbConn.Close()
			Me.dbConn.Dispose()
		End Sub

		Private Function n_executeActionQuery(ByVal pstrQuery As String, ByVal parr_str_param As String(,), ByVal pbol_StoreProc As Boolean) As Boolean
			Dim flag As Boolean
			Me.str_LastErroMsg = ""
			Try
				If (Not Me.n_check_conn_status()) Then
					Try
						Me.dbConn.Dispose()
					Catch exception As System.Exception
						ProjectData.SetProjectError(exception)
						ProjectData.ClearProjectError()
					End Try
					Me.dbConn = Nothing
					Me.active_connection()
				End If
				If (Me.dbCmd Is Nothing) Then
					If (Me.n_crete_command()) Then
						flag = True
						Return flag
					End If
				End If
				If (Not pbol_StoreProc) Then
					Me.dbCmd.CommandType = CommandType.Text
				Else
					Me.dbCmd.CommandType = CommandType.StoredProcedure
				End If
				If (parr_str_param IsNot Nothing) Then
					If (Me.locate_parameter(parr_str_param)) Then
						flag = True
						Return flag
					End If
				End If
				Me.dbCmd.CommandText = pstrQuery
				Me.dbCmd.ExecuteNonQuery()
				flag = False
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Me.str_LastErroMsg = exception1.ToString()
				flag = True
				ProjectData.ClearProjectError()
			End Try
			Return flag
		End Function

		Private Function n_executeQuery(ByVal pstrQuery As String, ByVal parr_str_param As String(,), ByVal pbol_StoreProc As Boolean) As System.Data.SqlClient.SqlDataReader
			Dim sqlDataReader As System.Data.SqlClient.SqlDataReader
			Me.str_LastErroMsg = ""
			Try
				If (Not Me.n_check_conn_status()) Then
					Try
						Me.dbConn.Dispose()
					Catch exception As System.Exception
						ProjectData.SetProjectError(exception)
						ProjectData.ClearProjectError()
					End Try
					Me.dbConn = Nothing
					Me.active_connection()
				End If
				If (Me.dbCmd Is Nothing) Then
					If (Me.n_crete_command()) Then
						sqlDataReader = Nothing
						Return sqlDataReader
					End If
				End If
				If (Not pbol_StoreProc) Then
					Me.dbCmd.CommandType = CommandType.Text
				Else
					Me.dbCmd.CommandType = CommandType.StoredProcedure
				End If
				If (parr_str_param Is Nothing) Then
					Me.dbCmd.Parameters.Clear()
				ElseIf (Me.locate_parameter(parr_str_param)) Then
					sqlDataReader = Nothing
					Return sqlDataReader
				End If
				Me.dbCmd.CommandText = pstrQuery
				Me.dbDr = Me.dbCmd.ExecuteReader()
				sqlDataReader = Me.dbDr
			Catch exception1 As System.Exception
				ProjectData.SetProjectError(exception1)
				Me.str_LastErroMsg = exception1.ToString()
				sqlDataReader = Nothing
				ProjectData.ClearProjectError()
			End Try
			Return sqlDataReader
		End Function

		Public Sub Rollback_Trans()
			Me.dbCmd.Transaction.Rollback()
		End Sub
	End Class
End Namespace