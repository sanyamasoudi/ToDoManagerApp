namespace Crud.Test;
[TestClass]
public class WebTest
{
    Table tableTest;
    CRUD crudTest;
    private static void GetEmptyData(out Table tableTest, out CRUD crudTest)
    {
        tableTest = new Table();
        crudTest = new CRUD(tableTest);
        crudTest.RemoveAllToDos("sanya");
    }
    [TestMethod]
    public  void TestCreateUserAndIsUserIdentify()
    {
        using Table tableTest=new Table();
        CRUD crudTest=new CRUD(tableTest);
        crudTest.CreatUser("Dorsa",2233);
        var IsLogined=crudTest.IsUserIdentify("Dorsa",2233);
        Assert.AreEqual(IsLogined,true); 
    }
    [TestMethod]
    public  void TestCreatToDo()
    {
        GetEmptyData(out tableTest, out crudTest);
        crudTest.CreatToDo("Task1", "Description1", true, "sanya");
        var result = crudTest.GetToDos("sanya");
        Assert.AreEqual(result[result.Count - 1].Subject, "Task1");
        Assert.AreEqual(result[result.Count - 1].Description, "Description1");
        Assert.AreEqual(result[result.Count - 1].IsCompleted, true);
        Assert.AreEqual(result[result.Count - 1].Username, "sanya");
    }

    [TestMethod]
    public  void TestRemoveToDo()
    {
        GetEmptyData(out tableTest, out crudTest);
        crudTest.CreatToDo("Task1","Description1",true,"sanya");
        crudTest.RemoveToDo("Task1","Description1","sanya");
        var result=crudTest.GetToDos("sanya");
        bool IsntRemove=false;
        foreach(var i in result)
            if(i.Subject=="Task1" && i.Description=="Description1" && i.IsCompleted==true)
                IsntRemove=true;
        Assert.AreEqual(IsntRemove,false);
    }
    [TestMethod]
    public  void TestCompletedAndInCompletedToDos()
    {
        GetEmptyData(out tableTest, out crudTest);
        crudTest.CreatToDo("Task1","Description1",false,"sanya");
        crudTest.CreatToDo("Task2","Description2",true,"sanya");
        crudTest.CreatToDo("Task3","Description3",false,"sanya");
        crudTest.CreatToDo("Task4","Description4",true,"sanya");
        var Cresult=crudTest.GetCompletedToDos("sanya");
        var ICresult=crudTest.GetInCompletedToDos("sanya");
        Assert.AreEqual(Cresult[0].Subject,"Task2");
        Assert.AreEqual(Cresult[1].Subject,"Task4");

        Assert.AreEqual(ICresult[0].Subject,"Task1");
        Assert.AreEqual(ICresult[1].Subject,"Task3");
    }
    [TestMethod]
    public  void TestMarkToDoComplete()
    {
        GetEmptyData(out tableTest, out crudTest);
        crudTest.CreatToDo("Task1","Description1",false,"sanya");
        crudTest.MarkToDoAsComplete("Task1","sanya");
        var result=crudTest.GetToDos("sanya");
        Assert.AreEqual(result[result.Count-1].IsCompleted,true);
    }
    [TestMethod]
    public  void TestMarkToDoInComplete()
    {
        GetEmptyData(out tableTest, out crudTest);
        crudTest.CreatToDo("Task1","Description1",true,"sanya");
        crudTest.MarkToDoAsNotComplete("Task1","sanya");
        var result=crudTest.GetToDos("sanya");
        Assert.AreEqual(result[result.Count-1].IsCompleted,false);
    }

}