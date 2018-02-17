using SchoolLibraryAPI.Common;
using SchoolLibraryAPI.Common.Models;

namespace SchoolLibraryAPI.Core.Services
{
    public interface ILoanService : IService<LoanModel>
    {
        bool RequestBook(string username, int bookID);
        bool UpdateBookStatus(int userID, int bookID, BookStatus bookStatus);
    }
}