Public Class frmConfig

    Private Sub ButtonC_Click(sender As Object, e As EventArgs) Handles ButtonC.Click
        Dim _cadena As String = String.Format("DATA SOURCE={0};PASSWORD={1};PERSIST SECURITY INFO=True;USER ID={2}", txtDS.Text, txtPass.Text, txtUserID.Text)

        If ProbarConexion1(_cadena) = "OK" Then
            MsgBox("Conexión correcta", MsgBoxStyle.Information, "OK")
        Else
            MsgBox(ProbarConexion1(_cadena), MsgBoxStyle.Critical, "ERROR")
        End If

        'If ProbarConexion(_cadena) Then
        '    MsgBox("Conexión correcta", MsgBoxStyle.Information, "OK")
        'Else
        '    MsgBox("Error de conexión", MsgBoxStyle.Critical, "ERROR")
        'End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim _cadena As String = String.Format("DATA SOURCE={0};PASSWORD={1};PERSIST SECURITY INFO=True;USER ID={2}", txtDS.Text, txtPass.Text, txtUserID.Text)

        If ProbarConexion(_cadena) Then
            My.Settings.Miconexion = _cadena
            My.Settings.ConexionEntity = "metadata=res://*/Entity.appca.csdl|res://*/Entity.appca.ssdl|" _
                                       & "res://*/Entity.appca.msl;provider=Oracle.DataAccess.Client;provider connection string='" _
                                       & _cadena & "' "
            My.Settings.Save()
            MsgBox("Nueva configuración guardada", MsgBoxStyle.Information, "OK")
            frmLogin.Show()
            Close()
        Else
            MsgBox("Error de conexión", MsgBoxStyle.Critical, "ERROR")
        End If
    End Sub


End Class