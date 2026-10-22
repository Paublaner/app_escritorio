namespace App_Escritorio.UI
{
    partial class CategoriasView
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
            btnCrear = new Button();
            gridCategorias = new DataGridView();
            lblFiltro = new Label();
            cmbFiltro = new ComboBox();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)gridCategorias).BeginInit();
            SuspendLayout();
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(145, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Categorías";
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
            // btnCrear
            //
            btnCrear.Location = new Point(432, 76);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(150, 30);
            btnCrear.TabIndex = 5;
            btnCrear.Text = "Crear categoría";
            btnCrear.Click += btnCrear_Click;
            //
            // gridCategorias
            //
            gridCategorias.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridCategorias.Location = new Point(0, 124);
            gridCategorias.Name = "gridCategorias";
            gridCategorias.Size = new Size(950, 456);
            gridCategorias.TabIndex = 6;
            //
            // lblFiltro
            //
            lblFiltro.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblFiltro.AutoSize = true;
            lblFiltro.Location = new Point(0, 600);
            lblFiltro.Name = "lblFiltro";
            lblFiltro.Size = new Size(58, 19);
            lblFiltro.TabIndex = 7;
            lblFiltro.Text = "Mostrar";
            //
            // cmbFiltro
            //
            cmbFiltro.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cmbFiltro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltro.Location = new Point(66, 596);
            cmbFiltro.Name = "cmbFiltro";
            cmbFiltro.Size = new Size(160, 25);
            cmbFiltro.TabIndex = 8;
            cmbFiltro.SelectionChangeCommitted += cmbFiltro_SelectionChangeCommitted;
            //
            // btnEliminar
            //
            btnEliminar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEliminar.Location = new Point(825, 592);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(125, 34);
            btnEliminar.TabIndex = 9;
            btnEliminar.Text = "Eliminar";
            btnEliminar.Click += btnEliminar_Click;
            //
            // CategoriasView
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(248, 250, 252);
            Controls.Add(lblTitulo);
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(lblTipo);
            Controls.Add(cmbTipo);
            Controls.Add(btnCrear);
            Controls.Add(gridCategorias);
            Controls.Add(lblFiltro);
            Controls.Add(cmbFiltro);
            Controls.Add(btnEliminar);
            Font = new Font("Segoe UI", 10F);
            Name = "CategoriasView";
            Size = new Size(950, 640);
            ((System.ComponentModel.ISupportInitialize)gridCategorias).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblTipo;
        private ComboBox cmbTipo;
        private Button btnCrear;
        private DataGridView gridCategorias;
        private Label lblFiltro;
        private ComboBox cmbFiltro;
        private Button btnEliminar;
    }
}
