Imports System.Xml
Public Class frmConfigVentanilla
    Dim socketCliente As New SocketCliente

    Private Sub frmConfigVentanilla_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        NOMBRELabel1.Text = SesionActiva.Sucursal
        NOMBRE_OFICINALabel1.Text = SesionActiva.Oficina
        eAPPCA.CargarVentanillas(NUMERO_VENTANILLAComboBox)

        If Not My.Settings.IdVentanilla Is Nothing Then
            NUMERO_VENTANILLAComboBox.SelectedValue = My.Settings.IdVentanilla
        End If

        If Not My.Settings.Host Is Nothing Then
            txtIP.Text = My.Settings.Host
        End If

    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        My.Settings.IdVentanilla = NUMERO_VENTANILLAComboBox.SelectedValue
        My.Settings.NumeroVentanilla = NUMERO_VENTANILLAComboBox.Text

        If My.Settings.Host = "" Then
            TabControl1.SelectTab(1)
            Exit Sub
        End If

        frmVentanilla.Show()
        Close()
    End Sub

    Private Sub btnConexion_Click(sender As Object, e As EventArgs) Handles btnConexion.Click
        Try
            If txtIP.Text.Trim = "" Then
                MsgBox("Ingrese la dirección IP primero", MsgBoxStyle.Exclamation, "IP")
                Exit Sub
            End If

            SocketCliente.IP = txtIP.Text
            SocketCliente.Puerto = 11000

            SocketCliente.Conectar()

            If MsgBox(String.Format("Conexión exitosa{0}¿Desea guardar la dirección IP?", vbCrLf), MsgBoxStyle.Question + vbYesNo, "Confirme") = MsgBoxResult.Yes Then
                My.Settings.Host = txtIP.Text
                MsgBox("La dirección IP ha sido configurada correctamente", MsgBoxStyle.Information, "Correcto")
            End If

            SocketCliente.Desconectar()
            TabControl1.SelectTab(0)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnGuardarIP_Click(sender As Object, e As EventArgs) Handles btnGuardarIP.Click
        If txtIP.Text.Trim = "" Then
            MsgBox("Debe agregar la dirección IP de la pantalla", MsgBoxStyle.Critical, "Error")
            Exit Sub
        End If

        My.Settings.Host = txtIP.Text
        MsgBox("La dirección IP ha sido configurada correctamente", MsgBoxStyle.Information, "Correcto")
        TabControl1.SelectTab(0)
    End Sub
End Class