using System;
using System.Collections.Generic;

namespace STARTBUY_API
{
    public partial class TblVentasProductos
    {
        public TblVentasProductos()
        {
            TblGanancias = new HashSet<TblGanancias>();
        }

        public int VentaProductoId { get; set; }
        public int? EmpresaId { get; set; }
        public int? ProductoId { get; set; }
        public decimal? Precio { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Total { get; set; }
        public decimal? TotalNeto { get; set; }
        public int? UsuarioComprador { get; set; }
        public DateTime? Fecha { get; set; }
        public int? EstadoVentaId { get; set; }

        public virtual TblProductos Producto { get; set; }
        public virtual ICollection<TblGanancias> TblGanancias { get; set; }
    }
}
