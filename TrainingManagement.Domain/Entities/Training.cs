namespace TrainingManagement.Domain.Entities;

public class Training
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int TeacherId { get; set; }
    public int InstructorId { get; set; }

    public Teacher Teacher { get; set; }
    public Instructor Instructor { get; set; }
    public ICollection<TrainingCourse> TrainingCourses { get; set; }
}
