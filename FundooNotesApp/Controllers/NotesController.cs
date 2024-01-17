using BusinessLayer.Services;
using BusinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly IUserBusinessNotes _userNotes;
        public NotesController(IUserBusinessNotes userNotes)
        {
            this._userNotes = userNotes;
            
        }

        [Authorize]
        [HttpPost]
        [Route("Notes")]

        public IActionResult UserNoteCreation(UserNotesModel userNotes)
        {
            try
            {
                int userId = int.Parse(User.Claims.Where(x=>x.Type=="UserId").FirstOrDefault().Value);
                var result = _userNotes.CreateUserNotes(userNotes,userId);
                if (result != null)
                {
                    return Ok(new ResponseModel<UserNotesEntity> { Success = true, Message = "created Notes Successfully.", Data = result });

                }
                else
                {
                    return BadRequest(new ResponseModel<UserNotesEntity> { Success = false, Message = "failed to create Notes" });
                }
            }
            catch (Exception e)
            {

                return BadRequest(e.Message); 
            }
        }
    }
}
