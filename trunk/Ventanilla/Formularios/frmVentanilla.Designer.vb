<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVentanilla
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.PanelConexion = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnReinicar = New System.Windows.Forms.Button()
        Me.btnLlamarEspecial = New System.Windows.Forms.Button()
        Me.lblTicketActual = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.dgvEnEspera = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dgvEspecial = New System.Windows.Forms.DataGridView()
        Me.btnPonerEspera = New System.Windows.Forms.Button()
        Me.btnTerminar = New System.Windows.Forms.Button()
        Me.btnLlamar = New System.Windows.Forms.Button()
        Me.lblNumVentanilla = New System.Windows.Forms.Label()
        Me.btnTramite = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.tmNotificacion = New System.Windows.Forms.Timer(Me.components)
        Me.btnRellamar = New System.Windows.Forms.Button()
        Me.tmBuscaPG = New System.Windows.Forms.Timer(Me.components)
        Me.PanelConexion.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgvEnEspera, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvEspecial, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelConexion
        '
        Me.PanelConexion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelConexion.Controls.Add(Me.Label1)
        Me.PanelConexion.Controls.Add(Me.btnReinicar)
        Me.PanelConexion.Location = New System.Drawing.Point(36, 75)
        Me.PanelConexion.Name = "PanelConexion"
        Me.PanelConexion.Size = New System.Drawing.Size(372, 193)
        Me.PanelConexion.TabIndex = 17
        Me.PanelConexion.Tag = "NoDeshabilitar"
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(-1, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(372, 41)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "No se puede conectar a la pantalla. Reconectando..."
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnReinicar
        '
        Me.btnReinicar.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnReinicar.Location = New System.Drawing.Point(137, 123)
        Me.btnReinicar.Name = "btnReinicar"
        Me.btnReinicar.Size = New System.Drawing.Size(93, 38)
        Me.btnReinicar.TabIndex = 0
        Me.btnReinicar.Tag = "NoDeshabilitar"
        Me.btnReinicar.Text = "Reiniciar sesión"
        Me.btnReinicar.UseVisualStyleBackColor = True
        Me.btnReinicar.Visible = False
        '
        'btnLlamarEspecial
        '
        Me.btnLlamarEspecial.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.btnLlamarEspecial.Location = New System.Drawing.Point(137, 56)
        Me.btnLlamarEspecial.Name = "btnLlamarEspecial"
        Me.btnLlamarEspecial.Size = New System.Drawing.Size(143, 51)
        Me.btnLlamarEspecial.TabIndex = 11
        Me.btnLlamarEspecial.Text = "Llamar asistencia especial"
        Me.btnLlamarEspecial.UseVisualStyleBackColor = True
        '
        'lblTicketActual
        '
        Me.lblTicketActual.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTicketActual.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblTicketActual.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!)
        Me.lblTicketActual.Location = New System.Drawing.Point(47, 0)
        Me.lblTicketActual.Name = "lblTicketActual"
        Me.lblTicketActual.Size = New System.Drawing.Size(404, 44)
        Me.lblTicketActual.TabIndex = 16
        Me.lblTicketActual.Text = "sfsdfsdf"
        Me.lblTicketActual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dgvEnEspera)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(292, 56)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(149, 287)
        Me.GroupBox2.TabIndex = 15
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Tickets en espera"
        '
        'dgvEnEspera
        '
        Me.dgvEnEspera.AllowUserToAddRows = False
        Me.dgvEnEspera.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvEnEspera.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvEnEspera.Location = New System.Drawing.Point(3, 16)
        Me.dgvEnEspera.Name = "dgvEnEspera"
        Me.dgvEnEspera.ReadOnly = True
        Me.dgvEnEspera.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvEnEspera.Size = New System.Drawing.Size(143, 268)
        Me.dgvEnEspera.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dgvEspecial)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(134, 113)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(152, 230)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Asistencia especial"
        '
        'dgvEspecial
        '
        Me.dgvEspecial.AllowUserToAddRows = False
        Me.dgvEspecial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvEspecial.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvEspecial.Enabled = False
        Me.dgvEspecial.Location = New System.Drawing.Point(3, 16)
        Me.dgvEspecial.MultiSelect = False
        Me.dgvEspecial.Name = "dgvEspecial"
        Me.dgvEspecial.ReadOnly = True
        Me.dgvEspecial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvEspecial.Size = New System.Drawing.Size(146, 211)
        Me.dgvEspecial.TabIndex = 2
        '
        'btnPonerEspera
        '
        Me.btnPonerEspera.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.btnPonerEspera.Location = New System.Drawing.Point(5, 108)
        Me.btnPonerEspera.Name = "btnPonerEspera"
        Me.btnPonerEspera.Size = New System.Drawing.Size(128, 51)
        Me.btnPonerEspera.TabIndex = 12
        Me.btnPonerEspera.Text = "Poner en espera"
        Me.btnPonerEspera.UseVisualStyleBackColor = True
        '
        'btnTerminar
        '
        Me.btnTerminar.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.btnTerminar.Location = New System.Drawing.Point(5, 160)
        Me.btnTerminar.Name = "btnTerminar"
        Me.btnTerminar.Size = New System.Drawing.Size(128, 51)
        Me.btnTerminar.TabIndex = 13
        Me.btnTerminar.Text = "Terminar"
        Me.btnTerminar.UseVisualStyleBackColor = True
        '
        'btnLlamar
        '
        Me.btnLlamar.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.btnLlamar.Location = New System.Drawing.Point(5, 56)
        Me.btnLlamar.Name = "btnLlamar"
        Me.btnLlamar.Size = New System.Drawing.Size(128, 51)
        Me.btnLlamar.TabIndex = 10
        Me.btnLlamar.Text = "Llamar"
        Me.btnLlamar.UseVisualStyleBackColor = True
        '
        'lblNumVentanilla
        '
        Me.lblNumVentanilla.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNumVentanilla.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!)
        Me.lblNumVentanilla.ForeColor = System.Drawing.Color.ForestGreen
        Me.lblNumVentanilla.Location = New System.Drawing.Point(3, 287)
        Me.lblNumVentanilla.Name = "lblNumVentanilla"
        Me.lblNumVentanilla.Size = New System.Drawing.Size(130, 54)
        Me.lblNumVentanilla.TabIndex = 18
        Me.lblNumVentanilla.Text = "Ventanilla #4"
        Me.lblNumVentanilla.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnTramite
        '
        Me.btnTramite.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.btnTramite.Location = New System.Drawing.Point(5, 212)
        Me.btnTramite.Name = "btnTramite"
        Me.btnTramite.Size = New System.Drawing.Size(128, 51)
        Me.btnTramite.TabIndex = 20
        Me.btnTramite.Text = "Trámites"
        Me.btnTramite.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'Timer2
        '
        Me.Timer2.Interval = 1000
        '
        'tmNotificacion
        '
        Me.tmNotificacion.Interval = 1800000
        Me.tmNotificacion.Tag = "Interval = 30 minutos"
        '
        'btnRellamar
        '
        Me.btnRellamar.Image = Global.Ventanilla.My.Resources.Resources.bocina
        Me.btnRellamar.Location = New System.Drawing.Point(-1, -1)
        Me.btnRellamar.Name = "btnRellamar"
        Me.btnRellamar.Size = New System.Drawing.Size(50, 46)
        Me.btnRellamar.TabIndex = 19
        Me.btnRellamar.UseVisualStyleBackColor = True
        '
        'tmBuscaPG
        '
        '
        'frmVentanilla
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(451, 354)
        Me.Controls.Add(Me.btnRellamar)
        Me.Controls.Add(Me.PanelConexion)
        Me.Controls.Add(Me.btnLlamarEspecial)
        Me.Controls.Add(Me.lblTicketActual)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnPonerEspera)
        Me.Controls.Add(Me.btnTerminar)
        Me.Controls.Add(Me.btnLlamar)
        Me.Controls.Add(Me.lblNumVentanilla)
        Me.Controls.Add(Me.btnTramite)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmVentanilla"
        Me.PanelConexion.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.dgvEnEspera, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.dgvEspecial, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnRellamar As System.Windows.Forms.Button
    Friend WithEvents PanelConexion As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnLlamarEspecial As System.Windows.Forms.Button
    Friend WithEvents lblTicketActual As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents dgvEnEspera As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dgvEspecial As System.Windows.Forms.DataGridView
    Friend WithEvents btnPonerEspera As System.Windows.Forms.Button
    Friend WithEvents btnTerminar As System.Windows.Forms.Button
    Friend WithEvents btnLlamar As System.Windows.Forms.Button
    Friend WithEvents lblNumVentanilla As System.Windows.Forms.Label
    Friend WithEvents btnReinicar As System.Windows.Forms.Button
    Friend WithEvents btnTramite As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents tmNotificacion As System.Windows.Forms.Timer
    Friend WithEvents tmBuscaPG As System.Windows.Forms.Timer
End Class
