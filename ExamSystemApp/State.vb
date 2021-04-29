Imports MySql.Data.MySqlClient

Public Class State
    Dim con As MySqlConnection = New MySqlConnection("server=remotemysql.com;user id=HmysA4C1s5;password=4rEBrLepoZ;database=HmysA4C1s5;sslMode=none")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim dt As DataTable
    Dim sql As String
    Dim mxrow As Integer
    Dim mode As Integer REM 0 is new 1 is update

    Public Function retrieve_single_result(ByVal sql As String)
        Dim maxrow As Integer = 0

        Try
            con.Open()
            cmd = New MySqlCommand
            da = New MySqlDataAdapter
            dt = New DataTable
            With cmd
                .Connection = con
                .CommandText = sql
            End With
            da.SelectCommand = cmd
            da.Fill(dt)

            maxrow = dt.Rows.Count

        Catch ex As Exception
            Debug.Write(ex.StackTrace)
            MessageBox.Show(ex.Message)
        Finally
            con.Close()
            da.Dispose()
        End Try
        Return maxrow
    End Function

    Private Sub CallSelect()
        Dim nRows As Integer
        nRows = 0
        sql = "SELECT * FROM test "
        mxrow = retrieve_single_result(sql)
        REM MessageBox.Show("rows returned " + mxrow.ToString)
        If mxrow > 0 Then
            Do While (nRows < mxrow)
                REM MessageBox.Show(dt.Rows(nRows).Item("Name"))
                ListBox1.Items.Add(dt.Rows(nRows).Item("Name"))
                nRows += 1
            Loop
        End If
    End Sub


    Private Sub setFieldsSelectedIndex()
        If (ListBox1.SelectedIndex > -1) Then
            With dt.Rows(ListBox1.SelectedIndex)
                TextBoxID.Text = .Item("ID")
                TextBoxName.Text = .Item("Name")
                TextBoxCode.Text = .Item("Code")
            End With
            mode = 1
        End If
    End Sub
    Private Sub State_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Debug.WriteLine("form state loade even method")
        Call CallSelect()
        Me.WindowState = FormWindowState.Maximized
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
        Dim sql = "Insert into test(Name, Code) values (@Name, @Code)"
        REM sql = "Insert into test(Name, Code) values ('" + TextBoxName.Text + "','" + TextBoxCode.Text + "')"
        Try
            con.Open()
            cmd = New MySqlCommand
            da = New MySqlDataAdapter
            dt = New DataTable
            With cmd
                .Connection = con
                .CommandText = sql
            End With

            cmd.Parameters.AddWithValue("@Name", TextBoxName.Text)
            cmd.Parameters.AddWithValue("@Code", TextBoxCode.Text)
            cmd.Prepare()
            cmd.ExecuteNonQuery()
            MessageBox.Show("Inserted!")
        Catch ex As Exception
            Debug.Write(ex.StackTrace)
            MessageBox.Show(ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub UpdateRec()
        Dim sql = "Update test Set Name = @Name , Code =  @Code Where ID = @ID"

        Try
            con.Open()
            cmd = New MySqlCommand
            da = New MySqlDataAdapter
            dt = New DataTable
            With cmd
                .Connection = con
                .CommandText = sql
            End With

            cmd.Parameters.AddWithValue("@Name", TextBoxName.Text)
            cmd.Parameters.AddWithValue("@Code", TextBoxCode.Text)
            cmd.Parameters.AddWithValue("@ID", TextBoxID.Text)
            cmd.Prepare()
            cmd.ExecuteNonQuery()
            MessageBox.Show("Updated!")
        Catch ex As Exception
            Debug.Write(ex.StackTrace)
            MessageBox.Show(ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub


    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        REM sender has selected item
        REM listbox1's selected index would be set
        REM MessageBox.Show(ListBox1.SelectedIndex.ToString)
        Call setFieldsSelectedIndex()
    End Sub

    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        TextBoxCode.Text = ""
        TextBoxName.Text = ""
        TextBoxID.Text = ""
        mode = 0

    End Sub
End Class