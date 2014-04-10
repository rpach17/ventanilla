Imports System.Xml
Public Class frmLogin

    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not ProbarConexion(My.Settings.Miconexion) Then
            frmConfig.Show()
            Close()
            Exit Sub
        End If

        If My.Settings.IdVentanilla = 0 Then
            chkConfVentanilla.Visible = True
            chkConfVentanilla.Checked = True
            chkConfVentanilla.Enabled = False
        Else
            chkConfVentanilla.Visible = True
            chkConfVentanilla.Checked = False
        End If
    End Sub

    Private Sub txtUser_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUser.KeyPress
        If e.KeyChar = ChrW(13) Then
            txtPass.Focus()
        End If
    End Sub

    Private Sub btnEntrar_Click(sender As Object, e As EventArgs) Handles btnEntrar.Click
        If txtUser.Text.Trim = Nothing Then
            txtUser.Focus()
            Exit Sub
        End If

        If txtPass.Text.Trim = Nothing Then
            txtPass.Focus()
            Exit Sub
        End If

        If eAPPCA.Login(txtUser.Text, SHA1(txtPass.Text)) Then
            If chkConfVentanilla.Checked Then
                frmConfigVentanilla.Show()
            Else
                frmVentanilla.Show()
            End If
            Close()
        Else
            Dim s As String = "Error al iniciar sesión. Posibles razones:" & vbCrLf & vbCrLf & _
                          "1 - Usuario y/o contraseña incorrectos" & vbCrLf & _
                          "2 - El usuario no está habilitado" & vbCrLf & _
                          "3 - El usuario no tiene acceso a este módulo" & vbCrLf & _
                          "4 - El usuario no existe"
            MsgBox(s, MsgBoxStyle.Critical, "Error inicio de sesión")
        End If

    End Sub


End Class