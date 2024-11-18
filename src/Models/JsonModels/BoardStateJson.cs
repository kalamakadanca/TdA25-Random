using TourDeApp.Models.Schemas;

namespace TourDeApp.Models.JsonModels
{
    public class BoardStateJson
    {
        public string[][] Board { get; set; }
        private readonly string[] _boardInputEnum = [" ", "X", "O"];

        public bool IsBoardValid() => Board.All(x => x.All(y => _boardInputEnum.Contains(y)));
    }
}
