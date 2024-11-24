
using DVDRetal.DataBase;
using DVDRetal.IRepository;
using DVDRetal.IService;
using DVDRetal.Repository;
using DVDRetal.Service;
using Microsoft.EntityFrameworkCore;

namespace DVDRetal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseWebRoot("wwwroot");


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<DVDContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<ICustomerService,CustomerService>();
            builder.Services.AddScoped<IManagerService,ManagerService>();
            builder.Services.AddScoped<IRentalService,RentalService>();

            builder.Services.AddScoped<ICustomerRepository,CustomerRepository>();
            builder.Services.AddScoped<IManagerRepository,ManagerRepository>();
            builder.Services.AddScoped<IRentalRepository,RentalRepository>();

            builder.Services.AddCors(opt => opt.AddPolicy(
    name: "CORSOpenPolicy",
    builder =>
    {
        builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
    }));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("CORSOpenPolicy");

            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
