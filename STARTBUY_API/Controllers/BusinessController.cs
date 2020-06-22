using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace STARTBUY_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : Controller
    {
        private readonly StartBuyContext _context;

        public BusinessController(StartBuyContext context)
        {
            _context = context;
        }

        [HttpGet("GetBusinessCategories")]
        public async Task<ActionResult> GetBusinessCategories()
        {
            var categories = await _context.TblCategoriasEmpresa.ToListAsync();
            return Ok(categories);
        }


        [HttpGet("GetBusinessByCategories/{id}")]
        public async Task<ActionResult> GetBusinessByCategories(int id)
        {
            var business = await _context.TblEmpresas.Where(x => x.CategoriaEmpresaId == id).ToListAsync();

            var validar = business == null;
            if (validar)
            {
                return NotFound();
            }

            return Ok(business);

        }

        [HttpGet("GetProductsByBusiness/{id}")]
        public async Task<ActionResult> GetProductsByBusiness(int id)
        {
            var products = await _context.TblProductos.Where(x=> x.EmpresaId == id).OrderBy(y=>y.CategoriaProductoId).ToListAsync();
            return Ok(products);
        }

    }
}