using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Task.AI;
using Table ToDoContext=new Table();
CRUD Crud=new CRUD(ToDoContext);

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSignalR(opt => {opt.EnableDetailedErrors = true;});

builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>  
{  
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().WithMethods("POST", "GET").WithHeaders("x-requested-with");
    });
});  
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseCors();


app.MapHub<SynchronizedCollectionHub<ToDoItems>>("/CollectionHub");
app.UseSwagger();
app.UseSwaggerUI();

//User
app.MapPost("/CreatUser",Crud.CreatUser);
app.MapGet("/GetUser",Crud.GetUser);
app.MapGet("/IsUserIdentify",Crud.IsUserIdentify);
//ToDo
app.MapPost("/CreatToDo",Crud.CreatToDo);
app.MapPost("/RemoveToDo",Crud.RemoveToDo);
app.MapPost("/RemoveAllToDos",Crud.RemoveAllToDos);
app.MapPost("/MarkToDoAsComplete",Crud.MarkToDoAsComplete);
app.MapPost("/MarkToDoAsNotComplete",Crud.MarkToDoAsNotComplete);
app.MapGet("/GetToDos",Crud.GetToDos);
app.MapGet("/GetCompletedToDos",Crud.GetCompletedToDos);
app.MapGet("/GetInCompletedToDos",Crud.GetInCompletedToDos);
//Inspire
app.MapGet("/InspireWithPoem",Crud.InspireWithPoem);
app.MapGet("/InspireActivity", Crud.InspireActivity);
app.MapGet("/InspireFood", Crud.InspireFood);
app.MapGet("/InspireMovie", Crud.InspireMovie);
app.MapGet("/BreakTask",Crud.BreakTask);
//Get User Interests
app.MapPost("/InfoToList",Crud.InfoToList);
app.MapGet("/GetUserInFoActivity",Crud.GetUserInFoActivity);
app.MapGet("/GetUserInFoFood",Crud.GetUserInFoFood);
app.MapGet("/GetUserInFoMovie",Crud.GetUserInFoMovie);


app.Run();
