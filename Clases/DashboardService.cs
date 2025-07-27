using Microsoft.EntityFrameworkCore;
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



    public decimal ObtenerOtrosIngresosHoy(DateTime fecha)
    {
        var hoy = fecha.Date;
        return context.Ingresos
            .AsEnumerable()
            .Where(i => i.Secuencial_Empresa == secuencialEmpresa &&
                        TryFecha(i.Fecha, out var f) && f.Date == hoy &&
                        i.Tipo_Ingreso == "Ingreso Manual")
            .Sum(i => ConvertirDecimal(i.Total));
    }

    public decimal ObtenerOtrosEgresosHoy(DateTime fecha)
    {
        var hoy = fecha.Date;
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
    

    public List<Producto> ObtenerProductosPorVencer(int dias = 5)
    {
        DateTime hoy = DateTime.Today.Date;
        DateTime limite = hoy.AddDays(dias);

        return context.Productos
            .AsEnumerable()
            .Where(p => p.Secuencial_Empresa == secuencialEmpresa &&
                        TryFecha(p.Fecha_Caducidad, out var fechaCaducidad) &&
                        fechaCaducidad.Date >= hoy &&
                        fechaCaducidad.Date <= limite)
            .ToList();
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



    public decimal ObtenerTotalIngresosHoy(DateTime fecha)
    {
        var hoy = fecha.Date;
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

    public decimal ObtenerTotalEgresosHoy(DateTime fecha)
    {
        var hoy = fecha.Date;
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

    public List<object> ObtenerCuentasPorCobrarVencidasConCliente()
    {
        var hoy = DateTime.Today;

        var resultado = context.Cuentas_Cobrar
            .Where(c => c.Secuencial_Empresa == secuencialEmpresa)
            .Join(context.Clientes,
                  cc => cc.Secuencial_Cliente,
                  cli => cli.Secuencial,
                  (cc, cli) => new { Cuenta = cc, Cliente = cli })
            .AsEnumerable()
            .Where(x => !string.IsNullOrEmpty(x.Cuenta.Fecha_Vencimiento) &&
                        TryFecha(x.Cuenta.Fecha_Vencimiento, out var f) &&
                        f.Date < hoy &&
                        x.Cuenta.Saldo > 0)
            .Select(x => new
            {
                Cuenta = x.Cuenta.Secuencial,
                Factura = x.Cuenta.Secuencial_Factura,
                Vencio = x.Cuenta.Fecha_Vencimiento,
                Saldo = x.Cuenta.Saldo,
                Cliente = x.Cliente.Nombre,
                Telefono = x.Cliente.Telefono,
                
            })
            .ToList<object>();

        return resultado;
    }




    public List<object> ObtenerCuentasPorPagarVencidasConProveedor()
    {
        var hoy = DateTime.Today;

        var resultado = context.Cuentas_Pagar
            .Where(p => p.Secuencial_Empresa == secuencialEmpresa)
            .Join(context.Proveedores,
                  cp => cp.Secuencial_Proveedor,
                  prov => prov.Secuencial,
                  (cp, prov) => new { Cuenta = cp, Proveedor = prov })
            .AsEnumerable()
            .Where(x => !string.IsNullOrEmpty(x.Cuenta.Fecha_Vencimiento) &&
                        TryFecha(x.Cuenta.Fecha_Vencimiento, out var f) &&
                        f.Date < hoy &&
                        x.Cuenta.Saldo > 0)
            .Select(x => new
            {
                Cuenta = x.Cuenta.Secuencial,
                Factura = x.Cuenta.Secuencial_Factura,
                Vencio = x.Cuenta.Fecha_Vencimiento,
                Saldo = x.Cuenta.Saldo,
                Proveedor = x.Proveedor.Nombre,
                Telefono = x.Proveedor.Telefono
            })
            .ToList<object>();

        return resultado;
    }





    public List<Cuentas_Cobrar> ObtenerCuentasPorCobrarVencidas() 
    { 
        var hoy = DateTime.Today; 
        return context.Cuentas_Cobrar
            .AsEnumerable()
            .Where(c => c.Secuencial_Empresa == secuencialEmpresa && 
            TryFecha(c.Fecha_Vencimiento, out var f) && f.Date < hoy && c.Saldo > 0).ToList(); }



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
            string fechaTexto = fecha.ToString("dd/MM/yyyy");


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
            string fechaTexto = fecha.ToString("dd/MM/yyyy");


            return db.Egresos
                .Where(e => e.Tipo_Egreso == "Egreso Manual" &&
                            e.Fecha.StartsWith(fechaTexto) &&
                            e.Secuencial_Empresa == secuencialEmpresa)
                .Sum(e => (decimal?)e.Total) ?? 0;
        }
    }



    public decimal ObtenerTotalIngresosPorFecha(DateTime fecha)
    {
        string fechaTexto = fecha.ToString("dd/MM/yyyy");


        using (var db = new Monitux_DB_Context())
        {
            return db.Ingresos
                .Where(i => i.Fecha.StartsWith(fechaTexto) && i.Secuencial_Empresa == secuencialEmpresa)
                .Sum(i => (decimal?)i.Total) ?? 0;
        }
    }

    public decimal ObtenerTotalEgresosPorFecha(DateTime fecha)
    {
        string fechaTexto = fecha.ToString("dd/MM/yyyy");


        using (var db = new Monitux_DB_Context())
        {
            return db.Egresos
                .Where(e => e.Fecha.StartsWith(fechaTexto) && e.Secuencial_Empresa == secuencialEmpresa)
                .Sum(e => (decimal?)e.Total) ?? 0;
        }
    }



    ////////////////////////

    public List<object> ObtenerProductosAgotados()
    {
        using (var db = new Monitux_DB_Context())
        {
            return db.Productos
                     .Where(p => (p.Cantidad < p.Existencia_Minima || p.Cantidad == 0)
                              && p.Secuencial_Empresa == secuencialEmpresa)
                     .Select(p => new
                     {
                         Codigo = p.Codigo,
                         Descripcion = p.Descripcion,
                            Cantidad = p.Cantidad,
                            Stock_Minimo = p.Existencia_Minima,
                     })
                     .ToList<object>(); // 👈 necesario para devolver como lista genérica
        }
    }



     public List<object> ObtenerProductosVencidosLista()
     {
         using (var db = new Monitux_DB_Context())
         {
             return db.Productos
                      .Where(p => p.Secuencial_Empresa == secuencialEmpresa)
                      .AsEnumerable()
                      .Select(p => new
                      {
                          Producto = p,
                          FechaValida = DateTime.TryParse(p.Fecha_Caducidad, out DateTime fecha) ? fecha : (DateTime?)null
                      })
                      .Where(x => x.FechaValida.HasValue && x.FechaValida.Value < DateTime.Today)
                      .Select(x => new
                      {
                          Codigo = x.Producto.Codigo,
                          Descripcion = x.Producto.Descripcion,
                          Fecha_Caducidad = x.Producto.Fecha_Caducidad,
                          Cantidad = x.Producto.Cantidad
                      })
                      .ToList<object>();
         }
     }
     //Ojo Este método está comentado porque no se usa en el código actual, pero lo dejo aquí por si lo necesitas más adelante.


  /*  public List<object> ObtenerProductosPorVencerLista(int dias = 5)
    {
        using (var db = new Monitux_DB_Context())
        {
            DateTime hoy = DateTime.Today;
            DateTime limite = hoy.AddDays(dias);

            return db.Productos
                     .Where(p => p.Secuencial_Empresa == secuencialEmpresa)
                     .AsEnumerable()
                     .Select(p => new
                     {
                         Producto = p,
                         FechaValida = DateTime.TryParse(p.Fecha_Caducidad, out DateTime fecha) ? fecha : (DateTime?)null
                     })
                     .Where(x => x.FechaValida.HasValue &&
                                 x.FechaValida.Value.Date >= hoy &&
                                 x.FechaValida.Value.Date <= limite)
                     .Select(x => new
                     {
                         Codigo = x.Producto.Codigo,
                         Descripcion = x.Producto.Descripcion,
                         Fecha_Caducidad = x.Producto.Fecha_Caducidad,
                         Cantidad = x.Producto.Cantidad
                     })
                     .ToList<object>();
        }
    }
  */



    public List<object> ObtenerProductosConStockCriticoLista()
    {
        using (var db = new Monitux_DB_Context())
        {
            return db.Productos
                     .Where(p => p.Secuencial_Empresa == secuencialEmpresa)
                     .AsEnumerable() // evaluación en memoria
                     .Where(p => p.Cantidad > 0 && p.Cantidad == p.Existencia_Minima)
                     .Select(p => new
                     {
                         Codigo = p.Codigo,
                         Descripcion = p.Descripcion,
                         Cantidad = p.Cantidad,
                         Stock_Minimo = p.Existencia_Minima,
                         Estado = "Stock crítico"
                     })
                     .ToList<object>();
        }
    }






    public decimal ObtenerTotalIngresosDelMes()
    {
        using (var db = new Monitux_DB_Context())
        {
            DateTime fechaInicioMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime fechaFinMes = fechaInicioMes.AddMonths(1).AddDays(-1);


         

            var ingresosDelMes = db.Ingresos
                .AsEnumerable() // pasamos a memoria porque la fecha es string
                .Where(i => DateTime.TryParseExact(i.Fecha.Split(' ')[0], "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime fechaIngreso)
                         && fechaIngreso >= fechaInicioMes
                         && fechaIngreso <= fechaFinMes
                         && i.Secuencial_Empresa == this.secuencialEmpresa)
                .Sum(i => (decimal?)i.Total) ?? 0;

            return ingresosDelMes;
        }
    }




    public decimal ObtenerTotalEgresosDelMes()
    {
        using (var db = new Monitux_DB_Context())
        {
            DateTime fechaInicioMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime fechaFinMes = fechaInicioMes.AddMonths(1).AddDays(-1);

            var egresosDelMes = db.Egresos
                .AsEnumerable()
                .Where(e => DateTime.TryParseExact(e.Fecha.Split(' ')[0], "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime fechaEgreso)
                         && fechaEgreso >= fechaInicioMes
                         && fechaEgreso <= fechaFinMes
                         && e.Secuencial_Empresa == this.secuencialEmpresa)
                .Sum(e => (decimal?)e.Total) ?? 0;

            return egresosDelMes;
        }
    }






    ////////////////////////

    public decimal CalcularCambioPorcentualVentasMes()
    {
        using (var db = new Monitux_DB_Context())
        {
            var ahora = DateTime.Now;
            int mesActual = ahora.Month, añoActual = ahora.Year;
            int mesAnterior = mesActual == 1 ? 12 : mesActual - 1;
            int añoAnterior = mesActual == 1 ? añoActual - 1 : añoActual;

            var ventas = db.Ventas.ToList();

            var totalActual = ventas
                .Where(v => DateTime.TryParse(v.Fecha, out var f) && f.Month == mesActual && f.Year == añoActual)
                .Sum(v => v.Total);

            var totalAnterior = ventas
                .Where(v => DateTime.TryParse(v.Fecha, out var f) && f.Month == mesAnterior && f.Year == añoAnterior)
                .Sum(v => v.Total);

            if (totalAnterior == 0) return 0;
            return (decimal)(((totalActual - totalAnterior) / totalAnterior) * 100);
        }
    }

    public decimal CalcularCambioPorcentualComprasMes()
    {
        using (var db = new Monitux_DB_Context())
        {
            var ahora = DateTime.Now;
            int mesActual = ahora.Month, añoActual = ahora.Year;
            int mesAnterior = mesActual == 1 ? 12 : mesActual - 1;
            int añoAnterior = mesActual == 1 ? añoActual - 1 : añoActual;

            var compras = db.Compras.ToList();

            var totalActual = compras
                .Where(c => DateTime.TryParse(c.Fecha, out var f) && f.Month == mesActual && f.Year == añoActual)
                .Sum(c => c.Total);

            var totalAnterior = compras
                .Where(c => DateTime.TryParse(c.Fecha, out var f) && f.Month == mesAnterior && f.Year == añoAnterior)
                .Sum(c => c.Total);

            if (totalAnterior == 0) return 0;
            return (decimal)(((totalActual - totalAnterior) / totalAnterior) * 100);
        }
    }

    public decimal CalcularCambioPorcentualIngresosMes()
    {
        using (var db = new Monitux_DB_Context())
        {
            var ahora = DateTime.Now;
            int mesActual = ahora.Month, añoActual = ahora.Year;
            int mesAnterior = mesActual == 1 ? 12 : mesActual - 1;
            int añoAnterior = mesActual == 1 ? añoActual - 1 : añoActual;

            var ingresos = db.Ingresos.ToList();

            var totalActual = ingresos
                .Where(i => DateTime.TryParse(i.Fecha, out var f) && f.Month == mesActual && f.Year == añoActual)
                .Sum(i => i.Total);

            var totalAnterior = ingresos
                .Where(i => DateTime.TryParse(i.Fecha, out var f) && f.Month == mesAnterior && f.Year == añoAnterior)
                .Sum(i => i.Total);

            if (totalAnterior == 0) return 0;
            return (decimal)(((totalActual - totalAnterior) / totalAnterior) * 100);
        }
    }

    public decimal CalcularCambioPorcentualEgresosMes()
    {
        using (var db = new Monitux_DB_Context())
        {
            var ahora = DateTime.Now;
            int mesActual = ahora.Month, añoActual = ahora.Year;
            int mesAnterior = mesActual == 1 ? 12 : mesActual - 1;
            int añoAnterior = mesActual == 1 ? añoActual - 1 : añoActual;

            var egresos = db.Egresos.ToList();

            var totalActual = egresos
                .Where(e => DateTime.TryParse(e.Fecha, out var f) && f.Month == mesActual && f.Year == añoActual)
                .Sum(e => e.Total);

            var totalAnterior = egresos
                .Where(e => DateTime.TryParse(e.Fecha, out var f) && f.Month == mesAnterior && f.Year == añoAnterior)
                .Sum(e => e.Total);

            if (totalAnterior == 0) return 0;
            return (decimal)(((totalActual - totalAnterior) / totalAnterior) * 100);
        }
    }

    /////////////////////////



    public decimal ObtenerTotalVentasHoy(DateTime fecha)
    {
        DateTime hoy = fecha.Date;

        return context.Ventas
            .AsEnumerable()
            .Where(v => v.Secuencial_Empresa == secuencialEmpresa &&
                        TryFecha(v.Fecha, out var f) &&
                        f.Date == hoy)
            .Sum(v => ConvertirDecimal(v.Total));
    }


    public decimal ObtenerTotalComprasHoy(DateTime fecha)
    {
        var hoy = fecha.Date;
        return context.Compras
            .AsEnumerable()
            .Where(c => c.Secuencial_Empresa == secuencialEmpresa &&
                        TryFecha(c.Fecha, out var f) && f.Date == hoy)
            .Sum(c => ConvertirDecimal(c.Total));
    }




    #region Para el Proceso de cargar tarjetas por fecha


    public decimal CalcularCambioPorcentualVentasMes(DateTime fecha)
    {
        var inicioMesActual = new DateTime(fecha.Year, fecha.Month, 1);
        var inicioMesAnterior = inicioMesActual.AddMonths(-1);

        var totalMesActual = ObtenerTotalVentasPorRango(inicioMesActual, inicioMesActual.AddMonths(1).AddDays(-1));
        var totalMesAnterior = ObtenerTotalVentasPorRango(inicioMesAnterior, inicioMesActual.AddDays(-1));

        if (totalMesAnterior == 0)
            return 0;

        return ((totalMesActual - totalMesAnterior) / totalMesAnterior) * 100;
    }

    public decimal CalcularCambioPorcentualComprasMes(DateTime fecha)
    {
        var inicioMesActual = new DateTime(fecha.Year, fecha.Month, 1);
        var inicioMesAnterior = inicioMesActual.AddMonths(-1);

        var totalMesActual = ObtenerTotalComprasPorRango(inicioMesActual, inicioMesActual.AddMonths(1).AddDays(-1));
        var totalMesAnterior = ObtenerTotalComprasPorRango(inicioMesAnterior, inicioMesActual.AddDays(-1));

        if (totalMesAnterior == 0)
            return 0;

        return ((totalMesActual - totalMesAnterior) / totalMesAnterior) * 100;
    }

    public decimal CalcularCambioPorcentualIngresosMes(DateTime fecha)
    {
        var inicioMesActual = new DateTime(fecha.Year, fecha.Month, 1);
        var inicioMesAnterior = inicioMesActual.AddMonths(-1);

        var totalMesActual = ObtenerTotalIngresosPorRango(inicioMesActual, inicioMesActual.AddMonths(1).AddDays(-1));
        var totalMesAnterior = ObtenerTotalIngresosPorRango(inicioMesAnterior, inicioMesActual.AddDays(-1));

        if (totalMesAnterior == 0)
            return 0;

        return ((totalMesActual - totalMesAnterior) / totalMesAnterior) * 100;
    }

    public decimal CalcularCambioPorcentualEgresosMes(DateTime fecha)
    {
        var inicioMesActual = new DateTime(fecha.Year, fecha.Month, 1);
        var inicioMesAnterior = inicioMesActual.AddMonths(-1);

        var totalMesActual = ObtenerTotalEgresosPorRango(inicioMesActual, inicioMesActual.AddMonths(1).AddDays(-1));
        var totalMesAnterior = ObtenerTotalEgresosPorRango(inicioMesAnterior, inicioMesActual.AddDays(-1));

        if (totalMesAnterior == 0)
            return 0;

        return ((totalMesActual - totalMesAnterior) / totalMesAnterior) * 100;
    }




    /////////////////////////////
    ///
    public decimal ObtenerTotalVentasPorRango(DateTime desde, DateTime hasta)
    {
        using (var db = new Monitux_DB_Context())
        {
            return db.Ventas
                     .AsEnumerable()
                     .Where(v =>
                         DateTime.TryParseExact(v.Fecha, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime fecha) &&
                         fecha >= desde && fecha <= hasta)
                     .Sum(v => (decimal?)v.Total) ?? 0;
        }
    }

    public decimal ObtenerTotalComprasPorRango(DateTime desde, DateTime hasta)
    {
        using (var db = new Monitux_DB_Context())
        {
            return db.Compras
                     .AsEnumerable()
                     .Where(c =>
                         DateTime.TryParseExact(c.Fecha, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime fecha) &&
                         fecha >= desde && fecha <= hasta)
                     .Sum(c => (decimal?)c.Total) ?? 0;
        }
    }

    public decimal ObtenerTotalIngresosPorRango(DateTime desde, DateTime hasta)
    {
        using (var db = new Monitux_DB_Context())
        {
            return db.Ingresos
                     .AsEnumerable()
                     .Where(i =>
                         DateTime.TryParseExact(i.Fecha, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime fecha) &&
                         fecha >= desde && fecha <= hasta)
                     .Sum(i => (decimal?)i.Total) ?? 0;
        }
    }

    public decimal ObtenerTotalEgresosPorRango(DateTime desde, DateTime hasta)
    {
        using (var db = new Monitux_DB_Context())
        {
            return db.Egresos
                     .AsEnumerable()
                     .Where(e =>
                         DateTime.TryParseExact(e.Fecha, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime fecha) &&
                         fecha >= desde && fecha <= hasta)
                     .Sum(e => (decimal?)e.Total) ?? 0;
        }
    }





    #endregion



    ///////////////////////



}