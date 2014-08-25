Public Class frmEntregaDocs

    Private Sub frmEntregaDocs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        eAPPCA.TramitesEntregar(dgvTramites)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnFiltrar.Click
        eAPPCA.TramitesEntregar(dgvTramites, txtCodigoTramite.Text)
    End Sub

    Private Tramitess As New List(Of TRAMITES)
    Private Sub dgvTramites_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTramites.CellClick
        If e.RowIndex < 0 OrElse Not e.ColumnIndex = dgvTramites.Columns("Entregar").Index Then Return

        'eAPPCA.entregaTramite(DameID(dgvTramites))
        MsgBox(String.Format("Tramite {0} Entregado", DameID(dgvTramites, 1)))
        eAPPCA.TramitesEntregar(dgvTramites)

    End Sub
End Class