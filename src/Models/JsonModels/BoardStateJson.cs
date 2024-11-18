using TourDeApp.Models.Schemas;

namespace TourDeApp.Models.JsonModels
{
    public class BoardStateJson : BoardState
    {
        public new Cell[][] Board { get; set; }
    }
}
