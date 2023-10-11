using System.ComponentModel.DataAnnotations;

namespace Neddit.Model;

public class Comment
{
    public float Id { get; set; }
    [Required]
    public float userId { get; set; }
    [Required]
    public string text { get; set; }
    public DateTime date { get; set; }
    public int votes { get; set; }
    
    public Comment()
    {
        
    }
}