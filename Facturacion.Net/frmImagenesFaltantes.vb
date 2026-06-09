Imports System.IO
Imports DataSourceManager.Lib
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmImagenesFaltantes

    Private Const RutaImagenes As String = "F:\imagenes\"

    Private Sub frmImagenesFaltantes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConfigurarEstiloGrid(dgvImagenes)
        dgvImagenes.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvImagenes.MultiSelect = True
        dgvImagenes.DataSource = Nothing
    End Sub

    Private Sub dgvImagenes_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvImagenes.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.A Then
            dgvImagenes.SelectAll()
            e.Handled = True
        End If
    End Sub

    Private Sub btnImagenes_Click(sender As Object, e As EventArgs) Handles btnImagenes.Click
        btnImagenes.Enabled = False
        Try
            dgvImagenes.DataSource = ObtenerImagenesFaltantes()
        Catch ex As Exception
            MessageBox.Show("Error al controlar imágenes: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnImagenes.Enabled = True
        End Try
    End Sub

    Private Function ObtenerImagenesFaltantes() As DataTable
        Dim basePath As String = RutaImagenes
        If Not basePath.EndsWith("\") Then basePath &= "\"

        If Not Directory.Exists(basePath) Then
            Throw New DirectoryNotFoundException("No existe el directorio de imágenes: " & basePath)
        End If

        Dim archivosExistentes As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        For Each ruta As String In Directory.EnumerateFiles(basePath, "fot_*.jpg", SearchOption.TopDirectoryOnly)
            archivosExistentes.Add(Path.GetFileName(ruta))
        Next

        Dim sql As String =
            "SELECT DISTINCT Articulo, Diseno, Proveedor " &
            "FROM MaeStk " &
            "WHERE idgrupo = 14 " &
            "  AND lista = 1 " &
            "  AND fechabaja IS NULL " &
            "  AND uventa >= '2024-01-01' " &
            "  AND proveedor NOT IN ('Reconstrucción', 'OTROS', 'Otras Marcas', 'VARIOS', 'SALDOS VARIOS') " &
            "  AND Diseno IS NOT NULL " &
            "  AND LTRIM(RTRIM(Diseno)) <> ''"

        Dim dt As DataTable = DSM.ExecuteQuery(DSM.Stock, sql)

        Dim resultado As New DataTable()
        resultado.Columns.Add("Articulo", GetType(Integer))
        resultado.Columns.Add("Diseno", GetType(String))
        resultado.Columns.Add("Proveedor", GetType(String))
        resultado.Columns.Add("Archivo", GetType(String))

        If dt Is Nothing OrElse dt.Rows.Count = 0 Then
            Return resultado
        End If

        For Each row As DataRow In dt.Rows
            Dim articulo As Integer = Convert.ToInt32(row("Articulo"))
            Dim diseno As String = Convert.ToString(row("Diseno")).Trim()
            Dim proveedor As String = Convert.ToString(row("Proveedor")).Trim()

            Dim archivo As String = "fot_" & diseno & ".jpg"
            If Not archivosExistentes.Contains(archivo) Then
                resultado.Rows.Add(articulo, diseno, proveedor, basePath & archivo)
            End If
        Next

        Return resultado
    End Function

End Class
