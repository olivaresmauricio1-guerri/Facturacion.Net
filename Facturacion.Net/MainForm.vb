Imports System.Diagnostics
Imports System.Windows.Forms
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class MainForm

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = $"FACTURACION V.{Application.ProductVersion}"
        Me.WindowState = FormWindowState.Maximized

        Try
            Dim sucursales = DSM.ExecuteQuery(
               DSM.Stock,
                "SELECT Descripcion FROM Sucursales WHERE idSucursal = @Sucursal",
                CmdParams("@Sucursal", SucursalActual))

            If sucursales.Rows.Count > 0 Then
                General.DescripcionSucursal = sucursales.Rows(0)("Descripcion").ToString()
            End If

            Dim empresas = DSM.ExecuteQuery(
               DSM.Stock,
                "SELECT Descripcion, CUIT, CBU, Alias FROM Empresas WHERE Codigo = @Empresa",
                CmdParams("@Empresa", 1))

            If empresas.Rows.Count > 0 Then
                General.EmpresaActual = empresas.Rows(0)("Descripcion").ToString()
                General.CuitEmpresa = empresas.Rows(0)("CUIT").ToString()
                General.CBUEmpresa = empresas.Rows(0)("CBU").ToString()
                General.AliasEmpresa = empresas.Rows(0)("Alias").ToString()
            End If

        Catch ex As Exception
            MessageBox.Show($"Error al cargar la configuración: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try



        Panel1.Text = $"Sistema de Facturación - Sucursal: {DescripcionSucursal} - Punto de Venta {General.PuntoVentaActual} - Sucursal {General.SucursalActual}"
        Panel2.Text = DateTime.Now.ToString("dd/MM/yyyy")
        Panel3.Text = DateTime.Now.ToString("HH:mm")
        Panel4.Text = UsuarioActual & " | " & Mid(General.Entorno, 1, 3).ToUpper()

        Timer1.Start()
    End Sub

    Private Sub MnuConImp_Click(sender As Object, e As EventArgs) Handles MnuConImp.Click
        Using dlg As New PrintDialog()
            dlg.UseEXDialog = True
            dlg.ShowDialog(Me)
        End Using
    End Sub

    Private Sub mncalcu_Click(sender As Object, e As EventArgs) Handles mncalcu.Click
        Try
            Process.Start("calc.exe")
        Catch ex As Exception
            MessageBox.Show("No se pudo abrir la calculadora", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnalma_Click(sender As Object, e As EventArgs) Handles mnalma.Click
        Try
            ' Windows 10/11 might not have timedate.cpl runnable this way, but trying standard command
            Process.Start("control", "timedate.cpl")
        Catch ex As Exception
            MessageBox.Show("No se pudo abrir el almanaque", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MnuSalir_Click(sender As Object, e As EventArgs) Handles MnuSalir.Click
        Me.Close()
    End Sub

    Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Application.Exit()
    End Sub
    ' --- Clientes ---
    Private Sub mnvh_Click(sender As Object, e As EventArgs) Handles mnvh.Click
        MessageBox.Show("Recepción de Vehículos no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub mncot_Click(sender As Object, e As EventArgs) Handles mncot.Click
        MessageBox.Show("Completar OT no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub mnlistad_Click(sender As Object, e As EventArgs) Handles mnlistad.Click
        frmListadoNovedades.AbrirInstancia(Me)
    End Sub
    Private Sub novb_Click(sender As Object, e As EventArgs) Handles novb.Click
        frmNovecc.AbrirInstancia(Me)
    End Sub

    ' --- Stock ---
    Private Sub mnlisnovk_Click(sender As Object, e As EventArgs) Handles mnlisnovk.Click
        frmListadoNovedadesStock.AbrirInstancia(Me)
    End Sub
    Private Sub mnstock_Click(sender As Object, e As EventArgs) Handles mnstock.Click
        frmNovestk.AbrirInstancia(Me)
    End Sub

    ' --- Facturacion ---
    Private Sub mnfacpedi_Click(sender As Object, e As EventArgs) Handles mnfacpedi.Click
        frmFacturaPedi.AbrirInstancia(Me)
    End Sub
    Private Sub mnnota_Click(sender As Object, e As EventArgs) Handles mnnota.Click
        MessageBox.Show("Remito y Factura no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub mntique_Click(sender As Object, e As EventArgs) Handles mntique.Click
        MessageBox.Show("Tique Autoshop no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub mnfa_Click(sender As Object, e As EventArgs) Handles mnfa.Click
        MessageBox.Show("Factura no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub mncre_Click(sender As Object, e As EventArgs) Handles mncre.Click
        MessageBox.Show("Nota de Crédito no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub mndebi_Click(sender As Object, e As EventArgs) Handles mndebi.Click
        MessageBox.Show("Nota de Debito no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub mnremi_Click(sender As Object, e As EventArgs) Handles mnremi.Click
        MessageBox.Show("Remito a Cliente no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub mninter_Click(sender As Object, e As EventArgs) Handles mninter.Click
        MessageBox.Show("Remito Uso Interno no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub mnrecubo_Click(sender As Object, e As EventArgs) Handles mnrecubo.Click
        MessageBox.Show("Recibo no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub mnpresu_Click(sender As Object, e As EventArgs) Handles mnpresu.Click
        frmReimprimirCbte.AbrirInstancia(Me)
    End Sub
    Private Sub mnmerca_Click(sender As Object, e As EventArgs) Handles mnmerca.Click
        MessageBox.Show("Ingreso de Mercadería no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub mnfacrecon_Click(sender As Object, e As EventArgs) Handles mnfacrecon.Click
        MessageBox.Show("Facturar Reconstrucción no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub mncaja_Click(sender As Object, e As EventArgs) Handles mncaja.Click
        MessageBox.Show("Cierre de Caja no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub mnx_Click(sender As Object, e As EventArgs) Handles mnx.Click
        MessageBox.Show("Cierres Fiscales no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' --- Nomencladores ---
    Private Sub mnremitos_Click(sender As Object, e As EventArgs) Handles mnremitos.Click
        frmControlRemito.AbrirInstancia(Me)

    End Sub

    ' --- Seguridad ---
    Private Sub MnuSegInf_Click(sender As Object, e As EventArgs) Handles MnuSegInf.Click
        MessageBox.Show("Informacion Reservada no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub MnuSegSes_Click(sender As Object, e As EventArgs) Handles MnuSegSes.Click
        Dim f As New frmSesion()
        f.ShowDialog()
    End Sub
    Private Sub MnuSegINI_Click(sender As Object, e As EventArgs) Handles MnuSegINI.Click
        MessageBox.Show("Editar .INI no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' --- Ventanas ---
    Private Sub MnuVenCer_Click(sender As Object, e As EventArgs) Handles MnuVenCer.Click
        If Me.ActiveMdiChild IsNot Nothing Then
            Me.ActiveMdiChild.Close()
        End If
    End Sub
    Private Sub MnuVenTod_Click(sender As Object, e As EventArgs) Handles MnuVenTod.Click
        For Each child As Form In Me.MdiChildren
            child.Close()
        Next
    End Sub
    Private Sub MnuVenCas_Click(sender As Object, e As EventArgs) Handles MnuVenCas.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub
    Private Sub MnuVenVer_Click(sender As Object, e As EventArgs) Handles MnuVenVer.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub
    Private Sub MnuVenHor_Click(sender As Object, e As EventArgs) Handles MnuVenHor.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub
    Private Sub MnuVenIco_Click(sender As Object, e As EventArgs) Handles MnuVenIco.Click
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub
    Private Sub MnuVenImp_Click(sender As Object, e As EventArgs) Handles MnuVenImp.Click
        If Me.ActiveMdiChild IsNot Nothing Then
            ' Print logic placeholder
            MessageBox.Show("Imprimir Ventana no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    ' --- Ayuda ---
    Private Sub MnuAceAce_Click(sender As Object, e As EventArgs) Handles MnuAceAce.Click
        MessageBox.Show("FACTURACION - Guerrini Neumáticos S.A. V." & Application.ProductVersion, "Acerca de", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub MnuAceAyu_Click(sender As Object, e As EventArgs) Handles MnuAceAyu.Click
        MessageBox.Show("Ayuda no disponible", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

End Class
