<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmNovestk
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        dgvNovedades = New DataGridView()
        PanelDatos = New Panel()
        cmdBuscarArticulo = New Button()
        txtBonificacion = New TextBox()
        Label5 = New Label()
        txtDespacho = New TextBox()
        Label18 = New Label()
        chkCancelado = New CheckBox()
        chkMesAnterior = New CheckBox()
        chkCtaAgip = New CheckBox()
        CmbViajante = New ComboBox()
        Label16 = New Label()
        CmbCanal = New ComboBox()
        Label15 = New Label()
        txtImporte = New TextBox()
        Label14 = New Label()
        txtValorPU = New TextBox()
        Label13 = New Label()
        txtCantidad = New TextBox()
        Label12 = New Label()
        txtArticulo = New TextBox()
        Label11 = New Label()
        CmbProveedor = New ComboBox()
        Label10 = New Label()
        txtObservaciones = New TextBox()
        Label19 = New Label()
        txtPV = New TextBox()
        Label9 = New Label()
        CmbSucursal = New ComboBox()
        Label20 = New Label()
        CmbTipoVenta = New ComboBox()
        Label8 = New Label()
        CmbCondicion = New ComboBox()
        Label7 = New Label()
        CmbComprobante = New ComboBox()
        Label6 = New Label()
        dtpFecha = New DateTimePicker()
        Label4 = New Label()
        txtNroComprobante = New TextBox()
        Label3 = New Label()
        txtFactura = New TextBox()
        Label1 = New Label()
        txtNroCuenta = New TextBox()
        Label2 = New Label()
        lblArticuloDescripcion = New Label()
        cmdAgregar = New Button()
        cmdBorrar = New Button()
        cmdModificar = New Button()
        cmdAceptar = New Button()
        cmdCancelar = New Button()
        cmdCerrar = New Button()
        optTotal = New RadioButton()
        optParcial = New RadioButton()
        lblBL = New Label()
        CmbBL = New ComboBox()
        cmdBL = New Button()
        lblDesp = New Label()
        txtDesp = New TextBox()
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
        dgvNovedades.Size = New Size(1034, 259)
        dgvNovedades.TabIndex = 0
        ' 
        ' PanelDatos
        ' 
        PanelDatos.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        PanelDatos.BorderStyle = BorderStyle.FixedSingle
        PanelDatos.Controls.Add(cmdBuscarArticulo)
        PanelDatos.Controls.Add(txtBonificacion)
        PanelDatos.Controls.Add(Label5)
        PanelDatos.Controls.Add(txtDespacho)
        PanelDatos.Controls.Add(Label18)
        PanelDatos.Controls.Add(chkCancelado)
        PanelDatos.Controls.Add(chkMesAnterior)
        PanelDatos.Controls.Add(chkCtaAgip)
        PanelDatos.Controls.Add(CmbViajante)
        PanelDatos.Controls.Add(Label16)
        PanelDatos.Controls.Add(CmbCanal)
        PanelDatos.Controls.Add(Label15)
        PanelDatos.Controls.Add(txtImporte)
        PanelDatos.Controls.Add(Label14)
        PanelDatos.Controls.Add(txtValorPU)
        PanelDatos.Controls.Add(Label13)
        PanelDatos.Controls.Add(txtCantidad)
        PanelDatos.Controls.Add(Label12)
        PanelDatos.Controls.Add(txtArticulo)
        PanelDatos.Controls.Add(Label11)
        PanelDatos.Controls.Add(CmbProveedor)
        PanelDatos.Controls.Add(Label10)
        PanelDatos.Controls.Add(txtObservaciones)
        PanelDatos.Controls.Add(Label19)
        PanelDatos.Controls.Add(txtPV)
        PanelDatos.Controls.Add(Label9)
        PanelDatos.Controls.Add(CmbSucursal)
        PanelDatos.Controls.Add(Label20)
        PanelDatos.Controls.Add(CmbTipoVenta)
        PanelDatos.Controls.Add(Label8)
        PanelDatos.Controls.Add(CmbCondicion)
        PanelDatos.Controls.Add(Label7)
        PanelDatos.Controls.Add(CmbComprobante)
        PanelDatos.Controls.Add(Label6)
        PanelDatos.Controls.Add(dtpFecha)
        PanelDatos.Controls.Add(Label4)
        PanelDatos.Controls.Add(txtNroComprobante)
        PanelDatos.Controls.Add(Label3)
        PanelDatos.Controls.Add(txtFactura)
        PanelDatos.Controls.Add(Label1)
        PanelDatos.Controls.Add(txtNroCuenta)
        PanelDatos.Controls.Add(Label2)
        PanelDatos.Location = New Point(12, 304)
        PanelDatos.Name = "PanelDatos"
        PanelDatos.Size = New Size(1034, 200)
        PanelDatos.TabIndex = 1
        ' 
        ' cmdBuscarArticulo
        ' 
        cmdBuscarArticulo.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        cmdBuscarArticulo.FlatStyle = FlatStyle.Flat
        cmdBuscarArticulo.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdBuscarArticulo.Location = New Point(222, 13)
        cmdBuscarArticulo.Name = "cmdBuscarArticulo"
        cmdBuscarArticulo.Size = New Size(55, 23)
        cmdBuscarArticulo.TabIndex = 55
        cmdBuscarArticulo.Text = "Buscar"
        cmdBuscarArticulo.UseVisualStyleBackColor = True
        ' 
        ' txtBonificacion
        ' 
        txtBonificacion.Location = New Point(780, 42)
        txtBonificacion.Name = "txtBonificacion"
        txtBonificacion.Size = New Size(240, 23)
        txtBonificacion.TabIndex = 53
        txtBonificacion.Text = "0"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(683, 45)
        Label5.Name = "Label5"
        Label5.Size = New Size(76, 15)
        Label5.TabIndex = 54
        Label5.Text = "Bonificación:"
        ' 
        ' txtDespacho
        ' 
        txtDespacho.Location = New Point(780, 133)
        txtDespacho.Name = "txtDespacho"
        txtDespacho.Size = New Size(240, 23)
        txtDespacho.TabIndex = 18
        ' 
        ' Label18
        ' 
        Label18.AutoSize = True
        Label18.Location = New Point(683, 136)
        Label18.Name = "Label18"
        Label18.Size = New Size(62, 15)
        Label18.TabIndex = 52
        Label18.Text = "Despacho:"
        ' 
        ' chkCancelado
        ' 
        chkCancelado.AutoSize = True
        chkCancelado.Location = New Point(640, 202)
        chkCancelado.Name = "chkCancelado"
        chkCancelado.Size = New Size(82, 19)
        chkCancelado.TabIndex = 49
        chkCancelado.Text = "Cancelado"
        chkCancelado.UseVisualStyleBackColor = True
        ' 
        ' chkMesAnterior
        ' 
        chkMesAnterior.AutoSize = True
        chkMesAnterior.Location = New Point(740, 202)
        chkMesAnterior.Name = "chkMesAnterior"
        chkMesAnterior.Size = New Size(94, 19)
        chkMesAnterior.TabIndex = 50
        chkMesAnterior.Text = "Mes Anterior"
        chkMesAnterior.UseVisualStyleBackColor = True
        ' 
        ' chkCtaAgip
        ' 
        chkCtaAgip.AutoSize = True
        chkCtaAgip.Location = New Point(500, 202)
        chkCtaAgip.Name = "chkCtaAgip"
        chkCtaAgip.Size = New Size(111, 19)
        chkCtaAgip.TabIndex = 48
        chkCtaAgip.Text = "Cta. y Ord. AGIP"
        chkCtaAgip.UseVisualStyleBackColor = True
        ' 
        ' CmbViajante
        ' 
        CmbViajante.FormattingEnabled = True
        CmbViajante.Location = New Point(780, 103)
        CmbViajante.Name = "CmbViajante"
        CmbViajante.Size = New Size(240, 23)
        CmbViajante.TabIndex = 16
        ' 
        ' Label16
        ' 
        Label16.AutoSize = True
        Label16.Location = New Point(683, 106)
        Label16.Name = "Label16"
        Label16.Size = New Size(52, 15)
        Label16.TabIndex = 46
        Label16.Text = "Viajante:"
        ' 
        ' CmbCanal
        ' 
        CmbCanal.FormattingEnabled = True
        CmbCanal.Location = New Point(780, 73)
        CmbCanal.Name = "CmbCanal"
        CmbCanal.Size = New Size(240, 23)
        CmbCanal.TabIndex = 15
        ' 
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.Location = New Point(683, 76)
        Label15.Name = "Label15"
        Label15.Size = New Size(72, 15)
        Label15.TabIndex = 44
        Label15.Text = "Canal Venta:"
        ' 
        ' txtImporte
        ' 
        txtImporte.Location = New Point(780, 163)
        txtImporte.Name = "txtImporte"
        txtImporte.Size = New Size(240, 23)
        txtImporte.TabIndex = 14
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.Location = New Point(683, 166)
        Label14.Name = "Label14"
        Label14.Size = New Size(36, 15)
        Label14.TabIndex = 42
        Label14.Text = "Total:"
        ' 
        ' txtValorPU
        ' 
        txtValorPU.Location = New Point(780, 13)
        txtValorPU.Name = "txtValorPU"
        txtValorPU.Size = New Size(240, 23)
        txtValorPU.TabIndex = 13
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.Location = New Point(683, 16)
        Label13.Name = "Label13"
        Label13.Size = New Size(95, 15)
        Label13.TabIndex = 40
        Label13.Text = "Precion Unitario:"
        ' 
        ' txtCantidad
        ' 
        txtCantidad.Location = New Point(120, 42)
        txtCantidad.Name = "txtCantidad"
        txtCantidad.Size = New Size(100, 23)
        txtCantidad.TabIndex = 12
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Location = New Point(10, 45)
        Label12.Name = "Label12"
        Label12.Size = New Size(58, 15)
        Label12.TabIndex = 38
        Label12.Text = "Cantidad:"
        ' 
        ' txtArticulo
        ' 
        txtArticulo.Location = New Point(120, 13)
        txtArticulo.Name = "txtArticulo"
        txtArticulo.Size = New Size(100, 23)
        txtArticulo.TabIndex = 11
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Location = New Point(10, 16)
        Label11.Name = "Label11"
        Label11.Size = New Size(52, 15)
        Label11.TabIndex = 36
        Label11.Text = "Articulo:"
        ' 
        ' CmbProveedor
        ' 
        CmbProveedor.FormattingEnabled = True
        CmbProveedor.Location = New Point(450, 103)
        CmbProveedor.Name = "CmbProveedor"
        CmbProveedor.Size = New Size(200, 23)
        CmbProveedor.TabIndex = 10
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Location = New Point(340, 106)
        Label10.Name = "Label10"
        Label10.Size = New Size(64, 15)
        Label10.TabIndex = 34
        Label10.Text = "Proveedor:"
        ' 
        ' txtObservaciones
        ' 
        txtObservaciones.Location = New Point(120, 200)
        txtObservaciones.Name = "txtObservaciones"
        txtObservaciones.Size = New Size(500, 23)
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
        ' txtPV
        ' 
        txtPV.BackColor = Color.White
        txtPV.Location = New Point(450, 13)
        txtPV.Name = "txtPV"
        txtPV.Size = New Size(91, 23)
        txtPV.TabIndex = 7
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Location = New Point(340, 16)
        Label9.Name = "Label9"
        Label9.Size = New Size(90, 15)
        Label9.TabIndex = 50
        Label9.Text = "Punto de Venta:"
        ' 
        ' CmbSucursal
        ' 
        CmbSucursal.FormattingEnabled = True
        CmbSucursal.Location = New Point(450, 43)
        CmbSucursal.Name = "CmbSucursal"
        CmbSucursal.Size = New Size(200, 23)
        CmbSucursal.TabIndex = 8
        ' 
        ' Label20
        ' 
        Label20.AutoSize = True
        Label20.Location = New Point(340, 46)
        Label20.Name = "Label20"
        Label20.Size = New Size(54, 15)
        Label20.TabIndex = 31
        Label20.Text = "Sucursal:"
        ' 
        ' CmbTipoVenta
        ' 
        CmbTipoVenta.FormattingEnabled = True
        CmbTipoVenta.Location = New Point(450, 73)
        CmbTipoVenta.Name = "CmbTipoVenta"
        CmbTipoVenta.Size = New Size(200, 23)
        CmbTipoVenta.TabIndex = 9
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(340, 76)
        Label8.Name = "Label8"
        Label8.Size = New Size(66, 15)
        Label8.TabIndex = 21
        Label8.Text = "Tipo Venta:"
        ' 
        ' CmbCondicion
        ' 
        CmbCondicion.FormattingEnabled = True
        CmbCondicion.Location = New Point(450, 163)
        CmbCondicion.Name = "CmbCondicion"
        CmbCondicion.Size = New Size(190, 23)
        CmbCondicion.TabIndex = 6
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(340, 166)
        Label7.Name = "Label7"
        Label7.Size = New Size(65, 15)
        Label7.TabIndex = 23
        Label7.Text = "Condicion:"
        ' 
        ' CmbComprobante
        ' 
        CmbComprobante.FormattingEnabled = True
        CmbComprobante.Location = New Point(120, 133)
        CmbComprobante.Name = "CmbComprobante"
        CmbComprobante.Size = New Size(190, 23)
        CmbComprobante.TabIndex = 4
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(10, 136)
        Label6.Name = "Label6"
        Label6.Size = New Size(84, 15)
        Label6.TabIndex = 19
        Label6.Text = "Comprobante:"
        ' 
        ' dtpFecha
        ' 
        dtpFecha.Format = DateTimePickerFormat.Short
        dtpFecha.Location = New Point(120, 103)
        dtpFecha.Name = "dtpFecha"
        dtpFecha.Size = New Size(100, 23)
        dtpFecha.TabIndex = 3
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(10, 106)
        Label4.Name = "Label4"
        Label4.Size = New Size(41, 15)
        Label4.TabIndex = 17
        Label4.Text = "Fecha:"
        ' 
        ' txtNroComprobante
        ' 
        txtNroComprobante.Location = New Point(120, 163)
        txtNroComprobante.Name = "txtNroComprobante"
        txtNroComprobante.Size = New Size(190, 23)
        txtNroComprobante.TabIndex = 5
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(10, 166)
        Label3.Name = "Label3"
        Label3.Size = New Size(76, 15)
        Label3.TabIndex = 16
        Label3.Text = "Nro.Compte:"
        ' 
        ' txtFactura
        ' 
        txtFactura.Location = New Point(120, 73)
        txtFactura.Name = "txtFactura"
        txtFactura.Size = New Size(190, 23)
        txtFactura.TabIndex = 2
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(10, 76)
        Label1.Name = "Label1"
        Label1.Size = New Size(72, 15)
        Label1.TabIndex = 25
        Label1.Text = "Nro.Factura:"
        ' 
        ' txtNroCuenta
        ' 
        txtNroCuenta.Location = New Point(450, 132)
        txtNroCuenta.Name = "txtNroCuenta"
        txtNroCuenta.Size = New Size(190, 23)
        txtNroCuenta.TabIndex = 1
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(340, 135)
        Label2.Name = "Label2"
        Label2.Size = New Size(71, 15)
        Label2.TabIndex = 15
        Label2.Text = "Nro.Cuenta:"
        ' 
        ' lblArticuloDescripcion
        ' 
        lblArticuloDescripcion.AutoSize = True
        lblArticuloDescripcion.Font = New Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblArticuloDescripcion.ForeColor = Color.FromArgb(CByte(0), CByte(0), CByte(192))
        lblArticuloDescripcion.Location = New Point(23, 518)
        lblArticuloDescripcion.Name = "lblArticuloDescripcion"
        lblArticuloDescripcion.Size = New Size(0, 20)
        lblArticuloDescripcion.TabIndex = 37
        ' 
        ' cmdAgregar
        ' 
        cmdAgregar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        cmdAgregar.FlatStyle = FlatStyle.Flat
        cmdAgregar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdAgregar.Location = New Point(484, 510)
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
        cmdBorrar.Location = New Point(578, 510)
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
        cmdModificar.Location = New Point(672, 510)
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
        cmdAceptar.Location = New Point(766, 510)
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
        cmdCancelar.Location = New Point(860, 510)
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
        cmdCerrar.Location = New Point(954, 510)
        cmdCerrar.Name = "cmdCerrar"
        cmdCerrar.Size = New Size(88, 30)
        cmdCerrar.TabIndex = 21
        cmdCerrar.Text = "Salir"
        cmdCerrar.UseVisualStyleBackColor = False
        ' 
        ' optTotal
        ' 
        optTotal.AutoSize = True
        optTotal.Location = New Point(100, 278)
        optTotal.Name = "optTotal"
        optTotal.Size = New Size(67, 19)
        optTotal.TabIndex = 61
        optTotal.TabStop = True
        optTotal.Text = "Tránsito"
        optTotal.UseVisualStyleBackColor = True
        ' 
        ' optParcial
        ' 
        optParcial.AutoSize = True
        optParcial.Location = New Point(12, 278)
        optParcial.Name = "optParcial"
        optParcial.Size = New Size(67, 19)
        optParcial.TabIndex = 60
        optParcial.TabStop = True
        optParcial.Text = "ZFranca"
        optParcial.UseVisualStyleBackColor = True
        ' 
        ' lblBL
        ' 
        lblBL.AutoSize = True
        lblBL.Location = New Point(180, 280)
        lblBL.Name = "lblBL"
        lblBL.Size = New Size(23, 15)
        lblBL.TabIndex = 62
        lblBL.Text = "BL:"
        ' 
        ' CmbBL
        ' 
        CmbBL.FormattingEnabled = True
        CmbBL.Location = New Point(210, 277)
        CmbBL.Name = "CmbBL"
        CmbBL.Size = New Size(150, 23)
        CmbBL.TabIndex = 63
        ' 
        ' cmdBL
        ' 
        cmdBL.Location = New Point(370, 277)
        cmdBL.Name = "cmdBL"
        cmdBL.Size = New Size(75, 23)
        cmdBL.TabIndex = 64
        cmdBL.Text = "Cargar BL"
        cmdBL.UseVisualStyleBackColor = True
        ' 
        ' lblDesp
        ' 
        lblDesp.AutoSize = True
        lblDesp.Location = New Point(460, 280)
        lblDesp.Name = "lblDesp"
        lblDesp.Size = New Size(62, 15)
        lblDesp.TabIndex = 65
        lblDesp.Text = "Despacho:"
        ' 
        ' txtDesp
        ' 
        txtDesp.Location = New Point(530, 277)
        txtDesp.Name = "txtDesp"
        txtDesp.Size = New Size(120, 23)
        txtDesp.TabIndex = 66
        ' 
        ' lnkCopiar
        ' 
        lnkCopiar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        lnkCopiar.AutoSize = True
        lnkCopiar.Location = New Point(824, 281)
        lnkCopiar.Name = "lnkCopiar"
        lnkCopiar.Size = New Size(97, 15)
        lnkCopiar.TabIndex = 67
        lnkCopiar.TabStop = True
        lnkCopiar.Text = "Copiar selección:"
        ' 
        ' chkEncabezados
        ' 
        chkEncabezados.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        chkEncabezados.AutoSize = True
        chkEncabezados.Location = New Point(927, 281)
        chkEncabezados.Name = "chkEncabezados"
        chkEncabezados.Size = New Size(119, 19)
        chkEncabezados.TabIndex = 68
        chkEncabezados.Text = "Con encabezados"
        ' 
        ' frmNovestk
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1058, 552)
        Controls.Add(lnkCopiar)
        Controls.Add(chkEncabezados)
        Controls.Add(txtDesp)
        Controls.Add(lblDesp)
        Controls.Add(cmdBL)
        Controls.Add(CmbBL)
        Controls.Add(lblBL)
        Controls.Add(optTotal)
        Controls.Add(optParcial)
        Controls.Add(cmdCerrar)
        Controls.Add(cmdCancelar)
        Controls.Add(cmdAceptar)
        Controls.Add(cmdModificar)
        Controls.Add(cmdBorrar)
        Controls.Add(cmdAgregar)
        Controls.Add(PanelDatos)
        Controls.Add(dgvNovedades)
        Controls.Add(lblArticuloDescripcion)
        Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmNovestk"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Mantenimiento de Novedades de Stock"
        CType(dgvNovedades, ComponentModel.ISupportInitialize).EndInit()
        PanelDatos.ResumeLayout(False)
        PanelDatos.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents dgvNovedades As System.Windows.Forms.DataGridView
    Friend WithEvents PanelDatos As System.Windows.Forms.Panel
    Friend WithEvents txtDespacho As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents chkCancelado As System.Windows.Forms.CheckBox
    Friend WithEvents chkMesAnterior As System.Windows.Forms.CheckBox
    Friend WithEvents CmbViajante As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents CmbCanal As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtImporte As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtValorPU As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtCantidad As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtArticulo As System.Windows.Forms.TextBox
    Friend WithEvents lblArticuloDescripcion As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents CmbProveedor As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtObservaciones As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtPV As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents CmbSucursal As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents CmbTipoVenta As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents CmbCondicion As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents CmbComprobante As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtNroComprobante As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtFactura As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtNroCuenta As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdAgregar As System.Windows.Forms.Button
    Friend WithEvents cmdModificar As System.Windows.Forms.Button
    Friend WithEvents cmdBorrar As System.Windows.Forms.Button
    Friend WithEvents cmdAceptar As System.Windows.Forms.Button
    Friend WithEvents cmdCancelar As System.Windows.Forms.Button
    Friend WithEvents cmdCerrar As System.Windows.Forms.Button

    ' Controls for BL Process
    Friend WithEvents optTotal As System.Windows.Forms.RadioButton
    Friend WithEvents optParcial As System.Windows.Forms.RadioButton
    Friend WithEvents lblBL As System.Windows.Forms.Label
    Friend WithEvents CmbBL As System.Windows.Forms.ComboBox
    Friend WithEvents cmdBL As System.Windows.Forms.Button
    Friend WithEvents lblDesp As System.Windows.Forms.Label
    Friend WithEvents txtDesp As System.Windows.Forms.TextBox
    Friend WithEvents chkCtaAgip As System.Windows.Forms.CheckBox
    Friend WithEvents txtBonificacion As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents cmdBuscarArticulo As Button
    Friend WithEvents lnkCopiar As LinkLabel
    Friend WithEvents chkEncabezados As CheckBox
End Class
