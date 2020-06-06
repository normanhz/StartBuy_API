using System;
using System.Collections.Generic;

namespace STARTBUY_API
{
    public partial class TblGanancias
    {
        public int GananciaId { get; set; }
        public int? VentaProductoId { get; set; }
        public decimal? Total { get; set; }

        public virtual TblVentasProductos VentaProducto { get; set; }
    }
}
