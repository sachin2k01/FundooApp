﻿using MassTransit;
using ModelLayer.Models;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRepo
    {
        UserEntity UserRegister(RegisterModel register);

        public string UserLogin(LoginModel login);
        public Task<string> ForgotPassword(ForgotPasswordModel forgotPassword,IBus bus);


    }
}