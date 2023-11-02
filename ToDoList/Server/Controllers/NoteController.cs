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

        public async Task<ActionResult<List<Note>>> GetNotes()
        {
            var result = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (result == null)
                return NotFound();

            var userNotes = result.Notes;

            foreach (var note in userNotes)
            {
                // Carga explícita de las SubNotes para cada Note.
                context.Entry(note).Collection(n => n.subNotes).Load();
            }

            return Ok(userNotes);
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

            // Obtiene las SubNotes de la base de datos.
            var subNotesToDelete = context.SubNotes.Where(sn => sn.NoteId == noteToDelete.Id);
            noteToDelete.subNotes.Clear();
            context.Notes.Remove(noteToDelete);
            await context.SaveChangesAsync();

            // Verifica si todas las SubNotes se eliminaron correctamente.
            var subNotesCount = await context.SubNotes.CountAsync(sn => sn.ID == id);
            if (subNotesCount == 0)
            {
                await Console.Out.WriteLineAsync("hi");
            }

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
        [HttpPost]
        [Route("CreateSubNote/{id}")]
        public async Task<ActionResult<List<Note>>> CreateSubNote(int id, [FromBody]  string description )       
        {
            SubNotes subnote=new SubNotes();
            var result = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (result == null)
                return NotFound();
            var subnoteToCreate = result.Notes.FirstOrDefault(n => n.Id == id);
            subnote.Title = description;
            subnote.Status = false;
            subnoteToCreate.subNotes.Add(subnote);
            if (subnoteToCreate==null)
            {
                return NotFound("Note not found");
            }
            //subnoteToCreate.subNotes.Add(notes);
            await context.SaveChangesAsync();

            return Ok();

        }
        
        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<ActionResult<Note>> EditNote(int id, [FromBody] Note updatedNote)
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
            //if (noteToEdit.Status == Status.Complete)
            //{
            //    noteToEdit.Status = Status.Pending;
            //}
            //else
            //{
            //    noteToEdit.Status = Status.Complete;
            //}
            
            noteToEdit.Description = updatedNote.Description;
            noteToEdit.Status=updatedNote.Status;
            noteToEdit.Favorite=updatedNote.Favorite;
            noteToEdit.Title = updatedNote.Title;
            noteToEdit.UpdatedAt = DateTime.Now;
            context.Update(noteToEdit);
            // Guarda los cambios en la base de datos
            await context.SaveChangesAsync();

            return Ok(noteToEdit);
        }


    }
}
