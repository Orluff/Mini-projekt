using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Neddit;

using Model;
using Data;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();
        var db = new Context();
        
        User newUser = new User("Nicolaj");
        db.Add(newUser);
        
        Console.WriteLine(JsonSerializer.Serialize(newUser));
        Console.WriteLine(JsonSerializer.Serialize(new ThreadPost(newUser, "Header", "Text")));
        Console.WriteLine(JsonSerializer.Serialize(new Comment(newUser, "This is a comment")));
        
        /*
        using (var db = new Context())
        {
    
            // Create
            Console.WriteLine("Indsætter et nyt thread");
            db.Add(new ThreadPost(newUser, "Header", "Random Text"));
            db.SaveChanges();

            // Read
            Console.WriteLine("Find det sidste thread");
            var lastThread = db.Threads
                .OrderBy(t => t.Id)
                .Last();
            Console.WriteLine($"Text: {lastThread.text}");
        }
        */
        
        //Threads
        app.MapGet("/api/threads", () =>
        {
            return db.Threads.Include(u => u.user)
                .Include(c => c.comments)
                .ThenInclude(uc => uc.user);
        });

        app.MapPost("/api/threads", (ThreadPost thread) =>
        {
            db.Add(thread);
            db.SaveChanges();
        });
        
        //Users
        app.MapGet("/api/users", () =>
        {
            return db.Users;
        });
        
        app.MapPost("/api/users", (User user) =>
        {
            db.Add(user);
            db.SaveChanges();
        });
        
        //Comments
        app.MapGet("/api/comments", () =>
        {
            return db.Comments.Include(u => u.user);
        });
        
        app.MapPost("/api/threads/{id}", (int id, Comment comment) =>
        {
            var thread = db.Threads.SingleOrDefault(t => t.Id == id);
            if (thread == null)
            {
                return Results.NotFound("Thread not found");
            }

            thread.comments.Add(comment);
            db.SaveChanges();
            
            return Results.Created($"/api/threads/{id}", comment);
        });

        app.Run();
    }
}