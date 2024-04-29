using System;
using Hex.Runtime.Level.CoordinateSystems;
using UnityEngine;

namespace Hex.Runtime.Level
{
    public interface ICell
    {
        public bool IsWalkable { get; set; }
        public float DistanceToStartNode { get; set; }
        public float DistanceToTargetNode { get; set; }
        public float TotalCost { get; }
        public (int, int)[] Neighbors { get; }
        public ICell Connection { get; set; }
        public void Init(Axial coordinates, Action<ICell> onCellClicked);
        public void SetColor(Color newColor);
        public float GetDistanceTo(ICell targetCell);
    }
}