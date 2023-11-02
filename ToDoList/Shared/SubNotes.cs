using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Shared
{
    public class SubNotes
    {
        public int ID { get; set; }
        public String Title { get; set; }
        public Boolean Status { get; set; }

        public int NoteId { get;set; }
        public Note Note { get; set; }
    }
}
