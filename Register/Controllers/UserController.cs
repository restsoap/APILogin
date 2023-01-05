using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Register.Data;
using Register.Models;

namespace Register.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IConfiguration _configu;
        public readonly DataContext _context;

        public UserController(IConfiguration config, DataContext context)
        {
            _configu = config;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("CreateUser")]
        public IActionResult Create(Usuario usuario)
        {
            if(_context.Users.Where(u => u.Email == usuario.Email).FirstOrDefault() != null)
            {
                return Ok("Already Exist");
            }
            usuario.MemberSince= DateTime.Now;
            _context.Users.Add(usuario);
            _context.SaveChanges();
            return Ok("Success");
        }

        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public IActionResult Login(Login usuario)
        {
            var userAvaliable = _context.Users.Where(u => u.Email == usuario.Email && u.Pwd == usuario.Pwd).FirstOrDefault();
            if (userAvaliable != null)
            {
                return Ok(new JwtService(_configu).GenerateToken(
                    userAvaliable.Id.ToString(),
                    userAvaliable.FirstName,
                    userAvaliable.LastName,
                    userAvaliable.Email,
                    userAvaliable.Mobile,
                    userAvaliable.Gender
                    )
                );
            }
            return Ok("Failure");
        }



    }
}
