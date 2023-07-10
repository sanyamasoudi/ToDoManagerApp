using Swagger;
namespace SwaggerClient;
public  static class Methods
{
    public static async Task RegisterAsync(string userName,int passWord)
    {
        using HttpClient httpClient=new HttpClient();
        swaggerClient client=new swaggerClient("http://localhost:5292",httpClient);
        await client.CreatUserAsync(userName,passWord);
    }
    public static async Task<bool> LoginAsync(string userName,int passWord)
    {
        using HttpClient httpClient=new HttpClient();
        swaggerClient client=new swaggerClient("http://localhost:5292",httpClient);
        return await client.IsUserIdentifyAsync(userName,passWord);
    }
    public static async Task ContinueAsync(string Activity,string Food,string Movie,string UserName)
    {
        using HttpClient httpClient=new HttpClient();
        swaggerClient client=new swaggerClient("http://localhost:5292",httpClient);
        await client.InfoToListAsync(Activity,Food,Movie,UserName);
    }
    public static async Task AddTask(string subject, string description, bool isCompleted, string username)
    {
        using HttpClient httpClient=new HttpClient();
        swaggerClient client=new swaggerClient("http://localhost:5292",httpClient);
        await client.CreatToDoAsync(subject,description,isCompleted,username);
    }
    public static async Task DeleteTask(string subject, string description, string username)
    {
        using HttpClient httpClient=new HttpClient();
        swaggerClient client=new swaggerClient("http://localhost:5292",httpClient);
        await client.RemoveToDoAsync(subject,description,username);
    }
    public static async Task<ICollection<ToDoItems>> GetAllTask(string username)
    {
        using HttpClient httpClient=new HttpClient();
        swaggerClient client=new swaggerClient("http://localhost:5292",httpClient);
        return await client.GetToDosAsync(username);
    } 
    public static async Task DeleteAllTask(string username)
    {
        using HttpClient httpClient=new HttpClient();
        swaggerClient client=new swaggerClient("http://localhost:5292",httpClient);
        await client.RemoveAllToDosAsync(username);
    }
    public static async Task MarkTaskAsComplete(string subject, string name)
    {
        using HttpClient httpClient=new HttpClient();
        swaggerClient client=new swaggerClient("http://localhost:5292",httpClient);
        await client.MarkToDoAsCompleteAsync(subject,name); 
    }
    public static async Task MarkTaskAsNotComplete(string subject, string name)
    {
        using HttpClient httpClient=new HttpClient();
        swaggerClient client=new swaggerClient("http://localhost:5292",httpClient);
        await client.MarkToDoAsNotCompleteAsync(subject,name); 
    }
    public static async Task<string> InspireWithMolaviAsync(string Description)
    {
        using HttpClient httpClient=new HttpClient();
        swaggerClient client=new swaggerClient("http://localhost:5292",httpClient);
        return await client.InspireWithPoemAsync(Description);
    }
    public static async Task<string> InspireWithActivityAsync(string UserName,string Description)
    {
        using HttpClient httpClient=new HttpClient();
        swaggerClient client=new swaggerClient("http://localhost:5292",httpClient);
        return await client.InspireActivityAsync(UserName,Description);
    }
    public static async Task<string> InspireWithFoodAsync(string UserName,string Description)
    {
        using HttpClient httpClient=new HttpClient();
        swaggerClient client=new swaggerClient("http://localhost:5292",httpClient);
        return await client.InspireFoodAsync(UserName,Description);
    }
    public static async Task<string> InspireWithMovieAsync(string UserName,string Description)
    {
        using HttpClient httpClient=new HttpClient();
        swaggerClient client=new swaggerClient("http://localhost:5292",httpClient);
        return await client.InspireMovieAsync(UserName,Description);
    }
    public static async Task<string> BreakTaskAsync(string Description,int Duration)
    {
        using HttpClient httpClient=new HttpClient();
        swaggerClient client=new swaggerClient("http://localhost:5292",httpClient);
        return await client.BreakTaskAsync(Description,Duration);
    }
}