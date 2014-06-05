Public Class frmListadoGestiones

    Private Sub frmListadoGestiones_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BottomRightFormLocation(e)
    End Sub

    Sub BottomRightFormLocation(ByVal evento As EventArgs)
        Dim src = Screen.FromPoint(Location)
        Location = New Point(src.WorkingArea.Right - Width, src.WorkingArea.Bottom - Height)
        MyBase.OnActivated(evento)
    End Sub
End Class