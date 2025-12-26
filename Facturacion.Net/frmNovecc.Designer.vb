<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNovecc
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
        PanelDatos = New Panel()
        Label10 = New Label()
        TxtCP = New TextBox()
        txtObservaciones = New TextBox()
        Label19 = New Label()
        txtBonificacion = New TextBox()
        Label20 = New Label()
        txtPV = New TextBox()
        Label13 = New Label()
        CmbBanco = New ComboBox()
        Label11 = New Label()
        CmbSucursal = New ComboBox()
        Label9 = New Label()
        CmbTipoVenta = New ComboBox()
        Label8 = New Label()
        CmbCondicion = New ComboBox()
        Label14 = New Label()
        CmbComprobante = New ComboBox()
        Label6 = New Label()
        CmbTipoValor = New ComboBox()
        Label12 = New Label()
        txtNroCheque = New TextBox()
        Label18 = New Label()
        txtRegInterno = New TextBox()
        Label17 = New Label()
        txtFechaVto = New TextBox()
        Label15 = New Label()
        txtMonto = New TextBox()
        Label5 = New Label()
        txtFecha = New TextBox()
        Label4 = New Label()
        txtNroComprobante = New TextBox()
        Label3 = New Label()
        txtNroCupon = New TextBox()
        Label16 = New Label()
        txtNroFactura = New TextBox()
        Label1 = New Label()
        txtInterno = New TextBox()
        Label7 = New Label()
        txtNroCuenta = New TextBox()
        Label2 = New Label()
        chkAnterior = New CheckBox()
        cmdAgregar = New Button()
        cmdBorrar = New Button()
        cmdModificar = New Button()
        cmdAceptar = New Button()
        cmdCancelar = New Button()
        cmdCerrar = New Button()
        lnkCopiar = New LinkLabel()
        chkEncabezados = New CheckBox()
        CType(dgvNovedades, ComponentModel.ISupportInitialize).BeginInit()
        PanelDatos.SuspendLayout()
        SuspendLayout()
        ' 
        ' dgvNovedades
        ' 
        dgvNovedades.AllowUserToAddRows = False
        dgvNovedades.AllowUserToDeleteRows = False
        dgvNovedades.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvNovedades.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvNovedades.Location = New Point(12, 12)
        dgvNovedades.Name = "dgvNovedades"
        dgvNovedades.Size = New Size(1034, 263)
        dgvNovedades.TabIndex = 0
        ' 
        ' PanelDatos
        ' 
        PanelDatos.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        PanelDatos.BorderStyle = BorderStyle.FixedSingle
        PanelDatos.Controls.Add(Label10)
        PanelDatos.Controls.Add(TxtCP)
        PanelDatos.Controls.Add(txtObservaciones)
        PanelDatos.Controls.Add(Label19)
        PanelDatos.Controls.Add(txtBonificacion)
        PanelDatos.Controls.Add(Label20)
        PanelDatos.Controls.Add(txtPV)
        PanelDatos.Controls.Add(Label13)
        PanelDatos.Controls.Add(CmbBanco)
        PanelDatos.Controls.Add(Label11)
        PanelDatos.Controls.Add(CmbSucursal)
        PanelDatos.Controls.Add(Label9)
        PanelDatos.Controls.Add(CmbTipoVenta)
        PanelDatos.Controls.Add(Label8)
        PanelDatos.Controls.Add(CmbCondicion)
        PanelDatos.Controls.Add(Label14)
        PanelDatos.Controls.Add(CmbComprobante)
        PanelDatos.Controls.Add(Label6)
        PanelDatos.Controls.Add(CmbTipoValor)
        PanelDatos.Controls.Add(Label12)
        PanelDatos.Controls.Add(txtNroCheque)
        PanelDatos.Controls.Add(Label18)
        PanelDatos.Controls.Add(txtRegInterno)
        PanelDatos.Controls.Add(Label17)
        PanelDatos.Controls.Add(txtFechaVto)
        PanelDatos.Controls.Add(Label15)
        PanelDatos.Controls.Add(txtMonto)
        PanelDatos.Controls.Add(Label5)
        PanelDatos.Controls.Add(txtFecha)
        PanelDatos.Controls.Add(Label4)
        PanelDatos.Controls.Add(txtNroComprobante)
        PanelDatos.Controls.Add(Label3)
        PanelDatos.Controls.Add(txtNroCupon)
        PanelDatos.Controls.Add(Label16)
        PanelDatos.Controls.Add(txtNroFactura)
        PanelDatos.Controls.Add(Label1)
        PanelDatos.Controls.Add(txtInterno)
        PanelDatos.Controls.Add(Label7)
        PanelDatos.Controls.Add(txtNroCuenta)
        PanelDatos.Controls.Add(Label2)
        PanelDatos.Location = New Point(12, 308)
        PanelDatos.Name = "PanelDatos"
        PanelDatos.Size = New Size(1034, 200)
        PanelDatos.TabIndex = 1
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Location = New Point(340, 16)
        Label10.Name = "Label10"
        Label10.Size = New Size(90, 15)
        Label10.TabIndex = 50
        Label10.Text = "Punto de Venta:"
        ' 
        ' TxtCP
        ' 
        TxtCP.Location = New Point(780, 133)
        TxtCP.Name = "TxtCP"
        TxtCP.Size = New Size(240, 23)
        TxtCP.TabIndex = 17
        ' 
        ' txtObservaciones
        ' 
        txtObservaciones.Location = New Point(120, 200)
        txtObservaciones.Name = "txtObservaciones"
        txtObservaciones.Size = New Size(530, 23)
        txtObservaciones.TabIndex = 47
        ' 
        ' Label19
        ' 
        Label19.AutoSize = True
        Label19.Location = New Point(10, 203)
        Label19.Name = "Label19"
        Label19.Size = New Size(84, 15)
        Label19.TabIndex = 48
        Label19.Text = "Observaciones"
        ' 
        ' txtBonificacion
        ' 
        txtBonificacion.Location = New Point(780, 200)
        txtBonificacion.Name = "txtBonificacion"
        txtBonificacion.Size = New Size(240, 23)
        txtBonificacion.TabIndex = 45
        ' 
        ' Label20
        ' 
        Label20.AutoSize = True
        Label20.Location = New Point(690, 203)
        Label20.Name = "Label20"
        Label20.Size = New Size(73, 15)
        Label20.TabIndex = 46
        Label20.Text = "Bonificacion"
        ' 
        ' txtPV
        ' 
        txtPV.BackColor = Color.White
        txtPV.Location = New Point(450, 13)
        txtPV.Name = "txtPV"
        txtPV.Size = New Size(200, 23)
        txtPV.TabIndex = 7
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.Location = New Point(690, 135)
        Label13.Name = "Label13"
        Label13.Size = New Size(67, 15)
        Label13.TabIndex = 32
        Label13.Text = "Cod.Postal:"
        ' 
        ' CmbBanco
        ' 
        CmbBanco.FormattingEnabled = True
        CmbBanco.Location = New Point(780, 73)
        CmbBanco.Name = "CmbBanco"
        CmbBanco.Size = New Size(240, 23)
        CmbBanco.TabIndex = 15
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Location = New Point(690, 76)
        Label11.Name = "Label11"
        Label11.Size = New Size(43, 15)
        Label11.TabIndex = 35
        Label11.Text = "Banco:"
        ' 
        ' CmbSucursal
        ' 
        CmbSucursal.FormattingEnabled = True
        CmbSucursal.Location = New Point(780, 43)
        CmbSucursal.Name = "CmbSucursal"
        CmbSucursal.Size = New Size(240, 23)
        CmbSucursal.TabIndex = 14
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Location = New Point(690, 46)
        Label9.Name = "Label9"
        Label9.Size = New Size(54, 15)
        Label9.TabIndex = 31
        Label9.Text = "Sucursal:"
        ' 
        ' CmbTipoVenta
        ' 
        CmbTipoVenta.FormattingEnabled = True
        CmbTipoVenta.Location = New Point(780, 13)
        CmbTipoVenta.Name = "CmbTipoVenta"
        CmbTipoVenta.Size = New Size(240, 23)
        CmbTipoVenta.TabIndex = 13
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(690, 16)
        Label8.Name = "Label8"
        Label8.Size = New Size(66, 15)
        Label8.TabIndex = 21
        Label8.Text = "Tipo Venta:"
        ' 
        ' CmbCondicion
        ' 
        CmbCondicion.FormattingEnabled = True
        CmbCondicion.Location = New Point(780, 102)
        CmbCondicion.Name = "CmbCondicion"
        CmbCondicion.Size = New Size(240, 23)
        CmbCondicion.TabIndex = 16
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.Location = New Point(690, 105)
        Label14.Name = "Label14"
        Label14.Size = New Size(65, 15)
        Label14.TabIndex = 23
        Label14.Text = "Condicion:"
        ' 
        ' CmbComprobante
        ' 
        CmbComprobante.FormattingEnabled = True
        CmbComprobante.Location = New Point(450, 133)
        CmbComprobante.Name = "CmbComprobante"
        CmbComprobante.Size = New Size(200, 23)
        CmbComprobante.TabIndex = 11
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(340, 136)
        Label6.Name = "Label6"
        Label6.Size = New Size(84, 15)
        Label6.TabIndex = 19
        Label6.Text = "Comprobante:"
        ' 
        ' CmbTipoValor
        ' 
        CmbTipoValor.FormattingEnabled = True
        CmbTipoValor.Location = New Point(450, 103)
        CmbTipoValor.Name = "CmbTipoValor"
        CmbTipoValor.Size = New Size(200, 23)
        CmbTipoValor.TabIndex = 10
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Location = New Point(340, 106)
        Label12.Name = "Label12"
        Label12.Size = New Size(63, 15)
        Label12.TabIndex = 26
        Label12.Text = "Tipo Valor:"
        ' 
        ' txtNroCheque
        ' 
        txtNroCheque.Location = New Point(780, 163)
        txtNroCheque.Name = "txtNroCheque"
        txtNroCheque.Size = New Size(240, 23)
        txtNroCheque.TabIndex = 18
        ' 
        ' Label18
        ' 
        Label18.AutoSize = True
        Label18.Location = New Point(690, 166)
        Label18.Name = "Label18"
        Label18.Size = New Size(74, 15)
        Label18.TabIndex = 33
        Label18.Text = "Nro.Cheque:"
        Label18.Visible = False
        ' 
        ' txtRegInterno
        ' 
        txtRegInterno.Location = New Point(450, 73)
        txtRegInterno.Name = "txtRegInterno"
        txtRegInterno.Size = New Size(200, 23)
        txtRegInterno.TabIndex = 9
        ' 
        ' Label17
        ' 
        Label17.AutoSize = True
        Label17.Location = New Point(340, 76)
        Label17.Name = "Label17"
        Label17.Size = New Size(71, 15)
        Label17.TabIndex = 30
        Label17.Text = "Reg.Interno:"
        ' 
        ' txtFechaVto
        ' 
        txtFechaVto.Location = New Point(450, 43)
        txtFechaVto.Name = "txtFechaVto"
        txtFechaVto.Size = New Size(200, 23)
        txtFechaVto.TabIndex = 8
        ' 
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.Location = New Point(340, 46)
        Label15.Name = "Label15"
        Label15.Size = New Size(64, 15)
        Label15.TabIndex = 28
        Label15.Text = "Fecha vto.:"
        ' 
        ' txtMonto
        ' 
        txtMonto.Location = New Point(120, 163)
        txtMonto.Name = "txtMonto"
        txtMonto.Size = New Size(190, 23)
        txtMonto.TabIndex = 6
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(10, 166)
        Label5.Name = "Label5"
        Label5.Size = New Size(46, 15)
        Label5.TabIndex = 18
        Label5.Text = "Monto:"
        ' 
        ' txtFecha
        ' 
        txtFecha.Location = New Point(120, 133)
        txtFecha.Name = "txtFecha"
        txtFecha.Size = New Size(190, 23)
        txtFecha.TabIndex = 5
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(10, 136)
        Label4.Name = "Label4"
        Label4.Size = New Size(41, 15)
        Label4.TabIndex = 17
        Label4.Text = "Fecha:"
        ' 
        ' txtNroComprobante
        ' 
        txtNroComprobante.Location = New Point(120, 103)
        txtNroComprobante.Name = "txtNroComprobante"
        txtNroComprobante.Size = New Size(190, 23)
        txtNroComprobante.TabIndex = 4
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(10, 106)
        Label3.Name = "Label3"
        Label3.Size = New Size(76, 15)
        Label3.TabIndex = 16
        Label3.Text = "Nro.Compte:"
        ' 
        ' txtNroCupon
        ' 
        txtNroCupon.Location = New Point(120, 73)
        txtNroCupon.Name = "txtNroCupon"
        txtNroCupon.Size = New Size(190, 23)
        txtNroCupon.TabIndex = 3
        ' 
        ' Label16
        ' 
        Label16.AutoSize = True
        Label16.Location = New Point(10, 76)
        Label16.Name = "Label16"
        Label16.Size = New Size(69, 15)
        Label16.TabIndex = 24
        Label16.Text = "Nro.Cupon:"
        ' 
        ' txtNroFactura
        ' 
        txtNroFactura.Location = New Point(120, 43)
        txtNroFactura.Name = "txtNroFactura"
        txtNroFactura.Size = New Size(190, 23)
        txtNroFactura.TabIndex = 2
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(10, 46)
        Label1.Name = "Label1"
        Label1.Size = New Size(72, 15)
        Label1.TabIndex = 25
        Label1.Text = "Nro.Factura:"
        ' 
        ' txtInterno
        ' 
        txtInterno.Location = New Point(450, 163)
        txtInterno.Name = "txtInterno"
        txtInterno.Size = New Size(200, 23)
        txtInterno.TabIndex = 12
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(340, 166)
        Label7.Name = "Label7"
        Label7.Size = New Size(72, 15)
        Label7.TabIndex = 20
        Label7.Text = "Imp.Interno:"
        ' 
        ' txtNroCuenta
        ' 
        txtNroCuenta.Location = New Point(120, 13)
        txtNroCuenta.Name = "txtNroCuenta"
        txtNroCuenta.Size = New Size(190, 23)
        txtNroCuenta.TabIndex = 1
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(10, 16)
        Label2.Name = "Label2"
        Label2.Size = New Size(71, 15)
        Label2.TabIndex = 15
        Label2.Text = "Nro.Cuenta:"
        ' 
        ' chkAnterior
        ' 
        chkAnterior.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        chkAnterior.AutoSize = True
        chkAnterior.Location = New Point(12, 518)
        chkAnterior.Name = "chkAnterior"
        chkAnterior.Size = New Size(69, 19)
        chkAnterior.TabIndex = 44
        chkAnterior.Text = "Anterior"
        chkAnterior.UseVisualStyleBackColor = True
        ' 
        ' cmdAgregar
        ' 
        cmdAgregar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        cmdAgregar.FlatStyle = FlatStyle.Flat
        cmdAgregar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdAgregar.Location = New Point(484, 514)
        cmdAgregar.Name = "cmdAgregar"
        cmdAgregar.Size = New Size(88, 30)
        cmdAgregar.TabIndex = 22
        cmdAgregar.Text = "&Agregar"
        cmdAgregar.UseVisualStyleBackColor = True
        ' 
        ' cmdBorrar
        ' 
        cmdBorrar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        cmdBorrar.FlatStyle = FlatStyle.Flat
        cmdBorrar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdBorrar.Location = New Point(578, 514)
        cmdBorrar.Name = "cmdBorrar"
        cmdBorrar.Size = New Size(88, 30)
        cmdBorrar.TabIndex = 23
        cmdBorrar.Text = "&Borrar"
        cmdBorrar.UseVisualStyleBackColor = True
        ' 
        ' cmdModificar
        ' 
        cmdModificar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        cmdModificar.FlatStyle = FlatStyle.Flat
        cmdModificar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdModificar.Location = New Point(672, 514)
        cmdModificar.Name = "cmdModificar"
        cmdModificar.Size = New Size(88, 30)
        cmdModificar.TabIndex = 24
        cmdModificar.Text = "&Modificar"
        cmdModificar.UseVisualStyleBackColor = True
        ' 
        ' cmdAceptar
        ' 
        cmdAceptar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        cmdAceptar.Enabled = False
        cmdAceptar.FlatStyle = FlatStyle.Flat
        cmdAceptar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdAceptar.Location = New Point(766, 514)
        cmdAceptar.Name = "cmdAceptar"
        cmdAceptar.Size = New Size(88, 30)
        cmdAceptar.TabIndex = 19
        cmdAceptar.Text = "Ac&eptar"
        cmdAceptar.UseVisualStyleBackColor = True
        ' 
        ' cmdCancelar
        ' 
        cmdCancelar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        cmdCancelar.Enabled = False
        cmdCancelar.FlatStyle = FlatStyle.Flat
        cmdCancelar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdCancelar.Location = New Point(860, 514)
        cmdCancelar.Name = "cmdCancelar"
        cmdCancelar.Size = New Size(88, 30)
        cmdCancelar.TabIndex = 20
        cmdCancelar.Text = "&Cancelar"
        cmdCancelar.UseVisualStyleBackColor = True
        ' 
        ' cmdCerrar
        ' 
        cmdCerrar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        cmdCerrar.BackColor = Color.IndianRed
        cmdCerrar.FlatStyle = FlatStyle.Flat
        cmdCerrar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdCerrar.ForeColor = Color.White
        cmdCerrar.Location = New Point(954, 514)
        cmdCerrar.Name = "cmdCerrar"
        cmdCerrar.Size = New Size(88, 30)
        cmdCerrar.TabIndex = 21
        cmdCerrar.Text = "Salir"
        cmdCerrar.UseVisualStyleBackColor = False
        ' 
        ' lnkCopiar
        ' 
        lnkCopiar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        lnkCopiar.AutoSize = True
        lnkCopiar.Location = New Point(822, 283)
        lnkCopiar.Name = "lnkCopiar"
        lnkCopiar.Size = New Size(97, 15)
        lnkCopiar.TabIndex = 45
        lnkCopiar.TabStop = True
        lnkCopiar.Text = "Copiar selección:"
        ' 
        ' chkEncabezados
        ' 
        chkEncabezados.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        chkEncabezados.AutoSize = True
        chkEncabezados.Location = New Point(925, 283)
        chkEncabezados.Name = "chkEncabezados"
        chkEncabezados.Size = New Size(119, 19)
        chkEncabezados.TabIndex = 46
        chkEncabezados.Text = "Con encabezados"
        ' 
        ' frmNovecc
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1058, 556)
        Controls.Add(lnkCopiar)
        Controls.Add(chkEncabezados)
        Controls.Add(cmdCerrar)
        Controls.Add(cmdCancelar)
        Controls.Add(cmdAceptar)
        Controls.Add(cmdModificar)
        Controls.Add(cmdBorrar)
        Controls.Add(cmdAgregar)
        Controls.Add(chkAnterior)
        Controls.Add(PanelDatos)
        Controls.Add(dgvNovedades)
        Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmNovecc"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Novedades Cuentas Corrientes"
        CType(dgvNovedades, ComponentModel.ISupportInitialize).EndInit()
        PanelDatos.ResumeLayout(False)
        PanelDatos.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents dgvNovedades As System.Windows.Forms.DataGridView
    Friend WithEvents PanelDatos As System.Windows.Forms.Panel
    Friend WithEvents txtNroFactura As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtNroCuenta As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtMonto As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtFecha As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtNroComprobante As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtNroCupon As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtInterno As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents CmbComprobante As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CmbTipoValor As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtNroCheque As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtRegInterno As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtFechaVto As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents CmbBanco As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents CmbSucursal As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents CmbTipoVenta As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents CmbCondicion As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtPV As System.Windows.Forms.TextBox
    Friend WithEvents txtBonificacion As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtObservaciones As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents chkAnterior As System.Windows.Forms.CheckBox
    Friend WithEvents cmdAgregar As System.Windows.Forms.Button
    Friend WithEvents cmdBorrar As System.Windows.Forms.Button
    Friend WithEvents cmdModificar As System.Windows.Forms.Button
    Friend WithEvents cmdAceptar As System.Windows.Forms.Button
    Friend WithEvents cmdCancelar As System.Windows.Forms.Button
    Friend WithEvents cmdCerrar As System.Windows.Forms.Button
    Friend WithEvents TxtCP As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents lnkCopiar As LinkLabel
    Friend WithEvents chkEncabezados As CheckBox
End Class
