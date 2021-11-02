using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<User>> ValidateUser([FromQuery] string username, [FromQuery] string password)
        {
            Console.WriteLine("Here");
            try
            {
                var user = await userService.ValidateUser(username, password);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> Register([FromBody] User user)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                User added = await userService.RegisterUser(user);
                return Created($"{added.Username}", added);
            }
            catch (Exception e)
            {
                
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
       
        
    }
}