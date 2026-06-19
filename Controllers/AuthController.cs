using laTienda.DTOs.Auth;
using laTienda.Helpers;
using laTienda.Models;
using laTienda.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace laTienda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly PruebaContext _context;
        private readonly JwtService _jwtService;

        public AuthController(
            PruebaContext context,
            JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginRequestDTO request)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Usuarioroles)
                .ThenInclude(ur => ur.IdrolNavigation)
                .FirstOrDefaultAsync(
                    u => u.Email == request.Email);

            if (usuario == null)
            {
                return Unauthorized(
                    "Correo o contraseña incorrectos");
            }

            bool passwordValida =
                PasswordHelper.VerificarPassword(
                    request.Password,
                    usuario.Passwordhash,
                    usuario.Passwordsalt);

            if (!passwordValida)
            {
                return Unauthorized(
                    "Correo o contraseña incorrectos");
            }

            var roles = usuario.Usuarioroles
                .Select(r => r.IdrolNavigation.Nombrerol)
                .ToList();

            var token =
                _jwtService.GenerarToken(
                    usuario,
                    roles);

            return Ok(new LoginResponseDTO
            {
                Token = token,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Roles = roles
            });
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(
            RegisterRequestDTO request)
        {
            bool existeUsuario =
                await _context.Usuarios
                .AnyAsync(x => x.Email == request.Email);

            if (existeUsuario)
            {
                return BadRequest(
                    "El correo ya está registrado");
            }

            PasswordHelper.CrearPasswordHash(
                request.Password,
                out byte[] hash,
                out byte[] salt);

            var usuario = new Usuario
            {
                Tipodoc = request.Tipodoc,
                Nrodoc = request.Nrodoc,
                Nombre = request.Nombre,
                Email = request.Email,
                Passwordhash = hash,
                Passwordsalt = salt
            };

            _context.Usuarios.Add(usuario);

            await _context.SaveChangesAsync();

            foreach (var rolId in request.Roles)
            {
                _context.Usuarioroles.Add(
                    new Usuariorole
                    {
                        Idusuario = usuario.Idusuario,
                        Idrol = rolId
                    });
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Usuario registrado correctamente"
            });
        }
    }
}