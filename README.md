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

<p><img src="https://sanyamasoudi.github.io/sanyamasoudi/assets/ToDoManagerAppImages/1.png" alt="&quot;wpfClient1-finalProject&quot;">
<img src="https://sanyamasoudi.github.io/sanyamasoudi/assets/ToDoManagerAppImages/2.png" alt="&quot;wpfClient2-finalProject&quot;">
<img src="https://sanyamasoudi.github.io/sanyamasoudi/assets/ToDoManagerAppImages/3.png" alt="&quot;wpfClient3-finalProject&quot;">
<img src="https://sanyamasoudi.github.io/sanyamasoudi/assets/ToDoManagerAppImages/4.png" alt="&quot;wpfClient4-finalProject&quot;"></p>
  
