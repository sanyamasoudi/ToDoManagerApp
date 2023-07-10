using System.Windows;
public class ToDoItems
{
    public string Subject{get;set;}
    public string Description{ get; set; }
    public bool IsCompleted{get;set;}
}
public static class UserNameSaver
{
    public static string UserName;
}