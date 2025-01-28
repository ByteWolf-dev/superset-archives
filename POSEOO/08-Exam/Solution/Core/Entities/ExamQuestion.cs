namespace Core.Entities;

using System.ComponentModel.DataAnnotations;

using Base.Core.Entities;

using Microsoft.EntityFrameworkCore;

[Index(nameof(ExamId), nameof(Number), IsUnique = true)]
public class ExamQuestion : EntityObject
{
    public int    Number      { get; set; }

    [MaxLength(1024)]
    public string Description { get; set; } = string.Empty;
    public int    Points      { get; set; }

    public int   ExamId { get; set; }
    public Exam? Exam   { get; set; }
}