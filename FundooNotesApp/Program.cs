using BusinessLayer.Interface;
using BusinessLayer.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;

namespace FundooNotesApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddTransient<IUserBusiness, UserBusiness>();
            builder.Services.AddTransient<IUserRepo, UserRepo>();
            builder.Services.AddTransient<IUserBusinessNotes, UserNotesBusiness>();
            builder.Services.AddTransient<IUserNotesRepo, UserNotesRepo>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //database connection
            builder.Services.AddDbContext<FundooContext>(
                x => x.UseSqlServer(builder.Configuration["connectionString:FundooDB"]));

            //rabbit mq registration code
            builder.Services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host(new Uri("rabbitmq://localhost"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                }));
            });
            //// Add MassTransit hosted service for health checks
            //builder.Services.AddMassTransitHostedService();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
