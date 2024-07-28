namespace controle_tasks.Models;

public class Task //Modelagem dos dados(Lógica de negócios)
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public int Priority { get; set; }
}
