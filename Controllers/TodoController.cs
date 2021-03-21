using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using aks_12_factors_microservice.Model;
using aks_12_factors_microservice.Repository;

namespace aks_12_factors_microservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> _logger;
		private readonly TodoContext _context;

        public TodoController(ILogger<TodoController> logger, TodoContext ctx)
        {
            _logger = logger;
			this._context = ctx;
        }

        [HttpGet]
        public IEnumerable<Todo> Get()
        {
            _logger.LogInformation("Getting TODOs");
			return this._context.Todos.OrderBy( todo => todo.Id).AsEnumerable();
        }

		[HttpPost]  
		public Todo Post([FromBody] Todo todo)
        {
            _logger.LogInformation("Adding new Todo");
			this._context.Todos.Add(todo);
			this._context.SaveChanges();
			return todo;
        }

		[HttpPut]  
		[Route("{id}")]
		public Todo Put([FromRoute] int id, [FromBody] Todo todo)
        {
            _logger.LogInformation("Saving todo");
			Todo oldTodo = this._context.Todos.Find(id);
			if (oldTodo == null) {
                _logger.LogInformation("Todo not found");
				throw new Exception("Not found");
			}
			oldTodo.Title = todo.Title;
			oldTodo.Description = todo.Description;
			oldTodo.Complete = todo.Complete;

			this._context.Todos.Update(oldTodo);
			this._context.SaveChanges();
			return oldTodo;
        }
    }
}
