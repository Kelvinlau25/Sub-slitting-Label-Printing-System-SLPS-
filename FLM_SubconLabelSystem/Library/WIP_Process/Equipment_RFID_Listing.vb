Imports System
Imports System.Diagnostics

Namespace WIP_Process
	Public Class Equipment_RFID_Listing
		Private str_Equipment_RFID As String

		Public Property Equipment_RFID As String
			Get
				Return Me.str_Equipment_RFID
			End Get
			Set(ByVal value As String)
				Me.str_Equipment_RFID = value
			End Set
		End Property

		<DebuggerNonUserCode>
		Public Sub New()
			MyBase.New()
		End Sub
	End Class
End Namespace