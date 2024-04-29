using Hex.Runtime.Level;

namespace Hex.Runtime.GameControl.States
{
    public abstract class GameState
    {
        public abstract void Initialize(GameManager gameManagerReference);
    }
}