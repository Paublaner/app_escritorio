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
            lblMes = new Label();
            dtpMes = new SelectorFecha();
            lblFiltroCuenta = new Label();
            cmbFiltroCuenta = new ComboBox();
            gridMovimientos = new DataGridView();
            lblResumen = new Label();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)numImporte).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridMovimientos).BeginInit();
            SuspendLayout();
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(165, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Movimientos";
            //
            // lblCuenta
            //
            lblCuenta.AutoSize = true;
            lblCuenta.Location = new Point(0, 56);
            lblCuenta.Name = "lblCuenta";
            lblCuenta.Size = new Size(52, 19);
            lblCuenta.TabIndex = 1;
            lblCuenta.Text = "Cuenta";
            //
            // cmbCuenta
            //
            cmbCuenta.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCuenta.Location = new Point(0, 78);
            cmbCuenta.Name = "cmbCuenta";
            cmbCuenta.Size = new Size(170, 25);
            cmbCuenta.TabIndex = 2;
            //
            // lblCategoria
            //
            lblCategoria.AutoSize = true;
            lblCategoria.Location = new Point(182, 56);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(68, 19);
            lblCategoria.TabIndex = 3;
            lblCategoria.Text = "Categoría";
            //
            // cmbCategoria
            //
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.Location = new Point(182, 78);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(170, 25);
            cmbCategoria.TabIndex = 4;
            //
            // lblImporte
            //
            lblImporte.AutoSize = true;
            lblImporte.Location = new Point(364, 56);
            lblImporte.Name = "lblImporte";
            lblImporte.Size = new Size(58, 19);
            lblImporte.TabIndex = 5;
            lblImporte.Text = "Importe";
            //
            // numImporte
            //
            numImporte.DecimalPlaces = 2;
            numImporte.Location = new Point(364, 78);
            numImporte.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            numImporte.Name = "numImporte";
            numImporte.Size = new Size(110, 25);
            numImporte.TabIndex = 6;
            numImporte.TextAlign = HorizontalAlignment.Right;
            numImporte.ThousandsSeparator = true;
            //
            // lblFecha
            //
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(486, 56);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(43, 19);
            lblFecha.TabIndex = 7;
            lblFecha.Text = "Fecha";
            //
            // dtpFecha
            //
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(486, 78);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(120, 25);
            dtpFecha.TabIndex = 8;
            //
            // lblDescripcion
            //
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(618, 56);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(80, 19);
            lblDescripcion.TabIndex = 9;
            lblDescripcion.Text = "Descripción";
            //
            // txtDescripcion
            //
            txtDescripcion.Location = new Point(618, 78);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(190, 25);
            txtDescripcion.TabIndex = 10;
            //
            // btnRegistrar
            //
            btnRegistrar.Location = new Point(820, 76);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(130, 30);
            btnRegistrar.TabIndex = 11;
            btnRegistrar.Text = "Registrar";
            btnRegistrar.Click += btnRegistrar_Click;
            //
            // lblMes
            //
            lblMes.AutoSize = true;
            lblMes.Location = new Point(0, 136);
            lblMes.Name = "lblMes";
            lblMes.Size = new Size(34, 19);
            lblMes.TabIndex = 12;
            lblMes.Text = "Mes";
            //
            // dtpMes
            //
            dtpMes.CustomFormat = "MMMM yyyy";
            dtpMes.Format = DateTimePickerFormat.Custom;
            dtpMes.Location = new Point(42, 132);
            dtpMes.Name = "dtpMes";
            dtpMes.ShowUpDown = true;
            dtpMes.Size = new Size(170, 25);
            dtpMes.TabIndex = 13;
            dtpMes.ValueChanged += dtpMes_ValueChanged;
            //
            // lblFiltroCuenta
            //
            lblFiltroCuenta.AutoSize = true;
            lblFiltroCuenta.Location = new Point(232, 136);
            lblFiltroCuenta.Name = "lblFiltroCuenta";
            lblFiltroCuenta.Size = new Size(52, 19);
            lblFiltroCuenta.TabIndex = 14;
            lblFiltroCuenta.Text = "Cuenta";
            //
            // cmbFiltroCuenta
            //
            cmbFiltroCuenta.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroCuenta.Location = new Point(292, 132);
            cmbFiltroCuenta.Name = "cmbFiltroCuenta";
            cmbFiltroCuenta.Size = new Size(200, 25);
            cmbFiltroCuenta.TabIndex = 15;
            cmbFiltroCuenta.SelectionChangeCommitted += cmbFiltroCuenta_SelectionChangeCommitted;
            //
            // gridMovimientos
            //
            gridMovimientos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridMovimientos.Location = new Point(0, 176);
            gridMovimientos.Name = "gridMovimientos";
            gridMovimientos.Size = new Size(950, 404);
            gridMovimientos.TabIndex = 16;
            //
            // lblResumen
            //
            lblResumen.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblResumen.AutoSize = true;
            lblResumen.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblResumen.Location = new Point(0, 600);
            lblResumen.Name = "lblResumen";
            lblResumen.Size = new Size(0, 20);
            lblResumen.TabIndex = 17;
            //
            // btnEliminar
            //
            btnEliminar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEliminar.Location = new Point(825, 592);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(125, 34);
            btnEliminar.TabIndex = 18;
            btnEliminar.Text = "Eliminar";
            btnEliminar.Click += btnEliminar_Click;
            //
            // MovimientosView
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(248, 250, 252);
            Controls.Add(lblTitulo);
            Controls.Add(lblCuenta);
            Controls.Add(cmbCuenta);
            Controls.Add(lblCategoria);
            Controls.Add(cmbCategoria);
            Controls.Add(lblImporte);
            Controls.Add(numImporte);
            Controls.Add(lblFecha);
            Controls.Add(dtpFecha);
            Controls.Add(lblDescripcion);
            Controls.Add(txtDescripcion);
            Controls.Add(btnRegistrar);
            Controls.Add(lblMes);
            Controls.Add(dtpMes);
            Controls.Add(lblFiltroCuenta);
            Controls.Add(cmbFiltroCuenta);
            Controls.Add(gridMovimientos);
            Controls.Add(lblResumen);
            Controls.Add(btnEliminar);
            Font = new Font("Segoe UI", 10F);
            Name = "MovimientosView";
            Size = new Size(950, 640);
            ((System.ComponentModel.ISupportInitialize)numImporte).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridMovimientos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
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
        private Label lblMes;
        private SelectorFecha dtpMes;
        private Label lblFiltroCuenta;
        private ComboBox cmbFiltroCuenta;
        private DataGridView gridMovimientos;
        private Label lblResumen;
        private Button btnEliminar;
    }
}
