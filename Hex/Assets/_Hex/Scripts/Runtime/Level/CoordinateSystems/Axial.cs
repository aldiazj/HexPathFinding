using UnityEngine;

namespace Hex.Runtime.Level.CoordinateSystems
{
    public readonly struct Axial
    {
        private static readonly float SQRT3 = Mathf.Sqrt(3);

        public int Q { get; }
        public int R { get; }

        public Axial(int q, int r)
        {
            Q = q;
            R = r;
        }

        public float GetDistance(Axial other) => (this - other).AxialDistance();

        private float AxialDistance()
        {
            return (Mathf.Abs(Q) + Mathf.Abs(Q + R) + Mathf.Abs(R)) * 0.5f;
        }

        public static Axial operator -(Axial a, Axial b)
        {
            return new Axial(a.Q - b.Q, a.R - b.R);
        }

        public Vector2 GetCartesianPos(float size)
        {
            float x = size * (SQRT3 * Q + SQRT3 / 2f * R);
            float y = size * (3f / 2f * R);
            return new Vector2(x, y);
        }
    }
}