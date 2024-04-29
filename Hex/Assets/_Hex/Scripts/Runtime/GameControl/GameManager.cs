using Hex.Runtime.GameControl.States;
using Hex.Runtime.Level;
using Hex.Runtime.UI;
using UnityEngine;

namespace Hex.Runtime.GameControl
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject cellPrefab;
        [SerializeField, Range(3,10),] private int width;
        [SerializeField, Range(3,10),] private int height;
        [SerializeField] private UIManager uiManager;

        public IMap Map { get; private set; }
        public ICell StartCell { get; set; }
        public ICell EndCell { get; set; }
        private StateManager<GameState> StateManager { get; set; }
        private CellSelection lastSelectionState;

        private void Awake()
        {
            Map = new HexMap();
            Map.Init(width, height, cellPrefab, OnCellClicked);
            StateManager = new StateManager<GameState>();
            CellSelection startingState = new CellStartSelection();
            startingState.Initialize(this);
            StateManager.SwitchToState(startingState);
            uiManager.Init(onStartCellPressed: OnCellSelectionPressed<CellStartSelection>,
                onEndCellPressed: OnCellSelectionPressed<CellEndSelection>,
                onObstacleCellPressed: OnCellSelectionPressed<CellObstaclesSelection>,
                onFindPathPressed: FindPath,
                onClearPressed: ClearMap);
            lastSelectionState = startingState;
        }

        private void ClearMap()
        {
            uiManager.ClearResult();
            StartCell = null;
            EndCell = null;
            uiManager.SetFindAvailability(false);
            foreach (ICell cell in Map.Grid.Values)
            {
                cell.IsWalkable = true;
                cell.SetColor(Color.white);
            }

            if (StateManager.Current is FindPath)
            {
                StateManager.SwitchToState(lastSelectionState);
            }
        }

        private void FindPath()
        {
            FindPath findPath = new FindPath();
            findPath.Initialize(this);
            StateManager.SwitchToState(findPath);
            findPath.ShowResults();
            uiManager.ShowResult(findPath.GetPathLength());
        }

        private void OnCellClicked(ICell clickedCell)
        {
            if (StateManager.Current is CellSelection cellSelectionState)
            {
                cellSelectionState.OnCellClicked(clickedCell);
                uiManager.SetStartCellCoordinates(StartCell != null ? StartCell.ToString(): "");
                uiManager.SetEndCellCoordinates(EndCell != null ? EndCell.ToString(): "");
            }

            uiManager.SetFindAvailability(StartCell != null && EndCell != null);
        }

        private void OnCellSelectionPressed<T>() where T : CellSelection, new()
        {
            if (StateManager.Current is FindPath)
            {
                ClearMap();
            }
            T newCellSelectionState = new T();
            newCellSelectionState.Initialize(this);
            StateManager.SwitchToState(newCellSelectionState);
            lastSelectionState = newCellSelectionState;
        }
    }
}