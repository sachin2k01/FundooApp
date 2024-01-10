using ModelLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class UserRepo : IUserRepo
    {
        private readonly FundooContext fundooContext;
        public UserRepo(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public UserEntity UserRegister(RegisterModel register)
        {
            UserEntity entity = new UserEntity();
            entity.FirstName = register.FirstName;
            entity.LastName = register.LastName;
            entity.Email = register.Email;
            entity.Password = register.Password;
            fundooContext.Users.Add(entity);
            fundooContext.SaveChanges();
            return entity;

        }
    }
}
