namespace App_Escritorio.UI
{
    partial class CuentasView
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
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblTipo = new Label();
            cmbTipo = new ComboBox();
            lblSaldo = new Label();
            numSaldo = new NumericUpDown();
            btnCrear = new Button();
            gridCuentas = new DataGridView();
            lblTotal = new Label();
            btnRenombrar = new Button();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)numSaldo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridCuentas).BeginInit();
            SuspendLayout();
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(116, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Cuentas";
            //
            // lblNombre
            //
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(0, 56);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(59, 19);
            lblNombre.TabIndex = 1;
            lblNombre.Text = "Nombre";
            //
            // txtNombre
            //
            txtNombre.Location = new Point(0, 78);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(240, 25);
            txtNombre.TabIndex = 2;
            //
            // lblTipo
            //
            lblTipo.AutoSize = true;
            lblTipo.Location = new Point(256, 56);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(35, 19);
            lblTipo.TabIndex = 3;
            lblTipo.Text = "Tipo";
            //
            // cmbTipo
            //
            cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipo.Location = new Point(256, 78);
            cmbTipo.Name = "cmbTipo";
            cmbTipo.Size = new Size(160, 25);
            cmbTipo.TabIndex = 4;
            //
            // lblSaldo
            //
            lblSaldo.AutoSize = true;
            lblSaldo.Location = new Point(432, 56);
            lblSaldo.Name = "lblSaldo";
            lblSaldo.Size = new Size(84, 19);
            lblSaldo.TabIndex = 5;
            lblSaldo.Text = "Saldo inicial";
            //
            // numSaldo
            //
            numSaldo.DecimalPlaces = 2;
            numSaldo.Location = new Point(432, 78);
            numSaldo.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            numSaldo.Name = "numSaldo";
            numSaldo.Size = new Size(150, 25);
            numSaldo.TabIndex = 6;
            numSaldo.TextAlign = HorizontalAlignment.Right;
            numSaldo.ThousandsSeparator = true;
            //
            // btnCrear
            //
            btnCrear.Location = new Point(598, 76);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(130, 30);
            btnCrear.TabIndex = 7;
            btnCrear.Text = "Crear cuenta";
            btnCrear.Click += btnCrear_Click;
            //
            // gridCuentas
            //
            gridCuentas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridCuentas.Location = new Point(0, 124);
            gridCuentas.Name = "gridCuentas";
            gridCuentas.Size = new Size(950, 456);
            gridCuentas.TabIndex = 8;
            //
            // lblTotal
            //
            lblTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTotal.Location = new Point(0, 600);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(95, 20);
            lblTotal.TabIndex = 9;
            lblTotal.Text = "Saldo total:";
            //
            // btnRenombrar
            //
            btnRenombrar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnRenombrar.Location = new Point(690, 592);
            btnRenombrar.Name = "btnRenombrar";
            btnRenombrar.Size = new Size(125, 34);
            btnRenombrar.TabIndex = 10;
            btnRenombrar.Text = "Renombrar";
            btnRenombrar.Click += btnRenombrar_Click;
            //
            // btnEliminar
            //
            btnEliminar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEliminar.Location = new Point(825, 592);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(125, 34);
            btnEliminar.TabIndex = 11;
            btnEliminar.Text = "Eliminar";
            btnEliminar.Click += btnEliminar_Click;
            //
            // CuentasView
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(248, 250, 252);
            Controls.Add(lblTitulo);
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(lblTipo);
            Controls.Add(cmbTipo);
            Controls.Add(lblSaldo);
            Controls.Add(numSaldo);
            Controls.Add(btnCrear);
            Controls.Add(gridCuentas);
            Controls.Add(lblTotal);
            Controls.Add(btnRenombrar);
            Controls.Add(btnEliminar);
            Font = new Font("Segoe UI", 10F);
            Name = "CuentasView";
            Size = new Size(950, 640);
            ((System.ComponentModel.ISupportInitialize)numSaldo).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridCuentas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblTipo;
        private ComboBox cmbTipo;
        private Label lblSaldo;
        private NumericUpDown numSaldo;
        private Button btnCrear;
        private DataGridView gridCuentas;
        private Label lblTotal;
        private Button btnRenombrar;
        private Button btnEliminar;
    }
}
