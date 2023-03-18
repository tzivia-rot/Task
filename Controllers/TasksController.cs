using Microsoft.AspNetCore.Mvc;
using Tasks.Interfaces;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Tasks.model;

namespace Tasks.Controllers;
[ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
       private ITaskHttp TaskService;
        public TaskController(ITaskHttp taskService){
            this.TaskService=taskService;
        }
        [HttpGet]
        [Authorize(Policy = "User")]
        public IEnumerable<model.Task> Get()
        {

            string token=Request.Headers.Authorization;
            
            return TaskService.GetAll(token);
        }

        [HttpGet("{id}")]
          [Authorize(Policy = "User")]
        public ActionResult<model.Task> Get(int id)
        {
            var p = TaskService.Get(id);
            if (p == null)
                return NotFound();
             return p;
        }

        [HttpPost]
          [Authorize(Policy = "User")]
        public ActionResult Post(model.Task task)
        {
            TaskService.Add(task);
            return CreatedAtAction(nameof(Post), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "User")]
        public ActionResult Put(int id, model.Task task)
        {
            if (! TaskService.Update(id, task))
                return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "User")]
        public ActionResult Delete (int id)
        {
            if (! TaskService.Delete(id))
                return NotFound();
            return NoContent();            
        }

    }
