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
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public async void Login_Click(object sender, RoutedEventArgs e)
        {

            bool userfound=false;
            try
            {
                userfound=await SwaggerClient.Methods.LoginAsync(this.UserNameBox,this.PassWordBox);
                if (userfound)  
                {  
                    MainWindow mw=new();
                    this.Close();
                    mw.Show();
                }  
                else  
                {  
                    MessageBox.Show("UserName or Password is not Valid", "ERRORE", MessageBoxButton.OK, MessageBoxImage.Error);  
                }  
            }
            catch(ApiException) //catch ApiException is to user dont exit from app if server not connected
            {MessageBox.Show("An error occured please try again", "ERRORE", MessageBoxButton.OK, MessageBoxImage.Error);}

        }

        public async void Register_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await SwaggerClient.Methods.RegisterAsync(this.UserNameBox,this.PassWordBox);
                MessageBox.Show("SuccessFully Added");
            }
            catch(ApiException)
            { MessageBox.Show("An error occured please try again", "ERRORE", MessageBoxButton.OK, MessageBoxImage.Error);}

        }
        //this method for passwordbox to just get integer as input
        public void ValidNumber(object sender, TextCompositionEventArgs args)
        {
            args.Handled = false;
            foreach (var item in args.Text)
            {
                if (!char.IsDigit(item))
                    args.Handled = true;
            }
        }
        string _UserNameBox;
        string _PassWordBox;//i define passwordbox as string to user first open program see the passwordbox empty not 0 in it
        //if you debug and get nullExeception ignore it and press f11 to jump from this part:) (it just for get better ui)
        public string UserNameBox 
        {   get=> _UserNameBox;
            set
            {
                _UserNameBox=value; 
                UserNameSaver.UserName=value;//save the last username that save..this part is usefull for manage todo
                //because each tasks is for a user..so i need name of username to add remove ,..
            }
        }
        public int PassWordBox
        {
            get=>int.Parse(_PassWordBox);
            set
            {
                _PassWordBox=value.ToString();
            }
        }
    }
}

