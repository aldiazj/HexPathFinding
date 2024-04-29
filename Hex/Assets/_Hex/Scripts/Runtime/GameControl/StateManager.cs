using Hex.Runtime.GameControl.States;

namespace Hex.Runtime.GameControl
{
    public class StateManager<T> where T : GameState
    {
        public T Current { get; private set; }

        public void SwitchToState(T newState)
        {
            if (Current != null && Current.GetType() == newState.GetType())
            {
                return;
            }

            Current = newState;
        }
    }
}