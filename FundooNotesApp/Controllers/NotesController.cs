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

        [HttpPost]
        [Route("Notes")]
        [Authorize]
        public IActionResult UserNoteCreation([FromForm]UserNotesModel userNotes)
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

        [Authorize]
        [HttpGet]
        [Route("NoteById")]
        public IActionResult GetNoteById(int id)
        {
            int userId = int.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            var noteInfo=_userNotes.GetNotesById(id,userId);
            if(noteInfo != null)
            {
                return Ok(noteInfo);
            }
            else
            {
                return BadRequest("Note Not Found");
            }

        }

        [HttpGet("GetAllNodes")]
        public IActionResult GetAllNodes()
        {
            var notesInfo= _userNotes.GetAllNodes();
            if(notesInfo != null)
            {
                return Ok(notesInfo);
            }
            else
            {
                return BadRequest();
            }

        }

        [Authorize]
        [HttpGet]
        [Route("UserNotes")]
        public IActionResult GetUserNotes()
        {
            int userId = int.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            if(userId!=0)
            {
                var userNote=_userNotes.GetUserNotesById(userId);
                return Ok(userNote);
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
