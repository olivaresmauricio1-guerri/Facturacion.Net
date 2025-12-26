Imports DSM = DataSourceManager.Lib.DataSourceManager
Public Class frmArticuloSelector
    Private tabla As New DataTable
    Private filaActual As DataGridViewRow
    Private filaActualIndice As Integer = -1
    Public Property Seleccion As Dictionary(Of String, Object)

    Private Sub frmArticuloSelector_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        gridbuscar()
        GridConfigurarColumnas()
    End Sub
    Private Sub DgvListado_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvListado.CellClick
        If DgvListado.SelectedRows.Count > 1 Or e.RowIndex < 0 Then
            filaActual = Nothing
            filaActualIndice = -1
            Return
        End If
        filaActualIndice = e.RowIndex
        filaActual = DgvListado.Rows(e.RowIndex)
    End Sub
    Private Sub DgvListado_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvListado.CellDoubleClick
        AceptarSeleccion()
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles CmdAceptar.Click
        AceptarSeleccion()
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Close()
    End Sub
    Private Sub gridbuscar()
        Dim texto As String = txtBuscar.Text.Trim()
        Dim sql As String = "SELECT * FROM MaeStk WHERE 1=1"
        Dim parametros As New List(Of Object)


        If Not String.IsNullOrEmpty(texto) Then
            sql &= " AND (Articulo LIKE @Articulo OR Descripcion LIKE @Descripcion)"
            parametros.AddRange(New Object() {"@Articulo", $"%{texto}%", "@Descripcion", $"%{texto.Trim()}%"})
        End If

        Dim tabla = DSM.ExecuteQuery(DSM.Stock, sql, CmdParams(parametros.ToArray()))
        DgvListado.DataSource = tabla

        ' si no hay resultados, limpiar la selección
        If tabla.Rows.Count = 0 Then
            filaActualIndice = -1
            filaActual = Nothing
            Return
        End If

        ' Si hay resultados, seleccionar la primera fila
        filaActualIndice = 0
        filaActual = DgvListado.Rows(0)

    End Sub

    Public Sub AceptarSeleccion()
        If filaActual IsNot Nothing Then
            Seleccion = Funciones.DataGridViewRowToDictionary(filaActual)
        End If
    End Sub
    Public Sub GridConfigurarColumnas()
        For Each col As DataGridViewColumn In DgvListado.Columns
            col.Visible = False
        Next

        DgvListado.Columns("Articulo").Visible = True
        DgvListado.Columns("Articulo").HeaderText = "Articulo"
        DgvListado.Columns("Articulo").Width = 90
        DgvListado.Columns("Articulo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DgvListado.Columns("Proveedor").Visible = True
        DgvListado.Columns("Proveedor").HeaderText = "Proveedor"
        DgvListado.Columns("Proveedor").Width = 100
        DgvListado.Columns("Proveedor").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        DgvListado.Columns("Descripcion").Visible = True
        DgvListado.Columns("Descripcion").HeaderText = "Descripción"
        DgvListado.Columns("Descripcion").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        DgvListado.Columns("Diametro").Visible = True
        DgvListado.Columns("Diametro").HeaderText = "Diámetro"
        DgvListado.Columns("Diametro").Width = 70
        DgvListado.Columns("Diametro").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        DgvListado.Columns("Seccion").Visible = True
        DgvListado.Columns("Seccion").HeaderText = "Sección"
        DgvListado.Columns("Seccion").Width = 50
        DgvListado.Columns("Seccion").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        DgvListado.Columns("Tipo").Visible = True
        DgvListado.Columns("Tipo").HeaderText = "Tipo"
        DgvListado.Columns("Tipo").Width = 50
        DgvListado.Columns("Tipo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        DgvListado.Columns("Llanta").Visible = True
        DgvListado.Columns("Llanta").HeaderText = "Llanta"
        DgvListado.Columns("Llanta").Width = 50
        DgvListado.Columns("Llanta").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        ConfigurarEstiloGrid(DgvListado)

        DgvListado.MultiSelect = False
        DgvListado.SelectionMode = DataGridViewSelectionMode.FullRowSelect

    End Sub

    Private Sub txtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
        gridbuscar()
    End Sub
End Class