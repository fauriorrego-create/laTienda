using AutoMapper;
using laTienda.DTOs;
using laTienda.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace laTienda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly PruebaContext _context;
        private readonly IMapper _mapper;

        public ProductoController(PruebaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // LISTAR TODOS LOS PRODUCTOS
        [HttpGet]
        public IActionResult Listar()
        {
            var productos = _context.Productos.ToList();

            var productosDTO = _mapper.Map<List<ProductoDTO>>(productos);

            return Ok(productosDTO);
        }

        // OBTENER PRODUCTO POR ID
        [HttpGet("{id}")]
        public IActionResult Obtener(int id)
        {
            var producto = _context.Productos.Find(id);

            if (producto == null)
                return NotFound("Producto no encontrado");

            var productoDTO = _mapper.Map<ProductoDTO>(producto);

            return Ok(productoDTO);
        }

        // GUARDAR PRODUCTO
        [HttpPost]
        public IActionResult Guardar([FromBody] ProductoCreateDTO dto)
        {
            var producto = _mapper.Map<Producto>(dto);

            _context.Productos.Add(producto);
            _context.SaveChanges();

            var productoDTO = _mapper.Map<ProductoDTO>(producto);

            return Ok(new
            {
                mensaje = "Producto guardado correctamente",
                producto = productoDTO
            });
        }

        // EDITAR PRODUCTO
        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] ProductoCreateDTO dto)
        {
            var producto = _context.Productos.Find(id);

            if (producto == null)
                return NotFound("Producto no encontrado");

            _mapper.Map(dto, producto);

            _context.SaveChanges();

            var productoDTO = _mapper.Map<ProductoDTO>(producto);

            return Ok(new
            {
                mensaje = "Producto actualizado correctamente",
                producto = productoDTO
            });
        }

        // ELIMINAR PRODUCTO
        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            var producto = _context.Productos.Find(id);

            if (producto == null)
                return NotFound("Producto no encontrado");

            _context.Productos.Remove(producto);
            _context.SaveChanges();

            return Ok(new
            {
                mensaje = "Producto eliminado correctamente"
            });
        }
    }
}