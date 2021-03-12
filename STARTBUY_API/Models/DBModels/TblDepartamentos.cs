using System;
using System.Collections.Generic;

namespace STARTBUY_API
{
    public partial class TblDepartamentos
    {
        public TblDepartamentos()
        {
            TblCiudades = new HashSet<TblCiudades>();
            TblEmpresas = new HashSet<TblEmpresas>();
            TblUsuariosAsociados = new HashSet<TblUsuariosAsociados>();
            TblUsuariosPersonas = new HashSet<TblUsuariosPersonas>();
        }

        public int DepartamentoId { get; set; }
        public string Departamento { get; set; }
        public int? PaisId { get; set; }

        public virtual TblPaises Pais { get; set; }
        public virtual ICollection<TblCiudades> TblCiudades { get; set; }
        public virtual ICollection<TblEmpresas> TblEmpresas { get; set; }
        public virtual ICollection<TblUsuariosAsociados> TblUsuariosAsociados { get; set; }
        public virtual ICollection<TblUsuariosPersonas> TblUsuariosPersonas { get; set; }
    }
}
