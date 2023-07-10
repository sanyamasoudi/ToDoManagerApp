namespace MVVM.Test;
using Moq;
using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
[TestClass]
public class SynchronizedCollectionHubTests
{
    [TestMethod]
    public void SendNotificationToClients()
    {
        var mockIHubClients = new Mock<IHubCallerClients>(MockBehavior.Strict);
        var mockIClientProxy = new Mock<IClientProxy>();
        mockIHubClients.SetupGet(c => c.All).Returns(mockIClientProxy.Object);
        var hub = new SynchronizedCollectionHub<ToDoItems>{Clients = mockIHubClients.Object};

        string expectedUsername = "testUser";

        hub.SendNotificationToClients(expectedUsername);

        var objArr=new object[]{expectedUsername};
        mockIClientProxy.Verify(c => c.SendCoreAsync("ReceiveNotification",objArr, CancellationToken.None), Times.Once);
    }
        [TestMethod]
        public void AddToClients()
        {
            var mockIHubClients = new Mock<IHubCallerClients>(MockBehavior.Strict);
            var mockIClientProxy = new Mock<IClientProxy>();
            mockIHubClients.SetupGet(c => c.All).Returns(mockIClientProxy.Object);
            var hub = new SynchronizedCollectionHub<ToDoItems>{Clients = mockIHubClients.Object};
    
            ToDoItems expectedItem = new ToDoItems{Subject="SubjectTest",Description="DescriptionTest",IsCompleted=false};
            string expectedUsername = "testUser";

            hub.AddToClients(expectedItem, expectedUsername);

            mockIHubClients.Verify(c=>c.All,Times.Once);
            var objArr=new object[]{expectedItem,expectedUsername};
            mockIClientProxy.Verify(c => c.SendCoreAsync("OnAdd",objArr,CancellationToken.None), Times.Once);
        }
        [TestMethod]
        public void RemoveFromClients()
        {
            var mockIHubClients = new Mock<IHubCallerClients>(MockBehavior.Strict);
            var mockIClientProxy = new Mock<IClientProxy>();
            mockIHubClients.SetupGet(c => c.All).Returns(mockIClientProxy.Object);
            var hub = new SynchronizedCollectionHub<ToDoItems>{Clients = mockIHubClients.Object};
    
            ToDoItems expectedItem = new ToDoItems{Subject="SubjectTest",Description="DescriptionTest",IsCompleted=false};
            string expectedUsername = "testUser";

            hub.AddToClients(expectedItem, expectedUsername);
            hub.RemoveFromClients(0,expectedUsername);

            var objArr=new object[]{0,expectedUsername};
            mockIClientProxy.Verify(c => c.SendCoreAsync("OnRemove",objArr,CancellationToken.None), Times.Once);
        }
        [TestMethod]
        public void UpdateToClients()
        {
            var mockIHubClients = new Mock<IHubCallerClients>(MockBehavior.Strict);
            var mockIClientProxy = new Mock<IClientProxy>();
            mockIHubClients.SetupGet(c => c.All).Returns(mockIClientProxy.Object);
            var hub = new SynchronizedCollectionHub<ToDoItems>{Clients = mockIHubClients.Object};
    
            var updateCollection=new System.Collections.ObjectModel.ObservableCollection<ToDoItems>();
            updateCollection.Add(new ToDoItems{Subject="UpdateSubject",Description="UpdateDescription",IsCompleted=false});
            string expectedUsername = "testUser";
            hub.UpdateToClients(updateCollection,expectedUsername);

            mockIHubClients.Verify(c=>c.All,Times.Once);
            var objArr=new object[]{updateCollection,expectedUsername};
            mockIClientProxy.Verify(c => c.SendCoreAsync("OnUpdate",objArr,CancellationToken.None), Times.Once);
        }

}

