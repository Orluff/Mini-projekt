using System.ComponentModel.DataAnnotations;

namespace Neddit.Model;

public class ThreadPost
{
    public float threadId { get; set; }
    
    [Required]
    public float userId { get; set; }
    [Required]
    public string header { get; set; }
    public string text { get; set; }
    public DateTime date { get; set; }
    public int votes { get; set; }
    public List<Comment> comments { get; set; }
    
    public ThreadPost()
    {
        
    }
}