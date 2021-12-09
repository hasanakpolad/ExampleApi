using ExampleApi.DataAccess.UnitOfWork;
using ExampleApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MapGet("GetUserById/{id}", (int id) =>
 {
     using (var uow = new UnitOfWork())
     {
         var data = uow.GetRepository<User>().Get(x => x.Id.Equals(id));
         if (data != null) return Results.Ok(data);
         else return Results.NotFound("Veri bulunamadı.");
     }
 });
app.MapGet("GetAllUsers", () =>
{
    using (var uow = new UnitOfWork())
    {
        var data = uow.GetRepository<User>().GetAll();
        return Results.Ok(data);
    }
});

app.Run();
