<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImagenesFaltantes
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
        btnImagenes = New Button()
        dgvImagenes = New DataGridView()
        CType(dgvImagenes, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btnImagenes
        ' 
        btnImagenes.Location = New Point(620, 628)
        btnImagenes.Name = "btnImagenes"
        btnImagenes.Size = New Size(75, 23)
        btnImagenes.TabIndex = 0
        btnImagenes.Text = "Cargar"
        btnImagenes.UseVisualStyleBackColor = True
        ' 
        ' dgvImagenes
        ' 
        dgvImagenes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvImagenes.Location = New Point(12, 31)
        dgvImagenes.Name = "dgvImagenes"
        dgvImagenes.Size = New Size(692, 581)
        dgvImagenes.TabIndex = 1
        ' 
        ' frmImagenesFaltantes
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(707, 663)
        Controls.Add(dgvImagenes)
        Controls.Add(btnImagenes)
        Name = "frmImagenesFaltantes"
        Text = "frmImagenesFaltantes"
        CType(dgvImagenes, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents btnImagenes As Button
    Friend WithEvents dgvImagenes As DataGridView
End Class
