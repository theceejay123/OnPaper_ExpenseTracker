using OnPaper.ExpenseTracker.Core.Interfaces;
using OnPaper.ExpenseTracker.Core.Models;
using OnPaper.ExpenseTracker.Core.Specifications;

namespace OnPaper.ExpenseTracker.Infrastructure.Services;

public class WorkBookService : IWorkBookService
{
    private readonly IUnitOfWork _unitOfWork;

    public WorkBookService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<WorkBook> UpdateWorkBookAsync(WorkBook workBook)
    {
        var spec = new WorkbookWithTransactionsSpecification(workBook.Id, workBook.AppUserId);
        var workBookToUpdate = await _unitOfWork.Repository<WorkBook>().GetWithSpecificationAsync(spec);
        if (workBookToUpdate == null)
        {
            return null;
        }

        workBookToUpdate.Name = workBook.Name;
        workBookToUpdate.Description = workBook.Description;
        workBookToUpdate.UpdateDate = DateTime.UtcNow;
        _unitOfWork.Repository<WorkBook>().Update(workBookToUpdate);
        await _unitOfWork.CompleteAsync();
        return workBookToUpdate;
    }

    public async Task<WorkBook> DeleteWorkBookAsync(int id, string appUserId)
    {
        var spec = new WorkbookWithTransactionsSpecification(id, appUserId);
        var workBookToDelete = await _unitOfWork.Repository<WorkBook>().GetWithSpecificationAsync(spec);
        if (workBookToDelete == null)
        {
            return null;
        }
        _unitOfWork.Repository<WorkBook>().Delete(workBookToDelete);
        await _unitOfWork.CompleteAsync();
        return workBookToDelete;
    }

    public async Task<WorkBook> CreateWorkBookAsync(WorkBook workBook)
    {
        _unitOfWork.Repository<WorkBook>().Create(workBook);
        var result = await _unitOfWork.CompleteAsync();
        return result <= 0 ? null : workBook;
    }

    public async Task<IReadOnlyList<WorkBook>> ListWorkBooksAsync(string appUserId)
    {
        var spec = new WorkbookWithTransactionsSpecification(appUserId);
        return await _unitOfWork.Repository<WorkBook>().ListWithSpecificationAsync(spec);
    }

    public async Task<WorkBook> GetWorkBookByIdAsync(int id, string appUserId)
    {
        var spec = new WorkbookWithTransactionsSpecification(id, appUserId);
        return await _unitOfWork.Repository<WorkBook>().GetWithSpecificationAsync(spec);
    }
}