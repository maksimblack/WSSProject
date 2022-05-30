using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSSDepartments.Model;

namespace WSSDepartments.DatabaseLayer
{
	public class DatabaseContext: DbContext
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
		{
			Database.EnsureCreated();
		}

		public DbSet<Company> Companies { get; set; }
	}
}
