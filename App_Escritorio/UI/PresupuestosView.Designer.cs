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
            lblSubtitulo = new Label();
            panelFormulario = new Tarjeta();
            lblCategoria = new Label();
            cmbCategoria = new ComboBox();
            lblLimite = new Label();
            numLimite = new NumericUpDown();
            lblMes = new Label();
            dtpMes = new SelectorFecha();
            btnDefinir = new Button();
            panelLista = new Tarjeta();
            gridPresupuestos = new DataGridView();
            lblTotal = new Label();
            panelFormulario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numLimite).BeginInit();
            panelLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridPresupuestos).BeginInit();
            SuspendLayout();
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(185, 37);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Presupuestos";
            //
            // lblSubtitulo
            //
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Location = new Point(3, 42);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(271, 19);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Pon un límite a tus gastos de cada mes";
            //
            // panelFormulario
            //
            panelFormulario.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelFormulario.Controls.Add(lblCategoria);
            panelFormulario.Controls.Add(cmbCategoria);
            panelFormulario.Controls.Add(lblLimite);
            panelFormulario.Controls.Add(numLimite);
            panelFormulario.Controls.Add(lblMes);
            panelFormulario.Controls.Add(dtpMes);
            panelFormulario.Controls.Add(btnDefinir);
            panelFormulario.Location = new Point(0, 78);
            panelFormulario.Name = "panelFormulario";
            panelFormulario.Size = new Size(950, 104);
            panelFormulario.TabIndex = 2;
            //
            // lblCategoria
            //
            lblCategoria.AutoSize = true;
            lblCategoria.Font = new Font("Segoe UI Semibold", 9F);
            lblCategoria.Location = new Point(22, 20);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(110, 15);
            lblCategoria.TabIndex = 0;
            lblCategoria.Text = "Categoría de gasto";
            //
            // cmbCategoria
            //
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.Location = new Point(22, 44);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(220, 25);
            cmbCategoria.TabIndex = 1;
            //
            // lblLimite
            //
            lblLimite.AutoSize = true;
            lblLimite.Font = new Font("Segoe UI Semibold", 9F);
            lblLimite.Location = new Point(258, 20);
            lblLimite.Name = "lblLimite";
            lblLimite.Size = new Size(40, 15);
            lblLimite.TabIndex = 2;
            lblLimite.Text = "Límite";
            //
            // numLimite
            //
            numLimite.DecimalPlaces = 2;
            numLimite.Location = new Point(258, 44);
            numLimite.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            numLimite.Name = "numLimite";
            numLimite.Size = new Size(140, 25);
            numLimite.TabIndex = 3;
            numLimite.TextAlign = HorizontalAlignment.Right;
            numLimite.ThousandsSeparator = true;
            //
            // lblMes
            //
            lblMes.AutoSize = true;
            lblMes.Font = new Font("Segoe UI Semibold", 9F);
            lblMes.Location = new Point(414, 20);
            lblMes.Name = "lblMes";
            lblMes.Size = new Size(28, 15);
            lblMes.TabIndex = 4;
            lblMes.Text = "Mes";
            //
            // dtpMes
            //
            dtpMes.CustomFormat = "MMMM yyyy";
            dtpMes.Format = DateTimePickerFormat.Custom;
            dtpMes.Location = new Point(414, 44);
            dtpMes.Name = "dtpMes";
            dtpMes.ShowUpDown = true;
            dtpMes.Size = new Size(180, 25);
            dtpMes.TabIndex = 5;
            dtpMes.ValueChanged += dtpMes_ValueChanged;
            //
            // btnDefinir
            //
            btnDefinir.Location = new Point(610, 40);
            btnDefinir.Name = "btnDefinir";
            btnDefinir.Size = new Size(190, 34);
            btnDefinir.TabIndex = 6;
            btnDefinir.Text = "Definir presupuesto";
            btnDefinir.Click += btnDefinir_Click;
            //
            // panelLista
            //
            panelLista.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelLista.Controls.Add(gridPresupuestos);
            panelLista.Controls.Add(lblTotal);
            panelLista.Location = new Point(0, 196);
            panelLista.Name = "panelLista";
            panelLista.Size = new Size(950, 444);
            panelLista.TabIndex = 3;
            //
            // gridPresupuestos
            //
            gridPresupuestos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridPresupuestos.Location = new Point(14, 12);
            gridPresupuestos.Name = "gridPresupuestos";
            gridPresupuestos.Size = new Size(922, 360);
            gridPresupuestos.TabIndex = 0;
            gridPresupuestos.SelectionChanged += gridPresupuestos_SelectionChanged;
            //
            // lblTotal
            //
            lblTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTotal.Location = new Point(24, 393);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(0, 20);
            lblTotal.TabIndex = 1;
            //
            // PresupuestosView
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(244, 246, 251);
            Controls.Add(lblTitulo);
            Controls.Add(lblSubtitulo);
            Controls.Add(panelFormulario);
            Controls.Add(panelLista);
            Font = new Font("Segoe UI", 10F);
            Name = "PresupuestosView";
            Size = new Size(950, 640);
            panelFormulario.ResumeLayout(false);
            panelFormulario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numLimite).EndInit();
            panelLista.ResumeLayout(false);
            panelLista.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridPresupuestos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblSubtitulo;
        private Tarjeta panelFormulario;
        private Label lblCategoria;
        private ComboBox cmbCategoria;
        private Label lblLimite;
        private NumericUpDown numLimite;
        private Label lblMes;
        private SelectorFecha dtpMes;
        private Button btnDefinir;
        private Tarjeta panelLista;
        private DataGridView gridPresupuestos;
        private Label lblTotal;
    }
}
