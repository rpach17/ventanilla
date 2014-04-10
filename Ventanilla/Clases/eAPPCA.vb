Public Class eAPPCA
    Shared ctx As New Entidades(My.Settings.ConexionEntity)

#Region "Inicio de sesión"
    Public Shared Function Login(ByVal user As String, ByVal pass As String) As Boolean
        Dim usu = (From users In ctx.USUARIOS
                   From per In users.PERFILES
                   Where users.USUARIO = user AndAlso users.CONTRASENA = pass AndAlso users.ESTADO = 1 AndAlso per.IDPERFIL = My.Settings.IDPerfil
                   Select users).ToList()

        If usu.Count = 1 Then
            For Each u In usu
                With SesionActiva
                    .IdUsuario = u.IDUSUARIO
                    .Nombre = String.Format("{0} {1}", u.NOMBRE, u.APELLIDOS)
                    .Usuario = u.USUARIO
                    .Sucursal = u.DETALLE_SUCURSAL_OFICINA.SUCURSALES.NOMBRE
                    .Oficina = u.DETALLE_SUCURSAL_OFICINA.OFICINAS.NOMBRE_OFICINA
                    .IdSucursalOficina = u.IDDETALLE_SUCURSAL_OFICINA
                End With
            Next
            Return True
        Else
            Return False
        End If
    End Function
#End Region

#Region "Configuracion Ventanilla"
    Public Shared Sub CargarVentanillas(ByVal cbo As ComboBox)
        Dim ven = (From v In ctx.VENTANILLAS
                  Where v.IDDETALLE_SUCURSAL_OFICINA = SesionActiva.IdSucursalOficina
                  Select v)
        cbo.DataSource = ven
        cbo.DisplayMember = "NUMERO_VENTANILLA"
        cbo.ValueMember = "IDVENTANILLA"
    End Sub
#End Region

#Region "Datos Ventanilla"
    Public Shared Sub ticketEspera(ByVal grid As DataGridView)
        Dim f = ctx.CreateQuery(Of Date)("CurrentDateTime()")
        Dim dia As Date = f.AsEnumerable().First()

        'Dim ges = (From p In ctx.PETICION_GESTIONES
        '          Where p.ENESPERA = 1 AndAlso p.IDDETALLE_SUCURSAL_OFICINA = SesionActiva.IdSucursalOficina AndAlso p.FECHAHORA_PETICION = dia
        '          Select p.IDPETICION, Ticket = p.GESTIONES.CODIGO + p.SECUENCIA, p.GESTIONES.CODIGO, p.SECUENCIA).ToList()

        ctx.Refresh(Objects.RefreshMode.StoreWins, ctx.PETICION_GESTIONES)
        Dim ges = (From p In ctx.PETICION_GESTIONES.ToList()
                  Where p.ENESPERA = 1 AndAlso p.IDDETALLE_SUCURSAL_OFICINA = SesionActiva.IdSucursalOficina AndAlso p.FECHAHORA_PETICION.DayOfYear = dia.DayOfYear
                  Select New With {p.IDPETICION, .Ticket = p.GESTIONES.CODIGO & "" & p.SECUENCIA, p.GESTIONES.CODIGO, p.SECUENCIA}).ToList()

        grid.DataSource = ges
        grid.Columns(0).Visible = False
        grid.Columns(2).Visible = False
        grid.Columns(3).Visible = False
    End Sub

    Public Shared Sub ticketEspecial(ByVal grid As DataGridView)
        Dim lista As List(Of Decimal?) = New List(Of Decimal?)

        Dim f = ctx.CreateQuery(Of Date)("CurrentDateTime()")
        Dim dia As DateTime = f.AsEnumerable().First()

        ctx.Refresh(Objects.RefreshMode.StoreWins, ctx.PETICION_GESTIONES)
        'ctx.Refresh(Objects.RefreshMode.StoreWins, ctx.VENTANILLAS)

        lista = (From g In ctx.DETALLE_OFICINA_GESTIONES.ToList()
                 From v In g.VENTANILLAS.ToList()
                 Where v.IDVENTANILLA = My.Settings.IdVentanilla
                 Select g.IDGESTION).ToList()

        Dim ges = (From p In ctx.PETICION_GESTIONES.ToList()
                   Where p.TERCERAEDAD = 1 AndAlso p.ATENDIDO = 0 AndAlso p.IDDETALLE_SUCURSAL_OFICINA = SesionActiva.IdSucursalOficina AndAlso lista.Contains(p.IDGESTION) AndAlso p.FECHAHORA_PETICION.DayOfYear = dia.DayOfYear
                   Select New With {p.IDPETICION, .Ticket = p.GESTIONES.CODIGO & "" & p.SECUENCIA, p.GESTIONES.CODIGO, p.SECUENCIA}).ToList()

        grid.DataSource = ges
        grid.Columns(0).Visible = False
        grid.Columns(2).Visible = False
        grid.Columns(3).Visible = False
    End Sub

    Public Shared Sub PonerEspera(ByVal idp As Integer, Optional ByVal espera As Integer = 1)
        If idp <> 0 Then
            Dim t As PETICION_GESTIONES = (From p In ctx.PETICION_GESTIONES
                Where p.IDPETICION = idp
                Select p).SingleOrDefault
            t.ENESPERA = espera
            ctx.SaveChanges()
        End If
    End Sub

    Public Shared Sub FinAtencion(ByVal idp As Integer)
        Dim f = ctx.CreateQuery(Of Date)("CurrentDateTime()")
        Dim dia As Date = f.AsEnumerable().First()
        Dim t As PETICION_GESTIONES = (From p In ctx.PETICION_GESTIONES
                Where p.IDPETICION = idp
                Select p).SingleOrDefault
        t.FECHAHORA_FINALIZAR = dia
        ctx.SaveChanges()
    End Sub

#End Region

End Class
