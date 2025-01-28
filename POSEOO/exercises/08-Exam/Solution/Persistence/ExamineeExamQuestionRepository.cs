namespace Persistence;

using Base.Persistence;

using Core.Contracts;
using Core.Entities;

public class ExamineeExamQuestionRepository : GenericRepository<ExamineeExamQuestion>, IExamineeExamQuestionRepository
{
    public ExamineeExamQuestionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

}