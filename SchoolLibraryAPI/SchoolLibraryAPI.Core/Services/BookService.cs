using AutoMapper;
using SchoolLibraryAPI.Common.Models;
using SchoolLibraryAPI.DAL;
using System.Collections.Generic;
using System.Linq;

using SchoolLibraryAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using SchoolLibraryAPI.Common;

namespace SchoolLibraryAPI.Core.Services
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly LibraryContext _dbContext;

        public BookService(IMapper mapper, LibraryContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public IList<BookModel> Get()
        {
            var query = _dbContext
                .Books.AsNoTracking()
                .Include(x => x.Publisher)
                .Include(x => x.BookAuthor)
                .ThenInclude(x => x.Author)
                .Where(x => x.IsDeleted == null || !x.IsDeleted.Value);

            var books = query.ToList();

            var result = _mapper.Map<IList<Book>, IList<BookModel>>(books);

            return result;
        }

        public BookModel GetById(int id)
        {
            var book = _dbContext.Books.AsNoTracking()
                .Include(x => x.Publisher)
                .Include(x => x.BookAuthor)
                .ThenInclude(x => x.Author)
                .FirstOrDefault(x => x.BookId == id);
            var result = _mapper.Map<Book, BookModel>(book);

            return result;
        }

        public void Update(BookModel model)
        {
            var book = model.BookID > 0 ? _dbContext.Books
                .FirstOrDefault(x => x.BookId == model.BookID) : new Book() { BookStatus = (int)BookStatus.Available };

            if (book != null)
            {
                book.AdditionalInformation = model.AdditionalInformation;
                book.PublisherId = model.PublisherID ?? 0;                
                book.ReleaseDate = model.ReleaseDate;
                book.Title = model.Title;

                if (model.BookID == 0)
                {
                    _dbContext.Books.Add(book);
                }

                var bookAuthors = model.BookID > 0 ?
                    _dbContext.BookAuthor.Where(x => x.BookId == model.BookID).ToList() :
                    new List<BookAuthor>();

                foreach (var ba in bookAuthors)
                {
                    if (model.AuthorIds.All(x => x != ba.AuthorId))
                    {
                        _dbContext.BookAuthor.Remove(ba);
                    }
                }

                foreach (var authorId in model.AuthorIds)
                {
                    if (bookAuthors.All(x => x.AuthorId != authorId))
                    {
                        _dbContext.BookAuthor.Add(new BookAuthor
                        {
                            BookId = model.BookID,
                            Book = book,
                            AuthorId = authorId
                        });
                    }
                }

                _dbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var book = _dbContext.Books.FirstOrDefault(x => x.BookId == id);
            if (book != null)
            {
                book.IsDeleted = true;
                _dbContext.SaveChanges();
            }
        }
    }
}
