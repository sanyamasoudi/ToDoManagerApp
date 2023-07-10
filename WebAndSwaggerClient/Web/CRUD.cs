using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task.AI;
public class CRUD
{

    public Table Context{get;set;}
    public CRUD(Table Context)
    {
        this.Context=Context;
    }
    //UserManager
    public void CreatUser(string username,int password)
    {
        Context.Users.Add(new User{Username=username,Password=password});
        Context.SaveChanges();
    }
    public User[] GetUser()=>Context.Users.ToArray();
    public bool IsUserIdentify(string username,int password)
    {
        foreach(var user in GetUser())
        {
            if(user.Username==username && user.Password==password)
                return true;
        }
        return false;
    }
    //ToDoManager
    public List<ToDoItems> GetToDos(string username)
    {
        return Context.ToDoWorks
            .Where(t => t.Username == username)
            .ToList();
    }
    public void CreatToDo(string subject,string description,bool isCompleted,string username)
    {
        var t=new ToDoItems(subject,description,isCompleted,username);
        Context.ToDoWorks.Add(t);
        Context.SaveChanges();
    }
    public void RemoveAllToDos(string username)
    {
        var AllToDos=
        Context.ToDoWorks.ToList()
        .FindAll(f=>f.Username==username);
        if(AllToDos.Count!=0)
        {
            foreach(var i in AllToDos)
            Context.ToDoWorks.Remove(i);
        }
        Context.SaveChanges();
    }
    public void RemoveToDo(string subject,string description,string username)
    {
        var t=
        Context.ToDoWorks.ToList()
        .Find(f=>f.Subject==subject && f.Description==description && f.Username==username);
        if(t!=null)
            Context.ToDoWorks.Remove(t);
        Context.SaveChanges();
    }
    public void MarkToDoAsComplete(string subject,string name)
    {
        var result=GetToDos(name).Find(p=>p.Subject.ToLower()==subject.ToLower());
        if(result!=null) result.IsCompleted=true;
        Context.SaveChanges();
    }
    public void MarkToDoAsNotComplete(string subject,string name)
    {
        var result=GetToDos(name).Find(p=>p.Subject.ToLower()==subject.ToLower());
        if(result!=null) result.IsCompleted=false;
        Context.SaveChanges();
    }
    //this part i dont use it in my Clients..is for to better see result
    public List<ToDoItems> GetCompletedToDos(string name)=>
    GetToDos(name)
    .FindAll(p=>p.IsCompleted==true && p.Username==name);
    public List<ToDoItems> GetInCompletedToDos(string name)=>
    GetToDos(name)
    .FindAll(p=>p.IsCompleted==false && p.Username==name);

    //InspireManager
    public async Task<string> Inspire(Func<string,List<string>> fn,string username,string name,string SubjectOfRandomItem)
    {
        var ListObj=fn(username);
        string randomItem = GetRandomString(ListObj);
        var t=Context.DChatGPTResponse.ToList().Find(f=>f.Interest==randomItem && f.Name==name && f.UName==username);
        if(t==null)
        {
            var result =await Task.AI.InspireForTask.Withinterest(SubjectOfRandomItem,randomItem, name);
            var cht=new ChatGPTResponse(randomItem,name,result,username);
            Context.DChatGPTResponse.Add(cht);
            Context.SaveChanges();
            return cht.Response;
        }
        return t.Response;
    }
    public string GetRandomString(List<string> List)
    {
        Random rand = new Random();
        int index = rand.Next(List.Count);
        return List[index];
    }
    public async Task<string> InspireWithPoem(string ToDo)
    {
        var t=Context.DChatGPTResponse.ToList().Find(f=>f.Interest=="Poem" && f.Name==ToDo);
        if(t==null)
        {
            var result=await Task.AI.InspireForTask.WithPoem(ToDo);
            var cht=new ChatGPTResponse("Poem",ToDo,result,null);
            Context.DChatGPTResponse.Add(cht);
            Context.SaveChanges();
            return cht.Response;
        }
        return t.Response;
    }
    public Task<string> InspireActivity(string username, string name)=>Inspire(GetUserInFoActivity,username,name,"");

    public Task<string> InspireFood(string username, string name)=> Inspire(GetUserInFoFood,username,name,"food");

    public Task<string> InspireMovie(string username, string name)=>Inspire(GetUserInFoMovie,username,name,"movie");

    public void InfoToList(string lineA,string LineF,string LineM,string username)
    {
        var interest=new UserInterestInfo(lineA.Split(","),LineF.Split(","),LineM.Split(","),username);
        var IsThereAnother=Context.UserInterests.ToList().Find(f=>f.Username==username);
        if(IsThereAnother!=null)
        {
            Context.UserInterests.Remove(IsThereAnother);
            Context.SaveChanges();
        }
        Context.UserInterests.Add(interest);
        Context.SaveChanges();
    }
    public async Task<string> BreakTask(string taskName, int subTaskMinute)
    {
        var t=Context.DChatGPTResponse.ToList().Find(f=>f.Interest==$"{subTaskMinute}" && f.Name==taskName);
        if(t==null)
        {
            var info = await Task.AI.InspireForTask.WithBreakTask(taskName, subTaskMinute);
            var cht=new ChatGPTResponse($"{subTaskMinute}",taskName,info,"");
            Context.DChatGPTResponse.Add(cht);
            Context.SaveChanges();
            return info;
        }
        return t.Response;
    }
    //helper method for inspire
    public List<string> GetUserInFoActivity(string username)=>
    Context.UserInterests.ToList().FirstOrDefault(i=>i.Username==username).Activity;
    public List<string> GetUserInFoFood(string username)=>
    Context.UserInterests.ToList().FirstOrDefault(i=>i.Username==username).Food;
    public List<string> GetUserInFoMovie(string username)=>
    Context.UserInterests.ToList().FirstOrDefault(i=>i.Username==username).Movie;



}