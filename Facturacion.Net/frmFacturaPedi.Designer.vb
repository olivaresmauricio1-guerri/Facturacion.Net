<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFacturaPedi
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

    'NOTA: El Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        optLujan = New RadioButton()
        optNqn = New RadioButton()
        chkPendiente = New CheckBox()
        DBCTipoV = New TextBox()
        DBCExpreso = New TextBox()
        optGaray = New RadioButton()
        chkExtra = New CheckBox()
        dgvPedidos = New DataGridView()
        cmdBaja = New Button()
        optBsAs = New RadioButton()
        optMza = New RadioButton()
        txtcomentario = New TextBox()
        txtDespachos = New TextBox()
        cmdSalir = New Button()
        cmdFacturar = New Button()
        cmdVer = New Button()
        MaskGrabado = New TextBox()
        MaskIB = New TextBox()
        MaskInscrip = New TextBox()
        MaskNoInscrip = New TextBox()
        MaskTotal = New TextBox()
        Label2 = New Label()
        Label1 = New Label()
        PanelShape1 = New Panel()
        CType(dgvPedidos, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' optLujan
        ' 
        optLujan.AutoSize = True
        optLujan.Font = New Font("Microsoft Sans Serif", 9F, FontStyle.Bold)
        optLujan.Location = New Point(289, 600)
        optLujan.Margin = New Padding(4, 3, 4, 3)
        optLujan.Name = "optLujan"
        optLujan.Size = New Size(61, 19)
        optLujan.TabIndex = 22
        optLujan.Text = "Lujan"
        optLujan.UseVisualStyleBackColor = True
        ' 
        ' optNqn
        ' 
        optNqn.AutoSize = True
        optNqn.Font = New Font("Microsoft Sans Serif", 9F, FontStyle.Bold)
        optNqn.Location = New Point(289, 582)
        optNqn.Margin = New Padding(4, 3, 4, 3)
        optNqn.Name = "optNqn"
        optNqn.Size = New Size(83, 19)
        optNqn.TabIndex = 21
        optNqn.Text = "Neuquen"
        optNqn.UseVisualStyleBackColor = True
        ' 
        ' chkPendiente
        ' 
        chkPendiente.AutoSize = True
        chkPendiente.Location = New Point(205, 563)
        chkPendiente.Margin = New Padding(4, 3, 4, 3)
        chkPendiente.Name = "chkPendiente"
        chkPendiente.Size = New Size(15, 14)
        chkPendiente.TabIndex = 19
        chkPendiente.UseVisualStyleBackColor = True
        ' 
        ' DBCTipoV
        ' 
        DBCTipoV.Location = New Point(355, 655)
        DBCTipoV.Margin = New Padding(4, 3, 4, 3)
        DBCTipoV.Name = "DBCTipoV"
        DBCTipoV.Size = New Size(131, 23)
        DBCTipoV.TabIndex = 18
        ' 
        ' DBCExpreso
        ' 
        DBCExpreso.Location = New Point(439, 655)
        DBCExpreso.Margin = New Padding(4, 3, 4, 3)
        DBCExpreso.Name = "DBCExpreso"
        DBCExpreso.Size = New Size(168, 23)
        DBCExpreso.TabIndex = 17
        ' 
        ' optGaray
        ' 
        optGaray.AutoSize = True
        optGaray.Font = New Font("Microsoft Sans Serif", 9F, FontStyle.Bold)
        optGaray.Location = New Point(289, 563)
        optGaray.Margin = New Padding(4, 3, 4, 3)
        optGaray.Name = "optGaray"
        optGaray.Size = New Size(62, 19)
        optGaray.TabIndex = 16
        optGaray.Text = "Garay"
        optGaray.UseVisualStyleBackColor = True
        ' 
        ' chkExtra
        ' 
        chkExtra.AutoSize = True
        chkExtra.Location = New Point(168, 554)
        chkExtra.Margin = New Padding(4, 3, 4, 3)
        chkExtra.Name = "chkExtra"
        chkExtra.Size = New Size(15, 14)
        chkExtra.TabIndex = 15
        chkExtra.UseVisualStyleBackColor = True
        ' 
        ' dgvPedidos
        ' 
        dgvPedidos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvPedidos.Location = New Point(0, 0)
        dgvPedidos.Margin = New Padding(4, 3, 4, 3)
        dgvPedidos.Name = "dgvPedidos"
        dgvPedidos.Size = New Size(701, 518)
        dgvPedidos.TabIndex = 13
        ' 
        ' cmdBaja
        ' 
        cmdBaja.FlatStyle = FlatStyle.Flat
        cmdBaja.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cmdBaja.Location = New Point(9, 554)
        cmdBaja.Margin = New Padding(4, 3, 4, 3)
        cmdBaja.Name = "cmdBaja"
        cmdBaja.Size = New Size(104, 43)
        cmdBaja.TabIndex = 12
        cmdBaja.Text = "Bajar Pedido"
        cmdBaja.UseVisualStyleBackColor = True
        ' 
        ' optBsAs
        ' 
        optBsAs.AutoSize = True
        optBsAs.Font = New Font("Microsoft Sans Serif", 9F, FontStyle.Bold)
        optBsAs.Location = New Point(289, 526)
        optBsAs.Margin = New Padding(4, 3, 4, 3)
        optBsAs.Name = "optBsAs"
        optBsAs.Size = New Size(60, 19)
        optBsAs.TabIndex = 11
        optBsAs.Text = "BsAs."
        optBsAs.UseVisualStyleBackColor = True
        ' 
        ' optMza
        ' 
        optMza.AutoSize = True
        optMza.Font = New Font("Microsoft Sans Serif", 9F, FontStyle.Bold)
        optMza.Location = New Point(289, 545)
        optMza.Margin = New Padding(4, 3, 4, 3)
        optMza.Name = "optMza"
        optMza.Size = New Size(56, 19)
        optMza.TabIndex = 10
        optMza.Text = "Mza."
        optMza.UseVisualStyleBackColor = True
        ' 
        ' txtcomentario
        ' 
        txtcomentario.Location = New Point(364, 720)
        txtcomentario.Margin = New Padding(4, 3, 4, 3)
        txtcomentario.Name = "txtcomentario"
        txtcomentario.Size = New Size(112, 23)
        txtcomentario.TabIndex = 9
        ' 
        ' txtDespachos
        ' 
        txtDespachos.Location = New Point(19, 711)
        txtDespachos.Margin = New Padding(4, 3, 4, 3)
        txtDespachos.Name = "txtDespachos"
        txtDespachos.Size = New Size(131, 23)
        txtDespachos.TabIndex = 3
        ' 
        ' cmdSalir
        ' 
        cmdSalir.BackColor = Color.IndianRed
        cmdSalir.FlatStyle = FlatStyle.Flat
        cmdSalir.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cmdSalir.ForeColor = Color.White
        cmdSalir.Location = New Point(597, 535)
        cmdSalir.Margin = New Padding(4, 3, 4, 3)
        cmdSalir.Name = "cmdSalir"
        cmdSalir.Size = New Size(94, 80)
        cmdSalir.TabIndex = 2
        cmdSalir.Text = "Salir"
        cmdSalir.UseVisualStyleBackColor = False
        ' 
        ' cmdFacturar
        ' 
        cmdFacturar.BackColor = Color.SteelBlue
        cmdFacturar.FlatStyle = FlatStyle.Flat
        cmdFacturar.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cmdFacturar.ForeColor = Color.White
        cmdFacturar.Location = New Point(495, 535)
        cmdFacturar.Margin = New Padding(4, 3, 4, 3)
        cmdFacturar.Name = "cmdFacturar"
        cmdFacturar.Size = New Size(94, 80)
        cmdFacturar.TabIndex = 1
        cmdFacturar.Text = "Facturar"
        cmdFacturar.UseVisualStyleBackColor = False
        ' 
        ' cmdVer
        ' 
        cmdVer.BackColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        cmdVer.FlatStyle = FlatStyle.Flat
        cmdVer.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cmdVer.Location = New Point(383, 535)
        cmdVer.Margin = New Padding(4, 3, 4, 3)
        cmdVer.Name = "cmdVer"
        cmdVer.Size = New Size(94, 80)
        cmdVer.TabIndex = 0
        cmdVer.Text = "Pendientes Facturación"
        cmdVer.UseVisualStyleBackColor = False
        ' 
        ' MaskGrabado
        ' 
        MaskGrabado.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        MaskGrabado.Location = New Point(19, 748)
        MaskGrabado.Margin = New Padding(4, 3, 4, 3)
        MaskGrabado.Name = "MaskGrabado"
        MaskGrabado.Size = New Size(94, 20)
        MaskGrabado.TabIndex = 4
        ' 
        ' MaskIB
        ' 
        MaskIB.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        MaskIB.Location = New Point(121, 748)
        MaskIB.Margin = New Padding(4, 3, 4, 3)
        MaskIB.Name = "MaskIB"
        MaskIB.Size = New Size(94, 20)
        MaskIB.TabIndex = 5
        ' 
        ' MaskInscrip
        ' 
        MaskInscrip.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        MaskInscrip.Location = New Point(224, 748)
        MaskInscrip.Margin = New Padding(4, 3, 4, 3)
        MaskInscrip.Name = "MaskInscrip"
        MaskInscrip.Size = New Size(94, 20)
        MaskInscrip.TabIndex = 6
        ' 
        ' MaskNoInscrip
        ' 
        MaskNoInscrip.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        MaskNoInscrip.Location = New Point(317, 748)
        MaskNoInscrip.Margin = New Padding(4, 3, 4, 3)
        MaskNoInscrip.Name = "MaskNoInscrip"
        MaskNoInscrip.Size = New Size(94, 20)
        MaskNoInscrip.TabIndex = 7
        ' 
        ' MaskTotal
        ' 
        MaskTotal.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        MaskTotal.Location = New Point(439, 748)
        MaskTotal.Margin = New Padding(4, 3, 4, 3)
        MaskTotal.Name = "MaskTotal"
        MaskTotal.Size = New Size(94, 20)
        MaskTotal.TabIndex = 8
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(205, 582)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(69, 15)
        Label2.TabIndex = 20
        Label2.Text = "Pendiente E"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.White
        Label1.Font = New Font("Microsoft Sans Serif", 24F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(131, 545)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(39, 37)
        Label1.TabIndex = 14
        Label1.Text = "E"
        ' 
        ' PanelShape1
        ' 
        PanelShape1.BackColor = Color.White
        PanelShape1.Location = New Point(121, 535)
        PanelShape1.Margin = New Padding(4, 3, 4, 3)
        PanelShape1.Name = "PanelShape1"
        PanelShape1.Size = New Size(76, 75)
        PanelShape1.TabIndex = 23
        ' 
        ' frmFacturaPedi
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(706, 620)
        Controls.Add(PanelShape1)
        Controls.Add(optLujan)
        Controls.Add(optNqn)
        Controls.Add(chkPendiente)
        Controls.Add(DBCTipoV)
        Controls.Add(DBCExpreso)
        Controls.Add(optGaray)
        Controls.Add(chkExtra)
        Controls.Add(dgvPedidos)
        Controls.Add(cmdBaja)
        Controls.Add(optBsAs)
        Controls.Add(optMza)
        Controls.Add(txtcomentario)
        Controls.Add(txtDespachos)
        Controls.Add(cmdSalir)
        Controls.Add(cmdFacturar)
        Controls.Add(cmdVer)
        Controls.Add(MaskGrabado)
        Controls.Add(MaskIB)
        Controls.Add(MaskInscrip)
        Controls.Add(MaskNoInscrip)
        Controls.Add(MaskTotal)
        Controls.Add(Label2)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmFacturaPedi"
        Text = "Facturar Pedidos"
        CType(dgvPedidos, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents optLujan As System.Windows.Forms.RadioButton
    Friend WithEvents optNqn As System.Windows.Forms.RadioButton
    Friend WithEvents chkPendiente As System.Windows.Forms.CheckBox
    Friend WithEvents DBCTipoV As System.Windows.Forms.TextBox
    Friend WithEvents DBCExpreso As System.Windows.Forms.TextBox
    Friend WithEvents optGaray As System.Windows.Forms.RadioButton
    Friend WithEvents chkExtra As System.Windows.Forms.CheckBox
    Friend WithEvents dgvPedidos As System.Windows.Forms.DataGridView
    Friend WithEvents cmdBaja As System.Windows.Forms.Button
    Friend WithEvents optBsAs As System.Windows.Forms.RadioButton
    Friend WithEvents optMza As System.Windows.Forms.RadioButton
    Friend WithEvents txtcomentario As System.Windows.Forms.TextBox
    Friend WithEvents txtDespachos As System.Windows.Forms.TextBox
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
    Friend WithEvents cmdFacturar As System.Windows.Forms.Button
    Friend WithEvents cmdVer As System.Windows.Forms.Button
    Friend WithEvents MaskGrabado As System.Windows.Forms.TextBox
    Friend WithEvents MaskIB As System.Windows.Forms.TextBox
    Friend WithEvents MaskInscrip As System.Windows.Forms.TextBox
    Friend WithEvents MaskNoInscrip As System.Windows.Forms.TextBox
    Friend WithEvents MaskTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PanelShape1 As System.Windows.Forms.Panel
End Class
