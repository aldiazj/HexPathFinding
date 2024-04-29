using Hex.Runtime.Level;
using UnityEngine;

namespace Hex.Runtime.GameControl.States
{
    public class CellObstaclesSelection : CellSelection
    {
        public override void OnCellClicked(ICell clickedCell)
        {
            clickedCell.IsWalkable = !clickedCell.IsWalkable;
            clickedCell.SetColor(clickedCell.IsWalkable ? Color.white : Color.black);
        }
    }
}