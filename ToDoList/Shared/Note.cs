using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Shared
{
    public class Note
    {
    
        public int Id { get; set; }
        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(50, ErrorMessage = "El título no debe exceder los 50 caracteres.")]
        public string Title { get; set; } = string.Empty;
        [StringLength(500, ErrorMessage = "La descripción no debe exceder los 500 caracteres.")]
        [Required(ErrorMessage = "Se requiere una descripción.")]
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }=DateTime.Now;
        public DateTime UpdatedAt { get; set; }= DateTime.Now;
        public Status Status { get; set; }
       public bool Favorite { get; set; }

        public virtual ICollection<SubNotes> subNotes { get; set; } = new List<SubNotes>();
        public virtual ICollection<Tags> Tags { get; set; }=new List<Tags>();

    }
}
