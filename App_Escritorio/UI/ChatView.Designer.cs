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
            panelChat = new Tarjeta();
            panelMensajes = new Panel();
            panelEntrada = new Tarjeta();
            txtMensaje = new TextBox();
            btnEnviar = new Button();
            panelChat.SuspendLayout();
            panelEntrada.SuspendLayout();
            SuspendLayout();
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(136, 37);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Asistente";
            //
            // lblSubtitulo
            //
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Location = new Point(3, 42);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(380, 19);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Pregúntale por tus gastos, tus presupuestos o cómo ahorrar";
            //
            // btnNuevo
            //
            btnNuevo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNuevo.Location = new Point(750, 8);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(200, 38);
            btnNuevo.TabIndex = 2;
            btnNuevo.Text = "Nueva conversación";
            btnNuevo.Click += btnNuevo_Click;
            //
            // panelChat
            //
            panelChat.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelChat.Controls.Add(panelMensajes);
            panelChat.Location = new Point(0, 78);
            panelChat.Name = "panelChat";
            panelChat.Size = new Size(950, 476);
            panelChat.TabIndex = 3;
            //
            // panelMensajes
            //
            panelMensajes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelMensajes.AutoScroll = true;
            panelMensajes.Location = new Point(10, 10);
            panelMensajes.Name = "panelMensajes";
            panelMensajes.Size = new Size(928, 448);
            panelMensajes.TabIndex = 0;
            //
            // panelEntrada
            //
            panelEntrada.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelEntrada.Controls.Add(txtMensaje);
            panelEntrada.Controls.Add(btnEnviar);
            panelEntrada.Location = new Point(0, 562);
            panelEntrada.Name = "panelEntrada";
            panelEntrada.Radio = 20;
            panelEntrada.Size = new Size(950, 78);
            panelEntrada.TabIndex = 4;
            //
            // txtMensaje
            //
            txtMensaje.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtMensaje.BorderStyle = BorderStyle.None;
            txtMensaje.Font = new Font("Segoe UI", 10.5F);
            txtMensaje.Location = new Point(24, 18);
            txtMensaje.Multiline = true;
            txtMensaje.Name = "txtMensaje";
            txtMensaje.PlaceholderText = "Escribe tu pregunta (Mayús+Intro para cambiar de línea)";
            txtMensaje.Size = new Size(762, 42);
            txtMensaje.TabIndex = 0;
            txtMensaje.KeyDown += txtMensaje_KeyDown;
            //
            // btnEnviar
            //
            btnEnviar.Anchor = AnchorStyles.Right;
            btnEnviar.Location = new Point(802, 16);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(128, 42);
            btnEnviar.TabIndex = 1;
            btnEnviar.Text = "Enviar";
            btnEnviar.Click += btnEnviar_Click;
            //
            // ChatView
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(244, 246, 251);
            Controls.Add(lblTitulo);
            Controls.Add(lblSubtitulo);
            Controls.Add(btnNuevo);
            Controls.Add(panelChat);
            Controls.Add(panelEntrada);
            Font = new Font("Segoe UI", 10F);
            Name = "ChatView";
            Size = new Size(950, 640);
            panelChat.ResumeLayout(false);
            panelEntrada.ResumeLayout(false);
            panelEntrada.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblSubtitulo;
        private Button btnNuevo;
        private Tarjeta panelChat;
        private Panel panelMensajes;
        private Tarjeta panelEntrada;
        private TextBox txtMensaje;
        private Button btnEnviar;
    }
}
