namespace Core.QueryResults;

using System;

public class ExamExaminee
{
    public int      ExamId       { get; set; }
    public string   ExamName     { get; set; } = string.Empty;
    public DateOnly ExamDate     { get; set; }
    public int      ExamineeId   { get; set; }
    public string   ExamineeName { get; set; } = string.Empty;
}