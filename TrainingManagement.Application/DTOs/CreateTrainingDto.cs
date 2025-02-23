namespace TrainingManagement.Application.DTOs;

public class CreateTrainingDto
{
    public string Name { get; set; }
    public int TeacherId { get; set; }
    public int InstructorId { get; set; }
    public List<int> CourseIds { get; set; }
    public List<int> StudentIds { get; set; }
}
