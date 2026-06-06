// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
// Account account = new Account("aliya","agbetoba@gmail.com");
// Console.WriteLine(account.Display());
// Console.WriteLine(account.GetName());
// Console.WriteLine(account.GetLearningId());

// Console.WriteLine(DateTime.Now);

Question question = new  Question("What is your name? ", "Aliya",DateTime.Now);
Thread thread = new Thread(question);

Response response = new Response("I am rainbow!","Nyx",DateTime.Now);
Response response1 = new Response("I am Sunshine!","Irene",DateTime.Now);
thread.AddResponse(response);
thread.AddResponse(response1);
thread.Display();
thread.FilterResponse("Irene");
//thread.FilterResponse("joy");


int x  = 2;
int y = x + 1;
Console.WriteLine(y);