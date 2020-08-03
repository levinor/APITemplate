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
        [HttpGet, Route("getallusers")]
        public async Task<IActionResult> GetAllUsers()
        {
            _logger.LogDebug($"{HttpContext.TraceIdentifier}: GetAllUsers called");
            return Ok(_service.GetAllUsers().Select(x => _mapper.Map<UserModel>(x)));
        }



        /// <summary>                  
        /// Get a single user from his ID
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("getuser/{Id}")]
        public async Task<IActionResult> GetUserById(int Id)
        {
            _logger.LogDebug($"{HttpContext.TraceIdentifier}: GetUserById called with Id: {Id}");
            return Ok(_mapper.Map<UserModel>(_service.GetUserById(Id)));
        }
        
    }
}
