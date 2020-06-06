using System;
using System.Collections.Generic;

namespace STARTBUY_API
{
    public partial class TblCategoriasEmpresa
    {
        public TblCategoriasEmpresa()
        {
            TblEmpresas = new HashSet<TblEmpresas>();
        }

        public int CategoriaEmpresaId { get; set; }
        public string CategoriaEmpresa { get; set; }
        public string CategoriaEmpresaImage { get; set; }

        public virtual ICollection<TblEmpresas> TblEmpresas { get; set; }
    }
}
