namespace TrainingManagement.Application.DTOs;

public class ReportDto
{
    public string Course { get; set; }
    public string Teacher { get; set; }
    public List<string> PassedStudents { get; set; }
    public List<string> FailedStudents { get; set; }
}

