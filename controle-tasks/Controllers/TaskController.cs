using Task = controle_tasks.Models.Task;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")] //roteamenteo /api/Tasks
public class TaskController : ControllerBase
{
    //adiciona o serviço task
    private readonly TaskService _taskService;

    //injeta o serviço
    public TaskController(TaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public IEnumerable<Task> Get()
    {
        return _taskService.GetAllTasks(); //buscar todas as tasks
    }

    [HttpGet("{id}")]
    public ActionResult<Task> Get(int id)
    {
        var task = _taskService.GetTaskById(id); //buscar todas as tasks por id

        //verifica se o id da task é null
        if (task is null)
            return NotFound();
        
        return task;
    }

    [HttpPost]
    public IActionResult Post([FromBody] Task task)
    {
        _taskService.AddTask(task); //adicionar task
        return CreatedAtAction(nameof(Get), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Task task) //editar tasks
    {
        var taskEditar = _taskService.GetTaskById(id); //busca pelo id

        //verifica se o id é null
        if (taskEditar is null)
            return NotFound();
        
        task.Id = id;
        _taskService.UpdateTask(task);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var taskExcluir = _taskService.GetTaskById(id); //busca pelo id

        //verifica se o id é null
        if (taskExcluir is null)
            return NotFound();

        _taskService.RemoveTask(id);
        return NoContent();
    }
}
