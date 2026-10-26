namespace App_Escritorio.UI
{
    partial class ChatView
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
            btnNuevo = new Button();
            panelMensajes = new Panel();
            txtMensaje = new TextBox();
            btnEnviar = new Button();
            SuspendLayout();
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(126, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Asistente";
            //
            // lblSubtitulo
            //
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Location = new Point(2, 40);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(380, 19);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Pregúntale por tus gastos, tus presupuestos o cómo ahorrar";
            //
            // btnNuevo
            //
            btnNuevo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNuevo.Location = new Point(770, 0);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(180, 34);
            btnNuevo.TabIndex = 2;
            btnNuevo.Text = "Nueva conversación";
            btnNuevo.Click += btnNuevo_Click;
            //
            // panelMensajes
            //
            panelMensajes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelMensajes.AutoScroll = true;
            panelMensajes.Location = new Point(0, 76);
            panelMensajes.Name = "panelMensajes";
            panelMensajes.Size = new Size(950, 480);
            panelMensajes.TabIndex = 3;
            //
            // txtMensaje
            //
            txtMensaje.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtMensaje.Location = new Point(0, 572);
            txtMensaje.Multiline = true;
            txtMensaje.Name = "txtMensaje";
            txtMensaje.PlaceholderText = "Escribe tu pregunta (Mayús+Intro para cambiar de línea)";
            txtMensaje.ScrollBars = ScrollBars.Vertical;
            txtMensaje.Size = new Size(810, 56);
            txtMensaje.TabIndex = 4;
            txtMensaje.KeyDown += txtMensaje_KeyDown;
            //
            // btnEnviar
            //
            btnEnviar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEnviar.Location = new Point(826, 572);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(124, 56);
            btnEnviar.TabIndex = 5;
            btnEnviar.Text = "Enviar";
            btnEnviar.Click += btnEnviar_Click;
            //
            // ChatView
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(248, 250, 252);
            Controls.Add(lblTitulo);
            Controls.Add(lblSubtitulo);
            Controls.Add(btnNuevo);
            Controls.Add(panelMensajes);
            Controls.Add(txtMensaje);
            Controls.Add(btnEnviar);
            Font = new Font("Segoe UI", 10F);
            Name = "ChatView";
            Size = new Size(950, 640);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblSubtitulo;
        private Button btnNuevo;
        private Panel panelMensajes;
        private TextBox txtMensaje;
        private Button btnEnviar;
    }
}
