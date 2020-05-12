using System;
using System.Collections.Generic;

namespace STARTBUY_API
{
    public partial class TblProductos
    {
        public int ProductoId { get; set; }
        public string Producto { get; set; }
        public int? EmpresaId { get; set; }
        public int? CategoriaProductoId { get; set; }
        public decimal? Precio { get; set; }
        public int? CantidadEnStock { get; set; }
        public int? UsuarioIngreso { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public int? UsuarioModifico { get; set; }
        public DateTime? FechaModifico { get; set; }
    }
}
