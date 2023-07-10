using System.Collections.ObjectModel;
using Microsoft.AspNetCore.SignalR;

public class SynchronizedCollectionHub<T> :Hub
{
    public async void SendNotificationToClients(string Username)
    {
        await Clients.All.SendAsync("ReceiveNotification",Username);
    }
    public async void AddToClients(T item,string Username)
    {
        await Clients.All.SendAsync("OnAdd",item,Username);
    }
    public async void RemoveFromClients(int item,string Username)
    {
        await Clients.All.SendAsync("OnRemove",item,Username);
    }
    public async void UpdateToClients(ObservableCollection<T> update,string Username)
    {
        await Clients.All.SendAsync("OnUpdate", update,Username);
    }

}