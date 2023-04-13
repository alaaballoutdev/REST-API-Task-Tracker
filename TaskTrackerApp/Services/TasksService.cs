
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskTrackerApp.Model;

namespace TaskTrackerApp.Services
{
    public class TasksService
    {
        private readonly TaskDb _context;
        
        
        public TasksService(TaskDb context) {
            _context = context;
        }

        public async Task<IEnumerable<TaskModel>> GetTasks(bool?sorted , bool?ascending,string ?search)
        {
            if (sorted != null && sorted == true)
            {
                if (ascending != null && ascending == false)
                {
                    return await GetTasksDescending(search);

                }
                return await GetTasksAscending(search);
            }
            return await _context.Tasks.Select(task =>
                new TaskModel
                {
                    TaskModelId = task.TaskModelId,
                    TaskName = task.TaskName,
                    TaskDate = task.TaskDate
                })
                .Where(task => search != null ? task.TaskName.ToLower().Contains(search.ToLower()) : true)
                .ToListAsync();

        }

        private async Task<IEnumerable<TaskModel>> GetTasksAscending(string?search)
        {
            return await _context.Tasks.Select(task =>
           new TaskModel
           {
               TaskModelId = task.TaskModelId,
               TaskName = task.TaskName,
               TaskDate = task.TaskDate
           })
           .OrderBy(task => task.TaskDate)
           .Where(task => search != null ? task.TaskName.ToLower().Contains(search.ToLower()) : true)
           .ToListAsync();

        }

        private  async Task<IEnumerable<TaskModel>> GetTasksDescending(string ?name)
        {
            return await _context.Tasks.Select(task =>
              new TaskModel
              {
                  TaskModelId = task.TaskModelId,
                  TaskName = task.TaskName,
                  TaskDate = task.TaskDate
              })
              .Where(task => name != null ? task.TaskName.ToLower().Contains(name.ToLower()) : true)
              .OrderByDescending(task=>task.TaskDate)
              .ToListAsync();

        }




        public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasksToday(bool? sorted, bool? ascending)
        {

            if (sorted != null && sorted == true)
            {
                if (ascending != null && ascending == false)
                {
                    return await GetTasksTodayDescending();
                }
                else
                {
                    return await GetTasksTodayAscending();
                }

            }

            return await _context.Tasks
               .Where(task => task.TaskDate.Date == DateTime.Today)
               .Select(task =>
                   new TaskModel
                   {
                       TaskModelId = task.TaskModelId,
                       TaskName = task.TaskName,
                       TaskDate = task.TaskDate
                   }).ToListAsync();


        }

        private async Task<ActionResult<IEnumerable<TaskModel>>> GetTasksTodayAscending()
        {
            return await _context.Tasks
             .Where(task => task.TaskDate.Date == DateTime.Today)
             .Select(task =>
                 new TaskModel
                 {
                     TaskModelId = task.TaskModelId,
                     TaskName = task.TaskName,
                     TaskDate = task.TaskDate
                 })
             .OrderBy(task=>task.TaskDate)
             .ToListAsync();
        }

        private async Task<ActionResult<IEnumerable<TaskModel>>> GetTasksTodayDescending()
        {
            return await _context.Tasks
             .Where(task => task.TaskDate.Date == DateTime.Today)
             .Select(task =>
                 new TaskModel
                 {
                     TaskModelId = task.TaskModelId,
                     TaskName = task.TaskName,
                     TaskDate = task.TaskDate
                 })
             .OrderByDescending(task => task.TaskDate)
             .ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasksInRange(DateTime from , DateTime to ,bool? sorted, bool? ascending)
        {

            if (sorted != null && sorted == true)
            {
                if (ascending != null && ascending == false)
                {
                    return await GetTasksInRangeDescending(from,to);
                }
                else
                {
                    return await GetTasksInRangeAscending(from,to);
                }

            }

            return await _context.Tasks
               .Where(task => task.TaskDate>=from && task.TaskDate <= to)
               .Select(task =>
                   new TaskModel
                   {
                       TaskModelId = task.TaskModelId,
                       TaskName = task.TaskName,
                       TaskDate = task.TaskDate
                   })
               .ToListAsync();


        }

        private async Task<ActionResult<IEnumerable<TaskModel>>> GetTasksInRangeAscending(DateTime from, DateTime to)
        {
            return await _context.Tasks
             .Where(task => task.TaskDate >= from && task.TaskDate <= to)
             .Select(task =>
                 new TaskModel
                 {
                     TaskModelId = task.TaskModelId,
                     TaskName = task.TaskName,
                     TaskDate = task.TaskDate
                 })
             .OrderBy(task=>task.TaskDate)
             .ToListAsync();
        }

        private async Task<ActionResult<IEnumerable<TaskModel>>> GetTasksInRangeDescending(DateTime from, DateTime to)
        {
            return await _context.Tasks
            .Where(task => task.TaskDate >= from && task.TaskDate <= to)
            .Select(task =>
                new TaskModel
                {
                    TaskModelId = task.TaskModelId,
                    TaskName = task.TaskName,
                    TaskDate = task.TaskDate
                })
            .OrderByDescending(task => task.TaskDate)
            .ToListAsync();

        }


    }
}
