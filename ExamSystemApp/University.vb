﻿Imports MySql.Data.MySqlClient
Public Class University

    Dim con As MySqlConnection = New MySqlConnection("server=13.71.16.66;user id=itfyme;password=itfyme;database=examsystem;sslMode=none")
    Dim cmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim dt As DataTable
    Dim ds As DataSet
    Dim stateDT As DataTable
    Dim cityDT As DataTable
    Dim sql As String
    Dim mxrow As Integer
    Dim mode As Integer REM 0 is new 1 is update
    Dim isModified As Boolean
    Dim ID As Integer ' holds current updated universityID number

    Private Sub GetState()
        ' retrive the state and bind to combo box
        con.Open()
        cmd = New MySqlCommand
        da = New MySqlDataAdapter
        stateDT = New DataTable

        With cmd
            .Connection = con
            .CommandText = "sStateGetList"
            .CommandType = CommandType.StoredProcedure
        End With

        cmd.Parameters.AddWithValue("pStateIDs", "")
        da.SelectCommand = cmd
        da.Fill(stateDT)
        cmbState.DataSource = stateDT
        cmbState.DisplayMember = "Name"
        cmbState.ValueMember = "StateID"
        con.Close()
    End Sub
    Private Sub GetCity()
        ' retrive the state and bind to combo box
        con.Open()
        cmd = New MySqlCommand
        da = New MySqlDataAdapter
        cityDT = New DataTable

        With cmd
            .Connection = con
            .CommandText = "sCityGetList"
            .CommandType = CommandType.StoredProcedure
        End With

        cmd.Parameters.AddWithValue("pCityIDs", "")
        da.SelectCommand = cmd
        da.Fill(cityDT)
        cmbCity.DataSource = cityDT
        cmbCity.DisplayMember = "Name"
        cmbCity.ValueMember = "CityID"
        con.Close()
    End Sub
    Private Sub GetUniversity()
        ' retrive the state and bind to combo box
        con.Open()
        cmd = New MySqlCommand
        da = New MySqlDataAdapter
        ds = New DataSet
        dt = New DataTable

        With cmd
            .Connection = con
            .CommandText = "sUniversityGetListPage"
            .CommandType = CommandType.StoredProcedure
        End With

        cmd.Parameters.AddWithValue("pUniversityName", "")
        cmd.Parameters.AddWithValue("pStateID", 0)
        cmd.Parameters.AddWithValue("pCityID", 0)
        cmd.Parameters.AddWithValue("pPageNum", 1)
        cmd.Parameters.AddWithValue("pPageSize", 20)
        da.SelectCommand = cmd
        da.Fill(ds)
        ListBox1.DataSource = ds.Tables(1)
        ListBox1.DisplayMember = "Name"
        ListBox1.ValueMember = "UniversityID"
        con.Close()
    End Sub
    Private Sub FilterCityBasedOnStateSelected()
        ' stateDT contains statelist
        ' cityDT contains citylist
        cityDT.Select("StateID = " + cmbCity.SelectedValue.ToString)
    End Sub
    Private Sub University_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' get state list - to bind to state dropdown
        ' get city list - to bind to city dropdown
        ' get univsersity list - to show on the list
        GetState()
        GetCity()
        GetUniversity()
        'cmbState.SelectedIndex = 0
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbState.SelectedIndexChanged
        'FilterCityBasedOnStateSelected()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If (mode = 0) Then
            Call InsertRec()
        Else
            Call UpdateRec()
        End If
    End Sub

    Private Sub InsertRec()
        Dim sql = "sUniversityAdd"
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
            cmd.Parameters.AddWithValue("pAddr1", txtAddr1.Text)
            cmd.Parameters.AddWithValue("pAddr2", txtAdd2.Text)
            cmd.Parameters.AddWithValue("pAddr3", txtAdd3.Text)
            cmd.Parameters.AddWithValue("pCityID", cmbCity.SelectedValue)
            cmd.Parameters.AddWithValue("pStateID", cmbState.SelectedValue)
            cmd.Parameters.AddWithValue("pPinCode", 1)
            cmd.Parameters.AddWithValue("pPhone", "")
            cmd.Parameters.AddWithValue("pEmail", "")
            cmd.Parameters.AddWithValue("pLogo", "")
            cmd.Parameters.AddWithValue("pURL", "")
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
        Dim sql = "sUniversityUpdte"
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

            cmd.Parameters.AddWithValue("pUnivesityID", ID)
            cmd.Parameters.AddWithValue("pName", TextBoxName.Text)
            cmd.Parameters.AddWithValue("pCode", TextBoxCode.Text)
            cmd.Parameters.AddWithValue("pAddr1", txtAddr1.Text)
            cmd.Parameters.AddWithValue("pAddr2", txtAdd2.Text)
            cmd.Parameters.AddWithValue("pAddr3", txtAdd3.Text)
            cmd.Parameters.AddWithValue("pCityID", cmbCity.SelectedValue)
            cmd.Parameters.AddWithValue("pStateID", cmbState.SelectedValue)
            cmd.Parameters.AddWithValue("pPinCode", "")
            cmd.Parameters.AddWithValue("pPhone", "")
            cmd.Parameters.AddWithValue("pEmail", "")
            cmd.Parameters.AddWithValue("pLogo", "")
            cmd.Parameters.AddWithValue("pURL", "")
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
End Class