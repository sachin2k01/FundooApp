﻿using MassTransit;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IUserBusiness
    {
        public UserEntity UserRegister(RegisterModel register);
        public string UserLogin(LoginModel login);
        public Task<string> ForgotPassword(string emailTo, IBus bus);

        public ProductEntity AddProduct(ProductModel product);

        public UserEntity GetUsersById(int id);


    }
}
