using ExampleApi.DataAccess.UnitOfWork;
using ExampleApi.Models;
using Microsoft.AspNetCore.Mvc;

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

#region UserMethods

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
        return Results.Ok(data.ToList());
    }
});
app.MapPost("AddUser", ([FromBody] User model) =>
{
    using (var uow = new UnitOfWork())
    {
        uow.GetRepository<User>().Add(model);
        if (uow.SaveChanges() > 0) return Results.Ok(model);
        else return Results.StatusCode(500);
    }
});
app.MapPut("UpdateUser", ([FromBody] User model) =>
{
    using (var uow = new UnitOfWork())
    {
        if (model == null) return Results.BadRequest();

        uow.GetRepository<User>().Update(model);
        if (uow.SaveChanges() > 0) return Results.Ok(model);
        else return Results.StatusCode(500);

    }
});
app.MapDelete("DeleteUser", ([FromBody] User model) =>
{
    using (var uow = new UnitOfWork())
    {
        if (model == null) return Results.BadRequest();
        var data = uow.GetRepository<User>().Get(x => x.Id.Equals(model.Id));
        if (data != null)
            uow.GetRepository<User>().Delete(data);
        else return Results.NotFound("Kullanıcı Bulunumadı");
        if (uow.SaveChanges() > 0) return Results.Ok(data);
        return Results.StatusCode(500);
    }
});
#endregion

#region ProductMethods

#endregion

app.Run();
