namespace Persistence;

using Core.Contracts;

using Base.Persistence;

public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
{
    public ApplicationDbContext? ApplicationDbContext => BaseApplicationDbContext as ApplicationDbContext;

    public UnitOfWork(
        ApplicationDbContext            applicationDbContext,
        IExamRepository                 exam,
        IExamQuestionRepository         examQuestion,
        IExamineeRepository             examinee,
        IExamineeExamQuestionRepository examineeExamQuestion
    ) : base(applicationDbContext)
    {
        Exam                 = exam;
        ExamQuestion         = examQuestion;
        Examinee             = examinee;
        ExamineeExamQuestion = examineeExamQuestion;
    }

    public IExamRepository                 Exam                 { get; }
    public IExamQuestionRepository         ExamQuestion         { get; }
    public IExamineeRepository             Examinee             { get; }
    public IExamineeExamQuestionRepository ExamineeExamQuestion { get; }
}