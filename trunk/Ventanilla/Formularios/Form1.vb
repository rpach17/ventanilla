Imports DevExpress.XtraReports.UI
Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using rpt As New rptRecibo("Rony Francisco Pacheco", "0801198502379", "2739-7364", "3627-8367", "rpach17@gmail.com", "USUARIO1", "Autentica", "081400400001", "01/08/2014", "24/08/2014")
            Using pr As New ReportPrintTool(rpt)
                pr.ShowPreviewDialog()
            End Using
        End Using
    End Sub
End Class