Imports Microsoft.VisualBasic

Public Class GlobalFunctions

    Public Shared Function encrypt(ByVal plaintext As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim SHA256 As New System.Security.Cryptography.SHA256Cng
        Dim ciphertext As String = ""
        Try
            AES.GenerateIV()
            AES.Key = SHA256.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes("N882A66A56299823AA026E8GARR1B527WDYY3A6EA2FF14F8DB70AB2829B69DDF"))

            AES.Mode = System.Security.Cryptography.CipherMode.CBC
            Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
            Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(plaintext)
            ciphertext = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))

            Return Convert.ToBase64String(AES.IV) & Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))

        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Shared Function decrypt(ByVal ciphertext As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim SHA256 As New System.Security.Cryptography.SHA256Cng
        Dim plaintext As String = ""
        Dim iv As String = ""
        Try
            Dim delim As String() = New String(0) {"=="}
            Dim ivct = ciphertext.Split(delim, StringSplitOptions.None)
            iv = ivct(0) & "=="
            ciphertext = If(ivct.Length = 3, ivct(1) & "==", ivct(1))


            AES.Key = SHA256.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes("N882A66A56299823AA026E8GARR1B527WDYY3A6EA2FF14F8DB70AB2829B69DDF"))
            AES.IV = Convert.FromBase64String(iv)
            AES.Mode = System.Security.Cryptography.CipherMode.CBC
            Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
            Dim Buffer As Byte() = Convert.FromBase64String(ciphertext)
            plaintext = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return plaintext
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function


    Public Shared Function retriveIV(ByVal ciphertext As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim SHA256 As New System.Security.Cryptography.SHA256Cng
        Dim plaintext As String = ""
        Dim iv As String = ""
        Try
            Dim delim As String() = New String(0) {"=="}
            Dim ivct = ciphertext.Split(delim, StringSplitOptions.None)
            iv = ivct(0) & "=="
            ciphertext = If(ivct.Length = 3, ivct(1) & "==", ivct(1))


            AES.Key = SHA256.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes("N882A66A56299823AA026E8GARR1B527WDYY3A6EA2FF14F8DB70AB2829B69DDF"))
            AES.IV = Convert.FromBase64String(iv)
            AES.Mode = System.Security.Cryptography.CipherMode.CBC
            Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
            Dim Buffer As Byte() = Convert.FromBase64String(ciphertext)
            plaintext = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return iv
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Shared Function encryptIV(ByVal plaintext As String, ByVal iv As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim SHA256 As New System.Security.Cryptography.SHA256Cng
        Dim ciphertext As String = ""
        Try

            AES.Key = SHA256.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes("N882A66A56299823AA026E8GARR1B527WDYY3A6EA2FF14F8DB70AB2829B69DDF"))
            AES.IV = Convert.FromBase64String(iv)
            AES.Mode = System.Security.Cryptography.CipherMode.CBC
            Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
            Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(plaintext)
            ciphertext = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))

            Return Convert.ToBase64String(AES.IV) & Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))

        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

End Class
