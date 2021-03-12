using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace STARTBUY_API.Models.Stored_Procedures
{
    public class SP_SaveProductsToCart
    {
        [Key]
        public int ProductoId { get; set; }

        public int Cantidad { get; set; }

        public int UsuarioComprador { get; set; }
    }
}
