using OnPaper.ExpenseTracker.Core.Models;

namespace OnPaper.ExpenseTracker.Core.Interfaces;

public interface IWorkBookService
{
    Task<WorkBook> UpdateWorkBookAsync(WorkBook workBook);
    Task<WorkBook> DeleteWorkBookAsync(int id, string appUserId);
    Task<WorkBook> CreateWorkBookAsync(WorkBook workBook);
    Task<IReadOnlyList<WorkBook>> ListWorkBooksAsync(string appUserId);
    Task<WorkBook> GetWorkBookByIdAsync(int id, string appUserId);
}