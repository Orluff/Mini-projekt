using System.ComponentModel.DataAnnotations;

namespace Neddit.Model;

public class ThreadPost
{
    public int Id { get; set; }
    
    [Required]
    public User user { get; set; }
    [Required]
    public string header { get; set; }
    public string text { get; set; }
    public DateTime date { get; set; }
    public int votes { get; set; }
    public List<Comment> comments { get; set; } = new List<Comment>();
    
    public ThreadPost()
    {
        date = DateTime.Now;
    }
    
    public ThreadPost(User User, string Header, string Text)
    {
        this.user = User;
        this.header = Header;
        this.text = Text;
        date = DateTime.Now;
    }
}