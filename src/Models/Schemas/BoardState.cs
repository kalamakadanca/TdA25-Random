namespace TourDeApp.Models.Schemas
{
    public class BoardState
    {
        public string[,] Board { get; set; }


        public BoardState(int size = 15)
        {
            Board = new string[size, size];

            // Nastaví všechny hodnoty na nic (those who know)
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Board[i, j] = "";
                }
            }
        }
    }
}
