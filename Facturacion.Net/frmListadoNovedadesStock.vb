Imports DSM = DataSourceManager.Lib.DataSourceManager
Public Class frmListadoNovedadesStock
    Private Shared instancia As frmListadoNovedadesStock
    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmListadoNovedadesStock()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub
    Private Sub frmListadoNovedadesStock_formclosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Me.Close()
    End Sub

    Private Sub CmdImprimir_Click(sender As Object, e As EventArgs) Handles CmdImprimir.Click
        Dim criterio As String
        If UsuarioActual <> "JULIOM" Then
            criterio = "({NoveStk.Puntodeventa}=" & PuntoVentaActual & ")"
        End If

        Process.Start(General.ReportesPath, "Facturacion listanovFacturacion RecordSelectionFormula """ & criterio & """")
    End Sub
End Class