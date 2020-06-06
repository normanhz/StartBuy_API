using System;
using System.Collections.Generic;

namespace STARTBUY_API
{
    public partial class TblRoles
    {
        public TblRoles()
        {
            TblUsuarios = new HashSet<TblUsuarios>();
        }

        public int RoleId { get; set; }
        public string Role { get; set; }

        public virtual ICollection<TblUsuarios> TblUsuarios { get; set; }
    }
}
