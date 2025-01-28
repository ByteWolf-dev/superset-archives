namespace Core.Entities;

using Base.Core.Entities;

using Microsoft.EntityFrameworkCore;

[Index(nameof(ExamId), nameof(ExamineeId), nameof(ExamQuestionId), IsUnique = true)]
public class ExamineeExamQuestion : EntityObject
{
    public int   ExamId { get; set; }
    public Exam? Exam   { get; set; }

    public int       ExamineeId { get; set; }
    public Examinee? Examinee   { get; set; }

    public int           ExamQuestionId { get; set; }
    public ExamQuestion? ExamQuestion   { get; set; }

    public double ScorePercentage { get; set; }
}