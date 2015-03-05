Public Class frmTrm

    Private Sub frmTrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Ubica la pantalla al lado derecho del formulario de ventanilla
        Dim src = Screen.FromPoint(Location)
        Location = New Point(frmVentanilla.Location.X - Me.Width, frmVentanilla.Location.Y)
        MyBase.OnActivated(e)
    End Sub

    Private Sub btnCrear_Click(sender As Object, e As EventArgs) Handles btnCrear.Click

        If frmVentanilla.IDPeticion <> 0 Then
            Dim infGestion As eAPPCA.InfoGestion = eAPPCA.obtenerIdGestion(frmVentanilla.IDPeticion)

            With frmTramite
                .IdGestion1 = infGestion.IdGestion
                .NombreGestion1 = infGestion.NombreGestion
                .Show()
            End With
        End If
        Close()
    End Sub

    Private Sub btnRecepcion_Click(sender As Object, e As EventArgs) Handles btnRecepcion.Click
        frmRecepcionDocs.Show()
        Close()
    End Sub

    Private Sub btnEntrega_Click(sender As Object, e As EventArgs) Handles btnEntrega.Click
        frmEntregaDocs.Show()
        Close()
    End Sub
End Class