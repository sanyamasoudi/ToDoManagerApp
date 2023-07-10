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
using SwaggerClient;
using Swagger;

namespace WpfPro
{
    public partial class ProfileWindow : Window
    {
        public ProfileWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public async void Continue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await SwaggerClient.Methods.ContinueAsync(ActivityBox,FoodBox,MovieBox,UserNameSaver.UserName);//save interests of user
                var cw=new ChatGPTWindow();
                this.Close();
                cw.Show(); 
            }
            catch(ApiException) 
            {MessageBox.Show("An error occured please try again", "ERRORE", MessageBoxButton.OK, MessageBoxImage.Error);}
        }
        public string FoodBox{get;set;}
        public string MovieBox{get;set;}
        public string ActivityBox{get;set;}
        
    }
}

