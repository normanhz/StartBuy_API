using System;
using System.Collections.Generic;

namespace STARTBUY_API
{
    public partial class TblCategoriasProductos
    {
        public TblCategoriasProductos()
        {
            TblVentasProductos = new HashSet<TblVentasProductos>();
        }

        public int CategoriaProductoId { get; set; }
        public string CategoriaProducto { get; set; }
        public int? EmpresaId { get; set; }

        public virtual TblEmpresas Empresa { get; set; }
        public virtual ICollection<TblVentasProductos> TblVentasProductos { get; set; }
    }
}
