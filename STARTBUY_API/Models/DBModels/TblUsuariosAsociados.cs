﻿using System;
using System.Collections.Generic;

namespace STARTBUY_API
{
    public partial class TblUsuariosAsociados
    {
        public int UsuarioAsociadoId { get; set; }
        public string Usuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Password { get; set; }
        public string NombreEmpresa { get; set; }
        public int? GeneroId { get; set; }
        public int? PaisId { get; set; }
        public int? DepartamentoId { get; set; }
        public int? CiudadId { get; set; }
        public string Email { get; set; }
        public string DireccionCompleta { get; set; }
        public int? Telefono { get; set; }
        public int? EmpresaId { get; set; }
        public int? RoleId { get; set; }
        public bool? ConfirmadoPorGerencia { get; set; }

        public virtual TblCiudades Ciudad { get; set; }
        public virtual TblDepartamentos Departamento { get; set; }
        public virtual TblPaises Pais { get; set; }
        public virtual TblRoles Role { get; set; }
    }
}
