using System;
using System.Collections.Generic;

namespace STARTBUY_API
{
    public partial class TblUsuarios
    {
        public int UsuarioId { get; set; }
        public string Usuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Password { get; set; }
        public int? GeneroId { get; set; }
        public int? PaisId { get; set; }
        public int? CiudadId { get; set; }
        public string Email { get; set; }
        public string DireccionCompleta { get; set; }
        public int? Telefono { get; set; }
        public int? EmpresaId { get; set; }
        public int? RoleId { get; set; }
    }
}
