using Task = controle_tasks.Models.Task;

namespace controle_tasks.Repositories
{
    //Princípio de responsabilidade única do SOLID pois TaskRepository serve apenas para armazenar taskss em memória.
    public class TaskRepository : ITaskRepository
    {
        private readonly List<Task> _tasks = new List<Task>(); //Lista onde serão salvas em memória as tasks

        public void CriarTask(Task task) //CREATE
        {
            task.Id = _tasks.Any() ? _tasks.Max(t => t.Id) + 1 : 1;
            _tasks.Add(task);
        }

        public void EditarTask(Task task) //UPDATE
        {
            var taskExistente = GetById(task.Id); //busca pelo id
            if (taskExistente != null)
            {
                taskExistente.Title = task.Title;
                taskExistente.Description = task.Description;
                taskExistente.DueDate = task.DueDate;
                taskExistente.Priority = task.Priority;
            }
        }
        public void ExcluirTask(int id) //DELETE
        {
            var task = GetById(id); //busca pelo id

            if (task != null)          //se a task não for null ele delete
                _tasks.Remove(task);
            
        }

        public Task GetById(int id)
        {
            return _tasks.SingleOrDefault(t => t.Id == id);
        }

        public IEnumerable<Task> GetAll()
        {
            return _tasks;
        }
    }
}
