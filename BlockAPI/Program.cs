
using SmartContractLib.Data;
using SmartContractLib.Services;
using System.Net.NetworkInformation;

namespace BlockAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(policy => policy.AddPolicy("open", opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
            builder.Services.AddResponseCaching();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ContractService>();
            builder.Services.AddSingleton<AppSettingModel>();
            builder.Services.AddHealthChecks();
            var app = builder.Build();
            using (var serviceScope = app.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                var myappdep = services.GetRequiredService<AppSettingModel>();
         
          //      myappdep.ContractPath = builder.Configuration.GetConnectionString("cn");
            //    myappdep.ConLog = builder.Configuration.GetConnectionString("cnservice");
                myappdep.ContractPath = builder.Configuration.GetValue<string>("ContractPath");
             
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("open");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            app.MapHealthChecks("/health").AllowAnonymous();
            app.UseResponseCaching();
            app.Run();
        }
    }
}