using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Neddit;

using shared.Model;
using Data;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        var AllowCors = "_AllowCors";
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: AllowCors, builder => {
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
        
        var app = builder.Build();
        app.UseCors(AllowCors);
        var db = new Context();
        
        User newUser = new User("Nicolaj");
        //db.Add(newUser);
        
        Console.WriteLine(JsonSerializer.Serialize(newUser));
        Console.WriteLine(JsonSerializer.Serialize(new ThreadPost(newUser, "Header", "Text")));
        Console.WriteLine(JsonSerializer.Serialize(new Comment(newUser, "This is a comment")));
        
        //Threads
        app.MapGet("/api/threads", () =>
        {
            return db.Threads.Include(u => u.user)
                .Include(c => c.comments)
                .ThenInclude(uc => uc.user);
        });
        
        app.MapGet("/api/thread/{id}", (int id) =>
        {
            var thread = db.Threads
                .Include(t => t.user)
                .Include(t => t.comments)
                .ThenInclude(uc => uc.user)
                .SingleOrDefault(t => t.Id == id);

            return thread;
        });

        app.MapPost("/api/threads", (ThreadPost thread) =>
        {
            db.Threads.Add(thread);
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

            return Results.Ok(comment);
        });
        
        //Thread - Likes/Dislikes
        app.MapPut("/api/threads/like/{id}", (int id) =>
        {
            var thread = db.Threads.SingleOrDefault(t => t.Id == id);
            if (thread == null)
            {
                return Results.NotFound("Thread not found");
            }

            thread.votes++;
            db.SaveChanges();

            return Results.Ok();
        });
        
        app.MapPut("/api/threads/dislike/{id}", (int id) =>
        {
            var thread = db.Threads.SingleOrDefault(t => t.Id == id);
            if (thread == null)
            {
                return Results.NotFound("Thread not found");
            }

            thread.votes--;
            db.SaveChanges();

            return Results.Ok();
        });
        
        //Comment - Likes/Dislikes
        app.MapPut("/api/comments/like/{id}", (int id) =>
        {
            var comment = db.Comments.SingleOrDefault(c => c.Id == id);
            if (comment == null)
            {
                return Results.NotFound("Comment not found");
            }

            comment.votes++;
            db.SaveChanges();

            return Results.Ok();
        });
        
        app.MapPut("/api/comments/dislike/{id}", (int id) =>
        {
            var comment = db.Comments.SingleOrDefault(c => c.Id == id);
            if (comment == null)
            {
                return Results.NotFound("Comment not found");
            }

            comment.votes--;
            db.SaveChanges();

            return Results.Ok();
        });

        app.Run();
    }
}