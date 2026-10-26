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
            lblSubtitulo = new Label();
            panelFormulario = new Tarjeta();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblTipo = new Label();
            cmbTipo = new ComboBox();
            btnCrear = new Button();
            panelLista = new Tarjeta();
            lblFiltro = new Label();
            cmbFiltro = new ComboBox();
            gridCategorias = new DataGridView();
            btnEliminar = new Button();
            panelFormulario.SuspendLayout();
            panelLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridCategorias).BeginInit();
            SuspendLayout();
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(157, 37);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Categorías";
            //
            // lblSubtitulo
            //
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Location = new Point(3, 42);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(249, 19);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Organiza tus ingresos y tus gastos";
            //
            // panelFormulario
            //
            panelFormulario.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelFormulario.Controls.Add(lblNombre);
            panelFormulario.Controls.Add(txtNombre);
            panelFormulario.Controls.Add(lblTipo);
            panelFormulario.Controls.Add(cmbTipo);
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
            txtNombre.PlaceholderText = "Ej.: Supermercado";
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
            // btnCrear
            //
            btnCrear.Location = new Point(454, 40);
            btnCrear.Name = "btnCrear";
            btnCrear.Size = new Size(170, 34);
            btnCrear.TabIndex = 4;
            btnCrear.Text = "Crear categoría";
            btnCrear.Click += btnCrear_Click;
            //
            // panelLista
            //
            panelLista.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelLista.Controls.Add(lblFiltro);
            panelLista.Controls.Add(cmbFiltro);
            panelLista.Controls.Add(gridCategorias);
            panelLista.Controls.Add(btnEliminar);
            panelLista.Location = new Point(0, 196);
            panelLista.Name = "panelLista";
            panelLista.Size = new Size(950, 444);
            panelLista.TabIndex = 3;
            //
            // lblFiltro
            //
            lblFiltro.AutoSize = true;
            lblFiltro.Font = new Font("Segoe UI Semibold", 9F);
            lblFiltro.Location = new Point(22, 23);
            lblFiltro.Name = "lblFiltro";
            lblFiltro.Size = new Size(50, 15);
            lblFiltro.TabIndex = 0;
            lblFiltro.Text = "Mostrar";
            //
            // cmbFiltro
            //
            cmbFiltro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltro.Location = new Point(84, 18);
            cmbFiltro.Name = "cmbFiltro";
            cmbFiltro.Size = new Size(160, 25);
            cmbFiltro.TabIndex = 1;
            cmbFiltro.SelectionChangeCommitted += cmbFiltro_SelectionChangeCommitted;
            //
            // gridCategorias
            //
            gridCategorias.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridCategorias.Location = new Point(14, 58);
            gridCategorias.Name = "gridCategorias";
            gridCategorias.Size = new Size(922, 314);
            gridCategorias.TabIndex = 2;
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
            // CategoriasView
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(244, 246, 251);
            Controls.Add(lblTitulo);
            Controls.Add(lblSubtitulo);
            Controls.Add(panelFormulario);
            Controls.Add(panelLista);
            Font = new Font("Segoe UI", 10F);
            Name = "CategoriasView";
            Size = new Size(950, 640);
            panelFormulario.ResumeLayout(false);
            panelFormulario.PerformLayout();
            panelLista.ResumeLayout(false);
            panelLista.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridCategorias).EndInit();
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
        private Button btnCrear;
        private Tarjeta panelLista;
        private Label lblFiltro;
        private ComboBox cmbFiltro;
        private DataGridView gridCategorias;
        private Button btnEliminar;
    }
}
