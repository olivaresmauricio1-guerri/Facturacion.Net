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
        Me.dgvNovedades = New System.Windows.Forms.DataGridView()
        Me.PanelDatos = New System.Windows.Forms.Panel()
        Me.txtObservaciones = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtBonificacion = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtNombre = New System.Windows.Forms.TextBox()
        Me.CmbPostal = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.CmbBanco = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.CmbSucursal = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.CmbTipoVenta = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.CmbCondicion = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.CmbComprobante = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CmbTipoValor = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtNroCheque = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtRegInterno = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtFechaVto = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtMonto = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtFecha = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtNroComprobante = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtNroCupon = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtNroFactura = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtInterno = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtNroCuenta = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chkAnterior = New System.Windows.Forms.CheckBox()
        Me.cmdAgregar = New System.Windows.Forms.Button()
        Me.cmdBorrar = New System.Windows.Forms.Button()
        Me.cmdModificar = New System.Windows.Forms.Button()
        Me.cmdAceptar = New System.Windows.Forms.Button()
        Me.cmdCancelar = New System.Windows.Forms.Button()
        Me.cmdCerrar = New System.Windows.Forms.Button()
        CType(Me.dgvNovedades, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelDatos.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvNovedades
        '
        Me.dgvNovedades.AllowUserToAddRows = False
        Me.dgvNovedades.AllowUserToDeleteRows = False
        Me.dgvNovedades.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvNovedades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvNovedades.Location = New System.Drawing.Point(12, 12)
        Me.dgvNovedades.MultiSelect = False
        Me.dgvNovedades.Name = "dgvNovedades"
        Me.dgvNovedades.ReadOnly = True
        Me.dgvNovedades.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvNovedades.Size = New System.Drawing.Size(1050, 240)
        Me.dgvNovedades.TabIndex = 0
        '
        'PanelDatos
        '
        Me.PanelDatos.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelDatos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelDatos.Controls.Add(Me.txtObservaciones)
        Me.PanelDatos.Controls.Add(Me.Label19)
        Me.PanelDatos.Controls.Add(Me.txtBonificacion)
        Me.PanelDatos.Controls.Add(Me.Label20)
        Me.PanelDatos.Controls.Add(Me.txtNombre)
        Me.PanelDatos.Controls.Add(Me.CmbPostal)
        Me.PanelDatos.Controls.Add(Me.Label13)
        Me.PanelDatos.Controls.Add(Me.CmbBanco)
        Me.PanelDatos.Controls.Add(Me.Label11)
        Me.PanelDatos.Controls.Add(Me.CmbSucursal)
        Me.PanelDatos.Controls.Add(Me.Label9)
        Me.PanelDatos.Controls.Add(Me.CmbTipoVenta)
        Me.PanelDatos.Controls.Add(Me.Label8)
        Me.PanelDatos.Controls.Add(Me.CmbCondicion)
        Me.PanelDatos.Controls.Add(Me.Label14)
        Me.PanelDatos.Controls.Add(Me.CmbComprobante)
        Me.PanelDatos.Controls.Add(Me.Label6)
        Me.PanelDatos.Controls.Add(Me.CmbTipoValor)
        Me.PanelDatos.Controls.Add(Me.Label12)
        Me.PanelDatos.Controls.Add(Me.txtNroCheque)
        Me.PanelDatos.Controls.Add(Me.Label18)
        Me.PanelDatos.Controls.Add(Me.txtRegInterno)
        Me.PanelDatos.Controls.Add(Me.Label17)
        Me.PanelDatos.Controls.Add(Me.txtFechaVto)
        Me.PanelDatos.Controls.Add(Me.Label15)
        Me.PanelDatos.Controls.Add(Me.txtMonto)
        Me.PanelDatos.Controls.Add(Me.Label5)
        Me.PanelDatos.Controls.Add(Me.txtFecha)
        Me.PanelDatos.Controls.Add(Me.Label4)
        Me.PanelDatos.Controls.Add(Me.txtNroComprobante)
        Me.PanelDatos.Controls.Add(Me.Label3)
        Me.PanelDatos.Controls.Add(Me.txtNroCupon)
        Me.PanelDatos.Controls.Add(Me.Label16)
        Me.PanelDatos.Controls.Add(Me.txtNroFactura)
        Me.PanelDatos.Controls.Add(Me.Label1)
        Me.PanelDatos.Controls.Add(Me.txtInterno)
        Me.PanelDatos.Controls.Add(Me.Label7)
        Me.PanelDatos.Controls.Add(Me.txtNroCuenta)
        Me.PanelDatos.Controls.Add(Me.Label2)
        Me.PanelDatos.Location = New System.Drawing.Point(12, 260)
        Me.PanelDatos.Name = "PanelDatos"
        Me.PanelDatos.Size = New System.Drawing.Size(1050, 200)
        Me.PanelDatos.TabIndex = 1
        '
        'txtObservaciones
        '
        Me.txtObservaciones.Location = New System.Drawing.Point(120, 200)
        Me.txtObservaciones.Name = "txtObservaciones"
        Me.txtObservaciones.Size = New System.Drawing.Size(530, 23)
        Me.txtObservaciones.TabIndex = 47
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(10, 203)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(84, 15)
        Me.Label19.TabIndex = 48
        Me.Label19.Text = "Observaciones"
        '
        'txtBonificacion
        '
        Me.txtBonificacion.Location = New System.Drawing.Point(780, 200)
        Me.txtBonificacion.Name = "txtBonificacion"
        Me.txtBonificacion.Size = New System.Drawing.Size(240, 23)
        Me.txtBonificacion.TabIndex = 45
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(690, 203)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(73, 15)
        Me.Label20.TabIndex = 46
        Me.Label20.Text = "Bonificacion"
        '
        'txtNombre
        '
        Me.txtNombre.Location = New System.Drawing.Point(340, 20)
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.ReadOnly = True
        Me.txtNombre.Size = New System.Drawing.Size(310, 23)
        Me.txtNombre.TabIndex = 44
        '
        'CmbPostal
        '
        Me.CmbPostal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbPostal.FormattingEnabled = True
        Me.CmbPostal.Location = New System.Drawing.Point(780, 110)
        Me.CmbPostal.Name = "CmbPostal"
        Me.CmbPostal.Size = New System.Drawing.Size(240, 23)
        Me.CmbPostal.TabIndex = 43
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(690, 113)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(64, 15)
        Me.Label13.TabIndex = 32
        Me.Label13.Text = "Cod.Postal"
        '
        'CmbBanco
        '
        Me.CmbBanco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbBanco.FormattingEnabled = True
        Me.CmbBanco.Location = New System.Drawing.Point(780, 80)
        Me.CmbBanco.Name = "CmbBanco"
        Me.CmbBanco.Size = New System.Drawing.Size(240, 23)
        Me.CmbBanco.TabIndex = 42
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(690, 83)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(40, 15)
        Me.Label11.TabIndex = 35
        Me.Label11.Text = "Banco"
        '
        'CmbSucursal
        '
        Me.CmbSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbSucursal.FormattingEnabled = True
        Me.CmbSucursal.Location = New System.Drawing.Point(780, 50)
        Me.CmbSucursal.Name = "CmbSucursal"
        Me.CmbSucursal.Size = New System.Drawing.Size(240, 23)
        Me.CmbSucursal.TabIndex = 41
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(690, 53)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(51, 15)
        Me.Label9.TabIndex = 31
        Me.Label9.Text = "Sucursal"
        '
        'CmbTipoVenta
        '
        Me.CmbTipoVenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbTipoVenta.FormattingEnabled = True
        Me.CmbTipoVenta.Location = New System.Drawing.Point(780, 20)
        Me.CmbTipoVenta.Name = "CmbTipoVenta"
        Me.CmbTipoVenta.Size = New System.Drawing.Size(240, 23)
        Me.CmbTipoVenta.TabIndex = 40
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(690, 23)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(65, 15)
        Me.Label8.TabIndex = 21
        Me.Label8.Text = "Tipo Venta:"
        '
        'CmbCondicion
        '
        Me.CmbCondicion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbCondicion.FormattingEnabled = True
        Me.CmbCondicion.Location = New System.Drawing.Point(780, 140)
        Me.CmbCondicion.Name = "CmbCondicion"
        Me.CmbCondicion.Size = New System.Drawing.Size(240, 23)
        Me.CmbCondicion.TabIndex = 39
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(690, 143)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(65, 15)
        Me.Label14.TabIndex = 23
        Me.Label14.Text = "Condicion:"
        '
        'CmbComprobante
        '
        Me.CmbComprobante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbComprobante.FormattingEnabled = True
        Me.CmbComprobante.Location = New System.Drawing.Point(450, 140)
        Me.CmbComprobante.Name = "CmbComprobante"
        Me.CmbComprobante.Size = New System.Drawing.Size(200, 23)
        Me.CmbComprobante.TabIndex = 38
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(340, 143)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(81, 15)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = "Comprobante"
        '
        'CmbTipoValor
        '
        Me.CmbTipoValor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbTipoValor.FormattingEnabled = True
        Me.CmbTipoValor.Location = New System.Drawing.Point(450, 110)
        Me.CmbTipoValor.Name = "CmbTipoValor"
        Me.CmbTipoValor.Size = New System.Drawing.Size(200, 23)
        Me.CmbTipoValor.TabIndex = 37
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(340, 113)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(59, 15)
        Me.Label12.TabIndex = 26
        Me.Label12.Text = "Tipo Valor"
        '
        'txtNroCheque
        '
        Me.txtNroCheque.Location = New System.Drawing.Point(780, 170)
        Me.txtNroCheque.Name = "txtNroCheque"
        Me.txtNroCheque.Size = New System.Drawing.Size(240, 23)
        Me.txtNroCheque.TabIndex = 34
        Me.txtNroCheque.Visible = False
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(690, 173)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(71, 15)
        Me.Label18.TabIndex = 33
        Me.Label18.Text = "Nro.Cheque"
        Me.Label18.Visible = False
        '
        'txtRegInterno
        '
        Me.txtRegInterno.Location = New System.Drawing.Point(450, 80)
        Me.txtRegInterno.Name = "txtRegInterno"
        Me.txtRegInterno.Size = New System.Drawing.Size(200, 23)
        Me.txtRegInterno.TabIndex = 29
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(340, 83)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(67, 15)
        Me.Label17.TabIndex = 30
        Me.Label17.Text = "Reg.Interno"
        '
        'txtFechaVto
        '
        Me.txtFechaVto.Location = New System.Drawing.Point(450, 50)
        Me.txtFechaVto.Name = "txtFechaVto"
        Me.txtFechaVto.Size = New System.Drawing.Size(200, 23)
        Me.txtFechaVto.TabIndex = 27
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(340, 53)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(62, 15)
        Me.Label15.TabIndex = 28
        Me.Label15.Text = "Fecha vto."
        '
        'txtMonto
        '
        Me.txtMonto.Location = New System.Drawing.Point(120, 170)
        Me.txtMonto.Name = "txtMonto"
        Me.txtMonto.Size = New System.Drawing.Size(190, 23)
        Me.txtMonto.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(10, 173)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 15)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "Monto"
        '
        'txtFecha
        '
        Me.txtFecha.Location = New System.Drawing.Point(120, 140)
        Me.txtFecha.Name = "txtFecha"
        Me.txtFecha.Size = New System.Drawing.Size(190, 23)
        Me.txtFecha.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 143)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(38, 15)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "Fecha"
        '
        'txtNroComprobante
        '
        Me.txtNroComprobante.Location = New System.Drawing.Point(120, 110)
        Me.txtNroComprobante.Name = "txtNroComprobante"
        Me.txtNroComprobante.Size = New System.Drawing.Size(190, 23)
        Me.txtNroComprobante.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 113)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 15)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Nro.Compte"
        '
        'txtNroCupon
        '
        Me.txtNroCupon.Location = New System.Drawing.Point(120, 80)
        Me.txtNroCupon.Name = "txtNroCupon"
        Me.txtNroCupon.Size = New System.Drawing.Size(190, 23)
        Me.txtNroCupon.TabIndex = 3
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(10, 83)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(65, 15)
        Me.Label16.TabIndex = 24
        Me.Label16.Text = "Nro.Cupon"
        '
        'txtNroFactura
        '
        Me.txtNroFactura.Location = New System.Drawing.Point(120, 50)
        Me.txtNroFactura.Name = "txtNroFactura"
        Me.txtNroFactura.Size = New System.Drawing.Size(190, 23)
        Me.txtNroFactura.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 15)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "Nro.Factura"
        '
        'txtInterno
        '
        Me.txtInterno.Location = New System.Drawing.Point(450, 170)
        Me.txtInterno.Name = "txtInterno"
        Me.txtInterno.Size = New System.Drawing.Size(200, 23)
        Me.txtInterno.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(340, 173)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(69, 15)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "Imp.Interno"
        '
        'txtNroCuenta
        '
        Me.txtNroCuenta.Location = New System.Drawing.Point(120, 20)
        Me.txtNroCuenta.Name = "txtNroCuenta"
        Me.txtNroCuenta.Size = New System.Drawing.Size(190, 23)
        Me.txtNroCuenta.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 15)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Cuenta"
        '
        'chkAnterior
        '
        Me.chkAnterior.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkAnterior.AutoSize = True
        Me.chkAnterior.Location = New System.Drawing.Point(12, 470)
        Me.chkAnterior.Name = "chkAnterior"
        Me.chkAnterior.Size = New System.Drawing.Size(69, 19)
        Me.chkAnterior.TabIndex = 44
        Me.chkAnterior.Text = "Anterior"
        Me.chkAnterior.UseVisualStyleBackColor = True
        '
        'cmdAgregar
        '
        Me.cmdAgregar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAgregar.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.cmdAgregar.Location = New System.Drawing.Point(420, 466)
        Me.cmdAgregar.Name = "cmdAgregar"
        Me.cmdAgregar.Size = New System.Drawing.Size(100, 30)
        Me.cmdAgregar.TabIndex = 13
        Me.cmdAgregar.Text = "&Agregar"
        Me.cmdAgregar.UseVisualStyleBackColor = True
        '
        'cmdBorrar
        '
        Me.cmdBorrar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdBorrar.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.cmdBorrar.Location = New System.Drawing.Point(530, 466)
        Me.cmdBorrar.Name = "cmdBorrar"
        Me.cmdBorrar.Size = New System.Drawing.Size(100, 30)
        Me.cmdBorrar.TabIndex = 12
        Me.cmdBorrar.Text = "&Borrar"
        Me.cmdBorrar.UseVisualStyleBackColor = True
        '
        'cmdModificar
        '
        Me.cmdModificar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdModificar.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.cmdModificar.Location = New System.Drawing.Point(640, 466)
        Me.cmdModificar.Name = "cmdModificar"
        Me.cmdModificar.Size = New System.Drawing.Size(100, 30)
        Me.cmdModificar.TabIndex = 10
        Me.cmdModificar.Text = "&Modificar"
        Me.cmdModificar.UseVisualStyleBackColor = True
        '
        'cmdAceptar
        '
        Me.cmdAceptar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAceptar.Enabled = False
        Me.cmdAceptar.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.cmdAceptar.Location = New System.Drawing.Point(750, 466)
        Me.cmdAceptar.Name = "cmdAceptar"
        Me.cmdAceptar.Size = New System.Drawing.Size(100, 30)
        Me.cmdAceptar.TabIndex = 11
        Me.cmdAceptar.Text = "Ac&eptar"
        Me.cmdAceptar.UseVisualStyleBackColor = True
        '
        'cmdCancelar
        '
        Me.cmdCancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancelar.Enabled = False
        Me.cmdCancelar.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.cmdCancelar.Location = New System.Drawing.Point(860, 466)
        Me.cmdCancelar.Name = "cmdCancelar"
        Me.cmdCancelar.Size = New System.Drawing.Size(100, 30)
        Me.cmdCancelar.TabIndex = 9
        Me.cmdCancelar.Text = "&Cancelar"
        Me.cmdCancelar.UseVisualStyleBackColor = True
        '
        'cmdCerrar
        '
        Me.cmdCerrar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCerrar.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.cmdCerrar.Location = New System.Drawing.Point(970, 466)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(90, 30)
        Me.cmdCerrar.TabIndex = 14
        Me.cmdCerrar.Text = "C&errar"
        Me.cmdCerrar.UseVisualStyleBackColor = True
        '
        'frmNovecc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1074, 508)
        Me.Controls.Add(Me.cmdCerrar)
        Me.Controls.Add(Me.cmdCancelar)
        Me.Controls.Add(Me.cmdAceptar)
        Me.Controls.Add(Me.cmdModificar)
        Me.Controls.Add(Me.cmdBorrar)
        Me.Controls.Add(Me.cmdAgregar)
        Me.Controls.Add(Me.chkAnterior)
        Me.Controls.Add(Me.PanelDatos)
        Me.Controls.Add(Me.dgvNovedades)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmNovecc"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Novedades Cuentas Corrientes"
        CType(Me.dgvNovedades, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelDatos.ResumeLayout(False)
        Me.PanelDatos.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
    Friend WithEvents CmbPostal As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents CmbBanco As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents CmbSucursal As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents CmbTipoVenta As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents CmbCondicion As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtNombre As System.Windows.Forms.TextBox
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
End Class
