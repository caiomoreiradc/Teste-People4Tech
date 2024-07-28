using Task = controle_tasks.Models.Task;
using controle_tasks.Repositories;


//Príncipo Aberto/Fechado do SOLID, o taskservice pode ser estendido e suportar mais funcionalidades
//Princípio da inversão de dependência pois depende da abstração de  ITaskRepository 
public class TaskService
{
    private readonly ITaskRepository _taskRepository; //instancia interface

    public TaskService(ITaskRepository taskRepository) //DI
    {
        _taskRepository = taskRepository;
    }

    public void AddTask(Task task) //CREATE
    {
        _taskRepository.CriarTask(task);
    }
    public void UpdateTask(Task task) //UPDATE
    {
        _taskRepository.EditarTask(task);
    }
    public void RemoveTask(int id) //DELETE
    {
        _taskRepository.ExcluirTask(id);
    }
    public Task GetTaskById(int id) //GET BY ID
    {
        return _taskRepository.GetById(id);
    }
    public IEnumerable<Task> GetAllTasks()
    {
        return _taskRepository.GetAll();
    }
}
