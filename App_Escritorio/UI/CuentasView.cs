using App_Escritorio.Domain.Model;
using App_Escritorio.Domain.Ports.Driving;

namespace App_Escritorio.UI
{
    public partial class CuentasView : UserControl, IVista
    {
        private readonly ICuentaServicio _cuentaServicio;

        public CuentasView(ICuentaServicio cuentaServicio)
        {
            _cuentaServicio = cuentaServicio;

            InitializeComponent();

            Estilo.Grid(gridCuentas);
            Estilo.BotonPrimario(btnCrear);
            Estilo.BotonSecundario(btnRenombrar);
            Estilo.BotonPeligro(btnEliminar);

            cmbTipo.DisplayMember = nameof(Opcion<Cuenta.TipoCuenta>.Texto);
            cmbTipo.ValueMember = nameof(Opcion<Cuenta.TipoCuenta>.Valor);
            cmbTipo.DataSource = Enum.GetValues<Cuenta.TipoCuenta>()
                .Select(t => new Opcion<Cuenta.TipoCuenta>(t, Textos.Tipo(t)))
                .ToList();
        }

        public async Task CargarAsync()
        {
            var cuentas = await _cuentaServicio.ObtenerTodasAsync();

            if (!Mensajes.Comprobar(cuentas, this))
            {
                return;
            }

            gridCuentas.DataSource = cuentas.Value
                .Select(c => new FilaCuenta { Id = c.Id, Nombre = c.Nombre, Tipo = Textos.Tipo(c.Tipo), Saldo = c.Saldo })
                .ToList();
            Estilo.Formato(gridCuentas, nameof(FilaCuenta.Saldo), "C2", true);

            var total = await _cuentaServicio.ObtenerSaldoTotalAsync();

            if (Mensajes.Comprobar(total, this))
            {
                lblTotal.Text = $"Saldo total: {total.Value:C2}";
            }
        }

        private FilaCuenta? Seleccionada => gridCuentas.CurrentRow?.DataBoundItem as FilaCuenta;

        private async void btnCrear_Click(object? sender, EventArgs e)
        {
            var resultado = await _cuentaServicio.CrearAsync(txtNombre.Text, numSaldo.Value, (Cuenta.TipoCuenta)cmbTipo.SelectedValue!);

            if (!Mensajes.Comprobar(resultado, this))
            {
                return;
            }

            txtNombre.Clear();
            numSaldo.Value = 0;
            await CargarAsync();
        }

        private async void btnRenombrar_Click(object? sender, EventArgs e)
        {
            if (Seleccionada is not { } fila)
            {
                return;
            }

            string? nuevoNombre = DialogoTexto.Pedir(this, "Renombrar cuenta", "Nuevo nombre:", fila.Nombre);

            if (nuevoNombre is null)
            {
                return;
            }

            var resultado = await _cuentaServicio.RenombrarAsync(fila.Id, nuevoNombre);

            if (Mensajes.Comprobar(resultado, this))
            {
                await CargarAsync();
            }
        }

        private async void btnEliminar_Click(object? sender, EventArgs e)
        {
            if (Seleccionada is not { } fila || !Mensajes.Confirmar(this, $"¿Eliminar la cuenta \"{fila.Nombre}\"?"))
            {
                return;
            }

            var resultado = await _cuentaServicio.EliminarAsync(fila.Id);

            if (Mensajes.Comprobar(resultado, this))
            {
                await CargarAsync();
            }
        }
    }
}
