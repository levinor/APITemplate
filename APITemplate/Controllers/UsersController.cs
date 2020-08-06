using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Levinor.APITemplate.Models.User;
using Levinor.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Levinor.APITemplate.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            IUserService service,
             IMapper mapper,
             ILogger<UsersController> logger
            )
        {
            _mapper = mapper;
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Get list of all users
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("{token}/getallusers")]
        public async Task<IActionResult> GetAllUsers(string token)
        {
            _logger.LogDebug($"{HttpContext.TraceIdentifier}: GetAllUsers called");
            _service.CheckToken(token);
            return Ok(_service.GetAllUsers().Select(x => _mapper.Map<UserModel>(x)));
        }



        /// <summary>                  
        /// Get a single user from his ID
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("{token}/getuser/{Id}")]
        public async Task<IActionResult> GetUserById(string token, int Id)
        {
            _logger.LogDebug($"{HttpContext.TraceIdentifier}: GetUserById called with Id: {Id}");
            _service.CheckToken(token);
            return Ok(_mapper.Map<UserModel>(_service.GetUserById(Id)));
        }


        /// <summary>
        /// Tries to Login and returns a token needed for other operations
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("getlogintoken")]
        public async Task<IActionResult> GetLoginToken([FromBody] GetLoginTokenModel model)
        {
            _logger.LogDebug($"{HttpContext.TraceIdentifier}: GetLoginToken called for user: {model.email}");
            return Ok(_service.GetLoginToken(model.email, model.password));
        }

        /// <summary>
        /// Changes thepassword for the user
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("{token}/setnewpassword")]
        public async Task<IActionResult> SetNewPassword(string token, [FromBody] ChangePasswordRequestModel model)
        {
            _logger.LogDebug($"{HttpContext.TraceIdentifier}: ChangePassword called for user: {model.email}");
            _service.CheckToken(token);
            _service.SetNewPassword(token, model.email, model.currentPassword, model.newPassword);
            return Ok();
        }

    }
}
