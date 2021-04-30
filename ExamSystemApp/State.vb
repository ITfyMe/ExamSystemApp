Imports MySql.Data.MySqlClient

Public Class State
    REM Dim con As MySqlConnection = New MySqlConnection("server=remotemysql.com;user id=HmysA4C1s5;password=4rEBrLepoZ;database=HmysA4C1s5;sslMode=none")
    Dim con As MySqlConnection = New MySqlConnection("server=13.71.16.66;user id=itfyme;password=itfyme;database=examsystem;sslMode=none")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim dt As DataTable
    Dim ds As DataSet
    Dim sql As String
    Dim mxrow As Integer
    Dim mode As Integer REM 0 is new 1 is update
    Dim isModified As Boolean

    Public Function GetData(ByVal sql As String)
        Dim maxrow As Integer = 0

        Try
            con.Open()
            cmd = New MySqlCommand
            da = New MySqlDataAdapter
            dt = New DataTable
            ds = New DataSet


            ' calling stored procedure
            With cmd
                .Connection = con
                .CommandText = sql
                .CommandType = CommandType.StoredProcedure
            End With

            ' add parameters to the stored procedure
            cmd.Parameters.AddWithValue("pStateName", "")
            cmd.Parameters.AddWithValue("pCode", "")
            cmd.Parameters.AddWithValue("pPageNum", 1)
            cmd.Parameters.AddWithValue("pPageSize", 40)
            REM we are executing two sql queries and results come in two result set
            REM use dataset to get the result
            da.SelectCommand = cmd

            da.Fill(ds)

            'Now the data contain two tables and their row count may be zero 
            If ds.Tables(0).Rows.Count > 0 Then
                REM ds contains mulitple tables which you can loop to get the data
                Dim nRows = ds.Tables(0).Rows(0).Item(0)
                lblRows.Text = "Showing " + nRows.ToString + " records"
                dt = ds.Tables(1)
                maxrow = dt.Rows.Count
            End If
            maxrow = dt.Rows.Count

        Catch ex As Exception
            Debug.Write(ex.StackTrace)
            MessageBox.Show(ex.Message)
        Finally
            con.Close()

            'da.Dispose()
        End Try
        Return maxrow
    End Function

    Private Sub CallSelect()
        Dim nRows As Integer
        nRows = 0
        ' initialize the procedure name to get the data
        sql = "sStateGetListPage"
        mxrow = GetData(sql)
        'MessageBox.Show("rows returned " + mxrow.ToString)

        'If mxrow > 0 Then
        '    Do While (nRows < mxrow)
        '        REM MessageBox.Show(dt.Rows(nRows).Item("Name"))
        '        ListBox1.Items.Add(dt.Rows(nRows).Item("Name"))
        '        nRows += 1
        '    Loop
        'End If
        ListBox1.DataSource = dt
        ListBox1.DisplayMember = "Name"
    End Sub


    Private Sub setFieldsSelectedIndex()
        If (ListBox1.SelectedIndex > -1) Then
            With dt.Rows(ListBox1.SelectedIndex)
                TextBoxID.Text = .Item("StateID")
                TextBoxName.Text = .Item("Name")
                TextBoxCode.Text = .Item("Code")
            End With
            'TextBoxID.Text = dt.Rows(ListBox1.SelectedIndex).Item("StateID")
            'TextBoxName.Text = dt.Rows(ListBox1.SelectedIndex).Item("Name")
            'TextBoxCode.Text = dt.Rows(ListBox1.SelectedIndex).Item("Code")
            mode = 1
        End If
    End Sub
    Private Sub State_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' get list of state from the database
        Call CallSelect()
        Me.WindowState = FormWindowState.Maximized
        isModified = False
    End Sub

    Private Sub Save_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        REM this can new record or old record ?
        If (mode = 0) Then
            Call InsertRec()
        Else
            Call UpdateRec()
        End If
    End Sub

    Private Sub InsertRec()
        Dim sql = "sStateAdd"
        Try
            con.Open()
            cmd = New MySqlCommand
            da = New MySqlDataAdapter
            dt = New DataTable
            With cmd
                .Connection = con
                .CommandText = sql
                .CommandType = CommandType.StoredProcedure
            End With

            cmd.Parameters.AddWithValue("pName", TextBoxName.Text)
            cmd.Parameters.AddWithValue("pCode", TextBoxCode.Text)
            cmd.Prepare()
            cmd.ExecuteNonQuery()
            isModified = False
            MessageBox.Show("Inserted!")
        Catch ex As Exception
            Debug.Write(ex.StackTrace)
            MessageBox.Show(ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub UpdateRec()
        Dim sql = "sStateUpdate"

        Try
            con.Open()
            cmd = New MySqlCommand
            da = New MySqlDataAdapter
            dt = New DataTable
            With cmd
                .Connection = con
                .CommandText = sql
                .CommandType = CommandType.StoredProcedure
            End With

            cmd.Parameters.AddWithValue("pName", TextBoxName.Text)
            cmd.Parameters.AddWithValue("pCode", TextBoxCode.Text)
            cmd.Parameters.AddWithValue("pStateID", TextBoxID.Text)
            cmd.Prepare()
            cmd.ExecuteNonQuery()
            isModified = False
            MessageBox.Show("Updated!")
        Catch ex As Exception
            Debug.Write(ex.StackTrace)
            MessageBox.Show(ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub ResetFields()
        TextBoxCode.Text = ""
        TextBoxName.Text = ""
        TextBoxID.Text = ""
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If isModified = True Then
            Dim res = MsgBox("Are ou sure you dont want to save the data?", MsgBoxStyle.OkCancel)
            If (res = MsgBoxResult.Ok) Then
                ResetFields()
            End If
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        REM sender has selected item
        REM listbox1's selected index would be set
        REM MessageBox.Show(ListBox1.SelectedIndex.ToString)
        Call setFieldsSelectedIndex()
    End Sub

    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Call ResetFields()
        mode = 0 ' New
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If isModified = True Then
            Dim res = MsgBox("Unsaved data is pending. Do you want to close?", MsgBoxStyle.OkCancel)
            If (res = MsgBoxResult.Ok) Then
                Me.Close()
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub TextBoxName_TextChanged(sender As Object, e As EventArgs) Handles TextBoxName.TextChanged
        isModified = True
    End Sub

    Private Sub TextBoxCode_TextChanged(sender As Object, e As EventArgs) Handles TextBoxCode.TextChanged
        isModified = True
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Dim importForm = New Import
        importForm.ShowDialog(Me)
    End Sub
End Class