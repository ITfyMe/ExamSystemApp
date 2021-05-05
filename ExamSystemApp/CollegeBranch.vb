Public Class CollegeBranch
    Dim ListCollege As List(Of CCollege) = New List(Of CCollege)
    Dim ListBranch As List(Of CBranch) = New List(Of CBranch)

    Private Sub PopulateBranch()
        For index As Int16 = 1 To 15
            ListBranch.Add(New CBranch(index, "Branch" + index.ToString, "B" + index.ToString))
        Next
    End Sub
    Private Sub PopulateCollege()
        For index As Int16 = 1 To 10
            ListCollege.Add(New CCollege(index, "College" + index.ToString, "CC" + index.ToString))
        Next
    End Sub
    Private Sub CollegeBranch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PopulateBranch()
        PopulateCollege()
        lbCollege.DisplayMember = "Name"
        lbCollege.ValueMember = "ID"
        lbCollege.DataSource = ListCollege
        grdBranch.DataSource = ListBranch


        ' make all columns read only
        'For Each column In grdBranch.Columns
        '    column.ReadOnly = True
        'Next

        ' add one column to shown the check box to select the branches
        Dim chk As New DataGridViewCheckBoxColumn
        grdBranch.Columns.Add(chk)
        chk.HeaderText = "Select"
        chk.Name = "chk"
    End Sub

    Private Sub lbCollege_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbCollege.SelectedIndexChanged
        txtCollege.Text = ListCollege(lbCollege.SelectedIndex).Name
    End Sub
End Class