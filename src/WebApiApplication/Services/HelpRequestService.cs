using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiApplication.Context;
using WebApiCore.Models;

namespace WebApiApplication.Services
{
    public class HelpRequestService
    {
        private readonly IWebApiDbContext _context;

        public HelpRequestService(IWebApiDbContext context)
        {
            _context = context;
        }

    }
}