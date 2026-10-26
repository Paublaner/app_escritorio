using System.Text.RegularExpressions;
using App_Escritorio.Domain.Ports.Driving;

namespace App_Escritorio.UI
{
    public partial class ChatView : UserControl, IVista
    {
        private const string Saludo =
            "Hola, soy tu asistente de finanzas. Puedo revisar tus cuentas y tus gastos y decirte dónde puedes ahorrar. ¿En qué te ayudo?";

        private readonly IAsistenteServicio _asistenteServicio;
        private readonly List<BurbujaChat> _burbujas = new();

        // Identifica la conversación en el agente: con el mismo hilo, recuerda lo que se ha hablado
        private string _hiloId = Guid.NewGuid().ToString();

        public ChatView(IAsistenteServicio asistenteServicio)
        {
            _asistenteServicio = asistenteServicio;

            InitializeComponent();

            Estilo.BotonPrimario(btnEnviar);
            Estilo.BotonSecundario(btnNuevo);
            Estilo.Icono(btnEnviar, Dibujo.IconoEnviar);
            Estilo.Icono(btnNuevo, Dibujo.IconoNuevo);
            Estilo.Marcar(Rol.Secundario, lblSubtitulo);

            panelMensajes.Resize += (_, _) => Recolocar();

            Agregar(Saludo, esUsuario: false);
        }

        public Task CargarAsync()
        {
            txtMensaje.Focus();
            return Task.CompletedTask;
        }

        private async Task EnviarAsync()
        {
            string pregunta = txtMensaje.Text.Trim();

            if (pregunta.Length == 0 || !txtMensaje.Enabled)
            {
                return;
            }

            txtMensaje.Clear();
            Agregar(pregunta, esUsuario: true);

            // El modelo tarda unos segundos: se bloquea la entrada y se muestra que está trabajando
            Bloquear(true);
            var pensando = Agregar("Pensando…", esUsuario: false);

            try
            {
                var respuesta = await _asistenteServicio.PreguntarAsync(pregunta, _hiloId);

                Quitar(pensando);

                if (respuesta.IsSuccess)
                {
                    Agregar(LimpiarMarkdown(respuesta.Value), esUsuario: false);
                }
                else
                {
                    Agregar(string.Join(Environment.NewLine, respuesta.Errors.Select(e => e.Message)), esUsuario: false, esError: true);
                }
            }
            finally
            {
                Bloquear(false);
            }
        }

        private BurbujaChat Agregar(string texto, bool esUsuario, bool esError = false)
        {
            var burbuja = new BurbujaChat(texto, esUsuario, esError);

            _burbujas.Add(burbuja);
            panelMensajes.Controls.Add(burbuja);
            Recolocar();

            panelMensajes.ScrollControlIntoView(burbuja);
            return burbuja;
        }

        private void Quitar(BurbujaChat burbuja)
        {
            _burbujas.Remove(burbuja);
            panelMensajes.Controls.Remove(burbuja);
            burbuja.Dispose();
            Recolocar();
        }

        // Coloca las burbujas una debajo de otra: las del usuario a la derecha y las del asistente a la izquierda
        private void Recolocar()
        {
            float escala = DeviceDpi / 96f;
            int margen = (int)(12 * escala);
            int separacion = (int)(10 * escala);
            int ancho = panelMensajes.ClientSize.Width;
            int anchoMaximo = Math.Max((int)(ancho * 0.75), 100);

            panelMensajes.SuspendLayout();

            // Las posiciones son relativas a lo que se ve, así que hay que sumar lo que se ha desplazado
            int y = margen + panelMensajes.AutoScrollPosition.Y;

            foreach (var burbuja in _burbujas)
            {
                burbuja.MaximumSize = new Size(anchoMaximo, 0);

                int x = burbuja.EsUsuario ? ancho - margen - burbuja.Width : margen;
                burbuja.Location = new Point(x, y);

                y += burbuja.Height + separacion;
            }

            panelMensajes.ResumeLayout();
        }

        private void Bloquear(bool bloquear)
        {
            txtMensaje.Enabled = !bloquear;
            btnEnviar.Enabled = !bloquear;
            btnNuevo.Enabled = !bloquear;

            if (!bloquear)
            {
                txtMensaje.Focus();
            }
        }

        // El modelo responde en Markdown; un Label no lo entiende, así que se quitan las marcas más comunes
        private static string LimpiarMarkdown(string texto)
        {
            texto = Regex.Replace(texto, @"^\s*#{1,6}\s*", "", RegexOptions.Multiline);
            texto = Regex.Replace(texto, @"^(\s*)[-*]\s+", "$1• ", RegexOptions.Multiline);
            texto = texto.Replace("**", "").Replace("__", "");

            return texto.Trim();
        }

        private async void btnEnviar_Click(object? sender, EventArgs e)
        {
            await EnviarAsync();
        }

        // Intro envía; Mayús+Intro hace un salto de línea
        private async void txtMensaje_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true;
                await EnviarAsync();
            }
        }

        private void btnNuevo_Click(object? sender, EventArgs e)
        {
            foreach (var burbuja in _burbujas.ToList())
            {
                Quitar(burbuja);
            }

            // Un hilo nuevo hace que el agente empiece sin recordar la conversación anterior
            _hiloId = Guid.NewGuid().ToString();
            Agregar(Saludo, esUsuario: false);
            txtMensaje.Focus();
        }
    }
}
