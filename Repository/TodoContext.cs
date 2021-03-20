using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using aks_12_factors_microservice.Model;


namespace aks_12_factors_microservice.Repository
{
    public class TodoContext: DbContext
    {

		public TodoContext(DbContextOptions<TodoContext> options) : base(options) {
		}
		public DbSet<Todo> Todos {get; set;}
    }
}