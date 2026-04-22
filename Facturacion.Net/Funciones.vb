Imports System.Diagnostics
Imports System.Drawing
Imports System.Globalization
Imports System.Net
Imports System.Net.Http
Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading.Tasks
Imports System.Windows.Forms.DataVisualization.Charting
Imports DataSourceManager.Lib
Imports PdfSharpCore.Drawing
Imports PdfSharpCore.Pdf
Imports QRCoder
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class ClienteFiscalData
    Public Property NroCuenta As Long
    Public Property Bloqueado As Boolean
    Public Property Cuit As String
    Public Property TipoIva As Integer
    Public Property RetenerIngBrutos As Boolean
    Public Property RazonSocial As String
    Public Property IdCtaCte As Integer
    Public Property IdSucursal As Integer
    Public Property TipoComprobante As Integer
    Public Property TipoDoc As Integer
    Public Property PorcIva As Double
    Public Property PorcNoInscripto As Double
    Public Property PorcIBrutos As Double
    Public Property CodigoAfip As Integer
    Public Property IIBBPorProvincia As Double()
    Public Property Tipocomp As Integer
    Public Property idVendedor As Integer
End Class

Public Module Funciones

    Public Function CmdParams(ParamArray values() As Object) As Dictionary(Of String, Object)
        Dim dict As New Dictionary(Of String, Object)

        For i As Integer = 0 To values.Length - 2 Step 2
            Dim key = values(i).ToString()
            Dim val = values(i + 1)
            dict(key) = val
        Next

        Return dict
    End Function

    Public Sub CopiarDataGrid(grid As DataGridView, Optional incluirEncabezados As Boolean = True)
        If grid Is Nothing Then Exit Sub

        Dim dataObj As DataObject = Nothing

        ' Guardamos configuración actual
        Dim modoOriginal = grid.ClipboardCopyMode
        grid.ClipboardCopyMode = If(incluirEncabezados,
                                DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText,
                                DataGridViewClipboardCopyMode.EnableWithoutHeaderText)

        If grid.SelectedCells.Count > 0 Then
            ' Obtener el rango mínimo y máximo
            Dim selectedCells = grid.SelectedCells.Cast(Of DataGridViewCell)().OrderBy(Function(c) c.RowIndex).ThenBy(Function(c) c.ColumnIndex).ToList()

            Dim minRow = selectedCells.Min(Function(c) c.RowIndex)
            Dim maxRow = selectedCells.Max(Function(c) c.RowIndex)
            Dim minCol = selectedCells.Min(Function(c) c.ColumnIndex)
            Dim maxCol = selectedCells.Max(Function(c) c.ColumnIndex)

            Dim sb As New System.Text.StringBuilder()

            ' Encabezados si se pidió
            If incluirEncabezados Then
                For col = minCol To maxCol
                    If grid.Columns(col).Visible Then
                        sb.Append(grid.Columns(col).HeaderText)
                        If col < maxCol Then sb.Append(vbTab)
                    End If
                Next
                sb.AppendLine()
            End If

            ' Filas de datos
            For row = minRow To maxRow
                For col = minCol To maxCol
                    If grid.Columns(col).Visible Then
                        Dim cell = grid.Rows(row).Cells(col)
                        If cell.Selected Then
                            sb.Append(cell.Value?.ToString())
                        End If
                        If col < maxCol Then sb.Append(vbTab)
                    End If
                Next
                sb.AppendLine()
            Next

            dataObj = New DataObject()
            dataObj.SetText(sb.ToString())
        Else
            ' Nada seleccionado, copiar todo
            grid.SelectAll()
            dataObj = grid.GetClipboardContent()
            grid.ClearSelection()
        End If

        If dataObj IsNot Nothing Then
            Clipboard.SetDataObject(dataObj)
        End If

        ' Restaurar configuración
        grid.ClipboardCopyMode = modoOriginal
    End Sub

    Public Sub SetControlesEnabled(estado As Boolean, ParamArray controles() As Control)
        For Each ctrl In controles
            ctrl.Enabled = estado
            ' si es textbox cambiar back color, y si es dropdown tambien pero con la propiedad correcta
            ctrl.BackColor = Color.White
        Next
    End Sub

    Public Sub ConfigurarEstiloGrid(dgv As DataGridView)
        dgv.RowHeadersWidth = 20

        dgv.SelectionMode = DataGridViewSelectionMode.CellSelect
        dgv.MultiSelect = False
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False
        dgv.AllowUserToResizeColumns = False
        dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
        dgv.AllowUserToResizeRows = False
        ' dgv.RowHeadersVisible = False

        ' Fuente común para todas las celdas
        Dim fuenteCeldas As New Font("Segoe UI", 8, FontStyle.Regular)

        ' Estilo por defecto (filas impares - fondo blanco)
        dgv.DefaultCellStyle.BackColor = Color.White
        dgv.DefaultCellStyle.Font = fuenteCeldas
        dgv.DefaultCellStyle.ForeColor = Color.Black
        dgv.DefaultCellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.SteelBlue) 'FromArgb(100, 149, 237) ' Azul para selección
        dgv.DefaultCellStyle.SelectionForeColor = Color.White

        ' Estilo alternativo (filas pares - gris muy claro)
        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromKnownColor(KnownColor.FloralWhite)
        dgv.AlternatingRowsDefaultCellStyle.Font = fuenteCeldas
        dgv.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black
        dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.SteelBlue)
        dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White

        ' Estilo del encabezado de columnas
        dgv.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        dgv.EnableHeadersVisualStyles = False
        dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv.ColumnHeadersDefaultCellStyle.BackColor
        dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgv.ColumnHeadersDefaultCellStyle.ForeColor

        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
    End Sub


    Public Function DataGridViewRowToDictionary(row As DataGridViewRow) As Dictionary(Of String, Object)
        Dim dict As New Dictionary(Of String, Object)
        For Each cell As DataGridViewCell In row.Cells
            dict(row.DataGridView.Columns(cell.ColumnIndex).Name) = cell.Value
        Next
        Return dict
    End Function

    Public Function ObtenerDatosFiscalesCliente(nroCuenta As Long, usuarioActual As String, ByRef data As ClienteFiscalData, ByRef errorMsg As String) As Boolean
        data = Nothing
        errorMsg = ""

        Dim dtClientes As DataTable = DSM.ExecuteQuery(DSM.Stock, "SELECT * FROM MaeCtaCte WHERE NroCuenta = @NroCuenta", CmdParams("@NroCuenta", nroCuenta))
        If dtClientes Is Nothing OrElse dtClientes.Rows.Count = 0 Then
            errorMsg = "Cliente inexistente. No se puede facturar el pedido."
            Return False
        End If

        Dim rowCliente As DataRow = dtClientes.Rows(0)

        Dim bloqueado As Boolean = False
        If rowCliente.Table.Columns.Contains("Bloqueado") AndAlso Not IsDBNull(rowCliente("Bloqueado")) Then
            bloqueado = Convert.ToBoolean(rowCliente("Bloqueado"))
        End If
        If bloqueado Then
            errorMsg = "El cliente está bloqueado. No se puede facturar el pedido."
            Return False
        End If

        Dim cuitSinGuiones As String = ""
        If rowCliente.Table.Columns.Contains("Cuit") AndAlso Not IsDBNull(rowCliente("Cuit")) Then
            cuitSinGuiones = rowCliente("Cuit").ToString().Replace("-", "").Trim()
        End If
        If cuitSinGuiones = "" OrElse Not IsNumeric(cuitSinGuiones) Then
            errorMsg = "El CUIT del cliente es inválido. No se puede facturar el pedido."
            Return False
        End If

        Dim tipoIva As Integer = 0
        If rowCliente.Table.Columns.Contains("idTipoIva") AndAlso Not IsDBNull(rowCliente("idTipoIva")) Then
            tipoIva = Convert.ToInt32(rowCliente("idTipoIva"))
        End If

        Dim retenerIngBrutos As Boolean = False
        If rowCliente.Table.Columns.Contains("ingBrutos") AndAlso Not IsDBNull(rowCliente("ingBrutos")) Then
            retenerIngBrutos = Convert.ToBoolean(rowCliente("ingBrutos"))
        End If

        Dim razonSocial As String = ""
        If rowCliente.Table.Columns.Contains("Nombre") AndAlso Not IsDBNull(rowCliente("Nombre")) Then
            razonSocial = rowCliente("Nombre").ToString()
        End If

        Dim idCtaCte As Integer = 0
        If rowCliente.Table.Columns.Contains("idCtaCte") AndAlso Not IsDBNull(rowCliente("idCtaCte")) Then
            idCtaCte = Convert.ToInt32(rowCliente("idCtaCte"))
        End If

        Dim idSucursal As Integer = 0
        If rowCliente.Table.Columns.Contains("idsucursal") AndAlso Not IsDBNull(rowCliente("idsucursal")) Then
            idSucursal = Convert.ToInt32(rowCliente("idsucursal"))
        ElseIf rowCliente.Table.Columns.Contains("IdSucursal") AndAlso Not IsDBNull(rowCliente("IdSucursal")) Then
            idSucursal = Convert.ToInt32(rowCliente("IdSucursal"))
        End If

        Dim tipoDoc As Integer = 0
        If rowCliente.Table.Columns.Contains("TipoDto") AndAlso Not IsDBNull(rowCliente("TipoDto")) Then
            tipoDoc = Convert.ToInt32(rowCliente("TipoDto"))
        End If

        Dim idVendedor As Integer = 0
        If rowCliente.Table.Columns.Contains("idVendedor") AndAlso Not IsDBNull(rowCliente("idVendedor")) Then
            idVendedor = Convert.ToInt32(rowCliente("idVendedor"))
        End If

        Dim dtTipoIva As DataTable = DSM.ExecuteQuery(DSM.Stock, "SELECT * FROM TipoIva WHERE Codigo = @Codigo", CmdParams("@Codigo", tipoIva))
        If dtTipoIva Is Nothing OrElse dtTipoIva.Rows.Count = 0 Then
            errorMsg = "Tipo de IVA inexistente. No se puede facturar el pedido."
            Return False
        End If

        Dim rowTipoIva As DataRow = dtTipoIva.Rows(0)

        Dim porcIva As Double = Convert.ToDouble(rowTipoIva("Iva")) / 100 + 1
        Dim porcNoInscripto As Double = Convert.ToDouble(rowTipoIva("NoInscripto"))
        Dim porcIBrutos As Double = Convert.ToDouble(rowTipoIva("IBrutos"))
        Dim codigoAfip As Integer = Convert.ToInt32(rowTipoIva("CodigoAfip"))

        Dim tablaIbrutos(24) As Double

        If Not String.Equals(usuarioActual, "PEDRO", StringComparison.OrdinalIgnoreCase) Then
            Dim dtIB As DataTable = DSM.ExecuteQuery(DSM.Stock, "SELECT * FROM Percepciones WHERE nrocuenta = @NroCuenta", CmdParams("@NroCuenta", nroCuenta))
            If dtIB IsNot Nothing AndAlso dtIB.Rows.Count > 0 Then
                For Each rowIB As DataRow In dtIB.Rows
                    Dim codProvincia As Integer = Convert.ToInt32(rowIB("codprovincia"))
                    Dim alicuota As Double = Convert.ToDouble(rowIB("alicuota"))

                    If codProvincia >= 1 AndAlso codProvincia <= 24 Then
                        tablaIbrutos(codProvincia) = alicuota
                    End If
                Next
            End If
        End If

        For i As Integer = 1 To 24
            If tablaIbrutos(i) > 1 Then
                errorMsg = "Error en tabla percepciones Ingresos Brutos (Gustavo Mancifesta)"
                Return False
            End If
        Next

        Dim Tipocomp = 0
        Select Case tipoIva
            Case 1, 6
                Tipocomp = 1
            Case 3, 4, 5
                Tipocomp = 6
            Case 7
                Tipocomp = 19
            Case 59 'Nota de Credito A
                Tipocomp = 3
            Case 60 'Nota de Credito B
                Tipocomp = 8
            Case 61 'Nota de Debito A
                Tipocomp = 2
            Case 62 'Nota de Debito B
                Tipocomp = 7
        End Select

        data = New ClienteFiscalData() With {
            .NroCuenta = nroCuenta,
            .Bloqueado = bloqueado,
            .Cuit = cuitSinGuiones,
            .TipoIva = tipoIva,
            .RetenerIngBrutos = retenerIngBrutos,
            .RazonSocial = razonSocial,
            .IdCtaCte = idCtaCte,
            .IdSucursal = idSucursal,
            .TipoComprobante = Tipocomp,
            .TipoDoc = tipoDoc,
            .PorcIva = porcIva,
            .PorcNoInscripto = porcNoInscripto,
            .PorcIBrutos = porcIBrutos,
            .CodigoAfip = codigoAfip,
            .IIBBPorProvincia = tablaIbrutos,
            .Tipocomp = Tipocomp,
            .idVendedor = idVendedor
        }

        Return True
    End Function

    Public Class NetworkHelper

        Public Shared Function VerificarInternet() As Boolean
            Do
                Try
                    ' Request liviano para validar salida a Internet
                    Dim request As HttpWebRequest = CType(WebRequest.Create("http://clients3.google.com/generate_204"), HttpWebRequest)
                    request.Timeout = 3000
                    request.Method = "GET"
                    request.KeepAlive = False

                    Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                        If response.StatusCode = HttpStatusCode.NoContent OrElse response.StatusCode = HttpStatusCode.OK Then
                            Return True
                        End If
                    End Using

                Catch
                    ' No hay internet o bloqueo de red
                End Try

                Dim result As DialogResult = MessageBox.Show(
                "No hay conexión a Internet." & vbCrLf & "¿Desea reintentar?",
                "Sin conexión",
                MessageBoxButtons.RetryCancel,
                MessageBoxIcon.Warning
            )

                If result = DialogResult.Cancel Then
                    Return False
                End If

            Loop
        End Function

    End Class

    Public Class TotalesResult
        Public Property TotG As Double
        Public Property TotNI As Double
        Public Property TotI As Double
        Public Property TotIB As Double
        Public Property TotalFinal As Double
    End Class


    Public Function Totales(
    ByVal idPropio As Long,
    ByVal porcIva As Double,
    ByVal porcNI As Double,
    ByVal usuario As String,
    ByVal tablaIbrutos() As Double,
    ByVal nroCuenta As Long,
    ByVal nroFactura As Long,
    ByVal puntoDeVenta As Integer,
    ByVal tipocomp As Integer
) As TotalesResult

        Dim r As New TotalesResult()
        Dim totalBase As Double = 0

        ' 1) Lee total de Facturas por IdPropio
        Dim sqlTotal As String = "SELECT ISNULL(SUM(Facturas.Total),0) AS SumaDeTotal FROM Facturas WHERE IdPropio = @IdPropio"
        Dim dt As DataTable = DSM.ExecuteQuery(DSM.Stock, sqlTotal, CmdParams("@IdPropio", idPropio))
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 AndAlso Not IsDBNull(dt.Rows(0)("SumaDeTotal")) Then
            totalBase = Convert.ToDouble(dt.Rows(0)("SumaDeTotal"))
        End If

        ' 2) Cálculo
        If porcIva <> 0 Then r.TotG = totalBase / porcIva
        If porcNI <> 0 Then r.TotNI = (r.TotG * porcNI) / 100
        r.TotI = totalBase - r.TotG
        r.TotIB = 0

        ' 3) Persistencia simple de IngresosBrutos (equivalente VB6)
        If UCase(usuario) <> "PEDRO" AndAlso tablaIbrutos IsNot Nothing Then
            For i As Integer = 1 To 24
                If tablaIbrutos.Length > i AndAlso tablaIbrutos(i) > 0 Then
                    Dim monto As Double = r.TotG * tablaIbrutos(i)
                    r.TotIB += monto

                    Dim sqlIns As String =
                    "INSERT INTO IngresosBrutos (NroCuenta, NroFactura, alicuota, Monto, puntodeventa, PROVINCIA, codigoafip) " &
                    "SELECT @NroCuenta, @NroFactura, @Alicuota, @Monto, @PuntoDeVenta, p.Descripcion, @CodigoAfip " &
                    "FROM provincias p WHERE p.idprovincia = @IdProvincia"

                    DSM.Execute(DSM.Stock, sqlIns, CmdParams(
                    "@NroCuenta", nroCuenta,
                    "@NroFactura", nroFactura,
                    "@Alicuota", tablaIbrutos(i),
                    "@Monto", monto,
                    "@PuntoDeVenta", puntoDeVenta,
                    "@CodigoAfip", tipocomp,
                    "@IdProvincia", i
                ))
                End If
            Next
        End If

        r.TotalFinal = r.TotG + r.TotNI + r.TotI + r.TotIB
        Return r
    End Function
    Public Function UnificarNro(prefijo As Integer, numero As Long) As String
        Dim prefijoA As String = If(prefijo > 0, prefijo.ToString("0000"), "0000")
        Dim numeroA As String = If(numero > 0, numero.ToString("00000000"), "00000000")
        Return prefijoA & "-" & numeroA
    End Function
    Public Function MapearTipoDocAfip(ByVal tipoDocInterno As Integer) As Integer
        Select Case tipoDocInterno
            Case 67 'CUIT (interno)
                Return 80 'CUIT (AFIP)
            Case 76 'CUIL (interno)
                Return 86 'CUIL (AFIP)
            Case 50 'DNI (interno)
                Return 96 'DNI (AFIP)
            Case 51 'PASAPORTE (interno)
                Return 94 'Pasaporte (AFIP)
            Case 32 'NINGUNO (interno)
                Return 99 'Sin identificar / venta global diaria (AFIP)
            Case Else
                Return 99
        End Select
    End Function

    Private Function LetraPorCodigoAfip(ByVal codigoAfip As Integer) As String
        Select Case codigoAfip
            Case 1, 2, 3, 201, 202, 203
                Return "A"
            Case 6, 7, 8, 206, 207, 208
                Return "B"
            Case 11, 12, 13, 211, 212, 213
                Return "C"
            Case 51, 52, 53
                Return "M"
            Case Else
                Return ""
        End Select
    End Function

    Private Function PrefijoPdfPorCodigoAfip(ByVal codigoAfip As Integer, ByVal tipoArchivo As String) As String
        If Not String.IsNullOrWhiteSpace(tipoArchivo) Then
            Return tipoArchivo.Trim()
        End If

        Select Case codigoAfip
            Case 201, 206, 211
                Return "FactCred"
            Case 1, 6, 11, 51
                Return "Fact"
            Case 2, 7, 12, 52, 202, 207
                Return "ND"
            Case 3, 8, 13, 53, 203, 208
                Return "NC"
            Case Else
                Return "Fact"
        End Select
    End Function

    Private Function ConstruirRutaPdf(
        ByVal puntoVenta As Integer,
        ByVal nroComprobante As Long,
        ByVal codigoAfip As Integer,
        ByVal tipoArchivo As String
    ) As String
        Dim carpetaPv As String = Path.Combine("C:\Sistema\Facturacion", puntoVenta.ToString())
        Directory.CreateDirectory(carpetaPv)

        Dim prefijo As String = PrefijoPdfPorCodigoAfip(codigoAfip, tipoArchivo)
        Dim letra As String = LetraPorCodigoAfip(codigoAfip)

        Dim pvFmt As String = puntoVenta.ToString("0000")
        Dim nroFmt As String = nroComprobante.ToString("00000000")

        Dim nombre As String
        If String.IsNullOrWhiteSpace(letra) Then
            nombre = prefijo & " N " & pvFmt & "-" & nroFmt & ".pdf"
        Else
            nombre = prefijo & " " & letra & " N " & pvFmt & "-" & nroFmt & ".pdf"
        End If

        Return Path.Combine(carpetaPv, nombre)
    End Function

    Public Function GenerarPdfFactura(
        ByVal nroCuenta As Long,
        ByVal puntoVenta As Integer,
        ByVal nroComprobante As Long,
        ByVal codigoAfip As Integer,
        ByVal datosCliente As ClienteFiscalData,
        ByVal totales As TotalesResult,
        ByVal dtItems As DataTable,
        ByVal cae As String,
        ByVal vencimientoCae As String,
        Optional ByVal tipoArchivo As String = Nothing,
        Optional ByVal vendedor As String = Nothing,
        Optional ByVal nroPedido As Long = 0,
        Optional ByVal esPresupuesto As Boolean = False
    ) As String
        Dim outputPath As String = ConstruirRutaPdf(puntoVenta, nroComprobante, codigoAfip, tipoArchivo)

        Dim dtEmpresa As DataTable = DSM.ExecuteQuery(
            DSM.Stock,
            "SELECT TOP 1 * FROM Empresas WHERE Codigo = @Empresa",
            CmdParams("@Empresa", 1)
        )
        If dtEmpresa Is Nothing OrElse dtEmpresa.Rows.Count = 0 Then
            Throw New Exception("Datos de empresa no encontrados.")
        End If

        Dim emp As DataRow = dtEmpresa.Rows(0)

        Dim doc As New PdfDocument()
        Dim page As PdfPage = doc.AddPage()
        page.Size = PdfSharpCore.PageSize.A4
        Dim gfx As XGraphics = XGraphics.FromPdfPage(page)
        Dim pageWidth As Double = page.Width.Point
        Dim pageHeight As Double = page.Height.Point

        Dim fontTitle As New XFont("Arial", 14, XFontStyle.Bold)
        Dim fontBold As New XFont("Arial", 10, XFontStyle.Bold)
        Dim fontRegular As New XFont("Arial", 9, XFontStyle.Regular)
        Dim fontSmall As New XFont("Arial", 8, XFontStyle.Regular)
        Dim fontItalic As New XFont("Arial", 12, XFontStyle.Italic)
        Dim fontClientTitle As New XFont("Arial", 13, XFontStyle.Bold)
        Dim fontClientItalic As New XFont("Arial", 11, XFontStyle.Italic)

        Dim marginLeft As Double = 40
        Dim y As Double = 40

        If Not esPresupuesto Then
            Dim logoPath As String = "F:\Facturacion\Img\logo.jpg"
            If File.Exists(logoPath) Then
                Try
                    Using logo As XImage = XImage.FromFile(logoPath)
                        gfx.DrawImage(logo, marginLeft, y - 10, 180, 55)
                    End Using
                Catch
                End Try
            End If
        End If

        Dim empresaNombre As String = ""
        If emp.Table.Columns.Contains("Descripcion") Then
            empresaNombre = emp("Descripcion").ToString()
        End If

        Dim empresaDireccion As String = ""
        If emp.Table.Columns.Contains("CalleNro") Then
            empresaDireccion = emp("CalleNro").ToString()
        End If
        If emp.Table.Columns.Contains("Localidad") Then
            Dim loc = emp("Localidad").ToString()
            If Not String.IsNullOrWhiteSpace(loc) Then
                If String.IsNullOrWhiteSpace(empresaDireccion) Then
                    empresaDireccion = loc
                Else
                    empresaDireccion &= " - " & loc
                End If
            End If
        End If

        Dim empresaEmail As String = ""
        If emp.Table.Columns.Contains("email") Then
            empresaEmail = emp("email").ToString()
        End If

        Dim empresaWeb As String = ""
        If emp.Table.Columns.Contains("web") Then
            empresaWeb = emp("web").ToString()
        End If

        Dim empresaCondIva As String = ""
        If emp.Table.Columns.Contains("CondIva") Then
            empresaCondIva = emp("CondIva").ToString()
        End If

        Dim xLeft As Double = marginLeft
        Dim yLeft As Double = 95
        If Not String.IsNullOrWhiteSpace(empresaNombre) Then
            gfx.DrawString(empresaNombre, fontBold, XBrushes.Black, New XPoint(xLeft, yLeft))
            yLeft += 14
        End If
        If Not String.IsNullOrWhiteSpace(empresaDireccion) Then
            gfx.DrawString(empresaDireccion, fontRegular, XBrushes.Black, New XPoint(xLeft, yLeft))
            yLeft += 14
        End If
        If Not String.IsNullOrWhiteSpace(empresaEmail) Then
            gfx.DrawString("Email. " & empresaEmail, fontRegular, XBrushes.Black, New XPoint(xLeft, yLeft))
            yLeft += 14
        End If
        If Not String.IsNullOrWhiteSpace(empresaWeb) Then
            gfx.DrawString("Web. " & empresaWeb, fontRegular, XBrushes.Black, New XPoint(xLeft, yLeft))
            yLeft += 14
        End If
        If Not String.IsNullOrWhiteSpace(empresaCondIva) Then
            gfx.DrawString(empresaCondIva, fontRegular, XBrushes.Black, New XPoint(xLeft, yLeft))
            yLeft += 14
        End If

        Dim pvFmt As String = puntoVenta.ToString("0000")
        Dim nroFmt As String = nroComprobante.ToString("00000000")

        Dim letra As String = LetraPorCodigoAfip(codigoAfip)
        Dim tituloArchivo As String = PrefijoPdfPorCodigoAfip(codigoAfip, tipoArchivo)
        Dim tituloCbte As String = tituloArchivo
        If esPresupuesto Then
            tituloCbte = "PRESUPUESTO"
        Else
            Select Case tituloArchivo
                Case "Fact"
                    tituloCbte = "FACTURA"
                Case "NC"
                    tituloCbte = "NOTA DE CREDITO"
                Case "ND"
                    tituloCbte = "NOTA DE DEBITO"
                Case "FactCred"
                    tituloCbte = "FACTURA DE CREDITO"
                Case "Recibos"
                    tituloCbte = "RECIBO"
            End Select
        End If

        Dim boxW As Double = 48
        Dim boxH As Double = 48
        Dim xBox As Double = (pageWidth / 2) - (boxW / 2)
        Dim yBox As Double = 35
        gfx.DrawRectangle(XPens.Black, xBox, yBox, boxW, boxH)
        If Not String.IsNullOrWhiteSpace(letra) Then
            gfx.DrawString(letra, New XFont("Arial", 16, XFontStyle.Bold), XBrushes.Black, New XRect(xBox, yBox + 2, boxW, 24), XStringFormats.Center)
        End If
        gfx.DrawString("COD. " & codigoAfip.ToString(), fontSmall, XBrushes.Black, New XRect(xBox, yBox + 26, boxW, 18), XStringFormats.Center)

        Dim xRight As Double = xBox + boxW + 24
        Dim rightW As Double = pageWidth - marginLeft - xRight

        Dim yRightTop As Double = 60
        Dim yRight As Double = yRightTop

        gfx.DrawString("ORIGINAL", fontItalic, XBrushes.Black, New XRect(xRight, yRight - 28, rightW, 18), XStringFormats.Center)
        gfx.DrawString(tituloCbte & ": " & pvFmt & "-" & nroFmt, fontItalic, XBrushes.Black, New XPoint(xRight, yRight))
        yRight += 40

        Dim empCuit As String = ""
        If emp.Table.Columns.Contains("CUIT") Then
            empCuit = emp("CUIT").ToString()
        End If
        If Not String.IsNullOrWhiteSpace(empCuit) Then
            gfx.DrawString("C.U.I.T.: " & empCuit, fontRegular, XBrushes.Black, New XPoint(xRight, yRight))
            yRight += 18
        End If

        Dim empIb As String = ""
        If emp.Table.Columns.Contains("IngresosBrutos") Then
            empIb = emp("IngresosBrutos").ToString()
        End If
        If Not String.IsNullOrWhiteSpace(empIb) Then
            gfx.DrawString("Ingresos Brutos: " & empIb, fontRegular, XBrushes.Black, New XPoint(xRight, yRight))
            yRight += 18
        End If

        If emp.Table.Columns.Contains("InicioActividades") AndAlso Not IsDBNull(emp("InicioActividades")) Then
            Dim ini As DateTime = Convert.ToDateTime(emp("InicioActividades"))
            gfx.DrawString("Inicio de Actividades: " & ini.ToString("dd/MM/yy"), fontRegular, XBrushes.Black, New XPoint(xRight, yRight))
            yRight += 18
        End If

        gfx.DrawString("FECHA : " & DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), fontRegular, XBrushes.Black, New XPoint(xRight, 81))

        Dim penRed As New XPen(XColors.Red, 2)
        Dim yLinea As Double = 175
        gfx.DrawLine(penRed, marginLeft, yLinea, pageWidth - marginLeft, yLinea)

        Dim dtCli As DataTable = DSM.ExecuteQuery(
            DSM.Stock,
            "SELECT TOP 1 * FROM MaeCtaCte WHERE NroCuenta = @NroCuenta",
            CmdParams("@NroCuenta", nroCuenta)
        )

        Dim cliNombre As String = If(datosCliente Is Nothing, "", datosCliente.RazonSocial)
        Dim cliCuit As String = If(datosCliente Is Nothing, "", datosCliente.Cuit)
        Dim cliDomicilio As String = ""
        Dim cliLocalidad As String = ""
        If dtCli IsNot Nothing AndAlso dtCli.Rows.Count > 0 Then
            Dim rCli As DataRow = dtCli.Rows(0)
            If rCli.Table.Columns.Contains("Nombre") AndAlso Not IsDBNull(rCli("Nombre")) Then
                cliNombre = rCli("Nombre").ToString()
            End If
            If rCli.Table.Columns.Contains("CalleNro") AndAlso Not IsDBNull(rCli("CalleNro")) Then
                cliDomicilio = rCli("CalleNro").ToString()
            End If
            If rCli.Table.Columns.Contains("Localidad") AndAlso Not IsDBNull(rCli("Localidad")) Then
                cliLocalidad = rCli("Localidad").ToString()
            End If
            If rCli.Table.Columns.Contains("Departamento") AndAlso Not IsDBNull(rCli("Departamento")) Then
                Dim dep As String = rCli("Departamento").ToString()
                If Not String.IsNullOrWhiteSpace(dep) Then
                    If String.IsNullOrWhiteSpace(cliLocalidad) Then
                        cliLocalidad = dep
                    Else
                        cliLocalidad &= " - " & dep
                    End If
                End If
            End If
            If rCli.Table.Columns.Contains("Provincia") AndAlso Not IsDBNull(rCli("Provincia")) Then
                Dim prov As String = rCli("Provincia").ToString()
                If Not String.IsNullOrWhiteSpace(prov) Then
                    If String.IsNullOrWhiteSpace(cliLocalidad) Then
                        cliLocalidad = prov
                    Else
                        cliLocalidad &= " - " & prov
                    End If
                End If
            End If
        End If

        Dim cliCondIva As String = ""
        Dim dtTipoIva As DataTable = DSM.ExecuteQuery(DSM.Stock, "SELECT TOP 1 Descripcion FROM TipoIva WHERE Codigo = @Codigo", CmdParams("@Codigo", datosCliente.TipoIva))
        If dtTipoIva IsNot Nothing AndAlso dtTipoIva.Rows.Count > 0 AndAlso dtTipoIva.Columns.Contains("Descripcion") Then
            cliCondIva = dtTipoIva.Rows(0)("Descripcion").ToString()
        End If

        Dim yCliStart As Double = yLinea + 6 + 16
        Dim yCli As Double = yCliStart
        gfx.DrawString(cliNombre, fontClientTitle, XBrushes.Black, New XPoint(marginLeft, yCli))
        yCli += 16
        If Not String.IsNullOrWhiteSpace(cliDomicilio) Then
            gfx.DrawString(cliDomicilio, fontClientItalic, XBrushes.Black, New XPoint(marginLeft, yCli))
            yCli += 16
        End If
        If Not String.IsNullOrWhiteSpace(cliLocalidad) Then
            gfx.DrawString(cliLocalidad, fontClientItalic, XBrushes.Black, New XPoint(marginLeft, yCli))
            yCli += 16
        End If

        Dim colW As Double = 260
        Dim xRightCli As Double = pageWidth - marginLeft - colW
        Dim yRightCli As Double = yLinea + 6
        gfx.DrawString(cliCondIva, fontClientItalic, XBrushes.Black, New XRect(xRightCli, yRightCli, colW, 16), XStringFormats.TopRight)
        yRightCli += 16
        gfx.DrawString("C.U.I.T : " & cliCuit, fontClientItalic, XBrushes.Black, New XRect(xRightCli, yRightCli, colW, 16), XStringFormats.TopRight)
        yRightCli += 16
        gfx.DrawString("Nro. Cuenta: " & nroCuenta.ToString(), fontClientItalic, XBrushes.Black, New XRect(xRightCli, yRightCli, colW, 16), XStringFormats.TopRight)

        Dim footerHeight As Double = 190
        Dim qrTop As Double = pageHeight - 130
        Dim footerTop As Double = qrTop - 10

        Dim yCliEnd As Double = yCli
        Dim yRightCliEnd As Double = yRightCli + 16
        Dim yDetalleHeader As Double = Math.Max(yCliEnd, yRightCliEnd) + 10

        gfx.DrawString("Cant", fontBold, XBrushes.Black, New XPoint(marginLeft, yDetalleHeader))
        gfx.DrawString("Articulo", fontBold, XBrushes.Black, New XPoint(marginLeft + 50, yDetalleHeader))
        gfx.DrawString("Descripcion", fontBold, XBrushes.Black, New XPoint(marginLeft + 140, yDetalleHeader))
        gfx.DrawString("P.Unit", fontBold, XBrushes.Black, New XPoint(pageWidth - marginLeft - 150, yDetalleHeader))
        gfx.DrawString("Importe", fontBold, XBrushes.Black, New XPoint(pageWidth - marginLeft - 70, yDetalleHeader))
        Dim yLineaDetalle As Double = yDetalleHeader + 10
        gfx.DrawLine(XPens.Black, marginLeft, yLineaDetalle, pageWidth - marginLeft, yLineaDetalle)
        y = yLineaDetalle + 14

        If dtItems IsNot Nothing Then
            For Each it As DataRow In dtItems.Rows
                Dim cant As Double = 0
                If it.Table.Columns.Contains("cantidad") AndAlso Not IsDBNull(it("cantidad")) Then
                    cant = Math.Abs(Convert.ToDouble(it("cantidad")))
                End If

                Dim art As String = ""
                If it.Table.Columns.Contains("Articulo") AndAlso Not IsDBNull(it("Articulo")) Then
                    art = it("Articulo").ToString()
                End If

                Dim desc As String = ""
                If it.Table.Columns.Contains("DESCRIPCION") AndAlso Not IsDBNull(it("DESCRIPCION")) Then
                    desc = it("DESCRIPCION").ToString()
                End If

                Dim pu As Double = 0
                If it.Table.Columns.Contains("VALORPU") AndAlso Not IsDBNull(it("VALORPU")) Then
                    pu = Convert.ToDouble(it("VALORPU"))
                ElseIf it.Table.Columns.Contains("valorPU") AndAlso Not IsDBNull(it("valorPU")) Then
                    pu = Convert.ToDouble(it("valorPU"))
                End If

                Dim imp As Double = 0
                If it.Table.Columns.Contains("Total") AndAlso Not IsDBNull(it("Total")) Then
                    imp = Convert.ToDouble(it("Total"))
                End If
                Dim rowHeight As Double = Math.Max(12, fontSmall.GetHeight() + 2)
                Dim yRowTop As Double = y

                gfx.DrawString(cant.ToString("0.##"), fontSmall, XBrushes.Black, New XRect(marginLeft, yRowTop, 45, rowHeight), XStringFormats.TopLeft)
                gfx.DrawString(art, fontSmall, XBrushes.Black, New XRect(marginLeft + 50, yRowTop, 85, rowHeight), XStringFormats.TopLeft)
                gfx.DrawString(desc, fontSmall, XBrushes.Black, New XRect(marginLeft + 140, yRowTop, pageWidth - (marginLeft + 140) - 230, rowHeight), XStringFormats.TopLeft)

                Dim culturaMoneda As CultureInfo = CultureInfo.GetCultureInfo("es-AR")
                Dim puTxt As String = Math.Round(pu, 0).ToString("C0", culturaMoneda)
                Dim impTxt As String = Math.Round(imp, 0).ToString("C0", culturaMoneda)
                gfx.DrawString(puTxt, fontSmall, XBrushes.Black, New XRect(pageWidth - marginLeft - 150, yRowTop, 80, rowHeight), XStringFormats.TopRight)
                gfx.DrawString(impTxt, fontSmall, XBrushes.Black, New XRect(pageWidth - marginLeft - 70, yRowTop, 70, rowHeight), XStringFormats.TopRight)

                y += rowHeight
                If y > footerTop Then
                    page = doc.AddPage()
                    page.Size = PdfSharpCore.PageSize.A4
                    gfx = XGraphics.FromPdfPage(page)
                    pageWidth = page.Width.Point
                    pageHeight = page.Height.Point
                    qrTop = pageHeight - 130
                    footerTop = qrTop - 10
                    y = 40
                End If
            Next
        End If

        gfx.DrawLine(XPens.Black, marginLeft, qrTop - 6, pageWidth - marginLeft, qrTop - 6)

        Dim footerYBase As Double = qrTop

        Dim xLabelImporte As Double = pageWidth - marginLeft - 150
        Dim xValorImporte As Double = pageWidth - marginLeft - 70

        Dim culturaMonedaTot As CultureInfo = CultureInfo.GetCultureInfo("es-AR")
        Dim netoTxt As String = Math.Round(totales.TotG, 0).ToString("C0", culturaMonedaTot)
        Dim ivaTxt As String = Math.Round(totales.TotI, 0).ToString("C0", culturaMonedaTot)
        Dim iibbTxt As String = Math.Round(totales.TotIB, 0).ToString("C0", culturaMonedaTot)
        Dim totalTxt As String = Math.Round(totales.TotalFinal, 0).ToString("C0", culturaMonedaTot)

        gfx.DrawString("NETO:", fontBold, XBrushes.Black, New XRect(xLabelImporte, footerYBase, 80, 14), XStringFormats.TopLeft)
        gfx.DrawString(netoTxt, fontBold, XBrushes.Black, New XRect(xValorImporte - 10, footerYBase, 80, 14), XStringFormats.TopRight)

        gfx.DrawString("IVA:", fontBold, XBrushes.Black, New XRect(xLabelImporte, footerYBase + 14, 80, 14), XStringFormats.TopLeft)
        gfx.DrawString(ivaTxt, fontBold, XBrushes.Black, New XRect(xValorImporte - 10, footerYBase + 14, 80, 14), XStringFormats.TopRight)

        gfx.DrawString("IIBB:", fontBold, XBrushes.Black, New XRect(xLabelImporte, footerYBase + 28, 80, 14), XStringFormats.TopLeft)
        gfx.DrawString(iibbTxt, fontBold, XBrushes.Black, New XRect(xValorImporte - 10, footerYBase + 28, 80, 14), XStringFormats.TopRight)

        gfx.DrawString("TOTAL:", fontBold, XBrushes.Black, New XRect(xLabelImporte, footerYBase + 44, 80, 14), XStringFormats.TopLeft)
        gfx.DrawString(totalTxt, fontBold, XBrushes.Black, New XRect(xValorImporte - 10, footerYBase + 44, 80, 14), XStringFormats.TopRight)

        Dim footerLeftX As Double = marginLeft
        Dim footerTextX As Double = marginLeft + 120
        Dim footerTextW As Double = pageWidth - footerTextX - marginLeft - 200

        If Not String.IsNullOrWhiteSpace(vendedor) Then
            gfx.DrawString("Vendedor: " & vendedor, fontSmall, XBrushes.Black, New XRect(footerTextX, footerYBase, footerTextW, 14), XStringFormats.TopLeft)
        End If
        If nroPedido > 0 Then
            gfx.DrawString("Pedido: " & nroPedido.ToString(), fontSmall, XBrushes.Black, New XRect(footerTextX, footerYBase + 14, footerTextW, 14), XStringFormats.TopLeft)
        End If

        If Not esPresupuesto AndAlso Not String.IsNullOrWhiteSpace(cae) Then
            gfx.DrawString("CAE: " & cae, fontSmall, XBrushes.Black, New XRect(footerTextX, footerYBase + 28, footerTextW, 14), XStringFormats.TopLeft)
            If Not String.IsNullOrWhiteSpace(vencimientoCae) Then
                gfx.DrawString("Vto CAE: " & vencimientoCae, fontSmall, XBrushes.Black, New XRect(footerTextX, footerYBase + 42, footerTextW, 14), XStringFormats.TopLeft)
            End If
        End If

        If Not esPresupuesto AndAlso Not String.IsNullOrWhiteSpace(cae) Then
            Try
                Dim dtEmpCuit As String = ""
                If emp.Table.Columns.Contains("CUIT") Then
                    dtEmpCuit = emp("CUIT").ToString().Replace("-", "")
                End If

                Dim fechaQr As String = Date.Today.ToString("yyyy-MM-dd")
                Dim tipoDocRec As Integer = MapearTipoDocAfip(datosCliente.TipoDoc)
                Dim docRec As String = If(datosCliente Is Nothing, "", datosCliente.Cuit)
                docRec = If(docRec Is Nothing, "", docRec.Replace("-", ""))

                Dim importeQr As String = totales.TotalFinal.ToString("0.00", CultureInfo.InvariantCulture)
                Dim strAfip As String = "{'ver':1,'fecha':'" & fechaQr & "','cuit':" & dtEmpCuit & ",'ptoVta':" & puntoVenta & ",'tipoCmp':" & codigoAfip & ",'nroCmp':" & nroComprobante & ",'importe':" & importeQr & ",'moneda':'PES','ctz':1,'tipoDocRec':" & tipoDocRec & ",'nroDocRec':" & docRec & ",'tipoCodAut':'E','codAut':" & cae & "}"
                strAfip = strAfip.Replace("'", """")
                Dim strBase64 As String = Convert.ToBase64String(Encoding.UTF8.GetBytes(strAfip))
                Dim urlQr As String = "https://www.afip.gob.ar/fe/qr/?p=" & strBase64

                Using qrGenerator As New QRCodeGenerator()
                    Dim qrCodeData As QRCodeData = qrGenerator.CreateQrCode(urlQr, QRCodeGenerator.ECCLevel.Q)
                    Using qrCode As New QRCode(qrCodeData)
                        Using qrCodeImage As Bitmap = qrCode.GetGraphic(20)
                            Dim qrDir As String = "C:\qr\" & puntoVenta.ToString()
                            If Not Directory.Exists(qrDir) Then Directory.CreateDirectory(qrDir)
                            Dim qrPath As String = Path.Combine(qrDir, "qr.jpg")
                            Try
                                If File.Exists(qrPath) Then File.Delete(qrPath)
                            Catch
                                qrPath = Path.Combine(qrDir, "qr_" & Guid.NewGuid().ToString() & ".jpg")
                            End Try

                            qrCodeImage.Save(qrPath, System.Drawing.Imaging.ImageFormat.Jpeg)

                            Using xImage As XImage = XImage.FromFile(qrPath)
                                gfx.DrawImage(xImage, footerLeftX, pageHeight - 130, 100, 100)
                            End Using

                            Try
                                If File.Exists(qrPath) Then File.Delete(qrPath)
                            Catch
                            End Try
                        End Using
                    End Using
                End Using
            Catch
            End Try
        End If

        doc.Save(outputPath)
        doc.Close()

        Return outputPath
    End Function
    Public Sub EliminarTemporales(Fact As Double, NroCuenta1 As Long, PV As Integer, tipocomp As Integer, Optional propio As Integer = 0)
        Dim MiSql = "Delete from IngresosBrutos where codigoafip = " & tipocomp & " and  (nrofactura=0 or nrofactura=" & Fact & ") and nrocuenta=" & NroCuenta1 & " AND PUNTODEVENTA = " & PV
        DSM.Execute(DSM.Stock, MiSql)

        If propio > 0 Then
            Dim MiSql2 = "Delete from Facturas where IdPropio=" & propio
            DSM.Execute(DSM.Stock, MiSql2)

            MiSql2 = "Delete from Leyendas where IdPropio=" & propio
            DSM.Execute(DSM.Stock, MiSql2)
        End If
    End Sub
End Module
