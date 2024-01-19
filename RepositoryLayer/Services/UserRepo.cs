using MassTransit;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using ModelLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RepositoryLayer.Services
{
    public class UserRepo : IUserRepo
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;
        public UserRepo(FundooContext fundooContext,IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }

        public UserEntity UserRegister(RegisterModel register)
        {
            UserEntity entity = new UserEntity();
            entity.FirstName = register.FirstName;
            entity.LastName = register.LastName;
            entity.Email = register.Email;
            entity.Password = EncryptPassword(register.Password);
            fundooContext.Users.Add(entity);
            fundooContext.SaveChanges();
            return entity;

        }

        //public UserEntity GetUserById(int id)
        //{
        //    UserEntity userdet = fundooContext.Users.FirstOrDefault(u => u.UserId == id);
        //    return userdet;
           
        //}

        public static string EncryptPassword(string password)
        {
            try
            {
                byte[] encrypt_password=new byte[password.Length];
                encrypt_password=Encoding.UTF8.GetBytes(password);
                string encodedPassword = Convert.ToBase64String(encrypt_password);
                return encodedPassword;

            }
            catch(Exception ex)
            {
                return $"Encryption Failed.! {ex.Message}";
            }
        }

        public static string DecryptPassword(string encryptedPassword)
        {
            try
            {
                byte[] decrypt_password = Convert.FromBase64String(encryptedPassword);
                string originalPassword = Encoding.UTF8.GetString(decrypt_password);
                return originalPassword;
            }
            catch (Exception ex)
            {
                return $"Decryption Failed.! {ex.Message}";
            }
        }


        public string UserLogin(LoginModel login)
        {
            // Fetch user by email from the database
            var userFromDb = fundooContext.Users.FirstOrDefault(x => x.Email == login.Email);

            if (userFromDb != null && DecryptPassword(userFromDb.Password) == login.Password)
            {
                var token = GenerateToken(userFromDb.Email, userFromDb.UserId);
                return token;
            }
            else
            {
                return null;
            }
        }


        public string GenerateToken(string email, int userId)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email",email),
                new Claim("UserId",userId.ToString())
            };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(5),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<string> ForgotPassword(string emailTo,IBus bus)
        {
            try
            {
                if (string.IsNullOrEmpty(emailTo))
                {
                    return null;
                }
                else
                {
                    var result=fundooContext.Users.FirstOrDefault(x => x.Email == emailTo);
                    if (result==null)
                    {
                        return null;
                    }
                    else
                    {
                        var tkn = GenerateToken(emailTo, result.UserId);
                        Sent sent = new Sent();
                        sent.SendMessage(emailTo,tkn);

                        Uri uri = new Uri("rabbitmq://localhost/NotesEmail_Queue");
                        var endpoint = await bus.GetSendEndpoint(uri);
                        return "Message Sent Successfull";

                    }
                   
                }
                
            }
            catch (Exception e)
            {

                return e.Message;
            }
           
        }
   


        public UserEntity GetUsersById(int id)
        {
            UserEntity user = fundooContext.Users.FirstOrDefault(x => x.UserId == id);
            return user;
        }


        public List<UserEntity> GetAllUsers()
        {
            var users= new List<UserEntity>();
            users=fundooContext.Users.ToList();
            return users;
        }

        public UserEntity UpdateUser(int userid, RegisterModel updateproperties) 
        {
            var existingUser = fundooContext.Users.Find(userid);
            if(existingUser != null)
            {
                existingUser.FirstName = updateproperties.FirstName ?? existingUser.FirstName;
                existingUser.LastName= updateproperties.LastName ?? existingUser.LastName;
                existingUser.Email= updateproperties.Email ?? existingUser.Email;
                existingUser.Password= updateproperties.Password ?? existingUser.Password;
                fundooContext.SaveChanges();
                return existingUser;
            }
            else
            {
                return null;
            }
        }

        public string DeleteUser(int id)
        {
            var deleteUser = fundooContext.Users.Find(id);
            if(deleteUser != null)
            {
                fundooContext.Users.Remove(deleteUser);
                fundooContext.SaveChanges();
                return "User Deleted Successfully";
            }
            else
            {
                return "Invaid UserId";
            }
        }

        public UserEntity GetUserByName(string userName)
        {
            var UserDetails=fundooContext.Users.FirstOrDefault(x=>x.FirstName==userName);
            if(UserDetails != null)
            {
                return UserDetails;
            }
            else
            {
                return null;
            }
        }
    }
}
