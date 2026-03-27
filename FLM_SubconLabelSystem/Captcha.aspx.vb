Imports System.Drawing
Imports System.Drawing.Imaging

Imports System.Drawing.Text
Partial Class Captcha
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'create object of Bitmap Class and set its width and height.
        Dim objBMP As Bitmap = New Bitmap(180, 51)
        'Create Graphics object and assign bitmap object to graphics' object.
        Dim objGraphics As Graphics = Graphics.FromImage(objBMP)
        objGraphics.Clear(Color.OrangeRed)
        objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias
        Dim objFont As Font = New Font("arial", 30, FontStyle.Regular)
        'genetating random 6 digit random number
        Dim randomStr As String = GeneratePassword()
        'set this random number in session
        Session.Add("randomStr", randomStr)
        objGraphics.DrawString(randomStr, objFont, Brushes.White, 2, 2)
        Response.ContentType = "image/GIF"
        objBMP.Save(Response.OutputStream, ImageFormat.Gif)
        objFont.Dispose()
        objGraphics.Dispose()
        objBMP.Dispose()
    End Sub
    Public Function GeneratePassword() As String
        ' Below code describes how to create random numbers.some of the digits and letters
        ' are ommited because they look same like "i","o","1","0","I","O".
        Dim allowedChars As String = "a,b,c,d,e,f,g,h,j,k,m,n,p,q,r,s,t,u,v,w,x,y,z,"
        allowedChars += "A,B,C,D,E,F,G,H,J,K,L,M,N,P,Q,R,S,T,U,V,W,X,Y,Z,"
        allowedChars += "2,3,4,5,6,7,8,9"
        Dim sep() As Char = {","c}
        Dim arr() As String = allowedChars.Split(sep)
        Dim passwordString As String = ""
        Dim temp As String
        Dim rand As Random = New Random()
        Dim i As Integer
        For i = 0 To 6 - 1 Step i + 1
            temp = arr(rand.Next(0, arr.Length))
            passwordString += temp
        Next
        Return passwordString
    End Function
End Class