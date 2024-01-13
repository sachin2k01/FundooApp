using BusinessLayer.Interface;
using MassTransit;
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


        public string UserLogin(LoginModel login)
        {
            return userRepo.UserLogin(login);
        }
        public async Task<string> ForgotPassword(string emailTo, IBus bus)
        {
            return await userRepo.ForgotPassword(emailTo, bus);
        }

        public ProductEntity AddProduct(ProductModel product)
        {
            return userRepo.AddProduct(product);
        }

        public UserEntity GetUsersById(int id)
        {
            return userRepo.GetUsersById(id);
        }
    }
}
