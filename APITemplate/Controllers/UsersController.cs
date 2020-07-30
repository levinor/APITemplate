using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Levinor.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Levinor.APITemplate.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _service;

        public UsersController(
            IUserService service
            )
        {
            _service = service;
        }
        // GET: api/<UsersController>
        [HttpGet, Route("getallusers")]
        public IEnumerable<object> GetAllUsers()
        {
            return _service.GetAllUsers();
        }

        //// GET api/<UsersController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<UsersController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<UsersController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<UsersController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
