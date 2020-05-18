using System;
using System.Collections.Generic;

namespace STARTBUY_API
{
    public partial class TblUsuariosPersonas
    {
        public int UsuarioPersonaId { get; set; }
        public string Usuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Password { get; set; }
        public int? CodigoVerificacion { get; set; }
        public int? GeneroId { get; set; }
        public int? PaisId { get; set; }
        public int? DepartamentoId { get; set; }
        public int? CiudadId { get; set; }
        public string Email { get; set; }
        public string DireccionCompleta { get; set; }
        public int? Telefono { get; set; }
        public bool? CuentaVerificada { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaModifico { get; set; }

        public virtual TblCiudades Ciudad { get; set; }
        public virtual TblDepartamentos Departamento { get; set; }
        public virtual TblGeneros Genero { get; set; }
        public virtual TblPaises Pais { get; set; }
    }
}
