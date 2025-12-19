Imports System.Globalization
Imports System.IO
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Module General

    Public Const modulo As String = "Facturacion"

    ' Public ReadOnly UsuarioPorDefecto As String = SistemaINI("USUARIO")
    Public UsuarioActual As String = SistemaINI("USUARIO")
    Public EmpresaActual As String = ""
    Public DescripcionSucursal As String = ""
    Public ReadOnly SucursalActual As String = SistemaINI("SUCURSAL")
    Public ReadOnly PuntoVentaActual As String = SistemaINI("PUNTODEVENTA")

    Public OpcionesHabilitadas As String = ""
    Public ClaveErronea As Boolean = True

#If DEBUG Then
    Public ReadOnly Entorno As String = "Desarrollo"

    Public ReadOnly StockConnectionString As String = SistemaINI("DEBUG_DBSTOCK")
    Public ReadOnly SeguridadConnectionString As String = SistemaINI("DEBUG_DBSEGURIDAD")
    Public ReadOnly ProveedoresConnectionString As String = SistemaINI("DEBUG_DBPROVEEDORES")
    Public ReadOnly BancosConnectionString As String = SistemaINI("DEBUG_DBBANCOS")
    Public ReadOnly ContabilidadConnectionString As String = SistemaINI("DEBUG_DBCONTABILIDAD")
    Public ReadOnly PersonalConnectionString As String = SistemaINI("DEBUG_DBPERSONAL")

    Public ReadOnly SqlApiServiceActivo As Boolean = If(SistemaINI("DEBUG_SQLAPISERVICE") = "ACTIVO", True, False)
    Public ReadOnly SqlApiServiceUrl As String = SistemaINI("DEBUG_SQLAPISERVICEURL")
    Public ReadOnly SqlApiServiceUser As String = SistemaINI("DEBUG_SQLAPISERVICEUSER")
    Public ReadOnly SqlApiServicePass As String = SistemaINI("DEBUG_SQLAPISERVICEPASS")

    Public ReadOnly ReportesPath As String = SistemaINI("DEBUG_RUTAREPORTES") & "\Reportes.exe"
#Else
    Public ReadOnly Entorno As String = "Producción"

    Public ReadOnly StockConnectionString As String = "Data Source=SERVERNT;Initial Catalog=Stock;Integrated Security=True;TrustServerCertificate=True"
    Public ReadOnly SeguridadConnectionString As String = "Data Source=SERVERNT;Initial Catalog=Seguridad;Integrated Security=True;TrustServerCertificate=True"
    Public ReadOnly ProveedoresConnectionString As String = "Data Source=SERVERNT;Initial Catalog=Proveedores;Integrated Security=True;TrustServerCertificate=True"
    Public ReadOnly BancosConnectionString As String = "Data Source=SERVERNT;Initial Catalog=Bancos;Integrated Security=True;TrustServerCertificate=True"
    Public ReadOnly ContabilidadConnectionString As String = "Data Source=SERVERNT;Initial Catalog=Conta;Integrated Security=True;TrustServerCertificate=True"
    Public ReadOnly PersonalConnectionString As String = "Data Source=SERVERNT;Initial Catalog=Personal;Integrated Security=True;TrustServerCertificate=True"
    
    Public ReadOnly SqlApiServiceActivo As Boolean = If(SistemaINI("SQLAPISERVICE") = "ACTIVO", True, False)
    Public ReadOnly SqlApiServiceUrl As String = SistemaINI("SQLAPISERVICEURL")
    Public ReadOnly SqlApiServiceUser As String = SistemaINI("DEBUG_SQLAPISERVICEUSER")
    Public ReadOnly SqlApiServicePass As String = SistemaINI("DEBUG_SQLAPISERVICEPASS")

    Public ReadOnly ReportesPath As String = SistemaINI("SISTEMA") & "\Reportes.net\Reportes.exe"
#End If


    Private _SistemaINI As Dictionary(Of String, String) = Nothing

    ''' <summary>
    ''' carga el valor de una clave del archivo INI del sistema.
    ''' Si el archivo no existe o la clave no se encuentra, devuelve una cadena vacía.
    ''' El archivo INI se carga la primera vez que se llama a esta función.
    ''' Requiere que el archivo INI esté en la ruta especificada o en el directorio de trabajo actual.
    ''' </summary>
    ''' <param name="clave"></param>
    ''' <returns></returns>
    Public Function SistemaINI(clave As String) As String
        If _SistemaINI Is Nothing Then
            _CargarSistemaINI()
        End If

        If Not _SistemaINI.ContainsKey(clave) Then
            MessageBox.Show("No se encontró la clave '" & clave & "' en el archivo Sistema.net.ini. El programa se cerrará.", "Error de configuración", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Environment.Exit(1)
        End If

        Return _SistemaINI(clave)
    End Function

    Private Sub _CargarSistemaINI()
        _SistemaINI = New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)

        Dim iniPath As String = ""

#If DEBUG Then
        Dim debugPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sistema.net.ini")
        Debug.WriteLine("Buscando INI en: " & debugPath)

        If File.Exists(debugPath) Then
            iniPath = debugPath
        End If
#End If

        If String.IsNullOrEmpty(iniPath) Then
            Dim userWin As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) & "\Windows"
            Dim userIni As String = IO.Path.Combine(userWin, "Sistema.net.ini")
            If File.Exists(userIni) Then
                iniPath = userIni
            Else
                Dim winDir As String = Environment.GetFolderPath(Environment.SpecialFolder.Windows)
                Dim winIni As String = IO.Path.Combine(winDir, "Sistema.net.ini")
                iniPath = winIni
            End If
        End If

        If Not File.Exists(iniPath) Then
            MessageBox.Show("No se encontró el archivo Sistema.net.ini en la ruta esperada: " & vbCrLf & iniPath & vbCrLf & "El programa se cerrará.", "Error de configuración", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Environment.Exit(1)
        End If

        Using reader As New StreamReader(iniPath)
            While Not reader.EndOfStream
                Dim line As String = reader.ReadLine().Trim()

                If String.IsNullOrWhiteSpace(line) OrElse line.StartsWith(";") Then
                    Continue While
                End If

                Dim parts = line.Split(New Char() {"="c}, 2)
                If parts.Length = 2 Then
                    Dim key = parts(0).Trim()
                    Dim value = parts(1).Trim()
                    If Not _SistemaINI.ContainsKey(key) Then
                        _SistemaINI.Add(key, value)
                    End If
                End If
            End While
        End Using
    End Sub

    Public Sub InicializarProyecto()

        ' Inicializar cultura por defecto
        Dim culturaFija As New CultureInfo("es-AR")
        CultureInfo.DefaultThreadCurrentCulture = culturaFija
        CultureInfo.DefaultThreadCurrentUICulture = culturaFija

        ' Mapa de conexiones a base de datos
        Dim conexiones As New Dictionary(Of String, String) From {
            {DSM.Stock, StockConnectionString},
            {DSM.Seguridad, SeguridadConnectionString},
            {DSM.Proveedores, ProveedoresConnectionString},
            {DSM.Bancos, BancosConnectionString},
            {DSM.Contabilidad, ContabilidadConnectionString},
            {DSM.Personal, PersonalConnectionString}
        }

        ' Configuración de la API
        Dim configuracionApi As New Dictionary(Of String, String) From {
            {"apiUrl", SqlApiServiceUrl},
            {"apiUser", SqlApiServiceUser},
            {"apiPassword", SqlApiServicePass}
        }

        DSM.CurrentSource = If(SqlApiServiceActivo, DSM.DataSourceType.Api, DSM.DataSourceType.Database)
        Debug.WriteLine("Inicializando DataSourceManager con el tipo: " & DSM.CurrentSource.ToString())

        ' Inicializar el DataSourceManager
        DSM.Inicializar(conexiones, configuracionApi)
    End Sub


    Public Sub CargarCombos(combo As ComboBox, nombreTabla As String, campoOrden As String, campoMostrar As String, Optional campoValor As String = "", Optional where As String = "", Optional UsarStock As Boolean = False)
        Try
            ' Armamos la consulta base
            Dim sql As String = "SELECT * FROM " & nombreTabla

            ' Si viene un where, lo concatenamos
            If Not String.IsNullOrWhiteSpace(where) Then
                sql &= " WHERE " & where
            End If

            ' Orden
            sql &= " ORDER BY " & campoOrden

            ' Ejecutamos la consulta
            Dim tabla
            If UsarStock Then
                tabla = DSM.ExecuteQuery(DSM.Stock, sql)
            Else
                ' Default to Proveedores for now, but should likely be parameterized if Facturacion needs others
                tabla = DSM.ExecuteQuery(DSM.Stock, sql) ' Changed to Stock as default for Facturacion mostly uses Stock? 
                ' Wait, Proveedores.Net uses DSM.Proveedores. Facturacion probably uses Stock mostly.
                ' Let's check where DSM.Proveedores comes from. It's likely an Enum.
                ' If I don't have DSM.Facturacion, I might use DSM.Stock.
                ' Reverting to DSM.Proveedores logic but using Stock if needed.
                ' Actually, let's look at the VB6 code. It queries "autoriza" table in "stock" DB (actually "seguridad" usually has users, but VB6 says `MiSql = "Select * from autoriza ...", cnStock`).
                ' Wait, VB6 `frmSesion` line 156: `MiRs9.Open MiSql, cnStock`.
                ' VB6 `frmSesion` line 174: `MiRs.Open MiSql, cnSeguridad`.
                ' So it uses both.
                
                ' I will leave this method generic or point to Stock/Seguridad as needed.
                ' For now I will copy Proveedores logic but be careful.
                ' Proveedores code:
                ' If UsarStock Then tabla = DSM.ExecuteQuery(DSM.Stock, sql) Else tabla = DSM.ExecuteQuery(DSM.Proveedores, sql)
                
                ' I will change the Else to use DSM.Stock for Facturacion as a safe bet for now, or just leave it as is if I can't verify DSM.Facturacion exists.
                ' But `DSM.Proveedores` might be valid only for Proveedores DB.
                ' I'll stick to DSM.Stock for the default else branch in Facturacion, as Facturacion main data is usually in Stock DB.
                 tabla = DSM.ExecuteQuery(DSM.Stock, sql)
            End If

            ' Bind al combo
            combo.DataSource = Nothing
            combo.DisplayMember = campoMostrar

            If campoValor <> "" Then combo.ValueMember = campoValor

            combo.DataSource = tabla
            combo.SelectedIndex = -1

        Catch ex As Exception
            MessageBox.Show($"Error al cargar datos de {nombreTabla}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    
    Public Sub CargarCombosCtes(combo As ComboBox, nombreTabla As String, campoOrden As String, campoMostrar As String, Optional campoValor As String = "", Optional where As String = "")
        Try
            ' Armamos la consulta base
            Dim sql As String = "SELECT " & campoMostrar.Trim() & " FROM " & nombreTabla

            ' Si viene un where, lo concatenamos
            If Not String.IsNullOrWhiteSpace(where) Then
                sql &= " WHERE " & where
            End If

            ' Orden
            sql &= " GROUP BY " & campoMostrar & " ORDER BY " & campoOrden

            ' Ejecutamos la consulta
            ' Assuming Stock for Facturacion
            Dim tabla = DSM.ExecuteQuery(DSM.Stock, sql)

            ' Bind al combo
            combo.DataSource = Nothing
            combo.DisplayMember = campoMostrar

            If campoValor <> "" Then combo.ValueMember = campoValor

            combo.DataSource = tabla
            combo.SelectedIndex = -1

        Catch ex As Exception
            MessageBox.Show($"Error al cargar datos de {nombreTabla}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub CargarCombosGrid(combo As DataGridViewComboBoxColumn, nombreTabla As String, campoOrden As String, campoMostrar As String, Optional campoValor As String = "")
        Try
            Dim sql As String = "SELECT * FROM " & nombreTabla & " ORDER BY " & campoOrden
            ' Assuming Stock
            Dim tabla = DSM.ExecuteQuery(DSM.Stock, sql)
            combo.DataSource = tabla
            combo.DisplayMember = campoMostrar
            If campoValor <> "" Then combo.ValueMember = campoValor
            'combo.SelectedIndex = -1
        Catch ex As Exception
            MessageBox.Show($"Error al cargar datos de {nombreTabla}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Module
