namespace Core.Contracts;

using Base.Core.Contracts;

public interface IUnitOfWork : IBaseUnitOfWork
{
    public IExamRepository                 Exam                 { get; }
    public IExamQuestionRepository         ExamQuestion         { get; }
    public IExamineeRepository             Examinee             { get; }
    public IExamineeExamQuestionRepository ExamineeExamQuestion { get; }
}