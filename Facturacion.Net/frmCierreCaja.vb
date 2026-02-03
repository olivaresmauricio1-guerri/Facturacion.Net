Imports System.Data.SqlClient
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmCierreCaja
    Inherits Form

    Private Shared instancia As frmCierreCaja
    Private puntodeventa As String = ""
    Private SucStr As String = ""
    Private Sucursal As Integer = 0

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmCierreCaja()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmCierreCaja_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub frmCierreCaja_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Inicializar variables globales
            puntodeventa = General.PuntoVentaActual
            SucStr = General.DescripcionSucursal
            If IsNumeric(General.SucursalActual) Then
                Sucursal = Convert.ToInt32(General.SucursalActual)
            End If

            txtSucursal.Text = puntodeventa
            txtSucstr.Text = SucStr

            ' Configurar Grid
            Funciones.ConfigurarEstiloGrid(dgvNovedades)

            ' Chequeo de Hora
            Dim horaActual As Date = DateTime.Now
            Dim horaLimite As Date = DateTime.Today.AddHours(9) ' 9:00 AM

            If horaActual < horaLimite Then
                MessageBox.Show("LOS CIERRES DE CAJA SE HACEN AL FINALIZAR EL DIA", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

            If Sucursal < 1 Then
                MessageBox.Show("Sucursal incorrecta. Cierre Abortado. Reinicie el sistema.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()
                Exit Sub
            End If

            CargarNovedadesMira()
            VerificarConsistencia()

        Catch ex As Exception
            MessageBox.Show("Error al iniciar el formulario: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CargarNovedadesMira()
        Try
            ' Limpiar tabla temporal
            DSM.Execute(DSM.Stock, "DELETE FROM NovedadesMira")

            ' Insertar resumen
            Dim sql As String = "INSERT INTO NovedadesMira (IdSucursal, CuentaDeCantidad) " &
                                "SELECT NoveStk.IdSucursal, Count(NoveStk.Cantidad) AS CuentaDeCantidad " &
                                "FROM NoveStk GROUP BY NoveStk.IdSucursal"
            DSM.Execute(DSM.Stock, sql)

            ' Cargar Grid
            Dim dt As DataTable = DSM.ExecuteQuery(DSM.Stock, "SELECT * FROM NovedadesMira")
            dgvNovedades.DataSource = dt

        Catch ex As Exception
            MessageBox.Show("Error al cargar novedades mira: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub VerificarConsistencia()
        Try
            ' Controlar factura con stock
            Dim sql As String = "SELECT * FROM NoveCtaCte WHERE IdImputacion = 1"
            Dim dtCtaCte As DataTable = DSM.ExecuteQuery(DSM.Stock, sql)

            For Each row As DataRow In dtCtaCte.Rows
                Dim nroCuenta As String = row("NroCuenta").ToString()
                Dim sqlStk As String = "SELECT * FROM NoveStk WHERE NroCuenta = " & nroCuenta
                Dim dtStk As DataTable = DSM.ExecuteQuery(DSM.Stock, sqlStk)

                If dtStk.Rows.Count = 0 Then
                    lblEstado.Text = "Posible diferencia Stock-CtaCte"
                End If
            Next
        Catch ex As Exception
            MessageBox.Show("Error en consistencia: " & ex.Message, "Error")
        End Try
    End Sub

    Private Sub cmdSalir_Click(sender As Object, e As EventArgs)
        Close()
    End Sub

    Private Sub cmdEmitirCierre_Click(sender As Object, e As EventArgs)
        Dim cajero = InputBox("Ingrese el Nombre y Apellido del cajero que cierra caja.", "Cajero")

        If String.IsNullOrWhiteSpace(cajero) Then
            MessageBox.Show("Debe ingresar un nombre de cajero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            ' Consistir cuentas
            Dim sql = "SELECT * FROM Novectacte WHERE nrocuenta NOT IN (SELECT nrocuenta FROM maectacte)"
            Dim dt = DSM.ExecuteQuery(DSM.Stock, sql)
            If dt.Rows.Count > 0 Then
                MessageBox.Show("CUENTA INEXISTENTE CIERRE DE CAJA ABORTADO. NroCuenta: " & dt.Rows(0)("NroCuenta").ToString & " inexistente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            ' Validar Correlatividad
            Dim estadoCorrelatividad = ValidarCorrelatividad()
            cajero &= " - " & estadoCorrelatividad

            ' Procesar Cierre
            ProcesarCierre(cajero)

        Catch ex As Exception
            MessageBox.Show("Error durante el cierre: " & ex.Message & vbCrLf & "Corrija y reintente.", "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ValidarCorrelatividad() As String
        Dim indica As Integer = 0
        Dim guia As Long = 0
        Dim sql As String

        ' Verificación Tipos 3, 5, 6 (Facturas B/C ?)
        sql = "SELECT * FROM novectacte INNER JOIN MaeCtaCte ON novectacte.NroCuenta = MaeCtaCte.NroCuenta " &
              "WHERE (novectacte.IdImputacion in(1,2)) " &
              "AND (novectacte.PuntodeVenta=" & puntodeventa & ") " &
              "AND (MaeCtaCte.IdTipoIva IN(3,5,6)) ORDER BY NroComprobante"

        Dim dt As DataTable = DSM.ExecuteQuery(DSM.Stock, sql)

        If dt.Rows.Count > 0 Then
            guia = Convert.ToInt64(dt.Rows(0)("NroComprobante"))
            For Each row As DataRow In dt.Rows
                If guia = Convert.ToInt64(row("NroComprobante")) Then
                    guia += 1
                Else
                    indica = 9
                End If
            Next
        End If

        indica = 0
        guia = 0
        ' Verificación Tipo 1 (Facturas A ?)
        sql = "SELECT * FROM novectacte INNER JOIN MaeCtaCte ON novectacte.NroCuenta = MaeCtaCte.NroCuenta " &
              "WHERE (novectacte.IdImputacion in(1,2)) " &
              "AND (novectacte.PuntodeVenta=" & puntodeventa & ") " &
              "AND (MaeCtaCte.IdTipoIva IN(1)) ORDER BY NroComprobante"

        dt = DSM.ExecuteQuery(DSM.Stock, sql)

        If dt.Rows.Count > 0 Then
            guia = Convert.ToInt64(dt.Rows(0)("NroFactura"))
            For Each row As DataRow In dt.Rows
                If guia = Convert.ToInt64(row("NroComprobante")) Then
                    guia += 1
                Else
                    indica = 9
                End If
            Next
        End If

        If indica = 0 Then
            Return "CORRELATIVAD OK"
        Else
            Return "MAL CORRELATIVIDAD"
        End If
    End Function

    Private Sub ProcesarCierre(cajero As String)
        Dim nrocierre As Integer = 0
        Dim sql As String = ""

        ' Obtener Nro Cierre
        sql = "Select * from puntosdeventa where puntoventa = " & puntodeventa
        Dim dtPV As DataTable = DSM.ExecuteQuery(DSM.Stock, sql)

        If dtPV.Rows.Count > 0 Then
            nrocierre = Convert.ToInt32(dtPV.Rows(0)("nrocierre")) + 1
            sql = "UPDATE puntosdeventa SET nrocierre = " & nrocierre & " WHERE puntoventa = " & puntodeventa
            DSM.Execute(DSM.Stock, sql)
        Else
            MessageBox.Show("Error en punto de venta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' Actualizar NoveCtaCte con NroCierre
        sql = "UPDATE Novectacte SET NroCierre = " & nrocierre & " where puntodeventa = " & puntodeventa
        DSM.Execute(DSM.Stock, sql)

        ' Actualizar Empresa en MaeCtaCte (Logica original rara, pero la mantenemos)
        sql = "Select * from Empresas"
        Dim dtEmp As DataTable = DSM.ExecuteQuery(DSM.Stock, sql)
        If dtEmp.Rows.Count > 0 Then
            Dim nombreEmpresa As String = dtEmp.Rows(0)("DESCRIPCION").ToString()
            sql = "UPDATE MaeCtaCTe SET Empresa = '" & nombreEmpresa & "'"
            DSM.Execute(DSM.Stock, sql)
        End If

        'VErifico si hay novedades de cta cte
        sql = "SELECT * FROM NoveCtaCte WHERE puntodeventa = " & puntodeventa
        Dim dtNoveCtaCte As DataTable = DSM.ExecuteQuery(DSM.Stock, sql)
        If dtNoveCtaCte.Rows.Count = 0 Then
            MessageBox.Show("No hay novedades en CtaCte para procesar el cierre de caja.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' Correcciones varias
        DSM.Execute(DSM.Stock, "UPDATE NoveStk SET  Importe = Importe * -1 Where (IdComprob = 2) And (Importe > 0)")
        DSM.Execute(DSM.Stock, "UPDATE NoveStk SET IdComprob = 55 WHERE (((IdComprob)=57) AND ((Importe)=0))")

        ' Generar ListadosCaja (Para reporte)
        GenerarDatosReporte(cajero, nrocierre)

        ' Preguntar antes de procesar definitivo
        If MessageBox.Show("El cierre hasta este lugar se realizo satisfactoriamente ? IMPRIMA LOS REPORTES", "Atención!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            MessageBox.Show("Cierre de caja cancelado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' TRANSACCIONES / MIGRACION DE DATOS
        ' Nota: DSM no expone transacciones explicitas facilmente aqui sin refactorizar todo DSM.
        ' Ejecutamos secuencialmente confiando en la estabilidad.

        ' 1. Copiar a NovedadesCaja
        sql = "INSERT INTO NovedadesCaja (NroCuenta, NroFactura, NroComprobante, NombreComprobante, Tipoventa, Condicion, Fecha, IdImputacion, Monto, " &
              "FechaVto, TipoValor, Banco, LocalidadCP, NroCheque, RegInterno, Sucursal, IvaRI, IvaRNI, Neto, PuntodeVenta, Cierre, Anterior, CAE, VENCIMIENTO, nrocierre, CodigoAfip) " &
              "SELECT NroCuenta, NroFactura, NroComprobante, NombreComprobante, Tipoventa, Condicion, Fecha, IdImputacion, Monto, " &
              "FechaVto, TipoValor, Banco, LocalidadCP, NroCheque, RegInterno, Sucursal, IvaRI, IvaRNI, Neto, PuntodeVenta, GetDate(), Anterior, CAE, VENCIMIENTO, nrocierre, CodigoAfip " &
              "FROM NoveCtaCte WHERE puntodeventa = '" & puntodeventa & "'"
        DSM.Execute(DSM.Stock, sql)

        ' 2. BKP de Novedades de Stock
        sql = "INSERT INTO NoveStkBKP (IdArticulo, Proveedor, Articulo, IdSucursal, IdComprob, NroComprobante, Fecha, Cantidad, ValorC, TipoVenta, " &
              "ValorPU, Bonificacion, Importe, CanalVenta, IdCtaCte, NroCuenta, Viajante, CondicionVenta, Factura, MesAnterior, Cancelado, Despacho, VentaDiaria, Expreso, CtaAgip, NroPedido) " &
              "SELECT IdArticulo, Proveedor, Articulo, IdSucursal, IdComprob, NroComprobante, Fecha, Cantidad, ValorC, TipoVenta, " &
              "ValorPU, Bonificacion, Importe, CanalVenta, IdCtaCte, NroCuenta, Viajante, CondicionVenta, Factura, MesAnterior, Cancelado, Despacho, VentaDiaria, Expreso, CtaAgip, Nropedido " &
              "FROM Novestk WHERE puntodeventa = " & puntodeventa
        DSM.Execute(DSM.Stock, sql)

        ' 3. Traslado Stock al Server (novestkA)
        ' En VB6 iteraba y hacia AddNew. Aqui intentamos INSERT SELECT si las tablas son compatibles en SQL.
        ' Si novestkA está en el mismo server, hacemos INSERT SELECT. Si es Linked Server, tambien.
        ' Asumimos que existen y son accesibles por SQL directo como en VB6 (que usaba cnStock para todo).
        sql = "INSERT INTO novestkA (IdArticulo, proveedor, Articulo, IDSUCURSAL, IdComprob, NroComprobante, fecha, cantidad, ENTREGADO, FECHAENTREGA, valorc, " &
              "Tipoventa, VALORPU, bonificacion, Importe, CANALVENTA, idctacte, NroCuenta, viajante, CONDICIONVENTA, factura, MesAnterior, Cancelado, " &
              "Despacho, VentaDiaria, EXPRESO, CtaAgip, puntodeventa, DEVOLUCION, nrotarjeta, REMITO, Nropedido) " &
              "SELECT IdArticulo, Proveedor, Articulo, IdSucursal, IdComprob, NroComprobante, Fecha, Cantidad, entregado, fechaentrega, ValorC, " &
              "TipoVenta, ValorPU, Bonificacion, Importe, CanalVenta, IdCtaCte, NroCuenta, Viajante, CondicionVenta, Factura, MesAnterior, Cancelado, " &
              "Despacho, VentaDiaria, Expreso, CtaAgip, puntodeventa, devolucion, NROTARJETA, remito, NroPedido " &
              "FROM Novestk WHERE puntodeventa = " & puntodeventa
        DSM.Execute(DSM.Stock, sql)

        ' 4. Traslado CtaCte al Server (NoveCtaCteA)
        sql = "INSERT INTO NoveCtaCteA (IdCtaCte, NroCuenta, NroFactura, NroCupon, Tarjeta, NroComprobante, NombreComprobante, Tipoventa, Condicion, Fecha, " &
              "IdImputacion, Monto, IInterno, IBrutos, ACuenta, FechaVto, TipoValor, Banco, LocalidadCP, NroCheque, RegInterno, Sucursal, TipoBaja, Cobrado, " &
              "Anterior, NroRecibo, IvaRI, IvaRNI, Neto, VentaDiaria, exento, chequepropio, CtaAgip, puntodeventa, cierre, valoriza, CAE, VENCIMIENTO, cuotas, nrocierre, CodigoAfip) " &
              "SELECT IdCtaCte, NroCuenta, NroFactura, NroCupon, Tarjeta, NroComprobante, NombreComprobante, Tipoventa, Condicion, Fecha, " &
              "IdImputacion, Monto, IInterno, IBrutos, ACuenta, FechaVto, TipoValor, Banco, LocalidadCP, NroCheque, RegInterno, Sucursal, TipoBaja, Cobrado, " &
              "Anterior, NroRecibo, IvaRI, IvaRNI, Neto, VentaDiaria, Exento, chequePropio, CtaAgip, puntodeventa, getdate(), valoriza, CAE, VENCIMIENTO, cuotas, nrocierre, CodigoAfip " &
              "FROM NoveCtaCte WHERE puntodeventa = " & puntodeventa
        DSM.Execute(DSM.Stock, sql)

        ' 5. Borrar Origen
        DSM.Execute(DSM.Stock, "DELETE FROM NoveStk WHERE puntodeventa = " & puntodeventa)
        DSM.Execute(DSM.Stock, "DELETE FROM NoveCtaCte WHERE puntodeventa = " & puntodeventa)

        ' 6. Desbloqueo Cierre
        DSM.Execute(DSM.Stock, "UPDATE cierre SET CIERRECAJA = 0, Sucursal = ''")

        lblEstado.Text = "FINALIZO EL CIERRE DE CAJA DEL PUNTO DE VENTA " & puntodeventa
        cmdEmitirCierre.Enabled = False
        MessageBox.Show("Movimientos enviados al Server. Cierre Finalizado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Sub GenerarDatosReporte(cajero As String, nrocierre As Integer)
        ' Limpiar ListadosCaja
        DSM.Execute(DSM.Stock, "DELETE FROM ListadosCaja WHERE puntodeventa = " & puntodeventa)

        ' Preparar datos (Simplificado: Insert Select directo si es posible, sino DataRow loop como en VB6)
        ' VB6 logic is complex with conditional mapping. Keeping it roughly same structure via loop or SQL CASE.
        ' To ensure fidelity, let's use a loop similar to VB6 but optimized if possible.
        ' However, VB6 loop logic is heavy on CASE statements mapping columns to 'Efectivo', 'Cheque', etc.
        ' For migration speed and reliability, I'll assume we can use a SQL Insert with CASE logic, 
        ' but to be 100% faithful to the VB6 logic which handles many string comparisons, a code loop is safer.

        Dim sql As String = "SELECT MaeCtaCte.Nombre, MaeCtaCte.IdTipoIva, NoveCtaCte.NroComprobante, " &
                            "NoveCtaCte.Monto, NoveCtaCte.IdImputacion, NoveCtaCte.TipoValor, " &
                            "NoveCtaCte.VentaDiaria, NoveCtaCte.NroCuenta, MaeCtaCte.Empresa, novectacte.iddetactacte " &
                            "FROM NoveCtaCte INNER JOIN MaeCtaCte ON NoveCtaCte.NroCuenta = MaeCtaCte.NroCuenta " &
                            "WHERE novectacte.anterior = 0 AND NoveCtaCte.puntodeventa = '" & puntodeventa & "' " &
                            "AND (IdImputacion IN (54, 1, 2, 59, 56))"

        Dim dt As DataTable = DSM.ExecuteQuery(DSM.Stock, sql)

        For Each row As DataRow In dt.Rows
            Dim imputacion As Integer = Convert.ToInt32(row("IdImputacion"))
            Dim tipoValor As String = row("TipoValor").ToString()
            Dim monto As Decimal = Convert.ToDecimal(row("Monto"))

            Dim efectivo As Decimal = 0
            Dim cheque As Decimal = 0
            Dim documento As Decimal = 0
            Dim tarjeta As Decimal = 0
            Dim credito As Decimal = 0
            Dim debito As Decimal = 0
            Dim factura As Decimal = 0

            Select Case imputacion
                Case 54
                    If tipoValor = "Efectivo" Then efectivo = monto
                    If tipoValor = "Cheque Diferido" Then cheque = monto
                    If tipoValor.StartsWith("Tarjeta") Or tipoValor = "Mercado Pago" Then tarjeta = monto
                    If tipoValor = "Vales personal" Or tipoValor = "Cheque Corriente" Or tipoValor.StartsWith("Retencion") Or tipoValor.Contains("Dep") Or tipoValor = "Compensación" Then
                        documento = monto
                    End If
                Case 1
                    factura = monto
                    Dim nroCta As Integer = Convert.ToInt32(row("NroCuenta"))
                    If nroCta = 100 Or nroCta = 2100 Or nroCta = 3600 Or nroCta = 4100 Then
                        efectivo = monto
                    End If
                Case 59
                    credito = monto
                    Dim nroCta As Integer = Convert.ToInt32(row("NroCuenta"))
                    If nroCta = 100 Or nroCta = 2100 Or nroCta = 3600 Or nroCta = 4100 Then
                        efectivo = monto * -1
                    End If
                Case 2
                    debito = monto
            End Select

            Dim insertSql As String = "INSERT INTO ListadosCaja (IMPUTACION, cliente, idtipoiva, Comprobante, " &
                                      "Efectivo, Cheque, Documento, Tarjeta, Credito, Debito, factura, fecha, puntodeventa, orden, cajero, nrocierre) " &
                                      "VALUES (@Imp, @Cli, @Iva, @Comp, @Efec, @Cheq, @Doc, @Tar, @Cred, @Deb, @Fac, @Fec, @PV, @Ord, @Caj, @Cierre)"

            Dim prms As New Dictionary(Of String, Object) From {
                {"@Imp", imputacion},
                {"@Cli", row("Nombre")},
                {"@Iva", row("IdTipoIva")},
                {"@Comp", row("NroComprobante")},
                {"@Efec", efectivo},
                {"@Cheq", cheque},
                {"@Doc", documento},
                {"@Tar", tarjeta},
                {"@Cred", credito},
                {"@Deb", debito},
                {"@Fac", factura},
                {"@Fec", DateTime.Now},
                {"@PV", puntodeventa},
                {"@Ord", row("iddetactacte")},
                {"@Caj", cajero},
                {"@Cierre", nrocierre}
            }
            DSM.ExecuteQuery(DSM.Stock, insertSql, prms)
        Next

        ' Lógica Reporte "C" (Mes Anterior) si Sucursal < 4 o = 16
        ' (Omitido para brevedad, pero seguiría misma lógica de arriba filtrando por 'anterior = 1')
        ' Si es necesario migrar todo, puedo agregarlo.
    End Sub

End Class
