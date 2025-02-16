using Microsoft.AspNetCore.Identity;

namespace TourDeApp.Models;

public class User : IdentityUser
{
    private int _elo = 0;
    public int Elo { get { return _elo; } set { if (value < 0) { _elo = 0; } else { _elo = value; } } }
    public int Wins { get; set; }
    public int Draws { get; set; }
    public int Losses { get; set; }

    // Call after a game between two player finishes (not normal local game)
    public void UpdateELO(float gameResult, int opponentElo)
    {
        int expectedResult = CalculateExpectedResult(Elo, opponentElo);
        double winDrawRatio = CalculateWinDrawRatio();
        
        double eloIncrease = Elo + 40 * ((gameResult - expectedResult) * (1 + 0.5 * (0.5 - winDrawRatio)));

        Elo += (int) Math.Ceiling(eloIncrease);
    }

    private int CalculateExpectedResult(int elo, int opponentElo)
    {
        return 1 / 1 + 10 * ((opponentElo - elo) / 400);
    }

    private double CalculateWinDrawRatio()
    {
        return (Wins + Draws) / (Wins + Draws + Losses);
    }
}