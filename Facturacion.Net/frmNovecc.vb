Imports System.Data.SqlClient
Imports Microsoft.Data
Imports DSM = DataSourceManager.Lib.DataSourceManager
Public Class frmNovecc
    Inherits Form

    Private Shared instancia As frmNovecc
    Private esNuevo As Boolean = False
    Private _idNovedadSeleccionada As Integer = 0
    Private _idCtaCteSeleccionada As Integer = 0
    Private filaActual As DataGridViewRow
    Private filaActualIndice As Integer = -1
    Private _suspenderAccionFiltros As Boolean = False

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmNovecc()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub
    Private Sub frmNovecc_formclosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub frmNovecc_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ConfigurarEstiloGrid(dgvNovedades)
            CargarCombos()
            CargarNovedades()
            FormModoConsulta()
            dgvNovedades.MultiSelect = True
        Catch ex As Exception
            MessageBox.Show("Error al iniciar el formulario: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CargarCombos()
        Try
            ' Cargar combos utilizando la función compartida en General.vb
            ' Asumiendo nombres de tablas y campos estándar
            General.CargarCombos(CmbTipoVenta, "TipoVenta", "Descripcion", "Descripcion", "IdTipoVenta", "", True)
            General.CargarCombos(CmbCondicion, "CondicionVenta", "Descripcion", "Descripcion", "IdCondicion", "", True)
            General.CargarCombos(CmbComprobante, "NumerosComprobantes", "Descripcion", "Descripcion", "ImputaCC", "IdPunto=56", True)
            General.CargarCombos(CmbSucursal, "Sucursales", "Descripcion", "Descripcion", "IdSucursal", "", True)
            General.CargarCombos(CmbBanco, "Bancos", "Descripcion", "Descripcion", "IdBanco", "", True)
            General.CargarCombos(CmbTipoValor, "TipoValores", "Descripcion", "Descripcion", "Descripcion", "", True)
        Catch ex As Exception
            MessageBox.Show("Error al cargar combos: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CargarNovedades()
        Try
            Dim sql As String = "SELECT IdDetaCtaCte, IdCtaCte, NroCuenta, NroFactura, NroCupon, Tarjeta, NroComprobante, NombreComprobante, Tipoventa, Condicion, Fecha, Valoriza, IdImputacion, Monto, IInterno, IBrutos, ACuenta, FechaVto, TipoValor, 
                         Banco, Cuotas, LocalidadCP, NroCheque, RegInterno, Sucursal, TipoBaja, Cobrado, Anterior, NroRecibo, IvaRI, IvaRNI, Neto, VentaDiaria, Exento, ChequePropio, CtaAgip, PuntodeVenta, IdPropio, CAE, Vencimiento, Enviado, 
                         NroCierre, CodigoAfip FROM NoveCtaCte ORDER BY Puntodeventa, NroComprobante "
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
            'AplicarSeleccionActual()
            GridConfigurarColumnas()
        Catch ex As Exception
            MessageBox.Show("Error al cargar novedades: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub chkAnterior_CheckedChanged(sender As Object, e As EventArgs) Handles chkAnterior.CheckedChanged
        CargarNovedades()
    End Sub


    Private Sub FormModoConsulta()
        SetControlesEnabled(True, cmdAgregar, cmdModificar, cmdBorrar, dgvNovedades, chkAnterior)
        SetControlesEnabled(False, cmdAceptar, cmdCancelar, txtNroCuenta, txtFecha, txtNroComprobante, txtMonto, txtNroCupon, txtNroFactura, txtInterno, txtRegInterno, txtFechaVto, txtNroCheque, txtBonificacion, txtObservaciones, CmbTipoVenta, CmbCondicion, CmbComprobante, CmbSucursal, CmbBanco, TxtCP, CmbTipoValor, txtPV)

    End Sub

    Private Sub FormModoEdicion()
        SetControlesEnabled(False, cmdAgregar, cmdModificar, cmdBorrar, dgvNovedades)
        SetControlesEnabled(True, cmdAceptar, cmdCancelar, txtNroCuenta, txtFecha, txtNroComprobante, txtMonto, txtNroCupon, txtNroFactura, txtInterno, txtRegInterno, txtFechaVto, txtNroCheque, txtBonificacion, txtObservaciones, CmbTipoVenta, CmbCondicion, CmbComprobante, CmbSucursal, CmbBanco, TxtCP, CmbTipoValor, chkAnterior, txtPV)
    End Sub

    Private Sub FormLimpiarSeleccionado()
        txtNroCuenta.Text = ""
        txtNroFactura.Text = ""
        txtNroCupon.Text = ""
        txtNroComprobante.Text = ""
        txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy")
        txtMonto.Text = "0.00"
        txtInterno.Text = ""
        txtRegInterno.Text = ""
        txtFechaVto.Text = ""
        txtNroCheque.Text = ""
        txtBonificacion.Text = "0.00"
        txtObservaciones.Text = ""
        TxtCP.Text = ""
        txtPV.Text = ""
        chkAnterior.Checked = False

        CmbTipoVenta.SelectedIndex = -1
        CmbCondicion.SelectedIndex = -1
        CmbComprobante.SelectedIndex = -1
        CmbSucursal.SelectedIndex = -1
        CmbBanco.SelectedIndex = -1
        CmbTipoValor.SelectedIndex = -1

        _idCtaCteSeleccionada = 0
        _idNovedadSeleccionada = 0
    End Sub

    Private Sub cmdAgregar_Click(sender As Object, e As EventArgs) Handles cmdAgregar.Click
        esNuevo = True
        FormLimpiarSeleccionado()
        FormModoEdicion()
        txtNroCuenta.Focus()
    End Sub

    Private Sub cmdModificar_Click(sender As Object, e As EventArgs) Handles cmdModificar.Click
        _suspenderAccionFiltros = True
        If filaActual Is Nothing Then Return
        FormModoEdicion()
        _suspenderAccionFiltros = False
    End Sub

    Private Sub cmdBorrar_Click(sender As Object, e As EventArgs) Handles cmdBorrar.Click
        If filaActual Is Nothing Then
            MessageBox.Show("Seleccione un registro para borrar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("¿Está seguro que desea borrar la novedad?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                Dim Sql = "DELETE FROM NoveCtaCte WHERE IdDetaCtaCte = @IdDetaCtaCte"
                Dim parametros = CmdParams("@IdDetaCtaCte", Convert.ToInt32(filaActual.Cells("IdDetaCtaCte").Value))
                DSM.Execute(DSM.Stock, Sql, parametros)
                CargarNovedades()
            Catch ex As Exception
                MessageBox.Show("Error al borrar: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub cmdCancelar_Click(sender As Object, e As EventArgs) Handles cmdCancelar.Click
        FormModoConsulta()
    End Sub

    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub

    Private Sub cmdAceptar_Click(sender As Object, e As EventArgs) Handles cmdAceptar.Click
        If Not ValidarDatos() Then Return

        Try
            Dim prms As New Dictionary(Of String, Object)

            prms.Add("@NroCuenta", _idCtaCteSeleccionada)
            prms.Add("@Fecha", Convert.ToDateTime(txtFecha.Text))
            prms.Add("@NroComprobante", If(String.IsNullOrEmpty(txtNroComprobante.Text), DBNull.Value, txtNroComprobante.Text))
            prms.Add("@Monto", Convert.ToDecimal(txtMonto.Text))
            prms.Add("@PuntodeVenta", If(String.IsNullOrEmpty(txtPV.Text), DBNull.Value, txtPV.Text))
            prms.Add("@IdImputacion", If(CmbComprobante.SelectedValue Is Nothing, DBNull.Value, CmbComprobante.SelectedValue))
            prms.Add("@NombreComprobante", If(CmbComprobante.Text = "", DBNull.Value, CmbComprobante.Text))

            ' Campos analizados (Strings desde Combos)
            prms.Add("@Tipoventa", CmbTipoVenta.Text)
            prms.Add("@Condicion", CmbCondicion.Text)
            prms.Add("@Sucursal", CmbSucursal.Text)
            prms.Add("@Banco", CmbBanco.Text)
            prms.Add("@LocalidadCP", If(String.IsNullOrEmpty(TxtCP.Text), DBNull.Value, TxtCP.Text))
            prms.Add("@TipoValor", CmbTipoValor.Text)

            prms.Add("@NroCheque", If(String.IsNullOrEmpty(txtNroCheque.Text), DBNull.Value, txtNroCheque.Text))
            prms.Add("@NroCupon", If(String.IsNullOrEmpty(txtNroCupon.Text), DBNull.Value, txtNroCupon.Text))
            prms.Add("@NroFactura", If(String.IsNullOrEmpty(txtNroFactura.Text), DBNull.Value, txtNroFactura.Text))
            prms.Add("@RegInterno", If(String.IsNullOrEmpty(txtRegInterno.Text), DBNull.Value, txtRegInterno.Text))

            Dim impInterno As Decimal = 0
            Decimal.TryParse(txtInterno.Text, impInterno)
            prms.Add("@IInterno", impInterno)

            prms.Add("@Anterior", chkAnterior.Checked)

            If IsDate(txtFechaVto.Text) Then
                prms.Add("@FechaVto", Convert.ToDateTime(txtFechaVto.Text))
            Else
                prms.Add("@FechaVto", DBNull.Value)
            End If

            Dim sql As String = ""
            If esNuevo Then
                sql = "INSERT INTO NoveCtaCte (NroCuenta, Fecha, NroComprobante, Monto, PuntodeVenta, IdImputacion, NombreComprobante, " &
                      "Tipoventa, Condicion, Sucursal, Banco, LocalidadCP, TipoValor, NroCheque, NroCupon, NroFactura, RegInterno, IInterno, FechaVto, Anterior) " &
                      "VALUES (@NroCuenta, @Fecha, @NroComprobante, @Monto, @PuntodeVenta, @IdImputacion, @NombreComprobante, " &
                      "@Tipoventa, @Condicion, @Sucursal, @Banco, @LocalidadCP, @TipoValor, @NroCheque, @NroCupon, @NroFactura, @RegInterno, @IInterno, @FechaVto, @Anterior)"
            Else
                sql = "UPDATE NoveCtaCte SET Fecha=@Fecha, NroComprobante=@NroComprobante, " &
                      "Monto=@Monto, PuntodeVenta=@PuntodeVenta, IdImputacion=@IdImputacion, " &
                      "Tipoventa=@Tipoventa, Condicion=@Condicion, Sucursal=@Sucursal, Banco=@Banco, LocalidadCP=@LocalidadCP, " &
                      "TipoValor=@TipoValor, NroCheque=@NroCheque, NroCupon=@NroCupon, NroFactura=@NroFactura, RegInterno=@RegInterno, " &
                      "IInterno=@IInterno, FechaVto=@FechaVto, Anterior=@Anterior " &
                      "WHERE IdDetaCtaCte = " & _idNovedadSeleccionada
            End If

            DSM.ExecuteQuery(DSM.Stock, sql, prms, True)

            CargarNovedades()
            FormModoConsulta()

        Catch ex As Exception
            MessageBox.Show("Error al guardar: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ValidarDatos() As Boolean
        If _idCtaCteSeleccionada = 0 Then
            MessageBox.Show("Debe seleccionar una cuenta válida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtNroCuenta.Focus()
            Return False
        End If

        If Not IsDate(txtFecha.Text) Then
            MessageBox.Show("Fecha inválida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtFecha.Focus()
            Return False
        End If

        If Not IsNumeric(txtMonto.Text) Then
            MessageBox.Show("El monto debe ser numérico.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtMonto.Focus()
            Return False
        End If

        If CmbComprobante.SelectedIndex = -1 Then
            MessageBox.Show("Debe seleccionar un comprobante.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CmbComprobante.Focus()
            Return False
        End If

        'If CmbTipoVenta.SelectedIndex = -1 Then
        '    MessageBox.Show("Debe seleccionar un tipo de venta.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '    CmbTipoVenta.Focus()
        '    Return False
        'End If

        If CmbCondicion.SelectedIndex = -1 Then
            MessageBox.Show("Debe seleccionar una condición de venta.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CmbCondicion.Focus()
            Return False
        End If

        If CmbSucursal.SelectedIndex = -1 Then
            MessageBox.Show("Debe seleccionar una sucursal.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CmbSucursal.Focus()
            Return False
        End If

        If Not String.IsNullOrEmpty(txtInterno.Text) AndAlso Not IsNumeric(txtInterno.Text) Then
            MessageBox.Show("El impuesto interno debe ser numérico.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtInterno.Focus()
            Return False
        End If

        If CmbTipoValor.Text.ToUpper().Contains("CHEQUE") AndAlso String.IsNullOrWhiteSpace(txtNroCheque.Text) Then
            MessageBox.Show("Debe ingresar el número de cheque.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtNroCheque.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub Controls_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNroCuenta.KeyPress, txtFecha.KeyPress, txtNroComprobante.KeyPress, txtMonto.KeyPress, txtNroCupon.KeyPress, txtNroFactura.KeyPress, txtInterno.KeyPress, txtRegInterno.KeyPress, txtFechaVto.KeyPress, txtNroCheque.KeyPress, txtBonificacion.KeyPress, txtObservaciones.KeyPress, CmbTipoVenta.KeyPress, CmbCondicion.KeyPress, CmbComprobante.KeyPress, CmbSucursal.KeyPress, CmbBanco.KeyPress, CmbTipoValor.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub CmbTipoValor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbTipoValor.SelectedIndexChanged
        If CmbTipoValor.Text.ToUpper().Contains("CHEQUE") Then
            txtNroCheque.Visible = True
            Label18.Visible = True
        Else
            txtNroCheque.Visible = False
            Label18.Visible = False
            txtNroCheque.Text = ""
        End If
    End Sub

    Private Sub txtNroCuenta_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtNroCuenta.Validating
        If String.IsNullOrWhiteSpace(txtNroCuenta.Text) Then Return

        Try
            Dim sql As String = "SELECT Nrocuenta, Nombre FROM MaeCtaCte WHERE NroCuenta = " & txtNroCuenta.Text
            Dim dt As DataTable = DataSourceManager.Lib.DataSourceManager.ExecuteQuery(DataSourceManager.Lib.DataSourceManager.Stock, sql)

            If dt.Rows.Count > 0 Then
                _idCtaCteSeleccionada = Convert.ToInt32(dt.Rows(0)("NroCuenta"))
            Else
                _idCtaCteSeleccionada = 0
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub dgvNovedades_SelectionChanged(sender As Object, e As EventArgs) Handles dgvNovedades.SelectionChanged
        If dgvNovedades Is Nothing OrElse dgvNovedades.CurrentRow Is Nothing Then Return
        filaActualIndice = -1
        filaActual = Nothing
        AplicarSeleccionActual()
    End Sub

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

    Public Sub FormObtenerSeleccionado()
        If filaActual Is Nothing Then Return

        ' Limpiar combos
        CmbTipoVenta.SelectedIndex = -1
        CmbTipoVenta.Text = String.Empty
        CmbCondicion.SelectedIndex = -1
        CmbCondicion.Text = String.Empty
        CmbComprobante.SelectedIndex = -1
        CmbComprobante.Text = String.Empty
        CmbSucursal.SelectedIndex = -1
        CmbSucursal.Text = String.Empty
        CmbBanco.SelectedIndex = -1
        CmbBanco.Text = String.Empty
        TxtCP.Text = String.Empty
        CmbTipoValor.SelectedIndex = -1
        CmbTipoValor.Text = String.Empty

        If filaActual IsNot Nothing Then
            Try
                _idNovedadSeleccionada = If(filaActual.Cells("IdDetaCtaCte").Value IsNot DBNull.Value, Convert.ToInt32(filaActual.Cells("IdDetaCtaCte").Value), 0)
                _idCtaCteSeleccionada = If(filaActual.Cells("IdCtaCte").Value IsNot DBNull.Value, Convert.ToInt32(filaActual.Cells("IdCtaCte").Value), 0)

                txtNroCuenta.Text = If(filaActual.Cells("NroCuenta").Value IsNot DBNull.Value, filaActual.Cells("NroCuenta").Value.ToString(), String.Empty)
                txtNroFactura.Text = If(filaActual.Cells("NroFactura").Value IsNot DBNull.Value, filaActual.Cells("NroFactura").Value.ToString(), String.Empty)
                txtPV.Text = If(filaActual.Cells("PuntodeVenta").Value IsNot DBNull.Value, filaActual.Cells("PuntodeVenta").Value.ToString(), String.Empty)
                txtNroCupon.Text = If(filaActual.Cells("NroCupon").Value IsNot DBNull.Value, filaActual.Cells("NroCupon").Value.ToString(), String.Empty)
                txtNroComprobante.Text = If(filaActual.Cells("NroComprobante").Value IsNot DBNull.Value, filaActual.Cells("NroComprobante").Value.ToString(), String.Empty)
                CmbComprobante.SelectedValue = If(filaActual.Cells("IdImputacion").Value IsNot DBNull.Value, filaActual.Cells("IdImputacion").Value, Nothing)
                CmbTipoVenta.Text = If(filaActual.Cells("TipoVenta").Value IsNot DBNull.Value, filaActual.Cells("TipoVenta").Value.ToString(), String.Empty)
                CmbCondicion.Text = If(filaActual.Cells("Condicion").Value IsNot DBNull.Value, filaActual.Cells("Condicion").Value.ToString(), String.Empty)

                If filaActual.Cells("Fecha").Value IsNot DBNull.Value Then
                    txtFecha.Text = Convert.ToDateTime(filaActual.Cells("Fecha").Value).ToString("dd/MM/yyyy")
                End If

                txtMonto.Text = If(filaActual.Cells("Monto").Value IsNot DBNull.Value, filaActual.Cells("Monto").Value.ToString(), "0.00")
                CmbSucursal.Text = If(filaActual.Cells("Sucursal").Value IsNot DBNull.Value, filaActual.Cells("Sucursal").Value.ToString(), String.Empty)
                CmbBanco.Text = If(filaActual.Cells("Banco").Value IsNot DBNull.Value, filaActual.Cells("Banco").Value.ToString(), String.Empty)
                TxtCP.Text = If(filaActual.Cells("LocalidadCP").Value IsNot DBNull.Value, filaActual.Cells("LocalidadCP").Value.ToString(), String.Empty)
                CmbTipoValor.Text = If(filaActual.Cells("TipoValor").Value IsNot DBNull.Value, filaActual.Cells("TipoValor").Value.ToString(), String.Empty)

                txtNroCheque.Text = If(filaActual.Cells("NroCheque").Value IsNot DBNull.Value, filaActual.Cells("NroCheque").Value.ToString(), String.Empty)
                txtRegInterno.Text = If(filaActual.Cells("RegInterno").Value IsNot DBNull.Value, filaActual.Cells("RegInterno").Value.ToString(), String.Empty)
                txtInterno.Text = If(filaActual.Cells("IInterno").Value IsNot DBNull.Value, Convert.ToDecimal(filaActual.Cells("IInterno").Value).ToString("N2"), "0.00")

                If filaActual.Cells("FechaVto").Value IsNot DBNull.Value Then
                    txtFechaVto.Text = Convert.ToDateTime(filaActual.Cells("FechaVto").Value).ToString("dd/MM/yyyy")
                Else
                    txtFechaVto.Text = ""
                End If

                chkAnterior.Checked = If(filaActual.Cells("Anterior").Value IsNot DBNull.Value, Convert.ToBoolean(filaActual.Cells("Anterior").Value), False)

                If _idCtaCteSeleccionada = 0 AndAlso Not String.IsNullOrWhiteSpace(txtNroCuenta.Text) Then
                    txtNroCuenta_Validating(Nothing, Nothing)
                End If
            Catch ex As Exception
                ' Si falta alguna columna en el grid, no bloqueamos todo, pero mostramos error en debug
                MsgBox("Error en FormObtenerSeleccionado: " & ex.Message, MsgBoxStyle.Critical, "Error")
            End Try
        End If
    End Sub

    Public Sub GridConfigurarColumnas()
        Dim grid = dgvNovedades

        For Each col As DataGridViewColumn In grid.Columns
            col.Visible = False
        Next

        If grid.Columns.Contains("IdDetaCtaCte") Then
            grid.Columns("IdDetaCtaCte").Visible = False
        End If

        If grid.Columns.Contains("NroCuenta") Then
            grid.Columns("NroCuenta").Visible = True
            grid.Columns("NroCuenta").HeaderText = "Nro.Cta."
            grid.Columns("NroCuenta").Width = 80
            grid.Columns("NroCuenta").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If

        If grid.Columns.Contains("PuntodeVenta") Then
            grid.Columns("PuntodeVenta").Visible = True
            grid.Columns("PuntodeVenta").HeaderText = "P.Vta."
            grid.Columns("PuntodeVenta").Width = 60
            grid.Columns("PuntodeVenta").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If

        If grid.Columns.Contains("NroFactura") Then
            grid.Columns("NroFactura").Visible = True
            grid.Columns("NroFactura").HeaderText = "Nro.Fact."
            grid.Columns("NroFactura").Width = 80
            grid.Columns("NroFactura").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If

        If grid.Columns.Contains("Monto") Then
            grid.Columns("Monto").Visible = True
            grid.Columns("Monto").HeaderText = "Monto"
            grid.Columns("Monto").Width = 100
            grid.Columns("Monto").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            grid.Columns("Monto").DefaultCellStyle.Format = "N2"
        End If

        If grid.Columns.Contains("NroComprobante") Then
            grid.Columns("NroComprobante").Visible = True
            grid.Columns("NroComprobante").HeaderText = "Nro.Comp."
            grid.Columns("NroComprobante").Width = 80
            grid.Columns("NroComprobante").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If

        If grid.Columns.Contains("NombreComprobante") Then
            grid.Columns("NombreComprobante").Visible = True
            grid.Columns("NombreComprobante").HeaderText = "Tipo Comp."
            grid.Columns("NombreComprobante").Width = 120
        End If

        If grid.Columns.Contains("Condicion") Then
            grid.Columns("Condicion").Visible = True
            grid.Columns("Condicion").HeaderText = "Condición"
            grid.Columns("Condicion").Width = 100
        End If

        If grid.Columns.Contains("Fecha") Then
            grid.Columns("Fecha").Visible = True
            grid.Columns("Fecha").HeaderText = "Fecha"
            grid.Columns("Fecha").Width = 80
            grid.Columns("Fecha").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            grid.Columns("Fecha").DefaultCellStyle.Format = "dd/MM/yyyy"
        End If

        If grid.Columns.Contains("Tipoventa") Then
            grid.Columns("Tipoventa").Visible = True
            grid.Columns("Tipoventa").HeaderText = "Tipo Venta"
            grid.Columns("Tipoventa").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End If

        If grid.Columns.Contains("Sucursal") Then
            grid.Columns("Sucursal").Visible = True
            grid.Columns("Sucursal").HeaderText = "Sucursal"
            grid.Columns("Sucursal").Width = 100
        End If

        If grid.Columns.Contains("Anterior") Then
            grid.Columns("Anterior").Visible = True
            grid.Columns("Anterior").HeaderText = "Anterior"
            grid.Columns("Anterior").Width = 60
        End If

        ConfigurarEstiloGrid(grid)

        grid.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect
    End Sub

    Private Sub lnkCopiar_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCopiar.LinkClicked
        CopiarDataGrid(dgvNovedades, chkEncabezados.Checked)
    End Sub

End Class
