using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiMy.Models;

namespace WebApiMy.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
       
        /// <summary>
        /// Получаем информацию по одному пользователю
        /// </summary>
        /// <param name="login"></param>
        /// <returns>Возвращает массив по одному пользователю</returns>
        [HttpGet("{login}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<AccountDto>>> Get(string login) {
            await Task.Delay(100);
            try
            {
                var data = new AccountDto
                {
                    Email = "test@test.ru",
                    Login = login,
                    Name = "Поздравляю!",
                    RoleName = "У тебя всё правильно работает!"
                };


                // Возвращаем список
                return new List<AccountDto> { data }; ;
            }
            catch
            {
                throw;
            }
        } 
    }
}
