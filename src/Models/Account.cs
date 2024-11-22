namespace TourDeApp.Models;

public class Account
{
    public string Username { get; set; }
    public string Password { get; set; }
    
    // TODO: Store games created by account
    // public Game[} Games { get; set; }

    public Account(string username, string password)
    {
        Username = username;
        Password = password;
    }
}