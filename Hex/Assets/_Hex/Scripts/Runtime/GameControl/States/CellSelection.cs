using Hex.Runtime.Level;

namespace Hex.Runtime.GameControl.States
{
    public abstract class CellSelection : GameState
    {
        protected GameManager gameManager;
        public abstract void OnCellClicked(ICell clickedCell);
        public override void Initialize(GameManager gameManagerReference)
        {
            gameManager = gameManagerReference;
        }
    }
}