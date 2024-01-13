using MassTransit;
using ModelLayer.Models;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRepo
    {
        UserEntity UserRegister(RegisterModel register);

        public string UserLogin(LoginModel login);
        public Task<string> ForgotPassword(string emailTo,IBus bus);

        public ProductEntity AddProduct(ProductModel product);

        public UserEntity GetUsersById(int id);


    }
}