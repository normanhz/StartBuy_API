using System;
using System.Collections.Generic;

namespace STARTBUY_API
{
    public partial class TblMetodosPago
    {
        public int MetodoPagoId { get; set; }
        public int? NumeroTarjeta { get; set; }
        public DateTime? FechaExpira { get; set; }
        public int? Cvc { get; set; }
        public int? UsuarioPersonaId { get; set; }
        public bool? PorDefecto { get; set; }

        public virtual TblUsuariosPersonas UsuarioPersona { get; set; }
    }
}
