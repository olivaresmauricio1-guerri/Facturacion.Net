<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmListadoNovedadesStock
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
        GroupBox1 = New GroupBox()
        CmdSalir = New Button()
        CmdImprimir = New Button()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(CmdSalir)
        GroupBox1.Controls.Add(CmdImprimir)
        GroupBox1.Location = New Point(2, 1)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(274, 68)
        GroupBox1.TabIndex = 1
        GroupBox1.TabStop = False
        ' 
        ' CmdSalir
        ' 
        CmdSalir.BackColor = Color.IndianRed
        CmdSalir.FlatStyle = FlatStyle.Flat
        CmdSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        CmdSalir.ForeColor = Color.White
        CmdSalir.Location = New Point(154, 22)
        CmdSalir.Name = "CmdSalir"
        CmdSalir.Size = New Size(88, 33)
        CmdSalir.TabIndex = 1
        CmdSalir.Text = "Listar"
        CmdSalir.UseVisualStyleBackColor = False
        ' 
        ' CmdImprimir
        ' 
        CmdImprimir.FlatStyle = FlatStyle.Flat
        CmdImprimir.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        CmdImprimir.Location = New Point(34, 22)
        CmdImprimir.Name = "CmdImprimir"
        CmdImprimir.Size = New Size(88, 33)
        CmdImprimir.TabIndex = 0
        CmdImprimir.Text = "Imprimir"
        CmdImprimir.UseVisualStyleBackColor = True
        ' 
        ' frmListadoNovedadesStock
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(279, 72)
        Controls.Add(GroupBox1)
        FormBorderStyle = FormBorderStyle.FixedSingle
        Name = "frmListadoNovedadesStock"
        Text = "Listado de Novedades Stock"
        GroupBox1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents CmdSalir As Button
    Friend WithEvents CmdImprimir As Button
End Class
