using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SchoolLibraryAPI.Common.Models;
using SchoolLibraryAPI.DAL;
using SchoolLibraryAPI.DAL.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SchoolLibraryAPI.Common;

namespace SchoolLibraryAPI.Core.Services
{
    public class LoanService : ILoanService
    {
        private readonly IMapper _mapper;
        private readonly LibraryContext _dbContext;

        public LoanService(IMapper mapper, LibraryContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public void Delete(int id) { throw new NotImplementedException(); }

        public LoanModel GetById(int id) { throw new NotImplementedException(); }

        public void Update(LoanModel model) { throw new NotImplementedException(); }

        public IList<LoanModel> Get()
        {
            var query = _dbContext
                .Books.AsNoTracking()
                .Include(x => x.BookAuthor)
                    .ThenInclude(x => x.Author)
                .Include(x => x.Loan)
                    .ThenInclude(x => x.User);

            var loans = query.Where(x => x.LoanId != null).ToList();

            var result = _mapper.Map<IList<Book>, IList<LoanModel>>(loans);

            return result;
        }

        public bool RequestBook(string username, int bookID)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.UserName.ToLower() == username.ToLower());
            if (user == null || user.UserId == 0)
            {
                return false;
            }

            var book = _dbContext.Books.FirstOrDefault(x => x.BookId == bookID);
            if (book == null)
            {
                return false;
            }

            if (book.BookStatus != (int)BookStatus.Available)
            {
                return false;
            }

            book.BookStatus = (int)BookStatus.Requested;

            var loan = new Loan
            {
                BookId = book.BookId,
                RequestDate = DateTime.UtcNow,
                UserId = user.UserId
            };

            book.LoanId = null;
            book.Loan = loan;

            _dbContext.SaveChanges();

            return true;
        }

        public bool UpdateBookStatus(int userID, int bookID, BookStatus bookStatus)
        {
            var book = _dbContext.Books.AsQueryable().Include(x => x.Loan).FirstOrDefault(x => x.BookId == bookID);
            if (book == null)
            {
                return false;
            }


            bool result = false;

            if (bookStatus == BookStatus.Available)
            {
                result = ChangeBookStatusToAvailable(userID, book);
            }
            else if (bookStatus == BookStatus.Borrowed)
            {
                result = ChangeBookStatusToBorrowed(userID, book);
            }
            else if (bookStatus == BookStatus.Lost)
            {
                result = ChangeBookStatusToLost(userID, book);
            }

            if (result)
            {
                _dbContext.SaveChanges();
            }

            return result;
        }

        private bool ChangeBookStatusToAvailable(int userID, Book book)
        {
            if (book.BookStatus == (int)BookStatus.Available ||
                book.BookStatus == (int)BookStatus.Lost ||
                book.Loan.UserId != userID)
            {
                return false;
            }

            book.BookStatus = (int)BookStatus.Available;

            book.LoanId = null;
            book.Loan.ReturnDate = DateTime.UtcNow;
            book.Loan = null;

            return true;
        }

        private bool ChangeBookStatusToBorrowed(int userID, Book book)
        {
            if (book.BookStatus != (int)BookStatus.Requested ||
                book.Loan.UserId != userID)
            {
                return false;
            }

            book.BookStatus = (int)BookStatus.Borrowed;

            book.Loan.BorrowDate = DateTime.UtcNow;

            return true;
        }

        private bool ChangeBookStatusToLost(int userID, Book book)
        {
            if (book.Loan.UserId != userID)
            {
                return false;
            }

            book.BookStatus = (int)BookStatus.Lost;

            return true;
        }
    }
}
