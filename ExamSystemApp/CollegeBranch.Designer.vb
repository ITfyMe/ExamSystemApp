<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CollegeBranch
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lbCollege = New System.Windows.Forms.ListBox()
        Me.grdBranch = New System.Windows.Forms.DataGridView()
        Me.txtCollege = New System.Windows.Forms.TextBox()
        CType(Me.grdBranch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbCollege
        '
        Me.lbCollege.FormattingEnabled = True
        Me.lbCollege.ItemHeight = 15
        Me.lbCollege.Location = New System.Drawing.Point(12, 28)
        Me.lbCollege.Name = "lbCollege"
        Me.lbCollege.Size = New System.Drawing.Size(121, 394)
        Me.lbCollege.TabIndex = 0
        '
        'grdBranch
        '
        Me.grdBranch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdBranch.Location = New System.Drawing.Point(197, 71)
        Me.grdBranch.Name = "grdBranch"
        Me.grdBranch.RowTemplate.Height = 25
        Me.grdBranch.Size = New System.Drawing.Size(559, 351)
        Me.grdBranch.TabIndex = 1
        '
        'txtCollege
        '
        Me.txtCollege.Location = New System.Drawing.Point(197, 39)
        Me.txtCollege.Name = "txtCollege"
        Me.txtCollege.Size = New System.Drawing.Size(559, 23)
        Me.txtCollege.TabIndex = 2
        '
        'CollegeBranch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1111, 694)
        Me.Controls.Add(Me.txtCollege)
        Me.Controls.Add(Me.grdBranch)
        Me.Controls.Add(Me.lbCollege)
        Me.Name = "CollegeBranch"
        Me.Text = "CollegeBranch"
        CType(Me.grdBranch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbCollege As ListBox
    Friend WithEvents grdBranch As DataGridView
    Friend WithEvents txtCollege As TextBox
End Class
