using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hex.Runtime.Level
{
    public interface IMap
    {
        public Dictionary<(int, int), ICell> Grid { get; }
        public void Init(int width, int height, GameObject cellPrefab, Action<ICell> onCellClicked);
        public IEnumerable<ICell> GetExistingCellsAt(IEnumerable<(int, int)> coordinates);
    }
}