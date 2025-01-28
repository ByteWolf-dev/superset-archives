namespace Core.Contracts;

using System.Threading.Tasks;
using System.Collections.Generic;

using Core.QueryResults;

public interface IImportController 
{
    Task<int> ImportCsvFileAsync(CsvFile csvFile);

    Task<IEnumerable<CsvFile>> GetNotImportedFilesAsync();
}