Imports System
Imports System.Diagnostics

Namespace WIP_Process
	Public Class RFID_Reader_Setting
		Private str_Station_Name As String

		Private str_Catagory As String

		Private str_Exe_Name As String

		Private str_IPAddr As String

		Private str_AppPath As String

		Public Property AppPath As String
			Get
				Return Me.str_AppPath
			End Get
			Set(ByVal value As String)
				Me.str_AppPath = value
			End Set
		End Property

		Public Property Catagory As String
			Get
				Return Me.str_Catagory
			End Get
			Set(ByVal value As String)
				Me.str_Catagory = value
			End Set
		End Property

		Public Property Exe_Name As String
			Get
				Return Me.str_Exe_Name
			End Get
			Set(ByVal value As String)
				Me.str_Exe_Name = value
			End Set
		End Property

		Public Property IP_Addr As String
			Get
				Return Me.str_IPAddr
			End Get
			Set(ByVal value As String)
				Me.str_IPAddr = value
			End Set
		End Property

		Public Property Station_Name As String
			Get
				Return Me.str_Station_Name
			End Get
			Set(ByVal value As String)
				Me.str_Station_Name = value
			End Set
		End Property

		<DebuggerNonUserCode>
		Public Sub New()
			MyBase.New()
		End Sub
	End Class
End Namespace