using BusinessLayer.Interface;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepo userRepo;

        public UserBusiness(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
           
        }


        public UserEntity UserRegister(RegisterModel register)
        {
            return userRepo.UserRegister(register);
        }

        public UserEntity GetUserById(int id)
        {
            return userRepo.GetUserById(id);
        }
    }
}
