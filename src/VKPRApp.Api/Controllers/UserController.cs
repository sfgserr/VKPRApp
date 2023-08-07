using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VKPRApp.Shared.Services;

namespace VKPRApp.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly string _accessToken;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _accessToken = configuration["AccessToken"];
        }

        [Route("getUserById")]
        [HttpGet]
        public async Task<ActionResult<Shared.Models.User>> GetUserById(string userId, string accessToken)
        {
            if (_accessToken != accessToken)
                return Unauthorized();

            Shared.Models.User? user = await _userService.GetUserByUserId(userId);

            return user is null ? NotFound() : user;
        }

        [Route("addUser")]
        [HttpPost]
        public async Task<ActionResult> AddUser(string jsonUser, string accessToken)
        {
            if (_accessToken != accessToken)
                return Unauthorized();

            Shared.Models.User? user = JsonConvert.DeserializeObject<Shared.Models.User>(jsonUser);

            if (user is null)
                return BadRequest();

            await _userService.AddUser(user);
            return Ok();
        }

        [Route("updateUser")]
        [HttpPut]
        public async Task<ActionResult<Shared.Models.User>> UpdateUser(string jsonUser, string accessToken)
        {
            if (_accessToken != accessToken)
                return Unauthorized();

            Shared.Models.User? user = JsonConvert.DeserializeObject<Shared.Models.User>(jsonUser);

            if (user is null)
                return BadRequest();

            return await _userService.UpdateUser(user);
        }

        [Route("updateAutoRenewal")]
        [HttpPut]
        public async Task<ActionResult<Shared.Models.User>> UpdateAutoRenewal(string userId, bool value, string accessToken)
        {
            if (_accessToken != accessToken)
                return Unauthorized();

            return await _userService.UpdateAutoRenewal(userId, value);
        }

        [Route("getUsers")]
        [HttpGet]
        public async Task<ActionResult> GetUsers(string accessToken)
        {
            if (_accessToken != accessToken)
                return Unauthorized();

            List<Shared.Models.User> users = await _userService.GetUsers();
            return Ok(users);
        }

        [Route("completeTask")]
        [HttpPut]
        public async Task<ActionResult<Shared.Models.User>> CompleteTask(Shared.Models.TaskType taskType, string userId, string accessToken)
        {
            if (_accessToken != accessToken)
                return Unauthorized();

            return await _userService.CompleteTask(taskType, userId);
        }

        [Route("setBankCardToNull")]
        [HttpPut]
        public async Task<ActionResult<Shared.Models.User>> SetBankCardToNull(string userId, string accessToken)
        {
            if (_accessToken != accessToken)
                return Unauthorized();

            return await _userService.SetBankCardToNull(userId);
        }
    }
}
