namespace Persistence;

using Base.Persistence;

using Core.Contracts;
using Core.Entities;

using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class ExamineeRepository : GenericRepository<Examinee>, IExamineeRepository
{
    public ExamineeRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
    public async Task<Examinee?> GetByAsync(string name)
    {
        return await DbSet.FirstOrDefaultAsync(e => e.Name == name);
    }
}