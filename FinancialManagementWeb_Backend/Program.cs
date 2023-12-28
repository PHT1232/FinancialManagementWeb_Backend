using EntityFramework.Repository;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
using ProjectModel.ReceiptComponents;
using ProjectModel.AuthModel;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProjectDbContext>(opts => opts.UseSqlServer(builder.Configuration["ConnectionString:FinancialManagement"]));
builder.Services.AddScoped<IDataRepository<Receipt>, ReceiptRepository>();
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ProjectDbContext>();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); 

app.UseAuthorization();

app.MapControllers();

app.Run();
