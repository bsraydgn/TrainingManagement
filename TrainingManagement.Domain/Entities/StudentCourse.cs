namespace TrainingManagement.Domain.Entities;

public class StudentCourse
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public string Status { get; set; }
    public int Grade { get; set; }

    public Student Student { get; set; }
    public Course Course { get; set; }
}
