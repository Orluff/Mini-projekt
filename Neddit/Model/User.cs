using System.ComponentModel.DataAnnotations;

namespace Neddit.Model;

public class User
{
    public float userId { get; set; }
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