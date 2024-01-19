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

        public UserEntity GetUsersById(int id);

        public List<UserEntity> GetAllUsers();

        public UserEntity UpdateUser(int userid, RegisterModel updateproperties);
        public string DeleteUser(int id);

        public UserEntity GetUserByName(string userName);


    }
}