﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ToDoList.Server.Data;
using ToDoList.Shared;

namespace ToDoList.Server.Areas.Identity.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ToDoList.Server.Data.ApplicationDbContext _context;

        public DetailsModel(ToDoList.Server.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Note Note { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Notes == null)
            {
                return NotFound();
            }

            var note = await _context.Notes.FirstOrDefaultAsync(m => m.Id == id);
            if (note == null)
            {
                return NotFound();
            }
            else 
            {
                Note = note;
            }
            return Page();
        }
    }
}
