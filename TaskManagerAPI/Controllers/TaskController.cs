using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private static readonly List<TaskModel> Tasks = new List<TaskModel>();

    [HttpGet]
    public IActionResult GetTasks() => Ok(Tasks);

    [HttpPost]
    public IActionResult AddTask(TaskModel task)
    {
        task.Id = Tasks.Count + 1;
        Tasks.Add(task);
        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
    }

    [HttpGet("{id}")]
    public IActionResult GetTask(int id)
    {
        var task = Tasks.FirstOrDefault(t => t.Id == id);
        return task != null ? Ok(task) : NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTask(int id, TaskModel updatedTask)
    {
        var task = Tasks.FirstOrDefault(t => t.Id == id);
        if (task == null) return NotFound();

        task.Name = updatedTask.Name;
        task.IsComplete = updatedTask.IsComplete;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTask(int id)
    {
        var task = Tasks.FirstOrDefault(t => t.Id == id);
        if (task == null) return NotFound();

        Tasks.Remove(task);
        return NoContent();
    }
}
