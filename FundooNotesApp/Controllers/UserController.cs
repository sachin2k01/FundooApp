using BusinessLayer.Interface;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;

        }

        [HttpPost]
        [Route("reg")]
        public IActionResult Register(RegisterModel model)
        {
            var result = userBusiness.UserRegister(model);
            if (result != null)
            {
                return Ok(new ResponseModel<UserEntity> { Success = true, Message = "Register Successfull", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "Register Not Successfull" });
            }

        }
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            UserEntity user = userBusiness.GetUserById(id);

            if (user == null)
            {
                return NotFound(); // If user with given email is not found
            }

            return Ok(user); // Return user details if found
        }


    }
}
