using System;
using System.Collections.Generic;

namespace STARTBUY_API
{
    public partial class TblNoticias
    {
        public int NoticiaId { get; set; }
        public int? EmpresaId { get; set; }
        public string Descripcion { get; set; }

        public virtual TblEmpresas Empresa { get; set; }
    }
}
