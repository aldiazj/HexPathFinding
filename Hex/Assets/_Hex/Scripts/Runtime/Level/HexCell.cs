using System;
using Hex.Runtime.Level.CoordinateSystems;
using UnityEngine;

namespace Hex.Runtime.Level
{
    public class HexCell : MonoBehaviour, ICell
    {
        private const float SPACING = 0.1f;

        [SerializeField] private SpriteRenderer spriteRenderer;

        private Axial Coordinates { get; set; }

        private Action<ICell> onClicked;

        public bool IsWalkable { get; set; } = true;
        public float DistanceToStartNode { get; set; }
        public float DistanceToTargetNode { get; set; }
        public float TotalCost => DistanceToStartNode + DistanceToTargetNode;
        public ICell Connection { get; set; }
        public (int, int)[] Neighbors { get; private set; }


        public void Init(Axial coordinates, Action<ICell> onCellClicked)
        {
            Coordinates = coordinates;
            Neighbors = new[]
            {
                (coordinates.Q + 1, coordinates.R),
                (coordinates.Q + 1, coordinates.R - 1),
                (coordinates.Q, coordinates.R - 1),
                (coordinates.Q - 1, coordinates.R),
                (coordinates.Q - 1, coordinates.R + 1),
                (coordinates.Q, coordinates.R + 1),
            };
            onClicked = onCellClicked;
            transform.position = coordinates.GetCartesianPos(transform.localScale.x / 2 + SPACING);
        }

        public void SetColor(Color newColor)
        {
            spriteRenderer.color = newColor;
        }

        public float GetDistanceTo(ICell targetCell)
        {
            HexCell targetCellAsHex = (HexCell)targetCell;
            return Coordinates.GetDistance(targetCellAsHex.Coordinates);
        }

        private void OnMouseDown()
        {
            onClicked?.Invoke(this);
        }
    }
}