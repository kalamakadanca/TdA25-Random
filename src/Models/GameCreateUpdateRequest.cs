namespace TourDeApp.Models
{
    public class GameCreateUpdateRequest
    {
        public string Name { get; set; }
        public DifficultyType Difficulty { get; set; }
        public string[][] Board { get; set; }
    }
}
