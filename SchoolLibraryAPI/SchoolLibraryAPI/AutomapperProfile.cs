using AutoMapper;
using SchoolLibraryAPI.Common.Models;
using SchoolLibraryAPI.DAL.Entities;
using System.Linq;

namespace SchoolLibraryAPI
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<User, UserModel>()
                        .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                        .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                        .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                        .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                        .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
                        .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                        .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                        .ForMember(dest => dest.PasswordConfirm, opt => opt.Ignore())
                        .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()))
                        .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.UserId))
                        .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                        .ReverseMap();

            CreateMap<Publisher, PublisherModel>()
                .ForMember(dest => dest.AdditionalInformation, opt => opt.MapFrom(src => src.AdditionalInformation))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PublisherID, opt => opt.MapFrom(src => src.PublisherId))
                .ReverseMap();

            CreateMap<Author, AuthorModel>()
                .ForMember(dest => dest.AdditionalInformation, opt => opt.MapFrom(src => src.AdditionalInformation))
                .ForMember(dest => dest.AuthorID, opt => opt.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ReverseMap();

            CreateMap<Book, BookModel>()
                .ForMember(dest => dest.AdditionalInformation, opt => opt.MapFrom(src => src.AdditionalInformation))
                .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.BookAuthor.Select(x => x.Author)))
                .ForMember(dest => dest.AuthorIds, opt => opt.MapFrom(src => src.BookAuthor.Select(x => x.AuthorId)))
                .ForMember(dest => dest.BookID, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
                .ForMember(dest => dest.PublisherID, opt => opt.MapFrom(src => src.PublisherId))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.BookStatus))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher))
                .ReverseMap();

            CreateMap<Loan, LoanModel>()
                .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book))
                .ForMember(dest => dest.BookID, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.BorrowDate, opt => opt.MapFrom(src => src.BorrowDate))
                .ForMember(dest => dest.LoanID, opt => opt.MapFrom(src => src.LoanId))
                .ForMember(dest => dest.RequestDate, opt => opt.MapFrom(src => src.RequestDate))
                .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.UserId))
                .ReverseMap();

            CreateMap<Book, LoanModel>()
                .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.BookID, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.BorrowDate, opt => opt.MapFrom(src => src.Loan.BorrowDate))
                .ForMember(dest => dest.LoanID, opt => opt.MapFrom(src => src.LoanId))
                .ForMember(dest => dest.RequestDate, opt => opt.MapFrom(src => src.Loan.RequestDate))
                .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.Loan.ReturnDate))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.Loan.User))
                .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.Loan.UserId))
                .ReverseMap();
        }
    }
}
