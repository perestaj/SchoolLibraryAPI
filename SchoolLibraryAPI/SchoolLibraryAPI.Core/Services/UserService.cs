using SchoolLibraryAPI.Common.Models;
using SchoolLibraryAPI.DAL;
using SchoolLibraryAPI.DAL.Entities;
using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using AutoMapper;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using SchoolLibraryAPI.Common;
using System.Collections.Generic;
using SchoolLibraryAPI.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace SchoolLibraryAPI.Core
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly LibraryContext _dbContext;
        private readonly IOptions<AppSettings> _options;

        public UserService(IMapper mapper, LibraryContext dbContext, IOptions<AppSettings> options)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _options = options;
        }

        public IList<UserModel> Get()
        {
            var users = _dbContext.Users
                .Where(x => x.IsDeleted == null || !x.IsDeleted.Value)
                .ToList();

            var result = _mapper.Map<IList<User>, IList<UserModel>>(users);

            return result;
        }

        public UserModel GetById(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.UserId == id);
            var result = _mapper.Map<User, UserModel>(user);
            result.Password = string.Empty;

            return result;
        }

        public UserAddEditResult Update(UserModel model)
        {
            if (_dbContext.Users.AsNoTracking().Any(x => x.UserId != model.UserID && x.UserName.ToLower() == model.UserName.ToLower()))
            {
                return new UserAddEditResult { Success = false, UserNameTaken = true };
            }

            if (!string.IsNullOrEmpty(model.Email) &&
                _dbContext.Users.AsNoTracking().Any(x => x.UserId != model.UserID && x.Email.ToLower() == model.Email.ToLower()))
            {
                return new UserAddEditResult { Success = false, EmailTaken = true };
            }

            User user = null;
            if (model.UserID == 0)
            {
                user = new User();
                _dbContext.Users.Add(user);
            }
            else
            {
                user = _dbContext.Users.FirstOrDefault(x => x.UserId == model.UserID);
            }

            if (user != null)
            {
                user.UserName = model.UserName;
                user.Address = model.Address;
                user.DateOfBirth = model.DateOfBirth;
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Role = (int)model.Role;

                if (!string.IsNullOrEmpty(model.Password))
                {
                    user.PasswordSalt = CreateSalt512();
                    user.Password = GetEncodedPassword(model.Password, user.PasswordSalt);
                }

                _dbContext.SaveChanges();
            }

            return new UserAddEditResult { Success = true };
        }


        public void Delete(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.UserId == id);
            if (user != null)
            {
                user.IsDeleted = true;
                _dbContext.SaveChanges();
            }
        }

        public TokenModel GetToken(string userName, string password)
        {
            TokenModel token = null;

            var user = _dbContext.Users.FirstOrDefault(x => x.UserName == userName);
            if (user == null)
            {
                return token;
            }

            string hashedPassword = GetEncodedPassword(password, user.PasswordSalt);

            if (string.Equals(user.Password, hashedPassword))
            {
                var claims = new[]
                {
                                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                                new Claim("roles", ((Role)user.Role).ToString())  ,
                                new Claim(_options.Value.ClaimTypeUserName, user.UserName)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.SymmetricSecurityKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var securityToken = new JwtSecurityToken(
                    issuer: _options.Value.Issuer,
                    audience: _options.Value.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: creds
                    );

                token = new TokenModel
                {
                    UserName = userName,
                    Role = user.Role,
                    Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                    Expiration = securityToken.ValidTo
                };
            }

            return token;
        }

        private static string GetEncodedPassword(string password, string passwordSalt)
        {
            var encoder = new ASCIIEncoding();
            var messageBytes = encoder.GetBytes(password);
            var secretKeyBytes = new byte[passwordSalt.Length / 2];
            for (int index = 0; index < secretKeyBytes.Length; index++)
            {
                string byteValue = passwordSalt.Substring(index * 2, 2);
                secretKeyBytes[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }
            var hmacsha512 = new HMACSHA512(secretKeyBytes);

            byte[] hashValue = hmacsha512.ComputeHash(messageBytes);

            string hmac = "";
            foreach (byte x in hashValue)
            {
                hmac += String.Format("{0:x2}", x);
            }

            return hmac.ToUpper();
        }

        private static string CreateSalt512()
        {
            byte[] saltData = new byte[512];
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            csprng.GetBytes(saltData);
            return BitConverter.ToString((new SHA512Managed()).ComputeHash(saltData)).Replace("-", "");
        }

        void IService<UserModel>.Update(UserModel model)
        {
            throw new NotImplementedException();
        }
    }
}
