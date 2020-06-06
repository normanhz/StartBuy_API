using System;
using System.Collections.Generic;

namespace STARTBUY_API
{
    public partial class TblProductos
    {
        public TblProductos()
        {
            TblVentasProductos = new HashSet<TblVentasProductos>();
        }

        public int ProductoId { get; set; }
        public string Producto { get; set; }
        public string ProductoImage { get; set; }
        public int? EmpresaId { get; set; }
        public int? CategoriaProductoId { get; set; }
        public decimal? Precio { get; set; }
        public int? CantidadEnStock { get; set; }
        public int? UsuarioIngreso { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public int? UsuarioModifico { get; set; }
        public DateTime? FechaModifico { get; set; }

        public virtual TblEmpresas Empresa { get; set; }
        public virtual ICollection<TblVentasProductos> TblVentasProductos { get; set; }
    }
}
