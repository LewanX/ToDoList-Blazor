using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Shared
{
    public class Tags
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
