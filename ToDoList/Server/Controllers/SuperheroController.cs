using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToDoList.Client;
using ToDoList.Server.Data;
using ToDoList.Server.Models;
using ToDoList.Shared;

namespace ToDoList.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SuperheroController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> _userManager;
        public SuperheroController(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<ActionResult<List<Superhero>>> GetSuperHeroes()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            //var result = await context.Users
            //    .Include(u=>u.SuperHeroes)
            //    .FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (user == null)
                    return NotFound();   
                return Ok(user.SuperHeroes);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSuperhero(int id)
        {
            var superhero = await context.SuperHeroes.FindAsync(id);

            if (superhero == null)
            {
                return NotFound();
            }

            context.SuperHeroes.Remove(superhero);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
