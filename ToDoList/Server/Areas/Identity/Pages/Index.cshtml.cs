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
    public class IndexModel : PageModel
    {
        private readonly ToDoList.Server.Data.ApplicationDbContext _context;

        public IndexModel(ToDoList.Server.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Note> Note { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Notes != null)
            {
                Note = await _context.Notes.ToListAsync();
            }
        }
    }
}
