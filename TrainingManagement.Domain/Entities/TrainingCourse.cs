namespace TrainingManagement.Domain.Entities;

public class TrainingCourse
{
    public int Id { get; set; }
    public int TrainingId { get; set; }
    public int CourseId { get; set; }

    public Training Training { get; set; }
    public Course Course { get; set; }
}

