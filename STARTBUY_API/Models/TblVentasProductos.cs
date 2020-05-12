using System;
using System.Collections.Generic;

namespace STARTBUY_API
{
    public partial class TblVentasProductos
    {
        public int VentaProductoId { get; set; }
        public int? EmpresaId { get; set; }
        public int? CategoriaProductoId { get; set; }
        public int? ProductoId { get; set; }
        public decimal? Precio { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Total { get; set; }
        public int? UsuarioComprador { get; set; }
        public DateTime? Fecha { get; set; }
        public int? EstadoVentaId { get; set; }
    }
}
