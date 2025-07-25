using Monitux_POS.Clases;

using Monitux_POS.Clases;

public class DashboardService
{
    private readonly Monitux_DB_Context context;
    private readonly int secuencialEmpresa;

    public DashboardService(int secuencialEmpresa)
    {
        context = new Monitux_DB_Context();
        this.secuencialEmpresa = secuencialEmpresa;
    }

    private bool TryFecha(string fechaStr, out DateTime fecha)
    {
        return DateTime.TryParse(fechaStr, out fecha);
    }

    private decimal ConvertirDecimal(object total)
    {
        return total != null ? Convert.ToDecimal(total) : 0m;
    }

    public decimal ObtenerTotalVentasHoy()
    {
        var hoy = DateTime.Today;
        return context.Ventas
            .AsEnumerable()
            .Where(v => v.Secuencial_Empresa == secuencialEmpresa &&
                        TryFecha(v.Fecha, out var f) && f.Date == hoy)
            .Sum(v => ConvertirDecimal(v.Total));
    }

    public decimal ObtenerTotalVentasDelMes()
    {
        var hoy = DateTime.Today;
        return context.Ventas
            .AsEnumerable()
            .Where(v => v.Secuencial_Empresa == secuencialEmpresa &&
                        TryFecha(v.Fecha, out var f) &&
                        f.Month == hoy.Month && f.Year == hoy.Year)
            .Sum(v => ConvertirDecimal(v.Total));
    }

    public decimal[] ObtenerVentasUltimos7Dias()
    {
        var hoy = DateTime.Today;
        var ventas = context.Ventas
            .AsEnumerable()
            .Where(v => v.Secuencial_Empresa == secuencialEmpresa &&
                        TryFecha(v.Fecha, out _))
            .Select(v => new
            {
                Fecha = DateTime.Parse(v.Fecha),
                Total = ConvertirDecimal(v.Total)
            }).ToList();

        return Enumerable.Range(0, 7)
            .Select(offset =>
            {
                var dia = hoy.AddDays(-offset);
                return ventas.Where(v => v.Fecha.Date == dia).Sum(v => v.Total);
            }).ToArray();
    }

    public decimal ObtenerVentasPorUsuario(int secuencialUsuario)
    {
        return context.Ventas
            .AsEnumerable()
            .Where(v => v.Secuencial_Empresa == secuencialEmpresa &&
                        v.Secuencial_Usuario == secuencialUsuario)
            .Sum(v => ConvertirDecimal(v.Total));
    }

    public decimal ObtenerOtrosIngresosHoy()
    {
        var hoy = DateTime.Today;
        return context.Ingresos
            .AsEnumerable()
            .Where(i => i.Secuencial_Empresa == secuencialEmpresa &&
                        TryFecha(i.Fecha, out var f) && f.Date == hoy &&
                        i.Tipo_Ingreso == "Ingreso Manual")
            .Sum(i => ConvertirDecimal(i.Total));
    }

    public decimal ObtenerOtrosEgresosHoy()
    {
        var hoy = DateTime.Today;
        return context.Egresos
            .AsEnumerable()
            .Where(e => e.Secuencial_Empresa == secuencialEmpresa &&
                        TryFecha(e.Fecha, out var f) && f.Date == hoy &&
                        e.Tipo_Egreso == "Egreso Manual")
            .Sum(e => ConvertirDecimal(e.Total));
    }

    public int ObtenerProductosVencidos()
    {
        var hoy = DateTime.Today;
        return context.Productos
            .AsEnumerable()
            .Count(p => p.Secuencial_Empresa == secuencialEmpresa &&
                        TryFecha(p.Fecha_Caducidad, out var f) && f < hoy);
    }

    public string[] ObtenerActividadReciente(int cantidad = 10)
    {
        return context.Actividades
            .AsEnumerable()
            .Where(a => a.Secuencial_Empresa == secuencialEmpresa &&
                        TryFecha(a.Fecha, out _))
            .OrderByDescending(a => TryFecha(a.Fecha, out var f) ? f : DateTime.MinValue)
            .Take(cantidad)
            .Select(a => $"{a.Fecha}: {a.Descripcion}")
            .ToArray();
    }

    public decimal ObtenerTotalComprasHoy()
    {
        var hoy = DateTime.Today;
        return context.Compras
            .AsEnumerable()
            .Where(c => c.Secuencial_Empresa == secuencialEmpresa &&
                        TryFecha(c.Fecha, out var f) && f.Date == hoy)
            .Sum(c => ConvertirDecimal(c.Total));
    }

    public decimal ObtenerTotalComprasDelMes()
    {
        var hoy = DateTime.Today;
        return context.Compras
            .AsEnumerable()
            .Where(c => c.Secuencial_Empresa == secuencialEmpresa &&
                        TryFecha(c.Fecha, out var f) &&
                        f.Month == hoy.Month && f.Year == hoy.Year)
            .Sum(c => ConvertirDecimal(c.Total));
    }

    public decimal[] ObtenerComprasUltimos7Dias()
    {
        var hoy = DateTime.Today;
        var compras = context.Compras
            .AsEnumerable()
            .Where(c => c.Secuencial_Empresa == secuencialEmpresa &&
                        TryFecha(c.Fecha, out _))
            .Select(c => new
            {
                Fecha = DateTime.Parse(c.Fecha),
                Total = ConvertirDecimal(c.Total)
            }).ToList();

        return Enumerable.Range(0, 7)
            .Select(offset =>
            {
                var dia = hoy.AddDays(-offset);
                return compras.Where(c => c.Fecha.Date == dia).Sum(c => c.Total);
            }).ToArray();
    }

    public decimal ObtenerComprasPorUsuario(int secuencialUsuario)
    {
        return context.Compras
            .AsEnumerable()
            .Where(c => c.Secuencial_Empresa == secuencialEmpresa &&
                        c.Secuencial_Usuario == secuencialUsuario)
            .Sum(c => ConvertirDecimal(c.Total));
    }

    public decimal ObtenerTotalIngresosHoy()
    {
        var hoy = DateTime.Today;
        return context.Ingresos
            .AsEnumerable()
            .Where(i => i.Secuencial_Empresa == secuencialEmpresa &&
                        TryFecha(i.Fecha, out var f) && f.Date == hoy)
            .Sum(i => ConvertirDecimal(i.Total));
    }

    public decimal ObtenerTotalEgresosHoy()
    {
        var hoy = DateTime.Today;
        return context.Egresos
            .AsEnumerable()
            .Where(e => e.Secuencial_Empresa == secuencialEmpresa &&
                        TryFecha(e.Fecha, out var f) && f.Date == hoy)
            .Sum(e => ConvertirDecimal(e.Total));
    }

    public int ObtenerStockCritico()
    {
        return context.Productos
            .AsEnumerable()
            .Count(p => p.Secuencial_Empresa == secuencialEmpresa &&
                        ConvertirDecimal(p.Cantidad) > 0 &&
                        ConvertirDecimal(p.Cantidad) == ConvertirDecimal(p.Existencia_Minima));
    }

    public int ObtenerAgotados()
    {
        return context.Productos
            .AsEnumerable()
            .Count(p => p.Secuencial_Empresa == secuencialEmpresa &&
                        ConvertirDecimal(p.Cantidad) < ConvertirDecimal(p.Existencia_Minima));
    }

    public List<Cuentas_Cobrar> ObtenerCuentasPorCobrarVencidas()
    {
        var hoy = DateTime.Today;
        return context.Cuentas_Cobrar
            .AsEnumerable()
            .Where(c => c.Secuencial_Empresa == secuencialEmpresa &&
                        TryFecha(c.Fecha_Vencimiento, out var f) &&
                        f.Date < hoy && c.Saldo > 0)
            .ToList();
    }

    public List<Cuentas_Pagar> ObtenerCuentasPorPagarVencidas()
    {
        var hoy = DateTime.Today;
        return context.Cuentas_Pagar
            .AsEnumerable()
            .Where(c => c.Secuencial_Empresa == secuencialEmpresa &&
                        TryFecha(c.Fecha_Vencimiento, out var f) &&
                        f.Date < hoy && c.Saldo > 0)
            .ToList();
    }

    public decimal CalcularCambioPorcentualVentas()
    {
        var hoy = DateTime.Today;
        var ayer = hoy.AddDays(-1);
        var ventasHoy = ObtenerTotalVentasPorFecha(hoy);
        var ventasAyer = ObtenerTotalVentasPorFecha(ayer);
        return ventasAyer == 0 ? 0 : ((ventasHoy - ventasAyer) / ventasAyer) * 100;
    }

    public decimal ObtenerTotalVentasPorFecha(DateTime fecha)
    {
        return context.Ventas
            .AsEnumerable()
            .Where(v => v.Secuencial_Empresa == secuencialEmpresa &&
                        TryFecha(v.Fecha, out var f) && f.Date == fecha)
            .Sum(v => ConvertirDecimal(v.Total));
    }

    public decimal ObtenerTotalComprasPorFecha(DateTime fecha)
    {
        return context.Compras
            .AsEnumerable()
            .Where(c => c.Secuencial_Empresa == secuencialEmpresa &&
                        TryFecha(c.Fecha, out var f) && f.Date == fecha)
            .Sum(c => ConvertirDecimal(c.Total));
    }




    //public decimal CalcularCambioPorcentualCompras()
    //{
    //    var hoy = DateTime.Today;
    //    var ayer = hoy.AddDays(-1);
    //    var comprasHoy = ObtenerTotalComprasPorFecha(hoy);
    //    var comprasAyer = ObtenerTotalComprasPorFecha(ayer);

    //    return comprasAyer == 0 ? 0 : ((comprasHoy - comprasAyer) / comprasAyer) * 100;
    //}


    public decimal CalcularCambioPorcentualVentas(DateTime fecha)
    {
        var totalHoy = ObtenerTotalVentasPorFecha(fecha);
        var totalAyer = ObtenerTotalVentasPorFecha(fecha.AddDays(-1));

        if (totalAyer == 0)
            return totalHoy > 0 ? 100 : 0;

        return ((totalHoy - totalAyer) / totalAyer) * 100;
    }





    public decimal CalcularCambioPorcentualCompras(DateTime fecha)
    {
        var totalHoy = ObtenerTotalComprasPorFecha(fecha);
        var totalAyer = ObtenerTotalComprasPorFecha(SafeAddDays(fecha, -1));
        return CalcularCambioPorcentual(totalHoy, totalAyer);
    }

    public decimal CalcularCambioPorcentualIngresos(DateTime fecha)
    {
        var totalHoy = ObtenerTotalIngresosPorFecha(fecha);
        var totalAyer = ObtenerTotalIngresosPorFecha(SafeAddDays(fecha, -1));
        return CalcularCambioPorcentual(totalHoy, totalAyer);
    }

    public decimal CalcularCambioPorcentualEgresos(DateTime fecha)
    {
        var totalHoy = ObtenerTotalEgresosPorFecha(fecha);
        var totalAyer = ObtenerTotalEgresosPorFecha(SafeAddDays(fecha, -1));
        return CalcularCambioPorcentual(totalHoy, totalAyer);
    }

    private decimal CalcularCambioPorcentual(decimal hoy, decimal ayer)
    {
        if (ayer == 0)
            return hoy > 0 ? 100 : 0;

        return ((hoy - ayer) / ayer) * 100;
    }

    private DateTime SafeAddDays(DateTime fecha, int dias)
    {
        // Prevenir que se salga del rango permitido por DateTime
        try
        {
            return fecha.AddDays(dias);
        }
        catch (ArgumentOutOfRangeException)
        {
            return dias < 0 ? DateTime.MinValue : DateTime.MaxValue;
        }
    }

    public decimal ObtenerOtrosIngresosPorFecha(DateTime fecha)
    {
        using (var db = new Monitux_DB_Context())
        {
            string fechaTexto = fecha.ToString("dd/MM/yyyy HH:mm:ss");

            return db.Ingresos
                .Where(i => i.Tipo_Ingreso == "Ingreso Manual" &&
                            i.Fecha.StartsWith(fechaTexto) &&
                            i.Secuencial_Empresa == secuencialEmpresa)
                .Sum(i => (decimal?)i.Total) ?? 0;
        }
    }



    public decimal ObtenerOtrosEgresosPorFecha(DateTime fecha)
    {
        using (var db = new Monitux_DB_Context())
        {
            string fechaTexto = fecha.ToString("dd/MM/yyyy HH:mm:ss");

            return db.Egresos
                .Where(e => e.Tipo_Egreso == "Egreso Manual" &&
                            e.Fecha.StartsWith(fechaTexto) &&
                            e.Secuencial_Empresa == secuencialEmpresa)
                .Sum(e => (decimal?)e.Total) ?? 0;
        }
    }



    public decimal ObtenerTotalIngresosPorFecha(DateTime fecha)
    {
        string fechaTexto = fecha.ToString("dd/MM/yyyy HH:mm:ss");

        using (var db = new Monitux_DB_Context())
        {
            return db.Ingresos
                .Where(i => i.Fecha.StartsWith(fechaTexto) && i.Secuencial_Empresa == secuencialEmpresa)
                .Sum(i => (decimal?)i.Total) ?? 0;
        }
    }

    public decimal ObtenerTotalEgresosPorFecha(DateTime fecha)
    {
        string fechaTexto = fecha.ToString("dd/MM/yyyy HH:mm:ss");

        using (var db = new Monitux_DB_Context())
        {
            return db.Egresos
                .Where(e => e.Fecha.StartsWith(fechaTexto) && e.Secuencial_Empresa == secuencialEmpresa)
                .Sum(e => (decimal?)e.Total) ?? 0;
        }
    }



}