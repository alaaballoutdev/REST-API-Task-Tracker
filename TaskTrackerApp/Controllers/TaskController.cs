using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using TaskTrackerApp.Model;
using TaskTrackerApp.Services;

namespace TaskTrackerApp.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class TaskController : Controller
    {

        private readonly TaskService  _service;


        
        public TaskController(TaskService service) { 
            _service = service;
        }

        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaskModel>> Get(int id) {
            var task =  await _service.GetTaskById(id);
            if(task == null) {
                return StatusCode(StatusCodes.Status404NotFound);
               
            
            }
            return task;
           
            
        }
            
        
        [HttpPost]
        public async Task<ActionResult> AddTask(TaskModel task)
        {
            try
            {
                if (task == null)
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable);

                }
                await _service.AddTask(task);
                 return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex) { 
                return StatusCode(StatusCodes.Status500InternalServerError,
              "Internal Server Error");
            } 
           
            


        }

        
        
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteTask(int id) 
        {
            try
            {
                bool success = await _service.DeleteTask(id);
                if (success)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                else
                {

                    return StatusCode(StatusCodes.Status400BadRequest,$"Task with id ={id} doesn't exist");
                }
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError,
              ex);


            }
        
        
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateTask(int id,TaskModel task)
        {
            bool success = await _service.UpdateTask(id, task);

            if (success)
            {
                return StatusCode(StatusCodes.Status202Accepted);
            }

            return StatusCode(StatusCodes.Status400BadRequest);
            
           


        }






    }
}
