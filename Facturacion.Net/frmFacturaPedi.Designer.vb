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
        optGaray = New RadioButton()
        chkExtra = New CheckBox()
        dgvPedidos = New DataGridView()
        cmdBaja = New Button()
        optBsAs = New RadioButton()
        optMza = New RadioButton()
        cmdSalir = New Button()
        cmdFacturar = New Button()
        cmdVer = New Button()
        Label1 = New Label()
        PanelShape1 = New Panel()
        Button1 = New Button()
        CType(dgvPedidos, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' optLujan
        ' 
        optLujan.AutoSize = True
        optLujan.Font = New Font("Microsoft Sans Serif", 9.0F, FontStyle.Bold)
        optLujan.Location = New Point(323, 595)
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
        optNqn.Font = New Font("Microsoft Sans Serif", 9.0F, FontStyle.Bold)
        optNqn.Location = New Point(323, 577)
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
        chkPendiente.Location = New Point(205, 523)
        chkPendiente.Margin = New Padding(4, 3, 4, 3)
        chkPendiente.Name = "chkPendiente"
        chkPendiente.Size = New Size(88, 19)
        chkPendiente.TabIndex = 19
        chkPendiente.Text = "Pendiente E"
        chkPendiente.UseVisualStyleBackColor = True
        ' 
        ' optGaray
        ' 
        optGaray.AutoSize = True
        optGaray.Font = New Font("Microsoft Sans Serif", 9.0F, FontStyle.Bold)
        optGaray.Location = New Point(323, 558)
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
        chkExtra.Location = New Point(205, 548)
        chkExtra.Margin = New Padding(4, 3, 4, 3)
        chkExtra.Name = "chkExtra"
        chkExtra.Size = New Size(99, 19)
        chkExtra.TabIndex = 15
        chkExtra.Text = "Extraterritorial"
        chkExtra.UseVisualStyleBackColor = True
        ' 
        ' dgvPedidos
        ' 
        dgvPedidos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvPedidos.Location = New Point(0, 0)
        dgvPedidos.Margin = New Padding(4, 3, 4, 3)
        dgvPedidos.Name = "dgvPedidos"
        dgvPedidos.Size = New Size(745, 518)
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
        optBsAs.Font = New Font("Microsoft Sans Serif", 9.0F, FontStyle.Bold)
        optBsAs.Location = New Point(323, 521)
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
        optMza.Font = New Font("Microsoft Sans Serif", 9.0F, FontStyle.Bold)
        optMza.Location = New Point(323, 540)
        optMza.Margin = New Padding(4, 3, 4, 3)
        optMza.Name = "optMza"
        optMza.Size = New Size(56, 19)
        optMza.TabIndex = 10
        optMza.Text = "Mza."
        optMza.UseVisualStyleBackColor = True
        ' 
        ' cmdSalir
        ' 
        cmdSalir.BackColor = Color.IndianRed
        cmdSalir.FlatStyle = FlatStyle.Flat
        cmdSalir.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        cmdSalir.ForeColor = Color.White
        cmdSalir.Location = New Point(649, 533)
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
        cmdFacturar.Location = New Point(547, 533)
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
        cmdVer.Location = New Point(435, 533)
        cmdVer.Margin = New Padding(4, 3, 4, 3)
        cmdVer.Name = "cmdVer"
        cmdVer.Size = New Size(94, 80)
        cmdVer.TabIndex = 0
        cmdVer.Text = "Pendientes Facturación"
        cmdVer.UseVisualStyleBackColor = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.White
        Label1.Font = New Font("Microsoft Sans Serif", 24.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
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
        PanelShape1.Location = New Point(121, 544)
        PanelShape1.Margin = New Padding(4, 3, 4, 3)
        PanelShape1.Name = "PanelShape1"
        PanelShape1.Size = New Size(62, 66)
        PanelShape1.TabIndex = 23
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(12, 525)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 23)
        Button1.TabIndex = 24
        Button1.Text = "Button1"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' frmFacturaPedi
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(745, 626)
        Controls.Add(Button1)
        Controls.Add(PanelShape1)
        Controls.Add(optLujan)
        Controls.Add(optNqn)
        Controls.Add(chkPendiente)
        Controls.Add(optGaray)
        Controls.Add(chkExtra)
        Controls.Add(dgvPedidos)
        Controls.Add(cmdBaja)
        Controls.Add(optBsAs)
        Controls.Add(optMza)
        Controls.Add(cmdSalir)
        Controls.Add(cmdFacturar)
        Controls.Add(cmdVer)
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
    Friend WithEvents optGaray As System.Windows.Forms.RadioButton
    Friend WithEvents chkExtra As System.Windows.Forms.CheckBox
    Friend WithEvents dgvPedidos As System.Windows.Forms.DataGridView
    Friend WithEvents cmdBaja As System.Windows.Forms.Button
    Friend WithEvents optBsAs As System.Windows.Forms.RadioButton
    Friend WithEvents optMza As System.Windows.Forms.RadioButton
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
    Friend WithEvents cmdFacturar As System.Windows.Forms.Button
    Friend WithEvents cmdVer As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PanelShape1 As System.Windows.Forms.Panel
    Friend WithEvents Button1 As Button
End Class
