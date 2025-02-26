using TourDeApp.Models;
using System.Timers;
using Timer = System.Timers.Timer;

namespace TourDeApp.Components.Services;

public class MatchmakingService
{
    private List<User> users;
    private static Timer timer;
    
    public MatchmakingService()
    {
        timer = new Timer(5000);
        timer.Elapsed += FindMatches;
        timer.AutoReset = true;
        timer.Start();
    }
    
    private void AddToQueue(User user) => users.Add(user);

    private void FindMatches(object sender, ElapsedEventArgs e)
    {
        if (users.Count < 2) return;

        users = users.OrderBy(p => p.Elo).ToList();

        for (int i = 0; i < users.Count - 1; i += 2)
        {
            string[] match = new string[] { users[0].Email, users[1].Email };
        }
    }
}