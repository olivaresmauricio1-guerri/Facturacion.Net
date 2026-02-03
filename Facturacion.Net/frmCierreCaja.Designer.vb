<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCierreCaja
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        dgvNovedades = New DataGridView()
        chkComienzoMes = New CheckBox()
        lblEstado = New Label()
        txtSucstr = New TextBox()
        txtSucursal = New TextBox()
        cmdSalir = New Button()
        cmdEmitirCierre = New Button()
        CType(dgvNovedades, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' dgvNovedades
        ' 
        dgvNovedades.AllowUserToAddRows = False
        dgvNovedades.AllowUserToDeleteRows = False
        dgvNovedades.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvNovedades.Location = New Point(12, 37)
        dgvNovedades.Name = "dgvNovedades"
        dgvNovedades.ReadOnly = True
        dgvNovedades.Size = New Size(272, 212)
        dgvNovedades.TabIndex = 0
        ' 
        ' chkComienzoMes
        ' 
        chkComienzoMes.AutoSize = True
        chkComienzoMes.Location = New Point(70, 12)
        chkComienzoMes.Name = "chkComienzoMes"
        chkComienzoMes.Size = New Size(129, 19)
        chkComienzoMes.TabIndex = 4
        chkComienzoMes.Text = "Comienzo de Mes ?"
        chkComienzoMes.UseVisualStyleBackColor = True
        ' 
        ' lblEstado
        ' 
        lblEstado.BackColor = Color.White
        lblEstado.BorderStyle = BorderStyle.FixedSingle
        lblEstado.Font = New Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblEstado.ForeColor = Color.Red
        lblEstado.Location = New Point(12, 252)
        lblEstado.Name = "lblEstado"
        lblEstado.Size = New Size(272, 79)
        lblEstado.TabIndex = 7
        lblEstado.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' txtSucstr
        ' 
        txtSucstr.BackColor = Color.White
        txtSucstr.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtSucstr.Location = New Point(70, 334)
        txtSucstr.Name = "txtSucstr"
        txtSucstr.ReadOnly = True
        txtSucstr.Size = New Size(214, 25)
        txtSucstr.TabIndex = 6
        ' 
        ' txtSucursal
        ' 
        txtSucursal.BackColor = Color.White
        txtSucursal.Font = New Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtSucursal.Location = New Point(12, 334)
        txtSucursal.Name = "txtSucursal"
        txtSucursal.ReadOnly = True
        txtSucursal.Size = New Size(50, 25)
        txtSucursal.TabIndex = 5
        ' 
        ' cmdSalir
        ' 
        cmdSalir.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        cmdSalir.BackColor = Color.IndianRed
        cmdSalir.FlatStyle = FlatStyle.Flat
        cmdSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cmdSalir.ForeColor = Color.White
        cmdSalir.Location = New Point(196, 372)
        cmdSalir.Name = "cmdSalir"
        cmdSalir.Size = New Size(88, 30)
        cmdSalir.TabIndex = 9
        cmdSalir.Text = "&Salir"
        cmdSalir.UseVisualStyleBackColor = False
        ' 
        ' cmdEmitirCierre
        ' 
        cmdEmitirCierre.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        cmdEmitirCierre.FlatStyle = FlatStyle.Flat
        cmdEmitirCierre.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cmdEmitirCierre.Location = New Point(102, 372)
        cmdEmitirCierre.Name = "cmdEmitirCierre"
        cmdEmitirCierre.Size = New Size(88, 30)
        cmdEmitirCierre.TabIndex = 8
        cmdEmitirCierre.Text = "&Emitir Cierre"
        cmdEmitirCierre.UseVisualStyleBackColor = True
        ' 
        ' frmCierreCaja
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(293, 414)
        Controls.Add(cmdSalir)
        Controls.Add(cmdEmitirCierre)
        Controls.Add(lblEstado)
        Controls.Add(txtSucstr)
        Controls.Add(txtSucursal)
        Controls.Add(chkComienzoMes)
        Controls.Add(dgvNovedades)
        Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmCierreCaja"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Cierre de Caja"
        CType(dgvNovedades, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents dgvNovedades As System.Windows.Forms.DataGridView
    Friend WithEvents chkComienzoMes As CheckBox
    Friend WithEvents lblEstado As Label
    Friend WithEvents txtSucstr As TextBox
    Friend WithEvents txtSucursal As TextBox
    Friend WithEvents cmdSalir As Button
    Friend WithEvents cmdEmitirCierre As Button
End Class
