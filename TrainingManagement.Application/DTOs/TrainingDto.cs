namespace TrainingManagement.Application.DTOs;

public class TrainingDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int TeacherId { get; set; }
    public int InstructorId { get; set; }
    public List<int> CourseIds { get; set; }
    public List<int> StudentIds { get; set; }
}
