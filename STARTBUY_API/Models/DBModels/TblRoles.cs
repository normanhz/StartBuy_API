using System;
using System.Collections.Generic;

namespace STARTBUY_API
{
    public partial class TblRoles
    {
        public TblRoles()
        {
            TblUsuariosAsociados = new HashSet<TblUsuariosAsociados>();
        }

        public int RoleId { get; set; }
        public string Role { get; set; }

        public virtual ICollection<TblUsuariosAsociados> TblUsuariosAsociados { get; set; }
    }
}
