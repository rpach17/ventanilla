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

#Region "Trámite"
    Shared Function BuscarResponsable(ByVal identidad As String, _
                                 ByVal txtPN As TextBox, _
                                 ByVal txtSN As TextBox, _
                                 ByVal txtPA As TextBox, _
                                 ByVal txtSA As TextBox, _
                                 ByVal txtT As TextBox, _
                                 ByVal txtC As TextBox, _
                                 ByVal txtEmail As TextBox, _
                                 ByVal lbl As Label) As Integer

        ' 1- Si existe localmente (Para hacer UPDATE)
        ' 2- No existe y vienen los datos del RNP (INSERT)
        ' 0- No existe en local ni en DB de RNP (Mostrar mensaje)


        Dim conteo As Integer = (From r In ctx.RESPONSABLE.ToList
                                Where r.NUMERO_IDENTIDAD = identidad
                                Select r).Count

        If conteo = 1 Then
            Dim respon = (From r In ctx.RESPONSABLE.ToList
                               Where r.NUMERO_IDENTIDAD = identidad
                               Select r).SingleOrDefault

            txtPN.Text = respon.PRIMER_NOMBRE
            txtSN.Text = respon.SEGUNDO_NOMBRE
            txtPA.Text = respon.PRIMER_APELLIDO
            txtSA.Text = respon.SEGUNDO_APELLIDO
            txtT.Text = respon.TELEFONO
            txtC.Text = respon.CELULAR
            txtEmail.Text = respon.CORREO

            Return 1
        Else
            'Buscar en las BD del registro
            lbl.Visible = True

            Return 0
        End If
    End Function

    Shared Function ObtenerRequisitos(ByVal idGestion As Integer) As List(Of REQUISITOS)
        Return (From req In ctx.REQUISITOS Where req.IDGESTION = idGestion Order By req.IDREQUISITO Select req).ToList()
    End Function

    Shared Function ActualizarResponsable(ByVal identidad As String, ByVal tel As String, ByVal cel As String, ByVal correo As String) As Integer
        Dim r As RESPONSABLE = (From re In ctx.RESPONSABLE
                                   Where re.NUMERO_IDENTIDAD = identidad
                                   Select re).SingleOrDefault
        r.TELEFONO = tel
        r.CELULAR = cel
        r.CORREO = correo

        ctx.SaveChanges()

        Return r.IDRESPONSABLE
    End Function

    Shared Function obtenerIdGestion(ByVal idP As Integer) As Integer
        Return (From pg In ctx.PETICION_GESTIONES Where pg.IDPETICION = idP Select pg.IDGESTION).FirstOrDefault
    End Function

    Shared Sub AgregarRequisito(ByVal req As RECEPCION_REQUISITOS)
        Try
            ctx.RECEPCION_REQUISITOS.AddObject(req)
            ctx.SaveChanges()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

#End Region



End Class
