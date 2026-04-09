Imports Microsoft.VisualBasic.CompilerServices
Imports Oracle.DataAccess.Client
Imports System
Imports System.ComponentModel
Imports System.Configuration
Imports System.Data
Imports System.Web.Configuration

Namespace cls_DB
    Public Class cls_Oracle
        Private dbOraConn As OracleConnection

        Private dbOraCmd As OracleCommand

        Private dbOraParam As OracleParameter

        Private str_connstr As String

        Private dbOraDr As OracleDataReader

        Private str_LastErroMsg As String

        Private str_DB_Tag As String

        Public Sub New(ByVal pStr_DB_Tag_or_ConnStr As String)
            MyBase.New()
            Me.dbOraConn = Nothing
            Me.dbOraCmd = Nothing
            Me.dbOraParam = Nothing
            Me.str_connstr = ""
            Me.dbOraDr = Nothing
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

        Public Function crete_command() As Boolean
            Return Me.n_crete_command()
        End Function

        Public Sub dispose_command()
            Me.n_dispose_command()
        End Sub

        Public Sub dispose_dbConn()
            Me.n_dispose_dbConn()
        End Sub

        Public Function executeActionQuery(ByVal pstrQuery As String, ByVal parr_str_param As String(,), ByVal pbol_StoreProc As Boolean) As String
            Return Me.n_executeActionQuery(pstrQuery, parr_str_param, pbol_StoreProc)
        End Function

        Public Function executeQuery(ByVal pstrQuery As String, ByVal parr_str_param As String(,), ByVal pbol_StoreProc As Boolean) As OracleDataReader
            Return Me.n_executeQuery(pstrQuery, parr_str_param, pbol_StoreProc)
        End Function

        Public Function get_LastError() As String
            Return Me.str_LastErroMsg
        End Function

        Public Function getDBConn() As OracleConnection
            Return Me.dbOraConn
        End Function

        Private Sub Locate_ConnStr(ByVal pStr_DB_Tag_or_ConnStr As String)
            Dim str As String = Me.chk_config_file_auto(pStr_DB_Tag_or_ConnStr)
            If (Operators.CompareString(str, "", False) = 0) Then
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
            Me.dbOraCmd.Parameters.Clear()
            Dim length As Integer = parr_str_param.GetLength(0) - 1
            i = 0
            While True
                If (i > length) Then
                    If (Not flag1) Then
                        Dim num As Integer = parr_str_param.GetLength(0) - 1
                        For i = 0 To num Step 1
                            Dim oracleParameter As Oracle.DataAccess.Client.OracleParameter = New Oracle.DataAccess.Client.OracleParameter() With
                            {
                                .ParameterName = parr_str_param(i, 0),
                                .Value = parr_str_param(i, 1)
                            }
                            Me.dbOraCmd.Parameters.Add(oracleParameter)
                        Next

                    End If
                    flag = False
                    Exit While
                ElseIf (Operators.CompareString(parr_str_param(i, 0), "", False) <> 0) Then
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
                If (Operators.CompareString(Me.str_DB_Tag, "", False) <> 0) Then
                    Me.str_connstr = Me.chk_config_file_auto(Me.str_DB_Tag)
                End If
                Me.dbOraConn = New OracleConnection(Me.str_connstr)
                Me.dbOraConn.Open()
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
            If (Me.dbOraConn IsNot Nothing) Then
                If (Operators.CompareString(Me.str_DB_Tag, "", False) <> 0) Then
                    Me.str_connstr = Me.chk_config_file_auto(Me.str_DB_Tag)
                End If
                flag = Me.active_connection()
            ElseIf (Me.dbOraConn IsNot Nothing) Then
                flag = If(Me.dbOraConn.State <> 1, False, True)
            Else
                flag = False
            End If
            Return flag
        End Function

        Private Function n_crete_command() As Boolean
            Dim flag As Boolean
            Me.str_LastErroMsg = ""
            Try
                Me.dbOraCmd = New OracleCommand() With
                {
                    .Connection = Me.dbOraConn
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
                Me.dbOraDr.Close()
                Me.dbOraDr.Dispose()
                Me.dbOraDr = Nothing
            Catch exception As System.Exception
                ProjectData.SetProjectError(exception)
                ProjectData.ClearProjectError()
            End Try
            Try
                Me.dbOraCmd.Parameters.Clear()
                Me.dbOraCmd.Dispose()
                Me.dbOraCmd = Nothing
            Catch exception1 As System.Exception
                ProjectData.SetProjectError(exception1)
                ProjectData.ClearProjectError()
            End Try
        End Sub

        Private Sub n_dispose_dbConn()
            Me.dbOraConn.Close()
            Me.dbOraConn.Dispose()
        End Sub

        Private Function n_executeActionQuery(ByVal pstrQuery As String, ByVal parr_str_param As String(,), ByVal pbol_StoreProc As Boolean) As String
            Dim str As String
            Me.str_LastErroMsg = ""
            Try
                If (Not Me.n_check_conn_status()) Then
                    Try
                        Me.dbOraConn.Dispose()
                    Catch exception As System.Exception
                        ProjectData.SetProjectError(exception)
                        ProjectData.ClearProjectError()
                    End Try
                    Me.dbOraConn = Nothing
                    Me.active_connection()
                End If
                If (Me.dbOraCmd Is Nothing) Then
                    If (Me.n_crete_command()) Then
                        str = Conversions.ToString(True)
                        Return str
                    End If
                End If
                If (Not pbol_StoreProc) Then
                    Me.dbOraCmd.CommandType = 1
                Else
                    Me.dbOraCmd.CommandType = 4
                End If
                If (parr_str_param Is Nothing) Then
                    Me.dbOraCmd.Parameters.Clear()
                    Me.dbOraCmd.Parameters.Add(New OracleParameter("RETURN_VALUE", OracleDbType.Int64, 20)).Direction = 2
                    dbOraCmd.Parameters("RETURN_VALUE").DbType = DbType.Int64

                ElseIf (Not Me.locate_parameter(parr_str_param)) Then
                    Me.dbOraCmd.Parameters.Add(New OracleParameter("RETURN_VALUE", OracleDbType.Int64, 20)).Direction = 2
                    dbOraCmd.Parameters("RETURN_VALUE").DbType = DbType.Int64
                Else
                    str = Conversions.ToString(True)
                    Return str
                End If
                Me.dbOraCmd.CommandText = pstrQuery
                Me.dbOraCmd.ExecuteNonQuery()
                Dim str1 As String = Me.dbOraCmd.Parameters("RETURN_VALUE").Value.ToString()
                str = str1
            Catch exception1 As System.Exception
                ProjectData.SetProjectError(exception1)
                Me.str_LastErroMsg = exception1.ToString()
                str = Conversions.ToString(True)
                ProjectData.ClearProjectError()
            End Try
            Return str
        End Function

        Private Function n_executeQuery(ByVal pstrQuery As String, ByVal parr_str_param As String(,), ByVal pbol_StoreProc As Boolean) As OracleDataReader
            Dim oracleDataReaders As OracleDataReader
            Me.str_LastErroMsg = ""
            Try
                If (Not Me.n_check_conn_status()) Then
                    Try
                        Me.dbOraConn.Dispose()
                    Catch exception As System.Exception
                        ProjectData.SetProjectError(exception)
                        ProjectData.ClearProjectError()
                    End Try
                    Me.dbOraConn = Nothing
                    Me.active_connection()
                End If
                If (Me.dbOraCmd Is Nothing) Then
                    If (Me.n_crete_command()) Then
                        oracleDataReaders = Nothing
                        Return oracleDataReaders
                    End If
                End If
                If (Not pbol_StoreProc) Then
                    Me.dbOraCmd.CommandType = 1
                Else
                    Me.dbOraCmd.CommandType = 4
                End If
                If (parr_str_param Is Nothing) Then
                    Me.dbOraCmd.Parameters.Clear()
                    Me.dbOraCmd.Parameters.Add(New OracleParameter("sref", OracleDbType.RefCursor)).Direction = 2
                ElseIf (Not Me.locate_parameter(parr_str_param)) Then
                    Me.dbOraCmd.Parameters.Add(New OracleParameter("sref", OracleDbType.RefCursor)).Direction = 2
                Else
                    oracleDataReaders = Nothing
                    Return oracleDataReaders
                End If
                Me.dbOraCmd.CommandText = pstrQuery
                Me.dbOraDr = Me.dbOraCmd.ExecuteReader()
                oracleDataReaders = Me.dbOraDr
            Catch exception1 As System.Exception
                ProjectData.SetProjectError(exception1)
                Me.str_LastErroMsg = exception1.ToString()
                oracleDataReaders = Nothing
                ProjectData.ClearProjectError()
            End Try
            Return oracleDataReaders
        End Function
    End Class
End Namespace