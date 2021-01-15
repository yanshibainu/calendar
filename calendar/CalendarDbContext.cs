using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using calendar.Entities;
using Microsoft.EntityFrameworkCore;

namespace calendar
{
    public class CalendarDbContext : DbContext
    {
        public CalendarDbContext(DbContextOptions<CalendarDbContext> options) : base(options)
        {
        }

        public DbSet<Workday> Workdays { get; set; }
    }
}
