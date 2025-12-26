<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmArticuloSelector
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
        Label1 = New Label()
        txtBuscar = New TextBox()
        DgvListado = New DataGridView()
        CmdAceptar = New Button()
        CmdSalir = New Button()
        CType(DgvListado, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(45, 15)
        Label1.TabIndex = 0
        Label1.Text = "Buscar:"
        ' 
        ' txtBuscar
        ' 
        txtBuscar.Location = New Point(63, 6)
        txtBuscar.Name = "txtBuscar"
        txtBuscar.Size = New Size(511, 23)
        txtBuscar.TabIndex = 1
        ' 
        ' DgvListado
        ' 
        DgvListado.AllowUserToAddRows = False
        DgvListado.AllowUserToDeleteRows = False
        DgvListado.AllowUserToResizeColumns = False
        DgvListado.AllowUserToResizeRows = False
        DgvListado.Location = New Point(12, 35)
        DgvListado.Name = "DgvListado"
        DgvListado.ReadOnly = True
        DgvListado.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DgvListado.Size = New Size(671, 250)
        DgvListado.TabIndex = 3
        ' 
        ' CmdAceptar
        ' 
        CmdAceptar.Cursor = Cursors.Hand
        CmdAceptar.FlatStyle = FlatStyle.Flat
        CmdAceptar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdAceptar.Location = New Point(527, 291)
        CmdAceptar.Name = "CmdAceptar"
        CmdAceptar.Size = New Size(75, 30)
        CmdAceptar.TabIndex = 26
        CmdAceptar.Text = "&Aceptar"
        ' 
        ' CmdSalir
        ' 
        CmdSalir.BackColor = Color.IndianRed
        CmdSalir.Cursor = Cursors.Hand
        CmdSalir.FlatStyle = FlatStyle.Flat
        CmdSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdSalir.ForeColor = Color.White
        CmdSalir.Location = New Point(608, 291)
        CmdSalir.Name = "CmdSalir"
        CmdSalir.Size = New Size(75, 30)
        CmdSalir.TabIndex = 27
        CmdSalir.Text = "&Salir"
        CmdSalir.UseVisualStyleBackColor = False
        ' 
        ' frmArticuloSelector
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(695, 326)
        Controls.Add(CmdAceptar)
        Controls.Add(CmdSalir)
        Controls.Add(DgvListado)
        Controls.Add(txtBuscar)
        Controls.Add(Label1)
        Name = "frmArticuloSelector"
        Text = "Selector de Artículos"
        CType(DgvListado, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtBuscar As TextBox
    Friend WithEvents DgvListado As DataGridView
    Friend WithEvents CmdAceptar As Button
    Friend WithEvents CmdSalir As Button
End Class
