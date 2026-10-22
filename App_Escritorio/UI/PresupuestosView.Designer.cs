namespace App_Escritorio.UI
{
    partial class PresupuestosView
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
            lblCategoria = new Label();
            cmbCategoria = new ComboBox();
            lblLimite = new Label();
            numLimite = new NumericUpDown();
            lblMes = new Label();
            dtpMes = new DateTimePicker();
            btnDefinir = new Button();
            gridPresupuestos = new DataGridView();
            lblTotal = new Label();
            ((System.ComponentModel.ISupportInitialize)numLimite).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridPresupuestos).BeginInit();
            SuspendLayout();
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(170, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Presupuestos";
            //
            // lblCategoria
            //
            lblCategoria.AutoSize = true;
            lblCategoria.Location = new Point(0, 56);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(126, 19);
            lblCategoria.TabIndex = 1;
            lblCategoria.Text = "Categoría de gasto";
            //
            // cmbCategoria
            //
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.Location = new Point(0, 78);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(220, 25);
            cmbCategoria.TabIndex = 2;
            //
            // lblLimite
            //
            lblLimite.AutoSize = true;
            lblLimite.Location = new Point(236, 56);
            lblLimite.Name = "lblLimite";
            lblLimite.Size = new Size(47, 19);
            lblLimite.TabIndex = 3;
            lblLimite.Text = "Límite";
            //
            // numLimite
            //
            numLimite.DecimalPlaces = 2;
            numLimite.Location = new Point(236, 78);
            numLimite.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            numLimite.Name = "numLimite";
            numLimite.Size = new Size(140, 25);
            numLimite.TabIndex = 4;
            numLimite.TextAlign = HorizontalAlignment.Right;
            numLimite.ThousandsSeparator = true;
            //
            // lblMes
            //
            lblMes.AutoSize = true;
            lblMes.Location = new Point(392, 56);
            lblMes.Name = "lblMes";
            lblMes.Size = new Size(34, 19);
            lblMes.TabIndex = 5;
            lblMes.Text = "Mes";
            //
            // dtpMes
            //
            dtpMes.CustomFormat = "MMMM yyyy";
            dtpMes.Format = DateTimePickerFormat.Custom;
            dtpMes.Location = new Point(392, 78);
            dtpMes.Name = "dtpMes";
            dtpMes.ShowUpDown = true;
            dtpMes.Size = new Size(180, 25);
            dtpMes.TabIndex = 6;
            dtpMes.ValueChanged += dtpMes_ValueChanged;
            //
            // btnDefinir
            //
            btnDefinir.Location = new Point(588, 76);
            btnDefinir.Name = "btnDefinir";
            btnDefinir.Size = new Size(170, 30);
            btnDefinir.TabIndex = 7;
            btnDefinir.Text = "Definir presupuesto";
            btnDefinir.Click += btnDefinir_Click;
            //
            // gridPresupuestos
            //
            gridPresupuestos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridPresupuestos.Location = new Point(0, 124);
            gridPresupuestos.Name = "gridPresupuestos";
            gridPresupuestos.Size = new Size(950, 456);
            gridPresupuestos.TabIndex = 8;
            gridPresupuestos.SelectionChanged += gridPresupuestos_SelectionChanged;
            //
            // lblTotal
            //
            lblTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTotal.Location = new Point(0, 600);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(0, 20);
            lblTotal.TabIndex = 9;
            //
            // PresupuestosView
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(248, 250, 252);
            Controls.Add(lblTitulo);
            Controls.Add(lblCategoria);
            Controls.Add(cmbCategoria);
            Controls.Add(lblLimite);
            Controls.Add(numLimite);
            Controls.Add(lblMes);
            Controls.Add(dtpMes);
            Controls.Add(btnDefinir);
            Controls.Add(gridPresupuestos);
            Controls.Add(lblTotal);
            Font = new Font("Segoe UI", 10F);
            Name = "PresupuestosView";
            Size = new Size(950, 640);
            ((System.ComponentModel.ISupportInitialize)numLimite).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridPresupuestos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblCategoria;
        private ComboBox cmbCategoria;
        private Label lblLimite;
        private NumericUpDown numLimite;
        private Label lblMes;
        private DateTimePicker dtpMes;
        private Button btnDefinir;
        private DataGridView gridPresupuestos;
        private Label lblTotal;
    }
}
