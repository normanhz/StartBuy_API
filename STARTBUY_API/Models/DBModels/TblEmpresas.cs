using System;
using System.Collections.Generic;

namespace STARTBUY_API
{
    public partial class TblEmpresas
    {
        public TblEmpresas()
        {
            TblCategoriasProductos = new HashSet<TblCategoriasProductos>();
            TblProductos = new HashSet<TblProductos>();
            TblVentasProductos = new HashSet<TblVentasProductos>();
        }

        public int EmpresaId { get; set; }
        public string Empresa { get; set; }
        public string EmpresaImage { get; set; }
        public int? PaisId { get; set; }
        public int? DepartamentoId { get; set; }
        public int? CiudadId { get; set; }
        public string DireccionCompleta { get; set; }
        public string NombreContacto { get; set; }
        public int? NumeroContacto { get; set; }
        public bool? Estado { get; set; }
        public int? CategoriaEmpresaId { get; set; }

        public virtual TblCategoriasEmpresa CategoriaEmpresa { get; set; }
        public virtual TblCiudades Ciudad { get; set; }
        public virtual TblDepartamentos Departamento { get; set; }
        public virtual TblPaises Pais { get; set; }
        public virtual ICollection<TblCategoriasProductos> TblCategoriasProductos { get; set; }
        public virtual ICollection<TblProductos> TblProductos { get; set; }
        public virtual ICollection<TblVentasProductos> TblVentasProductos { get; set; }
    }
}
