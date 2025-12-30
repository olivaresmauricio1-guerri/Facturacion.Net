Imports DSM = DataSourceManager.Lib.DataSourceManager
Public Class frmClienteSelector
    Private tabla As New DataTable
    Private filaActual As DataGridViewRow
    Private filaActualIndice As Integer = -1
    Public Property Seleccion As Dictionary(Of String, Object)
    Private Sub frmClienteSelector_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        Dim sql As String = "SELECT * FROM MaeCtaCte WHERE 1=1 AND Estado <> 'Baja' "
        Dim parametros As New List(Of Object)


        If Not String.IsNullOrEmpty(texto) Then
            sql &= " AND (NroCuenta LIKE @NroCuenta OR Nombre LIKE @Nombre)"
            parametros.AddRange(New Object() {"@NroCuenta", $"%{texto}%", "@Nombre", $"%{texto.Trim()}%"})
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

        DgvListado.Columns("NroCuenta").Visible = True
        DgvListado.Columns("NroCuenta").HeaderText = "NroCuenta"
        DgvListado.Columns("NroCuenta").Width = 90
        DgvListado.Columns("NroCuenta").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DgvListado.Columns("Nombre").Visible = True
        DgvListado.Columns("Nombre").HeaderText = "Nombre"
        DgvListado.Columns("Nombre").Width = 100
        DgvListado.Columns("Nombre").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        DgvListado.Columns("CUIT").Visible = True
        DgvListado.Columns("CUIT").HeaderText = "C.U.I.T."
        DgvListado.Columns("CUIT").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter


        ConfigurarEstiloGrid(DgvListado)

        DgvListado.MultiSelect = False
        DgvListado.SelectionMode = DataGridViewSelectionMode.FullRowSelect

    End Sub

    Private Sub txtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
        gridbuscar()
    End Sub


End Class