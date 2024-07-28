using Task = controle_tasks.Models.Task;
using controle_tasks.Repositories;

public class TaskSeed
{
    public static void InicializarTasks(IServiceProvider serviceProvider)
    {
        //declara o repositório de tasks
        var taskRepository = serviceProvider.GetRequiredService<ITaskRepository>();

        // adiciona algumas tarefas iniciais para facilitar a interação no front
        taskRepository.CriarTask(new Task
        {
            Title = "Comprar carne",
            Description = "Comprar carne no açougue",
            DueDate = DateTime.Now.AddDays(1),
            Priority = 1
        });

        taskRepository.CriarTask(new Task
        {
            Title = "Estudar para prova",
            Description = "Estudar para a prova de algorítmos",
            DueDate = DateTime.Now.AddDays(3),
            Priority = 2
        });

        taskRepository.CriarTask(new Task
        {
            Title = "Lavar o carro",
            Description = "Lavar o carro para o final de semana",
            DueDate = DateTime.Now.AddDays(2),
            Priority = 3
        });
    }
}
