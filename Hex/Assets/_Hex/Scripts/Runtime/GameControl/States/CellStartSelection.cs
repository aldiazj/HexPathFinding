using Hex.Runtime.Level;
using UnityEngine;

namespace Hex.Runtime.GameControl.States
{
    public class CellStartSelection : CellSelection
    {
        public override void OnCellClicked(ICell cell)
        {
            if (gameManager.EndCell == cell)
            {
                return;
            }

            if (gameManager.StartCell != null)
            {
                gameManager.StartCell.SetColor(Color.white);
            }
            gameManager.StartCell = cell;
            cell.SetColor(Color.blue);
        }
    }
}