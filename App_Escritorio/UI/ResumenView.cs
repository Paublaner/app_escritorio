using App_Escritorio.Domain.Model;
using App_Escritorio.Domain.Ports.Driving;

namespace App_Escritorio.UI
{
    public partial class ResumenView : UserControl, IVista
    {
        private readonly ICuentaServicio _cuentaServicio;
        private readonly ICategoriaServicio _categoriaServicio;
        private readonly IMovimientoServicio _movimientoServicio;
        private readonly IPresupuestoServicio _presupuestoServicio;

        public ResumenView(ICuentaServicio cuentaServicio, ICategoriaServicio categoriaServicio, IMovimientoServicio movimientoServicio, IPresupuestoServicio presupuestoServicio)
        {
            _cuentaServicio = cuentaServicio;
            _categoriaServicio = categoriaServicio;
            _movimientoServicio = movimientoServicio;
            _presupuestoServicio = presupuestoServicio;

            InitializeComponent();

            Estilo.Grid(gridPresupuestos);
            Estilo.Grid(gridMovimientos);
            gridPresupuestos.CellFormatting += Estilo.ColorearPresupuesto;
            gridMovimientos.CellFormatting += Estilo.ColorearMovimiento;

            Estilo.Marcar(Rol.Secundario, lblMes, lblIngresosTitulo, lblGastosTitulo, lblIngresosPie, lblGastosPie);
            Estilo.Marcar(Rol.Destacado, lblSaldoTitulo, lblSaldoValor, lblSaldoPie);
        }

        public async Task CargarAsync()
        {
            DateTime hoy = DateTime.Today;
            lblMes.Text = Textos.Mes(hoy);

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

            var saldoTotal = await _cuentaServicio.ObtenerSaldoTotalAsync();

            if (!Mensajes.Comprobar(saldoTotal, this))
            {
                return;
            }

            var movimientos = await _movimientoServicio.ObtenerPorMesAsync(hoy.Year, hoy.Month);

            if (!Mensajes.Comprobar(movimientos, this))
            {
                return;
            }

            var presupuestos = await Consultas.FilasPresupuestosAsync(_presupuestoServicio, categorias.Value, hoy.Year, hoy.Month);

            if (!Mensajes.Comprobar(presupuestos, this))
            {
                return;
            }

            decimal ingresos = movimientos.Value.Where(m => m.Tipo == Categoria.TipoMovimiento.Ingreso).Sum(m => m.Importe);
            decimal gastos = movimientos.Value.Where(m => m.Tipo == Categoria.TipoMovimiento.Gasto).Sum(m => m.Importe);
            decimal balance = ingresos - gastos;

            lblSaldoValor.Text = saldoTotal.Value.ToString("C2");
            lblSaldoPie.Text = cuentas.Value.Count == 1 ? "En 1 cuenta" : $"Repartido en {cuentas.Value.Count} cuentas";

            lblIngresosValor.Text = ingresos.ToString("C2");
            lblIngresosPie.Text = $"Balance del mes: {(balance > 0 ? "+" : "")}{balance:C2}";

            lblGastosValor.Text = gastos.ToString("C2");
            lblGastosPie.Text = ingresos > 0 ? $"{gastos / ingresos:P0} de tus ingresos" : "Sin ingresos este mes";

            graficoGastos.MostrarDatos(Consultas.GastosPorCategoria(movimientos.Value, categorias.Value));

            gridPresupuestos.DataSource = presupuestos.Value;
            // En la tarjeta no caben los importes: basta con la barra de lo usado; el detalle está en Presupuestos
            Estilo.Ocultar(gridPresupuestos, nameof(FilaPresupuesto.Limite), nameof(FilaPresupuesto.Gastado), nameof(FilaPresupuesto.Restante));
            Estilo.Formato(gridPresupuestos, nameof(FilaPresupuesto.Uso), "P0", true);

            gridMovimientos.DataSource = Consultas.FilasMovimientos(movimientos.Value, cuentas.Value, categorias.Value);
            Estilo.Ocultar(gridMovimientos, nameof(FilaMovimiento.Cuenta), nameof(FilaMovimiento.Descripcion));
            Estilo.Formato(gridMovimientos, nameof(FilaMovimiento.Fecha), "d", false);
            Estilo.Formato(gridMovimientos, nameof(FilaMovimiento.Importe), "C2", true);

            // En el resumen no se selecciona nada, es solo de consulta
            gridPresupuestos.ClearSelection();
            gridMovimientos.ClearSelection();
        }
    }
}
