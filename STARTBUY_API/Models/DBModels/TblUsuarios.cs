using System;
using System.Collections.Generic;

namespace STARTBUY_API
{
    public partial class TblUsuarios
    {
        public int UsuarioId { get; set; }
        public string Email { get; set; }
        public string Usuario { get; set; }
        public string Nombres { get; set; }
        public string Password { get; set; }
        public bool? IsAdmin { get; set; }
    }
}
