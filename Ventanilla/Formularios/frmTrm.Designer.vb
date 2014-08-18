<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTrm
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnRecepcion = New System.Windows.Forms.Button()
        Me.btnCrear = New System.Windows.Forms.Button()
        Me.btnEntrega = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnRecepcion
        '
        Me.btnRecepcion.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.btnRecepcion.Location = New System.Drawing.Point(14, 130)
        Me.btnRecepcion.Name = "btnRecepcion"
        Me.btnRecepcion.Size = New System.Drawing.Size(128, 51)
        Me.btnRecepcion.TabIndex = 21
        Me.btnRecepcion.Text = "Recepcionar trámite"
        Me.btnRecepcion.UseVisualStyleBackColor = True
        '
        'btnCrear
        '
        Me.btnCrear.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.btnCrear.Location = New System.Drawing.Point(14, 73)
        Me.btnCrear.Name = "btnCrear"
        Me.btnCrear.Size = New System.Drawing.Size(128, 51)
        Me.btnCrear.TabIndex = 21
        Me.btnCrear.Text = "Crear trámite"
        Me.btnCrear.UseVisualStyleBackColor = True
        '
        'btnEntrega
        '
        Me.btnEntrega.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.btnEntrega.Location = New System.Drawing.Point(14, 187)
        Me.btnEntrega.Name = "btnEntrega"
        Me.btnEntrega.Size = New System.Drawing.Size(128, 51)
        Me.btnEntrega.TabIndex = 21
        Me.btnEntrega.Text = "Entregar trámite"
        Me.btnEntrega.UseVisualStyleBackColor = True
        '
        'frmTrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(154, 354)
        Me.Controls.Add(Me.btnEntrega)
        Me.Controls.Add(Me.btnCrear)
        Me.Controls.Add(Me.btnRecepcion)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.Name = "frmTrm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnRecepcion As System.Windows.Forms.Button
    Friend WithEvents btnCrear As System.Windows.Forms.Button
    Friend WithEvents btnEntrega As System.Windows.Forms.Button
End Class
