using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ProgramDbContext>(options =>
    options.UseMySql("Server=localhost;Database=mindfulme;User=root;Password=12Cjkysirj90;Port=3306;",
        new MySqlServerVersion(new Version(8, 0, 33))));
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAny", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAny");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
