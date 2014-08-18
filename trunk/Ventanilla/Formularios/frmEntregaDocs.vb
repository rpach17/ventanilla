Public Class frmEntregaDocs

    Private Sub frmEntregaDocs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        eAPPCA.TramitesEntregar(dgvTramites)

    End Sub
End Class