Imports DSM = DataSourceManager.Lib.DataSourceManager
Public Class frmListadoNovedades

    Private Shared instancia As frmListadoNovedades
    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmListadoNovedades()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub
    Private Sub frmListadoNovedades_formclosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub
    Private Sub CmdImprimir_Click(sender As Object, e As EventArgs) Handles CmdImprimir.Click
        Dim criterio As String

        criterio = "({NoveCtaCte.Puntodeventa}=" & PuntoVentaActual & ")"


        Process.Start(General.ReportesPath, "Facturacion lisnovFacturacion RecordSelectionFormula """ & criterio & """")
    End Sub

    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Me.Close()
    End Sub
End Class