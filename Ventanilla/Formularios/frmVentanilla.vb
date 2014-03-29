﻿Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports System.Threading

Public Class frmVentanilla
    Dim cnn As New OracleConnection(My.Settings.Miconexion)
    Dim WithEvents socketCliente As New SocketCliente
    Private IDPeticion As Integer = 0
    Dim codigoSep As String = ""
    Dim timer As System.Threading.Timer

    Private Sub TicketsEnEspera()
        eAPPCA.ticketEspera(dgvEnEspera)
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
        Else
            lblTicketActual.BackColor = Color.Green
            lblTicketActual.ForeColor = Color.White
            lblTicketActual.Text = String.Format("Atendiendo el ticket {0}{1}", codigo, secuencia)
            btnPonerEspera.Enabled = True
        End If
    End Sub

    Private Sub VerificarConexion()
        Try
            socketCliente.IP = My.Settings.Host
            socketCliente.Puerto = 11000

            socketCliente.Conectar()
            timer.Change(Timeout.Infinite, Timeout.Infinite) 'Disabled timer

            For Each ctrl In Me.Controls
                Dim miCtrl As Control = DirectCast(ctrl, Control)
                miCtrl.Enabled = True
            Next

            PanelConexion.Visible = False
        Catch ex As Exception
            For Each ctrl In Me.Controls
                Dim miCtrl As Control = DirectCast(ctrl, Control)
                If miCtrl.Tag Is Nothing Then
                    miCtrl.Enabled = False
                End If
            Next

            PanelConexion.Visible = True
        End Try
    End Sub

    Private Sub btnTerminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTerminar.Click
        If IDPeticion = 0 Then
            Atencion(IDPeticion, "Ventanilla fuera de servicio")
            Exit Sub
        Else
            eAPPCA.FinAtencion(IDPeticion)
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
            If flag = 1 Then
                socketCliente.EnviarDatos(String.Format("{0}@{1}@{2}", codigoSep, My.Settings.NumeroVentanilla, flag))
            Else
                codigoSep = String.Format("{0} {1}", SepararCodigo(codigo), secuencia)
                socketCliente.EnviarDatos(String.Format("{0}@{1}@{2}", codigoSep, My.Settings.NumeroVentanilla, flag))
            End If
        End If
    End Sub

    Private Sub frmVentanilla_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Ubica la pantalla en la esquina inferior derecha
        Dim src = Screen.FromPoint(Location)
        Location = New Point(src.WorkingArea.Right - Me.Width, src.WorkingArea.Bottom - Me.Height)
        MyBase.OnActivated(e)

        'Permite que se cambien las propiedades de un control desde un hilo de ejecucion
        Control.CheckForIllegalCrossThreadCalls = False

        'Comienza un timer basado en un hilo de ejecucion
        timer = New System.Threading.Timer(New TimerCallback(AddressOf VerificarConexion), Nothing, 0, 10000)

        TicketsEnEspera()
        TicketsAtencionEspecial()
        Text = String.Format("[{0}] - {1} - {2}", SesionActiva.Usuario, SesionActiva.Sucursal, SesionActiva.Oficina)
        lblNumVentanilla.Text = String.Format("Ventanilla #{0}", My.Settings.NumeroVentanilla)
        Atencion(0, "Ventanilla fuera de servicio")
    End Sub

    Private Sub frmVentanilla_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If IDPeticion <> 0 Then
            'FinAtencion(IDPeticion)
        End If
        Try
            socketCliente.Desconectar()
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
        Try

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

                LlamadoEnPantalla(myCMD.Parameters("CODIGO").Value.ToString, myCMD.Parameters("SECUENCIA").Value.ToString, 0)
                Atencion(IDPeticion, "En este momento no hay Tickets en espera", CType(myCMD.Parameters("CODIGO").Value.ToString, String), CType(myCMD.Parameters("SECUENCIA").Value.ToString, String))
            End Using

            'Refrescar los listados de tickets en cada llamado
            TicketsAtencionEspecial()
            TicketsEnEspera()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Oracle Error")
        Finally
            cnn.Close()
        End Try
    End Sub

    Private Sub dgvEnEspera_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles dgvEnEspera.DoubleClick
        If IDPeticion = 0 Then
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
        Try
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

            LlamadoEnPantalla(myCMD.Parameters("CODIGO").Value.ToString, myCMD.Parameters("SECUENCIA").Value.ToString, 0)

            Atencion(IDPeticion, "En este momento no hay Tickets en espera", CType(myCMD.Parameters("CODIGO").Value.ToString, String), CType(myCMD.Parameters("SECUENCIA").Value.ToString, String))

            'Refrescar los listados de tickets en cada llamado
            TicketsAtencionEspecial()
            TicketsEnEspera()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString)
        Finally
            cnn.Close()
        End Try
        TicketsAtencionEspecial()
    End Sub

    Private Sub socketCliente_ConexionTerminada() Handles socketCliente.ConexionTerminada
        timer.Change(0, 10000) 'Habilita el timer
    End Sub

    Private Sub btnReinicar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReinicar.Click
        frmLogin.Show()
        Close()
    End Sub

    Private Sub btnRellamar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRellamar.Click
        LlamadoEnPantalla(codigoSep, 0, 1)
    End Sub
End Class