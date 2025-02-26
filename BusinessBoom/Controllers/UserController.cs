using BusinessBoom.DTO;
using BusinessBoom.Exceptions;
using BusinessBoom.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusinessBoom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserService _userService;

        public UserController(UserService service)
        {
            _userService = service;
        }

        /// <summary>
        /// Создает нового пользователя.
        /// </summary>
        /// <param name="email">Электронная почта нового пользователя.</param>
        /// <returns>Созданный пользователь.</returns>
        /// <response code="201">Возвращает созданного пользователя.</response>
        /// <response code="400">Возвращает ошибку, если электронная почта введена некорректно.</response>
        [HttpPost("/users")]
        public IActionResult AddUser([FromBody] string email)
        {
            try
            {
                var result = _userService.Create(email);
                return CreatedAtAction(nameof(AddUser), new { id = result.Id }, result);
            }
            catch (EmailInvalidException ex)
            {
                Console.WriteLine(ex);
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Получает баланс пользователя по его идентификатору.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Баланс пользователя.</returns>
        /// <response code="200">Возвращает баланс пользователя.</response>
        /// <response code="404">Возвращает ошибку, если кошелек не найден.</response>
        [HttpGet("{userId}/balance")]
        public IActionResult GetBalance(Guid userId)
        {
            try
            {
                var result = _userService.GetBalance(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Делает депозит на счет пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="user">Объект с данными для депозита.</param>
        /// <returns>Обновленный баланс пользователя.</returns>
        /// <response code="200">Возвращает обновленный баланс пользователя.</response>
        /// <response code="400">Возвращает ошибку, если сумма депозита меньше нуля.</response>
        /// <response code="404">Возвращает ошибку, если кошелек не найден.</response>
        [HttpPut("{userId}/deposit")]
        public IActionResult Deposit(Guid userId, [FromBody] UserRequestDto user)
        {
            try
            {
                var result = _userService.Deposit(userId, user);
                return Ok(result);
            }
            catch (DepositZeroException ex)
            {
                Console.WriteLine(ex);
                return BadRequest(new { message = ex.Message });
            }
            catch (WalletNotFoundException ex)
            {
                Console.WriteLine(ex);
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Производит вывод средств со счета пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="user">Объект с данными для вывода.</param>
        /// <returns>Обновленный баланс пользователя.</returns>
        /// <response code="200">Возвращает обновленный баланс пользователя.</response>
        /// <response code="400">Возвращает ошибку, если сумма вывода меньше нуля.</response>
        /// <response code="404">Возвращает ошибку, если кошелек не найден.</response>
        [HttpPut("{userId}/withdraw")]
        public IActionResult Withdraw(Guid userId, [FromBody] UserRequestDto user)
        {
            try
            {
                var result = _userService.Withdraw(userId, user);
                return Ok(result);
            }
            catch (DepositZeroException ex)
            {
                Console.WriteLine(ex);
                return BadRequest(new { message = ex.Message });
            }
            catch (WalletNotFoundException ex)
            {
                Console.WriteLine(ex);
                return NotFound(new { message = ex.Message });
            }
        }
    }
}