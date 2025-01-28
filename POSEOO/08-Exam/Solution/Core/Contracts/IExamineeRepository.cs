namespace Core.Contracts;

using Base.Core.Contracts;

using Core.Entities;

using System.Threading.Tasks;

public interface IExamineeRepository : IGenericRepository<Examinee>
{
    Task<Examinee?> GetByAsync(string name);
}