using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Levinor.APITemplate.Models.User;
using Levinor.Business.Domain;
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
        [HttpGet, Route("{token}/get")]
        public async Task<IActionResult> GetAllUsers(string token)
        {
            _logger.LogDebug($"{HttpContext.TraceIdentifier}: GetAllUsers called");
            _service.CheckToken(Guid.Parse(token));
            return Ok(_service.GetAllUsers().Select(x => _mapper.Map<GetUserResponseModel>(x)));
        }



        /// <summary>                  
        /// Get a single user from his ID
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("{token}/get/{Id}")]
        public async Task<IActionResult> GetUserById(string token, int Id)
        {
            _logger.LogDebug($"{HttpContext.TraceIdentifier}: GetUserById called with Id: {Id}");
            _service.CheckToken(Guid.Parse(token));
            return Ok(_mapper.Map<GetUserResponseModel>(_service.GetUserById(Id)));
        }


        /// <summary>
        /// Tries to Login and returns a token needed for other operations
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("getlogintoken")]
        public async Task<IActionResult> GetLoginToken([FromBody] GetLoginTokenModel request)
        {
            _logger.LogDebug($"{HttpContext.TraceIdentifier}: GetLoginToken called for user: {request.Email}");
            return Ok(_service.GetLoginToken(_mapper.Map<User>(request), _mapper.Map<Password>(request)));
        }

        /// <summary>
        /// Changes thepassword for the user
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("{token}/setnewpassword")]
        public async Task<IActionResult> SetNewPassword(string token, [FromBody] ChangePasswordRequestModel request)
        {
            _logger.LogDebug($"{HttpContext.TraceIdentifier}: ChangePassword called for user: {request.Email}");
            _service.CheckToken(Guid.Parse(token));
            _service.SetNewPassword(Guid.Parse(token), _mapper.Map<User>(request), _mapper.Map<Password>(request));
            return Ok();
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("{token}/new")]
        public async Task<IActionResult> SetNewUser(string token, [FromBody] SetNewUserRequestModel request)
        {
            _logger.LogDebug($"{HttpContext.TraceIdentifier}: SetNewUser called for user: {request.Email}");
            _service.CheckToken(Guid.Parse(token));
            _service.SetNewUser(Guid.Parse(token), _mapper.Map<User>(request), _mapper.Map<Password>(request), _mapper.Map<Role>(request));
            return Ok();
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("{token}/delete/{email}")]
        public async Task<IActionResult> DeleteUser(string token, string email)
        {
            _logger.LogDebug($"{HttpContext.TraceIdentifier}: DeleteUser called for user: {email}");
            _service.CheckToken(Guid.Parse(token));
            _service.DeleteUser(Guid.Parse(token), email);
            return Ok();
        }

    }
}
