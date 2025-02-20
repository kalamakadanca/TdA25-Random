using Microsoft.AspNetCore.Identity;

namespace TourDeApp.Models;

public class User : IdentityUser
{
    private int _elo = 0;
    public int Elo 
    { 
        get => _elo;
        set => _elo = value < 0 ? 0 : value;
    }

    public int Wins { get; set; } = 0;
    public int Draws { get; set; } = 0;
    public int Losses { get; set; } = 0;

    private const int SCALING_FACTOR = 400;
    private const int K_FACTOR = 40;
    private const float ALPHA = 0.5f;

    public void UpdateELO(float gameResult, int opponentElo)
    {
        double winDrawRatio;
        
        int expectedResult = CalculateExpectedResult(Elo, opponentElo);
        
        // Check for zero division
        try
        {
            winDrawRatio = CalculateWinDrawRatio();
        }
        catch
        {
            winDrawRatio = 0;
        }
        
        double eloIncrease = Elo + K_FACTOR * ((gameResult - expectedResult) * (1 + ALPHA * (ALPHA - winDrawRatio)));

        Elo += (int) Math.Ceiling(eloIncrease);
    }

    private int CalculateExpectedResult(int elo, int opponentElo) => 1 / 1 + 10 * ((opponentElo - elo) / SCALING_FACTOR);

    // ReSharper disable once PossibleLossOfFraction
    private double CalculateWinDrawRatio() => (Wins + Draws) / (Wins + Draws + Losses);
}