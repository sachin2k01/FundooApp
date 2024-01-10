using ModelLayer.Models;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRepo
    {
        UserEntity UserRegister(RegisterModel register);
    }
}