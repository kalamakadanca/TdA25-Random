﻿namespace TourDeApp.Components.Models.Schemas
{
    public class Cell
    {
        public CellState State { get; set; }

        public Cell()
        {
            State = CellState.empty;
        }
    }
}
