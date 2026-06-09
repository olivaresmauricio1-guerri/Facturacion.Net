<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNotaVenta
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
        PanelTop = New Panel()
        cmbExpreso = New ComboBox()
        txtFlete = New TextBox()
        Label7 = New Label()
        Label6 = New Label()
        cmbCondicion = New ComboBox()
        Label5 = New Label()
        dtpFecha = New DateTimePicker()
        Label4 = New Label()
        cmdBuscarCliente = New Button()
        txtCuit = New TextBox()
        txtRazonSocial = New TextBox()
        txtNroCuenta = New TextBox()
        Label3 = New Label()
        Label2 = New Label()
        Label1 = New Label()
        dgvItems = New DataGridView()
        PanelBottom = New Panel()
        cmdCancelar = New Button()
        cmdFinalizar = New Button()
        cmdQuitar = New Button()
        cmdAgregar = New Button()
        txtTotalFinal = New TextBox()
        Label12 = New Label()
        txtTotIB = New TextBox()
        Label11 = New Label()
        txtTotNI = New TextBox()
        Label10 = New Label()
        txtTotI = New TextBox()
        Label9 = New Label()
        txtTotG = New TextBox()
        Label8 = New Label()
        PanelTop.SuspendLayout()
        CType(dgvItems, ComponentModel.ISupportInitialize).BeginInit()
        PanelBottom.SuspendLayout()
        SuspendLayout()
        ' 
        ' PanelTop
        ' 
        PanelTop.Controls.Add(cmbExpreso)
        PanelTop.Controls.Add(txtFlete)
        PanelTop.Controls.Add(Label7)
        PanelTop.Controls.Add(Label6)
        PanelTop.Controls.Add(cmbCondicion)
        PanelTop.Controls.Add(Label5)
        PanelTop.Controls.Add(dtpFecha)
        PanelTop.Controls.Add(Label4)
        PanelTop.Controls.Add(cmdBuscarCliente)
        PanelTop.Controls.Add(txtCuit)
        PanelTop.Controls.Add(txtRazonSocial)
        PanelTop.Controls.Add(txtNroCuenta)
        PanelTop.Controls.Add(Label3)
        PanelTop.Controls.Add(Label2)
        PanelTop.Controls.Add(Label1)
        PanelTop.Dock = DockStyle.Top
        PanelTop.Location = New Point(0, 0)
        PanelTop.Name = "PanelTop"
        PanelTop.Size = New Size(1100, 123)
        PanelTop.TabIndex = 0
        ' 
        ' cmbExpreso
        ' 
        cmbExpreso.DropDownStyle = ComboBoxStyle.DropDownList
        cmbExpreso.FormattingEnabled = True
        cmbExpreso.Location = New Point(304, 94)
        cmbExpreso.Name = "cmbExpreso"
        cmbExpreso.Size = New Size(160, 23)
        cmbExpreso.TabIndex = 12
        ' 
        ' txtFlete
        ' 
        txtFlete.Location = New Point(470, 94)
        txtFlete.Name = "txtFlete"
        txtFlete.Size = New Size(120, 23)
        txtFlete.TabIndex = 1
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(470, 76)
        Label7.Name = "Label7"
        Label7.Size = New Size(32, 15)
        Label7.TabIndex = 11
        Label7.Text = "Flete"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(304, 76)
        Label6.Name = "Label6"
        Label6.Size = New Size(47, 15)
        Label6.TabIndex = 10
        Label6.Text = "Expreso"
        ' 
        ' cmbCondicion
        ' 
        cmbCondicion.DropDownStyle = ComboBoxStyle.DropDownList
        cmbCondicion.FormattingEnabled = True
        cmbCondicion.Location = New Point(138, 94)
        cmbCondicion.Name = "cmbCondicion"
        cmbCondicion.Size = New Size(160, 23)
        cmbCondicion.TabIndex = 6
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(138, 76)
        Label5.Name = "Label5"
        Label5.Size = New Size(62, 15)
        Label5.TabIndex = 9
        Label5.Text = "Condición"
        ' 
        ' dtpFecha
        ' 
        dtpFecha.Format = DateTimePickerFormat.Short
        dtpFecha.Location = New Point(12, 94)
        dtpFecha.Name = "dtpFecha"
        dtpFecha.Size = New Size(110, 23)
        dtpFecha.TabIndex = 5
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(12, 76)
        Label4.Name = "Label4"
        Label4.Size = New Size(38, 15)
        Label4.TabIndex = 8
        Label4.Text = "Fecha"
        ' 
        ' cmdBuscarCliente
        ' 
        cmdBuscarCliente.Location = New Point(12, 36)
        cmdBuscarCliente.Name = "cmdBuscarCliente"
        cmdBuscarCliente.Size = New Size(120, 27)
        cmdBuscarCliente.TabIndex = 0
        cmdBuscarCliente.Text = "Buscar Cliente"
        cmdBuscarCliente.UseVisualStyleBackColor = True
        ' 
        ' txtCuit
        ' 
        txtCuit.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtCuit.Location = New Point(443, 40)
        txtCuit.Name = "txtCuit"
        txtCuit.ReadOnly = True
        txtCuit.Size = New Size(160, 23)
        txtCuit.TabIndex = 4
        ' 
        ' txtRazonSocial
        ' 
        txtRazonSocial.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtRazonSocial.Location = New Point(217, 40)
        txtRazonSocial.Name = "txtRazonSocial"
        txtRazonSocial.ReadOnly = True
        txtRazonSocial.Size = New Size(220, 23)
        txtRazonSocial.TabIndex = 3
        ' 
        ' txtNroCuenta
        ' 
        txtNroCuenta.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtNroCuenta.Location = New Point(138, 40)
        txtNroCuenta.Name = "txtNroCuenta"
        txtNroCuenta.ReadOnly = True
        txtNroCuenta.Size = New Size(73, 23)
        txtNroCuenta.TabIndex = 2
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(443, 22)
        Label3.Name = "Label3"
        Label3.Size = New Size(33, 15)
        Label3.TabIndex = 7
        Label3.Text = "CUIT"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(217, 22)
        Label2.Name = "Label2"
        Label2.Size = New Size(73, 15)
        Label2.TabIndex = 6
        Label2.Text = "Razón Social"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(138, 22)
        Label1.Name = "Label1"
        Label1.Size = New Size(65, 15)
        Label1.TabIndex = 5
        Label1.Text = "NroCuenta"
        ' 
        ' dgvItems
        ' 
        dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvItems.Dock = DockStyle.Fill
        dgvItems.Location = New Point(0, 123)
        dgvItems.Name = "dgvItems"
        dgvItems.Size = New Size(1100, 437)
        dgvItems.TabIndex = 1
        ' 
        ' PanelBottom
        ' 
        PanelBottom.Controls.Add(cmdCancelar)
        PanelBottom.Controls.Add(cmdFinalizar)
        PanelBottom.Controls.Add(cmdQuitar)
        PanelBottom.Controls.Add(cmdAgregar)
        PanelBottom.Controls.Add(txtTotalFinal)
        PanelBottom.Controls.Add(Label12)
        PanelBottom.Controls.Add(txtTotIB)
        PanelBottom.Controls.Add(Label11)
        PanelBottom.Controls.Add(txtTotNI)
        PanelBottom.Controls.Add(Label10)
        PanelBottom.Controls.Add(txtTotI)
        PanelBottom.Controls.Add(Label9)
        PanelBottom.Controls.Add(txtTotG)
        PanelBottom.Controls.Add(Label8)
        PanelBottom.Dock = DockStyle.Bottom
        PanelBottom.Location = New Point(0, 560)
        PanelBottom.Name = "PanelBottom"
        PanelBottom.Size = New Size(1100, 90)
        PanelBottom.TabIndex = 2
        ' 
        ' cmdCancelar
        ' 
        cmdCancelar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        cmdCancelar.BackColor = Color.IndianRed
        cmdCancelar.FlatStyle = FlatStyle.Flat
        cmdCancelar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdCancelar.ForeColor = Color.White
        cmdCancelar.Location = New Point(988, 18)
        cmdCancelar.Name = "cmdCancelar"
        cmdCancelar.Size = New Size(100, 58)
        cmdCancelar.TabIndex = 8
        cmdCancelar.Text = "Cancelar"
        cmdCancelar.UseVisualStyleBackColor = False
        ' 
        ' cmdFinalizar
        ' 
        cmdFinalizar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        cmdFinalizar.BackColor = Color.SteelBlue
        cmdFinalizar.FlatStyle = FlatStyle.Flat
        cmdFinalizar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdFinalizar.ForeColor = Color.White
        cmdFinalizar.Location = New Point(882, 18)
        cmdFinalizar.Name = "cmdFinalizar"
        cmdFinalizar.Size = New Size(100, 58)
        cmdFinalizar.TabIndex = 7
        cmdFinalizar.Text = "Finalizar"
        cmdFinalizar.UseVisualStyleBackColor = False
        ' 
        ' cmdQuitar
        ' 
        cmdQuitar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        cmdQuitar.FlatStyle = FlatStyle.Flat
        cmdQuitar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdQuitar.Location = New Point(766, 18)
        cmdQuitar.Name = "cmdQuitar"
        cmdQuitar.Size = New Size(110, 58)
        cmdQuitar.TabIndex = 6
        cmdQuitar.Text = "Quitar"
        cmdQuitar.UseVisualStyleBackColor = True
        ' 
        ' cmdAgregar
        ' 
        cmdAgregar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        cmdAgregar.FlatStyle = FlatStyle.Flat
        cmdAgregar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdAgregar.Location = New Point(650, 18)
        cmdAgregar.Name = "cmdAgregar"
        cmdAgregar.Size = New Size(110, 58)
        cmdAgregar.TabIndex = 5
        cmdAgregar.Text = "Agregar"
        cmdAgregar.UseVisualStyleBackColor = True
        ' 
        ' txtTotalFinal
        ' 
        txtTotalFinal.Location = New Point(476, 53)
        txtTotalFinal.Name = "txtTotalFinal"
        txtTotalFinal.ReadOnly = True
        txtTotalFinal.Size = New Size(130, 23)
        txtTotalFinal.TabIndex = 4
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Location = New Point(476, 35)
        Label12.Name = "Label12"
        Label12.Size = New Size(33, 15)
        Label12.TabIndex = 16
        Label12.Text = "Total"
        ' 
        ' txtTotIB
        ' 
        txtTotIB.Location = New Point(360, 53)
        txtTotIB.Name = "txtTotIB"
        txtTotIB.ReadOnly = True
        txtTotIB.Size = New Size(110, 23)
        txtTotIB.TabIndex = 3
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Location = New Point(360, 35)
        Label11.Name = "Label11"
        Label11.Size = New Size(27, 15)
        Label11.TabIndex = 15
        Label11.Text = "IIBB"
        ' 
        ' txtTotNI
        ' 
        txtTotNI.Location = New Point(244, 53)
        txtTotNI.Name = "txtTotNI"
        txtTotNI.ReadOnly = True
        txtTotNI.Size = New Size(110, 23)
        txtTotNI.TabIndex = 2
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Location = New Point(244, 35)
        Label10.Name = "Label10"
        Label10.Size = New Size(19, 15)
        Label10.TabIndex = 14
        Label10.Text = "NI"
        ' 
        ' txtTotI
        ' 
        txtTotI.Location = New Point(128, 53)
        txtTotI.Name = "txtTotI"
        txtTotI.ReadOnly = True
        txtTotI.Size = New Size(110, 23)
        txtTotI.TabIndex = 1
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Location = New Point(128, 35)
        Label9.Name = "Label9"
        Label9.Size = New Size(24, 15)
        Label9.TabIndex = 13
        Label9.Text = "IVA"
        ' 
        ' txtTotG
        ' 
        txtTotG.Location = New Point(12, 53)
        txtTotG.Name = "txtTotG"
        txtTotG.ReadOnly = True
        txtTotG.Size = New Size(110, 23)
        txtTotG.TabIndex = 0
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(12, 35)
        Label8.Name = "Label8"
        Label8.Size = New Size(33, 15)
        Label8.TabIndex = 12
        Label8.Text = "Neto"
        ' 
        ' frmNotaVenta
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1100, 650)
        Controls.Add(dgvItems)
        Controls.Add(PanelBottom)
        Controls.Add(PanelTop)
        MinimumSize = New Size(1116, 689)
        Name = "frmNotaVenta"
        Text = "Nota de Venta"
        PanelTop.ResumeLayout(False)
        PanelTop.PerformLayout()
        CType(dgvItems, ComponentModel.ISupportInitialize).EndInit()
        PanelBottom.ResumeLayout(False)
        PanelBottom.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents PanelTop As Panel
    Friend WithEvents cmdBuscarCliente As Button
    Friend WithEvents txtCuit As TextBox
    Friend WithEvents txtRazonSocial As TextBox
    Friend WithEvents txtNroCuenta As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents dtpFecha As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents cmbCondicion As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents txtFlete As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents dgvItems As DataGridView
    Friend WithEvents PanelBottom As Panel
    Friend WithEvents txtTotG As TextBox
    Friend WithEvents txtTotI As TextBox
    Friend WithEvents txtTotNI As TextBox
    Friend WithEvents txtTotIB As TextBox
    Friend WithEvents txtTotalFinal As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents cmdAgregar As Button
    Friend WithEvents cmdQuitar As Button
    Friend WithEvents cmdFinalizar As Button
    Friend WithEvents cmdCancelar As Button
    Friend WithEvents cmbExpreso As ComboBox
End Class
