Public Class frmEntregaDocs

    Private Sub frmEntregaDocs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        eAPPCA.TramitesEntregar(dgvTramites)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnFiltrar.Click
        eAPPCA.TramitesEntregar(dgvTramites, txtCodigoTramite.Text)
    End Sub
End Class