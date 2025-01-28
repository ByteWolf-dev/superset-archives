namespace Core.QueryResults;

using System.Collections.Generic;

public class ExamResult
{
    public int    ExamineeId   { get; set; }
    public string ExamineeName { get; set; } = string.Empty;

    public double TotalScore { get; set; }

    public double TotalPercent { get; set; }

    public int Grade { get; set; }

    public IList<(double Score, int Number)> Score { get; set; } = new List<(double Score, int Number)>();
}