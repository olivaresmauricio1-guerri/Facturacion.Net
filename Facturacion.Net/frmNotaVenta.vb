Imports System.Globalization
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmNotaVenta
    Private Shared instancia As frmNotaVenta = Nothing
    Private _datosCliente As ClienteFiscalData
    Private _items As DataTable

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmNotaVenta()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmNotaVenta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpFecha.Value = Date.Today

        General.CargarCombos(cmbCondicion, "CondicionVenta", "Descripcion", "Descripcion", "Codigo")
        General.CargarCombos(cmbExpreso, "Expresos", "Descripcion", "Descripcion", "IdExpreso")

        _items = New DataTable()
        _items.Columns.Add("Articulo", GetType(Integer))
        _items.Columns.Add("Cantidad", GetType(Double))
        _items.Columns.Add("Descripcion", GetType(String))
        _items.Columns.Add("ValorPU", GetType(Double))
        _items.Columns.Add("Descuento", GetType(Double))
        _items.Columns.Add("Total", GetType(Double))
        _items.Columns.Add("IdArticulo", GetType(Integer))
        _items.Columns.Add("Despacho", GetType(String))

        dgvItems.DataSource = _items
        ConfigurarGridItems()
        AsegurarFilaVaciaFinal(True)
        RecalcularTotalesPantalla()
    End Sub

    Private Sub ConfigurarGridItems()
        ConfigurarEstiloGrid(dgvItems)
        dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvItems.MultiSelect = False
        dgvItems.AllowUserToAddRows = False
        dgvItems.AllowUserToDeleteRows = False
        dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvItems.EditMode = DataGridViewEditMode.EditOnEnter

        For Each col As DataGridViewColumn In dgvItems.Columns
            col.Visible = False
        Next

        If dgvItems.Columns.Contains("Articulo") Then
            dgvItems.Columns("Articulo").Visible = True
            dgvItems.Columns("Articulo").HeaderText = "Artículo"
            dgvItems.Columns("Articulo").FillWeight = 12
            dgvItems.Columns("Articulo").ReadOnly = False
        End If

        If dgvItems.Columns.Contains("Cantidad") Then
            dgvItems.Columns("Cantidad").Visible = True
            dgvItems.Columns("Cantidad").HeaderText = "Cantidad"
            dgvItems.Columns("Cantidad").FillWeight = 10
        End If

        If dgvItems.Columns.Contains("Descripcion") Then
            dgvItems.Columns("Descripcion").Visible = True
            dgvItems.Columns("Descripcion").HeaderText = "Descripción"
            dgvItems.Columns("Descripcion").FillWeight = 40
            dgvItems.Columns("Descripcion").ReadOnly = True
        End If

        If dgvItems.Columns.Contains("ValorPU") Then
            dgvItems.Columns("ValorPU").Visible = True
            dgvItems.Columns("ValorPU").HeaderText = "Unitario"
            dgvItems.Columns("ValorPU").FillWeight = 12
            dgvItems.Columns("ValorPU").DefaultCellStyle.Format = "N2"
        End If

        If dgvItems.Columns.Contains("Descuento") Then
            dgvItems.Columns("Descuento").Visible = True
            dgvItems.Columns("Descuento").HeaderText = "% Desc"
            dgvItems.Columns("Descuento").FillWeight = 10
            dgvItems.Columns("Descuento").DefaultCellStyle.Format = "N2"
        End If

        If dgvItems.Columns.Contains("Total") Then
            dgvItems.Columns("Total").Visible = True
            dgvItems.Columns("Total").HeaderText = "Total"
            dgvItems.Columns("Total").FillWeight = 16
            dgvItems.Columns("Total").ReadOnly = True
            dgvItems.Columns("Total").DefaultCellStyle.Format = "N2"
        End If

        If dgvItems.Columns.Contains("Despacho") Then
            dgvItems.Columns("Despacho").Visible = True
            dgvItems.Columns("Despacho").HeaderText = "Despacho"
            dgvItems.Columns("Despacho").FillWeight = 15
        End If
    End Sub

    Private Sub cmdBuscarCliente_Click(sender As Object, e As EventArgs) Handles cmdBuscarCliente.Click
        Using f As New frmClienteSelector()
            If f.ShowDialog(Me) <> DialogResult.OK OrElse f.Seleccion Is Nothing Then
                Return
            End If

            Dim nroCuenta As Long = 0
            If f.Seleccion.ContainsKey("NroCuenta") AndAlso f.Seleccion("NroCuenta") IsNot Nothing Then
                nroCuenta = CLng(f.Seleccion("NroCuenta"))
            End If

            Dim err As String = ""
            Dim datos As ClienteFiscalData = Nothing
            If Not ObtenerDatosFiscalesCliente(nroCuenta, UsuarioActual, datos, err) Then
                MessageBox.Show(err, "Cliente", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            _datosCliente = datos
            txtNroCuenta.Text = datos.NroCuenta.ToString()
            txtRazonSocial.Text = datos.RazonSocial
            txtCuit.Text = datos.Cuit
            RecalcularTotalesPantalla()
        End Using
    End Sub

    Private Sub cmdAgregar_Click(sender As Object, e As EventArgs) Handles cmdAgregar.Click
        Using f As New frmArticuloSelector()
            If f.ShowDialog(Me) <> DialogResult.OK OrElse f.Seleccion Is Nothing Then
                Return
            End If

            Dim articulo As Integer = 0
            If f.Seleccion.ContainsKey("Articulo") AndAlso f.Seleccion("Articulo") IsNot Nothing Then
                articulo = Convert.ToInt32(f.Seleccion("Articulo"))
            End If
            If articulo <= 0 Then Return

            AsegurarFilaVaciaFinal()
            Dim rowIndex As Integer = _items.Rows.Count - 1

            Dim dupIdx As Integer = BuscarFilaArticulo(articulo, rowIndex)
            If dupIdx >= 0 Then
                Dim rowDup = _items.Rows(rowIndex)
                rowDup("Articulo") = articulo

                Dim errDup As String = ""
                If Not CargarDatosArticuloEnFila(rowDup, articulo, errDup) Then
                    MessageBox.Show(errDup, "Artículo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    LimpiarFila(rowDup)
                    RecalcularTotalesPantalla()
                    Return
                End If

                SetMergeTargetIndex(rowIndex, dupIdx)
                ActualizarTotalLinea(rowDup)
                RecalcularTotalesPantalla()
                MoverACelda(rowIndex, "Cantidad")
                Return
            End If

            Dim row = _items.Rows(rowIndex)
            row("Articulo") = articulo

            Dim err As String = ""
            If Not CargarDatosArticuloEnFila(row, articulo, err) Then
                MessageBox.Show(err, "Artículo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                LimpiarFila(row)
                RecalcularTotalesPantalla()
                Return
            End If

            ClearMergeTargetIndex(rowIndex)
            ActualizarTotalLinea(row)
            RecalcularTotalesPantalla()
            MoverACelda(rowIndex, "Cantidad")
        End Using
    End Sub

    Private Sub cmdQuitar_Click(sender As Object, e As EventArgs) Handles cmdQuitar.Click
        If dgvItems.SelectedRows.Count = 0 Then Return
        Dim idx As Integer = dgvItems.SelectedRows(0).Index
        If idx < 0 OrElse idx >= _items.Rows.Count Then Return
        If idx = _items.Rows.Count - 1 AndAlso FilaEstaVacia(_items.Rows(idx)) Then Return
        _items.Rows.RemoveAt(idx)
        AsegurarFilaVaciaFinal()
        RecalcularTotalesPantalla()
    End Sub

    Private Sub dgvItems_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvItems.CellEndEdit
        If e.RowIndex < 0 OrElse e.RowIndex >= _items.Rows.Count Then Return

        Dim colName As String = dgvItems.Columns(e.ColumnIndex).Name
        Dim r As DataRow = _items.Rows(e.RowIndex)

        If String.Equals(colName, "Articulo", StringComparison.OrdinalIgnoreCase) Then
            Dim articulo As Integer = 0
            If r.IsNull("Articulo") OrElse Not Integer.TryParse(r("Articulo").ToString(), articulo) OrElse articulo <= 0 Then
                LimpiarFila(r)
                ClearMergeTargetIndex(e.RowIndex)
                RecalcularTotalesPantalla()
                Return
            End If

            Dim err As String = ""
            If Not CargarDatosArticuloEnFila(r, articulo, err) Then
                MessageBox.Show(err, "Artículo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                LimpiarFila(r)
                ClearMergeTargetIndex(e.RowIndex)
                RecalcularTotalesPantalla()
                MoverACelda(e.RowIndex, "Articulo")
                Return
            End If

            Dim dupIdx As Integer = BuscarFilaArticulo(articulo, e.RowIndex)
            If dupIdx >= 0 Then
                SetMergeTargetIndex(e.RowIndex, dupIdx)
            Else
                ClearMergeTargetIndex(e.RowIndex)
            End If

            ActualizarTotalLinea(r)
            RecalcularTotalesPantalla()
            MoverACelda(e.RowIndex, "Cantidad")
            Return
        End If

        If String.Equals(colName, "Cantidad", StringComparison.OrdinalIgnoreCase) OrElse
           String.Equals(colName, "ValorPU", StringComparison.OrdinalIgnoreCase) OrElse
           String.Equals(colName, "Descuento", StringComparison.OrdinalIgnoreCase) Then

            ActualizarTotalLinea(r)
            RecalcularTotalesPantalla()
        End If

        If String.Equals(colName, "Cantidad", StringComparison.OrdinalIgnoreCase) Then
            Dim mergeTarget As Integer = GetMergeTargetIndex(e.RowIndex)
            If mergeTarget >= 0 Then
                Dim addQty As Double = ObtenerCantidad(r)
                If addQty <= 0 Then
                    MoverACelda(e.RowIndex, "Cantidad")
                    Return
                End If

                SumarCantidadEnFila(mergeTarget, addQty)
                LimpiarFila(r)
                ClearMergeTargetIndex(e.RowIndex)
                AsegurarFilaVaciaFinal()
                RecalcularTotalesPantalla()
                MoverACelda(_items.Rows.Count - 1, "Articulo")
                Return
            End If

            MoverACelda(e.RowIndex, "ValorPU")
            Return
        End If

        If String.Equals(colName, "ValorPU", StringComparison.OrdinalIgnoreCase) Then
            MoverACelda(e.RowIndex, "Descuento")
            Return
        End If

        If String.Equals(colName, "Descuento", StringComparison.OrdinalIgnoreCase) OrElse
           String.Equals(colName, "Despacho", StringComparison.OrdinalIgnoreCase) Then
            AsegurarFilaVaciaFinal()
            MoverACelda(_items.Rows.Count - 1, "Articulo")
        End If
    End Sub

    Private Sub dgvItems_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvItems.KeyDown
        If e.KeyCode <> Keys.Enter Then Return
        If dgvItems.CurrentCell Is Nothing Then Return

        e.Handled = True
        e.SuppressKeyPress = True
        dgvItems.EndEdit()
    End Sub

    Private Sub dgvItems_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dgvItems.DataError
        e.ThrowException = False
    End Sub

    Private Sub AsegurarFilaVaciaFinal(Optional enfocar As Boolean = False)
        If _items Is Nothing Then Return

        If _items.Rows.Count = 0 OrElse Not FilaEstaVacia(_items.Rows(_items.Rows.Count - 1)) Then
            Dim r = _items.NewRow()
            r("Articulo") = DBNull.Value
            r("Cantidad") = DBNull.Value
            r("Descripcion") = ""
            r("ValorPU") = DBNull.Value
            r("Descuento") = DBNull.Value
            r("Total") = DBNull.Value
            r("IdArticulo") = DBNull.Value
            r("Despacho") = ""
            _items.Rows.Add(r)
        End If

        If enfocar AndAlso dgvItems IsNot Nothing AndAlso _items.Rows.Count > 0 Then
            Dim idx As Integer = _items.Rows.Count - 1
            MoverACelda(idx, "Articulo")
        End If
    End Sub

    Private Function FilaEstaVacia(r As DataRow) As Boolean
        If r Is Nothing Then Return True

        Dim art As Integer = 0
        If Not r.IsNull("Articulo") Then
            Integer.TryParse(r("Articulo").ToString(), art)
        End If

        Dim desc As String = ""
        If Not r.IsNull("Descripcion") Then desc = r("Descripcion").ToString()

        Dim cant As Double = 0
        Dim pu As Double = 0
        Dim dto As Double = 0

        If Not r.IsNull("Cantidad") Then Double.TryParse(r("Cantidad").ToString(), NumberStyles.Any, CultureInfo.CurrentCulture, cant)
        If Not r.IsNull("ValorPU") Then Double.TryParse(r("ValorPU").ToString(), NumberStyles.Any, CultureInfo.CurrentCulture, pu)
        If Not r.IsNull("Descuento") Then Double.TryParse(r("Descuento").ToString(), NumberStyles.Any, CultureInfo.CurrentCulture, dto)

        If art <> 0 Then Return False
        If Not String.IsNullOrWhiteSpace(desc) Then Return False
        If cant <> 0 OrElse pu <> 0 OrElse dto <> 0 Then Return False

        Dim despacho As String = ""
        If Not r.IsNull("Despacho") Then despacho = r("Despacho").ToString()
        If Not String.IsNullOrWhiteSpace(despacho) Then Return False

        Return True
    End Function

    Private Sub LimpiarFila(r As DataRow)
        If r Is Nothing Then Return
        r("Articulo") = DBNull.Value
        r("Cantidad") = DBNull.Value
        r("Descripcion") = ""
        r("ValorPU") = DBNull.Value
        r("Descuento") = DBNull.Value
        r("Total") = DBNull.Value
        r("IdArticulo") = DBNull.Value
        r("Despacho") = ""
    End Sub

    Private Sub SetMergeTargetIndex(rowIndex As Integer, targetIndex As Integer)
        If dgvItems Is Nothing OrElse rowIndex < 0 OrElse rowIndex >= dgvItems.Rows.Count Then Return
        dgvItems.Rows(rowIndex).Tag = targetIndex
    End Sub

    Private Function GetMergeTargetIndex(rowIndex As Integer) As Integer
        If dgvItems Is Nothing OrElse rowIndex < 0 OrElse rowIndex >= dgvItems.Rows.Count Then Return -1
        Dim tagVal = dgvItems.Rows(rowIndex).Tag
        If tagVal Is Nothing Then Return -1
        If TypeOf tagVal Is Integer Then Return CInt(tagVal)
        Dim parsed As Integer = -1
        If Integer.TryParse(tagVal.ToString(), parsed) Then Return parsed
        Return -1
    End Function

    Private Sub ClearMergeTargetIndex(rowIndex As Integer)
        If dgvItems Is Nothing OrElse rowIndex < 0 OrElse rowIndex >= dgvItems.Rows.Count Then Return
        dgvItems.Rows(rowIndex).Tag = Nothing
    End Sub

    Private Function BuscarFilaArticulo(articulo As Integer, excludeRowIndex As Integer) As Integer
        If _items Is Nothing Then Return -1

        For i As Integer = 0 To _items.Rows.Count - 1
            If i = excludeRowIndex Then Continue For

            Dim row = _items.Rows(i)
            If row Is Nothing OrElse row.IsNull("Articulo") Then Continue For

            Dim artRow As Integer = 0
            If Integer.TryParse(row("Articulo").ToString(), artRow) AndAlso artRow = articulo Then
                Return i
            End If
        Next

        Return -1
    End Function

    Private Function ObtenerCantidad(r As DataRow) As Double
        If r Is Nothing OrElse r.IsNull("Cantidad") Then Return 0
        Dim cant As Double = 0
        Double.TryParse(r("Cantidad").ToString(), NumberStyles.Any, CultureInfo.CurrentCulture, cant)
        Return cant
    End Function

    Private Sub SumarCantidadEnFila(rowIndex As Integer, addQty As Double)
        If _items Is Nothing Then Return
        If rowIndex < 0 OrElse rowIndex >= _items.Rows.Count Then Return
        If addQty = 0 Then Return

        Dim r As DataRow = _items.Rows(rowIndex)
        Dim currentQty As Double = ObtenerCantidad(r)
        r("Cantidad") = currentQty + addQty
        ActualizarTotalLinea(r)
    End Sub

    Private Function CargarDatosArticuloEnFila(r As DataRow, articulo As Integer, ByRef err As String) As Boolean
        err = ""
        Try
            Dim dt As DataTable = DSM.ExecuteQuery(
                DSM.Stock,
                "SELECT TOP 1 IdArticulo, Descripcion, Publico FROM MaeStk WHERE Articulo = @Articulo",
                CmdParams("@Articulo", articulo)
            )

            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                err = "Artículo inexistente."
                Return False
            End If

            Dim mae As DataRow = dt.Rows(0)
            Dim idArticulo As Integer = If(IsDBNull(mae("IdArticulo")), 0, Convert.ToInt32(mae("IdArticulo")))
            Dim descripcion As String = If(IsDBNull(mae("Descripcion")), "", mae("Descripcion").ToString())

            r("IdArticulo") = idArticulo
            r("Descripcion") = descripcion

            If r.IsNull("Cantidad") OrElse Convert.ToDouble(If(r("Cantidad"), 0)) = 0 Then
                r("Cantidad") = 1
            End If

            If r.IsNull("Descuento") Then
                r("Descuento") = 0
            End If

            If r.IsNull("ValorPU") OrElse Convert.ToDouble(If(r("ValorPU"), 0)) = 0 Then
                If dt.Columns.Contains("Publico") AndAlso Not IsDBNull(mae("Publico")) Then
                    Dim publico As Double = 0
                    Double.TryParse(mae("Publico").ToString(), NumberStyles.Any, CultureInfo.CurrentCulture, publico)
                    If publico > 0 Then
                        r("ValorPU") = publico
                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            err = "Error al buscar artículo: " & ex.Message
            Return False
        End Try
    End Function

    Private Sub ActualizarTotalLinea(r As DataRow)
        If r Is Nothing OrElse r.IsNull("Articulo") Then
            r("Total") = DBNull.Value
            Return
        End If

        Dim cantidad As Double = 0
        Dim valorPU As Double = 0
        Dim descuento As Double = 0

        If Not r.IsNull("Cantidad") Then Double.TryParse(r("Cantidad").ToString(), NumberStyles.Any, CultureInfo.CurrentCulture, cantidad)
        If Not r.IsNull("ValorPU") Then Double.TryParse(r("ValorPU").ToString(), NumberStyles.Any, CultureInfo.CurrentCulture, valorPU)
        If Not r.IsNull("Descuento") Then Double.TryParse(r("Descuento").ToString(), NumberStyles.Any, CultureInfo.CurrentCulture, descuento)

        If cantidad = 0 AndAlso valorPU = 0 Then
            r("Total") = DBNull.Value
            Return
        End If

        r("Total") = CalcularTotalLinea(cantidad, valorPU, descuento)
    End Sub

    Private Sub MoverACelda(rowIndex As Integer, columnName As String)
        If dgvItems Is Nothing OrElse dgvItems.IsDisposed Then Return

        BeginInvoke(
            Sub()
                If dgvItems Is Nothing OrElse dgvItems.IsDisposed Then Return
                If rowIndex < 0 OrElse rowIndex >= dgvItems.Rows.Count Then Return
                If Not dgvItems.Columns.Contains(columnName) Then Return
                If Not dgvItems.Visible OrElse Not dgvItems.Enabled Then Return

                dgvItems.CurrentCell = dgvItems.Rows(rowIndex).Cells(columnName)
                dgvItems.BeginEdit(True)
            End Sub
        )
    End Sub

    Private Function CalcularTotalLinea(ByVal cantidad As Double, ByVal valorPU As Double, ByVal descuento As Double) As Double
        Dim base As Double = cantidad * valorPU
        If descuento > 0 Then
            base = base * (1 - (descuento / 100))
        End If
        Return Math.Round(base, 2)
    End Function

    Private Sub RecalcularTotalesPantalla()
        Dim totalBase As Double = 0
        For Each r As DataRow In _items.Rows
            If r IsNot Nothing AndAlso Not IsDBNull(r("Total")) Then
                totalBase += Convert.ToDouble(r("Total"))
            End If
        Next

        Dim totG As Double = 0
        Dim totNI As Double = 0
        Dim totI As Double = 0
        Dim totIB As Double = 0

        If _datosCliente IsNot Nothing AndAlso _datosCliente.PorcIva <> 0 Then
            totG = totalBase / _datosCliente.PorcIva
        End If
        If _datosCliente IsNot Nothing AndAlso _datosCliente.PorcNoInscripto <> 0 Then
            totNI = (totG * _datosCliente.PorcNoInscripto) / 100
        End If
        totI = totalBase - totG

        If _datosCliente IsNot Nothing AndAlso _datosCliente.IIBBPorProvincia IsNot Nothing AndAlso Not String.Equals(UsuarioActual, "PEDRO", StringComparison.OrdinalIgnoreCase) Then
            For i As Integer = 1 To 24
                If _datosCliente.IIBBPorProvincia.Length > i AndAlso _datosCliente.IIBBPorProvincia(i) > 0 Then
                    totIB += totG * _datosCliente.IIBBPorProvincia(i)
                End If
            Next
        End If

        txtTotG.Text = Math.Round(totG, 2).ToString("N2")
        txtTotI.Text = Math.Round(totI, 2).ToString("N2")
        txtTotNI.Text = Math.Round(totNI, 2).ToString("N2")
        txtTotIB.Text = Math.Round(totIB, 2).ToString("N2")
        txtTotalFinal.Text = Math.Round(totG + totNI + totI + totIB, 2).ToString("N2")
    End Sub

    Private Sub cmdCancelar_Click(sender As Object, e As EventArgs) Handles cmdCancelar.Click
        If MessageBox.Show("¿Desea cancelar la operación?", "Cancelar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return

        If _datosCliente IsNot Nothing AndAlso General.propio > 0 Then
            EliminarTemporales(0, _datosCliente.NroCuenta, Convert.ToInt32(PuntoVentaActual), _datosCliente.Tipocomp, CInt(General.propio))
        End If

        General.propio = 0
        _items.Rows.Clear()
        RecalcularTotalesPantalla()
    End Sub

    Private Sub cmdFinalizar_Click(sender As Object, e As EventArgs) Handles cmdFinalizar.Click
        If _datosCliente Is Nothing Then
            MessageBox.Show("Debe seleccionar un cliente.", "Nota de Venta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        If _items.Rows.Count = 0 Then
            MessageBox.Show("Debe cargar al menos un ítem.", "Nota de Venta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        If MessageBox.Show("¿Confirma la emisión?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Return
        End If

        cmdFinalizar.Enabled = False
        cmdCancelar.Enabled = False

        Try
            EnsurePropio()

            Dim dtPV As DataTable = DSM.ExecuteQuery(DSM.Stock, "SELECT * FROM Puntosdeventa WHERE puntoventa = " & PuntoVentaActual)
            If dtPV Is Nothing OrElse dtPV.Rows.Count <= 0 Then
                MessageBox.Show("ERROR EN PUNTO DE VENTA", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            PruebaElec = Convert.ToBoolean(dtPV.Rows(0)("PruebaElec"))

            Dim idImputa As Integer
            If String.Equals(UsuarioActual, "PEDRO", StringComparison.OrdinalIgnoreCase) Then
                idImputa = 57
            Else
                idImputa = 1
            End If

            Dim sqlNroCpte As String
            If idImputa = 57 Then
                sqlNroCpte = "SELECT * FROM NumerosComprobantes WHERE ImputaStk = 57 "
            Else
                sqlNroCpte = "SELECT * FROM NumerosComprobantes WHERE ImputaCC = 1 "
            End If
            sqlNroCpte &= " AND idPunto = " & PuntoVentaActual
            Dim dtNroCpte As DataTable = DSM.ExecuteQuery(DSM.Stock, sqlNroCpte)
            If dtNroCpte Is Nothing OrElse dtNroCpte.Rows.Count <= 0 Then
                MessageBox.Show("ERROR AL OBTENER NUMERO DE COMPROBANTE. PUNTO DE VENTA INEXISTENTE", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim nroNotaVenta As Long = 0
            Dim nroFactura As Long = 0

            If idImputa = 57 Then
                Dim nroComprobC = Convert.ToInt32(dtNroCpte.Rows(0)("NroComprobC"))
                nroNotaVenta = nroComprobC + 1
            Else
                Select Case _datosCliente.TipoIva
                    Case 7
                        nroFactura = Convert.ToInt64(dtNroCpte.Rows(0)("NroComprobE")) + 1
                    Case 1, 2
                        nroFactura = Convert.ToInt64(dtNroCpte.Rows(0)("NroComprobA")) + 1
                    Case 2 To 6
                        nroFactura = Convert.ToInt64(dtNroCpte.Rows(0)("NroComprobB")) + 1
                End Select
            End If

            If Not NetworkHelper.VerificarInternet() Then Return

            Dim nroCompReal As Long = If(idImputa = 57, nroNotaVenta, nroFactura)
            Dim nroUnificado As String = UnificarNro(Convert.ToInt32(PuntoVentaActual), nroCompReal)

            Dim idCondicion As Integer = 1
            If cmbCondicion.SelectedValue IsNot Nothing AndAlso IsNumeric(cmbCondicion.SelectedValue) Then
                idCondicion = Convert.ToInt32(cmbCondicion.SelectedValue)
            End If

            DSM.Execute(DSM.Stock, "DELETE FROM Facturas WHERE IdPropio = @IdPropio", CmdParams("@IdPropio", General.propio))

            For Each r As DataRow In _items.Rows
                Dim articulo As Integer = Convert.ToInt32(r("Articulo"))
                Dim cantidad As Double = Convert.ToDouble(r("Cantidad"))
                Dim valorPU As Double = Convert.ToDouble(r("ValorPU"))
                Dim descuento As Double = Convert.ToDouble(r("Descuento"))
                Dim totalLinea As Double = Convert.ToDouble(r("Total"))
                Dim despacho As String = If(r("Despacho") Is DBNull.Value, "", r("Despacho").ToString())

                Dim neto As Double = 0
                If _datosCliente.PorcIva <> 0 Then
                    neto = totalLinea / _datosCliente.PorcIva
                End If
                Dim ivaRI As Double = totalLinea - neto

                Dim ivaRNI As Double = 0
                If _datosCliente.PorcNoInscripto <> 0 Then
                    ivaRNI = totalLinea * _datosCliente.PorcNoInscripto / 100
                End If

                Dim sumaIB As Double = 0
                If _datosCliente.IIBBPorProvincia IsNot Nothing AndAlso Not String.Equals(UsuarioActual, "PEDRO", StringComparison.OrdinalIgnoreCase) Then
                    For i As Integer = 1 To 24
                        If _datosCliente.IIBBPorProvincia.Length > i AndAlso _datosCliente.IIBBPorProvincia(i) <> 0 Then
                            sumaIB += totalLinea * _datosCliente.IIBBPorProvincia(i)
                        End If
                    Next
                End If

                Dim sqlInsertFactura As String =
                    "INSERT INTO Facturas (" &
                    "IdPropio, IdEmpresa, idctacte, idsucursal, IdTarjeta, " &
                    "IDCONDICION, IdTipoVenta, Articulo, IdArticulo, cantidad, " &
                    "DESCRIPCION, Despacho, VALORPU, descuento, " &
                    "Neto, IvaRI, IvaRNI, ingbrutos, Total, fecha" &
                    ") VALUES (" &
                    "@IdPropio, @IdEmpresa, @IdCtaCte, @IdSucursal, @IdTarjeta, " &
                    "@IdCondicion, @IdTipoVenta, @Articulo, @IdArticulo, @Cantidad, " &
                    "@Descripcion, @Despacho, @ValorPU, @Descuento, " &
                    "@Neto, @IvaRI, @IvaRNI, @IngBrutos, @Total, @Fecha" &
                    ")"

                Dim parsInsertFactura = CmdParams(
                    "@IdPropio", General.propio,
                    "@IdEmpresa", 1,
                    "@IdCtaCte", _datosCliente.NroCuenta,
                    "@IdSucursal", _datosCliente.IdSucursal,
                    "@IdTarjeta", 0,
                    "@IdCondicion", idCondicion,
                    "@IdTipoVenta", 2,
                    "@Articulo", articulo,
                    "@IdArticulo", If(IsDBNull(r("IdArticulo")), 0, Convert.ToInt32(r("IdArticulo"))),
                    "@Cantidad", cantidad,
                    "@Descripcion", r("Descripcion").ToString(),
                    "@Despacho", despacho,
                    "@ValorPU", valorPU,
                    "@Descuento", descuento,
                    "@Neto", neto,
                    "@IvaRI", ivaRI,
                    "@IvaRNI", ivaRNI,
                    "@IngBrutos", sumaIB,
                    "@Total", totalLinea,
                    "@Fecha", dtpFecha.Value.Date
                )

                DSM.Execute(DSM.Stock, sqlInsertFactura, parsInsertFactura)
            Next

            Dim Total = Totales(
                idPropio:=General.propio,
                porcIva:=_datosCliente.PorcIva,
                porcNI:=_datosCliente.PorcNoInscripto,
                usuario:=UsuarioActual,
                tablaIbrutos:=_datosCliente.IIBBPorProvincia,
                nroCuenta:=_datosCliente.NroCuenta,
                nroFactura:=nroCompReal,
                puntoDeVenta:=Convert.ToInt32(PuntoVentaActual),
                tipocomp:=_datosCliente.Tipocomp
            )

            Dim sqlLeyendas As String =
                "INSERT INTO Leyendas (IdPropio, Total1, Total2, Total3, Total4, Total5, NroComprobante, TipoComprobante, EXPRESO, flete) " &
                "VALUES (@IdPropio, @TotG, @TotIB, @TotI, @TotNI, @Total, @NroComprobante, @TipoComprobante, @Expreso, @Flete)"

            Dim parsLeyendas = CmdParams(
                "@IdPropio", General.propio,
                "@TotG", Total.TotG,
                "@TotIB", Total.TotIB,
                "@TotI", Total.TotI,
                "@TotNI", Total.TotNI,
                "@Total", Total.TotalFinal,
                "@NroComprobante", nroUnificado,
                "@TipoComprobante", If(cmbCondicion.Text, ""),
                "@Expreso", If(cmbExpreso.Text, ""),
                "@Flete", If(txtFlete.Text, "")
            )
            DSM.Execute(DSM.Stock, sqlLeyendas, parsLeyendas)

            Dim resultadoFe As FEResultadoProceso = Nothing

            If dtPV.Rows(0)("EsElectronica").Equals(True) AndAlso Not String.Equals(UsuarioActual, "PEDRO", StringComparison.OrdinalIgnoreCase) Then
                If FacturacionElectronica.EmisorComprobante Is Nothing Then
                    FacturacionElectronica.ConfigurarEmisorAfipWsfe()
                End If

                Dim solicitud As New FESolicitudComprobante With {
                    .IdPropio = General.propio,
                    .PuntoVenta = Convert.ToInt32(PuntoVentaActual),
                    .EsPrueba = Convert.ToBoolean(dtPV.Rows(0)("PruebaElec")),
                    .TipoComprobante = _datosCliente.Tipocomp,
                    .EsFce = False,
                    .NroComprobante = nroFactura,
                    .NroCuenta = _datosCliente.NroCuenta,
                    .Cliente = _datosCliente,
                    .Totales = Total,
                    .Articulos = Nothing
                }

                resultadoFe = FacturacionElectronica.ProcesarComprobanteElectronico(solicitud)

                If Not resultadoFe.Exito Then
                    MessageBox.Show("Error al emitir factura electrónica: " & resultadoFe.Mensaje, "Error de Facturación Electrónica", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    EliminarTemporales(nroCompReal, _datosCliente.NroCuenta, Convert.ToInt32(PuntoVentaActual), _datosCliente.Tipocomp, CInt(General.propio))
                    Return
                End If
            End If

            Dim dtFacturas As DataTable = DSM.ExecuteQuery(DSM.Stock, "SELECT * FROM Facturas WHERE IdPropio = @IdPropio", CmdParams("@IdPropio", General.propio))
            If dtFacturas Is Nothing OrElse dtFacturas.Rows.Count = 0 Then
                MessageBox.Show("No se encontraron ítems para emitir.", "Nota de Venta", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim dtViajante As DataTable = DSM.ExecuteQuery(DSM.Stock, "Select * FROM Viajantes WHERE codigo = " & _datosCliente.idVendedor)
            Dim vendedor As String = If(dtViajante Is Nothing OrElse dtViajante.Rows.Count = 0, "", dtViajante.Rows(0)("Descripcion").ToString())

            Dim nroFacturaParaNove As Long = nroFactura
            Dim anterior As Boolean = False
            If String.Equals(UsuarioActual, "PEDRO", StringComparison.OrdinalIgnoreCase) Then
                nroFacturaParaNove = 0
                anterior = True
            End If

            Dim sqlNoveStk As String =
                "INSERT INTO NoveStk (" &
                "IdArticulo, proveedor, Articulo, idsucursal, IdComprob, NroComprobante, fecha, cantidad, valorc, " &
                "Tipoventa, VALORPU, bonificacion, Importe, idctacte, NroCuenta, Viajante, CONDICIONVENTA, factura, " &
                "MesAnterior, Cancelado, EXPRESO, Despacho, puntodeventa, Nropedido" &
                ") VALUES (" &
                "@IdArticulo, @proveedor, @Articulo, @idsucursal, @IdComprob, @NroComprobante, @fecha, @cantidad, @valorc, " &
                "@Tipoventa, @VALORPU, @bonificacion, @Importe, @idctacte, @NroCuenta, @Viajante, @CONDICIONVENTA, @factura, " &
                "@MesAnterior, @Cancelado, @EXPRESO, @Despacho, @puntodeventa, @Nropedido" &
                ")"

            For Each item As DataRow In dtFacturas.Rows
                Dim dtMaeStk As DataTable = DSM.ExecuteQuery(DSM.Stock, "Select * FROM MaeStk WHERE Articulo = " & item("Articulo"))
                Dim proveedor As Object = If(dtMaeStk Is Nothing OrElse dtMaeStk.Rows.Count = 0, 0, dtMaeStk.Rows(0)("Proveedor"))

                Dim valorc As Double = 0
                If dtMaeStk IsNot Nothing AndAlso dtMaeStk.Rows.Count > 0 Then
                    valorc = Convert.ToDouble(dtMaeStk.Rows(0)("FOB")) * Convert.ToDouble(item("Cantidad"))
                End If

                Dim valorPU As Double = 0
                If item.Table.Columns.Contains("VALORPU") AndAlso Not IsDBNull(item("VALORPU")) Then
                    valorPU = Convert.ToDouble(item("VALORPU"))
                ElseIf item.Table.Columns.Contains("valorPU") AndAlso Not IsDBNull(item("valorPU")) Then
                    valorPU = Convert.ToDouble(item("valorPU"))
                End If

                Dim bonifPct As Double = 0
                If item.Table.Columns.Contains("bonificacion") AndAlso Not IsDBNull(item("bonificacion")) Then
                    bonifPct = Convert.ToDouble(item("bonificacion"))
                ElseIf item.Table.Columns.Contains("descuento") AndAlso Not IsDBNull(item("descuento")) Then
                    bonifPct = Convert.ToDouble(item("descuento"))
                End If

                Dim parsNoveStk = CmdParams(
                    "@IdArticulo", Convert.ToInt32(item("IdArticulo")),
                    "@proveedor", proveedor,
                    "@Articulo", item("Articulo"),
                    "@idsucursal", Convert.ToInt32(item("idSucursal")),
                    "@IdComprob", 57,
                    "@NroComprobante", nroCompReal,
                    "@fecha", dtpFecha.Value.Date,
                    "@cantidad", Convert.ToDouble(item("Cantidad")) * -1,
                    "@valorc", valorc,
                    "@Tipoventa", "Cuenta Corriente",
                    "@VALORPU", valorPU,
                    "@bonificacion", (Convert.ToDouble(item("Total")) * bonifPct) / 100,
                    "@Importe", Convert.ToDouble(item("Total")),
                    "@idctacte", SucursalActual,
                    "@NroCuenta", _datosCliente.NroCuenta,
                    "@Viajante", vendedor,
                    "@CONDICIONVENTA", cmbCondicion.Text,
                    "@factura", nroFacturaParaNove,
                    "@MesAnterior", False,
                    "@Cancelado", False,
                    "@EXPRESO", If(cmbExpreso.Text, ""),
                    "@Despacho", item("Despacho"),
                    "@puntodeventa", PuntoVentaActual,
                    "@Nropedido", 0
                )
                DSM.Execute(DSM.Stock, sqlNoveStk, parsNoveStk)
            Next

            Dim nroFac As Long = If(String.Equals(UsuarioActual, "PEDRO", StringComparison.OrdinalIgnoreCase), nroNotaVenta, nroFactura)
            Dim codAfip As Integer = If(resultadoFe IsNot Nothing AndAlso resultadoFe.Exito, resultadoFe.CodigoAfip, _datosCliente.Tipocomp)

            Dim sqlUpsertNoveCC As String =
                "IF EXISTS (" &
                "   SELECT 1 FROM NoveCtaCte " &
                "   WHERE NroCuenta = @NroCuenta AND Puntodeventa = @Puntodeventa AND NroComprobante = @NroComprobante AND Codigoafip = @Codigoafip AND idIMputacion = @idIMputacion" &
                ") " &
                "BEGIN " &
                "   UPDATE NoveCtaCte SET " &
                "       idCtaCte = @idCtaCte, " &
                "       NroFactura = @NroFactura, " &
                "       NombreComprobante = @NombreComprobante, " &
                "       TipoVenta = @TipoVenta, " &
                "       Condicion = @Condicion, " &
                "       Valoriza = @Valoriza, " &
                "       VentaDiaria = @VentaDiaria, " &
                "       Fecha = @Fecha, " &
                "       Monto = @Monto, " &
                "       IInterno = @IInterno, " &
                "       IBrutos = @IBrutos, " &
                "       Acuenta = @Acuenta, " &
                "       TipoValor = @TipoValor, " &
                "       Banco = @Banco, " &
                "       LocalidadCP = @LocalidadCP, " &
                "       NroCheque = @NroCheque, " &
                "       Reginterno = @Reginterno, " &
                "       Sucursal = @Sucursal, " &
                "       TipoBaja = @TipoBaja, " &
                "       Cobrado = @Cobrado, " &
                "       Anterior = @Anterior, " &
                "       IvaRI = @IvaRI, " &
                "       IvaRNI = @IvaRNI, " &
                "       Neto = @Neto, " &
                "       CtaAgip = @CtaAgip, " &
                "       Exento = @Exento, " &
                "       Cae = @Cae, " &
                "       Vencimiento = @Vencimiento " &
                "   WHERE NroCuenta = @NroCuenta AND Puntodeventa = @Puntodeventa AND NroComprobante = @NroComprobante AND Codigoafip = @Codigoafip AND idIMputacion = @idIMputacion; " &
                "END " &
                "ELSE " &
                "BEGIN " &
                "   INSERT INTO NoveCtaCte (idCtaCte, NroCuenta, Puntodeventa, NroFactura, NroComprobante, NombreComprobante, TipoVenta, Condicion, Valoriza, " &
                "       VentaDiaria, Fecha, idIMputacion, Monto, IInterno, IBrutos, Acuenta, TipoValor, Banco, LocalidadCP, NroCheque, Reginterno, Sucursal, TipoBaja, " &
                "       Cobrado, Anterior, IvaRI, IvaRNI, Neto, CtaAgip, Exento, Cae, Vencimiento, Codigoafip) " &
                "   VALUES (@idCtaCte, @NroCuenta, @Puntodeventa, @NroFactura, @NroComprobante, @NombreComprobante, @TipoVenta, @Condicion, @Valoriza, " &
                "       @VentaDiaria, @Fecha, @idIMputacion, @Monto, @IInterno, @IBrutos, @Acuenta, @TipoValor, @Banco, @LocalidadCP, @NroCheque, @Reginterno, @Sucursal, @TipoBaja, " &
                "       @Cobrado, @Anterior, @IvaRI, @IvaRNI, @Neto, @CtaAgip, @Exento, @Cae, @Vencimiento, @Codigoafip); " &
                "END"

            Dim parsNoveCC = CmdParams(
                "@idCtaCte", _datosCliente.IdCtaCte,
                "@NroCuenta", _datosCliente.NroCuenta,
                "@Puntodeventa", PuntoVentaActual,
                "@NroFactura", nroFac,
                "@NroComprobante", nroFac,
                "@NombreComprobante", "Factura",
                "@TipoVenta", cmbCondicion.Text,
                "@Condicion", cmbCondicion.Text,
                "@Valoriza", dtpFecha.Value.Date,
                "@VentaDiaria", True,
                "@Fecha", dtpFecha.Value.Date,
                "@idIMputacion", 1,
                "@Monto", Total.TotalFinal,
                "@IInterno", 0,
                "@IBrutos", Total.TotIB,
                "@Acuenta", 0,
                "@TipoValor", 0,
                "@Banco", 0,
                "@LocalidadCP", 0,
                "@NroCheque", 0,
                "@Reginterno", 0,
                "@Sucursal", DescripcionSucursal,
                "@TipoBaja", 0,
                "@Cobrado", False,
                "@Anterior", anterior,
                "@IvaRI", Total.TotI,
                "@IvaRNI", Total.TotNI,
                "@Neto", Total.TotG,
                "@CtaAgip", False,
                "@Exento", False,
                "@Cae", If(resultadoFe Is Nothing, "", resultadoFe.Cae),
                "@Vencimiento", If(resultadoFe Is Nothing, "", resultadoFe.Venicimiento),
                "@Codigoafip", codAfip
            )
            DSM.Execute(DSM.Stock, sqlUpsertNoveCC, parsNoveCC)

            Funciones.GenerarPdfFactura(
                nroCuenta:=_datosCliente.NroCuenta,
                puntoVenta:=Convert.ToInt32(PuntoVentaActual),
                nroComprobante:=nroCompReal,
                codigoAfip:=codAfip,
                datosCliente:=_datosCliente,
                totales:=Total,
                dtItems:=dtFacturas,
                cae:=If(resultadoFe Is Nothing, "", resultadoFe.Cae),
                vencimientoCae:=If(resultadoFe Is Nothing, "", resultadoFe.Venicimiento),
                vendedor:=vendedor,
                nroPedido:=0,
                esPresupuesto:=String.Equals(UsuarioActual, "PEDRO", StringComparison.OrdinalIgnoreCase)
            )

            If idImputa = 57 Then
                DSM.Execute(
                    DSM.Stock,
                    "UPDATE NumerosComprobantes Set NroComprobC = NroComprobC + 1 WHERE ImputaStk = @Imputa And idPunto = @IdPunto",
                    CmdParams("@Imputa", idImputa, "@IdPunto", PuntoVentaActual)
                )
            Else
                Select Case _datosCliente.TipoIva
                    Case 7
                        DSM.Execute(
                            DSM.Stock,
                            "UPDATE NumerosComprobantes Set NroComprobE = NroComprobE + 1 WHERE ImputaCC = 1 And idPunto = @IdPunto",
                            CmdParams("@IdPunto", PuntoVentaActual)
                        )
                    Case 1, 6
                        DSM.Execute(
                            DSM.Stock,
                            "UPDATE NumerosComprobantes Set NroComprobA = NroComprobA + 1 WHERE ImputaCC = 1 And idPunto = @IdPunto",
                            CmdParams("@IdPunto", PuntoVentaActual)
                        )
                    Case 2 To 5
                        DSM.Execute(
                            DSM.Stock,
                            "UPDATE NumerosComprobantes Set NroComprobB = NroComprobB + 1 WHERE ImputaCC = 1 And idPunto = @IdPunto",
                            CmdParams("@IdPunto", PuntoVentaActual)
                        )
                End Select
            End If

            MessageBox.Show("Operación finalizada.", "Nota de Venta", MessageBoxButtons.OK, MessageBoxIcon.Information)

            General.propio = 0
            _items.Rows.Clear()
            RecalcularTotalesPantalla()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Nota de Venta", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmdFinalizar.Enabled = True
            cmdCancelar.Enabled = True
        End Try
    End Sub

    Private Sub EnsurePropio()
        If General.propio <> 0 Then Return

        Dim prop As Long
        Dim dtExiste As DataTable
        Dim existe As Boolean
        Do
            prop = Convert.ToInt64(PuntoVentaActual.ToString() & General.fncIdPropio().ToString())
            dtExiste = DSM.ExecuteQuery(DSM.Stock, "SELECT COUNT(*) AS Cnt FROM Facturas WHERE IdPropio = @IdPropio", CmdParams("@IdPropio", prop))
            existe = Convert.ToInt32(dtExiste.Rows(0)("Cnt")) > 0
            If Not existe Then
                General.propio = prop
                Exit Do
            End If
        Loop
    End Sub
End Class
