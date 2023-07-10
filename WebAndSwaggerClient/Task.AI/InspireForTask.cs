namespace Task.AI;
using OpenAI_API;
using OpenAI_API.Completions;
public static class InspireForTask
{
    public static OpenAIAPI openai = new OpenAIAPI(My.Key);
    public static CompletionRequest completionRequest = new CompletionRequest();
    public static async Task<string>  WithPoem(string name)
    {
        completionRequest.Prompt = "یک شعر از مولوی به من بده که مفهوم تسک زیر را برساند"+"حتمن در ان کلمه ای از تسک زیر به کار رفته باشد"
        + "   تسک:" + $"{name}" +"حتمن شعر دارای وزن و فافیه مناسب باشد " +" لطفن جواب را در قالب زیر به من بده"
        + " اسم شعر:   "
        + " متن شعر:   "
        + " اسم شاعر:   ";
        return await ProcessOutput();
    }

    public static async Task<string>  Withinterest(string interestSubject,string interest,string name)
    {
        completionRequest.Prompt ="I have to do the following task"+$"({name})"+" but I'm not in the mood "+"I very like"+$"{interestSubject}"
        +$"{interest}"+"and my interest is"+$"{interest}"+"Give me enough motivation to do the following task"+ $"({name})"+"with my interest"
        +"\n Task:"+$"{name}";
        return await ProcessOutput();
    }
    public static async Task<string> WithBreakTask(string name,int subTaskMinute)
    {
        completionRequest.Prompt = $"break down the following task into {subTaskMinute} subtasks.use the Json template below: "
            + "[\n"
            + "    {\n"
            + "        \"taskNumber\": 1,\n"
            + "        \"taskName\": \"name_1\",\n"
            + "        \"taskDescription\": \"description_1\"\n"
            + "    },\n"
            + "    {\n"
            + "        \"taskNumber\": 2,\n"
            + "        \"taskName\": \"name_2\",\n"
            + "        \"taskDescription\": \"description_2\"\n"
            + "    }\n"
            + "]\n"
            + $"The task is: {name}";
        return await ProcessOutput();
    }
    private static async Task<string>ProcessOutput()
    {
        string outputResult = "";
        completionRequest.Model = OpenAI_API.Models.Model.DavinciText;
        completionRequest.MaxTokens = 1024;

        var completions = await openai.Completions.CreateCompletionAsync(completionRequest);

        foreach (var completion in completions.Completions)
        {
            outputResult += completion.Text;
        }

        return outputResult;
    }
}
