using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STARTBUY_API.Classes
{
    public class AppServices
    {
        StartBuyContext _context = new StartBuyContext();
        public AppServices()
        {

        }

        public async void PutUserVerification(int id)
        {
            var data = await _context.TblUsuariosPersonas.FirstOrDefaultAsync(x => x.UsuarioPersonaId == id);

            data.CuentaVerificada = true;

            await _context.SaveChangesAsync();
        }
    }
}
