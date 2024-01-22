using MassTransit;
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


        public UserEntity GetUsersById(int id);
        public List<UserEntity> GetAllUsers();

        public UserEntity UpdateUser(int userid, RegisterModel updateproperties);
        public string DeleteUser(int id);
        public UserEntity GetUserByName(string userName);

        public UserEntity NewUserUpdate(RegisterModel userinfo);

        public List<UserEntity> GetPersonByAlphabet(string name);

        public string ResetUserPassword(int userId, string email, string password, string confirm_Passoword);


    }
}
