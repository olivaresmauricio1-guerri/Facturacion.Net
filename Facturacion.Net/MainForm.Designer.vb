<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub


    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        MenuStrip = New MenuStrip()
        MnuConfiguracion = New ToolStripMenuItem()
        MnuConImp = New ToolStripMenuItem()
        LC1 = New ToolStripSeparator()
        mncalcu = New ToolStripMenuItem()
        mnalma = New ToolStripMenuItem()
        mnguion = New ToolStripSeparator()
        MnuSalir = New ToolStripMenuItem()
        MnuSto = New ToolStripMenuItem()
        mnvh = New ToolStripMenuItem()
        mncot = New ToolStripMenuItem()
        mnlistad = New ToolStripMenuItem()
        novb = New ToolStripMenuItem()
        MnuConsultas = New ToolStripMenuItem()
        mnlisnovk = New ToolStripMenuItem()
        mnstock = New ToolStripMenuItem()
        mng4 = New ToolStripSeparator()
        mnfac = New ToolStripMenuItem()
        mnrat55 = New ToolStripSeparator()
        mnfacpedi = New ToolStripMenuItem()
        ray66 = New ToolStripSeparator()
        mnnota = New ToolStripMenuItem()
        ray786 = New ToolStripSeparator()
        mntique = New ToolStripMenuItem()
        ray675 = New ToolStripSeparator()
        mnfa = New ToolStripMenuItem()
        mncre = New ToolStripMenuItem()
        mndebi = New ToolStripMenuItem()
        mnremi = New ToolStripMenuItem()
        mninter = New ToolStripMenuItem()
        mnrecubo = New ToolStripMenuItem()
        mnpresu = New ToolStripMenuItem()
        mnmerca = New ToolStripMenuItem()
        mnls3 = New ToolStripSeparator()
        mnfacrecon = New ToolStripMenuItem()
        ray777 = New ToolStripSeparator()
        mncaja = New ToolStripMenuItem()
        mnx = New ToolStripMenuItem()
        mnls4 = New ToolStripSeparator()
        MnuNom = New ToolStripMenuItem()
        mnremitos = New ToolStripMenuItem()
        MnuSeg = New ToolStripMenuItem()
        MnuSegInf = New ToolStripMenuItem()
        LS1 = New ToolStripSeparator()
        MnuSegSes = New ToolStripMenuItem()
        LS2 = New ToolStripSeparator()
        MnuSegINI = New ToolStripMenuItem()
        MnuVen = New ToolStripMenuItem()
        MnuAceVen = New ToolStripMenuItem()
        MnuVenCer = New ToolStripMenuItem()
        MnuVenTod = New ToolStripMenuItem()
        LV1 = New ToolStripSeparator()
        MnuVenCas = New ToolStripMenuItem()
        MnuVenVer = New ToolStripMenuItem()
        MnuVenHor = New ToolStripMenuItem()
        LV2 = New ToolStripSeparator()
        MnuVenIco = New ToolStripMenuItem()
        MnuVenImp = New ToolStripMenuItem()
        MnuAce = New ToolStripMenuItem()
        MnuAceAce = New ToolStripMenuItem()
        MnuAceAyu = New ToolStripMenuItem()
        ToolTip = New ToolTip(components)
        StatusBar1 = New StatusStrip()
        Panel1 = New ToolStripStatusLabel()
        Panel2 = New ToolStripStatusLabel()
        Panel3 = New ToolStripStatusLabel()
        Panel4 = New ToolStripStatusLabel()
        Timer1 = New Timer(components)
        MenuStrip.SuspendLayout()
        StatusBar1.SuspendLayout()
        SuspendLayout()
        ' 
        ' MenuStrip
        ' 
        MenuStrip.Items.AddRange(New ToolStripItem() {MnuConfiguracion, MnuSto, MnuConsultas, mnfac, MnuNom, MnuSeg, MnuVen, MnuAce})
        MenuStrip.Location = New Point(0, 0)
        MenuStrip.MdiWindowListItem = MnuAceVen
        MenuStrip.Name = "MenuStrip"
        MenuStrip.Padding = New Padding(7, 2, 0, 2)
        MenuStrip.Size = New Size(737, 24)
        MenuStrip.TabIndex = 5
        MenuStrip.Text = "MenuStrip"
        ' 
        ' MnuConfiguracion
        ' 
        MnuConfiguracion.DropDownItems.AddRange(New ToolStripItem() {MnuConImp, LC1, mncalcu, mnalma, mnguion, MnuSalir})
        MnuConfiguracion.Name = "MnuConfiguracion"
        MnuConfiguracion.Size = New Size(95, 20)
        MnuConfiguracion.Text = "&Configuración"
        ' 
        ' MnuConImp
        ' 
        MnuConImp.Name = "MnuConImp"
        MnuConImp.Size = New Size(186, 22)
        MnuConImp.Text = "Especificar &Impresora"
        ' 
        ' LC1
        ' 
        LC1.Name = "LC1"
        LC1.Size = New Size(183, 6)
        ' 
        ' mncalcu
        ' 
        mncalcu.Name = "mncalcu"
        mncalcu.Size = New Size(186, 22)
        mncalcu.Text = "&Calculadora"
        ' 
        ' mnalma
        ' 
        mnalma.Name = "mnalma"
        mnalma.Size = New Size(186, 22)
        mnalma.Text = "&Almanaque"
        ' 
        ' mnguion
        ' 
        mnguion.Name = "mnguion"
        mnguion.Size = New Size(183, 6)
        ' 
        ' MnuSalir
        ' 
        MnuSalir.Name = "MnuSalir"
        MnuSalir.Size = New Size(186, 22)
        MnuSalir.Text = "&Salir"
        ' 
        ' MnuSto
        ' 
        MnuSto.DropDownItems.AddRange(New ToolStripItem() {mnvh, mncot, mnlistad, novb})
        MnuSto.Name = "MnuSto"
        MnuSto.Size = New Size(61, 20)
        MnuSto.Text = "&Clientes"
        ' 
        ' mnvh
        ' 
        mnvh.Name = "mnvh"
        mnvh.Size = New Size(198, 22)
        mnvh.Text = "Recepción de Vehículos"
        ' 
        ' mncot
        ' 
        mncot.Name = "mncot"
        mncot.Size = New Size(198, 22)
        mncot.Text = "Completar OT"
        ' 
        ' mnlistad
        ' 
        mnlistad.Name = "mnlistad"
        mnlistad.Size = New Size(198, 22)
        mnlistad.Text = "&Listar Novedades"
        ' 
        ' novb
        ' 
        novb.Name = "novb"
        novb.Size = New Size(198, 22)
        novb.Text = "Novedades CtaCte"
        ' 
        ' MnuConsultas
        ' 
        MnuConsultas.DropDownItems.AddRange(New ToolStripItem() {mnlisnovk, mnstock, mng4})
        MnuConsultas.Name = "MnuConsultas"
        MnuConsultas.Size = New Size(48, 20)
        MnuConsultas.Text = "&Stock"
        ' 
        ' mnlisnovk
        ' 
        mnlisnovk.Name = "mnlisnovk"
        mnlisnovk.Size = New Size(165, 22)
        mnlisnovk.Text = "&Listar Novedades"
        ' 
        ' mnstock
        ' 
        mnstock.Name = "mnstock"
        mnstock.Size = New Size(165, 22)
        mnstock.Text = "Novedades Stock"
        ' 
        ' mng4
        ' 
        mng4.Name = "mng4"
        mng4.Size = New Size(162, 6)
        ' 
        ' mnfac
        ' 
        mnfac.DropDownItems.AddRange(New ToolStripItem() {mnrat55, mnfacpedi, ray66, mnnota, ray786, mntique, ray675, mnfa, mncre, mndebi, mnremi, mninter, mnrecubo, mnpresu, mnmerca, mnls3, mnfacrecon, ray777, mncaja, mnx, mnls4})
        mnfac.Name = "mnfac"
        mnfac.Size = New Size(81, 20)
        mnfac.Text = "&Facturación"
        ' 
        ' mnrat55
        ' 
        mnrat55.Name = "mnrat55"
        mnrat55.Size = New Size(212, 6)
        ' 
        ' mnfacpedi
        ' 
        mnfacpedi.Name = "mnfacpedi"
        mnfacpedi.Size = New Size(215, 22)
        mnfacpedi.Text = "Facturar Pedido"
        ' 
        ' ray66
        ' 
        ray66.Name = "ray66"
        ray66.Size = New Size(212, 6)
        ' 
        ' mnnota
        ' 
        mnnota.Name = "mnnota"
        mnnota.Size = New Size(215, 22)
        mnnota.Text = "&Remito y Factura"
        ' 
        ' ray786
        ' 
        ray786.Name = "ray786"
        ray786.Size = New Size(212, 6)
        ' 
        ' mntique
        ' 
        mntique.Name = "mntique"
        mntique.Size = New Size(215, 22)
        mntique.Text = "&Tique Autoshop"
        ' 
        ' ray675
        ' 
        ray675.Name = "ray675"
        ray675.Size = New Size(212, 6)
        ' 
        ' mnfa
        ' 
        mnfa.Name = "mnfa"
        mnfa.Size = New Size(215, 22)
        mnfa.Text = "&Factura"
        ' 
        ' mncre
        ' 
        mncre.Name = "mncre"
        mncre.Size = New Size(215, 22)
        mncre.Text = "Nota &de Crédito"
        ' 
        ' mndebi
        ' 
        mndebi.Name = "mndebi"
        mndebi.Size = New Size(215, 22)
        mndebi.Text = "Nota de Debito"
        ' 
        ' mnremi
        ' 
        mnremi.Name = "mnremi"
        mnremi.Size = New Size(215, 22)
        mnremi.Text = "&Remito a Cliente"
        ' 
        ' mninter
        ' 
        mninter.Name = "mninter"
        mninter.Size = New Size(215, 22)
        mninter.Text = "Remito &Uso Interno"
        ' 
        ' mnrecubo
        ' 
        mnrecubo.Name = "mnrecubo"
        mnrecubo.Size = New Size(215, 22)
        mnrecubo.Text = "Reci&bo"
        ' 
        ' mnpresu
        ' 
        mnpresu.Name = "mnpresu"
        mnpresu.Size = New Size(215, 22)
        mnpresu.Text = "Reimprimir Comprobantes"
        ' 
        ' mnmerca
        ' 
        mnmerca.Name = "mnmerca"
        mnmerca.Size = New Size(215, 22)
        mnmerca.Text = "Ingreso de Mercadería"
        ' 
        ' mnls3
        ' 
        mnls3.Name = "mnls3"
        mnls3.Size = New Size(212, 6)
        ' 
        ' mnfacrecon
        ' 
        mnfacrecon.Name = "mnfacrecon"
        mnfacrecon.Size = New Size(215, 22)
        mnfacrecon.Text = "&Facturar Reconstrucción"
        ' 
        ' ray777
        ' 
        ray777.Name = "ray777"
        ray777.Size = New Size(212, 6)
        ' 
        ' mncaja
        ' 
        mncaja.Name = "mncaja"
        mncaja.Size = New Size(215, 22)
        mncaja.Text = "Cierre de Ca&ja"
        ' 
        ' mnx
        ' 
        mnx.Name = "mnx"
        mnx.Size = New Size(215, 22)
        mnx.Text = "Cierres Fiscales"
        ' 
        ' mnls4
        ' 
        mnls4.Name = "mnls4"
        mnls4.Size = New Size(212, 6)
        ' 
        ' MnuNom
        ' 
        MnuNom.DropDownItems.AddRange(New ToolStripItem() {mnremitos})
        MnuNom.Name = "MnuNom"
        MnuNom.Size = New Size(103, 20)
        MnuNom.Text = "&Nomencladores"
        ' 
        ' mnremitos
        ' 
        mnremitos.Name = "mnremitos"
        mnremitos.Size = New Size(170, 22)
        mnremitos.Text = "Controlar Remitos"
        ' 
        ' MnuSeg
        ' 
        MnuSeg.DropDownItems.AddRange(New ToolStripItem() {MnuSegInf, LS1, MnuSegSes, LS2, MnuSegINI})
        MnuSeg.Name = "MnuSeg"
        MnuSeg.Size = New Size(72, 20)
        MnuSeg.Text = "&Seguridad"
        ' 
        ' MnuSegInf
        ' 
        MnuSegInf.Name = "MnuSegInf"
        MnuSegInf.Size = New Size(195, 22)
        MnuSegInf.Text = "&Informacion Reservada"
        ' 
        ' LS1
        ' 
        LS1.Name = "LS1"
        LS1.Size = New Size(192, 6)
        ' 
        ' MnuSegSes
        ' 
        MnuSegSes.Name = "MnuSegSes"
        MnuSegSes.Size = New Size(195, 22)
        MnuSegSes.Text = "&Iniciar Sesion"
        ' 
        ' LS2
        ' 
        LS2.Name = "LS2"
        LS2.Size = New Size(192, 6)
        ' 
        ' MnuSegINI
        ' 
        MnuSegINI.Name = "MnuSegINI"
        MnuSegINI.Size = New Size(195, 22)
        MnuSegINI.Text = "&Editar .INI"
        ' 
        ' MnuVen
        ' 
        MnuVen.DropDownItems.AddRange(New ToolStripItem() {MnuAceVen, MnuVenCer, MnuVenTod, LV1, MnuVenCas, MnuVenVer, MnuVenHor, LV2, MnuVenIco, MnuVenImp})
        MnuVen.Name = "MnuVen"
        MnuVen.Size = New Size(66, 20)
        MnuVen.Text = "&Ventanas"
        ' 
        ' MnuAceVen
        ' 
        MnuAceVen.Name = "MnuAceVen"
        MnuAceVen.Size = New Size(174, 22)
        MnuAceVen.Text = "Ventanas &Activas"
        ' 
        ' MnuVenCer
        ' 
        MnuVenCer.Name = "MnuVenCer"
        MnuVenCer.Size = New Size(174, 22)
        MnuVenCer.Text = "&Cerrar"
        ' 
        ' MnuVenTod
        ' 
        MnuVenTod.Name = "MnuVenTod"
        MnuVenTod.Size = New Size(174, 22)
        MnuVenTod.Text = "Cerrar &Todas"
        ' 
        ' LV1
        ' 
        LV1.Name = "LV1"
        LV1.Size = New Size(171, 6)
        ' 
        ' MnuVenCas
        ' 
        MnuVenCas.Name = "MnuVenCas"
        MnuVenCas.Size = New Size(174, 22)
        MnuVenCas.Text = "&Cascada"
        ' 
        ' MnuVenVer
        ' 
        MnuVenVer.Name = "MnuVenVer"
        MnuVenVer.Size = New Size(174, 22)
        MnuVenVer.Text = "&Vertical"
        ' 
        ' MnuVenHor
        ' 
        MnuVenHor.Name = "MnuVenHor"
        MnuVenHor.Size = New Size(174, 22)
        MnuVenHor.Text = "&Horizontal"
        ' 
        ' LV2
        ' 
        LV2.Name = "LV2"
        LV2.Size = New Size(171, 6)
        ' 
        ' MnuVenIco
        ' 
        MnuVenIco.Name = "MnuVenIco"
        MnuVenIco.Size = New Size(174, 22)
        MnuVenIco.Text = "Reorganizar &Iconos"
        ' 
        ' MnuVenImp
        ' 
        MnuVenImp.Name = "MnuVenImp"
        MnuVenImp.Size = New Size(174, 22)
        MnuVenImp.Text = "&Imprimir Ventana"
        ' 
        ' MnuAce
        ' 
        MnuAce.DropDownItems.AddRange(New ToolStripItem() {MnuAceAce, MnuAceAyu})
        MnuAce.Name = "MnuAce"
        MnuAce.Size = New Size(24, 20)
        MnuAce.Text = "&?"
        ' 
        ' MnuAceAce
        ' 
        MnuAceAce.Name = "MnuAceAce"
        MnuAceAce.Size = New Size(135, 22)
        MnuAceAce.Text = "&Acerca de..."
        ' 
        ' MnuAceAyu
        ' 
        MnuAceAyu.Name = "MnuAceAyu"
        MnuAceAyu.Size = New Size(135, 22)
        MnuAceAyu.Text = "A&yuda"
        ' 
        ' StatusBar1
        ' 
        StatusBar1.ImageScalingSize = New Size(20, 20)
        StatusBar1.Items.AddRange(New ToolStripItem() {Panel1, Panel2, Panel3, Panel4})
        StatusBar1.Location = New Point(0, 501)
        StatusBar1.Name = "StatusBar1"
        StatusBar1.Padding = New Padding(1, 0, 16, 0)
        StatusBar1.Size = New Size(737, 22)
        StatusBar1.TabIndex = 9
        StatusBar1.Text = "StatusStrip1"
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.Silver
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(574, 17)
        Panel1.Spring = True
        Panel1.Text = "Sistema de Facturación"
        Panel1.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.Silver
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(65, 17)
        Panel2.Text = "01/01/2024"
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.Silver
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(34, 17)
        Panel3.Text = "00:00"
        ' 
        ' Panel4
        ' 
        Panel4.BackColor = Color.Silver
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(47, 17)
        Panel4.Text = "Usuario"
        ' 
        ' Timer1
        ' 
        Timer1.Interval = 1000
        ' 
        ' MainForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(737, 523)
        Controls.Add(StatusBar1)
        Controls.Add(MenuStrip)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        IsMdiContainer = True
        MainMenuStrip = MenuStrip
        Margin = New Padding(4, 3, 4, 3)
        Name = "MainForm"
        Text = "Sistema de Facturación"
        MenuStrip.ResumeLayout(False)
        MenuStrip.PerformLayout()
        StatusBar1.ResumeLayout(False)
        StatusBar1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    
    Friend WithEvents MnuConfiguracion As ToolStripMenuItem
    Friend WithEvents MnuConImp As ToolStripMenuItem
    Friend WithEvents LC1 As ToolStripSeparator
    Friend WithEvents mncalcu As ToolStripMenuItem
    Friend WithEvents mnalma As ToolStripMenuItem
    Friend WithEvents mnguion As ToolStripSeparator
    Friend WithEvents MnuSalir As ToolStripMenuItem

    Friend WithEvents MnuSto As ToolStripMenuItem
    Friend WithEvents mnvh As ToolStripMenuItem
    Friend WithEvents mncot As ToolStripMenuItem
    Friend WithEvents mnlistad As ToolStripMenuItem
    Friend WithEvents novb As ToolStripMenuItem

    Friend WithEvents MnuConsultas As ToolStripMenuItem
    Friend WithEvents mnlisnovk As ToolStripMenuItem
    Friend WithEvents mnstock As ToolStripMenuItem
    Friend WithEvents mng4 As ToolStripSeparator

    Friend WithEvents mnfac As ToolStripMenuItem
    Friend WithEvents mnrat55 As ToolStripSeparator
    Friend WithEvents mnfacpedi As ToolStripMenuItem
    Friend WithEvents ray66 As ToolStripSeparator
    Friend WithEvents mnnota As ToolStripMenuItem
    Friend WithEvents ray786 As ToolStripSeparator
    Friend WithEvents mntique As ToolStripMenuItem
    Friend WithEvents ray675 As ToolStripSeparator
    Friend WithEvents mnfa As ToolStripMenuItem
    Friend WithEvents mncre As ToolStripMenuItem
    Friend WithEvents mndebi As ToolStripMenuItem
    Friend WithEvents mnremi As ToolStripMenuItem
    Friend WithEvents mninter As ToolStripMenuItem
    Friend WithEvents mnrecubo As ToolStripMenuItem
    Friend WithEvents mnpresu As ToolStripMenuItem
    Friend WithEvents mnmerca As ToolStripMenuItem
    Friend WithEvents mnls3 As ToolStripSeparator
    Friend WithEvents mnfacrecon As ToolStripMenuItem
    Friend WithEvents ray777 As ToolStripSeparator
    Friend WithEvents mncaja As ToolStripMenuItem
    Friend WithEvents mnx As ToolStripMenuItem
    Friend WithEvents mnls4 As ToolStripSeparator

    Friend WithEvents MnuNom As ToolStripMenuItem
    Friend WithEvents mnremitos As ToolStripMenuItem

    Friend WithEvents MnuSeg As ToolStripMenuItem
    Friend WithEvents MnuSegInf As ToolStripMenuItem
    Friend WithEvents LS1 As ToolStripSeparator
    Friend WithEvents MnuSegSes As ToolStripMenuItem
    Friend WithEvents LS2 As ToolStripSeparator
    Friend WithEvents MnuSegINI As ToolStripMenuItem

    Friend WithEvents MnuVen As ToolStripMenuItem
    Friend WithEvents MnuAceVen As ToolStripMenuItem
    Friend WithEvents MnuVenCer As ToolStripMenuItem
    Friend WithEvents MnuVenTod As ToolStripMenuItem
    Friend WithEvents LV1 As ToolStripSeparator
    Friend WithEvents MnuVenCas As ToolStripMenuItem
    Friend WithEvents MnuVenVer As ToolStripMenuItem
    Friend WithEvents MnuVenHor As ToolStripMenuItem
    Friend WithEvents LV2 As ToolStripSeparator
    Friend WithEvents MnuVenIco As ToolStripMenuItem
    Friend WithEvents MnuVenImp As ToolStripMenuItem

    Friend WithEvents MnuAce As ToolStripMenuItem
    Friend WithEvents MnuAceAce As ToolStripMenuItem
    Friend WithEvents MnuAceAyu As ToolStripMenuItem
    Friend WithEvents StatusBar1 As StatusStrip
    Friend WithEvents Panel1 As ToolStripStatusLabel
    Friend WithEvents Panel2 As ToolStripStatusLabel
    Friend WithEvents Panel3 As ToolStripStatusLabel
    Friend WithEvents Panel4 As ToolStripStatusLabel
    Friend WithEvents Timer1 As Timer

End Class
