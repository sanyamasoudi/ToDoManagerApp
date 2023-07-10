using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.Json;
using Newtonsoft.Json;
using MVVM;
using System.Threading.Tasks;
using SwaggerClient;
using Swagger;

namespace WpfPro
{
    public partial class ChatGPTWindow : Window
    {
        public ChatGPTWindow()
        {
            InitializeComponent();
            this.DataContext=this;
        }

        public async void InspireForTaskWithPoem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show(await SwaggerClient.Methods.InspireWithMolaviAsync(this.TodoDescription));
            }
            catch(ApiException) 
            {MessageBox.Show("An error occured please try again", "ERRORE", MessageBoxButton.OK, MessageBoxImage.Error);}
        }
        public async void InspireWithFoods_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show(await SwaggerClient.Methods.InspireWithFoodAsync(UserNameSaver.UserName,this.TodoDescription));
            }
            catch(ApiException) 
            {MessageBox.Show("An error occured please try again", "ERRORE", MessageBoxButton.OK, MessageBoxImage.Error);}
        }
        public async void InspireWithMovies_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show(await SwaggerClient.Methods.InspireWithMovieAsync(UserNameSaver.UserName,this.TodoDescription));
            }
            catch(ApiException) 
            {MessageBox.Show("An error occured please try again", "ERRORE", MessageBoxButton.OK, MessageBoxImage.Error);}
        }
        public async void InspireWithActivities_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show(await SwaggerClient.Methods.InspireWithActivityAsync(UserNameSaver.UserName,this.TodoDescription));
            }
            catch(ApiException) 
            {MessageBox.Show("An error occured please try again", "ERRORE", MessageBoxButton.OK, MessageBoxImage.Error);}
        }
        public async void BreakTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show(await SwaggerClient.Methods.BreakTaskAsync(this.TodoDescription,this.Duration));
            }
            catch(ApiException) 
            {MessageBox.Show("An error occured please try again", "ERRORE", MessageBoxButton.OK, MessageBoxImage.Error);}
        }
        public string TodoDescription{get;set;}
        string _Duration;
        public int Duration
        {
            get=>int.Parse(_Duration);
            set
            {
                _Duration=value.ToString();
            }
        }
    }
}

