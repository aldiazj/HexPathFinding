using Hex.Runtime.Level;
using UnityEngine;

namespace Hex.Runtime.GameControl.States
{
    public class CellEndSelection : CellSelection
    {
        public override void OnCellClicked(ICell clickedCell)
        {
            if (gameManager.StartCell == clickedCell)
            {
                return;
            }

            if (gameManager.EndCell != null)
            {
                gameManager.EndCell.SetColor(Color.white);
            }

            gameManager.EndCell = clickedCell;
            clickedCell.SetColor(Color.green);
        }
    }
}