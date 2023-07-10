// See https://aka.ms/new-console-template for more information
using Swagger;


using HttpClient httpClient=new HttpClient();
swaggerClient client=new swaggerClient("http://localhost:5292",httpClient);
await client.InfoToListAsync("walking","rice and fish","harry potter","helia");
var response=await client.InspireFoodAsync("helia","study math");
Console.WriteLine(response);