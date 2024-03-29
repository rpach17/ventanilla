﻿Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports System.Threading

Public Class frmVentanilla
    Dim cnn As New OracleConnection(My.Settings.Miconexion)
    Dim WithEvents socketCliente As New SocketCliente
    Public IDPeticion As Integer = 0
    Dim codigoSep As String = ""

    'Dim timer As System.Threading.Timer
    Dim hiloConexion As Thread

    Private Sub frmVentanilla_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        CargarTramitesAtrasados()

        tmNotificacion.Enabled = True
        'Ubica la pantalla en la esquina inferior derecha
        Dim src = Screen.FromPoint(Location)
        Location = New Point(src.WorkingArea.Right - Me.Width, src.WorkingArea.Bottom - Me.Height)
        MyBase.OnActivated(e)

        'Permite que se cambien las propiedades de un control desde un hilo de ejecucion
        Control.CheckForIllegalCrossThreadCalls = False

        'Se deshabilitan los controles por defecto, para reactivarlos cuando la ventanilla se conecte a la pantalla
        For Each ctrl In Me.Controls
            Dim miCtrl As Control = DirectCast(ctrl, Control)
            If miCtrl.Tag Is Nothing Then
                miCtrl.Enabled = False
            End If
        Next

        'Comienza un timer basado en un hilo de ejecucion
        'timer = New System.Threading.Timer(New TimerCallback(AddressOf VerificarConexion), Nothing, 0, 2000)
        hiloConexion = New Thread(AddressOf VerificarConexion)
        hiloConexion.Start()

        TicketsEnEspera()
        TicketsAtencionEspecial()

        Text = String.Format("[{0}] - {1} - {2}", SesionActiva.Usuario, SesionActiva.Sucursal, SesionActiva.Oficina)
        lblNumVentanilla.Text = String.Format("Ventanilla #{0}", My.Settings.NumeroVentanilla)
        Atencion(0, "Ventanilla fuera de atención")
    End Sub

    Private Sub TicketsEnEspera()
        dgvEnEspera.Enabled = False
        dgvEnEspera.Cursor = Cursors.WaitCursor
        eAPPCA.ticketEspera(dgvEnEspera)
        dgvEnEspera.Enabled = True
        dgvEnEspera.Cursor = Cursors.Default
    End Sub

    Private Sub TicketsAtencionEspecial()
        eAPPCA.ticketEspecial(dgvEspecial)
    End Sub

    Private Sub Atencion(ByVal IDP As Integer, Optional ByVal msj As String = "", Optional ByVal codigo As String = "", Optional ByVal secuencia As String = "")
        If IDP = 0 Then
            lblTicketActual.BackColor = Color.Orange
            lblTicketActual.ForeColor = Color.White
            lblTicketActual.Text = msj
            btnPonerEspera.Enabled = False
            tmBuscaPG.Enabled = True
        Else
            lblTicketActual.BackColor = Color.Green
            lblTicketActual.ForeColor = Color.White
            lblTicketActual.Text = String.Format("Atendiendo el ticket {0}{1}", codigo, secuencia)
            btnPonerEspera.Enabled = True
            tmBuscaPG.Enabled = False
        End If
    End Sub

    Private Sub VerificarConexion()
        While True
            Try
                socketCliente.IP = My.Settings.Host
                socketCliente.Puerto = 11000

                socketCliente.Conectar()

                'Timer.Change(Timeout.Infinite, Timeout.Infinite) 'Disabled timer

                For Each ctrl In Me.Controls
                    Dim miCtrl As Control = DirectCast(ctrl, Control)
                    miCtrl.Enabled = True
                Next

                PanelConexion.Visible = False
                If IDPeticion = 0 Then
                    btnPonerEspera.Enabled = False
                End If

                'conectado = True
                Exit While
            Catch ex As Exception

                For Each ctrl In Me.Controls
                    Dim miCtrl As Control = DirectCast(ctrl, Control)
                    If miCtrl.Tag Is Nothing Then
                        miCtrl.Enabled = False
                    End If
                Next

                PanelConexion.Visible = True

                'conectado = False
                'Exit While
                'Thread.Sleep(1000)
            End Try
        End While

    End Sub

    Private Sub btnTerminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTerminar.Click
        If IDPeticion = 0 Then
            Atencion(IDPeticion, "Ventanilla fuera de atención")
            Exit Sub
        Else
            eAPPCA.FinAtencion(IDPeticion)
            IDPeticion = 0
            Atencion(IDPeticion, "Ventanilla fuera de atención")
        End If
    End Sub

    Private Function SepararCodigo(ByVal cod As String) As String
        For i As Integer = cod.Length - 1 To 1 Step -1
            cod = cod.Insert(i, " ")
        Next
        Return cod
    End Function

    Private Sub LlamadoEnPantalla(ByVal codigo As String, ByVal secuencia As Integer, ByVal flag As Integer)
        If IDPeticion > 0 Then
            If flag = 1 Then 'El flag es para rellamar en la pantalla
                socketCliente.EnviarDatos(String.Format("{0}@{1}@{2}", codigoSep, My.Settings.NumeroVentanilla, flag))
            Else
                codigoSep = String.Format("{0} {1}", SepararCodigo(codigo), secuencia)
                socketCliente.EnviarDatos(String.Format("{0}@{1}@{2}", codigoSep, My.Settings.NumeroVentanilla, flag))
            End If
        End If
    End Sub

    Private Sub frmVentanilla_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If IDPeticion <> 0 Then
            eAPPCA.FinAtencion(IDPeticion)
        End If
        frmTrm.Close()
        'Try
        '    hiloConexion.Abort()
        '    socketCliente.Desconectar()

        '    'End
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try

        Try
            Application.ExitThread()
            End
        Catch ex As Exception
        End Try

    End Sub

    Private Sub btnPonerEspera_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPonerEspera.Click
        eAPPCA.PonerEspera(IDPeticion)
        TicketsEnEspera()
        IDPeticion = 0
        Atencion(0, "Ticket en espera")
    End Sub

    Private Sub btnLlamar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLlamar.Click
        Dim codigo, secuencia As String
        Try
            frmTrm.btnCrear.Enabled = True ' Habilita el botón del tramite ya que se desactivó cuando se clickeó
            Using myCMD As New OracleCommand() With _
            {
                .Connection = cnn, _
                .CommandText = "SP_LLAMARTICKET", _
                .CommandType = CommandType.StoredProcedure
            }

                cnn.Open()

                With myCMD
                    .Parameters.Add("VIDDETALLESO", OracleDbType.Decimal, 10, Nothing, ParameterDirection.Input).Value = SesionActiva.IdSucursalOficina
                    .Parameters.Add("VIDVENTANILLA", OracleDbType.Decimal, 10, Nothing, ParameterDirection.Input).Value = My.Settings.IdVentanilla
                    .Parameters.Add("VIDUSUARIO", OracleDbType.Decimal, 10, Nothing, ParameterDirection.Input).Value = SesionActiva.IdUsuario

                    .Parameters.Add("VIDPETICION", OracleDbType.Decimal, 10, Nothing, ParameterDirection.InputOutput).Value = IDPeticion
                    .Parameters.Add("CODIGO", OracleDbType.NVarchar2, 10, Nothing, ParameterDirection.Output)
                    .Parameters.Add("SECUENCIA", OracleDbType.Decimal, 10, Nothing, ParameterDirection.Output)

                    .ExecuteNonQuery()
                End With

                IDPeticion = Val(myCMD.Parameters("VIDPETICION").Value.ToString)

                'Obtener el IDGestion en base a la IDPeticion


                'LlamadoEnPantalla(myCMD.Parameters("CODIGO").Value.ToString, myCMD.Parameters("SECUENCIA").Value.ToString, 0)
                codigo = myCMD.Parameters("CODIGO").Value.ToString
                secuencia = myCMD.Parameters("SECUENCIA").Value.ToString
                Atencion(IDPeticion, "En este momento no hay Tickets en espera", CType(myCMD.Parameters("CODIGO").Value.ToString, String), CType(myCMD.Parameters("SECUENCIA").Value.ToString, String))

                btnLlamar.Enabled = False
                Timer1.Enabled = True
            End Using

            'Refrescar los listados de tickets en cada llamado
            'TicketsAtencionEspecial()
            'TicketsEnEspera()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Oracle Error")
        Finally
            cnn.Close()
        End Try

        LlamadoEnPantalla(codigo, secuencia, 0)
        'Refrescar los listados de tickets en cada llamado
        TicketsAtencionEspecial()
        TicketsEnEspera()
    End Sub

    Private Sub dgvEnEspera_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles dgvEnEspera.DoubleClick
        If IDPeticion = 0 Then
            frmTrm.btnCrear.Enabled = True ' Habilita el botón del tramite ya que se desactivó cuando se clickeó
            IDPeticion = DameID(dgvEnEspera, 0)
            Atencion(IDPeticion, , DameID(dgvEnEspera, 2), DameID(dgvEnEspera, 3))
            eAPPCA.PonerEspera(IDPeticion, 0)

            LlamadoEnPantalla(DameID(dgvEnEspera, 2), DameID(dgvEnEspera, 3), 0)

            'Refrescar los listados de tickets en cada llamado
            TicketsAtencionEspecial()
            TicketsEnEspera()
        Else
            Dim ticket As String = DameID(dgvEnEspera, 1)
            If MsgBox(String.Format("¿Desea finalizar la gestión actual y atender el ticket {0}?", ticket), MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirme") = MsgBoxResult.Yes Then

                frmTrm.btnCrear.Enabled = True ' Habilita el botón del tramite ya que se desactivó cuando se clickeó

                eAPPCA.FinAtencion(IDPeticion)
                IDPeticion = DameID(dgvEnEspera, 0)
                Atencion(IDPeticion, , DameID(dgvEnEspera, 2), DameID(dgvEnEspera, 3))
                eAPPCA.PonerEspera(IDPeticion, 0)

                LlamadoEnPantalla(DameID(dgvEnEspera, 2), DameID(dgvEnEspera, 3), 0)

                'Refrescar los listados de tickets en cada llamado
                TicketsAtencionEspecial()
                TicketsEnEspera()
            End If
        End If
    End Sub

    Private Sub btnLlamarEspecial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLlamarEspecial.Click
        Dim codigo As String
        Dim secuencia As String

        Try
            frmTrm.btnCrear.Enabled = True ' Habilita el botón del tramite ya que se desactivó cuando se clickeó

            Dim myCMD As New OracleCommand()
            myCMD.Connection = cnn
            myCMD.CommandText = "SP_LLAMARTICKET_AE"
            myCMD.CommandType = CommandType.StoredProcedure

            myCMD.Parameters.Add("VIDDETALLESO", OracleDbType.Decimal, 10, Nothing, ParameterDirection.Input).Value = SesionActiva.IdSucursalOficina
            myCMD.Parameters.Add("VIDVENTANILLA", OracleDbType.Decimal, 10, Nothing, ParameterDirection.Input).Value = My.Settings.IdVentanilla
            myCMD.Parameters.Add("VIDUSUARIO", OracleDbType.Decimal, 10, Nothing, ParameterDirection.Input).Value = SesionActiva.IdUsuario

            myCMD.Parameters.Add("VIDPETICION", OracleDbType.Decimal, 10, Nothing, ParameterDirection.InputOutput).Value = IDPeticion
            myCMD.Parameters.Add("CODIGO", OracleDbType.NVarchar2, 10, Nothing, ParameterDirection.Output)
            myCMD.Parameters.Add("SECUENCIA", OracleDbType.Decimal, 10, Nothing, ParameterDirection.Output)

            cnn.Open()
            myCMD.ExecuteNonQuery()
            IDPeticion = Val(myCMD.Parameters("VIDPETICION").Value.ToString)

            codigo = myCMD.Parameters("CODIGO").Value.ToString
            secuencia = myCMD.Parameters("SECUENCIA").Value.ToString
            'LlamadoEnPantalla(myCMD.Parameters("CODIGO").Value.ToString, myCMD.Parameters("SECUENCIA").Value.ToString, 0)

            Atencion(IDPeticion, "En este momento no hay tickets en espera", CType(myCMD.Parameters("CODIGO").Value.ToString, String), CType(myCMD.Parameters("SECUENCIA").Value.ToString, String))

            btnLlamarEspecial.Enabled = False
            Timer2.Enabled = True

            'Refrescar los listados de tickets en cada llamado
            'TicketsAtencionEspecial()
            'TicketsEnEspera()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString)
        Finally
            cnn.Close()
        End Try
        LlamadoEnPantalla(codigo, secuencia, 0)
        TicketsAtencionEspecial()
        TicketsEnEspera()
    End Sub

    Private Sub socketCliente_ConexionTerminada() Handles socketCliente.ConexionTerminada
        'timer.Change(0, 2000) 'Habilita el timer
        hiloConexion.Abort()

        For Each ctrl In Me.Controls
            Dim miCtrl As Control = DirectCast(ctrl, Control)
            If miCtrl.Tag Is Nothing Then
                miCtrl.Enabled = False
            End If
        Next

        PanelConexion.Visible = True

        hiloConexion = New Thread(AddressOf VerificarConexion)
        hiloConexion.Start()
    End Sub

    Private Sub btnReinicar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReinicar.Click
        'frmLogin.Show()
        'Close()
        'frmTramite.ShowDialog()
    End Sub

    Private Sub btnRellamar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRellamar.Click
        LlamadoEnPantalla(codigoSep, 0, 1)
    End Sub

    Dim i As Integer = 0

    Private Sub dgvEnEspera_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvEnEspera.MouseEnter
        TicketsAtencionEspecial()
        TicketsEnEspera()
    End Sub

    Private Sub btnTramite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTramite.Click
        'With frmTrm
        '    .ShowDialog()
        'End With
        'If IDPeticion <> 0 Then
        '    Dim infGestion As eAPPCA.InfoGestion = eAPPCA.obtenerIdGestion(IDPeticion)


        '    With frmTramite
        '        .IdGestion1 = infGestion.IdGestion
        '        .NombreGestion1 = infGestion.NombreGestion
        '        .Show()
        '    End With
        'End If
    End Sub

    Dim contador As Integer = 0

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If contador = 10 Then
            btnLlamar.Enabled = True
            Timer1.Enabled = False
            contador = 0
            btnLlamar.Text = "Llamar"
        Else
            btnLlamar.Text = 10 - contador
            contador += 1
        End If
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If contador = 10 Then
            btnLlamarEspecial.Enabled = True
            Timer2.Enabled = False
            contador = 0
            btnLlamarEspecial.Text = "Llamar asistencia"
        Else
            btnLlamarEspecial.Text = 10 - contador
            contador += 1
        End If
    End Sub

    Private Sub tmBuscaPG_Tick(sender As Object, e As EventArgs) Handles tmBuscaPG.Tick

    End Sub

    Sub CargarTramitesAtrasados()
        Try
            Using myCMD As New OracleCommand() With {.Connection = cnn, .CommandText = "SP_TRAMITES_SIN_ENTREGAR", .CommandType = CommandType.StoredProcedure}
                myCMD.Parameters.Add("VIDUSER", OracleDbType.Decimal, 10, Nothing, ParameterDirection.Input).Value = SesionActiva.IdUsuario
                Dim refCursor As OracleParameter = New OracleParameter With
                {
                    .OracleDbType = OracleDbType.RefCursor,
                    .Direction = ParameterDirection.Output
                }
                myCMD.Parameters.Add(refCursor)
                cnn.Open()
                myCMD.ExecuteNonQuery()

                Dim cursor As OracleRefCursor = DirectCast(refCursor.Value, OracleRefCursor)
                Dim reader As OracleDataReader = cursor.GetDataReader
                'Dim fi As FieldInfo = reader.GetType().GetField("m_rowSize", BindingFlags.Instance & BindingFlags.NonPublic)
                'Dim rowSize As Integer = Convert.ToInt32(fi.GetValue(reader))
                'reader.FetchSize = rowSize * 100

                If reader.HasRows Then
                    frmNotificacion.dgvTramites.Rows.Clear()

                    While reader.Read
                        frmNotificacion.dgvTramites.Rows.Add(reader("CODIGOTRAMITE").ToString, MinutosAHoras(CInt(reader("MINUTOS_PASADOS"))), reader("NUMERO_SALTO").ToString, String.Format("{0} minutos", reader("MINUTOS").ToString))
                    End While

                    frmNotificacion.lblInfo.Text = String.Format("Usted tiene {0} trámites pendientes de entrega", frmNotificacion.dgvTramites.Rows.Count)

                    reader.Close()
                    refCursor.Dispose()

                    frmNotificacion.Show()
                    frmNotificacion.TopMost = True
                End If

            End Using
        Catch ex As Exception
        Finally
            cnn.Close()
        End Try
    End Sub

    Private Sub tmNotificacion_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmNotificacion.Tick
        CargarTramitesAtrasados()
    End Sub

End Class