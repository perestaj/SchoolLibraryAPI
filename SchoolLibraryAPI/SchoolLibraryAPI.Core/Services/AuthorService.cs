using System.Collections.Generic;
using AutoMapper;
using SchoolLibraryAPI.Common.Models;
using SchoolLibraryAPI.DAL;
using System.Linq;
using SchoolLibraryAPI.DAL.Entities;

namespace SchoolLibraryAPI.Core.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IMapper _mapper;
        private readonly LibraryContext _dbContext;

        public AuthorService(IMapper mapper, LibraryContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public void Delete(int id)
        {
            var author = _dbContext.Authors.FirstOrDefault(x => x.AuthorId == id);
            if (author != null)
            {
                author.IsDeleted = true;
                _dbContext.SaveChanges();
            }
        }

        public IList<AuthorModel> Get()
        {
            var authors = _dbContext.Authors
                .Where(x => x.IsDeleted == null || !x.IsDeleted.Value)
                .ToList();

            var result = _mapper.Map<IList<Author>, IList<AuthorModel>>(authors);

            return result;
        }

        public AuthorModel GetById(int id)
        {
            var author = _dbContext.Authors.FirstOrDefault(x => x.AuthorId == id);
            var result = _mapper.Map<Author, AuthorModel>(author);

            return result;
        }

        public void Update(AuthorModel model)
        {
            Author author = null;

            if (model.AuthorID == 0)
            {
                author = _mapper.Map<AuthorModel, Author>(model);
                author.IsDeleted = false;

                _dbContext.Authors.Add(author);
            }
            else
            {
                author = _dbContext.Authors.FirstOrDefault(x => x.AuthorId == model.AuthorID);
                if (author != null)
                {
                    author.AdditionalInformation = model.AdditionalInformation;
                    author.FirstName = model.FirstName;
                    author.LastName = model.LastName;                    
                }
            }

            _dbContext.SaveChanges();
        }
    }
}
