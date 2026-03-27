Imports System.Reflection
Imports System.Resources
Imports System.Globalization

Namespace Control
    ''' <summary>
    ''' Retrieve the value from the resource page
    ''' -----------------------------------------
    ''' C.C.Yeon    25 April 2011   initial Version
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Resource
        Public Shared Function RetrieveValue(ByVal Resource As String, ByVal Field As String) As String
            Dim mng As New ResourceManager(Resource, Assembly.GetExecutingAssembly)
            RetrieveValue = mng.GetString(Field)
        End Function
    End Class
End Namespace

