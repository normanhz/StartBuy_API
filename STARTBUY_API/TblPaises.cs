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
            TblUsuarios = new HashSet<TblUsuarios>();
            TblUsuariosPersonas = new HashSet<TblUsuariosPersonas>();
        }

        public int PaisId { get; set; }
        public string CodePais { get; set; }
        public string Pais { get; set; }

        public virtual ICollection<TblDepartamentos> TblDepartamentos { get; set; }
        public virtual ICollection<TblEmpresas> TblEmpresas { get; set; }
        public virtual ICollection<TblUsuarios> TblUsuarios { get; set; }
        public virtual ICollection<TblUsuariosPersonas> TblUsuariosPersonas { get; set; }
    }
}
