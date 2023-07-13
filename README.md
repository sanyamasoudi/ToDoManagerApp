# ToDoManagerApp
# IUST Advanced Programming Project

The Todo Manager is a application built using c#, SgnalR, database, OpenAi. It allows users to create, read, update, and delete their tasks or to-dos. The application has a login system that ensures only authenticated users can access their to-dos.

This project is part of the "Advanced Programming" course by Dr.Sauleh Etemadi on IUST.

- You must use .net8 or change target framwork and packages versions to correctly work
- You must run api (WebAndSwaggerClient\Web) and after that run clients
- For usen ChatGPT part  in clients you must have api key and insert it in WebAndSwaggerClient\Task.AI\MyKey.cs

## Technologies used

- C#
- OpenAi
- SQL
- SignalR
  
## Clients:

- Cmd
- BlazorServer
- BlazorWasm
- Wpf

## Features

- User login and authentication system
- Create new tasks
- View existing tasks
- Edit task details
- Delete tasks
- Inspire for tasks with ChatGPT
- BreakTask

## Getting started

1. Clone the repository 
```
git clone https://github.com/sanyamasoudi/ToDoManagerApp.git
```

2. Go to WebAndSwaggerClient\Web and write this in cmd then go to each of clients And run it
  ```
 dotnet run
  ```
3. For blazorServer and blazorwasm open your web browser and go to `http://localhost:5229` , `http://localhost:5158`

## How to use

1. Register or login to your account
2. Create new tasks by clicking on "AddToDo" button
3. Delete tasks using the "DeleteToDo" button
4. Show existing tasks using the "ShowToDo" button
5. Use openAi using the "ChatGPT" button and then complete your informations
6. Break task to SubTasks using "Breaktask" button and Inspire for do your task By Inspires buttons
   
[You-Can-See-Images-About-My-Project-In-MyBlog-With-Some-Persion-Explanation](https://sanyamasoudi.github.io/sanyamasoudi/CreateMyFirstApp-post/)
<p><img src="../assets/ToDoManagerAppImages/1.png" alt="&quot;ٌwpfClient1-finalProject&quot;">
<img src="../assets/ToDoManagerAppImages/2.png" alt="&quot;wpfClient2-finalProject&quot;">
<img src="../assets/ToDoManagerAppImages/3.png" alt="&quot;wpfClient3-finalProject&quot;">
<img src="../assets/ToDoManagerAppImages/4.png" alt="&quot;wpfClient4-finalProject&quot;"></p>

<img decoding="async" data-attachment-id="45" data-permalink="https://sunblogi.wordpress.com/2023/03/25/what-is-coding/giphy-1/" data-orig-file="https://sunblogi.files.wordpress.com/2023/03/giphy-1.gif" data-orig-size="450,270" data-comments-opened="1" data-image-meta="{&quot;aperture&quot;:&quot;0&quot;,&quot;credit&quot;:&quot;&quot;,&quot;camera&quot;:&quot;&quot;,&quot;caption&quot;:&quot;&quot;,&quot;created_timestamp&quot;:&quot;0&quot;,&quot;copyright&quot;:&quot;&quot;,&quot;focal_length&quot;:&quot;0&quot;,&quot;iso&quot;:&quot;0&quot;,&quot;shutter_speed&quot;:&quot;0&quot;,&quot;title&quot;:&quot;&quot;,&quot;orientation&quot;:&quot;0&quot;}" data-image-title="giphy-1" data-image-description="" data-image-caption="" data-medium-file="https://sunblogi.files.wordpress.com/2023/03/giphy-1.gif?w=300" data-large-file="https://sunblogi.files.wordpress.com/2023/03/giphy-1.gif?w=450" src="https://sunblogi.files.wordpress.com/2023/03/giphy-1.gif?w=617" alt="" class="wp-image-45" width="617" height="370" srcset="https://sunblogi.files.wordpress.com/2023/03/giphy-1.gif?w=617&amp;zoom=2 1.5x" src-orig="https://sunblogi.files.wordpress.com/2023/03/giphy-1.gif?w=450" scale="1.5">
