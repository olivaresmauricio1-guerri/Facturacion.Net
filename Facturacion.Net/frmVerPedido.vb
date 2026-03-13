Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmVerPedido
    Private Shared instancia As frmVerPedido
    Public Property idPedido As Double
    Public Shared Sub AbrirInstancia(mdiParent As Form, nroPedido As Integer)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmVerPedido()
            instancia.MdiParent = mdiParent
        End If

    End Sub

    Private Sub frmVerPedido_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub frmVerPedido_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ConfigurarEstiloGrid(dgvPedidos)
            dgvPedidos.MultiSelect = False
            dgvPedidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect

            If idPedido > 0 Then
                CargarPedido()
            End If
        Catch ex As Exception
            MessageBox.Show("Error al iniciar el formulario: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdSalir_Click(sender As Object, e As EventArgs) Handles cmdSalir.Click
        Close()
    End Sub

    Private Sub CargarPedido()
        If idPedido <= 0 Then
            dgvPedidos.DataSource = Nothing
            txtCliente.Text = ""
            txtvendedor.Text = ""
            txtfechaP.Text = ""
            txtFechaE.Text = ""
            txtAutoriza.Text = ""
            txtFecha.Text = ""
            txtComenta.Text = ""
            txtObs.Text = ""
            Return
        End If

        Try
            Dim sqlUpdate As String =
                "UPDATE pc SET " &
                "pc.Mza = cs.Matriz, " &
                "pc.BsAs = cs.BsAs, " &
                "pc.Fiscal = cs.Fiscal, " &
                "pc.ZFLaplata = cs.ZFLaplata " &
                "FROM ConsultaStock cs " &
                "INNER JOIN PedidosClientes pc ON pc.Articulo = cs.Articulo " &
                "WHERE pc.NroPedido = @NroPedido"

            DSM.Execute(DSM.Stock, sqlUpdate, CmdParams("@NroPedido", idPedido))

            Dim sql As String = "SELECT * FROM PedidosClientes WHERE NroPedido = @NroPedido ORDER BY Articulo"
            Dim dt As DataTable = DSM.ExecuteQuery(DSM.Stock, sql, CmdParams("@NroPedido", idPedido))
            dgvPedidos.DataSource = dt

            GridConfigurarColumnas()

            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                txtCliente.Text = ""
                txtvendedor.Text = ""
                txtfechaP.Text = ""
                txtFechaE.Text = ""
                txtAutoriza.Text = ""
                txtFecha.Text = ""
                txtComenta.Text = ""
                txtObs.Text = ""
                Return
            End If

            Dim r = dt.Rows(0)
            txtCliente.Text = dt.Rows(0)("Cliente").ToString()
            txtvendedor.Text = dt.Rows(0)("Vendedor").ToString()
            txtfechaP.Text = Format(dt.Rows(0)("FechaPedido"), "dd/MM/yyyy")
            txtFechaE.Text = Format(dt.Rows(0)("FechaEntrega"), "dd/MM/yyyy")
            txtAutoriza.Text = dt.Rows(0)("Autoriza").ToString()
            txtFecha.Text = Format(dt.Rows(0)("FechaAutoriza"), "dd/MM/yyyy")
            txtComenta.Text = dt.Rows(0)("Comentarios").ToString()
            txtObs.Text = dt.Rows(0)("ObsvAuto").ToString()
        Catch ex As Exception
            MessageBox.Show("Error al cargar pedido: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub GridConfigurarColumnas()
        Dim grid = dgvPedidos
        If grid Is Nothing OrElse grid.Columns Is Nothing Then Return

        For Each col As DataGridViewColumn In grid.Columns
            col.Visible = False
        Next

        MostrarColumna(grid, "NroPedido", "Pedido", 70, DataGridViewContentAlignment.MiddleRight)
        MostrarColumna(grid, "Articulo", "Articulo", 90, DataGridViewContentAlignment.MiddleLeft)
        MostrarColumna(grid, "Diametro", "Sección", 70, DataGridViewContentAlignment.MiddleLeft)
        MostrarColumna(grid, "Seccion", "Perfil", 70, DataGridViewContentAlignment.MiddleLeft)
        MostrarColumna(grid, "Tipo", "Tipo", 55, DataGridViewContentAlignment.MiddleLeft)
        MostrarColumna(grid, "Llanta", "Llanta", 60, DataGridViewContentAlignment.MiddleLeft)
        MostrarColumna(grid, "Diseno", "Diseño", 65, DataGridViewContentAlignment.MiddleLeft)
        MostrarColumna(grid, "Telas", "Telas", 55, DataGridViewContentAlignment.MiddleLeft)
        MostrarColumna(grid, "Valvula", "Válvula", 60, DataGridViewContentAlignment.MiddleLeft)
        MostrarColumna(grid, "Uso", "Uso", 55, DataGridViewContentAlignment.MiddleLeft)
        MostrarColumna(grid, "UnidadesxCaja", "xCaja", 55, DataGridViewContentAlignment.MiddleRight)
        MostrarColumna(grid, "CantidadPedida", "Pedido", 60, DataGridViewContentAlignment.MiddleRight)
        MostrarColumna(grid, "CantidadEntregada", "Entregado", 70, DataGridViewContentAlignment.MiddleRight)
        MostrarColumna(grid, "EntregaActual", "Entrega", 65, DataGridViewContentAlignment.MiddleRight)
        MostrarColumna(grid, "Mza", "Mza", 55, DataGridViewContentAlignment.MiddleCenter)
        MostrarColumna(grid, "BsAs", "BsAs", 55, DataGridViewContentAlignment.MiddleCenter)
        MostrarColumna(grid, "Fiscal", "Fiscal", 55, DataGridViewContentAlignment.MiddleCenter)
        MostrarColumna(grid, "ZFLaPlata", "Aduana", 65, DataGridViewContentAlignment.MiddleCenter)
        MostrarColumna(grid, "ZFLaplata", "Aduana", 65, DataGridViewContentAlignment.MiddleCenter)

        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    End Sub

    Private Sub MostrarColumna(grid As DataGridView, nombre As String, titulo As String, ancho As Integer, align As DataGridViewContentAlignment)
        If Not grid.Columns.Contains(nombre) Then Return
        Dim c = grid.Columns(nombre)
        c.Visible = True
        c.HeaderText = titulo
        c.Width = ancho
        c.DefaultCellStyle.Alignment = align
    End Sub

End Class
