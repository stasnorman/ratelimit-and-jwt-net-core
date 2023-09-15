using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiMy.Models;

namespace WebApiMy.Controllers
{
    [Route("api/sign-in")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        public TokenController(IConfiguration config)
        {
            _configuration = config;
        }
        /// <summary>
        /// Генерация уникального токена пользователя
        /// </summary>
        /// <param name="_userData">Принимает логин и пароль пользователя</param>
        /// <returns>Возвращает Bearer-токен и статус</returns>
        [HttpPost]
        public async Task<IActionResult> Post(AccountGetToken _userData)
        {

            if (_userData.Password != null && _userData.Login != null)
            {
                var user = await GetUser(_userData.Login, _userData.Password);

                if (user != null)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Login", user.Login),
                        new Claim("Password", user.Password)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddDays(1),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Данные неверны. Повторите авторизацию.");
                }
            }
            else
            {
                return BadRequest();
            }       
        }

        /// <summary>
        /// Получение данных для генерации токена
        /// </summary>
        /// <param name="login">Введите логин пользователя</param>
        /// <param name="password">Введите пароль пользователя</param>
        /// <returns>Возвращает коллекцию по пользователю</returns>
        private async Task<Account> GetUser(string login, string password)
        {
            Account user = new Account
            {
                Login = login,
                Password = password
            };

            return user;
        }

    }
}
