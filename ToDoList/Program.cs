using Microsoft.EntityFrameworkCore;
using ToDoList.EfCore;
using ToDoList.Controllers;
using ToDoList.Services;
using ToDoList.GlobalErrorHandling;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToDoList.Models.Utility;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.WithOrigins("http://localhost:4200")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                          });
});

builder.Services.AddDbContext<ToDoContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoContext"), x => x.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery));
});

//To confirgure Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// To configiure Jwt Bearer
.AddJwtBearer(options =>
 {
     options.SaveToken = true;
     options.RequireHttpsMetadata = false;
     options.TokenValidationParameters = new TokenValidationParameters()
     {
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("4e9f5a0824554525bbf35490d8da48f2")),
         ValidateIssuerSigningKey = true,
         ValidateIssuer = false,
         ValidateAudience = false,
     };
 });

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserTaskService, UserTaskService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IExceptionLoggerService, ExceptionLoggerService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<CliamService>();



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //var logger = app.Services.GetRequiredService<ILogger<ExceptionHandlerMiddleware>>();
    //app.ConfigureExceptionHandler(logger);
    
}
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();
app.UseAuthentication();    
app.UseAuthorization();
//Custom middleware exception logger;
app.UseLogException();
app.MapControllers();

app.Run();
