Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types
Imports System.Reflection

Public Class frmNotificacion
    Dim cnn As New OracleConnection(My.Settings.Miconexion)

    Private Sub frmNotificacion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim src = Screen.FromPoint(Location)
        Location = New Point(src.WorkingArea.Right - Width, src.WorkingArea.Bottom - Height)
        MyBase.OnActivated(e)

        Timer1.Enabled = True

        Try
            Using myCMD As New OracleCommand() With {.Connection = cnn, .CommandText = "SP_TRAMITES_SIN_ENTREGAR", .CommandType = CommandType.StoredProcedure}
                myCMD.Parameters.Add("VIDUSER", OracleDbType.Decimal, 10, Nothing, ParameterDirection.Input).Value = SesionActiva.IdUsuario
                Dim refCursor As OracleParameter = New OracleParameter With
                {
                    .OracleDbType = OracleDbType.RefCursor,
                    .Direction = ParameterDirection.Output
                }
                myCMD.Parameters.Add(refCursor)
                cnn.Open()
                myCMD.ExecuteNonQuery()

                Dim cursor As OracleRefCursor = DirectCast(refCursor.Value, OracleRefCursor)
                Dim reader As OracleDataReader = cursor.GetDataReader
                'Dim fi As FieldInfo = reader.GetType().GetField("m_rowSize", BindingFlags.Instance & BindingFlags.NonPublic)
                'Dim rowSize As Integer = Convert.ToInt32(fi.GetValue(reader))
                'reader.FetchSize = rowSize * 100

                If reader.HasRows Then
                    While reader.Read
                        dgvTramites.Rows.Add(reader("CODIGOTRAMITE").ToString, MinutosAHoras(CInt(reader("MINUTOS_PASADOS"))), reader("NUMERO_SALTO").ToString, String.Format("{0} minutos", reader("MINUTOS").ToString))
                    End While
                End If

                reader.Close()
                refCursor.Dispose()
            End Using
        Catch ex As Exception
        Finally
            cnn.Close()
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Opacity = 100.0R Then
            Timer1.Enabled = False
        Else
            Opacity += 0.1R
        End If
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Close()
    End Sub
End Class