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

End Class
