namespace Neddit;

using Model;
using Data;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();
        
        /*
        using (var db = new Context())
        {
            Console.WriteLine($"Database path: {db.DbPath}.");
    
            // Create
            Console.WriteLine("Indsæt et nyt thread");
            db.Add(new ThreadPost());
            db.SaveChanges();

            // Read
            Console.WriteLine("Find det sidste thread");
            var lastThread = db.Threads
                .OrderBy(b => b.threadId)
                .Last();
            Console.WriteLine($"Text: {lastThread.text}");
        }
        */
        app.MapGet("/", () => "Hello World!");

        app.Run();
    }
}