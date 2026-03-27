Imports System
Imports System.Diagnostics

Namespace WIP_Process
	Public Class Machine_Name_Dtl
		Private str_ID_MM_MACHINE As String

		Private str_MACHINE_CODE As String

		Private str_MACHINE_DESC As String

		Public Property ID_MM_MACHINE As String
			Get
				Return Me.str_ID_MM_MACHINE
			End Get
			Set(ByVal value As String)
				Me.str_ID_MM_MACHINE = value
			End Set
		End Property

		Public Property MACHINE_CODE As String
			Get
				Return Me.str_MACHINE_CODE
			End Get
			Set(ByVal value As String)
				Me.str_MACHINE_CODE = value
			End Set
		End Property

		Public Property MACHINE_DESC As String
			Get
				Return Me.str_MACHINE_DESC
			End Get
			Set(ByVal value As String)
				Me.str_MACHINE_DESC = value
			End Set
		End Property

		<DebuggerNonUserCode>
		Public Sub New()
			MyBase.New()
		End Sub
	End Class
End Namespace