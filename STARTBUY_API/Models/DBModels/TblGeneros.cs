using System;
using System.Collections.Generic;

namespace STARTBUY_API
{
    public partial class TblGeneros
    {
        public TblGeneros()
        {
            TblUsuariosPersonas = new HashSet<TblUsuariosPersonas>();
        }

        public int GeneroId { get; set; }
        public string Genero { get; set; }

        public virtual ICollection<TblUsuariosPersonas> TblUsuariosPersonas { get; set; }
    }
}
