Imports System.Data.SqlClient
Imports System.Reflection.Metadata.Ecma335
Imports DataSourceManager.Lib
Imports Microsoft.Data
Imports Microsoft.Identity.Client
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

        ' Configuración inicial de controles

        MaskGrabado.Text = "0.00"
        MaskIB.Text = "0.00"
        MaskInscrip.Text = "0.00"
        MaskNoInscrip.Text = "0.00"
        MaskTotal.Text = "0.00"

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
        FiltraPedidos("Neuuquen")
    End Sub

    Private Sub optLujan_CheckedChanged(sender As Object, e As EventArgs) Handles optLujan.CheckedChanged
        FiltraPedidos("Lujan")
    End Sub
    Private Sub cmdFacturar_Click(sender As Object, e As EventArgs) Handles cmdFacturar.Click
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
            MessageBox.Show("Proceso de facturación completado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error durante la facturación: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub FiltraPedidos(sucur As String)
        Dim Sql = "SELECT NroPedido, Vendedor, Cliente, SumadeEntregaActual as Entrega, Bloqueado FROM PedidosFactura WHERE DepEntrega = '" & sucur & "' ORDER BY Cliente"
        Dim dt As DataTable = DSM.ExecuteQuery(DSM.Stock, Sql)
        dgvPedidos.DataSource = dt
    End Sub
    Private Sub ProcesarFacturacion(row As DataGridViewRow)

        'Muestro detalle de pedido antes de facturar
        VerDetallePedido(NroPedido)

        'Valido el pedido antes de facturar
        If ValidadPedidoParaFacturar(NroPedido) = False Then Return

        'Obtengo el pedido a facturar
        Dim sqlPedido As String = "SELECT * FROM PedidosClientes WHERE NroPedido = " & NroPedido & " AND EntregaActual > 0"
        Dim dtPedido As DataTable = DSM.ExecuteQuery(DSM.Stock, sqlPedido)
        NroCuenta = Convert.ToInt64(dtPedido.Rows(0)("Cuenta"))

        'Obtengo el Cliente para saber si esta bloqueado 
        Dim sqlClientes As String = "SELECT * FROM MaeCtaCte WHERE NroCuenta = " & NroCuenta
        Dim dtClientes As DataTable = DSM.ExecuteQuery(DSM.Stock, sqlClientes)

        If dtClientes IsNot Nothing AndAlso dtClientes.Rows.Count > 0 Then
            If Convert.ToBoolean(dtClientes.Rows(0)("Bloqueado")) Then
                MessageBox.Show("El cliente está bloqueado. No se puede facturar el pedido.", "Error de Cliente", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim CuitSinGuiones As String = dtClientes.Rows(0)("Cuit").ToString().Replace("-", "")
            If Not IsNumeric(CuitSinGuiones) Then
                MessageBox.Show("El CUIT del cliente es inválido. No se puede facturar el pedido.", "Error de Cliente", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim TipoIva As Integer = dtClientes.Rows(0)("idTipoIva")
            Dim Retener As Boolean = Convert.ToBoolean(dtClientes.Rows(0)("ingBrutos"))
            Dim razonsocial As String = dtClientes.Rows(0)("Nombre").ToString()
            Dim idCtaCte As Integer = dtClientes.Rows(0)("idCtaCte")
            Dim CUIT As String = CuitSinGuiones
            Dim TipoComprobante As Integer = dtClientes.Rows(0)("idTipoIva")
            Dim TipoDoc As Integer = dtClientes.Rows(0)("TipoDto")

            'Determino tipo de IVA
            Dim sqlTipoIva As String = "SELECT * FROM TiposIva WHERE Codigo = " & TipoIva
            Dim dtTipoIva As DataTable = DSM.ExecuteQuery(DSM.Stock, sqlTipoIva)
            Dim PorcIva As Double = dtTipoIva.Rows(0)("Porcentaje") / 100 + 1
            Dim PorcNI As Double = dtTipoIva.Rows(0)("NoInscripto")
            Dim PorcIB As Double = dtTipoIva.Rows(0)("IBrutos")
            Dim CodAfip As Integer = dtTipoIva.Rows(0)("CodigoAfip")
            Dim tablaIbrutos(24) As Double

            If UsuarioActual <> "PEDRO" Then

                Dim sqlIB As String = "SELECT * FROM Percepciones WHERE nrocuenta = '" & NroCuenta & "'"
                Dim dtIB As DataTable = DSM.ExecuteQuery(DSM.Stock, sqlIB)
                Dim codProvincia As Integer
                Dim alicuota As Double

                If dtIB IsNot Nothing AndAlso dtIB.Rows.Count > 0 Then
                    For Each rowIB As DataRow In dtIB.Rows

                        codProvincia = Convert.ToInt32(rowIB("codprovincia"))
                        alicuota = Convert.ToDouble(rowIB("alicuota"))

                        If codProvincia >= 1 AndAlso codProvincia <= 24 Then
                            tablaIbrutos(codProvincia) = alicuota
                        End If

                    Next
                End If

            End If

            For i As Integer = 1 To 24
                If tablaIbrutos(i) > 1 Then
                    MsgBox("Error en tabla percepciones Ingresos Brutos (Gustavo Mancifesta)")
                    Exit Sub
                End If
            Next

        End If





    End Sub
    Private Function ValidadPedidoParaFacturar(nroPedido As Double) As Boolean
        ' 1. Controlo duplicidad del articulo en el pedido 
        Dim sqlDuplicado As String = "SELECT NroPedido, Articulo, COUNT(Articulo) as Cuenta FROM PedidosClientes WHERE NroPedido = " & nroPedido & " GROUP BY NroPedido, Articulo HAVING COUNT(Articulo) > 1"
        Dim dtDuplicado As DataTable = DSM.ExecuteQuery(DSM.Stock, sqlDuplicado)

        If dtDuplicado IsNot Nothing AndAlso dtDuplicado.Rows.Count > 0 Then
            Dim articulosDuplicados As String = String.Join(", ", dtDuplicado.AsEnumerable().[Select](Function(r) r("Articulo").ToString()))
            MessageBox.Show("El pedido contiene artículos duplicados: " & articulosDuplicados & ". Por favor revise el pedido antes de facturar.", "Error de Duplicidad", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        ' 2. Control que el pedido no tenga mas de 24 lineas
        Dim sqlLineas As String = "SELECT COUNT(NroPedido) FROM PedidosClientes WHERE NroPedido = " & nroPedido & " AND Entregaactual > 0"
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
        Dim sqlDetalle As String = "SELECT NroPedido, Articulo, CantidadPedida, EntregaActual, NroFactura FROM PedidosClientes WHERE NroPedido = " & nroPedido & " AND EntregaActual > 0"
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
