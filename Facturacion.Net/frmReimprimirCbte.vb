Imports System.IO
Imports System.Linq
Imports System.Reflection

Public Class frmReimprimirCbte
    Inherits Form

    Private Shared instancia As frmReimprimirCbte

    ' Modelo simple para la grilla
    Private Class DocumentoPdf
        Public Property NombreArchivo As String
        Public Property Fecha As DateTime
        Public Property TipoComprobante As String
        Public Property RutaCompleta As String
    End Class

    Private _todosLosDocumentos As New List(Of DocumentoPdf)()
    Private _puntoVentaSeleccionado As String = ""

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmReimprimirCbte()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmReimprimirCbte_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub frmReimprimirCbte_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ConfigurarEstiloGrid()

            ' Inicializar filtros de fecha
            Dim hoy = DateTime.Today
            ' Por defecto mostramos el último mes
            dtpDesde.Value = hoy.AddDays(-30)
            dtpHasta.Value = hoy
            chkLimite.Checked = True

            CargarCombos()

            ' Seleccionar primer PV por defecto si hay
            If cmbPuntoVenta.Items.Count > 0 Then
                cmbPuntoVenta.SelectedIndex = 0
            End If

            ' Cargar documentos iniciales
            CargarDocumentos()

        Catch ex As Exception
            MessageBox.Show("Error al iniciar el formulario: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ConfigurarEstiloGrid()
        ' Configuración visual similar a frmNovecc y estándar del sistema
        With dgvDocumentos
            .AutoGenerateColumns = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .RowHeadersVisible = False
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .ReadOnly = True
            .BackgroundColor = SystemColors.Control
            .BorderStyle = BorderStyle.Fixed3D

            .Columns.Clear()

            Dim colNombre As New DataGridViewTextBoxColumn()
            colNombre.DataPropertyName = "NombreArchivo"
            colNombre.HeaderText = "Archivo"
            colNombre.Name = "NombreArchivo"
            colNombre.MinimumWidth = 200
            .Columns.Add(colNombre)

            Dim colFecha As New DataGridViewTextBoxColumn()
            colFecha.DataPropertyName = "Fecha"
            colFecha.HeaderText = "Fecha"
            colFecha.Name = "Fecha"
            colFecha.Width = 120
            colFecha.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"
            .Columns.Add(colFecha)

            Dim colTipo As New DataGridViewTextBoxColumn()
            colTipo.DataPropertyName = "TipoComprobante"
            colTipo.HeaderText = "Tipo"
            colTipo.Name = "TipoComprobante"
            colTipo.Width = 150
            .Columns.Add(colTipo)
        End With
    End Sub

    Private Sub CargarCombos()
        Try
            ' Cargar Puntos de Venta (Carpetas numéricas)
            cmbPuntoVenta.Items.Clear()
            Dim baseDir As String = "F:\Facturacion"
            If Directory.Exists(baseDir) Then
                Dim dirs = Directory.GetDirectories(baseDir)
                For Each dir As String In dirs
                    Dim name As String = Path.GetFileName(dir)
                    If IsNumeric(name) Then
                        cmbPuntoVenta.Items.Add(name)
                    End If
                Next
            End If
            ' Fallback si no hay carpetas
            If cmbPuntoVenta.Items.Count = 0 Then
                cmbPuntoVenta.Items.Add("1")
            End If

            ' Cargar Tipos de Comprobante (Hardcoded para filtro)
            cmbTipo.Items.Clear()
            cmbTipo.Items.Add("Todos")
            cmbTipo.Items.Add("Factura A")
            cmbTipo.Items.Add("Factura B")
            cmbTipo.Items.Add("Nota Débito A")
            cmbTipo.Items.Add("Nota Débito B")
            cmbTipo.Items.Add("Nota Crédito A")
            cmbTipo.Items.Add("Nota Crédito B")
            cmbTipo.Items.Add("Recibo")
            cmbTipo.Items.Add("Remito")
            cmbTipo.Items.Add("Otro")
            cmbTipo.SelectedIndex = 0

        Catch ex As Exception
            MessageBox.Show("Error al cargar combos: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CargarDocumentos()
        Try
            _todosLosDocumentos.Clear()

            If cmbPuntoVenta.SelectedItem Is Nothing Then Return
            _puntoVentaSeleccionado = cmbPuntoVenta.SelectedItem.ToString()

            Dim dirPath As String = Path.Combine("F:\Facturacion", _puntoVentaSeleccionado)

            If Directory.Exists(dirPath) Then
                ' Usamos DirectoryInfo para obtener metadatos directamente
                Dim di As New DirectoryInfo(dirPath)
                Dim files = di.GetFiles("*.pdf")

                Dim listaDocs As New List(Of DocumentoPdf)(files.Length)

                For Each fi As FileInfo In files
                    Dim doc As New DocumentoPdf With {
                        .NombreArchivo = fi.Name,
                        .Fecha = fi.LastWriteTime,
                        .RutaCompleta = fi.FullName,
                        .TipoComprobante = ObtenerTipoComprobante(fi.Name)
                    }
                    listaDocs.Add(doc)
                Next

                ' Ordenar por fecha descendente una sola vez al cargar
                _todosLosDocumentos = listaDocs.OrderByDescending(Function(d) d.Fecha).ToList()
            End If

            AplicarFiltros()

        Catch ex As Exception
            MessageBox.Show("Error al cargar documentos: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ObtenerTipoComprobante(nombreArchivo As String) As String
        Dim n = nombreArchivo.ToUpper()
        If n.StartsWith("FACT A") Then Return "Factura A"
        If n.StartsWith("FACT B") Then Return "Factura B"
        If n.StartsWith("ND A") Then Return "Nota Débito A"
        If n.StartsWith("ND B") Then Return "Nota Débito B"
        If n.StartsWith("NC A") Then Return "Nota Crédito A"
        If n.StartsWith("NC B") Then Return "Nota Crédito B"
        If n.StartsWith("REC") Then Return "Recibo"
        If n.StartsWith("REM") Then Return "Remito"
        Return "Otro"
    End Function

    Private Sub AplicarFiltros()
        Dim docsFiltrados = _todosLosDocumentos.AsEnumerable()

        ' 1. Filtro por Fecha (Desde inicio del día Desde hasta final del día Hasta)
        Dim fDesde = dtpDesde.Value.Date
        Dim fHasta = dtpHasta.Value.Date.AddDays(1).AddTicks(-1)

        docsFiltrados = docsFiltrados.Where(Function(d) d.Fecha >= fDesde AndAlso d.Fecha <= fHasta)

        ' 2. Filtro por Tipo
        If cmbTipo.SelectedIndex > 0 Then ' 0 es Todos
            Dim tipoSel = cmbTipo.SelectedItem.ToString()
            docsFiltrados = docsFiltrados.Where(Function(d) d.TipoComprobante = tipoSel)
        End If

        ' 3. Filtro por Texto (Nombre de archivo)
        If Not String.IsNullOrWhiteSpace(txtBuscar.Text) Then
            Dim txt = txtBuscar.Text.Trim().ToUpper()
            docsFiltrados = docsFiltrados.Where(Function(d) d.NombreArchivo.ToUpper().Contains(txt))
        End If

        ' 4. Limite de resultados (Optimización de UI)
        If chkLimite.Checked Then
            docsFiltrados = docsFiltrados.Take(100)
        End If

        dgvDocumentos.DataSource = docsFiltrados.ToList()
    End Sub

    ' Eventos de Controles

    Private Sub cmbPuntoVenta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPuntoVenta.SelectedIndexChanged
        CargarDocumentos()
    End Sub

    Private Sub cmbTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTipo.SelectedIndexChanged
        AplicarFiltros()
    End Sub

    Private Sub txtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
        AplicarFiltros()
    End Sub

    Private Sub dtpDesde_ValueChanged(sender As Object, e As EventArgs) Handles dtpDesde.ValueChanged
        AplicarFiltros()
    End Sub

    Private Sub dtpHasta_ValueChanged(sender As Object, e As EventArgs) Handles dtpHasta.ValueChanged
        AplicarFiltros()
    End Sub

    Private Sub chkLimite_CheckedChanged(sender As Object, e As EventArgs) Handles chkLimite.CheckedChanged
        AplicarFiltros()
    End Sub

    Private Sub dgvDocumentos_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDocumentos.CellDoubleClick
        If e.RowIndex < 0 Then Return

        Dim doc = TryCast(dgvDocumentos.Rows(e.RowIndex).DataBoundItem, DocumentoPdf)
        If doc IsNot Nothing Then
            Try
                Dim psi As New ProcessStartInfo(doc.RutaCompleta) With {.UseShellExecute = True}
                Process.Start(psi)
            Catch ex As Exception
                MessageBox.Show("No se pudo abrir el archivo: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub cmdGenerarPDF_Click(sender As Object, e As EventArgs) Handles cmdGenerarPDF.Click
        Try
            ' Intentar abrir el formulario de Facturación si existe en el proyecto
            'Dim frm As New frmFactu()
            'frm.Show()
            'frm.BringToFront()
        Catch ex As Exception
            MessageBox.Show("No se pudo abrir la ventana de generación de PDF." & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub

End Class
