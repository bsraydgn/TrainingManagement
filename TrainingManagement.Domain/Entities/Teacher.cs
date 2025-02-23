namespace TrainingManagement.Domain.Entities;
public class Teacher
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public ICollection<Training> Trainings { get; set; }
}
