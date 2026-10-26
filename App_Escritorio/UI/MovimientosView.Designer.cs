namespace App_Escritorio.UI
{
    partial class MovimientosView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblSubtitulo = new Label();
            panelFormulario = new Tarjeta();
            lblCuenta = new Label();
            cmbCuenta = new ComboBox();
            lblCategoria = new Label();
            cmbCategoria = new ComboBox();
            lblImporte = new Label();
            numImporte = new NumericUpDown();
            lblFecha = new Label();
            dtpFecha = new SelectorFecha();
            lblDescripcion = new Label();
            txtDescripcion = new TextBox();
            btnRegistrar = new Button();
            panelLista = new Tarjeta();
            lblMes = new Label();
            dtpMes = new SelectorFecha();
            lblFiltroCuenta = new Label();
            cmbFiltroCuenta = new ComboBox();
            gridMovimientos = new DataGridView();
            lblResumen = new Label();
            btnEliminar = new Button();
            panelFormulario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numImporte).BeginInit();
            panelLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridMovimientos).BeginInit();
            SuspendLayout();
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(178, 37);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Movimientos";
            //
            // lblSubtitulo
            //
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Location = new Point(3, 42);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(230, 19);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Registra tus ingresos y tus gastos";
            //
            // panelFormulario
            //
            panelFormulario.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelFormulario.Controls.Add(lblCuenta);
            panelFormulario.Controls.Add(cmbCuenta);
            panelFormulario.Controls.Add(lblCategoria);
            panelFormulario.Controls.Add(cmbCategoria);
            panelFormulario.Controls.Add(lblImporte);
            panelFormulario.Controls.Add(numImporte);
            panelFormulario.Controls.Add(lblFecha);
            panelFormulario.Controls.Add(dtpFecha);
            panelFormulario.Controls.Add(lblDescripcion);
            panelFormulario.Controls.Add(txtDescripcion);
            panelFormulario.Controls.Add(btnRegistrar);
            panelFormulario.Location = new Point(0, 78);
            panelFormulario.Name = "panelFormulario";
            panelFormulario.Size = new Size(950, 104);
            panelFormulario.TabIndex = 2;
            //
            // lblCuenta
            //
            lblCuenta.AutoSize = true;
            lblCuenta.Font = new Font("Segoe UI Semibold", 9F);
            lblCuenta.Location = new Point(22, 20);
            lblCuenta.Name = "lblCuenta";
            lblCuenta.Size = new Size(45, 15);
            lblCuenta.TabIndex = 0;
            lblCuenta.Text = "Cuenta";
            //
            // cmbCuenta
            //
            cmbCuenta.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCuenta.Location = new Point(22, 44);
            cmbCuenta.Name = "cmbCuenta";
            cmbCuenta.Size = new Size(160, 25);
            cmbCuenta.TabIndex = 1;
            //
            // lblCategoria
            //
            lblCategoria.AutoSize = true;
            lblCategoria.Font = new Font("Segoe UI Semibold", 9F);
            lblCategoria.Location = new Point(194, 20);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(59, 15);
            lblCategoria.TabIndex = 2;
            lblCategoria.Text = "Categoría";
            //
            // cmbCategoria
            //
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.Location = new Point(194, 44);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(170, 25);
            cmbCategoria.TabIndex = 3;
            //
            // lblImporte
            //
            lblImporte.AutoSize = true;
            lblImporte.Font = new Font("Segoe UI Semibold", 9F);
            lblImporte.Location = new Point(376, 20);
            lblImporte.Name = "lblImporte";
            lblImporte.Size = new Size(51, 15);
            lblImporte.TabIndex = 4;
            lblImporte.Text = "Importe";
            //
            // numImporte
            //
            numImporte.DecimalPlaces = 2;
            numImporte.Location = new Point(376, 44);
            numImporte.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            numImporte.Name = "numImporte";
            numImporte.Size = new Size(110, 25);
            numImporte.TabIndex = 5;
            numImporte.TextAlign = HorizontalAlignment.Right;
            numImporte.ThousandsSeparator = true;
            //
            // lblFecha
            //
            lblFecha.AutoSize = true;
            lblFecha.Font = new Font("Segoe UI Semibold", 9F);
            lblFecha.Location = new Point(498, 20);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(38, 15);
            lblFecha.TabIndex = 6;
            lblFecha.Text = "Fecha";
            //
            // dtpFecha
            //
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(498, 44);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(120, 25);
            dtpFecha.TabIndex = 7;
            //
            // lblDescripcion
            //
            lblDescripcion.AutoSize = true;
            lblDescripcion.Font = new Font("Segoe UI Semibold", 9F);
            lblDescripcion.Location = new Point(630, 20);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(70, 15);
            lblDescripcion.TabIndex = 8;
            lblDescripcion.Text = "Descripción";
            //
            // txtDescripcion
            //
            txtDescripcion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDescripcion.Location = new Point(630, 44);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.PlaceholderText = "Opcional";
            txtDescripcion.Size = new Size(150, 25);
            txtDescripcion.TabIndex = 9;
            //
            // btnRegistrar
            //
            btnRegistrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRegistrar.Location = new Point(792, 40);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(139, 34);
            btnRegistrar.TabIndex = 10;
            btnRegistrar.Text = "Registrar";
            btnRegistrar.Click += btnRegistrar_Click;
            //
            // panelLista
            //
            panelLista.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelLista.Controls.Add(lblMes);
            panelLista.Controls.Add(dtpMes);
            panelLista.Controls.Add(lblFiltroCuenta);
            panelLista.Controls.Add(cmbFiltroCuenta);
            panelLista.Controls.Add(gridMovimientos);
            panelLista.Controls.Add(lblResumen);
            panelLista.Controls.Add(btnEliminar);
            panelLista.Location = new Point(0, 196);
            panelLista.Name = "panelLista";
            panelLista.Size = new Size(950, 444);
            panelLista.TabIndex = 3;
            //
            // lblMes
            //
            lblMes.AutoSize = true;
            lblMes.Font = new Font("Segoe UI Semibold", 9F);
            lblMes.Location = new Point(22, 23);
            lblMes.Name = "lblMes";
            lblMes.Size = new Size(28, 15);
            lblMes.TabIndex = 0;
            lblMes.Text = "Mes";
            //
            // dtpMes
            //
            dtpMes.CustomFormat = "MMMM yyyy";
            dtpMes.Format = DateTimePickerFormat.Custom;
            dtpMes.Location = new Point(60, 18);
            dtpMes.Name = "dtpMes";
            dtpMes.ShowUpDown = true;
            dtpMes.Size = new Size(170, 25);
            dtpMes.TabIndex = 1;
            dtpMes.ValueChanged += dtpMes_ValueChanged;
            //
            // lblFiltroCuenta
            //
            lblFiltroCuenta.AutoSize = true;
            lblFiltroCuenta.Font = new Font("Segoe UI Semibold", 9F);
            lblFiltroCuenta.Location = new Point(250, 23);
            lblFiltroCuenta.Name = "lblFiltroCuenta";
            lblFiltroCuenta.Size = new Size(45, 15);
            lblFiltroCuenta.TabIndex = 2;
            lblFiltroCuenta.Text = "Cuenta";
            //
            // cmbFiltroCuenta
            //
            cmbFiltroCuenta.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroCuenta.Location = new Point(304, 18);
            cmbFiltroCuenta.Name = "cmbFiltroCuenta";
            cmbFiltroCuenta.Size = new Size(200, 25);
            cmbFiltroCuenta.TabIndex = 3;
            cmbFiltroCuenta.SelectionChangeCommitted += cmbFiltroCuenta_SelectionChangeCommitted;
            //
            // gridMovimientos
            //
            gridMovimientos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridMovimientos.Location = new Point(14, 58);
            gridMovimientos.Name = "gridMovimientos";
            gridMovimientos.Size = new Size(922, 314);
            gridMovimientos.TabIndex = 4;
            //
            // lblResumen
            //
            lblResumen.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblResumen.AutoSize = true;
            lblResumen.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblResumen.Location = new Point(24, 393);
            lblResumen.Name = "lblResumen";
            lblResumen.Size = new Size(0, 20);
            lblResumen.TabIndex = 5;
            //
            // btnEliminar
            //
            btnEliminar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEliminar.Location = new Point(806, 385);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(125, 36);
            btnEliminar.TabIndex = 6;
            btnEliminar.Text = "Eliminar";
            btnEliminar.Click += btnEliminar_Click;
            //
            // MovimientosView
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(244, 246, 251);
            Controls.Add(lblTitulo);
            Controls.Add(lblSubtitulo);
            Controls.Add(panelFormulario);
            Controls.Add(panelLista);
            Font = new Font("Segoe UI", 10F);
            Name = "MovimientosView";
            Size = new Size(950, 640);
            panelFormulario.ResumeLayout(false);
            panelFormulario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numImporte).EndInit();
            panelLista.ResumeLayout(false);
            panelLista.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridMovimientos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblSubtitulo;
        private Tarjeta panelFormulario;
        private Label lblCuenta;
        private ComboBox cmbCuenta;
        private Label lblCategoria;
        private ComboBox cmbCategoria;
        private Label lblImporte;
        private NumericUpDown numImporte;
        private Label lblFecha;
        private SelectorFecha dtpFecha;
        private Label lblDescripcion;
        private TextBox txtDescripcion;
        private Button btnRegistrar;
        private Tarjeta panelLista;
        private Label lblMes;
        private SelectorFecha dtpMes;
        private Label lblFiltroCuenta;
        private ComboBox cmbFiltroCuenta;
        private DataGridView gridMovimientos;
        private Label lblResumen;
        private Button btnEliminar;
    }
}
