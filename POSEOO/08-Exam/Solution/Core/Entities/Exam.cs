namespace Core.Entities;

using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

using Base.Core.Entities;

using Microsoft.EntityFrameworkCore;

[Index(nameof(Name), nameof(Date), IsUnique = true)]
public class Exam : EntityObject
{
    public DateOnly Date { get; set; }

    [MaxLength(256)]
    public string Name { get; set; } = string.Empty;

    public IList<ExamQuestion>?         ExamQuestions         { get; set; }
    public IList<ExamineeExamQuestion>? ExamineeExamQuestions { get; set; }
}