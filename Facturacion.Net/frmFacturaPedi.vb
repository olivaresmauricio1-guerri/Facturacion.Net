Imports System.Data.SqlClient
Imports System.Diagnostics.Eventing.Reader
Imports System.Net
Imports System.Reflection
Imports System.Reflection.Metadata.Ecma335
Imports DataSourceManager.Lib
Imports Microsoft.Data
Imports Microsoft.Identity.Client
Imports Microsoft.Identity.Client.Extensions
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmFacturaPedi
    Private Shared instancia As frmFacturaPedi = Nothing

    ' Variables para manejo de totales y lógica fiscal
    Private _Total As Double
    Private _Iva As Double
    Private _Neto As Double
    Private _Exento As Double
    Private NroPedido As Double
    Private NroCuenta As Long
    Dim plazoPactado As String = ""
    Dim Expreso As String = ""
    Dim Flete As String = ""

    ' Simulación del objeto HASAR (Fiscal Printer) para compilación
    ' En producción, esto debe ser reemplazado por la referencia real al OCX o DLL
    Private HASAR1 As Object = Nothing

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmFacturaPedi()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmFacturaPedi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConfigurarEstiloGrid(dgvPedidos)
        CargarPedidos()

        If SucursalActual <> 1 Then
            cmdBaja.Visible = False
        End If

    End Sub

    Private Sub ConfigurarGrid()
        dgvPedidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvPedidos.MultiSelect = True
        dgvPedidos.AllowUserToAddRows = False
        dgvPedidos.ReadOnly = True
        dgvPedidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

        ' Estilos básicos similares a VB6 (opcional)
        dgvPedidos.BackgroundColor = System.Drawing.SystemColors.Control
        dgvPedidos.BorderStyle = BorderStyle.Fixed3D
    End Sub

    Private Sub CargarPedidos()

        DSM.ExecuteQuery(DSM.Stock, "DELETE FROM PedidosFactura")

        Try
            ' Consulta basada en el RecordSource de VB6
            Dim sql As String = "INSERT INTO PedidosFactura ( NroPedido, Vendedor, Cliente, SumaDeEntregaActual, Bloqueado, DepEntrega, contenedor) " &
            "SELECT PedidosClientes.NroPedido, PedidosClientes.Vendedor, " &
            "PedidosClientes.Cliente, Sum(PedidosClientes.EntregaActual) AS SumaDeEntregaActual, " &
            "(MaeCtaCte.Bloqueado) AS PrimeroDeBloqueado, (PedidosClientes.DepEntrega) As depentrega, (PedidosClientes.contenedor) AS contenedor " &
            "FROM PedidosClientes INNER JOIN MaeCtaCte ON PedidosClientes.Cuenta = MaeCtaCte.NroCuenta " &
            "GROUP BY MaeCtaCte.Bloqueado,PedidosClientes.DepEntrega,PedidosClientes.NroPedido, PedidosClientes.Vendedor, PedidosClientes.Cliente, " &
            "PedidosClientes.contenedor HAVING (((Sum(PedidosClientes.EntregaActual))<>0)) AND contenedor = 0"

            DSM.Execute(DSM.Stock, sql)

            sql = "SELECT NroPedido, Vendedor, Cliente, SumadeEntregaActual as Entrega, Bloqueado FROM PedidosFactura ORDER BY Cliente"
            Dim dt As DataTable = DSM.ExecuteQuery(DSM.Stock, sql)
            dgvPedidos.DataSource = dt

            Dim dtPV As DataTable = DSM.ExecuteQuery(DSM.Stock, "SELECT * FROM Puntosdeventa WHERE puntoventa = " & PuntoVentaActual)
            If dtPV Is Nothing OrElse dtPV.Rows.Count <= 0 Then
                MessageBox.Show("ERROR EN PUNTO DE VENTA", "ERROR EN PUNTO DE VENTA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            If dtPV.Rows(0)("activo") = 0 Then
                MessageBox.Show("PUNTO DE VENTA NO HABLITADO PARA FACTURAR", "ERROR EN PUNTO DE VENTA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                cmdFacturar.Visible = False
                Return
            End If

            GridConfigurarColumnas()

            Select Case SucursalActual
                Case 1
                    optMza.Checked = True
                Case 3
                    optBsAs.Checked = True
                Case 14
                    optGaray.Checked = True
                Case 15
                    optNqn.Checked = True
            End Select

        Catch ex As Exception
            MessageBox.Show("Error al cargar pedidos: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdVer_Click(sender As Object, e As EventArgs) Handles cmdVer.Click
        CargarPedidos()
    End Sub

    Private Sub cmdSalir_Click(sender As Object, e As EventArgs) Handles cmdSalir.Click
        Me.Close()
    End Sub

    Private Sub cmdBaja_Click(sender As Object, e As EventArgs) Handles cmdBaja.Click
        If dgvPedidos.SelectedRows.Count = 0 Then
            MessageBox.Show("Seleccione un Pedido para dar de baja", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        If MessageBox.Show("¿Está seguro dar de Baja los pedidos seleccionados?", "Confirmar Baja", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                For Each row As DataGridViewRow In dgvPedidos.SelectedRows
                    Dim nroPedido As Integer = Convert.ToInt32(row.Cells("NroPedido").Value)
                    Dim sql As String = "SELECT * FROM PedidosClientes WHERE NroPedido = " & nroPedido
                    Dim dt As DataTable = DSM.ExecuteQuery(DSM.Stock, sql)

                    If SucursalActual > 4 AndAlso SucursalActual <> dt.Rows(0)("Sucursal") Then
                        MessageBox.Show("El pedido no pertenece a su Sucursal. No se puede dar de BAJA", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    End If

                    sql = "UPDATE PedidosClientes SET EntregaActual=0 WHERE NroPedido = " & nroPedido
                    DSM.ExecuteQuery(DSM.Stock, sql)

                Next
                CargarPedidos()
                MessageBox.Show("El Pedido fue dado de BAJA Correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Error al dar de baja: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
    Private Sub optBsAs_CheckedChanged(sender As Object, e As EventArgs) Handles optBsAs.CheckedChanged
        FiltraPedidos("BsAs")
    End Sub

    Private Sub optMza_CheckedChanged(sender As Object, e As EventArgs) Handles optMza.CheckedChanged
        FiltraPedidos("Matriz")
    End Sub

    Private Sub optGaray_CheckedChanged(sender As Object, e As EventArgs) Handles optGaray.CheckedChanged
        FiltraPedidos("Garay")
    End Sub

    Private Sub optNqn_CheckedChanged(sender As Object, e As EventArgs) Handles optNqn.CheckedChanged
        FiltraPedidos("Neuquen")
    End Sub

    Private Sub optLujan_CheckedChanged(sender As Object, e As EventArgs) Handles optLujan.CheckedChanged
        FiltraPedidos("Lujan")
    End Sub
    Private Sub cmdFacturar_Click(sender As Object, e As EventArgs) Handles cmdFacturar.Click
        If chkExtra.Checked Then
            If MessageBox.Show("Está por emitir una Factura Extraterritorial, ¿Desea continuar?", "Confirmar Extraterritorial", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                chkExtra.Checked = False
                Return
            End If
        End If
        If dgvPedidos.SelectedRows.Count = 0 Then
            MessageBox.Show("Debe Seleccionar un Pedido", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        If MessageBox.Show("Confirma la Facturación?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Return

        cmdFacturar.Enabled = False

        Try
            For Each row As DataGridViewRow In dgvPedidos.SelectedRows
                NroPedido = Convert.ToDouble(row.Cells("NroPedido").Value)
                ProcesarFacturacion(row)
            Next

            CargarPedidos()
        Catch ex As Exception
            MessageBox.Show("Error durante la facturación: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        cmdFacturar.Enabled = True
    End Sub
    Public Sub FiltraPedidos(sucur As String)
        Dim Sql = "SELECT NroPedido, Vendedor, Cliente, SumadeEntregaActual as Entrega, Bloqueado FROM PedidosFactura WHERE DepEntrega = '" & sucur & "' ORDER BY Cliente"
        Dim dt As DataTable = DSM.ExecuteQuery(DSM.Stock, Sql)
        dgvPedidos.DataSource = dt
    End Sub
    Private Sub ProcesarFacturacion(row As DataGridViewRow)

        'Valido el pedido antes de facturar
        If ValidadPedidoParaFacturar(NroPedido) = False Then Return

        'Obtengo el pedido a facturar
        Dim sqlPedido As String = "SELECT * FROM PedidosClientes WHERE NroPedido = " & NroPedido & " AND EntregaActual > 0"
        Dim dtPedido As DataTable = DSM.ExecuteQuery(DSM.Stock, sqlPedido)
        NroCuenta = Convert.ToInt64(dtPedido.Rows(0)("Cuenta"))

        'Obtengo el Cliente para saber si esta bloqueado 
        Dim datosCliente As ClienteFiscalData = Nothing
        Dim err As String = ""
        If Not ObtenerDatosFiscalesCliente(NroCuenta, UsuarioActual, datosCliente, err) Then
            MessageBox.Show("Error al obtener datos del cliente: " & err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If dtPedido.Rows(0)("PlazoPactado") <> "" AndAlso Not IsDBNull(dtPedido.Rows(0)("PlazoPactado")) Then
            plazoPactado = dtPedido.Rows(0)("PlazoPactado")
        End If

        If dtPedido.Rows(0)("Flete") <> "" AndAlso Not IsDBNull(dtPedido.Rows(0)("Flete")) Then
            Flete = dtPedido.Rows(0)("Flete")
        End If

        'Muestro detalle de pedido antes de facturar
        VerDetallePedido(NroPedido)

        For Each item As DataRow In dtPedido.Rows
            'Leo los articulos del maestk
            Dim sqlArticulo As String = "SELECT * FROM MaeStk WHERE Articulo = " & item("Articulo")
            Dim dtArticulo As DataTable = DSM.ExecuteQuery(DSM.Stock, sqlArticulo)
            If dtArticulo Is Nothing OrElse dtArticulo.Rows.Count = 0 Then
                MessageBox.Show("Artículo " & item("Articulo").ToString() & " inexistente -Tarea Cancelada-", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            'Obtengo el Propio,  y controlo que no exista para evitar duplicados
            If General.propio = 0 Then
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
            End If

            'Colocacion del nro de despacho
            Dim Resto As Integer = item("EntregaActual")
            Dim CantDespacho As Integer = 0
            Dim Despacho As String
            Dim sqlDespacho As String = "SELECT TOP (1) * FROM Despacho WHERE Articulo = @Articulo AND Cantidad > 0 ORDER BY Fecha DESC"
            Dim dtDespacho As DataTable = DSM.ExecuteQuery(DSM.Stock, sqlDespacho, CmdParams("@Articulo", item("Articulo")))

            If dtDespacho Is Nothing OrElse dtDespacho.Rows.Count = 0 Then
                MessageBox.Show("Artículo " & item("Articulo").ToString() & " sin Stock en Despachos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Despacho = ""
            Else
                Dim rowDespacho As DataRow = dtDespacho.Rows(0)
                Despacho = rowDespacho("Despacho").ToString()
                CantDespacho = Convert.ToInt32(rowDespacho("Cantidad")) - Resto
                If CantDespacho < 0 Then CantDespacho = 0

                Dim sqlUpdateDespacho As String =
                    "UPDATE Despacho " &
                    "SET Cantidad = @Cantidad " &
                    "WHERE Articulo = @Articulo " &
                    "AND Despacho = @Despacho " &
                    "AND Fecha = @Fecha"

                Dim parsUpdateDespacho = CmdParams(
                    "@Articulo", item("Articulo"),
                    "@Despacho", rowDespacho("Despacho"),
                    "@Fecha", rowDespacho("Fecha"),
                    "@Cantidad", CantDespacho
                )

                DSM.Execute(DSM.Stock, sqlUpdateDespacho, parsUpdateDespacho)
            End If


            If item.Table.Columns.Contains("plazopactado") AndAlso Not IsDBNull(item("plazopactado")) Then
                plazoPactado = item("plazopactado").ToString()
            End If

            Dim idCondicion As Integer = 1
            If Not String.IsNullOrWhiteSpace(plazoPactado) Then
                Dim dtCondicion As DataTable = DSM.ExecuteQuery(DSM.Stock, "SELECT Codigo, Descripcion FROM CondicionVenta WHERE Descripcion = @Descripcion", CmdParams("@Descripcion", plazoPactado))
                If dtCondicion IsNot Nothing AndAlso dtCondicion.Rows.Count > 0 Then
                    idCondicion = Convert.ToInt32(dtCondicion.Rows(0)("Codigo"))
                End If
            End If

            Dim precioUnitario As Double = 0
            If item.Table.Columns.Contains("preciounitario") AndAlso Not IsDBNull(item("preciounitario")) Then
                precioUnitario = Convert.ToDouble(item("preciounitario"))
            End If

            Dim cantidad As Double = Convert.ToDouble(item("EntregaActual"))
            Dim memTotal As Double = precioUnitario * cantidad

            Dim neto As Double = 0
            If datosCliente IsNot Nothing AndAlso datosCliente.PorcIva <> 0 Then
                neto = memTotal / datosCliente.PorcIva
            End If
            Dim ivaRI As Double = memTotal - neto

            Dim ivaRNI As Double = 0
            If datosCliente IsNot Nothing AndAlso datosCliente.PorcNoInscripto <> 0 Then
                ivaRNI = memTotal * datosCliente.PorcNoInscripto / 100
            End If

            Dim sumaIB As Double = 0
            If datosCliente IsNot Nothing AndAlso datosCliente.IIBBPorProvincia IsNot Nothing AndAlso Not String.Equals(UsuarioActual, "PEDRO", StringComparison.OrdinalIgnoreCase) Then
                For i As Integer = 1 To 24
                    If datosCliente.IIBBPorProvincia.Length > i AndAlso datosCliente.IIBBPorProvincia(i) <> 0 Then
                        sumaIB += memTotal * datosCliente.IIBBPorProvincia(i)
                    End If
                Next
            End If

            Dim descripcionArticulo As String = dtArticulo.Rows(0)("Descripcion").ToString()
            Dim descripcion As String = descripcionArticulo
            If Not String.IsNullOrEmpty(Despacho) AndAlso Despacho.Length > 2 Then
                Dim descParte As String = descripcionArticulo.Substring(0, Math.Min(33, descripcionArticulo.Length))
                Dim despachoParte As String = Despacho.Substring(0, Math.Min(16, Despacho.Length))
                descripcion = descParte & " " & despachoParte
            End If

            Dim idArticulo As Integer = 0
            If dtArticulo.Columns.Contains("IdArticulo") AndAlso Not IsDBNull(dtArticulo.Rows(0)("IdArticulo")) Then
                idArticulo = Convert.ToInt32(dtArticulo.Rows(0)("IdArticulo"))
            End If

            Dim idSucursal As Integer = 0
            If datosCliente IsNot Nothing Then
                idSucursal = datosCliente.IdSucursal
            End If

            'Inserto en Facturas
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
                "@IdCtaCte", NroCuenta,
                "@IdSucursal", idSucursal,
                "@IdTarjeta", 0,
                "@IdCondicion", idCondicion,
                "@IdTipoVenta", 2,
                "@Articulo", item("Articulo"),
                "@IdArticulo", idArticulo,
                "@Cantidad", cantidad,
                "@Descripcion", descripcion,
                "@Despacho", Despacho,
                "@ValorPU", precioUnitario,
                "@Descuento", 0,
                "@Neto", neto,
                "@IvaRI", ivaRI,
                "@IvaRNI", ivaRNI,
                "@IngBrutos", sumaIB,
                "@Total", memTotal,
                "@Fecha", Date.Today
            )

            DSM.Execute(DSM.Stock, sqlInsertFactura, parsInsertFactura)
        Next

        'Controlo que no haya articulos duplicados con el mismo propio
        Dim sqlDuplicados As String = "SELECT Articulo, COUNT(Articulo) AS Cuenta FROM Facturas WHERE IdPropio = " & General.propio & " GROUP BY Articulo HAVING COUNT(*) > 1"
        Dim dtDuplicados As DataTable = DSM.ExecuteQuery(DSM.Stock, sqlDuplicados)
        If dtDuplicados IsNot Nothing AndAlso dtDuplicados.Rows.Count > 0 Then
            Dim articulosDuplicados As String =
                String.Join(
                    vbCrLf,
                    dtDuplicados.AsEnumerable().[Select](
                        Function(r)
                            Return r("Articulo").ToString() & " (x" & Convert.ToInt32(r("Cuenta")) & ")"
                        End Function
                    )
                )

            MessageBox.Show("Artículos duplicados:" & vbCrLf & articulosDuplicados, "Error de Duplicidad", MessageBoxButtons.OK, MessageBoxIcon.Error)
            DSM.ExecuteQuery(DSM.Stock, "DELETE FROM Facturas WHERE IdPropio = " & General.propio)
            General.propio = 0
            Exit Sub
        End If

        'Procedo a Facturar
        Dim NroNotaVenta As Long
        Dim Emite As Boolean
        Dim copias As Integer
        Dim SucuPrt As Integer
        Dim idImputa As Integer
        Dim NroFactura As Long

        'Totales
        Dim Total = Totales(idPropio:=General.propio,
            porcIva:=datosCliente.PorcIva,
            porcNI:=datosCliente.PorcNoInscripto,
            usuario:=UsuarioActual,
            tablaIbrutos:=datosCliente.IIBBPorProvincia,
            nroCuenta:=NroCuenta,
            nroFactura:=NroFactura,
            puntoDeVenta:=PuntoVentaActual,
            tipocomp:=datosCliente.Tipocomp)


        'Leo el punto de venta para obtener parametros
        Dim dtPV As DataTable = DSM.ExecuteQuery(DSM.Stock, "SELECT * FROM Puntosdeventa WHERE puntoventa = " & PuntoVentaActual)
        If dtPV Is Nothing OrElse dtPV.Rows.Count <= 0 Then
            MessageBox.Show("ERROR EN PUNTO DE VENTA", "ERROR EN PUNTO DE VENTA", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        PruebaElec = dtPV.Rows(0)("PruebaElec")

        If UsuarioActual = "PEDRO" Then
            Emite = False
            copias = 1
            idImputa = 57
        Else
            Emite = True
            copias = 1
            idImputa = 1
        End If

        'Obtengo el nro de comprobante a emitir
        Dim sqlNroCpte As String
        If idImputa = 57 Then
            sqlNroCpte = "SELECT * FROM NumerosComprobantes WHERE ImputaStk = 57 "
        Else
            sqlNroCpte = "SELECT * FROM NumerosComprobantes WHERE ImputaCC = 1 "
        End If
        sqlNroCpte &= " AND idPunto = " & PuntoVentaActual
        Dim dtNroCpte As DataTable = DSM.ExecuteQuery(DSM.Stock, sqlNroCpte)

        If dtNroCpte Is Nothing OrElse dtNroCpte.Rows.Count <= 0 Then
            MessageBox.Show("ERROR AL OBTENER NUMERO DE COMPROBANTE. PUNTO DE VENTA INEXISTENTE", "ERROR AL OBTENER NUMERO DE COMPROBANTE", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        SucuPrt = Convert.ToInt32(dtNroCpte.Rows(0)("idsucursal"))


        If UsuarioActual = "PEDRO" Then
            Dim nroComprobC = Convert.ToInt32(dtNroCpte.Rows(0)("NroComprobC"))
            NroNotaVenta = nroComprobC + 1

        Else
            Select Case datosCliente.TipoIva
                Case 7
                    NroFactura = Convert.ToInt64(dtNroCpte.Rows(0)("NroComprobE")) + 1
                Case 1, 2
                    NroFactura = Convert.ToInt64(dtNroCpte.Rows(0)("NroComprobA")) + 1
                Case 2 To 6
                    NroFactura = Convert.ToInt64(dtNroCpte.Rows(0)("NroComprobB")) + 1
            End Select
        End If

        'Verifico la conexion a internet
        If Not NetworkHelper.VerificarInternet() Then Exit Sub

        Dim nroUnificado As String
        If idImputa = 57 Then
            nroUnificado = UnificarNro(PuntoVentaActual, CLng(NroNotaVenta))
        Else
            nroUnificado = UnificarNro(PuntoVentaActual, CLng(NroFactura))
        End If

        'Inserto en Leyendas
        Dim sqlLeyendas As String = "INSERT INTO Leyendas (IdPropio, Total1, Total2, Total3,
                                    Total4, Total5, NroComprobante, TipoComprobante, EXPRESO, flete)
                                VALUES
                                (@IdPropio, CASE WHEN @TotG <> 0 THEN @TotG ELSE 0 END,
                                    CASE WHEN @TotIB <> 0 THEN @TotIB ELSE 0 END,
                                    CASE WHEN @TotI <> 0 THEN @TotI ELSE 0 END,
                                    CASE WHEN @TotNI <> 0 THEN @TotNI ELSE 0 END,
                                    CASE WHEN @Total <> 0 THEN @Total ELSE 0 END,
                                    @NroComprobante,
                                    @TipoComprobante,
                                    @Expreso,
                                    @Flete)"
        Dim parsLeyendas = CmdParams("@IdPropio", General.propio,
                                     "@TotG", Total.TotG,
                                     "@TotIB", Total.TotIB,
                                     "@TotI", Total.TotI,
                                     "@TotNI", Total.TotNI,
                                     "@Total", Total.TotalFinal,
                                     "@NroComprobante", nroUnificado,
                                     "@TipoComprobante", plazoPactado,
                                     "@Expreso", Expreso,
                                     "@Flete", Flete)
        DSM.Execute(DSM.Stock, sqlLeyendas, parsLeyendas)

        Dim resultadoFe As FEResultadoProceso = Nothing

        'Factura Electronica y usuario <> PEDRO
        If dtPV.Rows(0)("EsElectronica").Equals(True) AndAlso Not String.Equals(UsuarioActual, "PEDRO", StringComparison.OrdinalIgnoreCase) Then

            If FacturacionElectronica.EmisorComprobante Is Nothing Then
                FacturacionElectronica.ConfigurarEmisorAfipWsfe()
            End If

            Dim solicitud As New FESolicitudComprobante With {
                .IdPropio = General.propio,
                .PuntoVenta = Convert.ToInt32(PuntoVentaActual),
                .EsPrueba = Convert.ToBoolean(dtPV.Rows(0)("PruebaElec")),
                .TipoComprobante = datosCliente.Tipocomp,
                .EsFce = False,
                .NroComprobante = NroFactura,
                .NroCuenta = NroCuenta,
                .Cliente = datosCliente,
                .Totales = Total,
                .Articulos = Nothing
            }

            resultadoFe = FacturacionElectronica.ProcesarComprobanteElectronico(solicitud)

            If Not resultadoFe.Exito Then
                MessageBox.Show("Error al emitir factura electrónica: " & resultadoFe.Mensaje, "Error de Facturación Electrónica", MessageBoxButtons.OK, MessageBoxIcon.Error)
                EliminarTemporales(NroFactura, datosCliente.NroCuenta, PuntoVentaActual, datosCliente.Tipocomp, General.propio)
                Return
            Else
                MessageBox.Show("Factura electrónica " & NroFactura & " emitida correctamente.", "Factura Electrónica", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        End If

        Dim sqlFacturas As String = "Select * FROM Facturas WHERE IdPropio = " & General.propio
        Dim dtFacturas As DataTable = DSM.ExecuteQuery(DSM.Stock, sqlFacturas)
        If dtFacturas Is Nothing OrElse dtFacturas.Rows.Count = 0 Then
            MessageBox.Show("Error conexión con Stock, Procedimiento anulado.", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim sqlNoveStk As String
        Dim sqlMaeStk As String
        Dim dtMaeStk As DataTable
        Dim sqlNoveCC As String

        If NroNotaVenta = 0 Then
            NroNotaVenta = NroFactura
        End If

        Dim Anterior As Boolean = False
        If String.Equals(UsuarioActual, "PEDRO", StringComparison.OrdinalIgnoreCase) Then
            NroFactura = 0
            Anterior = True
        End If

        'Obtengo el viajante del cliente para cargarlo en la Novedad de Stock
        Dim sqlViajante As String = "Select * FROM Viajantes WHERE codigo = " & datosCliente.idVendedor
        Dim dtViajante As DataTable = DSM.ExecuteQuery(DSM.Stock, sqlViajante)


        'Grabo Novedad de Stock por cada articulo facturado
        For Each item As DataRow In dtFacturas.Rows
            sqlMaeStk = "Select * FROM MaeStk WHERE Articulo = " & item("Articulo") & ""
            dtMaeStk = DSM.ExecuteQuery(DSM.Stock, sqlMaeStk)

            Dim valorc As Double = 0
            If chkExtra.Checked Then
                valorc = (Convert.ToDouble(dtMaeStk.Rows(0)("Costo")) * Convert.ToDouble(item("Cantidad")) * Convert.ToDouble(item("FOB"))) / 1.21
            ElseIf Convert.ToInt32(item("idSucursal")) = 2 Then
                valorc = Convert.ToDouble(dtMaeStk.Rows(0)("FOB")) * Convert.ToDouble(item("Cantidad"))
            Else
                valorc = Convert.ToDouble(dtMaeStk.Rows(0)("FOB")) * Convert.ToDouble(dtMaeStk.Rows(0)("Costo")) * Convert.ToDouble(item("Cantidad"))
            End If

            sqlNoveStk = "INSERT INTO NoveStk (" &
            "IdArticulo, proveedor, Articulo, idsucursal, IdComprob, NroComprobante, fecha, cantidad, valorc, " &
            "Tipoventa, VALORPU, bonificacion, Importe, idctacte, NroCuenta, Viajante, CONDICIONVENTA, factura, " &
            "MesAnterior, Cancelado, EXPRESO, Despacho, puntodeventa, Nropedido" &
            ") VALUES (" &
            "@IdArticulo, @proveedor, @Articulo, @idsucursal, @IdComprob, @NroComprobante, @fecha, @cantidad, @valorc, " &
            "@Tipoventa, @VALORPU, @bonificacion, @Importe, @idctacte, @NroCuenta, @Viajante, @CONDICIONVENTA, @factura, " &
            "@MesAnterior, @Cancelado, @EXPRESO, @Despacho, @puntodeventa, @Nropedido" &
            ")"

            Dim parsNoveStk = CmdParams(
            "@IdArticulo", Convert.ToInt32(item("IdArticulo")),
            "@proveedor", dtMaeStk.Rows(0)("Proveedor"),
            "@Articulo", item("Articulo"),
            "@idsucursal", Convert.ToInt32(item("idSucursal")),
            "@IdComprob", 57,
            "@NroComprobante", NroNotaVenta,
            "@fecha", Date.Today,
            "@cantidad", Convert.ToDouble(item("Cantidad") * -1),
            "@valorc", valorc,
            "@Tipoventa", "Cuenta Corriente",
            "@VALORPU", Convert.ToDouble(item("valorPU")),
            "@bonificacion", (Convert.ToDouble(item("Total")) * Convert.ToDouble(item("bonificacion"))) / 100,
            "@Importe", Convert.ToDouble(item("Total")),
            "@idctacte", SucursalActual,
            "@NroCuenta", NroCuenta,
            "@Viajante", dtViajante.Rows(0)("Descripcion").ToString(),
            "@CONDICIONVENTA", plazoPactado,
            "@factura", NroFactura,
            "@MesAnterior", False,
            "@Cancelado", False,
            "@EXPRESO", "Propio",
            "@Despacho", item("Despacho"),
            "@puntodeventa", PuntoVentaActual,
            "@Nropedido", NroPedido)

            DSM.Execute(DSM.Stock, sqlNoveStk, parsNoveStk)
        Next

        Dim NroFac As Integer = NroFactura
        If String.Equals(UsuarioActual, "PEDRO", StringComparison.OrdinalIgnoreCase) Then
            NroFac = NroNotaVenta
        End If

        'Grabo Novedad en Cuenta Corriente
        sqlNoveCC = "INSERT INTO NoveCtaCte (idCtaCte, NroCuenta, Puntodeventa, NroFactura, NroComprobante, NombreComprobante, TipoVenta, Condicion, Valoriza, " &
                    "VentaDiaria, Fecha, idIMputacion, Monto, IInterno, IBrutos, Acuenta, TipoValor,Banco, LocalidadCP, NroCheque, Reginterno, Sucursal, TipoBaja, " &
                    "Cobrado, Anterior, IvaRI, IvaRNI, Neto, CtaAgip, Exento, Cae, Vencimiento, Codigoafip" &
                    ") VALUES (@idCtaCte, @NroCuenta, @Puntodeventa, @NroFactura, @NroComprobante, @NombreComprobante, @TipoVenta, @Condicion, @Valoriza, " &
                    "@VentaDiaria, @Fecha, @idIMputacion, @Monto, @IInterno, @IBrutos, @Acuenta, @TipoValor, @Banco, @LocalidadCP, @NroCheque, @Reginterno, @Sucursal, @TipoBaja, " &
                    "@Cobrado, @Anterior, @IvaRI, @IvaRNI, @Neto, @CtaAgip, @Exento, @Cae, @Vencimiento, @Codigoafip)"
        Dim parsNoveCC = CmdParams(
            "@idCtaCte", datosCliente.IdCtaCte,
            "@NroCuenta", NroCuenta,
            "@Puntodeventa", PuntoVentaActual,
            "@NroFactura", NroFac,
            "@NroComprobante", NroFac,
            "@NombreComprobante", "Factura",
            "@TipoVenta", plazoPactado,
            "@Condicion", plazoPactado,
            "@Valoriza", dtPedido.Rows(0)("FechaV"),
            "@VentaDiaria", True,
            "@Fecha", Date.Today,
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
            "@Anterior", If(chkExtra.Checked, False, Anterior),
            "@IvaRI", If(chkExtra.Checked, 0, Total.TotI),
            "@IvaRNI", If(chkExtra.Checked, 0, Total.TotNI),
            "@Neto", If(chkExtra.Checked, 0, Total.TotG),
            "@CtaAgip", False,
            "@Exento", If(chkExtra.Checked, True, False),
            "@Cae", If(resultadoFe Is Nothing, "", resultadoFe.Cae),
            "@Vencimiento", If(resultadoFe Is Nothing, "", resultadoFe.Venicimiento),
            "@Codigoafip", If(resultadoFe Is Nothing, 0, resultadoFe.CodigoAfip)
)
        DSM.Execute(DSM.Stock, sqlNoveCC, parsNoveCC)

        Dim codAfip As Integer = If(resultadoFe IsNot Nothing AndAlso resultadoFe.Exito, resultadoFe.CodigoAfip, datosCliente.Tipocomp)

        Dim nroCompReal As Long = NroFactura
        If idImputa = 57 Then
            nroCompReal = NroNotaVenta
        End If

        Funciones.GenerarPdfFactura(
                nroCuenta:=NroCuenta,
                puntoVenta:=PuntoVentaActual,
                nroComprobante:=nroCompReal,
                codigoAfip:=codAfip,
                datosCliente:=datosCliente,
                totales:=Total,
                dtItems:=dtFacturas,
                cae:=If(resultadoFe Is Nothing, "", resultadoFe.Cae),
                vencimientoCae:=If(resultadoFe Is Nothing, "", resultadoFe.Venicimiento),
                vendedor:=If(dtViajante Is Nothing OrElse dtViajante.Rows.Count = 0, "", dtViajante.Rows(0)("Descripcion").ToString()),
                nroPedido:=CLng(NroPedido),
                esPresupuesto:=String.Equals(UsuarioActual, "PEDRO", StringComparison.OrdinalIgnoreCase)
            )


        'Poner factura en pedido
        Dim sqlUpdatePedido As String = "UPDATE PedidosClientes Set NroFactura = @NroFactura, Puntodeventa = @Puntodeventa WHERE NroPedido = @NroPedido And entregaactual <> 0"
        DSM.Execute(DSM.Stock, sqlUpdatePedido, CmdParams("@NroFactura", NroFac, "@Puntodeventa", PuntoVentaActual, "@NroPedido", NroPedido))

        'Bajar pedido
        Dim sqlBajaPedido As String = "UPDATE PedidosClientes Set EntregaActual = 0 WHERE NroPedido = @NroPedido"
        DSM.Execute(DSM.Stock, sqlBajaPedido, CmdParams("@NroPedido", NroPedido))

        'Actualizo nro de comprobante en NumerosComprobantes
        If idImputa = 57 Then
            DSM.Execute(
                DSM.Stock,
                "UPDATE NumerosComprobantes " &
                "Set NroComprobC = NroComprobC + 1 " &
                "WHERE ImputaStk = @Imputa And idPunto = @IdPunto",
                CmdParams("@Imputa", idImputa, "@IdPunto", PuntoVentaActual))
        Else
            Select Case datosCliente.TipoIva
                Case 7
                    DSM.Execute(
                        DSM.Stock,
                        "UPDATE NumerosComprobantes " &
                        "Set NroComprobE = NroComprobE + 1 " &
                        "WHERE ImputaCC = 1 And idPunto = @IdPunto",
                        CmdParams("@IdPunto", PuntoVentaActual))
                Case 1, 6
                    DSM.Execute(
                        DSM.Stock,
                        "UPDATE NumerosComprobantes " &
                        "Set NroComprobA = NroComprobA + 1 " &
                        "WHERE ImputaCC = 1 And idPunto = @IdPunto",
                        CmdParams("@IdPunto", PuntoVentaActual))
                Case 2 To 5
                    DSM.Execute(
                        DSM.Stock,
                        "UPDATE NumerosComprobantes " &
                        "Set NroComprobB = NroComprobB + 1 " &
                        "WHERE ImputaCC = 1 And idPunto = @IdPunto",
                        CmdParams("@IdPunto", PuntoVentaActual))
            End Select
        End If

        'Grabar datos del transporte
        Dim sqlTransporte As String = "INSERT INTO transportePedido (Factura, NroCuenta,  Fecha, Transporte, Nropedido, Sucursal) " &
                                      "VALUES (@NroFactura, @NroCuenta, @Fecha, @Transporte, @NroPedido, @Sucursal)"
        Dim parsTransporte = CmdParams(
            "@NroPedido", NroPedido,
            "@NroFactura", NroFac,
            "@NroCuenta", NroCuenta,
            "@Fecha", Date.Today,
            "@Transporte", dtPedido.Rows(0)("Transporte"),
            "@Sucursal", SucursalActual
        )
        DSM.Execute(DSM.Stock, sqlTransporte, parsTransporte)

        'Actualizo Stockglobal
        Dim sqlStockGlobal As String
        Dim Descarga As String = dtPV.Rows(0)("Deposito").ToString()
        For Each item As DataRow In dtFacturas.Rows
            sqlStockGlobal =
                "UPDATE StockGlobal " &
                "Set " & Descarga & " = ISNULL(" & Descarga & ", 0) + @Cantidad, " &
                "afact = Case When afact Is NULL Then afact Else afact + @Cantidad End " &
                "WHERE Articulo = @Articulo"
            Dim parsStockGlobal = CmdParams(
                "@Cantidad", Convert.ToDouble(item("Cantidad")) * -1,
                "@Articulo", item("Articulo")
            )
            DSM.Execute(DSM.Stock, sqlStockGlobal, parsStockGlobal)
        Next

        Dim sqlElimina = "Delete from Facturas where IdPropio=" & propio
        DSM.Execute(DSM.Stock, sqlElimina)

        sqlElimina = "Delete from Leyendas where IdPropio=" & propio
        DSM.Execute(DSM.Stock, sqlElimina)

        'Rebloquea el cliente

        Dim sqlBloqueo As String = "UPDATE MaeCtaCte SET Bloqueado = 1 WHERE NroCuenta = @NroCuenta"
        'DSM.Execute(DSM.Stock, sqlBloqueo, CmdParams("@NroCuenta", datosCliente.NroCuenta))

        'Cargo pedidos
        CargarPedidos()

    End Sub

    Private Function ValidadPedidoParaFacturar(nroPedido As Double) As Boolean
        ' 1. Controlo duplicidad del articulo en el pedido 
        Dim sqlDuplicado As String = "Select NroPedido, Articulo, COUNT(Articulo) As Cuenta FROM PedidosClientes WHERE NroPedido = " & nroPedido & " GROUP BY NroPedido, Articulo HAVING COUNT(Articulo) > 1"
        Dim dtDuplicado As DataTable = DSM.ExecuteQuery(DSM.Stock, sqlDuplicado)

        If dtDuplicado IsNot Nothing AndAlso dtDuplicado.Rows.Count > 0 Then
            Dim articulosDuplicados As String = String.Join(", ", dtDuplicado.AsEnumerable().[Select](Function(r) r("Articulo").ToString()))
            MessageBox.Show("El pedido contiene artículos duplicados: " & articulosDuplicados & ". Por favor revise el pedido antes de facturar.", "Error de Duplicidad", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        ' 2. Control que el pedido no tenga mas de 24 lineas
        Dim sqlLineas As String = "SELECT COUNT(NroPedido) FROM PedidosClientes WHERE NroPedido = " & nroPedido & " And Entregaactual > 0"
        Dim dtLineas As DataTable = DSM.ExecuteQuery(DSM.Stock, sqlLineas)
        Dim cantidadLineas As Integer = Convert.ToInt32(dtLineas.Rows(0)(0))

        If cantidadLineas > 24 Then
            MessageBox.Show("El pedido tiene " & cantidadLineas & " líneas. No se puede facturar pedidos con más de 24 líneas.", "Error de Líneas", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        ' 3. Control que el pedido tenga articulos para facturar (EntregaActual > 0)
        If cantidadLineas = 0 Then
            MessageBox.Show("El pedido no tiene artículos para facturar.", "Error de Facturación", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        ' 4. Recorro dtLineas para controlar que el articulo no haya sido totalemten facturado (NroFactura<>0 y CAntidadpedida = Cantidadentregada)
        Dim sqlDetalle As String = "SELECT NroPedido, Articulo, CantidadPedida, EntregaActual, NroFactura, Contenedor FROM PedidosClientes WHERE NroPedido = " & nroPedido & " And EntregaActual > 0"
        Dim dtDetalle As DataTable = DSM.ExecuteQuery(DSM.Stock, sqlDetalle)
        Dim articulosFacturarados As New List(Of String)

        For Each item As DataRow In dtDetalle.Rows
            If Convert.ToInt32(item("NroFactura")) <> 0 AndAlso Convert.ToDouble(item("CantidadPedida")) = Convert.ToDouble(item("EntregaActual")) Then
                articulosFacturarados.Add(item("Articulo").ToString())
            End If
        Next
        If articulosFacturarados.Count > 0 Then
            MessageBox.Show("Los siguientes artículos ya fueron totalmente facturados: " & String.Join(", ", articulosFacturarados) & ". Por favor revise el pedido antes de facturar.", "Error de Facturación", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        ' 5. Control que el pedido no sea un remito de uso interno (contenedor = True)
        If dtDetalle.Rows(0)("contenedor") = True Then
            MessageBox.Show("Usted seleccionó un Remito de Uso Interno para facturar.", "Error de Facturación", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Return True

    End Function
    Private Sub ActualizarStock(idArticulo As Integer, cantidad As Double)
        ' Lógica para descontar stock
        Dim sql As String = "UPDATE Articulos SET Stock = Stock - " & cantidad.ToString().Replace(",", ".") & " WHERE IdArticulo = " & idArticulo
        DSM.ExecuteQuery(DSM.Stock, sql)
    End Sub

    Private Function ImprimirTicketFiscal(nroPedido As Integer, dtDetalle As DataTable) As Boolean
        ' TODO: Integrar lógica de controlador fiscal HASAR
        ' Aquí iría la inicialización del OCX y el envío de comandos
        ' Por ahora retornamos True para simular éxito
        Return True
    End Function

    Private Sub RegistrarEnCtaCte(rowPedido As DataGridViewRow, monto As Double)
        ' Insertar movimiento en NoveCtaCte
        Try
            Dim nroCuenta As Integer = 0
            If Not IsDBNull(rowPedido.Cells("NroCuenta").Value) Then
                nroCuenta = Convert.ToInt32(rowPedido.Cells("NroCuenta").Value)
            End If

            Dim fecha As String = DateTime.Now.ToString("yyyy-MM-dd")
            Dim comprobante As String = "FAC" ' Ejemplo

            ' SQL simplificado - Ajustar campos según estructura real de NoveCtaCte
            Dim sql As String = String.Format("INSERT INTO NoveCtaCte (NroCuenta, Fecha, Monto, NroComprobante, TipoVenta) VALUES ({0}, '{1}', {2}, {3}, '{4}')",
                                              nroCuenta, fecha, monto.ToString().Replace(",", "."), rowPedido.Cells("NroPedido").Value, "Cuenta Corriente")

            DSM.ExecuteQuery(DSM.Stock, sql)
        Catch ex As Exception
            ' Log error pero no detener proceso masivo si es posible
            Console.WriteLine("Error al registrar en CtaCte: " & ex.Message)
        End Try
    End Sub

    ' Helpers para opciones
    Private Function ObtenerSucursal() As String
        If optMza.Checked Then Return "Mendoza"
        If optBsAs.Checked Then Return "Buenos Aires"
        If optGaray.Checked Then Return "Garay"
        If optNqn.Checked Then Return "Neuquen"
        If optLujan.Checked Then Return "Lujan"
        Return ""
    End Function
    Public Sub GridConfigurarColumnas()
        Dim grid = dgvPedidos

        For Each col As DataGridViewColumn In grid.Columns
            col.Visible = False
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        Next


        If grid.Columns.Contains("NroPedido") Then
            grid.Columns("NroPedido").Visible = True
            grid.Columns("NroPedido").HeaderText = "Nro.Pedido"
            grid.Columns("NroPedido").Width = 80
            grid.Columns("NroPedido").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        End If

        If grid.Columns.Contains("Vendedor") Then
            grid.Columns("Vendedor").Visible = True
            grid.Columns("Vendedor").HeaderText = "Vendedor"
            grid.Columns("Vendedor").Width = 150
            grid.Columns("Vendedor").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        End If

        If grid.Columns.Contains("Cliente") Then
            grid.Columns("Cliente").Visible = True
            grid.Columns("Cliente").HeaderText = "Cliente"
            grid.Columns("Cliente").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End If

        If grid.Columns.Contains("Entrega") Then
            grid.Columns("Entrega").Visible = True
            grid.Columns("Entrega").HeaderText = "Entrega"
            grid.Columns("Entrega").Width = 70
            grid.Columns("Entrega").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End If

        If grid.Columns.Contains("Bloqueado") Then
            Dim colBloqueado = grid.Columns("Bloqueado")
            Dim colBloqueadoIndex = colBloqueado.Index
            If Not TypeOf colBloqueado Is DataGridViewCheckBoxColumn Then
                grid.Columns.Remove(colBloqueado)
                Dim chkCol As New DataGridViewCheckBoxColumn()
                grid.Columns.Insert(colBloqueadoIndex, chkCol)
            End If
            grid.Columns("Bloqueado").Visible = True
            grid.Columns("Bloqueado").HeaderText = "Bloqueado"
            grid.Columns("Bloqueado").Width = 70
            grid.Columns("Bloqueado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            grid.Columns("Bloqueado").ReadOnly = True
        End If

        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    End Sub

    Private Sub dgvPedidos_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPedidos.CellDoubleClick
        NroPedido = Convert.ToDouble(dgvPedidos.CurrentRow.Cells("NroPedido").Value)
        VerDetallePedido(NroPedido)
    End Sub
    Private Sub VerDetallePedido(id As Double)
        If dgvPedidos.SelectedRows.Count = 0 Then
            MessageBox.Show("Seleccione un Pedido para ver su detalle", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        Using frm As New frmVerPedido()
            frm.idPedido = id
            frm.ShowDialog(Me)
        End Using
    End Sub

End Class
