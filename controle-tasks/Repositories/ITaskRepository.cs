using Task = controle_tasks.Models.Task;
namespace controle_tasks.Repositories;

//Príncipo Aberto/Fechado do SOLID, o taskrepository pode ser implementado por vários tipos de repositório. ex:db

//Princípio de segregação de interface do SOLIDO pois define apenas métodos necessários para o repositório tarefa
public interface ITaskRepository //Interface de Task Repository
{
    void CriarTask(Task task);
    void EditarTask(Task task);
    void ExcluirTask(int id);
    Task GetById(int id);
    IEnumerable<Task> GetAll();
}
