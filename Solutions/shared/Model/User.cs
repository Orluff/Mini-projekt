using System.ComponentModel.DataAnnotations;

namespace shared.Model;

public class User
{
    public int Id { get; set; }
    [Required]
    public string username { get; set; }

    public User()
    {
        // Parameterless constructor
    }

    public User(string UserName)
    {
        this.username = UserName;
    }
}