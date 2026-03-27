Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class WebService
     Inherits System.Web.Services.WebService

    Public Class ColorDetail
        Public ID As String
        Public SerialNo As String
        Public ColorSeq As String
        Public ColorWay As String
        Public ContractQty As Integer
    End Class

    <WebMethod()> _
   Public Function GetColorDetailList() As List(Of ColorDetail)
        MsgBox("vxcvxc")
        GetColorDetailList = New List(Of ColorDetail)
        For i As Integer = 0 To 100
            Dim temp As New ColorDetail
            temp.ID = "" & i
            temp.SerialNo = "SerialNo" & i
            temp.ColorSeq = "ColorSeq" & i
            temp.ColorWay = "ColorWay" & i
            temp.ContractQty = i
            GetColorDetailList.Add(temp)
        Next


        Return GetColorDetailList

    End Function

End Class
