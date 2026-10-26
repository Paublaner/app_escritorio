using App_Escritorio.Domain.Model;
using App_Escritorio.Domain.Ports.Driving;

namespace App_Escritorio.UI
{
    public partial class MovimientosView : UserControl, IVista
    {
        private readonly IMovimientoServicio _movimientoServicio;
        private readonly ICuentaServicio _cuentaServicio;
        private readonly ICategoriaServicio _categoriaServicio;

        private List<Cuenta> _cuentas = [];
        private List<Categoria> _categorias = [];

        public MovimientosView(IMovimientoServicio movimientoServicio, ICuentaServicio cuentaServicio, ICategoriaServicio categoriaServicio)
        {
            _movimientoServicio = movimientoServicio;
            _cuentaServicio = cuentaServicio;
            _categoriaServicio = categoriaServicio;

            InitializeComponent();

            Estilo.Grid(gridMovimientos);
            gridMovimientos.CellFormatting += Estilo.ColorearMovimiento;
            Estilo.BotonPrimario(btnRegistrar);
            Estilo.BotonPeligro(btnEliminar);
            Estilo.Icono(btnRegistrar, Dibujo.IconoNuevo);
            Estilo.Icono(btnEliminar, Dibujo.IconoEliminar);
            Estilo.Marcar(Rol.Secundario, lblSubtitulo, lblCuenta, lblCategoria, lblImporte, lblFecha, lblDescripcion, lblMes, lblFiltroCuenta);
        }

        public async Task CargarAsync()
        {
            var cuentas = await _cuentaServicio.ObtenerTodasAsync();

            if (!Mensajes.Comprobar(cuentas, this))
            {
                return;
            }

            var categorias = await _categoriaServicio.ObtenerTodasAsync();

            if (!Mensajes.Comprobar(categorias, this))
            {
                return;
            }

            _cuentas = cuentas.Value;
            _categorias = categorias.Value;

            Combos.Rellenar(cmbCuenta, _cuentas
                .Select(c => new Opcion<int>(c.Id, c.Nombre))
                .ToList());

            Combos.Rellenar(cmbCategoria, _categorias
                .OrderBy(c => c.Tipo)
                .ThenBy(c => c.Nombre)
                .Select(c => new Opcion<int>(c.Id, $"{c.Nombre} ({Textos.Tipo(c.Tipo)})"))
                .ToList());

            // En el filtro, null significa "todas las cuentas"
            Combos.Rellenar(cmbFiltroCuenta, new List<Opcion<int?>> { new(null, "Todas") }
                .Concat(_cuentas.Select(c => new Opcion<int?>(c.Id, c.Nombre)))
                .ToList());

            await CargarMovimientosAsync();
        }

        private async Task CargarMovimientosAsync()
        {
            var movimientos = await _movimientoServicio.ObtenerPorMesAsync(dtpMes.Value.Year, dtpMes.Value.Month);

            if (!Mensajes.Comprobar(movimientos, this))
            {
                return;
            }

            var cuentaId = (cmbFiltroCuenta.SelectedItem as Opcion<int?>)?.Valor;

            var filtrados = movimientos.Value
                .Where(m => cuentaId is null || m.CuentaId == cuentaId)
                .ToList();

            gridMovimientos.DataSource = Consultas.FilasMovimientos(filtrados, _cuentas, _categorias);
            Estilo.Formato(gridMovimientos, nameof(FilaMovimiento.Fecha), "d", false);
            Estilo.Formato(gridMovimientos, nameof(FilaMovimiento.Importe), "C2", true);

            decimal ingresos = filtrados.Where(m => m.Tipo == Categoria.TipoMovimiento.Ingreso).Sum(m => m.Importe);
            decimal gastos = filtrados.Where(m => m.Tipo == Categoria.TipoMovimiento.Gasto).Sum(m => m.Importe);

            lblResumen.Text = $"Ingresos: {ingresos:C2}     Gastos: {gastos:C2}     Balance: {ingresos - gastos:C2}";
        }

        private async void btnRegistrar_Click(object? sender, EventArgs e)
        {
            if (cmbCuenta.SelectedItem is not Opcion<int> cuenta)
            {
                Mensajes.Aviso(this, "Crea primero una cuenta");
                return;
            }

            if (cmbCategoria.SelectedItem is not Opcion<int> categoria)
            {
                Mensajes.Aviso(this, "Crea primero una categoría");
                return;
            }

            var resultado = await _movimientoServicio.RegistrarAsync(cuenta.Valor, categoria.Valor, numImporte.Value, dtpFecha.Value.Date, txtDescripcion.Text);

            if (!Mensajes.Comprobar(resultado, this))
            {
                return;
            }

            numImporte.Value = 0;
            txtDescripcion.Clear();
            await CargarMovimientosAsync();
        }

        private async void btnEliminar_Click(object? sender, EventArgs e)
        {
            if (gridMovimientos.CurrentRow?.DataBoundItem is not FilaMovimiento fila
                || !Mensajes.Confirmar(this, $"¿Eliminar el movimiento de {fila.Importe:C2} del {fila.Fecha:d}? El saldo de la cuenta se ajustará."))
            {
                return;
            }

            var resultado = await _movimientoServicio.EliminarAsync(fila.Id);

            if (Mensajes.Comprobar(resultado, this))
            {
                await CargarMovimientosAsync();
            }
        }

        private async void dtpMes_ValueChanged(object? sender, EventArgs e)
        {
            await CargarMovimientosAsync();
        }

        private async void cmbFiltroCuenta_SelectionChangeCommitted(object? sender, EventArgs e)
        {
            await CargarMovimientosAsync();
        }
    }
}
