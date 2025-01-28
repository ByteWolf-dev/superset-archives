namespace Core.Entities;

using System.Collections.Generic;

using Base.Core.Entities;

using Microsoft.EntityFrameworkCore;

[Index(nameof(Name), IsUnique = true)]
public class Examinee : EntityObject
{
    public string Name { get; set; } = string.Empty;

    public IList<ExamineeExamQuestion>? ExamineeExamQuestions { get; set; }
}