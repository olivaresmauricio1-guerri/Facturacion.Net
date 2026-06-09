Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class FEErrorInfo
    Public Property Codigo As String
    Public Property Mensaje As String
End Class

Public Class FERespuestaWs
    Public Property Exito As Boolean
    Public Property Cae As String
    Public Property Venicimiento As String
    Public Property NumeroComprobante As Long
    Public Property TipoComprobanteAfip As Integer
    Public Property CodigoAfip As Integer
    Public Property Mensaje As String
    Public Property Errores As List(Of FEErrorInfo)
End Class

Public Class FESolicitudComprobante
    Public Property IdPropio As Long
    Public Property PuntoVenta As Integer
    Public Property EsPrueba As Boolean
    Public Property TipoComprobante As Integer
    Public Property EsFce As Boolean
    Public Property NroComprobante As Long
    Public Property NroCuenta As Long
    Public Property Cliente As ClienteFiscalData
    Public Property Totales As TotalesResult
    Public Property Articulos As DataTable
End Class

Public Class FEResultadoProceso
    Public Property Exito As Boolean
    Public Property Mensaje As String
    Public Property PuntoVenta As Integer
    Public Property EsPrueba As Boolean
    Public Property TipoComprobanteEmitido As Integer
    Public Property NumeroComprobante As Long
    Public Property Cae As String
    Public Property Venicimiento As String
    Public Property CodigoAfip As Integer
    Public Property ReintentadoComoFce As Boolean
    Public Property CodigoError As String
End Class

Public Delegate Function EmisorComprobanteDelegate(solicitud As FESolicitudComprobante) As FERespuestaWs

Public Module FacturacionElectronica

    Public Property EmisorComprobante As EmisorComprobanteDelegate


    Public Function ProcesarComprobanteElectronico(ByVal solicitudComun As FESolicitudComprobante) As FEResultadoProceso
        Dim resultado As New FEResultadoProceso With {
            .Exito = False,
            .Mensaje = "",
            .ReintentadoComoFce = False
        }

        If solicitudComun Is Nothing Then
            resultado.Mensaje = "Solicitud inválida (Nothing)."
            Return resultado
        End If

        If solicitudComun.Cliente Is Nothing Then
            resultado.Mensaje = "Solicitud inválida: Cliente no informado."
            Return resultado
        End If

        If solicitudComun.Totales Is Nothing Then
            resultado.Mensaje = "Solicitud inválida: Totales no informados."
            Return resultado
        End If

        Dim solicitudBase As New FESolicitudComprobante With {
            .IdPropio = solicitudComun.IdPropio,
            .PuntoVenta = solicitudComun.PuntoVenta,
            .EsPrueba = solicitudComun.EsPrueba,
            .TipoComprobante = solicitudComun.TipoComprobante,
            .EsFce = False,
            .NroComprobante = solicitudComun.NroComprobante,
            .NroCuenta = solicitudComun.NroCuenta,
            .Cliente = solicitudComun.Cliente,
            .Totales = solicitudComun.Totales,
            .Articulos = solicitudComun.Articulos
        }

        Dim respuestaComun As FERespuestaWs = EnviarComprobante(solicitudBase, resultado)
        If respuestaComun Is Nothing Then
            Return resultado
        End If

        If respuestaComun.Exito Then
            CompletarResultadoExitoso(resultado, respuestaComun, solicitudBase)
            Return resultado
        End If

        If ContieneCodigoError(respuestaComun.Errores, "10016") Then
            Dim codigoError = ObtenerCodigoError(respuestaComun)
            CompletarResultadoError(resultado, respuestaComun, solicitudBase, codigoError)
            Return resultado
        End If

        If Not ContieneCodigoError(respuestaComun.Errores, "10192") Then
            Dim codigoError = ObtenerCodigoError(respuestaComun)
            CompletarResultadoError(resultado, respuestaComun, solicitudBase, codigoError)
            Return resultado
        End If

        Dim solicitudFce As New FESolicitudComprobante With {
            .IdPropio = solicitudBase.IdPropio,
            .PuntoVenta = solicitudBase.PuntoVenta,
            .EsPrueba = solicitudBase.EsPrueba,
            .TipoComprobante = ObtenerTipoComprobanteFce(solicitudBase.TipoComprobante),
            .EsFce = True,
            .NroComprobante = 0,
            .NroCuenta = solicitudBase.NroCuenta,
            .Cliente = solicitudBase.Cliente,
            .Totales = solicitudBase.Totales,
            .Articulos = solicitudBase.Articulos
        }

        resultado.ReintentadoComoFce = True
        Dim respuestaFce As FERespuestaWs = EnviarComprobante(solicitudFce, resultado)
        If respuestaFce Is Nothing Then
            Return resultado
        End If

        If respuestaFce.Exito Then
            CompletarResultadoExitoso(resultado, respuestaFce, solicitudFce)
            FCE = True
        Else
            Dim codigoError = ObtenerCodigoError(respuestaFce)
            CompletarResultadoError(resultado, respuestaFce, solicitudFce, codigoError)
        End If

        Return resultado
    End Function

    Public Sub ConfigurarEmisorAfipWsfe()
        EmisorComprobante = AddressOf EmitirPorAfipWsfe
    End Sub

    Private Function ContieneCodigoError(ByVal errores As List(Of FEErrorInfo), ByVal codigo As String) As Boolean
        If errores Is Nothing OrElse errores.Count = 0 OrElse String.IsNullOrWhiteSpace(codigo) Then
            Return False
        End If

        For Each er In errores
            If er IsNot Nothing AndAlso String.Equals(er.Codigo?.Trim(), codigo.Trim(), StringComparison.OrdinalIgnoreCase) Then
                Return True
            End If
        Next

        Return False
    End Function

    Private Function TryRecuperarCaePorConsulta(
        ByVal ws As ServiceSoapClient,
        ByVal auth As ar.gov.afip.dif.fev1.FEAuthRequest,
        ByVal ptoVta As Integer,
        ByVal cbteTipo As Integer,
        ByVal cbteNro As Long,
        ByRef cae As String,
        ByRef vto As String,
        ByVal errores As List(Of FEErrorInfo)
    ) As Boolean
        cae = ""
        vto = ""

        Try
            Dim consReq As New ar.gov.afip.dif.fev1.FECompConsultaReq With {
                .CbteNro = cbteNro,
                .CbteTipo = cbteTipo,
                .PtoVta = ptoVta
            }

            Dim consResp = ws.FECompConsultar(auth, consReq)

            If consResp IsNot Nothing AndAlso consResp.Errors IsNot Nothing AndAlso errores IsNot Nothing Then
                For Each er In consResp.Errors
                    errores.Add(New FEErrorInfo With {.Codigo = er.Code.ToString(), .Mensaje = er.Msg})
                Next
            End If

            Dim r = consResp?.ResultGet
            If r Is Nothing OrElse Not String.Equals(r.Resultado, "A", StringComparison.OrdinalIgnoreCase) Then
                Return False
            End If

            cae = If(r.CodAutorizacion, "")
            vto = If(r.FchVto, "")

            Return Not String.IsNullOrWhiteSpace(cae)
        Catch
            Return False
        End Try
    End Function

    Private Function EmitirPorAfipWsfe(ByVal sol As FESolicitudComprobante) As FERespuestaWs
        Dim r As New FERespuestaWs With {
            .Exito = False,
            .Cae = "",
            .Venicimiento = "",
            .NumeroComprobante = 0,
            .TipoComprobanteAfip = If(sol Is Nothing, 0, sol.TipoComprobante),
            .CodigoAfip = If(sol Is Nothing, 0, sol.TipoComprobante),
            .Mensaje = "",
            .Errores = New List(Of FEErrorInfo)()
        }

        If sol Is Nothing Then
            r.Mensaje = "Solicitud inválida."
            r.Errores.Add(New FEErrorInfo With {.Codigo = "REQ", .Mensaje = r.Mensaje})
            Return r
        End If

        If sol.Cliente Is Nothing Then
            r.Mensaje = "Cliente no informado."
            r.Errores.Add(New FEErrorInfo With {.Codigo = "CLI", .Mensaje = r.Mensaje})
            Return r
        End If

        Dim idLogin As Integer = If(sol.EsPrueba, 1, 2)
        Dim dtLogin As DataTable = DSM.ExecuteQuery(
            DSM.Stock,
            "SELECT  token, sign FROM LoginAfip WHERE IdLogin = @IdLogin",
            CmdParams("@IdLogin", idLogin)
        )
        If dtLogin Is Nothing OrElse dtLogin.Rows.Count = 0 Then
            r.Mensaje = "No existe token/sign en LoginAfip para IdLogin=" & idLogin
            r.Errores.Add(New FEErrorInfo With {.Codigo = "LOGIN", .Mensaje = r.Mensaje})
            Return r
        End If

        Dim token As String = dtLogin.Rows(0)("token").ToString()
        Dim sign As String = dtLogin.Rows(0)("sign").ToString()

        Dim auth As New ar.gov.afip.dif.fev1.FEAuthRequest With {
            .Token = token,
            .Sign = sign,
            .Cuit = General.CuitEmpresa.Replace("-", "")
        }

        Dim endpointName As String = If(sol.EsPrueba, "ServiceSoap_HOMO", "ServiceSoap")
        Using ws As New ServiceSoapClient(endpointName)
            Dim nroCbte As Long = sol.NroComprobante
            If nroCbte <= 0 Then
                Dim last = ws.FECompUltimoAutorizado(auth, sol.PuntoVenta, sol.TipoComprobante)
                nroCbte = CLng(last.CbteNro) + 1
            End If
            r.NumeroComprobante = nroCbte

            Dim docNro As Long = 0
            If Not String.IsNullOrWhiteSpace(sol.Cliente.Cuit) Then
                docNro = CLng(sol.Cliente.Cuit.Replace("-", ""))
            End If

            Dim cab As New ar.gov.afip.dif.fev1.FECAECabRequest With {
                .CantReg = 1,
                .PtoVta = sol.PuntoVenta,
                .CbteTipo = sol.TipoComprobante
            }

            Dim det As New ar.gov.afip.dif.fev1.FECAEDetRequest With {
                .Concepto = 1,
                .DocTipo = MapearTipoDocAfip(sol.Cliente.TipoDoc),
                .DocNro = docNro,
                .CbteDesde = nroCbte,
                .CbteHasta = nroCbte,
                .CbteFch = Date.Today.ToString("yyyyMMdd"),
                .ImpTotal = Math.Round(sol.Totales.TotalFinal, 2),
                .ImpTotConc = Math.Round(sol.Totales.TotNI, 2),
                .ImpNeto = Math.Round(sol.Totales.TotG, 2),
                .ImpOpEx = 0,
                .ImpTrib = Math.Round(sol.Totales.TotIB, 2),
                .ImpIVA = Math.Round(sol.Totales.TotI, 2),
                .MonId = "PES",
                .MonCotiz = 1,
                .CondicionIVAReceptorId = sol.Cliente.CodigoAfip
            }

            If det.ImpTrib > 0 Then
                ' AFIP exige que si ImpTrib > 0 también se envíe el array Tributos.
                ' Se informa IIBB (Id=7) por provincia usando Cliente.IIBBPorProvincia, sin depender de la tabla IngresosBrutos.
                Dim baseImp As Double = det.ImpNeto
                If baseImp <= 0 Then
                    baseImp = det.ImpTotal
                End If

                Dim tributosList As New List(Of ar.gov.afip.dif.fev1.Tributo)()
                Dim objetivoImpTrib As Double = det.ImpTrib
                Dim sumaImportes As Double = 0
                Dim ultimoIndice As Integer = -1

                If sol.Cliente IsNot Nothing AndAlso sol.Cliente.IIBBPorProvincia IsNot Nothing Then
                    Dim maxProv As Integer = Math.Min(24, sol.Cliente.IIBBPorProvincia.Length - 1)
                    For i As Integer = 1 To maxProv
                        Dim alicuota As Double = sol.Cliente.IIBBPorProvincia(i)
                        If alicuota > 0 Then
                            Dim importe As Double = Math.Round(baseImp * alicuota, 2)
                            If importe <> 0 Then
                                Dim trib As New ar.gov.afip.dif.fev1.Tributo With {
                                    .Id = 7,
                                    .Desc = "Percepción de IIBB (Prov " & i.ToString("00") & ")",
                                    .BaseImp = baseImp,
                                    .Alic = Math.Round(alicuota * 100, 6),
                                    .Importe = importe
                                }
                                tributosList.Add(trib)
                                sumaImportes += importe
                                ultimoIndice = tributosList.Count - 1
                            End If
                        End If
                    Next
                End If

                If tributosList.Count = 0 Then
                    Dim alic As Double = 0
                    If baseImp > 0 Then
                        alic = Math.Round((objetivoImpTrib / baseImp) * 100, 6)
                    End If

                    tributosList.Add(New ar.gov.afip.dif.fev1.Tributo With {
                        .Id = 7,
                        .Desc = "Percepción de IIBB",
                        .BaseImp = baseImp,
                        .Alic = alic,
                        .Importe = objetivoImpTrib
                    })
                    sumaImportes = objetivoImpTrib
                    ultimoIndice = 0
                Else
                    Dim diferencia As Double = Math.Round(objetivoImpTrib - sumaImportes, 2)
                    If diferencia <> 0 AndAlso ultimoIndice >= 0 Then
                        tributosList(ultimoIndice).Importe = Math.Round(tributosList(ultimoIndice).Importe + diferencia, 2)
                        sumaImportes += diferencia
                    End If
                End If

                det.Tributos = tributosList.ToArray()
            End If

            If sol.EsFce Then
                Dim fechaCbte As Date
                If Not Date.TryParseExact(det.CbteFch, "yyyyMMdd", Global.System.Globalization.CultureInfo.InvariantCulture, Global.System.Globalization.DateTimeStyles.None, fechaCbte) Then
                    fechaCbte = Date.Today
                End If

                det.FchVtoPago = fechaCbte.AddDays(30).ToString("yyyyMMdd")

                Dim opcionalesList As New List(Of ar.gov.afip.dif.fev1.Opcional)()
                opcionalesList.Add(New ar.gov.afip.dif.fev1.Opcional With {.Id = "2101", .Valor = If(General.CBUEmpresa, "").Trim()})
                opcionalesList.Add(New ar.gov.afip.dif.fev1.Opcional With {.Id = "2102", .Valor = If(General.AliasEmpresa, "").Trim()})
                opcionalesList.Add(New ar.gov.afip.dif.fev1.Opcional With {.Id = "27", .Valor = "SCA"})
                det.Opcionales = opcionalesList.ToArray()
            End If

            Dim IvaProd As New ar.gov.afip.dif.fev1.AlicIva With {
                .Id = 5,
                .BaseImp = Math.Round(sol.Totales.TotG, 2),
                .Importe = Math.Round(sol.Totales.TotI, 2)
            }
            det.Iva = New ar.gov.afip.dif.fev1.AlicIva() {IvaProd}

            Dim req As New ar.gov.afip.dif.fev1.FECAERequest With {
                .FeCabReq = cab,
                .FeDetReq = New ar.gov.afip.dif.fev1.FECAEDetRequest() {det}
            }

            Dim resp As ar.gov.afip.dif.fev1.FECAEResponse = Nothing
            Try
                resp = ws.FECAESolicitar(auth, req)
            Catch
                Dim caeRec As String = ""
                Dim vtoRec As String = ""
                Dim erroresRec As New List(Of FEErrorInfo)()
                If TryRecuperarCaePorConsulta(ws, auth, sol.PuntoVenta, sol.TipoComprobante, nroCbte, caeRec, vtoRec, erroresRec) Then
                    r.Exito = True
                    r.Cae = caeRec
                    r.Venicimiento = vtoRec
                    r.Mensaje = "Aprobado (recuperado por consulta)"
                    r.Errores = New List(Of FEErrorInfo)()
                    Return r
                End If
                Throw
            End Try

            If resp IsNot Nothing AndAlso resp.Errors IsNot Nothing Then
                For Each er In resp.Errors
                    r.Errores.Add(New FEErrorInfo With {.Codigo = er.Code.ToString(), .Mensaje = er.Msg})
                Next
            End If

            If resp IsNot Nothing AndAlso resp.FeDetResp IsNot Nothing AndAlso resp.FeDetResp.Length > 0 Then
                Dim detResp = resp.FeDetResp(0)

                If detResp IsNot Nothing AndAlso String.Equals(detResp.Resultado, "A", StringComparison.OrdinalIgnoreCase) Then
                    r.Exito = True
                    r.Cae = If(detResp.CAE, "")
                    r.Venicimiento = If(detResp.CAEFchVto, "")
                    r.Mensaje = "Aprobado"
                    Return r
                End If

                If detResp IsNot Nothing AndAlso detResp.Observaciones IsNot Nothing Then
                    For Each ob In detResp.Observaciones
                        r.Errores.Add(New FEErrorInfo With {.Codigo = ob.Code.ToString(), .Mensaje = ob.Msg})
                    Next
                End If
            End If

            If ContieneCodigoError(r.Errores, "10016") Then
                System.Windows.Forms.MessageBox.Show(
                    "El comprobante ya fue procesado en ARCA. Haga clic en Aceptar para guardar el registro en el sistema.",
                    "Comprobante ya procesado",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Information
                )

                Dim caeRec As String = ""
                Dim vtoRec As String = ""
                Dim erroresRec As New List(Of FEErrorInfo)()
                If TryRecuperarCaePorConsulta(ws, auth, sol.PuntoVenta, sol.TipoComprobante, nroCbte, caeRec, vtoRec, erroresRec) Then
                    r.Exito = True
                    r.Cae = caeRec
                    r.Venicimiento = vtoRec
                    r.Mensaje = "Aprobado (recuperado por consulta)"
                    r.Errores = New List(Of FEErrorInfo)()
                    Return r
                End If
                If erroresRec.Count > 0 Then
                    r.Errores.AddRange(erroresRec)
                End If
            End If

            If r.Errores.Count > 0 Then
                r.Mensaje = r.Errores(0).Mensaje
            Else
                r.Mensaje = "Rechazado"
            End If

            Return r
        End Using
    End Function

    Private Function EnviarComprobante(ByVal solicitud As FESolicitudComprobante, ByRef resultado As FEResultadoProceso) As FERespuestaWs
        If EmisorComprobante Is Nothing Then
            resultado.Mensaje = "No hay emisor AFIP configurado. Debe registrar la implementación del web service."
            Return Nothing
        End If

        Try
            Dim respuesta = EmisorComprobante.Invoke(solicitud)
            If respuesta Is Nothing Then
                resultado.Mensaje = "El emisor AFIP devolvió una respuesta vacía."
                Return Nothing
            End If
            Return respuesta
        Catch ex As Exception
            resultado.Mensaje = ex.Message
            Return Nothing
        End Try
    End Function

    Private Sub CompletarResultadoExitoso(ByRef resultado As FEResultadoProceso, ByVal respuesta As FERespuestaWs, ByVal solicitud As FESolicitudComprobante)
        resultado.Exito = True
        resultado.PuntoVenta = solicitud.PuntoVenta
        resultado.EsPrueba = solicitud.EsPrueba
        resultado.TipoComprobanteEmitido = solicitud.TipoComprobante
        resultado.NumeroComprobante = respuesta.NumeroComprobante
        resultado.Cae = If(respuesta.Cae, "")
        resultado.Venicimiento = If(respuesta.Venicimiento, "")
        resultado.CodigoAfip = solicitud.TipoComprobante
        resultado.CodigoError = ""
        If String.IsNullOrWhiteSpace(respuesta.Mensaje) Then
            resultado.Mensaje = "Comprobante emitido correctamente."
        Else
            resultado.Mensaje = respuesta.Mensaje
        End If
    End Sub

    Private Sub CompletarResultadoError(ByRef resultado As FEResultadoProceso, ByVal respuesta As FERespuestaWs, ByVal solicitud As FESolicitudComprobante, ByVal codigoError As String)
        resultado.Exito = False
        resultado.PuntoVenta = solicitud.PuntoVenta
        resultado.EsPrueba = solicitud.EsPrueba
        resultado.TipoComprobanteEmitido = solicitud.TipoComprobante
        resultado.NumeroComprobante = 0
        resultado.Cae = ""
        resultado.Venicimiento = ""
        resultado.CodigoAfip = solicitud.TipoComprobante
        resultado.CodigoError = If(codigoError, "").Trim()
        If String.IsNullOrWhiteSpace(respuesta.Mensaje) Then
            resultado.Mensaje = "AFIP rechazó el comprobante."
        Else
            resultado.Mensaje = respuesta.Mensaje
        End If
    End Sub

    Private Function ObtenerCodigoError(ByVal respuesta As FERespuestaWs) As String
        If respuesta Is Nothing OrElse respuesta.Errores Is Nothing OrElse respuesta.Errores.Count = 0 Then
            Return ""
        End If
        Dim codigo As String = respuesta.Errores(0).Codigo
        If codigo Is Nothing Then
            Return ""
        End If
        Return codigo.Trim()
    End Function

    Private Function ObtenerTipoComprobanteFce(ByVal tipoComprobanteNormal As Integer) As Integer
        Select Case tipoComprobanteNormal
            Case 1
                Return 201
            Case 6
                Return 206
            Case 19
                Return 211
            Case Else
                Return tipoComprobanteNormal
        End Select
    End Function

    Public Function EmitirFacturaElectronica(
        ByVal datosCliente As ClienteFiscalData,
        ByVal totales As TotalesResult,
        ByVal nroFactura As Long,
        ByVal plazoPactado As String,
        ByVal expreso As String,
        ByVal flete As String,
        ByVal idImputa As Integer
    ) As Boolean
        If EmisorComprobante Is Nothing Then
            ConfigurarEmisorAfipWsfe()
        End If

        Dim sol As New FESolicitudComprobante With {
            .IdPropio = General.propio,
            .PuntoVenta = Convert.ToInt32(PuntoVentaActual),
            .EsPrueba = PruebaElec,
            .TipoComprobante = datosCliente.Tipocomp,
            .EsFce = False,
            .NroComprobante = nroFactura,
            .NroCuenta = datosCliente.NroCuenta,
            .Cliente = datosCliente,
            .Totales = totales,
            .Articulos = Nothing
        }

        Dim resultado = ProcesarComprobanteElectronico(sol)
        Return resultado.Exito
    End Function

End Module
