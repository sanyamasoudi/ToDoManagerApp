using System.Collections.ObjectModel;
namespace MVVM;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

public class SynchronizedCollection<T>
{
    public HubConnection hubConnection{get;set;}
    public string ServerAddress{get;set;}
    public string UserName{get;set;}
    public ObservableCollection<T> ToDoCollection{get;set;}

    public SynchronizedCollection(string serverAddress,string userName)
    {
        this.ServerAddress=serverAddress;
        this.UserName=userName;
        ToDoCollection=new ObservableCollection<T>();
        hubConnection=new HubConnectionBuilder().WithUrl(ServerAddress).Build();

        hubConnection.StartAsync();
        
        hubConnection.On("ReceiveNotification",(string Username)=>
        {
            if(this.UserName==Username)
                Console.WriteLine("An update occured");
        });
        hubConnection.On("OnAdd",(T item,string Username)=>
        {
            if(this.UserName==Username)
                ToDoCollection.Add(item);

        });
        hubConnection.On("OnRemove",(int item,string Username)=>
        {
            if(this.UserName==Username)
                ToDoCollection.RemoveAt(item);
        });
        hubConnection.On("OnUpdate",(ObservableCollection<T> update,string Username)=>
        {

            if(this.UserName==Username)
            {
                ToDoCollection.Clear();
                foreach(var i in update)
                    ToDoCollection.Add(i);
            }
        });
        
    }
    public void SendNotify(string Username)
    {
        hubConnection.InvokeAsync("SendNotificationToClients", Username);

    }
    public void Add(T item,string Username)
    {
        hubConnection.InvokeAsync("AddToClients",item,Username);

    }
    public void RemoveTask(int item,string Username)
    {
        hubConnection.InvokeAsync("RemoveFromClients",item,Username);

    }
    public void Update(ObservableCollection<T> update,string Username)
    {
        hubConnection.InvokeAsync("UpdateToClients",update,Username);

    }

}
