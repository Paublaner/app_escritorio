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

            Estilo.Marcar(Rol.Secundario, lblMes, lblSaldoTitulo, lblIngresosTitulo, lblGastosTitulo);
            Estilo.Marcar(Rol.Ingreso, lblIngresosValor);
            Estilo.Marcar(Rol.Gasto, lblGastosValor);
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

            lblSaldoValor.Text = saldoTotal.Value.ToString("C2");
            lblIngresosValor.Text = movimientos.Value.Where(m => m.Tipo == Categoria.TipoMovimiento.Ingreso).Sum(m => m.Importe).ToString("C2");
            lblGastosValor.Text = movimientos.Value.Where(m => m.Tipo == Categoria.TipoMovimiento.Gasto).Sum(m => m.Importe).ToString("C2");

            graficoGastos.MostrarDatos(Consultas.GastosPorCategoria(movimientos.Value, categorias.Value));

            gridPresupuestos.DataSource = presupuestos.Value;
            Estilo.Ocultar(gridPresupuestos, nameof(FilaPresupuesto.Restante));
            Estilo.Formato(gridPresupuestos, nameof(FilaPresupuesto.Limite), "C2", true);
            Estilo.Formato(gridPresupuestos, nameof(FilaPresupuesto.Gastado), "C2", true);
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
