using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToDoList.Server.Data;
using ToDoList.Server.Data.Migrations;
using ToDoList.Server.Models;
using ToDoList.Shared;

namespace ToDoList.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> _userManager;
        public NoteController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<Note>>> GetNotes()
        {
            var result= await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (result == null)
                return NotFound();
            return Ok(result.Notes);
        }
        [HttpPost]
        public async Task<ActionResult<List<Note>>> PostNote(Note note)
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user == null)
            {
                return NotFound("User not found");
            }
            user.Notes.Add(note);
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok(user.Notes); 
            }
            else
            {
                return BadRequest("Failed to add the note");
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<ActionResult<List<Note>>> DeleteNote(int id)
        {
            var result = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (result == null)
            {
                return NotFound("User not found");
            }
            var noteToDelete = result.Notes.FirstOrDefault(n => n.Id == id);

            if (noteToDelete == null)
            {
                return NotFound("Note not found");
            }
            context.Notes.Remove(noteToDelete);
            await context.SaveChangesAsync();
            //var updateResult = await _userManager.UpdateAsync(result);

            return NoContent();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<List<Note>>> CreateNote(Note notes)
        {
            var result = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (result == null)
                return NotFound();
            result.Notes.Add(notes);
            await context.SaveChangesAsync();
      
            return Ok();
          
        }

        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<ActionResult<Note>> EditNote(int id, Note updatedNote)
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user == null)
            {
                return NotFound("User not found");
            }

            var noteToEdit = user.Notes.FirstOrDefault(n => n.Id == id);

            if (noteToEdit == null)
            {
                return NotFound("Note not found");
            }
            if (noteToEdit.Status == Status.Complete)
            {
                noteToEdit.Status = Status.Pending;
            }
            else
            {
                noteToEdit.Status = Status.Complete;
            }

            context.Update(noteToEdit);
            // Guarda los cambios en la base de datos
            await context.SaveChangesAsync();

            return Ok(noteToEdit);
        }


    }
}
