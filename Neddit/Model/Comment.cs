using System.ComponentModel.DataAnnotations;

namespace Neddit.Model;

public class Comment
{
    public int Id { get; set; }
    [Required]
    public User user { get; set; }
    [Required]
    public string text { get; set; }
    public DateTime date { get; set; }
    public int votes { get; set; }
    
    public Comment()
    {
        date = DateTime.Now;
    }

    public Comment(User User, string Text)
    {
        date = DateTime.Now;
        this.text = Text;
        this.user = User;
    }
}