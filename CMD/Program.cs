using System.Collections.ObjectModel;
using System;
class Program
{
    public static MVVM.SynchronizedCollection<ToDoItems> SCollection;
    static async Task Main()
    {
        Console.WriteLine("Hello,Welcome To ToDoManager\nPlease Select And Write Your Command");
        var color=Console.ForegroundColor;
        Console.ForegroundColor=ConsoleColor.Green;
        Console.WriteLine("Register");Console.WriteLine("Login");
        Console.ForegroundColor=color;
        var reed=Console.ReadLine();
        
        if(reed=="Login")
        {
            Console.WriteLine("\nPlease Write Your Info");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("UserName:"); string UserName = Console.ReadLine();
            Console.Write("PassWord:"); int PassWord = int.Parse(Console.ReadLine());
            SCollection=new MVVM.SynchronizedCollection<ToDoItems>("http://localhost:5292/CollectionHub",UserName);
            Console.ForegroundColor = color;
            try
            {
                var IsLogined = await SwaggerClient.Methods.LoginAsync(UserName, PassWord);
                await Process(UserName, IsLogined);
                await ProcessCommand(UserName, IsLogined);//for specify that user want continue or not
            }
            catch(HttpRequestException){ShowErrorMsg("An error occured please try again");}
        }
        if(reed=="Register")
        {
            Console.WriteLine("\nPlease Write Your Info");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("UserName:"); string UserName = Console.ReadLine();
            Console.Write("PassWord:"); int PassWord = int.Parse(Console.ReadLine());
            SCollection=new MVVM.SynchronizedCollection<ToDoItems>("http://localhost:5292/CollectionHub",UserName);
            Console.ForegroundColor = color;
            try
            {
                await SwaggerClient.Methods.RegisterAsync(UserName,PassWord);
                SuccessAction();
                await ProcessCommand(UserName,true);
            }
            catch(HttpRequestException){ShowErrorMsg("An error occured please try again");}
        }
    }
    private static async Task ProcessCommand(string UserName, bool IsLogined)
    {
        while(true)
        {
            Console.WriteLine("\ndo you want to continue?");
            Console.ForegroundColor=ConsoleColor.Green;
            Console.WriteLine("Yes");Console.WriteLine("No");
            Console.ForegroundColor=ConsoleColor.White;
            var Continue = Console.ReadLine();
            if(Continue=="Yes") await Process(UserName,IsLogined);
            else break;
        }


    }
    private static async Task Process(string UserName, bool IsLogined)
    {
        if (IsLogined)
        {
            await InitialCollection(UserName);//first initial SyncronizeCollection with data in database
            Console.WriteLine("\nPlease Select And Write Your Command");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Add"); Console.WriteLine("Remove"); Console.WriteLine("Show");Console.WriteLine("MarkAsComplete");Console.WriteLine("ChatGPT");
            Console.ForegroundColor = ConsoleColor.White;
            switch (Console.ReadLine())
            {
                case "Add":
                    Console.WriteLine("\nPlease Select And Write Your Command");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Subject:"); var Subject = Console.ReadLine();
                    Console.Write("Description:"); var Description = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    try
                    {
                        SCollection.SendNotify(UserName);
                        SCollection.Add(new ToDoItems{Subject=Subject,Description=Description,IsCompleted=false},UserName);
                        await SwaggerClient.Methods.AddTask(Subject,Description,false,UserName);
                        SuccessAction();
                    }
                    catch(HttpRequestException){ShowErrorMsg("An error occured please try again");}
                    break;
                case "Remove":
                    Console.WriteLine("\nPlease Select And Write Your Command");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Subject:"); var SubjectR = Console.ReadLine();
                    Console.Write("Description:"); var DescriptionR = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    try
                    {
                        var item = SCollection.ToDoCollection.FirstOrDefault(i => i.Subject==SubjectR && i.Description ==DescriptionR);
                        if(item!=null)
                        {
                            SCollection.SendNotify(UserName);
                            SCollection.RemoveTask(SCollection.ToDoCollection.IndexOf(item), UserName);
                            await SwaggerClient.Methods.DeleteTask(SubjectR,DescriptionR,UserName);
                            SuccessAction();
                        }
                        else
                            ShowErrorMsg("Item not found");
                    }
                    catch(HttpRequestException){ShowErrorMsg("An error occured please try again");}
                    break;
                case "MarkAsComplete":
                    Console.WriteLine("\nPlease Select And Write Your Command");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Subject:"); var SubjectM = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    try
                    {
                        await SwaggerClient.Methods.MarkTaskAsComplete(SubjectM,UserName);
                        SCollection.ToDoCollection.FirstOrDefault(i => i.Subject==SubjectM).IsCompleted=true;
                        SCollection.Update(SCollection.ToDoCollection,UserName);        
                        SCollection.SendNotify(UserName);
                        SuccessAction();
                    }
                    catch(HttpRequestException){ShowErrorMsg("An error occured please try again");}
                    break;
                case "Show":
                    Console.WriteLine();
                    Console.WriteLine("Subject Description IsCompleted");
                    SCollection.ToDoCollection.ToList().ForEach(i => Console.WriteLine($"{i.Subject} {i.Description} {i.IsCompleted}"));
                    break;
                case "ChatGPT":
                    Console.WriteLine("\nPlease Update Your Profile To Continue");
                    Console.ForegroundColor=ConsoleColor.Green;
                    Console.WriteLine("Your Favorite Activity:");var ActivityBox=Console.ReadLine();
                    Console.WriteLine("\nYour Favorite Food:");var FoodBox=Console.ReadLine();
                    Console.WriteLine("\nYour Favorite Movie:");var MovieBox=Console.ReadLine();
                    Console.ForegroundColor=ConsoleColor.White;
                    try
                    {
                        await SwaggerClient.Methods.ContinueAsync(ActivityBox,FoodBox,MovieBox,UserName);
                        await ChatGPTCommand(UserName);
                    }
                    catch(Exception) {ShowErrorMsg("Please Answer All Of Required Questions To Continue");}
                    await ProcessCommand(UserName,true);
                    break;
            }
        }
        else ShowErrorMsg("Invalid username or password\nPlease Register First");
    }
    private static async Task ChatGPTCommand(string UserName)
    {
        Console.Write("\nPlease Write Task Description:");
        var TodoDescription=Console.ReadLine();
        Console.WriteLine("\nPlease Select And Write Your Command");
        Console.ForegroundColor=ConsoleColor.Green;
        Console.WriteLine("Inspire With Activity");
        Console.WriteLine("Inspire With Foods");
        Console.WriteLine("Inspire With Movies");
        Console.WriteLine("Inspire With Molavi");
        Console.WriteLine("Break Task");
        Console.ForegroundColor=ConsoleColor.White;
        var inpt=Console.ReadLine();
        switch(inpt)
        {
            case("Inspire With Activity"):
                Console.WriteLine(await SwaggerClient.Methods.InspireWithActivityAsync(UserName,TodoDescription));
                break;
            case("Inspire With Foods"):
                Console.WriteLine(await SwaggerClient.Methods.InspireWithFoodAsync(UserName,TodoDescription));
                break;
            case("Inspire With Movies"):
                Console.WriteLine(await SwaggerClient.Methods.InspireWithMovieAsync(UserName,TodoDescription));
                break;
            case("Inspire With Molavi"):
                Console.WriteLine(await SwaggerClient.Methods.InspireWithMolaviAsync(TodoDescription));
                break;
            case("Break Task"):
                Console.Write("\nDuration:");var Duration=int.Parse(Console.ReadLine());
                Console.WriteLine(await SwaggerClient.Methods.BreakTaskAsync(TodoDescription,Duration));
                break;
        }
    }
    private static async Task InitialCollection(string UserName)
    {
        SCollection.ToDoCollection.Clear();
        try
        {
            var response=await SwaggerClient.Methods.GetAllTask(UserName);
            foreach(var i in response)
            {
                SCollection.ToDoCollection.Add(new ToDoItems{Subject=i.Subject,Description=i.Description,IsCompleted=i.IsCompleted});
            }
        }
        catch(HttpRequestException){ShowErrorMsg("An error occured please try again");}  
    }
    private static void ShowErrorMsg(string errorMsg)
    {
        var c=Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(errorMsg);
        Console.ForegroundColor = c;
    }
    private static void SuccessAction()
    {
        var c=Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("The process was SuccessFull");
        Console.ForegroundColor = c;
    }
}
public class ToDoItems
{
    public string Subject{get;set;}
    public string Description{ get; set; }
    public bool IsCompleted{get;set;}
}