namespace Persistence;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Core.Contracts;

using System.Threading.Tasks;

using Core.Entities;
using Core.QueryResults;

using System.IO;

public class ImportController : IImportController
{
    public ImportController(IUnitOfWork uow)
    {
        _uow = uow;
    }

    private IUnitOfWork _uow;

    public async Task<int> ImportCsvFileAsync(CsvFile csvFile)
    {
        var lines = await System.IO.File.ReadAllLinesAsync(csvFile.FileName);

        var exam = await _uow.Exam.GetByAsync(csvFile.ExamName, csvFile.ExamDate);

        if (exam == null)
        {
            exam = new Exam()
            {
                Date          = csvFile.ExamDate,
                Name          = csvFile.ExamName,
                ExamQuestions = new List<ExamQuestion>()
            };
            await _uow.Exam.AddAsync(exam);
        }

        var questionsInCsv = lines.Select(l =>
        {
            var part = l.Split(';');
            return new ExamQuestion()
            {
                Exam        = exam,
                Number      = int.Parse(part[0]),
                Points      = 1,
                Description = $"ExamQuestion {part[0]}"
            };
        }).ToList();

        var newQuestions = questionsInCsv
            .ExceptBy(exam.ExamQuestions!.Select(m => m.Number), m => m.Number)
            .ToList();

        await _uow.ExamQuestion.AddRangeAsync(newQuestions);

        var examinee = await _uow.Examinee.GetByAsync(csvFile.ExamineeName);

        if (examinee == null)
        {
            examinee = new Examinee()
            {
                Name = csvFile.ExamineeName
            };
            await _uow.Examinee.AddAsync(examinee);
        }

        var resultInCsv = lines.Select(l =>
        {
            var part = l.Split(';');
            return new ExamineeExamQuestion()
            {
                Exam            = exam,
                Examinee        = examinee,
                ExamQuestion    = exam.ExamQuestions!.Single(q => q.Number == int.Parse(part[0])),
                ScorePercentage = double.Parse(part[1], CultureInfo.InvariantCulture)
            };
        }).ToList();
        await _uow.ExamineeExamQuestion.AddRangeAsync(resultInCsv);

        await _uow.SaveChangesAsync();

        return resultInCsv.Count;
    }

    public async Task<IEnumerable<CsvFile>> GetNotImportedFilesAsync()
    {
        var files = Directory.GetFiles("Csv-Files", "*.csv")
            .Select(f =>
            {
                var filename = Path.GetFileNameWithoutExtension(f);
                var part     = filename.Split("_");
                return new CsvFile()
                {
                    ExamDate     = DateOnly.ParseExact(part[0], "yyyyMMdd"),
                    ExamName     = part[1],
                    ExamineeName = part[2],
                    FileName     = f
                };
            });

        var inDb = await _uow.Exam.GetExamExamineeAsync();
        return files.ExceptBy(inDb.Select(e => (e.ExamDate, e.ExamName, e.ExamineeName)), e => (e.ExamDate, e.ExamName, e.ExamineeName)).ToList();
    }
}