using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

using shared.Model;

namespace Neddit_app.Data;

public class ApiService
{
    private readonly HttpClient http;
    private readonly IConfiguration configuration;
    private readonly string baseAPI = "";

    public ApiService(HttpClient http, IConfiguration configuration)
    {
        this.http = http;
        this.configuration = configuration;
        this.baseAPI = configuration["base_api"];
    }

    public async Task<ThreadPost[]> GetPosts()
    {
        string url = $"{baseAPI}threads/";
        return await http.GetFromJsonAsync<ThreadPost[]>(url);
    }

    public async Task<ThreadPost> GetPost(int id)
    {
        string url = $"{baseAPI}thread/{id}/";
        return await http.GetFromJsonAsync<ThreadPost>(url);
    }

    public async Task<Comment> CreateComment(int postId, Comment comment)
    {
        string url = $"{baseAPI}threads/{postId}";

        HttpResponseMessage msg = await http.PostAsJsonAsync(url, comment);

        string responseContent = await msg.Content.ReadAsStringAsync();

        Comment newComment = await msg.Content.ReadFromJsonAsync<Comment>();
        
        return newComment;
    }

    public async Task UpvotePost(int id)
    {
        string url = $"{baseAPI}threads/like/{id}";
        
        HttpResponseMessage response = await http.PutAsync(url, null);
    }

    
    public async Task DownvotePost(int id)
    {
        string url = $"{baseAPI}threads/dislike/{id}";
        
        HttpResponseMessage response = await http.PutAsync(url, null);
    }
    
    public async Task UpvoteComment(int id)
    {
        string url = $"{baseAPI}comments/like/{id}";
        
        HttpResponseMessage response = await http.PutAsync(url, null);
    }
    
    public async Task DownvoteComment(int id)
    {
        string url = $"{baseAPI}comments/dislike/{id}";
        
        HttpResponseMessage response = await http.PutAsync(url, null);
    }
}
