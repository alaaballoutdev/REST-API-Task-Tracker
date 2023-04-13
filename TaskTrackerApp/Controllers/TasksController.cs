using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using TaskTrackerApp.Model;
using TaskTrackerApp.Services;

namespace TaskTrackerApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private TasksService _service;


        public TasksController(TasksService service)
        {
            _service = service;
        }

        
        [HttpGet]
        public async Task<IEnumerable<TaskModel>> GetTasks(bool? sorted, bool? ascending,string ?search)
        {
            
              return await _service.GetTasks(sorted,ascending,search);
        }
      
        [HttpGet("today")]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasksToday(bool? sorted, bool? ascending)
        {
            
            return await _service.GetTasksToday( sorted, ascending);
        }

        [HttpGet("range")]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasksInRange(DateTime from, DateTime to, bool? sorted, bool? ascending)
        {
            return await _service.GetTasksInRange(from, to,sorted, ascending);            
            
        }
        












    }
}