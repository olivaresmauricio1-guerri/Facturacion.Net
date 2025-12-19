<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmControlRemito
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmControlRemito))
        optUsoInterno = New RadioButton()
        optRemito = New RadioButton()
        btnSalir = New Button()
        btnBuscar = New Button()
        txtRemito = New TextBox()
        dtpFecha = New DateTimePicker()
        SuspendLayout()
        ' 
        ' optUsoInterno
        ' 
        optUsoInterno.AutoSize = True
        optUsoInterno.Location = New Point(336, 27)
        optUsoInterno.Margin = New Padding(4, 3, 4, 3)
        optUsoInterno.Name = "optUsoInterno"
        optUsoInterno.Size = New Size(86, 19)
        optUsoInterno.TabIndex = 6
        optUsoInterno.Text = "Uso Interno"
        optUsoInterno.UseVisualStyleBackColor = True
        ' 
        ' optRemito
        ' 
        optRemito.AutoSize = True
        optRemito.Checked = True
        optRemito.Location = New Point(336, 9)
        optRemito.Margin = New Padding(4, 3, 4, 3)
        optRemito.Name = "optRemito"
        optRemito.Size = New Size(63, 19)
        optRemito.TabIndex = 5
        optRemito.TabStop = True
        optRemito.Text = "Remito"
        optRemito.UseVisualStyleBackColor = True
        ' 
        ' btnSalir
        ' 
        btnSalir.BackColor = Color.IndianRed
        btnSalir.FlatStyle = FlatStyle.Flat
        btnSalir.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnSalir.ForeColor = Color.White
        btnSalir.Location = New Point(551, 10)
        btnSalir.Margin = New Padding(4, 3, 4, 3)
        btnSalir.Name = "btnSalir"
        btnSalir.Size = New Size(85, 37)
        btnSalir.TabIndex = 4
        btnSalir.Text = "Salir"
        btnSalir.UseVisualStyleBackColor = False
        ' 
        ' btnBuscar
        ' 
        btnBuscar.FlatStyle = FlatStyle.Flat
        btnBuscar.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnBuscar.Location = New Point(458, 9)
        btnBuscar.Margin = New Padding(4, 3, 4, 3)
        btnBuscar.Name = "btnBuscar"
        btnBuscar.Size = New Size(85, 37)
        btnBuscar.TabIndex = 1
        btnBuscar.Text = "Buscar"
        btnBuscar.UseVisualStyleBackColor = True
        ' 
        ' txtRemito
        ' 
        txtRemito.Font = New Font("Microsoft Sans Serif", 13.5F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtRemito.Location = New Point(9, 11)
        txtRemito.Margin = New Padding(4, 3, 4, 3)
        txtRemito.Name = "txtRemito"
        txtRemito.Size = New Size(132, 28)
        txtRemito.TabIndex = 0
        txtRemito.TextAlign = HorizontalAlignment.Center
        ' 
        ' dtpFecha
        ' 
        dtpFecha.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        dtpFecha.Format = DateTimePickerFormat.Short
        dtpFecha.Location = New Point(149, 14)
        dtpFecha.Margin = New Padding(4, 3, 4, 3)
        dtpFecha.Name = "dtpFecha"
        dtpFecha.Size = New Size(140, 22)
        dtpFecha.TabIndex = 2
        ' 
        ' frmControlRemito
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(645, 59)
        Controls.Add(dtpFecha)
        Controls.Add(optUsoInterno)
        Controls.Add(optRemito)
        Controls.Add(btnSalir)
        Controls.Add(btnBuscar)
        Controls.Add(txtRemito)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmControlRemito"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Control de remitos"
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents optUsoInterno As RadioButton
    Friend WithEvents optRemito As RadioButton
    Friend WithEvents btnSalir As Button
    Friend WithEvents btnBuscar As Button
    Friend WithEvents txtRemito As TextBox
    Friend WithEvents dtpFecha As DateTimePicker
End Class