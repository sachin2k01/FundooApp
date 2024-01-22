using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using RepositoryLayer.Entity;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColloboratorController : ControllerBase
    {
        private readonly IColloboratorBusiness colloborator;
        public ColloboratorController(IColloboratorBusiness colloborator)
        {
            this.colloborator = colloborator;
           
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateColloborator(int noteId,string collob_Email)
        {
            int userId = int.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            if(userId != 0) 
            {
                var collob=colloborator.AddColloborator(noteId, collob_Email,userId);
                if(collob!=null)
                {
                    return Ok(new ResponseModel<ColloboratorEntity> { Success = true, Message = "Register Successfull", Data = collob });
                }
                else
                {
                return BadRequest(new ResponseModel<string> { Success = true, Message = "Register Successfull" });
                }
            }
            else
            {
                return BadRequest("invalid user");
            }

        }


        [HttpGet]
        [Authorize]
        public IActionResult GetColloboratorById(int colloborator_id)
        {
            int userId = int.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            if(userId != 0)
            {
                var user=colloborator.GetColloboratorById(colloborator_id);
                if(user!=null)
                {
                    return Ok(user);
                }
                else
                {
                    return BadRequest("Colloborator Not Found");
                }
                
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpDelete]
        [Authorize]

        public IActionResult DeleteColloborator(string  colloborator_Email) 
        {
            int userId = int.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            if(userId!=0)
            {
                var del_collob=colloborator.DeleteColloborator(colloborator_Email);
                if(del_collob!=null)
                {
                    return Ok(new ResponseModel<string> { Success = true, Data = del_collob });
                }
                else
                {
                    return BadRequest("Not Deleted");
                }

            }
            else
            {
                return BadRequest();
            }


        }
    }
}
