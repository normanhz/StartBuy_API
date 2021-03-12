using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using STARTBUY_API.Models.Stored_Procedures;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata;

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

        //[HttpGet("GetProductsByBusiness/{id}")]
        //public async Task<ActionResult> GetProductsByBusiness(int id)
        //{
        //    var products = await _context.TblProductos.Where(x => x.EmpresaId == id).OrderBy(y => y.CategoriaProductoId).ToListAsync();
        //    return Ok(products);
        //}

        [HttpGet("GetProductsByBusiness/{id}")]
        public async Task<ActionResult> GetProductsByBusiness(int id)
        {
            var products = await _context.TblCategoriasProductos.Where(cat => cat.EmpresaId == id)
                .Select(Category => new
                {
                    CategoriaProductoId = Category.CategoriaProductoId,
                    CategoriaProducto = Category.CategoriaProducto,
                    Products = _context.TblProductos.Where(prod => prod.CategoriaProductoId == Category.CategoriaProductoId && prod.Estado == true)
                .Select(Product => new
                {
                    ProductId = Product.ProductoId,
                    Producto = Product.Producto,
                    Descripcion = Product.Descripcion,
                    ProductImage = Product.ProductoImage,
                    EmpresaId = Product.EmpresaId,
                    CategoriaProductoId = Product.CategoriaProductoId,
                    Precio = Product.Precio,
                    CantidadEnStock = Product.CantidadEnStock
                }).ToList()
                }).ToListAsync();

            var validar = products == null;
            if (validar)
            {
                return NotFound();
            }

            return Ok(products);
        }

        [HttpGet("GetProductById/{id}")]
        public async Task<ActionResult> GetProductById(int id)
        {
            var product = await _context.TblProductos.Where(x => x.ProductoId == id).ToListAsync();
            return Ok(product);
        }


        [HttpPost("SaveProductsToCart")]
        public async Task<ActionResult<SP_SaveProductsToCart>> SaveProductsToCart(SP_SaveProductsToCart products)
        {

            SP_SaveProductsToCart item = new SP_SaveProductsToCart()
            {
                ProductoId = products.ProductoId,
                Cantidad = products.Cantidad,
                UsuarioComprador = products.UsuarioComprador

            };

            await _context.Database.ExecuteSqlRawAsync($"SP_SaveProductsToCart {item.ProductoId}, {item.Cantidad}, {item.UsuarioComprador}");

            return Ok(new
                {
                ProductoId = item.ProductoId,
                Cantidad = item.Cantidad,
                UsuarioComprador = item.UsuarioComprador
            });
            
          
        }


        [HttpGet("GetProductsInCart/{id}")]
        public async Task<ActionResult> GetProductsInCart(int id)
        {
            var products = await _context.TblVentasProductos.Where(v => v.UsuarioComprador == id && v.EstadoVentaId == 1)
                .Select(venta => new
                {
                    VentaProductoId = venta.VentaProductoId,
                    ProductoId = venta.ProductoId,
                    Precio = venta.Precio,
                    Cantidad = venta.Cantidad,
                    Total = venta.Total,
                    TotalCompra = _context.TblVentasProductos.Sum(ve => ve.Total),
                    TotalNeto = venta.TotalNeto,
                    UsuarioComprador = venta.UsuarioComprador,
                    Fecha = venta.Fecha,
                    EstadoVentaId = venta.EstadoVentaId,
                    Products = _context.TblProductos.Where(prod => prod.ProductoId == venta.ProductoId)
                .Select(Product => new
                {
                    ProductId = Product.ProductoId,
                    Producto = Product.Producto,
                    Descripcion = Product.Descripcion,
                    ProductImage = Product.ProductoImage,
                    EmpresaId = Product.EmpresaId,
                    CategoriaProductoId = Product.CategoriaProductoId,
                    Precio = Product.Precio,
                    CantidadEnStock = Product.CantidadEnStock
                }).ToList()
                }).ToListAsync();

            var validar = products == null;
            if (validar)
            {
                return NotFound();
            }

            return Ok(products);
        }

        [HttpGet("GetTotalCompraByUserId/{id}")]
        public async Task<ActionResult> GetTotalCompraByUserId(int id)
        {
            var product =  _context.TblVentasProductos.Where(x => x.UsuarioComprador == id && x.EstadoVentaId == 1).Select(x=> x.Total).Sum();
            return Ok(product);
        }

        [HttpDelete("DeleteProductInCart/{id}")]
        public async Task<ActionResult> DeleteProductInCart(int id)
        {
            var product =  _context.TblVentasProductos.Where(x => x.VentaProductoId == id).FirstOrDefault();

            _context.TblVentasProductos.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpPost("CalcularGanancias")]
        public async Task<ActionResult<SP_SaveProductsToCart>> CalcularGanancias(SP_CalcularGanancias products)
        {

            SP_CalcularGanancias item = new SP_CalcularGanancias()
            {
                UsuarioComprador = products.UsuarioComprador

            };

            await _context.Database.ExecuteSqlRawAsync($"SP_CalcularGanancias {item.UsuarioComprador}");

            return Ok(new
            {
                UsuarioComprador = item.UsuarioComprador
            });


        }

        [HttpGet("GetProductosVendidos/{id}")]
        public async Task<ActionResult<TblVentasProductos>> GetProductosVendidos(int id)
        {
            var products = await _context.TblVentasProductos.Where(v => v.EstadoVentaId == 3 || v.EstadoVentaId == 4)
                .Select(venta => new
                {
                   EmpresaId = venta.Producto.EmpresaId,
                   Empresa = venta.Producto.Empresa.Empresa,
                   Producto_Image = venta.Producto.ProductoImage,
                   Producto = venta.Producto.Producto,
                   Precio = venta.Producto.Precio,
                   Cantidad = venta.Cantidad,
                   Total_Neto = venta.TotalNeto,
                   Fecha = venta.Fecha
                }).ToListAsync();

            var validar = products == null;
            if (validar)
            {
                return NotFound();
            }

            return Ok(products.Where(x => x.EmpresaId == id));
        }

         [HttpGet("GetTotalVendidoByEmpresa/{id}")]
         public async Task<ActionResult<TblVentasProductos>> GetTotalVendidoByEmpresa(int id)
         {

             var products =  _context.TblVentasProductos.Where(x => x.EmpresaId == id && x.EstadoVentaId == 3 || x.EstadoVentaId == 4).Select(x => x.TotalNeto).Sum();

             var validar = products == null;
             if (validar)
             {
                 return NotFound();
             }

             return Ok(products);

         }

        [HttpGet("GetProductosPendientes/{id}")]
        public async Task<ActionResult<TblVentasProductos>> GetProductosPendientes(int id)
        {
            var products = await _context.TblVentasProductos.Where(v => v.EstadoVentaId == 3)
                .Select(venta => new
                {
                    EmpresaId = venta.Producto.EmpresaId,
                    Empresa = venta.Producto.Empresa.Empresa,
                    Producto_Image = venta.Producto.ProductoImage,
                    Producto = venta.Producto.Producto,
                    Precio = venta.Producto.Precio,
                    Cantidad = venta.Cantidad,
                    Total_Neto = venta.TotalNeto,
                    Fecha = venta.Fecha
                }).ToListAsync();

            var validar = products == null;
            if (validar)
            {
                return NotFound();
            }

            return Ok(products.Where(x => x.EmpresaId == id));
        }


        [HttpGet("GetNewsByBusiness/{id}")]
        public async Task<ActionResult<TblNoticias>> GetNewsByBusiness(int id)
        {
            var noticias = await _context.TblNoticias.Where(v => v.EmpresaId == id)
                .Select(noticia => new
                {
                    NoticiaID = noticia.NoticiaId,
                    EmpresaID = noticia.EmpresaId,
                    Descripcion = noticia.Descripcion   
                }).ToListAsync();

            var validar = noticias == null;
            if (validar)
            {
                return NotFound();
            }

            return Ok(noticias);
        }

        [HttpGet("GetNewsBusinessById/{id}")]
        public async Task<ActionResult<TblNoticias>> GetNewsBusinessById(int id)
        {
            var noticias = await _context.TblNoticias.Where(v => v.NoticiaId == id)
                .Select(noticia => new
                {
                    NoticiaID = noticia.NoticiaId,
                    EmpresaID = noticia.EmpresaId,
                    Descripcion = noticia.Descripcion
                }).ToListAsync();

            var validar = noticias == null;
            if (validar)
            {
                return NotFound();
            }

            return Ok(noticias);
        }

        [HttpPut("EditNoticeInfo/{id}")]
        public async Task<IActionResult> PutEditNoticeInfo(int id, TblNoticias notices)
        {
            var data = await _context.TblNoticias.FirstOrDefaultAsync(x => x.NoticiaId == id);

            var validar = data == null;
            if (validar)
            {
                return NotFound();
            }

            data.Descripcion = notices.Descripcion;
            data.EmpresaId = notices.EmpresaId;

            await _context.SaveChangesAsync();
            return Ok(new
            {
                NoticiaId = data.NoticiaId,
                Descripcion = data.Descripcion,
                EmpresaId = data.EmpresaId
            });
        }

        [HttpPost("SaveNotices")]
        public async Task<ActionResult<TblNoticias>> PostSaveNotices(TblNoticias notices)
        {
         

            TblNoticias item = new TblNoticias()
            {
                Descripcion = notices.Descripcion,
                EmpresaId = notices.EmpresaId

            };


            var validar = item == null;
            if (!validar)
            {
                _context.TblNoticias.Add(item);
                await _context.SaveChangesAsync();


                return Ok(new
                {
                    NoticiaId = item.NoticiaId,
                    Descripcion = item.Descripcion,
                    EmpresaId = item.EmpresaId
                });
            }
            else
            {
                return BadRequest("Error al guardar la noticia.");
            }
        }

        [HttpDelete("DeleteNotices/{id}")]
        public async Task<ActionResult> DeleteNotices(int id)
        {
            var notice = _context.TblNoticias.Where(x => x.NoticiaId == id).FirstOrDefault();

            _context.TblNoticias.Remove(notice);
            await _context.SaveChangesAsync();

            return Ok(notice);
        }

        [HttpGet("GetAllNews")]
        public async Task<ActionResult<TblNoticias>> GetAllNews()
        {
            var noticias = await _context.TblEmpresas.Where(x=>x.TblNoticias.Count > 0)
                 .Select(empresas => new
                 {
                     EmpresaId = empresas.EmpresaId,
                     Empresa = empresas.Empresa,
                     Noticias = _context.TblNoticias.Where(not => not.EmpresaId == empresas.EmpresaId)
                .Select(notices => new
                {
                    NoticiaId = notices.NoticiaId,
                    Descripcion = notices.Descripcion
                }).ToList()
                 }).ToListAsync();

            var validar = noticias == null;
            if (validar)
            {
                return NotFound();
            }

            return Ok(noticias);
        }

        [HttpGet("GetProductosByEmpresaId/{id}")]
        public async Task<ActionResult<TblProductos>> GetProductosByEmpresaId(int id)
        {
            var products = await _context.TblProductos.Where(v => v.EmpresaId == id ).ToListAsync();

            var validar = products == null;
            if (validar)
            {
                return NotFound();
            }

            return Ok(products);
        }

        [HttpPut("EditEstadoProducto/{id}")]
        public async Task<IActionResult> EditEstadoProducto(int id, TblProductos productos)
        {
            var data = await _context.TblProductos.FirstOrDefaultAsync(x => x.ProductoId == id);

            var validar = data == null;
            if (validar)
            {
                return NotFound();
            }

            data.Estado = productos.Estado;
            data.ProductoId = id;

            await _context.SaveChangesAsync();
            return Ok(new
            {
                ProductoId = data.ProductoId,
                Descripcion = data.Descripcion,
                Producto = data.Producto
            });
        }
    }
}