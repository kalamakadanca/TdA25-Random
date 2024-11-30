using System.Text.Json.Serialization;
using TourDeApp.Infrastructure;

namespace TourDeApp.Models.JsonModels
{
    public class BoardStateJson
    {
        public string[][] Board { get; set; }
        
        [JsonIgnore]
        private const int _length = 15;
        
        public string? IsBoardValid()
        {
            Board = Board.Select(x => x.Select(s => s.ToUpper())
                .ToArray())
                .ToArray();

            
            int countX = Board.Sum(row => row.Count(item => item == GlobalSettings.PossibleBoardContent[1]));
            int countO = Board.Sum(row => row.Count(item => item == GlobalSettings.PossibleBoardContent[2]));

            if (countX < countO) 
                return "invalid starting player";
            if (Board.Length != _length)
                return "invalid board size";
            if (!Board.All(x => x.All(y => GlobalSettings.PossibleBoardContent.Contains(y))))
                return "board can contain only ' ', 'X' and 'O' characters";
            
            return null;
        }
    }
}
