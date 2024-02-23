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
        private readonly ILogger<NotesController> _logger;
        public NotesController(IUserBusinessNotes userNotes, ILogger<NotesController> logger)
        {
            this._userNotes = userNotes;
            _logger = logger;
        }

        [HttpPost]
        [Route("Notes")]
        [Authorize]
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

        [Authorize]
        [HttpGet]
        [Route("NoteById")]
        public IActionResult GetNoteById(int id)
        {
            //_logger.LogInformation("Entered into GetNodeBy Id Controller");
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

        [Authorize]
        [HttpPut]
        [Route("UpdateNote")]
        public IActionResult UpdateUsersNote(int noteId, NotesUpdateModel noteModel)
        {
            int userId = int.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            if(userId!=0)
            {
                var updateNote=_userNotes.UpdateNotes(noteId, userId,noteModel);
                if(updateNote!=null)
                {
                    return Ok(updateNote);

                }
                else
                {
                    return NotFound();
                }
                
            }
            else
            {
                return BadRequest("invalid note data");

            }
        }

        [HttpDelete]
        [Route("NodeDelete")]
        [Authorize]

        public IActionResult DeleteUserNode(int noteId)
        {
            int userId = int.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            if(userId!=0)
            {
                var delNode = _userNotes.DeleteNode(noteId, userId);
                if(delNode!=null)
                {
                    return Ok(delNode);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut]
        [Route("archieveNote")]
        [Authorize]
        public IActionResult ArchiveNote(int noteId)
        {
            int userId = int.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            if(userId!=0)
            {
                var notes = _userNotes.ArchieveNotes(userId, noteId);
                if(notes!=null)
                {
                    return Ok(notes);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }

        }


        [HttpPut]
        [Route("trashNote")]
        [Authorize]
        public IActionResult TrashNote(int noteId)
        {
            int userId = int.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            if (userId != 0)
            {
                var notes = _userNotes.TrashNotes(userId, noteId);
                if (notes != null)
                {
                    return Ok(notes);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut]
        [Route("addColorToNote")]
        [Authorize]
        public IActionResult AddColor(int noteId, string color )
        {
            int userId = int.Parse(User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value);
            if (userId != 0)
            {
                var notes = _userNotes.AddNoteColor(userId, noteId, color);
                if (notes != null)
                {
                    return Ok(notes);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }

        }

    }
}
