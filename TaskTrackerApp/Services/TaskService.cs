using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTrackerApp.Model;

namespace TaskTrackerApp.Services
{
    public class TaskService
    {
        private readonly TaskDb _context;

        public TaskService(TaskDb context)
        {
            _context = context;
        }

        public async Task<TaskModel> GetTaskById(int id)
        {

            return await _context.Tasks.FirstOrDefaultAsync(task => task.TaskModelId == id);
        }


        public async Task<ActionResult<Int32>> AddTask(TaskModel task)
        {
            _context.Add(task);
            await _context.SaveChangesAsync();
            return task.TaskModelId;
        }

        public async Task<bool> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return false;

            }
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<bool> UpdateTask(int id, TaskModel task)
        {

            var taskToUpdate = await _context.Tasks.FindAsync(id);

            if (task == null || taskToUpdate == null)
            {
                return false;
            }

            taskToUpdate.TaskName = task.TaskName;
            taskToUpdate.TaskDate = task.TaskDate;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
