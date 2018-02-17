using SchoolLibraryAPI.Common;
using SchoolLibraryAPI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolLibraryAPI.DAL
{
    public static class DbInitializer
    {
        public static void Initialize(LibraryContext context)
        {
            context.Database.EnsureCreated();

            if (context.Authors.Any())
            {
                return;
            }

            var users = new[]
            {
                new User
                {
                    Role = (int)Role.Administrator,
                    FirstName = "admin",
                    LastName = "admin",
                    UserName = "admin",
                    //Password = "admin",
                    Password = "34A4E04935C6DB76517399972ECCD951CDB66E14B1B97D49074A9559EF27A0E99798AE7894709556106BE587D08954BFA795FFB06D4B9A5D5D9108FF4FB104B5",
                    PasswordSalt = "82EAA0C0ECB6692FC776763250E55DC63F33528130009062B3E11BC208D44620752EEDA3EC1F165E2078F9879DD8A95B44C159819DD0CDB9C52BEF25442450F5",
                    Address = "Long Street 3, London",
                    DateOfBirth = new DateTime(1980, 3, 20),
                    Email = "admin@admin.pl",
                    IsDeleted = false
                },

                new User
                {
                    Role = (int)Role.Librarian,
                    FirstName = "Barbara",
                    LastName = "O'Connor",
                    UserName = "barbara",
                    //Password = "library",
                    Password = "ABB36BD3FAF5A3132AC066C23502DEC4C817CB13E8CD2AF3006AC3715C19EDD5348F59C315CF3804FE7BE3BF37EB5E4F161E98693E1D9FDD81F6D0AD93E8A33B",
                    PasswordSalt = "697AAB2CDAA87646F08BC5F06CDB79EB47220178B2113C11B65B3026FC2B08109DA0BCC55533ACC44E063CA6DE1FBE9B67CE977BEFF1F0C794F10AFA04F3F99A",
                    Address = "Flower Av. 3/5, Toronto",
                    DateOfBirth = new DateTime(1959, 12, 3),
                    Email = "Barbara@test.com",
                    IsDeleted = false
                },

                new User
                {
                    Role = (int)Role.Student,
                    FirstName = "William",
                    LastName = "Farrel",
                    UserName = "bil",
                    //Password = "bil",
                    Password = "4AA19DAFBBE0612AB91511F7E99013D826796440EC91FE2FDCD3429872CE62EEB975CD5281A08333A98D82124C8B657C1087AA4641F40993FA0E512207373E05",
                    PasswordSalt = "0B46D47179F862AA59C5A4E14039F217175F6FDC1F89B1C542E940E27CA71BDE5BD9401686AD281D90951635286E74DFB77F608ACB8C06308F5D17CF8BDD6148",
                    Address = "5th Avenue, Chicago",
                    DateOfBirth = new DateTime(1989, 2, 15),
                    Email = "student@test.pl",
                    IsDeleted = false
                },

                new User
                {
                    Role = (int)Role.Student,
                    FirstName = "Peter",
                    LastName = "Key",
                    UserName = "peter",
                    //Password = "peter",
                    Password = "B94A634F2A1FA6B0725E7F9C557A9B0F2909E47DD723BB85D4A97876A45D82C0FAF038C231E59177F1E6872D07556948DDF27A008496498E16DAE3DE5E2AE77F",
                    PasswordSalt = "FD216FD3C2A88A3CF3B80B00B72F5C39C75D3CB9E8B07371AC126597F376FB7B2D05295A9AE72CEBC695C6F301039E7DA1613147957EE5556C1727DCEF03B46B",
                    Address = "Northway 20, Melbourne",
                    DateOfBirth = new DateTime(1984, 12, 10),
                    Email = "peter@test.com.pl",
                    IsDeleted = false
                }
            };

            context.Users.AddRange(users);

            var lmAuthor = new Author
            {
                FirstName = "Lucy",
                LastName = "Montgomery",
                AdditionalInformation = "Novelist"
            };

            var amAuthor = new Author
            {
                FirstName = "Adam",
                LastName = "Mickiewicz",
                AdditionalInformation = "Polish Writer"
            };

            var cpAuthor = new Author
            {
                FirstName = "Charles",
                LastName = "Petzold",
                AdditionalInformation = "Charles Petzold (born February 2, 1953, New Brunswick, New Jersey)" +
                    "is an American programmer and technical author on Microsoft Windows applications. " +
                    "He is also a Microsoft Most Valuable Professional and was named one of Microsoft's seven Windows Pioneers."
            };

            var jsAuthor = new Author
            {
                FirstName = "John",
                LastName = "Smith",
                AdditionalInformation = "A writer"
            };

            var sfAuthor = new Author
            {
                FirstName = "Susan",
                LastName = "Forrester",
                AdditionalInformation = "A famous writer"
            };

            var authors = new Author[] { lmAuthor, amAuthor, cpAuthor, jsAuthor, sfAuthor };

            context.Authors.AddRange(authors);

            var aPublisher = new Publisher
            {
                Name = "Apress",
                Address = "Apress Media, LLC; 233 Spring Street, 6th Floor; New York, NY 1001",
                AdditionalInformation = "Apress, a Springer Nature company, is a publisher dedicated to meeting the information " +
                    "needs of developers, IT professionals, and tech communities worldwide."
            };

            var gPublisher = new Publisher
            {
                Name = "Greg",
                Address = "Wydawnictwo Greg; ul. Klasztorna 2B; 31-979 Kraków, Poland",
                AdditionalInformation = "Publisher of school books"
            };

            var sPublisher = new Publisher
            {
                Name = "Starfire",
                Address = "Starfire Publishing Ltd; BCM Starfire; London WC1N 3XX; United Kingdom",
                AdditionalInformation = "Starfire Publishing"
            };

            var publishers = new[] { aPublisher, gPublisher, sPublisher };

            context.Publishers.AddRange(publishers);

            var aBook = new Book
            {
                Title = "Anne of Green Gables",
                ReleaseDate = new DateTime(2003, 3, 3),
                AdditionalInformation = "One of the most popular novels",
                IsDeleted = false,
                Publisher = gPublisher,
                BookStatus = (int)BookStatus.Available
            };

            aBook.BookAuthor = new List<BookAuthor>(new[] { new BookAuthor { Author = lmAuthor, Book = aBook } });

            var pBook = new Book
            {
                Title = "Pan Tadeusz",
                ReleaseDate = new DateTime(1950, 1, 10),
                AdditionalInformation = "Polish bestseller",
                IsDeleted = false,
                Publisher = gPublisher,
                BookStatus = (int)BookStatus.Available
            };

            pBook.BookAuthor = new List<BookAuthor>(new[] { new BookAuthor { Author = amAuthor, Book = pBook } });

            var wBook = new Book
            {
                Title = "Windows 95 development",
                ReleaseDate = new DateTime(1996, 3, 3),
                AdditionalInformation = "Software development classic",
                IsDeleted = false,
                Publisher = aPublisher,
                BookStatus = (int)BookStatus.Requested
            };

            wBook.BookAuthor = new List<BookAuthor>(new[]
            {
                new BookAuthor { Author = cpAuthor, Book = wBook },
                new BookAuthor { Author = jsAuthor, Book = wBook },
                new BookAuthor { Author = sfAuthor, Book = wBook }
            });

            var books = new[]
            {
                aBook,
                pBook,
                wBook
            };

            context.Books.AddRange(books);

            context.SaveChanges();
        }
    }
}
