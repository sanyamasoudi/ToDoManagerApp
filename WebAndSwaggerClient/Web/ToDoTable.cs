using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;


public class ToDoItems
{
    [Key]
    public  int Id{get;set;}
    public string Subject{get;set;}
    public string Description{ get; set; }
    public bool IsCompleted{get;set;}
    public string Username{ get; set; }
    public ToDoItems(string subject,string description,bool isCompleted,string username)
    {
        this.Subject=subject;
        this.Description=description;
        this.IsCompleted=isCompleted;
        this.Username=username;
    }
}
public class User
{
    [Key]
    public string Username { get; set; }
    public int Password { get; set; }
}
public class UserInterestInfo
{
    [Key]
    public int Id { get; set; }
    [NotMapped] //bacause for each list i should define primarykey it is an easy soloution to i dont get error
    public List<string> Activity { get; set; } = new List<string>();
    [NotMapped]
    public List<string> Food { get; set; } = new List<string>();
    [NotMapped]
    public List<string> Movie { get; set; } = new List<string>();
    public string Username { get; set; }

    public UserInterestInfo() { }

    public UserInterestInfo(ICollection<string> listA, ICollection<string> listF, ICollection<string> listM, string username)
    {
        this.Activity = listA.ToList();
        this.Food = listF.ToList();
        this.Movie = listM.ToList();
        this.Username = username;
    }
}
public class ChatGPTResponse
{
    [Key]
    public int Id { get; set; }
    public string Interest{ get; set; }
    public string Name{ get; set; }
    public string Response{ get; set; }
    public string UName{ get; set; }
    public ChatGPTResponse()
    { }
    public ChatGPTResponse(string interest,string name,string response,string susername)
    {
        this.Interest=interest;
        this.Name=name;
        this.Response=response;
        this.UName=susername;
    }
}
public class Table:DbContext
{
    public DbSet<ToDoItems> ToDoWorks { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserInterestInfo> UserInterests { get; set; }
    public DbSet<ChatGPTResponse> DChatGPTResponse { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

        optionsBuilder.UseSqlite(@"Data Source=data.db");

    } 
}