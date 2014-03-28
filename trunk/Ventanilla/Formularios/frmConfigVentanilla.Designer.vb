<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfigVentanilla
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
        Dim NOMBRE_OFICINALabel As System.Windows.Forms.Label
        Dim NOMBRELabel As System.Windows.Forms.Label
        Dim NUMERO_VENTANILLALabel As System.Windows.Forms.Label
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.NUMERO_VENTANILLAComboBox = New System.Windows.Forms.ComboBox()
        Me.NOMBRELabel1 = New System.Windows.Forms.Label()
        Me.NOMBRE_OFICINALabel1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.btnConexion = New System.Windows.Forms.Button()
        Me.txtIP = New System.Windows.Forms.TextBox()
        Me.btnGuardarIP = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        NOMBRE_OFICINALabel = New System.Windows.Forms.Label()
        NOMBRELabel = New System.Windows.Forms.Label()
        NUMERO_VENTANILLALabel = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'NOMBRE_OFICINALabel
        '
        NOMBRE_OFICINALabel.AutoSize = True
        NOMBRE_OFICINALabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        NOMBRE_OFICINALabel.Location = New System.Drawing.Point(138, 66)
        NOMBRE_OFICINALabel.Name = "NOMBRE_OFICINALabel"
        NOMBRE_OFICINALabel.Size = New System.Drawing.Size(58, 20)
        NOMBRE_OFICINALabel.TabIndex = 5
        NOMBRE_OFICINALabel.Text = "Oficina"
        '
        'NOMBRELabel
        '
        NOMBRELabel.AutoSize = True
        NOMBRELabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        NOMBRELabel.Location = New System.Drawing.Point(125, 32)
        NOMBRELabel.Name = "NOMBRELabel"
        NOMBRELabel.Size = New System.Drawing.Size(71, 20)
        NOMBRELabel.TabIndex = 3
        NOMBRELabel.Text = "Sucursal"
        '
        'NUMERO_VENTANILLALabel
        '
        NUMERO_VENTANILLALabel.AutoSize = True
        NUMERO_VENTANILLALabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        NUMERO_VENTANILLALabel.Location = New System.Drawing.Point(39, 99)
        NUMERO_VENTANILLALabel.Name = "NUMERO_VENTANILLALabel"
        NUMERO_VENTANILLALabel.Size = New System.Drawing.Size(157, 20)
        NUMERO_VENTANILLALabel.TabIndex = 7
        NUMERO_VENTANILLALabel.Text = "Número de ventanilla"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(498, 223)
        Me.TabControl1.TabIndex = 11
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(NOMBRE_OFICINALabel)
        Me.TabPage1.Controls.Add(Me.btnGuardar)
        Me.TabPage1.Controls.Add(Me.NUMERO_VENTANILLAComboBox)
        Me.TabPage1.Controls.Add(NOMBRELabel)
        Me.TabPage1.Controls.Add(NUMERO_VENTANILLALabel)
        Me.TabPage1.Controls.Add(Me.NOMBRELabel1)
        Me.TabPage1.Controls.Add(Me.NOMBRE_OFICINALabel1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(490, 197)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Ventanilla"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'btnGuardar
        '
        Me.btnGuardar.Location = New System.Drawing.Point(219, 136)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(121, 40)
        Me.btnGuardar.TabIndex = 9
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'NUMERO_VENTANILLAComboBox
        '
        Me.NUMERO_VENTANILLAComboBox.DisplayMember = "NUMERO_VENTANILLA"
        Me.NUMERO_VENTANILLAComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.NUMERO_VENTANILLAComboBox.FormattingEnabled = True
        Me.NUMERO_VENTANILLAComboBox.Location = New System.Drawing.Point(220, 96)
        Me.NUMERO_VENTANILLAComboBox.Name = "NUMERO_VENTANILLAComboBox"
        Me.NUMERO_VENTANILLAComboBox.Size = New System.Drawing.Size(42, 28)
        Me.NUMERO_VENTANILLAComboBox.TabIndex = 8
        Me.NUMERO_VENTANILLAComboBox.ValueMember = "IDVENTANILLA"
        '
        'NOMBRELabel1
        '
        Me.NOMBRELabel1.AutoSize = True
        Me.NOMBRELabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.NOMBRELabel1.Location = New System.Drawing.Point(220, 32)
        Me.NOMBRELabel1.Name = "NOMBRELabel1"
        Me.NOMBRELabel1.Size = New System.Drawing.Size(57, 20)
        Me.NOMBRELabel1.TabIndex = 4
        Me.NOMBRELabel1.Text = "Label1"
        '
        'NOMBRE_OFICINALabel1
        '
        Me.NOMBRE_OFICINALabel1.AutoSize = True
        Me.NOMBRE_OFICINALabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.NOMBRE_OFICINALabel1.Location = New System.Drawing.Point(220, 66)
        Me.NOMBRE_OFICINALabel1.Name = "NOMBRE_OFICINALabel1"
        Me.NOMBRE_OFICINALabel1.Size = New System.Drawing.Size(57, 20)
        Me.NOMBRE_OFICINALabel1.TabIndex = 6
        Me.NOMBRE_OFICINALabel1.Text = "Label1"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.btnConexion)
        Me.TabPage2.Controls.Add(Me.txtIP)
        Me.TabPage2.Controls.Add(Me.btnGuardarIP)
        Me.TabPage2.Controls.Add(Me.Label1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(490, 197)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Dirección de pantalla"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'btnConexion
        '
        Me.btnConexion.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.btnConexion.Location = New System.Drawing.Point(324, 49)
        Me.btnConexion.Name = "btnConexion"
        Me.btnConexion.Size = New System.Drawing.Size(144, 27)
        Me.btnConexion.TabIndex = 4
        Me.btnConexion.Text = "Probar conexión"
        Me.btnConexion.UseVisualStyleBackColor = True
        '
        'txtIP
        '
        Me.txtIP.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.txtIP.Location = New System.Drawing.Point(30, 50)
        Me.txtIP.Name = "txtIP"
        Me.txtIP.Size = New System.Drawing.Size(188, 26)
        Me.txtIP.TabIndex = 3
        '
        'btnGuardarIP
        '
        Me.btnGuardarIP.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.btnGuardarIP.Location = New System.Drawing.Point(224, 49)
        Me.btnGuardarIP.Name = "btnGuardarIP"
        Me.btnGuardarIP.Size = New System.Drawing.Size(94, 27)
        Me.btnGuardarIP.TabIndex = 2
        Me.btnGuardarIP.Text = "Guardar"
        Me.btnGuardarIP.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.Label1.Location = New System.Drawing.Point(26, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(192, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Dirección IP de la pantalla"
        '
        'frmConfigVentanilla
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(520, 247)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "frmConfigVentanilla"
        Me.Text = "Configuración de Ventanilla"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents NUMERO_VENTANILLAComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents NOMBRELabel1 As System.Windows.Forms.Label
    Friend WithEvents NOMBRE_OFICINALabel1 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents btnConexion As System.Windows.Forms.Button
    Friend WithEvents txtIP As System.Windows.Forms.TextBox
    Friend WithEvents btnGuardarIP As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
