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
            lblSubtitulo = new Label();
            panelFormulario = new Tarjeta();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblTipo = new Label();
            cmbTipo = new ComboBox();
            lblSaldo = new Label();
            numSaldo = new NumericUpDown();
            btnCrear = new Button();
            panelLista = new Tarjeta();
            gridCuentas = new DataGridView();
            lblTotal = new Label();
            btnRenombrar = new Button();
            btnEliminar = new Button();
            panelFormulario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSaldo).BeginInit();
            panelLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridCuentas).BeginInit();
            SuspendLayout();
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(125, 37);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Cuentas";
            //
            // lblSubtitulo
            //
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Location = new Point(3, 42);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(213, 19);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Tus cuentas y su saldo actual";
            //
            // panelFormulario
            //
            panelFormulario.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelFormulario.Controls.Add(lblNombre);
            panelFormulario.Controls.Add(txtNombre);
            panelFormulario.Controls.Add(lblTipo);
            panelFormulario.Controls.Add(cmbTipo);
            panelFormulario.Controls.Add(lblSaldo);
            panelFormulario.Controls.Add(numSaldo);
            panelFormulario.Controls.Add(btnCrear);
            panelFormulario.Location = new Point(0, 78);
            panelFormulario.Name = "panelFormulario";
            panelFormulario.Size = new Size(950, 104);
            panelFormulario.TabIndex = 2;
            //
            // lblNombre
            //
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI Semibold", 9F);
            lblNombre.Location = new Point(22, 20);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(51, 15);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre";
            //
            // txtNombre
            //
            txtNombre.Location = new Point(22, 44);
            txtNombre.Name = "txtNombre";
            txtNombre.PlaceholderText = "Ej.: Cuenta nómina";
            txtNombre.Size = new Size(240, 25);
            txtNombre.TabIndex = 1;
            //
            // lblTipo
            //
            lblTipo.AutoSize = true;
            lblTipo.Font = new Font("Segoe UI Semibold", 9F);
            lblTipo.Location = new Point(278, 20);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(30, 15);
            lblTipo.TabIndex = 2;
            lblTipo.Text = "Tipo";
            //
            // cmbTipo
            //
            cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipo.Location = new Point(278, 44);
            cmbTipo.Name = "cmbTipo";
            cmbTipo.Size = new Size(160, 25);
            cmbTipo.TabIndex = 3;
            //
            // lblSaldo
            //
            lblSaldo.AutoSize = true;
            lblSaldo.Font = new Font("Segoe UI Semibold", 9F);
            lblSaldo.Location = new Point(454, 20);
            lblSaldo.Name = "lblSaldo";
            lblSaldo.Size = new Size(72, 15);
            lblSaldo.TabIndex = 4;
            lblSaldo.Text = "Saldo inicial";
            //
            // numSaldo
            //
            numSaldo.DecimalPlaces = 2;
            numSaldo.Location = new Point(454, 44);
            numSaldo.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            numSaldo.Name = "numSaldo";
            numSaldo.Size = new Size(150, 25);
            numSaldo.TabIndex = 5;
            numSaldo.TextAlign = HorizontalAlignment.Right;
            numSaldo.ThousandsSeparator = true;
            //
            // btnCrear
            //
            btnCrear.Location = new Point(620, 40);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(150, 34);
            btnCrear.TabIndex = 6;
            btnCrear.Text = "Crear cuenta";
            btnCrear.Click += btnCrear_Click;
            //
            // panelLista
            //
            panelLista.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelLista.Controls.Add(gridCuentas);
            panelLista.Controls.Add(lblTotal);
            panelLista.Controls.Add(btnRenombrar);
            panelLista.Controls.Add(btnEliminar);
            panelLista.Location = new Point(0, 196);
            panelLista.Name = "panelLista";
            panelLista.Size = new Size(950, 444);
            panelLista.TabIndex = 3;
            //
            // gridCuentas
            //
            gridCuentas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridCuentas.Location = new Point(14, 12);
            gridCuentas.Name = "gridCuentas";
            gridCuentas.Size = new Size(922, 360);
            gridCuentas.TabIndex = 0;
            //
            // lblTotal
            //
            lblTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTotal.Location = new Point(24, 393);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(88, 20);
            lblTotal.TabIndex = 1;
            lblTotal.Text = "Saldo total:";
            //
            // btnRenombrar
            //
            btnRenombrar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnRenombrar.Location = new Point(666, 385);
            btnRenombrar.Name = "btnRenombrar";
            btnRenombrar.Size = new Size(130, 36);
            btnRenombrar.TabIndex = 2;
            btnRenombrar.Text = "Renombrar";
            btnRenombrar.Click += btnRenombrar_Click;
            //
            // btnEliminar
            //
            btnEliminar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEliminar.Location = new Point(806, 385);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(125, 36);
            btnEliminar.TabIndex = 3;
            btnEliminar.Text = "Eliminar";
            btnEliminar.Click += btnEliminar_Click;
            //
            // CuentasView
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(244, 246, 251);
            Controls.Add(lblTitulo);
            Controls.Add(lblSubtitulo);
            Controls.Add(panelFormulario);
            Controls.Add(panelLista);
            Font = new Font("Segoe UI", 10F);
            Name = "CuentasView";
            Size = new Size(950, 640);
            panelFormulario.ResumeLayout(false);
            panelFormulario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numSaldo).EndInit();
            panelLista.ResumeLayout(false);
            panelLista.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridCuentas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblSubtitulo;
        private Tarjeta panelFormulario;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblTipo;
        private ComboBox cmbTipo;
        private Label lblSaldo;
        private NumericUpDown numSaldo;
        private Button btnCrear;
        private Tarjeta panelLista;
        private DataGridView gridCuentas;
        private Label lblTotal;
        private Button btnRenombrar;
        private Button btnEliminar;
    }
}
