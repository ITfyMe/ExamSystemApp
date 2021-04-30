Imports System.IO
Imports System.Data
Public Class Import
    Private Sub Import_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim fName As String = ""
        OpenFileDialog1.InitialDirectory = "c:\desktop"
        OpenFileDialog1.Filter = "CSV files (*.csv)|*.CSV"
        OpenFileDialog1.FilterIndex = 2
        OpenFileDialog1.RestoreDirectory = True
        If (OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            fName = OpenFileDialog1.FileName
        End If

        Dim TextLine As String = ""
        Dim SplitLine() As String
        Dim nLine = 0
        If System.IO.File.Exists(fName) = True Then
            gridImport.Columns.Add("Name", "Name")
            gridImport.Columns.Add("Code", "Code")
            Dim objReader As New System.IO.StreamReader(fName)
            Do While objReader.Peek() <> -1
                TextLine = objReader.ReadLine()
                If (nLine = 0) Then
                    Debug.Write("Header text" + TextLine)
                Else
                    'gridImport.Columns.AddRange()
                End If

                SplitLine = Split(TextLine, ",")
                Debug.Write(SplitLine.Count)
                Me.gridImport.Rows.Add(SplitLine)
            Loop
        Else
            MsgBox("File Does Not Exist")
        End If
    End Sub
End Class