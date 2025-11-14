using System;
using AnimesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimesAPI.Data
{
	public class AppDbContext: DbContext
	{
        

        public DbSet<Animes> Animes { set; get; }
		public AppDbContext( DbContextOptions<AppDbContext> options) :base(options)
		{
		}
	}
}

