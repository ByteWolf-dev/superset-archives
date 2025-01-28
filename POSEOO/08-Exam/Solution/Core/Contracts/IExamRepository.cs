namespace Core.Contracts;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Base.Core.Contracts;

using Core.Entities;
using Core.QueryResults;

public interface IExamRepository : IGenericRepository<Exam>
{
    Task<Exam?> GetByAsync(string name, DateOnly date);

    Task<IList<ExamExaminee>> GetExamExamineeAsync();

    Task<IList<ExamResult>> GetExamResultAsync(int examId);
}