using ModelLayer.Models;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRepo
    {
        UserEntity UserRegister(RegisterModel register);

        //UserEntity UserLogin(LoginModel login);
        UserEntity GetUserById(int id);
    }
}