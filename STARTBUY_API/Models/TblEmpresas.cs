using System;
using System.Collections.Generic;

namespace STARTBUY_API
{
    public partial class TblEmpresas
    {
        public int EmpresaId { get; set; }
        public string Empresa { get; set; }
        public int? PaisId { get; set; }
        public int? CiudadId { get; set; }
        public string DireccionCompleta { get; set; }
        public string NombreContacto { get; set; }
        public int? NumeroContacto { get; set; }
        public bool? Estado { get; set; }
    }
}
