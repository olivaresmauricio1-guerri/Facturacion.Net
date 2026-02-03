<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReimprimirCbte
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
        Me.dgvDocumentos = New System.Windows.Forms.DataGridView()
        Me.PanelFiltros = New System.Windows.Forms.Panel()
        Me.chkLimite = New System.Windows.Forms.CheckBox()
        Me.dtpHasta = New System.Windows.Forms.DateTimePicker()
        Me.lblHasta = New System.Windows.Forms.Label()
        Me.dtpDesde = New System.Windows.Forms.DateTimePicker()
        Me.lblDesde = New System.Windows.Forms.Label()
        Me.lblPuntoVenta = New System.Windows.Forms.Label()
        Me.cmbPuntoVenta = New System.Windows.Forms.ComboBox()
        Me.lblTipo = New System.Windows.Forms.Label()
        Me.cmbTipo = New System.Windows.Forms.ComboBox()
        Me.lblBuscar = New System.Windows.Forms.Label()
        Me.txtBuscar = New System.Windows.Forms.TextBox()
        Me.cmdGenerarPDF = New System.Windows.Forms.Button()
        Me.cmdCerrar = New System.Windows.Forms.Button()
        CType(Me.dgvDocumentos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelFiltros.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvDocumentos
        '
        Me.dgvDocumentos.AllowUserToAddRows = False
        Me.dgvDocumentos.AllowUserToDeleteRows = False
        Me.dgvDocumentos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvDocumentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDocumentos.Location = New System.Drawing.Point(12, 108)
        Me.dgvDocumentos.MultiSelect = False
        Me.dgvDocumentos.Name = "dgvDocumentos"
        Me.dgvDocumentos.ReadOnly = True
        Me.dgvDocumentos.RowHeadersVisible = False
        Me.dgvDocumentos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDocumentos.Size = New System.Drawing.Size(860, 386)
        Me.dgvDocumentos.TabIndex = 0
        '
        'PanelFiltros
        '
        Me.PanelFiltros.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelFiltros.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelFiltros.Controls.Add(Me.chkLimite)
        Me.PanelFiltros.Controls.Add(Me.dtpHasta)
        Me.PanelFiltros.Controls.Add(Me.lblHasta)
        Me.PanelFiltros.Controls.Add(Me.dtpDesde)
        Me.PanelFiltros.Controls.Add(Me.lblDesde)
        Me.PanelFiltros.Controls.Add(Me.lblPuntoVenta)
        Me.PanelFiltros.Controls.Add(Me.cmbPuntoVenta)
        Me.PanelFiltros.Controls.Add(Me.lblTipo)
        Me.PanelFiltros.Controls.Add(Me.cmbTipo)
        Me.PanelFiltros.Controls.Add(Me.lblBuscar)
        Me.PanelFiltros.Controls.Add(Me.txtBuscar)
        Me.PanelFiltros.Location = New System.Drawing.Point(12, 12)
        Me.PanelFiltros.Name = "PanelFiltros"
        Me.PanelFiltros.Size = New System.Drawing.Size(860, 90)
        Me.PanelFiltros.TabIndex = 1
        '
        'chkLimite
        '
        Me.chkLimite.AutoSize = True
        Me.chkLimite.Checked = True
        Me.chkLimite.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkLimite.Location = New System.Drawing.Point(460, 19)
        Me.chkLimite.Name = "chkLimite"
        Me.chkLimite.Size = New System.Drawing.Size(145, 19)
        Me.chkLimite.TabIndex = 4
        Me.chkLimite.Text = "Ver solo últimos 100"
        Me.chkLimite.UseVisualStyleBackColor = True
        '
        'dtpHasta
        '
        Me.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpHasta.Location = New System.Drawing.Point(220, 52)
        Me.dtpHasta.Name = "dtpHasta"
        Me.dtpHasta.Size = New System.Drawing.Size(100, 23)
        Me.dtpHasta.TabIndex = 8
        '
        'lblHasta
        '
        Me.lblHasta.AutoSize = True
        Me.lblHasta.Location = New System.Drawing.Point(175, 56)
        Me.lblHasta.Name = "lblHasta"
        Me.lblHasta.Size = New System.Drawing.Size(40, 15)
        Me.lblHasta.TabIndex = 7
        Me.lblHasta.Text = "Hasta:"
        '
        'dtpDesde
        '
        Me.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDesde.Location = New System.Drawing.Point(60, 52)
        Me.dtpDesde.Name = "dtpDesde"
        Me.dtpDesde.Size = New System.Drawing.Size(100, 23)
        Me.dtpDesde.TabIndex = 6
        '
        'lblDesde
        '
        Me.lblDesde.AutoSize = True
        Me.lblDesde.Location = New System.Drawing.Point(10, 56)
        Me.lblDesde.Name = "lblDesde"
        Me.lblDesde.Size = New System.Drawing.Size(42, 15)
        Me.lblDesde.TabIndex = 5
        Me.lblDesde.Text = "Desde:"
        '
        'lblPuntoVenta
        '
        Me.lblPuntoVenta.AutoSize = True
        Me.lblPuntoVenta.Location = New System.Drawing.Point(10, 20)
        Me.lblPuntoVenta.Name = "lblPuntoVenta"
        Me.lblPuntoVenta.Size = New System.Drawing.Size(89, 15)
        Me.lblPuntoVenta.TabIndex = 0
        Me.lblPuntoVenta.Text = "Punto de venta:"
        '
        'cmbPuntoVenta
        '
        Me.cmbPuntoVenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPuntoVenta.FormattingEnabled = True
        Me.cmbPuntoVenta.Location = New System.Drawing.Point(105, 17)
        Me.cmbPuntoVenta.Name = "cmbPuntoVenta"
        Me.cmbPuntoVenta.Size = New System.Drawing.Size(90, 23)
        Me.cmbPuntoVenta.TabIndex = 1
        '
        'lblTipo
        '
        Me.lblTipo.AutoSize = True
        Me.lblTipo.Location = New System.Drawing.Point(210, 20)
        Me.lblTipo.Name = "lblTipo"
        Me.lblTipo.Size = New System.Drawing.Size(32, 15)
        Me.lblTipo.TabIndex = 2
        Me.lblTipo.Text = "Tipo:"
        '
        'cmbTipo
        '
        Me.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTipo.FormattingEnabled = True
        Me.cmbTipo.Location = New System.Drawing.Point(248, 17)
        Me.cmbTipo.Name = "cmbTipo"
        Me.cmbTipo.Size = New System.Drawing.Size(195, 23)
        Me.cmbTipo.TabIndex = 3
        '
        'lblBuscar
        '
        Me.lblBuscar.AutoSize = True
        Me.lblBuscar.Location = New System.Drawing.Point(340, 56)
        Me.lblBuscar.Name = "lblBuscar"
        Me.lblBuscar.Size = New System.Drawing.Size(44, 15)
        Me.lblBuscar.TabIndex = 9
        Me.lblBuscar.Text = "Buscar:"
        '
        'txtBuscar
        '
        Me.txtBuscar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBuscar.Location = New System.Drawing.Point(390, 52)
        Me.txtBuscar.Name = "txtBuscar"
        Me.txtBuscar.Size = New System.Drawing.Size(450, 23)
        Me.txtBuscar.TabIndex = 10
        '
        'cmdGenerarPDF
        '
        Me.cmdGenerarPDF.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdGenerarPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdGenerarPDF.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.cmdGenerarPDF.Location = New System.Drawing.Point(12, 505)
        Me.cmdGenerarPDF.Name = "cmdGenerarPDF"
        Me.cmdGenerarPDF.Size = New System.Drawing.Size(120, 30)
        Me.cmdGenerarPDF.TabIndex = 11
        Me.cmdGenerarPDF.Text = "Generar PDF"
        Me.cmdGenerarPDF.UseVisualStyleBackColor = True
        '
        'cmdCerrar
        '
        Me.cmdCerrar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCerrar.BackColor = System.Drawing.Color.IndianRed
        Me.cmdCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCerrar.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.cmdCerrar.ForeColor = System.Drawing.Color.White
        Me.cmdCerrar.Location = New System.Drawing.Point(784, 505)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(88, 30)
        Me.cmdCerrar.TabIndex = 12
        Me.cmdCerrar.Text = "Cerrar"
        Me.cmdCerrar.UseVisualStyleBackColor = False
        '
        'frmReimprimirCbte
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 547)
        Me.Controls.Add(Me.cmdCerrar)
        Me.Controls.Add(Me.cmdGenerarPDF)
        Me.Controls.Add(Me.PanelFiltros)
        Me.Controls.Add(Me.dgvDocumentos)
        Me.MinimumSize = New System.Drawing.Size(700, 450)
        Me.Name = "frmReimprimirCbte"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reimprimir Comprobantes"
        CType(Me.dgvDocumentos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelFiltros.ResumeLayout(False)
        Me.PanelFiltros.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dgvDocumentos As System.Windows.Forms.DataGridView
    Friend WithEvents PanelFiltros As System.Windows.Forms.Panel
    Friend WithEvents lblPuntoVenta As System.Windows.Forms.Label
    Friend WithEvents cmbPuntoVenta As System.Windows.Forms.ComboBox
    Friend WithEvents lblTipo As System.Windows.Forms.Label
    Friend WithEvents cmbTipo As System.Windows.Forms.ComboBox
    Friend WithEvents lblBuscar As System.Windows.Forms.Label
    Friend WithEvents txtBuscar As System.Windows.Forms.TextBox
    Friend WithEvents cmdGenerarPDF As System.Windows.Forms.Button
    Friend WithEvents cmdCerrar As System.Windows.Forms.Button
    Friend WithEvents dtpDesde As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDesde As System.Windows.Forms.Label
    Friend WithEvents dtpHasta As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblHasta As System.Windows.Forms.Label
    Friend WithEvents chkLimite As System.Windows.Forms.CheckBox
End Class
