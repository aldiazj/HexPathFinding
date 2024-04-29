using System;
using System.Collections.Generic;
using Hex.Runtime.Level.CoordinateSystems;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Hex.Runtime.Level
{
    public class HexMap : IMap
    {
        public Dictionary<(int, int), ICell> Grid { get; } = new Dictionary<(int, int), ICell>();

        public void Init(int width, int height, GameObject cellPrefab, Action<ICell> onCellClickedCallback)
        {
            Transform gridContainer = new GameObject("Grid").transform;

            for (int r = 0; r < height; r++)
            {
                int rOffset = r >> 1;
                for (int q = -rOffset; q < width - rOffset; q++)
                {
                    GameObject instantiatedCellGameObject = Object.Instantiate(cellPrefab, gridContainer);
                    ICell newCell = instantiatedCellGameObject.GetComponent<ICell>();
                    newCell.Init(new Axial(q, r), onCellClickedCallback, q+rOffset);
                    Grid.Add((q, r), newCell);
                }
            }
        }

        public IEnumerable<ICell> GetExistingCellsAt(IEnumerable<(int, int)> coordinates)
        {
            IList<ICell> cells = new List<ICell>();
            foreach ((int, int) coordinate in coordinates)
            {
                if (Grid.TryGetValue(coordinate, out ICell cell))
                {
                    cells.Add(cell);
                }
            }

            return cells;
        }
    }
}