namespace Persistence;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Base.Persistence;

using Core.Contracts;
using Core.Entities;
using Core.QueryResults;

using Microsoft.EntityFrameworkCore;

public class ExamRepository : GenericRepository<Exam>, IExamRepository
{
    public ExamRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Exam?> GetByAsync(string name, DateOnly date)
    {
        return await DbSet.Include(e => e.ExamQuestions).FirstOrDefaultAsync(e => e.Name == name && e.Date == date);
    }

    public async Task<IList<ExamExaminee>> GetExamExamineeAsync()
    {
        return await Context.Set<ExamineeExamQuestion>()
            .Include(e => e.Exam)
            .Include(e => e.Examinee)
            .GroupBy(e => new { ExamineeName = e.Examinee!.Name, e.ExamineeId, e.Exam!.Date, ExamName = e.Exam!.Name, e.ExamId })
            .Select(x =>
                new ExamExaminee()
                {
                    ExamId       = x.Key.ExamId,
                    ExamDate     = x.Key.Date,
                    ExamName     = x.Key.ExamName,
                    ExamineeId   = x.Key.ExamineeId,
                    ExamineeName = x.Key.ExamineeName,
                })
            .ToListAsync();
    }

    public async Task<IList<ExamResult>> GetExamResultAsync(int examId)
    {
        var exam        = await GetByIdAsync(examId, nameof(Exam.ExamQuestions));
        var totalPoints = exam.ExamQuestions.Sum(x => x.Points);
        var query = await Context
            .Set<ExamineeExamQuestion>()
            .Include(e => e.ExamQuestion)
            .Include(e => e.Examinee)
            .AsNoTracking()
            .Where(e => e.ExamId == examId)
            .GroupBy(g => new { g.ExamineeId, g.Examinee!.Name })
            .ToListAsync();

        int ToGrade(double percent)
        {
            return (percent*100.0) switch
            {
                >= 92.0 => 1,
                >= 81.0 => 2,
                >= 66.0 => 3,
                >= 50.0 => 4,
                _       => 5
            };
        }

        var result = query.Select(g =>
        {
            var totalScore   = g.Sum(y => y.ScorePercentage * y.ExamQuestion!.Points);
            var totalPercent = totalScore / totalPoints;
            return new ExamResult()
            {
                ExamineeId   = g.Key.ExamineeId,
                ExamineeName = g.Key.Name,
                TotalScore   = double.Round(totalScore,1),
                TotalPercent = double.Round(totalPercent*100.0,1),
                Grade        = ToGrade(totalPercent),
                Score        = g.Select(x => (x.ScorePercentage, x.ExamQuestion!.Number)).ToList()
            };
        }).ToList();

        return result;
    }
}