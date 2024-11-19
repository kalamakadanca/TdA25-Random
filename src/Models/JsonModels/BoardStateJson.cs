using System.Text.Json.Serialization;

namespace TourDeApp.Models.JsonModels
{
    public class BoardStateJson
    {
        public string[][] Board { get; set; }
        
        [JsonIgnore]
        private readonly string[] _boardInputEnum = [" ", "X", "O"];
        [JsonIgnore]
        private const int _lenght = 15;
        
        public string? IsBoardValid()
        {
            Board = Board.Select(x => x.Select(s => s.ToUpper())
                .ToArray())
                .ToArray();

            if (Board.Length != _lenght)
                return "board lenght is not valid";
            else if (!Board.All(x => x.All(y => _boardInputEnum.Contains(y))))
                return "board can contain only ' ', 'X' and 'O' characters";
            else
                return null;
        }
    }
}
