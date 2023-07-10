using System.Threading;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MVVM;
using SwaggerClient;
using Swagger;

namespace WpfPro
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();//this method put the first data from database into my SyncronizeCollection
            InitialCollection();
            DataGridShow.DataContext = SCollection.ToDoCollection;
            DataGridShow.CanUserAddRows=false;//for datagrid to dont show me additional Row
            this.DataContext=this;
        }
        public async void InitialCollection()
        {
            try
            {
                var response=await SwaggerClient.Methods.GetAllTask(UserNameSaver.UserName);
                foreach(var i in response)
                    SCollection.ToDoCollection.Add(new ToDoItems{Subject=i.Subject,Description=i.Description,IsCompleted=i.IsCompleted});
            }
            catch(ApiException)
            { MessageBox.Show("An error occured please try again", "ERRORE", MessageBoxButton.OK, MessageBoxImage.Error);}
        }
        public void ShowToDo_Click(object sender, RoutedEventArgs e)
        {
            DataGridShow.Width=361;
            CloseDataGridButton.Width=48;
        }
        public async void CloseDataGridButton_Click(object sender, RoutedEventArgs e)
        {
            DataGridShow.Width=0;
            CloseDataGridButton.Width=0;
            //this part if user changed the datagrid..the the database is changed also
            await SwaggerClient.Methods.DeleteAllTask(UserNameSaver.UserName);
            foreach(var i in SCollection.ToDoCollection)
                await SwaggerClient.Methods.AddTask(i.Subject,i.Description,i.IsCompleted,UserNameSaver.UserName);
            
        }
        public async void AddToDo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SCollection.SendNotify(UserNameSaver.UserName);
                SCollection.Add(new ToDoItems{Subject=this.NameBox,Description=this.DescriptionBox,IsCompleted=false},UserNameSaver.UserName);
                await SwaggerClient.Methods.AddTask(this.NameBox,this.DescriptionBox,false,UserNameSaver.UserName);
                
                var view = CollectionViewSource.GetDefaultView(SCollection.ToDoCollection);
                view.Refresh();//is usefull to dont close and again open table..the changed received in table rapidly
                MessageBox.Show("Successfully Added");
            }
            catch(ApiException)
            { MessageBox.Show("An error occured please try again", "ERRORE", MessageBoxButton.OK, MessageBoxImage.Error);}
        }
        public async void DeleteToDo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = SCollection.ToDoCollection.FirstOrDefault(i => i.Subject == this.NameBox && i.Description == this.DescriptionBox);
                if (item != null)
                {
                    SCollection.SendNotify(UserNameSaver.UserName);
                    SCollection.RemoveTask(SCollection.ToDoCollection.IndexOf(item), UserNameSaver.UserName);
                    await SwaggerClient.Methods.DeleteTask(item.Subject, item.Description, UserNameSaver.UserName);
                    
                    var view = CollectionViewSource.GetDefaultView(SCollection.ToDoCollection);
                    view.Refresh();
                    MessageBox.Show("Successfully Deleted");
                }
                else
                {
                    MessageBox.Show("Item not found.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch(ApiException)
            { MessageBox.Show("An error occured please try again", "ERRORE", MessageBoxButton.OK, MessageBoxImage.Error);}
        }
        public void ChatGPT_Click(object sender, RoutedEventArgs e)
        {
            ProfileWindow pw = new ProfileWindow();
            this.Close();
            pw.Show();
        }
        public MVVM.SynchronizedCollection<ToDoItems> SCollection=new MVVM.SynchronizedCollection<ToDoItems>("http://localhost:5292/CollectionHub",UserNameSaver.UserName);
        public string NameBox{get;set;}
        public string DescriptionBox{get;set;}
    }
}
