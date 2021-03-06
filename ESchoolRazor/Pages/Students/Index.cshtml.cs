﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ESchool.Models;
using ESchoolRazor.Data;

namespace ESchoolRazor.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly ESchoolRazor.Data.ApplicationDbContext _context;

        public IndexModel(ESchoolRazor.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; }
        
        [BindProperty(SupportsGet =true)]
        public string NameSearch { get; set; }
        [BindProperty(SupportsGet = true)]
        public string PhoneSearch { get; set; }
        [BindProperty(SupportsGet = true)]
        public string EmailSearch { get; set; }

        public string FilterString => $"Name={NameSearch}; Phone={PhoneSearch}; Email={EmailSearch}";
        public bool IsFiltered => !string.IsNullOrWhiteSpace(NameSearch) || !string.IsNullOrWhiteSpace(PhoneSearch) || !string.IsNullOrWhiteSpace(EmailSearch);
        public async Task OnGetAsync()
        {
            var sl = _context.Students.AsQueryable();
            if (!string.IsNullOrWhiteSpace(NameSearch))
                sl = sl.Where(f => f.Name.Contains(NameSearch));

            if (!string.IsNullOrWhiteSpace(PhoneSearch))
                sl = sl.Where(f => f.PhoneNumber.Contains(PhoneSearch));
            
            if (!string.IsNullOrWhiteSpace(EmailSearch))
                sl = sl.Where(f => f.Email.Contains(EmailSearch));

            Student = await sl.ToListAsync();
        }

    }
}
