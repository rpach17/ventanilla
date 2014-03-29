Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports System.Threading

Public Class frmVentanilla
    Dim WithEvents socketCliente As New SocketCliente
    Private IDPeticion As Integer = 0
    Dim codigoSep As String = ""
    Dim timer As System.Threading.Timer

    Private Sub frmVentanilla_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Ubica la pantalla en la esquina inferior derecha
        Dim src = Screen.FromPoint(Location)
        Location = New Point(src.WorkingArea.Right - Me.Width, src.WorkingArea.Bottom - Me.Height)
        MyBase.OnActivated(e)

        'Permite que se cambien las propiedades de un control desde un hilo de ejecucion
        Control.CheckForIllegalCrossThreadCalls = False

        'Comienza un timer basado en un hilo de ejecucion
        timer = New System.Threading.Timer(New TimerCallback(AddressOf VerificarConexion), Nothing, 0, 10000)




    End Sub

    Private Sub TicketsEnEspera()
        'TICKETS_EN_ESPERATableAdapter.FillByIDSO(DataSet2.TICKETS_EN_ESPERA, SesionActiva.IdSucursalOficina)
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
End Class