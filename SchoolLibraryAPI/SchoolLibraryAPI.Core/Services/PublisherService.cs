using AutoMapper;
using SchoolLibraryAPI.Common.Models;
using SchoolLibraryAPI.DAL;
using System.Collections.Generic;
using System.Linq;
using SchoolLibraryAPI.DAL.Entities;

namespace SchoolLibraryAPI.Core.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IMapper _mapper;
        private readonly LibraryContext _dbContext;

        public PublisherService(IMapper mapper, LibraryContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }        

        public IList<PublisherModel> Get()
        {
            var publishers = _dbContext.Publishers
                .Where(x => x.IsDeleted == null || !x.IsDeleted.Value)
                .ToList();

            var result = _mapper.Map<IList<Publisher>, IList<PublisherModel>>(publishers);

            return result;
        }

        public PublisherModel GetById(int id)
        {
            var publisher = _dbContext.Publishers.FirstOrDefault(x => x.PublisherId == id);
            var result = _mapper.Map<Publisher, PublisherModel>(publisher);

            return result;
        }

        public void Update(PublisherModel model)
        {
            Publisher publisher = null;

            if (model.PublisherID == 0)
            {
                publisher = _mapper.Map<PublisherModel, Publisher>(model);
                publisher.IsDeleted = false;

                _dbContext.Publishers.Add(publisher);
            }
            else
            {
                publisher = _dbContext.Publishers.FirstOrDefault(x => x.PublisherId == model.PublisherID);
                if (publisher != null)
                {
                    publisher.AdditionalInformation = model.AdditionalInformation;
                    publisher.Address = model.Address;
                    publisher.Name = model.Name;                   
                }
            }

            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var publisher = _dbContext.Publishers.FirstOrDefault(x => x.PublisherId == id);
            if (publisher != null)
            {
                publisher.IsDeleted = true;
                _dbContext.SaveChanges();
            }
        }
    }
}
