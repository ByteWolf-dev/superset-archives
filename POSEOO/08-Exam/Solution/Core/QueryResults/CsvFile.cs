namespace Core.QueryResults;

using System;

public class CsvFile
{
    public required string   ExamName     { get; set; }
    public          DateOnly ExamDate     { get; set; }
    public required string   ExamineeName { get; set; }
    public required string   FileName { get; set; }
}