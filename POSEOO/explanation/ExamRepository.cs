namespace Persistence.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;

using Base.Persistence;

using Core.Contracts;
using Core.Entities;

using Microsoft.EntityFrameworkCore;

public class ExamRepository : GenericRepository<Exam>, IExamRepository
{
    public ExamRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<Exam>> GetAllExamnsIncludingQuestions()
    {
        return await Context.Set<Exam>().Include(exam => exam.ExamQuestions).ToListAsync();
    }
}