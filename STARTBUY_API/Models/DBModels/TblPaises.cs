using System;
using System.Collections.Generic;

namespace STARTBUY_API
{
    public partial class TblPaises
    {
        public TblPaises()
        {
            TblDepartamentos = new HashSet<TblDepartamentos>();
            TblEmpresas = new HashSet<TblEmpresas>();
            TblUsuariosAsociados = new HashSet<TblUsuariosAsociados>();
            TblUsuariosPersonas = new HashSet<TblUsuariosPersonas>();
        }

        public int PaisId { get; set; }
        public string CodePais { get; set; }
        public string Pais { get; set; }

        public virtual ICollection<TblDepartamentos> TblDepartamentos { get; set; }
        public virtual ICollection<TblEmpresas> TblEmpresas { get; set; }
        public virtual ICollection<TblUsuariosAsociados> TblUsuariosAsociados { get; set; }
        public virtual ICollection<TblUsuariosPersonas> TblUsuariosPersonas { get; set; }
    }
}
