Public Class MainForm
    Private Sub Menu1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MainMenu.ItemClicked
        If e.ClickedItem.Name = "Logout" Then
            Me.Close()
        End If
    End Sub

    Private Sub Master_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles Master.DropDownItemClicked
        Dim menuText = e.ClickedItem.ToString()

        Select Case (menuText)
            Case "State"
                Dim stateForm As New State()
                stateForm.MdiParent = Me
                stateForm.Show()
            Case Else

        End Select

    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim loginForm As New LoginForm
        Dim result = loginForm.ShowDialog(Me)
        Debug.Write(result)
        If result <> DialogResult.OK Then
            loginForm.Dispose()
            Me.Close()
        End If
    End Sub
End Class
