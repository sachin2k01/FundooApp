using BusinessLayer.Interface;
using BusinessLayer.Services;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
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

        private readonly IBus bus;

        public UserController(IUserBusiness userBusiness,IBus bus)
        {
            this.userBusiness = userBusiness;
            this.bus = bus;

        }

        [HttpPost]
        [Route("register")]
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

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginModel login) 
        {
            var logResult = userBusiness.UserLogin(login);
            if(logResult != null)
            {
                return Ok(new ResponseModel<string> { Success=true,Message="Login Successfull",Data=logResult});
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Login Not Successfull" });
            }
            
        }

        [HttpPost]
        [Route("forgot password")]

        public IActionResult ForgotPassword(string emailTo)
        {
            try
            {
                var result = userBusiness.ForgotPassword(emailTo,bus);
                if(result != null)
                {
                    return Ok(new ResponseModel<string> { Success = true, Message = "suceesfull" });


                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "not suceesfull" });
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Products")]

        public IActionResult AddProduct(ProductModel product)
        {
            var result = userBusiness.AddProduct(product);
            if(result!=null)
            {
                return Ok(new ResponseModel<ProductEntity> { Success = true, Message = "Successfully add the products to table", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<ProductEntity> { Success = false, Message = "Can't able to add products"});
            }
        }

        [HttpGet("Byid")]
        public IActionResult GetUserDetail(int id)
        {
            try
            {
                UserEntity userDetais = userBusiness.GetUsersById(id);
                if (userDetais != null)
                {
                    return Ok(new ResponseModel<UserEntity> { Success = true, Message = "Successfully Fetch User Details", Data = userDetais });
                }
                else
                {
                    return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "Not Successfully" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}
