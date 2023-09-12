using Microsoft.AspNetCore.Identity;
using ToDoList.Shared;

namespace ToDoList.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Superhero>SuperHeroes{ get; set; }=new List<Superhero>();
        public List<Note> Notes{ get; set; } = new List<Note>();
    }
}