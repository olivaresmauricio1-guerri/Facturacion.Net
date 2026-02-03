Imports System.Data.SqlClient
Imports Microsoft.Data
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmNovestk
    Inherits Form

    Private Shared instancia As frmNovestk
    Private esNuevo As Boolean = False
    Private _idNovedadSeleccionada As Integer = 0
    Private _idArticuloSeleccionado As Integer = 0
    Private filaActual As DataGridViewRow
    Private filaActualIndice As Integer = -1
    Private _suspenderAccionFiltros As Boolean = False

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmNovestk()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmNovestk_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub frmNovestk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            CargarCombos()
            CargarNovedades()
            FormModoConsulta()
            dgvNovedades.MultiSelect = True

            ConfigurarEstiloGrid(dgvNovedades)
        Catch ex As Exception
            MessageBox.Show("Error al iniciar el formulario: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CargarCombos()
        Try

            ' Proveedores
            General.CargarCombos(CmbProveedor, "Proveedores", "Descripcion", "Descripcion", "Descripcion", "", True)

            ' Comprobantes
            General.CargarCombos(CmbComprobante, "NumerosComprobantes", "Descripcion", "Descripcion", "ImputaStk", "IdPunto=56")

            ' Sucursales
            General.CargarCombos(CmbSucursal, "Sucursales", "Descripcion", "Descripcion", "IdSucursal", "", True)

            ' TipoVenta
            General.CargarCombos(CmbTipoVenta, "TipoVenta", "Descripcion", "Descripcion", "Descripcion", "", True)

            ' Condicion
            General.CargarCombos(CmbCondicion, "CondicionVenta", "Descripcion", "Descripcion", "Descripcion", "", True)

            ' Canales
            General.CargarCombos(CmbCanal, "CanalesVenta", "Descripcion", "Descripcion", "Descripcion", "", True)

            ' Viajantes
            General.CargarCombos(CmbViajante, "Viajantes", "Descripcion", "Descripcion", "Descripcion", "", True)

            CargarBLs()

        Catch ex As Exception
            MessageBox.Show("Error al cargar combos: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CargarBLs()
        Try
            Dim sql As String = "SELECT BL FROM Nacionalizado GROUP BY BL ORDER BY BL"
            Dim dt As DataTable = DSM.ExecuteQuery(DSM.Stock, sql)
            CmbBL.DataSource = dt
            CmbBL.DisplayMember = "BL"
            CmbBL.ValueMember = "BL"
            CmbBL.SelectedIndex = -1
        Catch ex As Exception

            MessageBox.Show("Error al cargar BLs: " & ex.Message)
        End Try
    End Sub

    Private Sub CargarNovedades()
        Try
            Dim sql As String = "SELECT * FROM NOVESTK ORDER BY PUNTODEVENTA, NROCOMPROBANTE"
            Dim dt As DataTable = DSM.ExecuteQuery(DSM.Stock, sql)
            dgvNovedades.DataSource = dt

            If dt.Rows.Count = 0 Then
                filaActualIndice = -1
                filaActual = Nothing
                FormLimpiarSeleccionado()
                Return
            End If

            filaActualIndice = 0
            filaActual = dgvNovedades.Rows(filaActualIndice)
            GridConfigurarColumnas()
        Catch ex As Exception
            MessageBox.Show("Error al cargar novedades: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridConfigurarColumnas()
        Dim grid = dgvNovedades

        For Each col As DataGridViewColumn In grid.Columns
            col.Visible = False
        Next

        If grid.Columns.Contains("idDetastk") Then
            grid.Columns("idDetastk").Visible = False
        End If

        If grid.Columns.Contains("NroCuenta") Then
            grid.Columns("NroCuenta").Visible = True
            grid.Columns("NroCuenta").HeaderText = "Nro Cuenta"
            grid.Columns("NroCuenta").Width = 100
        End If

        If grid.Columns.Contains("Factura") Then
            grid.Columns("Factura").Visible = True
            grid.Columns("Factura").HeaderText = "Factura"
            grid.Columns("Factura").Width = 100
        End If

        If grid.Columns.Contains("Bonificacion") Then
            grid.Columns("Bonificacion").Visible = True
            grid.Columns("Bonificacion").HeaderText = "Bonificación"
            grid.Columns("Bonificacion").Width = 100
        End If

        If grid.Columns.Contains("NroComprobante") Then
            grid.Columns("NroComprobante").Visible = True
            grid.Columns("NroComprobante").HeaderText = "Nro Comprobante"
            grid.Columns("NroComprobante").Width = 120
        End If

        If grid.Columns.Contains("Fecha") Then
            grid.Columns("Fecha").Visible = True
            grid.Columns("Fecha").HeaderText = "Fecha"
            grid.Columns("Fecha").Width = 100
        End If

        If grid.Columns.Contains("Importe") Then
            grid.Columns("Importe").Visible = True
            grid.Columns("Importe").HeaderText = "Importe"
            grid.Columns("Importe").Width = 100
        End If

        If grid.Columns.Contains("Articulo") Then
            grid.Columns("Articulo").Visible = True
            grid.Columns("Articulo").HeaderText = "Artículo"
            grid.Columns("Articulo").Width = 100
        End If

        If grid.Columns.Contains("Cantidad") Then
            grid.Columns("Cantidad").Visible = True
            grid.Columns("Cantidad").HeaderText = "Cantidad"
            grid.Columns("Cantidad").Width = 100
        End If

        If grid.Columns.Contains("ValorPU") Then
            grid.Columns("ValorPU").Visible = True
            grid.Columns("ValorPU").HeaderText = "Valor P.U."
            grid.Columns("ValorPU").Width = 100
        End If

        If grid.Columns.Contains("Despacho") Then
            grid.Columns("Despacho").Visible = True
            grid.Columns("Despacho").HeaderText = "Despacho"
            grid.Columns("Despacho").Width = 150
        End If

        If grid.Columns.Contains("PuntodeVenta") Then
            grid.Columns("PuntodeVenta").Visible = True
            grid.Columns("PuntodeVenta").HeaderText = "Punto de Venta"
            grid.Columns("PuntodeVenta").Width = 60
        End If

        If grid.Columns.Contains("IdComprob") Then
            grid.Columns("IdComprob").Visible = True
            grid.Columns("IdComprob").HeaderText = "IdComprob"
            grid.Columns("IdComprob").Width = 60
        End If

        ConfigurarEstiloGrid(grid)

        grid.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect

    End Sub

    Private Sub FormModoConsulta()
        SetControlesEnabled(True, cmdAgregar, cmdModificar, cmdBorrar)
        SetControlesEnabled(False, cmdAceptar, cmdCancelar, txtNroCuenta, dtpFecha, txtNroComprobante, txtImporte, txtFactura, txtBonificacion, txtDespacho, txtObservaciones, txtCantidad, txtValorPU, txtArticulo, txtPV, CmbProveedor, CmbComprobante, CmbSucursal, CmbTipoVenta, CmbCondicion, CmbCanal, CmbViajante, chkCancelado, chkMesAnterior, cmdBuscarArticulo)
    End Sub

    Private Sub FormModoEdicion()
        SetControlesEnabled(False, cmdAgregar, cmdModificar, cmdBorrar)
        SetControlesEnabled(True, cmdAceptar, cmdCancelar, txtNroCuenta, dtpFecha, txtNroComprobante, txtImporte, txtFactura, txtBonificacion, txtDespacho, txtObservaciones, txtCantidad, txtValorPU, txtArticulo, txtPV, CmbProveedor, CmbComprobante, CmbSucursal, CmbTipoVenta, CmbCondicion, CmbCanal, CmbViajante, chkCancelado, chkMesAnterior, cmdBuscarArticulo)
    End Sub

    Private Sub FormLimpiarSeleccionado()
        txtNroCuenta.Text = ""
        txtFactura.Text = ""
        txtBonificacion.Text = ""
        txtNroComprobante.Text = ""
        dtpFecha.Value = DateTime.Now
        txtImporte.Text = "0.00"
        txtCantidad.Text = "0"
        txtValorPU.Text = "0.00"
        txtDespacho.Text = ""
        txtObservaciones.Text = ""
        txtArticulo.Text = ""
        lblArticuloDescripcion.Text = ""
        txtPV.Text = ""
        chkCancelado.Checked = False
        chkMesAnterior.Checked = False

        CmbProveedor.SelectedIndex = -1
        CmbComprobante.SelectedIndex = -1
        CmbSucursal.SelectedIndex = -1
        CmbTipoVenta.SelectedIndex = -1
        CmbCondicion.SelectedIndex = -1
        CmbCanal.SelectedIndex = -1
        CmbViajante.SelectedIndex = -1

        _idNovedadSeleccionada = 0
        _idArticuloSeleccionado = 0
    End Sub

    Private Sub FormObtenerSeleccionado()
        If filaActual Is Nothing Then Return

        Try
            _idNovedadSeleccionada = If(filaActual.Cells("idDetastk").Value IsNot DBNull.Value, Convert.ToInt32(filaActual.Cells("idDetastk").Value), 0)

            txtNroCuenta.Text = If(filaActual.Cells("NroCuenta").Value IsNot DBNull.Value, filaActual.Cells("NroCuenta").Value.ToString(), "")
            txtFactura.Text = If(filaActual.Cells("Factura").Value IsNot DBNull.Value, filaActual.Cells("Factura").Value.ToString(), "")
            txtBonificacion.Text = If(filaActual.Cells("Bonificacion").Value IsNot DBNull.Value, filaActual.Cells("Bonificacion").Value.ToString(), "")
            txtNroComprobante.Text = If(filaActual.Cells("NroComprobante").Value IsNot DBNull.Value, filaActual.Cells("NroComprobante").Value.ToString(), "")

            If filaActual.Cells("Fecha").Value IsNot DBNull.Value Then
                dtpFecha.Value = Convert.ToDateTime(filaActual.Cells("Fecha").Value)
            End If

            txtImporte.Text = If(filaActual.Cells("Importe").Value IsNot DBNull.Value, filaActual.Cells("Importe").Value.ToString(), "0.00")
            txtCantidad.Text = If(filaActual.Cells("Cantidad").Value IsNot DBNull.Value, filaActual.Cells("Cantidad").Value.ToString(), "0")
            txtValorPU.Text = If(filaActual.Cells("ValorPU").Value IsNot DBNull.Value, filaActual.Cells("ValorPU").Value.ToString(), "0.00")

            txtDespacho.Text = If(filaActual.Cells("Despacho").Value IsNot DBNull.Value, filaActual.Cells("Despacho").Value.ToString(), "")
            txtArticulo.Text = If(filaActual.Cells("Articulo").Value IsNot DBNull.Value, filaActual.Cells("Articulo").Value.ToString(), "")
            txtPV.Text = If(filaActual.Cells("PuntodeVenta").Value IsNot DBNull.Value, filaActual.Cells("PuntodeVenta").Value.ToString(), "")

            ' Combos
            CmbProveedor.Text = If(filaActual.Cells("Proveedor").Value IsNot DBNull.Value, filaActual.Cells("Proveedor").Value.ToString(), "")
            CmbSucursal.Text = If(filaActual.Cells("IdSucursal").Value IsNot DBNull.Value, filaActual.Cells("IdSucursal").Value.ToString(), "")
            CmbSucursal.SelectedValue = If(filaActual.Cells("IdSucursal").Value IsNot DBNull.Value, filaActual.Cells("IdSucursal").Value, Nothing)
            CmbComprobante.SelectedValue = If(filaActual.Cells("IdComprob").Value IsNot DBNull.Value, filaActual.Cells("IdComprob").Value, Nothing)

            CmbTipoVenta.Text = If(filaActual.Cells("TipoVenta").Value IsNot DBNull.Value, filaActual.Cells("TipoVenta").Value.ToString(), "")
            CmbCondicion.Text = If(filaActual.Cells("CondicionVenta").Value IsNot DBNull.Value, filaActual.Cells("CondicionVenta").Value.ToString(), "")
            CmbCanal.Text = If(filaActual.Cells("CanalVenta").Value IsNot DBNull.Value, filaActual.Cells("CanalVenta").Value.ToString(), "")
            CmbViajante.Text = If(filaActual.Cells("Viajante").Value IsNot DBNull.Value, filaActual.Cells("Viajante").Value.ToString(), "")

            chkCancelado.Checked = If(filaActual.Cells("Cancelado").Value IsNot DBNull.Value, Convert.ToBoolean(filaActual.Cells("Cancelado").Value), False)
            chkMesAnterior.Checked = If(filaActual.Cells("MesAnterior").Value IsNot DBNull.Value, Convert.ToBoolean(filaActual.Cells("MesAnterior").Value), False)

        Catch ex As Exception
            MessageBox.Show("Error al obtener datos: " & ex.Message)
        End Try
    End Sub

    Private Sub cmdBL_Click(sender As Object, e As EventArgs) Handles cmdBL.Click
        If CmbBL.Text = "" Then
            MessageBox.Show("Debe seleccionar un BL", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If txtDesp.Text = "" Then
            MessageBox.Show("Ingrese un despacho", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Dim sql As String = ""
            If optTotal.Checked Then
                sql = "Select * from Nacionalizado where BL = '" & CmbBL.Text & "' and procesado = 0"
            Else
                sql = "Select * from Nacionalizado where procesado = 0 and BL = '" & CmbBL.Text & "' and estado = 'Salida x ZF'"
            End If

            Dim dtNac As DataTable = DSM.ExecuteQuery(DSM.Stock, sql)
            If dtNac.Rows.Count = 0 Then
                MessageBox.Show("No tiene registros para procesar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            For Each row As DataRow In dtNac.Rows

                Dim articuloCode As String = row("Articulo").ToString()
                Dim sqlMae As String = "Select * from MaeStk Where Articulo = @Articulo"
                Dim prmsMae As New Dictionary(Of String, Object)
                prmsMae.Add("@Articulo", articuloCode)
                Dim dtMae As DataTable = DSM.ExecuteQuery(DSM.Stock, sqlMae, prmsMae)

                If dtMae.Rows.Count = 0 Then
                    MessageBox.Show("Articulo Inexistente " & articuloCode)
                    Continue For
                End If
                Dim rowMae As DataRow = dtMae.Rows(0)

                ' Insert into NOVESTK
                Dim sqlInsert As String = "INSERT INTO NOVESTKA (IdArticulo, Articulo, IDSUCURSAL, IdComprob, proveedor, Despacho, Fecha, CANTIDAD, valorc) " &
                                          "VALUES (@IdArticulo, @Articulo, 3, 1, @proveedor, @Despacho, @Fecha, @CANTIDAD, @valorc)"
                Dim prmsInsert As New Dictionary(Of String, Object)
                prmsInsert.Add("@IdArticulo", rowMae("IdArticulo"))
                prmsInsert.Add("@Articulo", articuloCode)
                prmsInsert.Add("@proveedor", rowMae("proveedor"))
                prmsInsert.Add("@Despacho", txtDesp.Text)
                prmsInsert.Add("@Fecha", DateTime.Now)
                prmsInsert.Add("@CANTIDAD", row("CANTIDAD"))
                prmsInsert.Add("@valorc", row("FOB"))

                DSM.ExecuteQuery(DSM.Stock, sqlInsert, prmsInsert)

                ' Update MERCADERIATRANSITO
                Dim sqlTransito As String = "UPDATE MERCADERIATRANSITO SET SaldoActual = SaldoActual - @Cantidad WHERE BL = @BL AND ARTICULO = @Articulo"
                Dim prmsTransito As New Dictionary(Of String, Object)
                prmsTransito.Add("@Cantidad", row("CANTIDAD"))
                prmsTransito.Add("@BL", row("BL"))
                prmsTransito.Add("@Articulo", articuloCode)
                DSM.ExecuteQuery(DSM.Stock, sqlTransito, prmsTransito)
            Next

            ' Update Nacionalizado
            Dim sqlUpdNac As String = "UPDATE Nacionalizado SET Despacho = @Despacho, PROCESADO = 1, fecha = @Fecha WHERE BL = @BL AND procesado = 0"
            Dim prmsUpdNac As New Dictionary(Of String, Object)
            prmsUpdNac.Add("@Despacho", txtDesp.Text)
            prmsUpdNac.Add("@Fecha", DateTime.Now)
            prmsUpdNac.Add("@BL", CmbBL.Text)
            DSM.ExecuteQuery(DSM.Stock, sqlUpdNac, prmsUpdNac)

            CargarNovedades()
            MessageBox.Show("BL Cargado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error al procesar BL: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdBuscarArticulo_Click(sender As Object, e As EventArgs) Handles cmdBuscarArticulo.Click
        Using frm As New frmArticuloSelector()
            If frm.ShowDialog(Me) = DialogResult.OK Then
                Dim articulo = frm.Seleccion
                txtArticulo.Text = If(articulo IsNot Nothing, articulo.Item("Articulo").ToString(), String.Empty)
                CmbProveedor.Text = If(articulo IsNot Nothing, articulo.Item("Proveedor").ToString(), String.Empty)
                _idArticuloSeleccionado = If(articulo IsNot Nothing, Convert.ToInt32(articulo.Item("IdArticulo")), 0)
            End If
        End Using
    End Sub
    Private Sub cmdAgregar_Click(sender As Object, e As EventArgs) Handles cmdAgregar.Click
        esNuevo = True
        FormLimpiarSeleccionado()
        FormModoEdicion()
        txtArticulo.Focus()
    End Sub

    Private Sub cmdModificar_Click(sender As Object, e As EventArgs) Handles cmdModificar.Click
        If filaActual Is Nothing Then Return
        esNuevo = False
        FormModoEdicion()
    End Sub

    Private Sub cmdBorrar_Click(sender As Object, e As EventArgs) Handles cmdBorrar.Click
        If filaActual Is Nothing Then
            MessageBox.Show("Seleccione un registro para borrar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("¿Está seguro que desea borrar la novedad?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                Dim Sql = "DELETE FROM NOVESTK WHERE idDetastk = @idDetastk"
                Dim parametros = CmdParams("@idDetastk", _idNovedadSeleccionada)
                DSM.Execute(DSM.Stock, Sql, parametros)
                CargarNovedades()
            Catch ex As Exception
                MessageBox.Show("Error al borrar: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub cmdCancelar_Click(sender As Object, e As EventArgs) Handles cmdCancelar.Click
        FormModoConsulta()
        FormLimpiarSeleccionado()
        If filaActual IsNot Nothing Then FormObtenerSeleccionado()
    End Sub

    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub

    Private Sub dgvNovedades_SelectionChanged(sender As Object, e As EventArgs) Handles dgvNovedades.SelectionChanged
        If dgvNovedades Is Nothing OrElse dgvNovedades.CurrentRow Is Nothing Then Return
        filaActualIndice = -1
        filaActual = Nothing
        AplicarSeleccionActual()
    End Sub

    Private Sub cmdAceptar_Click(sender As Object, e As EventArgs) Handles cmdAceptar.Click
        If Not ValidarDatos() Then Return

        Try
            Dim prms As New Dictionary(Of String, Object)

            ' Campos
            prms.Add("@NroCuenta", If(String.IsNullOrEmpty(txtNroCuenta.Text), DBNull.Value, txtNroCuenta.Text))
            prms.Add("@Factura", If(String.IsNullOrEmpty(txtFactura.Text), DBNull.Value, txtFactura.Text))
            prms.Add("@Bonificacion", If(String.IsNullOrEmpty(txtBonificacion.Text), DBNull.Value, txtBonificacion.Text))
            prms.Add("@NroComprobante", If(String.IsNullOrEmpty(txtNroComprobante.Text), DBNull.Value, txtNroComprobante.Text))
            prms.Add("@Fecha", dtpFecha.Value)
            prms.Add("@Importe", Convert.ToDecimal(If(String.IsNullOrEmpty(txtImporte.Text), "0", txtImporte.Text)))
            prms.Add("@Cantidad", Convert.ToDecimal(If(String.IsNullOrEmpty(txtCantidad.Text), "0", txtCantidad.Text)))
            prms.Add("@ValorPU", Convert.ToDecimal(If(String.IsNullOrEmpty(txtValorPU.Text), "0", txtValorPU.Text)))
            prms.Add("@Despacho", If(String.IsNullOrEmpty(txtDespacho.Text), DBNull.Value, txtDespacho.Text))
            prms.Add("@Articulo", If(String.IsNullOrEmpty(txtArticulo.Text), DBNull.Value, txtArticulo.Text))
            prms.Add("@PuntodeVenta", If(String.IsNullOrEmpty(txtPV.Text), DBNull.Value, txtPV.Text))

            prms.Add("@Proveedor", If(CmbProveedor.Text = "", DBNull.Value, CmbProveedor.Text))
            prms.Add("@IdSucursal", If(CmbSucursal.SelectedValue Is Nothing, DBNull.Value, CmbSucursal.SelectedValue))
            prms.Add("@IdComprob", If(CmbComprobante.SelectedValue Is Nothing, DBNull.Value, CmbComprobante.SelectedValue))
            prms.Add("@TipoVenta", If(CmbTipoVenta.Text = "", DBNull.Value, CmbTipoVenta.Text))
            prms.Add("@CondicionVenta", If(CmbCondicion.Text = "", DBNull.Value, CmbCondicion.Text))
            prms.Add("@CanalVenta", If(CmbCanal.Text = "", DBNull.Value, CmbCanal.Text))
            prms.Add("@Viajante", If(CmbViajante.Text = "", DBNull.Value, CmbViajante.Text))

            prms.Add("@Cancelado", chkCancelado.Checked)
            prms.Add("@MesAnterior", chkMesAnterior.Checked)

            prms.Add("@IdArticulo", _idArticuloSeleccionado)

            Dim sql As String = ""
            If esNuevo Then
                sql = "INSERT INTO NOVESTK (NroCuenta, Factura, Bonificacion, NroComprobante, Fecha, Importe, Cantidad, ValorPU, Despacho, Articulo, PuntodeVenta, Proveedor, IdSucursal, IdComprob, TipoVenta, CondicionVenta, CanalVenta, Viajante, Cancelado, MesAnterior, IdArticulo) " &
                      "VALUES (@NroCuenta, @Factura, @Bonificacion, @NroComprobante, @Fecha, @Importe, @Cantidad, @ValorPU, @Despacho, @Articulo, @PuntodeVenta, @Proveedor, @IdSucursal, @IdComprob, @TipoVenta, @CondicionVenta, @CanalVenta, @Viajante, @Cancelado, @MesAnterior, @IdArticulo)"
            Else
                sql = "UPDATE NOVESTK SET NroCuenta=@NroCuenta, Factura=@Factura, Bonificacion=@Bonificacion, NroComprobante=@NroComprobante, Fecha=@Fecha, Importe=@Importe, Cantidad=@Cantidad, ValorPU=@ValorPU, Despacho=@Despacho, Articulo=@Articulo, PuntodeVenta=@PuntodeVenta, Proveedor=@Proveedor, IdSucursal=@IdSucursal, IdComprob=@IdComprob, TipoVenta=@TipoVenta, CondicionVenta=@CondicionVenta, CanalVenta=@CanalVenta, Viajante=@Viajante, Cancelado=@Cancelado, MesAnterior=@MesAnterior " &
                      "WHERE idDetastk = " & _idNovedadSeleccionada
            End If

            DSM.ExecuteQuery(DSM.Stock, sql, prms, True)

            CargarNovedades()
            FormModoConsulta()

        Catch ex As Exception
            MessageBox.Show("Error al guardar: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub txtValorPU_TextChanged(sender As Object, e As EventArgs) Handles txtValorPU.TextChanged
        'si el valor ingresado es numerico, calcular el importe
        Dim valorPU As Decimal
        Dim cantidad As Decimal
        If Decimal.TryParse(txtValorPU.Text, valorPU) AndAlso Decimal.TryParse(txtCantidad.Text, cantidad) Then
            Dim importe As Decimal = valorPU * Math.Abs(cantidad)
            txtImporte.Text = importe.ToString("F2")
        End If
    End Sub
    Private Function ValidarDatos() As Boolean
        If String.IsNullOrEmpty(txtArticulo.Text) Then
            MessageBox.Show("Debe ingresar un artículo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtArticulo.Focus()
            Return False
        End If

        If CmbComprobante.SelectedIndex = -1 Then
            MessageBox.Show("Debe seleccionar un comprobante.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CmbComprobante.Focus()
            Return False
        End If

        If CmbSucursal.SelectedIndex = -1 Then
            MessageBox.Show("Debe seleccionar una sucursal.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CmbSucursal.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub AplicarSeleccionActual()
        If dgvNovedades Is Nothing OrElse dgvNovedades.CurrentRow Is Nothing Then Return
        If dgvNovedades.SelectedRows.Count > 1 Then
            filaActualIndice = -1
            filaActual = Nothing
            FormLimpiarSeleccionado()
            Return
        End If
        Dim idx = dgvNovedades.CurrentRow.Index
        If idx < 0 OrElse idx = filaActualIndice Then Return
        FormModoConsulta()

        filaActualIndice = idx
        filaActual = dgvNovedades.CurrentRow
        FormObtenerSeleccionado()
    End Sub
    Private Sub ValidarArticulo()
        If String.IsNullOrEmpty(txtArticulo.Text) Then
            _idArticuloSeleccionado = 0
            lblArticuloDescripcion.Text = ""
            Return
        End If

        Try
            Dim sql As String = "SELECT IdArticulo, Descripcion FROM MAESTK WHERE Articulo = @Articulo"
            Dim prms As New Dictionary(Of String, Object)
            prms.Add("@Articulo", txtArticulo.Text)

            Dim dt As DataTable = DSM.ExecuteQuery(DSM.Stock, sql, prms)
            If dt.Rows.Count <= 0 Then
                MessageBox.Show("Artículo no encontrado.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            _idArticuloSeleccionado = Convert.ToInt32(dt.Rows(0)("IdArticulo"))
            lblArticuloDescripcion.Text = dt.Rows(0)("Descripcion").ToString()

        Catch ex As Exception
            MessageBox.Show("Error al buscar artículo: " & ex.Message)
        End Try
    End Sub

    Private Sub txtArticulo_LostFocus(sender As Object, e As EventArgs) Handles txtArticulo.LostFocus
        ValidarArticulo()
    End Sub

    Private Sub lnkCopiar_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCopiar.LinkClicked
        CopiarDataGrid(dgvNovedades, chkEncabezados.Checked)
    End Sub
End Class
