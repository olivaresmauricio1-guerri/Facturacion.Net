<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmVerPedido
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        grpCabecera = New GroupBox()
        lblFechaEntrega = New Label()
        lblFechaPedido = New Label()
        lblVendedor = New Label()
        lblCliente = New Label()
        txtFechaE = New TextBox()
        txtfechaP = New TextBox()
        txtvendedor = New TextBox()
        txtCliente = New TextBox()
        dgvPedidos = New DataGridView()
        lblAutoriza = New Label()
        txtAutoriza = New TextBox()
        lblFechaAutoriza = New Label()
        txtFecha = New TextBox()
        lblComentarios = New Label()
        txtComenta = New TextBox()
        lblObs = New Label()
        txtObs = New TextBox()
        cmdSalir = New Button()
        grpCabecera.SuspendLayout()
        CType(dgvPedidos, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' grpCabecera
        ' 
        grpCabecera.Controls.Add(lblFechaEntrega)
        grpCabecera.Controls.Add(lblFechaPedido)
        grpCabecera.Controls.Add(lblVendedor)
        grpCabecera.Controls.Add(lblCliente)
        grpCabecera.Controls.Add(txtFechaE)
        grpCabecera.Controls.Add(txtfechaP)
        grpCabecera.Controls.Add(txtvendedor)
        grpCabecera.Controls.Add(txtCliente)
        grpCabecera.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        grpCabecera.ForeColor = Color.Red
        grpCabecera.Location = New Point(0, 0)
        grpCabecera.Margin = New Padding(4, 3, 4, 3)
        grpCabecera.Name = "grpCabecera"
        grpCabecera.Padding = New Padding(4, 3, 4, 3)
        grpCabecera.Size = New Size(1160, 57)
        grpCabecera.TabIndex = 0
        grpCabecera.TabStop = False
        ' 
        ' lblFechaEntrega
        ' 
        lblFechaEntrega.AutoSize = True
        lblFechaEntrega.ForeColor = Color.Black
        lblFechaEntrega.Location = New Point(792, 15)
        lblFechaEntrega.Margin = New Padding(4, 0, 4, 0)
        lblFechaEntrega.Name = "lblFechaEntrega"
        lblFechaEntrega.Size = New Size(93, 13)
        lblFechaEntrega.TabIndex = 7
        lblFechaEntrega.Text = "Fecha Pactada"
        ' 
        ' lblFechaPedido
        ' 
        lblFechaPedido.AutoSize = True
        lblFechaPedido.ForeColor = Color.Black
        lblFechaPedido.Location = New Point(670, 15)
        lblFechaPedido.Margin = New Padding(4, 0, 4, 0)
        lblFechaPedido.Name = "lblFechaPedido"
        lblFechaPedido.Size = New Size(85, 13)
        lblFechaPedido.TabIndex = 6
        lblFechaPedido.Text = "Fecha Pedido"
        ' 
        ' lblVendedor
        ' 
        lblVendedor.AutoSize = True
        lblVendedor.ForeColor = Color.Black
        lblVendedor.Location = New Point(419, 14)
        lblVendedor.Margin = New Padding(4, 0, 4, 0)
        lblVendedor.Name = "lblVendedor"
        lblVendedor.Size = New Size(61, 13)
        lblVendedor.TabIndex = 5
        lblVendedor.Text = "Vendedor"
        ' 
        ' lblCliente
        ' 
        lblCliente.AutoSize = True
        lblCliente.ForeColor = Color.Black
        lblCliente.Location = New Point(9, 14)
        lblCliente.Margin = New Padding(4, 0, 4, 0)
        lblCliente.Name = "lblCliente"
        lblCliente.Size = New Size(46, 13)
        lblCliente.TabIndex = 4
        lblCliente.Text = "Cliente"
        ' 
        ' txtFechaE
        ' 
        txtFechaE.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtFechaE.ForeColor = Color.Black
        txtFechaE.Location = New Point(792, 29)
        txtFechaE.Margin = New Padding(4, 3, 4, 3)
        txtFechaE.Name = "txtFechaE"
        txtFechaE.ReadOnly = True
        txtFechaE.Size = New Size(103, 22)
        txtFechaE.TabIndex = 3
        ' 
        ' txtfechaP
        ' 
        txtfechaP.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtfechaP.ForeColor = Color.Black
        txtfechaP.Location = New Point(670, 29)
        txtfechaP.Margin = New Padding(4, 3, 4, 3)
        txtfechaP.Name = "txtfechaP"
        txtfechaP.ReadOnly = True
        txtfechaP.Size = New Size(103, 22)
        txtfechaP.TabIndex = 2
        ' 
        ' txtvendedor
        ' 
        txtvendedor.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtvendedor.ForeColor = Color.Black
        txtvendedor.Location = New Point(419, 28)
        txtvendedor.Margin = New Padding(4, 3, 4, 3)
        txtvendedor.Name = "txtvendedor"
        txtvendedor.ReadOnly = True
        txtvendedor.Size = New Size(243, 22)
        txtvendedor.TabIndex = 1
        ' 
        ' txtCliente
        ' 
        txtCliente.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtCliente.ForeColor = Color.FromArgb(CByte(64), CByte(0), CByte(0))
        txtCliente.Location = New Point(9, 28)
        txtCliente.Margin = New Padding(4, 3, 4, 3)
        txtCliente.Name = "txtCliente"
        txtCliente.ReadOnly = True
        txtCliente.Size = New Size(400, 22)
        txtCliente.TabIndex = 0
        ' 
        ' dgvPedidos
        ' 
        dgvPedidos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvPedidos.Location = New Point(0, 65)
        dgvPedidos.Margin = New Padding(4, 3, 4, 3)
        dgvPedidos.Name = "dgvPedidos"
        dgvPedidos.ReadOnly = True
        dgvPedidos.Size = New Size(1160, 326)
        dgvPedidos.TabIndex = 1
        ' 
        ' lblAutoriza
        ' 
        lblAutoriza.AutoSize = True
        lblAutoriza.Font = New Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblAutoriza.Location = New Point(9, 410)
        lblAutoriza.Margin = New Padding(4, 0, 4, 0)
        lblAutoriza.Name = "lblAutoriza"
        lblAutoriza.Size = New Size(57, 15)
        lblAutoriza.TabIndex = 2
        lblAutoriza.Text = "Autoriza :"
        ' 
        ' txtAutoriza
        ' 
        txtAutoriza.Location = New Point(89, 406)
        txtAutoriza.Margin = New Padding(4, 3, 4, 3)
        txtAutoriza.Name = "txtAutoriza"
        txtAutoriza.ReadOnly = True
        txtAutoriza.Size = New Size(112, 23)
        txtAutoriza.TabIndex = 3
        ' 
        ' lblFechaAutoriza
        ' 
        lblFechaAutoriza.AutoSize = True
        lblFechaAutoriza.Location = New Point(209, 410)
        lblFechaAutoriza.Margin = New Padding(4, 0, 4, 0)
        lblFechaAutoriza.Name = "lblFechaAutoriza"
        lblFechaAutoriza.Size = New Size(38, 15)
        lblFechaAutoriza.TabIndex = 4
        lblFechaAutoriza.Text = "Fecha"
        ' 
        ' txtFecha
        ' 
        txtFecha.Location = New Point(257, 406)
        txtFecha.Margin = New Padding(4, 3, 4, 3)
        txtFecha.Name = "txtFecha"
        txtFecha.ReadOnly = True
        txtFecha.Size = New Size(74, 23)
        txtFecha.TabIndex = 5
        ' 
        ' lblComentarios
        ' 
        lblComentarios.AutoSize = True
        lblComentarios.Location = New Point(339, 410)
        lblComentarios.Margin = New Padding(4, 0, 4, 0)
        lblComentarios.Name = "lblComentarios"
        lblComentarios.Size = New Size(70, 15)
        lblComentarios.TabIndex = 6
        lblComentarios.Text = "Comentario"
        ' 
        ' txtComenta
        ' 
        txtComenta.Location = New Point(423, 406)
        txtComenta.Margin = New Padding(4, 3, 4, 3)
        txtComenta.Name = "txtComenta"
        txtComenta.ReadOnly = True
        txtComenta.Size = New Size(352, 23)
        txtComenta.TabIndex = 7
        ' 
        ' lblObs
        ' 
        lblObs.AutoSize = True
        lblObs.Location = New Point(8, 439)
        lblObs.Margin = New Padding(4, 0, 4, 0)
        lblObs.Name = "lblObs"
        lblObs.Size = New Size(73, 15)
        lblObs.TabIndex = 8
        lblObs.Text = "Observación"
        ' 
        ' txtObs
        ' 
        txtObs.Location = New Point(89, 435)
        txtObs.Margin = New Padding(4, 3, 4, 3)
        txtObs.Name = "txtObs"
        txtObs.ReadOnly = True
        txtObs.Size = New Size(686, 23)
        txtObs.TabIndex = 9
        ' 
        ' cmdSalir
        ' 
        cmdSalir.BackColor = Color.IndianRed
        cmdSalir.FlatStyle = FlatStyle.Flat
        cmdSalir.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cmdSalir.ForeColor = Color.White
        cmdSalir.Location = New Point(1047, 406)
        cmdSalir.Margin = New Padding(4, 3, 4, 3)
        cmdSalir.Name = "cmdSalir"
        cmdSalir.Size = New Size(113, 52)
        cmdSalir.TabIndex = 10
        cmdSalir.Text = "&Salir"
        cmdSalir.UseVisualStyleBackColor = False
        ' 
        ' frmVerPedido
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1166, 466)
        Controls.Add(cmdSalir)
        Controls.Add(txtObs)
        Controls.Add(lblObs)
        Controls.Add(txtComenta)
        Controls.Add(lblComentarios)
        Controls.Add(txtFecha)
        Controls.Add(lblFechaAutoriza)
        Controls.Add(txtAutoriza)
        Controls.Add(lblAutoriza)
        Controls.Add(dgvPedidos)
        Controls.Add(grpCabecera)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmVerPedido"
        Text = "Ver Pedido"
        grpCabecera.ResumeLayout(False)
        grpCabecera.PerformLayout()
        CType(dgvPedidos, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents grpCabecera As System.Windows.Forms.GroupBox
    Friend WithEvents lblFechaEntrega As System.Windows.Forms.Label
    Friend WithEvents lblFechaPedido As System.Windows.Forms.Label
    Friend WithEvents lblVendedor As System.Windows.Forms.Label
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents txtFechaE As System.Windows.Forms.TextBox
    Friend WithEvents txtfechaP As System.Windows.Forms.TextBox
    Friend WithEvents txtvendedor As System.Windows.Forms.TextBox
    Friend WithEvents txtCliente As System.Windows.Forms.TextBox
    Friend WithEvents dgvPedidos As System.Windows.Forms.DataGridView
    Friend WithEvents lblAutoriza As System.Windows.Forms.Label
    Friend WithEvents txtAutoriza As System.Windows.Forms.TextBox
    Friend WithEvents lblFechaAutoriza As System.Windows.Forms.Label
    Friend WithEvents txtFecha As System.Windows.Forms.TextBox
    Friend WithEvents lblComentarios As System.Windows.Forms.Label
    Friend WithEvents txtComenta As System.Windows.Forms.TextBox
    Friend WithEvents lblObs As System.Windows.Forms.Label
    Friend WithEvents txtObs As System.Windows.Forms.TextBox
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
End Class

