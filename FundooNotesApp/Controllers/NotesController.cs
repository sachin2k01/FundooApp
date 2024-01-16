using BusinessLayer.Services;
using BusinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;

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

        public IActionResult UserNoteCreation(UserNotesModel userNotes)
        {
            var result = _userNotes.CreateUserNotes(userNotes);
            if(result != null) 
            {
                return Ok(result);

            }
            else
            {
                return BadRequest();
            }
        }
    }
}
