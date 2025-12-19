Imports System.Data.SqlClient
Imports DSM = DataSourceManager.Lib.DataSourceManager
Public Class frmNovecc
    Inherits Form

    Private Shared instancia As frmNovecc
    Private esNuevo As Boolean = False
    Private _idNovedadSeleccionada As Integer = 0
    Private _idCtaCteSeleccionada As Integer = 0
    Private filaActual As DataGridViewRow
    Private filaActualIndice As Integer = -1

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
        Catch ex As Exception
            MessageBox.Show("Error al iniciar el formulario: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CargarCombos()
        Try
            ' Cargar combos utilizando la función compartida en General.vb
            ' Asumiendo nombres de tablas y campos estándar
            General.CargarCombos(CmbTipoVenta, "TipoVenta", "Descripcion", "Descripcion", "IdTipoVenta", "", True)
            General.CargarCombos(CmbCondicion, "CondicionVenta", "Descripcion", "Descripcion", "IdCondicionVenta", "", True)
            General.CargarCombos(CmbComprobante, "NumerosComprobantes", "Descripcion", "Descripcion", "IdImputacion", "", True)
            General.CargarCombos(CmbSucursal, "Sucursales", "Descripcion", "Descripcion", "IdSucursal", "", True)
            General.CargarCombos(CmbBanco, "Bancos", "Descripcion", "Descripcion", "IdBanco", "", True)
            General.CargarCombos(CmbPostal, "CodigosPostales", "Localidad", "Localidad", "CodigoPostal", "", True)
            General.CargarCombos(CmbTipoValor, "TipoValores", "Descripcion", "Descripcion", "IdTipoValor", "", True)
        Catch ex As Exception
            MessageBox.Show("Error al cargar combos: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CargarNovedades()
        Try
            Dim sql As String = "SELECT * FROM NoveCtaCte ORDER BY Puntodeventa, NroComprobante "
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

        Catch ex As Exception
            MessageBox.Show("Error al cargar novedades: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub chkAnterior_CheckedChanged(sender As Object, e As EventArgs) Handles chkAnterior.CheckedChanged
        CargarNovedades()
    End Sub


    Private Sub FormModoConsulta()
        SetControlesEnabled(True, cmdAgregar, cmdModificar, cmdBorrar, cmdCerrar, dgvNovedades, chkAnterior)
        SetControlesEnabled(False, cmdAceptar, cmdCancelar, txtNroCuenta, txtFecha, txtNroComprobante, txtMonto, txtNroCupon, txtNroFactura, txtInterno, txtRegInterno, txtFechaVto, txtNroCheque, txtBonificacion, txtObservaciones, CmbTipoVenta, CmbCondicion, CmbComprobante, CmbSucursal, CmbBanco, CmbPostal, CmbTipoValor)

    End Sub

    Private Sub FormModoEdicion()
        SetControlesEnabled(False, cmdAgregar, cmdModificar, cmdBorrar, cmdCerrar, dgvNovedades)
        SetControlesEnabled(True, cmdAceptar, cmdCancelar, txtNroCuenta, txtFecha, txtNroComprobante, txtMonto, txtNroCupon, txtNroFactura, txtInterno, txtRegInterno, txtFechaVto, txtNroCheque, txtBonificacion, txtObservaciones, CmbTipoVenta, CmbCondicion, CmbComprobante, CmbSucursal, CmbBanco, CmbPostal, CmbTipoValor, chkAnterior)

        txtNombre.ReadOnly = True ' Siempre readonly, se busca por cuenta
    End Sub

    Private Sub FormLimpiarSeleccionado()
        txtNroCuenta.Text = ""
        txtNombre.Text = ""
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
        chkAnterior.Checked = False

        CmbTipoVenta.SelectedIndex = -1
        CmbCondicion.SelectedIndex = -1
        CmbComprobante.SelectedIndex = -1
        CmbSucursal.SelectedIndex = -1
        CmbBanco.SelectedIndex = -1
        CmbPostal.SelectedIndex = -1
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
        If dgvNovedades.SelectedRows.Count = 0 Then
            MessageBox.Show("Seleccione un registro para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        esNuevo = False
        FormModoEdicion()
        ' Los datos ya deberían estar cargados por el SelectionChanged
        txtNroCuenta.Focus()
    End Sub

    Private Sub cmdBorrar_Click(sender As Object, e As EventArgs) Handles cmdBorrar.Click
        If dgvNovedades.SelectedRows.Count = 0 Then
            MessageBox.Show("Seleccione un registro para borrar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("¿Está seguro que desea borrar la novedad?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                Dim id As Integer = Convert.ToInt32(dgvNovedades.SelectedRows(0).Cells("IdNoveCtaCte").Value)
                Dim sql As String = "DELETE FROM NoveCtaCte WHERE IdNoveCtaCte = " & id
                DSM.ExecuteQuery(DataSourceManager.Lib.DataSourceManager.Stock, sql)
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

            ' Lógica de Punto de Venta según SucursalActual (Migrado de VB6)
            Dim puntoVenta As Integer = 0
            If General.SucursalActual = "Suc.Tierra del Fuego" Then
                puntoVenta = 13
            ElseIf General.SucursalActual = "Casa Central" Then
                puntoVenta = 26
            Else
                ' Valor por defecto si no coincide (opcional, usar 0 o lo que corresponda)
                If IsNumeric(General.PuntoVentaActual) Then puntoVenta = CInt(General.PuntoVentaActual)
            End If

            prms.Add("@IdCtaCte", _idCtaCteSeleccionada)
            prms.Add("@Fecha", Convert.ToDateTime(txtFecha.Text))
            prms.Add("@NroComprobante", If(String.IsNullOrEmpty(txtNroComprobante.Text), DBNull.Value, txtNroComprobante.Text))
            prms.Add("@Importe", Convert.ToDecimal(txtMonto.Text))
            prms.Add("@PuntodeVenta", puntoVenta)
            prms.Add("@IdImputacion", If(CmbComprobante.SelectedValue Is Nothing, DBNull.Value, CmbComprobante.SelectedValue))
            prms.Add("@Observaciones", txtObservaciones.Text)

            ' Nuevos parámetros
            prms.Add("@IdTipoVenta", If(CmbTipoVenta.SelectedValue Is Nothing, DBNull.Value, CmbTipoVenta.SelectedValue))
            prms.Add("@IdCondicionVenta", If(CmbCondicion.SelectedValue Is Nothing, DBNull.Value, CmbCondicion.SelectedValue))
            prms.Add("@IdSucursal", If(CmbSucursal.SelectedValue Is Nothing, DBNull.Value, CmbSucursal.SelectedValue))
            prms.Add("@IdBanco", If(CmbBanco.SelectedValue Is Nothing, DBNull.Value, CmbBanco.SelectedValue))
            prms.Add("@CodigoPostal", If(CmbPostal.SelectedValue Is Nothing, DBNull.Value, CmbPostal.SelectedValue))
            prms.Add("@IdTipoValor", If(CmbTipoValor.SelectedValue Is Nothing, DBNull.Value, CmbTipoValor.SelectedValue))

            prms.Add("@NroCheque", If(String.IsNullOrEmpty(txtNroCheque.Text), DBNull.Value, txtNroCheque.Text))
            prms.Add("@NroCupon", If(String.IsNullOrEmpty(txtNroCupon.Text), DBNull.Value, txtNroCupon.Text))
            prms.Add("@NroFactura", If(String.IsNullOrEmpty(txtNroFactura.Text), DBNull.Value, txtNroFactura.Text))
            prms.Add("@RegInterno", If(String.IsNullOrEmpty(txtRegInterno.Text), DBNull.Value, txtRegInterno.Text))

            Dim bonificacion As Decimal = 0
            Decimal.TryParse(txtBonificacion.Text, bonificacion)
            prms.Add("@Bonificacion", bonificacion)

            Dim impInterno As Decimal = 0
            Decimal.TryParse(txtInterno.Text, impInterno)
            prms.Add("@ImpInterno", impInterno)

            prms.Add("@Anterior", chkAnterior.Checked)

            If IsDate(txtFechaVto.Text) Then
                prms.Add("@FechaVto", Convert.ToDateTime(txtFechaVto.Text))
            Else
                prms.Add("@FechaVto", DBNull.Value)
            End If

            Dim sql As String = ""
            If esNuevo Then
                sql = "INSERT INTO NoveCtaCte (IdCtaCte, Fecha, NroComprobante, Importe, PuntodeVenta, IdImputacion, Observaciones, " &
                      "IdTipoVenta, IdCondicionVenta, IdSucursal, IdBanco, CodigoPostal, IdTipoValor, NroCheque, NroCupon, NroFactura, RegInterno, Bonificacion, ImpInterno, FechaVto, Anterior) " &
                      "VALUES (@IdCtaCte, @Fecha, @NroComprobante, @Importe, @PuntodeVenta, @IdImputacion, @Observaciones, " &
                      "@IdTipoVenta, @IdCondicionVenta, @IdSucursal, @IdBanco, @CodigoPostal, @IdTipoValor, @NroCheque, @NroCupon, @NroFactura, @RegInterno, @Bonificacion, @ImpInterno, @FechaVto, @Anterior)"
            Else
                sql = "UPDATE NoveCtaCte SET IdCtaCte=@IdCtaCte, Fecha=@Fecha, NroComprobante=@NroComprobante, " &
                      "Importe=@Importe, PuntodeVenta=@PuntodeVenta, IdImputacion=@IdImputacion, Observaciones=@Observaciones, " &
                      "IdTipoVenta=@IdTipoVenta, IdCondicionVenta=@IdCondicionVenta, IdSucursal=@IdSucursal, IdBanco=@IdBanco, CodigoPostal=@CodigoPostal, " &
                      "IdTipoValor=@IdTipoValor, NroCheque=@NroCheque, NroCupon=@NroCupon, NroFactura=@NroFactura, RegInterno=@RegInterno, " &
                      "Bonificacion=@Bonificacion, ImpInterno=@ImpInterno, FechaVto=@FechaVto, Anterior=@Anterior " &
                      "WHERE IdNoveCtaCte = " & _idNovedadSeleccionada
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

        If CmbTipoVenta.SelectedIndex = -1 Then
            MessageBox.Show("Debe seleccionar un tipo de venta.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CmbTipoVenta.Focus()
            Return False
        End If

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

        If Not String.IsNullOrEmpty(txtBonificacion.Text) AndAlso Not IsNumeric(txtBonificacion.Text) Then
            MessageBox.Show("La bonificación debe ser numérica.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtBonificacion.Focus()
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

    Private Sub Controls_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNroCuenta.KeyPress, txtFecha.KeyPress, txtNroComprobante.KeyPress, txtMonto.KeyPress, txtNroCupon.KeyPress, txtNroFactura.KeyPress, txtInterno.KeyPress, txtRegInterno.KeyPress, txtFechaVto.KeyPress, txtNroCheque.KeyPress, txtBonificacion.KeyPress, txtObservaciones.KeyPress, CmbTipoVenta.KeyPress, CmbCondicion.KeyPress, CmbComprobante.KeyPress, CmbSucursal.KeyPress, CmbBanco.KeyPress, CmbPostal.KeyPress, CmbTipoValor.KeyPress
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
            Dim sql As String = "SELECT IdCtaCte, Nombre FROM MaeCtaCte WHERE NroCuenta = " & txtNroCuenta.Text
            Dim dt As DataTable = DataSourceManager.Lib.DataSourceManager.ExecuteQuery(DataSourceManager.Lib.DataSourceManager.Stock, sql)

            If dt.Rows.Count > 0 Then
                _idCtaCteSeleccionada = Convert.ToInt32(dt.Rows(0)("IdCtaCte"))
                txtNombre.Text = dt.Rows(0)("Nombre").ToString()
            Else
                txtNombre.Text = "CUENTA INEXISTENTE"
                _idCtaCteSeleccionada = 0
            End If
        Catch ex As Exception
            ' Ignorar error o loguear
            txtNombre.Text = "Error al buscar"
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
        CmbPostal.SelectedIndex = -1
        CmbPostal.Text = String.Empty
        CmbTipoValor.SelectedIndex = -1
        CmbTipoValor.Text = String.Empty

        If filaActual IsNot Nothing Then
            Try
                _idNovedadSeleccionada = If(filaActual.Cells("IdNoveCtaCte").Value IsNot DBNull.Value, Convert.ToInt32(filaActual.Cells("IdNoveCtaCte").Value), 0)
                _idCtaCteSeleccionada = If(filaActual.Cells("IdCtaCte").Value IsNot DBNull.Value, Convert.ToInt32(filaActual.Cells("IdCtaCte").Value), 0)

                txtNroCuenta.Text = If(filaActual.Cells("NroCuenta").Value IsNot DBNull.Value, filaActual.Cells("NroCuenta").Value.ToString(), String.Empty)
                txtNombre.Text = If(filaActual.Cells("Nombre").Value IsNot DBNull.Value, filaActual.Cells("Nombre").Value.ToString(), String.Empty)

                If filaActual.Cells("Fecha").Value IsNot DBNull.Value Then
                    txtFecha.Text = Convert.ToDateTime(filaActual.Cells("Fecha").Value).ToString("dd/MM/yyyy")
                End If

                txtMonto.Text = If(filaActual.Cells("Importe").Value IsNot DBNull.Value, filaActual.Cells("Importe").Value.ToString(), "0.00")
                txtNroComprobante.Text = If(filaActual.Cells("NroComprobante").Value IsNot DBNull.Value, filaActual.Cells("NroComprobante").Value.ToString(), String.Empty)

                txtObservaciones.Text = If(filaActual.Cells("Observaciones").Value IsNot DBNull.Value, filaActual.Cells("Observaciones").Value.ToString(), String.Empty)

                CmbComprobante.SelectedValue = If(filaActual.Cells("IdImputacion").Value IsNot DBNull.Value, filaActual.Cells("IdImputacion").Value, Nothing)
                CmbTipoVenta.SelectedValue = If(filaActual.Cells("IdTipoVenta").Value IsNot DBNull.Value, filaActual.Cells("IdTipoVenta").Value, Nothing)
                CmbCondicion.SelectedValue = If(filaActual.Cells("IdCondicionVenta").Value IsNot DBNull.Value, filaActual.Cells("IdCondicionVenta").Value, Nothing)
                CmbSucursal.SelectedValue = If(filaActual.Cells("IdSucursal").Value IsNot DBNull.Value, filaActual.Cells("IdSucursal").Value, Nothing)
                CmbBanco.SelectedValue = If(filaActual.Cells("IdBanco").Value IsNot DBNull.Value, filaActual.Cells("IdBanco").Value, Nothing)
                CmbPostal.SelectedValue = If(filaActual.Cells("CodigoPostal").Value IsNot DBNull.Value, filaActual.Cells("CodigoPostal").Value, Nothing)
                CmbTipoValor.SelectedValue = If(filaActual.Cells("IdTipoValor").Value IsNot DBNull.Value, filaActual.Cells("IdTipoValor").Value, Nothing)

                txtNroCheque.Text = If(filaActual.Cells("NroCheque").Value IsNot DBNull.Value, filaActual.Cells("NroCheque").Value.ToString(), String.Empty)
                txtNroCupon.Text = If(filaActual.Cells("NroCupon").Value IsNot DBNull.Value, filaActual.Cells("NroCupon").Value.ToString(), String.Empty)
                txtNroFactura.Text = If(filaActual.Cells("NroFactura").Value IsNot DBNull.Value, filaActual.Cells("NroFactura").Value.ToString(), String.Empty)
                txtRegInterno.Text = If(filaActual.Cells("RegInterno").Value IsNot DBNull.Value, filaActual.Cells("RegInterno").Value.ToString(), String.Empty)

                txtBonificacion.Text = If(filaActual.Cells("Bonificacion").Value IsNot DBNull.Value, Convert.ToDecimal(filaActual.Cells("Bonificacion").Value).ToString("N2"), "0.00")
                txtInterno.Text = If(filaActual.Cells("ImpInterno").Value IsNot DBNull.Value, Convert.ToDecimal(filaActual.Cells("ImpInterno").Value).ToString("N2"), "0.00")

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
                System.Diagnostics.Debug.WriteLine("Error en FormObtenerSeleccionado: " & ex.Message)
            End Try
        End If
    End Sub

End Class
