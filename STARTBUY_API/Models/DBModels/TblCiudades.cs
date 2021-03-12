using System;
using System.Collections.Generic;

namespace STARTBUY_API
{
    public partial class TblCiudades
    {
        public TblCiudades()
        {
            TblEmpresas = new HashSet<TblEmpresas>();
            TblUsuariosAsociados = new HashSet<TblUsuariosAsociados>();
            TblUsuariosPersonas = new HashSet<TblUsuariosPersonas>();
        }

        public int CiudadId { get; set; }
        public string Ciudad { get; set; }
        public int? DepartamentoId { get; set; }

        public virtual TblDepartamentos Departamento { get; set; }
        public virtual ICollection<TblEmpresas> TblEmpresas { get; set; }
        public virtual ICollection<TblUsuariosAsociados> TblUsuariosAsociados { get; set; }
        public virtual ICollection<TblUsuariosPersonas> TblUsuariosPersonas { get; set; }
    }
}
