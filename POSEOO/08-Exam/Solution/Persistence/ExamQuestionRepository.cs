namespace Persistence;

using Base.Persistence;

using Core.Contracts;
using Core.Entities;

public class ExamQuestionRepository : GenericRepository<ExamQuestion>, IExamQuestionRepository
{
    public ExamQuestionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

}