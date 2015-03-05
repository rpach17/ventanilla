Public Class rptReciboTramite2
    Public Sub New(ByVal barcode As String, _
                   ByVal nombreGestion As String, _
                   ByVal fecha As String, _
                   ByVal identidad As String, _
                   ByVal nombre As String, _
                   ByVal telf As String, _
                   ByVal cel As String, _
                   ByVal correo As String, _
                   ByVal nota As String, _
                   ByVal reciboNo As String, _
                   ByVal reciboMonto As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'cellCodeBar.Font = New Font("barcode font", 65.75!)
        'cellCodeBar.Text = barcode
        XrBarCode1.Text = barcode
        cellTramite.Text = nombreGestion
        cellFecha.Text = fecha

        cellIdentidad.Text = identidad
        cellNombre.Text = nombre
        cellTelf.Text = telf
        cellCel.Text = cel
        cellCorre.Text = correo
        cellNota.Text = nota
        cellReciboNo.Text = reciboNo
        cellReciboMonto.Text = String.Format("L. {0}.00", reciboMonto)

        Dim cuerpo As String = String.Format("Recib� del se�or(a)  ___________________________________ que labora en Registro Nacional de las Personas (RNP) el tr�mite de {0}.  Lugar y Fecha _________________________________", nombreGestion)
        lblCuerpo.Text = cuerpo

    End Sub


End Class